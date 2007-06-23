namespace Regulazy.UI
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regulazyHomepageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportBugMissingFeatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.osheroveToolsHomePageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roysBlogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolCmdEditMode = new System.Windows.Forms.ToolStripButton();
            this.toolCmdRegexMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtRegexInput = new Regulazy.UI.ScopeTextInputCtl();
            this.results = new Regulazy.UI.RegulazyResults();
            this.tipsScopeEditor = new System.Windows.Forms.ToolTip(this.components);
            this.cmdAutoMatch = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.importToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(642, 26);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 22);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Visible = false;
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(159, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Visible = false;
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(159, 6);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(159, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.webPageToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(65, 22);
            this.importToolStripMenuItem.Text = "&Import";
            // 
            // webPageToolStripMenuItem
            // 
            this.webPageToolStripMenuItem.Name = "webPageToolStripMenuItem";
            this.webPageToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.webPageToolStripMenuItem.Text = "&Web Page...";
            this.webPageToolStripMenuItem.Click += new System.EventHandler(this.webPageToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regulazyHomepageToolStripMenuItem,
            this.reportBugMissingFeatureToolStripMenuItem,
            this.toolStripMenuItem1,
            this.osheroveToolsHomePageToolStripMenuItem,
            this.roysBlogToolStripMenuItem,
            this.toolStripMenuItem2,
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripSeparator7,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // regulazyHomepageToolStripMenuItem
            // 
            this.regulazyHomepageToolStripMenuItem.Name = "regulazyHomepageToolStripMenuItem";
            this.regulazyHomepageToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.regulazyHomepageToolStripMenuItem.Text = "Regulazy Homepage";
            this.regulazyHomepageToolStripMenuItem.Click += new System.EventHandler(this.regulazyHomepageToolStripMenuItem_Click);
            // 
            // reportBugMissingFeatureToolStripMenuItem
            // 
            this.reportBugMissingFeatureToolStripMenuItem.Name = "reportBugMissingFeatureToolStripMenuItem";
            this.reportBugMissingFeatureToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.reportBugMissingFeatureToolStripMenuItem.Text = "Report Bug/Missing Feature...";
            this.reportBugMissingFeatureToolStripMenuItem.Click += new System.EventHandler(this.reportBugMissingFeatureToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(268, 6);
            // 
            // osheroveToolsHomePageToolStripMenuItem
            // 
            this.osheroveToolsHomePageToolStripMenuItem.Name = "osheroveToolsHomePageToolStripMenuItem";
            this.osheroveToolsHomePageToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.osheroveToolsHomePageToolStripMenuItem.Text = "Osherove Tools HomePage";
            this.osheroveToolsHomePageToolStripMenuItem.Click += new System.EventHandler(this.osheroveToolsHomePageToolStripMenuItem_Click);
            // 
            // roysBlogToolStripMenuItem
            // 
            this.roysBlogToolStripMenuItem.Name = "roysBlogToolStripMenuItem";
            this.roysBlogToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.roysBlogToolStripMenuItem.Text = "Roy\'s Blog";
            this.roysBlogToolStripMenuItem.Click += new System.EventHandler(this.roysBlogToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(268, 6);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check For Updates...";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(268, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.toolCmdEditMode,
            this.toolCmdRegexMode,
            this.toolStripSeparator6,
            this.toolStripButton1,
            this.cmdAutoMatch,
            this.helpToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 26);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(642, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
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
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // toolCmdEditMode
            // 
            this.toolCmdEditMode.Checked = true;
            this.toolCmdEditMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolCmdEditMode.Enabled = false;
            this.toolCmdEditMode.Image = ((System.Drawing.Image)(resources.GetObject("toolCmdEditMode.Image")));
            this.toolCmdEditMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCmdEditMode.Name = "toolCmdEditMode";
            this.toolCmdEditMode.Size = new System.Drawing.Size(87, 22);
            this.toolCmdEditMode.Text = "Text &Edit";
            this.toolCmdEditMode.Click += new System.EventHandler(this.toolCmdEditMode_Click);
            // 
            // toolCmdRegexMode
            // 
            this.toolCmdRegexMode.Image = ((System.Drawing.Image)(resources.GetObject("toolCmdRegexMode.Image")));
            this.toolCmdRegexMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCmdRegexMode.Name = "toolCmdRegexMode";
            this.toolCmdRegexMode.Size = new System.Drawing.Size(97, 22);
            this.toolCmdRegexMode.Text = "&Regex Edit";
            this.toolCmdRegexMode.Click += new System.EventHandler(this.toolCmdRegexMode_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(103, 22);
            this.toolStripButton1.Text = "&Generate...";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(61, 22);
            this.helpToolStripButton.Text = "Huh?";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 51);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtRegexInput);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.results);
            this.splitContainer1.Size = new System.Drawing.Size(642, 389);
            this.splitContainer1.SplitterDistance = 99;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 3;
            // 
            // txtRegexInput
            // 
            this.txtRegexInput.AcceptsTab = true;
            this.txtRegexInput.ActiveScopeBorderColor = System.Drawing.Color.Blue;
            this.txtRegexInput.ActiveScopeBorderWidth = 2;
            this.txtRegexInput.ActiveScopeFillColor = System.Drawing.Color.Red;
            this.txtRegexInput.AlwaysPasteNonRTF = true;
            this.txtRegexInput.BackColor = System.Drawing.Color.LightSteelBlue;
            this.txtRegexInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegexInput.CacheSpecialMarks = true;
            this.txtRegexInput.CanDrawScopes = true;
            this.txtRegexInput.DetectUrls = false;
            this.txtRegexInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRegexInput.DoPaint = true;
            this.txtRegexInput.EditModeBackColor = System.Drawing.Color.White;
            this.txtRegexInput.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtRegexInput.ImplicitScopeBorderColor = System.Drawing.Color.Blue;
            this.txtRegexInput.InputMode = Regulazy.UI.InputModes.RegexManipulation;
            this.txtRegexInput.Location = new System.Drawing.Point(0, 0);
            this.txtRegexInput.Name = "txtRegexInput";
            this.txtRegexInput.NonActiveScopeBorderColor = System.Drawing.Color.Red;
            this.txtRegexInput.NonActiveScopeBorderWidth = 1;
            this.txtRegexInput.NonActiveScopeFillColor = System.Drawing.Color.DarkGreen;
            this.txtRegexInput.ReadOnly = true;
            this.txtRegexInput.RegexModeBackColor = System.Drawing.Color.LightCyan;
            this.txtRegexInput.ShowNewLines = false;
            this.txtRegexInput.ShowSPaces = false;
            this.txtRegexInput.ShowTabs = false;
            this.txtRegexInput.Size = new System.Drawing.Size(642, 99);
            this.txtRegexInput.SpaceColor = System.Drawing.Color.Blue;
            this.txtRegexInput.TabIndex = 1;
            this.txtRegexInput.Text = "Text you\'ll use to build your expression with";
            this.txtRegexInput.WordWrap = false;
            this.txtRegexInput.TextChanged += new System.EventHandler(this.expressionInputCtl_TextChanged);
            // 
            // results
            // 
            this.results.ActiveView = Regulazy.UI.ResultsView.RawRegex;
            this.results.Dock = System.Windows.Forms.DockStyle.Fill;
            this.results.InputBackColor = System.Drawing.SystemColors.Window;
            this.results.InputFont = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.results.InputForeColor = System.Drawing.SystemColors.WindowText;
            this.results.Location = new System.Drawing.Point(0, 0);
            this.results.Name = "results";
            this.results.RegexText = "No Expression generated yet";
            this.results.RegXOptions = System.Text.RegularExpressions.RegexOptions.None;
            this.results.SampleText = "Insert Sample Input you would like to test your new expression against";
            this.results.Size = new System.Drawing.Size(642, 282);
            this.results.TabIndex = 0;
            this.results.Load += new System.EventHandler(this.results_Load);
            // 
            // tipsScopeEditor
            // 
            this.tipsScopeEditor.IsBalloon = true;
            this.tipsScopeEditor.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tipsScopeEditor.UseAnimation = false;
            this.tipsScopeEditor.UseFading = false;
            // 
            // cmdAutoMatch
            // 
            this.cmdAutoMatch.Image = ((System.Drawing.Image)(resources.GetObject("cmdAutoMatch.Image")));
            this.cmdAutoMatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdAutoMatch.Name = "cmdAutoMatch";
            this.cmdAutoMatch.Size = new System.Drawing.Size(103, 22);
            this.cmdAutoMatch.Text = "&Auto Match";
            this.cmdAutoMatch.Click += new System.EventHandler(this.cmdAutoMatch_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 440);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "ReguLazy by Roy Osherove (Beta)";
            this.Load += new System.EventHandler(this.RegulazyMainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private RegulazyResults results;
        private System.Windows.Forms.ToolStripButton toolCmdEditMode;
        private System.Windows.Forms.ToolStripButton toolCmdRegexMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private ScopeTextInputCtl txtRegexInput;
        private System.Windows.Forms.ToolStripMenuItem regulazyHomepageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportBugMissingFeatureToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem osheroveToolsHomePageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roysBlogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolTip tipsScopeEditor;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem webPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton cmdAutoMatch;


    }
}

