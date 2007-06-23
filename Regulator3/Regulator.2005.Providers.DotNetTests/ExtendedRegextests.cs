using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Text.RegularExpressions;
using Regulator2005.Core;

namespace Regulator2005.Tests
{
	[TestFixture]
	public class ExtendedRegexTests
	{
		#region Contructor tests
		[Test]
		public void Ctor1Param_PatternIsInitialized()
		{
			ExtendedRegex ex = new ExtendedRegex("");
			Assert.AreEqual(string.Empty, ex.Pattern);
		}

		[Test]
		public void Ctor2Param2_PatternIsInitialized()
		{
			ExtendedRegex ex = new ExtendedRegex("", RegexOptions.None);
			Assert.AreEqual(string.Empty, ex.Pattern);
		}

		[Test]
		public void Ctor1Param_PatternIsInitializedNotEmpty()
		{
			ExtendedRegex ex = new ExtendedRegex("a");
			Assert.AreEqual("a", ex.Pattern);
		}

		[Test]
		public void Ctor2Param2_PatternIsInitializedNotEmpty()
		{
			ExtendedRegex ex = new ExtendedRegex("a", RegexOptions.None);
			Assert.AreEqual("a", ex.Pattern);
		}

		#endregion
	}

	
}
