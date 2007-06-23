using System;
using System.IO;
using System.Reflection;
using Regulator.SDK.Plugins;
using Regulator.SDK.ApplicationSettings;


namespace Regulator.SDK
{
	public class AppContext
	{
		private RegexProject m_regActiveProject;
		private static AppContext _context = null;
		public static  AppContext Instance 
		{
			get
			{
				
				return _context;
			}
		}
		private IRegexDisplay _display= null;
		private DialogManager _dialogs= new DialogManager();
		private SDK.ApplicationSettings.Settings  _settings= null;
		private Plugins.PluginCollection m_Plugins= new Plugins.PluginCollection();


		
		public void SetDisplayManager(IRegexDisplay display)
		{
			_display= display;
		}
		
		//static ctor to initialize the singleton in a thread-safe manner
		 static AppContext()
		{
			if(_context==null)
			{
				_context = new AppContext();
				InitPlugins(_context);			
				
				
				
			}

		}
		private AppContext()
		{
			_settings = LoadSettings();
		}

		private ApplicationSettings.Settings LoadSettings()
		{
			ApplicationSettings.Settings settingsObj = ApplicationSettings.Settings.LoadFromSettingsFile();
			return settingsObj;
		}


		public DialogManager Dialogs
		{
			get
			{
				return _dialogs;
				
			}
		}

		

		public IRegexDisplay Display
		{
			get
			{
				return _display;
				
			}
		}


		public SDK.ApplicationSettings.Settings   Settings
		{
			get
			{
				return _settings;
				
				
			}
		}

		public Plugins.PluginCollection Plugins
		{
			get
			{
				return m_Plugins;
			}
		}

		public void OnProjectChange(RegexProject newProject)
		{
			m_regActiveProject= newProject;
			NotifyPlugins(newProject);
		}

		private void NotifyPlugins(RegexProject newProject)
		{
			foreach (IPlugin plugin in Plugins)
			{
				plugin.ProjectChanged(newProject);
			}
		}
//		private void InitPlugins()
//		{
//			PluginLoader loader = new PluginLoader();
//			loader.FileName="plugins.xml";
//			if(loader.ReadFile() && loader.LoadPlugins())
//			{
//				m_Plugins= loader.LoadedPlugins;
//			}
//			foreach (IPlugin plugin in m_Plugins)
//			{
//				plugin.OnInit(AppContext.Instance);
//			}
//		}

		private static void InitPlugins(AppContext context)
		{
			DynamicFindPluginProvider loader = new DynamicFindPluginProvider();
			loader.FileExtensionFilter="*.dll";
			loader.SearchDirectory= AppContext.Instance.Settings.BaseDir;
			
			loader.LoadPlugins();
			
			foreach (IPlugin plugin in loader.LoadedPlugins)
			{
				if(plugin!=null)
				{
				
				
					context.Plugins.Add(plugin);
					plugin.OnInit(context);
				}
			}
		}

		public void TerminateApplication()
		{

			TerminatePlugins();
		}

		private void TerminatePlugins()
		{
			foreach (IPlugin plugin in m_Plugins)
			{
				plugin.BeforeClose();
			}

			m_Plugins.Clear();
		}
		

		public RegexProject ActiveProject
		{
			get
			{
				return m_regActiveProject;
			}
		}

	}
}
