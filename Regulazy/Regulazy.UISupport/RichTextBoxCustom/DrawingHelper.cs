using System.Drawing;
using Osherove.Controls;
using RegexWizard.Framework;

namespace Regulazy.UI.RichTextBoxCustom
{
    public class DrawingHelper
    {
        #region Properties
        private ScopeAwareRichTextBox rtb = null;

        public ScopeAwareRichTextBox Rtb
        {
            get { return rtb; }
        }
        private Color implicitScopeBorderColor = Color.Gray;

        public Color ImplicitScopeBorderColor
        {
            get { return implicitScopeBorderColor; }
            set { implicitScopeBorderColor = value; }
        }


        public Color NonActiveScopeFillColor
        {
            get { return nonActiveScopeFillColor; }
            set { nonActiveScopeFillColor = value; }
        }

        private Color nonActiveScopeFillColor = Color.DarkGreen;
        private Color activeScopeFillColor = Color.Red;
        private Color activeScopeBorderColor = Color.Blue;
        private Color nonActiveScopeBorderColor = Color.Red;
        private int nonActiveScopeBorderWidth = 1;
        private int activeScopeBorderWidth = 2;
        private int scopeOpacity = 45;

        public Color ActiveScopeFillColor
        {
            get { return activeScopeFillColor; }
            set { activeScopeFillColor = value; }
        }

        public Color ActiveScopeBorderColor
        {
            get { return activeScopeBorderColor; }
            set { activeScopeBorderColor = value; }
        }

        public int ActiveScopeBorderWidth
        {
            get { return activeScopeBorderWidth; }
            set { activeScopeBorderWidth = value; }
        }

        public Color NonActiveScopeBorderColor
        {
            get { return nonActiveScopeBorderColor; }
            set { nonActiveScopeBorderColor = value; }
        }

        public int NonActiveScopeBorderWidth
        {
            get { return nonActiveScopeBorderWidth; }
            set { nonActiveScopeBorderWidth = value; }
        }


        public Font Font
        {
            get { return rtb.Font; }
            set { rtb.Font = value; }
        }

        #endregion

        public int ScopeOpacity
        {
            get { return scopeOpacity; }
            set { scopeOpacity = value; }
        }

        public void DrawScopeOfUnknownLength(Scope scope, bool isActive, Graphics g)
        {
            if (scope.IsRoot)
                return;

            DrawingParameters borderParameters =new DrawingParameters(Color.Empty, g, implicitScopeBorderColor, scopeOpacity, nonActiveScopeBorderWidth);
            DrawingParameters fillParameters =  new DrawingParameters(activeScopeFillColor, g, scopeOpacity);
            borderParameters.IsActive = isActive;
            fillParameters.IsActive = isActive;
            
            int startPos = scope.StartPosInRootScope;
            int length = scope.Length;

            if (scope.IsImplicit && scope.IsFlat)
            {
                borderParameters.BorderColor = implicitScopeBorderColor;
                borderParameters.BorderWidth = nonActiveScopeBorderWidth;
                rtb.DrawBorder(startPos, length, borderParameters);
            }
            if (!isActive && !scope.IsImplicit && scope.IsFlat)
            {
                borderParameters.BorderColor = nonActiveScopeBorderColor;
                rtb.DrawBorder(startPos, length, borderParameters);

                
                fillParameters.FillColor = nonActiveScopeFillColor;
                fillParameters.Opacity = 25;
                fillParameters.BorderWidth = nonActiveScopeBorderWidth;
                rtb.DrawFill(startPos, length, fillParameters);
            }

            if (isActive && !scope.IsImplicit && scope.IsFlat)
            {
                borderParameters.BorderColor = activeScopeBorderColor;
                borderParameters.BorderWidth = activeScopeBorderWidth;

                rtb.DrawBorder(startPos, length, borderParameters);
                fillParameters.FillColor = activeScopeFillColor;
                fillParameters.BorderWidth = activeScopeBorderWidth;

                rtb.DrawFill(startPos, length, fillParameters);
            }
        }

        private static bool isScopeStartsOrEndsOnFullScope(Scope scope)
        {
            bool isRoot = scope.ParentScope.IsRoot;
            Scope temp = scope;
            while (temp.ParentScope!=null)
            {
                temp = temp.ParentScope;
            }
            
            
            return isRoot;
        }


        public DrawingHelper(ScopeAwareRichTextBox txt)
        {
            rtb = txt;
        }
    }
}
