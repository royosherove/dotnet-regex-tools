using System;
using Regulator.SDK.Plugins;
using System.Threading;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Regulator.SDK;
using Regulator.SDK.Plugins.RegexLib.Services;	
using Regulator.SDK.ApplicationSettings;


namespace Regulator.SDK.Plugins.RegexLib
{
	/// <summary>
	/// Summary description for SubmitWizard.
	/// </summary>
	public class SubmitWizard : GenericDialogPlugin
	{
		private Syncfusion.Windows.Forms.Tools.WizardControl wizardControl1;
		private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel1;
		private Syncfusion.Windows.Forms.Tools.WizardContainer wizardContainer1;
		private Syncfusion.Windows.Forms.Tools.WizardControlPage wizardControlPage1;
		private System.Windows.Forms.Label label3;
		private Syncfusion.Windows.Forms.Tools.WizardControlPage wizardControlPage2;
		internal System.Windows.Forms.GroupBox UserInfoGroupBox;
		internal System.Windows.Forms.Label surnameLabel;
		internal System.Windows.Forms.Label firstNameLabel;
		internal System.Windows.Forms.Label emailLabel;
		private Syncfusion.Windows.Forms.Tools.WizardControlPage wizardControlPage3;
		private System.Windows.Forms.TextBox txtRegex;
		private System.Windows.Forms.Label lbllabel1;
		private Syncfusion.Windows.Forms.Tools.WizardControlPage wizardControlPage4;
		internal System.Windows.Forms.Label descriptionLabel;
		private System.Windows.Forms.Label lbllabel2;
		internal System.Windows.Forms.TextBox txtLastName;
		internal System.Windows.Forms.TextBox txtFirstName;
		internal System.Windows.Forms.TextBox txtEmail;
		internal System.Windows.Forms.Label lbllabel3;
		internal System.Windows.Forms.TextBox txtDescription;
		private Syncfusion.Windows.Forms.Tools.WizardControlPage wizardControlPage5;
		private Syncfusion.Windows.Forms.Tools.EditableList lstMatches;
		internal System.Windows.Forms.Label lbllabel5;
		private System.Windows.Forms.Button cmdAddToMatches;
		private System.Windows.Forms.Button cmdRemoveFromMatches;
		private Syncfusion.Windows.Forms.Tools.WizardControlPage wizardControlPage6;
		private System.Windows.Forms.Button cmdRemoveFromNonMatches;
		private System.Windows.Forms.Button cmdAddToNonMatches;
		internal System.Windows.Forms.Label label1;
		private Syncfusion.Windows.Forms.Tools.EditableList lstNonMatches;
		private System.Windows.Forms.Label lbllabel6;
		private Syncfusion.Windows.Forms.Tools.WizardControlPage wizardControlPage7;
		private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel4;
		private System.Windows.Forms.Label lbllabel8;
		private System.Windows.Forms.Label lbllabel9;
		private System.Windows.Forms.Label lblSubmitStatus;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblPleaseWait;
		private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel2;
		private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel5;
		private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel3;
		internal System.Windows.Forms.Label lbllabel4;
		private System.Windows.Forms.LinkLabel lnkSubmit;
		private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel6;
		private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel7;
		internal System.Windows.Forms.Label lbllabel7;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.TextBox txtSource;
		internal System.Windows.Forms.Label lbllabel10;
		private Syncfusion.Windows.Forms.Tools.GradientLabel lblTopBanner;
		private Syncfusion.Windows.Forms.Tools.GradientLabel lblStartBanner;
		private System.ComponentModel.IContainer components;
		

		public SubmitWizard()
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

		protected override void OnDialogHide()
		{
			base.OnDialogHide ();
			SaveSettings();
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SubmitWizard));
			this.wizardControl1 = new Syncfusion.Windows.Forms.Tools.WizardControl();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.gradientPanel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
			this.lblTopBanner = new Syncfusion.Windows.Forms.Tools.GradientLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.wizardContainer1 = new Syncfusion.Windows.Forms.Tools.WizardContainer();
			this.wizardControlPage1 = new Syncfusion.Windows.Forms.Tools.WizardControlPage(this.components);
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.lblStartBanner = new Syncfusion.Windows.Forms.Tools.GradientLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.wizardControlPage2 = new Syncfusion.Windows.Forms.Tools.WizardControlPage(this.components);
			this.lbllabel4 = new System.Windows.Forms.Label();
			this.gradientPanel3 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
			this.lbllabel6 = new System.Windows.Forms.Label();
			this.UserInfoGroupBox = new System.Windows.Forms.GroupBox();
			this.txtLastName = new System.Windows.Forms.TextBox();
			this.txtFirstName = new System.Windows.Forms.TextBox();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.surnameLabel = new System.Windows.Forms.Label();
			this.firstNameLabel = new System.Windows.Forms.Label();
			this.emailLabel = new System.Windows.Forms.Label();
			this.wizardControlPage3 = new Syncfusion.Windows.Forms.Tools.WizardControlPage(this.components);
			this.lbllabel3 = new System.Windows.Forms.Label();
			this.gradientPanel5 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
			this.lbllabel1 = new System.Windows.Forms.Label();
			this.txtRegex = new System.Windows.Forms.TextBox();
			this.wizardControlPage4 = new Syncfusion.Windows.Forms.Tools.WizardControlPage(this.components);
			this.txtSource = new System.Windows.Forms.TextBox();
			this.gradientPanel2 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
			this.lbllabel2 = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.descriptionLabel = new System.Windows.Forms.Label();
			this.lbllabel10 = new System.Windows.Forms.Label();
			this.wizardControlPage5 = new Syncfusion.Windows.Forms.Tools.WizardControlPage(this.components);
			this.lbllabel5 = new System.Windows.Forms.Label();
			this.gradientPanel6 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
			this.lbllabel8 = new System.Windows.Forms.Label();
			this.cmdRemoveFromMatches = new System.Windows.Forms.Button();
			this.cmdAddToMatches = new System.Windows.Forms.Button();
			this.lstMatches = new Syncfusion.Windows.Forms.Tools.EditableList();
			this.wizardControlPage6 = new Syncfusion.Windows.Forms.Tools.WizardControlPage(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.gradientPanel7 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
			this.lbllabel9 = new System.Windows.Forms.Label();
			this.cmdRemoveFromNonMatches = new System.Windows.Forms.Button();
			this.cmdAddToNonMatches = new System.Windows.Forms.Button();
			this.lstNonMatches = new Syncfusion.Windows.Forms.Tools.EditableList();
			this.wizardControlPage7 = new Syncfusion.Windows.Forms.Tools.WizardControlPage(this.components);
			this.lbllabel7 = new System.Windows.Forms.Label();
			this.lnkSubmit = new System.Windows.Forms.LinkLabel();
			this.lblPleaseWait = new System.Windows.Forms.Label();
			this.lblSubmitStatus = new System.Windows.Forms.Label();
			this.gradientPanel4 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
			((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
			this.wizardControl1.SuspendLayout();
			this.gradientPanel1.SuspendLayout();
			this.wizardContainer1.SuspendLayout();
			this.wizardControlPage1.SuspendLayout();
			this.wizardControlPage2.SuspendLayout();
			this.UserInfoGroupBox.SuspendLayout();
			this.wizardControlPage3.SuspendLayout();
			this.wizardControlPage4.SuspendLayout();
			this.wizardControlPage5.SuspendLayout();
			this.wizardControlPage6.SuspendLayout();
			this.wizardControlPage7.SuspendLayout();
			this.SuspendLayout();
			// 
			// wizardControl1
			// 
			// 
			// wizardControl1.BackButton
			// 
			this.wizardControl1.BackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.wizardControl1.BackButton.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.wizardControl1.BackButton.Enabled = false;
			this.wizardControl1.BackButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.wizardControl1.BackButton.Location = new System.Drawing.Point(220, 283);
			this.wizardControl1.BackButton.Name = "backButton";
			this.wizardControl1.BackButton.TabIndex = 1;
			this.wizardControl1.BackButton.Text = "<< &Back";
			this.wizardControl1.BackButton.Visible = false;
			this.wizardControl1.BackColor = System.Drawing.Color.White;
			this.wizardControl1.Banner = this.pictureBox1;
			this.wizardControl1.BannerPanel = this.gradientPanel1;
			// 
			// wizardControl1.CancelButton
			// 
			this.wizardControl1.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.wizardControl1.CancelButton.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.wizardControl1.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.wizardControl1.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.wizardControl1.CancelButton.Location = new System.Drawing.Point(375, 283);
			this.wizardControl1.CancelButton.Name = "cancelButton";
			this.wizardControl1.CancelButton.TabIndex = 3;
			this.wizardControl1.CancelButton.Text = "&Cancel";
			this.wizardControl1.CancelButton.Click += new System.EventHandler(this.wizardControl1_CancelButton_Click);
			this.wizardControl1.Controls.Add(this.wizardContainer1);
			this.wizardControl1.Controls.Add(this.wizardControl1.HelpButton);
			this.wizardControl1.Controls.Add(this.wizardControl1.CancelButton);
			this.wizardControl1.Controls.Add(this.wizardControl1.FinishButton);
			this.wizardControl1.Controls.Add(this.wizardControl1.NextButton);
			this.wizardControl1.Controls.Add(this.gradientPanel1);
			this.wizardControl1.Controls.Add(this.wizardControl1.BackButton);
			this.wizardControl1.Description = this.label4;
			this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			// 
			// wizardControl1.FinishButton
			// 
			this.wizardControl1.FinishButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.wizardControl1.FinishButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.wizardControl1.FinishButton.Location = new System.Drawing.Point(375, 283);
			this.wizardControl1.FinishButton.Name = "finishButton";
			this.wizardControl1.FinishButton.TabIndex = 4;
			this.wizardControl1.FinishButton.Text = "Finish";
			// 
			// wizardControl1.HelpButton
			// 
			this.wizardControl1.HelpButton.Location = new System.Drawing.Point(368, 283);
			this.wizardControl1.HelpButton.Name = "helpButton";
			this.wizardControl1.HelpButton.TabIndex = 5;
			this.wizardControl1.HelpButton.Text = "Help";
			this.wizardControl1.HelpButton.Visible = false;
			this.wizardControl1.Location = new System.Drawing.Point(0, 0);
			this.wizardControl1.Name = "wizardControl1";
			// 
			// wizardControl1.NextButton
			// 
			this.wizardControl1.NextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.wizardControl1.NextButton.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.wizardControl1.NextButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.wizardControl1.NextButton.Location = new System.Drawing.Point(295, 283);
			this.wizardControl1.NextButton.Name = "nextButton";
			this.wizardControl1.NextButton.TabIndex = 2;
			this.wizardControl1.NextButton.Text = "&Next >>";
			this.wizardControl1.SelectedPage = this.wizardControlPage1;
			this.wizardControl1.SelectedWizardPage = this.wizardControlPage1;
			this.wizardControl1.Size = new System.Drawing.Size(450, 311);
			this.wizardControl1.TabIndex = 0;
			this.wizardControl1.Title = this.label2;
			this.wizardControl1.WizardPageContainer = this.wizardContainer1;
			this.wizardControl1.WizardPages = new Syncfusion.Windows.Forms.Tools.WizardControlPage[] {
																										 this.wizardControlPage1,
																										 this.wizardControlPage2,
																										 this.wizardControlPage3,
																										 this.wizardControlPage4,
																										 this.wizardControlPage5,
																										 this.wizardControlPage6,
																										 this.wizardControlPage7};
			this.wizardControl1.Load += new System.EventHandler(this.wizardControl1_Load);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(379, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(61, 61);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// gradientPanel1
			// 
			this.gradientPanel1.BackColor = System.Drawing.Color.White;
			this.gradientPanel1.BorderColor = System.Drawing.Color.Black;
			this.gradientPanel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gradientPanel1.Controls.Add(this.lblTopBanner);
			this.gradientPanel1.Controls.Add(this.pictureBox1);
			this.gradientPanel1.Controls.Add(this.label2);
			this.gradientPanel1.Controls.Add(this.label4);
			this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
			this.gradientPanel1.Name = "gradientPanel1";
			this.gradientPanel1.Size = new System.Drawing.Size(450, 70);
			this.gradientPanel1.TabIndex = 11;
			// 
			// lblTopBanner
			// 
			this.lblTopBanner.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Vertical, System.Drawing.Color.FromArgb(((System.Byte)(237)), ((System.Byte)(240)), ((System.Byte)(247))), System.Drawing.SystemColors.ActiveCaptionText);
			this.lblTopBanner.BorderSides = System.Windows.Forms.Border3DSide.Left;
			this.lblTopBanner.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.lblTopBanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblTopBanner.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.lblTopBanner.Location = new System.Drawing.Point(0, 7);
			this.lblTopBanner.Name = "lblTopBanner";
			this.lblTopBanner.Size = new System.Drawing.Size(368, 56);
			this.lblTopBanner.TabIndex = 2;
			this.lblTopBanner.Text = "Submit expression to RegexLib.com";
			this.lblTopBanner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(10, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 15);
			this.label2.TabIndex = 4;
			this.label2.Text = "Page Title";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(20, 30);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(217, 15);
			this.label4.TabIndex = 5;
			this.label4.Text = "This is the description of the Wizard Page";
			// 
			// wizardContainer1
			// 
			this.wizardContainer1.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.wizardContainer1.Controls.Add(this.wizardControlPage1);
			this.wizardContainer1.Controls.Add(this.wizardControlPage2);
			this.wizardContainer1.Controls.Add(this.wizardControlPage3);
			this.wizardContainer1.Controls.Add(this.wizardControlPage4);
			this.wizardContainer1.Controls.Add(this.wizardControlPage5);
			this.wizardContainer1.Controls.Add(this.wizardControlPage6);
			this.wizardContainer1.Controls.Add(this.wizardControlPage7);
			this.wizardContainer1.Location = new System.Drawing.Point(0, 0);
			this.wizardContainer1.Name = "wizardContainer1";
			this.wizardContainer1.Size = new System.Drawing.Size(450, 270);
			this.wizardContainer1.TabIndex = 1;
			// 
			// wizardControlPage1
			// 
			this.wizardControlPage1.BackColor = System.Drawing.Color.White;
			this.wizardControlPage1.BackEnabled = false;
			this.wizardControlPage1.BackVisible = false;
			this.wizardControlPage1.Border3DStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.wizardControlPage1.BorderColor = System.Drawing.Color.Black;
			this.wizardControlPage1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.wizardControlPage1.Controls.Add(this.pictureBox2);
			this.wizardControlPage1.Controls.Add(this.lblStartBanner);
			this.wizardControlPage1.Controls.Add(this.label3);
			this.wizardControlPage1.Description = "This is the description of the Wizard Page";
			this.wizardControlPage1.FullPage = true;
			this.wizardControlPage1.GradientColors = new System.Drawing.Color[] {
																					System.Drawing.Color.Empty,
																					System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)))};
			this.wizardControlPage1.HelpVisible = false;
			this.wizardControlPage1.LayoutName = "Card1";
			this.wizardControlPage1.Location = new System.Drawing.Point(0, 0);
			this.wizardControlPage1.Name = "wizardControlPage1";
			this.wizardControlPage1.NextPage = null;
			this.wizardControlPage1.PreviousPage = null;
			this.wizardControlPage1.Size = new System.Drawing.Size(450, 270);
			this.wizardControlPage1.TabIndex = 0;
			this.wizardControlPage1.Title = "Page Title";
			// 
			// pictureBox2
			// 
			this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(40, 200);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(61, 61);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 4;
			this.pictureBox2.TabStop = false;
			// 
			// lblStartBanner
			// 
			this.lblStartBanner.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Vertical, System.Drawing.Color.FromArgb(((System.Byte)(237)), ((System.Byte)(240)), ((System.Byte)(247))), System.Drawing.SystemColors.ActiveCaptionText);
			this.lblStartBanner.BorderSides = System.Windows.Forms.Border3DSide.Top;
			this.lblStartBanner.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.lblStartBanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblStartBanner.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.lblStartBanner.Location = new System.Drawing.Point(16, 8);
			this.lblStartBanner.Name = "lblStartBanner";
			this.lblStartBanner.Size = new System.Drawing.Size(112, 192);
			this.lblStartBanner.TabIndex = 2;
			this.lblStartBanner.Text = "Submit\r\nexpression\r\nto\r\nRegexLib";
			this.lblStartBanner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.White;
			this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.label3.Location = new System.Drawing.Point(144, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(288, 136);
			this.label3.TabIndex = 1;
			this.label3.Text = @"Welcome to the RegexLib.com submit wizard.
This wilzard will allow you to submit your carefully manipulated and tested regular expression into the RegexLib.com database, where it can be shared with the world to let them know of your greatness.

Be sure to verify that everything works before submitting.";
			// 
			// wizardControlPage2
			// 
			this.wizardControlPage2.BackColor = System.Drawing.Color.White;
			this.wizardControlPage2.BorderColor = System.Drawing.Color.Black;
			this.wizardControlPage2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.wizardControlPage2.Controls.Add(this.lbllabel4);
			this.wizardControlPage2.Controls.Add(this.gradientPanel3);
			this.wizardControlPage2.Controls.Add(this.lbllabel6);
			this.wizardControlPage2.Controls.Add(this.UserInfoGroupBox);
			this.wizardControlPage2.Description = "This is the description of the Wizard Page";
			this.wizardControlPage2.FullPage = false;
			this.wizardControlPage2.HelpVisible = false;
			this.wizardControlPage2.LayoutName = "Card2";
			this.wizardControlPage2.Location = new System.Drawing.Point(0, 0);
			this.wizardControlPage2.Name = "wizardControlPage2";
			this.wizardControlPage2.NextPage = null;
			this.wizardControlPage2.PreviousPage = null;
			this.wizardControlPage2.Size = new System.Drawing.Size(688, 482);
			this.wizardControlPage2.TabIndex = 1;
			this.wizardControlPage2.Title = "Page Title";
			// 
			// lbllabel4
			// 
			this.lbllabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbllabel4.Location = new System.Drawing.Point(16, 8);
			this.lbllabel4.Name = "lbllabel4";
			this.lbllabel4.Size = new System.Drawing.Size(80, 19);
			this.lbllabel4.TabIndex = 22;
			this.lbllabel4.Text = "User Info:";
			// 
			// gradientPanel3
			// 
			this.gradientPanel3.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.gradientPanel3.BorderColor = System.Drawing.Color.Black;
			this.gradientPanel3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gradientPanel3.GradientBackground = true;
			this.gradientPanel3.GradientColors = new System.Drawing.Color[] {
																				System.Drawing.Color.Empty,
																				System.Drawing.Color.CornflowerBlue};
			this.gradientPanel3.Location = new System.Drawing.Point(8, 24);
			this.gradientPanel3.Name = "gradientPanel3";
			this.gradientPanel3.Size = new System.Drawing.Size(112, 168);
			this.gradientPanel3.TabIndex = 21;
			// 
			// lbllabel6
			// 
			this.lbllabel6.BackColor = System.Drawing.SystemColors.Info;
			this.lbllabel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbllabel6.Location = new System.Drawing.Point(136, 128);
			this.lbllabel6.Name = "lbllabel6";
			this.lbllabel6.Size = new System.Drawing.Size(288, 64);
			this.lbllabel6.TabIndex = 20;
			this.lbllabel6.Text = "NOTE: If you are NOT already a registered user of RegexLib.com, you will be AUTOM" +
				"ATICALLY registered as a regexlib user and a confirmation email will be sent to " +
				"the address you provide in this form.";
			this.lbllabel6.DoubleClick += new System.EventHandler(this.lbllabel6_DoubleClick);
			// 
			// UserInfoGroupBox
			// 
			this.UserInfoGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.UserInfoGroupBox.BackColor = System.Drawing.Color.White;
			this.UserInfoGroupBox.Controls.Add(this.txtLastName);
			this.UserInfoGroupBox.Controls.Add(this.txtFirstName);
			this.UserInfoGroupBox.Controls.Add(this.txtEmail);
			this.UserInfoGroupBox.Controls.Add(this.surnameLabel);
			this.UserInfoGroupBox.Controls.Add(this.firstNameLabel);
			this.UserInfoGroupBox.Controls.Add(this.emailLabel);
			this.UserInfoGroupBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.UserInfoGroupBox.Location = new System.Drawing.Point(136, 8);
			this.UserInfoGroupBox.Name = "UserInfoGroupBox";
			this.UserInfoGroupBox.Size = new System.Drawing.Size(528, 120);
			this.UserInfoGroupBox.TabIndex = 19;
			this.UserInfoGroupBox.TabStop = false;
			this.UserInfoGroupBox.Text = "User Info";
			// 
			// txtLastName
			// 
			this.txtLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.txtLastName.Location = new System.Drawing.Point(88, 88);
			this.txtLastName.Name = "txtLastName";
			this.txtLastName.Size = new System.Drawing.Size(400, 21);
			this.txtLastName.TabIndex = 6;
			this.txtLastName.Text = "";
			// 
			// txtFirstName
			// 
			this.txtFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.txtFirstName.Location = new System.Drawing.Point(88, 56);
			this.txtFirstName.Name = "txtFirstName";
			this.txtFirstName.Size = new System.Drawing.Size(400, 21);
			this.txtFirstName.TabIndex = 5;
			this.txtFirstName.Text = "";
			// 
			// txtEmail
			// 
			this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.txtEmail.Location = new System.Drawing.Point(88, 25);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(400, 21);
			this.txtEmail.TabIndex = 4;
			this.txtEmail.Text = "";
			// 
			// surnameLabel
			// 
			this.surnameLabel.BackColor = System.Drawing.Color.White;
			this.surnameLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.surnameLabel.Location = new System.Drawing.Point(16, 88);
			this.surnameLabel.Name = "surnameLabel";
			this.surnameLabel.Size = new System.Drawing.Size(72, 23);
			this.surnameLabel.TabIndex = 2;
			this.surnameLabel.Text = "Surname:";
			// 
			// firstNameLabel
			// 
			this.firstNameLabel.BackColor = System.Drawing.Color.White;
			this.firstNameLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.firstNameLabel.Location = new System.Drawing.Point(16, 56);
			this.firstNameLabel.Name = "firstNameLabel";
			this.firstNameLabel.Size = new System.Drawing.Size(72, 23);
			this.firstNameLabel.TabIndex = 1;
			this.firstNameLabel.Text = "First Name:";
			// 
			// emailLabel
			// 
			this.emailLabel.BackColor = System.Drawing.Color.White;
			this.emailLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.emailLabel.Location = new System.Drawing.Point(16, 24);
			this.emailLabel.Name = "emailLabel";
			this.emailLabel.Size = new System.Drawing.Size(72, 23);
			this.emailLabel.TabIndex = 0;
			this.emailLabel.Text = "Email:";
			// 
			// wizardControlPage3
			// 
			this.wizardControlPage3.BackColor = System.Drawing.Color.White;
			this.wizardControlPage3.BorderColor = System.Drawing.Color.Black;
			this.wizardControlPage3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.wizardControlPage3.Controls.Add(this.lbllabel3);
			this.wizardControlPage3.Controls.Add(this.gradientPanel5);
			this.wizardControlPage3.Controls.Add(this.lbllabel1);
			this.wizardControlPage3.Controls.Add(this.txtRegex);
			this.wizardControlPage3.Description = "This is the description of the Wizard Page";
			this.wizardControlPage3.FullPage = false;
			this.wizardControlPage3.HelpVisible = false;
			this.wizardControlPage3.LayoutName = "Card3";
			this.wizardControlPage3.Location = new System.Drawing.Point(0, 0);
			this.wizardControlPage3.Name = "wizardControlPage3";
			this.wizardControlPage3.NextPage = null;
			this.wizardControlPage3.PreviousPage = null;
			this.wizardControlPage3.Size = new System.Drawing.Size(450, 200);
			this.wizardControlPage3.TabIndex = 2;
			this.wizardControlPage3.Title = "Page Title";
			// 
			// lbllabel3
			// 
			this.lbllabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbllabel3.Location = new System.Drawing.Point(16, 8);
			this.lbllabel3.Name = "lbllabel3";
			this.lbllabel3.Size = new System.Drawing.Size(80, 19);
			this.lbllabel3.TabIndex = 17;
			this.lbllabel3.Text = "Pattern:";
			// 
			// gradientPanel5
			// 
			this.gradientPanel5.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.gradientPanel5.BorderColor = System.Drawing.Color.Black;
			this.gradientPanel5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gradientPanel5.GradientBackground = true;
			this.gradientPanel5.GradientColors = new System.Drawing.Color[] {
																				System.Drawing.Color.Empty,
																				System.Drawing.Color.CornflowerBlue};
			this.gradientPanel5.Location = new System.Drawing.Point(8, 24);
			this.gradientPanel5.Name = "gradientPanel5";
			this.gradientPanel5.Size = new System.Drawing.Size(112, 168);
			this.gradientPanel5.TabIndex = 20;
			// 
			// lbllabel1
			// 
			this.lbllabel1.BackColor = System.Drawing.SystemColors.Info;
			this.lbllabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbllabel1.Location = new System.Drawing.Point(144, 136);
			this.lbllabel1.Name = "lbllabel1";
			this.lbllabel1.Size = new System.Drawing.Size(280, 56);
			this.lbllabel1.TabIndex = 1;
			this.lbllabel1.Text = "Review the regular expression pattern that you wish to submit if you need to make" +
				" any last minute modifications";
			// 
			// txtRegex
			// 
			this.txtRegex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRegex.Location = new System.Drawing.Point(144, 24);
			this.txtRegex.Multiline = true;
			this.txtRegex.Name = "txtRegex";
			this.txtRegex.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtRegex.Size = new System.Drawing.Size(280, 104);
			this.txtRegex.TabIndex = 0;
			this.txtRegex.Text = "regex";
			// 
			// wizardControlPage4
			// 
			this.wizardControlPage4.BackColor = System.Drawing.Color.White;
			this.wizardControlPage4.BorderColor = System.Drawing.Color.Black;
			this.wizardControlPage4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.wizardControlPage4.Controls.Add(this.txtSource);
			this.wizardControlPage4.Controls.Add(this.gradientPanel2);
			this.wizardControlPage4.Controls.Add(this.lbllabel2);
			this.wizardControlPage4.Controls.Add(this.txtDescription);
			this.wizardControlPage4.Controls.Add(this.descriptionLabel);
			this.wizardControlPage4.Controls.Add(this.lbllabel10);
			this.wizardControlPage4.Description = "This is the description of the Wizard Page";
			this.wizardControlPage4.FullPage = false;
			this.wizardControlPage4.HelpEnabled = false;
			this.wizardControlPage4.HelpVisible = false;
			this.wizardControlPage4.LayoutName = "Card4";
			this.wizardControlPage4.Location = new System.Drawing.Point(0, 0);
			this.wizardControlPage4.Name = "wizardControlPage4";
			this.wizardControlPage4.NextPage = null;
			this.wizardControlPage4.PreviousPage = null;
			this.wizardControlPage4.Size = new System.Drawing.Size(450, 200);
			this.wizardControlPage4.TabIndex = 3;
			this.wizardControlPage4.Title = "Page Title";
			// 
			// txtSource
			// 
			this.txtSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSource.Location = new System.Drawing.Point(184, 72);
			this.txtSource.Name = "txtSource";
			this.txtSource.Size = new System.Drawing.Size(240, 21);
			this.txtSource.TabIndex = 20;
			this.txtSource.Text = "";
			// 
			// gradientPanel2
			// 
			this.gradientPanel2.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.gradientPanel2.BorderColor = System.Drawing.Color.Black;
			this.gradientPanel2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gradientPanel2.GradientBackground = true;
			this.gradientPanel2.GradientColors = new System.Drawing.Color[] {
																				System.Drawing.Color.Empty,
																				System.Drawing.Color.CornflowerBlue};
			this.gradientPanel2.Location = new System.Drawing.Point(8, 24);
			this.gradientPanel2.Name = "gradientPanel2";
			this.gradientPanel2.Size = new System.Drawing.Size(112, 168);
			this.gradientPanel2.TabIndex = 19;
			// 
			// lbllabel2
			// 
			this.lbllabel2.BackColor = System.Drawing.SystemColors.Info;
			this.lbllabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbllabel2.Location = new System.Drawing.Point(128, 96);
			this.lbllabel2.Name = "lbllabel2";
			this.lbllabel2.Size = new System.Drawing.Size(296, 96);
			this.lbllabel2.TabIndex = 18;
			this.lbllabel2.Text = @"*Description: Enter a description that RegexLib.com users can search by. Try to include as many helpful keywords as possible. For example, if you expression test for IP address, you might want to wrote ""IP"" as part of the description.
* Source:  Give credit to wherever you got this expression from.";
			// 
			// txtDescription
			// 
			this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDescription.Location = new System.Drawing.Point(128, 8);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDescription.Size = new System.Drawing.Size(296, 56);
			this.txtDescription.TabIndex = 17;
			this.txtDescription.Text = "This pattern matches..";
			// 
			// descriptionLabel
			// 
			this.descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.descriptionLabel.Location = new System.Drawing.Point(16, 8);
			this.descriptionLabel.Name = "descriptionLabel";
			this.descriptionLabel.Size = new System.Drawing.Size(80, 23);
			this.descriptionLabel.TabIndex = 16;
			this.descriptionLabel.Text = "Description:";
			// 
			// lbllabel10
			// 
			this.lbllabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbllabel10.Location = new System.Drawing.Point(128, 72);
			this.lbllabel10.Name = "lbllabel10";
			this.lbllabel10.Size = new System.Drawing.Size(47, 15);
			this.lbllabel10.TabIndex = 21;
			this.lbllabel10.Text = "Source:";
			// 
			// wizardControlPage5
			// 
			this.wizardControlPage5.BackColor = System.Drawing.Color.White;
			this.wizardControlPage5.BorderColor = System.Drawing.Color.Black;
			this.wizardControlPage5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.wizardControlPage5.Controls.Add(this.lbllabel5);
			this.wizardControlPage5.Controls.Add(this.gradientPanel6);
			this.wizardControlPage5.Controls.Add(this.lbllabel8);
			this.wizardControlPage5.Controls.Add(this.cmdRemoveFromMatches);
			this.wizardControlPage5.Controls.Add(this.cmdAddToMatches);
			this.wizardControlPage5.Controls.Add(this.lstMatches);
			this.wizardControlPage5.Description = "This is the description of the Wizard Page";
			this.wizardControlPage5.FullPage = false;
			this.wizardControlPage5.HelpEnabled = false;
			this.wizardControlPage5.HelpVisible = false;
			this.wizardControlPage5.LayoutName = "Card5";
			this.wizardControlPage5.Location = new System.Drawing.Point(0, 0);
			this.wizardControlPage5.Name = "wizardControlPage5";
			this.wizardControlPage5.NextPage = null;
			this.wizardControlPage5.PreviousPage = null;
			this.wizardControlPage5.Size = new System.Drawing.Size(450, 200);
			this.wizardControlPage5.TabIndex = 4;
			this.wizardControlPage5.Title = "Page Title";
			// 
			// lbllabel5
			// 
			this.lbllabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbllabel5.Location = new System.Drawing.Point(8, 8);
			this.lbllabel5.Name = "lbllabel5";
			this.lbllabel5.Size = new System.Drawing.Size(192, 32);
			this.lbllabel5.TabIndex = 17;
			this.lbllabel5.Text = "List of strings that will match using this pattern:";
			// 
			// gradientPanel6
			// 
			this.gradientPanel6.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.gradientPanel6.BorderColor = System.Drawing.Color.Black;
			this.gradientPanel6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gradientPanel6.GradientBackground = true;
			this.gradientPanel6.GradientColors = new System.Drawing.Color[] {
																				System.Drawing.Color.Empty,
																				System.Drawing.Color.CornflowerBlue};
			this.gradientPanel6.Location = new System.Drawing.Point(8, 40);
			this.gradientPanel6.Name = "gradientPanel6";
			this.gradientPanel6.Size = new System.Drawing.Size(112, 152);
			this.gradientPanel6.TabIndex = 21;
			// 
			// lbllabel8
			// 
			this.lbllabel8.BackColor = System.Drawing.SystemColors.Info;
			this.lbllabel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbllabel8.Location = new System.Drawing.Point(136, 128);
			this.lbllabel8.Name = "lbllabel8";
			this.lbllabel8.Size = new System.Drawing.Size(304, 64);
			this.lbllabel8.TabIndex = 20;
			this.lbllabel8.Text = "Enter strings that, if tested against, will bring back at least one match using t" +
				"his pattern. For example, if your pattern matches email, provide an email addres" +
				"s that will be recognized by it.";
			// 
			// cmdRemoveFromMatches
			// 
			this.cmdRemoveFromMatches.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.cmdRemoveFromMatches.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cmdRemoveFromMatches.Location = new System.Drawing.Point(360, 16);
			this.cmdRemoveFromMatches.Name = "cmdRemoveFromMatches";
			this.cmdRemoveFromMatches.Size = new System.Drawing.Size(80, 24);
			this.cmdRemoveFromMatches.TabIndex = 19;
			this.cmdRemoveFromMatches.Text = "&Remove";
			this.cmdRemoveFromMatches.Click += new System.EventHandler(this.cmdRemoveFromMatches_Click);
			// 
			// cmdAddToMatches
			// 
			this.cmdAddToMatches.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.cmdAddToMatches.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cmdAddToMatches.Location = new System.Drawing.Point(272, 16);
			this.cmdAddToMatches.Name = "cmdAddToMatches";
			this.cmdAddToMatches.Size = new System.Drawing.Size(80, 24);
			this.cmdAddToMatches.TabIndex = 18;
			this.cmdAddToMatches.Text = "&Add";
			this.cmdAddToMatches.Click += new System.EventHandler(this.cmdAddToMatches_Click);
			// 
			// lstMatches
			// 
			// 
			// lstMatches.Button
			// 
			this.lstMatches.Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.lstMatches.Button.Location = new System.Drawing.Point(112, 120);
			this.lstMatches.Button.Name = "button";
			this.lstMatches.Button.Size = new System.Drawing.Size(30, 20);
			this.lstMatches.Button.TabIndex = 2;
			this.lstMatches.Button.Text = "...";
			this.lstMatches.Button.Visible = false;
			this.lstMatches.Controls.Add(this.lstMatches.Button);
			this.lstMatches.Controls.Add(this.lstMatches.TextBox);
			this.lstMatches.Controls.Add(this.lstMatches.ListBox);
			// 
			// lstMatches.ListBox
			// 
			this.lstMatches.ListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstMatches.ListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstMatches.ListBox.Location = new System.Drawing.Point(0, 0);
			this.lstMatches.ListBox.Name = "listBox";
			this.lstMatches.ListBox.Size = new System.Drawing.Size(304, 80);
			this.lstMatches.ListBox.TabIndex = 0;
			this.lstMatches.Location = new System.Drawing.Point(136, 40);
			this.lstMatches.Name = "lstMatches";
			this.lstMatches.Size = new System.Drawing.Size(304, 80);
			this.lstMatches.TabIndex = 0;
			// 
			// lstMatches.TextBox
			// 
			this.lstMatches.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstMatches.TextBox.Location = new System.Drawing.Point(8, 120);
			this.lstMatches.TextBox.Name = "textBox";
			this.lstMatches.TextBox.TabIndex = 2;
			this.lstMatches.TextBox.Visible = false;
			// 
			// wizardControlPage6
			// 
			this.wizardControlPage6.BackColor = System.Drawing.Color.White;
			this.wizardControlPage6.BorderColor = System.Drawing.Color.Black;
			this.wizardControlPage6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.wizardControlPage6.Controls.Add(this.label1);
			this.wizardControlPage6.Controls.Add(this.gradientPanel7);
			this.wizardControlPage6.Controls.Add(this.lbllabel9);
			this.wizardControlPage6.Controls.Add(this.cmdRemoveFromNonMatches);
			this.wizardControlPage6.Controls.Add(this.cmdAddToNonMatches);
			this.wizardControlPage6.Controls.Add(this.lstNonMatches);
			this.wizardControlPage6.Description = "This is the description of the Wizard Page";
			this.wizardControlPage6.FullPage = false;
			this.wizardControlPage6.HelpEnabled = false;
			this.wizardControlPage6.HelpVisible = false;
			this.wizardControlPage6.LayoutName = "Card6";
			this.wizardControlPage6.Location = new System.Drawing.Point(0, 0);
			this.wizardControlPage6.Name = "wizardControlPage6";
			this.wizardControlPage6.NextPage = null;
			this.wizardControlPage6.PreviousPage = null;
			this.wizardControlPage6.Size = new System.Drawing.Size(450, 200);
			this.wizardControlPage6.TabIndex = 5;
			this.wizardControlPage6.Title = "Page Title";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 24);
			this.label1.TabIndex = 21;
			this.label1.Text = "List of strings that will NOT match using this pattern:";
			// 
			// gradientPanel7
			// 
			this.gradientPanel7.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.gradientPanel7.BorderColor = System.Drawing.Color.Black;
			this.gradientPanel7.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gradientPanel7.GradientBackground = true;
			this.gradientPanel7.GradientColors = new System.Drawing.Color[] {
																				System.Drawing.Color.Empty,
																				System.Drawing.Color.CornflowerBlue};
			this.gradientPanel7.Location = new System.Drawing.Point(8, 40);
			this.gradientPanel7.Name = "gradientPanel7";
			this.gradientPanel7.Size = new System.Drawing.Size(112, 152);
			this.gradientPanel7.TabIndex = 25;
			// 
			// lbllabel9
			// 
			this.lbllabel9.BackColor = System.Drawing.SystemColors.Info;
			this.lbllabel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbllabel9.Location = new System.Drawing.Point(136, 128);
			this.lbllabel9.Name = "lbllabel9";
			this.lbllabel9.Size = new System.Drawing.Size(304, 64);
			this.lbllabel9.TabIndex = 24;
			this.lbllabel9.Text = "Enter any special cases or known limitations for this pattern.\r\nFor example, if y" +
				"our pattern matches email, provide an email address that will NOT be recognized " +
				"by it.";
			// 
			// cmdRemoveFromNonMatches
			// 
			this.cmdRemoveFromNonMatches.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.cmdRemoveFromNonMatches.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cmdRemoveFromNonMatches.Location = new System.Drawing.Point(360, 16);
			this.cmdRemoveFromNonMatches.Name = "cmdRemoveFromNonMatches";
			this.cmdRemoveFromNonMatches.Size = new System.Drawing.Size(80, 24);
			this.cmdRemoveFromNonMatches.TabIndex = 23;
			this.cmdRemoveFromNonMatches.Text = "&Remove";
			this.cmdRemoveFromNonMatches.Click += new System.EventHandler(this.cmdRemoveFromNonMatches_Click);
			// 
			// cmdAddToNonMatches
			// 
			this.cmdAddToNonMatches.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.cmdAddToNonMatches.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cmdAddToNonMatches.Location = new System.Drawing.Point(272, 16);
			this.cmdAddToNonMatches.Name = "cmdAddToNonMatches";
			this.cmdAddToNonMatches.Size = new System.Drawing.Size(80, 24);
			this.cmdAddToNonMatches.TabIndex = 22;
			this.cmdAddToNonMatches.Text = "&Add";
			this.cmdAddToNonMatches.Click += new System.EventHandler(this.cmdAddToNonMatches_Click);
			// 
			// lstNonMatches
			// 
			// 
			// lstNonMatches.Button
			// 
			this.lstNonMatches.Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.lstNonMatches.Button.Location = new System.Drawing.Point(112, 120);
			this.lstNonMatches.Button.Name = "button";
			this.lstNonMatches.Button.Size = new System.Drawing.Size(30, 20);
			this.lstNonMatches.Button.TabIndex = 2;
			this.lstNonMatches.Button.Text = "...";
			this.lstNonMatches.Button.Visible = false;
			this.lstNonMatches.Controls.Add(this.lstNonMatches.Button);
			this.lstNonMatches.Controls.Add(this.lstNonMatches.TextBox);
			this.lstNonMatches.Controls.Add(this.lstNonMatches.ListBox);
			// 
			// lstNonMatches.ListBox
			// 
			this.lstNonMatches.ListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstNonMatches.ListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstNonMatches.ListBox.Location = new System.Drawing.Point(0, 0);
			this.lstNonMatches.ListBox.Name = "listBox";
			this.lstNonMatches.ListBox.Size = new System.Drawing.Size(304, 80);
			this.lstNonMatches.ListBox.TabIndex = 0;
			this.lstNonMatches.Location = new System.Drawing.Point(136, 40);
			this.lstNonMatches.Name = "lstNonMatches";
			this.lstNonMatches.Size = new System.Drawing.Size(304, 88);
			this.lstNonMatches.TabIndex = 20;
			// 
			// lstNonMatches.TextBox
			// 
			this.lstNonMatches.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstNonMatches.TextBox.Location = new System.Drawing.Point(8, 120);
			this.lstNonMatches.TextBox.Name = "textBox";
			this.lstNonMatches.TextBox.TabIndex = 2;
			this.lstNonMatches.TextBox.Visible = false;
			// 
			// wizardControlPage7
			// 
			this.wizardControlPage7.BackColor = System.Drawing.Color.White;
			this.wizardControlPage7.BorderColor = System.Drawing.Color.Black;
			this.wizardControlPage7.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.wizardControlPage7.Controls.Add(this.lbllabel7);
			this.wizardControlPage7.Controls.Add(this.lnkSubmit);
			this.wizardControlPage7.Controls.Add(this.lblPleaseWait);
			this.wizardControlPage7.Controls.Add(this.lblSubmitStatus);
			this.wizardControlPage7.Controls.Add(this.gradientPanel4);
			this.wizardControlPage7.Description = "This is the description of the Wizard Page";
			this.wizardControlPage7.FullPage = false;
			this.wizardControlPage7.HelpEnabled = false;
			this.wizardControlPage7.HelpVisible = false;
			this.wizardControlPage7.LayoutName = "Card7";
			this.wizardControlPage7.Location = new System.Drawing.Point(0, 0);
			this.wizardControlPage7.Name = "wizardControlPage7";
			this.wizardControlPage7.NextPage = null;
			this.wizardControlPage7.NextVisible = false;
			this.wizardControlPage7.PreviousPage = null;
			this.wizardControlPage7.Size = new System.Drawing.Size(450, 200);
			this.wizardControlPage7.TabIndex = 6;
			this.wizardControlPage7.Title = "Page Title";
			this.wizardControlPage7.Paint += new System.Windows.Forms.PaintEventHandler(this.wizardControlPage7_Paint);
			// 
			// lbllabel7
			// 
			this.lbllabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbllabel7.Location = new System.Drawing.Point(16, 8);
			this.lbllabel7.Name = "lbllabel7";
			this.lbllabel7.Size = new System.Drawing.Size(112, 16);
			this.lbllabel7.TabIndex = 22;
			this.lbllabel7.Text = "Ready to submit!";
			// 
			// lnkSubmit
			// 
			this.lnkSubmit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.lnkSubmit.Location = new System.Drawing.Point(304, 168);
			this.lnkSubmit.Name = "lnkSubmit";
			this.lnkSubmit.Size = new System.Drawing.Size(128, 24);
			this.lnkSubmit.TabIndex = 8;
			this.lnkSubmit.TabStop = true;
			this.lnkSubmit.Text = "&Submit Expression";
			this.lnkSubmit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSubmit_LinkClicked);
			// 
			// lblPleaseWait
			// 
			this.lblPleaseWait.ForeColor = System.Drawing.Color.Red;
			this.lblPleaseWait.Location = new System.Drawing.Point(160, 168);
			this.lblPleaseWait.Name = "lblPleaseWait";
			this.lblPleaseWait.Size = new System.Drawing.Size(72, 16);
			this.lblPleaseWait.TabIndex = 7;
			this.lblPleaseWait.Text = "Please wait...";
			this.lblPleaseWait.Visible = false;
			// 
			// lblSubmitStatus
			// 
			this.lblSubmitStatus.BackColor = System.Drawing.SystemColors.Info;
			this.lblSubmitStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblSubmitStatus.Location = new System.Drawing.Point(152, 16);
			this.lblSubmitStatus.Name = "lblSubmitStatus";
			this.lblSubmitStatus.Size = new System.Drawing.Size(264, 144);
			this.lblSubmitStatus.TabIndex = 6;
			this.lblSubmitStatus.Text = "The wizard has enough information to submit to RegexLib.com.\r\nPress \"Submit\" to e" +
				"xpress yourself!";
			// 
			// gradientPanel4
			// 
			this.gradientPanel4.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.gradientPanel4.BorderColor = System.Drawing.Color.Black;
			this.gradientPanel4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gradientPanel4.GradientBackground = true;
			this.gradientPanel4.GradientColors = new System.Drawing.Color[] {
																				System.Drawing.Color.Empty,
																				System.Drawing.Color.CornflowerBlue};
			this.gradientPanel4.Location = new System.Drawing.Point(8, 24);
			this.gradientPanel4.Name = "gradientPanel4";
			this.gradientPanel4.Size = new System.Drawing.Size(112, 168);
			this.gradientPanel4.TabIndex = 2;
			// 
			// SubmitWizard
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this.wizardControl1.CancelButton;
			this.ClientSize = new System.Drawing.Size(450, 311);
			this.Controls.Add(this.wizardControl1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MenuCation = "RegexLib.com submission wizard";
			this.MinimizeBox = false;
			this.Name = "SubmitWizard";
			this.Text = "RegexLib.com submission wizard";
			((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
			this.wizardControl1.ResumeLayout(false);
			this.gradientPanel1.ResumeLayout(false);
			this.wizardContainer1.ResumeLayout(false);
			this.wizardControlPage1.ResumeLayout(false);
			this.wizardControlPage2.ResumeLayout(false);
			this.UserInfoGroupBox.ResumeLayout(false);
			this.wizardControlPage3.ResumeLayout(false);
			this.wizardControlPage4.ResumeLayout(false);
			this.wizardControlPage5.ResumeLayout(false);
			this.wizardControlPage6.ResumeLayout(false);
			this.wizardControlPage7.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void wizardControl1_Load(object sender, System.EventArgs e)
		{
		
		}

		private UserInfo m_RegexlibInfo=new UserInfo();
		public override void BeforeClose()
		{
			base.BeforeClose ();
			SaveSettings();
		}

		private void SaveSettings()
		{
			RegexLibSettings settings= 
				new RegexLibSettings(
						GetExpressionInfoFromForm().UserInfo);
			settings.Save();
		}
		private void LoadSettings()
		{
			txtEmail.Text="";
			txtFirstName.Text="";
			txtLastName.Text="";
			m_RegexlibInfo = new UserInfo();
			RegexLibSettings settings= RegexLibSettings.Load();
			if(settings!=null)
			{
				m_RegexlibInfo= settings.userInfo;
			}

		}
		public override void OnDialogClick()
		{
			
			ShowWizard(AppContext.Instance.ActiveProject);
			
			
		}





		private void cmdAddToMatches_Click(object sender, System.EventArgs e)
		{
			AddItemToEditableList(lstMatches);
		}

		private void AddItemToEditableList(Syncfusion.Windows.Forms.Tools.EditableList list)
		{
			string text = 
				Microsoft.VisualBasic.Interaction.InputBox("Enter a string","Enter string","",list.Location.X,list.Location.Y);
			if(text.Length==0)
			{
				return ;
			}
			AddItemToEditableList(list,text);
		}

		private void AddItemToEditableList(Syncfusion.Windows.Forms.Tools.EditableList list,string text)
		{
			list.ListBox.Items.Add(text);
		}

		private void cmdRemoveFromMatches_Click(object sender, System.EventArgs e)
		{
			RemoveItemFromeditableList(lstMatches);
		}

		private void RemoveItemFromeditableList(Syncfusion.Windows.Forms.Tools.EditableList list)
		{
			try
			{
				list.ListBox.Items.Remove(list.ListBox.SelectedItem);
			}
			catch(Exception )
			{
			    
			}			
		}

		private void cmdAddToNonMatches_Click(object sender, System.EventArgs e)
		{
			AddItemToEditableList(lstNonMatches);
		}

		private void cmdRemoveFromNonMatches_Click(object sender, System.EventArgs e)
		{
			RemoveItemFromeditableList(lstNonMatches);
		}

		private Thread _submitThread = null;

		private void StartSubmissionProcess()
		{
			lblPleaseWait.Visible=true;
			lnkSubmit.Enabled=false;
			StartSumbitThread();

		}
		private void StartSumbitThread()
		{
			if(_submitThread!=null && _submitThread.ThreadState== ThreadState.Running)	
			{
				_submitThread.Abort();
			}
			_submitThread=new Thread(new ThreadStart(SubmitExpression));
			_submitThread.Start();
		}

		public void ShowWizard(RegexProject regexInfo)
		{
			LoadSettings();
			txtRegex.Text= regexInfo.Regex;
			AddItemToEditableList(lstMatches,regexInfo.Input);
			txtFirstName.Text= m_RegexlibInfo.FirstName;
			txtLastName.Text= m_RegexlibInfo.Surname;
			txtEmail.Text= m_RegexlibInfo.Email;

			ShowDialog();
		}

		private void SubmitExpression()
		{
			
			try
			{
				
				RegexLib.Services.PatternInfo pinfo= 
									GetExpressionInfoFromForm();

				RegexLibSubmitter submit = CreateSubmitter();
				submit.Submit(pinfo);
			}
			catch(Exception e)
			{
			    submit_Status(e.Message);
			}
		}

		private RegexLibSubmitter CreateSubmitter()
		{

			RegexLibSubmitter submit = new RegexLibSubmitter();
			submit.Status+=new RegexLibSubmitter.SubmitterStatusDelegate(submit_Status);
			submit.Finished+=new RegexLibSubmitter.FinishedDelegate(submit_Finished);
			submit.UserDetailsInfo= m_RegexlibInfo;

			return submit;
		}

		private RegexLib.Services.PatternInfo GetExpressionInfoFromForm()
		{
			RegexLib.Services.PatternInfo pinfo= new RegexLib.Services.PatternInfo();

			pinfo.RegularExpression= txtRegex.Text;
			pinfo.ProviderId=3;
			pinfo.Description= txtDescription.Text;
			pinfo.Matches= GetStringArrayFromList(lstMatches);
			pinfo.NotMatches= GetStringArrayFromList(lstNonMatches);
			pinfo.Source= txtSource.Text;
			
			m_RegexlibInfo.Email=txtEmail.Text;
			m_RegexlibInfo.FirstName= txtFirstName.Text;
			m_RegexlibInfo.Surname= txtLastName.Text;
			
			pinfo.UserInfo= m_RegexlibInfo;

			return pinfo;
		}
		private string[] GetStringArrayFromList(Syncfusion.Windows.Forms.Tools.EditableList list)
		{
			string[] arr = new string[list.ListBox.Items.Count];
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i]= (string)list.ListBox.Items[i];
			}

			return arr;
		}


		private void submit_Status(string status)
		{
			if(InvokeRequired)
			{
				Invoke(new RegexLibSubmitter.SubmitterStatusDelegate(submit_Status),new object[]{status});
				return ;
			}
			lblSubmitStatus.Text= status;
		}

		private void submit_Finished(RegexResult result)
		{
			if(InvokeRequired)
			{
				Invoke(new RegexLibSubmitter.FinishedDelegate(submit_Finished),new object[]{result});
				return ;
			}
			if(result!=null)
			{
				
				if(result.Message!=null &&
					result.Message.Trim()!=string.Empty)
				{
					submit_Status(result.Message);
				}
			
			}
			wizardControl1.FinishButton.Visible=true;
			lblPleaseWait.Visible=false;
			lnkSubmit.Enabled=true;
		}

		private void wizardControl1_CancelButton_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void wizardControlPage7_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void lnkSubmit_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			StartSubmissionProcess();
		}


		private void InitLabelGradientColorHack()
		{
			
			this.lblTopBanner.BackgroundColor = 
				new Syncfusion.Drawing.BrushInfo(
				Syncfusion.Drawing.GradientStyle.Horizontal,
				System.Drawing.Color.RoyalBlue, 
				System.Drawing.SystemColors.ActiveCaptionText);

			this.lblStartBanner.BackgroundColor = 
				new Syncfusion.Drawing.BrushInfo(
				Syncfusion.Drawing.GradientStyle.Vertical,
				System.Drawing.Color.RoyalBlue, 
				System.Drawing.SystemColors.ActiveCaptionText);

		}

		private void lbllabel6_Click(object sender, System.EventArgs e)
		{
		
		}

		private void lbllabel6_DoubleClick(object sender, EventArgs e)
		{
#if(DEBUG)
			RegexLib.Services.PatternInfo pinfo= GetExpressionInfoFromForm();
			RegexLibSettings s = new RegexLibSettings(pinfo.UserInfo);
			s.Save();
#endif		
		}
	}
}
