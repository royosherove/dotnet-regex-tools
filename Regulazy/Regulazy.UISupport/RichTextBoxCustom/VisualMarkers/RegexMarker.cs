using System.Drawing;
using Osherove.Controls;
using RegexWizard.Framework;

namespace Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers
{
    public class RegexMarker : VisualScopeMarker
    {
        public RegexMarker(Graphics g, Scope scopeToDraw, ScopeAwareRichTextBox txt)
            : base(g, scopeToDraw, txt)
        {
        }

        protected internal override void DrawCustomForSingleSurroundingRect(Rectangle rect, Scope scopeToDraw)
        {

            DrawingParameters borderParameters = new DrawingParameters(Color.Empty, graphics, RTB.ImplicitScopeBorderColor, 35, RTB.NonActiveScopeBorderWidth);
            DrawingParameters fillParameters = new DrawingParameters(Color.Empty, graphics, 35);
            fillParameters.IsActive = IsActive;
            borderParameters.IsActive = IsActive;

            borderParameters.BorderColor = Color.Black;
            borderParameters.Opacity = 255;
            borderParameters.BorderWidth = 2;

            fillParameters.FillColor = Color.LightYellow;
            fillParameters.Opacity = 200;

            Rectangle biggerRect = GetHigherRect(rect);

            fillParameters.Rect = biggerRect;
            borderParameters.Rect = biggerRect;

            DrawBorder(borderParameters);
            DrawFill(fillParameters);

            if (scopeToDraw.Name == string.Empty)
            {
                return;
            }
            Font italicFont = new Font(txt.Font.FontFamily, txt.Font.Size - 2, FontStyle.Italic);
            SizeF nameWidth = graphics.MeasureString(scopeToDraw.Name, italicFont);

            if (nameWidth.Width > rect.Width)
            {
                return;

            }

            graphics.DrawString(scopeToDraw.Suggestions[0].Description,
                                italicFont,
                                new SolidBrush(Color.Black),
                                GetMiddleRect(biggerRect));
        }

        private Rectangle GetHigherRect(Rectangle baseRect)
        {
            int factor = 4;
            int halfFactor = factor / 2;
            return new Rectangle(baseRect.X, baseRect.Y - halfFactor, baseRect.Width, baseRect.Height + factor);
        }

        private Rectangle GetMiddleRect(Rectangle baseRect)
        {
            int thirdWidth = baseRect.Width / 3;
            int middleX = (baseRect.X + thirdWidth);
            return new Rectangle(middleX, baseRect.Y, thirdWidth, baseRect.Height);
        }
    }
}
