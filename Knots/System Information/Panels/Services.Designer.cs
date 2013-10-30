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
	/// Designer for Services.
	/// </summary>
    public partial class Services : SystemInformation.TaskPanelBase
    {

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        public Services()
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.picturePanel = new System.Windows.Forms.PictureBox();
            this.labelServicesDescription = new System.Windows.Forms.Label();
            this.labelSeparatorTop = new System.Windows.Forms.Label();
            this.listviewServices = new System.Windows.Forms.ListView();
            this.DisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartMode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.State = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PathName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelPathName = new System.Windows.Forms.Label();
            this.labelDetails = new System.Windows.Forms.Label();
            this.labelSeparatorBottom = new System.Windows.Forms.Label();
            this.textboxPathName = new System.Windows.Forms.TextBox();
            this.textboxDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).BeginInit();
            this.SuspendLayout();
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
            this.picturePanel.Image = global::SystemInformation.Properties.Resources.Services_48x48;
            this.picturePanel.Location = new System.Drawing.Point(16, 16);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(48, 48);
            this.picturePanel.TabIndex = 6;
            this.picturePanel.TabStop = false;
            // 
            // labelServicesDescription
            // 
            this.labelServicesDescription.AutoSize = true;
            this.labelServicesDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelServicesDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelServicesDescription.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelServicesDescription.Location = new System.Drawing.Point(16, 76);
            this.labelServicesDescription.Name = "labelServicesDescription";
            this.labelServicesDescription.Size = new System.Drawing.Size(0, 17);
            this.labelServicesDescription.TabIndex = 13;
            // 
            // labelSeparatorTop
            // 
            this.labelSeparatorTop.BackColor = System.Drawing.Color.Black;
            this.labelSeparatorTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparatorTop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparatorTop.Location = new System.Drawing.Point(16, 98);
            this.labelSeparatorTop.Name = "labelSeparatorTop";
            this.labelSeparatorTop.Size = new System.Drawing.Size(482, 3);
            this.labelSeparatorTop.TabIndex = 12;
            // 
            // listviewServices
            // 
            this.listviewServices.BackColor = System.Drawing.SystemColors.Window;
            this.listviewServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DisplayName,
            this.StartMode,
            this.State,
            this.PathName,
            this.Description});
            this.listviewServices.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewServices.ForeColor = System.Drawing.Color.Black;
            this.listviewServices.FullRowSelect = true;
            this.listviewServices.LabelWrap = false;
            this.listviewServices.Location = new System.Drawing.Point(16, 108);
            this.listviewServices.MultiSelect = false;
            this.listviewServices.Name = "listviewServices";
            this.listviewServices.Size = new System.Drawing.Size(482, 160);
            this.listviewServices.TabIndex = 48;
            this.listviewServices.UseCompatibleStateImageBehavior = false;
            this.listviewServices.View = System.Windows.Forms.View.Details;
            this.listviewServices.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listviewServices_ColumnClick);
            this.listviewServices.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listviewServices_ItemSelectionChanged);
            // 
            // DisplayName
            // 
            this.DisplayName.Width = 286;
            // 
            // StartMode
            // 
            this.StartMode.Width = 86;
            // 
            // State
            // 
            this.State.Width = 86;
            // 
            // PathName
            // 
            this.PathName.Width = 0;
            // 
            // Description
            // 
            this.Description.Width = 0;
            // 
            // labelPathName
            // 
            this.labelPathName.BackColor = System.Drawing.Color.Transparent;
            this.labelPathName.ForeColor = System.Drawing.Color.Black;
            this.labelPathName.Location = new System.Drawing.Point(16, 304);
            this.labelPathName.Name = "labelPathName";
            this.labelPathName.Size = new System.Drawing.Size(82, 15);
            this.labelPathName.TabIndex = 68;
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
            // labelSeparatorBottom
            // 
            this.labelSeparatorBottom.BackColor = System.Drawing.Color.Black;
            this.labelSeparatorBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparatorBottom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparatorBottom.Location = new System.Drawing.Point(16, 296);
            this.labelSeparatorBottom.Name = "labelSeparatorBottom";
            this.labelSeparatorBottom.Size = new System.Drawing.Size(482, 3);
            this.labelSeparatorBottom.TabIndex = 62;
            // 
            // textboxPathName
            // 
            this.textboxPathName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxPathName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxPathName.Location = new System.Drawing.Point(101, 303);
            this.textboxPathName.Multiline = true;
            this.textboxPathName.Name = "textboxPathName";
            this.textboxPathName.ReadOnly = true;
            this.textboxPathName.Size = new System.Drawing.Size(393, 37);
            this.textboxPathName.TabIndex = 75;
            // 
            // textboxDescription
            // 
            this.textboxDescription.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxDescription.Location = new System.Drawing.Point(16, 346);
            this.textboxDescription.Multiline = true;
            this.textboxDescription.Name = "textboxDescription";
            this.textboxDescription.ReadOnly = true;
            this.textboxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxDescription.Size = new System.Drawing.Size(480, 74);
            this.textboxDescription.TabIndex = 76;
            // 
            // labelDescription
            // 
            this.labelDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelDescription.ForeColor = System.Drawing.Color.Black;
            this.labelDescription.Location = new System.Drawing.Point(16, 324);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(82, 15);
            this.labelDescription.TabIndex = 77;
            // 
            // Services
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.textboxDescription);
            this.Controls.Add(this.textboxPathName);
            this.Controls.Add(this.labelPathName);
            this.Controls.Add(this.labelDetails);
            this.Controls.Add(this.labelSeparatorBottom);
            this.Controls.Add(this.listviewServices);
            this.Controls.Add(this.labelServicesDescription);
            this.Controls.Add(this.labelSeparatorTop);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.picturePanel);
            this.Name = "Services";
            this.Load += new System.EventHandler(this.Services_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.Label labelTitle;
        System.Windows.Forms.PictureBox picturePanel;
        System.Windows.Forms.Label labelServicesDescription;
        System.Windows.Forms.Label labelSeparatorTop;
        ListView listviewServices;
        ColumnHeader DisplayName;
        ColumnHeader StartMode;
        ColumnHeader State;
        Label labelPathName;
        Label labelDetails;
        Label labelSeparatorBottom;
        TextBox textboxPathName;
        TextBox textboxDescription;
        ColumnHeader PathName;
        ColumnHeader Description;
        Label labelDescription;
		
	}
	
}
