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
	/// Designer for Introduction.
	/// </summary>
    /// 
    public partial class Introduction : SystemInformation.TaskPanelBase
	{

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        public Introduction()
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
            this.labelWindows = new System.Windows.Forms.Label();
            this.labelTitleCompany = new System.Windows.Forms.Label();
            this.labelCustomerOrganization = new System.Windows.Forms.Label();
            this.labelCustomerName = new System.Windows.Forms.Label();
            this.llbEULA = new System.Windows.Forms.LinkLabel();
            this.labelLicenseTerms = new System.Windows.Forms.Label();
            this.labelAppCopyright = new System.Windows.Forms.Label();
            this.labelAppDescription = new System.Windows.Forms.Label();
            this.labelAppVersion = new System.Windows.Forms.Label();
            this.labelAbout = new System.Windows.Forms.Label();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelDirections = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.picturePanel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).BeginInit();
            this.SuspendLayout();
            // 
            // labelWindows
            // 
            this.labelWindows.Location = new System.Drawing.Point(0, 0);
            this.labelWindows.Name = "labelWindows";
            this.labelWindows.Size = new System.Drawing.Size(100, 23);
            this.labelWindows.TabIndex = 0;
            // 
            // labelTitleCompany
            // 
            this.labelTitleCompany.Location = new System.Drawing.Point(0, 0);
            this.labelTitleCompany.Name = "labelTitleCompany";
            this.labelTitleCompany.Size = new System.Drawing.Size(100, 23);
            this.labelTitleCompany.TabIndex = 1;
            // 
            // labelCustomerOrganization
            // 
            this.labelCustomerOrganization.Location = new System.Drawing.Point(0, 0);
            this.labelCustomerOrganization.Name = "labelCustomerOrganization";
            this.labelCustomerOrganization.Size = new System.Drawing.Size(100, 23);
            this.labelCustomerOrganization.TabIndex = 2;
            // 
            // labelCustomerName
            // 
            this.labelCustomerName.Location = new System.Drawing.Point(0, 0);
            this.labelCustomerName.Name = "labelCustomerName";
            this.labelCustomerName.Size = new System.Drawing.Size(100, 23);
            this.labelCustomerName.TabIndex = 3;
            // 
            // llbEULA
            // 
            this.llbEULA.Location = new System.Drawing.Point(0, 0);
            this.llbEULA.Name = "llbEULA";
            this.llbEULA.Size = new System.Drawing.Size(100, 23);
            this.llbEULA.TabIndex = 4;
            // 
            // labelLicenseTerms
            // 
            this.labelLicenseTerms.Location = new System.Drawing.Point(0, 0);
            this.labelLicenseTerms.Name = "labelLicenseTerms";
            this.labelLicenseTerms.Size = new System.Drawing.Size(100, 23);
            this.labelLicenseTerms.TabIndex = 5;
            // 
            // labelAppCopyright
            // 
            this.labelAppCopyright.Location = new System.Drawing.Point(0, 0);
            this.labelAppCopyright.Name = "labelAppCopyright";
            this.labelAppCopyright.Size = new System.Drawing.Size(100, 23);
            this.labelAppCopyright.TabIndex = 6;
            // 
            // labelAppDescription
            // 
            this.labelAppDescription.Location = new System.Drawing.Point(0, 0);
            this.labelAppDescription.Name = "labelAppDescription";
            this.labelAppDescription.Size = new System.Drawing.Size(100, 23);
            this.labelAppDescription.TabIndex = 7;
            // 
            // labelAppVersion
            // 
            this.labelAppVersion.Location = new System.Drawing.Point(0, 0);
            this.labelAppVersion.Name = "labelAppVersion";
            this.labelAppVersion.Size = new System.Drawing.Size(100, 23);
            this.labelAppVersion.TabIndex = 8;
            // 
            // labelAbout
            // 
            this.labelAbout.Location = new System.Drawing.Point(0, 0);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(100, 23);
            this.labelAbout.TabIndex = 9;
            // 
            // labelSeparator
            // 
            this.labelSeparator.BackColor = System.Drawing.Color.Black;
            this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator.Location = new System.Drawing.Point(45, 156);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(425, 3);
            this.labelSeparator.TabIndex = 5;
            // 
            // labelDirections
            // 
            this.labelDirections.AutoSize = true;
            this.labelDirections.BackColor = System.Drawing.Color.Transparent;
            this.labelDirections.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDirections.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelDirections.Location = new System.Drawing.Point(45, 87);
            this.labelDirections.MaximumSize = new System.Drawing.Size(425, 0);
            this.labelDirections.Name = "labelDirections";
            this.labelDirections.Size = new System.Drawing.Size(0, 17);
            this.labelDirections.TabIndex = 4;
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
            this.labelTitle.TabIndex = 3;
            // 
            // picturePanel
            // 
            this.picturePanel.BackColor = System.Drawing.Color.Transparent;
            this.picturePanel.Image = global::SystemInformation.Properties.Resources.System_Information_48x48;
            this.picturePanel.Location = new System.Drawing.Point(16, 16);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(48, 48);
            this.picturePanel.TabIndex = 2;
            this.picturePanel.TabStop = false;
            // 
            // Introduction
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.labelWindows);
            this.Controls.Add(this.labelTitleCompany);
            this.Controls.Add(this.labelCustomerOrganization);
            this.Controls.Add(this.labelCustomerName);
            this.Controls.Add(this.llbEULA);
            this.Controls.Add(this.labelLicenseTerms);
            this.Controls.Add(this.labelAppCopyright);
            this.Controls.Add(this.labelAppDescription);
            this.Controls.Add(this.labelAppVersion);
            this.Controls.Add(this.labelAbout);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.labelDirections);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.picturePanel);
            this.Name = "Introduction";
            this.Size = new System.Drawing.Size(497, 425);
            this.Load += new System.EventHandler(this.Introduction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        System.Windows.Forms.Label labelTitle;
        System.Windows.Forms.PictureBox picturePanel;
        System.Windows.Forms.Label labelDirections;
        System.Windows.Forms.Label labelSeparator;
        System.Windows.Forms.Label labelAbout;
        System.Windows.Forms.Label labelAppVersion;
        System.Windows.Forms.Label labelAppDescription;
        System.Windows.Forms.Label labelAppCopyright;
        System.Windows.Forms.Label labelLicenseTerms;
        System.Windows.Forms.LinkLabel llbEULA;
        System.Windows.Forms.Label labelCustomerName;
        System.Windows.Forms.Label labelCustomerOrganization;
        System.Windows.Forms.Label labelTitleCompany;
        System.Windows.Forms.Label labelWindows;
		
        #endregion
		
	}
	
}
