using System.Drawing;
using Osherove.Controls;
using RegexWizard.Framework;

namespace Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers
{
    public class VisualScopeActive : VisualScopeMarker
    {
        public VisualScopeActive(Graphics g, Scope scopeToDraw, ScopeAwareRichTextBox txt)
            : base(g, scopeToDraw, txt)
        {
        }
        protected internal override void DrawCustomForSingleSurroundingRect(Rectangle rect, Scope scopeToDraw)
        {
            DrawingParameters borderParameters = new DrawingParameters(Color.Empty, graphics, RTB.ImplicitScopeBorderColor, 255, RTB.NonActiveScopeBorderWidth);
            DrawingParameters fillParameters = new DrawingParameters(RTB.ActiveScopeFillColor, graphics, 50);
            fillParameters.IsActive = IsActive;
            borderParameters.IsActive = IsActive;

            borderParameters.BorderColor = RTB.ActiveScopeBorderColor;
            borderParameters.BorderWidth = 1;

            fillParameters.FillColor = RTB.ActiveScopeFillColor;
            fillParameters.BorderWidth = 1;
            visible = true;


            Rectangle finalRect = GetActiveRectange(IsActive, rect);
            fillParameters.Rect = finalRect;
            borderParameters.Rect = finalRect;

            DrawBorder(borderParameters);
            DrawFill(fillParameters);
        }

        private Rectangle GetActiveRectange(bool active, Rectangle baseRect)
        {
            int factor = 4;
            int halfFactor = factor / 2;
            return new Rectangle(baseRect.X , baseRect.Y - halfFactor, baseRect.Width , baseRect.Height + factor);
//            return new Rectangle(baseRect.X - halfFactor, baseRect.Y - halfFactor, baseRect.Width + factor, baseRect.Height + factor);
        }

        
        
    }
}
