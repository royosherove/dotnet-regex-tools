using System;
using System.Windows.Forms;
using mshtml;
using RegexWizard.Framework;

namespace Regulazy.UI
{
    public partial class BrowserInputForm : Form
    {
        public BrowserInputForm()
        {
            InitializeComponent();
        }

        private void browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            lblLoading.Visible = true;
        }

        private void browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            lblLoading.Visible = false;
            txtHtmlView.Text = browser.DocumentText;
            IHTMLDocument2 doc2 = browser.Document.DomDocument as IHTMLDocument2;
//            doc2.designMode = "On";
            
            
        }

        private void cmdGo_Click(object sender, System.EventArgs e)
        {
            try
            {
                txtHtmlView.Text = "Loading HTML...";
                browser.DocumentText = "Loading...";
                Application.DoEvents();
                browser.Navigate(txtUrl.Text);
                Application.DoEvents();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string plainTextSelection = string.Empty;
        private string htmlTextSelection = string.Empty;

        public string FullHtmlText
        {
            get { return fullHtmlText; }
            set { fullHtmlText = value; }
        }

        private string fullHtmlText = string.Empty;

        public string PlainTextSelection
        {
            get { return plainTextSelection; }
            set { plainTextSelection = value; }
        }

        public string HtmlTextSelection
        {
            get { return htmlTextSelection; }
            set { htmlTextSelection = value; }
        }

        private void cmdCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void cmdOK_Click(object sender, System.EventArgs e)
        {
            if(tabControl1.SelectedTab==tabBrowser)
            {
                IHTMLDocument2 doc2 = browser.Document.DomDocument as IHTMLDocument2;
                IHTMLTxtRange range = doc2.selection.createRange() as IHTMLTxtRange;
                plainTextSelection = range.text;
                fullHtmlText = browser.DocumentText;
                
                HTMLInputFinder helper = new HTMLInputFinder();
                helper.SearchTargetHTML = fullHtmlText;
                helper.SearchFor = range.htmlText;
                if(helper.Find())
                {
                    htmlTextSelection = helper.FoundHTML;
                    fullHtmlText = browser.DocumentText;
                }
                else
                {

                    DialogResult userAction = MessageBox.Show("Note: HTML imported may be different from original HTML on the actual web page","Warning",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
                    if(userAction==DialogResult.OK)
                    {
                        plainTextSelection = string.Empty;
                        htmlTextSelection = range.htmlText;
                        fullHtmlText = browser.DocumentText;
                        Close();
                    }
                }
            }
            else
            {
                plainTextSelection = string.Empty;
                htmlTextSelection = txtHtmlView.SelectedText;
                fullHtmlText = browser.DocumentText;
            }
            Close();
            
        }

        /// <summary>
        /// THis method tries to bridge an annoying problem:
        /// The document text on the web browser may have DIFFERENT CASING than that of the selected range
        /// in the web browser. So this method simply gets the text with the REAL casing directly from the browser:
        /// </summary>
        /// <param name="textWithBadCasing"></param>
        /// <param name="browserCtl"></param>
        /// <returns></returns>
        private string GetTextFromDocumentLike(string textWithBadCasing, WebBrowser browserCtl)
        {
            string fullTextWithGoodCasing = browserCtl.DocumentText;
            string fullTextWithGoodCasingLowered = fullTextWithGoodCasing.ToLower();
            fullTextWithGoodCasingLowered = fullTextWithGoodCasing.Replace('"'.ToString(), string.Empty);
            fullTextWithGoodCasingLowered = fullTextWithGoodCasing.Replace('\n'.ToString(), string.Empty);
            
            string textWithBadCasingLowered = textWithBadCasing.ToLower();
            textWithBadCasingLowered = textWithBadCasing.Replace('\n'.ToString(), string.Empty);
            
            int startIndexInFullText = fullTextWithGoodCasingLowered.IndexOf(textWithBadCasingLowered);
            string substringWithoutQuotesAndNewlines = browserCtl.DocumentText.Substring(startIndexInFullText, textWithBadCasing.Length );
            return substringWithoutQuotesAndNewlines;
            
        }
    }
}