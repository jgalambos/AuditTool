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
        List<string> drives = new List<string>();
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            DriveInfo[] readout = DriveInfo.GetDrives();
            LabelReadout.Text = "";
            foreach (DriveInfo i in readout) {
                if (i.DriveType == DriveType.Fixed || i.DriveType == DriveType.Removable) {
                    LabelReadout.Text += i.ToString() + " ";
                    drives.Add(i.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            drives.Clear();
            DriveInfo[] readout = DriveInfo.GetDrives();
            foreach (DriveInfo i in readout) {
                if (i.DriveType == DriveType.Fixed || i.DriveType == DriveType.Removable) {
                    drives.Add(i.ToString());
                }
            }
            RichTextBoxErrorReadout.Text = "";
            TreeViewMain.Nodes.Clear();
            FileNode RootNode = new FileNode();
            //DirectoryInfo dir = new DirectoryInfo(@"C:\");
            foreach (string s in drives) {
                DirectoryInfo dir = new DirectoryInfo(s);
                RootNode = new FileNode(dir);
                GetFileStructure(RootNode);
                TreeViewMain.Nodes.Add(RootNode);
            }
        }

        private void GetFileStructure(FileNode root) {
            List<FileNode> FileNodes = new List<FileNode>();
            DirectoryInfo info = new DirectoryInfo(root.Text);
            GetFileStructureRecursive(root);
            root.Nodes.AddRange(FileNodes.ToArray());
            TreeViewMain.Nodes.AddRange(FileNodes.ToArray());
        }

        private void GetFileStructureRecursive(FileNode parent) {
            FileNode value = new FileNode();
            List<FileNode> FileNodes = new List<FileNode>();
            DirectoryInfo info = new DirectoryInfo(parent.Path);
            foreach (DirectoryInfo dir in info.EnumerateDirectories()) {
                try {
                    value = new FileNode(dir);
                    GetFileStructureRecursive(value);
                    FileNodes.Add(value);
                } catch (UnauthorizedAccessException ex) {
                    RichTextBoxErrorReadout.Text += ex.Message + Environment.NewLine;
                }
            }
            try {
                FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();

                foreach (FileInfo file in files) {
                    value = new FileNode(file);
                    FileNodes.Add(value);
                }
                parent.Nodes.AddRange(FileNodes.ToArray());
            } catch (UnauthorizedAccessException ex) {
                // Not sure what the best way to get around this is.  I should have access.
                RichTextBoxErrorReadout.Text += ex.Message + Environment.NewLine;
            }

        }
        /*
        private string GetDirectoriesAndFiles(string path) {
            List<FileNode> FileNodes = new List<FileNode>();
            List<FileNode> ChildNode = new List<FileNode>();
            string value = "";
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
            foreach (FileInfo file in files) {
                value += file.FullName + Environment.NewLine;
                FileNodes.Add(new FileNode(file.FullName));
            }
            foreach (DirectoryInfo dir in info.GetDirectories()) {
                value += dir.FullName + Environment.NewLine;
                FileNodes.Add(new FileNode(dir.FullName));
                value += GetDirectoriesAndFiles(dir.FullName.ToString());
            }
            TreeViewMain.Nodes.AddRange(FileNodes.ToArray());
            return value;
        }
        */
        private void TreeViewMain_AfterSelect(object sender, TreeViewEventArgs e) {
            if (TreeViewMain.SelectedNode != null) {
                float tempSize = ((FileNode)(TreeViewMain.SelectedNode)).Size;
                LabelReadout.Text = "";
                LabelReadout.Text += "Name: " + ((FileNode)(TreeViewMain.SelectedNode)).Name + Environment.NewLine;
                LabelReadout.Text += "Path: " + ((FileNode)(TreeViewMain.SelectedNode)).Path + Environment.NewLine;
                if (tempSize > 100000) {
                    tempSize /= 1000000;
                    LabelReadout.Text += "Size: " + tempSize.ToString("0.000") + " MB" + Environment.NewLine;
                } else if (tempSize > 100) {
                    tempSize /= 1000;
                    LabelReadout.Text += "Size: " + tempSize.ToString("0.000") + " KB" + Environment.NewLine;
                } else {
                    LabelReadout.Text += "Size: " + tempSize + " bytes" + Environment.NewLine;
                }
                LabelReadout.Text += "Date Created: " + ((FileNode)(TreeViewMain.SelectedNode)).CreateDate + Environment.NewLine;
                LabelReadout.Text += "Date Modified: " + ((FileNode)(TreeViewMain.SelectedNode)).ModifiedDate + Environment.NewLine;
                if (((FileNode)(TreeViewMain.SelectedNode)).Hidden)
                    LabelReadout.Text += "This file is Hidden.";
                
            }
        }
    }
}
