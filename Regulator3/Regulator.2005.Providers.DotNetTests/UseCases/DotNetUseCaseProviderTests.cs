using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Regulator2005.Core.DotNet;
using Regulator._2005.Providers.DotNet.UseCases;

namespace Regulator._2005.Providers.DotNetTests.UseCases
{
	[TestFixture]
	public class DotNetUseCaseProviderTests
	{
		DotNetUseCaseProvider provider = null;

		[SetUp]
		public void init()
		{
			provider = new DotNetUseCaseProvider();
		}
		[Test]
		public void Create()
		{
			DotNetUseCaseProvider provider = new DotNetUseCaseProvider();
			Assert.IsNotNull(provider);
		}

		[Test]
		public void ReturnsOneUseCase()
		{
			Assert.AreEqual(1,provider.GetAvailableUseCases().Count);
		}

		[Test]
		public void ReturnsReplaceUseCaseFirst()
		{
			Assert.IsTrue(provider.GetAvailableUseCases()[0] is ReplaceUseCase);

		}
	}
}
