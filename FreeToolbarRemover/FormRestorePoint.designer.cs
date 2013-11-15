namespace FreeToolbarRemover
{
	partial class FormRestorePoint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRestorePoint));
            this.tabRestore = new System.Windows.Forms.TabControl();
            this.tabPageRestore = new System.Windows.Forms.TabPage();
            this.btnDelete = new System.Windows.Forms.Button();
            this.listRestorePoint = new System.Windows.Forms.ListView();
            this.SequenceNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Backups = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CreationDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblSelectRestorePoint = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabRestore.SuspendLayout();
            this.tabPageRestore.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabRestore
            // 
            this.tabRestore.Controls.Add(this.tabPageRestore);
            this.tabRestore.Location = new System.Drawing.Point(1, 1);
            this.tabRestore.Name = "tabRestore";
            this.tabRestore.SelectedIndex = 0;
            this.tabRestore.Size = new System.Drawing.Size(642, 336);
            this.tabRestore.TabIndex = 0;
            // 
            // tabPageRestore
            // 
            this.tabPageRestore.Controls.Add(this.btnDelete);
            this.tabPageRestore.Controls.Add(this.listRestorePoint);
            this.tabPageRestore.Controls.Add(this.btnRestore);
            this.tabPageRestore.Controls.Add(this.btnCreate);
            this.tabPageRestore.Controls.Add(this.lblSelectRestorePoint);
            this.tabPageRestore.Location = new System.Drawing.Point(4, 22);
            this.tabPageRestore.Name = "tabPageRestore";
            this.tabPageRestore.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRestore.Size = new System.Drawing.Size(634, 310);
            this.tabPageRestore.TabIndex = 0;
            this.tabPageRestore.Text = "System Restore";
            this.tabPageRestore.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(225, 276);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(208, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete Restore Point";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // listRestorePoint
            // 
            this.listRestorePoint.AllowColumnReorder = true;
            this.listRestorePoint.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SequenceNo,
            this.Backups,
            this.CreationDate});
            this.listRestorePoint.FullRowSelect = true;
            this.listRestorePoint.Location = new System.Drawing.Point(12, 29);
            this.listRestorePoint.MultiSelect = false;
            this.listRestorePoint.Name = "listRestorePoint";
            this.listRestorePoint.ShowGroups = false;
            this.listRestorePoint.Size = new System.Drawing.Size(612, 236);
            this.listRestorePoint.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listRestorePoint.TabIndex = 3;
            this.listRestorePoint.UseCompatibleStateImageBehavior = false;
            this.listRestorePoint.View = System.Windows.Forms.View.Details;
            this.listRestorePoint.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listRestorePoint_ColumnClick);
            // 
            // SequenceNo
            // 
            this.SequenceNo.Text = "Sequence No";
            this.SequenceNo.Width = 0;
            // 
            // Backups
            // 
            this.Backups.Text = "Backups";
            this.Backups.Width = 306;
            // 
            // CreationDate
            // 
            this.CreationDate.Text = "CreationDate";
            this.CreationDate.Width = 306;
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(517, 276);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(106, 23);
            this.btnRestore.TabIndex = 2;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(11, 276);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(208, 23);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "Create Restore Point";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lblSelectRestorePoint
            // 
            this.lblSelectRestorePoint.AutoSize = true;
            this.lblSelectRestorePoint.Location = new System.Drawing.Point(8, 13);
            this.lblSelectRestorePoint.Name = "lblSelectRestorePoint";
            this.lblSelectRestorePoint.Size = new System.Drawing.Size(399, 13);
            this.lblSelectRestorePoint.TabIndex = 0;
            this.lblSelectRestorePoint.Text = "Please select a restore point you\'d like to restore to, and then click \'Restore\' " +
                "button.";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(523, 343);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormRestorePoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 373);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabRestore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRestorePoint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Restore & Backup Center";
            this.Load += new System.EventHandler(this.FrmRestorePoint_Load);
            this.tabRestore.ResumeLayout(false);
            this.tabPageRestore.ResumeLayout(false);
            this.tabPageRestore.PerformLayout();
            this.ResumeLayout(false);

        }

		#endregion

		System.Windows.Forms.TabControl tabRestore;
		System.Windows.Forms.TabPage tabPageRestore;
		System.Windows.Forms.Button btnRestore;
		System.Windows.Forms.Button btnCreate;
		System.Windows.Forms.Label lblSelectRestorePoint;
		System.Windows.Forms.Button btnClose;
		System.Windows.Forms.ListView listRestorePoint;
		System.Windows.Forms.ColumnHeader SequenceNo;
		System.Windows.Forms.ColumnHeader Backups;
		System.Windows.Forms.ColumnHeader CreationDate;
		System.Windows.Forms.Button btnDelete;

	}
}