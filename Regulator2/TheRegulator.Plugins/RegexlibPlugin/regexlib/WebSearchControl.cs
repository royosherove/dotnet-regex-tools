using System;
using Regulator.SDK.Plugins;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Regulator.SDK.Proxy;
using Regulator.SDK;
using Regulator.SDK.ApplicationSettings;
using Regulator.SDK.Plugins.RegexLib.Services;

namespace Regulator.SDK.Plugins.RegexLib
{
	/// <summary>
	/// Summary description for WebSearchControl.
	/// </summary>
	public class WebSearchControl : GenericDockedPlugin
	{
		private C1.Win.C1Command.C1ContextMenu c1ContextMenu1;
		private C1.Win.C1Command.C1CommandLink c1CommandLink1;
		private C1.Win.C1Command.C1Command cmdInsertCurrent;
		private C1.Win.C1Command.C1CommandLink c1CommandLink2;
		private C1.Win.C1Command.C1Command cmdInsertNew;
		private C1.Win.C1Command.C1CommandLink c1CommandLink3;
		private C1.Win.C1Command.C1Command cmdSetCurrentExpression;
		private C1.Win.C1Command.C1CommandLink c1CommandLink4;
		private C1.Win.C1Command.C1Command cmdSetCurrentInput;
		private System.Windows.Forms.MenuItem mnuInsertCurrentDocument;
		private System.Windows.Forms.MenuItem mnuSetCurrentRegex;
		private System.Windows.Forms.MenuItem mnuSetCurrentInput;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.MenuItem mnuINsertNewDocument;
		private System.Windows.Forms.Panel pnlpanel1;
		private C1.Win.C1FlexGrid.C1FlexGrid grd;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.TextBox txtInfo;
		private System.Windows.Forms.ContextMenu SnippetContextMenu;
		private System.Windows.Forms.LinkLabel llblinkLabel1;
		private System.Windows.Forms.Button cmdSearch;
		private System.Windows.Forms.Label lbllabel1;
		private System.Windows.Forms.TextBox txtStatus;
		private System.Windows.Forms.LinkLabel lnkConnectOptions;
		private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
		private System.Windows.Forms.MenuItem menuItem1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public override Icon Icon
		{
			get
			{
				return new RegexlibPlugin.Form1().Icon;
			}
		}

		public WebSearchControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.MenuCation="&Web Search";
			this.Name="Web Search";
			this.Shortcut=Shortcut.CtrlShiftF;
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
			this.c1ContextMenu1 = new C1.Win.C1Command.C1ContextMenu();
			this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
			this.cmdInsertCurrent = new C1.Win.C1Command.C1Command();
			this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
			this.cmdInsertNew = new C1.Win.C1Command.C1Command();
			this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
			this.cmdSetCurrentExpression = new C1.Win.C1Command.C1Command();
			this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
			this.cmdSetCurrentInput = new C1.Win.C1Command.C1Command();
			this.mnuInsertCurrentDocument = new System.Windows.Forms.MenuItem();
			this.mnuSetCurrentRegex = new System.Windows.Forms.MenuItem();
			this.mnuSetCurrentInput = new System.Windows.Forms.MenuItem();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.mnuINsertNewDocument = new System.Windows.Forms.MenuItem();
			this.pnlpanel1 = new System.Windows.Forms.Panel();
			this.grd = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.SnippetContextMenu = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.txtInfo = new System.Windows.Forms.TextBox();
			this.llblinkLabel1 = new System.Windows.Forms.LinkLabel();
			this.cmdSearch = new System.Windows.Forms.Button();
			this.lbllabel1 = new System.Windows.Forms.Label();
			this.txtStatus = new System.Windows.Forms.TextBox();
			this.lnkConnectOptions = new System.Windows.Forms.LinkLabel();
			this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
			this.pnlpanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
			this.SuspendLayout();
			// 
			// c1ContextMenu1
			// 
			this.c1ContextMenu1.CommandLinks.Add(this.c1CommandLink1);
			this.c1ContextMenu1.CommandLinks.Add(this.c1CommandLink2);
			this.c1ContextMenu1.CommandLinks.Add(this.c1CommandLink3);
			this.c1ContextMenu1.CommandLinks.Add(this.c1CommandLink4);
			this.c1ContextMenu1.Name = "c1ContextMenu1";
			this.c1ContextMenu1.Width = 100;
			this.c1ContextMenu1.Click += new C1.Win.C1Command.ClickEventHandler(this.c1ContextMenu1_Click);
			// 
			// c1CommandLink1
			// 
			this.c1CommandLink1.Command = this.cmdInsertCurrent;
			this.c1CommandLink1.Text = "Insert into &Current window";
			// 
			// cmdInsertCurrent
			// 
			this.cmdInsertCurrent.Name = "cmdInsertCurrent";
			this.cmdInsertCurrent.Text = "Insert into current window";
			// 
			// c1CommandLink2
			// 
			this.c1CommandLink2.Command = this.cmdInsertNew;
			// 
			// cmdInsertNew
			// 
			this.cmdInsertNew.Name = "cmdInsertNew";
			this.cmdInsertNew.Text = "Insert into &New window";
			// 
			// c1CommandLink3
			// 
			this.c1CommandLink3.Command = this.cmdSetCurrentExpression;
			// 
			// cmdSetCurrentExpression
			// 
			this.cmdSetCurrentExpression.Name = "cmdSetCurrentExpression";
			this.cmdSetCurrentExpression.Text = "Set as current &Expression";
			// 
			// c1CommandLink4
			// 
			this.c1CommandLink4.Command = this.cmdSetCurrentInput;
			// 
			// cmdSetCurrentInput
			// 
			this.cmdSetCurrentInput.Name = "cmdSetCurrentInput";
			this.cmdSetCurrentInput.Text = "Set as current &Input";
			// 
			// mnuInsertCurrentDocument
			// 
			this.mnuInsertCurrentDocument.DefaultItem = true;
			this.mnuInsertCurrentDocument.Index = 0;
			this.mnuInsertCurrentDocument.Text = "Set Regex and input in &Current document";
			this.mnuInsertCurrentDocument.Click += new System.EventHandler(this.mnuInsertCurrentDocument_Click);
			// 
			// mnuSetCurrentRegex
			// 
			this.mnuSetCurrentRegex.Index = 1;
			this.mnuSetCurrentRegex.Text = "Set &Regex in current";
			this.mnuSetCurrentRegex.Click += new System.EventHandler(this.mnuSetCurrentRegex_Click);
			// 
			// mnuSetCurrentInput
			// 
			this.mnuSetCurrentInput.Index = 2;
			this.mnuSetCurrentInput.Text = "Set &Input in current";
			this.mnuSetCurrentInput.Click += new System.EventHandler(this.mnuSetCurrentInput_Click);
			// 
			// txtSearch
			// 
			this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSearch.Location = new System.Drawing.Point(8, 24);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(272, 20);
			this.txtSearch.TabIndex = 0;
			this.txtSearch.Text = "search";
			this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
			this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			// 
			// mnuINsertNewDocument
			// 
			this.mnuINsertNewDocument.Index = 4;
			this.mnuINsertNewDocument.Text = "Import into &New document";
			this.mnuINsertNewDocument.Click += new System.EventHandler(this.mnuINsertNewDocument_Click);
			// 
			// pnlpanel1
			// 
			this.pnlpanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlpanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlpanel1.Controls.Add(this.grd);
			this.pnlpanel1.Controls.Add(this.splitter1);
			this.pnlpanel1.Controls.Add(this.txtInfo);
			this.pnlpanel1.Location = new System.Drawing.Point(8, 80);
			this.pnlpanel1.Name = "pnlpanel1";
			this.pnlpanel1.Size = new System.Drawing.Size(272, 328);
			this.pnlpanel1.TabIndex = 16;
			// 
			// grd
			// 
			this.grd.AllowEditing = false;
			this.grd.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.BothUniform;
			this.grd.AutoResize = false;
			this.grd.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
			this.grd.BackColor = System.Drawing.SystemColors.Window;
			this.grd.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			this.grd.ColumnInfo = @"3,0,0,0,0,85,Columns:0{Width:95;Name:""Description"";Caption:""Description"";DataType:System.String;TextAlign:LeftCenter;}	1{Name:""matches"";Caption:""Example"";DataType:System.String;TextAlign:LeftCenter;}	2{Name:""regular_expression"";Caption:""Expression"";DataType:System.String;TextAlign:LeftCenter;}	";
			this.grd.ContextMenu = this.SnippetContextMenu;
			this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd.ExtendLastCol = true;
			this.grd.Location = new System.Drawing.Point(0, 0);
			this.grd.Name = "grd";
			this.grd.Rows.Count = 5;
			this.grd.ScrollTips = true;
			this.grd.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd.Size = new System.Drawing.Size(270, 267);
			this.grd.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(@"Fixed{BackColor:Control;ForeColor:ControlText;Border:Flat,1,ControlDark,Both;}	Highlight{BackColor:Highlight;ForeColor:HighlightText;}	Search{BackColor:Highlight;ForeColor:HighlightText;}	Frozen{BackColor:Beige;}	EmptyArea{BackColor:AppWorkspace;Border:Flat,1,ControlDarkDark,Both;}	GrandTotal{BackColor:Black;ForeColor:White;}	Subtotal0{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal1{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal2{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal3{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal4{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal5{BackColor:ControlDarkDark;ForeColor:White;}	");
			this.grd.TabIndex = 0;
			this.grd.DoubleClick += new System.EventHandler(this.grd_DoubleClick);
			this.grd.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.grd_AfterSelChange);
			// 
			// SnippetContextMenu
			// 
			this.SnippetContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							   this.mnuInsertCurrentDocument,
																							   this.mnuSetCurrentRegex,
																							   this.mnuSetCurrentInput,
																							   this.menuItem1,
																							   this.mnuINsertNewDocument});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 3;
			this.menuItem1.Text = "-";
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 267);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(270, 3);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// txtInfo
			// 
			this.txtInfo.BackColor = System.Drawing.SystemColors.Info;
			this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtInfo.Location = new System.Drawing.Point(0, 270);
			this.txtInfo.Multiline = true;
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.ReadOnly = true;
			this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtInfo.Size = new System.Drawing.Size(270, 56);
			this.txtInfo.TabIndex = 0;
			this.txtInfo.Text = "information";
			// 
			// llblinkLabel1
			// 
			this.llblinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.llblinkLabel1.Location = new System.Drawing.Point(48, 8);
			this.llblinkLabel1.Name = "llblinkLabel1";
			this.llblinkLabel1.Size = new System.Drawing.Size(80, 16);
			this.llblinkLabel1.TabIndex = 3;
			this.llblinkLabel1.TabStop = true;
			this.llblinkLabel1.Text = "RegexLib.com";
			this.llblinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblinkLabel1_LinkClicked);
			// 
			// cmdSearch
			// 
			this.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cmdSearch.Location = new System.Drawing.Point(8, 48);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(80, 24);
			this.cmdSearch.TabIndex = 1;
			this.cmdSearch.Text = "&Search";
			this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
			// 
			// lbllabel1
			// 
			this.lbllabel1.Location = new System.Drawing.Point(8, 8);
			this.lbllabel1.Name = "lbllabel1";
			this.lbllabel1.Size = new System.Drawing.Size(40, 16);
			this.lbllabel1.TabIndex = 13;
			this.lbllabel1.Text = "Search";
			// 
			// txtStatus
			// 
			this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtStatus.BackColor = System.Drawing.Color.WhiteSmoke;
			this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtStatus.Location = new System.Drawing.Point(96, 48);
			this.txtStatus.Multiline = true;
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.ReadOnly = true;
			this.txtStatus.Size = new System.Drawing.Size(184, 24);
			this.txtStatus.TabIndex = 18;
			this.txtStatus.Text = "";
			this.txtStatus.Visible = false;
			// 
			// lnkConnectOptions
			// 
			this.lnkConnectOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lnkConnectOptions.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkConnectOptions.Location = new System.Drawing.Point(8, 416);
			this.lnkConnectOptions.Name = "lnkConnectOptions";
			this.lnkConnectOptions.Size = new System.Drawing.Size(120, 16);
			this.lnkConnectOptions.TabIndex = 2;
			this.lnkConnectOptions.TabStop = true;
			this.lnkConnectOptions.Text = "Connection &Options...";
			this.lnkConnectOptions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkConnectOptions_LinkClicked);
			// 
			// c1CommandHolder1
			// 
			this.c1CommandHolder1.Commands.Add(this.c1ContextMenu1);
			this.c1CommandHolder1.Commands.Add(this.cmdInsertCurrent);
			this.c1CommandHolder1.Commands.Add(this.cmdInsertNew);
			this.c1CommandHolder1.Commands.Add(this.cmdSetCurrentExpression);
			this.c1CommandHolder1.Commands.Add(this.cmdSetCurrentInput);
			this.c1CommandHolder1.Owner = this;
			// 
			// WebSearchControl
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.cmdSearch);
			this.Controls.Add(this.lbllabel1);
			this.Controls.Add(this.txtStatus);
			this.Controls.Add(this.lnkConnectOptions);
			this.Controls.Add(this.txtSearch);
			this.Controls.Add(this.pnlpanel1);
			this.Controls.Add(this.llblinkLabel1);
			this.Name = "WebSearchControl";
			this.Size = new System.Drawing.Size(288, 440);
			this.Load += new System.EventHandler(this.WebSearchControl_Load);
			this.pnlpanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion



		//----------------------CUSTOM CODE START

		private Thread _searchThread= null;
		

		public void RunSearch(string search)
		{
			txtSearch.Text= search;
			txtSearch.Focus();
			txtSearch.SelectAll();
			ExecuteSearch();
		}

		public void GetReadyToSearch()
		{
			txtSearch.Focus();
			txtSearch.SelectAll();
		}
		
		

		private void cmdSearch_Click(object sender, System.EventArgs e)
		{
			//StartSearchThread();
			//SearchWebAsync();
			ExecuteSearch();
		}

		private void ExecuteSearch()
		{
			try
			{
				StartSearchThread();
			}
			catch(Exception e)
			{
				ShowSearchStatus(e.Message);    
			}
			
		}

		private void StartSearchThread()
		{
			
			if(_searchThread!=null && 
				_searchThread.ThreadState==ThreadState.Running)			
			{
				_searchThread.Abort();
			}

			_searchThread = new Thread(new ThreadStart(SearchWeb));
			_searchThread.Start();
		}


		private delegate void StdDelegateString(string text);
		
		private void  ShowSearchStatus(string status)
		{
			if(InvokeRequired)
			{
				Invoke(new StdDelegateString(ShowSearchStatus),new object[]{status});
				return ;
			}

			txtStatus.Text= status;
			txtStatus.Visible=(status.Trim()!=string.Empty);
		}
		private void SearchWeb()
		{

			try
			{
				ShowSearchStatus("Querying service...");

				RegexLib.Services.Webservices searcher = CreateWebService();

				
				DataSet ds= searcher.listRegExp(txtSearch.Text,"",-1,50);

				ShowSearchStatus("Filling results...");
				BindGrid(ds);			
				ShowSearchStatus("");
			}
			catch(Exception e)
			{
				ShowSearchStatus(e.Message);
			    
			}

		}


		private Webservices CreateWebService()
		{
			Webservices searcher = new RegexLib.Services.Webservices();
				
			System.Net.WebProxy myProxy = ProxyFactory.Create(AppContext.Instance.Settings.ProxySettings,searcher.Url);
			if(myProxy!=null)
			{
				searcher.Proxy= myProxy;
			}

			return searcher;	
		}

		private void SearchWebAsync()
		{
			//Start the invokation and pass in a callback delegate 
			//to be called in finish
			ShowSearchStatus("Querying service...");

			
			RegexLib.Services.Webservices searcher = new RegexLib.Services.Webservices();
			IAsyncResult result = searcher.BeginlistRegExp(txtSearch.Text,"",-1,50,new AsyncCallback(OnSearchFinish),searcher);

		}

		private void OnSearchFinish(IAsyncResult result)
		{
			//recieve the searcher that we passed in as state
			//in SearchWebAsync
			RegexLib.Services.Webservices searcher = (RegexLib.Services.Webservices )result.AsyncState;
			//retrieve the result of the invocation
			DataSet ds= searcher.EndlistRegExp(result);
			BindGrid(ds);
			ShowSearchStatus("");
		}



		private delegate void StdDelegate();
		private delegate void StdDelegateDataset(DataSet ds);
		private void BindGrid(DataSet ds)
		{

			if(InvokeRequired)
			{
				Invoke(new StdDelegateDataset(BindGrid),new object[]{ds});
				return ;
			}
			grd.DataSource=GridFormattingHelper.CreateFormattedDataView( ds.Tables[0]);
			GridFormattingHelper.FormatGrid(grd);
		}



		

		private string GetCurrentRegex()
		{
			return GetCurrentRow()["regular_expression"].ToString();
						
		}

		private string GetCurrentInput()
		{
			string input = GetCurrentRow()["matches"].ToString();
			input = input.Replace("|||",Environment.NewLine);
			return input;
						
		}

		private string GetCurrentDescription()
		{
			return GetCurrentRow()["description"].ToString();
						
		}


		


		private DataRowView GetCurrentRow()
		{
			try
			{
				BindingManagerBase bind = BindingContext[grd.DataSource];
				return  (DataRowView) bind.Current;
							

			}
			catch(Exception )
			{
				return null;
			}
		}




		
		private void llblinkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.regexlib.com");
		}

		private void WebSearchForm_Load(object sender, System.EventArgs e)
		{
			txtSearch.Click+=new EventHandler(txtSearch_Click);
			grd.Dock=DockStyle.Fill;
		}

		private void txtSearch_Click(object sender, EventArgs e)
		{
			txtSearch.SelectAll();
		}

		private void grd_AfterSelChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
		{
			try
			{
				txtInfo.Text= GetCurrentDescription();
			}
			catch(Exception)
			{
				txtInfo.Text="";
			}
		}

		private void cmdInsertCurrent_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			InsertCurrentAllFields();
			
		}

		

		private void mnuSetCurrentRegex_Click(object sender, System.EventArgs e)
		{
			InsertCurrentRegex();
		}

		private void InsertCurrentAllFields()
		{
			try
			{
				_currentProject.Regex= GetCurrentRegex();
				_currentProject.Input= GetCurrentInput();
				
			}
			catch(Exception )
			{
			    
			}
		}
		private void InsertCurrentRegex()
		{
			try
			{
				_currentProject.Regex= GetCurrentRegex();
				//DisplayManager.Display.InsertTextIntoCurrentRegex(GetCurrentRegex(),true);
				
			}
			catch(Exception )
			{
			    
			}
		}

		private void InsertCurrentInput()
		{
			try
			{
				_currentProject.Input= GetCurrentInput();
				//DisplayManager.Display.InsertTextIntoCurrentInput(GetCurrentInput());
		
			}
			catch(Exception )
			{
		    
			}	
		}

		private void mnuInsertCurrentDocument_Click(object sender, System.EventArgs e)
		{
			InsertCurrentAllFields();
		}

		private void mnuSetCurrentInput_Click(object sender, System.EventArgs e)
		{
			InsertCurrentInput();
		}

		private void mnuINsertNewDocument_Click(object sender, System.EventArgs e)
		{
			InsertIntoNewDocumentAllFields();
		}

		private void InsertIntoNewDocumentAllFields()
		{
			try
			{
				
				AppContext.Instance.Display.CreateNewDocument(GetCurrentRegex(),GetCurrentInput(),GetCurrentDescription());		
				
			}
			catch(Exception )
			{
			    
			}
		}
		

		private void grd_DoubleClick(object sender, System.EventArgs e)
		{
			InsertCurrentAllFields();
		}

	

		private void lnkConnectOptions_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			AppContext.Instance.Dialogs.ShowConnectionOptions();
			
		}

		private void txtSearch_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtSearch_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
		}

		private void txtSearch_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode== Keys.Return)
			{
				e.Handled=true;
				ExecuteSearch();
			}
		}

		private void WebSearchControl_Load(object sender, System.EventArgs e)
		{
			txtSearch.Click+=new EventHandler(txtSearch_Click);
			txtSearch.GotFocus+=new EventHandler(txtSearch_Click);
			this.GotFocus+=new EventHandler(WebSearchControl_GotFocus);
		}

		public void ActivateSearch()
		{
			txtSearch.Focus();			
			txtSearch.SelectAll();
			txtSearch.HideSelection=false;
		}

		private void WebSearchControl_GotFocus(object sender, EventArgs e)
		{
			ActivateSearch();
		}

		private void c1ContextMenu1_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
		
		}

	
	}
}
