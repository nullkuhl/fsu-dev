using System.Resources;
using System.Globalization;
using System.Threading;

namespace FileUndelete
{
    partial class RecoverPathDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

        public ResourceManager rm = new ResourceManager("FileUndelete.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
			this.lblSelect = new System.Windows.Forms.Label();
			this.lblPath = new System.Windows.Forms.Label();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.btnNo = new System.Windows.Forms.Button();
			this.btnYes = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblSelect
			// 
			this.lblSelect.AutoSize = true;
			this.lblSelect.Location = new System.Drawing.Point(13, 13);
			this.lblSelect.Name = "lblSelect";
			this.lblSelect.Size = new System.Drawing.Size(35, 13);
			this.lblSelect.TabIndex = 0;
			this.lblSelect.Text = "label1";
			// 
			// lblPath
			// 
			this.lblPath.AutoSize = true;
			this.lblPath.Location = new System.Drawing.Point(13, 43);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(78, 13);
			this.lblPath.TabIndex = 1;
			this.lblPath.Text = "Recovery Path";
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(16, 59);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(263, 20);
			this.txtPath.TabIndex = 2;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(286, 57);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 3;
			this.btnBrowse.Text = "&Browse..";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// btnNo
			// 
			this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
			this.btnNo.Location = new System.Drawing.Point(286, 87);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new System.Drawing.Size(75, 23);
			this.btnNo.TabIndex = 4;
			this.btnNo.Text = "&No";
			this.btnNo.UseVisualStyleBackColor = true;
			this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
			// 
			// btnYes
			// 
			this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.btnYes.Location = new System.Drawing.Point(205, 87);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(75, 23);
			this.btnYes.TabIndex = 4;
			this.btnYes.Text = "&Yes";
			this.btnYes.UseVisualStyleBackColor = true;
			this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
			// 
			// RecoverPathDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(365, 115);
			this.Controls.Add(this.btnYes);
			this.Controls.Add(this.btnNo);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.lblPath);
			this.Controls.Add(this.lblSelect);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RecoverPathDialog";
			this.Text = "Confirm recover";
			this.Load += new System.EventHandler(this.RestorePathDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.Label lblSelect;
        System.Windows.Forms.Label lblPath;
        System.Windows.Forms.TextBox txtPath;
        System.Windows.Forms.Button btnBrowse;
        System.Windows.Forms.Button btnNo;
        System.Windows.Forms.Button btnYes;
    }
}