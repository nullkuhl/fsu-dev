using System.Resources;
using System.Globalization;
using System.Threading;

namespace ShortcutsFixer
{
    partial class Restore
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("ShortcutsFixer.Resources",
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
			this.dgwMain = new System.Windows.Forms.DataGridView();
			this.buttonRestore = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgwMain)).BeginInit();
			this.SuspendLayout();
			// 
			// dgwMain
			// 
			this.dgwMain.AllowUserToAddRows = false;
			this.dgwMain.AllowUserToDeleteRows = false;
			this.dgwMain.AllowUserToOrderColumns = true;
			this.dgwMain.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			this.dgwMain.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dgwMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgwMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgwMain.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dgwMain.Location = new System.Drawing.Point(0, 2);
			this.dgwMain.Name = "dgwMain";
			this.dgwMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgwMain.RowHeadersVisible = false;
			this.dgwMain.RowTemplate.Height = 24;
			this.dgwMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgwMain.Size = new System.Drawing.Size(517, 297);
			this.dgwMain.TabIndex = 0;
			// 
			// buttonRestore
			// 
			this.buttonRestore.Location = new System.Drawing.Point(394, 305);
			this.buttonRestore.Name = "buttonRestore";
			this.buttonRestore.Size = new System.Drawing.Size(116, 24);
			this.buttonRestore.TabIndex = 1;
			this.buttonRestore.Text = "Restore";
			this.buttonRestore.UseVisualStyleBackColor = true;
			this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(272, 305);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(116, 24);
			this.buttonDelete.TabIndex = 2;
			this.buttonDelete.Text = "Delete";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// Restore
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(516, 334);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonRestore);
			this.Controls.Add(this.dgwMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Restore";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Restore";
			this.Load += new System.EventHandler(this.Restore_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgwMain)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        System.Windows.Forms.DataGridView dgwMain;
        System.Windows.Forms.Button buttonRestore;
        System.Windows.Forms.Button buttonDelete;
    }
}