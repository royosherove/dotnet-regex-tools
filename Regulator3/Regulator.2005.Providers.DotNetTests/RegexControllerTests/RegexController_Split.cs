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

namespace Regulator2005.Tests.RegexControllerTests
{
	[TestFixture]
	public class RegexController_Split
		:DotNetControllerTestFixtureBase
	{

		protected override string getTestedMethodName()
		{
			return "OnSplit";
		}

		protected override void executeTestedMethod()
		{
			m_controller.ExecuteSplit();
		}

		[Test]
		public void RegexOptionsIgnoringWhiteSpaceAreNotIgnored()
		{
			setup_initControllerWithViewInputAndPattern(this, "XaaX", "a a");
			m_controller.DotNetRegexOptions = RegexOptions.IgnorePatternWhitespace;
			executeTestedMethod();
			Assert.AreEqual(2, passedInSplitArgs.Results.Length);
		}

		[Test]
		public void RegexOptionsNotIgnoringWhiteSpaceAreNotIgnored()
		{
			setup_initControllerWithViewInputAndPattern(this, "XaaX", "a a");
			m_controller.DotNetRegexOptions = RegexOptions.None;
			executeTestedMethod();
			Assert.AreEqual(1, passedInSplitArgs.Results.Length);
		}
		[Test]
		public void Split_SplitEventIsPassedWith2SplitResultsOnEmptyValues()
		{
			DynamicMock viewMock = setup_createEmptyViewMock();
			SplitArgsConstraint expectedArgs = new SplitArgsConstraint();
			expectedArgs.ArrayCount = 2;
			viewMock.Expect("OnSplit", expectedArgs);

			setup_attachMockViewToController(viewMock, m_controller);

			m_controller.ExecuteSplit();
			viewMock.Verify();
		}

		[Test]
		public void Create()
		{
			Assert.IsNotNull(m_controller);
		}


		[Test]
		public void Split_SplitEventIsPassedWith3SplitResults()
		{
			DynamicMock viewMock = setup_createEmptyViewMock();
			SplitArgsConstraint expectedArgs = new SplitArgsConstraint();
			expectedArgs.ArrayCount = 3;
			viewMock.Expect("OnSplit", expectedArgs);

			setup_attachMockViewToController(viewMock, m_controller);


			m_controller.Pattern = ",";
			m_controller.InputText = "a,b,c";

			m_controller.ExecuteSplit();
			viewMock.Verify();
		}


		[Test]
		public void Split_SplitEventNotThrownWithoutCallingSplit()
		{
			DynamicMock viewMock = setup_createEmptyViewMock();
			viewMock.ExpectNoCall("OnSplit", typeof(IRegexView));
			setup_attachMockViewToController(viewMock, m_controller);

		}

		#region private methods and constraints for the tplit method testing
		private class SplitArgsConstraint : IConstraint
		{
			#region IConstraint Members

			public int ArrayCount = 0;
			public bool Eval(object val)
			{
				if (val == null)
				{
					return false;
				}
				NSplitEventArgs args = ((NSplitEventArgs)val);
				if (args.Results == null)
				{
					return false;
				}
				return args.Results.Length == ArrayCount;
			}

			public object ExtractActualValue(object actual)
			{
				if (null == actual)
				{
					return null;
				}
				return ((NSplitEventArgs)actual).Results.Length.ToString();
			}

			public string Message
			{
				get { return " Result count of " + ArrayCount; }
			}

			#endregion
		}

		

		#endregion


		}
}
