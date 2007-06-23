using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using RegexWizard.Framework;
using RegexWizard.TemplateEngine;
using Regulazy.UI.Properties;
using Regulazy.UISupport;

namespace Regulazy.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            txtRegexInput.InputMode = InputModes.StandardEdit;
            txtRegexInput.ShowNewLines = false;
            txtRegexInput.ShowSPaces = false;
            txtRegexInput.ShowTabs = false;
            txtRegexInput.ActiveScopeChanged += new EventHandler<ScopeAwareRichTextBox.ScopeEventArgs>(sampleTextInput1_ActiveScopeChanged);
            txtRegexInput.ExpressionChanged += new EventHandler(OnExpressionChanged);
            txtRegexInput.InputModeChanged += new EventHandler(OnInputModeChanged);
            txtRegexInput.Text = "Put a piece of text you'd like to parse here,\n then press 'Regex Manipulation Mode'";
            tipsScopeEditor.InitialDelay = 3 * 1000;//seconds
        }
        private string lastSampleText = string.Empty;
        
        void OnInputModeChanged(object sender, EventArgs e)
        {

            if (txtRegexInput.InputMode == InputModes.RegexManipulation)
            {
                txtRegexInput.ShowNewLines = true;
                txtRegexInput.ShowSPaces = true;
                txtRegexInput.ShowTabs = true;
                SetSampleTextByUserRequestIfNeeded();
                results.ResetMatches(txtRegexInput.Text, RegexOptions.None);
                results.RegXOptions = RegexOptions.Multiline;
                RefreshResults(true);
            }
        }

        bool skipAskingUserForSample = false;
        
        private void SetSampleTextByUserRequestIfNeeded()
        {
            if (skipAskingUserForSample  
                ||
                (
                    results.SampleText != string.Empty 
                    && 
                    lastSampleText == txtRegexInput.Text)
                )
            {
                return;
            }
            else
            {
                lastSampleText = txtRegexInput.Text;
            }
            results.HighlightInputSample();
            DialogResult result = MessageBox.Show("Would you like to use this text as a sample input to test against?", "Use as Sample?",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            results.UnhighlightInputSample();

            if (result == DialogResult.Yes)
            {
                results.SampleText = txtRegexInput.Text;
            }
            skipAskingUserForSample = false;
        }

        void OnExpressionChanged(object sender, EventArgs e)
        {
            RefreshResults();
        }

        private void RefreshResults()
        {
            RefreshResults(false);
        }
        private void RefreshResultsOLD(bool makeAutoScopes)
        {
            if (DateTime.Now > new DateTime(2006, 12, 11))
            {
                DialogResult result = MessageBox.Show("This limited beta has expired.\n Press OK to go to the product homepage and download a new version.", "Application Expired", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    NavURL("http://regulazy.osherove.com");
                }
                return;
            }
            Scope root = txtRegexInput.RootScope;
            RegexAdvisor advisor = GetAdvisor();

            WaitCallback onThreadFinish = new WaitCallback(OnSuggestThreadFinish);
            string regex = advisor.Suggest(root)[0].RegexText;
            regex = optimize(regex);
            regex = decideIfStartOrEndLine(regex);
            results.RegexText = regex;
            results.RefreshResults();
            results.ShowScopeTree(root);

        }

        private static RegexAdvisor GetAdvisor()
        {
            RegexAdvisor advisor = new RegexAdvisor();
            UIActionSuggestionProvider.TeachRules(advisor);
            return advisor;
        }

        private static void doRunAutoScope(RegexAdvisor advisor, Scope root)
        {
                ThreadStart start = delegate
                                        {
                                            advisor.AutoScope(root);
                                        };

                Thread runner = new Thread(start);
                runner.Start();
        }

        private void RefreshResults(bool makeAutoScopes)
        {
//            if (DateTime.Now > new DateTime(2006, 12, 12))
//            {
//                DialogResult result = MessageBox.Show("This limited beta has expired.\n Press OK to go to the product homepage and download a new version.", "Application Expired", MessageBoxButtons.OKCancel);
//                if (result == DialogResult.OK)
//                {
//                    NavURL("http://regulazy.osherove.com");
//                }
//                return;
//            }
            Scope root = txtRegexInput.RootScope;
            RegexAdvisor advisor = new RegexAdvisor();
            UIActionSuggestionProvider.TeachRules(advisor);
//            if (makeAutoScopes)
//            {
////                ThreadStart start = delegate
////                                        {
//                                            advisor.AutoScope(root);
////                                        };
////
////                Thread runner = new Thread(start);
////                runner.Start();
//            }

            results.ShowInProgress();
            ThreadPool.QueueUserWorkItem(delegate
                                             {
                                                 Application.DoEvents();
                                                 string regex = advisor.Suggest(root)[0].RegexText;
                                                 regex = optimize(regex);
                                                 regex = decideIfStartOrEndLine(regex);
                                                 UIShowResultsAfterSuggest(root, regex);
                                             });
        }

        private void UIShowResultsAfterSuggest(Scope root, string regex)
        {
            ThreadStart uiThreadSafeActions = delegate()
                                {
                                    Application.DoEvents();
                                    results.RegexText = regex;
                                    results.RefreshResults();
                                    results.ShowScopeTree(root);
                                    results.HideInProgress();
                                };
            Invoke(uiThreadSafeActions);
        }

        private void OnSuggestThreadFinish(object state)
        {

        }


        private string decideIfStartOrEndLine(string regex)
        {
            try
            {
                if (regex.Length < 3) //^$
                    return regex;
                string strippedRegex = regex.TrimStart('^').TrimEnd('$');
                Regex r = new Regex(regex, results.RegXOptions);
                MatchCollection matches = r.Matches(results.SampleText);
                if (matches.Count < 2)
                {
                    MatchCollection altMatches = Regex.Matches(results.SampleText, strippedRegex, results.RegXOptions);
                    if (altMatches.Count > matches.Count)
                    {
                        return strippedRegex;
                    }
                }
                return regex;
            }

            catch (ArgumentException ae)
            {
                return regex;
            }

        }

        //        
        //        private static void LearnRulesOLD(RegexAdvisor advisor)
        //        {
        //            advisor.Learn(Resources.RulesFile);
        //        }
        //        


        private string doesNotStartWithEscapeChar(string what)
        {
            return Regex.Escape(what);
            //            string ban = @"[^\\]";
            string ban = @"(?:[^\\])";
            return ban + Regex.Escape(what);
        }
        private string optimize(string regex)
        {
            PatternOptimizer optimizer = new PatternOptimizer();
            optimizer.AutoAddLineEndMarks = true;
            return optimizer.Optimize(regex);

            string opt = regex;
            opt = Regex.Replace(opt, @"(?<before>.)\\^", "${before}");
            opt = Regex.Replace(opt, @"\\$(?<after>.)", "${after}");
            opt = Regex.Replace(opt, @"\$\^", "");
            opt = Regex.Replace(opt, @"\\?\\$", "?");
            //'opt = Regex.Replace(opt, "(\\s)+", Regex.Unescape("\\s+"))
            opt = Regex.Replace(opt, @"\++", Regex.Unescape(@"\+"));
            //'opt = Regex.Replace(opt, "\\+\\*", "+")
            opt = opt.Replace("?+", "?");
            opt = Regex.Replace(opt, doesNotStartWithEscapeChar("${"), "{");
            opt = Regex.Replace(opt, doesNotStartWithEscapeChar("$["), "[");
            opt = Regex.Replace(opt, doesNotStartWithEscapeChar("^?"), "^");

            if (!opt.EndsWith("$"))
            {
                opt += "$";
            }
            if (opt.StartsWith("^["))
            {
                opt = "^" + opt;
            }

            opt = dontAllowIntheMiddle("^", opt);
            opt = dontAllowIntheMiddle("$", opt);

            if (regex.StartsWith("^") && !opt.StartsWith("^"))
            {
                opt = "^" + opt;

            }
            return opt;
        }

        private string dontAllowIntheMiddle(string disallow, string input)
        {
            string format = string.Format(@"(?<before>.){0}(?<after>.)", Regex.Escape(disallow));
            string replaced = Regex.Replace(input, format, "${before}${after}");
            return replaced;
        }

        void sampleTextInput1_ActiveScopeChanged(object sender, ScopeAwareRichTextBox.ScopeEventArgs e)
        {

            if (e.Scope == null)
            {
                tipsScopeEditor.SetToolTip(txtRegexInput, string.Empty);
                tipsScopeEditor.ToolTipTitle = "No Active Scope";

                results.ShowRects(e.Scope, new List<Rectangle>());

                return;
            }
            tipsScopeEditor.Hide(txtRegexInput);

            if (e.Scope.Name != string.Empty)
            {
                tipsScopeEditor.ToolTipTitle = e.Scope.Name;
            }
            else
            {
                tipsScopeEditor.ToolTipTitle = "Unnamed Scope (right-click to rename)";
            }
            tipsScopeEditor.ReshowDelay = 500;
            tipsScopeEditor.UseAnimation = true;
            tipsScopeEditor.UseFading = false;
            tipsScopeEditor.InitialDelay = 500;

            string explanationText = String.Empty;
            tipsScopeEditor.ToolTipIcon = ToolTipIcon.Info;
            tipsScopeEditor.BackColor = Color.AliceBlue;
            tipsScopeEditor.ForeColor = Color.Black;

            if (e.Scope.IsImplicit)
            {
                explanationText =
                    "Automatic rule was used.\nRight-Click to select your own rule.";
            }
            else
            {
                tipsScopeEditor.ToolTipIcon = ToolTipIcon.Info;

                explanationText =
                    "Custom rule was used.\nRight-Click to select a different rule.";
            }
            if (e.Scope.Suggestions.Count > 0)
            {
                string desc = explanationText + "\n\nTry To Match:\n[" + TrimIfNeeded(e.Scope.Suggestions[0].Description) + "]";
                //                Text = removeNewLines(desc);
                tipsScopeEditor.SetToolTip(txtRegexInput, desc);
                //                tipsScopeEditor.Show(desc,txtRegexInput);
            }
            else
            {
                tipsScopeEditor.SetToolTip(txtRegexInput, explanationText + "\n\nTry To Match:\n[Exactly " + "\"" + TrimIfNeeded(e.Scope.Text) + "\"]");
            }

            Scope s = e.Scope;
            //            results.ShowRects(s, txtRegexInput.GetSurroundingRects(s.StartPosInRootScope, s.Length));
            results.HighlightScope(s);
            txtRegexInput.Focus();
        }

        private string TrimIfNeeded(string text)
        {
            if (text.Length > 100)
            {
                return text.Substring(0, 100) + "...";
            }
            else
            {
                return text;
            }
        }

        private string removeNewLines(string text)
        {
            return text.Replace(Environment.NewLine, " ");
        }


        private void toolCmdRegexMode_Click(object sender, EventArgs e)
        {
            txtRegexInput.InputMode = InputModes.RegexManipulation;


            toolCmdEditMode.Enabled = true;
            toolCmdEditMode.Checked = false;
            cmdAutoMatch.Enabled = (txtRegexInput.InputMode == InputModes.RegexManipulation);
            toolCmdRegexMode.Enabled = false;
            toolCmdRegexMode.Checked = true;
            RefreshResults(true);

        }


        private void toolCmdEditMode_Click(object sender, EventArgs e)
        {
            txtRegexInput.InputMode = InputModes.StandardEdit;
            cmdAutoMatch.Enabled = (txtRegexInput.InputMode == InputModes.RegexManipulation);

            toolCmdEditMode.Enabled = false;
            toolCmdEditMode.Checked = true;

            toolCmdRegexMode.Enabled = true;
            toolCmdRegexMode.Checked = false;

        }

        private void expressionInputCtl_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TemplateInput input = new TemplateInput();
            input.Pattern = results.RegexText;
            input.RegexOptions = results.RegXOptions;
            input.Input = results.SampleText;

            CodeGenDialog dlg = new CodeGenDialog(input);
            dlg.ShowDialog(this);
        }

        private void regulazyHomepageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavURL("http://ReguLazy.osherove.com");
        }

        private void NavURL(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Regulazy Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reportBugMissingFeatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavURL("mailto:Support@ISerializable.com?Subject=Regulazy Bug or Feature Request");
        }

        private void osheroveToolsHomePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavURL("http://tools.osherove.com/default.aspx?App=Regulazy");
        }

        private void roysBlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavURL("http://www.ISerializable.com");

        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavURL("http://ReguLazy.osherove.com");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void results_Load(object sender, EventArgs e)
        {

        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {

            new QuickDemoForm(Resources.Huh_ExpressionPane).ShowDialog(this.ParentForm);
        }

        private void RegulazyMainForm_Load(object sender, EventArgs e)
        {
            cmdAutoMatch.Enabled = (txtRegexInput.InputMode == InputModes.RegexManipulation);
        }

        private void webPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doImportPage();
        }

        private void doImportPage()
        {
            BrowserInputForm input = new BrowserInputForm();
            input.ShowDialog(this);
            
            if (input.HtmlTextSelection == string.Empty)
            {
                return;
            }
            skipAskingUserForSample = true;
            txtRegexInput.Text = input.HtmlTextSelection;
            results.SampleText = input.FullHtmlText;
        }

        private void cmdAutoMatch_Click(object sender, EventArgs e)
        {
            if(txtRegexInput.InputMode!=InputModes.RegexManipulation)
            {
                MessageBox.Show("You can only do this in Regex Edit Mode");
                return;
            }
            doRunAutoScope(GetAdvisor(),txtRegexInput.RootScope);
        }
    }
}