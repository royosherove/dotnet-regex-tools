using System;
using System.Drawing;
using System.Windows.Forms;
using Osherove.Controls;
using RegexWizard.Framework;
using Regulazy.UI;

namespace Regulazy.UISupport.UserActions
{
    public class ApplySuggestionOnSelectionAction : ScopeTextBoxUserAction
    {
        public ApplySuggestionOnSelectionAction(ScopeAwareRichTextBox txt, Suggestion sug)
            : base(txt, txt.ActiveScope)
        {
            selStart = txt.SelectionStart;
            selLength = txt.SelectionLength;

            Title = sug.Description;
            suggestion = sug;
        }
        
        
        private Suggestion suggestion;
        public override bool Execute()
        {
            try
            {
                txt.SelectionLength = selLength;
                Scope scopeToManipulate;

                if (txt.SelectionLength > 0)
                {
                    int startPos = txt.SelectionStart;
                    int len = txt.SelectionLength;
                    scopeToManipulate = txt.RootScope.FindInnerScope(startPos, len);
                    if (scopeToManipulate == null || scopeToManipulate == txt.RootScope ||
                        scopeToManipulate.Text != txt.SelectedText)
                    {
                        scopeToManipulate = txt.RootScope.DefineInnerScope(startPos, len);
                    }
                }
                else
                {
                    scopeToManipulate = txt.ActiveScope;
                    if (scopeToManipulate == null)
                    {
                        return false;
                    }
                }
                scopeToManipulate.Suggestions.Clear();
                scopeToManipulate.Suggestions.Add(suggestion);
                scopeToManipulate.IsImplicit = false;

                txt.SelectionLength = 0;
                
                txt.TriggerExpressionChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Problem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        int selStart;
        int selLength;
        
        protected override void   HighlightText(Color fillColor, int fillOpacity, Color borderColor, int borderOpacity,
                                             int borderWidth, bool shouldDrawFill, bool shouldDrawBorder)
        {
            txt.SelectionLength = 0;
            DrawingParameters fillData = new DrawingParameters(fillColor, txt.CreateGraphics(), fillColor, fillOpacity, borderWidth);
            DrawingParameters borderData = new DrawingParameters(borderColor, txt.CreateGraphics(), borderColor, borderOpacity, borderWidth);

            if (shouldDrawFill)
            {
                txt.DrawFill(selStart, selLength, fillData);
            }

            if (shouldDrawBorder)
            {
                txt.DrawBorder(selStart, selLength, borderData);
            }
        }

        public override void OnHighlightOff()
        {
            base.OnHighlightOff();
            txt.SelectionLength = selLength;
        }

       
    }
}
