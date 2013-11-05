using System.Resources;
using System.Globalization;
using System.Threading;

namespace Disk_Cleaner
{
    partial class FormRestore
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

        public ResourceManager rm = new ResourceManager("Disk_Cleaner.Resources",
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
			this.lblSelect = new System.Windows.Forms.Label();
			this.lvBackups = new System.Windows.Forms.ListView();
			this.clhDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.clhSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chbOwerwrite = new System.Windows.Forms.CheckBox();
			this.Delete = new System.Windows.Forms.Button();
			this.Restore = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblSelect
			// 
			this.lblSelect.Location = new System.Drawing.Point(12, 9);
			this.lblSelect.Name = "lblSelect";
			this.lblSelect.Size = new System.Drawing.Size(370, 40);
			this.lblSelect.TabIndex = 0;
			this.lblSelect.Text = "Select Backup.";
			// 
			// lvBackups
			// 
			this.lvBackups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhDate,
            this.clhSize});
			this.lvBackups.FullRowSelect = true;
			this.lvBackups.HideSelection = false;
			this.lvBackups.Location = new System.Drawing.Point(15, 52);
			this.lvBackups.Name = "lvBackups";
			this.lvBackups.Size = new System.Drawing.Size(367, 203);
			this.lvBackups.TabIndex = 1;
			this.lvBackups.UseCompatibleStateImageBehavior = false;
			this.lvBackups.View = System.Windows.Forms.View.Details;
			// 
			// clhDate
			// 
			this.clhDate.Text = "Creation Date";
			this.clhDate.Width = 240;
			// 
			// clhSize
			// 
			this.clhSize.Text = "Size";
			this.clhSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.clhSize.Width = 100;
			// 
			// chbOwerwrite
			// 
			this.chbOwerwrite.AutoSize = true;
			this.chbOwerwrite.Location = new System.Drawing.Point(15, 261);
			this.chbOwerwrite.Name = "chbOwerwrite";
			this.chbOwerwrite.Size = new System.Drawing.Size(202, 17);
			this.chbOwerwrite.TabIndex = 2;
			this.chbOwerwrite.Text = "Overwrite existing files when restoring";
			this.chbOwerwrite.UseVisualStyleBackColor = true;
			// 
			// Delete
			// 
			this.Delete.Location = new System.Drawing.Point(15, 287);
			this.Delete.Name = "Delete";
			this.Delete.Size = new System.Drawing.Size(75, 23);
			this.Delete.TabIndex = 3;
			this.Delete.Text = "Delete";
			this.Delete.UseVisualStyleBackColor = true;
			this.Delete.Click += new System.EventHandler(this.Delete_Click);
			// 
			// Restore
			// 
			this.Restore.Location = new System.Drawing.Point(195, 287);
			this.Restore.Name = "Restore";
			this.Restore.Size = new System.Drawing.Size(106, 23);
			this.Restore.TabIndex = 4;
			this.Restore.Text = "Restore";
			this.Restore.UseVisualStyleBackColor = true;
			this.Restore.Click += new System.EventHandler(this.Restore_Click);
			// 
			// Cancel
			// 
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(307, 287);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(75, 23);
			this.Cancel.TabIndex = 5;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// FormRestore
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Cancel;
			this.ClientSize = new System.Drawing.Size(394, 322);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.Restore);
			this.Controls.Add(this.Delete);
			this.Controls.Add(this.chbOwerwrite);
			this.Controls.Add(this.lvBackups);
			this.Controls.Add(this.lblSelect);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRestore";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Restore";
			this.Load += new System.EventHandler(this.FormRestore_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.Label lblSelect;
        System.Windows.Forms.ListView lvBackups;
        System.Windows.Forms.ColumnHeader clhDate;
        System.Windows.Forms.ColumnHeader clhSize;
        System.Windows.Forms.CheckBox chbOwerwrite;
        System.Windows.Forms.Button Delete;
        System.Windows.Forms.Button Restore;
        System.Windows.Forms.Button Cancel;
    }
}