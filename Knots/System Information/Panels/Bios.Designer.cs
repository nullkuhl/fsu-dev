using System.Resources;

namespace SystemInformation
{
	/// <summary>
	/// Designer for Bios.
	/// </summary>
    public partial class Bios : SystemInformation.TaskPanelBase
    {

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        /// <summary>
        /// Debug code for TaskPanelBase.
        /// </summary>
        public Bios()
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
		
		System.Windows.Forms.Label labelTitle;
		System.Windows.Forms.PictureBox picturePanel;
		System.Windows.Forms.Label labelSMBIOSPresent;
		System.Windows.Forms.Label labelReleaseDate;
		System.Windows.Forms.Label labelVersion;
		System.Windows.Forms.Label labelModel;
		System.Windows.Forms.Label labelManufacturer;
		System.Windows.Forms.Label labelSMPresentDesc;
		System.Windows.Forms.Label labelReleaseDateDesc;
		System.Windows.Forms.Label labelModelDesc;
		System.Windows.Forms.Label labelManDesc;
		System.Windows.Forms.Label labelDescription;
		System.Windows.Forms.Label labelSeparator;
		System.Windows.Forms.Label labelVersionDesc;
		System.Windows.Forms.Label labelSMVerDesc;
		System.Windows.Forms.Label labelSMBIOSVersion;
		System.Windows.Forms.Label labelFeatures;
		System.Windows.Forms.Label labelSeparator2;
		System.Windows.Forms.ListView listviewFeatures;
		System.Windows.Forms.ColumnHeader ColumnHeader1;
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		void InitializeComponent()
		{
            this.labelTitle = new System.Windows.Forms.Label();
            this.picturePanel = new System.Windows.Forms.PictureBox();
            this.labelSMBIOSPresent = new System.Windows.Forms.Label();
            this.labelReleaseDate = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelModel = new System.Windows.Forms.Label();
            this.labelManufacturer = new System.Windows.Forms.Label();
            this.labelSMPresentDesc = new System.Windows.Forms.Label();
            this.labelReleaseDateDesc = new System.Windows.Forms.Label();
            this.labelModelDesc = new System.Windows.Forms.Label();
            this.labelManDesc = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelVersionDesc = new System.Windows.Forms.Label();
            this.labelSMVerDesc = new System.Windows.Forms.Label();
            this.labelSMBIOSVersion = new System.Windows.Forms.Label();
            this.labelFeatures = new System.Windows.Forms.Label();
            this.labelSeparator2 = new System.Windows.Forms.Label();
            this.listviewFeatures = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTitle.Location = new System.Drawing.Point(80, 29);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(0, 25);
            this.labelTitle.TabIndex = 7;
            // 
            // picturePanel
            // 
            this.picturePanel.BackColor = System.Drawing.Color.Transparent;
            this.picturePanel.Image = global::SystemInformation.Properties.Resources.BIOS_48x48;
            this.picturePanel.Location = new System.Drawing.Point(16, 16);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(48, 48);
            this.picturePanel.TabIndex = 6;
            this.picturePanel.TabStop = false;
            // 
            // labelSMBIOSPresent
            // 
            this.labelSMBIOSPresent.BackColor = System.Drawing.Color.Transparent;
            this.labelSMBIOSPresent.Location = new System.Drawing.Point(168, 184);
            this.labelSMBIOSPresent.Name = "labelSMBIOSPresent";
            this.labelSMBIOSPresent.Size = new System.Drawing.Size(328, 15);
            this.labelSMBIOSPresent.TabIndex = 41;
            // 
            // labelReleaseDate
            // 
            this.labelReleaseDate.BackColor = System.Drawing.Color.Transparent;
            this.labelReleaseDate.Location = new System.Drawing.Point(168, 164);
            this.labelReleaseDate.Name = "labelReleaseDate";
            this.labelReleaseDate.Size = new System.Drawing.Size(328, 15);
            this.labelReleaseDate.TabIndex = 40;
            // 
            // labelVersion
            // 
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.Location = new System.Drawing.Point(168, 144);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(328, 15);
            this.labelVersion.TabIndex = 39;
            // 
            // labelModel
            // 
            this.labelModel.BackColor = System.Drawing.Color.Transparent;
            this.labelModel.Location = new System.Drawing.Point(168, 124);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(328, 15);
            this.labelModel.TabIndex = 38;
            // 
            // labelManufacturer
            // 
            this.labelManufacturer.BackColor = System.Drawing.Color.Transparent;
            this.labelManufacturer.Location = new System.Drawing.Point(168, 106);
            this.labelManufacturer.Name = "labelManufacturer";
            this.labelManufacturer.Size = new System.Drawing.Size(328, 15);
            this.labelManufacturer.TabIndex = 37;
            // 
            // labelSMPresentDesc
            // 
            this.labelSMPresentDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelSMPresentDesc.Location = new System.Drawing.Point(16, 184);
            this.labelSMPresentDesc.Name = "labelSMPresentDesc";
            this.labelSMPresentDesc.Size = new System.Drawing.Size(144, 13);
            this.labelSMPresentDesc.TabIndex = 36;
            // 
            // labelReleaseDateDesc
            // 
            this.labelReleaseDateDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelReleaseDateDesc.Location = new System.Drawing.Point(16, 164);
            this.labelReleaseDateDesc.Name = "labelReleaseDateDesc";
            this.labelReleaseDateDesc.Size = new System.Drawing.Size(144, 13);
            this.labelReleaseDateDesc.TabIndex = 35;
            // 
            // labelModelDesc
            // 
            this.labelModelDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelModelDesc.Location = new System.Drawing.Point(16, 124);
            this.labelModelDesc.Name = "labelModelDesc";
            this.labelModelDesc.Size = new System.Drawing.Size(144, 13);
            this.labelModelDesc.TabIndex = 34;
            // 
            // labelManDesc
            // 
            this.labelManDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelManDesc.Location = new System.Drawing.Point(16, 106);
            this.labelManDesc.Name = "labelManDesc";
            this.labelManDesc.Size = new System.Drawing.Size(144, 13);
            this.labelManDesc.TabIndex = 33;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelDescription.Location = new System.Drawing.Point(16, 72);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(0, 17);
            this.labelDescription.TabIndex = 32;
            // 
            // labelSeparator
            // 
            this.labelSeparator.BackColor = System.Drawing.Color.Black;
            this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator.Location = new System.Drawing.Point(16, 94);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(481, 3);
            this.labelSeparator.TabIndex = 31;
            // 
            // labelVersionDesc
            // 
            this.labelVersionDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelVersionDesc.Location = new System.Drawing.Point(16, 144);
            this.labelVersionDesc.Name = "labelVersionDesc";
            this.labelVersionDesc.Size = new System.Drawing.Size(144, 13);
            this.labelVersionDesc.TabIndex = 42;
            // 
            // labelSMVerDesc
            // 
            this.labelSMVerDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelSMVerDesc.Location = new System.Drawing.Point(16, 204);
            this.labelSMVerDesc.Name = "labelSMVerDesc";
            this.labelSMVerDesc.Size = new System.Drawing.Size(144, 13);
            this.labelSMVerDesc.TabIndex = 43;
            // 
            // labelSMBIOSVersion
            // 
            this.labelSMBIOSVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelSMBIOSVersion.Location = new System.Drawing.Point(168, 204);
            this.labelSMBIOSVersion.Name = "labelSMBIOSVersion";
            this.labelSMBIOSVersion.Size = new System.Drawing.Size(328, 15);
            this.labelSMBIOSVersion.TabIndex = 44;
            // 
            // labelFeatures
            // 
            this.labelFeatures.BackColor = System.Drawing.Color.Transparent;
            this.labelFeatures.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFeatures.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelFeatures.Location = new System.Drawing.Point(16, 232);
            this.labelFeatures.Name = "labelFeatures";
            this.labelFeatures.Size = new System.Drawing.Size(480, 17);
            this.labelFeatures.TabIndex = 46;
            // 
            // labelSeparator2
            // 
            this.labelSeparator2.BackColor = System.Drawing.Color.Black;
            this.labelSeparator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator2.Location = new System.Drawing.Point(16, 254);
            this.labelSeparator2.Name = "labelSeparator2";
            this.labelSeparator2.Size = new System.Drawing.Size(481, 3);
            this.labelSeparator2.TabIndex = 45;
            // 
            // listviewFeatures
            // 
            this.listviewFeatures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1});
            this.listviewFeatures.ForeColor = System.Drawing.Color.Black;
            this.listviewFeatures.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listviewFeatures.Location = new System.Drawing.Point(16, 266);
            this.listviewFeatures.Name = "listviewFeatures";
            this.listviewFeatures.Size = new System.Drawing.Size(481, 154);
            this.listviewFeatures.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listviewFeatures.TabIndex = 47;
            this.listviewFeatures.UseCompatibleStateImageBehavior = false;
            this.listviewFeatures.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Width = 380;
            // 
            // Bios
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.listviewFeatures);
            this.Controls.Add(this.labelFeatures);
            this.Controls.Add(this.labelSeparator2);
            this.Controls.Add(this.labelSMBIOSVersion);
            this.Controls.Add(this.labelSMVerDesc);
            this.Controls.Add(this.labelVersionDesc);
            this.Controls.Add(this.labelSMBIOSPresent);
            this.Controls.Add(this.labelReleaseDate);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelModel);
            this.Controls.Add(this.labelManufacturer);
            this.Controls.Add(this.labelSMPresentDesc);
            this.Controls.Add(this.labelReleaseDateDesc);
            this.Controls.Add(this.labelModelDesc);
            this.Controls.Add(this.labelManDesc);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.picturePanel);
            this.Name = "Bios";
            this.Load += new System.EventHandler(this.Bios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
        #endregion
		
	}
	
}
