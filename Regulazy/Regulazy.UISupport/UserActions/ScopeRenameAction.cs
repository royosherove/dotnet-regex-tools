using System;
using System.Drawing;
using System.Windows.Forms;
using Osherove.Controls;
using RegexWizard.Framework;
using Regulazy.UI;

namespace Regulazy.UISupport.UserActions
{
    public class ScopeRenameAction : ScopeTextBoxUserAction
    {
        public string TitlePrefix
        {
            get { return titlePrefix; }
            set
            {
                titlePrefix = value;
                SetTitle();
            }
        }

        public ScopeRenameAction(Scope scope, ScopeAwareRichTextBox text) 
            : base(text, scope)
        {
            titlePrefix = "Rename";
            SetTitle();
            highlightFillColor = Color.Yellow;
            highlightFillOpacity = 80;
            highlightBorderColor = Color.Red;
            highlightBorderOpacity = 180;
            highlightBorderWidth = 2;
        }

        private void SetTitle()
        {
            if (root != null && root.Name != string.Empty)
            {
                Title = string.Format("{0} [{1}]..", titlePrefix, root.Name);
            }
            else
            {
                Title = string.Format("{0}..", titlePrefix);

            }
        }
        private string newName;

        public string NewName
        {
            get { return newName; }
            set { newName = value; }
        }
        
        public override bool Execute()
        {
            TextBox txtNewName = new TextBox();
            txtNewName.BackColor = Color.Yellow;
            txtNewName.Parent = txt;
            txtNewName.Location = txt.GetPositionFromCharIndex(root.StartPosInRootScope);
            txtNewName.AutoSize = true;

            txt.Controls.Add(txtNewName);
            bool shouldChangeName = true;
            EventHandler ChangeNameHandler = delegate
            {
                if (shouldChangeName)
                {
                    root.Name = txtNewName.Text;
                }
                txtNewName.Hide();
                txtNewName.Parent = null;
                txt.Controls.Remove(txtNewName);
                txt.TriggerExpressionChanged();
                txt.Invalidate(root);
            };
            txtNewName.LostFocus += ChangeNameHandler;
            txtNewName.KeyDown += delegate(object sender, KeyEventArgs e)
                                 {
                                     if (e.KeyCode == Keys.Enter)
                                     {
                                         ChangeNameHandler(txtNewName, EventArgs.Empty);
                                     }
                                     if (e.KeyCode == Keys.Escape)
                                     {
                                         shouldChangeName = false;
                                         ChangeNameHandler(txtNewName, EventArgs.Empty);
                                     }
                                 };

            txtNewName.Text = root.Name;
            txtNewName.Visible = true;
            txtNewName.Focus();
            txtNewName.SelectAll();

            return true;
        }

    }
}
