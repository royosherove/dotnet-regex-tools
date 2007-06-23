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
	public class RegexController_Replace:DotNetControllerTestFixtureBase
	{

		protected override string getTestedMethodName()
		{
			return "OnReplace";
		}
		protected override void executeTestedMethod()
		{
			m_controller.ExecuteReplace();
		}

		[Test]
		public void PassedInReplaceArsAreNotNull()
		{
			setup_initControllerWithViewInputAndPattern(this, "a", "a");
			m_controller.ReplaceWithText = "b";

			executeTestedMethod();
			Assert.IsNotNull(passedInReplaceArgs);
		}

		[Test]
		public void PassedInReplaceArsAreCorrect()
		{
			setup_initControllerWithViewInputAndPattern(this, "a", "a");
			m_controller.ReplaceWithText = "b";

			executeTestedMethod();
			NReplaceEventArgs args = (NReplaceEventArgs)passedInReplaceArgs;
			Assert.AreEqual("b", args.OutputText);
		}
		
		[Test]
		public void PassedInReplaceArsAreCorrect_Angle2()
		{
			setup_initControllerWithViewInputAndPattern(this, "a", "a");
			m_controller.ReplaceWithText = "c";

			executeTestedMethod();
			NReplaceEventArgs args = (NReplaceEventArgs)passedInReplaceArgs;
			Assert.AreEqual("c", args.OutputText);
		}

	}
}
