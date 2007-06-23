namespace Regulator._2005.Providers.DotNet.Views
{
	partial class ReplaceView
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label4 = new System.Windows.Forms.Label();
			this.txtOutput = new Regulator2005.Framework.Controls.ExtendedTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtInput = new Regulator2005.Framework.Controls.ExtendedTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.ptReplaceWith = new Regulator2005.Framework.Controls.PatternDesigner();
			this.label5 = new System.Windows.Forms.Label();
			this.ptPattern = new Regulator2005.Framework.Controls.PatternDesigner();
			this.cmdGo = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.pnlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.label6);
			this.pnlMain.Controls.Add(this.label4);
			this.pnlMain.Controls.Add(this.txtOutput);
			this.pnlMain.Controls.Add(this.label3);
			this.pnlMain.Controls.Add(this.txtInput);
			this.pnlMain.Controls.Add(this.label2);
			this.pnlMain.Controls.Add(this.ptReplaceWith);
			this.pnlMain.Controls.Add(this.label5);
			this.pnlMain.Controls.Add(this.ptPattern);
			this.pnlMain.Controls.Add(this.cmdGo);
			this.pnlMain.Size = new System.Drawing.Size(501, 409);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(0, 35);
			this.textBox1.Size = new System.Drawing.Size(501, 47);
			this.textBox1.Text = "Use this view to test how text will be replaced using the regular expression of y" +
				"our choice.\r\n";
			// 
			// lblViewName
			// 
			this.lblViewName.Size = new System.Drawing.Size(73, 14);
			this.lblViewName.Text = "Replace View";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(22, 108);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(230, 14);
			this.label4.TabIndex = 18;
			this.label4.Text = "I would like to find (use a regex pattern here):";
			// 
			// txtOutput
			// 
			this.txtOutput.Location = new System.Drawing.Point(17, 334);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.Size = new System.Drawing.Size(478, 67);
			this.txtOutput.TabIndex = 17;
			this.txtOutput.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(19, 314);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 14);
			this.label3.TabIndex = 16;
			this.label3.Text = "Output:";
			// 
			// txtInput
			// 
			this.txtInput.Location = new System.Drawing.Point(17, 33);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(479, 61);
			this.txtInput.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, -24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(107, 14);
			this.label2.TabIndex = 14;
			this.label2.Text = "Given this input text:";
			// 
			// ptReplaceWith
			// 
			this.ptReplaceWith.Location = new System.Drawing.Point(22, 245);
			this.ptReplaceWith.Name = "ptReplaceWith";
			this.ptReplaceWith.PatternText = "";
			this.ptReplaceWith.Size = new System.Drawing.Size(457, 40);
			this.ptReplaceWith.TabIndex = 13;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(20, 223);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(220, 14);
			this.label5.TabIndex = 12;
			this.label5.Text = "Whatever is found should be replaced with:";
			// 
			// ptPattern
			// 
			this.ptPattern.Location = new System.Drawing.Point(19, 126);
			this.ptPattern.Name = "ptPattern";
			this.ptPattern.PatternText = "";
			this.ptPattern.Size = new System.Drawing.Size(457, 67);
			this.ptPattern.TabIndex = 11;
			// 
			// cmdGo
			// 
			this.cmdGo.Location = new System.Drawing.Point(404, 291);
			this.cmdGo.Name = "cmdGo";
			this.cmdGo.Size = new System.Drawing.Size(72, 24);
			this.cmdGo.TabIndex = 10;
			this.cmdGo.Text = "&Go";
			this.cmdGo.Click += new System.EventHandler(this.cmdGo_Click_1);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(22, 13);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(122, 14);
			this.label6.TabIndex = 19;
			this.label6.Text = "Given this input as text:";
			// 
			// ReplaceView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(501, 495);
			this.Name = "ReplaceView";
			this.Text = "Replace View";
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label4;
		private Regulator2005.Framework.Controls.ExtendedTextBox txtOutput;
		private System.Windows.Forms.Label label3;
		private Regulator2005.Framework.Controls.ExtendedTextBox txtInput;
		private System.Windows.Forms.Label label2;
		private Regulator2005.Framework.Controls.PatternDesigner ptReplaceWith;
		private System.Windows.Forms.Label label5;
		private Regulator2005.Framework.Controls.PatternDesigner ptPattern;
		private System.Windows.Forms.Button cmdGo;
		private System.Windows.Forms.Label label6;

	}
}