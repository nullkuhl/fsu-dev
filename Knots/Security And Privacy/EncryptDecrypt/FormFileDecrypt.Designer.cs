using System.Resources;
using System.Globalization;
using System.Threading;

namespace EncryptDecrypt
{
    partial class frmFileDecrypt
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
            this.chkOpenAfterDecryption = new System.Windows.Forms.CheckBox();
            this.chkDeleteEncrypted = new System.Windows.Forms.CheckBox();
            this.grbPassword = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.grbPath = new System.Windows.Forms.GroupBox();
            this.lblExtractTo = new System.Windows.Forms.Label();
            this.btnBrowseToExtract = new System.Windows.Forms.Button();
            this.txtExtractTo = new System.Windows.Forms.TextBox();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.prbDecrypting = new System.Windows.Forms.ProgressBar();
            this.grbPassword.SuspendLayout();
            this.grbPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkOpenAfterDecryption
            // 
            this.chkOpenAfterDecryption.AutoSize = true;
            this.chkOpenAfterDecryption.Location = new System.Drawing.Point(29, 145);
            this.chkOpenAfterDecryption.Name = "chkOpenAfterDecryption";
            this.chkOpenAfterDecryption.Size = new System.Drawing.Size(197, 17);
            this.chkOpenAfterDecryption.TabIndex = 20;
            this.chkOpenAfterDecryption.Text = "Open file after successful decryption";
            this.chkOpenAfterDecryption.UseVisualStyleBackColor = true;
            // 
            // chkDeleteEncrypted
            // 
            this.chkDeleteEncrypted.AutoSize = true;
            this.chkDeleteEncrypted.Location = new System.Drawing.Point(29, 166);
            this.chkDeleteEncrypted.Name = "chkDeleteEncrypted";
            this.chkDeleteEncrypted.Size = new System.Drawing.Size(229, 17);
            this.chkDeleteEncrypted.TabIndex = 18;
            this.chkDeleteEncrypted.Text = "Delete the encrypted file after decryption...!";
            this.chkDeleteEncrypted.UseVisualStyleBackColor = true;
            // 
            // grbPassword
            // 
            this.grbPassword.Controls.Add(this.txtPassword);
            this.grbPassword.Controls.Add(this.lblPassword);
            this.grbPassword.Location = new System.Drawing.Point(12, 84);
            this.grbPassword.Name = "grbPassword";
            this.grbPassword.Size = new System.Drawing.Size(462, 51);
            this.grbPassword.TabIndex = 17;
            this.grbPassword.TabStop = false;
            this.grbPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(140, 25);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(252, 20);
            this.txtPassword.TabIndex = 12;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(78, 28);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            // 
            // grbPath
            // 
            this.grbPath.Controls.Add(this.lblExtractTo);
            this.grbPath.Controls.Add(this.btnBrowseToExtract);
            this.grbPath.Controls.Add(this.txtExtractTo);
            this.grbPath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grbPath.Location = new System.Drawing.Point(12, 8);
            this.grbPath.Name = "grbPath";
            this.grbPath.Size = new System.Drawing.Size(462, 75);
            this.grbPath.TabIndex = 16;
            this.grbPath.TabStop = false;
            this.grbPath.Text = "Path Selection";
            // 
            // lblExtractTo
            // 
            this.lblExtractTo.AutoSize = true;
            this.lblExtractTo.Location = new System.Drawing.Point(8, 21);
            this.lblExtractTo.Name = "lblExtractTo";
            this.lblExtractTo.Size = new System.Drawing.Size(55, 13);
            this.lblExtractTo.TabIndex = 4;
            this.lblExtractTo.Text = "Extract to:";
            // 
            // btnBrowseToExtract
            // 
            this.btnBrowseToExtract.Location = new System.Drawing.Point(363, 38);
            this.btnBrowseToExtract.Name = "btnBrowseToExtract";
            this.btnBrowseToExtract.Size = new System.Drawing.Size(71, 24);
            this.btnBrowseToExtract.TabIndex = 11;
            this.btnBrowseToExtract.Text = "Browse...";
            this.btnBrowseToExtract.UseVisualStyleBackColor = true;
            this.btnBrowseToExtract.Click += new System.EventHandler(this.btnBrowseToExtract_Click);
            // 
            // txtExtractTo
            // 
            this.txtExtractTo.Location = new System.Drawing.Point(46, 42);
            this.txtExtractTo.Name = "txtExtractTo";
            this.txtExtractTo.Size = new System.Drawing.Size(311, 20);
            this.txtExtractTo.TabIndex = 10;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(300, 151);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(157, 26);
            this.btnDecrypt.TabIndex = 19;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // prbDecrypting
            // 
            this.prbDecrypting.Location = new System.Drawing.Point(12, 189);
            this.prbDecrypting.Name = "prbDecrypting";
            this.prbDecrypting.Size = new System.Drawing.Size(462, 12);
            this.prbDecrypting.TabIndex = 23;
            // 
            // frmFileDecrypt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 214);
            this.Controls.Add(this.prbDecrypting);
            this.Controls.Add(this.chkOpenAfterDecryption);
            this.Controls.Add(this.chkDeleteEncrypted);
            this.Controls.Add(this.grbPassword);
            this.Controls.Add(this.grbPath);
            this.Controls.Add(this.btnDecrypt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmFileDecrypt";
            this.Text = "File Decryption";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFileDecrypt_FormClosed);
            this.Load += new System.EventHandler(this.frmFileDecrypt_Load);
            this.grbPassword.ResumeLayout(false);
            this.grbPassword.PerformLayout();
            this.grbPath.ResumeLayout(false);
            this.grbPath.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		System.Windows.Forms.CheckBox chkOpenAfterDecryption;
        System.Windows.Forms.CheckBox chkDeleteEncrypted;
        System.Windows.Forms.GroupBox grbPassword;
        System.Windows.Forms.TextBox txtPassword;
        System.Windows.Forms.Label lblPassword;
        System.Windows.Forms.GroupBox grbPath;
        System.Windows.Forms.Label lblExtractTo;
        System.Windows.Forms.Button btnBrowseToExtract;
        System.Windows.Forms.TextBox txtExtractTo;
        System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.ProgressBar prbDecrypting;
    }
}