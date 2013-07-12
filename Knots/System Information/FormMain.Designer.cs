using System.Resources;
using System.Globalization;
using System.Threading;
using System.IO;

namespace SystemInformation
{
    partial class FormMain : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        public CultureInfo en = new CultureInfo("en");
        public CultureInfo de = new CultureInfo("de");

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("BIOS", 2, 2);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("CPU", 3, 3);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Drives", 4, 4);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Network", 5, 5);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Sound", 6, 6);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Video", 7, 7);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Computer", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Date and Time", 9, 9);
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Installed Programs", 10, 10);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Services", 11, 11);
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Special Folders", 12, 12);
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Startup Programs", 13, 13);
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("User Accounts", 14, 14);
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Visual Styles", 15, 15);
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Operating System", 16, 16, new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("System Information", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode15});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.treeviewSystemInfo = new System.Windows.Forms.TreeView();
            this.imagelistTree = new System.Windows.Forms.ImageList(this.components);
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslUpTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerTime = new System.Windows.Forms.Timer(this.components);
            this.timerTimeUp = new System.Windows.Forms.Timer(this.components);
            this.ucTop = new SystemInformation.TopControl();
            this.ucBottom = new SystemInformation.BottomControl();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            this.SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SplitContainer.Location = new System.Drawing.Point(0, 70);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.treeviewSystemInfo);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.SplitContainer.Panel2MinSize = 100;
            this.SplitContainer.Size = new System.Drawing.Size(798, 456);
            this.SplitContainer.SplitterDistance = 242;
            this.SplitContainer.SplitterWidth = 3;
            this.SplitContainer.TabIndex = 0;
            // 
            // treeviewSystemInfo
            // 
            this.treeviewSystemInfo.BackColor = System.Drawing.Color.White;
            this.treeviewSystemInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeviewSystemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeviewSystemInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeviewSystemInfo.ImageIndex = 0;
            this.treeviewSystemInfo.ImageList = this.imagelistTree;
            this.treeviewSystemInfo.Location = new System.Drawing.Point(0, 0);
            this.treeviewSystemInfo.Name = "treeviewSystemInfo";
            treeNode1.BackColor = System.Drawing.Color.Transparent;
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "BIOS";
            treeNode1.SelectedImageIndex = 2;
            treeNode1.Text = "BIOS";
            treeNode1.ToolTipText = "Information about the Basic Input Output System .";
            treeNode2.BackColor = System.Drawing.Color.Transparent;
            treeNode2.ImageIndex = 3;
            treeNode2.Name = "CPU";
            treeNode2.SelectedImageIndex = 3;
            treeNode2.Text = "CPU";
            treeNode2.ToolTipText = "Information about the Central Processing Unit (Processor).";
            treeNode3.BackColor = System.Drawing.Color.Transparent;
            treeNode3.ImageIndex = 4;
            treeNode3.Name = "Drives";
            treeNode3.SelectedImageIndex = 4;
            treeNode3.Text = "Drives";
            treeNode3.ToolTipText = "Information about hard drives, removable drives and volumes.";
            treeNode4.BackColor = System.Drawing.Color.Transparent;
            treeNode4.ImageIndex = 5;
            treeNode4.Name = "Network";
            treeNode4.SelectedImageIndex = 5;
            treeNode4.Text = "Network";
            treeNode4.ToolTipText = "Information about network interfaces.";
            treeNode5.BackColor = System.Drawing.Color.Transparent;
            treeNode5.ImageIndex = 6;
            treeNode5.Name = "Sound";
            treeNode5.SelectedImageIndex = 6;
            treeNode5.Text = "Sound";
            treeNode5.ToolTipText = "Information about sound controllers.";
            treeNode6.BackColor = System.Drawing.Color.Transparent;
            treeNode6.ImageIndex = 7;
            treeNode6.Name = "Video";
            treeNode6.SelectedImageIndex = 7;
            treeNode6.Text = "Video";
            treeNode6.ToolTipText = "Information about video controllers.";
            treeNode7.BackColor = System.Drawing.Color.Transparent;
            treeNode7.Checked = true;
            treeNode7.ImageIndex = 1;
            treeNode7.Name = "Computer";
            treeNode7.SelectedImageIndex = 1;
            treeNode7.Text = "Computer";
            treeNode7.ToolTipText = "General information about this computer.";
            treeNode8.ImageIndex = 9;
            treeNode8.Name = "DateTime";
            treeNode8.SelectedImageIndex = 9;
            treeNode8.Text = "Date and Time";
            treeNode8.ToolTipText = "Date and time information.";
            treeNode9.ImageIndex = 10;
            treeNode9.Name = "Installed Programs";
            treeNode9.SelectedImageIndex = 10;
            treeNode9.Text = "Installed Programs";
            treeNode9.ToolTipText = "List of software installed on this computer.";
            treeNode10.ImageIndex = 11;
            treeNode10.Name = "Services";
            treeNode10.SelectedImageIndex = 11;
            treeNode10.Text = "Services";
            treeNode10.ToolTipText = "List of Windows and third-party services installed on this computer.";
            treeNode11.BackColor = System.Drawing.Color.Transparent;
            treeNode11.ImageIndex = 12;
            treeNode11.Name = "SpecialFolders";
            treeNode11.SelectedImageIndex = 12;
            treeNode11.Text = "Special Folders";
            treeNode11.ToolTipText = "List of Windows® Special Folders and their paths.";
            treeNode12.ImageIndex = 13;
            treeNode12.Name = "Startup";
            treeNode12.SelectedImageIndex = 13;
            treeNode12.Text = "Startup Programs";
            treeNode12.ToolTipText = "Programs that run when you login.";
            treeNode13.ImageIndex = 14;
            treeNode13.Name = "UserAccounts";
            treeNode13.SelectedImageIndex = 14;
            treeNode13.Text = "User Accounts";
            treeNode13.ToolTipText = "User accounts and privilege levels.";
            treeNode14.ImageIndex = 15;
            treeNode14.Name = "VisualStyles";
            treeNode14.SelectedImageIndex = 15;
            treeNode14.Text = "Visual Styles";
            treeNode14.ToolTipText = "Information about the visual style currently in use.";
            treeNode15.BackColor = System.Drawing.Color.Transparent;
            treeNode15.ImageIndex = 16;
            treeNode15.Name = "OperatingSystem";
            treeNode15.SelectedImageIndex = 16;
            treeNode15.Text = "Operating System";
            treeNode15.ToolTipText = "Information about the operating system.";
            treeNode16.BackColor = System.Drawing.Color.Transparent;
            treeNode16.ImageIndex = 0;
            treeNode16.Name = "SystemInformation";
            treeNode16.SelectedImageIndex = 0;
            treeNode16.Text = "System Information";
            this.treeviewSystemInfo.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode16});
            this.treeviewSystemInfo.SelectedImageIndex = 0;
            this.treeviewSystemInfo.ShowPlusMinus = false;
            this.treeviewSystemInfo.Size = new System.Drawing.Size(242, 456);
            this.treeviewSystemInfo.TabIndex = 0;
            this.treeviewSystemInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeviewSystemInfo_AfterSelect);
            // 
            // imagelistTree
            // 
            this.imagelistTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagelistTree.ImageStream")));
            this.imagelistTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imagelistTree.Images.SetKeyName(0, "System Information_16x16.png");
            this.imagelistTree.Images.SetKeyName(1, "Computer_16x16.png");
            this.imagelistTree.Images.SetKeyName(2, "");
            this.imagelistTree.Images.SetKeyName(3, "processor16.png");
            this.imagelistTree.Images.SetKeyName(4, "Drive_16x16.png");
            this.imagelistTree.Images.SetKeyName(5, "Network_16x16.png");
            this.imagelistTree.Images.SetKeyName(6, "");
            this.imagelistTree.Images.SetKeyName(7, "Video_16x16.png");
            this.imagelistTree.Images.SetKeyName(8, "");
            this.imagelistTree.Images.SetKeyName(9, "Date-Time_16x16.png");
            this.imagelistTree.Images.SetKeyName(10, "Installed Programs_16x16.png");
            this.imagelistTree.Images.SetKeyName(11, "Services_16x16.png");
            this.imagelistTree.Images.SetKeyName(12, "SpecialFolder_16x16.png");
            this.imagelistTree.Images.SetKeyName(13, "Startup_16x16.png");
            this.imagelistTree.Images.SetKeyName(14, "Users_16x16.png");
            this.imagelistTree.Images.SetKeyName(15, "VisualStyles_16x16.png");
            this.imagelistTree.Images.SetKeyName(16, "Windows_16x16.png");
            // 
            // StatusStrip
            // 
            this.StatusStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslDateTime,
            this.tsslUpTime});
            this.StatusStrip.Location = new System.Drawing.Point(0, 526);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(798, 22);
            this.StatusStrip.SizingGrip = false;
            this.StatusStrip.TabIndex = 3;
            this.StatusStrip.Text = "StatusStrip";
            // 
            // tsslDateTime
            // 
            this.tsslDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslDateTime.Name = "tsslDateTime";
            this.tsslDateTime.Size = new System.Drawing.Size(0, 17);
            // 
            // tsslUpTime
            // 
            this.tsslUpTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslUpTime.Name = "tsslUpTime";
            this.tsslUpTime.Size = new System.Drawing.Size(0, 17);
            // 
            // timerTime
            // 
            this.timerTime.Enabled = true;
            this.timerTime.Interval = 1000;
            this.timerTime.Tick += new System.EventHandler(this.timerTime_Tick);
            // 
            // timerTimeUp
            // 
            this.timerTimeUp.Interval = 1000;
            this.timerTimeUp.Tick += new System.EventHandler(this.timerTimeUp_Tick);
            // 
            // ucTop
            // 
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTop.Location = new System.Drawing.Point(0, 0);
            this.ucTop.Name = "ucTop";
            this.ucTop.Size = new System.Drawing.Size(798, 64);
            this.ucTop.TabIndex = 4;
            // 
            // ucBottom
            // 
            this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucBottom.Location = new System.Drawing.Point(0, 548);
            this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
            this.ucBottom.MaximumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.MinimumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.Name = "ucBottom";
            this.ucBottom.Size = new System.Drawing.Size(798, 31);
            this.ucBottom.TabIndex = 5;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(798, 579);
            this.Controls.Add(this.ucTop);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.SplitContainer);
            this.Controls.Add(this.ucBottom);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;

           
            
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "System Information";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.ResumeLayout(false);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        System.Windows.Forms.SplitContainer SplitContainer;
        System.Windows.Forms.StatusStrip StatusStrip;
        System.Windows.Forms.ToolStripStatusLabel tsslDateTime;
        System.Windows.Forms.ImageList imagelistTree;
        System.Windows.Forms.TreeView treeviewSystemInfo;
        System.Windows.Forms.Timer timerTime;
        System.Windows.Forms.ToolStripStatusLabel tsslUpTime;
        System.Windows.Forms.Timer timerTimeUp;
        TopControl ucTop;
        BottomControl ucBottom;
    }
}
