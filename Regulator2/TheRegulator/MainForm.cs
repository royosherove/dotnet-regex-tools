using System;
using Regulator.SDK.Plugins;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using C1.Win.C1Command;
using System.Text.RegularExpressions;
using System.Threading;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using  Regulator.SDK;
using Regulator.SDK.ApplicationSettings;

namespace Regulator.GUI
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form,IRegexDisplay
	{
		private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
		private C1.Win.C1Command.C1CommandLink c1CommandLink1;
		private C1.Win.C1Command.C1Command cmdNewDocument;
		private C1.Win.C1Command.C1CommandLink c1CommandLink2;
		private C1.Win.C1Command.C1Command cmdSave;
		private C1.Win.C1Command.C1CommandLink c1CommandLink3;
		private C1.Win.C1Command.C1Command cmdLoadProject;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private C1.Win.C1Command.C1ToolBar c1ToolBar2;
		private C1.Win.C1Command.C1CommandLink c1CommandLink6;
		private C1.Win.C1Command.C1CommandLink c1CommandLink7;
		private C1.Win.C1Command.C1CommandLink c1CommandLink8;
		private C1.Win.C1Command.C1CommandLink c1CommandLink9;
		private C1.Win.C1Command.C1CommandLink c1CommandLink10;
		private C1.Win.C1Command.C1CommandLink c1CommandLink11;
		private C1.Win.C1Command.C1CommandLink c1CommandLink12;
		private C1.Win.C1Command.C1Command cmdMatch;
		private C1.Win.C1Command.C1Command cmdReplace;
		private C1.Win.C1Command.C1Command cmdCancelAction;
		private C1.Win.C1Command.C1CommandLink c1CommandLink18;
		private C1.Win.C1Command.C1CommandLink c1CommandLink19;
		private C1.Win.C1Command.C1CommandLink c1CommandLink20;
		private C1.Win.C1Command.C1Command cmdIgnoreCase;
		private C1.Win.C1Command.C1Command cmdMultiline;
		private C1.Win.C1Command.C1Command cmdSingleLine;
		private C1.Win.C1Command.C1Command cmdIgnoreWS;
		private C1.Win.C1Command.C1Command cmdRightToLeft;
		private C1.Win.C1Command.C1Command cmdExplicitCapture;
		private C1.Win.C1Command.C1Command cmdECMA;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private C1.Win.C1Command.C1CommandLink k;
		private C1.Win.C1Command.C1CommandMenu mnuFile;
		private C1.Win.C1Command.C1CommandLink c1CommandLink13;
		private C1.Win.C1Command.C1CommandLink c1CommandLink4;
		private C1.Win.C1Command.C1CommandLink c1CommandLink5;
		private C1.Win.C1Command.C1CommandLink c1CommandLink14;
		private C1.Win.C1Command.C1CommandMenu mnuDocument;
		private C1.Win.C1Command.C1CommandLink c1CommandLink17;
		private C1.Win.C1Command.C1CommandLink c1CommandLink15;
		private C1.Win.C1Command.C1CommandLink c1CommandLink16;
		private C1.Win.C1Command.C1MainMenu c1MainMenu1;
		private System.ComponentModel.IContainer components;
		private C1.Win.C1Command.C1CommandLink c1CommandLink22;
		private C1.Win.C1Command.C1CommandMenu mnuView;
		private C1.Win.C1Command.C1Command cmdViewWebSearch;
		private static System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private C1.Win.C1Command.C1CommandLink c1CommandLink25;
		private C1.Win.C1Command.C1CommandLink c1CommandLink26;
		private C1.Win.C1Command.C1Command cmdViewToolbox;

		private C1.Win.C1Command.C1CommandLink c1CommandLink29;
		private C1.Win.C1Command.C1CommandMenu mnuHelp;
		private C1.Win.C1Command.C1CommandLink c1CommandLink30;
		private C1.Win.C1Command.C1Command cmdAbout;
		private System.Windows.Forms.NotifyIcon tray;
		private System.Windows.Forms.ContextMenu TrayContextMenu;
		private System.Windows.Forms.MenuItem mnuTrayRestore;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem mnuTrayQuit;
		private C1.Win.C1Command.C1CommandLink c1CommandLink31;
		private C1.Win.C1Command.C1Command c1Command1;
		private C1.Win.C1Command.C1CommandLink c1CommandLink32;
		private C1.Win.C1Command.C1Command cmdQuit;
		private C1.Win.C1Command.C1CommandLink c1CommandLink33;
		private C1.Win.C1Command.C1Command cmdOptions;
		private C1.Win.C1Command.C1CommandLink c1CommandLink34;
		private C1.Win.C1Command.C1Command cmdComments;
		private C1.Win.C1Command.C1CommandLink c1CommandLink35;
		private C1.Win.C1Command.C1Command cmdWorkspace;
		private C1.Win.C1Command.C1Command cmdViewPerformance;
		private C1.Win.C1Command.C1Command cmdSubmitToRegexLib;
		private C1.Win.C1Command.C1CommandLink c1CommandLink39;
		private C1.Win.C1Command.C1CommandMenu mnuRecentFiles;
		private C1.Win.C1Command.C1CommandLink c1CommandLink40;
		private C1.Win.C1Command.C1CommandLink c1CommandLink41;
		private C1.Win.C1Command.C1Command cmdSaveAs;
		private C1.Win.C1Command.C1CommandLink c1CommandLink42;
		private C1.Win.C1Command.C1Command cmdDonate;
		private IRegexDisplay _currentInnerDisplay=null;
		private Syncfusion.Windows.Forms.Tools.DockingManager _dock;
		

		private AboutForm _about = null;
		private C1.Win.C1Command.C1CommandLink c1CommandLink43;
		private C1.Win.C1Command.C1CommandMenu mnuWindow;
		private C1.Win.C1Command.C1CommandLink c1CommandLink45;
		private C1.Win.C1Command.C1CommandMdiList c1CommandMdiList1;
		private C1.Win.C1Command.C1CommandLink c1CommandLink44;
		private C1.Win.C1Command.C1Command cmdSplit;
		private C1.Win.C1Command.C1CommandLink c1CommandLink46;
		private C1.Win.C1Command.C1CommandLink c1CommandLink47;
		private C1.Win.C1Command.C1CommandMenu mnuTools;
		private C1.Win.C1Command.C1CommandLink c1CommandLink21;
		private C1.Win.C1Command.C1Command cmdWhatsNew;
		private C1.Win.C1Command.C1CommandLink c1CommandLink23;
		private C1.Win.C1Command.C1Command cmdQuickMenuEditor;
		private System.Windows.Forms.HelpProvider help;
		private C1.Win.C1Command.C1CommandLink c1CommandLink24;
		private C1.Win.C1Command.C1Command cmdHelpUsing;
		private C1.Win.C1Command.C1CommandLink c1CommandLink27;
		private C1.Win.C1Command.C1Command cmdHelpKeyboard;
		private TabbedMDIManager _tabs = new TabbedMDIManager();

		private void DisplayAboutForm()
		{
#if(DEBUG)
			return;
#endif
			try
			{
				_about = new AboutForm();
				_about.ShowLoading();
				_about.Show();
				_about.TopMost=true;
				Application.DoEvents();
				Application.DoEvents();
				
			}
			catch(Exception )
			{
			    
			}

		}

		private void InitTabbedMDIManager()
		{
			_tabs.AttachToMdiContainer(this);
			
			
		}
		private string _InitialFileName = string.Empty;
		public MainForm(string fileName)
		{
			_InitialFileName =fileName;
			DisplayAboutForm();

			InitializeComponent();
			
			AppContext.Instance.Dialogs.AddDialog(typeof(OptionsForm),DialogCoreTypes.Options);
			this.MdiChildActivate+=new EventHandler(OnActiveDocumentChanged);

			
			InitTabbedMDIManager();
			PaneDisplay.Instance.BeforeLoadLayout+=new EventHandler(PaneDisplay_BeforeLoadLayout);
			
			PaneDisplay.Instance.InitPanes(_dock);
			PaneDisplay.Instance.LoadLayout();

			AppContext.Instance.SetDisplayManager(this);
			RefreshMRUMenu();
			OpenNewOrExistingDocument();
			AppContext.Instance.Settings.DialogSettings.RegisterForAutomaticSettingsBackup(this);
			
			HideAboutForm();
		}

		public MainForm():this(string.Empty)
		{
			InitSaveFileDialog();
		}

		private void InitSaveFileDialog()
		{
			if(dlgSaveFile !=null)
			{
				return ;
			}
			dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			dlgSaveFile.DefaultExt = "express";
			dlgSaveFile.Filter = "*.express (expression files)|*.express|*.* (all files)|*.*";

		}

		private void OpenNewOrExistingDocument()
		{
			if(_InitialFileName!=string.Empty)
			{
				OpenProject(_InitialFileName);
			}
			else
			{
				NewProject();

			}
		}
		private DockingStyle TranslatePluginDockToSyncfutionDock(PluginDockPositions position)
		{
			switch (position) 
			{
				case PluginDockPositions.Left:
					return  DockingStyle.Left;

				case PluginDockPositions.Bottom:
					return  DockingStyle.Bottom;

				case PluginDockPositions.Floating:
					return  DockingStyle.Left;
				
				case PluginDockPositions.Right:
					return  DockingStyle.Right;
				
				case PluginDockPositions.Top:
					return  DockingStyle.Top;
				
				
				default:
					return  DockingStyle.Left;
			}
			
		}

		private void AddPluginToMenu(IPlugin plugin,C1CommandMenu parentMenu)
		{
			C1Command cmd = new C1Command();
			
			cmd.Name=plugin.PluginName;
			cmd.UserData=plugin;
			cmd.Text = plugin.MenuCation;
			cmd.Click+=new ClickEventHandler(PluginMenuItem_Click);
			cmd.Shortcut= plugin.Shortcut;
			if(plugin.Icon!=null)
			{
				cmd.Image =  Image.FromHbitmap( plugin.Icon.ToBitmap().GetHbitmap());
			
			}

			c1CommandHolder1.Commands.Add(cmd);

			C1CommandLink link = new C1CommandLink(cmd);
			parentMenu.CommandLinks.Add(link);

			

		}
		private void InitPluginDockedWindow(IPlugin plugin)
		{
			this.Controls.Add(plugin.control);
			AddPluginToMenu(plugin,mnuView);


			DockingStyle dockPos = 
				TranslatePluginDockToSyncfutionDock(plugin.PreferredDockState);

			if (plugin.PreferredDockState==PluginDockPositions.Floating)
			{
				_dock.SetEnableDocking(plugin.control,true);
				_dock.SetFloatOnly(plugin.control,true);
				
			}
			else
			{
				_dock.DockControl(plugin.control,
					_dock.HostForm,
					dockPos,
					plugin.control.Width);		
				
			}
		}
		private void LoadPlugins()
		{
			foreach (IPlugin plugin in AppContext.Instance.Plugins)
			{
				switch (plugin.PluginType)
				{
					case PluginTypes.Dockable:
						InitPluginDockedWindow(plugin);
						break;

					case PluginTypes.Dialog:
						AddPluginToMenu(plugin,mnuTools);
						break;

					default:
						;
						break;
				}
			}
		}
		private void HideAboutForm()
		{
#if(DEBUG)
			return;
#endif

			try
			{
				_about.Hide();
				_about=null;
				

			}
			catch(Exception )
			{
			    
			}

		}


		private const string WEB_SEARCH_CAPTION = "Web Search";
		private const string TOOLBOX_CAPTION = "Snippets";
		private const string PERF_CAPTION = "Performance";


		




		public bool DetachCanProceed()
		{
			foreach (RegexDocument doc in _tabs.MdiChildren)
			{
				doc.Activate();
				if(!doc.CloseVerified &&
					!doc.LastCloseWasCanceled &&
					!doc.VerifyCanClose())
				{
					doc.LastCloseWasCanceled=false;
					return false;
				}

			}
			return true;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
			this.cmdNewDocument = new C1.Win.C1Command.C1Command();
			this.cmdSave = new C1.Win.C1Command.C1Command();
			this.cmdLoadProject = new C1.Win.C1Command.C1Command();
			this.cmdMatch = new C1.Win.C1Command.C1Command();
			this.cmdReplace = new C1.Win.C1Command.C1Command();
			this.cmdCancelAction = new C1.Win.C1Command.C1Command();
			this.cmdIgnoreCase = new C1.Win.C1Command.C1Command();
			this.cmdMultiline = new C1.Win.C1Command.C1Command();
			this.cmdSingleLine = new C1.Win.C1Command.C1Command();
			this.cmdIgnoreWS = new C1.Win.C1Command.C1Command();
			this.cmdRightToLeft = new C1.Win.C1Command.C1Command();
			this.cmdExplicitCapture = new C1.Win.C1Command.C1Command();
			this.cmdECMA = new C1.Win.C1Command.C1Command();
			this.mnuFile = new C1.Win.C1Command.C1CommandMenu();
			this.c1CommandLink13 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink41 = new C1.Win.C1Command.C1CommandLink();
			this.cmdSaveAs = new C1.Win.C1Command.C1Command();
			this.c1CommandLink5 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink31 = new C1.Win.C1Command.C1CommandLink();
			this.c1Command1 = new C1.Win.C1Command.C1Command();
			this.c1CommandLink39 = new C1.Win.C1Command.C1CommandLink();
			this.mnuRecentFiles = new C1.Win.C1Command.C1CommandMenu();
			this.c1CommandLink40 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink32 = new C1.Win.C1Command.C1CommandLink();
			this.cmdQuit = new C1.Win.C1Command.C1Command();
			this.mnuDocument = new C1.Win.C1Command.C1CommandMenu();
			this.c1CommandLink17 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink15 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink16 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink46 = new C1.Win.C1Command.C1CommandLink();
			this.cmdSplit = new C1.Win.C1Command.C1Command();
			this.mnuView = new C1.Win.C1Command.C1CommandMenu();
			this.c1CommandLink33 = new C1.Win.C1Command.C1CommandLink();
			this.cmdOptions = new C1.Win.C1Command.C1Command();
			this.cmdViewWebSearch = new C1.Win.C1Command.C1Command();
			this.cmdViewToolbox = new C1.Win.C1Command.C1Command();
			this.mnuHelp = new C1.Win.C1Command.C1CommandMenu();
			this.c1CommandLink21 = new C1.Win.C1Command.C1CommandLink();
			this.cmdWhatsNew = new C1.Win.C1Command.C1Command();
			this.c1CommandLink35 = new C1.Win.C1Command.C1CommandLink();
			this.cmdWorkspace = new C1.Win.C1Command.C1Command();
			this.c1CommandLink34 = new C1.Win.C1Command.C1CommandLink();
			this.cmdComments = new C1.Win.C1Command.C1Command();
			this.c1CommandLink42 = new C1.Win.C1Command.C1CommandLink();
			this.cmdDonate = new C1.Win.C1Command.C1Command();
			this.c1CommandLink30 = new C1.Win.C1Command.C1CommandLink();
			this.cmdAbout = new C1.Win.C1Command.C1Command();
			this.cmdViewPerformance = new C1.Win.C1Command.C1Command();
			this.cmdSubmitToRegexLib = new C1.Win.C1Command.C1Command();
			this.mnuWindow = new C1.Win.C1Command.C1CommandMenu();
			this.c1CommandLink45 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandMdiList1 = new C1.Win.C1Command.C1CommandMdiList();
			this.mnuTools = new C1.Win.C1Command.C1CommandMenu();
			this.c1CommandLink23 = new C1.Win.C1Command.C1CommandLink();
			this.cmdQuickMenuEditor = new C1.Win.C1Command.C1Command();
			this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.c1ToolBar2 = new C1.Win.C1Command.C1ToolBar();
			this.c1CommandLink18 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink19 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink44 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink20 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink6 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink7 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink8 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink9 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink10 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink11 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink12 = new C1.Win.C1Command.C1CommandLink();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.k = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink14 = new C1.Win.C1Command.C1CommandLink();
			this.c1MainMenu1 = new C1.Win.C1Command.C1MainMenu();
			this.c1CommandLink22 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink47 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink43 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink29 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink25 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink26 = new C1.Win.C1Command.C1CommandLink();
			this.tray = new System.Windows.Forms.NotifyIcon(this.components);
			this.TrayContextMenu = new System.Windows.Forms.ContextMenu();
			this.mnuTrayRestore = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.mnuTrayQuit = new System.Windows.Forms.MenuItem();
			this._dock = new Syncfusion.Windows.Forms.Tools.DockingManager(this.components);
			this.help = new System.Windows.Forms.HelpProvider();
			this.c1CommandLink24 = new C1.Win.C1Command.C1CommandLink();
			this.cmdHelpUsing = new C1.Win.C1Command.C1Command();
			this.c1CommandLink27 = new C1.Win.C1Command.C1CommandLink();
			this.cmdHelpKeyboard = new C1.Win.C1Command.C1Command();
			((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._dock)).BeginInit();
			this.SuspendLayout();
			// 
			// c1CommandHolder1
			// 
			this.c1CommandHolder1.Commands.Add(this.cmdNewDocument);
			this.c1CommandHolder1.Commands.Add(this.cmdSave);
			this.c1CommandHolder1.Commands.Add(this.cmdLoadProject);
			this.c1CommandHolder1.Commands.Add(this.cmdMatch);
			this.c1CommandHolder1.Commands.Add(this.cmdReplace);
			this.c1CommandHolder1.Commands.Add(this.cmdCancelAction);
			this.c1CommandHolder1.Commands.Add(this.cmdIgnoreCase);
			this.c1CommandHolder1.Commands.Add(this.cmdMultiline);
			this.c1CommandHolder1.Commands.Add(this.cmdSingleLine);
			this.c1CommandHolder1.Commands.Add(this.cmdIgnoreWS);
			this.c1CommandHolder1.Commands.Add(this.cmdRightToLeft);
			this.c1CommandHolder1.Commands.Add(this.cmdExplicitCapture);
			this.c1CommandHolder1.Commands.Add(this.cmdECMA);
			this.c1CommandHolder1.Commands.Add(this.mnuFile);
			this.c1CommandHolder1.Commands.Add(this.mnuDocument);
			this.c1CommandHolder1.Commands.Add(this.mnuView);
			this.c1CommandHolder1.Commands.Add(this.cmdViewWebSearch);
			this.c1CommandHolder1.Commands.Add(this.cmdViewToolbox);
			this.c1CommandHolder1.Commands.Add(this.mnuHelp);
			this.c1CommandHolder1.Commands.Add(this.cmdAbout);
			this.c1CommandHolder1.Commands.Add(this.c1Command1);
			this.c1CommandHolder1.Commands.Add(this.cmdQuit);
			this.c1CommandHolder1.Commands.Add(this.cmdOptions);
			this.c1CommandHolder1.Commands.Add(this.cmdComments);
			this.c1CommandHolder1.Commands.Add(this.cmdWorkspace);
			this.c1CommandHolder1.Commands.Add(this.cmdViewPerformance);
			this.c1CommandHolder1.Commands.Add(this.cmdSubmitToRegexLib);
			this.c1CommandHolder1.Commands.Add(this.mnuRecentFiles);
			this.c1CommandHolder1.Commands.Add(this.cmdSaveAs);
			this.c1CommandHolder1.Commands.Add(this.cmdDonate);
			this.c1CommandHolder1.Commands.Add(this.mnuWindow);
			this.c1CommandHolder1.Commands.Add(this.c1CommandMdiList1);
			this.c1CommandHolder1.Commands.Add(this.cmdSplit);
			this.c1CommandHolder1.Commands.Add(this.mnuTools);
			this.c1CommandHolder1.Commands.Add(this.cmdWhatsNew);
			this.c1CommandHolder1.Commands.Add(this.cmdQuickMenuEditor);
			this.c1CommandHolder1.Commands.Add(this.cmdHelpUsing);
			this.c1CommandHolder1.Commands.Add(this.cmdHelpKeyboard);
			this.c1CommandHolder1.Owner = this;
			// 
			// cmdNewDocument
			// 
			this.cmdNewDocument.Image = ((System.Drawing.Image)(resources.GetObject("cmdNewDocument.Image")));
			this.cmdNewDocument.Name = "cmdNewDocument";
			this.cmdNewDocument.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.cmdNewDocument.Text = "&New regex document";
			this.cmdNewDocument.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdNewDocument_Click);
			// 
			// cmdSave
			// 
			this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.cmdSave.Text = "&Save current document";
			this.cmdSave.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdSave_Click);
			// 
			// cmdLoadProject
			// 
			this.cmdLoadProject.Image = ((System.Drawing.Image)(resources.GetObject("cmdLoadProject.Image")));
			this.cmdLoadProject.Name = "cmdLoadProject";
			this.cmdLoadProject.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.cmdLoadProject.Text = "&Open a document";
			this.cmdLoadProject.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdLoadProject_Click);
			// 
			// cmdMatch
			// 
			this.cmdMatch.Image = ((System.Drawing.Image)(resources.GetObject("cmdMatch.Image")));
			this.cmdMatch.Name = "cmdMatch";
			this.cmdMatch.Shortcut = System.Windows.Forms.Shortcut.F5;
			this.cmdMatch.Text = "&Match";
			this.cmdMatch.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdMatch_Click);
			// 
			// cmdReplace
			// 
			this.cmdReplace.Image = ((System.Drawing.Image)(resources.GetObject("cmdReplace.Image")));
			this.cmdReplace.Name = "cmdReplace";
			this.cmdReplace.Shortcut = System.Windows.Forms.Shortcut.F6;
			this.cmdReplace.Text = "&Replace";
			this.cmdReplace.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdReplace_Click);
			// 
			// cmdCancelAction
			// 
			this.cmdCancelAction.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancelAction.Image")));
			this.cmdCancelAction.Name = "cmdCancelAction";
			this.cmdCancelAction.Shortcut = System.Windows.Forms.Shortcut.AltF12;
			this.cmdCancelAction.Text = "&Cancel running action";
			this.cmdCancelAction.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdCancelAction_Click);
			// 
			// cmdIgnoreCase
			// 
			this.cmdIgnoreCase.CheckAutoToggle = true;
			this.cmdIgnoreCase.Image = ((System.Drawing.Image)(resources.GetObject("cmdIgnoreCase.Image")));
			this.cmdIgnoreCase.Name = "cmdIgnoreCase";
			this.cmdIgnoreCase.Text = "Ignore case";
			this.cmdIgnoreCase.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdIgnoreCase_Click);
			// 
			// cmdMultiline
			// 
			this.cmdMultiline.CheckAutoToggle = true;
			this.cmdMultiline.Image = ((System.Drawing.Image)(resources.GetObject("cmdMultiline.Image")));
			this.cmdMultiline.Name = "cmdMultiline";
			this.cmdMultiline.Text = "Multiline";
			this.cmdMultiline.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdMultiline_Click);
			// 
			// cmdSingleLine
			// 
			this.cmdSingleLine.CheckAutoToggle = true;
			this.cmdSingleLine.Image = ((System.Drawing.Image)(resources.GetObject("cmdSingleLine.Image")));
			this.cmdSingleLine.Name = "cmdSingleLine";
			this.cmdSingleLine.Text = "Single line";
			this.cmdSingleLine.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdSingleLine_Click);
			// 
			// cmdIgnoreWS
			// 
			this.cmdIgnoreWS.CheckAutoToggle = true;
			this.cmdIgnoreWS.Image = ((System.Drawing.Image)(resources.GetObject("cmdIgnoreWS.Image")));
			this.cmdIgnoreWS.Name = "cmdIgnoreWS";
			this.cmdIgnoreWS.Text = "Ignore whitespace";
			this.cmdIgnoreWS.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdIgnoreWS_Click);
			// 
			// cmdRightToLeft
			// 
			this.cmdRightToLeft.CheckAutoToggle = true;
			this.cmdRightToLeft.Image = ((System.Drawing.Image)(resources.GetObject("cmdRightToLeft.Image")));
			this.cmdRightToLeft.Name = "cmdRightToLeft";
			this.cmdRightToLeft.Text = "Right to left";
			this.cmdRightToLeft.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdRightToLeft_Click);
			// 
			// cmdExplicitCapture
			// 
			this.cmdExplicitCapture.CheckAutoToggle = true;
			this.cmdExplicitCapture.Image = ((System.Drawing.Image)(resources.GetObject("cmdExplicitCapture.Image")));
			this.cmdExplicitCapture.Name = "cmdExplicitCapture";
			this.cmdExplicitCapture.Text = "Explicit capture";
			this.cmdExplicitCapture.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdExplicitCapture_Click);
			// 
			// cmdECMA
			// 
			this.cmdECMA.CheckAutoToggle = true;
			this.cmdECMA.Image = ((System.Drawing.Image)(resources.GetObject("cmdECMA.Image")));
			this.cmdECMA.Name = "cmdECMA";
			this.cmdECMA.Text = "ECMA script";
			this.cmdECMA.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdECMA_Click);
			// 
			// mnuFile
			// 
			this.mnuFile.CommandLinks.Add(this.c1CommandLink13);
			this.mnuFile.CommandLinks.Add(this.c1CommandLink4);
			this.mnuFile.CommandLinks.Add(this.c1CommandLink41);
			this.mnuFile.CommandLinks.Add(this.c1CommandLink5);
			this.mnuFile.CommandLinks.Add(this.c1CommandLink31);
			this.mnuFile.CommandLinks.Add(this.c1CommandLink39);
			this.mnuFile.CommandLinks.Add(this.c1CommandLink32);
			this.mnuFile.Image = ((System.Drawing.Image)(resources.GetObject("mnuFile.Image")));
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Text = "&File";
			// 
			// c1CommandLink13
			// 
			this.c1CommandLink13.Command = this.cmdNewDocument;
			this.c1CommandLink13.Text = "&New";
			// 
			// c1CommandLink4
			// 
			this.c1CommandLink4.Command = this.cmdSave;
			this.c1CommandLink4.Text = "&Save";
			// 
			// c1CommandLink41
			// 
			this.c1CommandLink41.Command = this.cmdSaveAs;
			// 
			// cmdSaveAs
			// 
			this.cmdSaveAs.Name = "cmdSaveAs";
			this.cmdSaveAs.Text = "Save &As...";
			this.cmdSaveAs.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdSaveAs_Click);
			// 
			// c1CommandLink5
			// 
			this.c1CommandLink5.Command = this.cmdLoadProject;
			this.c1CommandLink5.Text = "&Open...";
			// 
			// c1CommandLink31
			// 
			this.c1CommandLink31.Command = this.c1Command1;
			// 
			// c1Command1
			// 
			this.c1Command1.Name = "c1Command1";
			this.c1Command1.Text = "-";
			// 
			// c1CommandLink39
			// 
			this.c1CommandLink39.Command = this.mnuRecentFiles;
			// 
			// mnuRecentFiles
			// 
			this.mnuRecentFiles.CommandLinks.Add(this.c1CommandLink40);
			this.mnuRecentFiles.Name = "mnuRecentFiles";
			this.mnuRecentFiles.Text = "&Recent files";
			// 
			// c1CommandLink40
			// 
			this.c1CommandLink40.Text = "New Command";
			// 
			// c1CommandLink32
			// 
			this.c1CommandLink32.Command = this.cmdQuit;
			this.c1CommandLink32.Delimiter = true;
			// 
			// cmdQuit
			// 
			this.cmdQuit.Name = "cmdQuit";
			this.cmdQuit.Text = "&Quit";
			this.cmdQuit.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdQuit_Click);
			// 
			// mnuDocument
			// 
			this.mnuDocument.CommandLinks.Add(this.c1CommandLink17);
			this.mnuDocument.CommandLinks.Add(this.c1CommandLink15);
			this.mnuDocument.CommandLinks.Add(this.c1CommandLink16);
			this.mnuDocument.CommandLinks.Add(this.c1CommandLink46);
			this.mnuDocument.Name = "mnuDocument";
			this.mnuDocument.Text = "Document";
			// 
			// c1CommandLink17
			// 
			this.c1CommandLink17.Command = this.cmdCancelAction;
			// 
			// c1CommandLink15
			// 
			this.c1CommandLink15.Command = this.cmdMatch;
			// 
			// c1CommandLink16
			// 
			this.c1CommandLink16.Command = this.cmdReplace;
			// 
			// c1CommandLink46
			// 
			this.c1CommandLink46.Command = this.cmdSplit;
			// 
			// cmdSplit
			// 
			this.cmdSplit.Image = ((System.Drawing.Image)(resources.GetObject("cmdSplit.Image")));
			this.cmdSplit.Name = "cmdSplit";
			this.cmdSplit.Shortcut = System.Windows.Forms.Shortcut.F7;
			this.cmdSplit.Text = "Split";
			this.cmdSplit.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdSplit_Click);
			// 
			// mnuView
			// 
			this.mnuView.CommandLinks.Add(this.c1CommandLink33);
			this.mnuView.Name = "mnuView";
			this.mnuView.Text = "&View";
			// 
			// c1CommandLink33
			// 
			this.c1CommandLink33.Command = this.cmdOptions;
			this.c1CommandLink33.Delimiter = true;
			// 
			// cmdOptions
			// 
			this.cmdOptions.Image = ((System.Drawing.Image)(resources.GetObject("cmdOptions.Image")));
			this.cmdOptions.Name = "cmdOptions";
			this.cmdOptions.Text = "&Options";
			this.cmdOptions.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdOptions_Click);
			// 
			// cmdViewWebSearch
			// 
			this.cmdViewWebSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdViewWebSearch.Image")));
			this.cmdViewWebSearch.Name = "cmdViewWebSearch";
			this.cmdViewWebSearch.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftF;
			this.cmdViewWebSearch.Text = "&Web Search";
			this.cmdViewWebSearch.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdViewWebSearch_Click);
			// 
			// cmdViewToolbox
			// 
			this.cmdViewToolbox.Image = ((System.Drawing.Image)(resources.GetObject("cmdViewToolbox.Image")));
			this.cmdViewToolbox.Name = "cmdViewToolbox";
			this.cmdViewToolbox.Shortcut = System.Windows.Forms.Shortcut.ShiftF1;
			this.cmdViewToolbox.Text = "&Snippets";
			// 
			// mnuHelp
			// 
			this.mnuHelp.CommandLinks.Add(this.c1CommandLink24);
			this.mnuHelp.CommandLinks.Add(this.c1CommandLink27);
			this.mnuHelp.CommandLinks.Add(this.c1CommandLink21);
			this.mnuHelp.CommandLinks.Add(this.c1CommandLink35);
			this.mnuHelp.CommandLinks.Add(this.c1CommandLink34);
			this.mnuHelp.CommandLinks.Add(this.c1CommandLink42);
			this.mnuHelp.CommandLinks.Add(this.c1CommandLink30);
			this.mnuHelp.Name = "mnuHelp";
			this.mnuHelp.Text = "&Help";
			// 
			// c1CommandLink21
			// 
			this.c1CommandLink21.Command = this.cmdWhatsNew;
			// 
			// cmdWhatsNew
			// 
			this.cmdWhatsNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdWhatsNew.Image")));
			this.cmdWhatsNew.Name = "cmdWhatsNew";
			this.cmdWhatsNew.Text = "&What\'s New?";
			this.cmdWhatsNew.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdWhatsNew_Click);
			// 
			// c1CommandLink35
			// 
			this.c1CommandLink35.Command = this.cmdWorkspace;
			this.c1CommandLink35.Delimiter = true;
			this.c1CommandLink35.Text = "&Homepage";
			// 
			// cmdWorkspace
			// 
			this.cmdWorkspace.Image = ((System.Drawing.Image)(resources.GetObject("cmdWorkspace.Image")));
			this.cmdWorkspace.Name = "cmdWorkspace";
			this.cmdWorkspace.Text = "&GDN Workspace";
			this.cmdWorkspace.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdWorkspace_Click);
			// 
			// c1CommandLink34
			// 
			this.c1CommandLink34.Command = this.cmdComments;
			this.c1CommandLink34.Text = "&Knowledge Base";
			// 
			// cmdComments
			// 
			this.cmdComments.Image = ((System.Drawing.Image)(resources.GetObject("cmdComments.Image")));
			this.cmdComments.Name = "cmdComments";
			this.cmdComments.Text = "&Comments/Suggestions";
			this.cmdComments.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdComments_Click);
			// 
			// c1CommandLink42
			// 
			this.c1CommandLink42.Command = this.cmdDonate;
			this.c1CommandLink42.Delimiter = true;
			this.c1CommandLink42.Text = "&Like The Regulator?";
			// 
			// cmdDonate
			// 
			this.cmdDonate.Image = ((System.Drawing.Image)(resources.GetObject("cmdDonate.Image")));
			this.cmdDonate.Name = "cmdDonate";
			this.cmdDonate.Text = "&Donate";
			this.cmdDonate.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdDonate_Click);
			// 
			// c1CommandLink30
			// 
			this.c1CommandLink30.Command = this.cmdAbout;
			this.c1CommandLink30.Delimiter = true;
			// 
			// cmdAbout
			// 
			this.cmdAbout.Image = ((System.Drawing.Image)(resources.GetObject("cmdAbout.Image")));
			this.cmdAbout.Name = "cmdAbout";
			this.cmdAbout.Text = "&About";
			this.cmdAbout.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdAbout_Click);
			// 
			// cmdViewPerformance
			// 
			this.cmdViewPerformance.Image = ((System.Drawing.Image)(resources.GetObject("cmdViewPerformance.Image")));
			this.cmdViewPerformance.Name = "cmdViewPerformance";
			this.cmdViewPerformance.Shortcut = System.Windows.Forms.Shortcut.F10;
			this.cmdViewPerformance.Text = "Performance";
			// 
			// cmdSubmitToRegexLib
			// 
			this.cmdSubmitToRegexLib.Image = ((System.Drawing.Image)(resources.GetObject("cmdSubmitToRegexLib.Image")));
			this.cmdSubmitToRegexLib.Name = "cmdSubmitToRegexLib";
			this.cmdSubmitToRegexLib.Text = "&Submit to RegexLib.com";
			// 
			// mnuWindow
			// 
			this.mnuWindow.CommandLinks.Add(this.c1CommandLink45);
			this.mnuWindow.Name = "mnuWindow";
			this.mnuWindow.Text = "&Window";
			// 
			// c1CommandLink45
			// 
			this.c1CommandLink45.Command = this.c1CommandMdiList1;
			// 
			// c1CommandMdiList1
			// 
			this.c1CommandMdiList1.Name = "c1CommandMdiList1";
			// 
			// mnuTools
			// 
			this.mnuTools.CommandLinks.Add(this.c1CommandLink23);
			this.mnuTools.Name = "mnuTools";
			this.mnuTools.Text = "&Tools";
			// 
			// c1CommandLink23
			// 
			this.c1CommandLink23.Command = this.cmdQuickMenuEditor;
			// 
			// cmdQuickMenuEditor
			// 
			this.cmdQuickMenuEditor.Name = "cmdQuickMenuEditor";
			this.cmdQuickMenuEditor.Text = "&QuickMenu Editor...";
			this.cmdQuickMenuEditor.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdQuickMenuEditor_Click);
			// 
			// c1CommandLink1
			// 
			this.c1CommandLink1.Command = this.cmdNewDocument;
			// 
			// c1CommandLink2
			// 
			this.c1CommandLink2.Command = this.cmdSave;
			// 
			// c1CommandLink3
			// 
			this.c1CommandLink3.Command = this.cmdLoadProject;
			this.c1CommandLink3.Text = "Open";
			// 
			// dlgOpenFile
			// 
			this.dlgOpenFile.Filter = "*.express (expression files)|*.express|*.* (all files)|*.*";
			// 
			// c1ToolBar2
			// 
			this.c1ToolBar2.AutoSize = false;
			this.c1ToolBar2.CommandHolder = this.c1CommandHolder1;
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink1);
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink2);
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink3);
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink18);
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink19);
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink44);
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink20);
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink6);
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink7);
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink8);
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink9);
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink10);
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink11);
			this.c1ToolBar2.CommandLinks.Add(this.c1CommandLink12);
			this.c1ToolBar2.Dock = System.Windows.Forms.DockStyle.Top;
			this.c1ToolBar2.Location = new System.Drawing.Point(0, 22);
			this.c1ToolBar2.Name = "c1ToolBar2";
			this.c1ToolBar2.Size = new System.Drawing.Size(624, 24);
			this.c1ToolBar2.Text = "c1ToolBar2";
			// 
			// c1CommandLink18
			// 
			this.c1CommandLink18.ButtonLook = C1.Win.C1Command.ButtonLookFlags.TextAndImage;
			this.c1CommandLink18.Command = this.cmdMatch;
			this.c1CommandLink18.Delimiter = true;
			// 
			// c1CommandLink19
			// 
			this.c1CommandLink19.ButtonLook = C1.Win.C1Command.ButtonLookFlags.TextAndImage;
			this.c1CommandLink19.Command = this.cmdReplace;
			// 
			// c1CommandLink44
			// 
			this.c1CommandLink44.ButtonLook = C1.Win.C1Command.ButtonLookFlags.TextAndImage;
			this.c1CommandLink44.Command = this.cmdSplit;
			// 
			// c1CommandLink20
			// 
			this.c1CommandLink20.ButtonLook = C1.Win.C1Command.ButtonLookFlags.TextAndImage;
			this.c1CommandLink20.Command = this.cmdCancelAction;
			this.c1CommandLink20.Text = "&Cancel";
			// 
			// c1CommandLink6
			// 
			this.c1CommandLink6.Command = this.cmdIgnoreCase;
			this.c1CommandLink6.Delimiter = true;
			this.c1CommandLink6.Text = "Ignore case";
			// 
			// c1CommandLink7
			// 
			this.c1CommandLink7.Command = this.cmdMultiline;
			// 
			// c1CommandLink8
			// 
			this.c1CommandLink8.Command = this.cmdSingleLine;
			// 
			// c1CommandLink9
			// 
			this.c1CommandLink9.Command = this.cmdIgnoreWS;
			// 
			// c1CommandLink10
			// 
			this.c1CommandLink10.Command = this.cmdRightToLeft;
			// 
			// c1CommandLink11
			// 
			this.c1CommandLink11.Command = this.cmdExplicitCapture;
			// 
			// c1CommandLink12
			// 
			this.c1CommandLink12.Command = this.cmdECMA;
			this.c1CommandLink12.Text = "No whitespace";
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem4});
			this.menuItem1.MergeType = System.Windows.Forms.MenuMerge.Replace;
			this.menuItem1.Text = "&Regex";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.F5;
			this.menuItem2.Text = "Run &Match";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.F6;
			this.menuItem3.Text = "Run &Replace";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Shortcut = System.Windows.Forms.Shortcut.F12;
			this.menuItem4.Text = "&Cancel";
			// 
			// k
			// 
			this.k.Command = this.mnuFile;
			// 
			// c1CommandLink14
			// 
			this.c1CommandLink14.Command = this.mnuDocument;
			// 
			// c1MainMenu1
			// 
			this.c1MainMenu1.CommandHolder = this.c1CommandHolder1;
			this.c1MainMenu1.CommandLinks.Add(this.k);
			this.c1MainMenu1.CommandLinks.Add(this.c1CommandLink22);
			this.c1MainMenu1.CommandLinks.Add(this.c1CommandLink14);
			this.c1MainMenu1.CommandLinks.Add(this.c1CommandLink47);
			this.c1MainMenu1.CommandLinks.Add(this.c1CommandLink43);
			this.c1MainMenu1.CommandLinks.Add(this.c1CommandLink29);
			this.c1MainMenu1.Dock = System.Windows.Forms.DockStyle.Top;
			this.c1MainMenu1.Location = new System.Drawing.Point(0, 0);
			this.c1MainMenu1.Name = "c1MainMenu1";
			this.c1MainMenu1.Size = new System.Drawing.Size(624, 22);
			this.c1MainMenu1.Text = "c1MainMenu1";
			// 
			// c1CommandLink22
			// 
			this.c1CommandLink22.Command = this.mnuView;
			// 
			// c1CommandLink47
			// 
			this.c1CommandLink47.Command = this.mnuTools;
			// 
			// c1CommandLink43
			// 
			this.c1CommandLink43.Command = this.mnuWindow;
			// 
			// c1CommandLink29
			// 
			this.c1CommandLink29.Command = this.mnuHelp;
			// 
			// c1CommandLink25
			// 
			this.c1CommandLink25.Text = "New Command";
			// 
			// c1CommandLink26
			// 
			this.c1CommandLink26.Text = "New Command";
			// 
			// tray
			// 
			this.tray.ContextMenu = this.TrayContextMenu;
			this.tray.Text = "The Regulator";
			this.tray.DoubleClick += new System.EventHandler(this.tray_DoubleClick);
			// 
			// TrayContextMenu
			// 
			this.TrayContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.mnuTrayRestore,
																							this.menuItem7,
																							this.mnuTrayQuit});
			// 
			// mnuTrayRestore
			// 
			this.mnuTrayRestore.DefaultItem = true;
			this.mnuTrayRestore.Index = 0;
			this.mnuTrayRestore.Text = "&Resore The Regulator";
			this.mnuTrayRestore.Click += new System.EventHandler(this.mnuTrayRestore_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 1;
			this.menuItem7.Text = "-";
			// 
			// mnuTrayQuit
			// 
			this.mnuTrayQuit.Index = 2;
			this.mnuTrayQuit.Text = "&Quit";
			this.mnuTrayQuit.Click += new System.EventHandler(this.mnuTrayQuit_Click);
			// 
			// _dock
			// 
			this._dock.DockLayoutStream = ((System.IO.MemoryStream)(resources.GetObject("_dock.DockLayoutStream")));
			this._dock.HostForm = this;
			// 
			// help
			// 
			this.help.HelpNamespace = "Regulator2Help.chm";
			// 
			// c1CommandLink24
			// 
			this.c1CommandLink24.Command = this.cmdHelpUsing;
			// 
			// cmdHelpUsing
			// 
			this.cmdHelpUsing.Image = ((System.Drawing.Image)(resources.GetObject("cmdHelpUsing.Image")));
			this.cmdHelpUsing.Name = "cmdHelpUsing";
			this.cmdHelpUsing.Text = "&Using The Regulator...";
			this.cmdHelpUsing.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdHelpUsing_Click);
			// 
			// c1CommandLink27
			// 
			this.c1CommandLink27.Command = this.cmdHelpKeyboard;
			// 
			// cmdHelpKeyboard
			// 
			this.cmdHelpKeyboard.Image = ((System.Drawing.Image)(resources.GetObject("cmdHelpKeyboard.Image")));
			this.cmdHelpKeyboard.Name = "cmdHelpKeyboard";
			this.cmdHelpKeyboard.Text = "Keyboard Shortcuts...";
			this.cmdHelpKeyboard.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdHelpKeyboard_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(624, 421);
			this.Controls.Add(this.c1ToolBar2);
			this.Controls.Add(this.c1MainMenu1);
			this.HelpButton = true;
			this.help.SetHelpKeyword(this, "Introduction");
			this.IsMdiContainer = true;
			this.Name = "MainForm";
			this.help.SetShowHelp(this, true);
			this.Text = "The Regulator";
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.MainForm_HelpRequested);
			((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._dock)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		

		private void NewProject()
		{
			RegexDocument doc = new RegexDocument();
			
			doc.MdiParent=this;
			
			this.Show();
			this.SuspendLayout();
			doc.Show();

			doc.Activate();
			_currentInnerDisplay =GetSelectedDocument();
			GetSelectedDocument().RegexProject= new RegexProject();
			AppContext.Instance.OnProjectChange(GetSelectedDocument().RegexProject);
			GetSelectedDocument().RegexProcessingStarted+=new EventHandler(MainForm_RegexProcessingStarted);
			GetSelectedDocument().RegexProcessingFinished+=new Regulator.GUI.RegexDocument.FinishDelegate(MainForm_RegexProcessingFinished);

			GetSelectedDocument().RefereshDisplay();
			GetSelectedDocument().Focus();
			GetSelectedDocument().Dirty=false;

			GetSelectedDocument().CloseRequested+=new EventHandler(MainForm_CloseRequested);
			RefreshForDifferentDocument();
			this.ResumeLayout();
		}


		private RegexDocument GetSelectedDocument()
		{
			if( _tabs.MdiChildren.Length==0 )
			{
				return null;
			}
			
			return (RegexDocument)this.ActiveMdiChild;
		}


		private bool ActiveDocumentExists()
		{
			return (GetSelectedDocument()!=null);
		}

		

		private void cmdLoadProject_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			OpenProject();
		}

		private bool ProjectAlreadyOpen(string filename)
		{
			foreach (Form child in _tabs.MdiChildren)
			{
				if(((RegexDocument)child).RegexProject.FileName==filename)
				{
					child.Activate();
					return true;
				}

			}
			return false;

		}
		private void OpenProject(string fileName)
		{
			try
			{
				if(!File.Exists(fileName))
				{
					MessageBox.Show("Could not locate file: \n" + fileName);
					return ;
				}
				if(ProjectAlreadyOpen(fileName))
				{
					MessageBox.Show("This file is already open in the editor pane");
					return ;
				}
				NewProject();
				GetSelectedDocument().LoadFile(fileName);
				ShowCurrentDocumentProperties();
				AddFilenameToMRU(fileName);
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
			
		}

		private void RefreshMRUMenu()
		{
			mnuRecentFiles.CommandLinks.Clear();
			for (int i = 0; i < AppContext.Instance.Settings.MRU.Count; i++)
			{
				string filename = AppContext.Instance.Settings.MRU[i];
				C1Command cmd = new C1Command();
				cmd.Visible=true;
				cmd.Text= "&" + (i+1).ToString() + ". " + filename;
				cmd.Click+=new ClickEventHandler(cmd_Click);
				cmd.UserData = filename;
				mnuRecentFiles.CommandLinks.Add(new C1CommandLink(cmd));

			}
		}

		private void OpenProject()
		{
			DialogResult res= dlgOpenFile.ShowDialog();
			if(res== DialogResult.OK)
			{
				OpenProject(dlgOpenFile.FileName);
			}
		}


		private void SaveCurrentProject()
		{
			SaveCurrentProject(false);
		}

		public static bool SaveProject(RegexProject project)
		{
			return SaveProject(project,false);
		}
		public static bool SaveProject(RegexProject project,bool forceSaveAs)
		{
			bool AddToRecentList=false;
			if(project!=null)
			{
				if(forceSaveAs ||
					project.ShouldSaveAs)
				{
					if(dlgSaveFile.ShowDialog()==DialogResult.OK)
					{
						project.FileName = dlgSaveFile.FileName;
						
						AddToRecentList=true;
					}
					else
					{
						return AddToRecentList;
					}
				}
				project.Save(project.FileName);
				//force the Dirty flag to false (ugly)
				MainForm FormManager = (MainForm)AppContext.Instance.Display;
				FormManager.GetSelectedDocument().Dirty=false;
			
			}
			return AddToRecentList;
		}
		private void SaveCurrentProject(bool forceSaveAs)
		{
			bool AddToRecentList=false;
			if(ActiveDocumentExists())
			{
				AddToRecentList=SaveProject(GetSelectedDocument().RegexProject,forceSaveAs);
				
				if(AddToRecentList)
				{
					AddFilenameToMRU(GetSelectedDocument().RegexProject.FileName);
				}
			}
		}

		private void AddFilenameToMRU(string filename)
		{
			AppContext.Instance.Settings.MRU.Add(filename);
			AppContext.Instance.Settings.Save();
			RefreshMRUMenu();
			
		}


		private void cmdSave_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			SaveCurrentProject();
		}


		private void tabControl1_ClosePressed(object sender, System.EventArgs e)
		{
			OnRemoveCurrentDocument();
		}

		private void OnRemoveCurrentDocument()
		{
			

			EnableRunButtons(false);
			cmdCancelAction.Visible= false;

		}

		private void  EnableRunButtons(bool enabled)
		{

			cmdCancelAction.Visible= !enabled;
			
			cmdReplace.Enabled=enabled;
			cmdMatch.Enabled=enabled;
			cmdSplit.Enabled=enabled;
			
			//option buttons
			cmdECMA.Enabled= enabled;
			cmdExplicitCapture.Enabled= enabled;
			cmdIgnoreCase.Enabled= enabled;
			cmdRightToLeft.Enabled= enabled;
			cmdIgnoreWS.Enabled= enabled;
			cmdMultiline.Enabled= enabled;
			cmdSingleLine.Enabled= enabled;
			
		}

		private void cmdIgnoreCase_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{

			SetRegexOption(RegexOptions.IgnoreCase,cmdIgnoreCase.Checked);
		}

		private void SetRegexOption(RegexOptions option,bool enabled)
		{
			
			RegexProject proj =GetSelectedDocument().RegexProject;
			if(enabled)
			{
				proj.Options|=option;
			
			}
			else
			{
				
				proj.Options^=option;
			}
			
		}

		private void cmdMultiline_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			SetRegexOption(RegexOptions.Multiline,cmdMultiline.Checked);

		}

		private void cmdSingleLine_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			SetRegexOption(RegexOptions.Singleline,cmdSingleLine.Checked);

		}

		private void cmdIgnoreWS_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			SetRegexOption(RegexOptions.IgnorePatternWhitespace,cmdIgnoreWS.Checked);

		}

		private void cmdRightToLeft_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			SetRegexOption(RegexOptions.RightToLeft,cmdRightToLeft.Checked);

		}

		private void cmdExplicitCapture_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			SetRegexOption(RegexOptions.ExplicitCapture,cmdExplicitCapture.Checked);

		}

		private void cmdECMA_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			SetRegexOption(RegexOptions.ECMAScript,cmdECMA.Checked);

		}

	

		private void ShowCurrentDocumentProperties()
		{
			try
			{
				_currentInnerDisplay=GetSelectedDocument();
				RegexDocument doc = GetSelectedDocument();
				SetFormValuesFromProject(doc.RegexProject);
				EnableRunButtons(!doc.IsRunning);
			}
			catch(Exception )
			{
			    
			}
		}

		private void SetFormValuesFromProject(RegexProject project)
		{
			if(project==null)
			{
				return ;
			}



			cmdECMA.Checked= GetOptionState(project.Options , RegexOptions.ECMAScript);
			cmdExplicitCapture.Checked= GetOptionState(project.Options , RegexOptions.ExplicitCapture);
			cmdIgnoreCase.Checked= GetOptionState(project.Options , RegexOptions.IgnoreCase);
			cmdIgnoreWS.Checked= GetOptionState(project.Options , RegexOptions.IgnorePatternWhitespace);
			cmdMultiline.Checked= GetOptionState(project.Options , RegexOptions.Multiline);
			cmdRightToLeft.Checked= GetOptionState(project.Options , RegexOptions.RightToLeft);
			cmdSingleLine.Checked= GetOptionState(project.Options , RegexOptions.Singleline);

		}

		private bool GetOptionState(RegexOptions enumeratedObject,RegexOptions wantedEnum)
		{
			return (enumeratedObject & wantedEnum)==wantedEnum;
		}


		private void MainForm_RegexProcessingStarted(object sender, EventArgs e)
		{
			
			//if the sender is the active tab we can enable/disable the buttons
			if(((RegexDocument)sender).GetHashCode()==GetSelectedDocument().GetHashCode())
			{
				EnableRunButtons(false);	
			}
		}

		private void MainForm_RegexProcessingFinished(RegexDocument sender, RegexActionTypes action)
		{

			if (GetSelectedDocument()==null)
			{
				//we might get this event if only one tab exists
				//this is a bug. since we got this event
				//a form is running, it must be our form
				//if there is only one tab
				EnableRunButtons(true);	
				return ;
			}

			//if the sender is the active tab we can enable/disable the buttons
			if(((RegexDocument)sender).GetHashCode()==GetSelectedDocument().GetHashCode())
			{
				EnableRunButtons(true);	
			}
		}

		private void cmdCancelAction_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			GetSelectedDocument().CancelRunningThread();
		}

		private void cmdMatch_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			GetSelectedDocument().StartMatchThread();
		}

		private void cmdReplace_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			GetSelectedDocument().StartReplaceThread();
		}

		private void txtSearch_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtSearch_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			
		}


		private void txtSearch_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
		}



	
		protected override bool ProcessDialogKey(Keys keyData)
		{
			try
			{
				return base.ProcessDialogKey (keyData);
				
			}
			catch(Exception )
			{
				return false;	    
			}
		}
	
		/// <summary>
		///     we have to override this
		///     because otherwise we get strange ProcessMnemonic exceptions
		///     from thE MAGIC tab control 
		///     when typing inside the textAreaControl.
		///     Stragely, this occures only in this project
		///     and I cant reporo it..
		///     seems to be a mixture of all the controls on the form
		///     and the child forms hosted inside the tab control.
		/// </summary>
		/// <param name="charCode" type="char">
		///     <para>
		///         
		///     </para>
		/// </param>
		/// <returns>
		///     A bool value...
		/// </returns>
		protected override bool ProcessMnemonic(char charCode)
		{
			try
			{
				return base.ProcessMnemonic (charCode);
				
			}
			catch(Exception )
			{
				return false;
			}
		}
		#region IRegexDisplay Members

		public void RefreshSettings()
		{
		}


		public  void DisplayPlugin(IPlugin plugin)
		{
			switch (plugin.PluginType)
			{
				case PluginTypes.Dockable:
					PaneDisplay.Instance.ShowPane(plugin);
					plugin.OnDockActivate();
					break;

				case PluginTypes.Dialog:
					plugin.OnDialogClick();
					break;

				default:
					;
					break;
			}


		}
		[Obsolete("Use AppContext.Instance.ActiveDocument proerties instead")]
		public void InsertTextIntoCurrentRegex(string regex,bool clearFirst)
		{
			_currentInnerDisplay.InsertTextIntoCurrentRegex(regex,clearFirst);
		}

		[Obsolete("Use AppContext.Instance.ActiveDocument proerties instead")]
		public void InsertTextIntoCurrentRegex(string text)
		{
			if(_currentInnerDisplay!=null)
			{
				_currentInnerDisplay.InsertTextIntoCurrentRegex(text);
			}
		}

		[Obsolete("Use AppContext.Instance.ActiveDocument proerties instead")]
		public void InsertTextIntoCurrentRegex(string regex, string input, string description)
		{
			if(_currentInnerDisplay!=null)
			{
				_currentInnerDisplay.InsertTextIntoCurrentRegex(regex,input,description);
			}
		}

		public void CreateNewDocument(string regex, string input, string description)
		{
			NewProject();
			AppContext.Instance.ActiveProject.Regex= description + Environment.NewLine + regex;
			AppContext.Instance.ActiveProject.Input= input;
			
		}


		
		public RegexProject CurrentDocument
		{
			get
			{
				if(GetSelectedDocument()==null)
				{
					return null;
				}
				return 	GetSelectedDocument().RegexProject;
			}
		}
		#endregion

		private void cmdAbout_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			ShowAbout();
		}

		private void ShowAbout()
		{
			new AboutDialog().ShowDialog();
		}
	
		public void InsertTextIntoCurrentInput(string text)
		{
			_currentInnerDisplay.InsertTextIntoCurrentInput(text);

		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(!DetachCanProceed())
			{
				e.Cancel=true;
			}
			FinishUp();
		}

		private void  FinishUp()
		{
			PaneDisplay.Instance.SaveLayout();
			AppContext.Instance.Settings.Save();
		}


		private void MainForm_CloseRequested(object sender, EventArgs e)
		{
			OnRemoveCurrentDocument();
		}

		private FormWindowState LastWindowState=FormWindowState.Normal;
		private void MainForm_Resize(object sender, System.EventArgs e)
		{
			if(this.WindowState==FormWindowState.Minimized)
			{
				if(AppContext.Instance.Settings.MinimizeToTray)
				{
					tray.Icon= this.Icon;
					tray.Visible=true;
					this.Hide();
				}
			}
			else
			{
				LastWindowState =WindowState;
			}
			
		}

		private void mnuTrayRestore_Click(object sender, System.EventArgs e)
		{
			RestoreFormFromTray();
		}

		private void MainForm_SizeChanged(object sender, System.EventArgs e)
		{
		
		}

		private void cmdQuit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			Quit();
		}

		private void Quit()
		{
			Close();
		}

		private void mnuTrayQuit_Click(object sender, System.EventArgs e)
		{
			Quit();
		}

		private void tray_DoubleClick(object sender, System.EventArgs e)
		{
			RestoreFormFromTray();
		}

		private void RestoreFormFromTray()
		{
			
			tray.Visible=false;
			this.Show();
			this.Focus();
			this.BringToFront();
			this.WindowState=LastWindowState;
		}

		private void cmdOptions_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			ShowOptions();
		}

		private void ShowOptions()
		{
			AppContext.Instance.Dialogs.ShowOptions();

		}

		private void cmdComments_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{
				Process.Start(@"http://royo.is-a-geek.com/flexwiki/default.aspx/Regulator.HomePage");
			}
			catch(Exception )
			{
			    
			}
		}

		private void cmdWorkspace_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{
				Process.Start(@"http://regulator.sourceforge.net");
			}
			catch(Exception )
			{
			    
			}
		}



		private void OnActiveDocumentChanged(object sender, System.EventArgs e)
		{
			RefreshForDifferentDocument();
		}

		private void RefreshForDifferentDocument()
		{
			try
			{

				ShowCurrentDocumentProperties();
			}
			catch(Exception )
			{
			    
			}
	
		}



		private void PluginMenuItem_Click(object sender, ClickEventArgs e)
		{
			C1Command cmd = (C1Command)sender;
			IPlugin plugin = (IPlugin)cmd.UserData;

			DisplayPlugin(plugin);
		}


		private void cmd_Click(object sender, ClickEventArgs e)
		{
			C1Command cmd = (C1Command)sender;
			string filename = (string)cmd.UserData;
			OpenProject(filename);
		}

		private void cmdSaveAs_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			SaveCurrentProject(true);
		}

		private void cmdDonate_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			AboutDialog.ShowGiftRequest();
		}

		private void cmdNewDocument_Click(object sender, ClickEventArgs e)
		{
			NewProject();
		}

		private void cmdSplit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			GetSelectedDocument().StartSplitsThread();
		}

		private void PaneDisplay_BeforeLoadLayout(object sender, EventArgs e)
		{
			//We'll be loading any docked plugins here
			//so that the loaded plugins will be layout along with the other controls
			//this allows us to save the layout of the plugins as well
			LoadPlugins();
		}

		private void cmdViewWebSearch_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
		
		}

		private void cmdWhatsNew_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			string TopicName = "What's new";
			ShowHelpForTopic(TopicName);
//			try
//			{
//				Process.Start(AppContext.Instance.Settings.BaseDir+ @"\history.txt");
//			}
//			catch(Exception ee)
//			{
//				MessageBox.Show(ee.Message);
//			}
		}

		private void ShowHelpForTopic(string TopicName)
		{
			Help.ShowHelp(this,help.HelpNamespace,HelpNavigator.KeywordIndex,TopicName);
		}

		private void cmdQuickMenuEditor_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
		}

		private void MainForm_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
		{
			//Help.ShowHelp(this,"Regulator2Help.chm",HelpNavigator.KeywordIndex,"Introduction");
		}

		private void cmdHelpUsing_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			ShowHelpForTopic("Introduction");
		}

		private void cmdHelpKeyboard_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			ShowHelpForTopic("Shortcuts");
		
		}
	}
}
