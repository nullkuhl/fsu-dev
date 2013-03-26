using System.Resources;
using System.Globalization;
using System.Threading;

namespace EmptyFolderFinder
{
	partial class FormEmptyFolderFinder
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("EmptyFolderFinder.Resources",
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEmptyFolderFinder));
            this.lvMain = new System.Windows.Forms.ListView();
            this.clhFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imagesLarge = new System.Windows.Forms.ImageList(this.components);
            this.imagesSmall = new System.Windows.Forms.ImageList(this.components);
            this.tlsMain = new System.Windows.Forms.ToolStrip();
            this.tsbScan = new System.Windows.Forms.ToolStripButton();
            this.tss1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tss2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbExclude = new System.Windows.Forms.ToolStripButton();
            this.tss3 = new System.Windows.Forms.ToolStripSeparator();
            this.tdbCheck = new System.Windows.Forms.ToolStripDropDownButton();
            this.tmiCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiCheckNone = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiCheckInvert = new System.Windows.Forms.ToolStripMenuItem();
            this.bgwMain = new System.ComponentModel.BackgroundWorker();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tslScan = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelProgressBarSpacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspMain = new System.Windows.Forms.ToolStripProgressBar();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslAbort = new System.Windows.Forms.ToolStripStatusLabel();
            this.ucTop = new EmptyFolderFinder.TopControl();
            this.ucBottom = new EmptyFolderFinder.BottomControl();
            this.tlsMain.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMain
            // 
            this.lvMain.AllowColumnReorder = true;
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhFolder,
            this.clhPath});
            this.lvMain.FullRowSelect = true;
            this.lvMain.GridLines = true;
            this.lvMain.LargeImageList = this.imagesLarge;
            this.lvMain.Location = new System.Drawing.Point(0, 94);
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(781, 371);
            this.lvMain.SmallImageList = this.imagesSmall;
            this.lvMain.TabIndex = 0;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            this.lvMain.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMain_ColumnClick);
            this.lvMain.SelectedIndexChanged += new System.EventHandler(this.lvMain_SelectedIndexChanged);
            // 
            // clhFolder
            // 
            this.clhFolder.Text = "Folder Name";
            this.clhFolder.Width = 174;
            // 
            // clhPath
            // 
            this.clhPath.Text = "Folder Path";
            this.clhPath.Width = 603;
            // 
            // imagesLarge
            // 
            this.imagesLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imagesLarge.ImageSize = new System.Drawing.Size(16, 16);
            this.imagesLarge.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imagesSmall
            // 
            this.imagesSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imagesSmall.ImageSize = new System.Drawing.Size(16, 16);
            this.imagesSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tlsMain
            // 
            this.tlsMain.Dock = System.Windows.Forms.DockStyle.None;
            this.tlsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbScan,
            this.tss1,
            this.tsbDelete,
            this.tss2,
            this.tsbOpen,
            this.tsbExclude,
            this.tss3,
            this.tdbCheck});
            this.tlsMain.Location = new System.Drawing.Point(0, 69);
            this.tlsMain.Name = "tlsMain";
            this.tlsMain.Size = new System.Drawing.Size(556, 25);
            this.tlsMain.TabIndex = 3;
            this.tlsMain.Text = "toolStrip1";
            // 
            // tsbScan
            // 
            this.tsbScan.Image = global::EmptyFolderFinder.Properties.Resources._1303028214_old_edit_find;
            this.tsbScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScan.Name = "tsbScan";
            this.tsbScan.Size = new System.Drawing.Size(148, 22);
            this.tsbScan.Text = "Scan for Empty Folders";
            this.tsbScan.Click += new System.EventHandler(this.tsbScan_Click);
            // 
            // tss1
            // 
            this.tss1.Name = "tss1";
            this.tss1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbDelete
            // 
            this.tsbDelete.Image = global::EmptyFolderFinder.Properties.Resources._1303026989_101;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(150, 22);
            this.tsbDelete.Text = "Delete Checked Folders";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // tss2
            // 
            this.tss2.Name = "tss2";
            this.tss2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOpen
            // 
            this.tsbOpen.Image = global::EmptyFolderFinder.Properties.Resources._1303028470_folder_horizontal_open;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(92, 22);
            this.tsbOpen.Text = "Open Folder";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbExclude
            // 
            this.tsbExclude.Image = global::EmptyFolderFinder.Properties.Resources.dialog_cancel;
            this.tsbExclude.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExclude.Name = "tsbExclude";
            this.tsbExclude.Size = new System.Drawing.Size(67, 22);
            this.tsbExclude.Text = "Exclude";
            this.tsbExclude.Click += new System.EventHandler(this.tsbExclude_Click);
            // 
            // tss3
            // 
            this.tss3.Name = "tss3";
            this.tss3.Size = new System.Drawing.Size(6, 25);
            // 
            // tdbCheck
            // 
            this.tdbCheck.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiCheckAll,
            this.tmiCheckNone,
            this.tmiCheckInvert});
            this.tdbCheck.Image = global::EmptyFolderFinder.Properties.Resources._1303028498_finished_work;
            this.tdbCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tdbCheck.Name = "tdbCheck";
            this.tdbCheck.Size = new System.Drawing.Size(69, 22);
            this.tdbCheck.Text = "Check";
            // 
            // tmiCheckAll
            // 
            this.tmiCheckAll.Name = "tmiCheckAll";
            this.tmiCheckAll.Size = new System.Drawing.Size(140, 22);
            this.tmiCheckAll.Text = "Check All";
            this.tmiCheckAll.Click += new System.EventHandler(this.tmiCheckAll_Click);
            // 
            // tmiCheckNone
            // 
            this.tmiCheckNone.Name = "tmiCheckNone";
            this.tmiCheckNone.Size = new System.Drawing.Size(140, 22);
            this.tmiCheckNone.Text = "Check None";
            this.tmiCheckNone.Click += new System.EventHandler(this.tmiCheckNone_Click);
            // 
            // tmiCheckInvert
            // 
            this.tmiCheckInvert.Name = "tmiCheckInvert";
            this.tmiCheckInvert.Size = new System.Drawing.Size(140, 22);
            this.tmiCheckInvert.Text = "Check Invert";
            this.tmiCheckInvert.Click += new System.EventHandler(this.tmiCheckInvert_Click);
            // 
            // stsMain
            // 
            this.stsMain.AutoSize = false;
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslScan,
            this.toolStripStatusLabelProgressBarSpacer,
            this.tspMain,
            this.tslStatus,
            this.tslAbort});
            this.stsMain.Location = new System.Drawing.Point(0, 465);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(781, 22);
            this.stsMain.SizingGrip = false;
            this.stsMain.TabIndex = 4;
            this.stsMain.Text = "statusStrip1";
            // 
            // tslScan
            // 
            this.tslScan.ActiveLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.tslScan.AutoSize = false;
            this.tslScan.BackColor = System.Drawing.Color.Transparent;
            this.tslScan.Name = "tslScan";
            this.tslScan.Size = new System.Drawing.Size(125, 17);
            this.tslScan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelProgressBarSpacer
            // 
            this.toolStripStatusLabelProgressBarSpacer.AutoSize = false;
            this.toolStripStatusLabelProgressBarSpacer.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabelProgressBarSpacer.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.toolStripStatusLabelProgressBarSpacer.Name = "toolStripStatusLabelProgressBarSpacer";
            this.toolStripStatusLabelProgressBarSpacer.Size = new System.Drawing.Size(130, 16);
            // 
            // tspMain
            // 
            this.tspMain.MarqueeAnimationSpeed = 25;
            this.tspMain.Name = "tspMain";
            this.tspMain.Size = new System.Drawing.Size(130, 16);
            this.tspMain.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.tspMain.Visible = false;
            // 
            // tslStatus
            // 
            this.tslStatus.ActiveLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.tslStatus.AutoSize = false;
            this.tslStatus.BackColor = System.Drawing.Color.Transparent;
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(450, 17);
            this.tslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tslAbort
            // 
            this.tslAbort.AutoSize = false;
            this.tslAbort.BackColor = System.Drawing.Color.Transparent;
            this.tslAbort.IsLink = true;
            this.tslAbort.Name = "tslAbort";
            this.tslAbort.Size = new System.Drawing.Size(65, 17);
            this.tslAbort.Text = "Abort";
            this.tslAbort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tslAbort.ToolTipText = "Abort";
            this.tslAbort.Visible = false;
            this.tslAbort.Click += new System.EventHandler(this.toolStripStatusLabelAbort_Click);
            // 
            // ucTop
            // 
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTop.Location = new System.Drawing.Point(0, 0);
            this.ucTop.Name = "ucTop";
            this.ucTop.Size = new System.Drawing.Size(781, 64);
            this.ucTop.TabIndex = 15;
            // 
            // ucBottom
            // 
            this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucBottom.Location = new System.Drawing.Point(0, 487);
            this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
            this.ucBottom.MaximumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.MinimumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.Name = "ucBottom";
            this.ucBottom.Size = new System.Drawing.Size(781, 31);
            this.ucBottom.TabIndex = 16;
            // 
            // FormEmptyFolderFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(781, 518);
            this.Controls.Add(this.ucTop);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.tlsMain);
            this.Controls.Add(this.lvMain);
            this.Controls.Add(this.ucBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormEmptyFolderFinder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Empty Folder Finder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmEmptyFolderFinder_FormClosing);
            this.Load += new System.EventHandler(this.FrmEmptyFolderFinder_Load);
            this.Shown += new System.EventHandler(this.FrmEmptyFolderFinder_Shown);
            this.tlsMain.ResumeLayout(false);
            this.tlsMain.PerformLayout();
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		System.Windows.Forms.ListView lvMain;
		System.Windows.Forms.ImageList imagesSmall;
		System.Windows.Forms.ImageList imagesLarge;
		System.Windows.Forms.ToolStrip tlsMain;
		System.Windows.Forms.ToolStripButton tsbScan;
		System.Windows.Forms.ToolStripSeparator tss1;
		System.Windows.Forms.ToolStripButton tsbDelete;
		System.Windows.Forms.ToolStripSeparator tss2;
		System.Windows.Forms.ToolStripButton tsbOpen;
		System.Windows.Forms.ToolStripButton tsbExclude;
		System.Windows.Forms.ToolStripSeparator tss3;
		System.Windows.Forms.ColumnHeader clhFolder;
		System.Windows.Forms.ColumnHeader clhPath;
		System.ComponentModel.BackgroundWorker bgwMain;
		System.Windows.Forms.ToolStripDropDownButton tdbCheck;
		System.Windows.Forms.ToolStripMenuItem tmiCheckAll;
		System.Windows.Forms.ToolStripMenuItem tmiCheckNone;
		System.Windows.Forms.ToolStripMenuItem tmiCheckInvert;
		System.Windows.Forms.StatusStrip stsMain;
		System.Windows.Forms.ToolStripStatusLabel tslScan;
		System.Windows.Forms.ToolStripProgressBar tspMain;
		System.Windows.Forms.ToolStripStatusLabel tslStatus;
		TopControl ucTop;
		BottomControl ucBottom;
		System.Windows.Forms.ToolStripStatusLabel tslAbort;
		System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelProgressBarSpacer;
	}
}

