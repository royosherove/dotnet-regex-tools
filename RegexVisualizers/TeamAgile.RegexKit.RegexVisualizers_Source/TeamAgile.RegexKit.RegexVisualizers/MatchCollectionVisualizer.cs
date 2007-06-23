namespace TeamAgile.RegexKit.RegexVisualizers
{
    using Microsoft.VisualStudio.DebuggerVisualizers;
    using System;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using TeamAgile.RegexKit.UiKit;

    public class MatchCollectionVisualizer : DialogDebuggerVisualizer
    {
        private static Regex _savedInstance;

        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            MatchCollection matches = (MatchCollection) objectProvider.GetObject();
            MatchCollectionVisualizerCtl hostedControl = new MatchCollectionVisualizerCtl();
            hostedControl.DisplayMatches(matches);
            using (VisualizerDisplayForm form = new VisualizerDisplayForm("Matches Details"))
            {
                form.HostControl(hostedControl);
                windowService.ShowDialog((Form) form);
            }
        }

        public static void TestShowVisualizer(object objectToVisualize)
        {
            new VisualizerDevelopmentHost(objectToVisualize, typeof(MatchVisualizer)).ShowVisualizer();
        }

        public void tryeit()
        {
            using (Form form = new Form())
            {
                Match regexMatch = Regex.Match("aabbddaabbccsaaddbbccaaddasfaacd", "(aa).+?(aa)");
                MatchTree tree = new MatchTree();
                tree.ShowMatch(regexMatch);
                form.Text = "Match Details";
                form.Controls.Add(tree);
                tree.Parent = form;
                tree.Dock = DockStyle.Fill;
                form.ShowDialog();
            }
        }

        public void TryIt()
        {
            string pattern = "(aa)|(bb) |[fgd]|(a.+?b)";
            Regex regex = new Regex(pattern, RegexOptions.RightToLeft | RegexOptions.Multiline);
            Console.WriteLine(regex.Matches("aabddgdtrbaabbaabb").Count);
        }

        public static Regex SavedRegexInstance
        {
            get
            {
                return _savedInstance;
            }
            set
            {
                _savedInstance = value;
            }
        }
    }
}

