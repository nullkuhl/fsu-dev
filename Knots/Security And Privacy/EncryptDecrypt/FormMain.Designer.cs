using System.Resources;
using System.Globalization;
using System.Threading;

namespace EncryptDecrypt
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("EncryptDecrypt.Resources",
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.grbPath = new System.Windows.Forms.GroupBox();
            this.lblSaveToEncrypt = new System.Windows.Forms.Label();
            this.encToBrowseBtn = new System.Windows.Forms.Button();
            this.encryptToTxt = new System.Windows.Forms.TextBox();
            this.lblSelectEncrypt = new System.Windows.Forms.Label();
            this.btnOpenFileToEncrypt = new System.Windows.Forms.Button();
            this.enPath_textbox = new System.Windows.Forms.TextBox();
            this.grbPassword = new System.Windows.Forms.GroupBox();
            this.txtPasswdConfirm = new System.Windows.Forms.TextBox();
            this.txtPasswdEncrypt = new System.Windows.Forms.TextBox();
            this.lblPasswdEncrypt = new System.Windows.Forms.Label();
            this.lblPasswdConfirm = new System.Windows.Forms.Label();
            this.Encrypt_btn = new System.Windows.Forms.Button();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.EncryptionTab = new System.Windows.Forms.TabPage();
            this.prbEncrypting = new System.Windows.Forms.ProgressBar();
            this.chkCreateAuto = new System.Windows.Forms.CheckBox();
            this.chkDeleteAfterEncryption = new System.Windows.Forms.CheckBox();
            this.DecryptionTab = new System.Windows.Forms.TabPage();
            this.prbDecrypting = new System.Windows.Forms.ProgressBar();
            this.chkOpenAfterDecryption = new System.Windows.Forms.CheckBox();
            this.chkDeleteAfterDecryption = new System.Windows.Forms.CheckBox();
            this.grbPasswordDecr = new System.Windows.Forms.GroupBox();
            this.txtPasswdDecrypt = new System.Windows.Forms.TextBox();
            this.lblPasswdDecr = new System.Windows.Forms.Label();
            this.grbPathDecr = new System.Windows.Forms.GroupBox();
            this.lblSaveToDecr = new System.Windows.Forms.Label();
            this.btnSaveTo = new System.Windows.Forms.Button();
            this.extrect_Textbox = new System.Windows.Forms.TextBox();
            this.lblSelectDecr = new System.Windows.Forms.Label();
            this.btnOpenFileToDecrypt = new System.Windows.Forms.Button();
            this.dcPath_textbox = new System.Windows.Forms.TextBox();
            this.Decrypt_btn = new System.Windows.Forms.Button();
            this.ucBottom = new EncryptDecrypt.BottomControl();
            this.ucTop = new EncryptDecrypt.TopControl();
            this.grbPath.SuspendLayout();
            this.grbPassword.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.EncryptionTab.SuspendLayout();
            this.DecryptionTab.SuspendLayout();
            this.grbPasswordDecr.SuspendLayout();
            this.grbPathDecr.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbPath
            // 
            this.grbPath.Controls.Add(this.lblSaveToEncrypt);
            this.grbPath.Controls.Add(this.encToBrowseBtn);
            this.grbPath.Controls.Add(this.encryptToTxt);
            this.grbPath.Controls.Add(this.lblSelectEncrypt);
            this.grbPath.Controls.Add(this.btnOpenFileToEncrypt);
            this.grbPath.Controls.Add(this.enPath_textbox);
            this.grbPath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grbPath.Location = new System.Drawing.Point(16, 11);
            this.grbPath.Name = "grbPath";
            this.grbPath.Size = new System.Drawing.Size(462, 126);
            this.grbPath.TabIndex = 0;
            this.grbPath.TabStop = false;
            this.grbPath.Text = "Path Selection";
            // 
            // lblSaveToEncrypt
            // 
            this.lblSaveToEncrypt.AutoSize = true;
            this.lblSaveToEncrypt.Location = new System.Drawing.Point(15, 75);
            this.lblSaveToEncrypt.Name = "lblSaveToEncrypt";
            this.lblSaveToEncrypt.Size = new System.Drawing.Size(47, 13);
            this.lblSaveToEncrypt.TabIndex = 14;
            this.lblSaveToEncrypt.Text = "Save to:";
            // 
            // encToBrowseBtn
            // 
            this.encToBrowseBtn.Location = new System.Drawing.Point(329, 94);
            this.encToBrowseBtn.Name = "encToBrowseBtn";
            this.encToBrowseBtn.Size = new System.Drawing.Size(123, 23);
            this.encToBrowseBtn.TabIndex = 16;
            this.encToBrowseBtn.Text = "Browse...";
            this.encToBrowseBtn.UseVisualStyleBackColor = true;
            this.encToBrowseBtn.Click += new System.EventHandler(this.encToBrowseBtn_Click);
            // 
            // encryptToTxt
            // 
            this.encryptToTxt.Location = new System.Drawing.Point(19, 96);
            this.encryptToTxt.Name = "encryptToTxt";
            this.encryptToTxt.Size = new System.Drawing.Size(302, 20);
            this.encryptToTxt.TabIndex = 15;
            // 
            // lblSelectEncrypt
            // 
            this.lblSelectEncrypt.AutoSize = true;
            this.lblSelectEncrypt.Location = new System.Drawing.Point(15, 26);
            this.lblSelectEncrypt.Name = "lblSelectEncrypt";
            this.lblSelectEncrypt.Size = new System.Drawing.Size(161, 13);
            this.lblSelectEncrypt.TabIndex = 2;
            this.lblSelectEncrypt.Text = "Select a file you want to encrypt:";
            // 
            // btnOpenFileToEncrypt
            // 
            this.btnOpenFileToEncrypt.Location = new System.Drawing.Point(329, 45);
            this.btnOpenFileToEncrypt.Name = "btnOpenFileToEncrypt";
            this.btnOpenFileToEncrypt.Size = new System.Drawing.Size(123, 23);
            this.btnOpenFileToEncrypt.TabIndex = 2;
            this.btnOpenFileToEncrypt.Text = "Browse...";
            this.btnOpenFileToEncrypt.UseVisualStyleBackColor = true;
            this.btnOpenFileToEncrypt.Click += new System.EventHandler(this.btnOpenFileToEncrypt_Click);
            // 
            // enPath_textbox
            // 
            this.enPath_textbox.Location = new System.Drawing.Point(19, 47);
            this.enPath_textbox.Name = "enPath_textbox";
            this.enPath_textbox.Size = new System.Drawing.Size(302, 20);
            this.enPath_textbox.TabIndex = 1;
            // 
            // grbPassword
            // 
            this.grbPassword.Controls.Add(this.txtPasswdConfirm);
            this.grbPassword.Controls.Add(this.txtPasswdEncrypt);
            this.grbPassword.Controls.Add(this.lblPasswdEncrypt);
            this.grbPassword.Controls.Add(this.lblPasswdConfirm);
            this.grbPassword.Location = new System.Drawing.Point(16, 144);
            this.grbPassword.Name = "grbPassword";
            this.grbPassword.Size = new System.Drawing.Size(462, 95);
            this.grbPassword.TabIndex = 1;
            this.grbPassword.TabStop = false;
            this.grbPassword.Text = "Password";
            // 
            // txtPasswdConfirm
            // 
            this.txtPasswdConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPasswdConfirm.Location = new System.Drawing.Point(166, 57);
            this.txtPasswdConfirm.Name = "txtPasswdConfirm";
            this.txtPasswdConfirm.PasswordChar = '*';
            this.txtPasswdConfirm.Size = new System.Drawing.Size(252, 20);
            this.txtPasswdConfirm.TabIndex = 4;
            // 
            // txtPasswdEncrypt
            // 
            this.txtPasswdEncrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPasswdEncrypt.Location = new System.Drawing.Point(166, 25);
            this.txtPasswdEncrypt.Name = "txtPasswdEncrypt";
            this.txtPasswdEncrypt.PasswordChar = '*';
            this.txtPasswdEncrypt.Size = new System.Drawing.Size(252, 20);
            this.txtPasswdEncrypt.TabIndex = 3;
            // 
            // lblPasswdEncrypt
            // 
            this.lblPasswdEncrypt.Location = new System.Drawing.Point(19, 28);
            this.lblPasswdEncrypt.Name = "lblPasswdEncrypt";
            this.lblPasswdEncrypt.Size = new System.Drawing.Size(143, 13);
            this.lblPasswdEncrypt.TabIndex = 4;
            this.lblPasswdEncrypt.Text = "Password:";
            this.lblPasswdEncrypt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPasswdConfirm
            // 
            this.lblPasswdConfirm.Location = new System.Drawing.Point(19, 60);
            this.lblPasswdConfirm.Name = "lblPasswdConfirm";
            this.lblPasswdConfirm.Size = new System.Drawing.Size(143, 13);
            this.lblPasswdConfirm.TabIndex = 3;
            this.lblPasswdConfirm.Text = "Confirm Password:";
            this.lblPasswdConfirm.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Encrypt_btn
            // 
            this.Encrypt_btn.Location = new System.Drawing.Point(321, 309);
            this.Encrypt_btn.Name = "Encrypt_btn";
            this.Encrypt_btn.Size = new System.Drawing.Size(157, 26);
            this.Encrypt_btn.TabIndex = 7;
            this.Encrypt_btn.Text = "Encrypt";
            this.Encrypt_btn.UseVisualStyleBackColor = true;
            this.Encrypt_btn.Click += new System.EventHandler(this.Encrypt_btn_Click);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.EncryptionTab);
            this.tcMain.Controls.Add(this.DecryptionTab);
            this.tcMain.Location = new System.Drawing.Point(4, 68);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(511, 370);
            this.tcMain.TabIndex = 9;
            // 
            // EncryptionTab
            // 
            this.EncryptionTab.Controls.Add(this.prbEncrypting);
            this.EncryptionTab.Controls.Add(this.chkCreateAuto);
            this.EncryptionTab.Controls.Add(this.chkDeleteAfterEncryption);
            this.EncryptionTab.Controls.Add(this.Encrypt_btn);
            this.EncryptionTab.Controls.Add(this.grbPath);
            this.EncryptionTab.Controls.Add(this.grbPassword);
            this.EncryptionTab.Location = new System.Drawing.Point(4, 22);
            this.EncryptionTab.Name = "EncryptionTab";
            this.EncryptionTab.Padding = new System.Windows.Forms.Padding(3);
            this.EncryptionTab.Size = new System.Drawing.Size(503, 344);
            this.EncryptionTab.TabIndex = 0;
            this.EncryptionTab.Text = "Encryption";
            this.EncryptionTab.UseVisualStyleBackColor = true;
            // 
            // prbEncrypting
            // 
            this.prbEncrypting.Location = new System.Drawing.Point(16, 291);
            this.prbEncrypting.Name = "prbEncrypting";
            this.prbEncrypting.Size = new System.Drawing.Size(462, 12);
            this.prbEncrypting.TabIndex = 21;
            // 
            // chkCreateAuto
            // 
            this.chkCreateAuto.AutoSize = true;
            this.chkCreateAuto.Location = new System.Drawing.Point(30, 245);
            this.chkCreateAuto.Name = "chkCreateAuto";
            this.chkCreateAuto.Size = new System.Drawing.Size(191, 17);
            this.chkCreateAuto.TabIndex = 5;
            this.chkCreateAuto.Text = "Create an auto-decrypting EXE file.";
            this.chkCreateAuto.UseVisualStyleBackColor = true;
            // 
            // chkDeleteAfterEncryption
            // 
            this.chkDeleteAfterEncryption.AutoSize = true;
            this.chkDeleteAfterEncryption.Location = new System.Drawing.Point(30, 268);
            this.chkDeleteAfterEncryption.Name = "chkDeleteAfterEncryption";
            this.chkDeleteAfterEncryption.Size = new System.Drawing.Size(206, 17);
            this.chkDeleteAfterEncryption.TabIndex = 6;
            this.chkDeleteAfterEncryption.Text = "Delete the original file after encryption.";
            this.chkDeleteAfterEncryption.UseVisualStyleBackColor = true;
            // 
            // DecryptionTab
            // 
            this.DecryptionTab.Controls.Add(this.prbDecrypting);
            this.DecryptionTab.Controls.Add(this.chkOpenAfterDecryption);
            this.DecryptionTab.Controls.Add(this.chkDeleteAfterDecryption);
            this.DecryptionTab.Controls.Add(this.grbPasswordDecr);
            this.DecryptionTab.Controls.Add(this.grbPathDecr);
            this.DecryptionTab.Controls.Add(this.Decrypt_btn);
            this.DecryptionTab.Location = new System.Drawing.Point(4, 22);
            this.DecryptionTab.Name = "DecryptionTab";
            this.DecryptionTab.Padding = new System.Windows.Forms.Padding(3);
            this.DecryptionTab.Size = new System.Drawing.Size(503, 344);
            this.DecryptionTab.TabIndex = 1;
            this.DecryptionTab.Text = "Decryption";
            this.DecryptionTab.UseVisualStyleBackColor = true;
            // 
            // prbDecrypting
            // 
            this.prbDecrypting.Location = new System.Drawing.Point(16, 288);
            this.prbDecrypting.Name = "prbDecrypting";
            this.prbDecrypting.Size = new System.Drawing.Size(462, 12);
            this.prbDecrypting.TabIndex = 22;
            // 
            // chkOpenAfterDecryption
            // 
            this.chkOpenAfterDecryption.AutoSize = true;
            this.chkOpenAfterDecryption.Location = new System.Drawing.Point(27, 244);
            this.chkOpenAfterDecryption.Name = "chkOpenAfterDecryption";
            this.chkOpenAfterDecryption.Size = new System.Drawing.Size(200, 17);
            this.chkOpenAfterDecryption.TabIndex = 15;
            this.chkOpenAfterDecryption.Text = "Open file after successful decryption.";
            this.chkOpenAfterDecryption.UseVisualStyleBackColor = true;
            // 
            // chkDeleteAfterDecryption
            // 
            this.chkDeleteAfterDecryption.AutoSize = true;
            this.chkDeleteAfterDecryption.Location = new System.Drawing.Point(27, 265);
            this.chkDeleteAfterDecryption.Name = "chkDeleteAfterDecryption";
            this.chkDeleteAfterDecryption.Size = new System.Drawing.Size(220, 17);
            this.chkDeleteAfterDecryption.TabIndex = 16;
            this.chkDeleteAfterDecryption.Text = "Delete the encrypted file after decryption.";
            this.chkDeleteAfterDecryption.UseVisualStyleBackColor = true;
            // 
            // grbPasswordDecr
            // 
            this.grbPasswordDecr.Controls.Add(this.txtPasswdDecrypt);
            this.grbPasswordDecr.Controls.Add(this.lblPasswdDecr);
            this.grbPasswordDecr.Location = new System.Drawing.Point(16, 143);
            this.grbPasswordDecr.Name = "grbPasswordDecr";
            this.grbPasswordDecr.Size = new System.Drawing.Size(462, 72);
            this.grbPasswordDecr.TabIndex = 11;
            this.grbPasswordDecr.TabStop = false;
            this.grbPasswordDecr.Text = "Password";
            // 
            // txtPasswdDecrypt
            // 
            this.txtPasswdDecrypt.Location = new System.Drawing.Point(144, 32);
            this.txtPasswdDecrypt.Name = "txtPasswdDecrypt";
            this.txtPasswdDecrypt.PasswordChar = '*';
            this.txtPasswdDecrypt.Size = new System.Drawing.Size(252, 20);
            this.txtPasswdDecrypt.TabIndex = 14;
            // 
            // lblPasswdDecr
            // 
            this.lblPasswdDecr.Location = new System.Drawing.Point(54, 35);
            this.lblPasswdDecr.Name = "lblPasswdDecr";
            this.lblPasswdDecr.Size = new System.Drawing.Size(84, 13);
            this.lblPasswdDecr.TabIndex = 4;
            this.lblPasswdDecr.Text = "Password:";
            this.lblPasswdDecr.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grbPathDecr
            // 
            this.grbPathDecr.Controls.Add(this.lblSaveToDecr);
            this.grbPathDecr.Controls.Add(this.btnSaveTo);
            this.grbPathDecr.Controls.Add(this.extrect_Textbox);
            this.grbPathDecr.Controls.Add(this.lblSelectDecr);
            this.grbPathDecr.Controls.Add(this.btnOpenFileToDecrypt);
            this.grbPathDecr.Controls.Add(this.dcPath_textbox);
            this.grbPathDecr.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grbPathDecr.Location = new System.Drawing.Point(16, 11);
            this.grbPathDecr.Name = "grbPathDecr";
            this.grbPathDecr.Size = new System.Drawing.Size(462, 126);
            this.grbPathDecr.TabIndex = 10;
            this.grbPathDecr.TabStop = false;
            this.grbPathDecr.Text = "Path Selection";
            // 
            // lblSaveToDecr
            // 
            this.lblSaveToDecr.AutoSize = true;
            this.lblSaveToDecr.Location = new System.Drawing.Point(15, 75);
            this.lblSaveToDecr.Name = "lblSaveToDecr";
            this.lblSaveToDecr.Size = new System.Drawing.Size(47, 13);
            this.lblSaveToDecr.TabIndex = 4;
            this.lblSaveToDecr.Text = "Save to:";
            // 
            // btnSaveTo
            // 
            this.btnSaveTo.Location = new System.Drawing.Point(329, 94);
            this.btnSaveTo.Name = "btnSaveTo";
            this.btnSaveTo.Size = new System.Drawing.Size(123, 23);
            this.btnSaveTo.TabIndex = 13;
            this.btnSaveTo.Text = "Browse...";
            this.btnSaveTo.UseVisualStyleBackColor = true;
            this.btnSaveTo.Click += new System.EventHandler(this.btnSaveTo_Click);
            // 
            // extrect_Textbox
            // 
            this.extrect_Textbox.Location = new System.Drawing.Point(20, 96);
            this.extrect_Textbox.Name = "extrect_Textbox";
            this.extrect_Textbox.Size = new System.Drawing.Size(301, 20);
            this.extrect_Textbox.TabIndex = 12;
            // 
            // lblSelectDecr
            // 
            this.lblSelectDecr.AutoSize = true;
            this.lblSelectDecr.Location = new System.Drawing.Point(15, 26);
            this.lblSelectDecr.Name = "lblSelectDecr";
            this.lblSelectDecr.Size = new System.Drawing.Size(217, 13);
            this.lblSelectDecr.TabIndex = 2;
            this.lblSelectDecr.Text = "Select an encrypted file you want to decrypt:";
            // 
            // btnOpenFileToDecrypt
            // 
            this.btnOpenFileToDecrypt.Location = new System.Drawing.Point(329, 45);
            this.btnOpenFileToDecrypt.Name = "btnOpenFileToDecrypt";
            this.btnOpenFileToDecrypt.Size = new System.Drawing.Size(123, 23);
            this.btnOpenFileToDecrypt.TabIndex = 11;
            this.btnOpenFileToDecrypt.Text = "Browse...";
            this.btnOpenFileToDecrypt.UseVisualStyleBackColor = true;
            this.btnOpenFileToDecrypt.Click += new System.EventHandler(this.btnOpenFileToDecrypt_Click);
            // 
            // dcPath_textbox
            // 
            this.dcPath_textbox.Location = new System.Drawing.Point(20, 47);
            this.dcPath_textbox.Name = "dcPath_textbox";
            this.dcPath_textbox.Size = new System.Drawing.Size(301, 20);
            this.dcPath_textbox.TabIndex = 10;
            // 
            // Decrypt_btn
            // 
            this.Decrypt_btn.Location = new System.Drawing.Point(321, 306);
            this.Decrypt_btn.Name = "Decrypt_btn";
            this.Decrypt_btn.Size = new System.Drawing.Size(157, 26);
            this.Decrypt_btn.TabIndex = 17;
            this.Decrypt_btn.Text = "Decrypt";
            this.Decrypt_btn.UseVisualStyleBackColor = true;
            this.Decrypt_btn.Click += new System.EventHandler(this.Decrypt_btn_Click);
            // 
            // ucBottom
            // 
            this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucBottom.Location = new System.Drawing.Point(0, 444);
            this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
            this.ucBottom.MaximumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.MinimumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.Name = "ucBottom";
            this.ucBottom.Size = new System.Drawing.Size(519, 31);
            this.ucBottom.TabIndex = 11;
            // 
            // ucTop
            // 
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTop.Location = new System.Drawing.Point(0, 0);
            this.ucTop.Margin = new System.Windows.Forms.Padding(0);
            this.ucTop.Name = "ucTop";
            this.ucTop.Size = new System.Drawing.Size(519, 64);
            this.ucTop.TabIndex = 10;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(519, 475);
            this.Controls.Add(this.ucBottom);
            this.Controls.Add(this.ucTop);
            this.Controls.Add(this.tcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Encrypter and Decrypter";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grbPath.ResumeLayout(false);
            this.grbPath.PerformLayout();
            this.grbPassword.ResumeLayout(false);
            this.grbPassword.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.EncryptionTab.ResumeLayout(false);
            this.EncryptionTab.PerformLayout();
            this.DecryptionTab.ResumeLayout(false);
            this.DecryptionTab.PerformLayout();
            this.grbPasswordDecr.ResumeLayout(false);
            this.grbPasswordDecr.PerformLayout();
            this.grbPathDecr.ResumeLayout(false);
            this.grbPathDecr.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		System.Windows.Forms.GroupBox grbPath;
		System.Windows.Forms.Label lblSelectEncrypt;
		System.Windows.Forms.Button btnOpenFileToEncrypt;
		System.Windows.Forms.TextBox enPath_textbox;
		System.Windows.Forms.GroupBox grbPassword;
		System.Windows.Forms.Button Encrypt_btn;
		System.Windows.Forms.TabControl tcMain;
		System.Windows.Forms.TabPage EncryptionTab;
		System.Windows.Forms.TabPage DecryptionTab;
		System.Windows.Forms.Button Decrypt_btn;
		System.Windows.Forms.Label lblPasswdEncrypt;
		System.Windows.Forms.Label lblPasswdConfirm;
		System.Windows.Forms.TextBox txtPasswdConfirm;
		System.Windows.Forms.TextBox txtPasswdEncrypt;
		System.Windows.Forms.CheckBox chkDeleteAfterEncryption;
		System.Windows.Forms.GroupBox grbPasswordDecr;
		System.Windows.Forms.TextBox txtPasswdDecrypt;
		System.Windows.Forms.Label lblPasswdDecr;
		System.Windows.Forms.GroupBox grbPathDecr;
		System.Windows.Forms.Label lblSelectDecr;
		System.Windows.Forms.Button btnOpenFileToDecrypt;
		System.Windows.Forms.TextBox dcPath_textbox;
		System.Windows.Forms.CheckBox chkDeleteAfterDecryption;
		System.Windows.Forms.Label lblSaveToDecr;
		System.Windows.Forms.Button btnSaveTo;
		System.Windows.Forms.TextBox extrect_Textbox;
		System.Windows.Forms.CheckBox chkOpenAfterDecryption;
		System.Windows.Forms.CheckBox chkCreateAuto;
		System.Windows.Forms.Label lblSaveToEncrypt;
		System.Windows.Forms.Button encToBrowseBtn;
		System.Windows.Forms.TextBox encryptToTxt;
		TopControl ucTop;
		BottomControl ucBottom;
        private System.Windows.Forms.ProgressBar prbEncrypting;
        private System.Windows.Forms.ProgressBar prbDecrypting;
	}
}

