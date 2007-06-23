//code provided as is. You are free to use and redistribute.
//Author: Roy Osherove (http://www.iserializable.com)
using System;
using System.Security.Policy;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Regulator.SDK.Plugins
{
	/// <summary>
	/// Dynamically searches for plugins that implement IPlugin
	/// in a specific location
	/// </summary>
	[Serializable]
	public class DynamicFindPluginProvider
	{
		private string m_strSearchDirectory;

		private PluginCollection m_Plugins= new PluginCollection() ;


		private string m_strFileExtensionFilter;

		private ArrayList m_GoodTypes= new  ArrayList();

		public DynamicFindPluginProvider()
		{
		}





		private  ArrayList SearchPath(string path)
		{
			m_GoodTypes.Clear();
			foreach(string file in Directory.GetFiles(path,FileExtensionFilter))
			{
				//do not load syncfusion dll's 
				//save some time
				if(IsRelevantFileName(file))
				{
					TryLoadingPlugin(file);
				}
			}

			return m_GoodTypes;
		}

		private bool IsRelevantFileName(string file)
		{
			string[] badNames = {"syncfusion.","dundas","c1."};
			foreach (string badName in badNames)
			{
			
				if(file.ToLower().IndexOf(badName)!=-1)
				{
					return false;
				}

			}

			return true;

		}

		
		private void AddToGoodTypesCollection(Type goodType)
		{
			m_GoodTypes.Add(goodType)				;
		}

		private void TryLoadingPlugin(string path)
		{
			try
			{
				FileInfo file = new FileInfo(path);
				path = file.Name.Replace(file.Extension,"");
				
				Assembly asm= AppDomain.CurrentDomain.Load(path);
				
				foreach(Type t in  asm.GetTypes())
				{
					foreach(Type iface in  t.GetInterfaces())		
					{
						if(iface.Equals(typeof(IPlugin)))
						{
							AddToGoodTypesCollection(t);
							break; 
						}
					}
				}
			}
			catch(Exception e)
			{
			Console.WriteLine(e.ToString());
			}
		}

		public string FileExtensionFilter
		{
			get
			{
				return m_strFileExtensionFilter;
			}
			set
			{
				m_strFileExtensionFilter = value;
			}
		}








	
		#region IPluginProvider Members

		public PluginCollection LoadedPlugins
		{
			get
			{
				return m_Plugins;
			}
		}

		//Loads the possible plugin assemblies in a separate AppDomain
		//in order to allow unloading of unneeded assemblies
		public void LoadPlugins()
		{
			m_Plugins= new PluginCollection();

			if(SearchDirectory==null ||
				!Directory.Exists(SearchDirectory))
			{
				throw new DirectoryNotFoundException("Could not find plugin directory");
			}

			

			//The class actually created a new instance of the same class
			//only in a different AppDomain. It then passes it all the needed
			//parameters such as search path and file extensions.
			AppDomain domain = AppDomain.CreateDomain("DynamicPluginLoader");
			
			Assembly asm=  domain.Load(Assembly.GetExecutingAssembly().FullName);
			DynamicFindPluginProvider finder =  (DynamicFindPluginProvider )asm.CreateInstance(typeof(DynamicFindPluginProvider).ToString());
			//DynamicFindPluginProvider finder =  (DynamicFindPluginProvider )domain.CreateInstanceFromAndUnwrap(,typeof(DynamicFindPluginProvider).ToString() );
			finder.FileExtensionFilter= this.FileExtensionFilter;
			finder.SearchDirectory = this.SearchDirectory;

			ArrayList FoundPluginTypes = finder.SearchPath(this.SearchDirectory);
				
			AppDomain.Unload(domain);
			
			foreach(Type t in FoundPluginTypes)
			{
				try
				{
					if(t.IsAbstract)
					{
						continue; 
					}

					//do not instantiate if this type has the "GenericPlugin"  attribute
					object[] customAttribs = t.GetCustomAttributes(typeof(GenericPluginAttribute),false);
					if(customAttribs.Length>0)
					{
						continue; 
					}
					IPlugin plugin =(IPlugin )Activator.CreateInstance(t);
					m_Plugins.Add(plugin);
				}
				catch(Exception e){}
			}
		}

		#endregion

		public string SearchDirectory
		{
			get
			{
				return m_strSearchDirectory;
			}
			set
			{
				m_strSearchDirectory = value;
			}
		}

	}
}
