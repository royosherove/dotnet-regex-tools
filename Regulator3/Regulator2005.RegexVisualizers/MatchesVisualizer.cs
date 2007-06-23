using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Text.RegularExpressions;

namespace Regulator2005.RegexVisualizers
{
	// TODO: Add the following to SomeType's defintion to see this visualizer when debugging instances of SomeType:
	// 
	//  [DebuggerVisualizer(typeof(MatchesVisualizer))]
	//  [Serializable]
	//  public class SomeType
	//  {
	//   ...
	//  }
	// 
	/// <summary>
	/// A Visualizer for SomeType.  
	/// </summary>
	public class MatchesVisualizer : DialogDebuggerVisualizer
	{
		protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
		{
			// TODO: Get the object to display a visualizer for.
			//       Cast the result of objectProvider.GetObject() 
			//       to the type of the object being visualized.
			object data = (object)objectProvider.GetObject();

			
			// TODO: Display your view of the object.
			//       Replace displayForm with your own custom Form or Control.
			MatchesVisualizerForm displayForm = new MatchesVisualizerForm();
			displayForm.setDisplay((MatchCollection)data);
			windowService.ShowDialog(displayForm);
		}

		// TODO: Add the following to your testing code to test the visualizer:
		// 
		//    MatchesVisualizer.TestShowVisualizer(new SomeType());
		// 
		/// <summary>
		/// Tests the visualizer by hosting it outside of the debugger.
		/// </summary>
		/// <param name="objectToVisualize">The object to display in the visualizer.</param>
		public static void TestShowVisualizer(object objectToVisualize)
		{
			VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(MatchesVisualizer));
			visualizerHost.ShowVisualizer();
		}


		public static void VisualizeMatches()
		{
			MatchCollection matches = Regex.Matches("For a million and a one and some other and something else sentence", @"\w+");
			MatchesVisualizer.TestShowVisualizer(matches);

		}

	}
}