using Osherove.Controls;

namespace Regulazy.UI
{
    partial class CodeGenDialog
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
            this.txtOutput = new Osherove.Controls.CustomDrawRTB();
            this.cmdCSharp = new System.Windows.Forms.Button();
            this.cmdVB = new System.Windows.Forms.Button();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.AcceptsTab = true;
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.BackColor = System.Drawing.Color.White;
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutput.CacheSpecialMarks = true;
            this.txtOutput.DetectUrls = false;
            this.txtOutput.DoPaint = true;
            this.txtOutput.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtOutput.Location = new System.Drawing.Point(13, 42);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ShowNewLines = false;
            this.txtOutput.ShowSPaces = false;
            this.txtOutput.ShowTabs = false;
            this.txtOutput.Size = new System.Drawing.Size(446, 300);
            this.txtOutput.SpaceColor = System.Drawing.Color.Blue;
            this.txtOutput.TabIndex = 2;
            this.txtOutput.Text = "string regex=@\"\\w+(?<Group>.+)";
            this.txtOutput.WordWrap = false;
            // 
            // cmdCSharp
            // 
            this.cmdCSharp.Location = new System.Drawing.Point(13, 13);
            this.cmdCSharp.Name = "cmdCSharp";
            this.cmdCSharp.Size = new System.Drawing.Size(75, 23);
            this.cmdCSharp.TabIndex = 3;
            this.cmdCSharp.Text = "C#";
            this.cmdCSharp.UseVisualStyleBackColor = true;
            this.cmdCSharp.Click += new System.EventHandler(this.cmdCSharp_Click);
            // 
            // cmdVB
            // 
            this.cmdVB.Location = new System.Drawing.Point(104, 12);
            this.cmdVB.Name = "cmdVB";
            this.cmdVB.Size = new System.Drawing.Size(75, 23);
            this.cmdVB.TabIndex = 4;
            this.cmdVB.Text = "VB";
            this.cmdVB.UseVisualStyleBackColor = true;
            this.cmdVB.Click += new System.EventHandler(this.cmdVB_Click);
            // 
            // cmdCopy
            // 
            this.cmdCopy.Location = new System.Drawing.Point(312, 12);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(147, 23);
            this.cmdCopy.TabIndex = 5;
            this.cmdCopy.Text = "&Copy Code To Clipboard";
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // CodeGenDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(471, 354);
            this.Controls.Add(this.cmdCopy);
            this.Controls.Add(this.cmdVB);
            this.Controls.Add(this.cmdCSharp);
            this.Controls.Add(this.txtOutput);
            this.MinimizeBox = false;
            this.Name = "CodeGenDialog";
            this.Text = "Code Sample";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomDrawRTB txtOutput;
        private System.Windows.Forms.Button cmdCSharp;
        private System.Windows.Forms.Button cmdVB;
        private System.Windows.Forms.Button cmdCopy;
    }
}