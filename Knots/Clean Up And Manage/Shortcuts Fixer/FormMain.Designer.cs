using System.Resources;
using System.Globalization;
using System.Threading;

namespace ShortcutsFixer
{
	partial class FormMain
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.dgwMain = new System.Windows.Forms.DataGridView();
			this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tlsMain = new System.Windows.Forms.ToolStrip();
			this.btnScan = new System.Windows.Forms.ToolStripButton();
			this.tss1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnFixShortCut = new System.Windows.Forms.ToolStripButton();
			this.btnDelete = new System.Windows.Forms.ToolStripButton();
			this.tss2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnProperties = new System.Windows.Forms.ToolStripButton();
			this.btnOpenFolder = new System.Windows.Forms.ToolStripButton();
			this.tsbCheck = new System.Windows.Forms.ToolStripSplitButton();
			this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkInvertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tss3 = new System.Windows.Forms.ToolStripSeparator();
			this.btnRestore = new System.Windows.Forms.ToolStripButton();
			this.stsMain = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.prbMain = new System.Windows.Forms.ToolStripProgressBar();
			this.fileLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.Spring = new System.Windows.Forms.ToolStripStatusLabel();
			this.Abort = new System.Windows.Forms.ToolStripStatusLabel();
			this.ucTop = new ShortcutsFixer.TopControl();
			this.ucBottom = new ShortcutsFixer.BottomControl();
			((System.ComponentModel.ISupportInitialize)(this.dgwMain)).BeginInit();
			this.tlsMain.SuspendLayout();
			this.stsMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgwMain
			// 
			this.dgwMain.AllowUserToAddRows = false;
			this.dgwMain.AllowUserToDeleteRows = false;
			this.dgwMain.AllowUserToOrderColumns = true;
			this.dgwMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgwMain.BackgroundColor = System.Drawing.Color.White;
			this.dgwMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgwMain.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgwMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgwMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgwMain.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgwMain.Location = new System.Drawing.Point(0, 96);
			this.dgwMain.MultiSelect = false;
			this.dgwMain.Name = "dgwMain";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgwMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgwMain.RowHeadersVisible = false;
			this.dgwMain.RowTemplate.Height = 24;
			this.dgwMain.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dgwMain.Size = new System.Drawing.Size(793, 312);
			this.dgwMain.TabIndex = 7;
			this.dgwMain.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgwMain_CurrentCellDirtyStateChanged);
			// 
			// cmsMain
			// 
			this.cmsMain.Name = "contextMenuStrip1";
			this.cmsMain.Size = new System.Drawing.Size(61, 4);
			// 
			// tlsMain
			// 
			this.tlsMain.Dock = System.Windows.Forms.DockStyle.None;
			this.tlsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tlsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnScan,
            this.tss1,
            this.btnFixShortCut,
            this.btnDelete,
            this.tss2,
            this.btnProperties,
            this.btnOpenFolder,
            this.tsbCheck,
            this.tss3,
            this.btnRestore});
			this.tlsMain.Location = new System.Drawing.Point(2, 67);
			this.tlsMain.MinimumSize = new System.Drawing.Size(793, 0);
			this.tlsMain.Name = "tlsMain";
			this.tlsMain.Size = new System.Drawing.Size(793, 25);
			this.tlsMain.TabIndex = 10;
			this.tlsMain.Text = "toolStrip1";
			// 
			// btnScan
			// 
			this.btnScan.Image = global::ShortcutsFixer.Properties.Resources._1303028214_old_edit_find;
			this.btnScan.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnScan.Name = "btnScan";
			this.btnScan.Size = new System.Drawing.Size(52, 22);
			this.btnScan.Text = "Scan";
			this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
			// 
			// tss1
			// 
			this.tss1.Name = "tss1";
			this.tss1.Size = new System.Drawing.Size(6, 25);
			// 
			// btnFixShortCut
			// 
			this.btnFixShortCut.Enabled = false;
			this.btnFixShortCut.Image = global::ShortcutsFixer.Properties.Resources._1303028349_shortcut;
			this.btnFixShortCut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnFixShortCut.Name = "btnFixShortCut";
			this.btnFixShortCut.Size = new System.Drawing.Size(89, 22);
			this.btnFixShortCut.Text = "Fix Shortcut";
			this.btnFixShortCut.Click += new System.EventHandler(this.btnFix_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Enabled = false;
			this.btnDelete.Image = global::ShortcutsFixer.Properties.Resources._1303026989_101;
			this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 22);
			this.btnDelete.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// tss2
			// 
			this.tss2.Name = "tss2";
			this.tss2.Size = new System.Drawing.Size(6, 25);
			// 
			// btnProperties
			// 
			this.btnProperties.Enabled = false;
			this.btnProperties.Image = global::ShortcutsFixer.Properties.Resources._1303028428_Properties;
			this.btnProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnProperties.Name = "btnProperties";
			this.btnProperties.Size = new System.Drawing.Size(80, 22);
			this.btnProperties.Text = "Properties";
			this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
			// 
			// btnOpenFolder
			// 
			this.btnOpenFolder.Enabled = false;
			this.btnOpenFolder.Image = global::ShortcutsFixer.Properties.Resources._1303028470_folder_horizontal_open;
			this.btnOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnOpenFolder.Name = "btnOpenFolder";
			this.btnOpenFolder.Size = new System.Drawing.Size(92, 22);
			this.btnOpenFolder.Text = "Open Folder";
			this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
			// 
			// tsbCheck
			// 
			this.tsbCheck.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllToolStripMenuItem,
            this.checkNoneToolStripMenuItem,
            this.checkInvertToolStripMenuItem});
			this.tsbCheck.Enabled = false;
			this.tsbCheck.Image = global::ShortcutsFixer.Properties.Resources._1303028498_finished_work;
			this.tsbCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbCheck.Name = "tsbCheck";
			this.tsbCheck.Size = new System.Drawing.Size(72, 22);
			this.tsbCheck.Text = "Check";
			// 
			// checkAllToolStripMenuItem
			// 
			this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
			this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.checkAllToolStripMenuItem.Text = "Check All";
			this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.checkAllToolStripMenuItem_Click);
			// 
			// checkNoneToolStripMenuItem
			// 
			this.checkNoneToolStripMenuItem.Name = "checkNoneToolStripMenuItem";
			this.checkNoneToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.checkNoneToolStripMenuItem.Text = "Check None";
			this.checkNoneToolStripMenuItem.Click += new System.EventHandler(this.checkNoneToolStripMenuItem_Click);
			// 
			// checkInvertToolStripMenuItem
			// 
			this.checkInvertToolStripMenuItem.Name = "checkInvertToolStripMenuItem";
			this.checkInvertToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.checkInvertToolStripMenuItem.Text = "Check Invert";
			this.checkInvertToolStripMenuItem.Click += new System.EventHandler(this.checkInvertToolStripMenuItem_Click);
			// 
			// tss3
			// 
			this.tss3.Name = "tss3";
			this.tss3.Size = new System.Drawing.Size(6, 25);
			// 
			// btnRestore
			// 
			this.btnRestore.Image = global::ShortcutsFixer.Properties.Resources._1303028544_view_restore;
			this.btnRestore.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnRestore.Name = "btnRestore";
			this.btnRestore.Size = new System.Drawing.Size(66, 22);
			this.btnRestore.Text = "Restore";
			this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
			// 
			// stsMain
			// 
			this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.prbMain,
            this.fileLabel,
            this.Spring,
            this.Abort});
			this.stsMain.Location = new System.Drawing.Point(0, 411);
			this.stsMain.Name = "stsMain";
			this.stsMain.Size = new System.Drawing.Size(793, 22);
			this.stsMain.SizingGrip = false;
			this.stsMain.TabIndex = 12;
			this.stsMain.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(66, 17);
			this.lblStatus.Text = "statusLabel";
			// 
			// prbMain
			// 
			this.prbMain.Name = "prbMain";
			this.prbMain.Size = new System.Drawing.Size(100, 16);
			this.prbMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.prbMain.Visible = false;
			// 
			// fileLabel
			// 
			this.fileLabel.Name = "fileLabel";
			this.fileLabel.Size = new System.Drawing.Size(51, 17);
			this.fileLabel.Text = "fileLabel";
			// 
			// Spring
			// 
			this.Spring.Name = "Spring";
			this.Spring.Size = new System.Drawing.Size(624, 17);
			this.Spring.Spring = true;
			// 
			// Abort
			// 
			this.Abort.IsLink = true;
			this.Abort.Name = "Abort";
			this.Abort.Size = new System.Drawing.Size(37, 17);
			this.Abort.Text = "Abort";
			this.Abort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Abort.Click += new System.EventHandler(this.Abort_Click);
			// 
			// ucTop
			// 
			this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
			this.ucTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.ucTop.Location = new System.Drawing.Point(0, 0);
			this.ucTop.Margin = new System.Windows.Forms.Padding(4);
			this.ucTop.Name = "ucTop";
			this.ucTop.Size = new System.Drawing.Size(793, 64);
			this.ucTop.TabIndex = 14;
			// 
			// ucBottom
			// 
			this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ucBottom.Location = new System.Drawing.Point(0, 433);
			this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
			this.ucBottom.MaximumSize = new System.Drawing.Size(0, 31);
			this.ucBottom.MinimumSize = new System.Drawing.Size(0, 31);
			this.ucBottom.Name = "ucBottom";
			this.ucBottom.Size = new System.Drawing.Size(793, 31);
			this.ucBottom.TabIndex = 15;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(793, 464);
			this.Controls.Add(this.ucTop);
			this.Controls.Add(this.stsMain);
			this.Controls.Add(this.dgwMain);
			this.Controls.Add(this.tlsMain);
			this.Controls.Add(this.ucBottom);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Shortcut Fixer";
			this.Load += new System.EventHandler(this.FormMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgwMain)).EndInit();
			this.tlsMain.ResumeLayout(false);
			this.tlsMain.PerformLayout();
			this.stsMain.ResumeLayout(false);
			this.stsMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		System.Windows.Forms.DataGridView dgwMain;
		System.Windows.Forms.ContextMenuStrip cmsMain;
		System.Windows.Forms.ToolStrip tlsMain;
		System.Windows.Forms.ToolStripButton btnScan;
		System.Windows.Forms.ToolStripSeparator tss1;
		System.Windows.Forms.ToolStripButton btnFixShortCut;
		System.Windows.Forms.ToolStripButton btnDelete;
		System.Windows.Forms.ToolStripSeparator tss2;
		System.Windows.Forms.ToolStripButton btnProperties;
		System.Windows.Forms.ToolStripButton btnOpenFolder;
		System.Windows.Forms.ToolStripSplitButton tsbCheck;
		System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
		System.Windows.Forms.ToolStripMenuItem checkNoneToolStripMenuItem;
		System.Windows.Forms.ToolStripSeparator tss3;
		System.Windows.Forms.ToolStripButton btnRestore;
		System.Windows.Forms.StatusStrip stsMain;
		System.Windows.Forms.ToolStripProgressBar prbMain;
		System.Windows.Forms.ToolStripStatusLabel fileLabel;
		System.Windows.Forms.ToolStripStatusLabel lblStatus;
		System.Windows.Forms.ToolStripMenuItem checkInvertToolStripMenuItem;
		TopControl ucTop;
		BottomControl ucBottom;
		System.Windows.Forms.ToolStripStatusLabel Spring;
		System.Windows.Forms.ToolStripStatusLabel Abort;
	}
}

