using System.Resources;
using System.Windows.Forms;

namespace FreemiumUtilities.TracksEraser
{
	partial class frmTrackSelector
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("FreemiumUtilities.TracksEraser",
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrackSelector));
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
            this.ImgIcons = new System.Windows.Forms.ImageList(this.components);
            this.spcMain = new System.Windows.Forms.SplitContainer();
            this.trvMain = new FreemiumUtilities.TracksEraser.TriStateTreeView();
            this.resultsTxt = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblSelect = new System.Windows.Forms.Label();
            this.spcMain.Panel1.SuspendLayout();
            this.spcMain.Panel2.SuspendLayout();
            this.spcMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImgIcons
            // 
            this.ImgIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgIcons.ImageStream")));
            this.ImgIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ImgIcons.Images.SetKeyName(0, "internet_explorer.png");
            this.ImgIcons.Images.SetKeyName(1, "windows2.ico");
            this.ImgIcons.Images.SetKeyName(2, "plugin_disabled.png");
            this.ImgIcons.Images.SetKeyName(3, "hp_chrome.png");
            this.ImgIcons.Images.SetKeyName(4, "firefox_original.png");
            this.ImgIcons.Images.SetKeyName(5, "bullet_white.png");
            this.ImgIcons.Images.SetKeyName(6, "transparent.ico");
            this.ImgIcons.Images.SetKeyName(7, "icon-error.png");
            // 
            // spcMain
            // 
            this.spcMain.Location = new System.Drawing.Point(0, 25);
            this.spcMain.Name = "spcMain";
            // 
            // spcMain.Panel1
            // 
            this.spcMain.Panel1.Controls.Add(this.trvMain);
            this.spcMain.Panel1MinSize = 0;
            // 
            // spcMain.Panel2
            // 
            this.spcMain.Panel2.Controls.Add(this.resultsTxt);
            this.spcMain.Panel2MinSize = 0;
            this.spcMain.Size = new System.Drawing.Size(792, 584);
            this.spcMain.SplitterDistance = 280;
            this.spcMain.TabIndex = 4;
            // 
            // trvMain
            // 
            this.trvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvMain.ImageIndex = 5;
            this.trvMain.ImageList = this.ImgIcons;
            this.trvMain.Indent = 19;
            this.trvMain.LineColor = System.Drawing.Color.White;
            this.trvMain.Location = new System.Drawing.Point(0, 0);
            this.trvMain.Name = "trvMain";
            treeNode1.ImageIndex = 5;
            treeNode1.Name = "NodeWinRecentDocs";
            treeNode1.StateImageIndex = 0;
            treeNode1.Text = "Windows Recent Documents";
            treeNode2.ImageIndex = 5;
            treeNode2.Name = "NodeWinStrtMenu";
            treeNode2.StateImageIndex = 0;
            treeNode2.Text = "Windows Start Menu Run";
            treeNode3.ImageIndex = 5;
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
            this.trvMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode17,
            treeNode28,
            treeNode36,
            treeNode42});
            this.trvMain.SelectedImageIndex = 5;
            this.trvMain.ShowLines = false;
            this.trvMain.Size = new System.Drawing.Size(280, 584);
            this.trvMain.TabIndex = 2;
            this.trvMain.TriStateStyleProperty = FreemiumUtilities.TracksEraser.TriStateTreeView.TriStateStyles.Standard;
            this.trvMain.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvMain_AfterCheck);
            this.trvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvMain_AfterSelect);
            // 
            // resultsTxt
            // 
            this.resultsTxt.BackColor = System.Drawing.Color.White;
            this.resultsTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsTxt.Location = new System.Drawing.Point(0, 0);
            this.resultsTxt.Multiline = true;
            this.resultsTxt.Name = "resultsTxt";
            this.resultsTxt.ReadOnly = true;
            this.resultsTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.resultsTxt.Size = new System.Drawing.Size(508, 584);
            this.resultsTxt.TabIndex = 2;
            this.resultsTxt.Visible = false;
            this.resultsTxt.WordWrap = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(682, 615);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 30);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Location = new System.Drawing.Point(5, 5);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(205, 13);
            this.lblSelect.TabIndex = 7;
            this.lblSelect.Text = "Please select the tracks you wish to erase";
            // 
            // frmTrackSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(794, 648);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.spcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTrackSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Track Eraser";
            this.Load += new System.EventHandler(this.frmTrackSelector_Load);
            this.spcMain.Panel1.ResumeLayout(false);
            this.spcMain.Panel2.ResumeLayout(false);
            this.spcMain.Panel2.PerformLayout();
            this.spcMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		System.Windows.Forms.ImageList ImgIcons;
		System.Windows.Forms.SplitContainer spcMain;
		TriStateTreeView trvMain;
		System.Windows.Forms.TextBox resultsTxt;
		System.Windows.Forms.Button btnOK;
		System.Windows.Forms.Label lblSelect;
	}
}

