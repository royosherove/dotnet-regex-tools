using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Regulator2005.Framework.Controls
{
	public partial class PatternDesigner : UserControl
	{
		public PatternDesigner()
		{
			InitializeComponent();
		}

		
		public string PatternText
		{
			get { return txt.Text; }
			set { txt.Text = value; }
		}

	}


}
