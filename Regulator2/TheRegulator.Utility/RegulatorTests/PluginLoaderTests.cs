using System;
using NUnit.Framework;
using Regulator.SDK;

namespace Regulator.Tests
{
	[TestFixture]
	public class PluginLoaderTests
	{
		PluginLoader loader = null;
		[SetUp]
		public void Test()
		{
			loader = new PluginLoader();
			loader.FileName="plugins.xml";
		}
		[Test]
		public void TestReadPluginFile()
		{
			
			bool b = loader.ReadFile();
			Assert.IsTrue(b);
			Assert.IsTrue(loader.PluginCount==1);
			Assert.IsTrue(loader.m_PluginNames[0].Equals("Regulator.Tests.TestPlugin, RegulatorTests"));
		}

		[Test]
		public void TestClearPluginNamesWhenLoading()
		{
			bool b = loader.ReadFile();
			Assert.IsTrue(b);
			Assert.IsTrue(loader.PluginCount==1);

			b = loader.ReadFile();
			Assert.IsTrue(b);
			Assert.IsTrue(loader.PluginCount==1);
		        
		}

		
		[Test]
		public void TestReadBadPluginFile()
		{

			loader.FileName="bad file";
			bool b = loader.ReadFile();
			Assert.IsFalse(b);
			Assert.IsTrue(loader.PluginCount==0);
		        
		}
		[Test]
		public void TestLoadPlugins()
		{
			loader.ReadFile();
			Assert.IsTrue(loader.PluginCount==1);
			Assert.IsTrue(loader.LoadPlugins(),"Could not load TestPlugin!");
		    Assert.IsTrue(loader.LoadedPlugins.Count==1);
			Assert.IsNotNull(loader.LoadedPlugins[0]);
		}

	}
}
