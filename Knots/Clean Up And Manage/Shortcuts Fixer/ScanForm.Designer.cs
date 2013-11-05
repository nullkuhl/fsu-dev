using System.Resources;
using System.Globalization;
using System.Threading;

namespace ShortcutsFixer
{
	partial class ScanForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("ShortcutsFixer.Resources",
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanForm));
            this.radioButtonDesktop = new System.Windows.Forms.RadioButton();
            this.radioButtonDrives = new System.Windows.Forms.RadioButton();
            this.buttonScan = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.fileLabel = new System.Windows.Forms.Label();
            this.grbMain = new System.Windows.Forms.GroupBox();
            this.checkedListViewDrives = new System.Windows.Forms.ListView();
            this.imlMain = new System.Windows.Forms.ImageList(this.components);
            this.prbMain = new System.Windows.Forms.ProgressBar();
            this.grbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonDesktop
            // 
            this.radioButtonDesktop.AutoSize = true;
            this.radioButtonDesktop.Checked = true;
            this.radioButtonDesktop.Location = new System.Drawing.Point(15, 21);
            this.radioButtonDesktop.Name = "radioButtonDesktop";
            this.radioButtonDesktop.Size = new System.Drawing.Size(194, 17);
            this.radioButtonDesktop.TabIndex = 1;
            this.radioButtonDesktop.TabStop = true;
            this.radioButtonDesktop.Text = "Only Scan Start Menu And Desktop";
            this.radioButtonDesktop.UseVisualStyleBackColor = true;
            this.radioButtonDesktop.CheckedChanged += new System.EventHandler(this.radioButtonDesktop_CheckedChanged);
            // 
            // radioButtonDrives
            // 
            this.radioButtonDrives.AutoSize = true;
            this.radioButtonDrives.Location = new System.Drawing.Point(15, 44);
            this.radioButtonDrives.Name = "radioButtonDrives";
            this.radioButtonDrives.Size = new System.Drawing.Size(89, 17);
            this.radioButtonDrives.TabIndex = 2;
            this.radioButtonDrives.Text = "Check Drives";
            this.radioButtonDrives.UseVisualStyleBackColor = true;
            this.radioButtonDrives.CheckedChanged += new System.EventHandler(this.radioButtonDesktop_CheckedChanged);
            // 
            // buttonScan
            // 
            this.buttonScan.Location = new System.Drawing.Point(59, 266);
            this.buttonScan.Name = "buttonScan";
            this.buttonScan.Size = new System.Drawing.Size(111, 27);
            this.buttonScan.TabIndex = 3;
            this.buttonScan.Text = "Scan Now";
            this.buttonScan.UseVisualStyleBackColor = true;
            this.buttonScan.Click += new System.EventHandler(this.buttonScan_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(190, 266);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(111, 27);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Location = new System.Drawing.Point(28, 213);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(0, 13);
            this.fileLabel.TabIndex = 5;
            // 
            // grbMain
            // 
            this.grbMain.Controls.Add(this.checkedListViewDrives);
            this.grbMain.Controls.Add(this.radioButtonDrives);
            this.grbMain.Controls.Add(this.radioButtonDesktop);
            this.grbMain.Location = new System.Drawing.Point(12, 12);
            this.grbMain.Name = "grbMain";
            this.grbMain.Size = new System.Drawing.Size(347, 249);
            this.grbMain.TabIndex = 6;
            this.grbMain.TabStop = false;
            this.grbMain.Text = "Scan Options";
            // 
            // checkedListViewDrives
            // 
            this.checkedListViewDrives.CheckBoxes = true;
            this.checkedListViewDrives.Enabled = false;
            this.checkedListViewDrives.LargeImageList = this.imlMain;
            this.checkedListViewDrives.Location = new System.Drawing.Point(15, 74);
            this.checkedListViewDrives.Name = "checkedListViewDrives";
            this.checkedListViewDrives.Size = new System.Drawing.Size(315, 164);
            this.checkedListViewDrives.SmallImageList = this.imlMain;
            this.checkedListViewDrives.TabIndex = 7;
            this.checkedListViewDrives.UseCompatibleStateImageBehavior = false;
            this.checkedListViewDrives.View = System.Windows.Forms.View.List;
            // 
            // imlMain
            // 
            this.imlMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlMain.ImageStream")));
            this.imlMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imlMain.Images.SetKeyName(0, "hdd.ico");
            // 
            // prbMain
            // 
            this.prbMain.Location = new System.Drawing.Point(12, 238);
            this.prbMain.Name = "prbMain";
            this.prbMain.Size = new System.Drawing.Size(347, 23);
            this.prbMain.TabIndex = 7;
            this.prbMain.Visible = false;
            // 
            // ScanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 306);
            this.Controls.Add(this.prbMain);
            this.Controls.Add(this.grbMain);
            this.Controls.Add(this.fileLabel);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonScan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ScanForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scan Now";
            this.Load += new System.EventHandler(this.ScanWindow_Load);
            this.grbMain.ResumeLayout(false);
            this.grbMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.RadioButton radioButtonDesktop;
		public System.Windows.Forms.RadioButton radioButtonDrives;
		public System.Windows.Forms.Button buttonScan;
		public System.Windows.Forms.Button buttonCancel;
		public System.Windows.Forms.Label fileLabel;
		public System.Windows.Forms.GroupBox grbMain;
		public System.Windows.Forms.ImageList imlMain;
		public System.Windows.Forms.ListView checkedListViewDrives;
		public System.Windows.Forms.ProgressBar prbMain;
	}
}