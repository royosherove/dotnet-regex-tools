using System;
using Regulator.SDK.Plugins;
using System.Xml.Serialization;
using System.Collections;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using Regulator.SDK.Core;
using Regulator.SDK.Proxy;

namespace Regulator.SDK.ApplicationSettings
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	/// 
	[Serializable]
	public class Settings:SelfSerializer
	{
		private bool m_booIntelliseSenseInRegex=true;

		private bool m_booFillUnNamedCapturesInTree=true;

		private ContextMenu _quickContextMenu=null;
		private MenuItem _quickMenuItem=null;
		private QuickMenu _quickMenuData=null;

		public event EventHandler QuickMenuClicked=null;
		public event EventHandler SettingsChanged = null;

		private string _basedir = string.Empty;
		private string _quickMenuFile= string.Empty;
		public string BaseDir
		{
			get
			{
				if(_basedir==string.Empty)
				{
					_basedir= new FileInfo( Assembly.GetEntryAssembly().Location).Directory.FullName;
				}
				return _basedir;
			}
		}

		public string QuickMenuFileName
		{
			get
			{
				if(_quickMenuFile==string.Empty)
				{
					
					_quickMenuFile= Path.Combine( BaseDir , "quickmenu.config");
				}
				return _quickMenuFile;
			}
		}

		private void OnSettingChange()
		{
			TriggerSettingsChanged();
		}
	
		private void OnQuickMenuClick(object sender,EventArgs ea)
		{
			if(QuickMenuClicked!=null)
			{
				QuickMenuClicked(sender,ea);
			}
			
		}

		private void TriggerSettingsChanged()
		{
			if(SettingsChanged!=null)
			{
				SettingsChanged(this,new EventArgs());
			}
		}


		public ContextMenu QuickContextMenu
		{
			get
			{
				if(_quickContextMenu==null)
				{
					_quickContextMenu= MenuReader.ReadContextMenu(QuickMenuFileName,new EventHandler(OnQuickMenuClick));
				}
				return _quickContextMenu;
			}
		}


		public MenuItem QuickMenuItem
		{
			get
			{
				if(_quickMenuItem==null)
				{
					_quickMenuItem= MenuReader.ReadMenuItem(QuickMenuFileName,new EventHandler(OnQuickMenuClick));
				}
				return _quickMenuItem;
			}
		}
		private FormSettingsCollection m_forDialogSettings=new FormSettingsCollection();

		private MRUSettings m_mRUMRU=new MRUSettings();

		

		private ProxyInfo m_proProxySettings = new ProxyInfo();

		private bool m_booMinimizeToTray=false;

		

		public string SettingsFileNameEditor
		{
			get
			{
				return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),"editor.config");
			}
		}

		public static string GetSettingsFileName
		{
			get {return new ApplicationSettings.Settings().SettingsFileName;}
		}

		public string SettingsFileName
		{
			get
			{
				return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),"settings.config");
			}
		}
		public string SettingsFileNameDocking
		{
			get
			{
				return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),"dock.config");
			}
		}


		private static bool Encrypt()
		{
#if DEBUG
		{
			return true;
		}
#else
			return  FileEncryptor.EncryptFile(GetSettingsFileName,_key);
#endif
		}

		private static bool Decrypt()
		{
#if DEBUG
		{
			return true;
		}
#else
		{
			return  FileEncryptor.DecryptFile(GetSettingsFileName,_key);
		}
#endif
			
		}


		public static Settings LoadFromSettingsFile()
		{
			Settings newSettingsObj = new Settings();
			string filename = newSettingsObj.SettingsFileName;
			if(!File.Exists(filename))
			{
				return new Settings();
			}

			Decrypt();
			return (ApplicationSettings.Settings)ApplicationSettings.Settings.Load(newSettingsObj.GetType() ,filename);
		}

		
		public  Settings()
		{
			

		}

#if DEBUG
	
#else
		private static readonly string _key="Iu9Rl50t";
#endif
		
		public void Save()
		{
			Save(SettingsFileName);
			Encrypt();
		}

		public bool MinimizeToTray
		{
			get
			{
				return m_booMinimizeToTray;
			}
			set
			{
				m_booMinimizeToTray = value;
				OnSettingChange();
			}
		}


		public ProxyInfo ProxySettings
		{
			get
			{
				return m_proProxySettings;
			}
			set
			{
				m_proProxySettings = value;
			}
		}


		




		public MRUSettings MRU
		{
			get
			{
				return m_mRUMRU;
			}
			set
			{
				m_mRUMRU = value;
			}
		}


		public FormSettingsCollection DialogSettings
		{
			get
			{
				return m_forDialogSettings;
			}
			set
			{
				m_forDialogSettings = value;
			}
		}


		public bool FillUnNamedCapturesInTree
		{
			get
			{
				return m_booFillUnNamedCapturesInTree;
			}
			set
			{
				m_booFillUnNamedCapturesInTree = value;
				OnSettingChange();
			}
		}


		public QuickMenu QuickMenuData
		{
			get
			{
				if(_quickMenuData==null)
				{
					_quickMenuData= MenuReader.ReadQuickMenuData();
				}
				return _quickMenuData;
			}
		}


		public bool IntelliseSenseInRegex
		{
			get
			{
				return m_booIntelliseSenseInRegex;
			}
			set
			{
				m_booIntelliseSenseInRegex = value;
				OnSettingChange();
			}
		}

	}
}
