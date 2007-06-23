using System.Windows.Forms;
using Osherove.Controls;
using Regulazy.UI.Controls;

namespace Regulazy.UI
{
    partial class RegulazyResults
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegulazyResults));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("No Matches");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("No Matches");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("No Matches");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("No Matches");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("No Matches");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("No Matches");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("No Matches");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("No Matches");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("No Matches");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("No Matches");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("No Matches");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("No Matches");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("No Matches");
            this.tabsHolder = new System.Windows.Forms.TabControl();
            this.tabRawRegex = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.txtExpression = new System.Windows.Forms.RichTextBox();
            this.tabMatchTree = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtInputSample = new Osherove.Controls.CustomDrawRTB();
            this.matchesTree = new Regulazy.UI.Controls.MatchesTreeView();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tllLblResultsCount = new System.Windows.Forms.ToolStripLabel();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tabDebug = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.scopeTree = new Regulazy.UI.Controls.ScopeTreeView();
            this.lstRects = new System.Windows.Forms.ListBox();
            this.imglstTree = new System.Windows.Forms.ImageList(this.components);
            this.optionsToolStrip = new Regulazy.UI.MatchOptionsToolStrip();
            this.lblInProgress = new System.Windows.Forms.ToolStripLabel();
            this.tabsHolder.SuspendLayout();
            this.tabRawRegex.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabMatchTree.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabDebug.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabsHolder
            // 
            this.tabsHolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabsHolder.Controls.Add(this.tabRawRegex);
            this.tabsHolder.Controls.Add(this.tabMatchTree);
            this.tabsHolder.Controls.Add(this.tabDebug);
            this.tabsHolder.Location = new System.Drawing.Point(0, 0);
            this.tabsHolder.Name = "tabsHolder";
            this.tabsHolder.SelectedIndex = 0;
            this.tabsHolder.Size = new System.Drawing.Size(459, 286);
            this.tabsHolder.TabIndex = 1;
            // 
            // tabRawRegex
            // 
            this.tabRawRegex.Controls.Add(this.toolStrip1);
            this.tabRawRegex.Controls.Add(this.txtExpression);
            this.tabRawRegex.Location = new System.Drawing.Point(4, 22);
            this.tabRawRegex.Name = "tabRawRegex";
            this.tabRawRegex.Padding = new System.Windows.Forms.Padding(3);
            this.tabRawRegex.Size = new System.Drawing.Size(451, 260);
            this.tabRawRegex.TabIndex = 0;
            this.tabRawRegex.Text = "Resulting Expression";
            this.tabRawRegex.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripButton,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.lblInProgress});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(445, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(52, 22);
            this.copyToolStripButton.Text = "&Copy";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(51, 22);
            this.toolStripButton2.Text = "&Huh?";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // txtExpression
            // 
            this.txtExpression.AcceptsTab = true;
            this.txtExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpression.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExpression.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtExpression.Location = new System.Drawing.Point(3, 31);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(453, 223);
            this.txtExpression.TabIndex = 1;
            this.txtExpression.Text = "No Regex Expression Generated Yet...";
            // 
            // tabMatchTree
            // 
            this.tabMatchTree.Controls.Add(this.splitContainer1);
            this.tabMatchTree.Controls.Add(this.toolStrip2);
            this.tabMatchTree.Location = new System.Drawing.Point(4, 22);
            this.tabMatchTree.Name = "tabMatchTree";
            this.tabMatchTree.Padding = new System.Windows.Forms.Padding(3);
            this.tabMatchTree.Size = new System.Drawing.Size(451, 260);
            this.tabMatchTree.TabIndex = 1;
            this.tabMatchTree.Text = "Test Against Input";
            this.tabMatchTree.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtInputSample);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.matchesTree);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip3);
            this.splitContainer1.Size = new System.Drawing.Size(445, 254);
            this.splitContainer1.SplitterDistance = 65;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 4;
            // 
            // txtInputSample
            // 
            this.txtInputSample.AcceptsTab = true;
            this.txtInputSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInputSample.CacheSpecialMarks = true;
            this.txtInputSample.DetectUrls = false;
            this.txtInputSample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInputSample.DoPaint = true;
            this.txtInputSample.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtInputSample.Location = new System.Drawing.Point(0, 0);
            this.txtInputSample.Name = "txtInputSample";
            this.txtInputSample.ShowNewLines = false;
            this.txtInputSample.ShowSPaces = false;
            this.txtInputSample.ShowTabs = false;
            this.txtInputSample.Size = new System.Drawing.Size(445, 65);
            this.txtInputSample.SpaceColor = System.Drawing.Color.Blue;
            this.txtInputSample.TabIndex = 2;
            this.txtInputSample.Text = "Insert Sample Input you would like to test your new expression against";
            // 
            // matchesTree
            // 
            this.matchesTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.matchesTree.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.matchesTree.ImageIndex = 0;
            this.matchesTree.Location = new System.Drawing.Point(3, 28);
            this.matchesTree.Name = "matchesTree";
            treeNode1.ImageKey = "EmptyMatch";
            treeNode1.Name = "";
            treeNode1.SelectedImageKey = "EmptyMatch";
            treeNode1.Text = "No Matches";
            treeNode2.ImageKey = "EmptyMatch";
            treeNode2.Name = "";
            treeNode2.SelectedImageKey = "EmptyMatch";
            treeNode2.Text = "No Matches";
            treeNode3.ImageKey = "EmptyMatch";
            treeNode3.Name = "";
            treeNode3.SelectedImageKey = "EmptyMatch";
            treeNode3.Text = "No Matches";
            treeNode4.ImageKey = "EmptyMatch";
            treeNode4.Name = "";
            treeNode4.SelectedImageKey = "EmptyMatch";
            treeNode4.Text = "No Matches";
            treeNode5.ImageKey = "EmptyMatch";
            treeNode5.Name = "";
            treeNode5.SelectedImageKey = "EmptyMatch";
            treeNode5.Text = "No Matches";
            treeNode6.ImageKey = "EmptyMatch";
            treeNode6.Name = "";
            treeNode6.SelectedImageKey = "EmptyMatch";
            treeNode6.Text = "No Matches";
            treeNode7.ImageKey = "EmptyMatch";
            treeNode7.Name = "";
            treeNode7.SelectedImageKey = "EmptyMatch";
            treeNode7.Text = "No Matches";
            treeNode8.ImageKey = "EmptyMatch";
            treeNode8.Name = "";
            treeNode8.SelectedImageKey = "EmptyMatch";
            treeNode8.Text = "No Matches";
            treeNode9.ImageKey = "EmptyMatch";
            treeNode9.Name = "";
            treeNode9.SelectedImageKey = "EmptyMatch";
            treeNode9.Text = "No Matches";
            treeNode10.ImageKey = "EmptyMatch";
            treeNode10.Name = "";
            treeNode10.SelectedImageKey = "EmptyMatch";
            treeNode10.Text = "No Matches";
            treeNode11.ImageKey = "EmptyMatch";
            treeNode11.Name = "";
            treeNode11.SelectedImageKey = "EmptyMatch";
            treeNode11.Text = "No Matches";
            treeNode12.ImageKey = "EmptyMatch";
            treeNode12.Name = "";
            treeNode12.SelectedImageKey = "EmptyMatch";
            treeNode12.Text = "No Matches";
            treeNode13.ImageKey = "EmptyMatch";
            treeNode13.Name = "";
            treeNode13.SelectedImageKey = "EmptyMatch";
            treeNode13.Text = "No Matches";
            this.matchesTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13});
            this.matchesTree.SelectedImageIndex = 0;
            this.matchesTree.Size = new System.Drawing.Size(439, 150);
            this.matchesTree.TabIndex = 6;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tllLblResultsCount,
            this.helpToolStripButton});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(445, 25);
            this.toolStrip3.TabIndex = 4;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(84, 22);
            this.toolStripLabel2.Text = "Found Matches:";
            // 
            // tllLblResultsCount
            // 
            this.tllLblResultsCount.Name = "tllLblResultsCount";
            this.tllLblResultsCount.Size = new System.Drawing.Size(13, 22);
            this.tllLblResultsCount.Text = "0";
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(51, 22);
            this.helpToolStripButton.Text = "&Huh?";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.newToolStripButton,
            this.openToolStripButton,
            this.toolStripSeparator,
            this.copyToolStripButton1,
            this.toolStripSeparator2,
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(445, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            this.toolStrip2.Visible = false;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(99, 22);
            this.toolStripLabel1.Text = "Sample Text Input:";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // copyToolStripButton1
            // 
            this.copyToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton1.Image")));
            this.copyToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton1.Name = "copyToolStripButton1";
            this.copyToolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton1.Text = "&Copy";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(51, 22);
            this.toolStripButton1.Text = "&Huh?";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tabDebug
            // 
            this.tabDebug.Controls.Add(this.splitContainer2);
            this.tabDebug.Location = new System.Drawing.Point(4, 22);
            this.tabDebug.Name = "tabDebug";
            this.tabDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabDebug.Size = new System.Drawing.Size(451, 260);
            this.tabDebug.TabIndex = 4;
            this.tabDebug.Text = "Debug";
            this.tabDebug.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.scopeTree);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lstRects);
            this.splitContainer2.Size = new System.Drawing.Size(445, 254);
            this.splitContainer2.SplitterDistance = 269;
            this.splitContainer2.TabIndex = 0;
            // 
            // scopeTree
            // 
            this.scopeTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scopeTree.Location = new System.Drawing.Point(0, 0);
            this.scopeTree.Name = "scopeTree";
            this.scopeTree.Size = new System.Drawing.Size(269, 254);
            this.scopeTree.TabIndex = 1;
            // 
            // lstRects
            // 
            this.lstRects.BackColor = System.Drawing.SystemColors.Info;
            this.lstRects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRects.FormattingEnabled = true;
            this.lstRects.Location = new System.Drawing.Point(0, 0);
            this.lstRects.Name = "lstRects";
            this.lstRects.Size = new System.Drawing.Size(172, 251);
            this.lstRects.TabIndex = 1;
            // 
            // imglstTree
            // 
            this.imglstTree.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imglstTree.ImageSize = new System.Drawing.Size(16, 16);
            this.imglstTree.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // optionsToolStrip
            // 
            this.optionsToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.optionsToolStrip.Location = new System.Drawing.Point(0, 288);
            this.optionsToolStrip.Name = "optionsToolStrip";
            this.optionsToolStrip.RegXOptions = System.Text.RegularExpressions.RegexOptions.None;
            this.optionsToolStrip.Size = new System.Drawing.Size(459, 25);
            this.optionsToolStrip.TabIndex = 4;
            this.optionsToolStrip.Load += new System.EventHandler(this.optionsToolStrip_Load);
            this.optionsToolStrip.RegexOptionsChanged += new System.EventHandler<Regulazy.UI.MatchOptionsToolStrip.RegexOptionsEventArgs>(this.matchOptionsToolStrip1_RegexOptionsChanged);
            // 
            // lblInProgress
            // 
            this.lblInProgress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInProgress.ForeColor = System.Drawing.Color.Red;
            this.lblInProgress.Name = "lblInProgress";
            this.lblInProgress.Size = new System.Drawing.Size(73, 22);
            this.lblInProgress.Text = "Working...";
            this.lblInProgress.Visible = false;
            // 
            // RegulazyResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.optionsToolStrip);
            this.Controls.Add(this.tabsHolder);
            this.Name = "RegulazyResults";
            this.Size = new System.Drawing.Size(459, 313);
            this.tabsHolder.ResumeLayout(false);
            this.tabRawRegex.ResumeLayout(false);
            this.tabRawRegex.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabMatchTree.ResumeLayout(false);
            this.tabMatchTree.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabDebug.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabsHolder;
        private System.Windows.Forms.TabPage tabRawRegex;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private RichTextBox txtExpression;
        private System.Windows.Forms.TabPage tabMatchTree;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton copyToolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private CustomDrawRTB txtInputSample;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel tllLblResultsCount;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ImageList imglstTree;
        private MatchesTreeView matchesTree;
        private MatchOptionsToolStrip optionsToolStrip;
        private System.Windows.Forms.TabPage tabDebug;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ScopeTreeView scopeTree;
        private System.Windows.Forms.ListBox lstRects;
        private ToolStripLabel lblInProgress;
    }
}
