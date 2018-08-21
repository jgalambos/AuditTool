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
            TreeNode RootNode = new TreeNode(@"C:\Repositories");
            TreeViewMain.Nodes.Add(RootNode);
            LabelReadout.Text = GetDirectoriesAndFiles(@"C:\Repositories");
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
