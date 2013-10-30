using System.Resources;
using System.Globalization;
using System.Threading;

namespace FileSplitterAndJoiner
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		/// <summary>
		/// ResourceManager instance
		/// </summary>
		public ResourceManager rm = new ResourceManager("FileSplitterAndJoiner.Resources",
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
            System.Windows.Forms.TabPage tbpOptions;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.lblBiggerIsFaster = new System.Windows.Forms.Label();
            this.btnRefreshDebugInfo = new System.Windows.Forms.Button();
            this.lblNotRespond = new System.Windows.Forms.Label();
            this.lblDoEvents = new System.Windows.Forms.Label();
            this.cboDoEvents = new System.Windows.Forms.ComboBox();
            this.btnPauseResume = new System.Windows.Forms.Button();
            this.grbDebugVariables = new System.Windows.Forms.GroupBox();
            this.txtDebugVariables = new System.Windows.Forms.TextBox();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.chkIsJoinFileGenerating = new System.Windows.Forms.CheckBox();
            this.txtNumberOfFiles = new System.Windows.Forms.TextBox();
            this.txtNumberOfBytesAfterSplit = new System.Windows.Forms.TextBox();
            this.btnSplitFile = new System.Windows.Forms.Button();
            this.lblSplitNumberOfPieces = new System.Windows.Forms.Label();
            this.lblSplitFileSize = new System.Windows.Forms.Label();
            this.txtSplitFolder = new System.Windows.Forms.TextBox();
            this.txtSplitFileName = new System.Windows.Forms.TextBox();
            this.btnBrowseSplitFolder = new System.Windows.Forms.Button();
            this.btnBrowseSplitFile = new System.Windows.Forms.Button();
            this.cmnSize = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmi360KB = new System.Windows.Forms.ToolStripMenuItem();
            this.cmi720KB = new System.Windows.Forms.ToolStripMenuItem();
            this.cmi12MB = new System.Windows.Forms.ToolStripMenuItem();
            this.cmi13MB = new System.Windows.Forms.ToolStripMenuItem();
            this.tssMain = new System.Windows.Forms.ToolStripSeparator();
            this.cmiCustom = new System.Windows.Forms.ToolStripMenuItem();
            this.grbSplitFile = new System.Windows.Forms.GroupBox();
            this.grbSplitFolder = new System.Windows.Forms.GroupBox();
            this.grbNumberOfBytes = new System.Windows.Forms.GroupBox();
            this.cboFileSizes = new System.Windows.Forms.ComboBox();
            this.grbSplitInfo = new System.Windows.Forms.GroupBox();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tbpFileSplitter = new System.Windows.Forms.TabPage();
            this.grbNumberOfFiles = new System.Windows.Forms.GroupBox();
            this.btnPauseSplitting = new System.Windows.Forms.Button();
            this.prbSplitting = new System.Windows.Forms.ProgressBar();
            this.tbpFileJoiner = new System.Windows.Forms.TabPage();
            this.btnPauseJoining = new System.Windows.Forms.Button();
            this.grbJoinInfo = new System.Windows.Forms.GroupBox();
            this.lblJoinFileName = new System.Windows.Forms.Label();
            this.lblJoinNumberOfPieces = new System.Windows.Forms.Label();
            this.cboIsPiecesDeletedAfterJoining = new System.Windows.Forms.CheckBox();
            this.grbJoinFolder = new System.Windows.Forms.GroupBox();
            this.btnJoinBrowseFolder = new System.Windows.Forms.Button();
            this.txtJoinFolder = new System.Windows.Forms.TextBox();
            this.grbJoinFile = new System.Windows.Forms.GroupBox();
            this.txtJoinFileName = new System.Windows.Forms.TextBox();
            this.btnJoinBrowseFile = new System.Windows.Forms.Button();
            this.prbJoining = new System.Windows.Forms.ProgressBar();
            this.btnJoinFile = new System.Windows.Forms.Button();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.ucBottom = new FileSplitterAndJoiner.BottomControl();
            this.ucTop = new FileSplitterAndJoiner.TopControl();
            tbpOptions = new System.Windows.Forms.TabPage();
            tbpOptions.SuspendLayout();
            this.grbDebugVariables.SuspendLayout();
            this.cmnSize.SuspendLayout();
            this.grbSplitFile.SuspendLayout();
            this.grbSplitFolder.SuspendLayout();
            this.grbNumberOfBytes.SuspendLayout();
            this.grbSplitInfo.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tbpFileSplitter.SuspendLayout();
            this.grbNumberOfFiles.SuspendLayout();
            this.tbpFileJoiner.SuspendLayout();
            this.grbJoinInfo.SuspendLayout();
            this.grbJoinFolder.SuspendLayout();
            this.grbJoinFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbpOptions
            // 
            tbpOptions.Controls.Add(this.lblBiggerIsFaster);
            tbpOptions.Controls.Add(this.btnRefreshDebugInfo);
            tbpOptions.Controls.Add(this.lblNotRespond);
            tbpOptions.Controls.Add(this.lblDoEvents);
            tbpOptions.Controls.Add(this.cboDoEvents);
            tbpOptions.Controls.Add(this.btnPauseResume);
            tbpOptions.Controls.Add(this.grbDebugVariables);
            tbpOptions.Location = new System.Drawing.Point(4, 24);
            tbpOptions.Name = "tbpOptions";
            tbpOptions.Padding = new System.Windows.Forms.Padding(3);
            tbpOptions.Size = new System.Drawing.Size(435, 281);
            tbpOptions.TabIndex = 2;
            tbpOptions.Text = "Options";
            tbpOptions.UseVisualStyleBackColor = true;
            // 
            // lblBiggerIsFaster
            // 
            this.lblBiggerIsFaster.AutoSize = true;
            this.lblBiggerIsFaster.Location = new System.Drawing.Point(226, 10);
            this.lblBiggerIsFaster.Name = "lblBiggerIsFaster";
            this.lblBiggerIsFaster.Size = new System.Drawing.Size(81, 13);
            this.lblBiggerIsFaster.TabIndex = 6;
            this.lblBiggerIsFaster.Text = "(bigger is faster)";
            // 
            // btnRefreshDebugInfo
            // 
            this.btnRefreshDebugInfo.Location = new System.Drawing.Point(349, 134);
            this.btnRefreshDebugInfo.Name = "btnRefreshDebugInfo";
            this.btnRefreshDebugInfo.Size = new System.Drawing.Size(75, 57);
            this.btnRefreshDebugInfo.TabIndex = 3;
            this.btnRefreshDebugInfo.Text = "Refresh debug info";
            this.btnRefreshDebugInfo.UseVisualStyleBackColor = true;
            this.btnRefreshDebugInfo.Click += new System.EventHandler(this.ButtonRefreshDebugInfo_Click);
            // 
            // lblNotRespond
            // 
            this.lblNotRespond.AutoSize = true;
            this.lblNotRespond.Location = new System.Drawing.Point(8, 33);
            this.lblNotRespond.Name = "lblNotRespond";
            this.lblNotRespond.Size = new System.Drawing.Size(365, 13);
            this.lblNotRespond.TabIndex = 5;
            this.lblNotRespond.Text = "(the application may not respond for filename long time if the value is too big)";
            // 
            // lblDoEvents
            // 
            this.lblDoEvents.AutoSize = true;
            this.lblDoEvents.Location = new System.Drawing.Point(8, 10);
            this.lblDoEvents.Name = "lblDoEvents";
            this.lblDoEvents.Size = new System.Drawing.Size(122, 13);
            this.lblDoEvents.TabIndex = 4;
            this.lblDoEvents.Text = "Do events also at every:";
            // 
            // cboDoEvents
            // 
            this.cboDoEvents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoEvents.FormattingEnabled = true;
            this.cboDoEvents.Items.AddRange(new object[] {
            " 1 MB",
            " 5 MB",
            "15 MB",
            "25 MB",
            "35 MB",
            "45 MB"});
            this.cboDoEvents.Location = new System.Drawing.Point(154, 6);
            this.cboDoEvents.Name = "cboDoEvents";
            this.cboDoEvents.Size = new System.Drawing.Size(66, 21);
            this.cboDoEvents.TabIndex = 0;
            this.cboDoEvents.SelectedIndexChanged += new System.EventHandler(this.cboDoEvents_SelectedIndexChanged);
            // 
            // btnPauseResume
            // 
            this.btnPauseResume.Location = new System.Drawing.Point(349, 71);
            this.btnPauseResume.Name = "btnPauseResume";
            this.btnPauseResume.Size = new System.Drawing.Size(75, 57);
            this.btnPauseResume.TabIndex = 2;
            this.btnPauseResume.Text = "Pause Resume";
            this.btnPauseResume.UseVisualStyleBackColor = true;
            this.btnPauseResume.Click += new System.EventHandler(this.btnPauseResume_Click);
            // 
            // grbDebugVariables
            // 
            this.grbDebugVariables.Controls.Add(this.txtDebugVariables);
            this.grbDebugVariables.Location = new System.Drawing.Point(11, 51);
            this.grbDebugVariables.Name = "grbDebugVariables";
            this.grbDebugVariables.Size = new System.Drawing.Size(332, 219);
            this.grbDebugVariables.TabIndex = 0;
            this.grbDebugVariables.TabStop = false;
            this.grbDebugVariables.Text = "Debug variables";
            // 
            // txtDebugVariables
            // 
            this.txtDebugVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDebugVariables.Location = new System.Drawing.Point(3, 16);
            this.txtDebugVariables.Name = "txtDebugVariables";
            this.txtDebugVariables.Size = new System.Drawing.Size(326, 20);
            this.txtDebugVariables.TabIndex = 0;
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(26, 17);
            this.tslStatus.Text = "idle";
            // 
            // chkIsJoinFileGenerating
            // 
            this.chkIsJoinFileGenerating.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIsJoinFileGenerating.AutoSize = true;
            this.chkIsJoinFileGenerating.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIsJoinFileGenerating.ForeColor = System.Drawing.Color.Black;
            this.chkIsJoinFileGenerating.Location = new System.Drawing.Point(258, 213);
            this.chkIsJoinFileGenerating.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkIsJoinFileGenerating.Name = "chkIsJoinFileGenerating";
            this.chkIsJoinFileGenerating.Size = new System.Drawing.Size(224, 19);
            this.chkIsJoinFileGenerating.TabIndex = 17;
            this.chkIsJoinFileGenerating.Text = "Add filename self-joining executable";
            this.chkIsJoinFileGenerating.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIsJoinFileGenerating.UseVisualStyleBackColor = true;
            // 
            // txtNumberOfFiles
            // 
            this.txtNumberOfFiles.Enabled = false;
            this.txtNumberOfFiles.Location = new System.Drawing.Point(6, 51);
            this.txtNumberOfFiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNumberOfFiles.MaxLength = 4;
            this.txtNumberOfFiles.Name = "txtNumberOfFiles";
            this.txtNumberOfFiles.Size = new System.Drawing.Size(111, 21);
            this.txtNumberOfFiles.TabIndex = 16;
            this.txtNumberOfFiles.TextChanged += new System.EventHandler(this.txtNumberOfFiles_TextChanged);
            this.txtNumberOfFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumberOfFiles_KeyDown);
            // 
            // txtNumberOfBytesAfterSplit
            // 
            this.txtNumberOfBytesAfterSplit.Enabled = false;
            this.txtNumberOfBytesAfterSplit.Location = new System.Drawing.Point(6, 51);
            this.txtNumberOfBytesAfterSplit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNumberOfBytesAfterSplit.MaxLength = 14;
            this.txtNumberOfBytesAfterSplit.Name = "txtNumberOfBytesAfterSplit";
            this.txtNumberOfBytesAfterSplit.Size = new System.Drawing.Size(95, 21);
            this.txtNumberOfBytesAfterSplit.TabIndex = 14;
            this.txtNumberOfBytesAfterSplit.TextChanged += new System.EventHandler(this.txtNumberOfBytesAfterSplit_TextChanged);
            this.txtNumberOfBytesAfterSplit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumberOfBytesAfterSplit_KeyDown);
            this.txtNumberOfBytesAfterSplit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNumberOfBytesAfterSplit_KeyUp);
            this.txtNumberOfBytesAfterSplit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtNumberOfBytesAfterSplit_MouseDown);
            // 
            // btnSplitFile
            // 
            this.btnSplitFile.Enabled = false;
            this.btnSplitFile.Location = new System.Drawing.Point(250, 262);
            this.btnSplitFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSplitFile.Name = "btnSplitFile";
            this.btnSplitFile.Size = new System.Drawing.Size(121, 26);
            this.btnSplitFile.TabIndex = 9;
            this.btnSplitFile.Text = "Split file";
            this.btnSplitFile.UseVisualStyleBackColor = true;
            this.btnSplitFile.Click += new System.EventHandler(this.btnSplitFile_Click);
            // 
            // lblSplitNumberOfPieces
            // 
            this.lblSplitNumberOfPieces.AutoSize = true;
            this.lblSplitNumberOfPieces.Location = new System.Drawing.Point(6, 35);
            this.lblSplitNumberOfPieces.Name = "lblSplitNumberOfPieces";
            this.lblSplitNumberOfPieces.Size = new System.Drawing.Size(107, 15);
            this.lblSplitNumberOfPieces.TabIndex = 8;
            this.lblSplitNumberOfPieces.Text = "Number of pieces:";
            // 
            // lblSplitFileSize
            // 
            this.lblSplitFileSize.AutoSize = true;
            this.lblSplitFileSize.Location = new System.Drawing.Point(6, 19);
            this.lblSplitFileSize.Name = "lblSplitFileSize";
            this.lblSplitFileSize.Size = new System.Drawing.Size(55, 15);
            this.lblSplitFileSize.TabIndex = 7;
            this.lblSplitFileSize.Text = "File size:";
            // 
            // txtSplitFolder
            // 
            this.txtSplitFolder.Location = new System.Drawing.Point(6, 20);
            this.txtSplitFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSplitFolder.MaxLength = 1000;
            this.txtSplitFolder.Name = "txtSplitFolder";
            this.txtSplitFolder.ReadOnly = true;
            this.txtSplitFolder.Size = new System.Drawing.Size(417, 21);
            this.txtSplitFolder.TabIndex = 5;
            this.txtSplitFolder.TextChanged += new System.EventHandler(this.txtSplitFileName_TextChanged);
            // 
            // txtSplitFileName
            // 
            this.txtSplitFileName.Location = new System.Drawing.Point(6, 20);
            this.txtSplitFileName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSplitFileName.MaxLength = 1000;
            this.txtSplitFileName.Name = "txtSplitFileName";
            this.txtSplitFileName.ReadOnly = true;
            this.txtSplitFileName.Size = new System.Drawing.Size(417, 21);
            this.txtSplitFileName.TabIndex = 4;
            this.txtSplitFileName.TextChanged += new System.EventHandler(this.txtSplitFileName_TextChanged);
            this.txtSplitFileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSplitFileName_KeyDown);
            // 
            // btnBrowseSplitFolder
            // 
            this.btnBrowseSplitFolder.Location = new System.Drawing.Point(429, 18);
            this.btnBrowseSplitFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBrowseSplitFolder.Name = "btnBrowseSplitFolder";
            this.btnBrowseSplitFolder.Size = new System.Drawing.Size(41, 23);
            this.btnBrowseSplitFolder.TabIndex = 1;
            this.btnBrowseSplitFolder.Text = "...";
            this.btnBrowseSplitFolder.UseVisualStyleBackColor = true;
            this.btnBrowseSplitFolder.Click += new System.EventHandler(this.btnBrowseSplitFolder_Click);
            // 
            // btnBrowseSplitFile
            // 
            this.btnBrowseSplitFile.Location = new System.Drawing.Point(429, 18);
            this.btnBrowseSplitFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBrowseSplitFile.Name = "btnBrowseSplitFile";
            this.btnBrowseSplitFile.Size = new System.Drawing.Size(41, 23);
            this.btnBrowseSplitFile.TabIndex = 0;
            this.btnBrowseSplitFile.Text = "...";
            this.btnBrowseSplitFile.UseVisualStyleBackColor = true;
            this.btnBrowseSplitFile.Click += new System.EventHandler(this.btnBrowseSplitFile_Click);
            // 
            // cmnSize
            // 
            this.cmnSize.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmi360KB,
            this.cmi720KB,
            this.cmi12MB,
            this.cmi13MB,
            this.tssMain,
            this.cmiCustom});
            this.cmnSize.Name = "contextMenuStrip1";
            this.cmnSize.ShowImageMargin = false;
            this.cmnSize.Size = new System.Drawing.Size(149, 120);
            // 
            // cmi360KB
            // 
            this.cmi360KB.Name = "cmi360KB";
            this.cmi360KB.Size = new System.Drawing.Size(148, 22);
            this.cmi360KB.Text = "360 KB (minimum)";
            this.cmi360KB.Click += new System.EventHandler(this.cmi360KB_Click);
            // 
            // cmi720KB
            // 
            this.cmi720KB.Name = "cmi720KB";
            this.cmi720KB.Size = new System.Drawing.Size(148, 22);
            this.cmi720KB.Text = "720 KB";
            this.cmi720KB.Click += new System.EventHandler(this.cmi720KB_Click);
            // 
            // cmi12MB
            // 
            this.cmi12MB.Name = "cmi12MB";
            this.cmi12MB.Size = new System.Drawing.Size(148, 22);
            this.cmi12MB.Text = "1.2 MB";
            this.cmi12MB.Click += new System.EventHandler(this.cmi12MB_Click);
            // 
            // cmi13MB
            // 
            this.cmi13MB.Name = "cmi13MB";
            this.cmi13MB.Size = new System.Drawing.Size(148, 22);
            this.cmi13MB.Text = "1.38 MB";
            this.cmi13MB.Click += new System.EventHandler(this.cmi13MB_Click);
            // 
            // tssMain
            // 
            this.tssMain.Name = "tssMain";
            this.tssMain.Size = new System.Drawing.Size(145, 6);
            // 
            // cmiCustom
            // 
            this.cmiCustom.Name = "cmiCustom";
            this.cmiCustom.Size = new System.Drawing.Size(148, 22);
            this.cmiCustom.Text = "Custom";
            this.cmiCustom.Click += new System.EventHandler(this.cmiCustom_Click);
            // 
            // grbSplitFile
            // 
            this.grbSplitFile.Controls.Add(this.btnBrowseSplitFile);
            this.grbSplitFile.Controls.Add(this.txtSplitFileName);
            this.grbSplitFile.ForeColor = System.Drawing.Color.Black;
            this.grbSplitFile.Location = new System.Drawing.Point(6, 6);
            this.grbSplitFile.Name = "grbSplitFile";
            this.grbSplitFile.Size = new System.Drawing.Size(476, 51);
            this.grbSplitFile.TabIndex = 21;
            this.grbSplitFile.TabStop = false;
            this.grbSplitFile.Text = "Select file to split";
            // 
            // grbSplitFolder
            // 
            this.grbSplitFolder.Controls.Add(this.btnBrowseSplitFolder);
            this.grbSplitFolder.Controls.Add(this.txtSplitFolder);
            this.grbSplitFolder.ForeColor = System.Drawing.Color.Black;
            this.grbSplitFolder.Location = new System.Drawing.Point(6, 64);
            this.grbSplitFolder.Name = "grbSplitFolder";
            this.grbSplitFolder.Size = new System.Drawing.Size(476, 51);
            this.grbSplitFolder.TabIndex = 22;
            this.grbSplitFolder.TabStop = false;
            this.grbSplitFolder.Text = "Save pieces in folder";
            // 
            // grbNumberOfBytes
            // 
            this.grbNumberOfBytes.Controls.Add(this.cboFileSizes);
            this.grbNumberOfBytes.Controls.Add(this.txtNumberOfBytesAfterSplit);
            this.grbNumberOfBytes.ForeColor = System.Drawing.Color.Black;
            this.grbNumberOfBytes.Location = new System.Drawing.Point(6, 122);
            this.grbNumberOfBytes.Name = "grbNumberOfBytes";
            this.grbNumberOfBytes.Size = new System.Drawing.Size(164, 84);
            this.grbNumberOfBytes.TabIndex = 23;
            this.grbNumberOfBytes.TabStop = false;
            this.grbNumberOfBytes.Text = "data size in which splitting occurs after";
            // 
            // cboFileSizes
            // 
            this.cboFileSizes.Enabled = false;
            this.cboFileSizes.FormattingEnabled = true;
            this.cboFileSizes.Location = new System.Drawing.Point(107, 51);
            this.cboFileSizes.Name = "cboFileSizes";
            this.cboFileSizes.Size = new System.Drawing.Size(50, 23);
            this.cboFileSizes.TabIndex = 15;
            this.cboFileSizes.SelectedIndexChanged += new System.EventHandler(this.cboFileSizes_SelectedIndexChanged);
            // 
            // grbSplitInfo
            // 
            this.grbSplitInfo.Controls.Add(this.lblSplitFileSize);
            this.grbSplitInfo.Controls.Add(this.lblSplitNumberOfPieces);
            this.grbSplitInfo.ForeColor = System.Drawing.Color.Black;
            this.grbSplitInfo.Location = new System.Drawing.Point(305, 122);
            this.grbSplitInfo.Name = "grbSplitInfo";
            this.grbSplitInfo.Size = new System.Drawing.Size(177, 84);
            this.grbSplitInfo.TabIndex = 25;
            this.grbSplitInfo.TabStop = false;
            this.grbSplitInfo.Text = "Split Info";
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tbpFileSplitter);
            this.tcMain.Controls.Add(this.tbpFileJoiner);
            this.tcMain.Location = new System.Drawing.Point(1, 70);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(498, 324);
            this.tcMain.TabIndex = 4;
            // 
            // tbpFileSplitter
            // 
            this.tbpFileSplitter.Controls.Add(this.grbNumberOfFiles);
            this.tbpFileSplitter.Controls.Add(this.btnPauseSplitting);
            this.tbpFileSplitter.Controls.Add(this.grbSplitInfo);
            this.tbpFileSplitter.Controls.Add(this.grbSplitFile);
            this.tbpFileSplitter.Controls.Add(this.btnSplitFile);
            this.tbpFileSplitter.Controls.Add(this.chkIsJoinFileGenerating);
            this.tbpFileSplitter.Controls.Add(this.prbSplitting);
            this.tbpFileSplitter.Controls.Add(this.grbSplitFolder);
            this.tbpFileSplitter.Controls.Add(this.grbNumberOfBytes);
            this.tbpFileSplitter.Location = new System.Drawing.Point(4, 24);
            this.tbpFileSplitter.Name = "tbpFileSplitter";
            this.tbpFileSplitter.Padding = new System.Windows.Forms.Padding(3);
            this.tbpFileSplitter.Size = new System.Drawing.Size(490, 296);
            this.tbpFileSplitter.TabIndex = 0;
            this.tbpFileSplitter.Text = "File Splitter";
            this.tbpFileSplitter.UseVisualStyleBackColor = true;
            // 
            // grbNumberOfFiles
            // 
            this.grbNumberOfFiles.Controls.Add(this.txtNumberOfFiles);
            this.grbNumberOfFiles.ForeColor = System.Drawing.Color.Black;
            this.grbNumberOfFiles.Location = new System.Drawing.Point(176, 122);
            this.grbNumberOfFiles.Name = "grbNumberOfFiles";
            this.grbNumberOfFiles.Size = new System.Drawing.Size(123, 84);
            this.grbNumberOfFiles.TabIndex = 27;
            this.grbNumberOfFiles.TabStop = false;
            this.grbNumberOfFiles.Text = "Number of files to split your file into";
            // 
            // btnPauseSplitting
            // 
            this.btnPauseSplitting.Enabled = false;
            this.btnPauseSplitting.Location = new System.Drawing.Point(377, 262);
            this.btnPauseSplitting.Name = "btnPauseSplitting";
            this.btnPauseSplitting.Size = new System.Drawing.Size(105, 26);
            this.btnPauseSplitting.TabIndex = 26;
            this.btnPauseSplitting.Text = "Pause";
            this.btnPauseSplitting.UseVisualStyleBackColor = true;
            this.btnPauseSplitting.Click += new System.EventHandler(this.btnSplitPause_Click);
            // 
            // prbSplitting
            // 
            this.prbSplitting.Location = new System.Drawing.Point(13, 244);
            this.prbSplitting.Name = "prbSplitting";
            this.prbSplitting.Size = new System.Drawing.Size(469, 12);
            this.prbSplitting.TabIndex = 20;
            // 
            // tbpFileJoiner
            // 
            this.tbpFileJoiner.Controls.Add(this.btnPauseJoining);
            this.tbpFileJoiner.Controls.Add(this.grbJoinInfo);
            this.tbpFileJoiner.Controls.Add(this.cboIsPiecesDeletedAfterJoining);
            this.tbpFileJoiner.Controls.Add(this.grbJoinFolder);
            this.tbpFileJoiner.Controls.Add(this.grbJoinFile);
            this.tbpFileJoiner.Controls.Add(this.prbJoining);
            this.tbpFileJoiner.Controls.Add(this.btnJoinFile);
            this.tbpFileJoiner.Location = new System.Drawing.Point(4, 24);
            this.tbpFileJoiner.Name = "tbpFileJoiner";
            this.tbpFileJoiner.Padding = new System.Windows.Forms.Padding(3);
            this.tbpFileJoiner.Size = new System.Drawing.Size(490, 296);
            this.tbpFileJoiner.TabIndex = 1;
            this.tbpFileJoiner.Text = "File Joiner";
            this.tbpFileJoiner.UseVisualStyleBackColor = true;
            // 
            // btnPauseJoining
            // 
            this.btnPauseJoining.Enabled = false;
            this.btnPauseJoining.Location = new System.Drawing.Point(377, 262);
            this.btnPauseJoining.Name = "btnPauseJoining";
            this.btnPauseJoining.Size = new System.Drawing.Size(105, 26);
            this.btnPauseJoining.TabIndex = 27;
            this.btnPauseJoining.Text = "Pause";
            this.btnPauseJoining.UseVisualStyleBackColor = true;
            this.btnPauseJoining.Click += new System.EventHandler(this.btnJoinPause_Click);
            // 
            // grbJoinInfo
            // 
            this.grbJoinInfo.Controls.Add(this.lblJoinFileName);
            this.grbJoinInfo.Controls.Add(this.lblJoinNumberOfPieces);
            this.grbJoinInfo.ForeColor = System.Drawing.Color.Black;
            this.grbJoinInfo.Location = new System.Drawing.Point(6, 122);
            this.grbJoinInfo.Name = "grbJoinInfo";
            this.grbJoinInfo.Size = new System.Drawing.Size(476, 63);
            this.grbJoinInfo.TabIndex = 26;
            this.grbJoinInfo.TabStop = false;
            this.grbJoinInfo.Text = "Joining Info (.Join file content)";
            // 
            // lblJoinFileName
            // 
            this.lblJoinFileName.AutoSize = true;
            this.lblJoinFileName.ForeColor = System.Drawing.Color.Black;
            this.lblJoinFileName.Location = new System.Drawing.Point(6, 19);
            this.lblJoinFileName.Name = "lblJoinFileName";
            this.lblJoinFileName.Size = new System.Drawing.Size(65, 15);
            this.lblJoinFileName.TabIndex = 7;
            this.lblJoinFileName.Text = "File name:";
            // 
            // lblJoinNumberOfPieces
            // 
            this.lblJoinNumberOfPieces.AutoSize = true;
            this.lblJoinNumberOfPieces.ForeColor = System.Drawing.Color.Black;
            this.lblJoinNumberOfPieces.Location = new System.Drawing.Point(6, 35);
            this.lblJoinNumberOfPieces.Name = "lblJoinNumberOfPieces";
            this.lblJoinNumberOfPieces.Size = new System.Drawing.Size(107, 15);
            this.lblJoinNumberOfPieces.TabIndex = 8;
            this.lblJoinNumberOfPieces.Text = "Number of pieces:";
            // 
            // cboIsPiecesDeletedAfterJoining
            // 
            this.cboIsPiecesDeletedAfterJoining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboIsPiecesDeletedAfterJoining.AutoSize = true;
            this.cboIsPiecesDeletedAfterJoining.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cboIsPiecesDeletedAfterJoining.Checked = true;
            this.cboIsPiecesDeletedAfterJoining.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboIsPiecesDeletedAfterJoining.ForeColor = System.Drawing.Color.Black;
            this.cboIsPiecesDeletedAfterJoining.Location = new System.Drawing.Point(208, 198);
            this.cboIsPiecesDeletedAfterJoining.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboIsPiecesDeletedAfterJoining.Name = "cboIsPiecesDeletedAfterJoining";
            this.cboIsPiecesDeletedAfterJoining.Size = new System.Drawing.Size(274, 19);
            this.cboIsPiecesDeletedAfterJoining.TabIndex = 24;
            this.cboIsPiecesDeletedAfterJoining.Text = "Delete pieces and executable file after joining";
            this.cboIsPiecesDeletedAfterJoining.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cboIsPiecesDeletedAfterJoining.UseVisualStyleBackColor = true;
            // 
            // grbJoinFolder
            // 
            this.grbJoinFolder.Controls.Add(this.btnJoinBrowseFolder);
            this.grbJoinFolder.Controls.Add(this.txtJoinFolder);
            this.grbJoinFolder.ForeColor = System.Drawing.Color.Black;
            this.grbJoinFolder.Location = new System.Drawing.Point(6, 64);
            this.grbJoinFolder.Name = "grbJoinFolder";
            this.grbJoinFolder.Size = new System.Drawing.Size(476, 51);
            this.grbJoinFolder.TabIndex = 23;
            this.grbJoinFolder.TabStop = false;
            this.grbJoinFolder.Text = "Save joined file in folder";
            // 
            // btnJoinBrowseFolder
            // 
            this.btnJoinBrowseFolder.Location = new System.Drawing.Point(429, 18);
            this.btnJoinBrowseFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnJoinBrowseFolder.Name = "btnJoinBrowseFolder";
            this.btnJoinBrowseFolder.Size = new System.Drawing.Size(41, 23);
            this.btnJoinBrowseFolder.TabIndex = 1;
            this.btnJoinBrowseFolder.Text = "...";
            this.btnJoinBrowseFolder.UseVisualStyleBackColor = true;
            this.btnJoinBrowseFolder.Click += new System.EventHandler(this.btnJoinBrowseFolder_Click);
            // 
            // txtJoinFolder
            // 
            this.txtJoinFolder.Location = new System.Drawing.Point(6, 20);
            this.txtJoinFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtJoinFolder.MaxLength = 1000;
            this.txtJoinFolder.Name = "txtJoinFolder";
            this.txtJoinFolder.ReadOnly = true;
            this.txtJoinFolder.Size = new System.Drawing.Size(417, 21);
            this.txtJoinFolder.TabIndex = 5;
            this.txtJoinFolder.TextChanged += new System.EventHandler(this.txtJoinFileName_TextChanged);
            // 
            // grbJoinFile
            // 
            this.grbJoinFile.Controls.Add(this.txtJoinFileName);
            this.grbJoinFile.Controls.Add(this.btnJoinBrowseFile);
            this.grbJoinFile.ForeColor = System.Drawing.Color.Black;
            this.grbJoinFile.Location = new System.Drawing.Point(6, 6);
            this.grbJoinFile.Name = "grbJoinFile";
            this.grbJoinFile.Size = new System.Drawing.Size(476, 51);
            this.grbJoinFile.TabIndex = 22;
            this.grbJoinFile.TabStop = false;
            this.grbJoinFile.Text = "Browse for file part";
            // 
            // txtJoinFileName
            // 
            this.txtJoinFileName.Location = new System.Drawing.Point(6, 20);
            this.txtJoinFileName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtJoinFileName.MaxLength = 1000;
            this.txtJoinFileName.Name = "txtJoinFileName";
            this.txtJoinFileName.ReadOnly = true;
            this.txtJoinFileName.Size = new System.Drawing.Size(417, 21);
            this.txtJoinFileName.TabIndex = 5;
            this.txtJoinFileName.TextChanged += new System.EventHandler(this.txtJoinFileName_TextChanged);
            this.txtJoinFileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxJoinFileName_KeyDown);
            // 
            // btnJoinBrowseFile
            // 
            this.btnJoinBrowseFile.Location = new System.Drawing.Point(429, 18);
            this.btnJoinBrowseFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnJoinBrowseFile.Name = "btnJoinBrowseFile";
            this.btnJoinBrowseFile.Size = new System.Drawing.Size(41, 23);
            this.btnJoinBrowseFile.TabIndex = 6;
            this.btnJoinBrowseFile.Text = "...";
            this.btnJoinBrowseFile.UseVisualStyleBackColor = true;
            this.btnJoinBrowseFile.Click += new System.EventHandler(this.btnJoinBrowseFile_Click);
            // 
            // prbJoining
            // 
            this.prbJoining.Location = new System.Drawing.Point(13, 244);
            this.prbJoining.Name = "prbJoining";
            this.prbJoining.Size = new System.Drawing.Size(469, 12);
            this.prbJoining.TabIndex = 21;
            // 
            // btnJoinFile
            // 
            this.btnJoinFile.Enabled = false;
            this.btnJoinFile.Location = new System.Drawing.Point(249, 262);
            this.btnJoinFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnJoinFile.Name = "btnJoinFile";
            this.btnJoinFile.Size = new System.Drawing.Size(122, 26);
            this.btnJoinFile.TabIndex = 10;
            this.btnJoinFile.Text = "Join file";
            this.btnJoinFile.UseVisualStyleBackColor = true;
            this.btnJoinFile.Click += new System.EventHandler(this.btnJoinFile_Click);
            // 
            // stsMain
            // 
            this.stsMain.Location = new System.Drawing.Point(0, 397);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(499, 22);
            this.stsMain.TabIndex = 8;
            this.stsMain.Text = "statusStrip1";
            // 
            // ucBottom
            // 
            this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucBottom.Location = new System.Drawing.Point(0, 419);
            this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
            this.ucBottom.MaximumSize = new System.Drawing.Size(0, 36);
            this.ucBottom.MinimumSize = new System.Drawing.Size(0, 36);
            this.ucBottom.Name = "ucBottom";
            this.ucBottom.Size = new System.Drawing.Size(499, 36);
            this.ucBottom.TabIndex = 7;
            // 
            // ucTop
            // 
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTop.Location = new System.Drawing.Point(0, 0);
            this.ucTop.Name = "ucTop";
            this.ucTop.Size = new System.Drawing.Size(499, 64);
            this.ucTop.TabIndex = 6;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(499, 455);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.ucBottom);
            this.Controls.Add(this.ucTop);
            this.Controls.Add(this.tcMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "File Splitter and Joiner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            tbpOptions.ResumeLayout(false);
            tbpOptions.PerformLayout();
            this.grbDebugVariables.ResumeLayout(false);
            this.grbDebugVariables.PerformLayout();
            this.cmnSize.ResumeLayout(false);
            this.grbSplitFile.ResumeLayout(false);
            this.grbSplitFile.PerformLayout();
            this.grbSplitFolder.ResumeLayout(false);
            this.grbSplitFolder.PerformLayout();
            this.grbNumberOfBytes.ResumeLayout(false);
            this.grbNumberOfBytes.PerformLayout();
            this.grbSplitInfo.ResumeLayout(false);
            this.grbSplitInfo.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tbpFileSplitter.ResumeLayout(false);
            this.tbpFileSplitter.PerformLayout();
            this.grbNumberOfFiles.ResumeLayout(false);
            this.grbNumberOfFiles.PerformLayout();
            this.tbpFileJoiner.ResumeLayout(false);
            this.tbpFileJoiner.PerformLayout();
            this.grbJoinInfo.ResumeLayout(false);
            this.grbJoinInfo.PerformLayout();
            this.grbJoinFolder.ResumeLayout(false);
            this.grbJoinFolder.PerformLayout();
            this.grbJoinFile.ResumeLayout(false);
            this.grbJoinFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		System.Windows.Forms.TextBox txtSplitFileName;
		System.Windows.Forms.Button btnBrowseSplitFolder;
		System.Windows.Forms.Button btnBrowseSplitFile;
		System.Windows.Forms.TextBox txtSplitFolder;
		System.Windows.Forms.Label lblSplitNumberOfPieces;
		System.Windows.Forms.Label lblSplitFileSize;
		System.Windows.Forms.Button btnSplitFile;
		System.Windows.Forms.TextBox txtNumberOfBytesAfterSplit;
		System.Windows.Forms.ContextMenuStrip cmnSize;
		System.Windows.Forms.ToolStripMenuItem cmi360KB;
		System.Windows.Forms.ToolStripMenuItem cmi720KB;
		System.Windows.Forms.ToolStripMenuItem cmi12MB;
		System.Windows.Forms.ToolStripMenuItem cmi13MB;
		System.Windows.Forms.ToolStripMenuItem cmiCustom;
		System.Windows.Forms.ToolStripSeparator tssMain;
		System.Windows.Forms.TextBox txtNumberOfFiles;
		System.Windows.Forms.CheckBox chkIsJoinFileGenerating;
		System.Windows.Forms.GroupBox grbSplitFile;
		System.Windows.Forms.GroupBox grbSplitFolder;
		System.Windows.Forms.GroupBox grbNumberOfBytes;
		System.Windows.Forms.GroupBox grbSplitInfo;
		System.Windows.Forms.TabControl tcMain;
		System.Windows.Forms.TabPage tbpFileSplitter;
		System.Windows.Forms.TabPage tbpFileJoiner;
		System.Windows.Forms.TextBox txtJoinFileName;
		System.Windows.Forms.Button btnJoinBrowseFile;
		System.Windows.Forms.Button btnJoinFile;
		System.Windows.Forms.ProgressBar prbJoining;
		System.Windows.Forms.GroupBox grbJoinFile;
		System.Windows.Forms.GroupBox grbJoinFolder;
		System.Windows.Forms.Button btnJoinBrowseFolder;
		System.Windows.Forms.TextBox txtJoinFolder;
		System.Windows.Forms.CheckBox cboIsPiecesDeletedAfterJoining;
		System.Windows.Forms.GroupBox grbJoinInfo;
		System.Windows.Forms.Label lblJoinFileName;
		System.Windows.Forms.Label lblJoinNumberOfPieces;
		System.Windows.Forms.Button btnPauseSplitting;
		System.Windows.Forms.Button btnPauseJoining;
		System.Windows.Forms.ToolStripStatusLabel tslStatus;
		System.Windows.Forms.GroupBox grbDebugVariables;
		System.Windows.Forms.ComboBox cboDoEvents;
		System.Windows.Forms.TextBox txtDebugVariables;
		System.Windows.Forms.Button btnPauseResume;
		System.Windows.Forms.Button btnRefreshDebugInfo;
		System.Windows.Forms.Label lblNotRespond;
		System.Windows.Forms.Label lblDoEvents;
		System.Windows.Forms.Label lblBiggerIsFaster;
		System.Windows.Forms.GroupBox grbNumberOfFiles;
		System.Windows.Forms.ProgressBar prbSplitting;
		System.Windows.Forms.ComboBox cboFileSizes;
		TopControl ucTop;
		BottomControl ucBottom;
		System.Windows.Forms.StatusStrip stsMain;
	}
}

