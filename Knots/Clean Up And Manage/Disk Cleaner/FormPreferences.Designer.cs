using System.Resources;
using System.Globalization;
using System.Threading;

namespace Disk_Cleaner
{
	partial class FormPreferences
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("Disk_Cleaner.Resources",
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.buttonGDef = new System.Windows.Forms.Button();
            this.buttonGRem = new System.Windows.Forms.Button();
            this.buttonGAdd = new System.Windows.Forms.Button();
            this.lblLeave = new System.Windows.Forms.Label();
            this.listViewGeneral = new System.Windows.Forms.ListView();
            this.clhFileType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblReviewList = new System.Windows.Forms.Label();
            this.tabPageExclude = new System.Windows.Forms.TabPage();
            this.buttonERem = new System.Windows.Forms.Button();
            this.buttonEAdd = new System.Windows.Forms.Button();
            this.listViewExclude = new System.Windows.Forms.ListView();
            this.clhFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblJunk = new System.Windows.Forms.Label();
            this.tabPageInclude = new System.Windows.Forms.TabPage();
            this.buttonIRem = new System.Windows.Forms.Button();
            this.buttonIAdd = new System.Windows.Forms.Button();
            this.listViewInclude = new System.Windows.Forms.ListView();
            this.clhDirectory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblFiles = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.lblWaste = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageExclude.SuspendLayout();
            this.tabPageInclude.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageGeneral);
            this.tabControl.Controls.Add(this.tabPageExclude);
            this.tabControl.Controls.Add(this.tabPageInclude);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(520, 345);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.lblWaste);
            this.tabPageGeneral.Controls.Add(this.buttonGDef);
            this.tabPageGeneral.Controls.Add(this.buttonGRem);
            this.tabPageGeneral.Controls.Add(this.buttonGAdd);
            this.tabPageGeneral.Controls.Add(this.lblLeave);
            this.tabPageGeneral.Controls.Add(this.listViewGeneral);
            this.tabPageGeneral.Controls.Add(this.lblReviewList);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(512, 319);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // buttonGDef
            // 
            this.buttonGDef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGDef.Location = new System.Drawing.Point(420, 281);
            this.buttonGDef.Name = "buttonGDef";
            this.buttonGDef.Size = new System.Drawing.Size(75, 23);
            this.buttonGDef.TabIndex = 5;
            this.buttonGDef.Text = "Default";
            this.buttonGDef.UseVisualStyleBackColor = true;
            this.buttonGDef.Click += new System.EventHandler(this.buttonGDef_Click);
            // 
            // buttonGRem
            // 
            this.buttonGRem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGRem.Location = new System.Drawing.Point(105, 281);
            this.buttonGRem.Name = "buttonGRem";
            this.buttonGRem.Size = new System.Drawing.Size(75, 23);
            this.buttonGRem.TabIndex = 4;
            this.buttonGRem.Text = "remove";
            this.buttonGRem.UseVisualStyleBackColor = true;
            this.buttonGRem.Click += new System.EventHandler(this.buttonGRem_Click);
            // 
            // buttonGAdd
            // 
            this.buttonGAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGAdd.Location = new System.Drawing.Point(15, 281);
            this.buttonGAdd.Name = "buttonGAdd";
            this.buttonGAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonGAdd.TabIndex = 3;
            this.buttonGAdd.Text = "Add";
            this.buttonGAdd.UseVisualStyleBackColor = true;
            this.buttonGAdd.Click += new System.EventHandler(this.buttonGAdd_Click);
            // 
            // lblLeave
            // 
            this.lblLeave.AutoSize = true;
            this.lblLeave.Location = new System.Drawing.Point(12, 259);
            this.lblLeave.Name = "lblLeave";
            this.lblLeave.Size = new System.Drawing.Size(374, 13);
            this.lblLeave.TabIndex = 2;
            this.lblLeave.Text = "If you are unsure of these options, it is recommended to leave them as default.";
            // 
            // listViewGeneral
            // 
            this.listViewGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewGeneral.CheckBoxes = true;
            this.listViewGeneral.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhFileType,
            this.clhDescription});
            this.listViewGeneral.HideSelection = false;
            this.listViewGeneral.LabelEdit = true;
            this.listViewGeneral.Location = new System.Drawing.Point(15, 55);
            this.listViewGeneral.Name = "listViewGeneral";
            this.listViewGeneral.Size = new System.Drawing.Size(480, 186);
            this.listViewGeneral.TabIndex = 1;
            this.listViewGeneral.UseCompatibleStateImageBehavior = false;
            this.listViewGeneral.View = System.Windows.Forms.View.Details;
            this.listViewGeneral.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listViewGeneral_AfterLabelEdit);
            this.listViewGeneral.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewGeneral_ItemChecked);
            // 
            // clhFileType
            // 
            this.clhFileType.Text = "File Type";
            this.clhFileType.Width = 150;
            // 
            // clhDescription
            // 
            this.clhDescription.Text = "Description";
            this.clhDescription.Width = 250;
            // 
            // lblReviewList
            // 
            this.lblReviewList.Location = new System.Drawing.Point(12, 12);
            this.lblReviewList.Name = "lblReviewList";
            this.lblReviewList.Size = new System.Drawing.Size(483, 60);
            this.lblReviewList.TabIndex = 0;
            this.lblReviewList.Text = "Review the list of junk file types below and select or unselect items to include " +
    "in the search by checking or unchecking their corresponding boxes.";
            // 
            // tabPageExclude
            // 
            this.tabPageExclude.Controls.Add(this.buttonERem);
            this.tabPageExclude.Controls.Add(this.buttonEAdd);
            this.tabPageExclude.Controls.Add(this.listViewExclude);
            this.tabPageExclude.Controls.Add(this.lblJunk);
            this.tabPageExclude.Location = new System.Drawing.Point(4, 22);
            this.tabPageExclude.Name = "tabPageExclude";
            this.tabPageExclude.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExclude.Size = new System.Drawing.Size(512, 319);
            this.tabPageExclude.TabIndex = 1;
            this.tabPageExclude.Text = "Exclude";
            this.tabPageExclude.UseVisualStyleBackColor = true;
            // 
            // buttonERem
            // 
            this.buttonERem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonERem.Location = new System.Drawing.Point(105, 281);
            this.buttonERem.Name = "buttonERem";
            this.buttonERem.Size = new System.Drawing.Size(75, 23);
            this.buttonERem.TabIndex = 9;
            this.buttonERem.Text = "Remove";
            this.buttonERem.UseVisualStyleBackColor = true;
            this.buttonERem.Click += new System.EventHandler(this.buttonERem_Click);
            // 
            // buttonEAdd
            // 
            this.buttonEAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEAdd.Location = new System.Drawing.Point(15, 281);
            this.buttonEAdd.Name = "buttonEAdd";
            this.buttonEAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonEAdd.TabIndex = 8;
            this.buttonEAdd.Text = "Add";
            this.buttonEAdd.UseVisualStyleBackColor = true;
            this.buttonEAdd.Click += new System.EventHandler(this.buttonEAdd_Click);
            // 
            // listViewExclude
            // 
            this.listViewExclude.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewExclude.CheckBoxes = true;
            this.listViewExclude.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhFolder,
            this.clhDesc});
            this.listViewExclude.HideSelection = false;
            this.listViewExclude.LabelEdit = true;
            this.listViewExclude.Location = new System.Drawing.Point(15, 29);
            this.listViewExclude.Name = "listViewExclude";
            this.listViewExclude.Size = new System.Drawing.Size(480, 234);
            this.listViewExclude.TabIndex = 6;
            this.listViewExclude.UseCompatibleStateImageBehavior = false;
            this.listViewExclude.View = System.Windows.Forms.View.Details;
            this.listViewExclude.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listViewExclude_AfterLabelEdit);
            this.listViewExclude.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewGeneral_ItemChecked);
            // 
            // clhFolder
            // 
            this.clhFolder.Text = "Folder";
            this.clhFolder.Width = 300;
            // 
            // clhDesc
            // 
            this.clhDesc.Text = "Description";
            this.clhDesc.Width = 150;
            // 
            // lblJunk
            // 
            this.lblJunk.AutoSize = true;
            this.lblJunk.Location = new System.Drawing.Point(12, 13);
            this.lblJunk.Name = "lblJunk";
            this.lblJunk.Size = new System.Drawing.Size(317, 13);
            this.lblJunk.TabIndex = 5;
            this.lblJunk.Text = "All of the files in these folders should never be considered as junk.";
            // 
            // tabPageInclude
            // 
            this.tabPageInclude.Controls.Add(this.buttonIRem);
            this.tabPageInclude.Controls.Add(this.buttonIAdd);
            this.tabPageInclude.Controls.Add(this.listViewInclude);
            this.tabPageInclude.Controls.Add(this.lblFiles);
            this.tabPageInclude.Location = new System.Drawing.Point(4, 22);
            this.tabPageInclude.Name = "tabPageInclude";
            this.tabPageInclude.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInclude.Size = new System.Drawing.Size(512, 293);
            this.tabPageInclude.TabIndex = 2;
            this.tabPageInclude.Text = "Include";
            this.tabPageInclude.UseVisualStyleBackColor = true;
            // 
            // buttonIRem
            // 
            this.buttonIRem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonIRem.Location = new System.Drawing.Point(105, 255);
            this.buttonIRem.Name = "buttonIRem";
            this.buttonIRem.Size = new System.Drawing.Size(75, 23);
            this.buttonIRem.TabIndex = 13;
            this.buttonIRem.Text = "Remove";
            this.buttonIRem.UseVisualStyleBackColor = true;
            this.buttonIRem.Click += new System.EventHandler(this.buttonIRem_Click);
            // 
            // buttonIAdd
            // 
            this.buttonIAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonIAdd.Location = new System.Drawing.Point(15, 255);
            this.buttonIAdd.Name = "buttonIAdd";
            this.buttonIAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonIAdd.TabIndex = 12;
            this.buttonIAdd.Text = "Add";
            this.buttonIAdd.UseVisualStyleBackColor = true;
            this.buttonIAdd.Click += new System.EventHandler(this.buttonIAdd_Click);
            // 
            // listViewInclude
            // 
            this.listViewInclude.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewInclude.CheckBoxes = true;
            this.listViewInclude.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhDirectory,
            this.clhInfo});
            this.listViewInclude.HideSelection = false;
            this.listViewInclude.LabelEdit = true;
            this.listViewInclude.Location = new System.Drawing.Point(15, 29);
            this.listViewInclude.Name = "listViewInclude";
            this.listViewInclude.Size = new System.Drawing.Size(480, 208);
            this.listViewInclude.TabIndex = 11;
            this.listViewInclude.UseCompatibleStateImageBehavior = false;
            this.listViewInclude.View = System.Windows.Forms.View.Details;
            this.listViewInclude.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listViewInclude_AfterLabelEdit);
            this.listViewInclude.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewGeneral_ItemChecked);
            // 
            // clhDirectory
            // 
            this.clhDirectory.Text = "Folder";
            this.clhDirectory.Width = 300;
            // 
            // clhInfo
            // 
            this.clhInfo.Text = "Description";
            this.clhInfo.Width = 150;
            // 
            // lblFiles
            // 
            this.lblFiles.AutoSize = true;
            this.lblFiles.Location = new System.Drawing.Point(12, 13);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(411, 13);
            this.lblFiles.TabIndex = 10;
            this.lblFiles.Text = "All of the files in these folders should always be considered as junk, regardless" +
    " of type.";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(273, 363);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(363, 363);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonHelp
            // 
            this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHelp.Location = new System.Drawing.Point(453, 363);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(75, 23);
            this.buttonHelp.TabIndex = 3;
            this.buttonHelp.Text = "Help";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // lblWaste
            // 
            this.lblWaste.AutoSize = true;
            this.lblWaste.Location = new System.Drawing.Point(12, 244);
            this.lblWaste.Name = "lblWaste";
            this.lblWaste.Size = new System.Drawing.Size(334, 13);
            this.lblWaste.TabIndex = 6;
            this.lblWaste.Text = "Files inside the Windows temp folder are always considered as waste.";
            // 
            // FormPreferences
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(544, 398);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPreferences";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.FormPreferences_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.tabPageExclude.ResumeLayout(false);
            this.tabPageExclude.PerformLayout();
            this.tabPageInclude.ResumeLayout(false);
            this.tabPageInclude.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		System.Windows.Forms.TabControl tabControl;
		System.Windows.Forms.TabPage tabPageGeneral;
		System.Windows.Forms.TabPage tabPageExclude;
		System.Windows.Forms.TabPage tabPageInclude;
		System.Windows.Forms.Button buttonOK;
		System.Windows.Forms.Button buttonCancel;
		System.Windows.Forms.Button buttonHelp;
		System.Windows.Forms.Button buttonGDef;
		System.Windows.Forms.Button buttonGRem;
		System.Windows.Forms.Button buttonGAdd;
		System.Windows.Forms.Label lblLeave;
		System.Windows.Forms.ColumnHeader clhFileType;
		System.Windows.Forms.ColumnHeader clhDescription;
		System.Windows.Forms.Label lblReviewList;
		System.Windows.Forms.Button buttonERem;
		System.Windows.Forms.Button buttonEAdd;
		System.Windows.Forms.ColumnHeader clhFolder;
		System.Windows.Forms.ColumnHeader clhDesc;
		System.Windows.Forms.Label lblJunk;
		System.Windows.Forms.Button buttonIRem;
		System.Windows.Forms.Button buttonIAdd;
		System.Windows.Forms.ColumnHeader clhDirectory;
		System.Windows.Forms.ColumnHeader clhInfo;
		System.Windows.Forms.Label lblFiles;
		public System.Windows.Forms.ListView listViewGeneral;
		public System.Windows.Forms.ListView listViewExclude;
		public System.Windows.Forms.ListView listViewInclude;
        private System.Windows.Forms.Label lblWaste;
	}
}