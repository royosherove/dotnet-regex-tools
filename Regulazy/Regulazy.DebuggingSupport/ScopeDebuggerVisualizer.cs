using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;
using RegexWizard.Framework;
using Regulazy.UI.Controls;

namespace Regulazy.DebuggingSupport
{
    
    public partial class ScopeDebuggerVisualizer : DialogDebuggerVisualizer
    {
     

        ///<param name="objectProvider">An object of type <see cref="T:Microsoft.VisualStudio.DebuggerVisualizers.IVisualizerObjectProvider"></see>. This object provides communication from the debugger side of the visualizer to the object source (<see cref="T:Microsoft.VisualStudio.DebuggerVisualizers.VisualizerObjectSource"></see>) on the debuggee side.</param>
        ///<param name="windowService">An object of type <see cref="T:Microsoft.VisualStudio.DebuggerVisualizers.IDialogVisualizerService"></see>, which provides methods your visualizer can use to display Windows forms, controls, and dialogs.</param>
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            Scope s = objectProvider.GetObject() as Scope;
            ScopeTreeView view = new ScopeTreeView();
            Form dlg = new Form();
            dlg.Controls.Add(view);
            view.Dock=DockStyle.Fill;
            view.ShowScope(s);
            windowService.ShowDialog(dlg);
        }
    }
}