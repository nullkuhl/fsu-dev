using System.Resources;
using System.Globalization;
using System.Threading;

namespace FileUndelete
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("FileUndelete.Resources",
			System.Reflection.Assembly.GetExecutingAssembly());

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.dbcRestoreButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dccIsRecoverable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dtcSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txcFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txcFileId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tmrScan = new System.Windows.Forms.Timer(this.components);
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tslFilter = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslResults = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlClient = new System.Windows.Forms.Panel();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.chkPreview = new System.Windows.Forms.CheckBox();
            this.btnRestore = new System.Windows.Forms.Button();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.clhName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhOriginalPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tbpFolders = new System.Windows.Forms.TabPage();
            this.trvFolders = new System.Windows.Forms.TreeView();
            this.imlFolders = new System.Windows.Forms.ImageList(this.components);
            this.tbpTypes = new System.Windows.Forms.TabPage();
            this.trvExt = new System.Windows.Forms.TreeView();
            this.imlFiles = new System.Windows.Forms.ImageList(this.components);
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.cboDrives = new System.Windows.Forms.ComboBox();
            this.fbdMain = new System.Windows.Forms.FolderBrowserDialog();
            this.ucBottom = new FileUndelete.BottomControl();
            this.ucTop = new FileUndelete.TopControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.pnlClient.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tbpFolders.SuspendLayout();
            this.tbpTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvFiles
            // 
            resources.ApplyResources(this.dgvFiles, "dgvFiles");
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.AllowUserToDeleteRows = false;
            this.dgvFiles.AllowUserToOrderColumns = true;
            this.dgvFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dbcRestoreButton,
            this.dccIsRecoverable,
            this.dtcSize,
            this.txcFilePath,
            this.txcFileId});
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.ReadOnly = true;
            this.dgvFiles.RowHeadersVisible = false;
            this.dgvFiles.ShowCellErrors = false;
            this.dgvFiles.ShowEditingIcon = false;
            this.dgvFiles.ShowRowErrors = false;
            // 
            // dbcRestoreButton
            // 
            this.dbcRestoreButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dbcRestoreButton.FillWeight = 40F;
            resources.ApplyResources(this.dbcRestoreButton, "dbcRestoreButton");
            this.dbcRestoreButton.Name = "dbcRestoreButton";
            this.dbcRestoreButton.ReadOnly = true;
            this.dbcRestoreButton.Text = "Restore";
            this.dbcRestoreButton.UseColumnTextForButtonValue = true;
            // 
            // dccIsRecoverable
            // 
            this.dccIsRecoverable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dccIsRecoverable.DataPropertyName = "isRecoverable";
            this.dccIsRecoverable.FillWeight = 40F;
            resources.ApplyResources(this.dccIsRecoverable, "dccIsRecoverable");
            this.dccIsRecoverable.Name = "dccIsRecoverable";
            this.dccIsRecoverable.ReadOnly = true;
            // 
            // dtcSize
            // 
            this.dtcSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtcSize.DataPropertyName = "size";
            this.dtcSize.FillWeight = 50F;
            resources.ApplyResources(this.dtcSize, "dtcSize");
            this.dtcSize.Name = "dtcSize";
            this.dtcSize.ReadOnly = true;
            // 
            // txcFilePath
            // 
            this.txcFilePath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txcFilePath.DataPropertyName = "filePath";
            this.txcFilePath.FillWeight = 200F;
            resources.ApplyResources(this.txcFilePath, "txcFilePath");
            this.txcFilePath.Name = "txcFilePath";
            this.txcFilePath.ReadOnly = true;
            this.txcFilePath.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // txcFileId
            // 
            this.txcFileId.DataPropertyName = "fileId";
            this.txcFileId.FillWeight = 50F;
            resources.ApplyResources(this.txcFileId, "txcFileId");
            this.txcFileId.MaxInputLength = 0;
            this.txcFileId.Name = "txcFileId";
            this.txcFileId.ReadOnly = true;
            // 
            // tmrScan
            // 
            this.tmrScan.Interval = 400;
            this.tmrScan.Tick += new System.EventHandler(this.scanTimer_Tick);
            // 
            // stsMain
            // 
            resources.ApplyResources(this.stsMain, "stsMain");
            this.stsMain.Name = "stsMain";
            this.stsMain.SizingGrip = false;
            // 
            // tslFilter
            // 
            resources.ApplyResources(this.tslFilter, "tslFilter");
            this.tslFilter.Name = "tslFilter";
            // 
            // tslResults
            // 
            resources.ApplyResources(this.tslResults, "tslResults");
            this.tslResults.Name = "tslResults";
            this.tslResults.Spring = true;
            // 
            // pnlClient
            // 
            resources.ApplyResources(this.pnlClient, "pnlClient");
            this.pnlClient.Controls.Add(this.btnBrowse);
            this.pnlClient.Controls.Add(this.pnlPreview);
            this.pnlClient.Controls.Add(this.chkAll);
            this.pnlClient.Controls.Add(this.chkPreview);
            this.pnlClient.Controls.Add(this.btnRestore);
            this.pnlClient.Controls.Add(this.lvFiles);
            this.pnlClient.Controls.Add(this.tcMain);
            this.pnlClient.Controls.Add(this.btnFilter);
            this.pnlClient.Controls.Add(this.txtFilter);
            this.pnlClient.Controls.Add(this.btnScan);
            this.pnlClient.Controls.Add(this.cboDrives);
            this.pnlClient.Controls.Add(this.dgvFiles);
            this.pnlClient.Name = "pnlClient";
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // pnlPreview
            // 
            resources.ApplyResources(this.pnlPreview, "pnlPreview");
            this.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlPreview.Name = "pnlPreview";
            // 
            // chkAll
            // 
            resources.ApplyResources(this.chkAll, "chkAll");
            this.chkAll.Checked = true;
            this.chkAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAll.Name = "chkAll";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // chkPreview
            // 
            resources.ApplyResources(this.chkPreview, "chkPreview");
            this.chkPreview.Checked = true;
            this.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.UseVisualStyleBackColor = true;
            this.chkPreview.CheckedChanged += new System.EventHandler(this.chkPreview_CheckedChanged);
            // 
            // btnRestore
            // 
            resources.ApplyResources(this.btnRestore, "btnRestore");
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // lvFiles
            // 
            resources.ApplyResources(this.lvFiles, "lvFiles");
            this.lvFiles.CheckBoxes = true;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhName,
            this.clhState,
            this.clhOriginalPath,
            this.clhSize,
            this.clhTag});
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.HideSelection = false;
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvFiles_ColumnClick);
            // 
            // clhName
            // 
            resources.ApplyResources(this.clhName, "clhName");
            // 
            // clhState
            // 
            resources.ApplyResources(this.clhState, "clhState");
            // 
            // clhOriginalPath
            // 
            resources.ApplyResources(this.clhOriginalPath, "clhOriginalPath");
            // 
            // clhSize
            // 
            resources.ApplyResources(this.clhSize, "clhSize");
            // 
            // clhTag
            // 
            resources.ApplyResources(this.clhTag, "clhTag");
            // 
            // tcMain
            // 
            resources.ApplyResources(this.tcMain, "tcMain");
            this.tcMain.Controls.Add(this.tbpFolders);
            this.tcMain.Controls.Add(this.tbpTypes);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            // 
            // tbpFolders
            // 
            resources.ApplyResources(this.tbpFolders, "tbpFolders");
            this.tbpFolders.Controls.Add(this.trvFolders);
            this.tbpFolders.Name = "tbpFolders";
            this.tbpFolders.UseVisualStyleBackColor = true;
            // 
            // trvFolders
            // 
            resources.ApplyResources(this.trvFolders, "trvFolders");
            this.trvFolders.HideSelection = false;
            this.trvFolders.ImageList = this.imlFolders;
            this.trvFolders.Name = "trvFolders";
            this.trvFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvFolders_AfterSelect);
            // 
            // imlFolders
            // 
            this.imlFolders.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlFolders.ImageStream")));
            this.imlFolders.TransparentColor = System.Drawing.Color.Transparent;
            this.imlFolders.Images.SetKeyName(0, "hdd.ico");
            // 
            // tbpTypes
            // 
            resources.ApplyResources(this.tbpTypes, "tbpTypes");
            this.tbpTypes.Controls.Add(this.trvExt);
            this.tbpTypes.Name = "tbpTypes";
            this.tbpTypes.UseVisualStyleBackColor = true;
            // 
            // trvExt
            // 
            resources.ApplyResources(this.trvExt, "trvExt");
            this.trvExt.HideSelection = false;
            this.trvExt.ImageList = this.imlFiles;
            this.trvExt.Name = "trvExt";
            this.trvExt.StateImageList = this.imlFiles;
            this.trvExt.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvExt_AfterSelect);
            // 
            // imlFiles
            // 
            this.imlFiles.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlFiles.ImageStream")));
            this.imlFiles.TransparentColor = System.Drawing.Color.Transparent;
            this.imlFiles.Images.SetKeyName(0, "hdd.ico");
            this.imlFiles.Images.SetKeyName(1, "text_plain.png");
            // 
            // btnFilter
            // 
            resources.ApplyResources(this.btnFilter, "btnFilter");
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtFilter
            // 
            resources.ApplyResources(this.txtFilter, "txtFilter");
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.ReadOnly = true;
            // 
            // btnScan
            // 
            resources.ApplyResources(this.btnScan, "btnScan");
            this.btnScan.Name = "btnScan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // cboDrives
            // 
            resources.ApplyResources(this.cboDrives, "cboDrives");
            this.cboDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDrives.FormattingEnabled = true;
            this.cboDrives.Name = "cboDrives";
            // 
            // fbdMain
            // 
            resources.ApplyResources(this.fbdMain, "fbdMain");
            // 
            // ucBottom
            // 
            resources.ApplyResources(this.ucBottom, "ucBottom");
            this.ucBottom.Name = "ucBottom";
            // 
            // ucTop
            // 
            resources.ApplyResources(this.ucTop, "ucTop");
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Name = "ucTop";
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.pnlClient);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.ucBottom);
            this.Controls.Add(this.ucTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Activated += new System.EventHandler(this.frmMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.frmMain_Layout);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.pnlClient.ResumeLayout(false);
            this.pnlClient.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tbpFolders.ResumeLayout(false);
            this.tbpTypes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		System.Windows.Forms.DataGridView dgvFiles;
		System.Windows.Forms.Timer tmrScan;
		System.Windows.Forms.DataGridViewButtonColumn dbcRestoreButton;
		System.Windows.Forms.DataGridViewCheckBoxColumn dccIsRecoverable;
		System.Windows.Forms.DataGridViewTextBoxColumn dtcSize;
		System.Windows.Forms.DataGridViewTextBoxColumn txcFilePath;
		System.Windows.Forms.DataGridViewTextBoxColumn txcFileId;
		System.Windows.Forms.StatusStrip stsMain;
		System.Windows.Forms.Panel pnlClient;
		System.Windows.Forms.ListView lvFiles;
		System.Windows.Forms.TabControl tcMain;
		System.Windows.Forms.TabPage tbpFolders;
		System.Windows.Forms.TabPage tbpTypes;
		System.Windows.Forms.Button btnFilter;
		System.Windows.Forms.TextBox txtFilter;
		System.Windows.Forms.Button btnScan;
		System.Windows.Forms.ComboBox cboDrives;
		System.Windows.Forms.CheckBox chkAll;
		System.Windows.Forms.CheckBox chkPreview;
		System.Windows.Forms.Button btnRestore;
		System.Windows.Forms.Panel pnlPreview;
		System.Windows.Forms.ToolStripStatusLabel tslFilter;
		System.Windows.Forms.ToolStripStatusLabel tslResults;
		System.Windows.Forms.ColumnHeader clhName;
		System.Windows.Forms.ColumnHeader clhState;
		System.Windows.Forms.ColumnHeader clhOriginalPath;
		System.Windows.Forms.ColumnHeader clhSize;
		System.Windows.Forms.TreeView trvFolders;
		System.Windows.Forms.ColumnHeader clhTag;
		System.Windows.Forms.TreeView trvExt;
		System.Windows.Forms.FolderBrowserDialog fbdMain;
		System.Windows.Forms.Button btnBrowse;
		System.Windows.Forms.ImageList imlFolders;
		System.Windows.Forms.ImageList imlFiles;
		BottomControl ucBottom;
		TopControl ucTop;
		bool filterLost;
	}
}

