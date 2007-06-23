using System.Drawing;
using RegexWizard.Framework;

namespace Regulazy.UI.Controls.RichTextBoxCustom.VisualMarkers
{
    public class DebuggingMarker:VisualScopeMarker
    {
        public static DebuggingMarker Create(Graphics g, Scope scopeToDraw, ScopeAwareRichTextBox txt)
        {
            return new DebuggingMarker(g, scopeToDraw, txt);
        }
        
        private int startLineIndex;
        private int endLineIndex;

        public DebuggingMarker(Graphics g, Scope scopeToDraw, ScopeAwareRichTextBox txt) 
            : base(g, scopeToDraw, txt)
        {
        }

        public int StartLineIndex
        {
            get { return startLineIndex; }
            set { startLineIndex = value; }
        }

        public int EndLineIndex
        {
            get { return endLineIndex; }
            set { endLineIndex = value; }
        }

        protected internal override void DrawCustomForSingleSurroundingRect(Rectangle rect, Scope scopeToDraw)
        {
            startLineIndex = txt.GetLineFromCharIndex(scopeToDraw.StartPosInRootScope);
            endLineIndex = txt.GetLineFromCharIndex(scopeToDraw.EndPosInRootScope);
            string text = string.Format("Start:{0},End:{1}",startLineIndex,endLineIndex);
            txt.FindForm().Text = text;
//            Font font = new Font(txt.Font.FontFamily,txt.Font.Size*2);
//            graphics.DrawString(text,font,Brushes.Red,txt.Location);
        }
    }
}
