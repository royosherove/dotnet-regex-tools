namespace TeamAgile.RegexKit.RegexVisualizers
{
    using Microsoft.VisualStudio.DebuggerVisualizers;
    using System;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using TeamAgile.RegexKit.UiKit;

    public class RegexObjectVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            Regex r = (Regex) objectProvider.GetObject();
            MatchCollectionVisualizerCtl hostedControl = new MatchCollectionVisualizerCtl();
            using (VisualizerDisplayForm form = new VisualizerDisplayForm("Regex Details"))
            {
                hostedControl.DisplayRegex(r);
                form.HostControl(hostedControl);
                windowService.ShowDialog((Form) form);
            }
        }

        public static void TestShowVisualizer(object objectToVisualize)
        {
            new VisualizerDevelopmentHost(objectToVisualize, typeof(RegexObjectVisualizer)).ShowVisualizer();
        }

        public void TryIt()
        {
            Console.WriteLine(new Regex("my pattern").ToString());
        }
    }
}

