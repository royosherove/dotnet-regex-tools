using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using RegexWizard.Framework;
using Regulazy.UI;

namespace Regulazy.UISupport.UserActions
{
    public class DefineEncapsulatingScopeAction : ScopeTextBoxUserAction
    {
        private Scope root = null;

        public DefineEncapsulatingScopeAction(ScopeAwareRichTextBox txt, Scope target) 
            : base(txt,target)
        {
            root = target;
            Title = "Group these..";
        }

       
        public override bool Execute()
        {
            int start = txt.SelectionStart;
            int length = txt.SelectionLength;
            Scope newScope = root.DefineInnerScope(start, length);
            txt.TriggerExpressionChanged();
            txt.Invalidate(newScope);
            return true;
        }
    }
}
