using System.Drawing;
using Osherove.Controls;
using RegexWizard.Framework;

namespace Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers
{
    public class VisualScopeActiveNotImplicitFlat:VisualScopeMarker
    {
        public VisualScopeActiveNotImplicitFlat(Graphics g, Scope scopeToDraw, ScopeAwareRichTextBox txt) : base(g, scopeToDraw, txt)
        {
        }

        protected internal override void DrawCustomForSingleSurroundingRect(Rectangle rect, Scope scopeToDraw)
        {
            DrawingParameters borderParameters = new DrawingParameters(Color.Empty, graphics, RTB.ImplicitScopeBorderColor, 35, RTB.NonActiveScopeBorderWidth);
            DrawingParameters fillParameters = new DrawingParameters(Color.Empty, graphics, 35);
            fillParameters.IsActive = IsActive;
            borderParameters.IsActive = IsActive;

            borderParameters.BorderColor = RTB.NonActiveScopeBorderColor;

            fillParameters.FillColor = RTB.NonActiveScopeFillColor;
            fillParameters.Opacity = 25;
            fillParameters.BorderWidth = RTB.NonActiveScopeBorderWidth;
            visible = true;


            borderParameters.Rect = rect;
            fillParameters.Rect = rect;

            DrawBorder(borderParameters);
            DrawFill(fillParameters);
        }
    }
}
