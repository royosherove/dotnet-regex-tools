using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers;

namespace Osherove.Controls
{
    /// <summary>
    /// Summary description for CustomDrawRTB - Subclasses the
    //RichTextBox to allow control over flicker
    /// </summary>
    public class CustomDrawRTB : RichTextBox
    {
        [DllImport("user32.dll")]
        public static extern bool LoockWindowUpdate(IntPtr hWndLock);


        const short WM_PAINT = 0x00f;
        private const int RECT_RADIUS = 3;

        public RoundRectGraphics CreateGraphicsExtended()
        {
            return new RoundRectGraphics(CreateGraphics());
        }

        public CustomDrawRTB()
        {
            AcceptsTab = true;
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            //SetStyle(ControlStyles.CacheText, true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected bool doPaint = true;
        public bool ShowSPaces
        {
            get { return ShowSpaces; }
            set { ShowSpaces = value; }
        }
        //        private bool isRefreshing = false;

        public bool DoPaint
        {
            get { return doPaint; }
            set { doPaint = value; }
        }
        private bool ShowSpaces = true;
        private Color spaceColor = Color.Blue;

        public bool ShowTabs
        {
            get { return showTabs; }
            set { showTabs = value; }
        }

        private bool showTabs = true;
        private bool showNewLines = true;

        //        private bool isPainting = false;
        [DebuggerStepThrough]
        protected override void WndProc(ref Message m)
        {
            // Parts based on Code courtesy of Mark Mihevc
            // sometimes we want to eat the paint message so we don't have to see all the
            // flicker from when we select the text to change the color.
            if (m.Msg == WM_PAINT)
            {
                if (doPaint)
                {

                    base.WndProc(ref m);

                    //                    refreshJustOnce();
                    // LockWindowUpdate(Handle);

                    DrawSpecialMarks();

                    Graphics graphics = CreateGraphics();
                    Rectangle rectangle = new Rectangle(Location, Size);
                    PaintEventArgs args = new PaintEventArgs(graphics, rectangle);
                    OnPaint(args);

                    //                    LockWindowUpdate(IntPtr.Zero);

                }
                else
                {
                    m.Result = IntPtr.Zero;
                    //                    LockWindowUpdate(IntPtr.Zero);

                }
            }
            else
            {
                base.WndProc(ref m);
                //                LockWindowUpdate(IntPtr.Zero);

            }
        }

        private bool cacheSpecialMarks = true;

        public bool CacheSpecialMarks
        {
            [DebuggerStepThrough]
            get { return cacheSpecialMarks; }
            set
            {
                cacheSpecialMarks = value;
                cachedMarkers.Clear();
            }
        }

        List<VisualMarker> cachedMarkers = new List<VisualMarker>();
        private void DrawSpecialMarks()
        {
            if (cacheSpecialMarks && cachedMarkers.Count > 0)
            {
                Graphics g = CreateGraphics();
                foreach (VisualMarker marker in cachedMarkers)
                {
                    if (marker.IntersectsWith(g.ClipBounds))
                    {
                        marker.Redraw(g);
                    }
                }
                return;
            }

            showSpacesIfNeeded();
            showTabsIfNeeded();
            showNewLinesIfNeeded();
        }

        public bool ShowNewLines
        {
            [DebuggerStepThrough]
            get { return showNewLines; }
            [DebuggerStepThrough]
            set { showNewLines = value; }
        }

        public Color SpaceColor
        {
            [DebuggerStepThrough]
            get { return spaceColor; }
            [DebuggerStepThrough]
            set { spaceColor = value; }
        }


        private void showSpacesIfNeeded()
        {
            if (!ShowSpaces)
            {
                return;
            }
            DrawForEachSpecialMark<SpaceMarker>(" ", spaceColor);
        }

        private void showTabsIfNeeded()
        {
            if (!showTabs)
            {
                return;
            }
            DrawForEachSpecialMark<TabMarker>(@"\t", spaceColor);
        }


        private void showNewLinesIfNeeded()
        {
            if (!showNewLines)
            {
                return;
            }
            DrawForEachSpecialMark<NewLineMarker>(@"\n", Color.DarkBlue);
        }



        public delegate void DrawMarkDelegate(Brush brush, Graphics g, Point position, Point centerPosition);
        public virtual void DrawForEachSpecialMark(string searchForExpression, Color brushColor, DrawMarkDelegate drawingCode)
        {
            MatchCollection matches = Regex.Matches(Text, searchForExpression);
            if (matches.Count == 0)
            {
                return;
            }
            Graphics g = CreateGraphics();
            Brush brush = new SolidBrush(brushColor);
            foreach (Match match in matches)
            {
                Point position = GetPositionFromCharIndex(match.Index);
                Point centerPos = new Point(position.X, (int)(position.Y + (Font.Size * 0.75)));

                drawingCode(brush, g, position, centerPos);
            }
        }



        public virtual void DrawForEachSpecialMark<MARKER>(string searchForExpression, Color brushColor)
            where MARKER : VisualMarker, new()
        {
            MatchCollection matches = Regex.Matches(Text, searchForExpression, RegexOptions.Compiled);
            if (matches.Count == 0)
            {
                return;
            }
            Graphics g = CreateGraphics();
            int firstVisibleLine = FirstVisiblelineIndex;
            int lastVisibleLine = LastVisibleLineIndex;
            foreach (Match match in matches)
            {
                int matchLine = GetLineFromCharIndex(match.Index);
                MARKER marker = new MARKER();

                marker.StartIndex = match.Index;
                marker.Length = match.Length;
                marker.Txt = this;
                marker.Graphics = g;
                marker.BrushColor = brushColor;
//                if (marker.IntersectsWith(g.ClipBounds))
//                {
                if (matchLine >= firstVisibleLine && matchLine <= lastVisibleLine)
                {
                    marker.Redraw(g);
                }
                
//                }

                if (cacheSpecialMarks)
                {
                    cachedMarkers.Add(marker);
                }
            }
        }


        public delegate void DrawingDelegate(DrawingParameters drawingParameters);

        private void DrawBorder(DrawingParameters parameters)
        {
            //Rectangle surroundingRect = GetSurroundingRectForSingleLinedScope(parameters.StartPoint, parameters.EndPoint);
            Rectangle surroundingRect = parameters.Rect;
            InflateRectIfActive(parameters, surroundingRect);

            if (surroundingRect == Rectangle.Empty)
            {
                return;
            }
            drawRectWithColor(surroundingRect, parameters.BorderColor, parameters.G, parameters.Opacity, parameters.BorderWidth);
        }

        private void DrawFill(DrawingParameters parameters)
        {
            //Rectangle surroundingRect = GetSurroundingRectForSingleLinedScope(parameters.StartPoint, parameters.EndPoint);
            Rectangle surroundingRect = parameters.Rect;
            InflateRectIfActive(parameters, surroundingRect);

            if (surroundingRect == Rectangle.Empty)
            {
                return;
            }
            fillRectWithColor(surroundingRect, parameters.FillColor, parameters.G, parameters.Opacity);
        }

        private void InflateRectIfActive(DrawingParameters parameters, Rectangle surroundingRect)
        {
            if (parameters.IsActive)
            {
                surroundingRect.Inflate(0, Convert.ToInt32(Font.Size / 2));
                surroundingRect.Y -= Convert.ToInt32(Font.Size / 3);
            }
        }

        public Rectangle GetSurroundingRectForSingleLinedScope(int index, int length)
        {
            return GetSurroundingRect(index, length, false);
        }

        public Rectangle GetSurroundingRect(int index, int length, bool includeNewLine)
        {
            Point startPoint = GetPositionFromCharIndex(index);
            Point endPoint = GetPositionFromCharIndex(index + length);

            int rectWidth = endPoint.X - startPoint.X;
            if (rectWidth <= 0)
            {
                rectWidth = makePositiveNumber(rectWidth);
            }

            Rectangle rect = GetOptimizedRectangle(rectWidth, startPoint);
            if (includeNewLine)
            {
                rect = Rectangle.Inflate(rect, 15, 0);

            }
            return AdjustRectForScroll(rect);
        }

        public Rectangle GetSurroundingRectForFirstLine(int index, int length, int line)
        {
            
            //            return GetSurroundingRectForSingleLinedScope(index, length, true);
            Point startPoint = GetPositionFromCharIndex(index);
            string currentLine = Lines[line];
            int firstCharOfThisLine = GetFirstCharIndexFromLine(line);
            int lineEndCharIndex = firstCharOfThisLine + currentLine.Length;
            Rectangle newRect = GetSurroundingRectForSingleLinedScope(index, lineEndCharIndex - index);
            SizeF textSize = CreateGraphics().MeasureString("A",Font);
            newRect.Width += (int)textSize.Width;

            Rectangle withOffSet = AdjustRectForScroll(newRect);
            return withOffSet;
            
        }

        public Rectangle AdjustRectForScroll(Rectangle newRect)
        {
            return new Rectangle(newRect.X+ AutoScrollOffset.X,
                                 newRect.Y+ AutoScrollOffset.Y,
                                 newRect.Width,
                                 newRect.Height);
        }


        public Rectangle GetSurroundingRectForFirstLineOLD(int index, int length, int line)
        {
            //            return GetSurroundingRectForSingleLinedScope(index, length, true);
            Point startPoint = GetPositionFromCharIndex(index);
            Point endPointPossiblyOnDifferentLine = GetPositionFromCharIndex(index + length);
            Point endPointEndOfLine = getEndPointForLine(line);
            Point endPoint;

            if (endPointPossiblyOnDifferentLine.Y != startPoint.Y)
            {
                endPoint = endPointEndOfLine;
            }
            else
            {
                endPoint = endPointPossiblyOnDifferentLine;
            }

            int rectWidth = endPoint.X - startPoint.X;
            if (rectWidth <= 0)
            {
                rectWidth = makePositiveNumber(rectWidth);
                //return Rectangle.Empty;
            }

            Rectangle rect = GetOptimizedRectangle(rectWidth, startPoint);
            return rect;
        }

        private static int makePositiveNumber(int rectWidth)
        {
            return rectWidth * (-1);
        }

        
        public List<Rectangle> GetSurroundingRects(int startIndex, int length)
        {
            List<Rectangle> surroundingRects = new List<Rectangle>();


            int endIndex = startIndex + length;

            int startLineIndex = GetLineFromCharIndex(startIndex);
            int endLineIndex = GetLineFromCharIndex(endIndex);


            if (startLineIndex == endLineIndex)//spans one line
            {
                Rectangle rectForSingleLinedScope = GetSurroundingRectForSingleLinedScope(startIndex, length);
                surroundingRects.Add(rectForSingleLinedScope);
                return surroundingRects;
            }

            //compensate for first line being zero so calculations won't break
            int endLineForComparison = endLineIndex + 1;
            int startLineForComparison = startLineIndex + 1;
            if (endLineForComparison - startLineForComparison == 1)//spans 2 lines
            {
                surroundingRects.Add(GetSurroundingRectForFirstLine(startIndex, length, startLineIndex));

                int firstCharOfEndLine = GetFirstCharIndexFromLine(endLineIndex);
                surroundingRects.Add(GetSurroundingRectForSingleLinedScope(firstCharOfEndLine, (startIndex + length) - firstCharOfEndLine));
                return surroundingRects;
            }


            if ((endLineForComparison - startLineForComparison) > 1)//spans >3 lines
            {
                GetSurroundingForMorethan3Lines(endLineIndex, length, startIndex, startLineIndex, surroundingRects);
                return surroundingRects;
            }

            return surroundingRects;
        }

        private void GetSurroundingForMorethan3Lines(int endLineIndex, int length, int startIndex, int startLineIndex, List<Rectangle> surroundingRects)
        {
            int firstVisibleLine = FirstVisiblelineIndex;
            int lastVisibleLine = LastVisibleLineIndex;
            int scopeLines = (endLineIndex - startLineIndex) + 1;

            surroundingRects.Add(GetSurroundingRectForFirstLine(startIndex, length, startLineIndex));

            bool includeNewLine = true;
            int numOfMiddleLines = (endLineIndex - startLineIndex) - 2;//start and end line don't count.
            
            for (int i = 0; i <= numOfMiddleLines; i++)
            {
                int currentLineInLoop = startLineIndex + i + 1;
                if (currentLineInLoop > Lines.Length - 1||
                    currentLineInLoop< firstVisibleLine || 
                    currentLineInLoop> lastVisibleLine )
                {
                    continue;
                }
                int startCharPos = GetFirstCharIndexFromLine(currentLineInLoop);
                Rectangle rect = GetSurroundingRect(startCharPos, Lines[currentLineInLoop].Length, includeNewLine);
                surroundingRects.Add(rect);
            }

            int firstCharOfEndLine = GetFirstCharIndexFromLine(endLineIndex);
            surroundingRects.Add(GetSurroundingRectForSingleLinedScope(firstCharOfEndLine, (startIndex + length) - firstCharOfEndLine));
        }
        
        private int FirstVisiblelineIndex
        {
            get
            {
                Point pos = new Point(0, 0);
                return GetLineFromPos(pos);
            }
        }
        
        private int LastVisibleLineIndex
        {
            get
            {
                Point pos = new Point(ClientRectangle.Width, ClientRectangle.Height);
                return GetLineFromPos(pos);
            }
        }

        private int GetLineFromPos(Point pos)
        {
            int index = GetCharIndexFromPosition(pos);
            return GetLineFromCharIndex(index);
        }


        private void GetSurroundingForMorethan3LinesOLD(int endLineIndex, int length, int startIndex, int startLineIndex, List<Rectangle> surroundingRects)
        {
            int scopeLines = (endLineIndex - startLineIndex) + 1;

            surroundingRects.Add(GetSurroundingRectForFirstLine(startIndex, length, startLineIndex));

            bool includeNewLine = true;
            int numOfMiddleLines = (endLineIndex - startLineIndex) - 2;//start and end line don't count.
            for (int i = 0; i <= numOfMiddleLines; i++)
            {
                int currentLineInLoop = startLineIndex + i + 1;
                if (currentLineInLoop > Lines.Length - 1)
                {
                    continue;
                }
                int startCharPos = GetFirstCharIndexFromLine(currentLineInLoop);
                surroundingRects.Add(GetSurroundingRect(startCharPos, Lines[currentLineInLoop].Length, includeNewLine));
            }

            int firstCharOfEndLine = GetFirstCharIndexFromLine(endLineIndex);
            surroundingRects.Add(GetSurroundingRectForSingleLinedScope(firstCharOfEndLine, (startIndex + length) - firstCharOfEndLine));
        }

//        public Rectangle GetSurroundingRectForSingleLinedScope(Point startPoint, Point endPoint)
//        {
//
//            int rectWidth = endPoint.X - startPoint.X;
//            if (rectWidth <= 0)
//            {
//                rectWidth = makePositiveNumber(rectWidth);
//            }
//
//            Rectangle rect = GetOptimizedRectangle(rectWidth, startPoint);
//            rect.Width += 15;
//            return AdjustRectForScroll(rect);
//        }



        private Rectangle GetOptimizedRectangle(int rectWidth, Point startPoint)
        {
            //float thirdFontSize = 0;
            float partialFontSize = Font.Size / 3;
            Point betterStartPoint = new Point(startPoint.X, (int)(startPoint.Y + partialFontSize));
            return new Rectangle(betterStartPoint, new Size(rectWidth, (int)(Font.Size)));
        }



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


        public void DrawBorder(int startIndex, int length, DrawingParameters parameters)
        {
            DrawCustom(startIndex, length, parameters, DrawBorder);
        }

        public void DrawFill(int startIndex, int length, DrawingParameters parameters)
        {
            DrawCustom(startIndex, length, parameters, new DrawingDelegate(DrawFill));
        }

        public void DrawCustom(int startIndex, int length, DrawingParameters parameters, DrawingDelegate drawCallback)
        {
            List<Rectangle> rects;
            rects = GetSurroundingRects(startIndex, length);
            foreach (Rectangle rect in rects)
            {
                parameters.Rect = rect;
                drawCallback(parameters);
            }
        }

        public void DrawCustomOLD(int startIndex, int length, DrawingParameters parameters, DrawingDelegate drawCallback)
        {
            int endIndex = startIndex + length;

            int startLineIndex = GetLineFromCharIndex(startIndex);
            int endLineIndex = GetLineFromCharIndex(endIndex);


            if (startLineIndex == endLineIndex)//spans one line
            {
                parameters.StartPoint = GetPositionFromCharIndex(startIndex);
                parameters.EndPoint = GetPositionFromCharIndex(endIndex);
                drawCallback(parameters);
                //DrawBorder(parameters);
            }

            //compensate for first line being zero so calculations won't break
            int endLineForComparison = endLineIndex + 1;
            int startLineForComparison = startLineIndex + 1;

            if (endLineForComparison - startLineForComparison == 1)//spans 2 lines
            {
                parameters.StartPoint = GetPositionFromCharIndex(startIndex);
                parameters.EndPoint = getEndPointForLine(startLineIndex);

                drawCallback(parameters);

                DrawCustomOnEndLine(endIndex, endLineIndex, parameters, drawCallback);
            }

            if ((endLineForComparison - startLineForComparison) > 1)//spans >2 lines
            {
                int scopeLines = endLineIndex - startLineIndex;

                parameters.StartPoint = GetPositionFromCharIndex(startIndex);
                parameters.EndPoint = getEndPointForLine(startLineIndex);

                drawCallback(parameters);

                for (int i = startLineIndex + 1; i < scopeLines; i++)
                {
                    int startLineCharPos = GetFirstCharIndexFromLine(i);

                    parameters.StartPoint = GetPositionFromCharIndex(startLineCharPos);
                    parameters.EndPoint = getEndPointForLine(i);
                    drawCallback(parameters);
                }
                DrawCustomOnEndLine(endIndex, endLineIndex, parameters, drawCallback);
            }
        }
        private void DrawCustomOnEndLine(int endCharIndex, int lineIndex, DrawingParameters parameters, DrawingDelegate draw)
        {
            //Scope ends on the next line
            //outline the final lineToTheEnd
            int charPos = GetFirstCharIndexFromLine(lineIndex);
            parameters.StartPoint = GetPositionFromCharIndex(charPos);
            parameters.EndPoint = GetPositionFromCharIndex(endCharIndex);
            draw(parameters);
        }

        public Point getEndPointForLine(int lineIndex)
        {
            int thisLineLastCharIndex = getNewLineCharFromLineIndexUsingLineEnd(lineIndex);
            Point point = GetPositionFromCharIndex(thisLineLastCharIndex);
            return new Point(point.X + 15, point.Y);
        }

        public Point getEndPointForLineOLD(int lineIndex)
        {
            int thisLineLastCharIndex = getNewLineCharFromLineIndexUsingLineEnd(lineIndex);
            //            int thisLineLastCharIndex = getNewLineCharFromLineIndex(lineIndex);
            int thisLineLastCharIndex2 = thisLineLastCharIndex;
            //            int thisLineLastCharIndex2 = GetNewLineCharFromLineIndexUsingRegex(lineIndex);

            Point point = GetPositionFromCharIndex(thisLineLastCharIndex);
            Point lessExactPoint = GetPositionFromCharIndex(thisLineLastCharIndex2);

            if (lessExactPoint.X < point.X)
            {
                //return lessExactPoint;
                return new Point(((point.X - lessExactPoint.X) / 3 + 15), point.Y);
            }
            else
            {
                return new Point(point.X + 15, point.Y);
            }
        }
        public int getNewLineCharFromLineIndex(int lineIndex)
        {
            string currnetLine = Lines[lineIndex];

            if (lineIndex == 0)
            {
                return currnetLine.Length;
            }
            else
            {
                int charCount = currnetLine.Length;
                for (int i = 1; i <= lineIndex; i++)
                {
                    charCount += Lines[i].Length;
                }
                return charCount;
            }
        }

        public int getNewLineCharFromLineIndexUsingLineEnd(int lineIndex)
        {
            string currnetLine = Lines[lineIndex];

            //            int newLineIndexINLine = Lines[lineIndex].IndexOf(Environment.NewLine);
            //            if (lineIndex == 0)
            //            {
            //                return GetNewLineCharFromLineIndexUsingRegex(lineIndex);
            //            }
            int lineStartIndex = GetFirstCharIndexFromLine(lineIndex);
            //            return lineStartIndex + newLineIndexINLine;
            return lineStartIndex + currnetLine.Length;
        }

        public int GetNewLineCharFromLineIndexUsingRegex(int lineIndex)
        {
            string currnetLine = Lines[lineIndex];
            //int indexOfNewLine = currnetLine.LastIndexOf(Environment.NewLine);
            int indexOfNewLine = Regex.Match(currnetLine, @"$").Index;


            if (lineIndex == 0)
            {
                return indexOfNewLine;
            }
            else
            {
                int firstCharIndex = GetFirstCharIndexFromLine(lineIndex);
                return firstCharIndex + indexOfNewLine + 1;
            }
        }

        //
        //
        //        private void refreshJustOnce()
        //        {
        //            if (!isRefreshing)
        //            {
        //                isRefreshing = true;
        //                Refresh();
        //                isRefreshing = false;
        //            }
        //        }
    }
}