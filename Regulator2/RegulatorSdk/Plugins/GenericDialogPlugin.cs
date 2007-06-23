using System;
using Regulator.SDK;
using Regulator.SDK.Plugins;
using System.Windows.Forms;
using System.Drawing;

namespace Regulator.SDK.Plugins
{
	//this allows us to visually design the class
	//in design time, but make it abstract at release

	[GenericPlugin]
	public  class GenericDialogPlugin:Form,IPlugin
	{
		#region IPlugin Members
		private Shortcut m_Shortcut = Shortcut.None;
		public virtual Shortcut Shortcut
		{
			get
			{
				return m_Shortcut;
			}
			set
			{
				m_Shortcut= value;
			}
		}
		public virtual Regulator.SDK.Plugins.PluginDockPositions PreferredDockState
		{
			get
			{
				return PluginDockPositions.Left;
			}
		}

		public  virtual void BeforeClose()
		{
			
		}


		public Control control
		{
			get
			{
				
				// TODO:  Add GenericDialogPlugin.control getter implementation
				return this;
			}
		}

		public virtual Regulator.SDK.Plugins.PluginTypes PluginType
		{
			get
			{
				// TODO:  Add GenericDialogPlugin.PluginType getter implementation
				return PluginTypes.Dialog;
			}
		}

		public virtual string PluginName
		{
			get
			{
				// TODO:  Add GenericDialogPlugin.PluginName getter implementation
				return this.Name;
			}
			set
			{
				// TODO:  Add GenericDialogPlugin.PluginName setter implementation
			}
		}

		public virtual string MenuCation
		{
			get
			{
				// TODO:  Add GenericDialogPlugin.MenuCation getter implementation
				return this.Text;
			}
			set
			{
				this.Text= value;
			}
		}

		protected RegexProject _currentProject=null;

		protected virtual void OnDialogHide()
		{
			Hide();
		}
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			
			base.OnClosing (e);
			e.Cancel=true;
			Hide();
		}

		private void InitializeComponent()
		{
			// 
			// GenericDialogPlugin
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Name = "GenericDialogPlugin";
			this.Text = "Generic Regulator Dialog plugin form";

		}
	
		public virtual void ProjectChanged(Regulator.SDK.RegexProject newProject)
		{
			_currentProject= newProject;
			_currentProject.ActionStarted+=new Regulator.SDK.RegexProject.RegexActionStartDelegate(OnProjectActionStarted);
			_currentProject.MatchEnded+=new Regulator.SDK.RegexProject.RegexMatchEndedDelegate(OnProjectMatchEnded);
				_currentProject.ReplaceEnded+=new Regulator.SDK.RegexProject.RegexReplaceEndedDelegate(OnProjectReplaceEnded);
				_currentProject.SplitEnded+=new Regulator.SDK.RegexProject.RegexSplitEndedDelegate(OnProjectSplitEnded);
					_currentProject.Updated+=new EventHandler(OnProjectUpdated);
		}

		protected Regulator.SDK.AppContext _context=null;
		public virtual void OnInit(Regulator.SDK.AppContext context)
		{
			_context=context;
			//this.ActiveControl.BackColor=Color.Azure;
			
		}

		public  virtual  void OnDialogClick()
		{
						ShowDialog();
				}

		public virtual void OnDockActivate()
		{
			// TODO:  Add GenericDialogPlugin.OnDockActivate implementation
		}


		

		

		public virtual void ShowAbout()
		{
			// TODO:  Add GenericDialogPlugin.ShowAbout implementation
		}
		#endregion

		protected  virtual void OnProjectActionStarted(RegexProject sender, RegexActionTypes action)
		{

		}

		protected  virtual void OnProjectMatchEnded(RegexProject sender, System.Text.RegularExpressions.MatchCollection matches)
		{

		}

		protected  virtual void OnProjectReplaceEnded(RegexProject sender, string replaceResult)
		{

		}

		protected  virtual void OnProjectSplitEnded(RegexProject sender, string[] results)
		{

		}

		protected virtual void OnProjectUpdated(object sender, EventArgs e)
		{

		}
		

		
	}
}
