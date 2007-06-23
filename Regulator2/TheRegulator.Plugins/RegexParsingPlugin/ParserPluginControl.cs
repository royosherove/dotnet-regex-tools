using System;
using RegexTest;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Regulator.SDK;
using Regulator.SDK.Plugins;

namespace RegexParsingPlugin
{
	/// <summary>
	/// Summary description for ParserPluginControl.
	/// </summary>
	public class ParserPluginControl : GenericDockedPlugin
	{
		private System.Windows.Forms.TextBox txtText;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ParserPluginControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.PluginName= "Regex Analyzer";
			this.MenuCation="Regex &Analyzer";
			this.Shortcut= Shortcut.CtrlShiftA;
			
		}

		public override Icon Icon
		{
			get
			{
				return new AboutDialog().Icon;
			}
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
			this.txtText = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// txtText
			// 
			this.txtText.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.txtText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtText.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.txtText.Location = new System.Drawing.Point(0, 0);
			this.txtText.Multiline = true;
			this.txtText.Name = "txtText";
			this.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtText.Size = new System.Drawing.Size(104, 232);
			this.txtText.TabIndex = 0;
			this.txtText.Text = "Regex Analyzer";
			// 
			// ParserPluginControl
			// 
			this.Controls.Add(this.txtText);
			this.Name = "ParserPluginControl";
			this.Size = new System.Drawing.Size(104, 232);
			this.ResumeLayout(false);

		}
		#endregion
	
		public override void OnDialogClick()
		{
			
			base.OnDialogClick ();
			ParseRegex();
		}

		public override void ShowAbout()
		{
			string about = @"
Analyzer plugin for The Regulator. 
Adapted from Eric Gunnerson's Regex Workbench code by Roy Osherove.
Original code written by Eric Gunnerson
http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=43D952B8-AFC6-491B-8A5F-01EBD32F2A6C";
			MessageBox.Show(about);
		}


		private void ParseRegex()
		{
			if(!Visible)
			{
				return ;
			}

			try
			{
				txtText.Text= RegexTest.RegexExpression.Interpret(_currentProject.Regex);
			}
			catch(Exception e)
			{
				txtText.Text= e.Message;		    
			}
		}

	
		public override void OnDockActivate()
		{
			ParseRegex();
			// TODO:  Add ParserPluginControl.OnDockActivate implementation
			base.OnDockActivate ();
		}

		private void CurrentDocument_Updated(object sender, EventArgs e)
		{
			ParseRegex();
		}
	
		public override void ProjectChanged(RegexProject newProject)
		{
			
			base.ProjectChanged (newProject);
		}
	}
}
