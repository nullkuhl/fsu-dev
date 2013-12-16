namespace ScanFiles
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.abortScanBtn = new System.Windows.Forms.Button();
            this.startScanBtn = new System.Windows.Forms.Button();
            this.cbxAdvancedSearch = new System.Windows.Forms.CheckBox();
            this.cbxImages = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.scanProgress = new System.Windows.Forms.Label();
            this.drivesCombo = new System.Windows.Forms.ComboBox();
            this.filesGrid = new System.Windows.Forms.DataGridView();
            this.restoreButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.isRecoverableColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filePathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scanTimer = new System.Windows.Forms.Timer(this.components);
            this.cbxRecycleBinSearch = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.CausesValidation = false;
            this.groupBox1.Controls.Add(this.abortScanBtn);
            this.groupBox1.Controls.Add(this.startScanBtn);
            this.groupBox1.Controls.Add(this.cbxRecycleBinSearch);
            this.groupBox1.Controls.Add(this.cbxAdvancedSearch);
            this.groupBox1.Controls.Add(this.cbxImages);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.scanProgress);
            this.groupBox1.Controls.Add(this.drivesCombo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Drive";
            // 
            // abortScanBtn
            // 
            this.abortScanBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.abortScanBtn.Enabled = false;
            this.abortScanBtn.Location = new System.Drawing.Point(516, 45);
            this.abortScanBtn.Name = "abortScanBtn";
            this.abortScanBtn.Size = new System.Drawing.Size(50, 23);
            this.abortScanBtn.TabIndex = 2;
            this.abortScanBtn.Text = "Abort";
            this.abortScanBtn.UseVisualStyleBackColor = true;
            this.abortScanBtn.Click += new System.EventHandler(this.abortScanBtn_Click);
            // 
            // startScanBtn
            // 
            this.startScanBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startScanBtn.Location = new System.Drawing.Point(516, 16);
            this.startScanBtn.Name = "startScanBtn";
            this.startScanBtn.Size = new System.Drawing.Size(50, 23);
            this.startScanBtn.TabIndex = 1;
            this.startScanBtn.Text = "Scan";
            this.startScanBtn.UseVisualStyleBackColor = true;
            this.startScanBtn.Click += new System.EventHandler(this.startScanBtn_Click);
            // 
            // cbxAdvancedSearch
            // 
            this.cbxAdvancedSearch.AutoSize = true;
            this.cbxAdvancedSearch.Location = new System.Drawing.Point(149, 34);
            this.cbxAdvancedSearch.Name = "cbxAdvancedSearch";
            this.cbxAdvancedSearch.Size = new System.Drawing.Size(112, 17);
            this.cbxAdvancedSearch.TabIndex = 6;
            this.cbxAdvancedSearch.Text = "Advanced Search";
            this.cbxAdvancedSearch.UseVisualStyleBackColor = true;
            // 
            // cbxImages
            // 
            this.cbxImages.AutoSize = true;
            this.cbxImages.Location = new System.Drawing.Point(149, 16);
            this.cbxImages.Name = "cbxImages";
            this.cbxImages.Size = new System.Drawing.Size(83, 17);
            this.cbxImages.TabIndex = 5;
            this.cbxImages.Text = "Only images";
            this.cbxImages.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(277, 49);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(233, 11);
            this.progressBar1.TabIndex = 4;
            // 
            // scanProgress
            // 
            this.scanProgress.AutoSize = true;
            this.scanProgress.Location = new System.Drawing.Point(274, 23);
            this.scanProgress.Name = "scanProgress";
            this.scanProgress.Size = new System.Drawing.Size(83, 13);
            this.scanProgress.TabIndex = 3;
            this.scanProgress.Text = "Progress not set";
            // 
            // drivesCombo
            // 
            this.drivesCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drivesCombo.FormattingEnabled = true;
            this.drivesCombo.Location = new System.Drawing.Point(13, 20);
            this.drivesCombo.Name = "drivesCombo";
            this.drivesCombo.Size = new System.Drawing.Size(121, 21);
            this.drivesCombo.TabIndex = 0;
            // 
            // filesGrid
            // 
            this.filesGrid.AllowUserToAddRows = false;
            this.filesGrid.AllowUserToDeleteRows = false;
            this.filesGrid.AllowUserToOrderColumns = true;
            this.filesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.filesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.filesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.restoreButtonColumn,
            this.isRecoverableColumn,
            this.sizeColumn,
            this.filePathColumn,
            this.fileIdColumn});
            this.filesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesGrid.Location = new System.Drawing.Point(0, 74);
            this.filesGrid.Name = "filesGrid";
            this.filesGrid.ReadOnly = true;
            this.filesGrid.RowHeadersVisible = false;
            this.filesGrid.ShowCellErrors = false;
            this.filesGrid.ShowEditingIcon = false;
            this.filesGrid.ShowRowErrors = false;
            this.filesGrid.Size = new System.Drawing.Size(578, 237);
            this.filesGrid.TabIndex = 1;
            this.filesGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.filesGrid_CellContentClick);
            // 
            // restoreButtonColumn
            // 
            this.restoreButtonColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.restoreButtonColumn.FillWeight = 40F;
            this.restoreButtonColumn.HeaderText = "Action";
            this.restoreButtonColumn.MinimumWidth = 40;
            this.restoreButtonColumn.Name = "restoreButtonColumn";
            this.restoreButtonColumn.ReadOnly = true;
            this.restoreButtonColumn.Text = "Restore";
            this.restoreButtonColumn.UseColumnTextForButtonValue = true;
            this.restoreButtonColumn.Width = 60;
            // 
            // isRecoverableColumn
            // 
            this.isRecoverableColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.isRecoverableColumn.DataPropertyName = "isRecoverable";
            this.isRecoverableColumn.FillWeight = 40F;
            this.isRecoverableColumn.HeaderText = "Recoverable";
            this.isRecoverableColumn.MinimumWidth = 50;
            this.isRecoverableColumn.Name = "isRecoverableColumn";
            this.isRecoverableColumn.ReadOnly = true;
            this.isRecoverableColumn.Width = 70;
            // 
            // sizeColumn
            // 
            this.sizeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sizeColumn.DataPropertyName = "size";
            this.sizeColumn.FillWeight = 50F;
            this.sizeColumn.HeaderText = "Size";
            this.sizeColumn.MinimumWidth = 40;
            this.sizeColumn.Name = "sizeColumn";
            this.sizeColumn.ReadOnly = true;
            this.sizeColumn.Width = 70;
            // 
            // filePathColumn
            // 
            this.filePathColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.filePathColumn.DataPropertyName = "filePath";
            this.filePathColumn.FillWeight = 200F;
            this.filePathColumn.HeaderText = "File Path";
            this.filePathColumn.MinimumWidth = 150;
            this.filePathColumn.Name = "filePathColumn";
            this.filePathColumn.ReadOnly = true;
            this.filePathColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // fileIdColumn
            // 
            this.fileIdColumn.DataPropertyName = "fileId";
            this.fileIdColumn.FillWeight = 50F;
            this.fileIdColumn.HeaderText = "File Id";
            this.fileIdColumn.MaxInputLength = 0;
            this.fileIdColumn.MinimumWidth = 50;
            this.fileIdColumn.Name = "fileIdColumn";
            this.fileIdColumn.ReadOnly = true;
            // 
            // scanTimer
            // 
            this.scanTimer.Interval = 400;
            this.scanTimer.Tick += new System.EventHandler(this.scanTimer_Tick);
            // 
            // cbxRecycleBinSearch
            // 
            this.cbxRecycleBinSearch.AutoSize = true;
            this.cbxRecycleBinSearch.Location = new System.Drawing.Point(149, 51);
            this.cbxRecycleBinSearch.Name = "cbxRecycleBinSearch";
            this.cbxRecycleBinSearch.Size = new System.Drawing.Size(120, 17);
            this.cbxRecycleBinSearch.TabIndex = 6;
            this.cbxRecycleBinSearch.Text = "Recycle Bin Search";
            this.cbxRecycleBinSearch.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 311);
            this.Controls.Add(this.filesGrid);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(586, 340);
            this.Name = "Form1";
            this.Text = "Recovery for deleted files";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label scanProgress;
        private System.Windows.Forms.Button abortScanBtn;
        private System.Windows.Forms.Button startScanBtn;
        private System.Windows.Forms.ComboBox drivesCombo;
        private System.Windows.Forms.DataGridView filesGrid;
        private System.Windows.Forms.Timer scanTimer;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridViewButtonColumn restoreButtonColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isRecoverableColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sizeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filePathColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileIdColumn;
        private System.Windows.Forms.CheckBox cbxImages;
        private System.Windows.Forms.CheckBox cbxAdvancedSearch;
        private System.Windows.Forms.CheckBox cbxRecycleBinSearch;

    }
}

