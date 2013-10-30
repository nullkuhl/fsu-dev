using System.Resources;
using System.Globalization;
using System.Threading;

namespace FreemiumUtilities.TracksEraser
{
	partial class frmTrackOptions
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("Resources",
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrackOptions));
			this.spcMain = new System.Windows.Forms.SplitContainer();
			this.tbcMain = new System.Windows.Forms.TabControl();
			this.tbpIE = new System.Windows.Forms.TabPage();
			this.btnIECheckInvert = new System.Windows.Forms.Button();
			this.btnIECheckNone = new System.Windows.Forms.Button();
			this.btnIECheckAll = new System.Windows.Forms.Button();
			this.btnDeleteCookies = new System.Windows.Forms.Button();
			this.lsvIECookies = new System.Windows.Forms.ListView();
			this.Websites = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lblIECookies = new System.Windows.Forms.Label();
			this.tbpFF = new System.Windows.Forms.TabPage();
			this.btnFFCheckInvert = new System.Windows.Forms.Button();
			this.btnFFCheckNone = new System.Windows.Forms.Button();
			this.btnFFCheckAll = new System.Windows.Forms.Button();
			this.lblFFCookies = new System.Windows.Forms.Label();
			this.btnDelFFCookies = new System.Windows.Forms.Button();
			this.ListViewFF = new System.Windows.Forms.ListView();
			this.clhCookies = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tbpChrome = new System.Windows.Forms.TabPage();
			this.btnChrCheckInvert = new System.Windows.Forms.Button();
			this.btnChrCheckNone = new System.Windows.Forms.Button();
			this.btnChrCheckAll = new System.Windows.Forms.Button();
			this.lblChromeCookies = new System.Windows.Forms.Label();
			this.btnDelChromeCookies = new System.Windows.Forms.Button();
			this.ListViewChrome = new System.Windows.Forms.ListView();
			this.clhCookiesTemp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tbpIEURLs = new System.Windows.Forms.TabPage();
			this.btnUrlCheckInvert = new System.Windows.Forms.Button();
			this.btnUrlCheckNone = new System.Windows.Forms.Button();
			this.btnUrlCheckAll = new System.Windows.Forms.Button();
			this.btnDeleteCheckedURLs = new System.Windows.Forms.Button();
			this.lvIETypedURLs = new System.Windows.Forms.ListView();
			this.URLs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lblCheck = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.spcMain.Panel1.SuspendLayout();
			this.spcMain.Panel2.SuspendLayout();
			this.spcMain.SuspendLayout();
			this.tbcMain.SuspendLayout();
			this.tbpIE.SuspendLayout();
			this.tbpFF.SuspendLayout();
			this.tbpChrome.SuspendLayout();
			this.tbpIEURLs.SuspendLayout();
			this.SuspendLayout();
			// 
			// spcMain
			// 
			this.spcMain.Location = new System.Drawing.Point(0, 0);
			this.spcMain.Name = "spcMain";
			this.spcMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spcMain.Panel1
			// 
			this.spcMain.Panel1.Controls.Add(this.tbcMain);
			this.spcMain.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			// 
			// spcMain.Panel2
			// 
			this.spcMain.Panel2.Controls.Add(this.btnCancel);
			this.spcMain.Panel2.Controls.Add(this.btnClose);
			this.spcMain.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.spcMain.Panel2Collapsed = true;
			this.spcMain.Panel2MinSize = 0;
			this.spcMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.spcMain.Size = new System.Drawing.Size(793, 405);
			this.spcMain.SplitterDistance = 400;
			this.spcMain.TabIndex = 0;
			// 
			// tbcMain
			// 
			this.tbcMain.Controls.Add(this.tbpIE);
			this.tbcMain.Controls.Add(this.tbpFF);
			this.tbcMain.Controls.Add(this.tbpChrome);
			this.tbcMain.Controls.Add(this.tbpIEURLs);
			this.tbcMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbcMain.Location = new System.Drawing.Point(0, 0);
			this.tbcMain.Name = "tbcMain";
			this.tbcMain.SelectedIndex = 0;
			this.tbcMain.Size = new System.Drawing.Size(793, 405);
			this.tbcMain.TabIndex = 0;
			this.tbcMain.SelectedIndexChanged += new System.EventHandler(this.tbcMain_SelectedIndexChanged);
			// 
			// tbpIE
			// 
			this.tbpIE.Controls.Add(this.btnIECheckInvert);
			this.tbpIE.Controls.Add(this.btnIECheckNone);
			this.tbpIE.Controls.Add(this.btnIECheckAll);
			this.tbpIE.Controls.Add(this.btnDeleteCookies);
			this.tbpIE.Controls.Add(this.lsvIECookies);
			this.tbpIE.Controls.Add(this.lblIECookies);
			this.tbpIE.Location = new System.Drawing.Point(4, 22);
			this.tbpIE.Name = "tbpIE";
			this.tbpIE.Padding = new System.Windows.Forms.Padding(3);
			this.tbpIE.Size = new System.Drawing.Size(785, 379);
			this.tbpIE.TabIndex = 0;
			this.tbpIE.Text = "IE Cookie Manager";
			this.tbpIE.UseVisualStyleBackColor = true;
			// 
			// btnIECheckInvert
			// 
			this.btnIECheckInvert.Location = new System.Drawing.Point(249, 342);
			this.btnIECheckInvert.Name = "btnIECheckInvert";
			this.btnIECheckInvert.Size = new System.Drawing.Size(110, 30);
			this.btnIECheckInvert.TabIndex = 10;
			this.btnIECheckInvert.Text = "Check Invert";
			this.btnIECheckInvert.UseVisualStyleBackColor = true;
			this.btnIECheckInvert.Click += new System.EventHandler(this.btnCheckInvert_Click);
			// 
			// btnIECheckNone
			// 
			this.btnIECheckNone.Location = new System.Drawing.Point(130, 342);
			this.btnIECheckNone.Name = "btnIECheckNone";
			this.btnIECheckNone.Size = new System.Drawing.Size(110, 30);
			this.btnIECheckNone.TabIndex = 9;
			this.btnIECheckNone.Text = "Check None";
			this.btnIECheckNone.UseVisualStyleBackColor = true;
			this.btnIECheckNone.Click += new System.EventHandler(this.btnCheckNone_Click);
			// 
			// btnIECheckAll
			// 
			this.btnIECheckAll.Location = new System.Drawing.Point(12, 342);
			this.btnIECheckAll.Name = "btnIECheckAll";
			this.btnIECheckAll.Size = new System.Drawing.Size(110, 30);
			this.btnIECheckAll.TabIndex = 8;
			this.btnIECheckAll.Text = "Check All";
			this.btnIECheckAll.UseVisualStyleBackColor = true;
			this.btnIECheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
			// 
			// btnDeleteCookies
			// 
			this.btnDeleteCookies.Location = new System.Drawing.Point(582, 342);
			this.btnDeleteCookies.Name = "btnDeleteCookies";
			this.btnDeleteCookies.Size = new System.Drawing.Size(199, 30);
			this.btnDeleteCookies.TabIndex = 2;
			this.btnDeleteCookies.Text = "Delete Checked Cookies Now";
			this.btnDeleteCookies.UseVisualStyleBackColor = true;
			this.btnDeleteCookies.Click += new System.EventHandler(this.btnDeleteCookies_Click);
			// 
			// lsvIECookies
			// 
			this.lsvIECookies.CheckBoxes = true;
			this.lsvIECookies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.Websites});
			this.lsvIECookies.Location = new System.Drawing.Point(12, 60);
			this.lsvIECookies.Name = "lsvIECookies";
			this.lsvIECookies.Size = new System.Drawing.Size(769, 276);
			this.lsvIECookies.TabIndex = 1;
			this.lsvIECookies.UseCompatibleStateImageBehavior = false;
			this.lsvIECookies.View = System.Windows.Forms.View.Details;
			// 
			// Websites
			// 
			this.Websites.Text = "Cookies";
			this.Websites.Width = 646;
			// 
			// lblIECookies
			// 
			this.lblIECookies.AutoSize = true;
			this.lblIECookies.Location = new System.Drawing.Point(10, 18);
			this.lblIECookies.Name = "lblIECookies";
			this.lblIECookies.Size = new System.Drawing.Size(397, 13);
			this.lblIECookies.TabIndex = 0;
			this.lblIECookies.Text = "Check mark the cookies you want to keep when cleaning internet explorer cookies";
			// 
			// tbpFF
			// 
			this.tbpFF.Controls.Add(this.btnFFCheckInvert);
			this.tbpFF.Controls.Add(this.btnFFCheckNone);
			this.tbpFF.Controls.Add(this.btnFFCheckAll);
			this.tbpFF.Controls.Add(this.lblFFCookies);
			this.tbpFF.Controls.Add(this.btnDelFFCookies);
			this.tbpFF.Controls.Add(this.ListViewFF);
			this.tbpFF.Location = new System.Drawing.Point(4, 22);
			this.tbpFF.Name = "tbpFF";
			this.tbpFF.Size = new System.Drawing.Size(785, 379);
			this.tbpFF.TabIndex = 2;
			this.tbpFF.Text = "FF Cookie Manager";
			this.tbpFF.UseVisualStyleBackColor = true;
			// 
			// btnFFCheckInvert
			// 
			this.btnFFCheckInvert.Location = new System.Drawing.Point(249, 342);
			this.btnFFCheckInvert.Name = "btnFFCheckInvert";
			this.btnFFCheckInvert.Size = new System.Drawing.Size(110, 30);
			this.btnFFCheckInvert.TabIndex = 13;
			this.btnFFCheckInvert.Text = "Check Invert";
			this.btnFFCheckInvert.UseVisualStyleBackColor = true;
			this.btnFFCheckInvert.Click += new System.EventHandler(this.btnFFCheckInvert_Click);
			// 
			// btnFFCheckNone
			// 
			this.btnFFCheckNone.Location = new System.Drawing.Point(130, 342);
			this.btnFFCheckNone.Name = "btnFFCheckNone";
			this.btnFFCheckNone.Size = new System.Drawing.Size(110, 30);
			this.btnFFCheckNone.TabIndex = 12;
			this.btnFFCheckNone.Text = "Check None";
			this.btnFFCheckNone.UseVisualStyleBackColor = true;
			this.btnFFCheckNone.Click += new System.EventHandler(this.btnFFCheckNone_Click);
			// 
			// btnFFCheckAll
			// 
			this.btnFFCheckAll.Location = new System.Drawing.Point(12, 342);
			this.btnFFCheckAll.Name = "btnFFCheckAll";
			this.btnFFCheckAll.Size = new System.Drawing.Size(110, 30);
			this.btnFFCheckAll.TabIndex = 11;
			this.btnFFCheckAll.Text = "Check All";
			this.btnFFCheckAll.UseVisualStyleBackColor = true;
			this.btnFFCheckAll.Click += new System.EventHandler(this.btnFFCheckAll_Click);
			// 
			// lblFFCookies
			// 
			this.lblFFCookies.AutoSize = true;
			this.lblFFCookies.Location = new System.Drawing.Point(10, 18);
			this.lblFFCookies.Name = "lblFFCookies";
			this.lblFFCookies.Size = new System.Drawing.Size(364, 13);
			this.lblFFCookies.TabIndex = 4;
			this.lblFFCookies.Text = "Check mark the cookies you want to keep when cleaning in Firefox cookies";
			// 
			// btnDelFFCookies
			// 
			this.btnDelFFCookies.Location = new System.Drawing.Point(582, 342);
			this.btnDelFFCookies.Name = "btnDelFFCookies";
			this.btnDelFFCookies.Size = new System.Drawing.Size(199, 30);
			this.btnDelFFCookies.TabIndex = 3;
			this.btnDelFFCookies.Text = "Delete Checked Cookies Now";
			this.btnDelFFCookies.UseVisualStyleBackColor = true;
			this.btnDelFFCookies.Click += new System.EventHandler(this.btnDelFFCookies_Click);
			// 
			// ListViewFF
			// 
			this.ListViewFF.CheckBoxes = true;
			this.ListViewFF.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.clhCookies});
			this.ListViewFF.Location = new System.Drawing.Point(12, 60);
			this.ListViewFF.Name = "ListViewFF";
			this.ListViewFF.Size = new System.Drawing.Size(769, 276);
			this.ListViewFF.TabIndex = 2;
			this.ListViewFF.UseCompatibleStateImageBehavior = false;
			this.ListViewFF.View = System.Windows.Forms.View.Details;
			// 
			// clhCookies
			// 
			this.clhCookies.Text = "Cookies";
			this.clhCookies.Width = 646;
			// 
			// tbpChrome
			// 
			this.tbpChrome.Controls.Add(this.btnChrCheckInvert);
			this.tbpChrome.Controls.Add(this.btnChrCheckNone);
			this.tbpChrome.Controls.Add(this.btnChrCheckAll);
			this.tbpChrome.Controls.Add(this.lblChromeCookies);
			this.tbpChrome.Controls.Add(this.btnDelChromeCookies);
			this.tbpChrome.Controls.Add(this.ListViewChrome);
			this.tbpChrome.Location = new System.Drawing.Point(4, 22);
			this.tbpChrome.Name = "tbpChrome";
			this.tbpChrome.Size = new System.Drawing.Size(785, 379);
			this.tbpChrome.TabIndex = 3;
			this.tbpChrome.Text = "Google Chrome - Cookies";
			this.tbpChrome.UseVisualStyleBackColor = true;
			// 
			// btnChrCheckInvert
			// 
			this.btnChrCheckInvert.Location = new System.Drawing.Point(249, 342);
			this.btnChrCheckInvert.Name = "btnChrCheckInvert";
			this.btnChrCheckInvert.Size = new System.Drawing.Size(110, 30);
			this.btnChrCheckInvert.TabIndex = 13;
			this.btnChrCheckInvert.Text = "Check Invert";
			this.btnChrCheckInvert.UseVisualStyleBackColor = true;
			this.btnChrCheckInvert.Click += new System.EventHandler(this.btnChrCheckInvert_Click);
			// 
			// btnChrCheckNone
			// 
			this.btnChrCheckNone.Location = new System.Drawing.Point(130, 342);
			this.btnChrCheckNone.Name = "btnChrCheckNone";
			this.btnChrCheckNone.Size = new System.Drawing.Size(110, 30);
			this.btnChrCheckNone.TabIndex = 12;
			this.btnChrCheckNone.Text = "Check None";
			this.btnChrCheckNone.UseVisualStyleBackColor = true;
			this.btnChrCheckNone.Click += new System.EventHandler(this.btnChrCheckNone_Click);
			// 
			// btnChrCheckAll
			// 
			this.btnChrCheckAll.Location = new System.Drawing.Point(12, 342);
			this.btnChrCheckAll.Name = "btnChrCheckAll";
			this.btnChrCheckAll.Size = new System.Drawing.Size(110, 30);
			this.btnChrCheckAll.TabIndex = 11;
			this.btnChrCheckAll.Text = "Check All";
			this.btnChrCheckAll.UseVisualStyleBackColor = true;
			this.btnChrCheckAll.Click += new System.EventHandler(this.btnChrCheckAll_Click);
			// 
			// lblChromeCookies
			// 
			this.lblChromeCookies.AutoSize = true;
			this.lblChromeCookies.Location = new System.Drawing.Point(10, 18);
			this.lblChromeCookies.Name = "lblChromeCookies";
			this.lblChromeCookies.Size = new System.Drawing.Size(369, 13);
			this.lblChromeCookies.TabIndex = 7;
			this.lblChromeCookies.Text = "Check mark the cookies you want to keep when cleaning in Chrome cookies";
			// 
			// btnDelChromeCookies
			// 
			this.btnDelChromeCookies.Location = new System.Drawing.Point(582, 342);
			this.btnDelChromeCookies.Name = "btnDelChromeCookies";
			this.btnDelChromeCookies.Size = new System.Drawing.Size(199, 30);
			this.btnDelChromeCookies.TabIndex = 6;
			this.btnDelChromeCookies.Text = "Delete Checked Cookies Now";
			this.btnDelChromeCookies.UseVisualStyleBackColor = true;
			this.btnDelChromeCookies.Click += new System.EventHandler(this.btnDelChromeCookies_Click);
			// 
			// ListViewChrome
			// 
			this.ListViewChrome.CheckBoxes = true;
			this.ListViewChrome.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.clhCookiesTemp});
			this.ListViewChrome.Location = new System.Drawing.Point(12, 60);
			this.ListViewChrome.Name = "ListViewChrome";
			this.ListViewChrome.Size = new System.Drawing.Size(769, 276);
			this.ListViewChrome.TabIndex = 5;
			this.ListViewChrome.UseCompatibleStateImageBehavior = false;
			this.ListViewChrome.View = System.Windows.Forms.View.Details;
			// 
			// clhCookiesTemp
			// 
			this.clhCookiesTemp.Text = "Cookies";
			this.clhCookiesTemp.Width = 646;
			// 
			// tbpIEURLs
			// 
			this.tbpIEURLs.Controls.Add(this.btnUrlCheckInvert);
			this.tbpIEURLs.Controls.Add(this.btnUrlCheckNone);
			this.tbpIEURLs.Controls.Add(this.btnUrlCheckAll);
			this.tbpIEURLs.Controls.Add(this.btnDeleteCheckedURLs);
			this.tbpIEURLs.Controls.Add(this.lvIETypedURLs);
			this.tbpIEURLs.Controls.Add(this.lblCheck);
			this.tbpIEURLs.Location = new System.Drawing.Point(4, 22);
			this.tbpIEURLs.Name = "tbpIEURLs";
			this.tbpIEURLs.Padding = new System.Windows.Forms.Padding(3);
			this.tbpIEURLs.Size = new System.Drawing.Size(785, 379);
			this.tbpIEURLs.TabIndex = 1;
			this.tbpIEURLs.Text = "IE Typed URLs";
			this.tbpIEURLs.UseVisualStyleBackColor = true;
			// 
			// btnUrlCheckInvert
			// 
			this.btnUrlCheckInvert.Location = new System.Drawing.Point(249, 342);
			this.btnUrlCheckInvert.Name = "btnUrlCheckInvert";
			this.btnUrlCheckInvert.Size = new System.Drawing.Size(110, 30);
			this.btnUrlCheckInvert.TabIndex = 13;
			this.btnUrlCheckInvert.Text = "Check Invert";
			this.btnUrlCheckInvert.UseVisualStyleBackColor = true;
			this.btnUrlCheckInvert.Click += new System.EventHandler(this.btnUrlCheckInvert_Click);
			// 
			// btnUrlCheckNone
			// 
			this.btnUrlCheckNone.Location = new System.Drawing.Point(130, 342);
			this.btnUrlCheckNone.Name = "btnUrlCheckNone";
			this.btnUrlCheckNone.Size = new System.Drawing.Size(110, 30);
			this.btnUrlCheckNone.TabIndex = 12;
			this.btnUrlCheckNone.Text = "Check None";
			this.btnUrlCheckNone.UseVisualStyleBackColor = true;
			this.btnUrlCheckNone.Click += new System.EventHandler(this.btnUrlCheckNone_Click);
			// 
			// btnUrlCheckAll
			// 
			this.btnUrlCheckAll.Location = new System.Drawing.Point(12, 342);
			this.btnUrlCheckAll.Name = "btnUrlCheckAll";
			this.btnUrlCheckAll.Size = new System.Drawing.Size(110, 30);
			this.btnUrlCheckAll.TabIndex = 11;
			this.btnUrlCheckAll.Text = "Check All";
			this.btnUrlCheckAll.UseVisualStyleBackColor = true;
			this.btnUrlCheckAll.Click += new System.EventHandler(this.btnUrlCheckAll_Click);
			// 
			// btnDeleteCheckedURLs
			// 
			this.btnDeleteCheckedURLs.Location = new System.Drawing.Point(582, 342);
			this.btnDeleteCheckedURLs.Name = "btnDeleteCheckedURLs";
			this.btnDeleteCheckedURLs.Size = new System.Drawing.Size(199, 30);
			this.btnDeleteCheckedURLs.TabIndex = 2;
			this.btnDeleteCheckedURLs.Text = "Delete Checked URLs Now";
			this.btnDeleteCheckedURLs.UseVisualStyleBackColor = true;
			this.btnDeleteCheckedURLs.Click += new System.EventHandler(this.btnDeleteCheckedURLs_Click);
			// 
			// lvIETypedURLs
			// 
			this.lvIETypedURLs.CheckBoxes = true;
			this.lvIETypedURLs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.URLs});
			this.lvIETypedURLs.Location = new System.Drawing.Point(12, 60);
			this.lvIETypedURLs.Name = "lvIETypedURLs";
			this.lvIETypedURLs.Size = new System.Drawing.Size(769, 276);
			this.lvIETypedURLs.TabIndex = 1;
			this.lvIETypedURLs.UseCompatibleStateImageBehavior = false;
			this.lvIETypedURLs.View = System.Windows.Forms.View.Details;
			// 
			// URLs
			// 
			this.URLs.Text = "URLs";
			this.URLs.Width = 631;
			// 
			// lblCheck
			// 
			this.lblCheck.AutoSize = true;
			this.lblCheck.Location = new System.Drawing.Point(10, 18);
			this.lblCheck.Name = "lblCheck";
			this.lblCheck.Size = new System.Drawing.Size(421, 13);
			this.lblCheck.TabIndex = 0;
			this.lblCheck.Text = "Check mark the URLs you want to keep when you clean Internet Explorer - URL Histo" +
	"ry";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(710, 18);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(629, 18);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Ok";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// frmTrackOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(792, 402);
			this.Controls.Add(this.spcMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmTrackOptions";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Options";
			this.Load += new System.EventHandler(this.FrmOptions_Load);
			this.spcMain.Panel1.ResumeLayout(false);
			this.spcMain.Panel2.ResumeLayout(false);
			this.spcMain.ResumeLayout(false);
			this.tbcMain.ResumeLayout(false);
			this.tbpIE.ResumeLayout(false);
			this.tbpIE.PerformLayout();
			this.tbpFF.ResumeLayout(false);
			this.tbpFF.PerformLayout();
			this.tbpChrome.ResumeLayout(false);
			this.tbpChrome.PerformLayout();
			this.tbpIEURLs.ResumeLayout(false);
			this.tbpIEURLs.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		System.Windows.Forms.SplitContainer spcMain;
		System.Windows.Forms.Button btnCancel;
		System.Windows.Forms.Button btnClose;
		System.Windows.Forms.TabControl tbcMain;
		System.Windows.Forms.TabPage tbpIE;
		System.Windows.Forms.TabPage tbpIEURLs;
		System.Windows.Forms.Label lblIECookies;
		System.Windows.Forms.Button btnDeleteCookies;
		System.Windows.Forms.ListView lsvIECookies;
		System.Windows.Forms.Label lblCheck;
		System.Windows.Forms.ListView lvIETypedURLs;
		System.Windows.Forms.Button btnDeleteCheckedURLs;
		System.Windows.Forms.ColumnHeader Websites;
		System.Windows.Forms.ColumnHeader URLs;
		System.Windows.Forms.TabPage tbpFF;
		System.Windows.Forms.TabPage tbpChrome;
		System.Windows.Forms.Label lblFFCookies;
		System.Windows.Forms.Button btnDelFFCookies;
		System.Windows.Forms.ListView ListViewFF;
		System.Windows.Forms.ColumnHeader clhCookies;
		System.Windows.Forms.Label lblChromeCookies;
		System.Windows.Forms.Button btnDelChromeCookies;
		System.Windows.Forms.ListView ListViewChrome;
		System.Windows.Forms.ColumnHeader clhCookiesTemp;
		System.Windows.Forms.Button btnIECheckInvert;
		System.Windows.Forms.Button btnIECheckNone;
		System.Windows.Forms.Button btnIECheckAll;
		System.Windows.Forms.Button btnFFCheckInvert;
		System.Windows.Forms.Button btnFFCheckNone;
		System.Windows.Forms.Button btnFFCheckAll;
		System.Windows.Forms.Button btnChrCheckInvert;
		System.Windows.Forms.Button btnChrCheckNone;
		System.Windows.Forms.Button btnChrCheckAll;
		System.Windows.Forms.Button btnUrlCheckInvert;
		System.Windows.Forms.Button btnUrlCheckNone;
		System.Windows.Forms.Button btnUrlCheckAll;
	}
}