namespace Regulator2005.Framework.Controls
{
	partial class ExtendedTextBox
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txt = new System.Windows.Forms.TextBox();
			this.cmdOptions = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txt
			// 
			this.txt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txt.Location = new System.Drawing.Point(2, 3);
			this.txt.Multiline = true;
			this.txt.Name = "txt";
			this.txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txt.Size = new System.Drawing.Size(286, 46);
			this.txt.TabIndex = 0;
			this.txt.MouseLeave += new System.EventHandler(this.txt_MouseLeave);
			this.txt.MouseEnter += new System.EventHandler(this.txt_MouseEnter);
			// 
			// cmdOptions
			// 
			this.cmdOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
			this.cmdOptions.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cmdOptions.Location = new System.Drawing.Point(288, 3);
			this.cmdOptions.Name = "cmdOptions";
			this.cmdOptions.Size = new System.Drawing.Size(15, 18);
			this.cmdOptions.TabIndex = 1;
			this.cmdOptions.Text = "...";
			this.cmdOptions.UseVisualStyleBackColor = false;
			this.cmdOptions.MouseLeave += new System.EventHandler(this.cmdOptions_MouseLeave);
			this.cmdOptions.Click += new System.EventHandler(this.cmdOptions_Click);
			this.cmdOptions.MouseEnter += new System.EventHandler(this.cmdOptions_MouseEnter);
			// 
			// ExtendedTextBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdOptions);
			this.Controls.Add(this.txt);
			this.Name = "ExtendedTextBox";
			this.Size = new System.Drawing.Size(309, 53);
			this.MouseLeave += new System.EventHandler(this.ExtendedTextBox_MouseLeave);
			this.MouseEnter += new System.EventHandler(this.ExtendedTextBox_MouseEnter);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txt;
		private System.Windows.Forms.Button cmdOptions;
	}
}
