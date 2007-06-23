using System;
using System.Text.RegularExpressions;
using System.IO;
using Regulator.SDK;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Edit;
namespace Regulator.Ctl
{
	/// <summary>
	/// Summary description for RegexEditor.
	/// </summary>
	public class RegexEditor : System.Windows.Forms.UserControl
	{
		private bool m_booIntellisenseEnabled=true;

		private bool m_booNamedGroupsMode;

		private bool m_booDirty;

		public  Syncfusion.Windows.Forms.Edit.EditControl txtRegex;
		private System.Windows.Forms.ImageList imgLst;
		private System.ComponentModel.IContainer components;

		public RegexEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			InitializeEditControl();
	

		}

		
		private bool _SaveOptionsBeforeClosing=false;

		private void OptionsMenu_Clicked(object sender, EventArgs e)
		{
			_SaveOptionsBeforeClosing=true;
		}

		private void initRegexInput()
		{

			txtRegex.Dock= DockStyle.Fill;
			txtRegex.AutomaticOutliningEnabled=true;
			txtRegex.OutliningEnabled=true;
			if(this.DesignMode)
			{
				return ;
			}
			txtRegex.ContentChangedEventEnabled=true;
			txtRegex.ContentChanged+=new ContentChangedEventHandler(txtRegex_ContentChanged);
			this.txtRegex.FontChanged += new System.EventHandler(this.txtRegex_FontChanged);
			txtRegex.WordWrap=true;
			txtRegex.ScrollBars=ScrollBars.Both;
			txtRegex.StatusBarVisible=false;
			txtRegex.CopyWithoutSelection=true;
			txtRegex.HideSelection=false;
			
			//time/date insert using F5
			txtRegex.EditMenu.MenuItems[14].Shortcut=Shortcut.None;
			//get when the user clicks the options menu
			txtRegex.ContextMenu.MenuItems[8].Click+=new EventHandler(OptionsMenu_Clicked);
			//.ContextMenu.MenuItems[0].MenuItems[14].Shortcut=Shortcut.None;//time/date insert using F5
			
			txtRegex.FileExtension=".express";
			txtRegex.FileMenu.Enabled=false;
			txtRegex.FileMenu.Visible=false;
			txtRegex.ContextMenu.MenuItems[txtRegex.FileMenu.Index+1].Visible=false;//remove the extra line seperator
			txtRegex.EditMenu.MenuItems.Add(0,new MenuItem("&Escape selection",new EventHandler(FillSelectionSpaces),Shortcut.F9));
			txtRegex.BraceMatchingEnabled=true;

			//add the "add item" shortcuts
			
			if(!NamedGroupsMode)
			{
				AttachQuickCodeMenu();			
			}

			//load a user custom settings file if exists
			if(File.Exists(AppContext.Instance.Settings.SettingsFileNameEditor))
			{
				try
				{
					txtRegex.SettingFile= AppContext.Instance.Settings.SettingsFileNameEditor;
					
				}
				catch(Exception )
				{
					txtRegex.SettingFile=Path.Combine(Application.StartupPath ,"regex.ini");
				}
			}
			else
			{
				txtRegex.SettingFile=Path.Combine(Application.StartupPath ,"regex.ini");
			}
		}

		private void AttachQuickCodeMenu()
		{
			if(txtRegex.ContextMenu.MenuItems[0].Text!=AppContext.Instance.Settings.QuickMenuItem.Text)
			{
				txtRegex.ContextMenu.MenuItems.Add(0,AppContext.Instance.Settings.QuickMenuItem.CloneMenu());
				AppContext.Instance.Settings.QuickMenuClicked+=new EventHandler(OnAddItemShortcutClick);
			}
		}
		private void OnAddItemShortcutClick(object sender,EventArgs e)
		{

			//			if(this.MdiParent.ActiveMdiChild==null)
			//			{
			//				//this is not the active document
			//				return ;
			//			}
			//
			//			if(this.MdiParent.ActiveMdiChild.GetHashCode()!=this.GetHashCode())
			//			{
			//				//this is not the active document
			//				return ;
			//			}

			MenuItem menuItem = (MenuItem) sender;

			Regex regexBreak = new Regex(".+ - (?<Placeholder>.+)");
			Match match = regexBreak.Match(menuItem.Text);
			if (match.Success)
			{
				string insert = match.Groups["Placeholder"].ToString();
				AppContext.Instance.Display.InsertTextIntoCurrentRegex(insert);
			}

		}

		private string GetRegexText()
		{

			if(txtRegex.HasSelection)
			{
				return txtRegex.SelectedText;
			}
			else
			{
				return txtRegex.Text;
			}
		}

		private void FillSelectionSpaces(object sender, EventArgs ea)
		{
			string text = GetRegexText();
			text = text.Replace(" ",@"\s");
			text = text.Replace("\t",@"\t");
			text = text.Replace("\n",@"\n");
			
			text = text.Replace("[",@"\[");
			text = text.Replace("]",@"\]");

			text = text.Replace("(",@"\(");
			text = text.Replace(")",@"\)");

			text = text.Replace("#",@"\#");
			text = text.Replace(".",@"\.");
			text = text.Replace("*",@"\*");
			text = text.Replace("!",@"\!");
			text = text.Replace(":",@"\:");
			text = text.Replace("+",@"\+");
			text = text.Replace("?",@"\?");


			if(txtRegex.SelectedText.Length>0)
			{
				txtRegex.ReplaceSelection(text);
			}
			else
			{
				txtRegex.Text= text;
			}
		}


		private bool _IsPopupContextActive=false;
		private string _currentTextWithActiveIntellisense;

		private void txtRegex_ContentChanged(object sender, ContentChangedEventArgs e)
		{
			try
			{
				Dirty=true;
				if(_IsPopupContextActive && e.Text.IndexOf(":")>-1)
				{
					_currentTextWithActiveIntellisense= txtRegex.Text;

					_IsPopupContextActive=false;
					txtRegex.Delete(e.LocationRange);
					txtRegex.DeleteCharBeforeCaret();
					string toInsert = e.Text.Split(':')[0].Trim();
					
					txtRegex.Insert(toInsert);
					if(!SetTextSelection(toInsert))
					{
						SendKeys.Send("{LEFT}");
					
					}
				}

			}
			catch(Exception ee)
			{
			    
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RegexEditor));
			this.imgLst = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// imgLst
			// 
			this.imgLst.ImageSize = new System.Drawing.Size(16, 16);
			this.imgLst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLst.ImageStream")));
			this.imgLst.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// RegexEditor
			// 
			this.Name = "RegexEditor";
			this.Size = new System.Drawing.Size(377, 257);
			this.Load += new System.EventHandler(this.RegexEditor_Load);
			this.ResumeLayout(false);

		}

		private void InitializeEditControl()
		{
			this.SuspendLayout();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RegexEditor));
			this.txtRegex = new Syncfusion.Windows.Forms.Edit.EditControl();

			// 
			// txtRegex
			// 
			this.txtRegex.BackColor = System.Drawing.Color.White;
			this.txtRegex.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtRegex.ContextChoiceChar = new char[] {
															 '.'};
			this.txtRegex.CopyWithoutSelection = false;
			this.txtRegex.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtRegex.Location = new System.Drawing.Point(-15, 47);
			this.txtRegex.Name = "txtRegex";
			this.txtRegex.RightMarginLineColumn = 80;
			this.txtRegex.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtRegex.SelectionMarginVisible = true;
			this.txtRegex.Size = new System.Drawing.Size(320, 168);
			this.txtRegex.StartWithNewFile = true;
			this.txtRegex.TabIndex = 1;
			this.txtRegex.TextEncoding = ((System.Text.Encoding)(resources.GetObject("txtRegex.TextEncoding")));
			this.txtRegex.ContextChoicePopup += new Syncfusion.Windows.Forms.Edit.ContextChoicePopupEventHandler(this.txtRegex_ContextChoicePopup);
			this.txtRegex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRegex_KeyDown);
			this.Controls.Add(this.txtRegex);

			this.ResumeLayout(false);
		}
		#endregion

		protected override bool ProcessDialogKey(Keys keyData)
		{
			switch (keyData) 
			{
				case Keys.Control | Keys.Q:
					AppContext.Instance.Settings.QuickContextMenu.Show(txtRegex,txtRegex.GetPoint(txtRegex.CurrentLineChar));
					return true;

				
						
				default:
					;
					break;
			}

			return base.ProcessDialogKey (keyData);
		}


		private Hashtable LocateUniqueStartCharsInQuickMenuData()
		{
			Hashtable dict = new Hashtable();
			foreach (QuickMenu.MenuItemRow row in AppContext.Instance.Settings.QuickMenuData.MenuItem)
			{
				if(row.IsvalueNull() || row.value.Length==0)
				{
					continue;
				}
				dict[row.value.Substring(0,1)]= row.value;
			}
			return dict;
		}

		public override string Text
		{
			get
			{
				return GetRegexText();
			}
			set
			{
				txtRegex.Text= value;
			}
		}

		protected  override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
		}

		private void InitContextPopupCharsFromQuickMenuData()
		{
			if(this.DesignMode)
			{
				return ;
			}
			if(NamedGroupsMode)
			{
				this.txtRegex.ContextChoiceEnabled=true;
				this.txtRegex.ContextChoiceChar= new char[]{'$'};
				return ;
			}

			this.txtRegex.ContextChoiceEnabled=true;
			Hashtable UniqueStartChars = LocateUniqueStartCharsInQuickMenuData();
			
			this.txtRegex.ContextChoiceChar = new char[UniqueStartChars.Count];
			ArrayList list = new ArrayList(UniqueStartChars.Count);
			foreach (DictionaryEntry entry in UniqueStartChars)
			{
				list.Add(char.Parse(entry.Key.ToString()));
			}

			list.CopyTo(txtRegex.ContextChoiceChar);
		}


		public void InsertTextIntoCurrentRegex(string regex)
		{
			if(txtRegex.HasSelection)
			{
				txtRegex.ReplaceSelection(regex);
			}
			else
			{
				txtRegex.Insert(regex);
			}
			try
			{
				SetTextSelection(regex);
			}
			catch(Exception )
			{
			    
			}
		}

		private bool SetTextSelection(string regex)
		{
			bool success =false;
			try
			{
				Regex regexSelect = new Regex("(?<Select><[^<]+?>)");
				Match match = regexSelect.Match(regex);
				Group g =null;

				if (match.Success)
				{
					g = match.Groups["Select"];

					EditLocationRange foundRange= 
						txtRegex.Find(txtRegex.CurrentLineChar,
						g.Value,true,true,true,
						false,false,false,true);

					int lnStart = txtRegex.CurrentLineChar.L;
					int chStart = txtRegex.CurrentLineChar.C-regex.Length;
					txtRegex.Focus();
					//txtRegex.Select(foundRange);
					txtRegex.Select(lnStart,chStart+g.Index,lnStart,chStart+g.Index+g.Length);
					success =true;
				}
				
				else
				{
					txtRegex.SelectionLocationRange.Start = new EditLocation(txtRegex.SelectionLocationRange.Start.C,
						txtRegex.SelectionLocationRange.Start.C + g.Index);
					success =false;

				}
			
				txtRegex.Focus();
				return success;
			}
			catch(Exception e)
			{
				string s  = e.Message;
				return false;
			}
		}

		private void txtRegex_FontChanged(object sender, System.EventArgs e)
		{
			OnOptionsChanged();
		}

		


		
		private bool _isLoading=false;
		private void OnOptionsChanged()
		{
			if(_isLoading)
			{
				return ;
			}
			try
			{
				txtRegex.SaveSettingsToFile(AppContext.Instance.Settings.SettingsFileNameEditor);
			}
			catch(Exception e)
			{
				MessageBox.Show("Could not save settings to file: \n" + e.Message);
			}
		}

		public bool ReadOnly
		{
			get
			{
				return txtRegex.ReadOnly;
			}
			set{txtRegex.ReadOnly=value;}
		}
		private void RegexEditor_Load(object sender, System.EventArgs e)
		{
			initRegexInput();
			InitContextPopupCharsFromQuickMenuData();	

		
		}

		private void txtRegex_ContextChoicePopup(object sender, Syncfusion.Windows.Forms.Edit.ContextChoicePopupEventArgs e)
		{
			
			if(!m_booIntellisenseEnabled)
			{
				return ;
			}
			_IsPopupContextActive=true;
			InitContextChoices(e);
			try
			{
				if(NamedGroupsMode)
				{
					FillNamedGroups(e);
				}
				else
				{
					FillContextPopupChoices(e);
				}
				
			}
			catch(Exception )
			{
			    
			}

		}

		private void InitContextChoices( Syncfusion.Windows.Forms.Edit.ContextChoicePopupEventArgs e)
		{
			e.ItemFont= new Font("Tahoma",8,FontStyle.Bold);
			e.Choices=new ArrayList();
			e.ImageIndexes= new ArrayList();
			e.ItemImageList= imgLst;
		}

		//intellisense list icon indexes
		private const int IMG_NAMED_GROUP=0;
		private const int IMG_REGEX_INTELLISENSE=1;
		private const int IMG_ERROR=2;
		private const int IMG_NOT_FOUND=3;



		private void FillNamedGroups( Syncfusion.Windows.Forms.Edit.ContextChoicePopupEventArgs e)
		{
			InitContextChoices(e);
			
			try
			{

				string pattern= "\\(\\?<(?<GroupName>.*?)>";
				string regex = AppContext.Instance.ActiveProject.Regex;
				RegexOptions options = RegexOptions.IgnorePatternWhitespace | 
					RegexOptions.Multiline|
					RegexOptions.IgnoreCase;

				MatchCollection matches = Regex.Matches(regex,pattern,options);

				foreach (Match match in matches)
				{
					string groupName = match.Groups["GroupName"].Value;
					if(groupName!=null && groupName.Trim()!=string.Empty)
					{
						e.Choices.Add("{" + groupName + "}");
						e.ImageIndexes.Add(IMG_NAMED_GROUP);
					}
				}
				if(e.Choices.Count==0)
				{
					e.Choices.Add("No groups found...");
					e.ImageIndexes.Add(IMG_NOT_FOUND);

				}
			}
			catch(Exception ex)
			{
				e.Choices.Add("Error parsing for group names...");
				e.ImageIndexes.Add(IMG_ERROR);
			}
		}

		private void FillContextPopupChoices(Syncfusion.Windows.Forms.Edit.ContextChoicePopupEventArgs e)
		{
			if(NamedGroupsMode)
			{
				return ;
			}
			InitContextChoices(e);
			try
			{
				foreach (QuickMenu.MenuItemRow row in AppContext.Instance.Settings.QuickMenuData.MenuItem)
				{
					if(row.IsvalueNull() ||
						!row.value.StartsWith(e.ContextChoiceChar))
					{
						continue;
					}

					string suggestion = row.value;
					string description = row.name;
					string toAdd = suggestion+ "   :   " + description;

					e.Choices.Add(toAdd);
					e.ImageIndexes.Add(IMG_REGEX_INTELLISENSE);

				}
				if(e.Choices.Count==0)
				{
					e.Choices=null;
				}
			}
			catch(Exception ex)
			{
				e.Choices.Add("Error adding coices...");
				e.ImageIndexes.Add(IMG_ERROR);

			}
		}



		public event EventHandler Updated;
		private void txtRegex_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{

	
			if(Updated!=null)
			{
				Updated(this,new EventArgs());
			}
		}




		public bool Dirty
		{
			get
			{
				return m_booDirty;
			}
			set
			{
				m_booDirty = value;
			}
		}


		public bool NamedGroupsMode
		{
			get
			{
				return m_booNamedGroupsMode;
			}
			set
			{
				m_booNamedGroupsMode = value;
			}
		}


		public bool IntellisenseEnabled
		{
			get
			{
				return m_booIntellisenseEnabled;
			}
			set
			{
				m_booIntellisenseEnabled = value;
			}
		}

	}
}
