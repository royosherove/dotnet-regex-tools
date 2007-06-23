using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Regulazy.UI
{
    public partial class QuickDemoForm : Form
    {
        public QuickDemoForm()
        {
            InitializeComponent();
            
        }

        public QuickDemoForm(Bitmap bitmap):this()
        {
            pictureBox1.Image= bitmap;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}