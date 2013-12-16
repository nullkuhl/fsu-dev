using System.Resources;
using System.Globalization;
using System.Threading;

namespace Uninstall_Manager
{
    partial class UninstallManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UninstallManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.batchUninstallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uninstallThisProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRefreshApps = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtCommandLine = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFD = new System.Windows.Forms.SaveFileDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.OpenFD = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvInstalledApps = new System.Windows.Forms.DataGridView();
            this.colIcon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPublisher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInstalledOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlCommandLine = new System.Windows.Forms.Panel();
            this.showUpdatesChk = new System.Windows.Forms.CheckBox();
            this.topControl1 = new Uninstall_Manager.TopControl();
            this.buttomControl1 = new Uninstall_Manager.ButtomControl();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInstalledApps)).BeginInit();
            this.pnlCommandLine.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.batchUninstallToolStripMenuItem,
            this.uninstallThisProgramToolStripMenuItem,
            this.removeEntryToolStripMenuItem,
            this.mnuRefreshApps});
            this.menuStrip1.Location = new System.Drawing.Point(0, 66);
            this.menuStrip1.MinimumSize = new System.Drawing.Size(780, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(780, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // batchUninstallToolStripMenuItem
            // 
            this.batchUninstallToolStripMenuItem.Image = global::Uninstall_Manager.Properties.Resources._1303026907_list_edit;
            this.batchUninstallToolStripMenuItem.Name = "batchUninstallToolStripMenuItem";
            this.batchUninstallToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.batchUninstallToolStripMenuItem.Text = "Batch Uninstall";
            this.batchUninstallToolStripMenuItem.Click += new System.EventHandler(this.batchUninstallToolStripMenuItem_Click);
            // 
            // uninstallThisProgramToolStripMenuItem
            // 
            this.uninstallThisProgramToolStripMenuItem.Enabled = false;
            this.uninstallThisProgramToolStripMenuItem.Image = global::Uninstall_Manager.Properties.Resources._1303026952_edit_delete;
            this.uninstallThisProgramToolStripMenuItem.Name = "uninstallThisProgramToolStripMenuItem";
            this.uninstallThisProgramToolStripMenuItem.Size = new System.Drawing.Size(132, 20);
            this.uninstallThisProgramToolStripMenuItem.Text = "Uninstall_program";
            this.uninstallThisProgramToolStripMenuItem.Click += new System.EventHandler(this.uninstallThisProgramToolStripMenuItem1_Click);
            // 
            // removeEntryToolStripMenuItem
            // 
            this.removeEntryToolStripMenuItem.Enabled = false;
            this.removeEntryToolStripMenuItem.Image = global::Uninstall_Manager.Properties.Resources._1303026989_101;
            this.removeEntryToolStripMenuItem.Name = "removeEntryToolStripMenuItem";
            this.removeEntryToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.removeEntryToolStripMenuItem.Text = "Remove Entry";
            this.removeEntryToolStripMenuItem.Click += new System.EventHandler(this.removeEntryToolStripMenuItem_Click);
            // 
            // mnuRefreshApps
            // 
            this.mnuRefreshApps.Image = global::Uninstall_Manager.Properties.Resources._1303027013_gtk_refresh;
            this.mnuRefreshApps.Name = "mnuRefreshApps";
            this.mnuRefreshApps.Size = new System.Drawing.Size(74, 20);
            this.mnuRefreshApps.Text = "Refresh";
            this.mnuRefreshApps.Click += new System.EventHandler(this.mnuRefreshApps_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 579);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(780, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // txtCommandLine
            // 
            this.txtCommandLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommandLine.BackColor = System.Drawing.Color.White;
            this.txtCommandLine.Enabled = false;
            this.txtCommandLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommandLine.Location = new System.Drawing.Point(85, 4);
            this.txtCommandLine.Name = "txtCommandLine";
            this.txtCommandLine.ReadOnly = true;
            this.txtCommandLine.Size = new System.Drawing.Size(686, 20);
            this.txtCommandLine.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Command line:";
            // 
            // saveFD
            // 
            this.saveFD.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*\"";
            this.saveFD.Title = "Save";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "oneclick.png");
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // OpenFD
            // 
            this.OpenFD.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Current Installed Programs:";
            // 
            // txtSearch
            // 
            this.txtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtSearch.Location = new System.Drawing.Point(515, 93);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(262, 20);
            this.txtSearch.TabIndex = 150;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(426, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Search Program:";
            // 
            // dgvInstalledApps
            // 
            this.dgvInstalledApps.AllowUserToAddRows = false;
            this.dgvInstalledApps.AllowUserToDeleteRows = false;
            this.dgvInstalledApps.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvInstalledApps.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInstalledApps.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dgvInstalledApps.BackgroundColor = System.Drawing.Color.White;
            this.dgvInstalledApps.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvInstalledApps.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvInstalledApps.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvInstalledApps.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInstalledApps.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvInstalledApps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvInstalledApps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIcon,
            this.colImage,
            this.colName,
            this.colPublisher,
            this.colInstalledOn,
            this.colSize,
            this.colVersion,
            this.regKey});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInstalledApps.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvInstalledApps.GridColor = System.Drawing.Color.White;
            this.dgvInstalledApps.Location = new System.Drawing.Point(3, 118);
            this.dgvInstalledApps.MultiSelect = false;
            this.dgvInstalledApps.Name = "dgvInstalledApps";
            this.dgvInstalledApps.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvInstalledApps.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvInstalledApps.RowTemplate.Height = 20;
            this.dgvInstalledApps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInstalledApps.ShowEditingIcon = false;
            this.dgvInstalledApps.Size = new System.Drawing.Size(775, 431);
            this.dgvInstalledApps.TabIndex = 2;
            this.dgvInstalledApps.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInstalledApps_CellClick);
            this.dgvInstalledApps.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInstalledApps_CellContentClick);
            this.dgvInstalledApps.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInstalledApps_RowLeave);
            this.dgvInstalledApps.SelectionChanged += new System.EventHandler(this.dgvInstalledApps_SelectionChanged);
            // 
            // colIcon
            // 
            this.colIcon.HeaderText = "";
            this.colIcon.Name = "colIcon";
            this.colIcon.Visible = false;
            this.colIcon.Width = 30;
            // 
            // colImage
            // 
            this.colImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle3.NullValue")));
            this.colImage.DefaultCellStyle = dataGridViewCellStyle3;
            this.colImage.HeaderText = "";
            this.colImage.Name = "colImage";
            this.colImage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colImage.Width = 16;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.Width = 60;
            // 
            // colPublisher
            // 
            this.colPublisher.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colPublisher.HeaderText = "publisher";
            this.colPublisher.Name = "colPublisher";
            this.colPublisher.Width = 40;
            // 
            // colInstalledOn
            // 
            this.colInstalledOn.HeaderText = "Installed On";
            this.colInstalledOn.Name = "colInstalledOn";
            // 
            // colSize
            // 
            this.colSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colSize.HeaderText = "Size";
            this.colSize.Name = "colSize";
            this.colSize.ReadOnly = true;
            this.colSize.Width = 52;
            // 
            // colVersion
            // 
            this.colVersion.HeaderText = "Version";
            this.colVersion.Name = "colVersion";
            this.colVersion.ReadOnly = true;
            // 
            // regKey
            // 
            this.regKey.HeaderText = "Registry Key";
            this.regKey.Name = "regKey";
            this.regKey.Visible = false;
            // 
            // pnlCommandLine
            // 
            this.pnlCommandLine.Controls.Add(this.label1);
            this.pnlCommandLine.Controls.Add(this.txtCommandLine);
            this.pnlCommandLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCommandLine.Location = new System.Drawing.Point(0, 550);
            this.pnlCommandLine.Name = "pnlCommandLine";
            this.pnlCommandLine.Size = new System.Drawing.Size(780, 29);
            this.pnlCommandLine.TabIndex = 17;
            // 
            // showUpdatesChk
            // 
            this.showUpdatesChk.AutoSize = true;
            this.showUpdatesChk.BackColor = System.Drawing.Color.Transparent;
            this.showUpdatesChk.Location = new System.Drawing.Point(639, 70);
            this.showUpdatesChk.Name = "showUpdatesChk";
            this.showUpdatesChk.Size = new System.Drawing.Size(143, 17);
            this.showUpdatesChk.TabIndex = 152;
            this.showUpdatesChk.Text = "Show Windows Updates";
            this.showUpdatesChk.UseVisualStyleBackColor = false;
            this.showUpdatesChk.CheckedChanged += new System.EventHandler(this.showUpdatesChk_CheckedChanged);
            // 
            // topControl1
            // 
            this.topControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.topControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.topControl1.Location = new System.Drawing.Point(0, 0);
            this.topControl1.Name = "topControl1";
            this.topControl1.Size = new System.Drawing.Size(780, 64);
            this.topControl1.TabIndex = 153;
            // 
            // buttomControl1
            // 
            this.buttomControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttomControl1.Location = new System.Drawing.Point(0, 601);
            this.buttomControl1.Margin = new System.Windows.Forms.Padding(0);
            this.buttomControl1.MaximumSize = new System.Drawing.Size(0, 31);
            this.buttomControl1.MinimumSize = new System.Drawing.Size(0, 31);
            this.buttomControl1.Name = "buttomControl1";
            this.buttomControl1.Size = new System.Drawing.Size(780, 31);
            this.buttomControl1.TabIndex = 154;
            // 
            // UninstallManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(780, 632);
            this.Controls.Add(this.topControl1);
            this.Controls.Add(this.showUpdatesChk);
            this.Controls.Add(this.pnlCommandLine);
            this.Controls.Add(this.dgvInstalledApps);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.buttomControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UninstallManager";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Uninstall Manager";
            this.Load += new System.EventHandler(this.UninstallManager_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInstalledApps)).EndInit();
            this.pnlCommandLine.ResumeLayout(false);
            this.pnlCommandLine.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.TextBox txtCommandLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem batchUninstallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uninstallThisProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuRefreshApps;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SaveFileDialog saveFD;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.OpenFileDialog OpenFD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvInstalledApps;
        private System.Windows.Forms.Panel pnlCommandLine;
        private System.Windows.Forms.CheckBox showUpdatesChk;
        private TopControl topControl1;
        private ButtomControl buttomControl1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIcon;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPublisher;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInstalledOn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn regKey;
    }
}

