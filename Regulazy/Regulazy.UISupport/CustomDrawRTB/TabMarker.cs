using System.Drawing;
using System.Windows.Forms;
using Osherove.Controls;

namespace Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers
{
    public class TabMarker:VisualMarker
    {
         public TabMarker() : base(null, null, 0,0)
        {
        }

        public TabMarker(Graphics graphics, ScopeAwareRichTextBox txt, int startIndex, int length)
            : base(graphics, txt, startIndex, length)
        {
        }

        public override void Redraw(Graphics g)
        {
            if(txt==null)
                return;
            
            Point position = txt.GetPositionFromCharIndex(startIndex);
            Point centerPos = new Point(position.X, (int)(position.Y + (txt.Font.Size * 0.75)));
            float thirdFontSize = txt.Font.Size / 3;

            Brush brush = new SolidBrush(BrushColor);
            Pen pen = new Pen(brush);
            Point lineEnd = new Point((int)(centerPos.X + thirdFontSize), centerPos.Y);
            g.DrawLine(pen, centerPos, lineEnd);
            PointF topArrowHead = new PointF(lineEnd.X, (lineEnd.Y - thirdFontSize));
            PointF bottomArrowHead = new PointF(lineEnd.X, (lineEnd.Y + thirdFontSize));
            PointF middleArrowHead = new PointF(lineEnd.X + thirdFontSize, lineEnd.Y);
            g.DrawPolygon(pen, new PointF[] { topArrowHead, bottomArrowHead, middleArrowHead });
            g.FillPolygon(brush, new PointF[] { topArrowHead, bottomArrowHead, middleArrowHead });

        }

//         float thirdFontSize = Font.Size / 3;
//
//
//                            centerPosition.Offset((int)thirdFontSize, 0);
//                            centerPosition.Offset((int)thirdFontSize, 0);
//                            Pen pen = new Pen(brush);
//
//                            //the horizontal line
//                            Point lineEnd = new Point((int)(centerPosition.X + thirdFontSize), centerPosition.Y);
//                            g.DrawLine(pen, centerPosition, lineEnd);
//
//                            //the verical line from top
//                            PointF verticalLineStart = new PointF(lineEnd.X, (lineEnd.Y - thirdFontSize));
//                            g.DrawLine(pen, verticalLineStart, lineEnd);
//
//                            //the arrow on the left pointing left
//                            PointF topArrowHead = new PointF(centerPosition.X, (centerPosition.Y - thirdFontSize));
//                            PointF bottomArrowHead = new PointF(centerPosition.X, (centerPosition.Y + thirdFontSize));
//                            PointF middleArrowHead = new PointF(centerPosition.X - thirdFontSize, centerPosition.Y);
//
//                            g.DrawPolygon(pen, new PointF[] { topArrowHead, bottomArrowHead, middleArrowHead });
//                            g.FillPolygon(brush, new PointF[] { topArrowHead, bottomArrowHead, middleArrowHead });
//                       
        public override void OnMouseMove(MouseEventArgs e)
        {
        }
    }
}
