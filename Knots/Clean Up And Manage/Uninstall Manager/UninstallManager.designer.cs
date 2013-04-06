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
		System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UninstallManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mnsMain = new System.Windows.Forms.MenuStrip();
            this.batchUninstallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uninstallThisProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRefreshApps = new System.Windows.Forms.ToolStripMenuItem();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tslTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtCommandLine = new System.Windows.Forms.TextBox();
            this.lblCommand = new System.Windows.Forms.Label();
            this.saveFD = new System.Windows.Forms.SaveFileDialog();
            this.imlMain = new System.Windows.Forms.ImageList(this.components);
            this.OpenFD = new System.Windows.Forms.OpenFileDialog();
            this.lblPrograms = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
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
            this.ucTop = new Uninstall_Manager.TopControl();
            this.ucBottom = new Uninstall_Manager.BottomControl();
            this.mnsMain.SuspendLayout();
            this.stsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInstalledApps)).BeginInit();
            this.pnlCommandLine.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnsMain
            // 
            this.mnsMain.BackColor = System.Drawing.Color.Transparent;
            this.mnsMain.Dock = System.Windows.Forms.DockStyle.None;
            this.mnsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.batchUninstallToolStripMenuItem,
            this.uninstallThisProgramToolStripMenuItem,
            this.removeEntryToolStripMenuItem,
            this.mnuRefreshApps});
            this.mnsMain.Location = new System.Drawing.Point(0, 65);
            this.mnsMain.MinimumSize = new System.Drawing.Size(585, 0);
            this.mnsMain.Name = "mnsMain";
            this.mnsMain.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mnsMain.Size = new System.Drawing.Size(585, 24);
            this.mnsMain.TabIndex = 0;
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
            this.uninstallThisProgramToolStripMenuItem.Click += new System.EventHandler(this.uninstallThisProgramToolStripMenuItem_Click);
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
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslTotal});
            this.stsMain.Location = new System.Drawing.Point(0, 568);
            this.stsMain.Name = "stsMain";
            this.stsMain.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.stsMain.Size = new System.Drawing.Size(820, 22);
            this.stsMain.SizingGrip = false;
            this.stsMain.TabIndex = 2;
            // 
            // tslTotal
            // 
            this.tslTotal.BackColor = System.Drawing.Color.Transparent;
            this.tslTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslTotal.Name = "tslTotal";
            this.tslTotal.Size = new System.Drawing.Size(0, 17);
            // 
            // txtCommandLine
            // 
            this.txtCommandLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommandLine.BackColor = System.Drawing.Color.White;
            this.txtCommandLine.Enabled = false;
            this.txtCommandLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommandLine.Location = new System.Drawing.Point(114, 1);
            this.txtCommandLine.Margin = new System.Windows.Forms.Padding(2);
            this.txtCommandLine.Name = "txtCommandLine";
            this.txtCommandLine.ReadOnly = true;
            this.txtCommandLine.Size = new System.Drawing.Size(702, 20);
            this.txtCommandLine.TabIndex = 2;
            // 
            // lblCommand
            // 
            this.lblCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCommand.AutoSize = true;
            this.lblCommand.BackColor = System.Drawing.Color.Transparent;
            this.lblCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommand.Location = new System.Drawing.Point(4, 4);
            this.lblCommand.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(76, 13);
            this.lblCommand.TabIndex = 5;
            this.lblCommand.Text = "Command line:";
            // 
            // saveFD
            // 
            this.saveFD.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*\"";
            this.saveFD.Title = "Save";
            // 
            // imlMain
            // 
            this.imlMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlMain.ImageStream")));
            this.imlMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imlMain.Images.SetKeyName(0, "oneclick.png");
            // 
            // OpenFD
            // 
            this.OpenFD.FileName = "openFileDialog1";
            // 
            // lblPrograms
            // 
            this.lblPrograms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrograms.AutoSize = true;
            this.lblPrograms.BackColor = System.Drawing.Color.Transparent;
            this.lblPrograms.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrograms.Location = new System.Drawing.Point(20, 94);
            this.lblPrograms.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrograms.Name = "lblPrograms";
            this.lblPrograms.Size = new System.Drawing.Size(133, 13);
            this.lblPrograms.TabIndex = 13;
            this.lblPrograms.Text = "Current Installed Programs:";
            // 
            // txtSearch
            // 
            this.txtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtSearch.Location = new System.Drawing.Point(532, 91);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(275, 20);
            this.txtSearch.TabIndex = 150;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearch.Location = new System.Drawing.Point(362, 94);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(169, 17);
            this.lblSearch.TabIndex = 16;
            this.lblSearch.Text = "Search Program:";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            this.dgvInstalledApps.GridColor = System.Drawing.Color.White;
            this.dgvInstalledApps.Location = new System.Drawing.Point(13, 115);
            this.dgvInstalledApps.Margin = new System.Windows.Forms.Padding(2);
            this.dgvInstalledApps.MultiSelect = false;
            this.dgvInstalledApps.Name = "dgvInstalledApps";
            this.dgvInstalledApps.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvInstalledApps.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvInstalledApps.RowTemplate.Height = 20;
            this.dgvInstalledApps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInstalledApps.ShowEditingIcon = false;
            this.dgvInstalledApps.Size = new System.Drawing.Size(795, 433);
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.NullValue = null;
            this.colImage.DefaultCellStyle = dataGridViewCellStyle2;
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
            this.pnlCommandLine.Controls.Add(this.lblCommand);
            this.pnlCommandLine.Controls.Add(this.txtCommandLine);
            this.pnlCommandLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCommandLine.Location = new System.Drawing.Point(0, 546);
            this.pnlCommandLine.Margin = new System.Windows.Forms.Padding(2);
            this.pnlCommandLine.Name = "pnlCommandLine";
            this.pnlCommandLine.Size = new System.Drawing.Size(820, 22);
            this.pnlCommandLine.TabIndex = 17;
            // 
            // showUpdatesChk
            // 
            this.showUpdatesChk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showUpdatesChk.AutoSize = true;
            this.showUpdatesChk.BackColor = System.Drawing.Color.Transparent;
            this.showUpdatesChk.Location = new System.Drawing.Point(673, 70);
            this.showUpdatesChk.Margin = new System.Windows.Forms.Padding(2);
            this.showUpdatesChk.Name = "showUpdatesChk";
            this.showUpdatesChk.Size = new System.Drawing.Size(143, 17);
            this.showUpdatesChk.TabIndex = 152;
            this.showUpdatesChk.Text = "Show Windows Updates";
            this.showUpdatesChk.UseVisualStyleBackColor = false;
            this.showUpdatesChk.CheckedChanged += new System.EventHandler(this.showUpdatesChk_CheckedChanged);
            // 
            // ucTop
            // 
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTop.Location = new System.Drawing.Point(0, 0);
            this.ucTop.Name = "ucTop";
            this.ucTop.Size = new System.Drawing.Size(820, 63);
            this.ucTop.TabIndex = 153;
            // 
            // ucBottom
            // 
            this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucBottom.Location = new System.Drawing.Point(0, 590);
            this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
            this.ucBottom.MaximumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.MinimumSize = new System.Drawing.Size(0, 30);
            this.ucBottom.Name = "ucBottom";
            this.ucBottom.Size = new System.Drawing.Size(820, 30);
            this.ucBottom.TabIndex = 154;
            // 
            // UninstallManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(820, 620);
            this.Controls.Add(this.showUpdatesChk);
            this.Controls.Add(this.ucTop);
            this.Controls.Add(this.pnlCommandLine);
            this.Controls.Add(this.dgvInstalledApps);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblPrograms);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.mnsMain);
            this.Controls.Add(this.ucBottom);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnsMain;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UninstallManager";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Uninstall Manager";
            this.Load += new System.EventHandler(this.UninstallManager_Load);
            this.mnsMain.ResumeLayout(false);
            this.mnsMain.PerformLayout();
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInstalledApps)).EndInit();
            this.pnlCommandLine.ResumeLayout(false);
            this.pnlCommandLine.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		System.Windows.Forms.MenuStrip mnsMain;
		System.Windows.Forms.StatusStrip stsMain;
		public System.Windows.Forms.TextBox txtCommandLine;
		System.Windows.Forms.Label lblCommand;
		System.Windows.Forms.ToolStripMenuItem batchUninstallToolStripMenuItem;
		System.Windows.Forms.ToolStripMenuItem uninstallThisProgramToolStripMenuItem;
		System.Windows.Forms.ToolStripMenuItem removeEntryToolStripMenuItem;
		System.Windows.Forms.ToolStripMenuItem mnuRefreshApps;
		System.Windows.Forms.ToolStripStatusLabel tslTotal;
		System.Windows.Forms.SaveFileDialog saveFD;
		System.Windows.Forms.ImageList imlMain;
		System.Windows.Forms.OpenFileDialog OpenFD;
		System.Windows.Forms.Label lblPrograms;
		System.Windows.Forms.TextBox txtSearch;
		System.Windows.Forms.Label lblSearch;
		System.Windows.Forms.DataGridView dgvInstalledApps;
		System.Windows.Forms.Panel pnlCommandLine;
		System.Windows.Forms.CheckBox showUpdatesChk;
		TopControl ucTop;
		BottomControl ucBottom;
		System.Windows.Forms.DataGridViewCheckBoxColumn colIcon;
		System.Windows.Forms.DataGridViewImageColumn colImage;
		System.Windows.Forms.DataGridViewTextBoxColumn colName;
		System.Windows.Forms.DataGridViewTextBoxColumn colPublisher;
		System.Windows.Forms.DataGridViewTextBoxColumn colInstalledOn;
		System.Windows.Forms.DataGridViewTextBoxColumn colSize;
		System.Windows.Forms.DataGridViewTextBoxColumn colVersion;
		System.Windows.Forms.DataGridViewTextBoxColumn regKey;
	}
}

