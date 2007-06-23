//using System;
//using System.Drawing;
//using System.Windows.Forms;
//using RegexWizard.Framework;
//
//namespace Regulazy.UI
//{
//    public partial class RegexHintForm : Form
//    {
//        private Point rightConnection;
//        private Point rightConnectionEnd;
//        
//        private Point leftConnection;
//        private Point leftConnectionEnd;
//
//        private Point middleConnection;
//        private Point middleConnectionEnd;
//
//        public RegexHintForm()
//        {
//            DoubleBuffered = true;
//            InitializeComponent();
//            lblMiddle.Visible = false;
//            lblLeft.Visible = false;
//            lblRight.Visible = false;
//        }
//
//        protected override void OnPaint(PaintEventArgs e)
//        {
//
//            Graphics g = e.Graphics;
//            Pen leftPen = new Pen(Brushes.DarkGreen, 1);
//            Pen rightPen = new Pen(Brushes.Red, 1);
//            Pen middlePen = new Pen(Brushes.Blue, 1);
//            
//            base.OnPaint(e);
//            drawConnectorsForConnection(g, leftConnection, lblLeft, leftConnectionEnd, leftPen);
//            drawConnectorsForConnection(g, middleConnection, lblMiddle, middleConnectionEnd, middlePen);
//            drawConnectorsForConnection(g, rightConnection, lblRight, rightConnectionEnd, rightPen);
//        }
//
//        private void drawConnectorsForConnection(Graphics g, Point connectionStart, Label lbl, Point connectionEnd, Pen pen)
//        {
//            if(connectionStart!=Point.Empty && lbl.Visible)
//            {
//                
//                int middleX = (connectionEnd.X - connectionStart.X)/2;
//                
//                g.DrawLine(pen, connectionStart, connectionEnd);
//                //g.DrawLine(pen, lbl.Location, new Point(middleX, connectionStart.Y));
//            }
//        }
//
//        public void ShowTips(Scope leftScope,Scope middleScope,Scope rightScope)
//        {
//            leftConnection = Point.Empty;
//            HideTips();
//
//            if (middleScope!=null && 
//                middleScope.Suggestions.Count > 0)
//            {
//                lblMiddle.Text = middleScope.Suggestions[0].Description;
//                lblMiddle.Visible = true;
//            }
//
//            if (leftScope!=null &&
//                leftScope.Suggestions.Count>0)
//            {
//                lblLeft.Text = leftScope.Suggestions[0].Description;
//                lblLeft.Visible = true;
//            }
//            
//            if (rightScope!=null &&
//                rightScope.Suggestions.Count>0)
//            {
//                lblRight.Text = rightScope.Suggestions[0].Description;
//                lblRight.Visible = true;
//            }
//        }
//
//        public void ShowConnectors(Point left,Point leftEnd, 
//                                   Point middle, Point middleEnd, 
//                                   Point right,Point rightEnd)
//        {
//            this.leftConnection = left;
//            this.leftConnectionEnd = leftEnd;    
//            
//            this.rightConnection= right;
//            this.rightConnectionEnd = rightEnd;    
//            
//            this.middleConnection = middle;
//            this.middleConnectionEnd = middleEnd;    
//        }
//
//        private void RegexHintForm_Load(object sender, System.EventArgs e)
//        {
//
//        }
//
//        public void HideTips()
//        {
//            lblMiddle.Visible = false;
//            lblLeft.Visible = false;
//            lblRight.Visible = false;
//            Refresh();
//            //Invalidate();
//        }
//    }
//}