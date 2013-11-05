#define _MyType
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
	public partial class InstalledPrograms : SystemInformation.TaskPanelBase
    {

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());


        public InstalledPrograms()
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
            this.SmallImageList = new System.Windows.Forms.ImageList(this.components);
            this.LargeImageList = new System.Windows.Forms.ImageList(this.components);
            this.labelHelpLink = new System.Windows.Forms.LinkLabel();
            this.labelDisplayName = new System.Windows.Forms.Label();
            this.labelDisplayVersion = new System.Windows.Forms.Label();
            this.labelDisplayVersionDesc = new System.Windows.Forms.Label();
            this.pictureProgram = new System.Windows.Forms.PictureBox();
            this.labelNumberPrograms = new System.Windows.Forms.Label();
            this.labelEstimatedSize = new System.Windows.Forms.Label();
            this.labelEstSizeDesc = new System.Windows.Forms.Label();
            this.labelHelpTelephoneDesc = new System.Windows.Forms.Label();
            this.labelHelpLinkDesc = new System.Windows.Forms.Label();
            this.labelInstallDateDesc = new System.Windows.Forms.Label();
            this.labelHelpTelephone = new System.Windows.Forms.Label();
            this.labelInstallDate = new System.Windows.Forms.Label();
            this.labelDetails = new System.Windows.Forms.Label();
            this.labelSeparator2 = new System.Windows.Forms.Label();
            this.listviewPrograms = new System.Windows.Forms.ListView();
            this.DisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Publisher = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DisplayVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HelpLink = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HelpTelephone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InstallDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EstimatedSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IconIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelInstalledProgramsDescription = new System.Windows.Forms.Label();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.picturePanel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProgram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).BeginInit();
            this.SuspendLayout();
            // 
            // SmallImageList
            // 
            this.SmallImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.SmallImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.SmallImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // LargeImageList
            // 
            this.LargeImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.LargeImageList.ImageSize = new System.Drawing.Size(32, 32);
            this.LargeImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // labelHelpLink
            // 
            this.labelHelpLink.BackColor = System.Drawing.Color.Transparent;
            this.labelHelpLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.labelHelpLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelHelpLink.Location = new System.Drawing.Point(183, 379);
            this.labelHelpLink.Name = "labelHelpLink";
            this.labelHelpLink.Size = new System.Drawing.Size(316, 15);
            this.labelHelpLink.TabIndex = 83;
            this.labelHelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelHelpLink_LinkClicked);
            // 
            // labelDisplayName
            // 
            this.labelDisplayName.BackColor = System.Drawing.Color.Transparent;
            this.labelDisplayName.ForeColor = System.Drawing.Color.Black;
            this.labelDisplayName.Location = new System.Drawing.Point(76, 339);
            this.labelDisplayName.Name = "labelDisplayName";
            this.labelDisplayName.Size = new System.Drawing.Size(248, 15);
            this.labelDisplayName.TabIndex = 82;
            // 
            // labelDisplayVersion
            // 
            this.labelDisplayVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelDisplayVersion.ForeColor = System.Drawing.Color.Black;
            this.labelDisplayVersion.Location = new System.Drawing.Point(414, 339);
            this.labelDisplayVersion.Name = "labelDisplayVersion";
            this.labelDisplayVersion.Size = new System.Drawing.Size(85, 15);
            this.labelDisplayVersion.TabIndex = 81;
            this.labelDisplayVersion.Visible = false;
            // 
            // labelDisplayVersionDesc
            // 
            this.labelDisplayVersionDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelDisplayVersionDesc.ForeColor = System.Drawing.Color.Black;
            this.labelDisplayVersionDesc.Location = new System.Drawing.Point(357, 339);
            this.labelDisplayVersionDesc.Name = "labelDisplayVersionDesc";
            this.labelDisplayVersionDesc.Size = new System.Drawing.Size(52, 15);
            this.labelDisplayVersionDesc.TabIndex = 80;
            this.labelDisplayVersionDesc.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.labelDisplayVersionDesc.Visible = false;
            // 
            // pictureProgram
            // 
            this.pictureProgram.BackColor = System.Drawing.Color.Transparent;
            this.pictureProgram.Location = new System.Drawing.Point(16, 339);
            this.pictureProgram.Name = "pictureProgram";
            this.pictureProgram.Size = new System.Drawing.Size(32, 32);
            this.pictureProgram.TabIndex = 79;
            this.pictureProgram.TabStop = false;
            // 
            // labelNumberPrograms
            // 
            this.labelNumberPrograms.BackColor = System.Drawing.Color.Transparent;
            this.labelNumberPrograms.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumberPrograms.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelNumberPrograms.Location = new System.Drawing.Point(394, 307);
            this.labelNumberPrograms.Name = "labelNumberPrograms";
            this.labelNumberPrograms.Size = new System.Drawing.Size(104, 17);
            this.labelNumberPrograms.TabIndex = 78;
            this.labelNumberPrograms.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelEstimatedSize
            // 
            this.labelEstimatedSize.BackColor = System.Drawing.Color.Transparent;
            this.labelEstimatedSize.ForeColor = System.Drawing.Color.Black;
            this.labelEstimatedSize.Location = new System.Drawing.Point(414, 359);
            this.labelEstimatedSize.Name = "labelEstimatedSize";
            this.labelEstimatedSize.Size = new System.Drawing.Size(85, 15);
            this.labelEstimatedSize.TabIndex = 77;
            this.labelEstimatedSize.Visible = false;
            // 
            // labelEstSizeDesc
            // 
            this.labelEstSizeDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelEstSizeDesc.ForeColor = System.Drawing.Color.Black;
            this.labelEstSizeDesc.Location = new System.Drawing.Point(330, 359);
            this.labelEstSizeDesc.Name = "labelEstSizeDesc";
            this.labelEstSizeDesc.Size = new System.Drawing.Size(79, 15);
            this.labelEstSizeDesc.TabIndex = 76;
            this.labelEstSizeDesc.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.labelEstSizeDesc.Visible = false;
            // 
            // labelHelpTelephoneDesc
            // 
            this.labelHelpTelephoneDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelHelpTelephoneDesc.ForeColor = System.Drawing.Color.Black;
            this.labelHelpTelephoneDesc.Location = new System.Drawing.Point(76, 399);
            this.labelHelpTelephoneDesc.Name = "labelHelpTelephoneDesc";
            this.labelHelpTelephoneDesc.Size = new System.Drawing.Size(76, 15);
            this.labelHelpTelephoneDesc.TabIndex = 61;
            this.labelHelpTelephoneDesc.Visible = false;
            // 
            // labelHelpLinkDesc
            // 
            this.labelHelpLinkDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelHelpLinkDesc.ForeColor = System.Drawing.Color.Black;
            this.labelHelpLinkDesc.Location = new System.Drawing.Point(76, 379);
            this.labelHelpLinkDesc.Name = "labelHelpLinkDesc";
            this.labelHelpLinkDesc.Size = new System.Drawing.Size(101, 15);
            this.labelHelpLinkDesc.TabIndex = 60;
            this.labelHelpLinkDesc.Visible = false;
            // 
            // labelInstallDateDesc
            // 
            this.labelInstallDateDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelInstallDateDesc.ForeColor = System.Drawing.Color.Black;
            this.labelInstallDateDesc.Location = new System.Drawing.Point(76, 359);
            this.labelInstallDateDesc.Name = "labelInstallDateDesc";
            this.labelInstallDateDesc.Size = new System.Drawing.Size(121, 15);
            this.labelInstallDateDesc.TabIndex = 59;
            this.labelInstallDateDesc.Visible = false;
            // 
            // labelHelpTelephone
            // 
            this.labelHelpTelephone.BackColor = System.Drawing.Color.Transparent;
            this.labelHelpTelephone.ForeColor = System.Drawing.Color.Black;
            this.labelHelpTelephone.Location = new System.Drawing.Point(156, 399);
            this.labelHelpTelephone.Name = "labelHelpTelephone";
            this.labelHelpTelephone.Size = new System.Drawing.Size(343, 15);
            this.labelHelpTelephone.TabIndex = 57;
            this.labelHelpTelephone.Visible = false;
            // 
            // labelInstallDate
            // 
            this.labelInstallDate.BackColor = System.Drawing.Color.Transparent;
            this.labelInstallDate.ForeColor = System.Drawing.Color.Black;
            this.labelInstallDate.Location = new System.Drawing.Point(199, 359);
            this.labelInstallDate.Name = "labelInstallDate";
            this.labelInstallDate.Size = new System.Drawing.Size(126, 15);
            this.labelInstallDate.TabIndex = 55;
            this.labelInstallDate.Visible = false;
            // 
            // labelDetails
            // 
            this.labelDetails.AutoSize = true;
            this.labelDetails.BackColor = System.Drawing.Color.Transparent;
            this.labelDetails.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDetails.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelDetails.Location = new System.Drawing.Point(16, 307);
            this.labelDetails.Name = "labelDetails";
            this.labelDetails.Size = new System.Drawing.Size(0, 17);
            this.labelDetails.TabIndex = 53;
            // 
            // labelSeparator2
            // 
            this.labelSeparator2.BackColor = System.Drawing.Color.Black;
            this.labelSeparator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator2.Location = new System.Drawing.Point(16, 328);
            this.labelSeparator2.Name = "labelSeparator2";
            this.labelSeparator2.Size = new System.Drawing.Size(482, 3);
            this.labelSeparator2.TabIndex = 52;
            // 
            // listviewPrograms
            // 
            this.listviewPrograms.BackColor = System.Drawing.SystemColors.Window;
            this.listviewPrograms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DisplayName,
            this.Publisher,
            this.DisplayVersion,
            this.HelpLink,
            this.HelpTelephone,
            this.InstallDate,
            this.EstimatedSize,
            this.IconIndex});
            this.listviewPrograms.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewPrograms.ForeColor = System.Drawing.SystemColors.ControlText;
            this.listviewPrograms.FullRowSelect = true;
            this.listviewPrograms.LabelWrap = false;
            this.listviewPrograms.Location = new System.Drawing.Point(16, 108);
            this.listviewPrograms.MultiSelect = false;
            this.listviewPrograms.Name = "listviewPrograms";
            this.listviewPrograms.Size = new System.Drawing.Size(482, 191);
            this.listviewPrograms.SmallImageList = this.SmallImageList;
            this.listviewPrograms.TabIndex = 51;
            this.listviewPrograms.UseCompatibleStateImageBehavior = false;
            this.listviewPrograms.View = System.Windows.Forms.View.Details;
            this.listviewPrograms.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listviewStartup_ColumnClick);
            this.listviewPrograms.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listviewStartup_ItemSelectionChanged);
            // 
            // DisplayName
            // 
            this.DisplayName.Width = 280;
            // 
            // Publisher
            // 
            this.Publisher.Width = 178;
            // 
            // DisplayVersion
            // 
            this.DisplayVersion.Width = 0;
            // 
            // HelpLink
            // 
            this.HelpLink.Width = 0;
            // 
            // HelpTelephone
            // 
            this.HelpTelephone.Width = 0;
            // 
            // InstallDate
            // 
            this.InstallDate.Width = 0;
            // 
            // EstimatedSize
            // 
            this.EstimatedSize.Width = 0;
            // 
            // IconIndex
            // 
            this.IconIndex.Width = 0;
            // 
            // labelInstalledProgramsDescription
            // 
            this.labelInstalledProgramsDescription.AutoSize = true;
            this.labelInstalledProgramsDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelInstalledProgramsDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstalledProgramsDescription.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelInstalledProgramsDescription.Location = new System.Drawing.Point(16, 76);
            this.labelInstalledProgramsDescription.Name = "labelInstalledProgramsDescription";
            this.labelInstalledProgramsDescription.Size = new System.Drawing.Size(0, 17);
            this.labelInstalledProgramsDescription.TabIndex = 50;
            // 
            // labelSeparator
            // 
            this.labelSeparator.BackColor = System.Drawing.Color.Black;
            this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator.Location = new System.Drawing.Point(16, 98);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(482, 3);
            this.labelSeparator.TabIndex = 49;
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
            this.picturePanel.Image = global::SystemInformation.Properties.Resources.Installed_Programs_48x48;
            this.picturePanel.Location = new System.Drawing.Point(16, 16);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(48, 48);
            this.picturePanel.TabIndex = 6;
            this.picturePanel.TabStop = false;
            // 
            // InstalledPrograms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.labelHelpLink);
            this.Controls.Add(this.labelDisplayName);
            this.Controls.Add(this.labelDisplayVersion);
            this.Controls.Add(this.labelDisplayVersionDesc);
            this.Controls.Add(this.pictureProgram);
            this.Controls.Add(this.labelNumberPrograms);
            this.Controls.Add(this.labelEstimatedSize);
            this.Controls.Add(this.labelEstSizeDesc);
            this.Controls.Add(this.labelHelpTelephoneDesc);
            this.Controls.Add(this.labelHelpLinkDesc);
            this.Controls.Add(this.labelInstallDateDesc);
            this.Controls.Add(this.labelHelpTelephone);
            this.Controls.Add(this.labelInstallDate);
            this.Controls.Add(this.labelDetails);
            this.Controls.Add(this.labelSeparator2);
            this.Controls.Add(this.listviewPrograms);
            this.Controls.Add(this.labelInstalledProgramsDescription);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.picturePanel);
            this.Name = "InstalledPrograms";
            this.Load += new System.EventHandler(this.InstalledPrograms_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureProgram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
		System.Windows.Forms.Label labelTitle;
		System.Windows.Forms.PictureBox picturePanel;
		System.Windows.Forms.ListView listviewPrograms;
		System.Windows.Forms.ColumnHeader DisplayName;
		System.Windows.Forms.Label labelInstalledProgramsDescription;
		System.Windows.Forms.Label labelSeparator;
		System.Windows.Forms.Label labelDetails;
		System.Windows.Forms.Label labelSeparator2;
		System.Windows.Forms.Label labelHelpTelephoneDesc;
		System.Windows.Forms.Label labelHelpLinkDesc;
		System.Windows.Forms.Label labelInstallDateDesc;
		System.Windows.Forms.Label labelHelpTelephone;
		System.Windows.Forms.Label labelInstallDate;
		System.Windows.Forms.ImageList SmallImageList;
		System.Windows.Forms.Label labelEstSizeDesc;
		System.Windows.Forms.Label labelEstimatedSize;
		System.Windows.Forms.ColumnHeader Publisher;
		System.Windows.Forms.ColumnHeader DisplayVersion;
		System.Windows.Forms.ColumnHeader HelpLink;
		System.Windows.Forms.ColumnHeader HelpTelephone;
		System.Windows.Forms.ColumnHeader InstallDate;
		System.Windows.Forms.ColumnHeader EstimatedSize;
		System.Windows.Forms.Label labelNumberPrograms;
		System.Windows.Forms.PictureBox pictureProgram;
		System.Windows.Forms.Label labelDisplayVersion;
		System.Windows.Forms.Label labelDisplayVersionDesc;
		System.Windows.Forms.Label labelDisplayName;
		System.Windows.Forms.ImageList LargeImageList;
		System.Windows.Forms.ColumnHeader IconIndex;
		System.Windows.Forms.LinkLabel labelHelpLink;

        #endregion
    }
        
}
