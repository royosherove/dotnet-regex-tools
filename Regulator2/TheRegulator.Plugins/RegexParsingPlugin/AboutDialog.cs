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
    private System.Windows.Forms.Label lblProduct;
    private System.Windows.Forms.Label lblReferencedAssemblies;
    private System.Windows.Forms.LinkLabel lnkSBWebsite;
    private System.Windows.Forms.ListBox lstReferenceAssemblies;
    private System.Windows.Forms.Label lblDescription;
    private System.Windows.Forms.Label lblCopyright;
    private System.Windows.Forms.Button cmdReferences;
    private System.Windows.Forms.Label lblVersion;
	private System.Windows.Forms.Label lblCopyright2;
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
        Assembly        assem = Assembly.GetExecutingAssembly();
        AssemblyName    assemName = assem.GetName();
        object[]        attribs = null;

        // Title
        attribs = assem.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
        string  title = (attribs.Length != 0 ? ((AssemblyTitleAttribute)attribs[0]).Title : "");
        if( title.Length == 0 ) title = System.IO.Path.GetFileName(assem.Location);
        Text = "About " + title;
        lblProduct.Text = title;

        // Version
        string  version = assem.GetName().Version.ToString();
        lblVersion.Text = "v" + version + " (beta)";
        
        // Description
        attribs = assem.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), true);
        string  description = (attribs.Length != 0 ? ((AssemblyDescriptionAttribute)attribs[0]).Description : "");
        lblDescription.Text = description;

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
		this.lblProduct = new System.Windows.Forms.Label();
		this.cmdOk = new System.Windows.Forms.Button();
		this.lblReferencedAssemblies = new System.Windows.Forms.Label();
		this.lblDescription = new System.Windows.Forms.Label();
		this.lnkSBWebsite = new System.Windows.Forms.LinkLabel();
		this.lblCopyright = new System.Windows.Forms.Label();
		this.cmdReferences = new System.Windows.Forms.Button();
		this.lblVersion = new System.Windows.Forms.Label();
		this.lblCopyright2 = new System.Windows.Forms.Label();
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
		// lblProduct
		// 
		this.lblProduct.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(153)), ((System.Byte)(204)), ((System.Byte)(0)));
		this.lblProduct.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
		this.lblProduct.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblProduct.Location = new System.Drawing.Point(287, 34);
		this.lblProduct.Name = "lblProduct";
		this.lblProduct.Size = new System.Drawing.Size(199, 22);
		this.lblProduct.TabIndex = 2;
		this.lblProduct.Text = "lblProduct";
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
		this.lblReferencedAssemblies.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(153)), ((System.Byte)(204)), ((System.Byte)(0)));
		this.lblReferencedAssemblies.Enabled = false;
		this.lblReferencedAssemblies.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		this.lblReferencedAssemblies.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblReferencedAssemblies.Location = new System.Drawing.Point(9, 380);
		this.lblReferencedAssemblies.Name = "lblReferencedAssemblies";
		this.lblReferencedAssemblies.Size = new System.Drawing.Size(387, 14);
		this.lblReferencedAssemblies.TabIndex = 2;
		this.lblReferencedAssemblies.Text = "Referenced Assemblies:";
		// 
		// lblDescription
		// 
		this.lblDescription.BackColor = System.Drawing.Color.Black;
		this.lblDescription.Font = new System.Drawing.Font("Arial", 9F);
		this.lblDescription.ForeColor = System.Drawing.Color.White;
		this.lblDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblDescription.Location = new System.Drawing.Point(16, 2);
		this.lblDescription.Name = "lblDescription";
		this.lblDescription.Size = new System.Drawing.Size(461, 14);
		this.lblDescription.TabIndex = 2;
		this.lblDescription.Text = "lblDescription";
		// 
		// lnkSBWebsite
		// 
		this.lnkSBWebsite.BackColor = System.Drawing.Color.White;
		this.lnkSBWebsite.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
		this.lnkSBWebsite.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lnkSBWebsite.LinkArea = new System.Windows.Forms.LinkArea(0, 21);
		this.lnkSBWebsite.Location = new System.Drawing.Point(169, 352);
		this.lnkSBWebsite.Name = "lnkSBWebsite";
		this.lnkSBWebsite.Size = new System.Drawing.Size(157, 14);
		this.lnkSBWebsite.TabIndex = 4;
		this.lnkSBWebsite.TabStop = true;
		this.lnkSBWebsite.Text = "Regex WorkBench";
		this.lnkSBWebsite.VisitedLinkColor = System.Drawing.Color.Blue;
		this.lnkSBWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSBWebsite_LinkClicked);
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
		this.lblVersion.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(153)), ((System.Byte)(204)), ((System.Byte)(0)));
		this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
		this.lblVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblVersion.Location = new System.Drawing.Point(289, 57);
		this.lblVersion.Name = "lblVersion";
		this.lblVersion.Size = new System.Drawing.Size(197, 17);
		this.lblVersion.TabIndex = 9;
		this.lblVersion.Text = "lblVersion";
		// 
		// lblCopyright2
		// 
		this.lblCopyright2.BackColor = System.Drawing.Color.White;
		this.lblCopyright2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.lblCopyright2.Location = new System.Drawing.Point(136, 297);
		this.lblCopyright2.Name = "lblCopyright2";
		this.lblCopyright2.Size = new System.Drawing.Size(360, 43);
		this.lblCopyright2.TabIndex = 10;
		this.lblCopyright2.Text = "The code for this plugin was taken from Eric Gunnerson\'s Regex Workbench tool";
		// 
		// AboutDialog
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(153)), ((System.Byte)(204)), ((System.Byte)(0)));
		this.CausesValidation = false;
		this.ClientSize = new System.Drawing.Size(497, 367);
		this.Controls.Add(this.lblCopyright2);
		this.Controls.Add(this.lblVersion);
		this.Controls.Add(this.cmdReferences);
		this.Controls.Add(this.cmdOk);
		this.Controls.Add(this.lstReferenceAssemblies);
		this.Controls.Add(this.lnkSBWebsite);
		this.Controls.Add(this.lblDescription);
		this.Controls.Add(this.lblProduct);
		this.Controls.Add(this.lblReferencedAssemblies);
		this.Controls.Add(this.lblCopyright);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "AboutDialog";
		this.ShowInTaskbar = false;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "About App";
		this.ResumeLayout(false);

	}
	#endregion

    #region Event Handlers

    private void lnkSBWebsite_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
    {
        // Browse to SB website
        Process.Start("http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=c712f2df-b026-4d58-8961-4ee2729d7322");
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
}