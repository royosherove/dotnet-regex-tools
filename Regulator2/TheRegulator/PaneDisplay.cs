using System;
using Regulator.SDK.Plugins;
using Syncfusion.Runtime.Serialization;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;

namespace Regulator.GUI
{

	public enum PaneType
	{
		Other
	}

	public class PaneDisplay
	{
		private DockingManager _dock = new DockingManager();


		private static PaneDisplay _instance;
		public static PaneDisplay Instance
		{
			get
			{
				if(_instance==null)
				{
					_instance=new PaneDisplay();
				}

				return _instance;
			}
		}
		public   PaneDisplay(DockingManager dock)
		{
			InitPanes(dock);
		}
		 

		public PaneDisplay()
		{
		}

		public bool IsInitialized
		{
			get
			{
				return (_dock.HostForm!=null);
			}
		}

		private void VerifyInit()
		{
			if(!IsInitialized)
			{
				throw new Exception("Pane display manager is not initialized");
			}
		}
		public void LoadLayout()
		{
			VerifyInit();
			//_dock.LoadDockState(SerializeMode.XMLFile,Settings.DockSettingsFileName);
			_dock.LoadDockState();
		}

		public void SaveLayout()
		{
			VerifyInit();
			//_dock.SaveDockState(new AppStateSerializer(SerializeMode.XMLFile,Settings.DockSettingsFileName));
			_dock.SaveDockState();
		}
		public void  InitPanes(DockingManager dock)
		{
			if(IsInitialized)
			{
				throw new Exception("Panes already initialized");
			}
			_dock=dock;
			_dock.NewDockStateEndLoad+=new EventHandler(_dock_NewDockStateEndLoad);
			
			
		}
		 
		private void ShowControl(Control ctl)
		{
			ShowControl(ctl,true);
		}

		private void HideControl(Control ctl)
		{
			ShowControl(ctl,false);
		}

		private void ShowControl(Control ctl,bool visible)
		{
			if(visible)
			{
				if(!_dock.GetDockVisibility(ctl))
				{
					_dock.SetDockVisibility(ctl,true);
				}
				_dock.ActivateControl(ctl);
				return ;
			}
			_dock.SetDockVisibility(ctl,visible);

		}

		public void  ShowPane(IPlugin plugin)
		{
			if(plugin.PluginType!= PluginTypes.Dockable)
			{
				return ;
			}
			ShowControl(plugin.control);
			plugin.OnDockActivate();
		}

		public void  ShowPane(PaneType type)
		{
			switch (type) 
			{
					
				default:
					;
					break;
			}
		}

		private bool _isLoadingCustomLayout=false;
		public event EventHandler BeforeLoadLayout;

		private void TriggerPluginRequest()
		{
			if(BeforeLoadLayout!=null)
			{
				BeforeLoadLayout(this,null);
			}
		}


		private void _dock_NewDockStateEndLoad(object sender, EventArgs e)
		{
			if(_isLoadingCustomLayout)
			{
				return ;
			}
			_isLoadingCustomLayout=true;
			TriggerPluginRequest();
			PaneDisplay.Instance.LoadLayout();
			_isLoadingCustomLayout=false;
		}
	}
}
