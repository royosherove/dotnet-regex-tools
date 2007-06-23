using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock;
using Regulator2005.Core;
using Regulator2005.Core.Interfaces;
using NMock.Constraints;
using Regulator2005.Core.Args;
using System.Text.RegularExpressions;

namespace Regulator2005.Tests.ControllerFactoryTests
{
	[TestFixture]
	public class TaskProvidersProviderTests
	{
		[Test]
		public void ReturnsOneProviderByDefault()
		{
			UseCaseProviderProvider factory = new UseCaseProviderProvider();
			Assert.AreEqual(1, factory.GetAvailableProviders().Count, "One provider has to be returned by default");
		}
		[Test]
		public void DotNet()
		{
			UseCaseProviderProvider factory = new UseCaseProviderProvider();
			Assert.AreEqual(1, factory.GetAvailableProviders().Count, "One provider has to be returned by default");
		}
	}
}
