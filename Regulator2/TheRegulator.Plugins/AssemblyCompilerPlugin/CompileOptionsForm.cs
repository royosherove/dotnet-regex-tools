using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using Regulator.SDK;
using Regulator.SDK.Plugins;

public class CompileOptionsForm : GenericDialogPlugin
{
	private System.Windows.Forms.GroupBox groupBox1;
	public  System.Windows.Forms.TextBox txtFileName;
	private System.Windows.Forms.Button cmdBrowse;
	private System.Windows.Forms.GroupBox groupBox2;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label label3;
	public  System.Windows.Forms.ComboBox listProtection;
	public  System.Windows.Forms.TextBox txtClass;
	public  System.Windows.Forms.TextBox txtNamespace;
	private System.Windows.Forms.SaveFileDialog saveAssemblyDialog;
	private System.Windows.Forms.Button cmdCancel;
	private System.Windows.Forms.ErrorProvider errorProvider;
	private System.Windows.Forms.Button cmdCompile;
	private System.Windows.Forms.Label lblStatus;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.Container components = null;

	public CompileOptionsForm()
	{
		//
		// Required for Windows Form Designer support
		//
		InitializeComponent();
		this.PluginName= "Assembly Generator";
		this.MenuCation= "Compile to Assembly...";
		this.Shortcut= Shortcut.CtrlShiftC;
		
	}

	public override void ShowAbout()
	{
		new AboutDialog().ShowDialog();
	}


	private bool Compile()
	{
		AssemblyName   assemName = new AssemblyName();
		assemName.Name = Path.GetFileNameWithoutExtension(txtFileName.Text);

		string _regex = AppContext.Instance.ActiveProject.Regex;
		RegexOptions options = AppContext.Instance.ActiveProject.Options;

		RegexCompilationInfo   info = new RegexCompilationInfo(_regex, options, txtClass.Text, txtNamespace.Text, (listProtection.Text == "Public"));
		Regex.CompileToAssembly(new RegexCompilationInfo[] { info }, assemName);
		return true;
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CompileOptionsForm));
		this.label1 = new System.Windows.Forms.Label();
		this.txtFileName = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.saveAssemblyDialog = new System.Windows.Forms.SaveFileDialog();
		this.cmdCancel = new System.Windows.Forms.Button();
		this.listProtection = new System.Windows.Forms.ComboBox();
		this.txtNamespace = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtClass = new System.Windows.Forms.TextBox();
		this.cmdBrowse = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.errorProvider = new System.Windows.Forms.ErrorProvider();
		this.cmdCompile = new System.Windows.Forms.Button();
		this.lblStatus = new System.Windows.Forms.Label();
		this.groupBox2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.SuspendLayout();
		// 
		// label1
		// 
		this.label1.Location = new System.Drawing.Point(8, 24);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(72, 23);
		this.label1.TabIndex = 4;
		this.label1.Text = "&Namespace:";
		// 
		// txtFileName
		// 
		this.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtFileName.Location = new System.Drawing.Point(8, 24);
		this.txtFileName.Name = "txtFileName";
		this.txtFileName.Size = new System.Drawing.Size(368, 21);
		this.txtFileName.TabIndex = 1;
		this.txtFileName.Text = "MyRegexFileName.dll";
		this.txtFileName.Validating += new System.ComponentModel.CancelEventHandler(this.txtFileName_Validating);
		// 
		// label3
		// 
		this.label3.Location = new System.Drawing.Point(8, 72);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(72, 23);
		this.label3.TabIndex = 8;
		this.label3.Text = "&Protection:";
		// 
		// saveAssemblyDialog
		// 
		this.saveAssemblyDialog.DefaultExt = "dll";
		this.saveAssemblyDialog.FileName = "regex";
		this.saveAssemblyDialog.Filter = "Assemblies (*.dll)|*.dll";
		// 
		// cmdCancel
		// 
		this.cmdCancel.CausesValidation = false;
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.Location = new System.Drawing.Point(360, 201);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.TabIndex = 11;
		this.cmdCancel.Text = "Cancel";
		this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
		// 
		// listProtection
		// 
		this.listProtection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.listProtection.DropDownWidth = 121;
		this.listProtection.Items.AddRange(new object[] {
															"Private",
															"Public"});
		this.listProtection.Location = new System.Drawing.Point(88, 72);
		this.listProtection.Name = "listProtection";
		this.listProtection.TabIndex = 9;
		// 
		// txtNamespace
		// 
		this.txtNamespace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtNamespace.Location = new System.Drawing.Point(88, 24);
		this.txtNamespace.Name = "txtNamespace";
		this.txtNamespace.Size = new System.Drawing.Size(288, 21);
		this.txtNamespace.TabIndex = 5;
		this.txtNamespace.Text = "MyRegexNamespace";
		this.txtNamespace.Validating += new System.ComponentModel.CancelEventHandler(this.txtNamespace_Validating);
		// 
		// label2
		// 
		this.label2.Location = new System.Drawing.Point(8, 48);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(72, 23);
		this.label2.TabIndex = 6;
		this.label2.Text = "&Class:";
		// 
		// txtClass
		// 
		this.txtClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtClass.Location = new System.Drawing.Point(88, 48);
		this.txtClass.Name = "txtClass";
		this.txtClass.Size = new System.Drawing.Size(288, 21);
		this.txtClass.TabIndex = 7;
		this.txtClass.Text = "MyRegexClass";
		this.txtClass.Validating += new System.ComponentModel.CancelEventHandler(this.txtClass_Validating);
		// 
		// cmdBrowse
		// 
		this.cmdBrowse.Location = new System.Drawing.Point(384, 24);
		this.cmdBrowse.Name = "cmdBrowse";
		this.cmdBrowse.Size = new System.Drawing.Size(24, 20);
		this.cmdBrowse.TabIndex = 2;
		this.cmdBrowse.Text = "...";
		this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
		// 
		// groupBox2
		// 
		this.groupBox2.Controls.Add(this.listProtection);
		this.groupBox2.Controls.Add(this.txtNamespace);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.txtClass);
		this.groupBox2.Location = new System.Drawing.Point(13, 81);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(419, 108);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "&Type";
		// 
		// groupBox1
		// 
		this.groupBox1.Controls.Add(this.cmdBrowse);
		this.groupBox1.Controls.Add(this.txtFileName);
		this.groupBox1.Location = new System.Drawing.Point(13, 9);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(419, 60);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "&Assembly File Name";
		// 
		// errorProvider
		// 
		this.errorProvider.ContainerControl = this;
		// 
		// cmdCompile
		// 
		this.cmdCompile.Location = new System.Drawing.Point(8, 200);
		this.cmdCompile.Name = "cmdCompile";
		this.cmdCompile.TabIndex = 12;
		this.cmdCompile.Text = "&Compile";
		this.cmdCompile.Click += new System.EventHandler(this.cmdCompile_Click);
		// 
		// lblStatus
		// 
		this.lblStatus.ForeColor = System.Drawing.Color.Blue;
		this.lblStatus.Location = new System.Drawing.Point(88, 208);
		this.lblStatus.Name = "lblStatus";
		this.lblStatus.Size = new System.Drawing.Size(256, 16);
		this.lblStatus.TabIndex = 13;
		this.lblStatus.Text = "Compiling...";
		this.lblStatus.Visible = false;
		// 
		// CompileOptionsForm
		// 
		this.AcceptButton = this.cmdCompile;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
		this.CancelButton = this.cmdCancel;
		this.ClientSize = new System.Drawing.Size(442, 239);
		this.Controls.Add(this.lblStatus);
		this.Controls.Add(this.cmdCompile);
		this.Controls.Add(this.cmdCancel);
		this.Controls.Add(this.groupBox2);
		this.Controls.Add(this.groupBox1);
		this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.MaximizeBox = false;
		this.MenuCation = "Compile Assembly Options";
		this.MinimizeBox = false;
		this.Name = "CompileOptionsForm";
		this.ShowInTaskbar = false;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Compile Assembly Options";
		this.Load += new System.EventHandler(this.CompileOptionsForm_Load);
		this.groupBox2.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.ResumeLayout(false);

	}
	#endregion
    
	#region Event Handlers
	private void cmdBrowse_Click(object sender, System.EventArgs e)
	{
		saveAssemblyDialog.FileName = txtFileName.Text;
		if( saveAssemblyDialog.ShowDialog() != DialogResult.OK ) return;
		txtFileName.Text = saveAssemblyDialog.FileName;
	}

	private void cmdOK_Click(object sender, System.EventArgs e)
	{
		// Validate controls by setting focus to them
		bool   valid = true;
		foreach( Control control in Controls )
		{
			control.Focus();
			if( !Validate() ) valid = false;
		}
		if( valid ) Close();
	}
    
	private void cmdCancel_Click(object sender, System.EventArgs e)
	{
		lblStatus.Visible=false;
		Close();
	}    

	private void txtFileName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
	{
		string   error = "";
            
		// Check assembly file name has been entered
		string   assemFileName = Path.GetFileName(txtFileName.Text);
		if( assemFileName == "" )
		{
			error = "FileName can't be empty.";
			e.Cancel = true;
			errorProvider.SetError(txtFileName, error);
			return;
		}
        
		// Check that folder is valid
		string   assemDir = Path.GetDirectoryName(txtFileName.Text);
		if(assemDir != "")
		{
			if( !Directory.Exists(assemDir) )
			{   
				error = string.Format("Folder {0} does not exist.", assemDir);
				e.Cancel = true;
				errorProvider.SetError(txtFileName, error);
				return;
			}
		}
		errorProvider.SetError(txtFileName, error);
	}

	private void txtNamespace_Validating(object sender, System.ComponentModel.CancelEventArgs e)
	{
		// Check Namespace name
		string   error = "";
		Regex    reNamespace = new Regex(@"^(([a-zA-Z])(\.?[a-zA-Z][a-zA-Z0-9]*)*)$");
		if( !reNamespace.IsMatch(txtNamespace.Text) )
		{
			error = "Please provide a valid Namespace name.";
			e.Cancel = true;
		}
		errorProvider.SetError(txtNamespace, error);
	}

	private void txtClass_Validating(object sender, System.ComponentModel.CancelEventArgs e)
	{
		// Check Class name
		string   error = "";
		Regex    reClass = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");
		if( !reClass.IsMatch(txtClass.Text) )
		{
			error = "Please provide a valid class name.";
			e.Cancel = true;
		}
		errorProvider.SetError(txtClass, error);
	}

	private void CompileOptionsForm_Load(object sender, System.EventArgs e)
	{
		// Select first control on the form
		listProtection.SelectedIndex=0;
		txtFileName.SelectAll();
		txtFileName.Focus();
	}
	#endregion

	private void cmdCompile_Click(object sender, System.EventArgs e)
	{
		StartCompileProcedure();
	}

	private void StartCompileProcedure()
	{
		try
		{
			cmdCompile.Enabled=false;
			lblStatus.Text= "Compiling...";
			lblStatus.Visible=true;
			Application.DoEvents();
			Application.DoEvents();

			if(Compile())
			{
				lblStatus.Text= "Done!";
			}
			else
			{
				lblStatus.Text= "Oops! There was a problem compiling.";
			}

		}
		catch(Exception e)
		{
			lblStatus.Text= e.Message;
		}
		finally
		{
			cmdCompile.Enabled=true;
			
		}
	}
}