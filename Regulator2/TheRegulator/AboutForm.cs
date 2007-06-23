using System;
using System.Data;
using System.Diagnostics;

using System.Reflection;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Regulator.GUI
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lbllabel2;
		private System.Windows.Forms.LinkLabel lnkRoy;
		private System.Windows.Forms.Label lbllabel3;
		private System.Windows.Forms.LinkLabel lnkRegexLib;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.LinkLabel lnkCOmments;
		private System.Windows.Forms.LinkLabel lnkDarren;
		private System.Windows.Forms.LinkLabel lnkSyncFusion;
		private Syncfusion.Windows.Forms.Tools.GradientLabel gradientLabel1;
		private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel1;
		private Syncfusion.Windows.Forms.Tools.GradientLabel gradientLabel2;
		private System.Windows.Forms.Label lblLoading;
		private System.Windows.Forms.Panel pnThanks;
		private System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.PictureBox picLogo;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		InitLabelGradientColorHack();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public void ShowLoading()
		{
			
			lblLoading.Visible=true;
			pnThanks.Visible=false;
			cmdClose.Visible=false;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutForm));
			this.lblVersion = new System.Windows.Forms.Label();
			this.lbllabel2 = new System.Windows.Forms.Label();
			this.lnkRoy = new System.Windows.Forms.LinkLabel();
			this.lbllabel3 = new System.Windows.Forms.Label();
			this.lnkRegexLib = new System.Windows.Forms.LinkLabel();
			this.cmdClose = new System.Windows.Forms.Button();
			this.lnkCOmments = new System.Windows.Forms.LinkLabel();
			this.lnkDarren = new System.Windows.Forms.LinkLabel();
			this.lnkSyncFusion = new System.Windows.Forms.LinkLabel();
			this.gradientLabel1 = new Syncfusion.Windows.Forms.Tools.GradientLabel();
			this.gradientPanel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
			this.gradientLabel2 = new Syncfusion.Windows.Forms.Tools.GradientLabel();
			this.lblLoading = new System.Windows.Forms.Label();
			this.pnThanks = new System.Windows.Forms.Panel();
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.picLogo = new System.Windows.Forms.PictureBox();
			this.gradientPanel1.SuspendLayout();
			this.pnThanks.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblVersion
			// 
			this.lblVersion.Location = new System.Drawing.Point(8, 96);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(120, 16);
			this.lblVersion.TabIndex = 1;
			this.lblVersion.Text = "Version:";
			// 
			// lbllabel2
			// 
			this.lbllabel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.lbllabel2.Location = new System.Drawing.Point(8, 8);
			this.lbllabel2.Name = "lbllabel2";
			this.lbllabel2.Size = new System.Drawing.Size(24, 16);
			this.lbllabel2.TabIndex = 2;
			this.lbllabel2.Text = "By:";
			// 
			// lnkRoy
			// 
			this.lnkRoy.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.lnkRoy.Location = new System.Drawing.Point(32, 8);
			this.lnkRoy.Name = "lnkRoy";
			this.lnkRoy.Size = new System.Drawing.Size(104, 16);
			this.lnkRoy.TabIndex = 0;
			this.lnkRoy.TabStop = true;
			this.lnkRoy.Text = "Roy Osherove";
			this.lnkRoy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRoy_LinkClicked);
			// 
			// lbllabel3
			// 
			this.lbllabel3.Location = new System.Drawing.Point(16, 40);
			this.lbllabel3.Name = "lbllabel3";
			this.lbllabel3.Size = new System.Drawing.Size(88, 16);
			this.lbllabel3.TabIndex = 4;
			this.lbllabel3.Text = "A big thanks to:";
			// 
			// lnkRegexLib
			// 
			this.lnkRegexLib.Location = new System.Drawing.Point(16, 72);
			this.lnkRegexLib.Name = "lnkRegexLib";
			this.lnkRegexLib.Size = new System.Drawing.Size(80, 16);
			this.lnkRegexLib.TabIndex = 3;
			this.lnkRegexLib.TabStop = true;
			this.lnkRegexLib.Text = "RegexLib.com";
			this.lnkRegexLib.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRegexLib_LinkClicked);
			// 
			// cmdClose
			// 
			this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdClose.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cmdClose.Location = new System.Drawing.Point(16, 160);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(88, 24);
			this.cmdClose.TabIndex = 0;
			this.cmdClose.Text = "&Close";
			// 
			// lnkCOmments
			// 
			this.lnkCOmments.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.lnkCOmments.Location = new System.Drawing.Point(136, 8);
			this.lnkCOmments.Name = "lnkCOmments";
			this.lnkCOmments.Size = new System.Drawing.Size(48, 16);
			this.lnkCOmments.TabIndex = 1;
			this.lnkCOmments.TabStop = true;
			this.lnkCOmments.Text = "Email";
			this.lnkCOmments.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCOmments_LinkClicked);
			// 
			// lnkDarren
			// 
			this.lnkDarren.Location = new System.Drawing.Point(16, 56);
			this.lnkDarren.Name = "lnkDarren";
			this.lnkDarren.Size = new System.Drawing.Size(160, 16);
			this.lnkDarren.TabIndex = 2;
			this.lnkDarren.TabStop = true;
			this.lnkDarren.Text = "Darren Neimke\'s Web service";
			this.lnkDarren.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDarren_LinkClicked);
			// 
			// lnkSyncFusion
			// 
			this.lnkSyncFusion.Location = new System.Drawing.Point(16, 88);
			this.lnkSyncFusion.Name = "lnkSyncFusion";
			this.lnkSyncFusion.Size = new System.Drawing.Size(72, 16);
			this.lnkSyncFusion.TabIndex = 4;
			this.lnkSyncFusion.TabStop = true;
			this.lnkSyncFusion.Text = "SyncFusion";
			this.lnkSyncFusion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSyncFusion_LinkClicked);
			// 
			// gradientLabel1
			// 
			this.gradientLabel1.BorderSides = System.Windows.Forms.Border3DSide.Left;
			this.gradientLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.gradientLabel1.Font = new System.Drawing.Font("Papyrus", 33F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gradientLabel1.Location = new System.Drawing.Point(-8, 0);
			this.gradientLabel1.Name = "gradientLabel1";
			this.gradientLabel1.Size = new System.Drawing.Size(456, 88);
			this.gradientLabel1.TabIndex = 9;
			this.gradientLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// gradientPanel1
			// 
			this.gradientPanel1.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.gradientPanel1.BorderColor = System.Drawing.Color.Black;
			this.gradientPanel1.BorderSides = System.Windows.Forms.Border3DSide.Left;
			this.gradientPanel1.BorderSingle = System.Windows.Forms.ButtonBorderStyle.None;
			this.gradientPanel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gradientPanel1.Controls.Add(this.cmdClose);
			this.gradientPanel1.GradientBackground = true;
			this.gradientPanel1.GradientColors = new System.Drawing.Color[] {
																				System.Drawing.Color.Empty,
																				System.Drawing.Color.RoyalBlue};
			this.gradientPanel1.Location = new System.Drawing.Point(0, 112);
			this.gradientPanel1.Name = "gradientPanel1";
			this.gradientPanel1.Size = new System.Drawing.Size(128, 232);
			this.gradientPanel1.TabIndex = 0;
			// 
			// gradientLabel2
			// 
			this.gradientLabel2.BorderSides = System.Windows.Forms.Border3DSide.Right;
			this.gradientLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.gradientLabel2.Location = new System.Drawing.Point(128, 264);
			this.gradientLabel2.Name = "gradientLabel2";
			this.gradientLabel2.Size = new System.Drawing.Size(320, 16);
			this.gradientLabel2.TabIndex = 12;
			this.gradientLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblLoading
			// 
			this.lblLoading.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLoading.ForeColor = System.Drawing.Color.Red;
			this.lblLoading.Location = new System.Drawing.Point(136, 232);
			this.lblLoading.Name = "lblLoading";
			this.lblLoading.Size = new System.Drawing.Size(160, 32);
			this.lblLoading.TabIndex = 13;
			this.lblLoading.Text = "Loading...";
			this.lblLoading.Visible = false;
			// 
			// pnThanks
			// 
			this.pnThanks.Controls.Add(this.lbllabel3);
			this.pnThanks.Controls.Add(this.lnkRegexLib);
			this.pnThanks.Controls.Add(this.lnkDarren);
			this.pnThanks.Controls.Add(this.lnkSyncFusion);
			this.pnThanks.Controls.Add(this.lnkRoy);
			this.pnThanks.Controls.Add(this.lnkCOmments);
			this.pnThanks.Controls.Add(this.lbllabel2);
			this.pnThanks.Location = new System.Drawing.Point(144, 96);
			this.pnThanks.Name = "pnThanks";
			this.pnThanks.Size = new System.Drawing.Size(200, 136);
			this.pnThanks.TabIndex = 14;
			// 
			// picIcon
			// 
			this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
			this.picIcon.Location = new System.Drawing.Point(312, 144);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(128, 120);
			this.picIcon.TabIndex = 15;
			this.picIcon.TabStop = false;
			// 
			// picLogo
			// 
			this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
			this.picLogo.Location = new System.Drawing.Point(0, 0);
			this.picLogo.Name = "picLogo";
			this.picLogo.Size = new System.Drawing.Size(456, 88);
			this.picLogo.TabIndex = 16;
			this.picLogo.TabStop = false;
			// 
			// AboutForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.cmdClose;
			this.ClientSize = new System.Drawing.Size(440, 320);
			this.ControlBox = false;
			this.Controls.Add(this.picLogo);
			this.Controls.Add(this.picIcon);
			this.Controls.Add(this.lblLoading);
			this.Controls.Add(this.gradientLabel2);
			this.Controls.Add(this.gradientPanel1);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.gradientLabel1);
			this.Controls.Add(this.pnThanks);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About Regulator";
			this.Load += new System.EventHandler(this.AboutForm_Load);
			this.gradientPanel1.ResumeLayout(false);
			this.pnThanks.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void InitLabelGradientColorHack()
		{
						this.gradientLabel1.BackgroundColor = 
				new Syncfusion.Drawing.BrushInfo(
					Syncfusion.Drawing.GradientStyle.Vertical,
				System.Drawing.Color.RoyalBlue, 
				System.Drawing.SystemColors.ActiveCaptionText);

			this.gradientLabel2.BackgroundColor = 
				new Syncfusion.Drawing.BrushInfo(
				Syncfusion.Drawing.GradientStyle.Vertical,
				System.Drawing.Color.Wheat, 
				System.Drawing.SystemColors.ActiveCaptionText);

		}
		private void AboutForm_Load(object sender, System.EventArgs e)
		{
			


			lblVersion.Text= "Version " + Assembly.GetEntryAssembly().GetName().Version.ToString();
		}

		private void LinkIt(string link)
		{
			try
			{
				Process.Start(link);
			}
			catch(Exception )
			{
			    
			}

		}
		private void lnkRoy_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LinkIt(@"http://www.iserializable.com");
		}

		private void lnkRegexLib_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LinkIt(@"http://www.RegexLib.com");
		}

		private void lnkWebSite_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LinkIt(@"http://regulator.sourceforge.net");
		}

		private void lnkDarren_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LinkIt(@"http://weblogs.asp.net/dneimke/");
		}

		private void lnkCOmments_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LinkIt(@"mailto:Regulator@Osherove.com?subject=Regulator comment");
		}

		private void lnkSyncFusion_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LinkIt("http://www.syncfusion.com");
		}

		private void lbllabel1_Click(object sender, System.EventArgs e)
		{
		
		}

		private void lnkDonate_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LinkIt(@"https://www.paypal.com/xclick/business=RoyOsherove%40Hotmail.com&item_name=Help%20Keep%20The%20Regulator%20Alive&no_note=1&tax=0&currency_code=USD&lc=US");
		}
	}
}
