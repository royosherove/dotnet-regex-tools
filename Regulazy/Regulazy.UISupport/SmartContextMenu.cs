using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RegexWizard.Framework;
using Regulazy.UI;

namespace Regulazy.UISupport
{
    public class SmartContextMenu : ContextMenu
    {
        private  Scope scope;
        private ScopeAwareRichTextBox textBox;

        public SmartContextMenu(Scope targetScope,ScopeAwareRichTextBox TextBox)
        {
            scope = targetScope;
            textBox = TextBox;
        }
        
        public void ShowForScope(Control control, Point pos)
        {
            List<Rectangle> rects = textBox.GetSurroundingRects(scope);
            Point screenPoint = control.PointToScreen(pos);
            Point clientPoint = control.PointToClient(screenPoint);
            Rectangle hitTest = new Rectangle(clientPoint, new Size(4,4));
            if(rects.Count>0)
            {
//                ShowUnderScopeRect(rects[0], control.PointToScreen(pos), control);
                foreach (Rectangle rect in rects)
                {
                    if(rect.IntersectsWith(hitTest))
                    {
                        ShowUnderScopeRect(rect, control.PointToScreen(pos), control);
                        return;
                    }
                }
                base.Show(control, pos);
            }
            else
            {
                base.Show(control, pos);
            }
        }

        private void ShowUnderScopeRect(Rectangle rect, Point pos,Control control)
        {
            Point newPos = new Point(control.PointToClient(pos).X,rect.Y+rect.Height+rect.Height/3);
            Show(control, newPos);
        }
    }
}
