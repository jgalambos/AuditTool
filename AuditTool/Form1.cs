﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace AuditTool {
    public partial class Form1 : Form {
        bool testing = true;
        const string LOGDIR = @"c:\images\Auditlog";
        const string LOGPATH = @"C:\images\Auditlog\Log.txt";
        const string SCANPATH = @"C:\images";
        List<string> drives = new List<string>();
        System.Threading.Thread ScanThread;
        public Form1() {
            InitializeComponent();
            ScanThread = new System.Threading.Thread(Scan);
            ScanThread.IsBackground = true;
            LabelReadout.Text = "";
        }

        private void AddToErrorList(string text) {
            if (RichTextBoxErrorReadout.InvokeRequired) {
                RichTextBoxErrorReadout.Invoke(new MethodInvoker(() => AddToErrorList(text)));
                return;
            }
            if (text == "") {
                RichTextBoxErrorReadout.Text = "";
            } else {
                RichTextBoxErrorReadout.Text += text + Environment.NewLine;
            }
        }

        private void AddNodeToTree(FileNode node) {
            if (TreeViewMain.InvokeRequired) {
                TreeViewMain.Invoke(new MethodInvoker(() => AddNodeToTree(node)));
                return;
            }
            TreeViewMain.Nodes.Add(node);
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
            ScanThread.Start();
        }

        private void Scan() {
            drives.Clear();
            DriveInfo[] readout = DriveInfo.GetDrives();
            foreach (DriveInfo i in readout) {
                if (checkBoxScanExternal.Checked) {
                    if (i.DriveType == DriveType.Fixed || i.DriveType == DriveType.Removable) {
                        drives.Add(i.ToString());
                    }
                } else {
                    if (i.DriveType == DriveType.Fixed) {
                        drives.Add(i.ToString());
                    }
                }
            }
            AddToErrorList("");
            TreeViewMain.Nodes.Clear();
            FileNode RootNode = new FileNode();
            //DirectoryInfo dir = new DirectoryInfo(@"C:\");
            if (testing) {
                DirectoryInfo dir = new DirectoryInfo(SCANPATH);
                RootNode = new FileNode(dir);
                GetFileStructure(RootNode);
                AddNodeToTree(RootNode);
            } else {
                foreach (string s in drives) {
                    DirectoryInfo dir = new DirectoryInfo(s);
                    RootNode = new FileNode(dir);
                    GetFileStructure(RootNode);
                    AddNodeToTree(RootNode);
                }
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
                    AddToErrorList(ex.Message);
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

        private void TreeViewMain_AfterSelect(object sender, TreeViewEventArgs e) {
            if (TreeViewMain.SelectedNode != null) {
                float tempSize = ((FileNode)(TreeViewMain.SelectedNode)).Size;
                LabelReadout.Text = "";
                LabelReadout.Text += "Name: " + ((FileNode)(TreeViewMain.SelectedNode)).Name + Environment.NewLine;
                LabelReadout.Text += "Path: " + ((FileNode)(TreeViewMain.SelectedNode)).Path + Environment.NewLine;
                if (tempSize > 100000000) {
                    tempSize /= 1000000000;
                    LabelReadout.Text += "Size: " + tempSize.ToString("0.000") + " GB" + Environment.NewLine;
                } else if (tempSize > 100000) {
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

        private void button3_Click(object sender, EventArgs e) {
            ScanThread.Abort();
            RichTextBoxErrorReadout.Text = "";
        }

        private void button4_Click(object sender, EventArgs e) {
            JsonSerializer serializer = new JsonSerializer();
            
            Directory.CreateDirectory(LOGDIR);
            using (StreamWriter stream = new StreamWriter(LOGPATH))
            using (JsonWriter json = new JsonTextWriter(stream)) {
                foreach (FileNode fn in TreeViewMain.Nodes) {
                    serializer.Serialize(json, JsonConvert.SerializeObject(fn, Formatting.Indented));
                    RecursiveSerialize(fn, serializer, json);
                }
                RichTextBoxErrorReadout.Text += "Done Serializing";
            }
        }

        private void RecursiveSerialize(FileNode node, JsonSerializer serializer, JsonWriter json) {
            serializer.Serialize(json, JsonConvert.SerializeObject(node, Formatting.Indented));
            foreach(FileNode fn in node.Nodes) {
                RecursiveSerialize(fn, serializer, json);
            }
        }
    }
}