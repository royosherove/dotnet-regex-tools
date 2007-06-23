namespace Regulator._2005.Providers.DotNet.Views
{
	partial class MatchesView
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
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtInput = new Regulator2005.Framework.Controls.ExtendedTextBox();
			this.ptPattern = new Regulator2005.Framework.Controls.PatternDesigner();
			this.cmdGo = new System.Windows.Forms.Button();
			this.pnlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.label6);
			this.pnlMain.Controls.Add(this.label4);
			this.pnlMain.Controls.Add(this.txtInput);
			this.pnlMain.Controls.Add(this.ptPattern);
			this.pnlMain.Controls.Add(this.cmdGo);
			this.pnlMain.Size = new System.Drawing.Size(505, 396);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(0, 32);
			this.textBox1.Size = new System.Drawing.Size(505, 47);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(33, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(122, 14);
			this.label6.TabIndex = 24;
			this.label6.Text = "Given this input as text:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(28, 90);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(230, 14);
			this.label4.TabIndex = 23;
			this.label4.Text = "I would like to find (use a regex pattern here):";
			// 
			// txtInput
			// 
			this.txtInput.Location = new System.Drawing.Point(28, 38);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(479, 46);
			this.txtInput.TabIndex = 22;
			// 
			// ptPattern
			// 
			this.ptPattern.Location = new System.Drawing.Point(28, 110);
			this.ptPattern.Name = "ptPattern";
			this.ptPattern.PatternText = "";
			this.ptPattern.Size = new System.Drawing.Size(457, 88);
			this.ptPattern.TabIndex = 21;
			// 
			// cmdGo
			// 
			this.cmdGo.Location = new System.Drawing.Point(415, 204);
			this.cmdGo.Name = "cmdGo";
			this.cmdGo.Size = new System.Drawing.Size(72, 24);
			this.cmdGo.TabIndex = 20;
			this.cmdGo.Text = "&Go";
			// 
			// MatchesView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(505, 479);
			this.Name = "MatchesView";
			this.Text = "Matches View";
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label4;
		private Regulator2005.Framework.Controls.ExtendedTextBox txtInput;
		private Regulator2005.Framework.Controls.PatternDesigner ptPattern;
		private System.Windows.Forms.Button cmdGo;
	}
}