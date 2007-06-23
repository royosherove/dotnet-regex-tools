using System;
using System.Windows.Forms;
using RegexWizard.Framework;

namespace Regulazy.UI.Controls
{
    public class ScopeTreeView : TreeView
    {
        public void ShowScope(Scope root)
        {
            Nodes.Clear();
            AddScope(root, null);
        }

        private void AddScope(Scope root, TreeNode parentNode)
        {
            TreeNode n = null;
            if (parentNode == null)
            {
                n = Nodes.Add(key(root), "Root:" + root.Text.Replace("\n","\\n"));
                n.Tag = root;
            }
            if (parentNode != null)
                n = parentNode;
            
            if (root != null)
            {
                addScopeLeaf(n, "Left: ", root.InnerLeftScope);
                addScopeLeaf(n, "Middle: ", root.InnerMiddleScope);
                addScopeLeaf(n, "Right: ", root.InnerRightScope);
            }

            n.Expand();
        }


        private void addScopeLeaf(TreeNode parentTreeNode, string label, Scope scope)
        {
            if (scope == null) return;

            TreeNode newNode = parentTreeNode.Nodes.Add(key(scope),"["+ scope.StartPosInRootScope + "-" + scope.Length +"]" + scope.Text.Replace("\n", "\\n"));
            newNode.Tag = scope;
            AddScope(scope, newNode);
        }

        public void Highlight(Scope s)
        {
            HideSelection = false;
            TreeNode[] found = Nodes.Find(key(s),true);
            if(found.Length==0)
                return;
            TreeNode foundFirst = found[0];
            this.SelectedNode = foundFirst;
            
        }

        private string key(Scope s)
        {
            return s.StartPosInRootScope + " " + s.Length;
        }
    }
}
