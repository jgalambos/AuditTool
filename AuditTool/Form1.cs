using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AuditTool {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            DriveInfo[] readout = DriveInfo.GetDrives();
            LabelReadout.Text = "";
            foreach (DriveInfo i in readout) {
                if (i.DriveType == DriveType.Fixed || i.DriveType == DriveType.Removable) {
                    LabelReadout.Text += i.ToString() + " ";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            LabelReadout.Text = "";
            TreeViewMain.Nodes.Clear();
            TreeNode RootNode = new TreeNode(@"C:\");
            GetFileStructure(RootNode);
            TreeViewMain.Nodes.Add(RootNode);
            // LabelReadout.Text = GetDirectoriesAndFiles(@"C:\Repositories");
        }

        private void GetFileStructure(TreeNode root) {
            List<TreeNode> TreeNodes = new List<TreeNode>();
            DirectoryInfo info = new DirectoryInfo(root.Text);
            /*FileInfo[] files = info.GetFiles().OrderBy(p => p.LastAccessTime).ToArray();
            foreach (FileInfo file in files) {
                TreeNodes.Add(new TreeNode(file.FullName));
            }*/
            /*foreach (DirectoryInfo dir in info.GetDirectories()) {
                GetFileStructureRecursive(root);
                TreeNodes.Add(new TreeNode(dir.FullName));
            }*/
            GetFileStructureRecursive(root);
            root.Nodes.AddRange(TreeNodes.ToArray());
            TreeViewMain.Nodes.AddRange(TreeNodes.ToArray());
        }

        private void GetFileStructureRecursive(TreeNode parent) {
            TreeNode value = new TreeNode();
            List<TreeNode> TreeNodes = new List<TreeNode>();
            DirectoryInfo info = new DirectoryInfo(parent.Text);
            /*FileInfo[] files = info.GetFiles().OrderBy(p => p.LastAccessTime).ToArray();
            foreach (FileInfo file in files) {
                TreeNodes.Add(new TreeNode(file.FullName));
            }*/
            try {
                foreach (DirectoryInfo dir in info.GetDirectories()) {
                    value = new TreeNode(dir.FullName);
                    GetFileStructureRecursive(value);
                    TreeNodes.Add(value);
                }
                parent.Nodes.AddRange(TreeNodes.ToArray());
            } catch (UnauthorizedAccessException ex) {
                LabelReadout.Text += ex.Message + Environment.NewLine;
            }

        }

        private string GetDirectoriesAndFiles(string path) {
            List<TreeNode> TreeNodes = new List<TreeNode>();
            List<TreeNode> ChildNode = new List<TreeNode>();
            string value = "";
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
            foreach (FileInfo file in files) {
                value += file.FullName + Environment.NewLine;
                TreeNodes.Add(new TreeNode(file.FullName));
            }
            foreach (DirectoryInfo dir in info.GetDirectories()) {
                value += dir.FullName + Environment.NewLine;
                TreeNodes.Add(new TreeNode(dir.FullName));
                value += GetDirectoriesAndFiles(dir.FullName.ToString());
            }
            TreeViewMain.Nodes.AddRange(TreeNodes.ToArray());
            return value;
        }
    }
}
