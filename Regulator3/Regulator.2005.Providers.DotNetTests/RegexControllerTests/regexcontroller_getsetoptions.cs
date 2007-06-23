using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock;
using Regulator2005.Core;
using Regulator2005.Core.Interfaces;
using NMock.Constraints;
using Regulator2005.Tests.BaseClasses;
using Regulator2005.Core.Args;
using System.Text.RegularExpressions;
using Regulator2005.Core.Args.DotNet;


namespace Regulator2005.Tests.RegexControllerTests
{
	[TestFixture]
	public class RegexController_GetSetOptions:IRegexView
	{
		DotNetRegexEngine controller = new DotNetRegexEngine();
		
		[SetUp]
		public void setup()
		{
			wasSplitCalled = false;
			controller = new DotNetRegexEngine();
		}

		[Test]
		public void RemoveView()
		{
			controller.AttachView(this);
			controller.DetachView(this);
			controller.ExecuteSplit();

			Assert.IsFalse(wasSplitCalled, "Our view should have been detachedfrom the controller");

		}

		#region GetOptions tests
		[Test]
		public void GetOptions_returnsAllRegexOptionsOfDotNet()
		{
			RegexParsingOptions translated = controller.GetOptions();
			//there are actually 10 but one is just a multiple bit flag (RegexOptions.None)
			Assert.AreEqual(9, translated.Count, "all 9 regex options should be accounted for in the get options of a dotnet regex");
		}

		[Test]
		public void GetOptions_OneExpressionOptionIsSetForOneRegexOption()
		{
			verifyValidExpressionOptionIsSelected(RegexOptions.Compiled);
			verifyValidExpressionOptionIsSelected(RegexOptions.CultureInvariant);
			verifyValidExpressionOptionIsSelected(RegexOptions.ECMAScript);
			verifyValidExpressionOptionIsSelected(RegexOptions.ExplicitCapture);
			verifyValidExpressionOptionIsSelected(RegexOptions.IgnoreCase);
			verifyValidExpressionOptionIsSelected(RegexOptions.IgnorePatternWhitespace);
			verifyValidExpressionOptionIsSelected(RegexOptions.Multiline);
			verifyValidExpressionOptionIsSelected(RegexOptions.RightToLeft);
			verifyValidExpressionOptionIsSelected(RegexOptions.Singleline);
		}



		[Test]
		public void GetOptions_ZeroExpressionOptionsAreSetByDefault()
		{
			RegexParsingOptions translated = controller.GetOptions();
			Assert.AreEqual(0, getSelectedOptionCount(translated), "no regex option shoudl have been selected");
		}


		[Test]
		public void GetOptions_NoEmptyOptionName()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append('\n');

			bool wasBadName = false;
			int count = 0;
			foreach (ExpressionOption option in controller.GetOptions())
			{
				count++;
				builder.AppendFormat("{0}:[{1}]\n",
					count.ToString(), option.Name);

				if (option.Name.Trim() == string.Empty)
					wasBadName = true;
			}

			Assert.IsFalse(wasBadName,
				"an empty name was found in a regex option.Here's the full list:{0}",
				builder.ToString());


		} 
		#endregion

		[Test]
		public void SetOneOptionIsTranslatedBack()
		{
			setup_setParsingOptionOnController("IgnorePatternWhitespace", true);
			verify_ControllerParseOptionIs(RegexOptions.IgnorePatternWhitespace, true);
		}

		

		[Test]
		public void TurnOffOneOptionIsTranslatedBack()
		{
			setup_setParsingOptionOnController("IgnorePatternWhitespace",false);
			verify_ControllerParseOptionIs(RegexOptions.IgnorePatternWhitespace, false);
		}

		[Test]
		public void TurnOnTwoOptionsIsTranslatedBack()
		{
			setup_setParsingOptionOnController("IgnorePatternWhitespace", true);
			setup_setParsingOptionOnController("Compiled", true);
			verify_ControllerParseOptionIs(RegexOptions.Compiled, true);
			verify_ControllerParseOptionIs(RegexOptions.IgnorePatternWhitespace, true);
		}

		[Test]
		public void TurnOffTwoOptionsIsTranslatedBack()
		{
			setup_setParsingOptionOnController("IgnorePatternWhitespace", false);
			setup_setParsingOptionOnController("Compiled", false);
			verify_ControllerParseOptionIs(RegexOptions.Compiled, false);
			verify_ControllerParseOptionIs(RegexOptions.IgnorePatternWhitespace, false);
		}

		[Test]
		public void TurnOffOnePreviouslyOnOptionIsTranslatedBack()
		{
			setup_setParsingOptionOnController("IgnorePatternWhitespace", true);
			setup_setParsingOptionOnController("IgnorePatternWhitespace", false);

			verify_ControllerParseOptionIs(RegexOptions.IgnorePatternWhitespace, false);
		}

		[Test]
		public void TurnOffTwoPreviouslyOnOptionsIsTranslatedBack()
		{
			setup_setParsingOptionOnController("IgnorePatternWhitespace", true);
			setup_setParsingOptionOnController("Compiled", true);
			
			setup_setParsingOptionOnController("IgnorePatternWhitespace", false);
			setup_setParsingOptionOnController("Compiled", false);
			
			verify_ControllerParseOptionIs(RegexOptions.Compiled, false);
			verify_ControllerParseOptionIs(RegexOptions.IgnorePatternWhitespace, false);
		}

		[Test]
		public void TurnOnAllOptionsIsTranslatedBack()
		{
			setup_setParsingOptionOnController("IgnorePatternWhitespace", true);
			setup_setParsingOptionOnController("Compiled", true);
			setup_setParsingOptionOnController("CultureInvariant", true);
			setup_setParsingOptionOnController("ECMAScript", true);
			setup_setParsingOptionOnController("ExplicitCapture", true);
			setup_setParsingOptionOnController("IgnoreCase", true);
			setup_setParsingOptionOnController("Multiline", true);
			setup_setParsingOptionOnController("RightToLeft", true);
			setup_setParsingOptionOnController("Singleline", true);


			verify_ControllerParseOptionIs(RegexOptions.Compiled, true);
			verify_ControllerParseOptionIs(RegexOptions.IgnorePatternWhitespace, true);
			verify_ControllerParseOptionIs(RegexOptions.CultureInvariant, true);
			verify_ControllerParseOptionIs(RegexOptions.ECMAScript, true);
			verify_ControllerParseOptionIs(RegexOptions.ExplicitCapture, true);
			verify_ControllerParseOptionIs(RegexOptions.IgnoreCase, true);
			verify_ControllerParseOptionIs(RegexOptions.Multiline, true);
			verify_ControllerParseOptionIs(RegexOptions.RightToLeft, true);
			verify_ControllerParseOptionIs(RegexOptions.Singleline, true);
		}
		private void verify_ControllerParseOptionIs(RegexOptions option, bool shouldBeSelected)
		{
			Assert.AreEqual(
				shouldBeSelected,
				isControllerDotNetRegexOptionSelected(option),
				"A regex option [{0}]selection status was wrong", option);
		}
		
		private void setup_setParsingOptionOnController(string optionName, bool selected)
		{
			RegexParsingOptions options = controller.GetOptions();
			options.GetByName(optionName).Selected = selected;
			controller.SetOptions(options);
		}

		private bool isControllerDotNetRegexOptionSelected(RegexOptions option)
		{
			return 
				((controller.DotNetRegexOptions|option)
					==controller.DotNetRegexOptions);
		}

		private int getSelectedOptionCount(RegexParsingOptions options)
		{
			int selectedCount = 0;
			foreach (ExpressionOption option in options)
			{
				if (option.Selected)
					selectedCount++;
			}
			return selectedCount;
		}
		private void verifyValidExpressionOptionIsSelected(RegexOptions option)
		{
			controller.DotNetRegexOptions = option;
			RegexParsingOptions translated = controller.GetOptions();
			Assert.AreEqual(1, getSelectedOptionCount(translated), "one regex option [{0}] should have been selected", option);
		}


		private bool wasSplitCalled = false;

		#region IRegexView Members

		public void Display(Regulator2005.Framework.Args.DisplayArgs args)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		
		public void OnSplit(SplitEventArgsBase args)
		{
			wasSplitCalled = true;
		}

		public void OnMatches(MatchesEventArgsBase args)
		{

		}

		public void OnReplace(ReplaceEventArgsBase args)
		{
		
		}

		public void Init(IRegexEngine controller)
		{
			
		}
		#endregion
	}
}
