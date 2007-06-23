using System.Drawing;
using System.Windows.Forms;
using RegexWizard.Framework;

namespace Regulazy.UISupport
{
    public partial class ScopeActionPane : Control
    {
        private ScopeActionsInfo actionsInfo;

        public ScopeActionPane(ScopeActionsInfo actionInfo)
        {
            this.actionsInfo = actionInfo;
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint|
                     ControlStyles.AllPaintingInWmPaint|
                     ControlStyles.OptimizedDoubleBuffer|
                     ControlStyles.SupportsTransparentBackColor,
                     true);
            UpdateStyles();
        
        }

        private Color transperantColor=Color.Empty;

        public Color TransperantColor
        {
            get { return transperantColor; }
            set { transperantColor = value; }
        }

        private int alpha=255;

        public int Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }
        
        public ScopeActionsInfo ActionsInfo
        {
            get { return actionsInfo; }
            set { actionsInfo = value; }
        }

        private Point targetLocationText=Point.Empty;

        public Point TargetLocationText
        {
            get { return targetLocationText; }
            set { targetLocationText = value; }
        }
        private Rectangle targetRect=Rectangle.Empty;

        public Rectangle TargetRect
        {
            get { return targetRect; }
            set { targetRect = value; }
        }
        private Scope targetScope;

        public Scope TargetScope
        {
            get { return targetScope; }
            set { targetScope = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(actionsInfo==null|| 
               targetRect==Rectangle.Empty ||
                targetScope==null)
            {
                return;
            }
            Graphics g = e.Graphics;
            Rectangle topRect = Rectangle.FromLTRB(targetRect.Left,
                                                   targetRect.Top-targetRect.Height,
                                                   targetRect.Right,
                                                   targetRect.Top);
            g.DrawRectangle(Pens.Blue,topRect);
            g.DrawString("Rename",new Font("tahoma",12),Brushes.White,topRect.X,topRect.Y );
        }
    }
}
