using System;
using Regulator.SDK;
using Regulator.SDK.Plugins;

using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PluginLister
{
	/// <summary>
	/// Summary description for PluginList.
	/// </summary>
	public class PluginListForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView lvPlugins;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colLocation;
		private System.Windows.Forms.Button cmdAbout;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;

		public PluginListForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			

		}

		public  DialogResult ShowPluginDialog()
		{
			
			FillPlugins();
			return  ShowDialog();
		}


		private void FillPlugins()
		{
			lvPlugins.Items.Clear();
			foreach (IPlugin plugin in AppContext.Instance.Plugins)
			{
				ListViewItem lvi = new ListViewItem(plugin.PluginName);
				lvi.SubItems.Add(plugin.GetType().Assembly.Location);
				lvi.Tag= plugin;
				lvPlugins.Items.Add(lvi);
			}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PluginListForm));
			this.lvPlugins = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colLocation = new System.Windows.Forms.ColumnHeader();
			this.cmdClose = new System.Windows.Forms.Button();
			this.cmdAbout = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lvPlugins
			// 
			this.lvPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2});
			this.lvPlugins.Location = new System.Drawing.Point(8, 8);
			this.lvPlugins.Name = "lvPlugins";
			this.lvPlugins.Size = new System.Drawing.Size(275, 221);
			this.lvPlugins.TabIndex = 0;
			this.lvPlugins.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 116;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Location";
			this.columnHeader2.Width = 150;
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 121;
			// 
			// colLocation
			// 
			this.colLocation.Text = "Location";
			this.colLocation.Width = 148;
			// 
			// cmdClose
			// 
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.Location = new System.Drawing.Point(208, 240);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(75, 24);
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "&Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// cmdAbout
			// 
			this.cmdAbout.Location = new System.Drawing.Point(11, 240);
			this.cmdAbout.Name = "cmdAbout";
			this.cmdAbout.Size = new System.Drawing.Size(75, 24);
			this.cmdAbout.TabIndex = 2;
			this.cmdAbout.Text = "&About...";
			this.cmdAbout.Click += new System.EventHandler(this.cmdAbout_Click);
			// 
			// PluginListForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdClose;
			this.ClientSize = new System.Drawing.Size(296, 273);
			this.Controls.Add(this.cmdAbout);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.lvPlugins);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PluginListForm";
			this.Text = "The Regulator - Plugin list";
			this.Load += new System.EventHandler(this.PluginListForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdClose_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void cmdAbout_Click(object sender, System.EventArgs e)
		{
			ShowCurrentPluginAbout();
		}

		private void ShowCurrentPluginAbout()
		{
			IPlugin plugin = (IPlugin)lvPlugins.SelectedItems[0].Tag;
			plugin.ShowAbout();
		}

		private void PluginListForm_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
