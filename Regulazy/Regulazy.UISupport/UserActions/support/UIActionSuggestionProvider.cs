using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using RegexWizard.Framework;
using Regulazy.UI;
using Regulazy.UISupport.Properties;

namespace Regulazy.UISupport
{
    public class UIActionSuggestionProvider:ISuggestionProvider
    {
        private ScopeAwareRichTextBox txt;

        public ScopeAwareRichTextBox Txt
        {
            get { return txt; }
        }

        public UIActionSuggestionProvider(ScopeAwareRichTextBox textBox)
        {
            txt = textBox;
        }

        public List<Suggestion> GetSuggestions(Scope target)
        {
            RegexAdvisor advisor = new RegexAdvisor();
            advisor.MaxSuggestionLength = 45;
            TeachRules(advisor);
            string selection;
            if(target!=null)
            {
                selection = target.Text;
            }
            else
            {
                selection = txt.SelectedText;
            }
            return advisor.Suggest(selection);
        }

        public static void TeachRules(RegexAdvisor advisor)
        {
            string currentFile = string.Empty;

            try
            {
                string rootDir = Application.StartupPath;
                string filter = "*.rules";
                string[] files = Directory.GetFiles(rootDir, filter, SearchOption.TopDirectoryOnly);
                foreach (string filename in files)
                {
                    if(filename.ToLower().Contains("auto.rules"))
                    {
                        continue; 
                    }
                    string fullPath = Path.Combine(rootDir, filename);
                    currentFile = fullPath;
                    string rules = File.ReadAllText(fullPath);
                    advisor.Learn(rules);
                }
                TeachAutoRules(advisor);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not read rule file " + currentFile + " !\n" + e.Message);
            }
        }

        private static void TeachAutoRules(RegexAdvisor advisor)
        {
            string fullPath = Path.Combine(Application.StartupPath, "auto.rules");
            try
            {
                if(!File.Exists(fullPath))
                {
                    return;
                }
                string rules = File.ReadAllText(fullPath);
                advisor.LearnAutomaticRules(rules);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not read automatic rule file:\n" + fullPath + " !\n" + e.Message);
            }
        }

        public List<Suggestion> GetSuggestionsOLD(Scope target)
        {
            RegexAdvisor advisor = new RegexAdvisor();
            advisor.Learn(Resources.RulesFile);
            string selection;
            if(target!=null)
            {
                selection = target.Text;
            }
            else
            {
                selection = txt.SelectedText;
            }
            return advisor.Suggest(selection);
        }
    }
}
