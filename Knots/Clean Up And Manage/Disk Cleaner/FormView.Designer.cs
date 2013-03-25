using System.Resources;
using System.Globalization;
using System.Threading;

namespace Disk_Cleaner
{
	partial class frmView
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
            this.lblSelectPath = new System.Windows.Forms.Label();
            this.lvMain = new System.Windows.Forms.ListView();
            this.clhName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grbMain = new System.Windows.Forms.GroupBox();
            this.labelAccess = new System.Windows.Forms.Label();
            this.labelModified = new System.Windows.Forms.Label();
            this.labelCreated = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.labelExt = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.grbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSelectPath
            // 
            this.lblSelectPath.AutoSize = true;
            this.lblSelectPath.Location = new System.Drawing.Point(0, 13);
            this.lblSelectPath.Name = "lblSelectPath";
            this.lblSelectPath.Size = new System.Drawing.Size(184, 13);
            this.lblSelectPath.TabIndex = 1;
            this.lblSelectPath.Text = "Please select files you want to delete.";
            // 
            // lvMain
            // 
            this.lvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMain.CheckBoxes = true;
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhName,
            this.clhType,
            this.clhPath,
            this.clhSize});
            this.lvMain.FullRowSelect = true;
            this.lvMain.HideSelection = false;
            this.lvMain.Location = new System.Drawing.Point(3, 38);
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(635, 223);
            this.lvMain.TabIndex = 2;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            this.lvMain.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMain_ColumnClick);
            this.lvMain.SelectedIndexChanged += new System.EventHandler(this.lvMain_SelectedIndexChanged);
            // 
            // clhName
            // 
            this.clhName.Text = "Name";
            this.clhName.Width = 150;
            // 
            // clhPath
            // 
            this.clhPath.Text = "Path";
            this.clhPath.Width = 300;
            // 
            // clhSize
            // 
            this.clhSize.Text = "Size";
            this.clhSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clhSize.Width = 100;
            // 
            // clhType
            // 
            this.clhType.Text = "File Type";
            this.clhType.Width = 76;
            // 
            // grbMain
            // 
            this.grbMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grbMain.Controls.Add(this.labelAccess);
            this.grbMain.Controls.Add(this.labelModified);
            this.grbMain.Controls.Add(this.labelCreated);
            this.grbMain.Controls.Add(this.labelSize);
            this.grbMain.Controls.Add(this.labelExt);
            this.grbMain.Controls.Add(this.labelName);
            this.grbMain.Location = new System.Drawing.Point(3, 266);
            this.grbMain.Name = "grbMain";
            this.grbMain.Size = new System.Drawing.Size(635, 93);
            this.grbMain.TabIndex = 3;
            this.grbMain.TabStop = false;
            this.grbMain.Text = "Details";
            // 
            // labelAccess
            // 
            this.labelAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAccess.Location = new System.Drawing.Point(376, 69);
            this.labelAccess.Name = "labelAccess";
            this.labelAccess.Size = new System.Drawing.Size(250, 13);
            this.labelAccess.TabIndex = 5;
            this.labelAccess.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelModified
            // 
            this.labelModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelModified.Location = new System.Drawing.Point(376, 47);
            this.labelModified.Name = "labelModified";
            this.labelModified.Size = new System.Drawing.Size(250, 13);
            this.labelModified.TabIndex = 4;
            this.labelModified.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelCreated
            // 
            this.labelCreated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCreated.Location = new System.Drawing.Point(376, 25);
            this.labelCreated.Name = "labelCreated";
            this.labelCreated.Size = new System.Drawing.Size(250, 13);
            this.labelCreated.TabIndex = 3;
            this.labelCreated.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(14, 69);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(0, 13);
            this.labelSize.TabIndex = 2;
            // 
            // labelExt
            // 
            this.labelExt.AutoSize = true;
            this.labelExt.Location = new System.Drawing.Point(14, 47);
            this.labelExt.Name = "labelExt";
            this.labelExt.Size = new System.Drawing.Size(0, 13);
            this.labelExt.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(14, 25);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(0, 13);
            this.labelName.TabIndex = 0;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(473, 366);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(554, 366);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // frmView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(641, 401);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.grbMain);
            this.Controls.Add(this.lvMain);
            this.Controls.Add(this.lblSelectPath);
            this.Name = "frmView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormView_FormClosing);
            this.Load += new System.EventHandler(this.FormView_Load);
            this.grbMain.ResumeLayout(false);
            this.grbMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		System.Windows.Forms.Label lblSelectPath;
		System.Windows.Forms.GroupBox grbMain;
		System.Windows.Forms.Button buttonOK;
		System.Windows.Forms.Button buttonCancel;
		public System.Windows.Forms.ListView lvMain;
		System.Windows.Forms.ColumnHeader clhName;
		System.Windows.Forms.ColumnHeader clhPath;
		System.Windows.Forms.ColumnHeader clhSize;
		System.Windows.Forms.ColumnHeader clhType;
		System.Windows.Forms.Label labelAccess;
		System.Windows.Forms.Label labelModified;
		System.Windows.Forms.Label labelCreated;
		System.Windows.Forms.Label labelSize;
		System.Windows.Forms.Label labelExt;
		System.Windows.Forms.Label labelName;
	}
}