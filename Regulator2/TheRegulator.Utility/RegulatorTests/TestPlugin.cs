//using System;
//using DotNetMock.Framework;
//using Regulator.SDK.Plugins;
//using Regulator.SDK;
//namespace Regulator.Tests
//{
//	/// <summary>
//	/// Summary description for TestPlugin.
//	/// </summary>
//	public class TestPlugin:IPlugin
//	{
//		private bool m_booInitCalled=false;
//
//		private bool m_booHideCalled=false;
//
//		private bool m_booTerminateCalled=false;
//
//		private bool m_booActivateCalled=false;
//
//		
//
//		public TestPlugin()
//		{
//			//
//			// TODO: Add constructor logic here
//			//
//		}
//		#region IPlugin Members
//
//		public void OnActivate()
//		{
//			// TODO:  Add TestPlugin.OnActivate implementation
//		}
//
//		public void ProjectChanged(Regulator.SDK.RegexProject newProject)
//		{
//			// TODO:  Add TestPlugin.ProjectChanged implementation
//		}
//
//		public string Description
//		{
//			get
//			{
//				return "description";
//			}
//		}
//
//		public Regulator.SDK.Plugins.PluginTypes PluginType
//		{
//			get
//			{
//				// TODO:  Add TestPlugin.PluginType getter implementation
//				return new Regulator.SDK.Plugins.PluginTypes ();
//			}
//		}
//
//		public void AfterRegexAction(Regulator.SDK.RegexActionTypes action)
//		{
//			// TODO:  Add TestPlugin.AfterRegexAction implementation
//		}
//
//		public void OnTerminate()
//		{
//			// TODO:  Add TestPlugin.OnTerminate implementation
//		}
//
//		public void BeforeRegexAction(Regulator.SDK.RegexActionTypes action)
//		{
//			// TODO:  Add TestPlugin.BeforeRegexAction implementation
//		}
//
//		public void OnHide()
//		{
//			// TODO:  Add TestPlugin.OnHide implementation
//		}
//
//		public string Name
//		{
//			get
//			{
//				// TODO:  Add TestPlugin.Name getter implementation
//				return null;
//			}
//		}
//
//		public void OnInit(AppContext context)
//		{
//			
//		}
//
//		public System.Windows.Forms.Control control
//		{
//			get
//			{
//				// TODO:  Add TestPlugin.control getter implementation
//				return null;
//			}
//		}
//
//		public Regulator.SDK.Plugins.PluginDockPositions PreferredDockState
//		{
//			get
//			{
//				// TODO:  Add TestPlugin.PreferredDockState getter implementation
//				return new Regulator.SDK.Plugins.PluginDockPositions ();
//			}
//		}
//
//		#endregion
//
//		public bool ActivateCalled
//		{
//			get
//			{
//				return m_booActivateCalled;
//			}
//		}
//
//
//		public bool TerminateCalled
//		{
//			get
//			{
//				return m_booTerminateCalled;
//			}
//		}
//
//
//		public bool HideCalled
//		{
//			get
//			{
//				return m_booHideCalled;
//			}
//		}
//
//
//		public bool InitCalled
//		{
//			get
//			{
//				return m_booInitCalled;
//			}
//		}
//
//	}
//}
