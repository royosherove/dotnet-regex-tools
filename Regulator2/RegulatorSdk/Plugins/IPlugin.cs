using System;
using System.Drawing;
using System.Windows.Forms;

namespace Regulator.SDK.Plugins
{

	public enum PluginTypes
	{
		Dockable,
		Dialog,
		Hidden
	}

	public enum PluginDockPositions
	{
		Left,
		Right,
		Top,
		Bottom,
		Floating
	}
	public interface IPlugin
	{
		PluginDockPositions PreferredDockState{get;}
		Control control{get;}
		PluginTypes PluginType{get;}
		Icon Icon{get;}
		Shortcut Shortcut{get;set;}
		string PluginName{get;set;}
		string MenuCation{get;set;}
		void ProjectChanged(RegexProject newProject);
		void OnInit(AppContext context);
		void OnDialogClick();
		void OnDockActivate();
		void BeforeClose();
		void ShowAbout();
	}




	#region "'GenericPluginAttribute' custom attribute class"

	/// <summary>
	///     TODO: Describe the purpose of this custom attribute here
	/// </summary>
	/// <remarks>
	///     This attribute can target: Class
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class GenericPluginAttribute : System.Attribute

	{

		/// <summary>
		///     Parameterless (default) attribute class constructor
		/// </summary>
		public GenericPluginAttribute()
		{
			// If appropriate, add custom parameters to this constructor and
			// then add here the corresponding extra fields initialization code
		}

	}

	#endregion //('GenericPluginAttribute' custom attribute class)

}
