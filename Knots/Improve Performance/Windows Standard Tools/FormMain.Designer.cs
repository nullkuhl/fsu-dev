using System.Windows.Forms;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace WindowsStandardTools
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

        public ResourceManager rm = new ResourceManager("WindowsStandardTools.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.spcMain = new System.Windows.Forms.SplitContainer();
            this.ucTop = new WindowsStandardTools.TopControl();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblData = new System.Windows.Forms.Label();
            this.lblAdd = new System.Windows.Forms.Label();
            this.CheckDiskNote = new System.Windows.Forms.Label();
            this.DiskDefragNote = new System.Windows.Forms.Label();
            this.DiskDefrag = new System.Windows.Forms.Button();
            this.SysRestore = new System.Windows.Forms.Button();
            this.SysRestoreNote = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblSpacer = new System.Windows.Forms.Label();
            this.SysFileChecker = new System.Windows.Forms.Button();
            this.SysFileCheckerNote = new System.Windows.Forms.Label();
            this.Backup = new System.Windows.Forms.Button();
            this.BackupNote = new System.Windows.Forms.Label();
            this.CheckDisk = new System.Windows.Forms.Button();
            this.ucBottom = new WindowsStandardTools.BottomControl();
            this.spcMain.Panel1.SuspendLayout();
            this.spcMain.Panel2.SuspendLayout();
            this.spcMain.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // spcMain
            // 
            this.spcMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.spcMain.IsSplitterFixed = true;
            this.spcMain.Location = new System.Drawing.Point(0, 0);
            this.spcMain.Margin = new System.Windows.Forms.Padding(0);
            this.spcMain.Name = "spcMain";
            this.spcMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcMain.Panel1
            // 
            this.spcMain.Panel1.Controls.Add(this.ucTop);
            this.spcMain.Panel1MinSize = 64;
            // 
            // spcMain.Panel2
            // 
            this.spcMain.Panel2.Controls.Add(this.tlpMain);
            this.spcMain.Size = new System.Drawing.Size(665, 400);
            this.spcMain.SplitterDistance = 67;
            this.spcMain.SplitterWidth = 1;
            this.spcMain.TabIndex = 0;
            // 
            // ucTop
            // 
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTop.Location = new System.Drawing.Point(0, 0);
            this.ucTop.MaximumSize = new System.Drawing.Size(0, 64);
            this.ucTop.Name = "ucTop";
            this.ucTop.Size = new System.Drawing.Size(665, 64);
            this.ucTop.TabIndex = 0;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.77477F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.22523F));
            this.tlpMain.Controls.Add(this.lblData, 0, 7);
            this.tlpMain.Controls.Add(this.lblAdd, 0, 1);
            this.tlpMain.Controls.Add(this.CheckDiskNote, 1, 0);
            this.tlpMain.Controls.Add(this.DiskDefragNote, 1, 2);
            this.tlpMain.Controls.Add(this.DiskDefrag, 0, 2);
            this.tlpMain.Controls.Add(this.SysRestore, 0, 4);
            this.tlpMain.Controls.Add(this.SysRestoreNote, 1, 4);
            this.tlpMain.Controls.Add(this.lblInfo, 0, 3);
            this.tlpMain.Controls.Add(this.lblSpacer, 0, 5);
            this.tlpMain.Controls.Add(this.SysFileChecker, 0, 6);
            this.tlpMain.Controls.Add(this.SysFileCheckerNote, 1, 6);
            this.tlpMain.Controls.Add(this.Backup, 0, 8);
            this.tlpMain.Controls.Add(this.BackupNote, 1, 8);
            this.tlpMain.Controls.Add(this.CheckDisk, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 10;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(665, 332);
            this.tlpMain.TabIndex = 12;
            // 
            // lblData
            // 
            this.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tlpMain.SetColumnSpan(this.lblData, 2);
            this.lblData.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblData.Location = new System.Drawing.Point(3, 264);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(659, 2);
            this.lblData.TabIndex = 20;
            // 
            // lblAdd
            // 
            this.lblAdd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tlpMain.SetColumnSpan(this.lblAdd, 2);
            this.lblAdd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAdd.Location = new System.Drawing.Point(3, 58);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(659, 2);
            this.lblAdd.TabIndex = 9;
            // 
            // CheckDiskNote
            // 
            this.CheckDiskNote.AutoSize = true;
            this.CheckDiskNote.Location = new System.Drawing.Point(174, 9);
            this.CheckDiskNote.Margin = new System.Windows.Forms.Padding(10, 9, 10, 10);
            this.CheckDiskNote.Name = "CheckDiskNote";
            this.CheckDiskNote.Size = new System.Drawing.Size(473, 39);
            this.CheckDiskNote.TabIndex = 2;
            this.CheckDiskNote.Text = resources.GetString("CheckDiskNote.Text");
            // 
            // DiskDefragNote
            // 
            this.DiskDefragNote.AutoSize = true;
            this.DiskDefragNote.Location = new System.Drawing.Point(174, 69);
            this.DiskDefragNote.Margin = new System.Windows.Forms.Padding(10, 9, 10, 10);
            this.DiskDefragNote.Name = "DiskDefragNote";
            this.DiskDefragNote.Size = new System.Drawing.Size(476, 52);
            this.DiskDefragNote.TabIndex = 11;
            this.DiskDefragNote.Text = resources.GetString("DiskDefragNote.Text");
            // 
            // DiskDefrag
            // 
            this.DiskDefrag.Location = new System.Drawing.Point(10, 67);
            this.DiskDefrag.Margin = new System.Windows.Forms.Padding(10, 7, 3, 3);
            this.DiskDefrag.Name = "DiskDefrag";
            this.DiskDefrag.Size = new System.Drawing.Size(151, 42);
            this.DiskDefrag.TabIndex = 10;
            this.DiskDefrag.Text = "Disk Defragmenter";
            this.DiskDefrag.UseVisualStyleBackColor = true;
            this.DiskDefrag.Click += new System.EventHandler(this.DiskDefrag_Click);
            // 
            // SysRestore
            // 
            this.SysRestore.Location = new System.Drawing.Point(10, 140);
            this.SysRestore.Margin = new System.Windows.Forms.Padding(10, 7, 3, 3);
            this.SysRestore.Name = "SysRestore";
            this.SysRestore.Size = new System.Drawing.Size(151, 42);
            this.SysRestore.TabIndex = 12;
            this.SysRestore.Text = "System Restore";
            this.SysRestore.UseVisualStyleBackColor = true;
            this.SysRestore.Click += new System.EventHandler(this.SysRestore_Click);
            // 
            // SysRestoreNote
            // 
            this.SysRestoreNote.AutoSize = true;
            this.SysRestoreNote.Location = new System.Drawing.Point(174, 142);
            this.SysRestoreNote.Margin = new System.Windows.Forms.Padding(10, 9, 10, 10);
            this.SysRestoreNote.Name = "SysRestoreNote";
            this.SysRestoreNote.Size = new System.Drawing.Size(481, 39);
            this.SysRestoreNote.TabIndex = 13;
            this.SysRestoreNote.Text = resources.GetString("SysRestoreNote.Text");
            // 
            // lblInfo
            // 
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tlpMain.SetColumnSpan(this.lblInfo, 2);
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInfo.Location = new System.Drawing.Point(3, 131);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(659, 2);
            this.lblInfo.TabIndex = 14;
            // 
            // lblSpacer
            // 
            this.lblSpacer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tlpMain.SetColumnSpan(this.lblSpacer, 2);
            this.lblSpacer.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSpacer.Location = new System.Drawing.Point(3, 191);
            this.lblSpacer.Name = "lblSpacer";
            this.lblSpacer.Size = new System.Drawing.Size(659, 2);
            this.lblSpacer.TabIndex = 15;
            // 
            // SysFileChecker
            // 
            this.SysFileChecker.Location = new System.Drawing.Point(10, 200);
            this.SysFileChecker.Margin = new System.Windows.Forms.Padding(10, 7, 3, 3);
            this.SysFileChecker.Name = "SysFileChecker";
            this.SysFileChecker.Size = new System.Drawing.Size(151, 42);
            this.SysFileChecker.TabIndex = 16;
            this.SysFileChecker.Text = "System File Checker";
            this.SysFileChecker.UseVisualStyleBackColor = true;
            this.SysFileChecker.Click += new System.EventHandler(this.SysFileChecker_Click);
            // 
            // SysFileCheckerNote
            // 
            this.SysFileCheckerNote.AutoSize = true;
            this.SysFileCheckerNote.Location = new System.Drawing.Point(174, 202);
            this.SysFileCheckerNote.Margin = new System.Windows.Forms.Padding(10, 9, 10, 10);
            this.SysFileCheckerNote.Name = "SysFileCheckerNote";
            this.SysFileCheckerNote.Size = new System.Drawing.Size(478, 52);
            this.SysFileCheckerNote.TabIndex = 17;
            this.SysFileCheckerNote.Text = resources.GetString("SysFileCheckerNote.Text");
            // 
            // Backup
            // 
            this.Backup.Location = new System.Drawing.Point(10, 273);
            this.Backup.Margin = new System.Windows.Forms.Padding(10, 7, 3, 3);
            this.Backup.Name = "Backup";
            this.Backup.Size = new System.Drawing.Size(151, 42);
            this.Backup.TabIndex = 18;
            this.Backup.Text = "Backup";
            this.Backup.UseVisualStyleBackColor = true;
            this.Backup.Click += new System.EventHandler(this.Backup_Click);
            // 
            // BackupNote
            // 
            this.BackupNote.AutoSize = true;
            this.BackupNote.Location = new System.Drawing.Point(174, 275);
            this.BackupNote.Margin = new System.Windows.Forms.Padding(10, 9, 10, 10);
            this.BackupNote.Name = "BackupNote";
            this.BackupNote.Size = new System.Drawing.Size(481, 26);
            this.BackupNote.TabIndex = 19;
            this.BackupNote.Text = "The Backup utility helps you protect data from accidental loss if your system exp" +
                "eriences hardware or storage media faliure";
            // 
            // CheckDisk
            // 
            this.CheckDisk.Location = new System.Drawing.Point(10, 7);
            this.CheckDisk.Margin = new System.Windows.Forms.Padding(10, 7, 3, 3);
            this.CheckDisk.Name = "CheckDisk";
            this.CheckDisk.Size = new System.Drawing.Size(151, 42);
            this.CheckDisk.TabIndex = 1;
            this.CheckDisk.Text = "CheckDisk";
            this.CheckDisk.UseVisualStyleBackColor = true;
            this.CheckDisk.Click += new System.EventHandler(this.CheckDisk_Click);
            // 
            // ucBottom
            // 
            this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucBottom.Location = new System.Drawing.Point(0, 400);
            this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
            this.ucBottom.MaximumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.MinimumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.Name = "ucBottom";
            this.ucBottom.Size = new System.Drawing.Size(665, 31);
            this.ucBottom.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(665, 431);
            this.Controls.Add(this.ucBottom);
            this.Controls.Add(this.spcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Standard Tools";
            this.Load += new System.EventHandler(this.FrmWindowStdTools_Load);
            this.spcMain.Panel1.ResumeLayout(false);
            this.spcMain.Panel2.ResumeLayout(false);
            this.spcMain.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

		System.Windows.Forms.SplitContainer spcMain;
        BottomControl ucBottom;
		TopControl ucTop;
		TableLayoutPanel tlpMain;
		Label lblAdd;
		Button CheckDisk;
		Label CheckDiskNote;
		Label DiskDefragNote;
		Button DiskDefrag;
		Button SysRestore;
		Label SysRestoreNote;
		Label lblInfo;
		Label lblSpacer;
		Button SysFileChecker;
		Label SysFileCheckerNote;
		Label lblData;
		Button Backup;
		Label BackupNote;
    }
}