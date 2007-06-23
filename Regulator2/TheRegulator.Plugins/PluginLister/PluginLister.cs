using System;
using Regulator.SDK;
using Regulator.SDK.Plugins;


namespace PluginLister
{
	/// <summary>
	/// Summary description for PluginLister.
	/// </summary>
	public class PluginLister:IPlugin
	{
		public PluginLister()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IPlugin Members

		public void ProjectChanged(RegexProject newProject)
		{
			// TODO:  Add PluginLister.ProjectChanged implementation
		}

		public void ShowAbout()
		{
			// TODO:  Add PluginLister.ShowAbout implementation
		}

		public void OnDockActivate()
		{
			// TODO:  Add PluginLister.OnDockActivate implementation
		}

		public System.Windows.Forms.Shortcut Shortcut
		{
			get
			{
				// TODO:  Add PluginLister.Shortcut getter implementation
				return new System.Windows.Forms.Shortcut ();
			}
			set
			{
				// TODO:  Add PluginLister.Shortcut setter implementation
			}
		}

		public void OnDialogClick()
		{
			new PluginListForm().ShowPluginDialog();
		}

		public Regulator.SDK.Plugins.PluginTypes PluginType
		{
			get
			{
				// TODO:  Add PluginLister.PluginType getter implementation
				return PluginTypes.Dialog;
			}
		}

		public string MenuCation
		{
			get
			{
				// TODO:  Add PluginLister.MenuCation getter implementation
				return "Loaded Plugins...";
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public string PluginName
		{
			get
			{
				// TODO:  Add PluginLister.PluginName getter implementation
				return "Plugin lister";
			}
			set
			{
				// TODO:  Add PluginLister.PluginName setter implementation
			}
		}

		public void OnInit(AppContext context)
		{
		}

		public void BeforeClose()
		{
			// TODO:  Add PluginLister.BeforeClose implementation
		}

		public System.Drawing.Icon Icon
		{
			get
			{
				// TODO:  Add PluginLister.Icon getter implementation
				return new PluginListForm().Icon;
			}
		}

		public System.Windows.Forms.Control control
		{
			get
			{
				// TODO:  Add PluginLister.control getter implementation
				return new PluginListForm();
			}
		}

		public Regulator.SDK.Plugins.PluginDockPositions PreferredDockState
		{
			get
			{
				// TODO:  Add PluginLister.PreferredDockState getter implementation
				return PluginDockPositions.Left;
			}
		}

		#endregion
	}
}
