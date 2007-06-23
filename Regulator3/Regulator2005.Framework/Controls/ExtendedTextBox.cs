using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Regulator2005.Framework.Controls
{
	public partial class ExtendedTextBox : UserControl
	{
		public ExtendedTextBox()
		{
			InitializeComponent();
			cmdOptions.Visible = false;

		}

		private void txt_MouseEnter(object sender, EventArgs e)
		{
			DisplayOptions();
			
		}

		private void txt_MouseLeave(object sender, EventArgs e)
		{
			HideOptions();

		}

		private void HideOptions()
		{
			cmdOptions.Visible = false;
		}

		private void cmdOptions_Click(object sender, EventArgs e)
		{
			ShowTextEditor();
		}


		public string Text
		{
			get { return txt.Text; }
			set { txt.Text= value; }
		}


		public TextBox TextBoxControl
		{
			get { return txt; }
		}


		private void ShowTextEditor()
		{
			TextViewer viewer = new TextViewer();
			viewer.TextValue = txt.Text;
			DialogResult result = viewer.ShowDialog(this);
			if(result== DialogResult.OK)
				txt.Text = viewer.TextValue;
		}

		private void ExtendedTextBox_MouseEnter(object sender, EventArgs e)
		{
			DisplayOptions();
		}

		private void DisplayOptions()
		{
			cmdOptions.Visible = true;
		}

		private void ExtendedTextBox_MouseLeave(object sender, EventArgs e)
		{
			
		}

		private void cmdOptions_MouseEnter(object sender, EventArgs e)
		{
			DisplayOptions();
		}

		private void cmdOptions_MouseLeave(object sender, EventArgs e)
		{
			HideOptions();
		}
	}
}
