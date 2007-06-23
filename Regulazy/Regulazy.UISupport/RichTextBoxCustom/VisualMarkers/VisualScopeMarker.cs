using System;
using System.Drawing;
using System.Windows.Forms;
using Osherove.Controls;
using RegexWizard.Framework;

namespace Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers
{
    public abstract class VisualScopeMarker : VisualMarker
    {

        public ScopeAwareRichTextBox RTB
        {
            get
            {
                return Txt as ScopeAwareRichTextBox;
            }
        }
        
        protected Scope drawnScope = null;

        public VisualScopeMarker(Graphics g, Scope scopeToDraw, ScopeAwareRichTextBox txt)
            : base(g, txt, 0,0)
        {

            if (scopeToDraw==null)
            {
                return;
            }
            startIndex = scopeToDraw.StartPosInRootScope;
            length = scopeToDraw.Length;
              drawnScope = scopeToDraw;
        }

        public override void Redraw(Graphics g)
        {
            if (drawnScope==null)
                return;
            
            visible = false;

            graphics = g;


            if (drawnScope.IsRoot && drawnScope.IsImplicit)
            {
                return;

            }

            foreach (Rectangle rect in SurroundingRects)
            {

                DrawCustomForSingleSurroundingRect(rect,drawnScope);
            }
        }

        protected internal abstract void DrawCustomForSingleSurroundingRect(Rectangle rect,Scope scopeToDraw);

     
        public override void OnMouseMove(MouseEventArgs e)
        {
            
        }


        protected Rectangle GetMiddleRect(Rectangle baseRect)
        {
            int thirdWidth = baseRect.Width/3;
            int middleX = (baseRect.X + thirdWidth);
            return new Rectangle(middleX, baseRect.Y, thirdWidth, baseRect.Height);
        }
    }
}
