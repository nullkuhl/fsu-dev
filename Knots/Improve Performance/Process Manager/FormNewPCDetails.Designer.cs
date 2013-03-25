using System.Threading;
using System.Resources;
using System.Globalization;

namespace ProcessManager
{
    partial class FormNewPCDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

        public ResourceManager rm = new ResourceManager("ProcessManager.Resources",
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
			this.txtapplnpath = new System.Windows.Forms.TextBox();
			this.btnok = new System.Windows.Forms.Button();
			this.btncancel = new System.Windows.Forms.Button();
			this.btnbrowse = new System.Windows.Forms.Button();
			this.lblOpen = new System.Windows.Forms.Label();
			this.ofdMain = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// txtapplnpath
			// 
			this.txtapplnpath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtapplnpath.Location = new System.Drawing.Point(80, 16);
			this.txtapplnpath.Name = "txtapplnpath";
			this.txtapplnpath.Size = new System.Drawing.Size(248, 20);
			this.txtapplnpath.TabIndex = 0;
			// 
			// btnok
			// 
			this.btnok.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnok.Location = new System.Drawing.Point(32, 56);
			this.btnok.Name = "btnok";
			this.btnok.Size = new System.Drawing.Size(75, 23);
			this.btnok.TabIndex = 1;
			this.btnok.Text = "&Ok";
			this.btnok.Click += new System.EventHandler(this.btnok_Click);
			// 
			// btncancel
			// 
			this.btncancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btncancel.Location = new System.Drawing.Point(136, 56);
			this.btncancel.Name = "btncancel";
			this.btncancel.Size = new System.Drawing.Size(75, 23);
			this.btncancel.TabIndex = 2;
			this.btncancel.Text = "&Cancel";
			this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
			// 
			// btnbrowse
			// 
			this.btnbrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnbrowse.Location = new System.Drawing.Point(240, 56);
			this.btnbrowse.Name = "btnbrowse";
			this.btnbrowse.Size = new System.Drawing.Size(75, 23);
			this.btnbrowse.TabIndex = 3;
			this.btnbrowse.Text = "&Browse";
			this.btnbrowse.Click += new System.EventHandler(this.btnbrowse_Click);
			// 
			// lblOpen
			// 
			this.lblOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblOpen.Location = new System.Drawing.Point(16, 16);
			this.lblOpen.Name = "lblOpen";
			this.lblOpen.Size = new System.Drawing.Size(56, 23);
			this.lblOpen.TabIndex = 4;
			this.lblOpen.Text = "Open";
			// 
			// ofdMain
			// 
			this.ofdMain.AddExtension = false;
			this.ofdMain.CheckFileExists = false;
			this.ofdMain.CheckPathExists = false;
			this.ofdMain.DereferenceLinks = false;
			this.ofdMain.RestoreDirectory = true;
			this.ofdMain.ValidateNames = false;
			// 
			// FormNewPCDetails
			// 
			this.AcceptButton = this.btnok;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btncancel;
			this.ClientSize = new System.Drawing.Size(336, 96);
			this.ControlBox = false;
			this.Controls.Add(this.lblOpen);
			this.Controls.Add(this.btnbrowse);
			this.Controls.Add(this.btncancel);
			this.Controls.Add(this.btnok);
			this.Controls.Add(this.txtapplnpath);
			this.Name = "FormNewPCDetails";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.frmnewprcdetails_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }
        #endregion
    }
}