namespace FileUndelete
{
    partial class FormBusy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

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
            this.pcbLoading = new System.Windows.Forms.PictureBox();
            this.lblWait = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbLoading
            // 
            this.pcbLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pcbLoading.Image = global::FileUndelete.Properties.Resources.wait;
            this.pcbLoading.Location = new System.Drawing.Point(27, 12);
            this.pcbLoading.Name = "pcbLoading";
            this.pcbLoading.Size = new System.Drawing.Size(22, 20);
            this.pcbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcbLoading.TabIndex = 0;
            this.pcbLoading.TabStop = false;
            // 
            // lblWait
            // 
            this.lblWait.AutoSize = true;
            this.lblWait.Location = new System.Drawing.Point(63, 15);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(0, 13);
            this.lblWait.TabIndex = 1;
            // 
            // FormBusy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 43);
            this.ControlBox = false;
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.pcbLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBusy";
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
        System.Windows.Forms.Label lblWait;


    }
}