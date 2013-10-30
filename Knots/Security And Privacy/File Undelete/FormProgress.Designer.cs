using System.Resources;
using System.Globalization;
using System.Threading;

namespace FileUndelete
{
    partial class FormProgress
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
			this.labelProgress = new System.Windows.Forms.Label();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelProgress
			// 
			this.labelProgress.Location = new System.Drawing.Point(12, 18);
			this.labelProgress.Name = "labelProgress";
			this.labelProgress.Size = new System.Drawing.Size(470, 34);
			this.labelProgress.TabIndex = 0;
			this.labelProgress.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(15, 46);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(467, 23);
			this.progressBar.Step = 1;
			this.progressBar.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(206, 84);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// FormProgress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(494, 122);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.labelProgress);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProgress";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Searching";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProgress_FormClosing);
			this.Load += new System.EventHandler(this.FormProgress_Load);
			this.ResumeLayout(false);

        }

        #endregion

        System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Label labelProgress;
        public System.Windows.Forms.ProgressBar progressBar;
    }
}