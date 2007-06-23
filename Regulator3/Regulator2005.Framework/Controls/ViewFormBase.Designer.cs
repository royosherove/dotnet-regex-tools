namespace Regulator2005.Framework.Controls
{
	partial class ViewFormBase
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lblViewName = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.pnlMain);
			// 
			// Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.lblViewName);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Panel2.Controls.Add(this.textBox1);
			this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel2_Paint);
			this.splitContainer1.Size = new System.Drawing.Size(563, 407);
			this.splitContainer1.SplitterDistance = 336;
			this.splitContainer1.TabIndex = 0;
			// 
			// lblViewName
			// 
			this.lblViewName.AutoSize = true;
			this.lblViewName.Location = new System.Drawing.Point(67, 4);
			this.lblViewName.Name = "lblViewName";
			this.lblViewName.Size = new System.Drawing.Size(35, 14);
			this.lblViewName.TabIndex = 2;
			this.lblViewName.Text = "label2";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 14);
			this.label1.TabIndex = 1;
			this.label1.Text = "Help on:";
			// 
			// textBox1
			// 
			this.textBox1.AutoSize = false;
			this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBox1.Location = new System.Drawing.Point(0, 20);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(563, 47);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "This is the view description. You need to override a member to make this work for" +
				" yourview";
			// 
			// pnlMain
			// 
			this.pnlMain.BackColor = System.Drawing.Color.White;
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 0);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(563, 336);
			this.pnlMain.TabIndex = 0;
			// 
			// ViewFormBase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(563, 407);
			this.Controls.Add(this.splitContainer1);
			this.Name = "ViewFormBase";
			this.Text = "This is a base form view";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label label1;
		protected System.Windows.Forms.Panel pnlMain;
		protected System.Windows.Forms.TextBox textBox1;
		protected System.Windows.Forms.Label lblViewName;
	}
}