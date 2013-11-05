namespace Disk_Cleaner
{
    partial class FormProcessing
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.pcbLoading = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(60, 14);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(77, 15);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Processing...";
            // 
            // pcbLoading
            // 
            this.pcbLoading.Image = global::Disk_Cleaner.Properties.Resources.scanning;
            this.pcbLoading.Location = new System.Drawing.Point(39, 13);
            this.pcbLoading.Name = "pcbLoading";
            this.pcbLoading.Size = new System.Drawing.Size(16, 16);
            this.pcbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbLoading.TabIndex = 0;
            this.pcbLoading.TabStop = false;
            // 
            // FormProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(160, 30);
            this.ControlBox = false;
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pcbLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormProcessing";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Processing";
            this.Load += new System.EventHandler(this.FormProcessing_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.PictureBox pcbLoading;
        System.Windows.Forms.Label lblStatus;
    }
}