using System;
using Regulator.SDK;
using Regulator.SDK.Plugins;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Regulator.SDK.Plugins
{
	[GenericPlugin]
	public class GenericDockedPlugin : System.Windows.Forms.UserControl,IPlugin
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private PluginDockPositions _dockstate =PluginDockPositions.Left;
		public GenericDockedPlugin()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}


		public  virtual void BeforeClose()
		{
			
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
			components = new System.ComponentModel.Container();
		}
		#endregion

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
		public virtual void ShowAbout()
		{
			// TODO:  Add GenericDialogPlugin.ShowAbout implementation
		}
		public virtual Icon Icon
		{
			get
			{
				return null;
			}
		}

		public virtual Regulator.SDK.Plugins.PluginDockPositions PreferredDockState
		{
			get
			{
				return _dockstate ;
			}
			set
			{
				_dockstate = value;
			}
		}

		public virtual Control control
		{
			get
			{
				return this;
			}
		}

		public  virtual Regulator.SDK.Plugins.PluginTypes PluginType
		{
			get
			{
				return PluginTypes.Dockable;;
			}
		}

		public  virtual string PluginName
		{
			get
			{
				// TODO:  Add GenericDockedPlugin.PluginName getter implementation
				return this.Name;
			}
			set
			{
				this.Name=value;
			}
		}

		protected string m_MenuCaption=string.Empty;
		public  virtual string MenuCation
		{
			get
			{
				// TODO:  Add GenericDockedPlugin.MenuCation getter implementation
				return m_MenuCaption;
			}
			set
			{
				m_MenuCaption=value;
			}
		}

		protected RegexProject _currentProject=null;
		public virtual void ProjectChanged(Regulator.SDK.RegexProject newProject)
		{
			_currentProject= newProject;
			_currentProject.ActionStarted+=new Regulator.SDK.RegexProject.RegexActionStartDelegate(OnProjectActionStarted);
			_currentProject.MatchEnded+=new Regulator.SDK.RegexProject.RegexMatchEndedDelegate(OnProjectMatchEnded);
			_currentProject.ReplaceEnded+=new Regulator.SDK.RegexProject.RegexReplaceEndedDelegate(OnProjectReplaceEnded);
			_currentProject.SplitEnded+=new Regulator.SDK.RegexProject.RegexSplitEndedDelegate(OnProjectSplitEnded);
			_currentProject.Updated+=new EventHandler(OnProjectUpdated);
		}

		protected AppContext m_Context=null;
		public  virtual void OnInit(AppContext context)
		{
			m_Context=context;
		}

		public  virtual void OnDialogClick()
		{
			// TODO:  Add GenericDockedPlugin.OnDialogClick implementation
		}

		public  virtual void OnDockActivate()
		{
			
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
