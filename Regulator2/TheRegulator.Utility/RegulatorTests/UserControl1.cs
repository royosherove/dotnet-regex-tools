using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Regulator.SDK.Plugins;

namespace RegulatorTests
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class UserControl1 : GenericDockedPlugin
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UserControl1()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}
		protected override void OnProjectActionStarted(Regulator.SDK.RegexProject sender, Regulator.SDK.RegexActionTypes action)
		{
			base.OnProjectActionStarted (sender, action);
		}
		


		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// UserControl1
			// 
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(192)));
			this.Name = "UserControl1";

		}
		#endregion
	}
}
