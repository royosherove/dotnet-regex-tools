namespace Regulator2005.Framework.Controls
{
	partial class PatternDesigner
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
			this.txt = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// txt
			// 
			this.txt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.txt.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txt.HideSelection = false;
			this.txt.Location = new System.Drawing.Point(0, 0);
			this.txt.Name = "txt";
			this.txt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.txt.Size = new System.Drawing.Size(232, 65);
			this.txt.TabIndex = 0;
			this.txt.Text = "";
			// 
			// PatternDesigner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txt);
			this.Name = "PatternDesigner";
			this.Size = new System.Drawing.Size(232, 65);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox txt;
	}
}
