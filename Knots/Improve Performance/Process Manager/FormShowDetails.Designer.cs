using System.Threading;
using System.Resources;
using System.Globalization;

namespace ProcessManager
{
	partial class FormShowDetails
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("ProcessManager.Resources",
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShowDetails));
			this.tcMain = new System.Windows.Forms.TabControl();
			this.tbpGeneral = new System.Windows.Forms.TabPage();
			this.lvGeneral = new System.Windows.Forms.ListView();
			this.columnData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imlMain = new System.Windows.Forms.ImageList(this.components);
			this.tbpModuleInfo = new System.Windows.Forms.TabPage();
			this.lvModuleInfo = new System.Windows.Forms.ListView();
			this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnExecutable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.spcMain = new System.Windows.Forms.SplitContainer();
			this.btnClose = new System.Windows.Forms.Button();
			this.tcMain.SuspendLayout();
			this.tbpGeneral.SuspendLayout();
			this.tbpModuleInfo.SuspendLayout();
			this.spcMain.Panel1.SuspendLayout();
			this.spcMain.Panel2.SuspendLayout();
			this.spcMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// tcMain
			// 
			this.tcMain.Controls.Add(this.tbpGeneral);
			this.tcMain.Controls.Add(this.tbpModuleInfo);
			this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcMain.Location = new System.Drawing.Point(0, 0);
			this.tcMain.Name = "tcMain";
			this.tcMain.SelectedIndex = 0;
			this.tcMain.Size = new System.Drawing.Size(504, 373);
			this.tcMain.TabIndex = 0;
			// 
			// tbpGeneral
			// 
			this.tbpGeneral.Controls.Add(this.lvGeneral);
			this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
			this.tbpGeneral.Name = "tbpGeneral";
			this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tbpGeneral.Size = new System.Drawing.Size(496, 347);
			this.tbpGeneral.TabIndex = 0;
			this.tbpGeneral.Text = "General Information";
			this.tbpGeneral.UseVisualStyleBackColor = true;
			// 
			// lvGeneral
			// 
			this.lvGeneral.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnData,
            this.columnValue});
			this.lvGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvGeneral.Location = new System.Drawing.Point(3, 3);
			this.lvGeneral.Name = "lvGeneral";
			this.lvGeneral.Size = new System.Drawing.Size(490, 341);
			this.lvGeneral.SmallImageList = this.imlMain;
			this.lvGeneral.TabIndex = 0;
			this.lvGeneral.UseCompatibleStateImageBehavior = false;
			this.lvGeneral.View = System.Windows.Forms.View.Details;
			// 
			// columnData
			// 
			this.columnData.Text = "Data";
			this.columnData.Width = 140;
			// 
			// columnValue
			// 
			this.columnValue.Text = "Value";
			this.columnValue.Width = 336;
			// 
			// imlMain
			// 
			this.imlMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlMain.ImageStream")));
			this.imlMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imlMain.Images.SetKeyName(0, "icon1.PNG");
			this.imlMain.Images.SetKeyName(1, "icon2.PNG");
			// 
			// tbpModuleInfo
			// 
			this.tbpModuleInfo.Controls.Add(this.lvModuleInfo);
			this.tbpModuleInfo.Location = new System.Drawing.Point(4, 22);
			this.tbpModuleInfo.Name = "tbpModuleInfo";
			this.tbpModuleInfo.Padding = new System.Windows.Forms.Padding(3);
			this.tbpModuleInfo.Size = new System.Drawing.Size(496, 347);
			this.tbpModuleInfo.TabIndex = 1;
			this.tbpModuleInfo.Text = "Module Information";
			this.tbpModuleInfo.UseVisualStyleBackColor = true;
			// 
			// lvModuleInfo
			// 
			this.lvModuleInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnExecutable});
			this.lvModuleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvModuleInfo.Location = new System.Drawing.Point(3, 3);
			this.lvModuleInfo.Name = "lvModuleInfo";
			this.lvModuleInfo.Size = new System.Drawing.Size(490, 341);
			this.lvModuleInfo.TabIndex = 0;
			this.lvModuleInfo.UseCompatibleStateImageBehavior = false;
			this.lvModuleInfo.View = System.Windows.Forms.View.Details;
			// 
			// columnName
			// 
			this.columnName.Text = "Name";
			this.columnName.Width = 165;
			// 
			// columnExecutable
			// 
			this.columnExecutable.Text = "Executable";
			this.columnExecutable.Width = 260;
			// 
			// spcMain
			// 
			this.spcMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spcMain.IsSplitterFixed = true;
			this.spcMain.Location = new System.Drawing.Point(0, 0);
			this.spcMain.Name = "spcMain";
			this.spcMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spcMain.Panel1
			// 
			this.spcMain.Panel1.Controls.Add(this.tcMain);
			// 
			// spcMain.Panel2
			// 
			this.spcMain.Panel2.Controls.Add(this.btnClose);
			this.spcMain.Size = new System.Drawing.Size(504, 403);
			this.spcMain.SplitterDistance = 373;
			this.spcMain.TabIndex = 0;
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(426, -1);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// FormShowDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(504, 403);
			this.Controls.Add(this.spcMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "FormShowDetails";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Show Detail";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.FrmShowDetails_Load);
			this.tcMain.ResumeLayout(false);
			this.tbpGeneral.ResumeLayout(false);
			this.tbpModuleInfo.ResumeLayout(false);
			this.spcMain.Panel1.ResumeLayout(false);
			this.spcMain.Panel2.ResumeLayout(false);
			this.spcMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		System.Windows.Forms.TabControl tcMain;
		System.Windows.Forms.TabPage tbpGeneral;
		System.Windows.Forms.ListView lvGeneral;
		System.Windows.Forms.ColumnHeader columnData;
		System.Windows.Forms.ColumnHeader columnValue;
		System.Windows.Forms.TabPage tbpModuleInfo;
		System.Windows.Forms.SplitContainer spcMain;
		System.Windows.Forms.Button btnClose;
		System.Windows.Forms.ListView lvModuleInfo;
		System.Windows.Forms.ColumnHeader columnName;
		System.Windows.Forms.ColumnHeader columnExecutable;
		System.Windows.Forms.ImageList imlMain;

	}
}