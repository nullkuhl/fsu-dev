namespace PCCleaner
{
	partial class FormOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
            this.Language = new System.Windows.Forms.Label();
            this.Languages = new System.Windows.Forms.ComboBox();
            this.Theme = new System.Windows.Forms.Label();
            this.cboThemes = new System.Windows.Forms.ComboBox();
            this.chkMinToTray = new System.Windows.Forms.CheckBox();
            this.chkShowBaloon = new System.Windows.Forms.CheckBox();
            this.contextMenu = new System.Windows.Forms.GroupBox();
            this.clbContextMenuOptions = new System.Windows.Forms.CheckedListBox();
            this.SelectContextMenus = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Language
            // 
            this.Language.AutoSize = true;
            this.Language.Location = new System.Drawing.Point(12, 17);
            this.Language.Name = "Language";
            this.Language.Size = new System.Drawing.Size(58, 13);
            this.Language.TabIndex = 0;
            this.Language.Text = "Language:";
            // 
            // Languages
            // 
            this.Languages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Languages.FormattingEnabled = true;
            this.Languages.Items.AddRange(new object[] {
            "English",
            "Deutsch",
            "Spanish",
            "Portuguese",
            "Italian",
            "French"});
            this.Languages.Location = new System.Drawing.Point(76, 14);
            this.Languages.Name = "Languages";
            this.Languages.Size = new System.Drawing.Size(121, 21);
            this.Languages.TabIndex = 1;
            this.Languages.SelectedIndexChanged += new System.EventHandler(this.Languages_SelectedIndexChanged);
            // 
            // Theme
            // 
            this.Theme.AutoSize = true;
            this.Theme.Location = new System.Drawing.Point(27, 44);
            this.Theme.Name = "Theme";
            this.Theme.Size = new System.Drawing.Size(43, 13);
            this.Theme.TabIndex = 0;
            this.Theme.Text = "Theme:";
            // 
            // cboThemes
            // 
            this.cboThemes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThemes.FormattingEnabled = true;
            this.cboThemes.Items.AddRange(new object[] {
            "Blue",
            "Green",
            "Red"});
            this.cboThemes.Location = new System.Drawing.Point(76, 41);
            this.cboThemes.Name = "cboThemes";
            this.cboThemes.Size = new System.Drawing.Size(121, 21);
            this.cboThemes.TabIndex = 1;
            this.cboThemes.SelectedIndexChanged += new System.EventHandler(this.cboThemes_SelectedIndexChanged);
            // 
            // chkMinToTray
            // 
            this.chkMinToTray.AutoSize = true;
            this.chkMinToTray.Location = new System.Drawing.Point(15, 73);
            this.chkMinToTray.Name = "chkMinToTray";
            this.chkMinToTray.Size = new System.Drawing.Size(273, 17);
            this.chkMinToTray.TabIndex = 2;
            this.chkMinToTray.Text = "Minimize to tray when you close the program window";
            this.chkMinToTray.UseVisualStyleBackColor = true;
            this.chkMinToTray.CheckedChanged += new System.EventHandler(this.chkMinToTray_CheckedChanged);
            // 
            // chkShowBaloon
            // 
            this.chkShowBaloon.AutoSize = true;
            this.chkShowBaloon.Location = new System.Drawing.Point(15, 96);
            this.chkShowBaloon.Name = "chkShowBaloon";
            this.chkShowBaloon.Size = new System.Drawing.Size(193, 17);
            this.chkShowBaloon.TabIndex = 2;
            this.chkShowBaloon.Text = "Show balloon when minimize to tray";
            this.chkShowBaloon.UseVisualStyleBackColor = true;
            this.chkShowBaloon.CheckedChanged += new System.EventHandler(this.chkShowBaloon_CheckedChanged);
            // 
            // contextMenu
            // 
            this.contextMenu.Controls.Add(this.clbContextMenuOptions);
            this.contextMenu.Controls.Add(this.SelectContextMenus);
            this.contextMenu.Location = new System.Drawing.Point(12, 129);
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(447, 183);
            this.contextMenu.TabIndex = 3;
            this.contextMenu.TabStop = false;
            this.contextMenu.Text = "Context Menu";
            // 
            // clbContextMenuOptions
            // 
            this.clbContextMenuOptions.CheckOnClick = true;
            this.clbContextMenuOptions.FormattingEnabled = true;
            this.clbContextMenuOptions.Location = new System.Drawing.Point(9, 35);
            this.clbContextMenuOptions.Name = "clbContextMenuOptions";
            this.clbContextMenuOptions.Size = new System.Drawing.Size(431, 139);
            this.clbContextMenuOptions.TabIndex = 1;
            // 
            // SelectContextMenus
            // 
            this.SelectContextMenus.AutoSize = true;
            this.SelectContextMenus.Location = new System.Drawing.Point(6, 19);
            this.SelectContextMenus.Name = "SelectContextMenus";
            this.SelectContextMenus.Size = new System.Drawing.Size(358, 13);
            this.SelectContextMenus.TabIndex = 0;
            this.SelectContextMenus.Text = "Select the context menus that you want to integrate into Windows Explorer";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(384, 318);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 349);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.contextMenu);
            this.Controls.Add(this.chkShowBaloon);
            this.Controls.Add(this.chkMinToTray);
            this.Controls.Add(this.cboThemes);
            this.Controls.Add(this.Theme);
            this.Controls.Add(this.Languages);
            this.Controls.Add(this.Language);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.contextMenu.ResumeLayout(false);
            this.contextMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		System.Windows.Forms.Label Language;
		System.Windows.Forms.ComboBox Languages;
		System.Windows.Forms.Label Theme;
		System.Windows.Forms.ComboBox cboThemes;
		System.Windows.Forms.CheckBox chkMinToTray;
		System.Windows.Forms.CheckBox chkShowBaloon;
		System.Windows.Forms.GroupBox contextMenu;
		System.Windows.Forms.CheckedListBox clbContextMenuOptions;
		System.Windows.Forms.Label SelectContextMenus;
		System.Windows.Forms.Button btnOK;
	}
}