namespace AuditTool {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.button1 = new System.Windows.Forms.Button();
            this.LabelReadout = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.PanelTreeView = new System.Windows.Forms.Panel();
            this.TreeViewMain = new System.Windows.Forms.TreeView();
            this.PanelControl = new System.Windows.Forms.Panel();
            this.RichTextBoxErrorReadout = new System.Windows.Forms.RichTextBox();
            this.checkBoxScanExternal = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.PanelTreeView.SuspendLayout();
            this.PanelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(36, 593);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get Drives";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LabelReadout
            // 
            this.LabelReadout.Location = new System.Drawing.Point(3, 1);
            this.LabelReadout.Name = "LabelReadout";
            this.LabelReadout.Size = new System.Drawing.Size(327, 125);
            this.LabelReadout.TabIndex = 1;
            this.LabelReadout.Text = "label1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(160, 593);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Scan";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PanelTreeView
            // 
            this.PanelTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelTreeView.Controls.Add(this.TreeViewMain);
            this.PanelTreeView.Location = new System.Drawing.Point(13, 13);
            this.PanelTreeView.Name = "PanelTreeView";
            this.PanelTreeView.Size = new System.Drawing.Size(803, 691);
            this.PanelTreeView.TabIndex = 3;
            // 
            // TreeViewMain
            // 
            this.TreeViewMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeViewMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeViewMain.Location = new System.Drawing.Point(0, 0);
            this.TreeViewMain.Name = "TreeViewMain";
            this.TreeViewMain.Size = new System.Drawing.Size(797, 685);
            this.TreeViewMain.TabIndex = 0;
            this.TreeViewMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewMain_AfterSelect);
            // 
            // PanelControl
            // 
            this.PanelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanelControl.Controls.Add(this.button3);
            this.PanelControl.Controls.Add(this.checkBoxScanExternal);
            this.PanelControl.Controls.Add(this.RichTextBoxErrorReadout);
            this.PanelControl.Controls.Add(this.LabelReadout);
            this.PanelControl.Controls.Add(this.button2);
            this.PanelControl.Controls.Add(this.button1);
            this.PanelControl.Location = new System.Drawing.Point(822, 12);
            this.PanelControl.Name = "PanelControl";
            this.PanelControl.Size = new System.Drawing.Size(333, 686);
            this.PanelControl.TabIndex = 4;
            // 
            // RichTextBoxErrorReadout
            // 
            this.RichTextBoxErrorReadout.Location = new System.Drawing.Point(0, 129);
            this.RichTextBoxErrorReadout.Name = "RichTextBoxErrorReadout";
            this.RichTextBoxErrorReadout.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RichTextBoxErrorReadout.Size = new System.Drawing.Size(330, 458);
            this.RichTextBoxErrorReadout.TabIndex = 3;
            this.RichTextBoxErrorReadout.Text = "";
            // 
            // checkBoxScanExternal
            // 
            this.checkBoxScanExternal.AutoSize = true;
            this.checkBoxScanExternal.Location = new System.Drawing.Point(6, 106);
            this.checkBoxScanExternal.Name = "checkBoxScanExternal";
            this.checkBoxScanExternal.Size = new System.Drawing.Size(134, 17);
            this.checkBoxScanExternal.TabIndex = 4;
            this.checkBoxScanExternal.Text = "Scan removable drives";
            this.checkBoxScanExternal.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(160, 640);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Stop Scan";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 716);
            this.Controls.Add(this.PanelControl);
            this.Controls.Add(this.PanelTreeView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.PanelTreeView.ResumeLayout(false);
            this.PanelControl.ResumeLayout(false);
            this.PanelControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label LabelReadout;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel PanelTreeView;
        private System.Windows.Forms.TreeView TreeViewMain;
        private System.Windows.Forms.Panel PanelControl;
        private System.Windows.Forms.RichTextBox RichTextBoxErrorReadout;
        private System.Windows.Forms.CheckBox checkBoxScanExternal;
        private System.Windows.Forms.Button button3;
    }
}

