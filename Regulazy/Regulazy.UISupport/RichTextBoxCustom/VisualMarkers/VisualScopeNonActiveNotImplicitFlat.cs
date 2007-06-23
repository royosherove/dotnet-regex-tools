using System.Drawing;
using Osherove.Controls;
using RegexWizard.Framework;

namespace Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers
{
    public class VisualScopeNonActiveNotImplicitFlat:VisualScopeMarker
    {
        public VisualScopeNonActiveNotImplicitFlat(Graphics g, Scope scopeToDraw, ScopeAwareRichTextBox RTB) : base(g, scopeToDraw, RTB)
        {
        }

        protected internal override void DrawCustomForSingleSurroundingRect(Rectangle rect, Scope scopeToDraw)
        {
            DrawingParameters borderParameters = new DrawingParameters(Color.Empty, graphics, RTB.ImplicitScopeBorderColor, 35, RTB.NonActiveScopeBorderWidth);
            DrawingParameters fillParameters = new DrawingParameters(Color.Empty, graphics, 35);
            fillParameters.IsActive = IsActive;
            borderParameters.IsActive = IsActive;

            borderParameters.BorderColor = RTB.NonActiveScopeBorderColor;
            borderParameters.BorderWidth = RTB.NonActiveScopeBorderWidth;

            fillParameters.FillColor = RTB.NonActiveScopeFillColor;

            borderParameters.Rect = rect;
            fillParameters.Rect = rect;

            DrawBorder(borderParameters);
            DrawFill(fillParameters);
            
        }
    }
}
