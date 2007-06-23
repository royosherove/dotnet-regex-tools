using System;
using System.Drawing;
using Osherove.Controls;
using RegexWizard.Framework;

namespace Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers
{
    public class VisualScopeDrawingLogic:VisualScopeMarker
    {
        public VisualScopeDrawingLogic(Graphics g, Scope scopeToDraw, ScopeAwareRichTextBox txt) : base(g, scopeToDraw, txt)
        {
        }

        protected internal override void DrawCustomForSingleSurroundingRect(Rectangle rect, Scope scopeToDraw)
        {
            if (!IsActive && scopeToDraw.IsFlat && scopeToDraw.Name!=string.Empty)
            {
//                new RegexMarker(graphics, scopeToDraw, RTB).DrawCustomForSingleSurroundingRect(rect, scopeToDraw);
                new GroupNameMarker(graphics, scopeToDraw, RTB).DrawCustomForSingleSurroundingRect(rect, scopeToDraw);
                return;
            }
            
//            if (scopeToDraw.IsRoot && scopeToDraw.IsImplicit)
//            {
//                new VisualScopeInvisible(graphics, scopeToDraw, RTB).DrawCustomForSingleSurroundingRect(rect, scopeToDraw);
//                return;
//            }
            
            if (IsActive)
            {
                Scope namedParent = GetFirstNamedParentScopeFor(scopeToDraw);
                if (namedParent!=null)
                {
                    new NamedParentMarker(graphics, namedParent, RTB).DrawCustomForSingleSurroundingRect(rect, namedParent);
                }
                new VisualScopeActive(graphics, scopeToDraw, RTB).DrawCustomForSingleSurroundingRect(rect, scopeToDraw);
                
                return;
            }
            if (drawnScope.IsImplicit && drawnScope.IsFlat)
            {
//                new VisualScopeImplicitFlat(graphics, scopeToDraw, RTB).DrawCustomForSingleSurroundingRect(rect, scopeToDraw);
                return;

            }
            
//            if (drawnScope.IsImplicit && !drawnScope.IsFlat)
//            {
//                new NamedParentMarker(graphics, scopeToDraw, RTB).DrawCustomForSingleSurroundingRect(rect, scopeToDraw);
//                return;
//
//            }
            if (!IsActive && drawnScope.IsExplicit && drawnScope.IsFlat)
            {
                new VisualScopeNonActiveNotImplicitFlat(graphics, scopeToDraw, RTB).DrawCustomForSingleSurroundingRect(rect, scopeToDraw);
                return;
            }

            if (IsActive && drawnScope.IsExplicit  && drawnScope.IsFlat)
            {
                new VisualScopeActiveNotImplicitFlat(graphics, scopeToDraw, RTB).DrawCustomForSingleSurroundingRect(rect, scopeToDraw);
                return;
            }
        }

        private Scope GetFirstNamedParentScopeFor(Scope child)
        {
            Scope parent = child.ParentScope;
            while (parent!=null && parent.Name==string.Empty)
            {
                parent = parent.ParentScope;
            }
            return parent;
        }
    }

    public class VisualScopeInvisible : VisualScopeMarker
    {
        public VisualScopeInvisible(Graphics g, Scope scopeToDraw, ScopeAwareRichTextBox txt)
            : base(g, scopeToDraw, txt)
        {
        }

        protected internal override void DrawCustomForSingleSurroundingRect(Rectangle rect, Scope scopeToDraw)
        {
            DrawingParameters borderParameters = new DrawingParameters(Color.Empty, graphics, RTB.ImplicitScopeBorderColor, 35, RTB.NonActiveScopeBorderWidth);
            DrawingParameters fillParameters = new DrawingParameters(RTB.ActiveScopeFillColor, graphics, 35);
            fillParameters.IsActive = IsActive;
            borderParameters.IsActive = IsActive;

            borderParameters.BorderColor = RTB.ActiveScopeBorderColor;
            borderParameters.BorderWidth = RTB.ActiveScopeBorderWidth;

            fillParameters.FillColor = RTB.ActiveScopeFillColor;
            fillParameters.BorderWidth = RTB.ActiveScopeBorderWidth;
            visible = true;


            Rectangle finalRect = rect;
            fillParameters.Rect = finalRect;
            borderParameters.Rect = finalRect;

            DrawBorder(borderParameters);
            DrawFill(fillParameters);
           return;
        }
    }
}
