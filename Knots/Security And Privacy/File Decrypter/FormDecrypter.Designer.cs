using System.Resources;
using System.Globalization;
using System.Threading;

namespace Decrypter
{
    partial class FormDecrypter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDecrypter));
            this.passwordLbl = new System.Windows.Forms.Label();
            this.passwordTxt = new System.Windows.Forms.TextBox();
            this.extractToLbl = new System.Windows.Forms.Label();
            this.extractToTxt = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.processLbl = new System.Windows.Forms.Label();
            this.processPrgrss = new System.Windows.Forms.ProgressBar();
            this.decryptBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // passwordLbl
            // 
            this.passwordLbl.AutoSize = true;
            this.passwordLbl.Location = new System.Drawing.Point(12, 18);
            this.passwordLbl.Name = "passwordLbl";
            this.passwordLbl.Size = new System.Drawing.Size(123, 13);
            this.passwordLbl.TabIndex = 0;
            this.passwordLbl.Text = "Password for decryption:";
            // 
            // passwordTxt
            // 
            this.passwordTxt.Location = new System.Drawing.Point(146, 15);
            this.passwordTxt.Name = "passwordTxt";
            this.passwordTxt.Size = new System.Drawing.Size(153, 20);
            this.passwordTxt.TabIndex = 1;
            this.passwordTxt.UseSystemPasswordChar = true;
            // 
            // extractToLbl
            // 
            this.extractToLbl.AutoSize = true;
            this.extractToLbl.Location = new System.Drawing.Point(12, 51);
            this.extractToLbl.Name = "extractToLbl";
            this.extractToLbl.Size = new System.Drawing.Size(55, 13);
            this.extractToLbl.TabIndex = 3;
            this.extractToLbl.Text = "Extract to:";
            // 
            // extractToTxt
            // 
            this.extractToTxt.Location = new System.Drawing.Point(77, 48);
            this.extractToTxt.Name = "extractToTxt";
            this.extractToTxt.Size = new System.Drawing.Size(365, 20);
            this.extractToTxt.TabIndex = 4;
            // 
            // browseBtn
            // 
            this.browseBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.browseBtn.Location = new System.Drawing.Point(442, 46);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(70, 23);
            this.browseBtn.TabIndex = 5;
            this.browseBtn.Text = "&Browse...";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // processLbl
            // 
            this.processLbl.AutoSize = true;
            this.processLbl.Location = new System.Drawing.Point(12, 84);
            this.processLbl.Name = "processLbl";
            this.processLbl.Size = new System.Drawing.Size(48, 13);
            this.processLbl.TabIndex = 6;
            this.processLbl.Text = "Process:";
            // 
            // processPrgrss
            // 
            this.processPrgrss.Location = new System.Drawing.Point(15, 104);
            this.processPrgrss.Name = "processPrgrss";
            this.processPrgrss.Size = new System.Drawing.Size(497, 19);
            this.processPrgrss.TabIndex = 7;
            // 
            // decryptBtn
            // 
            this.decryptBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.decryptBtn.Location = new System.Drawing.Point(428, 142);
            this.decryptBtn.Name = "decryptBtn";
            this.decryptBtn.Size = new System.Drawing.Size(84, 23);
            this.decryptBtn.TabIndex = 8;
            this.decryptBtn.Text = "&Decrypt File";
            this.decryptBtn.UseVisualStyleBackColor = true;
            this.decryptBtn.Click += new System.EventHandler(this.decryptBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelBtn.Location = new System.Drawing.Point(338, 142);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(84, 23);
            this.cancelBtn.TabIndex = 9;
            this.cancelBtn.Text = "&Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // DecryptFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 177);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.decryptBtn);
            this.Controls.Add(this.processPrgrss);
            this.Controls.Add(this.processLbl);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.extractToTxt);
            this.Controls.Add(this.extractToLbl);
            this.Controls.Add(this.passwordTxt);
            this.Controls.Add(this.passwordLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DecryptFrm";
            this.Text = "Decrypt File";
            this.Load += new System.EventHandler(this.DecryptFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.Label passwordLbl;
        System.Windows.Forms.TextBox passwordTxt;
        System.Windows.Forms.Label extractToLbl;
        System.Windows.Forms.TextBox extractToTxt;
        System.Windows.Forms.Button browseBtn;
        System.Windows.Forms.Label processLbl;
        System.Windows.Forms.ProgressBar processPrgrss;
        System.Windows.Forms.Button decryptBtn;
        System.Windows.Forms.Button cancelBtn;
        System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

