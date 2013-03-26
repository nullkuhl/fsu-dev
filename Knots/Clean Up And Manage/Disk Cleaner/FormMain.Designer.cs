using System.Resources;
using System.Globalization;
using System.Threading;

namespace Disk_Cleaner
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("Disk_Cleaner.Resources",
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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabPageCleanup = new System.Windows.Forms.TabPage();
            this.ProcessedFiles = new System.Windows.Forms.ListView();
            this.clhFiles = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblWait = new System.Windows.Forms.Label();
            this.lblSelectDrives = new System.Windows.Forms.Label();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.pcbHDD = new System.Windows.Forms.PictureBox();
            this.lblDiskSpace = new System.Windows.Forms.Label();
            this.grbDescription = new System.Windows.Forms.GroupBox();
            this.buttonViewFiles = new System.Windows.Forms.Button();
            this.checkBoxGroup = new System.Windows.Forms.CheckBox();
            this.labelGroup = new System.Windows.Forms.Label();
            this.labelGain = new System.Windows.Forms.Label();
            this.listViewJunk = new System.Windows.Forms.ListView();
            this.clhObjects = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelProgress = new System.Windows.Forms.Label();
            this.progressBarSearch = new System.Windows.Forms.ProgressBar();
            this.listViewScanning = new System.Windows.Forms.ListView();
            this.clhNames = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonOptions = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblClickNext = new System.Windows.Forms.Label();
            this.listViewDrives = new System.Windows.Forms.ListView();
            this.clhName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhTotalSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhFree = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelClean = new System.Windows.Forms.Label();
            this.labelFinal = new System.Windows.Forms.Label();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.grbRestore = new System.Windows.Forms.GroupBox();
            this.pcbRestore = new System.Windows.Forms.PictureBox();
            this.buttonRestore = new System.Windows.Forms.Button();
            this.lblRestore = new System.Windows.Forms.Label();
            this.grbWinComponents = new System.Windows.Forms.GroupBox();
            this.pcbWinComponents = new System.Windows.Forms.PictureBox();
            this.buttonCleanWin = new System.Windows.Forms.Button();
            this.lblFreeSpace = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.ucBottom = new Disk_Cleaner.BottomControl();
            this.ucTop = new Disk_Cleaner.TopControl();
            this.tcMain.SuspendLayout();
            this.tabPageCleanup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbHDD)).BeginInit();
            this.grbDescription.SuspendLayout();
            this.tabPageOptions.SuspendLayout();
            this.grbRestore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbRestore)).BeginInit();
            this.grbWinComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbWinComponents)).BeginInit();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabPageCleanup);
            this.tcMain.Controls.Add(this.tabPageOptions);
            this.tcMain.HotTrack = true;
            this.tcMain.Location = new System.Drawing.Point(8, 70);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(424, 411);
            this.tcMain.TabIndex = 0;
            // 
            // tabPageCleanup
            // 
            this.tabPageCleanup.Controls.Add(this.ProcessedFiles);
            this.tabPageCleanup.Controls.Add(this.lblWait);
            this.tabPageCleanup.Controls.Add(this.lblSelectDrives);
            this.tabPageCleanup.Controls.Add(this.refreshBtn);
            this.tabPageCleanup.Controls.Add(this.pcbHDD);
            this.tabPageCleanup.Controls.Add(this.lblDiskSpace);
            this.tabPageCleanup.Controls.Add(this.grbDescription);
            this.tabPageCleanup.Controls.Add(this.labelGain);
            this.tabPageCleanup.Controls.Add(this.listViewJunk);
            this.tabPageCleanup.Controls.Add(this.labelProgress);
            this.tabPageCleanup.Controls.Add(this.progressBarSearch);
            this.tabPageCleanup.Controls.Add(this.listViewScanning);
            this.tabPageCleanup.Controls.Add(this.buttonNext);
            this.tabPageCleanup.Controls.Add(this.buttonBack);
            this.tabPageCleanup.Controls.Add(this.buttonOptions);
            this.tabPageCleanup.Controls.Add(this.pnlMain);
            this.tabPageCleanup.Controls.Add(this.lblClickNext);
            this.tabPageCleanup.Controls.Add(this.listViewDrives);
            this.tabPageCleanup.Controls.Add(this.labelClean);
            this.tabPageCleanup.Controls.Add(this.labelFinal);
            this.tabPageCleanup.Location = new System.Drawing.Point(4, 22);
            this.tabPageCleanup.Name = "tabPageCleanup";
            this.tabPageCleanup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCleanup.Size = new System.Drawing.Size(416, 385);
            this.tabPageCleanup.TabIndex = 0;
            this.tabPageCleanup.Text = "Disk Cleanup";
            this.tabPageCleanup.UseVisualStyleBackColor = true;
            // 
            // ProcessedFiles
            // 
            this.ProcessedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessedFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhFiles});
            this.ProcessedFiles.FullRowSelect = true;
            this.ProcessedFiles.HideSelection = false;
            this.ProcessedFiles.Location = new System.Drawing.Point(16, 60);
            this.ProcessedFiles.Name = "ProcessedFiles";
            this.ProcessedFiles.Size = new System.Drawing.Size(385, 273);
            this.ProcessedFiles.TabIndex = 19;
            this.ProcessedFiles.UseCompatibleStateImageBehavior = false;
            this.ProcessedFiles.View = System.Windows.Forms.View.Details;
            this.ProcessedFiles.Visible = false;
            // 
            // clhFiles
            // 
            this.clhFiles.Text = "Files";
            this.clhFiles.Width = 380;
            // 
            // lblWait
            // 
            this.lblWait.Location = new System.Drawing.Point(54, 15);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(356, 37);
            this.lblWait.TabIndex = 3;
            this.lblWait.Tag = "2";
            this.lblWait.Text = "Please wait until Disk Cleaner finishes scanning the selected drives.";
            // 
            // lblSelectDrives
            // 
            this.lblSelectDrives.AutoSize = true;
            this.lblSelectDrives.Location = new System.Drawing.Point(54, 20);
            this.lblSelectDrives.Name = "lblSelectDrives";
            this.lblSelectDrives.Size = new System.Drawing.Size(188, 13);
            this.lblSelectDrives.TabIndex = 0;
            this.lblSelectDrives.Tag = "1";
            this.lblSelectDrives.Text = "Select the drives you want to cleanup.";
            // 
            // refreshBtn
            // 
            this.refreshBtn.Image = global::Disk_Cleaner.Properties.Resources.refresh_small;
            this.refreshBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.refreshBtn.Location = new System.Drawing.Point(314, 16);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(86, 23);
            this.refreshBtn.TabIndex = 18;
            this.refreshBtn.Text = "Refresh";
            this.refreshBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // pcbHDD
            // 
            this.pcbHDD.Image = global::Disk_Cleaner.Properties.Resources._1305101708_hdd;
            this.pcbHDD.Location = new System.Drawing.Point(15, 12);
            this.pcbHDD.Name = "pcbHDD";
            this.pcbHDD.Size = new System.Drawing.Size(33, 45);
            this.pcbHDD.TabIndex = 17;
            this.pcbHDD.TabStop = false;
            // 
            // lblDiskSpace
            // 
            this.lblDiskSpace.AutoSize = true;
            this.lblDiskSpace.Location = new System.Drawing.Point(12, 233);
            this.lblDiskSpace.Name = "lblDiskSpace";
            this.lblDiskSpace.Size = new System.Drawing.Size(181, 13);
            this.lblDiskSpace.TabIndex = 13;
            this.lblDiskSpace.Tag = "3";
            this.lblDiskSpace.Text = "Total amount of disk space you gain:";
            // 
            // grbDescription
            // 
            this.grbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grbDescription.Controls.Add(this.buttonViewFiles);
            this.grbDescription.Controls.Add(this.checkBoxGroup);
            this.grbDescription.Controls.Add(this.labelGroup);
            this.grbDescription.Location = new System.Drawing.Point(15, 247);
            this.grbDescription.Name = "grbDescription";
            this.grbDescription.Size = new System.Drawing.Size(385, 86);
            this.grbDescription.TabIndex = 11;
            this.grbDescription.TabStop = false;
            this.grbDescription.Tag = "3";
            this.grbDescription.Text = "Description";
            // 
            // buttonViewFiles
            // 
            this.buttonViewFiles.Location = new System.Drawing.Point(301, 42);
            this.buttonViewFiles.Name = "buttonViewFiles";
            this.buttonViewFiles.Size = new System.Drawing.Size(75, 38);
            this.buttonViewFiles.TabIndex = 2;
            this.buttonViewFiles.Text = "View Files";
            this.buttonViewFiles.UseVisualStyleBackColor = true;
            this.buttonViewFiles.Click += new System.EventHandler(this.buttonViewFiles_Click);
            // 
            // checkBoxGroup
            // 
            this.checkBoxGroup.AutoSize = true;
            this.checkBoxGroup.Location = new System.Drawing.Point(6, 51);
            this.checkBoxGroup.Name = "checkBoxGroup";
            this.checkBoxGroup.Size = new System.Drawing.Size(15, 14);
            this.checkBoxGroup.TabIndex = 1;
            this.checkBoxGroup.UseVisualStyleBackColor = true;
            this.checkBoxGroup.CheckedChanged += new System.EventHandler(this.checkBoxGroup_CheckedChanged);
            // 
            // labelGroup
            // 
            this.labelGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGroup.Location = new System.Drawing.Point(3, 16);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(379, 67);
            this.labelGroup.TabIndex = 0;
            // 
            // labelGain
            // 
            this.labelGain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGain.Location = new System.Drawing.Point(250, 233);
            this.labelGain.Name = "labelGain";
            this.labelGain.Size = new System.Drawing.Size(150, 28);
            this.labelGain.TabIndex = 12;
            this.labelGain.Tag = "3";
            this.labelGain.Text = "N/A";
            this.labelGain.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // listViewJunk
            // 
            this.listViewJunk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewJunk.CheckBoxes = true;
            this.listViewJunk.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhObjects,
            this.clhFile,
            this.clhSize});
            this.listViewJunk.FullRowSelect = true;
            this.listViewJunk.HideSelection = false;
            this.listViewJunk.Location = new System.Drawing.Point(15, 60);
            this.listViewJunk.Name = "listViewJunk";
            this.listViewJunk.Size = new System.Drawing.Size(385, 170);
            this.listViewJunk.TabIndex = 10;
            this.listViewJunk.Tag = "3";
            this.listViewJunk.UseCompatibleStateImageBehavior = false;
            this.listViewJunk.View = System.Windows.Forms.View.Details;
            this.listViewJunk.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewJunk_ItemChecked);
            this.listViewJunk.SelectedIndexChanged += new System.EventHandler(this.listViewJunk_SelectedIndexChanged);
            // 
            // clhObjects
            // 
            this.clhObjects.Text = "Objects";
            this.clhObjects.Width = 150;
            // 
            // clhFile
            // 
            this.clhFile.Text = "Files";
            this.clhFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clhFile.Width = 80;
            // 
            // clhSize
            // 
            this.clhSize.Text = "Size";
            this.clhSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clhSize.Width = 120;
            // 
            // labelProgress
            // 
            this.labelProgress.Location = new System.Drawing.Point(12, 273);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(384, 51);
            this.labelProgress.TabIndex = 9;
            this.labelProgress.Tag = "2";
            this.labelProgress.Text = "N/A";
            // 
            // progressBarSearch
            // 
            this.progressBarSearch.Location = new System.Drawing.Point(15, 247);
            this.progressBarSearch.Name = "progressBarSearch";
            this.progressBarSearch.Size = new System.Drawing.Size(381, 23);
            this.progressBarSearch.TabIndex = 8;
            this.progressBarSearch.Tag = "2";
            // 
            // listViewScanning
            // 
            this.listViewScanning.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewScanning.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhNames,
            this.clhStatus});
            this.listViewScanning.HideSelection = false;
            this.listViewScanning.Location = new System.Drawing.Point(15, 60);
            this.listViewScanning.Name = "listViewScanning";
            this.listViewScanning.Size = new System.Drawing.Size(385, 170);
            this.listViewScanning.TabIndex = 7;
            this.listViewScanning.Tag = "2";
            this.listViewScanning.UseCompatibleStateImageBehavior = false;
            this.listViewScanning.View = System.Windows.Forms.View.Details;
            // 
            // clhNames
            // 
            this.clhNames.Text = "Name";
            this.clhNames.Width = 230;
            // 
            // clhStatus
            // 
            this.clhStatus.Text = "Status";
            this.clhStatus.Width = 120;
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(330, 350);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 6;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Enabled = false;
            this.buttonBack.Location = new System.Drawing.Point(240, 350);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 5;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonOptions
            // 
            this.buttonOptions.Location = new System.Drawing.Point(5, 350);
            this.buttonOptions.Name = "buttonOptions";
            this.buttonOptions.Size = new System.Drawing.Size(75, 23);
            this.buttonOptions.TabIndex = 4;
            this.buttonOptions.Tag = "1";
            this.buttonOptions.Text = "Options";
            this.buttonOptions.UseVisualStyleBackColor = true;
            this.buttonOptions.Click += new System.EventHandler(this.buttonOptions_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Location = new System.Drawing.Point(6, 339);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(400, 4);
            this.pnlMain.TabIndex = 3;
            // 
            // lblClickNext
            // 
            this.lblClickNext.AutoSize = true;
            this.lblClickNext.Location = new System.Drawing.Point(12, 273);
            this.lblClickNext.Name = "lblClickNext";
            this.lblClickNext.Size = new System.Drawing.Size(270, 13);
            this.lblClickNext.TabIndex = 2;
            this.lblClickNext.Tag = "1";
            this.lblClickNext.Text = "Click next when you have finished selecting your drives.";
            // 
            // listViewDrives
            // 
            this.listViewDrives.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDrives.CheckBoxes = true;
            this.listViewDrives.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhName,
            this.clhTotalSize,
            this.clhFree});
            this.listViewDrives.HideSelection = false;
            this.listViewDrives.Location = new System.Drawing.Point(15, 60);
            this.listViewDrives.Name = "listViewDrives";
            this.listViewDrives.Size = new System.Drawing.Size(385, 201);
            this.listViewDrives.TabIndex = 1;
            this.listViewDrives.Tag = "1";
            this.listViewDrives.UseCompatibleStateImageBehavior = false;
            this.listViewDrives.View = System.Windows.Forms.View.Details;
            // 
            // clhName
            // 
            this.clhName.Text = "Name";
            this.clhName.Width = 150;
            // 
            // clhTotalSize
            // 
            this.clhTotalSize.Text = "Total Size";
            this.clhTotalSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clhTotalSize.Width = 100;
            // 
            // clhFree
            // 
            this.clhFree.Text = "Free Size";
            this.clhFree.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clhFree.Width = 100;
            // 
            // labelClean
            // 
            this.labelClean.AutoSize = true;
            this.labelClean.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClean.Location = new System.Drawing.Point(51, 20);
            this.labelClean.Name = "labelClean";
            this.labelClean.Size = new System.Drawing.Size(30, 13);
            this.labelClean.TabIndex = 15;
            this.labelClean.Tag = "4";
            this.labelClean.Text = "N/A";
            // 
            // labelFinal
            // 
            this.labelFinal.Location = new System.Drawing.Point(53, 15);
            this.labelFinal.Name = "labelFinal";
            this.labelFinal.Size = new System.Drawing.Size(348, 45);
            this.labelFinal.TabIndex = 14;
            this.labelFinal.Tag = "3";
            this.labelFinal.Text = "N/A";
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.Controls.Add(this.grbRestore);
            this.tabPageOptions.Controls.Add(this.grbWinComponents);
            this.tabPageOptions.Location = new System.Drawing.Point(4, 22);
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOptions.Size = new System.Drawing.Size(416, 385);
            this.tabPageOptions.TabIndex = 1;
            this.tabPageOptions.Text = "More Options";
            this.tabPageOptions.UseVisualStyleBackColor = true;
            // 
            // grbRestore
            // 
            this.grbRestore.Controls.Add(this.pcbRestore);
            this.grbRestore.Controls.Add(this.buttonRestore);
            this.grbRestore.Controls.Add(this.lblRestore);
            this.grbRestore.Location = new System.Drawing.Point(15, 130);
            this.grbRestore.Name = "grbRestore";
            this.grbRestore.Size = new System.Drawing.Size(382, 100);
            this.grbRestore.TabIndex = 1;
            this.grbRestore.TabStop = false;
            this.grbRestore.Text = "Restore";
            // 
            // pcbRestore
            // 
            this.pcbRestore.Image = global::Disk_Cleaner.Properties.Resources._1305102170_view_restore;
            this.pcbRestore.Location = new System.Drawing.Point(14, 27);
            this.pcbRestore.Name = "pcbRestore";
            this.pcbRestore.Size = new System.Drawing.Size(37, 63);
            this.pcbRestore.TabIndex = 2;
            this.pcbRestore.TabStop = false;
            // 
            // buttonRestore
            // 
            this.buttonRestore.Location = new System.Drawing.Point(259, 62);
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.Size = new System.Drawing.Size(104, 23);
            this.buttonRestore.TabIndex = 0;
            this.buttonRestore.Text = "Restore...";
            this.buttonRestore.UseVisualStyleBackColor = true;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // lblRestore
            // 
            this.lblRestore.Location = new System.Drawing.Point(58, 27);
            this.lblRestore.Name = "lblRestore";
            this.lblRestore.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblRestore.Size = new System.Drawing.Size(306, 72);
            this.lblRestore.TabIndex = 1;
            this.lblRestore.Text = "You can restore the removed temporary files from a backup folder.";
            // 
            // grbWinComponents
            // 
            this.grbWinComponents.Controls.Add(this.pcbWinComponents);
            this.grbWinComponents.Controls.Add(this.buttonCleanWin);
            this.grbWinComponents.Controls.Add(this.lblFreeSpace);
            this.grbWinComponents.Location = new System.Drawing.Point(15, 15);
            this.grbWinComponents.Name = "grbWinComponents";
            this.grbWinComponents.Size = new System.Drawing.Size(382, 100);
            this.grbWinComponents.TabIndex = 0;
            this.grbWinComponents.TabStop = false;
            this.grbWinComponents.Text = "Windows components";
            // 
            // pcbWinComponents
            // 
            this.pcbWinComponents.Image = global::Disk_Cleaner.Properties.Resources._1305101876_server_components;
            this.pcbWinComponents.Location = new System.Drawing.Point(14, 25);
            this.pcbWinComponents.Name = "pcbWinComponents";
            this.pcbWinComponents.Size = new System.Drawing.Size(38, 60);
            this.pcbWinComponents.TabIndex = 4;
            this.pcbWinComponents.TabStop = false;
            // 
            // buttonCleanWin
            // 
            this.buttonCleanWin.Location = new System.Drawing.Point(259, 62);
            this.buttonCleanWin.Name = "buttonCleanWin";
            this.buttonCleanWin.Size = new System.Drawing.Size(104, 23);
            this.buttonCleanWin.TabIndex = 3;
            this.buttonCleanWin.Text = "Clean up...";
            this.buttonCleanWin.UseVisualStyleBackColor = true;
            this.buttonCleanWin.Click += new System.EventHandler(this.buttonCleanWin_Click);
            // 
            // lblFreeSpace
            // 
            this.lblFreeSpace.Location = new System.Drawing.Point(58, 25);
            this.lblFreeSpace.Name = "lblFreeSpace";
            this.lblFreeSpace.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblFreeSpace.Size = new System.Drawing.Size(306, 72);
            this.lblFreeSpace.TabIndex = 2;
            this.lblFreeSpace.Text = "You can free more disk space by removing optional Windows components that you do " +
                "not use.";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(326, 486);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(102, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ucBottom
            // 
            this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucBottom.Location = new System.Drawing.Point(0, 516);
            this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
            this.ucBottom.MaximumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.MinimumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.Name = "ucBottom";
            this.ucBottom.Size = new System.Drawing.Size(444, 31);
            this.ucBottom.TabIndex = 3;
            // 
            // ucTop
            // 
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTop.Location = new System.Drawing.Point(0, 0);
            this.ucTop.Name = "ucTop";
            this.ucTop.Size = new System.Drawing.Size(444, 64);
            this.ucTop.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(444, 547);
            this.Controls.Add(this.ucBottom);
            this.Controls.Add(this.ucTop);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.tcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Disk Cleaner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.tcMain.ResumeLayout(false);
            this.tabPageCleanup.ResumeLayout(false);
            this.tabPageCleanup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbHDD)).EndInit();
            this.grbDescription.ResumeLayout(false);
            this.grbDescription.PerformLayout();
            this.tabPageOptions.ResumeLayout(false);
            this.grbRestore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbRestore)).EndInit();
            this.grbWinComponents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbWinComponents)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		System.Windows.Forms.TabControl tcMain;
		System.Windows.Forms.TabPage tabPageCleanup;
		System.Windows.Forms.TabPage tabPageOptions;
		System.Windows.Forms.Button buttonClose;
		System.Windows.Forms.Label lblSelectDrives;
		System.Windows.Forms.ListView listViewDrives;
		System.Windows.Forms.ColumnHeader clhName;
		System.Windows.Forms.ColumnHeader clhTotalSize;
		System.Windows.Forms.ColumnHeader clhFree;
		System.Windows.Forms.Label lblClickNext;
		System.Windows.Forms.Panel pnlMain;
		System.Windows.Forms.Button buttonNext;
		System.Windows.Forms.Button buttonBack;
		System.Windows.Forms.Button buttonOptions;
		System.Windows.Forms.ListView listViewScanning;
		System.Windows.Forms.ColumnHeader clhNames;
		System.Windows.Forms.ColumnHeader clhStatus;
		System.Windows.Forms.Label lblWait;
		System.Windows.Forms.Label labelProgress;
		System.Windows.Forms.ProgressBar progressBarSearch;
		System.Windows.Forms.ListView listViewJunk;
		System.Windows.Forms.ColumnHeader clhObjects;
		System.Windows.Forms.ColumnHeader clhFile;
		System.Windows.Forms.ColumnHeader clhSize;
		System.Windows.Forms.GroupBox grbDescription;
		System.Windows.Forms.Label labelGain;
		System.Windows.Forms.Label lblDiskSpace;
		System.Windows.Forms.Label labelGroup;
		System.Windows.Forms.Button buttonViewFiles;
		System.Windows.Forms.CheckBox checkBoxGroup;
		System.Windows.Forms.Label labelFinal;
		System.Windows.Forms.Label labelClean;
		System.Windows.Forms.GroupBox grbRestore;
		System.Windows.Forms.Button buttonRestore;
		System.Windows.Forms.Label lblRestore;
		System.Windows.Forms.GroupBox grbWinComponents;
		System.Windows.Forms.Button buttonCleanWin;
		System.Windows.Forms.Label lblFreeSpace;
		System.Windows.Forms.PictureBox pcbHDD;
		System.Windows.Forms.PictureBox pcbWinComponents;
		System.Windows.Forms.PictureBox pcbRestore;
		System.Windows.Forms.Button refreshBtn;
		TopControl ucTop;
		BottomControl ucBottom;
		System.Windows.Forms.ListView ProcessedFiles;
		System.Windows.Forms.ColumnHeader clhFiles;
	}
}

