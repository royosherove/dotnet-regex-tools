using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using RegexWizard.Framework;
using Regulazy.UI.Properties;

namespace Regulazy.UI
{
    public enum ResultsView
    {
        RawRegex,
        MatchTree
    }
    public partial class RegulazyResults : UserControl
    {
        private Color lastInputBackColor=Color.Empty;
        private Color lastInputForeColor=Color.Empty;
        private Font lastInputFont=null;
        private bool loaded=false;

        public void HighlightInputSample()
        {
            
            lastInputBackColor = InputBackColor;
            InputBackColor = Color.Yellow;
            
            lastInputForeColor= InputForeColor;
            InputForeColor = Color.Black;

            lastInputFont = InputFont;
            InputFont = new Font(lastInputFont, FontStyle.Bold);
        }
        
        public void UnhighlightInputSample()
        {
            
            InputBackColor = lastInputBackColor;
            InputForeColor = lastInputForeColor;
            InputFont = lastInputFont;
        }
        public Color InputBackColor
        {
            get { return txtInputSample.BackColor; }
            set { txtInputSample.BackColor = value; }
        }

        public Color InputForeColor
        {
            get { return txtInputSample.ForeColor; }
            set { txtInputSample.ForeColor = value; }
        }

        public Font InputFont
        {
            get { return txtInputSample.Font; }
            set { txtInputSample.Font = value; }
        }

        public void ResetMatches(string pattern,RegexOptions options)
        {
            try
            {
                Regex regex = new Regex(pattern,options);
                MatchCollection matches = regex.Matches(txtInputSample.Text);
                tllLblResultsCount.Text = matches.Count.ToString();
                matchesTree.ShowMatches(matches, regex);
            }
            catch (Exception)
            {
                
            }
        }
        public RegulazyResults()
        {
            InitializeComponent();
            txtExpression.Text = "No Expression generated yet";

            txtInputSample.HideSelection = false;
            matchesTree.AfterSelect+=new TreeViewEventHandler(MatchesTree_OnAfterSelect);
            
            txtExpression.TextChanged += new EventHandler(txtExpression_TextChanged);
            txtInputSample.SelectionColor=Color.Black;
            tabDebug.Visible = false;
#if DEBUG
            tabDebug.Visible = true;
#endif
            
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            matchesTree.Nodes.Clear();
            txtExpression.Text = "No Expression generated yet";
#if RELEASE
            tabsHolder.TabPages.Remove(tabDebug);
#endif

            loaded = true;
        }

        private void MatchesTree_OnAfterSelect(object sender, TreeViewEventArgs e)
        {
            txtInputSample.HideSelection = false;
            Group currentGroup = e.Node.Tag as Group;
            if (currentGroup==null)
            {
                return;
            }
            txtInputSample.SelectionStart = currentGroup.Index;
            txtInputSample.SelectionLength = currentGroup.Length;

            txtInputSample.ScrollToCaret();
        }

      

        void txtExpression_TextChanged(object sender, EventArgs e)
        {
            RefreshResults();
        }

        public void RefreshResults()
        {
            bool isWhiteSpaceIrelevantToExpressions = optionsToolStrip.IsSet(RegexOptions.IgnorePatternWhitespace);
//            txtExpression.ShowNewLines = !isWhiteSpaceIrelevantToExpressions;
//            txtExpression.ShowSPaces = !isWhiteSpaceIrelevantToExpressions;
//            txtExpression.ShowTabs = !isWhiteSpaceIrelevantToExpressions;
            
            ResetMatches(txtExpression.Text,optionsToolStrip.RegXOptions);
        }

        public RegexOptions RegXOptions
        {
            get { return optionsToolStrip.RegXOptions; }
            set
            {
                optionsToolStrip.RegXOptions = value;
            }
        }

        public string RegexText
        {
            get { return txtExpression.Text; }
            set { txtExpression.Text = value; }
        }

        public ResultsView ActiveView
        {
            get
            {
                if(tabsHolder.SelectedTab==tabRawRegex)
                {
                    return ResultsView.RawRegex;
                }
                else
                {
                    return ResultsView.MatchTree;
                } 
            }
            set
            {
                switch (value)
                {
                    case ResultsView.RawRegex:
                        tabsHolder.SelectedTab = tabRawRegex;
                        break;
                    case ResultsView.MatchTree:
                        tabsHolder.SelectedTab = tabMatchTree;
                        
                        break;
                    default:
                        break;
                }
            }
        }

        public string SampleText
        {
            get
            {
                if(!InvokeRequired)
                {
                    return txtInputSample.Text;
                }
                string retVal=string.Empty;
                Invoke(new ThreadStart(delegate
                                           {
                                               retVal= txtInputSample.Text;
                                           }));
                return retVal;
            }
            set { txtInputSample.Text=value; }
        }

        public void ShowScopeTree(Scope root)
        {
            scopeTree.ShowScope(root);
        }

        public void ShowRects(Scope root, List<Rectangle> rects)
        {
            lstRects.Items.Clear();
            foreach (Rectangle rect in rects)
            {
                lstRects.Items.Add(rect.Width);
            }
        }

        private void matchOptionsToolStrip1_RegexOptionsChanged(object sender, MatchOptionsToolStrip.RegexOptionsEventArgs e)
        {
            RefreshResults();
        }

        public void HighlightScope(Scope s)
        {
            scopeTree.Highlight(s);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            new QuickDemoForm(Resources.Demo_Simple).ShowDialog(this.ParentForm);
        }

        private void optionsToolStrip_Load(object sender, EventArgs e)
        {


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new QuickDemoForm(Resources.Huh_ExpressionPane).ShowDialog(this.ParentForm);

        }

        public void ShowInProgress()
        {
            lblInProgress.Visible = true;
        }

        public void HideInProgress()
        {
            lblInProgress.Visible = false;
        }
    }
}
