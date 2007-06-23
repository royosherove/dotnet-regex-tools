using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Regulazy.UI;

namespace Osherove.Controls
{
    public abstract class VisualMarker
    {
        public int StartIndex
        {
            get { return startIndex; }
            set { startIndex = value; }
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        public Graphics Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }

        public CustomDrawRTB Txt
        {
            get { return txt; }
            set { txt = value; }
        }
        private Color brushColor = Color.Empty;

        public Color BrushColor
        {
            get { return brushColor; }
            set { brushColor = value; }
        }

        protected Graphics graphics = null;
        protected CustomDrawRTB txt = null;
        protected bool visible = false;
        protected int startIndex = -1;
        protected int length = -1;

        public List<Rectangle> SurroundingRects
        {
            
            get
            {

                if (txt!=null && surroundingRects == null)
                {
                    surroundingRects = txt.GetSurroundingRects(startIndex, length);
                }
                return surroundingRects;
            }
        }

        private List<Rectangle> surroundingRects = null;

        public bool Visible
        {
            get
            {
                
                return visible;
            }
        }

        public virtual void OnActivate()
        {
            isActive = true;
            
        }

        private bool isActive = false;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        protected VisualMarker(Graphics graphics, CustomDrawRTB txt, int startIndex, int length)
        {
            this.graphics = graphics;
            this.txt = txt;
            this.startIndex = startIndex;
            this.length = length;
        }   

        public virtual void OnDeactivate()
        {
            isActive = false;
        }

        public void Hide()
        {
            visible = false;
        }

        public abstract void Redraw(Graphics g);

        public bool IntersectsWithCursor(Point cursorLocation)
        {
            if(SurroundingRects ==null)
            {
                return false;
            }
            
            Rectangle cursorArea;
            cursorArea = new Rectangle(new Point(cursorLocation.X - 5, cursorLocation.Y - 10), new Size(15, 15));

            return Intersects(cursorArea);
        }

        private bool Intersects(Rectangle cursorArea)
        {
            foreach (Rectangle markRect in SurroundingRects)
            {
                if (markRect.IntersectsWith(cursorArea))
                {
                    return true;
                }
            }
            return false;
        }

        protected void DrawBorder(DrawingParameters parameters)
        {
            Rectangle surroundingRect = parameters.Rect;
            if (parameters.BorderColor == Color.Empty)
            {
                return;
            }

            if (surroundingRect == Rectangle.Empty)
            {
                return;
            }
            drawRectWithColor(surroundingRect, parameters.BorderColor, parameters.G, parameters.Opacity, parameters.BorderWidth);
        }

        protected void DrawFill(DrawingParameters parameters)
        {
            if (parameters.FillColor == Color.Empty)
            {
                return;
            }
            Rectangle surroundingRect = parameters.Rect;

            if (surroundingRect == Rectangle.Empty)
            {
                return;
            }
            fillRectWithColor(surroundingRect, parameters.FillColor, parameters.G, parameters.Opacity);
        }



        private const int RECT_RADIUS = 3;

        private RoundRectGraphics getGraphiX(Graphics g)
        {
            return new RoundRectGraphics(g);
        }

        public void drawRectWithColor(Rectangle rect, Color forecolor, Graphics g, int opacity, int width)
        {
            Color halfColor = Color.FromArgb(opacity, forecolor);
            SolidBrush brush = new SolidBrush(halfColor);
            Pen pen = new Pen(brush, width);
            getGraphiX(g).DrawRoundRectangle(pen, rect, RECT_RADIUS);
            
            //g.DrawRectangle(pen, rect);

        }

        public void fillRectWithColor(Rectangle rect, Color color, Graphics g, int opacity)
        {
            Color backcolor = Color.FromArgb(opacity, color);
            SolidBrush fillBrush = new SolidBrush(backcolor);
            getGraphiX(g).FillRoundRectangle(fillBrush, rect, RECT_RADIUS);
            //g.FillRectangle(fillBrush, rect);
        }

        public abstract void OnMouseMove(MouseEventArgs e);

        public bool IntersectsWith(RectangleF bounds)
        {
            return Intersects(Rectangle.Round( bounds));
        }
    }
}