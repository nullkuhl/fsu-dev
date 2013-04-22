using System.Resources;
using System.Globalization;
using System.Threading;

namespace FreemiumUtilities.TracksEraser
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("TracksEraser.Resources",
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Windows Recent Documents");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Windows Start Menu Run");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Microsoft Common Dialog - File/Folder List");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Windows Clipboard");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Windows Temporary Files");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Windows Recycle Bin");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Windows Mapped Network Drives");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Start Menu Click History");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("My Network Places");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Windows", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Internet Explorer - URL History");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Internet Explorer History");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Internet Explorer Cookies");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Intelligent Forms - Auto Complete Passwords");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Temporary Internet Files");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Index.dat Files");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Internet Explorer", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16});
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Windows Media Player");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("QuickTime Player");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Macromedia Flash Player");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Microsoft Office");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("MS Management Console");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("MS Wordpad");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("MS Paint");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Winrar");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Sun Java");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Windows Defender");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Plug-ins", 2, 2, new System.Windows.Forms.TreeNode[] {
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23,
            treeNode24,
            treeNode25,
            treeNode26,
            treeNode27});
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Mozilla Firefox - History");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Mozilla Firefox - Cookies");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Mozilla Firefox - Internet Cache");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Mozilla Firefox - Saved Form Information");
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Mozilla Firefox - Saved Passwords");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("Mozilla Firefox - Download Manager History");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Mozilla Firefox - Search History");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("Mozilla Firefox", 4, 4, new System.Windows.Forms.TreeNode[] {
            treeNode29,
            treeNode30,
            treeNode31,
            treeNode32,
            treeNode33,
            treeNode34,
            treeNode35});
            System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("Google Chrome - History");
            System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("Google Chrome - Cookies");
            System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("Google Chrome - Internet Cache");
            System.Windows.Forms.TreeNode treeNode40 = new System.Windows.Forms.TreeNode("Google Chrome - Saved From Information");
            System.Windows.Forms.TreeNode treeNode41 = new System.Windows.Forms.TreeNode("Google Chrome - Download History");
            System.Windows.Forms.TreeNode treeNode42 = new System.Windows.Forms.TreeNode("Google Chrome", 3, 3, new System.Windows.Forms.TreeNode[] {
            treeNode37,
            treeNode38,
            treeNode39,
            treeNode40,
            treeNode41});
            this.tlsMain = new System.Windows.Forms.ToolStrip();
            this.tsbAnalyse = new System.Windows.Forms.ToolStripButton();
            this.tsbErase = new System.Windows.Forms.ToolStripButton();
            this.tssFirst = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOptions = new System.Windows.Forms.ToolStripButton();
            this.tssSecond = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCheck = new System.Windows.Forms.ToolStripDropDownButton();
            this.tmiCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiUncheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ImgIcons = new System.Windows.Forms.ImageList(this.components);
            this.spcTracks = new System.Windows.Forms.SplitContainer();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tbpTracks = new System.Windows.Forms.TabPage();
            this.spcResults = new System.Windows.Forms.SplitContainer();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.lblResults = new System.Windows.Forms.Label();
            this.Scanninglbl = new System.Windows.Forms.Label();
            this.pcbScanning = new System.Windows.Forms.PictureBox();
            this.btnAnalyse = new System.Windows.Forms.Button();
            this.RunEraser = new System.Windows.Forms.Button();
            this.ucBottom = new FreemiumUtilities.TracksEraser.BottomControl();
            this.ucTop = new FreemiumUtilities.TracksEraser.TopControl();
            this.trvTracks = new FreemiumUtilities.TracksEraser.TriStateTreeView();
            this.tlsMain.SuspendLayout();
            this.spcTracks.Panel1.SuspendLayout();
            this.spcTracks.Panel2.SuspendLayout();
            this.spcTracks.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tbpTracks.SuspendLayout();
            this.spcResults.Panel1.SuspendLayout();
            this.spcResults.Panel2.SuspendLayout();
            this.spcResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbScanning)).BeginInit();
            this.SuspendLayout();
            // 
            // tlsMain
            // 
            this.tlsMain.Dock = System.Windows.Forms.DockStyle.None;
            this.tlsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAnalyse,
            this.tsbErase,
            this.tssFirst,
            this.tsbOptions,
            this.tssSecond,
            this.tsbCheck});
            this.tlsMain.Location = new System.Drawing.Point(0, 67);
            this.tlsMain.MinimumSize = new System.Drawing.Size(900, 0);
            this.tlsMain.Name = "tlsMain";
            this.tlsMain.Size = new System.Drawing.Size(900, 25);
            this.tlsMain.Stretch = true;
            this.tlsMain.TabIndex = 1;
            this.tlsMain.Text = "toolStrip1";
            // 
            // tsbAnalyse
            // 
            this.tsbAnalyse.Image = ((System.Drawing.Image)(resources.GetObject("tsbAnalyse.Image")));
            this.tsbAnalyse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAnalyse.Name = "tsbAnalyse";
            this.tsbAnalyse.Size = new System.Drawing.Size(68, 22);
            this.tsbAnalyse.Text = "Analyse";
            this.tsbAnalyse.Click += new System.EventHandler(this.tsbAnalyse_Click);
            // 
            // tsbErase
            // 
            this.tsbErase.Image = ((System.Drawing.Image)(resources.GetObject("tsbErase.Image")));
            this.tsbErase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbErase.Name = "tsbErase";
            this.tsbErase.Size = new System.Drawing.Size(135, 22);
            this.tsbErase.Text = "Erase checked tracks";
            this.tsbErase.Click += new System.EventHandler(this.tsbErase_Click);
            // 
            // tssFirst
            // 
            this.tssFirst.Name = "tssFirst";
            this.tssFirst.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOptions
            // 
            this.tsbOptions.Image = ((System.Drawing.Image)(resources.GetObject("tsbOptions.Image")));
            this.tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOptions.Name = "tsbOptions";
            this.tsbOptions.Size = new System.Drawing.Size(69, 22);
            this.tsbOptions.Text = "Options";
            this.tsbOptions.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // tssSecond
            // 
            this.tssSecond.Name = "tssSecond";
            this.tssSecond.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCheck
            // 
            this.tsbCheck.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiCheckAll,
            this.tmiUncheckAll});
            this.tsbCheck.Image = ((System.Drawing.Image)(resources.GetObject("tsbCheck.Image")));
            this.tsbCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheck.Name = "tsbCheck";
            this.tsbCheck.Size = new System.Drawing.Size(69, 22);
            this.tsbCheck.Text = "Check";
            // 
            // tmiCheckAll
            // 
            this.tmiCheckAll.Name = "tmiCheckAll";
            this.tmiCheckAll.Size = new System.Drawing.Size(139, 22);
            this.tmiCheckAll.Text = "Check All";
            this.tmiCheckAll.Click += new System.EventHandler(this.tmiCheckAll_Click);
            // 
            // tmiUncheckAll
            // 
            this.tmiUncheckAll.Name = "tmiUncheckAll";
            this.tmiUncheckAll.Size = new System.Drawing.Size(139, 22);
            this.tmiUncheckAll.Text = "Check None";
            this.tmiUncheckAll.Click += new System.EventHandler(this.tmiUncheckAll_Click);
            // 
            // ImgIcons
            // 
            this.ImgIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgIcons.ImageStream")));
            this.ImgIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ImgIcons.Images.SetKeyName(0, "internet_explorer.png");
            this.ImgIcons.Images.SetKeyName(1, "windows_vista.ico");
            this.ImgIcons.Images.SetKeyName(2, "plugin_disabled.png");
            this.ImgIcons.Images.SetKeyName(3, "hp_chrome.png");
            this.ImgIcons.Images.SetKeyName(4, "firefox_original.png");
            this.ImgIcons.Images.SetKeyName(5, "bullet_white.png");
            this.ImgIcons.Images.SetKeyName(6, "transparent.ico");
            this.ImgIcons.Images.SetKeyName(7, "icon-error.png");
            // 
            // spcTracks
            // 
            this.spcTracks.Location = new System.Drawing.Point(0, 95);
            this.spcTracks.Name = "spcTracks";
            // 
            // spcTracks.Panel1
            // 
            this.spcTracks.Panel1.Controls.Add(this.tcMain);
            // 
            // spcTracks.Panel2
            // 
            this.spcTracks.Panel2.Controls.Add(this.spcResults);
            this.spcTracks.Size = new System.Drawing.Size(828, 514);
            this.spcTracks.SplitterDistance = 250;
            this.spcTracks.TabIndex = 4;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tbpTracks);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(250, 514);
            this.tcMain.TabIndex = 0;
            // 
            // tbpTracks
            // 
            this.tbpTracks.Controls.Add(this.trvTracks);
            this.tbpTracks.Location = new System.Drawing.Point(4, 22);
            this.tbpTracks.Name = "tbpTracks";
            this.tbpTracks.Padding = new System.Windows.Forms.Padding(3);
            this.tbpTracks.Size = new System.Drawing.Size(242, 488);
            this.tbpTracks.TabIndex = 0;
            this.tbpTracks.Text = "Tracks";
            this.tbpTracks.UseVisualStyleBackColor = true;
            // 
            // spcResults
            // 
            this.spcResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcResults.IsSplitterFixed = true;
            this.spcResults.Location = new System.Drawing.Point(0, 0);
            this.spcResults.Name = "spcResults";
            this.spcResults.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcResults.Panel1
            // 
            this.spcResults.Panel1.Controls.Add(this.txtResults);
            this.spcResults.Panel1.Controls.Add(this.lblResults);
            this.spcResults.Panel1MinSize = 480;
            // 
            // spcResults.Panel2
            // 
            this.spcResults.Panel2.Controls.Add(this.Scanninglbl);
            this.spcResults.Panel2.Controls.Add(this.pcbScanning);
            this.spcResults.Panel2.Controls.Add(this.btnAnalyse);
            this.spcResults.Panel2.Controls.Add(this.RunEraser);
            this.spcResults.Panel2MinSize = 10;
            this.spcResults.Size = new System.Drawing.Size(574, 514);
            this.spcResults.SplitterDistance = 480;
            this.spcResults.SplitterWidth = 1;
            this.spcResults.TabIndex = 0;
            // 
            // txtResults
            // 
            this.txtResults.BackColor = System.Drawing.Color.White;
            this.txtResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResults.Location = new System.Drawing.Point(0, 20);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResults.Size = new System.Drawing.Size(574, 460);
            this.txtResults.TabIndex = 1;
            this.txtResults.WordWrap = false;
            // 
            // lblResults
            // 
            this.lblResults.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblResults.Location = new System.Drawing.Point(0, 0);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(574, 20);
            this.lblResults.TabIndex = 0;
            this.lblResults.Text = "Results";
            // 
            // Scanninglbl
            // 
            this.Scanninglbl.AutoSize = true;
            this.Scanninglbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.Scanninglbl.Location = new System.Drawing.Point(26, 0);
            this.Scanninglbl.MinimumSize = new System.Drawing.Size(200, 30);
            this.Scanninglbl.Name = "Scanninglbl";
            this.Scanninglbl.Padding = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.Scanninglbl.Size = new System.Drawing.Size(200, 30);
            this.Scanninglbl.TabIndex = 5;
            this.Scanninglbl.Text = "Scanning";
            this.Scanninglbl.Visible = false;
            // 
            // pcbScanning
            // 
            this.pcbScanning.Dock = System.Windows.Forms.DockStyle.Left;
            this.pcbScanning.Image = TracksEraser.Properties.Resources.scanning;
            this.pcbScanning.InitialImage = null;
            this.pcbScanning.Location = new System.Drawing.Point(0, 0);
            this.pcbScanning.Margin = new System.Windows.Forms.Padding(2);
            this.pcbScanning.Name = "pcbScanning";
            this.pcbScanning.Padding = new System.Windows.Forms.Padding(10, 9, 0, 0);
            this.pcbScanning.Size = new System.Drawing.Size(26, 33);
            this.pcbScanning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pcbScanning.TabIndex = 4;
            this.pcbScanning.TabStop = false;
            this.pcbScanning.Visible = false;
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAnalyse.Location = new System.Drawing.Point(294, 0);
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.Size = new System.Drawing.Size(140, 33);
            this.btnAnalyse.TabIndex = 2;
            this.btnAnalyse.Text = "Analyse";
            this.btnAnalyse.UseVisualStyleBackColor = true;
            this.btnAnalyse.Click += new System.EventHandler(this.btnAnalyse_Click);
            // 
            // RunEraser
            // 
            this.RunEraser.Dock = System.Windows.Forms.DockStyle.Right;
            this.RunEraser.Enabled = false;
            this.RunEraser.Location = new System.Drawing.Point(434, 0);
            this.RunEraser.Margin = new System.Windows.Forms.Padding(30, 3, 30, 3);
            this.RunEraser.Name = "RunEraser";
            this.RunEraser.Size = new System.Drawing.Size(140, 33);
            this.RunEraser.TabIndex = 3;
            this.RunEraser.Text = "Run Eraser";
            this.RunEraser.UseVisualStyleBackColor = true;
            this.RunEraser.Click += new System.EventHandler(this.tsbErase_Click);
            // 
            // ucBottom
            // 
            this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucBottom.Location = new System.Drawing.Point(0, 612);
            this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
            this.ucBottom.MaximumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.MinimumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.Name = "ucBottom";
            this.ucBottom.Size = new System.Drawing.Size(831, 31);
            this.ucBottom.TabIndex = 0;
            // 
            // ucTop
            // 
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTop.Location = new System.Drawing.Point(0, 0);
            this.ucTop.Margin = new System.Windows.Forms.Padding(4);
            this.ucTop.Name = "ucTop";
            this.ucTop.Size = new System.Drawing.Size(831, 64);
            this.ucTop.TabIndex = 5;
            // 
            // trvTracks
            // 
            this.trvTracks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvTracks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTracks.ImageIndex = 5;
            this.trvTracks.ImageList = this.ImgIcons;
            this.trvTracks.Indent = 19;
            this.trvTracks.LineColor = System.Drawing.Color.White;
            this.trvTracks.Location = new System.Drawing.Point(3, 3);
            this.trvTracks.Name = "trvTracks";
            treeNode1.Name = "NodeWinRecentDocs";
            treeNode1.StateImageIndex = 0;
            treeNode1.Text = "Windows Recent Documents";
            treeNode2.Name = "NodeWinStrtMenu";
            treeNode2.StateImageIndex = 0;
            treeNode2.Text = "Windows Start Menu Run";
            treeNode3.Name = "NodeFileFolderList";
            treeNode3.StateImageIndex = 0;
            treeNode3.Text = "Microsoft Common Dialog - File/Folder List";
            treeNode4.Name = "NodeWinTempFiles";
            treeNode4.StateImageIndex = 0;
            treeNode4.Text = "Windows Clipboard";
            treeNode5.Name = "Node11";
            treeNode5.StateImageIndex = 0;
            treeNode5.Text = "Windows Temporary Files";
            treeNode6.Name = "NodeWinRecycleBin";
            treeNode6.StateImageIndex = 0;
            treeNode6.Text = "Windows Recycle Bin";
            treeNode7.Name = "NodeWinMapNWDrive";
            treeNode7.StateImageIndex = 0;
            treeNode7.Text = "Windows Mapped Network Drives";
            treeNode8.Name = "NodeStrtMenuClickHistory";
            treeNode8.StateImageIndex = 0;
            treeNode8.Text = "Start Menu Click History";
            treeNode9.Name = "NodeNWDrive";
            treeNode9.StateImageIndex = 0;
            treeNode9.Text = "My Network Places";
            treeNode10.ImageIndex = 1;
            treeNode10.Name = "NodeWindows";
            treeNode10.SelectedImageIndex = 1;
            treeNode10.StateImageIndex = 0;
            treeNode10.Text = "Windows";
            treeNode11.Name = "NodeIEURLHistory";
            treeNode11.StateImageIndex = 0;
            treeNode11.Text = "Internet Explorer - URL History";
            treeNode12.Name = "NodeIEHistory";
            treeNode12.StateImageIndex = 0;
            treeNode12.Text = "Internet Explorer History";
            treeNode13.Name = "NodeIECookies";
            treeNode13.StateImageIndex = 0;
            treeNode13.Text = "Internet Explorer Cookies";
            treeNode14.Name = "NodePasswords";
            treeNode14.StateImageIndex = 0;
            treeNode14.Text = "Intelligent Forms - Auto Complete Passwords";
            treeNode15.Name = "NodeTempIEFiles";
            treeNode15.StateImageIndex = 0;
            treeNode15.Text = "Temporary Internet Files";
            treeNode16.Name = "NodeIndexDATFiles";
            treeNode16.StateImageIndex = 0;
            treeNode16.Text = "Index.dat Files";
            treeNode17.ImageIndex = 0;
            treeNode17.Name = "NodeIE";
            treeNode17.SelectedImageIndex = 0;
            treeNode17.StateImageIndex = 0;
            treeNode17.Text = "Internet Explorer";
            treeNode18.Name = "NodeWMP";
            treeNode18.StateImageIndex = 0;
            treeNode18.Text = "Windows Media Player";
            treeNode19.Name = "Node1";
            treeNode19.StateImageIndex = 0;
            treeNode19.Text = "QuickTime Player";
            treeNode20.Name = "Node2";
            treeNode20.StateImageIndex = 0;
            treeNode20.Text = "Macromedia Flash Player";
            treeNode21.Name = "Node3";
            treeNode21.StateImageIndex = 0;
            treeNode21.Text = "Microsoft Office";
            treeNode22.Name = "Node5";
            treeNode22.StateImageIndex = 0;
            treeNode22.Text = "MS Management Console";
            treeNode23.Name = "Node6";
            treeNode23.StateImageIndex = 0;
            treeNode23.Text = "MS Wordpad";
            treeNode24.Name = "Node7";
            treeNode24.StateImageIndex = 0;
            treeNode24.Text = "MS Paint";
            treeNode25.Name = "Node8";
            treeNode25.StateImageIndex = 0;
            treeNode25.Text = "Winrar";
            treeNode26.Name = "Node9";
            treeNode26.StateImageIndex = 0;
            treeNode26.Text = "Sun Java";
            treeNode27.Name = "Node10";
            treeNode27.StateImageIndex = 0;
            treeNode27.Text = "Windows Defender";
            treeNode28.ImageIndex = 2;
            treeNode28.Name = "NodePlugin";
            treeNode28.SelectedImageIndex = 2;
            treeNode28.StateImageIndex = 0;
            treeNode28.Text = "Plug-ins";
            treeNode29.Name = "NodeFirefoxHistory";
            treeNode29.StateImageIndex = 0;
            treeNode29.Text = "Mozilla Firefox - History";
            treeNode30.Name = "NodeFirefoxCookies";
            treeNode30.StateImageIndex = 0;
            treeNode30.Text = "Mozilla Firefox - Cookies";
            treeNode31.Name = "NodeFirefoxCache";
            treeNode31.StateImageIndex = 0;
            treeNode31.Text = "Mozilla Firefox - Internet Cache";
            treeNode32.Name = "NodeFirefoxFormInfo";
            treeNode32.StateImageIndex = 0;
            treeNode32.Text = "Mozilla Firefox - Saved Form Information";
            treeNode33.Name = "NodeFirefoxPassword";
            treeNode33.StateImageIndex = 0;
            treeNode33.Text = "Mozilla Firefox - Saved Passwords";
            treeNode34.Name = "NodeFirefoxDWMgr";
            treeNode34.StateImageIndex = 0;
            treeNode34.Text = "Mozilla Firefox - Download Manager History";
            treeNode35.Name = "NodeFirefoxHistory";
            treeNode35.StateImageIndex = 0;
            treeNode35.Text = "Mozilla Firefox - Search History";
            treeNode36.ImageIndex = 4;
            treeNode36.Name = "NodeFirefox";
            treeNode36.SelectedImageIndex = 4;
            treeNode36.StateImageIndex = 0;
            treeNode36.Text = "Mozilla Firefox";
            treeNode37.Name = "NodeChromeHistory";
            treeNode37.StateImageIndex = 0;
            treeNode37.Text = "Google Chrome - History";
            treeNode38.Name = "NodeChromeCookies";
            treeNode38.StateImageIndex = 0;
            treeNode38.Text = "Google Chrome - Cookies";
            treeNode39.Name = "NodeChromeCache";
            treeNode39.StateImageIndex = 0;
            treeNode39.Text = "Google Chrome - Internet Cache";
            treeNode40.Name = "NodeChromeFormInfo";
            treeNode40.StateImageIndex = 0;
            treeNode40.Text = "Google Chrome - Saved From Information";
            treeNode41.Name = "NodeChromeHistory";
            treeNode41.StateImageIndex = 0;
            treeNode41.Text = "Google Chrome - Download History";
            treeNode42.ImageIndex = 3;
            treeNode42.Name = "NodeChrome";
            treeNode42.SelectedImageIndex = 3;
            treeNode42.StateImageIndex = 0;
            treeNode42.Text = "Google Chrome";
            this.trvTracks.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode17,
            treeNode28,
            treeNode36,
            treeNode42});
            this.trvTracks.SelectedImageIndex = 5;
            this.trvTracks.ShowLines = false;
            this.trvTracks.Size = new System.Drawing.Size(236, 482);
            this.trvTracks.TabIndex = 1;
            this.trvTracks.TriStateStyleProperty = FreemiumUtilities.TracksEraser.TriStateTreeView.TriStateStyles.Standard;
            this.trvTracks.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvTracks_AfterCheck);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(831, 643);
            this.Controls.Add(this.ucBottom);
            this.Controls.Add(this.ucTop);
            this.Controls.Add(this.spcTracks);
            this.Controls.Add(this.tlsMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Track Eraser";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tlsMain.ResumeLayout(false);
            this.tlsMain.PerformLayout();
            this.spcTracks.Panel1.ResumeLayout(false);
            this.spcTracks.Panel2.ResumeLayout(false);
            this.spcTracks.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tbpTracks.ResumeLayout(false);
            this.spcResults.Panel1.ResumeLayout(false);
            this.spcResults.Panel1.PerformLayout();
            this.spcResults.Panel2.ResumeLayout(false);
            this.spcResults.Panel2.PerformLayout();
            this.spcResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbScanning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.ToolStrip tlsMain;
        System.Windows.Forms.ToolStripButton tsbAnalyse;
        System.Windows.Forms.ToolStripButton tsbErase;
        System.Windows.Forms.ToolStripSeparator tssFirst;
        System.Windows.Forms.ToolStripButton tsbOptions;
        System.Windows.Forms.ToolStripSeparator tssSecond;
        System.Windows.Forms.ToolStripDropDownButton tsbCheck;
        System.Windows.Forms.ToolStripMenuItem tmiCheckAll;
		System.Windows.Forms.ToolStripMenuItem tmiUncheckAll;
        System.Windows.Forms.ImageList ImgIcons;
        System.Windows.Forms.SplitContainer spcTracks;
        System.Windows.Forms.TabControl tcMain;
        System.Windows.Forms.TabPage tbpTracks;
		TriStateTreeView trvTracks;
        System.Windows.Forms.SplitContainer spcResults;
        System.Windows.Forms.Button RunEraser;
        System.Windows.Forms.Button btnAnalyse;
        System.Windows.Forms.TextBox txtResults;
        System.Windows.Forms.Label lblResults;
        TopControl ucTop;
        BottomControl ucBottom;
        System.Windows.Forms.PictureBox pcbScanning;
        System.Windows.Forms.Label Scanninglbl;
    }
}

