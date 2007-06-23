using System.Drawing;
using Osherove.Controls;
using RegexWizard.Framework;

namespace Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers
{
    public class VisualScopeImplicitFlat:VisualScopeMarker
    {

        protected internal override void DrawCustomForSingleSurroundingRect(Rectangle rect, Scope scopeToDraw)
        {
            DrawingParameters borderParameters = new DrawingParameters(Color.Empty, graphics, Color.Empty, 65, RTB.NonActiveScopeBorderWidth);
            DrawingParameters fillParameters = new DrawingParameters(Color.Empty, graphics, 35);
            fillParameters.IsActive = IsActive;
            borderParameters.IsActive = IsActive;

            borderParameters.BorderColor = Color.Silver;
            borderParameters.BorderWidth = RTB.NonActiveScopeBorderWidth;

            fillParameters.FillColor = Color.Silver;
            visible = true;


            fillParameters.Rect = rect;
            borderParameters.Rect = rect;

            DrawBorder(borderParameters);
//            DrawFill(fillParameters);
            
        }

        public VisualScopeImplicitFlat(Graphics g, Scope scopeToDraw, ScopeAwareRichTextBox txt) : base(g, scopeToDraw, txt)
        {
        }
    }
}
