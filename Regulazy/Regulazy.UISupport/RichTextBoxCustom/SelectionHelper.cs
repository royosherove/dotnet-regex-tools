
using System;
using System.Drawing;
using System.Windows.Forms;
using Osherove.Controls;
using RegexWizard.Framework;

namespace Regulazy.UI.RichTextBoxCustom
{
    /// <summary>
    /// This class is in charge of all interactive helping UI in the text box
    /// such as selecting spaces automatically on Click, or selecting words upt their boundary
    /// on double click.
    /// </summary>
    public class SelectionHelper
    {
        #region Delegating Data

        public int SelectionLength
        {
            get { return txt.SelectionLength; }
            set { txt.SelectionLength = value; }
        }

        public int SelectionStart
        {
            get { return txt.SelectionStart; }
            set { txt.SelectionStart = value; }
        }

        public string SelectedText
        {
            get { return txt.SelectedText; }
            set { txt.SelectedText = value; }
        }

        public string Text
        {
            get { return txt.Text; }
            set { txt.Text = value; }
        }


        #endregion

        private ScopeAwareRichTextBox txt = null;

        public SelectionHelper(ScopeAwareRichTextBox rtb)
        {
            txt = rtb;
            
            rtb.MouseMove += new MouseEventHandler(rtb_MouseMove);
            rtb.KeyDown += new KeyEventHandler(rtb_KeyDown);
            rtb.KeyUp += new KeyEventHandler(rtb_KeyUp);
            rtb.Click += new EventHandler(rtb_Click);
            rtb.DoubleClick += new EventHandler(rtb_DoubleClick);
        }

        void rtb_DoubleClick(object sender, EventArgs e)
        {
            adjustSelection();
        }

        void rtb_MouseMove(object sender, MouseEventArgs e)
        {
            lastMouseLocation = e.Location;
            drawSpecialSelectionIfNeeded(txt.GetCharIndexFromPosition(lastMouseLocation));

        }

        private void drawSpecialSelectionIfNeeded(int index)
        {
            if (!isControlPressed)
            {
                return;
            }

            //            Scope selection = GetSpecialSelection(index);
            //            DrawingParameters param = new DrawingParameters(Color.Red,txt.CreateGraphics(),Color.Red,255,1);
            //            txt.Refresh();
            //            txt.DrawBorder(selection.StartPosInRootScope,selection.EndPosInRootScope,param);
        }

        private bool isControlPressed = false;
        private Keys selectKey = Keys.ControlKey;
        private Point lastMouseLocation = Point.Empty;
        private Font lastFont = null;
        private Color lastBackColor;

        public Point LastMouseLocation
        {
            get { return lastMouseLocation; }
        }

        void rtb_Click(object sender, EventArgs e)
        {
            if (isControlPressed)
            {
                ActHighlightSimilarChars();
            }
        }



        public void ActHighlightSimilarChars()
        {
            int position = GetCharIndexFromPosition(LastMouseLocation);
            Char curChar = GetCharFromPosition(LastMouseLocation);
            SelectionStart = position;
            SelectionLength = 1;

            ExtendSelectionToSimilarCharsNearBy(
                            delegate(Char c)
                            {
                                return c == curChar;
                            });

            //if (ExtendSelectionToSimilarCharsNearBy(Char.IsLetterOrDigit))
            //  return;
        }



        private delegate bool MatchCharPredicate(Char c);


        public Scope GetSpecialSelection(int charIndex)
        {
            char curChar = txt.Text[charIndex];
            Scope newScope = new Scope(curChar.ToString(), charIndex);

            bool nextMatches = false;
            int compareToIndex = charIndex + 1;
            Char nextChar = ' ';

            if (compareToIndex < txt.Text.Length)
            {
                nextChar = txt.Text[compareToIndex++];
                nextMatches = (curChar == nextChar);
            }
            while (nextMatches)
            {
                newScope.Text += nextChar;
                if (compareToIndex >= txt.Text.Length)
                {
                    nextMatches = false;
                    continue;
                }
                nextChar = txt.Text[compareToIndex++];
                nextMatches = (curChar == nextChar);
            }
            bool prevMatches = false;
            int compareToPrevIndex = charIndex - 1;
            Char prevChar = ' ';

            if (compareToPrevIndex >= 0)
            {
                prevChar = txt.Text[compareToPrevIndex--];
                prevMatches = (curChar == prevChar);
            }
            while (prevMatches)
            {
                newScope.Text = prevChar + newScope.Text;
                newScope.StartPosInRootScope -= 1;
                if (compareToPrevIndex < 0)
                {
                    prevMatches = false;
                    continue;
                }
                prevChar = txt.Text[compareToPrevIndex--];
                prevMatches = (curChar == prevChar);
            }
            return newScope;
        }
        private bool ExtendSelectionToSimilarCharsNearBy(MatchCharPredicate isMatch)
        {

            bool nextMatches = false;
            if ((Text.Length - 1) > SelectionStart + SelectionLength)
            {
                Char nextChar = getCharAfterSelection(); //char after selection
                nextMatches = isMatch(nextChar);
            }

            bool prevMatches = false;
            if (SelectionStart - 1 > 0)
            {
                Char prevChar = getCharBeforeSelection(); //char before selection
                prevMatches = isMatch(prevChar);
            }

            if (!(prevMatches || nextMatches))
            {
                return false;
            }

            txt.DoPaint = false;
            try
            { extendSelectionForward(isMatch); }
            catch { }

            try
            { extendSelectionBackwards(isMatch); }
            catch { }

            txt.DoPaint = true;
            return true;
        }

        private bool ExtendSelectionToSimilarCharsNearByOLD(MatchCharPredicate isMatch)
        {

            bool nextMatches = false;
            if ((Text.Length - 1) > SelectionStart + SelectionLength)
            {
                Char nextChar = getCharAfterSelection(); //char after selection
                nextMatches = isMatch(nextChar);
            }

            bool prevMatches = false;
            if (SelectionStart - 1 > 0)
            {
                Char prevChar = getCharBeforeSelection(); //char before selection
                prevMatches = isMatch(prevChar);
            }

            if (!(prevMatches || nextMatches))
            {
                return false;
            }

            txt.DoPaint = false;
            try
            { extendSelectionForward(isMatch); }
            catch { }

            try
            { extendSelectionBackwards(isMatch); }
            catch { }

            txt.DoPaint = true;
            return true;
        }

        private char getCharBeforeSelection()
        {
            return Text[SelectionStart - 1];
        }

        private void extendSelectionForward(MatchCharPredicate matches)
        {
            Char nextChar = getCharAfterSelection(); //char after selection
            while (matches(nextChar))
            {
                SelectionLength += 1;
                nextChar = getCharAfterSelection(); //char after selection
            }
            SelectionLength += 1;
            if (!matches(SelectedText[SelectionLength - 1]))
            {
                SelectionLength -= 1;
            }
        }

        private char getCharAfterSelection()
        {
            return Text[SelectionStart + SelectionLength + 1];
        }


        private void extendSelectionBackwards(MatchCharPredicate matches)
        {
            Char prevChar = getCharBeforeSelection(); //char before selection
            while (matches(prevChar))
            {
                extendSelectionOneBack();
                prevChar = Text[SelectionStart - 1]; //char after selection
            }
            extendSelectionOneBack();


            if (!matches(SelectedText[0]))
            {
                SelectionLength -= 1;
                SelectionStart += 1;
            }
        }

        private void extendSelectionOneBack()
        {
            SelectionLength += 1;
            SelectionStart -= 1;
        }

        private int GetCharIndexFromPosition(Point location)
        {
            return txt.GetCharIndexFromPosition(location);
        }

        private Char GetCharFromPosition(Point location)
        {
            return txt.GetCharFromPosition(location);
        }

        private void adjustSelection()
        {
            while (txt.SelectedText.EndsWith(" ") && txt.SelectionLength > 0)
            {
                txt.SelectionLength -= 1;
            }
        }


        void rtb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == selectKey)
            {
                isControlPressed = false;
            }
        }

        void rtb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == selectKey)
            {
                isControlPressed = true;
            }
        }

    }
}
