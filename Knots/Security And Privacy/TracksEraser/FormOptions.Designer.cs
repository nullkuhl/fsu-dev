using System.Resources;
using System.Globalization;
using System.Threading;

namespace FreemiumUtilities.TracksEraser
{
	partial class frmOptions
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("TracksEraser.Resources",
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
            this.spcMain = new System.Windows.Forms.SplitContainer();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tbpCookiesIE = new System.Windows.Forms.TabPage();
            this.btnIECheckInvert = new System.Windows.Forms.Button();
            this.btnIECheckNone = new System.Windows.Forms.Button();
            this.btnIECheckAll = new System.Windows.Forms.Button();
            this.btnDeleteIECookies = new System.Windows.Forms.Button();
            this.lvCookiesIE = new System.Windows.Forms.ListView();
            this.Websites = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblCheckIE = new System.Windows.Forms.Label();
            this.tbpCookiesFF = new System.Windows.Forms.TabPage();
            this.btnFFCheckInvert = new System.Windows.Forms.Button();
            this.btnFFCheckNone = new System.Windows.Forms.Button();
            this.btnFFCheckAll = new System.Windows.Forms.Button();
            this.lblCheckFF = new System.Windows.Forms.Label();
            this.btnDelFFCookies = new System.Windows.Forms.Button();
            this.ListViewFF = new System.Windows.Forms.ListView();
            this.clhCookies = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpCookiesChrome = new System.Windows.Forms.TabPage();
            this.btnChrCheckInvert = new System.Windows.Forms.Button();
            this.btnChrCheckNone = new System.Windows.Forms.Button();
            this.btnChrCheckAll = new System.Windows.Forms.Button();
            this.lblCheckChrome = new System.Windows.Forms.Label();
            this.btnDelChromeCookies = new System.Windows.Forms.Button();
            this.ListViewChrome = new System.Windows.Forms.ListView();
            this.clhCookiesAll = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpIEURLs = new System.Windows.Forms.TabPage();
            this.btnUrlCheckInvert = new System.Windows.Forms.Button();
            this.btnUrlCheckNone = new System.Windows.Forms.Button();
            this.btnUrlCheckAll = new System.Windows.Forms.Button();
            this.btnDeleteIEURLs = new System.Windows.Forms.Button();
            this.lvIEURLs = new System.Windows.Forms.ListView();
            this.URLs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblCheckIEURLs = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.spcMain.Panel1.SuspendLayout();
            this.spcMain.Panel2.SuspendLayout();
            this.spcMain.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tbpCookiesIE.SuspendLayout();
            this.tbpCookiesFF.SuspendLayout();
            this.tbpCookiesChrome.SuspendLayout();
            this.tbpIEURLs.SuspendLayout();
            this.SuspendLayout();
            // 
            // spcMain
            // 
            this.spcMain.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.spcMain.Panel2.Controls.Add(this.btnCancel);
            this.spcMain.Panel2.Controls.Add(this.btnOK);
            this.spcMain.Size = new System.Drawing.Size(797, 474);
            this.spcMain.SplitterDistance = 415;
            this.spcMain.TabIndex = 0;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tbpCookiesIE);
            this.tcMain.Controls.Add(this.tbpCookiesFF);
            this.tcMain.Controls.Add(this.tbpCookiesChrome);
            this.tcMain.Controls.Add(this.tbpIEURLs);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(797, 415);
            this.tcMain.TabIndex = 0;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tbpCookiesIE
            // 
            this.tbpCookiesIE.Controls.Add(this.btnIECheckInvert);
            this.tbpCookiesIE.Controls.Add(this.btnIECheckNone);
            this.tbpCookiesIE.Controls.Add(this.btnIECheckAll);
            this.tbpCookiesIE.Controls.Add(this.btnDeleteIECookies);
            this.tbpCookiesIE.Controls.Add(this.lvCookiesIE);
            this.tbpCookiesIE.Controls.Add(this.lblCheckIE);
            this.tbpCookiesIE.Location = new System.Drawing.Point(4, 22);
            this.tbpCookiesIE.Name = "tbpCookiesIE";
            this.tbpCookiesIE.Padding = new System.Windows.Forms.Padding(3);
            this.tbpCookiesIE.Size = new System.Drawing.Size(789, 389);
            this.tbpCookiesIE.TabIndex = 0;
            this.tbpCookiesIE.Text = "IE Cookie Manager";
            this.tbpCookiesIE.UseVisualStyleBackColor = true;
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
            // btnDeleteIECookies
            // 
            this.btnDeleteIECookies.Location = new System.Drawing.Point(535, 342);
            this.btnDeleteIECookies.Name = "btnDeleteIECookies";
            this.btnDeleteIECookies.Size = new System.Drawing.Size(246, 30);
            this.btnDeleteIECookies.TabIndex = 2;
            this.btnDeleteIECookies.Text = "Delete Checked Cookies Now";
            this.btnDeleteIECookies.UseVisualStyleBackColor = true;
            this.btnDeleteIECookies.Click += new System.EventHandler(this.btnDeleteIECookies_Click);
            // 
            // lvCookiesIE
            // 
            this.lvCookiesIE.CheckBoxes = true;
            this.lvCookiesIE.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Websites});
            this.lvCookiesIE.Location = new System.Drawing.Point(12, 60);
            this.lvCookiesIE.Name = "lvCookiesIE";
            this.lvCookiesIE.Size = new System.Drawing.Size(769, 276);
            this.lvCookiesIE.TabIndex = 1;
            this.lvCookiesIE.UseCompatibleStateImageBehavior = false;
            this.lvCookiesIE.View = System.Windows.Forms.View.Details;
            // 
            // Websites
            // 
            this.Websites.Text = "Cookies";
            this.Websites.Width = 646;
            // 
            // lblCheckIE
            // 
            this.lblCheckIE.AutoSize = true;
            this.lblCheckIE.Location = new System.Drawing.Point(10, 18);
            this.lblCheckIE.Name = "lblCheckIE";
            this.lblCheckIE.Size = new System.Drawing.Size(397, 13);
            this.lblCheckIE.TabIndex = 0;
            this.lblCheckIE.Text = "Check mark the cookies you want to keep when cleaning internet explorer cookies";
            // 
            // tbpCookiesFF
            // 
            this.tbpCookiesFF.Controls.Add(this.btnFFCheckInvert);
            this.tbpCookiesFF.Controls.Add(this.btnFFCheckNone);
            this.tbpCookiesFF.Controls.Add(this.btnFFCheckAll);
            this.tbpCookiesFF.Controls.Add(this.lblCheckFF);
            this.tbpCookiesFF.Controls.Add(this.btnDelFFCookies);
            this.tbpCookiesFF.Controls.Add(this.ListViewFF);
            this.tbpCookiesFF.Location = new System.Drawing.Point(4, 22);
            this.tbpCookiesFF.Name = "tbpCookiesFF";
            this.tbpCookiesFF.Size = new System.Drawing.Size(789, 389);
            this.tbpCookiesFF.TabIndex = 2;
            this.tbpCookiesFF.Text = "FF Cookie Manager";
            this.tbpCookiesFF.UseVisualStyleBackColor = true;
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
            // lblCheckFF
            // 
            this.lblCheckFF.AutoSize = true;
            this.lblCheckFF.Location = new System.Drawing.Point(10, 18);
            this.lblCheckFF.Name = "lblCheckFF";
            this.lblCheckFF.Size = new System.Drawing.Size(364, 13);
            this.lblCheckFF.TabIndex = 4;
            this.lblCheckFF.Text = "Check mark the cookies you want to keep when cleaning in Firefox cookies";
            // 
            // btnDelFFCookies
            // 
            this.btnDelFFCookies.Location = new System.Drawing.Point(535, 342);
            this.btnDelFFCookies.Name = "btnDelFFCookies";
            this.btnDelFFCookies.Size = new System.Drawing.Size(246, 30);
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
            // tbpCookiesChrome
            // 
            this.tbpCookiesChrome.Controls.Add(this.btnChrCheckInvert);
            this.tbpCookiesChrome.Controls.Add(this.btnChrCheckNone);
            this.tbpCookiesChrome.Controls.Add(this.btnChrCheckAll);
            this.tbpCookiesChrome.Controls.Add(this.lblCheckChrome);
            this.tbpCookiesChrome.Controls.Add(this.btnDelChromeCookies);
            this.tbpCookiesChrome.Controls.Add(this.ListViewChrome);
            this.tbpCookiesChrome.Location = new System.Drawing.Point(4, 22);
            this.tbpCookiesChrome.Name = "tbpCookiesChrome";
            this.tbpCookiesChrome.Size = new System.Drawing.Size(789, 389);
            this.tbpCookiesChrome.TabIndex = 3;
            this.tbpCookiesChrome.Text = "Google Chrome - Cookies";
            this.tbpCookiesChrome.UseVisualStyleBackColor = true;
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
            // lblCheckChrome
            // 
            this.lblCheckChrome.AutoSize = true;
            this.lblCheckChrome.Location = new System.Drawing.Point(10, 18);
            this.lblCheckChrome.Name = "lblCheckChrome";
            this.lblCheckChrome.Size = new System.Drawing.Size(369, 13);
            this.lblCheckChrome.TabIndex = 7;
            this.lblCheckChrome.Text = "Check mark the cookies you want to keep when cleaning in Chrome cookies";
            // 
            // btnDelChromeCookies
            // 
            this.btnDelChromeCookies.Location = new System.Drawing.Point(535, 342);
            this.btnDelChromeCookies.Name = "btnDelChromeCookies";
            this.btnDelChromeCookies.Size = new System.Drawing.Size(246, 30);
            this.btnDelChromeCookies.TabIndex = 6;
            this.btnDelChromeCookies.Text = "Delete Checked Cookies Now";
            this.btnDelChromeCookies.UseVisualStyleBackColor = true;
            this.btnDelChromeCookies.Click += new System.EventHandler(this.btnDelChromeCookies_Click);
            // 
            // ListViewChrome
            // 
            this.ListViewChrome.CheckBoxes = true;
            this.ListViewChrome.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhCookiesAll});
            this.ListViewChrome.Location = new System.Drawing.Point(12, 60);
            this.ListViewChrome.Name = "ListViewChrome";
            this.ListViewChrome.Size = new System.Drawing.Size(769, 276);
            this.ListViewChrome.TabIndex = 5;
            this.ListViewChrome.UseCompatibleStateImageBehavior = false;
            this.ListViewChrome.View = System.Windows.Forms.View.Details;
            // 
            // clhCookiesAll
            // 
            this.clhCookiesAll.Text = "Cookies";
            this.clhCookiesAll.Width = 646;
            // 
            // tbpIEURLs
            // 
            this.tbpIEURLs.Controls.Add(this.btnUrlCheckInvert);
            this.tbpIEURLs.Controls.Add(this.btnUrlCheckNone);
            this.tbpIEURLs.Controls.Add(this.btnUrlCheckAll);
            this.tbpIEURLs.Controls.Add(this.btnDeleteIEURLs);
            this.tbpIEURLs.Controls.Add(this.lvIEURLs);
            this.tbpIEURLs.Controls.Add(this.lblCheckIEURLs);
            this.tbpIEURLs.Location = new System.Drawing.Point(4, 22);
            this.tbpIEURLs.Name = "tbpIEURLs";
            this.tbpIEURLs.Padding = new System.Windows.Forms.Padding(3);
            this.tbpIEURLs.Size = new System.Drawing.Size(789, 389);
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
            // btnDeleteIEURLs
            // 
            this.btnDeleteIEURLs.Location = new System.Drawing.Point(537, 342);
            this.btnDeleteIEURLs.Name = "btnDeleteIEURLs";
            this.btnDeleteIEURLs.Size = new System.Drawing.Size(246, 30);
            this.btnDeleteIEURLs.TabIndex = 2;
            this.btnDeleteIEURLs.Text = "Delete Checked URLs Now";
            this.btnDeleteIEURLs.UseVisualStyleBackColor = true;
            this.btnDeleteIEURLs.Click += new System.EventHandler(this.btnDeleteIEURLs_Click);
            // 
            // lvIEURLs
            // 
            this.lvIEURLs.CheckBoxes = true;
            this.lvIEURLs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.URLs});
            this.lvIEURLs.Location = new System.Drawing.Point(12, 60);
            this.lvIEURLs.Name = "lvIEURLs";
            this.lvIEURLs.Size = new System.Drawing.Size(769, 276);
            this.lvIEURLs.TabIndex = 1;
            this.lvIEURLs.UseCompatibleStateImageBehavior = false;
            this.lvIEURLs.View = System.Windows.Forms.View.Details;
            // 
            // URLs
            // 
            this.URLs.Text = "URLs";
            this.URLs.Width = 631;
            // 
            // lblCheckIEURLs
            // 
            this.lblCheckIEURLs.AutoSize = true;
            this.lblCheckIEURLs.Location = new System.Drawing.Point(10, 18);
            this.lblCheckIEURLs.Name = "lblCheckIEURLs";
            this.lblCheckIEURLs.Size = new System.Drawing.Size(421, 13);
            this.lblCheckIEURLs.TabIndex = 0;
            this.lblCheckIEURLs.Text = "Check mark the URLs you want to keep when you clean Internet Explorer - URL Histo" +
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
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(629, 18);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 474);
            this.Controls.Add(this.spcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.FrmOptions_Load);
            this.spcMain.Panel1.ResumeLayout(false);
            this.spcMain.Panel2.ResumeLayout(false);
            this.spcMain.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tbpCookiesIE.ResumeLayout(false);
            this.tbpCookiesIE.PerformLayout();
            this.tbpCookiesFF.ResumeLayout(false);
            this.tbpCookiesFF.PerformLayout();
            this.tbpCookiesChrome.ResumeLayout(false);
            this.tbpCookiesChrome.PerformLayout();
            this.tbpIEURLs.ResumeLayout(false);
            this.tbpIEURLs.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		System.Windows.Forms.SplitContainer spcMain;
		System.Windows.Forms.Button btnCancel;
		System.Windows.Forms.Button btnOK;
		System.Windows.Forms.TabControl tcMain;
		System.Windows.Forms.TabPage tbpCookiesIE;
		System.Windows.Forms.TabPage tbpIEURLs;
		System.Windows.Forms.Label lblCheckIE;
		System.Windows.Forms.Button btnDeleteIECookies;
		System.Windows.Forms.ListView lvCookiesIE;
		System.Windows.Forms.Label lblCheckIEURLs;
		System.Windows.Forms.ListView lvIEURLs;
		System.Windows.Forms.Button btnDeleteIEURLs;
		System.Windows.Forms.ColumnHeader Websites;
		System.Windows.Forms.ColumnHeader URLs;
		System.Windows.Forms.TabPage tbpCookiesFF;
		System.Windows.Forms.TabPage tbpCookiesChrome;
		System.Windows.Forms.Label lblCheckFF;
		System.Windows.Forms.Button btnDelFFCookies;
		System.Windows.Forms.ListView ListViewFF;
		System.Windows.Forms.ColumnHeader clhCookies;
		System.Windows.Forms.Label lblCheckChrome;
		System.Windows.Forms.Button btnDelChromeCookies;
		System.Windows.Forms.ListView ListViewChrome;
		System.Windows.Forms.ColumnHeader clhCookiesAll;
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