namespace TeamAgile.RegexKit.RegexVisualizers
{
    using Microsoft.VisualStudio.DebuggerVisualizers;
    using System;
    using System.Windows.Forms;
    using TeamAgile.RegexKit.UiKit;

    public class RegexStringVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            string pattern = (string) objectProvider.GetObject();
            MatchCollectionVisualizerCtl hostedControl = new MatchCollectionVisualizerCtl();
            hostedControl.DisplayPattern(pattern);
            using (VisualizerDisplayForm form = new VisualizerDisplayForm("Matches Details"))
            {
                form.HostControl(hostedControl);
                windowService.ShowDialog((Form) form);
            }
        }

        public static void TestShowVisualizer(object objectToVisualize)
        {
            new VisualizerDevelopmentHost(objectToVisualize, typeof(RegexStringVisualizer)).ShowVisualizer();
        }
    }
}

