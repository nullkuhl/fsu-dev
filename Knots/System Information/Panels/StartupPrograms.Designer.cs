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
	/// Designer for Startup.
	/// </summary>
    public partial class StartupPrograms : SystemInformation.TaskPanelBase
    {

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        public StartupPrograms()
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
            this.StartupImageList = new System.Windows.Forms.ImageList(this.components);
            this.labelArguments = new System.Windows.Forms.Label();
            this.labelArgumentsDesc = new System.Windows.Forms.Label();
            this.labelProductNameDesc = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelCommandDesc = new System.Windows.Forms.Label();
            this.labelFileVersionDesc = new System.Windows.Forms.Label();
            this.labelDescriptionDesc = new System.Windows.Forms.Label();
            this.labelCompanyDesc = new System.Windows.Forms.Label();
            this.labelCommand = new System.Windows.Forms.Label();
            this.labelFileVersion = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelCompany = new System.Windows.Forms.Label();
            this.labelDetails = new System.Windows.Forms.Label();
            this.labelSeparator2 = new System.Windows.Forms.Label();
            this.listviewStartup = new System.Windows.Forms.ListView();
            this.ItemName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelStartupDescription = new System.Windows.Forms.Label();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.picturePanel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).BeginInit();
            this.SuspendLayout();
            // 
            // StartupImageList
            // 
            this.StartupImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.StartupImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.StartupImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // labelArguments
            // 
            this.labelArguments.BackColor = System.Drawing.Color.Transparent;
            this.labelArguments.ForeColor = System.Drawing.Color.Black;
            this.labelArguments.Location = new System.Drawing.Point(153, 404);
            this.labelArguments.Name = "labelArguments";
            this.labelArguments.Size = new System.Drawing.Size(341, 15);
            this.labelArguments.TabIndex = 75;
            this.labelArguments.Visible = false;
            // 
            // labelArgumentsDesc
            // 
            this.labelArgumentsDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelArgumentsDesc.ForeColor = System.Drawing.Color.Black;
            this.labelArgumentsDesc.Location = new System.Drawing.Point(19, 404);
            this.labelArgumentsDesc.Name = "labelArgumentsDesc";
            this.labelArgumentsDesc.Size = new System.Drawing.Size(128, 15);
            this.labelArgumentsDesc.TabIndex = 74;
            this.labelArgumentsDesc.Visible = false;
            // 
            // labelProductNameDesc
            // 
            this.labelProductNameDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelProductNameDesc.ForeColor = System.Drawing.Color.Black;
            this.labelProductNameDesc.Location = new System.Drawing.Point(19, 324);
            this.labelProductNameDesc.Name = "labelProductNameDesc";
            this.labelProductNameDesc.Size = new System.Drawing.Size(128, 15);
            this.labelProductNameDesc.TabIndex = 73;
            this.labelProductNameDesc.Click += new System.EventHandler(this.labelProductNameDesc_Click);
            // 
            // labelProductName
            // 
            this.labelProductName.BackColor = System.Drawing.Color.Transparent;
            this.labelProductName.ForeColor = System.Drawing.Color.Black;
            this.labelProductName.Location = new System.Drawing.Point(153, 324);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(341, 15);
            this.labelProductName.TabIndex = 72;
            // 
            // labelCommandDesc
            // 
            this.labelCommandDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelCommandDesc.ForeColor = System.Drawing.Color.Black;
            this.labelCommandDesc.Location = new System.Drawing.Point(19, 384);
            this.labelCommandDesc.Name = "labelCommandDesc";
            this.labelCommandDesc.Size = new System.Drawing.Size(128, 15);
            this.labelCommandDesc.TabIndex = 71;
            // 
            // labelFileVersionDesc
            // 
            this.labelFileVersionDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelFileVersionDesc.ForeColor = System.Drawing.Color.Black;
            this.labelFileVersionDesc.Location = new System.Drawing.Point(19, 364);
            this.labelFileVersionDesc.Name = "labelFileVersionDesc";
            this.labelFileVersionDesc.Size = new System.Drawing.Size(128, 15);
            this.labelFileVersionDesc.TabIndex = 70;
            // 
            // labelDescriptionDesc
            // 
            this.labelDescriptionDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelDescriptionDesc.ForeColor = System.Drawing.Color.Black;
            this.labelDescriptionDesc.Location = new System.Drawing.Point(19, 344);
            this.labelDescriptionDesc.Name = "labelDescriptionDesc";
            this.labelDescriptionDesc.Size = new System.Drawing.Size(128, 15);
            this.labelDescriptionDesc.TabIndex = 69;
            // 
            // labelCompanyDesc
            // 
            this.labelCompanyDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelCompanyDesc.ForeColor = System.Drawing.Color.Black;
            this.labelCompanyDesc.Location = new System.Drawing.Point(19, 304);
            this.labelCompanyDesc.Name = "labelCompanyDesc";
            this.labelCompanyDesc.Size = new System.Drawing.Size(128, 15);
            this.labelCompanyDesc.TabIndex = 68;
            this.labelCompanyDesc.Click += new System.EventHandler(this.labelCompanyDesc_Click);
            // 
            // labelCommand
            // 
            this.labelCommand.BackColor = System.Drawing.Color.Transparent;
            this.labelCommand.ForeColor = System.Drawing.Color.Black;
            this.labelCommand.Location = new System.Drawing.Point(153, 383);
            this.labelCommand.Name = "labelCommand";
            this.labelCommand.Size = new System.Drawing.Size(341, 36);
            this.labelCommand.TabIndex = 67;
            // 
            // labelFileVersion
            // 
            this.labelFileVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelFileVersion.ForeColor = System.Drawing.Color.Black;
            this.labelFileVersion.Location = new System.Drawing.Point(153, 364);
            this.labelFileVersion.Name = "labelFileVersion";
            this.labelFileVersion.Size = new System.Drawing.Size(341, 15);
            this.labelFileVersion.TabIndex = 66;
            // 
            // labelDescription
            // 
            this.labelDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelDescription.ForeColor = System.Drawing.Color.Black;
            this.labelDescription.Location = new System.Drawing.Point(153, 344);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(341, 15);
            this.labelDescription.TabIndex = 65;
            // 
            // labelCompany
            // 
            this.labelCompany.BackColor = System.Drawing.Color.Transparent;
            this.labelCompany.ForeColor = System.Drawing.Color.Black;
            this.labelCompany.Location = new System.Drawing.Point(153, 304);
            this.labelCompany.Name = "labelCompany";
            this.labelCompany.Size = new System.Drawing.Size(341, 15);
            this.labelCompany.TabIndex = 64;
            // 
            // labelDetails
            // 
            this.labelDetails.AutoSize = true;
            this.labelDetails.BackColor = System.Drawing.Color.Transparent;
            this.labelDetails.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDetails.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelDetails.Location = new System.Drawing.Point(16, 275);
            this.labelDetails.Name = "labelDetails";
            this.labelDetails.Size = new System.Drawing.Size(0, 17);
            this.labelDetails.TabIndex = 63;
            // 
            // labelSeparator2
            // 
            this.labelSeparator2.BackColor = System.Drawing.Color.Black;
            this.labelSeparator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator2.Location = new System.Drawing.Point(16, 296);
            this.labelSeparator2.Name = "labelSeparator2";
            this.labelSeparator2.Size = new System.Drawing.Size(482, 3);
            this.labelSeparator2.TabIndex = 62;
            // 
            // listviewStartup
            // 
            this.listviewStartup.BackColor = System.Drawing.SystemColors.Window;
            this.listviewStartup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ItemName,
            this.FileName,
            this.Type,
            this.Status});
            this.listviewStartup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewStartup.ForeColor = System.Drawing.Color.Black;
            this.listviewStartup.FullRowSelect = true;
            this.listviewStartup.LabelWrap = false;
            this.listviewStartup.Location = new System.Drawing.Point(16, 108);
            this.listviewStartup.MultiSelect = false;
            this.listviewStartup.Name = "listviewStartup";
            this.listviewStartup.Size = new System.Drawing.Size(482, 160);
            this.listviewStartup.SmallImageList = this.StartupImageList;
            this.listviewStartup.TabIndex = 48;
            this.listviewStartup.UseCompatibleStateImageBehavior = false;
            this.listviewStartup.View = System.Windows.Forms.View.Details;
            this.listviewStartup.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listviewStartup_ColumnClick);
            this.listviewStartup.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listviewStartup_ItemSelectionChanged);
            // 
            // ItemName
            // 
            this.ItemName.Width = 160;
            // 
            // FileName
            // 
            this.FileName.Width = 120;
            // 
            // Type
            // 
            this.Type.Width = 117;
            // 
            // labelStartupDescription
            // 
            this.labelStartupDescription.AutoSize = true;
            this.labelStartupDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelStartupDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStartupDescription.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelStartupDescription.Location = new System.Drawing.Point(16, 76);
            this.labelStartupDescription.Name = "labelStartupDescription";
            this.labelStartupDescription.Size = new System.Drawing.Size(0, 17);
            this.labelStartupDescription.TabIndex = 13;
            // 
            // labelSeparator
            // 
            this.labelSeparator.BackColor = System.Drawing.Color.Black;
            this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator.Location = new System.Drawing.Point(16, 98);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(482, 3);
            this.labelSeparator.TabIndex = 12;
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
            this.picturePanel.Image = global::SystemInformation.Properties.Resources.Startup_48x48;
            this.picturePanel.Location = new System.Drawing.Point(16, 16);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(48, 48);
            this.picturePanel.TabIndex = 6;
            this.picturePanel.TabStop = false;
            // 
            // StartupPrograms
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.labelArguments);
            this.Controls.Add(this.labelArgumentsDesc);
            this.Controls.Add(this.labelProductNameDesc);
            this.Controls.Add(this.labelProductName);
            this.Controls.Add(this.labelCommandDesc);
            this.Controls.Add(this.labelFileVersionDesc);
            this.Controls.Add(this.labelDescriptionDesc);
            this.Controls.Add(this.labelCompanyDesc);
            this.Controls.Add(this.labelCommand);
            this.Controls.Add(this.labelFileVersion);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelCompany);
            this.Controls.Add(this.labelDetails);
            this.Controls.Add(this.labelSeparator2);
            this.Controls.Add(this.listviewStartup);
            this.Controls.Add(this.labelStartupDescription);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.picturePanel);
            this.Name = "StartupPrograms";
            this.Size = new System.Drawing.Size(525, 425);
            this.Load += new System.EventHandler(this.Startup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.Label labelTitle;
        System.Windows.Forms.PictureBox picturePanel;
        System.Windows.Forms.Label labelStartupDescription;
        System.Windows.Forms.Label labelSeparator;
        ListView listviewStartup;
        ColumnHeader ItemName;
        ColumnHeader FileName;
        ColumnHeader Type;
        ColumnHeader Status;
        Label labelCommandDesc;
        Label labelFileVersionDesc;
        Label labelDescriptionDesc;
        Label labelCompanyDesc;
        Label labelCommand;
        Label labelFileVersion;
        Label labelDescription;
        Label labelCompany;
        Label labelDetails;
        Label labelSeparator2;
        Label labelProductNameDesc;
        Label labelProductName;
        ImageList StartupImageList;
        Label labelArgumentsDesc;
        Label labelArguments;
		
	}
	
}
