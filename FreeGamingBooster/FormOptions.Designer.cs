namespace FreeGamingBooster
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
            this.chkMinToTray = new System.Windows.Forms.CheckBox();
            this.chkShowBaloon = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
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
            // chkMinToTray
            // 
            this.chkMinToTray.AutoSize = true;
            this.chkMinToTray.Location = new System.Drawing.Point(15, 41);
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
            this.chkShowBaloon.Location = new System.Drawing.Point(15, 64);
            this.chkShowBaloon.Name = "chkShowBaloon";
            this.chkShowBaloon.Size = new System.Drawing.Size(193, 17);
            this.chkShowBaloon.TabIndex = 2;
            this.chkShowBaloon.Text = "Show balloon when minimize to tray";
            this.chkShowBaloon.UseVisualStyleBackColor = true;
            this.chkShowBaloon.CheckedChanged += new System.EventHandler(this.chkShowBaloon_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(344, 102);
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
            this.ClientSize = new System.Drawing.Size(431, 133);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkShowBaloon);
            this.Controls.Add(this.chkMinToTray);
            this.Controls.Add(this.Languages);
            this.Controls.Add(this.Language);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		System.Windows.Forms.Label Language;
        System.Windows.Forms.ComboBox Languages;
		System.Windows.Forms.CheckBox chkMinToTray;
        System.Windows.Forms.CheckBox chkShowBaloon;
		System.Windows.Forms.Button btnOK;
	}
}