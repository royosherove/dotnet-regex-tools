using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Windows.Forms;
using Regulator2005.Framework.Controls;
using System.Text.RegularExpressions;
using Regulator2005.Core.Args;
using Regulator2005.Core.Args.DotNet;
using Regulator2005.Core;

namespace Regulator2005.Tests
{
	[TestFixture]
	public class MatchesTreeTests
	{
		[Test]
		public void TreeShowsZeroNodesOnZeroMatches()
		{
			MatchesTree mt = new MatchesTree();
			TreeView tree = mt.TreeView;

			mt.Display(new NMatchesEventArgs());
			Assert.AreEqual(0, tree.Nodes.Count);
		}
		[Test]
		public void TreeShowsOneMatchNodeOnOneMatch()
		{
			MatchesTree mt = new MatchesTree();
			TreeView tree = mt.TreeView;

			NMatchesEventArgs args = createNMatchesArgs("a","a");

			mt.Display(args);
			Assert.AreEqual(1, tree.Nodes.Count);
		}

		[Test]
		public void TreeShowsOneMatchTextNoBracesIfNoGroupName()
		{
			MatchesTree mt = new MatchesTree();
			TreeView tree = mt.TreeView;

			NMatchesEventArgs args = createNMatchesArgs("a", "a");

			mt.Display(args);
			Assert.AreEqual("[a]", tree.Nodes[0].Text);
		}

		[Test]
		public void TreeShowsOneMatchWithGroupNameIfExists()
		{
			MatchesTree mt = new MatchesTree();
			TreeView tree = mt.TreeView;

			NMatchesEventArgs args = createNMatchesArgs(@"(?<group>a)", "a");

			mt.Display(args);
			Assert.AreEqual("group:[a]", tree.Nodes[0].Nodes[0].Text);
		}

		private static NMatchesEventArgs createNMatchesArgs(string pattern, string input)
		{
			NMatchesEventArgs args = new NMatchesEventArgs();
			args.RegexInstance = new ExtendedRegex(pattern);
			args.Matches = args.RegexInstance.Matches(input);
			return args;
		}
	}
}
