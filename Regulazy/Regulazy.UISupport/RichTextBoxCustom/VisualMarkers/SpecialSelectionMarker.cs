using System;
using System.Drawing;
using System.Windows.Forms;
using Osherove.Controls;
using RegexWizard.Framework;
using Regulazy.UI;
using Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers;

namespace Regulazy.UISupport.RichTextBoxCustom.VisualMarkers
{
    public class SpecialSelectionMarker:VisualScopeMarker
    {
        public SpecialSelectionMarker(Graphics g, Scope scopeToDraw, ScopeAwareRichTextBox txt) : base(g, scopeToDraw, txt)
        {
        }

        protected internal override void DrawCustomForSingleSurroundingRect(Rectangle rect, Scope scopeToDraw)
        {
            txt.drawRectWithColor(rect,Color.Red,graphics,255,1);
            txt.fillRectWithColor(rect,Color.Yellow,graphics,70);
        }
    }
}
