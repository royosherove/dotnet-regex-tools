using System;
using Regulator.SDK.Plugins;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace RegulatorTests
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : Form
	{
		private System.Windows.Forms.LinkLabel llblinkLabel1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.llblinkLabel1 = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// llblinkLabel1
			// 
			this.llblinkLabel1.Location = new System.Drawing.Point(80, 40);
			this.llblinkLabel1.Name = "llblinkLabel1";
			this.llblinkLabel1.TabIndex = 0;
			this.llblinkLabel1.TabStop = true;
			this.llblinkLabel1.Text = "THis is a form";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.llblinkLabel1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		#region IPlugin Members

		public void ProjectChanged(Regulator.SDK.RegexProject newProject)
		{
			// TODO:  Add Form1.ProjectChanged implementation
		}

		public void OnDockActivate()
		{
			// TODO:  Add Form1.OnDockActivate implementation
		}

		public void OnDialogClick()
		{
			// TODO:  Add Form1.OnDialogClick implementation
		}

		public Regulator.SDK.Plugins.PluginTypes PluginType
		{
			get
			{
				// TODO:  Add Form1.PluginType getter implementation
				return new Regulator.SDK.Plugins.PluginTypes ();
			}
		}

		public string MenuCation
		{
			get
			{
				// TODO:  Add Form1.MenuCation getter implementation
				return null;
			}
			set
			{
				// TODO:  Add Form1.MenuCation setter implementation
			}
		}

		public string PluginName
		{
			get
			{
				// TODO:  Add Form1.PluginName getter implementation
				return null;
			}
			set
			{
				// TODO:  Add Form1.PluginName setter implementation
			}
		}

		public void OnInit(Regulator.SDK.AppContext context)
		{
			// TODO:  Add Form1.OnInit implementation
		}

		public Control control
		{
			get
			{
				// TODO:  Add Form1.control getter implementation
				return null;
			}
		}

		public Regulator.SDK.Plugins.PluginDockPositions PreferredDockState
		{
			get
			{
				// TODO:  Add Form1.PreferredDockState getter implementation
				return new Regulator.SDK.Plugins.PluginDockPositions ();
			}
		}

		#endregion
	}
}
