using System.Resources;
using System.Globalization;
using System.Threading;

namespace EmptyFolderFinder
{
	partial class frmCustomFileDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustomFileDialog));
            this.grbMain = new System.Windows.Forms.GroupBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtData = new System.Windows.Forms.TextBox();
            this.rdbScanFolder = new System.Windows.Forms.RadioButton();
            this.lvMain = new System.Windows.Forms.ListView();
            this.clhDrives = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rdbScanDrives = new System.Windows.Forms.RadioButton();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imagesSmall = new System.Windows.Forms.ImageList(this.components);
            this.grbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbMain
            // 
            this.grbMain.Controls.Add(this.btnSelect);
            this.grbMain.Controls.Add(this.txtData);
            this.grbMain.Controls.Add(this.rdbScanFolder);
            this.grbMain.Controls.Add(this.lvMain);
            this.grbMain.Controls.Add(this.rdbScanDrives);
            this.grbMain.Location = new System.Drawing.Point(2, -4);
            this.grbMain.Name = "grbMain";
            this.grbMain.Size = new System.Drawing.Size(397, 259);
            this.grbMain.TabIndex = 0;
            this.grbMain.TabStop = false;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(344, 222);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(29, 20);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(22, 223);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(316, 20);
            this.txtData.TabIndex = 3;
            // 
            // rdbScanFolder
            // 
            this.rdbScanFolder.AutoSize = true;
            this.rdbScanFolder.Location = new System.Drawing.Point(22, 205);
            this.rdbScanFolder.Name = "rdbScanFolder";
            this.rdbScanFolder.Size = new System.Drawing.Size(82, 17);
            this.rdbScanFolder.TabIndex = 2;
            this.rdbScanFolder.TabStop = true;
            this.rdbScanFolder.Text = "Scan Folder";
            this.rdbScanFolder.UseVisualStyleBackColor = true;
            this.rdbScanFolder.CheckedChanged += new System.EventHandler(this.rdbScanFolder_CheckedChanged);
            // 
            // lvMain
            // 
            this.lvMain.CheckBoxes = true;
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhDrives});
            this.lvMain.GridLines = true;
            this.lvMain.Location = new System.Drawing.Point(22, 44);
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(351, 139);
            this.lvMain.TabIndex = 1;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.List;
            // 
            // clhDrives
            // 
            this.clhDrives.Text = "Drives";
            this.clhDrives.Width = 287;
            // 
            // rdbScanDrives
            // 
            this.rdbScanDrives.AutoSize = true;
            this.rdbScanDrives.Checked = true;
            this.rdbScanDrives.Location = new System.Drawing.Point(22, 19);
            this.rdbScanDrives.Name = "rdbScanDrives";
            this.rdbScanDrives.Size = new System.Drawing.Size(83, 17);
            this.rdbScanDrives.TabIndex = 0;
            this.rdbScanDrives.TabStop = true;
            this.rdbScanDrives.Text = "Scan Drives";
            this.rdbScanDrives.UseVisualStyleBackColor = true;
            this.rdbScanDrives.CheckedChanged += new System.EventHandler(this.rdbScanDrives_CheckedChanged);
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(86, 269);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(111, 27);
            this.btnScan.TabIndex = 1;
            this.btnScan.Text = "Scan Now";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(212, 269);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(111, 27);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // imagesSmall
            // 
            this.imagesSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesSmall.ImageStream")));
            this.imagesSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesSmall.Images.SetKeyName(0, "Folder.ico");
            this.imagesSmall.Images.SetKeyName(1, "hdd.ico");
            // 
            // frmCustomFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 308);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.grbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCustomFileDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Drive Folder";
            this.Load += new System.EventHandler(this.FrmCustomFileDialog_Load);
            this.grbMain.ResumeLayout(false);
            this.grbMain.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		System.Windows.Forms.GroupBox grbMain;
		System.Windows.Forms.RadioButton rdbScanDrives;
		System.Windows.Forms.ListView lvMain;
		System.Windows.Forms.RadioButton rdbScanFolder;
		System.Windows.Forms.TextBox txtData;
		System.Windows.Forms.Button btnSelect;
		System.Windows.Forms.Button btnScan;
		System.Windows.Forms.Button btnCancel;
		System.Windows.Forms.ColumnHeader clhDrives;
		System.Windows.Forms.ImageList imagesSmall;
	}
}