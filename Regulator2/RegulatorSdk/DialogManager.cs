using System;
using System.Windows.Forms;
using System.Collections;

namespace Regulator.SDK
{

	public enum DialogCoreTypes
	{
			Options
	}
	public class DialogManager
	{
		private Hashtable _dialogs = new Hashtable();

		public void AddDialog(System.Type dialogType,string name)
		{
			if(_dialogs[name]==null)
			{
				_dialogs.Add(name,dialogType);

			}
			else
			{
				throw new Exception("Dialog with same name cannot be added twice");
			}
		}

		public void AddDialog(System.Type dialogType,DialogCoreTypes builtinDialogType)
		{
			string name = string.Empty;
			switch (builtinDialogType) 
			{
				case DialogCoreTypes.Options:
					name="options";
					break;
				default:
					return;
					
			}
			if(_dialogs[name]==null)
			{
				_dialogs.Add(name,dialogType);

			}
			else
			{
				throw new Exception("Dialog with same name cannot be added twice");
			}
		}


		public void ShowConnectionOptions()
		{
			IOptionsDialog options = (IOptionsDialog)Activator.CreateInstance((Type)_dialogs["options"]);
			options.ShowConnectionOptions();
		}

		public void ShowOptions()
		{
			IOptionsDialog options = (IOptionsDialog)Activator.CreateInstance((Type)_dialogs["options"]);
			options.ShowGeneralOptions();
			
		}
	}
}
