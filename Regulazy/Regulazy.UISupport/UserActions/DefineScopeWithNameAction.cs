using System;
using System.Windows.Forms;
using RegexWizard.Framework;
using Regulazy.UI;

namespace Regulazy.UISupport.UserActions
{
    public class DefineScopeWithNameAction : ScopeTextBoxUserAction
    {
        public DefineScopeWithNameAction(ScopeAwareRichTextBox txt) 
            : base(txt,null)
        {
            Title = "Seperate and name this part...";
        }
        
        public override bool Execute()
        {
            try
            {
                int start = txt.SelectionStart;
                int length = txt.SelectionLength;
                Scope newScope = txt.RootScope.DefineInnerScope(start, length);
                txt.TriggerExpressionChanged();
                txt.Invalidate(newScope);
                ScopeRenameAction renamer = new ScopeRenameAction(newScope, txt);
                renamer.Execute();
                return true;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
    }
}
