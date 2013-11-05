namespace RegistryCleaner
{
    partial class FrmBusy
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
			this.pcbLoading = new System.Windows.Forms.PictureBox();
			this.lblStatus = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pcbLoading)).BeginInit();
			this.SuspendLayout();
			// 
			// pcbLoading
			// 
			this.pcbLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pcbLoading.Image = global::RegistryCleaner.Properties.Resources.ajax_loader;
			this.pcbLoading.Location = new System.Drawing.Point(32, 12);
			this.pcbLoading.Name = "pcbLoading";
			this.pcbLoading.Size = new System.Drawing.Size(22, 20);
			this.pcbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pcbLoading.TabIndex = 0;
			this.pcbLoading.TabStop = false;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(63, 15);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(0, 13);
			this.lblStatus.TabIndex = 1;
			// 
			// FrmBusy
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(162, 44);
			this.ControlBox = false;
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.pcbLoading);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmBusy";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Processing";
			this.Load += new System.EventHandler(this.FrmBusy_Load);
			((System.ComponentModel.ISupportInitialize)(this.pcbLoading)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.PictureBox pcbLoading;
        System.Windows.Forms.Label lblStatus;


    }
}