using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Regulator2005.Framework.Controls
{
	public partial class TextViewer : Form
	{
		public TextViewer()
		{
			InitializeComponent();
		}

		public string TextValue
		{
			get { return txt.Text; }
			set { txt.Text= value; }
		}
	}


	

}