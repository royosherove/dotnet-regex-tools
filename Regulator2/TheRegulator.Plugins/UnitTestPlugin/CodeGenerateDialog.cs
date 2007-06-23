using System;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Regulator.SDK.Plugins;
using Regulator.SDK;

namespace Royo.Plugins.CodeGeneratorPlugin
{
	/// <summary>
	/// Summary description for CodeGenerateDialog.
	/// </summary>
	public class CodeGenerateDialog : GenericDialogPlugin
	{
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.RadioButton optVBNet;
		private System.Windows.Forms.RadioButton optCSharp;
		private System.Windows.Forms.Label lbllabel1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CodeGenerateDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.Name = "Code Generator";
			this.MenuCation= @"Generate code...";
			this.Shortcut= Shortcut.CtrlK;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CodeGenerateDialog));
			this.txtCode = new System.Windows.Forms.TextBox();
			this.optVBNet = new System.Windows.Forms.RadioButton();
			this.optCSharp = new System.Windows.Forms.RadioButton();
			this.lbllabel1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtCode
			// 
			this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtCode.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.txtCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.txtCode.Location = new System.Drawing.Point(8, 48);
			this.txtCode.Multiline = true;
			this.txtCode.Name = "txtCode";
			this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtCode.Size = new System.Drawing.Size(544, 264);
			this.txtCode.TabIndex = 0;
			this.txtCode.Text = "Sample code goes here";
			this.txtCode.WordWrap = false;
			// 
			// optVBNet
			// 
			this.optVBNet.Appearance = System.Windows.Forms.Appearance.Button;
			this.optVBNet.Checked = true;
			this.optVBNet.Location = new System.Drawing.Point(8, 24);
			this.optVBNet.Name = "optVBNet";
			this.optVBNet.Size = new System.Drawing.Size(64, 22);
			this.optVBNet.TabIndex = 1;
			this.optVBNet.TabStop = true;
			this.optVBNet.Text = "VB.NET";
			this.optVBNet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.optVBNet.CheckedChanged += new System.EventHandler(this.optVBNet_CheckedChanged);
			// 
			// optCSharp
			// 
			this.optCSharp.Appearance = System.Windows.Forms.Appearance.Button;
			this.optCSharp.Location = new System.Drawing.Point(80, 24);
			this.optCSharp.Name = "optCSharp";
			this.optCSharp.Size = new System.Drawing.Size(64, 22);
			this.optCSharp.TabIndex = 2;
			this.optCSharp.Text = "C#";
			this.optCSharp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.optCSharp.CheckedChanged += new System.EventHandler(this.optCSharp_CheckedChanged);
			// 
			// lbllabel1
			// 
			this.lbllabel1.Location = new System.Drawing.Point(8, 8);
			this.lbllabel1.Name = "lbllabel1";
			this.lbllabel1.Size = new System.Drawing.Size(100, 16);
			this.lbllabel1.TabIndex = 3;
			this.lbllabel1.Text = "Generate code in:";
			// 
			// CodeGenerateDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(560, 317);
			this.Controls.Add(this.lbllabel1);
			this.Controls.Add(this.optCSharp);
			this.Controls.Add(this.optVBNet);
			this.Controls.Add(this.txtCode);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MenuCation = "Regex Code Generator";
			this.Name = "CodeGenerateDialog";
			this.Text = "Regex Code Generator";
			this.Load += new System.EventHandler(this.CodeGenerateDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void CodeGenerateDialog_Load(object sender, System.EventArgs e)
		{
			GenerateCode();
		}

		private void GenerateCode()
		{
			string code = MakeCode("C",_currentProject.Regex,_currentProject.Options);
			txtCode.Text= code;
			//txtCode.SelectAll();
		}

		private bool GetOptionState(RegexOptions enumeratedObject,RegexOptions wantedEnum)
		{
			return (enumeratedObject & wantedEnum)==wantedEnum;
		}


		private string MakeCode(string language,string regexText,RegexOptions options)
		{
			return CodeGenerator.Create(GetLanguageSelection(),regexText,options);

		}

		private CodeGenerator.Language GetLanguageSelection()
		{
			if(optCSharp.Checked)
			{
				return CodeGenerator.Language.CSharp;
			}
			if(optVBNet.Checked)
			{
				return CodeGenerator.Language.VisualBasic;
			}

			//defaults to VB.Net
			//what can I say, I'm a Mort!
			return CodeGenerator.Language.VisualBasic;

		}

		private void optCSharp_CheckedChanged(object sender, System.EventArgs e)
		{
			GenerateCode();
		}

		private void optVBNet_CheckedChanged(object sender, System.EventArgs e)
		{
			GenerateCode();
		}
	}
}
