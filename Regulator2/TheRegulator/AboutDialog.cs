// AboutDialog.cs
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics; // Process
using System.Text; // Stringbuilder
using System.Reflection; // Assembly details
/// <summary>
/// Application AboutForm
/// </summary>
public class AboutDialog : System.Windows.Forms.Form
{
	private System.Windows.Forms.Button cmdOk;
	private System.Windows.Forms.Label lblReferencedAssemblies;
	private System.Windows.Forms.ListBox lstReferenceAssemblies;
	private System.Windows.Forms.Label lblCopyright;
	private System.Windows.Forms.Button cmdReferences;
	private System.Windows.Forms.Label lblVersion;
	private System.Windows.Forms.PictureBox picLogo;
	private System.Windows.Forms.PictureBox picIcon;
	private System.Windows.Forms.Panel pnThanks;
	private System.Windows.Forms.Label lbllabel3;
	private System.Windows.Forms.LinkLabel lnkRegexLib;
	private System.Windows.Forms.LinkLabel lnkDarren;
	private System.Windows.Forms.LinkLabel lnkSyncFusion;
	private System.Windows.Forms.LinkLabel lnkRoy;
	private System.Windows.Forms.Label lbllabel2;
	private System.Windows.Forms.LinkLabel lnkRegexDesigner;
	private System.Windows.Forms.LinkLabel lnkRegexWorkBench;
	private System.Windows.Forms.LinkLabel lnkExpresso;
	private System.Windows.Forms.LinkLabel lnkHomepage;
	private System.Windows.Forms.LinkLabel lnSupport;
	private System.Windows.Forms.Label lblCopyright2;
	private System.Windows.Forms.LinkLabel lnkGift;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.Container components = null;

	public AboutDialog()
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

	protected override void OnLoad(System.EventArgs e)
	{
		// Set form sizes
		Size = _collapsedSize;       
        
		// "Let the base class have its way"
		base.OnLoad(e);
        
		// Pull out assembly attributes
		Assembly        assem = Assembly.GetEntryAssembly();
		AssemblyName    assemName = assem.GetName();
		object[]        attribs = null;

		// Title
		attribs = assem.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
		string  title = (attribs.Length != 0 ? ((AssemblyTitleAttribute)attribs[0]).Title : "");
		if( title.Length == 0 ) title = System.IO.Path.GetFileName(assem.Location);
		Text = "About " + title;

		// Version
		string  version = assem.GetName().Version.ToString();
		lblVersion.Text = "v" + version + " (beta)";
        
		// Description
		attribs = assem.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), true);
		string  description = (attribs.Length != 0 ? ((AssemblyDescriptionAttribute)attribs[0]).Description : "");

		// Copyright
		attribs = assem.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);
		string  copyright = (attribs.Length != 0 ? ((AssemblyCopyrightAttribute)attribs[0]).Copyright : "");
		lblCopyright.Text = copyright;
        
		// Build referenced assemblies list
		foreach( AssemblyName referencedAssemName in assem.GetReferencedAssemblies() )
		{
			lstReferenceAssemblies.Items.Add(referencedAssemName.Name + " v" + referencedAssemName.Version.ToString());
		}
	}
	
	#region Windows Form Designer generated code
	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutDialog));
		this.lstReferenceAssemblies = new System.Windows.Forms.ListBox();
		this.cmdOk = new System.Windows.Forms.Button();
		this.lblReferencedAssemblies = new System.Windows.Forms.Label();
		this.lblCopyright = new System.Windows.Forms.Label();
		this.cmdReferences = new System.Windows.Forms.Button();
		this.lblVersion = new System.Windows.Forms.Label();
		this.picLogo = new System.Windows.Forms.PictureBox();
		this.picIcon = new System.Windows.Forms.PictureBox();
		this.pnThanks = new System.Windows.Forms.Panel();
		this.lnkExpresso = new System.Windows.Forms.LinkLabel();
		this.lnkRegexWorkBench = new System.Windows.Forms.LinkLabel();
		this.lnkRegexDesigner = new System.Windows.Forms.LinkLabel();
		this.lbllabel3 = new System.Windows.Forms.Label();
		this.lnkRegexLib = new System.Windows.Forms.LinkLabel();
		this.lnkDarren = new System.Windows.Forms.LinkLabel();
		this.lnkSyncFusion = new System.Windows.Forms.LinkLabel();
		this.lnkRoy = new System.Windows.Forms.LinkLabel();
		this.lbllabel2 = new System.Windows.Forms.Label();
		this.lnkHomepage = new System.Windows.Forms.LinkLabel();
		this.lnSupport = new System.Windows.Forms.LinkLabel();
		this.lblCopyright2 = new System.Windows.Forms.Label();
		this.lnkGift = new System.Windows.Forms.LinkLabel();
		this.pnThanks.SuspendLayout();
		this.SuspendLayout();
		// 
		// lstReferenceAssemblies
		// 
		this.lstReferenceAssemblies.Enabled = false;
		this.lstReferenceAssemblies.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		this.lstReferenceAssemblies.HorizontalScrollbar = true;
		this.lstReferenceAssemblies.Location = new System.Drawing.Point(7, 395);
		this.lstReferenceAssemblies.Name = "lstReferenceAssemblies";
		this.lstReferenceAssemblies.Size = new System.Drawing.Size(484, 82);
		this.lstReferenceAssemblies.Sorted = true;
		this.lstReferenceAssemblies.TabIndex = 7;
		// 
		// cmdOk
		// 
		this.cmdOk.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
		this.cmdOk.CausesValidation = false;
		this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.cmdOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		this.cmdOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdOk.Location = new System.Drawing.Point(408, 343);
		this.cmdOk.Name = "cmdOk";
		this.cmdOk.Size = new System.Drawing.Size(84, 20);
		this.cmdOk.TabIndex = 0;
		this.cmdOk.Text = "OK";
		// 
		// lblReferencedAssemblies
		// 
		this.lblReferencedAssemblies.BackColor = System.Drawing.Color.White;
		this.lblReferencedAssemblies.Enabled = false;
		this.lblReferencedAssemblies.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		this.lblReferencedAssemblies.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblReferencedAssemblies.Location = new System.Drawing.Point(9, 380);
		this.lblReferencedAssemblies.Name = "lblReferencedAssemblies";
		this.lblReferencedAssemblies.Size = new System.Drawing.Size(387, 14);
		this.lblReferencedAssemblies.TabIndex = 2;
		this.lblReferencedAssemblies.Text = "Referenced Assemblies:";
		// 
		// lblCopyright
		// 
		this.lblCopyright.BackColor = System.Drawing.Color.White;
		this.lblCopyright.Font = new System.Drawing.Font("Arial", 7F);
		this.lblCopyright.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblCopyright.Location = new System.Drawing.Point(6, 353);
		this.lblCopyright.Name = "lblCopyright";
		this.lblCopyright.Size = new System.Drawing.Size(164, 11);
		this.lblCopyright.TabIndex = 2;
		this.lblCopyright.Text = "lblCopyright";
		// 
		// cmdReferences
		// 
		this.cmdReferences.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
		this.cmdReferences.CausesValidation = false;
		this.cmdReferences.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		this.cmdReferences.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdReferences.Location = new System.Drawing.Point(315, 343);
		this.cmdReferences.Name = "cmdReferences";
		this.cmdReferences.Size = new System.Drawing.Size(87, 20);
		this.cmdReferences.TabIndex = 8;
		this.cmdReferences.Text = "References >>";
		this.cmdReferences.Click += new System.EventHandler(this.cmdReferences_Click);
		// 
		// lblVersion
		// 
		this.lblVersion.BackColor = System.Drawing.Color.White;
		this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
		this.lblVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblVersion.Location = new System.Drawing.Point(16, 104);
		this.lblVersion.Name = "lblVersion";
		this.lblVersion.Size = new System.Drawing.Size(197, 17);
		this.lblVersion.TabIndex = 9;
		this.lblVersion.Text = "lblVersion";
		// 
		// picLogo
		// 
		this.picLogo.BackColor = System.Drawing.Color.White;
		this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
		this.picLogo.Location = new System.Drawing.Point(40, 16);
		this.picLogo.Name = "picLogo";
		this.picLogo.Size = new System.Drawing.Size(424, 80);
		this.picLogo.TabIndex = 18;
		this.picLogo.TabStop = false;
		// 
		// picIcon
		// 
		this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
		this.picIcon.Location = new System.Drawing.Point(352, 144);
		this.picIcon.Name = "picIcon";
		this.picIcon.Size = new System.Drawing.Size(128, 120);
		this.picIcon.TabIndex = 17;
		this.picIcon.TabStop = false;
		// 
		// pnThanks
		// 
		this.pnThanks.Controls.Add(this.lnkExpresso);
		this.pnThanks.Controls.Add(this.lnkRegexWorkBench);
		this.pnThanks.Controls.Add(this.lnkRegexDesigner);
		this.pnThanks.Controls.Add(this.lbllabel3);
		this.pnThanks.Controls.Add(this.lnkRegexLib);
		this.pnThanks.Controls.Add(this.lnkDarren);
		this.pnThanks.Controls.Add(this.lnkSyncFusion);
		this.pnThanks.Controls.Add(this.lnkRoy);
		this.pnThanks.Controls.Add(this.lbllabel2);
		this.pnThanks.Location = new System.Drawing.Point(16, 136);
		this.pnThanks.Name = "pnThanks";
		this.pnThanks.Size = new System.Drawing.Size(320, 160);
		this.pnThanks.TabIndex = 19;
		// 
		// lnkExpresso
		// 
		this.lnkExpresso.Location = new System.Drawing.Point(32, 136);
		this.lnkExpresso.Name = "lnkExpresso";
		this.lnkExpresso.Size = new System.Drawing.Size(72, 16);
		this.lnkExpresso.TabIndex = 7;
		this.lnkExpresso.TabStop = true;
		this.lnkExpresso.Text = "Expresso";
		this.lnkExpresso.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkExpresso_LinkClicked);
		// 
		// lnkRegexWorkBench
		// 
		this.lnkRegexWorkBench.Location = new System.Drawing.Point(32, 120);
		this.lnkRegexWorkBench.Name = "lnkRegexWorkBench";
		this.lnkRegexWorkBench.Size = new System.Drawing.Size(196, 16);
		this.lnkRegexWorkBench.TabIndex = 6;
		this.lnkRegexWorkBench.TabStop = true;
		this.lnkRegexWorkBench.Text = "Eric Gunnerson\'s Regex WorkBench";
		this.lnkRegexWorkBench.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRegexWorkBench_LinkClicked);
		// 
		// lnkRegexDesigner
		// 
		this.lnkRegexDesigner.Location = new System.Drawing.Point(32, 104);
		this.lnkRegexDesigner.Name = "lnkRegexDesigner";
		this.lnkRegexDesigner.Size = new System.Drawing.Size(280, 16);
		this.lnkRegexDesigner.TabIndex = 5;
		this.lnkRegexDesigner.TabStop = true;
		this.lnkRegexDesigner.Text = "Chris Sells\' And Michael Weinhardt\'s Regex Designer";
		this.lnkRegexDesigner.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRegexDesigner_LinkClicked);
		// 
		// lbllabel3
		// 
		this.lbllabel3.Location = new System.Drawing.Point(24, 32);
		this.lbllabel3.Name = "lbllabel3";
		this.lbllabel3.Size = new System.Drawing.Size(88, 16);
		this.lbllabel3.TabIndex = 4;
		this.lbllabel3.Text = "A big thanks to:";
		// 
		// lnkRegexLib
		// 
		this.lnkRegexLib.Location = new System.Drawing.Point(32, 72);
		this.lnkRegexLib.Name = "lnkRegexLib";
		this.lnkRegexLib.Size = new System.Drawing.Size(80, 16);
		this.lnkRegexLib.TabIndex = 3;
		this.lnkRegexLib.TabStop = true;
		this.lnkRegexLib.Text = "RegexLib.com";
		this.lnkRegexLib.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRegexLib_LinkClicked);
		// 
		// lnkDarren
		// 
		this.lnkDarren.Location = new System.Drawing.Point(32, 56);
		this.lnkDarren.Name = "lnkDarren";
		this.lnkDarren.Size = new System.Drawing.Size(160, 16);
		this.lnkDarren.TabIndex = 2;
		this.lnkDarren.TabStop = true;
		this.lnkDarren.Text = "Darren Neimke\'s Web service";
		this.lnkDarren.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDarren_LinkClicked);
		// 
		// lnkSyncFusion
		// 
		this.lnkSyncFusion.Location = new System.Drawing.Point(32, 88);
		this.lnkSyncFusion.Name = "lnkSyncFusion";
		this.lnkSyncFusion.Size = new System.Drawing.Size(72, 16);
		this.lnkSyncFusion.TabIndex = 4;
		this.lnkSyncFusion.TabStop = true;
		this.lnkSyncFusion.Text = "SyncFusion";
		this.lnkSyncFusion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSyncFusion_LinkClicked);
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
		// lbllabel2
		// 
		this.lbllabel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
		this.lbllabel2.Location = new System.Drawing.Point(8, 8);
		this.lbllabel2.Name = "lbllabel2";
		this.lbllabel2.Size = new System.Drawing.Size(24, 16);
		this.lbllabel2.TabIndex = 2;
		this.lbllabel2.Text = "By:";
		// 
		// lnkHomepage
		// 
		this.lnkHomepage.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
		this.lnkHomepage.Location = new System.Drawing.Point(352, 280);
		this.lnkHomepage.Name = "lnkHomepage";
		this.lnkHomepage.Size = new System.Drawing.Size(64, 16);
		this.lnkHomepage.TabIndex = 8;
		this.lnkHomepage.TabStop = true;
		this.lnkHomepage.Text = "Homepage";
		this.lnkHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHomepage_LinkClicked);
		// 
		// lnSupport
		// 
		this.lnSupport.Location = new System.Drawing.Point(424, 280);
		this.lnSupport.Name = "lnSupport";
		this.lnSupport.Size = new System.Drawing.Size(60, 16);
		this.lnSupport.TabIndex = 8;
		this.lnSupport.TabStop = true;
		this.lnSupport.Text = "Support";
		this.lnSupport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnSupport_LinkClicked);
		// 
		// lblCopyright2
		// 
		this.lblCopyright2.BackColor = System.Drawing.Color.White;
		this.lblCopyright2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.lblCopyright2.Location = new System.Drawing.Point(100, 304);
		this.lblCopyright2.Name = "lblCopyright2";
		this.lblCopyright2.Size = new System.Drawing.Size(296, 15);
		this.lblCopyright2.TabIndex = 10;
		this.lblCopyright2.Text = "This program is totally free to use and distribute. However, you can";
		// 
		// lnkGift
		// 
		this.lnkGift.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
		this.lnkGift.Location = new System.Drawing.Point(100, 320);
		this.lnkGift.Name = "lnkGift";
		this.lnkGift.Size = new System.Drawing.Size(296, 16);
		this.lnkGift.TabIndex = 8;
		this.lnkGift.TabStop = true;
		this.lnkGift.Text = "Show your appreciation with an Amazon gift certificate";
		this.lnkGift.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGift_LinkClicked);
		// 
		// AboutDialog
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		this.BackColor = System.Drawing.Color.White;
		this.CausesValidation = false;
		this.ClientSize = new System.Drawing.Size(497, 367);
		this.Controls.Add(this.pnThanks);
		this.Controls.Add(this.picLogo);
		this.Controls.Add(this.picIcon);
		this.Controls.Add(this.lblCopyright2);
		this.Controls.Add(this.lblVersion);
		this.Controls.Add(this.cmdReferences);
		this.Controls.Add(this.cmdOk);
		this.Controls.Add(this.lstReferenceAssemblies);
		this.Controls.Add(this.lblReferencedAssemblies);
		this.Controls.Add(this.lblCopyright);
		this.Controls.Add(this.lnkHomepage);
		this.Controls.Add(this.lnSupport);
		this.Controls.Add(this.lnkGift);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "AboutDialog";
		this.ShowInTaskbar = false;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "About App";
		this.pnThanks.ResumeLayout(false);
		this.ResumeLayout(false);

	}
	#endregion

	#region Event Handlers


	private void SendSupportEmail()
	{
		// Send support email, with system and assembly reference details
		StringBuilder   sb = new StringBuilder();
		sb.Append("mailto:");
		sb.Append(@"RegulatorSupport@osherove.com");
		sb.AppendFormat("?subject=Support request for Regulator");
		sb.AppendFormat("&body={0}", BuildSupportMailBody());
		Process.Start(sb.ToString());
	}

    
	private void cmdReferences_Click(object sender, System.EventArgs e)
	{
		if( Size == _collapsedSize )
		{
			lblReferencedAssemblies.Enabled = true;
			lstReferenceAssemblies.Enabled = true;
			Size = _expandedSize;
			cmdReferences.Text = "References <<";
		}
		else
		{
			Size = _collapsedSize;
			lblReferencedAssemblies.Enabled = false;
			lstReferenceAssemblies.Enabled = false;
			cmdReferences.Text = "References >>";
		}
	}
    
	#endregion
       
	private string BuildSupportMailBody()
	{
		// Build support mail body, with system details
		StringBuilder   sb = new StringBuilder();
		sb.Append("Problem description:%0d%0d");
		sb.Append("Steps to reproduce:%0d%0d");
		sb.Append("Expected behavior:%0d%0d");
		sb.Append("Actual behavior:%0d%0d");
		sb.Append("System details%0d%0d");
		sb.Append(".NET Framework:%0d%0d");
		sb.Append("Operating System:%0d%0d");
		sb.Append("Machine:%0d%0d");
		sb.Append("Referenced Assemblies:%0d");

		foreach( AssemblyName assemName in Assembly.GetEntryAssembly().GetReferencedAssemblies() )
		{
			sb.Append(assemName.Name).Append(" v").Append(assemName.Version.ToString()).Append("%0d");
		}

		return sb.ToString();
	}

	private string BuildSalesMailBody()
	{
		return "";
	}

	private Size   _collapsedSize = new Size(503, 400);
	private Size   _expandedSize = new Size(503, 509);

	private void lnkRegexDesigner_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
	{
		LinkIt(@"http://www.sellsbrothers.com/tools/#regexd");
	}


	private void LinkIt(string link)
	{
		AboutDialog.DoLinkIt(link);

	}

	public static  void DoLinkIt(string link)
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

	private void lnkDarren_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
	{
		LinkIt(@"http://weblogs.asp.net/dneimke");
	}

	private void lnkRegexLib_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
	{
		LinkIt(@"http://www.regexlib.com");
		
	}

	private void lnkSyncFusion_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
	{
		LinkIt(@"http://www.syncfusion.com");
	
	}

	private void lnkRegexWorkBench_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
	{
		LinkIt(@"http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=c712f2df-b026-4d58-8961-4ee2729d7322");
	}

	private void lnkExpresso_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
	{
		LinkIt(@"http://www.codeproject.com/dotnet/expresso.asp?target=expresso");
	}

	private void lnkHomepage_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
	{
		LinkIt(@"http://regulator.sourceforge.net");
	}

	private void lnkGift_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
	{
		ShowGiftRequest();
	}

	private  void privateShowGiftRequest()
	{

		AboutDialog.ShowGiftRequest();
	}
	public static  void ShowGiftRequest()
	{
		try
		{
			string text = @"This program is totally free to use and distribute.
If you DO want to show your appreciation, feel free to send me
and amazon gift certificate to any amout you choose.
Simply mail the gift certificate to 
RoyOsherove@hotmail.com 
Thanks, 
Roy Osherove
(Press OK to open the Amazon Gifts homepage, or CANCEL to return to the program)";

			DialogResult result =   MessageBox.Show(text,"Show your appreciation",MessageBoxButtons.OKCancel,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
			if(result==DialogResult.OK)
			{
				AboutDialog.DoLinkIt(@"http://www.amazon.com/exec/obidos/gc-email-order1");	
			}
		}
		catch(Exception e)
		{
	     
		}
	}

	private void lnSupport_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
	{
		SendSupportEmail();
	}

}