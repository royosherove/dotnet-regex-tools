using System;
using System.Drawing;
using System.Drawing.Drawing2D;


// A simple extension to the Graphics class for extended 
// graphic routines, such, 
// as for creating rounded rectangles. 
// Because, Graphics class is an abstract class, 
// that is why it can not be inherited. Although, 
// I have provided a simple constructor 
// that builds the RoundRectGraphics object around a 
// previously created Graphics object. 
// Please contact: aaronreginald@yahoo.com for the most 
// recent implementations of
// this class. 

namespace Osherove.Controls
{
    /// <SUMMARY> 
    /// Inherited child for the class Graphics encapsulating 
    /// additional functionality for curves and rounded rectangles. 
    /// </SUMMARY> 
    public class RoundRectGraphics
    {
        private Graphics mGraphics;

        public Graphics Graphics
        {
            get { return mGraphics; }
            set { mGraphics = value; }
        }


        public RoundRectGraphics(Graphics graphics)
        {
            Graphics = graphics;
        }

        #region Fills a Rounded Rectangle with integers.

        public void FillRoundRectangle(Brush brush,
                                       int x, int y,
                                       int width, int height, int radius)
        {
            float fx = Convert.ToSingle(x);
            float fy = Convert.ToSingle(y);
            float fwidth = Convert.ToSingle(width);
            float fheight = Convert.ToSingle(height);
            float fradius = Convert.ToSingle(radius);
            FillRoundRectangle(brush, fx, fy,
                               fwidth, fheight, fradius);
        }


        public void FillRoundRectangle(Brush brush,
                                       Rectangle rect, int radius)
        {
            float fx = Convert.ToSingle(rect.X);
            float fy = Convert.ToSingle(rect.Y);
            float fwidth = Convert.ToSingle(rect.Width);
            float fheight = Convert.ToSingle(rect.Height);
            float fradius = Convert.ToSingle(radius);
            FillRoundRectangle(brush, fx, fy,
                               fwidth, fheight, fradius);
        }

        #endregion

        #region Fills a Rounded Rectangle with continuous numbers.

        public void FillRoundRectangle(Brush brush,
                                       float x, float y,
                                       float width, float height, float radius)
        {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            GraphicsPath path = GetRoundedRect(rectangle, radius);
            Graphics.FillPath(brush, path);
        }

        #endregion

        #region Draws a Rounded Rectangle border with integers.

        public void DrawRoundRectangle(Pen pen, int x, int y,
                                       int width, int height, int radius)
        {
            float fx = Convert.ToSingle(x);
            float fy = Convert.ToSingle(y);
            float fwidth = Convert.ToSingle(width);
            float fheight = Convert.ToSingle(height);
            float fradius = Convert.ToSingle(radius);
            DrawRoundRectangle(pen, fx, fy, fwidth, fheight, fradius);
        }

        public void DrawRoundRectangle(Pen pen, Rectangle rect, int radius)
        {
            float fx = Convert.ToSingle(rect.X);
            float fy = Convert.ToSingle(rect.Y);
            float fwidth = Convert.ToSingle(rect.Width);
            float fheight = Convert.ToSingle(rect.Height);
            float fradius = Convert.ToSingle(radius);
            DrawRoundRectangle(pen, fx, fy, fwidth, fheight, fradius);
        }

        #endregion

        #region Draws a Rounded Rectangle border with continuous numbers.

        public void DrawRoundRectangle(Pen pen,
                                       float x, float y,
                                       float width, float height, float radius)
        {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            GraphicsPath path = GetRoundedRect(rectangle, radius);
            Graphics.DrawPath(pen, path);
        }

        #endregion

        #region Get the desired Rounded Rectangle path.

        private GraphicsPath GetRoundedRect(RectangleF baseRect,
                                            float radius)
        {
            // if corner radius is less than or equal to zero, 
            // return the original rectangle 
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            // if the corner radius is greater than or equal to 
            // half the width, or height (whichever is shorter) 
            // then return a capsule instead of a lozenge 
            if (radius >= (Math.Min(baseRect.Width, baseRect.Height))/2.0)
                return GetCapsule(baseRect);

            // create the arc for the rectangle sides and declare 
            // a graphics path object for the drawing 
            float diameter = radius*2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new GraphicsPath();

            // top left arc 
            path.AddArc(arc, 180, 90);

            // top right arc 
            arc.X = baseRect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc 
            arc.Y = baseRect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc
            arc.X = baseRect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        #endregion

        #region Gets the desired Capsular path.

        private GraphicsPath GetCapsule(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    // return horizontal capsule 
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height )
                {
                    // return vertical capsule 
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    if (arc.Width > 0 && arc.Height > 0)
                    {
                        path.AddArc(arc, 180, 180);

                        arc.Y = baseRect.Bottom - diameter;
                        path.AddArc(arc, 0, 180);
                    }
                    else
                    {
                        path.AddEllipse(baseRect);
                    }
                }
                else
                {
                    // return circle 
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception ex)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }

        #endregion
    }

    internal class Drawing2D
    {
    }
}