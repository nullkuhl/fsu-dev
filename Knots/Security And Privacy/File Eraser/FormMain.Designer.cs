using System.Resources;
using System.Globalization;
using System.Threading;

namespace FileEraser
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

        public ResourceManager rm = new ResourceManager("FileEraser.Resources",
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
            this.tmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiVisit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiEmpty = new System.Windows.Forms.ToolStripSeparator();
            this.tmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.grbSelectFile = new System.Windows.Forms.GroupBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.cmdSelect = new System.Windows.Forms.Button();
            this.optFile = new System.Windows.Forms.RadioButton();
            this.optFolder = new System.Windows.Forms.RadioButton();
            this.grbOptions = new System.Windows.Forms.GroupBox();
            this.chkDelDir = new System.Windows.Forms.CheckBox();
            this.chkCloseInstance = new System.Windows.Forms.CheckBox();
            this.chkSubfolders = new System.Windows.Forms.CheckBox();
            this.stInfo = new System.Windows.Forms.StatusStrip();
            this.cmdShred = new System.Windows.Forms.Button();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.ofdFile = new System.Windows.Forms.OpenFileDialog();
            this.fbFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.lblFileName = new System.Windows.Forms.Label();
            this.ucTop = new FileEraser.TopControl();
            this.ucBottom = new FileEraser.BottomControl();
            this.grbSelectFile.SuspendLayout();
            this.grbOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmiAbout
            // 
            this.tmiAbout.Name = "tmiAbout";
            this.tmiAbout.Size = new System.Drawing.Size(123, 22);
            this.tmiAbout.Text = "About Us";
            // 
            // tmiVisit
            // 
            this.tmiVisit.Name = "tmiVisit";
            this.tmiVisit.Size = new System.Drawing.Size(123, 22);
            this.tmiVisit.Text = "Visit Us";
            // 
            // tmiEmpty
            // 
            this.tmiEmpty.Name = "tmiEmpty";
            this.tmiEmpty.Size = new System.Drawing.Size(120, 6);
            // 
            // tmiExit
            // 
            this.tmiExit.Name = "tmiExit";
            this.tmiExit.Size = new System.Drawing.Size(123, 22);
            this.tmiExit.Text = "Exit";
            // 
            // grbSelectFile
            // 
            this.grbSelectFile.Controls.Add(this.tbPath);
            this.grbSelectFile.Controls.Add(this.cmdSelect);
            this.grbSelectFile.Controls.Add(this.optFile);
            this.grbSelectFile.Controls.Add(this.optFolder);
            this.grbSelectFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSelectFile.Location = new System.Drawing.Point(18, 79);
            this.grbSelectFile.Name = "grbSelectFile";
            this.grbSelectFile.Size = new System.Drawing.Size(361, 75);
            this.grbSelectFile.TabIndex = 1;
            this.grbSelectFile.TabStop = false;
            this.grbSelectFile.Text = "File Selection";
            // 
            // tbPath
            // 
            this.tbPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPath.Location = new System.Drawing.Point(12, 20);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(307, 20);
            this.tbPath.TabIndex = 1;
            // 
            // cmdSelect
            // 
            this.cmdSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSelect.Location = new System.Drawing.Point(323, 19);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(30, 22);
            this.cmdSelect.TabIndex = 0;
            this.cmdSelect.Text = "...";
            this.cmdSelect.UseVisualStyleBackColor = true;
            this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // optFile
            // 
            this.optFile.AutoSize = true;
            this.optFile.Checked = true;
            this.optFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optFile.Location = new System.Drawing.Point(13, 48);
            this.optFile.Name = "optFile";
            this.optFile.Size = new System.Drawing.Size(75, 17);
            this.optFile.TabIndex = 0;
            this.optFile.TabStop = true;
            this.optFile.Text = "Delete File";
            this.optFile.UseVisualStyleBackColor = true;
            this.optFile.CheckedChanged += new System.EventHandler(this.optFile_CheckedChanged);
            // 
            // optFolder
            // 
            this.optFolder.AutoSize = true;
            this.optFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optFolder.Location = new System.Drawing.Point(133, 48);
            this.optFolder.Name = "optFolder";
            this.optFolder.Size = new System.Drawing.Size(150, 17);
            this.optFolder.TabIndex = 1;
            this.optFolder.TabStop = true;
            this.optFolder.Text = "Delete All Files in Directory";
            this.optFolder.UseVisualStyleBackColor = true;
            this.optFolder.CheckedChanged += new System.EventHandler(this.optFolder_CheckedChanged);
            // 
            // grbOptions
            // 
            this.grbOptions.Controls.Add(this.chkDelDir);
            this.grbOptions.Controls.Add(this.chkCloseInstance);
            this.grbOptions.Controls.Add(this.chkSubfolders);
            this.grbOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbOptions.Location = new System.Drawing.Point(385, 79);
            this.grbOptions.Name = "grbOptions";
            this.grbOptions.Size = new System.Drawing.Size(194, 90);
            this.grbOptions.TabIndex = 2;
            this.grbOptions.TabStop = false;
            this.grbOptions.Text = "Deletion Options";
            // 
            // chkDelDir
            // 
            this.chkDelDir.AutoSize = true;
            this.chkDelDir.Enabled = false;
            this.chkDelDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDelDir.Location = new System.Drawing.Point(10, 65);
            this.chkDelDir.Name = "chkDelDir";
            this.chkDelDir.Size = new System.Drawing.Size(105, 17);
            this.chkDelDir.TabIndex = 5;
            this.chkDelDir.Text = "Eliminate Folders";
            this.chkDelDir.UseVisualStyleBackColor = true;
            // 
            // chkCloseInstance
            // 
            this.chkCloseInstance.AutoSize = true;
            this.chkCloseInstance.Enabled = false;
            this.chkCloseInstance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCloseInstance.Location = new System.Drawing.Point(10, 25);
            this.chkCloseInstance.Name = "chkCloseInstance";
            this.chkCloseInstance.Size = new System.Drawing.Size(144, 17);
            this.chkCloseInstance.TabIndex = 4;
            this.chkCloseInstance.Text = "Close Running Instances";
            this.chkCloseInstance.UseVisualStyleBackColor = true;
            // 
            // chkSubfolders
            // 
            this.chkSubfolders.AutoSize = true;
            this.chkSubfolders.Enabled = false;
            this.chkSubfolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSubfolders.Location = new System.Drawing.Point(10, 45);
            this.chkSubfolders.Name = "chkSubfolders";
            this.chkSubfolders.Size = new System.Drawing.Size(109, 17);
            this.chkSubfolders.TabIndex = 2;
            this.chkSubfolders.Text = "Parse SubFolders";
            this.chkSubfolders.UseVisualStyleBackColor = true;
            this.chkSubfolders.CheckedChanged += new System.EventHandler(this.chkSubfolders_CheckedChanged);
            // 
            // stInfo
            // 
            this.stInfo.Location = new System.Drawing.Point(0, 218);
            this.stInfo.Name = "stInfo";
            this.stInfo.Size = new System.Drawing.Size(596, 22);
            this.stInfo.SizingGrip = false;
            this.stInfo.TabIndex = 3;
            this.stInfo.Text = "statusStrip1";
            // 
            // cmdShred
            // 
            this.cmdShred.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdShred.Location = new System.Drawing.Point(213, 175);
            this.cmdShred.Name = "cmdShred";
            this.cmdShred.Size = new System.Drawing.Size(191, 37);
            this.cmdShred.TabIndex = 4;
            this.cmdShred.Text = "Shred It";
            this.cmdShred.UseVisualStyleBackColor = true;
            this.cmdShred.Click += new System.EventHandler(this.cmdShred_Click);
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(502, 221);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(88, 17);
            this.pbProgress.TabIndex = 5;
            this.pbProgress.Visible = false;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(21, 168);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(0, 13);
            this.lblFileName.TabIndex = 7;
            this.lblFileName.Visible = false;
            // 
            // ucTop
            // 
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTop.Location = new System.Drawing.Point(0, 0);
            this.ucTop.Name = "ucTop";
            this.ucTop.Size = new System.Drawing.Size(596, 64);
            this.ucTop.TabIndex = 9;
            // 
            // ucBottom
            // 
            this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucBottom.Location = new System.Drawing.Point(0, 240);
            this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
            this.ucBottom.MaximumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.MinimumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.Name = "ucBottom";
            this.ucBottom.Size = new System.Drawing.Size(596, 31);
            this.ucBottom.TabIndex = 10;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 271);
            this.Controls.Add(this.ucTop);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.cmdShred);
            this.Controls.Add(this.stInfo);
            this.Controls.Add(this.grbOptions);
            this.Controls.Add(this.grbSelectFile);
            this.Controls.Add(this.ucBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Shredder";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grbSelectFile.ResumeLayout(false);
            this.grbSelectFile.PerformLayout();
            this.grbOptions.ResumeLayout(false);
            this.grbOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStripMenuItem tmiAbout;
        public System.Windows.Forms.ToolStripMenuItem tmiVisit;
        public System.Windows.Forms.ToolStripSeparator tmiEmpty;
        public System.Windows.Forms.ToolStripMenuItem tmiExit;
        System.Windows.Forms.GroupBox grbSelectFile;
        System.Windows.Forms.TextBox tbPath;
        System.Windows.Forms.Button cmdSelect;
        System.Windows.Forms.GroupBox grbOptions;
        System.Windows.Forms.RadioButton optFolder;
        System.Windows.Forms.RadioButton optFile;
        System.Windows.Forms.StatusStrip stInfo;
        System.Windows.Forms.Button cmdShred;
        System.Windows.Forms.ProgressBar pbProgress;
        System.Windows.Forms.CheckBox chkSubfolders;
        System.Windows.Forms.OpenFileDialog ofdFile;
        System.Windows.Forms.CheckBox chkCloseInstance;
        System.Windows.Forms.FolderBrowserDialog fbFolder;
		System.Windows.Forms.Label lblFileName;
        TopControl ucTop;
        BottomControl ucBottom;
		private System.Windows.Forms.CheckBox chkDelDir;
    }
}

