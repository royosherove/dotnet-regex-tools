using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Regulator2005.RegexVisualizers
{
	public partial class MatchesVisualizerForm : Form
	{
		public MatchesVisualizerForm()
		{
			InitializeComponent();
		}


		public void setDisplay(MatchCollection matches)
		{
			foreach (Match match in matches)
			{
				TreeNode MatchRoot = new TreeNode(match.Value);
				treeView1.Nodes.Add(MatchRoot);
				foreach (Group group in match.Groups)
				{
					TreeNode groupRoot = new TreeNode(group.Value);
					MatchRoot.Nodes.Add(groupRoot);
					foreach (Capture capture in group.Captures)
					{
						TreeNode captureRoot = new TreeNode(capture.Value);
						groupRoot.Nodes.Add(captureRoot);

					}
				}
				MatchRoot.Expand();
			}
		}

	}
}