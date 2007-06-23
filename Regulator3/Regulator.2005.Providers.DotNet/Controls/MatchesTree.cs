using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Regulator2005.Core.Args.DotNet;

namespace Regulator2005.Framework.Controls
{
	public partial class MatchesTree : UserControl
	{
		public MatchesTree()
		{
			InitializeComponent();
		}



		public TreeView TreeView
		{
			get { return tree; }
		}



		public void Display(NMatchesEventArgs args)
		{
			if (args.Matches == null)
				return;

			foreach (Match match in args.Matches)
			{
				string prefix = string.Empty;
				TreeNode MatchRoot = new TreeNode(string.Format("[{0}]",match.Value));
				tree.Nodes.Add(MatchRoot);
				DisplayGroups(args, match, MatchRoot);
				MatchRoot.Expand();
			}
		}

		private static void DisplayGroups(NMatchesEventArgs args, Match match, TreeNode MatchRoot)
		{
			const int DEFAULT_GROUP_INDEX = 1;
			if (match.Groups.Count <= DEFAULT_GROUP_INDEX)
				return;

			for (int i = DEFAULT_GROUP_INDEX; i < match.Groups.Count; i++)
			{
				Group group = match.Groups[i];
				string groupName = args.RegexInstance.GroupNameFromNumber(i);
				string finalNodeText = string.Empty;

				if(groupName!=string.Empty)
					finalNodeText= string.Format("{1}:[{0}]", group.Value, groupName);
				else
					finalNodeText = string.Format("{1}:[{0}]", group.Value, i);

				TreeNode groupRoot = new TreeNode(finalNodeText);
				MatchRoot.Nodes.Add(groupRoot);

				DisplayCaptures(group, groupRoot);
			}
		}

		private static void DisplayCaptures(Group group, TreeNode groupRoot)
		{
			foreach (Capture capture in group.Captures)
			{
				TreeNode captureRoot = new TreeNode(capture.Value);
				groupRoot.Nodes.Add(captureRoot);

			}
		}
	}
}
