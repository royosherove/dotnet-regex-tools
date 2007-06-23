using System;
using System.Drawing;
using Osherove.Controls;
using RegexWizard.Framework;
using Regulazy.UI;

namespace Regulazy.UISupport.UserActions
{
    public abstract class ScopeTextBoxUserAction : UserAction
    {
        protected ScopeTextBoxUserAction(ScopeAwareRichTextBox txt, Scope root)
        {
            this.txt = txt;
            this.root = root;
        }

        protected ScopeAwareRichTextBox txt;
        protected Scope root = null;
        protected string titlePrefix;
        protected Color highlightFillColor=Color.Green;
        protected int highlightFillOpacity=100;
        protected Color highlightBorderColor=Color.Red;
        protected int highlightBorderOpacity=200;
        protected int highlightBorderWidth=1;

        public ScopeAwareRichTextBox Txt
        {
            get { return txt; }
            set { txt = value; }
        }

        public Scope Root
        {
            get { return root; }
            set { root = value; }
        }

        public Color HighlightFillColor
        {
            get { return highlightFillColor; }
            set { highlightFillColor = value; }
        }

        public int HighlightFillOpacity
        {
            get { return highlightFillOpacity; }
            set { highlightFillOpacity = value; }
        }

        public Color HighlightBorderColor
        {
            get { return highlightBorderColor; }
            set { highlightBorderColor = value; }
        }

        public int HighlightBorderOpacity
        {
            get { return highlightBorderOpacity; }
            set { highlightBorderOpacity = value; }
        }

        public int HighlightBorderWidth
        {
            get { return highlightBorderWidth; }
            set { highlightBorderWidth = value; }
        }

        public  override void Highlight(object sender, EventArgs e)
        {
            base.Highlight(sender, e);
            HighlightText(highlightFillColor, 
                          highlightFillOpacity, 
                          highlightBorderColor, 
                          highlightBorderOpacity, 
                          highlightBorderWidth, 
                          true, 
                          true);
        }

        protected virtual void HighlightText(Color fillColor, int fillOpacity, Color borderColor, int borderOpacity, int borderWidth, bool shouldDrawFill, bool shouldDrawBorder)
        {
            DrawingParameters fillData = new DrawingParameters(fillColor, txt.CreateGraphics(),fillColor,fillOpacity,borderWidth);
            DrawingParameters borderData = new DrawingParameters(borderColor, txt.CreateGraphics(),borderColor,borderOpacity,borderWidth);
            int lastSelectionStart = txt.SelectionStart;
            int lastSelectionLength = txt.SelectionLength;
            if (root != null)
            {
                lastSelectionStart = root.StartPosInRootScope;
                lastSelectionLength = root.Length;
            }
            if (shouldDrawFill)
            {
                txt.DrawFill(lastSelectionStart, lastSelectionLength, fillData);
            }
            
            if (shouldDrawBorder)
            {
                txt.DrawBorder(lastSelectionStart, lastSelectionLength, borderData);
            }
        }

        public override void OnHighlightOff()
        {
            txt.Refresh();
        }
    }
}