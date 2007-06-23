using System;
using System.Collections.Generic;
using System.Drawing;
using Osherove.Controls;
using RegexWizard.Framework;

namespace Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers
{
    public class NamedParentMarker : VisualScopeMarker
    {
        public NamedParentMarker(Graphics g, Scope scopeToDraw, ScopeAwareRichTextBox txt)
            : base(g, scopeToDraw, txt)
        {
        }

       
        
        protected internal override void DrawCustomForSingleSurroundingRect(Rectangle rect, Scope scopeToDraw)
        {
            List<Rectangle> rects = txt.GetSurroundingRects(scopeToDraw.StartPosInRootScope,scopeToDraw.Length);
            int inflateFactor = 12;
            if(onMultipleLines(rects))
            {
                inflateFactor = 6;
            }
            foreach (Rectangle rec in rects)
            {
                Rectangle infaltedRect = GetInflated(true, rec, inflateFactor);
                txt.drawRectWithColor(infaltedRect,Color.Chocolate, graphics, 220,2);
            }
            
            
            return;
        }

        private bool onMultipleLines(List<Rectangle> rects)
        {
            if(rects==null || rects.Count==0)
            {
                return false;
            }
            return (rects.Count > 1);
            
            bool onmultipleLines = false;
            int lastTop = rects[0].Top;
            foreach (Rectangle rect in rects)
            {
                if(rect.Top!=lastTop)
                {
                    onmultipleLines = true;
                }
            }
            return onmultipleLines;
        }

        private Rectangle GetInflated(bool active, Rectangle baseRect, int inflateFactor)
        {
            int factor = inflateFactor;
            int halfFactor = factor / 2;
            return new Rectangle(baseRect.X , baseRect.Y - halfFactor, baseRect.Width , baseRect.Height + factor);
//            return new Rectangle(baseRect.X - halfFactor, baseRect.Y - halfFactor, baseRect.Width + factor, baseRect.Height + factor);
        }

        protected Rectangle GetHigherRect(Rectangle baseRect)
        {
            int factor = 4;
            int halfFactor = factor / 2;
            return new Rectangle(baseRect.X, baseRect.Y - halfFactor, baseRect.Width, baseRect.Height + factor);
        }
        
    }
}
