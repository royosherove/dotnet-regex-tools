using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using C1.Win.C1Command;
using Regulator.Ctl;
using Regulator.GUI.Util;
using Regulator.SDK;
using Regulator.SDK.Plugins;
using Regulator.SDK.Proxy;
using ThreadState = System.Threading.ThreadState;

namespace Regulator.GUI
{
	/// <summary>
	/// Summary description for RegexDocument.
	/// </summary>
	public class RegexDocument : Form,IRegexDisplay
	{
		private bool m_booLastCloseWasCanceled;


		private RegexOptions TheOptions=RegexOptions.None;
		private RegexProject _currentProject=new RegexProject();
		private Thread _executingRegexThread = null;
		private C1Command cmdMultiline;
		private C1Command cmdSingleLine;
		private C1Command cmdMatch;
		private C1Command cmdIgnoreCase;
		private C1Command cmdExplicitCapture;
		private Panel panel1;
		private TabControl tabSctInputPart;
		private Splitter splitter2;
		private TabPage tabMatches;
		private TreeView Tree;
		private TabPage tabOutput;
		private RichTextBox txtOutputReplace;
		private C1Command cmdCancelAction;
		private C1Command cmdECMAScript;
		private C1Command cmdRightToLeft;
		private C1Command cmdIgnoreWS;
		private Panel panel3;
		private Panel panel2;
		private Splitter splitter1;
		private C1Command cmdReplace;
		private C1CommandHolder c1CommandHolder1;
		private IContainer components;
		private TabControl tabControl1;
		private ContextMenu QuickRegexContexMenu;
		private MenuItem mnuNewAdd;
		private ImageList imglstTree;

		private bool m_dirty=false;
		private const int ICON_MATCH= 0;
		private const int ICON_GROUP= 1;
		private TabPage tabSplits;
		private RichTextBox txtInput;
		private ToolTip Tooltip;
		private OpenFileDialog dlgInputFileSelection;
		private C1ToolBar c1ToolBar1;
		private C1CommandLink c1CommandLink1;
		private C1CommandControl mnuInputFile;
		private TextBox txtInputFilename;
		private C1CommandLink c1CommandLink2;
		private C1Command cmdmnuSelectInputFile;
		private C1CommandLink c1CommandLink3;
		private C1Command cmdmnuRefreshFromInputFile;
		private ListView lvSplits;
		private ColumnHeader Index;
		private ColumnHeader text;
		private TabPage tabInputText;
		private TabPage tabReplaceWith;
		private const int ICON_CAPTURE= 2;
		private RegexEditor txtRegex;
		private RegexEditor txtReplaceWith;
		private RegexActionTypes _currentAction = RegexActionTypes.None;

		
		public bool Dirty
		{
			get
			{
				return m_dirty;
			}
			set
			{
				if(!value)
				{
					_closeVerified=false;
				
				}
				m_dirty=value;
				txtRegex.Dirty=value;
				SetTitleDirtySignal();
			}
		}

		private void SetTitleDirtySignal()
		{
			try
			{
				if (Dirty)
				{
					if(!Text.StartsWith("*"))
					{
						Text="*" + Text;
					}
				}
				else
				{
					if(Text.StartsWith("*"))
					{
						Text=Text.TrimStart('*');
					}
				}
			}
			catch(Exception )
			{
			    
			}
		}
		



		private delegate void StdDelegate();
		private delegate void StdDelegatePerf(TimeSpan t);
		private delegate void StdDelegatePerfActions(ActionType action,double seconds);
		
		private delegate int StdIntDelegate();
		private delegate int StdAddNodeDelegate(string caption,Match match,int iconIndex);
		private delegate void StdAddSubNodeDelegateGroup(int parentIndex,string caption,Group matchGroup,int groupIndex);
		private delegate void StdAddSubNodeDelegateCapture(int parentIndex,string caption,Capture captureMatch,int groupIndex,int captureIndex);
		private delegate void StdDelegateString(string text);

		public event EventHandler RegexProcessingStarted;
		public event FinishDelegate RegexProcessingFinished;

		[Obsolete("RegexDocument does not implement this method",true)]
		public void DisplayPlugin(IPlugin plugin)
		{
			
		}

		public void StartSplitsThread()
		{
			_currentProject.TriggerActionStart(RegexActionTypes.Split);
			try
			{
				StartThreadIfPossible(new ThreadStart(RunSplits));
			}
			catch(Exception )
			{
			    
			}
		}

		private void RunSplits()
		{
			Regex r;
			string[] found =null;

			_currentAction= RegexActionTypes.Split;
			PrepareToShowRegexMatches();
			PrepareToRunMatch();
			EnableRunButtonsOnDocument(false);
			if((r=MakeRegex())==null)
			{
				this.Cursor=Cursors.Default;
				TriggerFinishCallback(this,RegexActionTypes.Split);
				RegexProject.TriggerActionEnd(RegexActionTypes.Split,null);
				
				return;
			}


			// Store the results in the text box
			string text = txtInput.Text;
			TimeCounter timer = new TimeCounter();
			timer.Start();
			found = r.Split(text);

			timer.Stop();
				
			//----------------
			FillListWithSplits(found);
			ShowNumberOfCapturesInStatus(ActionType.Splits,timer.DurationSeconds);

			//DisplayPerformance(ActionType.Matches,timer.DurationSeconds);
			EnableRunButtonsOnDocument(true);
			TriggerFinishCallback(this,RegexActionTypes.Split);
			RegexProject.TriggerActionEnd(RegexActionTypes.Split,found);

		}

		private delegate void FillSplitsDelegate(string[] splits);
		private void FillListWithSplits(string[] splits)
		{
			if(InvokeRequired)
			{
				Invoke(new FillSplitsDelegate(FillListWithSplits),new object[]{splits});
				return ;
			}

			lvSplits.Items.Clear();
			for (int i = 0; i < splits.Length; i++)
			{
				AddSplitToListview(splits[i],i);
			}
		}

		private void AddSplitToListview(string text,int index)
		{
			ListViewItem li = new ListViewItem(new string[]{index.ToString(),text});
			lvSplits.Items.Add(li);
		}

		public RegexDocument()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			InitRegex();

			
		}

		private void InitRegex()
		{
			txtRegex.Dock= DockStyle.Fill;
			txtRegex.Updated+=new EventHandler(txtRegex_Updated);
			AppContext.Instance.Settings.SettingsChanged+=new EventHandler(Settings_SettingsChanged);
			RefreshSettings();
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			try
			{
				
			
				switch (keyData) 
				{

			#region Control focus keys: Ctrl+number
						
					case Keys.Control | Keys.D1:
						SetFocusOn(ControlFocus.RegexText);
						return true;
					case Keys.Control | Keys.D2:
						SetFocusOn(ControlFocus.InputText);
						return true;
					case Keys.Control | Keys.D3:
						SetFocusOn(ControlFocus.ReplaceWithText);
						return true;
					case Keys.Control | Keys.D4:
						SetFocusOn(ControlFocus.MatchesTree);
						return true;
					case Keys.Control | Keys.D5:
						SetFocusOn(ControlFocus.ReplaceOutput);
						return true;
					case Keys.Control | Keys.D6:
						SetFocusOn(ControlFocus.SplitsOutput);
						return true;


					case Keys.Control | Keys.NumPad1:
						SetFocusOn(ControlFocus.RegexText);
						return true;
					case Keys.Control | Keys.NumPad2:
						SetFocusOn(ControlFocus.InputText);
						return true;
					case Keys.Control | Keys.NumPad3:
						SetFocusOn(ControlFocus.ReplaceWithText);
						return true;
					case Keys.Control | Keys.NumPad4:
						SetFocusOn(ControlFocus.MatchesTree);
						return true;
					case Keys.Control | Keys.NumPad5:
						SetFocusOn(ControlFocus.ReplaceOutput);
						return true;
					case Keys.Control | Keys.NumPad6:
						SetFocusOn(ControlFocus.SplitsOutput);
						return true;
#endregion //Control focus keys: Ctrl+number

						

					case Keys.F5:
						StartMatchThread();
						break;

					case Keys.F6:
						StartReplaceThread();

						break;

					case Keys.F7:
						StartSplitsThread();

						break;

					case Keys.Control | Keys.S:
						Save();
						break;

					case Keys.Control | Keys.F4:
						if(!CloseVerified &&
							VerifyCanClose())
						{
							Close();
						}
						break;

					case Keys.Control | Keys.Shift | Keys.F:
						RaiseSearchPressedEvent();
						break;

					case Keys.Escape:
						CancelRunningThread();
						break;


					default:
						;
						break;
				}
				return base.ProcessDialogKey (keyData);
			}
			catch(Exception )
			{
				return false;	    
			}
		}

		public event EventHandler CloseRequested;
		private void RaiseCloseRequestedEvent()
		{
			if(CloseRequested!=null)
			{
				CloseRequested(this,new EventArgs());
			}
			
		}

		public event EventHandler SearchWebPressed;
		private void RaiseSearchPressedEvent()
		{
			if(SearchWebPressed!=null)
			{
				SearchWebPressed(this,new EventArgs());
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RegexDocument));
			this.cmdMultiline = new C1.Win.C1Command.C1Command();
			this.cmdSingleLine = new C1.Win.C1Command.C1Command();
			this.cmdMatch = new C1.Win.C1Command.C1Command();
			this.cmdIgnoreCase = new C1.Win.C1Command.C1Command();
			this.cmdExplicitCapture = new C1.Win.C1Command.C1Command();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabSctInputPart = new System.Windows.Forms.TabControl();
			this.tabInputText = new System.Windows.Forms.TabPage();
			this.c1ToolBar1 = new C1.Win.C1Command.C1ToolBar();
			this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
			this.cmdReplace = new C1.Win.C1Command.C1Command();
			this.cmdCancelAction = new C1.Win.C1Command.C1Command();
			this.cmdECMAScript = new C1.Win.C1Command.C1Command();
			this.cmdRightToLeft = new C1.Win.C1Command.C1Command();
			this.cmdIgnoreWS = new C1.Win.C1Command.C1Command();
			this.mnuInputFile = new C1.Win.C1Command.C1CommandControl();
			this.txtInputFilename = new System.Windows.Forms.TextBox();
			this.cmdmnuSelectInputFile = new C1.Win.C1Command.C1Command();
			this.cmdmnuRefreshFromInputFile = new C1.Win.C1Command.C1Command();
			this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
			this.txtInput = new System.Windows.Forms.RichTextBox();
			this.tabReplaceWith = new System.Windows.Forms.TabPage();
			this.txtReplaceWith = new Regulator.Ctl.RegexEditor();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabMatches = new System.Windows.Forms.TabPage();
			this.Tree = new System.Windows.Forms.TreeView();
			this.imglstTree = new System.Windows.Forms.ImageList(this.components);
			this.tabOutput = new System.Windows.Forms.TabPage();
			this.txtOutputReplace = new System.Windows.Forms.RichTextBox();
			this.tabSplits = new System.Windows.Forms.TabPage();
			this.lvSplits = new System.Windows.Forms.ListView();
			this.Index = new System.Windows.Forms.ColumnHeader();
			this.text = new System.Windows.Forms.ColumnHeader();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.txtRegex = new Regulator.Ctl.RegexEditor();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.QuickRegexContexMenu = new System.Windows.Forms.ContextMenu();
			this.mnuNewAdd = new System.Windows.Forms.MenuItem();
			this.Tooltip = new System.Windows.Forms.ToolTip(this.components);
			this.dlgInputFileSelection = new System.Windows.Forms.OpenFileDialog();
			this.panel1.SuspendLayout();
			this.tabSctInputPart.SuspendLayout();
			this.tabInputText.SuspendLayout();
			this.c1ToolBar1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
			this.tabReplaceWith.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabMatches.SuspendLayout();
			this.tabOutput.SuspendLayout();
			this.tabSplits.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdMultiline
			// 
			this.cmdMultiline.CheckAutoToggle = true;
			this.cmdMultiline.Image = ((System.Drawing.Image)(resources.GetObject("cmdMultiline.Image")));
			this.cmdMultiline.Name = "cmdMultiline";
			this.cmdMultiline.Text = "Multiline";
			// 
			// cmdSingleLine
			// 
			this.cmdSingleLine.CheckAutoToggle = true;
			this.cmdSingleLine.Image = ((System.Drawing.Image)(resources.GetObject("cmdSingleLine.Image")));
			this.cmdSingleLine.Name = "cmdSingleLine";
			this.cmdSingleLine.Text = "Single line";
			// 
			// cmdMatch
			// 
			this.cmdMatch.Image = ((System.Drawing.Image)(resources.GetObject("cmdMatch.Image")));
			this.cmdMatch.Name = "cmdMatch";
			this.cmdMatch.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
			this.cmdMatch.Text = "Match";
			// 
			// cmdIgnoreCase
			// 
			this.cmdIgnoreCase.CheckAutoToggle = true;
			this.cmdIgnoreCase.Image = ((System.Drawing.Image)(resources.GetObject("cmdIgnoreCase.Image")));
			this.cmdIgnoreCase.Name = "cmdIgnoreCase";
			this.cmdIgnoreCase.Text = "seperator";
			// 
			// cmdExplicitCapture
			// 
			this.cmdExplicitCapture.CheckAutoToggle = true;
			this.cmdExplicitCapture.Image = ((System.Drawing.Image)(resources.GetObject("cmdExplicitCapture.Image")));
			this.cmdExplicitCapture.Name = "cmdExplicitCapture";
			this.cmdExplicitCapture.Text = "Explicit capture";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tabSctInputPart);
			this.panel1.Controls.Add(this.splitter2);
			this.panel1.Controls.Add(this.tabControl1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 282);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(552, 171);
			this.panel1.TabIndex = 6;
			// 
			// tabSctInputPart
			// 
			this.tabSctInputPart.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabSctInputPart.Controls.Add(this.tabInputText);
			this.tabSctInputPart.Controls.Add(this.tabReplaceWith);
			this.tabSctInputPart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabSctInputPart.Location = new System.Drawing.Point(272, 0);
			this.tabSctInputPart.Name = "tabSctInputPart";
			this.tabSctInputPart.SelectedIndex = 0;
			this.tabSctInputPart.Size = new System.Drawing.Size(280, 171);
			this.tabSctInputPart.TabIndex = 0;
			this.tabSctInputPart.Enter += new System.EventHandler(this.tabSctInputPart_Enter);
			// 
			// tabInputText
			// 
			this.tabInputText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tabInputText.Controls.Add(this.c1ToolBar1);
			this.tabInputText.Controls.Add(this.txtInput);
			this.tabInputText.Location = new System.Drawing.Point(4, 25);
			this.tabInputText.Name = "tabInputText";
			this.tabInputText.Size = new System.Drawing.Size(272, 142);
			this.tabInputText.TabIndex = 0;
			this.tabInputText.Text = "Input";
			// 
			// c1ToolBar1
			// 
			this.c1ToolBar1.AutoSize = false;
			this.c1ToolBar1.CommandHolder = this.c1CommandHolder1;
			this.c1ToolBar1.CommandLinks.Add(this.c1CommandLink1);
			this.c1ToolBar1.CommandLinks.Add(this.c1CommandLink2);
			this.c1ToolBar1.CommandLinks.Add(this.c1CommandLink3);
			this.c1ToolBar1.Controls.Add(this.txtInputFilename);
			this.c1ToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.c1ToolBar1.Location = new System.Drawing.Point(0, 0);
			this.c1ToolBar1.Name = "c1ToolBar1";
			this.c1ToolBar1.Size = new System.Drawing.Size(270, 24);
			this.c1ToolBar1.Text = "c1ToolBar1";
			this.c1ToolBar1.Click += new System.EventHandler(this.c1ToolBar1_Click);
			this.c1ToolBar1.Enter += new System.EventHandler(this.c1ToolBar1_Enter);
			// 
			// c1CommandHolder1
			// 
			this.c1CommandHolder1.Commands.Add(this.cmdReplace);
			this.c1CommandHolder1.Commands.Add(this.cmdMatch);
			this.c1CommandHolder1.Commands.Add(this.cmdCancelAction);
			this.c1CommandHolder1.Commands.Add(this.cmdIgnoreCase);
			this.c1CommandHolder1.Commands.Add(this.cmdMultiline);
			this.c1CommandHolder1.Commands.Add(this.cmdSingleLine);
			this.c1CommandHolder1.Commands.Add(this.cmdExplicitCapture);
			this.c1CommandHolder1.Commands.Add(this.cmdECMAScript);
			this.c1CommandHolder1.Commands.Add(this.cmdRightToLeft);
			this.c1CommandHolder1.Commands.Add(this.cmdIgnoreWS);
			this.c1CommandHolder1.Commands.Add(this.mnuInputFile);
			this.c1CommandHolder1.Commands.Add(this.cmdmnuSelectInputFile);
			this.c1CommandHolder1.Commands.Add(this.cmdmnuRefreshFromInputFile);
			this.c1CommandHolder1.Owner = this;
			// 
			// cmdReplace
			// 
			this.cmdReplace.Image = ((System.Drawing.Image)(resources.GetObject("cmdReplace.Image")));
			this.cmdReplace.Name = "cmdReplace";
			this.cmdReplace.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
			this.cmdReplace.Text = "Replace";
			// 
			// cmdCancelAction
			// 
			this.cmdCancelAction.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancelAction.Image")));
			this.cmdCancelAction.Name = "cmdCancelAction";
			this.cmdCancelAction.Shortcut = System.Windows.Forms.Shortcut.F9;
			this.cmdCancelAction.Text = "&Cancel action";
			// 
			// cmdECMAScript
			// 
			this.cmdECMAScript.CheckAutoToggle = true;
			this.cmdECMAScript.Image = ((System.Drawing.Image)(resources.GetObject("cmdECMAScript.Image")));
			this.cmdECMAScript.Name = "cmdECMAScript";
			this.cmdECMAScript.Text = "ECMA script";
			// 
			// cmdRightToLeft
			// 
			this.cmdRightToLeft.CheckAutoToggle = true;
			this.cmdRightToLeft.Image = ((System.Drawing.Image)(resources.GetObject("cmdRightToLeft.Image")));
			this.cmdRightToLeft.Name = "cmdRightToLeft";
			this.cmdRightToLeft.Text = "Right to left";
			// 
			// cmdIgnoreWS
			// 
			this.cmdIgnoreWS.CheckAutoToggle = true;
			this.cmdIgnoreWS.Image = ((System.Drawing.Image)(resources.GetObject("cmdIgnoreWS.Image")));
			this.cmdIgnoreWS.Name = "cmdIgnoreWS";
			this.cmdIgnoreWS.Text = "Ignore pattern whitespace";
			// 
			// mnuInputFile
			// 
			this.mnuInputFile.Control = this.txtInputFilename;
			this.mnuInputFile.Name = "mnuInputFile";
			this.mnuInputFile.Text = "Input file";
			// 
			// txtInputFilename
			// 
			this.txtInputFilename.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(239)), ((System.Byte)(239)), ((System.Byte)(239)));
			this.txtInputFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtInputFilename.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtInputFilename.Location = new System.Drawing.Point(4, 2);
			this.txtInputFilename.Name = "txtInputFilename";
			this.txtInputFilename.Size = new System.Drawing.Size(204, 21);
			this.txtInputFilename.TabIndex = 1;
			this.txtInputFilename.TabStop = false;
			this.txtInputFilename.Text = "Input file";
			this.Tooltip.SetToolTip(this.txtInputFilename, "Input file for this document");
			this.txtInputFilename.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInputFilename_KeyDown);
			this.txtInputFilename.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtInputFilename_MouseDown);
			this.txtInputFilename.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInputFilename_KeyPress);
			this.txtInputFilename.TextChanged += new System.EventHandler(this.txtInputFilename_TextChanged);
			this.txtInputFilename.Enter += new System.EventHandler(this.txtInputFilename_Enter);
			// 
			// cmdmnuSelectInputFile
			// 
			this.cmdmnuSelectInputFile.Image = ((System.Drawing.Image)(resources.GetObject("cmdmnuSelectInputFile.Image")));
			this.cmdmnuSelectInputFile.Name = "cmdmnuSelectInputFile";
			this.cmdmnuSelectInputFile.ShowTextAsToolTip = false;
			this.cmdmnuSelectInputFile.Text = "Select input file...";
			this.cmdmnuSelectInputFile.ToolTipText = "Select input file...";
			this.cmdmnuSelectInputFile.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdmnuSelectInputFile_Click);
			// 
			// cmdmnuRefreshFromInputFile
			// 
			this.cmdmnuRefreshFromInputFile.Image = ((System.Drawing.Image)(resources.GetObject("cmdmnuRefreshFromInputFile.Image")));
			this.cmdmnuRefreshFromInputFile.Name = "cmdmnuRefreshFromInputFile";
			this.cmdmnuRefreshFromInputFile.Text = "Refresh current input from input file";
			this.cmdmnuRefreshFromInputFile.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdmnuRefreshFromInputFile_Click);
			// 
			// c1CommandLink1
			// 
			this.c1CommandLink1.Command = this.mnuInputFile;
			// 
			// c1CommandLink2
			// 
			this.c1CommandLink2.Command = this.cmdmnuSelectInputFile;
			// 
			// c1CommandLink3
			// 
			this.c1CommandLink3.Command = this.cmdmnuRefreshFromInputFile;
			// 
			// txtInput
			// 
			this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtInput.DetectUrls = false;
			this.txtInput.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtInput.HideSelection = false;
			this.txtInput.Location = new System.Drawing.Point(0, 24);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(272, 120);
			this.txtInput.TabIndex = 1;
			this.txtInput.Text = "Input text";
			this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
			// 
			// tabReplaceWith
			// 
			this.tabReplaceWith.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tabReplaceWith.Controls.Add(this.txtReplaceWith);
			this.tabReplaceWith.Location = new System.Drawing.Point(4, 25);
			this.tabReplaceWith.Name = "tabReplaceWith";
			this.tabReplaceWith.Size = new System.Drawing.Size(272, 142);
			this.tabReplaceWith.TabIndex = 1;
			this.tabReplaceWith.Text = "Replace with";
			this.tabReplaceWith.Visible = false;
			// 
			// txtReplaceWith
			// 
			this.txtReplaceWith.Dirty = false;
			this.txtReplaceWith.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtReplaceWith.Location = new System.Drawing.Point(0, 0);
			this.txtReplaceWith.Name = "txtReplaceWith";
			this.txtReplaceWith.NamedGroupsMode = true;
			this.txtReplaceWith.ReadOnly = false;
			this.txtReplaceWith.Size = new System.Drawing.Size(270, 140);
			this.txtReplaceWith.TabIndex = 2;
			this.txtReplaceWith.Updated += new System.EventHandler(this.txtReplaceWith_Updated);
			// 
			// splitter2
			// 
			this.splitter2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.splitter2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitter2.Location = new System.Drawing.Point(264, 0);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(8, 171);
			this.splitter2.TabIndex = 1;
			this.splitter2.TabStop = false;
			// 
			// tabControl1
			// 
			this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl1.Controls.Add(this.tabMatches);
			this.tabControl1.Controls.Add(this.tabOutput);
			this.tabControl1.Controls.Add(this.tabSplits);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(264, 171);
			this.tabControl1.TabIndex = 1;
			// 
			// tabMatches
			// 
			this.tabMatches.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.tabMatches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tabMatches.Controls.Add(this.Tree);
			this.tabMatches.Location = new System.Drawing.Point(4, 25);
			this.tabMatches.Name = "tabMatches";
			this.tabMatches.Size = new System.Drawing.Size(256, 142);
			this.tabMatches.TabIndex = 0;
			this.tabMatches.Text = "Matches";
			// 
			// Tree
			// 
			this.Tree.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Tree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Tree.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.Tree.ImageList = this.imglstTree;
			this.Tree.Location = new System.Drawing.Point(0, 0);
			this.Tree.Name = "Tree";
			this.Tree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																			 new System.Windows.Forms.TreeNode("Matches")});
			this.Tree.Size = new System.Drawing.Size(254, 140);
			this.Tree.TabIndex = 0;
			this.Tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Tree_AfterSelect);
			// 
			// imglstTree
			// 
			this.imglstTree.ImageSize = new System.Drawing.Size(16, 16);
			this.imglstTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstTree.ImageStream")));
			this.imglstTree.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tabOutput
			// 
			this.tabOutput.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.tabOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tabOutput.Controls.Add(this.txtOutputReplace);
			this.tabOutput.Location = new System.Drawing.Point(4, 25);
			this.tabOutput.Name = "tabOutput";
			this.tabOutput.Size = new System.Drawing.Size(256, 142);
			this.tabOutput.TabIndex = 1;
			this.tabOutput.Text = "Replace output";
			this.tabOutput.Visible = false;
			// 
			// txtOutputReplace
			// 
			this.txtOutputReplace.BackColor = System.Drawing.Color.White;
			this.txtOutputReplace.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtOutputReplace.DetectUrls = false;
			this.txtOutputReplace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOutputReplace.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtOutputReplace.Location = new System.Drawing.Point(0, 0);
			this.txtOutputReplace.Name = "txtOutputReplace";
			this.txtOutputReplace.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.txtOutputReplace.Size = new System.Drawing.Size(254, 140);
			this.txtOutputReplace.TabIndex = 0;
			this.txtOutputReplace.Text = "Replace output";
			// 
			// tabSplits
			// 
			this.tabSplits.Controls.Add(this.lvSplits);
			this.tabSplits.Location = new System.Drawing.Point(4, 25);
			this.tabSplits.Name = "tabSplits";
			this.tabSplits.Size = new System.Drawing.Size(256, 142);
			this.tabSplits.TabIndex = 2;
			this.tabSplits.Text = "Splits";
			// 
			// lvSplits
			// 
			this.lvSplits.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(231)), ((System.Byte)(245)), ((System.Byte)(252)));
			this.lvSplits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lvSplits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.Index,
																					   this.text});
			this.lvSplits.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvSplits.FullRowSelect = true;
			this.lvSplits.GridLines = true;
			this.lvSplits.Location = new System.Drawing.Point(0, 0);
			this.lvSplits.Name = "lvSplits";
			this.lvSplits.Size = new System.Drawing.Size(256, 142);
			this.lvSplits.TabIndex = 0;
			this.lvSplits.View = System.Windows.Forms.View.Details;
			// 
			// Index
			// 
			this.Index.Text = "index";
			this.Index.Width = 44;
			// 
			// text
			// 
			this.text.Text = "text";
			this.text.Width = 204;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.panel2);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(552, 276);
			this.panel3.TabIndex = 7;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.txtRegex);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(552, 276);
			this.panel2.TabIndex = 0;
			this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint_1);
			// 
			// txtRegex
			// 
			this.txtRegex.Dirty = false;
			this.txtRegex.Location = new System.Drawing.Point(24, 8);
			this.txtRegex.Name = "txtRegex";
			this.txtRegex.NamedGroupsMode = false;
			this.txtRegex.ReadOnly = false;
			this.txtRegex.Size = new System.Drawing.Size(424, 184);
			this.txtRegex.TabIndex = 1;
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 276);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(552, 6);
			this.splitter1.TabIndex = 8;
			this.splitter1.TabStop = false;
			// 
			// QuickRegexContexMenu
			// 
			this.QuickRegexContexMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								 this.mnuNewAdd});
			// 
			// mnuNewAdd
			// 
			this.mnuNewAdd.Index = 0;
			this.mnuNewAdd.Text = "test new add";
			// 
			// dlgInputFileSelection
			// 
			this.dlgInputFileSelection.Filter = "*.*|*.*";
			// 
			// RegexDocument
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(552, 453);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RegexDocument";
			this.Text = "RegexDocument";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.RegexDocument_Closing);
			this.Load += new System.EventHandler(this.RegexDocument_Load);
			this.Activated += new System.EventHandler(this.RegexDocument_Activated);
			this.panel1.ResumeLayout(false);
			this.tabSctInputPart.ResumeLayout(false);
			this.tabInputText.ResumeLayout(false);
			this.c1ToolBar1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
			this.tabReplaceWith.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabMatches.ResumeLayout(false);
			this.tabOutput.ResumeLayout(false);
			this.tabSplits.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		


		private void RegexDocument_Load(object sender, EventArgs e)
		{
			EnableRunButtonsOnDocument(true);
			Dirty=false;
			
		}


	
		



		private void OnAddItemShortcutClick(object sender,EventArgs e)
		{

			if(this.MdiParent.ActiveMdiChild==null)
			{
				//this is not the active document
				return ;
			}

			if(this.MdiParent.ActiveMdiChild.GetHashCode()!=this.GetHashCode())
			{
				//this is not the active document
				return ;
			}

			MenuItem menuItem = (MenuItem) sender;

			Regex regexBreak = new Regex(".+ - (?<Placeholder>.+)");
			Match match = regexBreak.Match(menuItem.Text);
			if (match.Success)
			{
				string insert = match.Groups["Placeholder"].ToString();
				AppContext.Instance.Display.InsertTextIntoCurrentRegex(insert);
			}

		}

		
		

		private void AttachToAddItemMenuEvents(MenuItem parentMenu,EventHandler handler)
		{
			foreach (MenuItem item in parentMenu.MenuItems)
			{
				if(item.MenuItems.Count==0)
				{
					item.Click+=handler;
					string name = item.Text;
				}
				else
				{
					AttachToAddItemMenuEvents(item,handler);
				}
			}
		}
	



		#region matching logic

		public delegate void FinishDelegate(RegexDocument sender, RegexActionTypes action);
		private void TriggerFinishCallback(RegexDocument sender, RegexActionTypes action)
		{
			if(InvokeRequired)
			{
				Invoke(new FinishDelegate(TriggerFinishCallback),new object[]{this,action});
				return ;
			}

			_currentAction= RegexActionTypes.None;
			if(RegexProcessingFinished!=null)
			{
				RegexProcessingFinished(sender,action);
			}
		}


		private void TriggerStartedCallback()
		{
			if(InvokeRequired)
			{
				Invoke(new StdDelegate(TriggerStartedCallback));
				return ;
			}

			if(RegexProcessingStarted!=null)
			{
				RegexProcessingStarted(this,null);
			}

		}


		

				/// <summary>
		/// Look for matches for the regular expression
		/// </summary>
//			private void RunMatchBAD()
//			{
//				try
//				{
//					Regex r;
//					Match m;
//
//					PrepareToShowRegexMatches();
//					PrepareToRunMatch();
//					if((r=MakeRegex())==null)
//					{
//						this.Cursor=Cursors.Default;
//						TriggerFinishCallback();
//						return;
//					}
//
//
//					// Store the results in the text box
//					string text = txtInput.Text;
//					long startTicks = DateTime.Now.Ticks;
//					long timeTook = 0;
//					long totalTimeTook=0;
//
//					for (m = r.Match(text); m.Success; m = m.NextMatch()) 
//					{
//						long EndTicks = DateTime.Now.Ticks;
//						timeTook= (EndTicks-startTicks);
//						totalTimeTook+=timeTook;
//						if(m.Value.Length>0)
//						{
//				
//							int ThisNode=	AddTreeNode("["+m.Value + "]",m);
//					
//							if(m.Groups.Count>1)
//							{
//								for (int i=1;i<m.Groups.Count;i++)
//								{
//									string SubNodeText = r.GroupNameFromNumber(i)+": ["+m.Groups[i].Value+"]";
//									AddSubNode(ThisNode,SubNodeText,m.Groups[i],i);
//							
//									//This bit of code puts in another level of nodes showing the captures for each group
//									int Number=m.Groups[i].Captures.Count;
//									if(Number>1)
//										for(int j=0;j<Number;j++)
//										{
//											AddSubNode(ThisNode,m.Groups[i].Captures[j].Value,m.Groups[i].Captures[j],i,j);
//										}
//								}
//							}
//						}
//						//reset the counting so we know how long the next loop took
//						startTicks = DateTime.Now.Ticks;
//					}
//					DisplayPerformance(0,totalTimeTook);
//				}
			
//				catch(Exception e)
//				{
//					SetResultText(e.Message);
//				}
//				finally
//				{
//					EnableRunButtons(true);
//					TriggerFinishCallback();
//
//				}
//			}
//
		private void ClearTreeNodes()
		{
			if(InvokeRequired)
			{
				Invoke(new StdDelegate(ClearTreeNodes));
				return ;
			} 

			Tree.Nodes.Clear();
		}

		/// <summary>
		/// Look for matches for the regular expression
		/// </summary>
		private void RunMatch()
		{
			
			MatchCollection  found =null;
			try
			{
				Regex r;
				_currentAction= RegexActionTypes.Match;
				PrepareToShowRegexMatches();
				PrepareToRunMatch();

				if((r=MakeRegex())==null)
				{
					this.Cursor=Cursors.Default;
					TriggerFinishCallback(this,RegexActionTypes.Match);
					RegexProject.TriggerActionEnd(RegexActionTypes.Match,null);
					return;
				}


				// Store the results in the text box
				string text = txtInput.Text;
				TimeCounter timer = new TimeCounter();
				timer.Start();
				found = r.Matches(text);
				timer.Stop();
				
				//----------------
				ShowNumberOfCapturesInStatus(ActionType.Matches,timer.DurationSeconds);
				

				FillTreeWithMatches(found,r);

				DisplayPerformance(ActionType.Matches,timer.DurationSeconds);
				EnableRunButtonsOnDocument(true);
				
			}
			catch(Exception e)
			{
				SetErrorText(e.Message);
			}
			finally
			{
				EnableRunButtonsOnDocument(true);
				TriggerFinishCallback(this,RegexActionTypes.Match);
				RegexProject.TriggerActionEnd(RegexActionTypes.Match,found);

				;

			}
		}
		
		private void FillTreeWithMatches(MatchCollection found,Regex CreatingRegexObject)
		{
			ClearTreeNodes();
			foreach (Match m  in found)
			{
					
			
				if(m.Value.Length>0)
				{
				
					int ThisNode=	AddTreeNode("["+m.Value + "]",m,ICON_GROUP);
					
					if(m.Groups.Count>1)
					{
						for (int i=1;i<m.Groups.Count;i++)
						{
							string SubNodeText = CreatingRegexObject.GroupNameFromNumber(i)+": ["+m.Groups[i].Value+"]";
							AddSubNode(ThisNode,SubNodeText,m.Groups[i],i);
							
							//This bit of code puts in another level of nodes showing the captures for each group
							int Number=m.Groups[i].Captures.Count;
							if(Number>1 && AppContext.Instance.Settings.FillUnNamedCapturesInTree)
							{
								for(int j=0;j<Number;j++)
								{
									AddSubNode(ThisNode,m.Groups[i].Captures[j].Value,m.Groups[i].Captures[j],i,j);
								}
							}
						}
					}
				}
			}

			SelectTreeFirstNode();
		}

		private void SelectTreeFirstNode()
		{
			if(InvokeRequired)
			{
				Invoke(new StdDelegate(SelectTreeFirstNode));
				return ;
			} 

			try
			{
//				Tree.Nodes[0].EnsureVisible();
				
//				Tree.Scrollable=false;
//				Tree.Scrollable=true;
//				Tree.Focus();

			}
			catch(Exception)
			{
				   
			}
		}

		private void DisplayPerformance(ActionType action,double timeTook)
		{
			if(InvokeRequired)
			{
				Invoke(new StdDelegatePerfActions(DisplayPerformance),new object[]{action,timeTook});
				return ;
			}

			ShowNumberOfCapturesInStatus(action,timeTook);
		}



		private enum ActionType
		{
			Replaces,
			Matches,
			Splits
		}
		private void ShowNumberOfCapturesInStatus(ActionType action,double timeTook)
		{
			if(InvokeRequired)
			{
				Invoke(new StdDelegatePerfActions(ShowNumberOfCapturesInStatus),new object[]{action,timeTook});
				return ;
			}

			string perfCount = "";
			perfCount = (timeTook).ToString("###0.0#####") + " sec)";
			
			switch (action) 
			{
				case ActionType.Matches:
					tabMatches.Text="Matches (" + Tree.Nodes.Count.ToString()+", " +perfCount;

					break;
				case ActionType.Replaces:
					tabOutput.Text= "Output (" +perfCount;

					break;

				case ActionType.Splits:
					tabSplits.Text="Splits (" + lvSplits.Items.Count.ToString()+", " +perfCount;
					tabControl1.SelectedTab=tabSplits;

					break;

				default:
					;
					break;
			}

		}

		private void ResetStatusBar()
		{
			if(InvokeRequired)
			{
				Invoke(new StdDelegate(ResetStatusBar));
				return ;
			}
//			statusBar.Panels[0].Text="";
//			statusBar.Panels[1].Text="";
//			statusBar.Panels[2].Text="";

		}

		private void PrepareToRunMatch()
		{
			if(InvokeRequired)
			{
				Invoke(new StdDelegate(PrepareToRunMatch));
				return ;
			}
			txtOutputReplace.ForeColor=Color.Black;
			ResetStatusBar();
			
		}

		private void PrepareToShowRegexMatches()
		{
			if(InvokeRequired)
			{
				Invoke(new StdDelegate(PrepareToShowRegexMatches));
				return ;
			}
			tabControl1.SelectedTab= tabMatches;
			txtOutputReplace.ForeColor=Color.Black;
			

		}


		private int AddTreeNode(string nodeText,Match currentMatchForNode,int iconIndex)
		{
			if(InvokeRequired)
			{
				
				return 
					(int)Invoke(new StdAddNodeDelegate(AddTreeNode),new object[]{nodeText,currentMatchForNode,iconIndex});;
			}
			
			TreeNode t = new TreeNode(nodeText,iconIndex,iconIndex);
			Tree.Nodes.Add(t);
			int ThisNode=Tree.Nodes.Count-1;
			Tree.Nodes[ThisNode].Tag=currentMatchForNode;
			
			return ThisNode;

		}

		private void AddSubNode(int parentNodeIndex,string caption,Group captureGroup,int groupIndex)
		{
			if(InvokeRequired)
			{
				Invoke(new StdAddSubNodeDelegateGroup(AddSubNode),new object[]{parentNodeIndex,caption,captureGroup,groupIndex});
				return ;
			}

			
			Tree.Nodes[parentNodeIndex].Nodes.Add(new TreeNode(caption,ICON_CAPTURE,ICON_CAPTURE));
			Tree.Nodes[parentNodeIndex].Nodes[groupIndex-1].Tag=captureGroup;
			Tree.Nodes[parentNodeIndex].Expand();
		}

		private void AddSubNode(int parentNodeIndex,string caption,Capture matchCapture,int groupIndex,int captureIndex)
		{
			if(InvokeRequired)
			{
				Invoke(new StdAddSubNodeDelegateCapture(AddSubNode),
					new object[]{parentNodeIndex,
									caption,
									matchCapture,
									groupIndex,
									captureIndex});
				return ;
			}

			TreeNode node = Tree.Nodes[parentNodeIndex].Nodes[groupIndex-1];
			node.Nodes.Add(new TreeNode(caption,ICON_CAPTURE,ICON_CAPTURE));
			node.Nodes[captureIndex].Tag=matchCapture;		
			
		}
		

		private string GetRegexText()
		{

				return txtRegex.Text;
		}
		private Regex MakeRegex()
		{
			
			Regex r;
			// First set the Regex options based on the check boxes
			TheOptions=RegexProject.Options;
			if(cmdIgnoreCase.Checked)TheOptions|=RegexOptions.IgnoreCase;
			if(cmdECMAScript.Checked)TheOptions|=RegexOptions.ECMAScript;
			if(cmdExplicitCapture.Checked)TheOptions|=RegexOptions.ExplicitCapture;
			if(cmdIgnoreWS.Checked)TheOptions|=RegexOptions.IgnorePatternWhitespace;
			if(cmdMultiline.Checked)TheOptions|=RegexOptions.Multiline;
			if(cmdRightToLeft.Checked)TheOptions|=RegexOptions.RightToLeft;
			if(cmdSingleLine.Checked)TheOptions|=RegexOptions.Singleline;
			
			try
			{
				r = new Regex(GetRegexText(),TheOptions);
				return r;
			}
			catch (Exception ex)
			{
				SetErrorText(ex.Message);
				return null;
			}
		}
		#endregion

		private void cmdMatch_Click(object sender, ClickEventArgs e)
		{
		StartMatchThread();
		}

		private bool m_IgnoreProjectUpdates=false;
		private  void SetCurrentProjectProperties()
		{
			if(RegexProject.IsLoading)
			{
				return ;
			}
			m_IgnoreProjectUpdates=true;

			RegexProject.Input= txtInput.Text;
			RegexProject.Regex= txtRegex.Text;
			RegexProject.ReplaceString= txtReplaceWith.Text;
			RegexProject.InputFilename=txtInputFilename.Text;
			
			m_IgnoreProjectUpdates=false;
			
		}

		public bool Save()
		{

			try
			{
				SetCurrentProjectProperties();

				
				if (!MainForm.SaveProject(RegexProject))
				{

					return false;
				}

				_closeVerified=true;
				SetFormValuesFromProject();

				Dirty=false;
				return true;
			}
			catch(Exception )
			{
				MessageBox.Show("There was a problem saving the file");		    
				return false;

			}

		}


		

		
		/// <summary>
		/// Make the regular expression and execute a "Replace" operation
		/// </summary>
		private void RunReplace()
		{
			string replacedText =null;
			try
			{
				Regex r;

				_currentAction= RegexActionTypes.Replace;
				PrepareToRunMatch();

				if((r=MakeRegex())==null)
				{
					this.Cursor=Cursors.Default;
					TriggerFinishCallback(this,RegexActionTypes.Replace);
					RegexProject.TriggerActionEnd(RegexActionTypes.Replace,null);
					return;
				}

				PrepareToShowRegexMatches();
				TimeCounter timer = new TimeCounter();
				timer.Start();

				replacedText = r.Replace(txtInput.Text,txtReplaceWith.Text);;
				timer.Stop();
				ClearTreeNodes();
				DisplayPerformance(ActionType.Replaces,timer.DurationSeconds);
				SetResultText(replacedText);
				EnableRunButtonsOnDocument(true);
				//TODO: Trigget finished callback for replace,split and match event using control.Invoke

			}
			catch(Exception e)
			{
			    SetErrorText(e.Message);
				
			}
			finally
			{
				EnableRunButtonsOnDocument(true);
				TriggerFinishCallback(this,RegexActionTypes.Replace);
				RegexProject.TriggerActionEnd(RegexActionTypes.Replace,replacedText);
				
			}

		}

		private void SetErrorText(string error)
		{
			if(InvokeRequired)
			{
				Invoke(new StdDelegateString(SetErrorText),new object[]{error});
				return ;
			}

			tabControl1.SelectedTab= tabOutput;
			txtOutputReplace.ForeColor=Color.Red;
			txtOutputReplace.Text=error;

		}

		private void SetResultText(string text)
		{
			if(InvokeRequired)
			{
				Invoke(new StdDelegateString(SetResultText),new object[]{text});
				return ;
			}

			tabControl1.SelectedTab= tabOutput;
			txtOutputReplace.Text=text;
		}


		public void StartReplaceThread()
		{
			_currentProject.TriggerActionStart(RegexActionTypes.Replace);
			StartThreadIfPossible(new ThreadStart(RunReplace));
		}

		private void StartThreadIfPossible(ThreadStart starter)
		{
			
			EnableRunButtonsOnDocument(false);
			if(_executingRegexThread!=null && 
				_executingRegexThread.ThreadState==ThreadState.Running)
			{
				MessageBox.Show("A regex  is currently running.\n Either wait for it to end or cancel it.");
				return ;
			}

			TriggerStartedCallback();
			_executingRegexThread = new Thread(new ThreadStart(starter));
			_executingRegexThread.Start();
		}

	 
	  public void LoadFile(string fileName)
		{
			RegexProject = (RegexProject)RegexProject.Load(RegexProject.GetType(),fileName);
			RegexProject.FileName= fileName;
			AppContext.Instance.OnProjectChange(RegexProject);
			Dirty=false;
		}

		



		public RegexProject RegexProject
		{
			get
			{
				return _currentProject;
			}
			set
			{
				_currentProject= value;
				RegisterForRegexEvents();
				SetFormValuesFromProject();
			}
		}

		private void RegisterForRegexEvents()
		{
			_currentProject.Updated+=new EventHandler(currentProject_Updated);
			_currentProject.RequestMatch+=new EventHandler(currentProject_RequestMatch);
			_currentProject.RequestReplace+=new EventHandler(currentProject_RequestReplace);
			_currentProject.RequestSplit+=new EventHandler(currentProject_RequestSplit);
		}

		public void RefereshDisplay()
		{
			SetFormValuesFromProject();
		}
		private bool _closeVerified=false;
		public bool CloseVerified
		{
			get
			{
				return _closeVerified;
			}
		}

		public bool VerifyCanClose()
		{
			_closeVerified=true;
			m_booLastCloseWasCanceled=false;

			if(IsRunning)
			{
				CancelRunningThread();				
			}

			if(!Dirty )
			{
				return true;
			}
			

			string question = "The document was changed. Would you like to save the changes?";
			string title ="Closing: " + this.Text;
			MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
			MessageBoxIcon icon =MessageBoxIcon.Question;
			DialogResult result = MessageBox.Show(this,question,title,buttons,icon);

			switch (result) 
			{
				case DialogResult.Yes:
					bool success = Save();
					_closeVerified=success;
					m_booLastCloseWasCanceled=!success;
					return success;

				case DialogResult.No:
					return true;
				case DialogResult.Cancel:
					//ask next time as well
					_closeVerified=false;
					m_booLastCloseWasCanceled=true;
					return false;

				default:
					return true;
			}
		}


		private delegate void StdBoolDelegate(bool val);
		private void  EnableRunButtonsOnDocument(bool enabled)
		{
			if(InvokeRequired)
			{
				Invoke(new StdBoolDelegate(EnableRunButtonsOnDocument),new object[]{enabled});
				return ;
			}
			if(enabled)
			{
				this.Cursor= Cursors.Default;
				
				Text=RegexProject.FileNameNoPath;
			}
			else
			{
				this.Cursor= Cursors.WaitCursor;
				Text=RegexProject.FileNameNoPath + "(Running)";
				
			}
			SetTitleDirtySignal();

			txtRegex.ReadOnly=!enabled;
			txtInput.ReadOnly= !enabled;
			
		}
		public void StartMatchThread()
		{
			_currentProject.TriggerActionStart(RegexActionTypes.Match);
			StartThreadIfPossible(new ThreadStart(RunMatch));
		}
		
		public void CancelRunningThread()
		{
			if(_currentAction==RegexActionTypes.None)
			{
				return ;
			}
			if(_executingRegexThread!=null &&
				_executingRegexThread.ThreadState==ThreadState.Running)
			{
				_executingRegexThread.Abort();
			}
			Debug.Assert(_currentAction!=RegexActionTypes.None);
			TriggerFinishCallback(this,_currentAction);
			
			EnableRunButtonsOnDocument(true);
		}

		public bool IsRunning
		{
			get
			{
				return (_executingRegexThread!=null &&
					(_executingRegexThread.ThreadState==
						ThreadState.Running)
					);
			}
		}


		private void SetFormValuesFromProject()
		{
			if(_currentProject==null)
			{
				return ;
			}

			_currentProject.BeginLoad();
			txtRegex.Text= _currentProject.Regex;
			txtRegex.Invalidate(true);
			txtInput.Text= _currentProject.Input;
			txtReplaceWith.Text = _currentProject.ReplaceString;
			txtInputFilename.Text= _currentProject.InputFilename;
			Text= _currentProject.FileNameNoPath;
			_currentProject.EndLoad();
			if(Text.Length==0)
			{
				Text="New Document";
			}
			
		}

		

		private bool GetOptionState(RegexOptions enumeratedObject,RegexOptions wantedEnum)
		{
			return (enumeratedObject & wantedEnum)==wantedEnum;
		}

		private void cmdCancelAction_Click(object sender, ClickEventArgs e)
		{
			CancelRunningThread();
		}

	
	
		#region IRegexDisplay Members

		public void RefreshSettings()
		{
			txtRegex.IntellisenseEnabled = AppContext.Instance.Settings.IntelliseSenseInRegex;
		}


		public void InsertTextIntoCurrentRegex(string regex)
		{
			txtRegex.InsertTextIntoCurrentRegex(regex);
		}

		public void InsertTextIntoCurrentRegex(string regex,bool clearFirst)
		{
			if(clearFirst)
			{
				txtRegex.Text= regex;
				Invalidate(true);
			}
			else
			{
				InsertTextIntoCurrentRegex(regex);
			}
			//SetTextSelection(regex);
		}

 

		public void InsertTextIntoCurrentRegex(string regex, string input, string description)
		{
			InsertTextIntoCurrentRegex(regex,true);
			InsertTextIntoCurrentInput(input);
			txtRegex.Text= "#" + description +"\n" + regex;
			Invalidate(true);
			
		}

		public void CreateNewDocument(string regex, string input, string description)
		{
			throw new   NotSupportedException("CreateNewDocument is not supperted by a RegexDocument object");
		}

		public RegexProject CurrentDocument
		{
			get
			{
				return this.RegexProject;
			}
		}
		#endregion

		private void panel2_Paint_1(object sender, PaintEventArgs e)
		{
			
		}

		private enum ControlFocus
		{
			RegexText,
			InputText,
			ReplaceWithText,
			MatchesTree,
			ReplaceOutput,
			SplitsOutput
			
		}
		private void SetFocusOn(ControlFocus selection)
		{
			try
			{
				switch (selection) 
				{
					case ControlFocus.RegexText:
						txtRegex.Focus();
						break;
					case ControlFocus.InputText:
						tabSctInputPart.SelectedTab= tabInputText;
						txtInput.Focus();
						break;
					case ControlFocus.ReplaceWithText:
						tabSctInputPart.SelectedTab= tabReplaceWith;
						txtReplaceWith.Focus();
						break;
					case ControlFocus.MatchesTree:
						tabControl1.SelectedTab= tabMatches;
						Tree.Focus();
					
						break;
					case ControlFocus.ReplaceOutput:
						tabControl1.SelectedTab= tabOutput;
						txtOutputReplace.Focus();

						break;
					case ControlFocus.SplitsOutput:
						tabControl1.SelectedTab= tabSplits;
						lvSplits.Focus();

						break;

					default:
						;
						break;
				}
			}
			catch(Exception )
			{
		    
			}
		}
	

	

		private void Tree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			ShowCurrentTreeResultInInputText();
		}


		private void ShowCurrentTreeResultInInputText()
		{
			try
			{
				if(Tree.SelectedNode.Parent==null) // Must be the top level node
				{
					Match m=(Match)Tree.SelectedNode.Tag;
					if(m!=null)
					{
						
						txtInput.Select( m.Index,m.Length);
						txtInput.ScrollToCaret();
					}
				}
				else if(Tree.SelectedNode.Parent.Parent==null) // Must be a group
				{
					Group g=(Group)Tree.SelectedNode.Tag;
					if(g!=null)
					{
						txtInput.Select(g.Index,g.Length);
						txtInput.ScrollToCaret();
					}
				}
				else // Must be a capture
				{
					Capture c=(Capture)Tree.SelectedNode.Tag;
					if(c!=null)
					{
						txtInput.Select(c.Index,c.Length);
						txtInput.ScrollToCaret();
					}
				}

			}
			catch(Exception )
			{
			    
			}
		}
	
		public void InsertTextIntoCurrentInput(string text)
		{
			txtInput.Text=text;

		}

		private void txtRegex_Changed(object sender, EventArgs e)
		{
			Dirty=true;
		}

		private void txtInput_TextChanged(object sender, EventArgs e)
		{
			Dirty=true;
			SetCurrentProjectProperties();
		}

		private void txtReplaceWith_TextChanged(object sender, EventArgs e)
		{
			Dirty=true;
			SetCurrentProjectProperties();
		}

		private void txtRegex_KeyPress(object sender, KeyPressEventArgs e)
		{
			
		}

		private void txtRegex_Load(object sender, EventArgs e)
		{

		}





		private void RegexDocument_Activated(object sender, EventArgs e)
		{
			AppContext.Instance.OnProjectChange(_currentProject);
		}

		private void txtRegex_ModifiedChanged(object sender, EventArgs e)
		{
			
		}

		private void txtInput_TextChanged_1(object sender, EventArgs e)
		{
			Dirty=true;
			SetCurrentProjectProperties();
		}


		private void txtRegex_BackColorChanged(object sender, EventArgs e)
		{
			//OnOptionsChanged();
			
		}

		

		private void txtRegex_BackgroundImageChanged(object sender, EventArgs e)
		{
//			OnOptionsChanged();
		}

		private void txtRegex_ForeColorChanged(object sender, EventArgs e)
		{
			//OnOptionsChanged();
		}



		private void RegexDocument_Closing(object sender, CancelEventArgs e)
		{
			if(!CloseVerified && 
				!VerifyCanClose())
			{
				e.Cancel=true;
				return ;
			}

			RaiseCloseRequestedEvent();
		}

		private void cmdInputChildRefreshFromFile_Click(object sender, EventArgs e)
		{
			RefreshTextFromInputFile();
		}

		private bool IsFileNameURL()
		{
			string regex = @"^((ht|f)tp(s?))\:";
			if(Regex.IsMatch(txtInputFilename.Text,regex))
			{
				return true;
			}

			return false;
		}

		private void DownloadURL()
		{
			try
			{
				txtInput.Text="Downloading...";
				Application.DoEvents();
				Application.DoEvents();
				WebRequest request = WebRequest.Create(txtInputFilename.Text);
				request.Proxy= ProxyFactory.Create( AppContext.Instance.Settings.ProxySettings,request.RequestUri.ToString());
				WebResponse response= request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				txtInput.Text=reader.ReadToEnd();
				response.Close();
			}
			catch(Exception e)
			{
				txtInput.Text=e.Message;
			    
			}

		}
		private void RefreshTextFromInputFile()
		{
			if(IsFileNameURL())
			{
				DownloadURL();
				return;
			}
			if(File.Exists(RegexProject.InputFilename))
			{
				try
				{
					
					StreamReader fs = File.OpenText(RegexProject.InputFilename);
					txtInput.Text=fs.ReadToEnd();
					fs.Close();

				}
				catch(Exception e)
				{
					MessageBox.Show("There was a problem reading from the file:\n" + e.Message);    
					return ;
				}
			}
			else
			{
				MessageBox.Show("Could not read from specified file");
			}
		}

		private void cmdInputChildSelectFile_Click(object sender, EventArgs e)
		{
			SelectInputFile();
		}

		private void SelectInputFile()
		{
			string filename =string.Empty;
			if(File.Exists(RegexProject.FileName))
			{
				filename= RegexProject.FileName;
			}
			dlgInputFileSelection.FileName=filename;
			if(DialogResult.OK==dlgInputFileSelection.ShowDialog(this))
			{
				txtInputFilename.Text= dlgInputFileSelection.FileName;
				RegexProject.InputFilename= dlgInputFileSelection.FileName;;
				RefreshTextFromInputFile();
			}

		}

		private void cmdmnuRefreshFromInputFile_Click(object sender, ClickEventArgs e)
		{
			RefreshTextFromInputFile();
		}

		private void cmdmnuSelectInputFile_Click(object sender, ClickEventArgs e)
		{
			SelectInputFile();
		}

		private void txtInputFilename_TextChanged(object sender, EventArgs e)
		{
			SetCurrentProjectProperties();
		}

		private void currentProject_Updated(object sender, EventArgs e)
		{
			if(!m_IgnoreProjectUpdates)
			{
				SetFormValuesFromProject();
			
			}
		}

		private void currentProject_RequestMatch(object sender, EventArgs e)
		{
			StartMatchThread();
		}

		private void currentProject_RequestReplace(object sender, EventArgs e)
		{
			StartReplaceThread();
		}

		private void currentProject_RequestSplit(object sender, EventArgs e)
		{
			StartSplitsThread();
		}

		private void txtInputFilename_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		private void txtInputFilename_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Return || e.KeyCode== Keys.Enter)
			{
				RefreshTextFromInputFile();
			}
		}

		private void txtInputFilename_Enter(object sender, EventArgs e)
		{
			
		}

		private void txtInputFilename_MouseDown(object sender, MouseEventArgs e)
		{
			
		
		}

		private void c1ToolBar1_Enter(object sender, EventArgs e)
		{
		
		}

		private void c1ToolBar1_Click(object sender, EventArgs e)
		{
			
		
		}

		private void txtRegex_ContextTooltipPopup(object sender, Syncfusion.Windows.Forms.Edit.ContextTooltipPopupEventArgs e)
		{
			//e.ItemTooltip= e.ItemName;
		}

		private void txtRegex_ContextPromptPopup(object sender, Syncfusion.Windows.Forms.Edit.ContextPromptPopupEventArgs e)
		{
		}



		private void txtRegex_ContentChanged_1(object sender, Syncfusion.Windows.Forms.Edit.ContentChangedEventArgs e)
		{
			
		}

		private void txtRegex_Updated(object sender, EventArgs e)
		{
			Dirty=true;
			SetCurrentProjectProperties();
		}

		private void tabSctInputPart_Enter(object sender, EventArgs e)
		{
			
		}

		private void txtReplaceWith_Updated(object sender, EventArgs e)
		{
			SetCurrentProjectProperties();
		}

		public bool LastCloseWasCanceled
		{
			get
			{
				return m_booLastCloseWasCanceled;
			}
			set
			{
				m_booLastCloseWasCanceled = value;
			}
		}

		private void Settings_SettingsChanged(object sender, EventArgs e)
		{
			RefreshSettings();
		}
	}
}
