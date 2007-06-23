namespace Regulazy.UI
{
    partial class ActionsPalletteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionsPalletteForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolCmdOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.thisIsATestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolCmdOptions,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(287, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolCmdOptions
            // 
            this.toolCmdOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolCmdOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thisIsATestToolStripMenuItem});
            this.toolCmdOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolCmdOptions.Image")));
            this.toolCmdOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCmdOptions.Name = "toolCmdOptions";
            this.toolCmdOptions.Size = new System.Drawing.Size(29, 22);
            this.toolCmdOptions.Text = "toolStripDropDownButton1";
            // 
            // thisIsATestToolStripMenuItem
            // 
            this.thisIsATestToolStripMenuItem.Name = "thisIsATestToolStripMenuItem";
            this.thisIsATestToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.thisIsATestToolStripMenuItem.Text = "This is a test";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ActionsPalletteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(287, 125);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ActionsPalletteForm";
            this.Text = "ActionsPalletteForm";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolCmdOptions;
        private System.Windows.Forms.ToolStripMenuItem thisIsATestToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}