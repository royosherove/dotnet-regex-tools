using System;
using System.Drawing;

namespace Osherove.Controls
{
    public class DrawingParameters
    {
        private Color fillColor;

        public Color FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        private  Point startPoint;
        private  Point endPoint;
        private  Graphics g;
        private  Color borderColor;
        private  int opacity;

        public Point StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        public Point EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        public Graphics G
        {
            get { return g; }
            set { g = value; }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        public int Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }

        public int BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; }
        }

        public Rectangle Rect
        {
            get { return rect; }
            set { rect =value; }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set { isActive = value; }
        }

        private  int borderWidth;
        private Rectangle rect = Rectangle.Empty;
        private bool isActive=false;

        public DrawingParameters(Point startPoint, Point endPoint, Graphics g, Color fillColor, int opacity)
        {
            this.fillColor = fillColor;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.g = g;
            this.opacity = opacity;
        }

        public DrawingParameters(Point startPoint, Point endPoint, Graphics g, Color borderColor, int opacity, int borderWidth)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.g = g;
            this.borderColor = borderColor;
            this.opacity = opacity;
            this.borderWidth = borderWidth;
        }
        private bool useHatchBrush;

        public bool UseHatchBrush
        {
            get { return useHatchBrush; }
            set { useHatchBrush = value; }
        }
        
        public DrawingParameters(Color fillColor, Graphics g, Color borderColor, int opacity, int borderWidth)
        {
            this.fillColor = fillColor;
            this.g = g;
            this.borderColor = borderColor;
            this.opacity = opacity;
            this.borderWidth = borderWidth;
        }

        public DrawingParameters(Color fillColor, Graphics g, int opacity)
        {
            this.fillColor = fillColor;
            this.g = g;
            this.opacity = opacity;
        }
    }
}