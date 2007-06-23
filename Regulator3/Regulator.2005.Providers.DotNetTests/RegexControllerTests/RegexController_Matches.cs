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
	public class RegexController_Matches
		:DotNetControllerTestFixtureBase
	{
		protected override void executeTestedMethod()
		{
			m_controller.ExecuteMatches();
		}
		protected override  string getTestedMethodName()
		{
			return "OnMatches";
		}

		[Test]
		public void PassedInArgsMatchCountIs1()
		{
			setup_initControllerWithViewInputAndPattern(this,"a","a");
			verify_ExecuteAndCheckMatchCountIs(1);
		}

		[Test]
		public void RegexOptionsAreotIgnored()
		{
			setup_initControllerWithViewInputAndPattern(this, "aa", "a a");
			m_controller.DotNetRegexOptions = RegexOptions.IgnorePatternWhitespace;
			verify_ExecuteAndCheckMatchCountIs(1);
		}

		[Test]
		public void RegexObjectIsPassedInArgs()
		{
			setup_initControllerWithViewInputAndPattern(this, "a", "a");
			executeTestedMethod();
			Assert.IsNotNull(((NMatchesEventArgs)passedInMatchesArgs).RegexInstance,
				"Matches comment should have passed in a live Regex insance to use for vairous data");
		}
		[Test]
		public void PassedInArgsMatchCountIs2()
		{
			setup_initControllerWithViewInputAndPattern(this, "a,a", "a");
			verify_ExecuteAndCheckMatchCountIs(2);
		}

		private void verify_ExecuteAndCheckMatchCountIs(int expectedMatchCount)
		{
			executeTestedMethod();
			Assert.AreEqual(expectedMatchCount, ((NMatchesEventArgs)passedInMatchesArgs).Matches.Count);
		}

	


	}
}
