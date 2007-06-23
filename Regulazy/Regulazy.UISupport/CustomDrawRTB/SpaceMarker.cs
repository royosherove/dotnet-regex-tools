using System;
using System.Drawing;
using System.Windows.Forms;
using Osherove.Controls;

namespace Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers
{
    public class SpaceMarker:VisualMarker
    {
        public SpaceMarker() : base(null, null, 0,0)
        {
        }

        public SpaceMarker(Graphics graphics, ScopeAwareRichTextBox txt, int startIndex, int length) : base(graphics, txt, startIndex, length)
        {
        }

        public override void Redraw(Graphics g)
        {
            if(txt==null)
                return;
            
            Point position = txt.GetPositionFromCharIndex(startIndex);
            Point centerPos = new Point(position.X, (int)(position.Y + (txt.Font.Size * 0.75)));
            RectangleF rectangle = new RectangleF(centerPos, new SizeF((txt.Font.Size / 3), (txt.Font.Size / 3)));
            g.FillEllipse(new SolidBrush(BrushColor), rectangle);
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
        }
    }
}
