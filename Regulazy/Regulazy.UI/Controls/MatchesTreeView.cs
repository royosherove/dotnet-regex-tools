using System.Text.RegularExpressions;
using System.Windows.Forms;
using Regulazy.UI.Properties;

namespace Regulazy.UI.Controls
{
    public class MatchesTreeView : TreeView
    {
        
        private const string ICON_MATCH_EMPTY = "EmptyMatch";
        private const string ICON_GROUP = "FullMatch";
        private const string ICON_CAPTURE = "Capture";
        private ImageList lstImg;

        public MatchesTreeView()
        {
            InitializeComponent();
            
            lstImg.Images.Add("FullMatch", Resources.FullMatch);
            lstImg.Images.Add("Capture", Resources.Capture);
            lstImg.Images.Add("EmptyMatch", Resources.EmptyMatch);
            lstImg.Images.Add("Info", Resources.Info);
            lstImg.Images.Add("Group", Resources.Group);
            ImageList = lstImg;
            AddTreeNode("No Matches", null, ICON_MATCH_EMPTY);
            
        }
        
        public void ShowMatches(MatchCollection matches,Regex creator)
        {
            FillTreeWithMatches(matches,creator);
        }

        private System.ComponentModel.IContainer components;

        private int AddTreeNode(string nodeText, Match currentMatchForNode, string imageKey)
        {

            TreeNode t = new TreeNode(nodeText);
            t.ImageKey = imageKey;
            t.SelectedImageKey= imageKey;
            Nodes.Add(t);
            int ThisNode = Nodes.Count - 1;
            Nodes[ThisNode].Tag = currentMatchForNode;

            return ThisNode;

        }

        private void AddSubNode(int parentNodeIndex, string caption, Group captureGroup, int groupIndex)
        {
            TreeNode newNode = makeCaptureNode(caption);

            Nodes[parentNodeIndex].Nodes.Add(newNode);
            Nodes[parentNodeIndex].Nodes[groupIndex - 1].Tag = captureGroup;
            Nodes[parentNodeIndex].Expand();
        }

        private static TreeNode makeCaptureNode(string caption)
        {
            TreeNode newNode = new TreeNode(caption);
            newNode.ImageKey = ICON_CAPTURE;
            newNode.SelectedImageKey= ICON_CAPTURE;
            return newNode;
        }

        private TreeNode AddSubNode(int parentNodeIndex, string caption, Capture matchCapture, int groupIndex, int captureIndex)
        {
            TreeNode node = Nodes[parentNodeIndex].Nodes[groupIndex - 1];
            TreeNode subNode = makeCaptureNode(caption);
            node.Nodes.Add(subNode);
            node.Nodes[captureIndex].Tag = matchCapture;
            return subNode;
        }
        private void ClearTreeNodes()
        {
            Nodes.Clear();
        }

        private void FillTreeWithMatches(MatchCollection found, Regex CreatingRegexObject)
        {
            ClearTreeNodes();
            foreach (Match m in found)
            {
                if (m.Value.Length > 0)
                {

                    int ThisNode = AddTreeNode("[" + m.Value + "]", m, ICON_GROUP);

                    if (m.Groups.Count > 1)
                    {
                        for (int i = 1; i < m.Groups.Count; i++)
                        {
                            string SubNodeText = CreatingRegexObject.GroupNameFromNumber(i) + ": [" + m.Groups[i].Value + "]";
                            AddSubNode(ThisNode, SubNodeText, m.Groups[i], i);

                            //This bit of code puts in another level of nodes showing the captures for each group
                            int Number = m.Groups[i].Captures.Count;
                            if (Number > 1 /*&& AppContext.Instance.Settings.FillUnNamedCapturesInTree*/)
                            {
                                for (int j = 0; j < Number; j++)
                                {
                                    TreeNode newNode = 
                                        AddSubNode(ThisNode, m.Groups[i].Captures[j].Value, m.Groups[i].Captures[j], i, j);
                                    
                                }
                            }
                        }
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lstImg = new System.Windows.Forms.ImageList(components);
            SuspendLayout();
            // 
            // lstImg
            // 
            lstImg.ColorDepth = ColorDepth.Depth8Bit;
            lstImg.ImageSize = new System.Drawing.Size(16, 16);
            lstImg.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MatchesTreeView
            // 
            Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            ResumeLayout(false);

        }

    }
}
