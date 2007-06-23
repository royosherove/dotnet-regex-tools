using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Regulator.SDK;
using Regulator.SDK.Plugins;
using Regulator.SDK.ApplicationSettings;
namespace Regulator.GUI.Controls
{
	/// <summary>
	/// Summary description for SnippetsControl.
	/// </summary>
	public class SnippetsControl : GenericDockedPlugin	
	{
		private C1.Win.C1FlexGrid.C1FlexGrid grdToolbox;
		private System.Windows.Forms.ContextMenu SnippetContextMenu;
		private System.Windows.Forms.MenuItem mniNsertSnippet;
		private System.Windows.Forms.MenuItem mnuEditSnippet;
		private System.Windows.Forms.MenuItem menuItem2;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public override Icon Icon
		{
			get
			{
				return new SnippetPlugin.Form1().Icon;
			}
		}

		public SnippetsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.Shortcut= Shortcut.CtrlShiftS;
			IPlugin plug = (IPlugin)this;
			this.MenuCation="Snippets";
			this.PreferredDockState= PluginDockPositions.Right;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.grdToolbox = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.SnippetContextMenu = new System.Windows.Forms.ContextMenu();
			this.mniNsertSnippet = new System.Windows.Forms.MenuItem();
			this.mnuEditSnippet = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.grdToolbox)).BeginInit();
			this.SuspendLayout();
			// 
			// grdToolbox
			// 
			this.grdToolbox.AllowAddNew = true;
			this.grdToolbox.AllowDelete = true;
			this.grdToolbox.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Both;
			this.grdToolbox.AllowEditing = false;
			this.grdToolbox.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
			this.grdToolbox.BackColor = System.Drawing.SystemColors.Window;
			this.grdToolbox.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			this.grdToolbox.ColumnInfo = "1,0,0,0,0,85,Columns:";
			this.grdToolbox.ContextMenu = this.SnippetContextMenu;
			this.grdToolbox.ExtendLastCol = true;
			this.grdToolbox.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
			this.grdToolbox.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
			this.grdToolbox.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
			this.grdToolbox.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
			this.grdToolbox.Location = new System.Drawing.Point(32, 16);
			this.grdToolbox.Name = "grdToolbox";
			this.grdToolbox.Rows.Count = 51;
			this.grdToolbox.Rows.Fixed = 0;
			this.grdToolbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.grdToolbox.ScrollTips = true;
			this.grdToolbox.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
			this.grdToolbox.Size = new System.Drawing.Size(128, 160);
			this.grdToolbox.Styles = ((C1.Win.C1FlexGrid.CellStyleCollection)(new C1.Win.C1FlexGrid.CellStyleCollection("")));
			this.grdToolbox.TabIndex = 1;
			this.grdToolbox.Click += new System.EventHandler(this.grdToolbox_Click);
			this.grdToolbox.DoubleClick += new System.EventHandler(this.grdToolbox_DoubleClick);
			// 
			// SnippetContextMenu
			// 
			this.SnippetContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							   this.mniNsertSnippet,
																							   this.mnuEditSnippet,
																							   this.menuItem2});
			// 
			// mniNsertSnippet
			// 
			this.mniNsertSnippet.DefaultItem = true;
			this.mniNsertSnippet.Index = 0;
			this.mniNsertSnippet.Text = "&Insert into Regex";
			this.mniNsertSnippet.Click += new System.EventHandler(this.mniNsertSnippet_Click);
			// 
			// mnuEditSnippet
			// 
			this.mnuEditSnippet.Index = 1;
			this.mnuEditSnippet.Text = "&Edit snippet";
			this.mnuEditSnippet.Click += new System.EventHandler(this.mnuEditSnippet_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.Text = "-";
			// 
			// SnippetsControl
			// 
			this.Controls.Add(this.grdToolbox);
			this.Name = "SnippetsControl";
			this.Size = new System.Drawing.Size(216, 248);
			this.Load += new System.EventHandler(this.SnippetsControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.grdToolbox)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion





		private void SaveSnippets()
		{
			SnippetManager.SaveSnippets(((DataTable) grdToolbox.DataSource).DataSet);

		}

		private void LoadSnippets()
		{
			DataSet ds = SnippetManager.GetSnippets();
			grdToolbox.DataSource= ds.Tables[0];
			ds.Tables[0].RowDeleted+=new DataRowChangeEventHandler(ToolboxForm_RowDeleted);
			ds.Tables[0].RowChanged+=new DataRowChangeEventHandler(ToolboxForm_RowChanged);
		}

		private void EditSnippet()
		{
			try
			{
				grdToolbox.StartEditing();
			}
			catch(Exception )
			{
			    
			}	
		}

		private void InsertCustomSnippet(string text)
		{
			
			AppContext.Instance.Display.InsertTextIntoCurrentRegex(text);
		}

		private void InsertCurrentSnippet()
		{
			try
			{
				string toInsert =grdToolbox.Selection.DataDisplay;
				if(toInsert.Trim().Length>0)
				{
					AppContext.Instance.Display.InsertTextIntoCurrentRegex(toInsert);
				}
				else
				{
					EditSnippet();
				}
				
			}
			catch(Exception )
			{
			    
			}
		}


		private void mniNsertSnippet_Click(object sender, System.EventArgs e)
		{
			InsertCurrentSnippet();
		}

		private void grdToolbox_DoubleClick(object sender, System.EventArgs e)
		{
			InsertCurrentSnippet();
		}

		private void grdToolbox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode) 
			{
				case  Keys.Enter:
					InsertCurrentSnippet();
					break;

					//				case Keys.Return :
					//					InsertCurrentSnippet();
					//					break;

				case Keys.F2:
					EditSnippet();
					break;
				default:
					;
					break;
			}
		}

		private void mnuEditSnippet_Click(object sender, System.EventArgs e)
		{
			EditSnippet();
		}


		private void ToolboxForm_Resize(object sender, System.EventArgs e)
		{
			try
			{
				grdToolbox.Cols[0].Width= grdToolbox.Width;
				
			}
			catch(Exception)
			{
			    
			}
		}

		private void ToolboxForm_RowDeleted(object sender, DataRowChangeEventArgs e)
		{
			SaveSnippets();
		}

		private void ToolboxForm_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			SaveSnippets();
		}
	
		
		private void grdToolbox_Click(object sender, System.EventArgs e)
		{
		
		}

		private void SnippetsControl_Load(object sender, System.EventArgs e)
		{
			grdToolbox.Dock=DockStyle.Fill;
			if(!this.DesignMode)
			{
				
			
				SnippetContextMenu.MenuItems.Add(AppContext.Instance.Settings.QuickMenuItem.CloneMenu());
				LoadSnippets();
			}
			
		}

	

	

		
	}
}
