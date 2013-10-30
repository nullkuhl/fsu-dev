using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace SystemInformation
{
	/// <summary>
	/// Designer for Drives.
	/// </summary>
    public partial class Drives : SystemInformation.TaskPanelBase
    {

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());
        
        public Drives()
        {
            //This call is required by the Windows Form Designer.
            InitializeComponent();

        }

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Drives));
            this.imagelistDrives = new System.Windows.Forms.ImageList(this.components);
            this.labelLegend = new System.Windows.Forms.Label();
            this.listviewDrives = new System.Windows.Forms.ListView();
            this.PhysDrive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Capacity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ModelNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelPhysHD = new System.Windows.Forms.Label();
            this.labelSeparator2 = new System.Windows.Forms.Label();
            this.listviewVolumes = new System.Windows.Forms.ListView();
            this.Drive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VolumeLabel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileSystem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TotalSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UsedSpace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FreeSpace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PercentFree = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ready = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelVolumes = new System.Windows.Forms.Label();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.picturePanel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).BeginInit();
            this.SuspendLayout();
            // 
            // imagelistDrives
            // 
            this.imagelistDrives.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagelistDrives.ImageStream")));
            this.imagelistDrives.TransparentColor = System.Drawing.Color.Transparent;
            this.imagelistDrives.Images.SetKeyName(0, "Floppy_16x16.png");
            this.imagelistDrives.Images.SetKeyName(1, "Drive_16x16.png");
            this.imagelistDrives.Images.SetKeyName(2, "Network_16x16.png");
            this.imagelistDrives.Images.SetKeyName(3, "CDROM_16x16.png");
            this.imagelistDrives.Images.SetKeyName(4, "UsbDrive_16x16.png");
            this.imagelistDrives.Images.SetKeyName(5, "Unknown_16x16.png");
            // 
            // labelLegend
            // 
            this.labelLegend.AutoSize = true;
            this.labelLegend.BackColor = System.Drawing.Color.Transparent;
            this.labelLegend.ForeColor = System.Drawing.Color.Black;
            this.labelLegend.Location = new System.Drawing.Point(300, 78);
            this.labelLegend.Name = "labelLegend";
            this.labelLegend.Size = new System.Drawing.Size(0, 15);
            this.labelLegend.TabIndex = 20;
            this.labelLegend.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // listviewDrives
            // 
            this.listviewDrives.BackColor = System.Drawing.SystemColors.Window;
            this.listviewDrives.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PhysDrive,
            this.Type,
            this.Capacity,
            this.ModelNumber,
            this.Status});
            this.listviewDrives.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewDrives.ForeColor = System.Drawing.SystemColors.ControlText;
            this.listviewDrives.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listviewDrives.Location = new System.Drawing.Point(16, 288);
            this.listviewDrives.Name = "listviewDrives";
            this.listviewDrives.ShowGroups = false;
            this.listviewDrives.Size = new System.Drawing.Size(482, 134);
            this.listviewDrives.SmallImageList = this.imagelistDrives;
            this.listviewDrives.TabIndex = 19;
            this.listviewDrives.UseCompatibleStateImageBehavior = false;
            this.listviewDrives.View = System.Windows.Forms.View.Details;
            // 
            // PhysDrive
            // 
            this.PhysDrive.Width = 40;
            // 
            // Type
            // 
            this.Type.Width = 50;
            // 
            // Capacity
            // 
            this.Capacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Capacity.Width = 80;
            // 
            // ModelNumber
            // 
            this.ModelNumber.Width = 240;
            // 
            // Status
            // 
            this.Status.Width = 45;
            // 
            // labelPhysHD
            // 
            this.labelPhysHD.AutoSize = true;
            this.labelPhysHD.BackColor = System.Drawing.Color.Transparent;
            this.labelPhysHD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPhysHD.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelPhysHD.Location = new System.Drawing.Point(16, 254);
            this.labelPhysHD.Name = "labelPhysHD";
            this.labelPhysHD.Size = new System.Drawing.Size(0, 17);
            this.labelPhysHD.TabIndex = 18;
            // 
            // labelSeparator2
            // 
            this.labelSeparator2.BackColor = System.Drawing.Color.Black;
            this.labelSeparator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator2.Location = new System.Drawing.Point(16, 276);
            this.labelSeparator2.Name = "labelSeparator2";
            this.labelSeparator2.Size = new System.Drawing.Size(482, 3);
            this.labelSeparator2.TabIndex = 17;
            // 
            // listviewVolumes
            // 
            this.listviewVolumes.BackColor = System.Drawing.SystemColors.Window;
            this.listviewVolumes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Drive,
            this.VolumeLabel,
            this.FileSystem,
            this.TotalSize,
            this.UsedSpace,
            this.FreeSpace,
            this.PercentFree,
            this.Ready});
            this.listviewVolumes.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewVolumes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.listviewVolumes.FullRowSelect = true;
            this.listviewVolumes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listviewVolumes.Location = new System.Drawing.Point(16, 110);
            this.listviewVolumes.MultiSelect = false;
            this.listviewVolumes.Name = "listviewVolumes";
            this.listviewVolumes.ShowGroups = false;
            this.listviewVolumes.Size = new System.Drawing.Size(482, 134);
            this.listviewVolumes.SmallImageList = this.imagelistDrives;
            this.listviewVolumes.TabIndex = 16;
            this.listviewVolumes.UseCompatibleStateImageBehavior = false;
            this.listviewVolumes.View = System.Windows.Forms.View.Details;
            // 
            // Drive
            // 
            this.Drive.Width = 40;
            // 
            // VolumeLabel
            // 
            this.VolumeLabel.Width = 90;
            // 
            // FileSystem
            // 
            this.FileSystem.Width = 40;
            // 
            // TotalSize
            // 
            this.TotalSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TotalSize.Width = 70;
            // 
            // UsedSpace
            // 
            this.UsedSpace.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UsedSpace.Width = 72;
            // 
            // FreeSpace
            // 
            this.FreeSpace.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.FreeSpace.Width = 70;
            // 
            // PercentFree
            // 
            this.PercentFree.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PercentFree.Width = 52;
            // 
            // Ready
            // 
            this.Ready.Width = 22;
            // 
            // labelVolumes
            // 
            this.labelVolumes.AutoSize = true;
            this.labelVolumes.BackColor = System.Drawing.Color.Transparent;
            this.labelVolumes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVolumes.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelVolumes.Location = new System.Drawing.Point(16, 76);
            this.labelVolumes.Name = "labelVolumes";
            this.labelVolumes.Size = new System.Drawing.Size(0, 17);
            this.labelVolumes.TabIndex = 15;
            // 
            // labelSeparator
            // 
            this.labelSeparator.BackColor = System.Drawing.Color.Black;
            this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator.Location = new System.Drawing.Point(16, 98);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(482, 3);
            this.labelSeparator.TabIndex = 14;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTitle.Location = new System.Drawing.Point(80, 28);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(0, 25);
            this.labelTitle.TabIndex = 7;
            // 
            // picturePanel
            // 
            this.picturePanel.BackColor = System.Drawing.Color.Transparent;
            this.picturePanel.Image = global::SystemInformation.Properties.Resources.Drive_48x48;
            this.picturePanel.Location = new System.Drawing.Point(16, 16);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(48, 48);
            this.picturePanel.TabIndex = 6;
            this.picturePanel.TabStop = false;
            // 
            // Drives
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.labelLegend);
            this.Controls.Add(this.listviewDrives);
            this.Controls.Add(this.labelPhysHD);
            this.Controls.Add(this.labelSeparator2);
            this.Controls.Add(this.listviewVolumes);
            this.Controls.Add(this.labelVolumes);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.picturePanel);
            this.Name = "Drives";
            this.Load += new System.EventHandler(this.Drives_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.Label labelTitle;
        System.Windows.Forms.PictureBox picturePanel;
        System.Windows.Forms.Label labelVolumes;
        System.Windows.Forms.Label labelSeparator;
        System.Windows.Forms.ListView listviewVolumes;
        System.Windows.Forms.ImageList imagelistDrives;
        System.Windows.Forms.ListView listviewDrives;
        System.Windows.Forms.Label labelPhysHD;
        System.Windows.Forms.Label labelSeparator2;
        System.Windows.Forms.ColumnHeader Drive;
        System.Windows.Forms.ColumnHeader VolumeLabel;
        System.Windows.Forms.ColumnHeader FileSystem;
        System.Windows.Forms.ColumnHeader TotalSize;
        System.Windows.Forms.ColumnHeader UsedSpace;
        System.Windows.Forms.ColumnHeader FreeSpace;
        System.Windows.Forms.ColumnHeader PercentFree;
        System.Windows.Forms.ColumnHeader Ready;
        System.Windows.Forms.Label labelLegend;
        System.Windows.Forms.ColumnHeader PhysDrive;
        System.Windows.Forms.ColumnHeader Capacity;
        System.Windows.Forms.ColumnHeader ModelNumber;
        ColumnHeader Type;
        ColumnHeader Status;
		
	}
	
}
