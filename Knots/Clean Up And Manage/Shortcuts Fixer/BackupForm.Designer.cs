using System.ComponentModel;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace ShortcutsFixer
{
    partial class BackupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        IContainer components = null;

		public ResourceManager rm = new ResourceManager("ShortcutsFixer.Resources", System.Reflection.Assembly.GetExecutingAssembly());

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
			this.lsbMain = new System.Windows.Forms.ListBox();
			this.lblRestorePoints = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lsbMain
			// 
			this.lsbMain.FormattingEnabled = true;
			this.lsbMain.Location = new System.Drawing.Point(3, 46);
			this.lsbMain.Name = "lsbMain";
			this.lsbMain.Size = new System.Drawing.Size(425, 173);
			this.lsbMain.TabIndex = 0;
			this.lsbMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsbMain_MouseDoubleClick);
			// 
			// lblRestorePoints
			// 
			this.lblRestorePoints.AutoSize = true;
			this.lblRestorePoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRestorePoints.Location = new System.Drawing.Point(5, 15);
			this.lblRestorePoints.Name = "lblRestorePoints";
			this.lblRestorePoints.Size = new System.Drawing.Size(280, 13);
			this.lblRestorePoints.TabIndex = 1;
			this.lblRestorePoints.Text = "Following are the Restore Points. Double Click to Restore.";
			// 
			// Backup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(430, 221);
			this.Controls.Add(this.lblRestorePoints);
			this.Controls.Add(this.lsbMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Backup";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Backup";
			this.Load += new System.EventHandler(this.Backup_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.ListBox lsbMain;
        System.Windows.Forms.Label lblRestorePoints;

    }
}