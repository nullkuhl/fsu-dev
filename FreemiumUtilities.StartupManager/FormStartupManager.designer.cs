namespace FreemiumUtilities.StartupManager
{
	partial class FrmStartupMan
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStartupMan));
			this.listviewStartup = new System.Windows.Forms.ListView();
			this.ItemName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.FileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panelDetails = new System.Windows.Forms.Panel();
			this.firstCover = new System.Windows.Forms.Label();
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
			this.pictureBoxPanel = new System.Windows.Forms.PictureBox();
			this.panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPanel)).BeginInit();
			this.SuspendLayout();
			// 
			// listviewStartup
			// 
			resources.ApplyResources(this.listviewStartup, "listviewStartup");
			this.listviewStartup.BackColor = System.Drawing.SystemColors.Window;
			this.listviewStartup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.ItemName,
			this.FileName,
			this.Type,
			this.Status});
			this.listviewStartup.ForeColor = System.Drawing.SystemColors.ControlText;
			this.listviewStartup.FullRowSelect = true;
			this.listviewStartup.MultiSelect = false;
			this.listviewStartup.Name = "listviewStartup";
			this.listviewStartup.UseCompatibleStateImageBehavior = false;
			this.listviewStartup.View = System.Windows.Forms.View.Details;
			// 
			// ItemName
			// 
			resources.ApplyResources(this.ItemName, "ItemName");
			// 
			// FileName
			// 
			resources.ApplyResources(this.FileName, "FileName");
			// 
			// Type
			// 
			resources.ApplyResources(this.Type, "Type");
			// 
			// Status
			// 
			resources.ApplyResources(this.Status, "Status");
			// 
			// panelDetails
			// 
			resources.ApplyResources(this.panelDetails, "panelDetails");
			this.panelDetails.Controls.Add(this.firstCover);
			this.panelDetails.Controls.Add(this.labelArguments);
			this.panelDetails.Controls.Add(this.labelArgumentsDesc);
			this.panelDetails.Controls.Add(this.labelProductNameDesc);
			this.panelDetails.Controls.Add(this.labelProductName);
			this.panelDetails.Controls.Add(this.labelCommandDesc);
			this.panelDetails.Controls.Add(this.labelFileVersionDesc);
			this.panelDetails.Controls.Add(this.labelDescriptionDesc);
			this.panelDetails.Controls.Add(this.labelCompanyDesc);
			this.panelDetails.Controls.Add(this.labelCommand);
			this.panelDetails.Controls.Add(this.labelFileVersion);
			this.panelDetails.Controls.Add(this.labelDescription);
			this.panelDetails.Controls.Add(this.labelCompany);
			this.panelDetails.Controls.Add(this.labelDetails);
			this.panelDetails.Controls.Add(this.pictureBoxPanel);
			this.panelDetails.Name = "panelDetails";
			// 
			// firstCover
			// 
			resources.ApplyResources(this.firstCover, "firstCover");
			this.firstCover.MinimumSize = new System.Drawing.Size(672, 125);
			this.firstCover.Name = "firstCover";
			// 
			// labelArguments
			// 
			this.labelArguments.BackColor = System.Drawing.Color.Transparent;
			this.labelArguments.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.labelArguments, "labelArguments");
			this.labelArguments.Name = "labelArguments";
			// 
			// labelArgumentsDesc
			// 
			this.labelArgumentsDesc.BackColor = System.Drawing.Color.Transparent;
			this.labelArgumentsDesc.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.labelArgumentsDesc, "labelArgumentsDesc");
			this.labelArgumentsDesc.Name = "labelArgumentsDesc";
			// 
			// labelProductNameDesc
			// 
			this.labelProductNameDesc.BackColor = System.Drawing.Color.Transparent;
			this.labelProductNameDesc.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.labelProductNameDesc, "labelProductNameDesc");
			this.labelProductNameDesc.Name = "labelProductNameDesc";
			// 
			// labelProductName
			// 
			this.labelProductName.BackColor = System.Drawing.Color.Transparent;
			this.labelProductName.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.labelProductName, "labelProductName");
			this.labelProductName.Name = "labelProductName";
			// 
			// labelCommandDesc
			// 
			this.labelCommandDesc.BackColor = System.Drawing.Color.Transparent;
			this.labelCommandDesc.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.labelCommandDesc, "labelCommandDesc");
			this.labelCommandDesc.Name = "labelCommandDesc";
			// 
			// labelFileVersionDesc
			// 
			this.labelFileVersionDesc.BackColor = System.Drawing.Color.Transparent;
			this.labelFileVersionDesc.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.labelFileVersionDesc, "labelFileVersionDesc");
			this.labelFileVersionDesc.Name = "labelFileVersionDesc";
			// 
			// labelDescriptionDesc
			// 
			this.labelDescriptionDesc.BackColor = System.Drawing.Color.Transparent;
			this.labelDescriptionDesc.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.labelDescriptionDesc, "labelDescriptionDesc");
			this.labelDescriptionDesc.Name = "labelDescriptionDesc";
			// 
			// labelCompanyDesc
			// 
			this.labelCompanyDesc.BackColor = System.Drawing.Color.Transparent;
			this.labelCompanyDesc.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.labelCompanyDesc, "labelCompanyDesc");
			this.labelCompanyDesc.Name = "labelCompanyDesc";
			// 
			// labelCommand
			// 
			this.labelCommand.BackColor = System.Drawing.Color.Transparent;
			this.labelCommand.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.labelCommand, "labelCommand");
			this.labelCommand.Name = "labelCommand";
			// 
			// labelFileVersion
			// 
			this.labelFileVersion.BackColor = System.Drawing.Color.Transparent;
			this.labelFileVersion.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.labelFileVersion, "labelFileVersion");
			this.labelFileVersion.Name = "labelFileVersion";
			// 
			// labelDescription
			// 
			this.labelDescription.BackColor = System.Drawing.Color.Transparent;
			this.labelDescription.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.labelDescription, "labelDescription");
			this.labelDescription.Name = "labelDescription";
			// 
			// labelCompany
			// 
			this.labelCompany.BackColor = System.Drawing.Color.Transparent;
			this.labelCompany.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.labelCompany, "labelCompany");
			this.labelCompany.Name = "labelCompany";
			// 
			// labelDetails
			// 
			resources.ApplyResources(this.labelDetails, "labelDetails");
			this.labelDetails.BackColor = System.Drawing.Color.Transparent;
			this.labelDetails.ForeColor = System.Drawing.Color.DarkGreen;
			this.labelDetails.Name = "labelDetails";
			// 
			// pictureBoxPanel
			// 
			this.pictureBoxPanel.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.pictureBoxPanel, "pictureBoxPanel");
			this.pictureBoxPanel.Name = "pictureBoxPanel";
			this.pictureBoxPanel.TabStop = false;
			// 
			// frmStartupMan
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listviewStartup);
			this.Controls.Add(this.panelDetails);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmStartupMan";
			this.panelDetails.ResumeLayout(false);
			this.panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPanel)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		System.Windows.Forms.Panel panelDetails;
		System.Windows.Forms.ListView listviewStartup;
		System.Windows.Forms.ColumnHeader ItemName;
		System.Windows.Forms.ColumnHeader FileName;
		System.Windows.Forms.ColumnHeader Type;
		System.Windows.Forms.ColumnHeader Status;
		System.Windows.Forms.PictureBox pictureBoxPanel;
		System.Windows.Forms.Label labelDetails;
		System.Windows.Forms.Label labelArguments;
		System.Windows.Forms.Label labelArgumentsDesc;
		System.Windows.Forms.Label labelProductNameDesc;
		System.Windows.Forms.Label labelProductName;
		System.Windows.Forms.Label labelCommandDesc;
		System.Windows.Forms.Label labelFileVersionDesc;
		System.Windows.Forms.Label labelDescriptionDesc;
		System.Windows.Forms.Label labelCompanyDesc;
		System.Windows.Forms.Label labelCommand;
		System.Windows.Forms.Label labelFileVersion;
		System.Windows.Forms.Label labelDescription;
		System.Windows.Forms.Label labelCompany;
		System.Windows.Forms.Label firstCover;
	}
}

