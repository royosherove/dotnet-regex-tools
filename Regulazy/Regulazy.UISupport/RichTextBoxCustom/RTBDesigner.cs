using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Regulazy.UI.RichTextBoxCustom
{
    public class RTBDesigner:ControlDesigner    
    {
        private DesignerVerbCollection verbs;
        private ScopeAwareRichTextBox rtb;
        private DesignerActionListCollection actionLists=new DesignerActionListCollection();

        public RTBDesigner()
        {
            this.verbs = new DesignerVerbCollection();
            verbs.Add(new DesignerVerb("Add Sample Scopes", new EventHandler(AddSampleScopes)));
        }

        
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            rtb = (ScopeAwareRichTextBox) component;
            
            
        }
        
        public override void OnSetComponentDefaults()
        {

           
                 
        } 

        public override DesignerVerbCollection Verbs
        {
            get
            {
                return verbs;
            }
        }         
        private void AddSampleScopes(object sender, EventArgs e)
        {
            rtb.BackColor = Color.AliceBlue;
            rtb.Text = "Sample Input Text Active Scope\ntab HEY ";
            rtb.ActiveScope = rtb.RootScope.DefineInnerScope(18, 12);
            rtb.RootScope.DefineInnerScope(6, 5);
            rtb.InputMode =InputModes.RegexManipulation;
            rtb.Refresh();
        }

    }
}
