using System;
using Regulator.SDK.Plugins;
using System.Collections;
using System.Xml;
using System.IO;

namespace Regulator.SDK
{
	/// <summary>
	/// Summary description for PluginLoader.
	/// </summary>
	public class PluginLoader
	{
		private PluginCollection m_pluLoadedPlugins = new PluginCollection();

		private string m_strFileName;
		public ArrayList m_PluginNames= new ArrayList();


		public PluginLoader()
		{
		}

		public int PluginCount
		{
			get
			{
				return m_PluginNames.Count;
			}
		}

		public bool ReadFile()
		{
			if(!File.Exists(FileName))
			{
				return false;
			}
			LoadFileXML(FileName);
			return true;
		}

		private void LoadFileXML(string filename)
		{
			m_PluginNames.Clear();
			XmlTextReader r = new XmlTextReader(filename);
			while(r.Read())
			{
				if(r.Name=="add")	
				{
					m_PluginNames.Add(r.GetAttribute("type"));
				}
			}
		}

		public string FileName
		{
			get
			{
				return m_strFileName;
			}
			set
			{
				m_strFileName = value;
			}
		}


		public bool LoadPlugins()
		{
			LoadedPlugins.Clear();
			foreach (string name in m_PluginNames)
			{
				IPlugin plugin =null;

				try
				{
					plugin = (IPlugin) Activator.CreateInstance(Type.GetType(name));
				}
				catch(Exception )
				{
					return false;				    
				}
			
				if(plugin!=null)
				{
					LoadedPlugins.Add(plugin);
				}
			}

			return true;
		}
		


		public PluginCollection LoadedPlugins
		{
			get
			{
				return m_pluLoadedPlugins;
			}
		}

	}
}
