using System.Resources;
using System.Globalization;
using System.Threading;

namespace FileUndelete
{
    partial class frmFilter
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
			this.grbMain = new System.Windows.Forms.GroupBox();
			this.chkIncludeNonRecoverable = new System.Windows.Forms.CheckBox();
			this.lblSize = new System.Windows.Forms.Label();
			this.nudSize = new System.Windows.Forms.NumericUpDown();
			this.cboSize = new System.Windows.Forms.ComboBox();
			this.chkSize = new System.Windows.Forms.CheckBox();
			this.lblFilename = new System.Windows.Forms.Label();
			this.txtFilename = new System.Windows.Forms.TextBox();
			this.chkFilename = new System.Windows.Forms.CheckBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.grbMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
			this.SuspendLayout();
			// 
			// grbMain
			// 
			this.grbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grbMain.Controls.Add(this.chkIncludeNonRecoverable);
			this.grbMain.Controls.Add(this.lblSize);
			this.grbMain.Controls.Add(this.nudSize);
			this.grbMain.Controls.Add(this.cboSize);
			this.grbMain.Controls.Add(this.chkSize);
			this.grbMain.Controls.Add(this.lblFilename);
			this.grbMain.Controls.Add(this.txtFilename);
			this.grbMain.Controls.Add(this.chkFilename);
			this.grbMain.Location = new System.Drawing.Point(12, 12);
			this.grbMain.Name = "grbMain";
			this.grbMain.Size = new System.Drawing.Size(410, 179);
			this.grbMain.TabIndex = 0;
			this.grbMain.TabStop = false;
			this.grbMain.Text = "Filter";
			// 
			// chkIncludeNonRecoverable
			// 
			this.chkIncludeNonRecoverable.AutoSize = true;
			this.chkIncludeNonRecoverable.Location = new System.Drawing.Point(15, 144);
			this.chkIncludeNonRecoverable.Name = "chkIncludeNonRecoverable";
			this.chkIncludeNonRecoverable.Size = new System.Drawing.Size(184, 17);
			this.chkIncludeNonRecoverable.TabIndex = 12;
			this.chkIncludeNonRecoverable.Text = "Also include non recoverable files";
			this.chkIncludeNonRecoverable.UseVisualStyleBackColor = true;
			// 
			// lblSize
			// 
			this.lblSize.AutoSize = true;
			this.lblSize.Location = new System.Drawing.Point(362, 102);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(21, 13);
			this.lblSize.TabIndex = 6;
			this.lblSize.Text = "KB";
			// 
			// nudSize
			// 
			this.nudSize.DecimalPlaces = 2;
			this.nudSize.Location = new System.Drawing.Point(236, 100);
			this.nudSize.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
			this.nudSize.Name = "nudSize";
			this.nudSize.Size = new System.Drawing.Size(120, 20);
			this.nudSize.TabIndex = 5;
			this.nudSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cboSize
			// 
			this.cboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSize.FormattingEnabled = true;
			this.cboSize.Items.AddRange(new object[] {
            "More than",
            "Less than"});
			this.cboSize.Location = new System.Drawing.Point(139, 99);
			this.cboSize.Name = "cboSize";
			this.cboSize.Size = new System.Drawing.Size(91, 21);
			this.cboSize.TabIndex = 4;
			// 
			// chkSize
			// 
			this.chkSize.AutoSize = true;
			this.chkSize.Location = new System.Drawing.Point(15, 101);
			this.chkSize.Name = "chkSize";
			this.chkSize.Size = new System.Drawing.Size(61, 17);
			this.chkSize.TabIndex = 3;
			this.chkSize.Text = "By Size";
			this.chkSize.UseVisualStyleBackColor = true;
			// 
			// lblFilename
			// 
			this.lblFilename.Location = new System.Drawing.Point(12, 42);
			this.lblFilename.Name = "lblFilename";
			this.lblFilename.Size = new System.Drawing.Size(383, 54);
			this.lblFilename.TabIndex = 2;
			this.lblFilename.Text = "All or part of the file Name. You can use wildcards, eg. *.doc.\r\n? Match one char" +
    " acter of anything\r\n* Match any number of anything";
			// 
			// txtFilename
			// 
			this.txtFilename.Location = new System.Drawing.Point(139, 19);
			this.txtFilename.Name = "txtFilename";
			this.txtFilename.Size = new System.Drawing.Size(256, 20);
			this.txtFilename.TabIndex = 1;
			// 
			// chkFilename
			// 
			this.chkFilename.AutoSize = true;
			this.chkFilename.Location = new System.Drawing.Point(15, 21);
			this.chkFilename.Name = "chkFilename";
			this.chkFilename.Size = new System.Drawing.Size(83, 17);
			this.chkFilename.TabIndex = 0;
			this.chkFilename.Text = "By Filename";
			this.chkFilename.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(257, 202);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(347, 202);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// frmFilter
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(434, 237);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.grbMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmFilter";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Filter";
			this.grbMain.ResumeLayout(false);
			this.grbMain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        System.Windows.Forms.GroupBox grbMain;
        System.Windows.Forms.Button btnOK;
        System.Windows.Forms.Button btnCancel;
        System.Windows.Forms.Label lblSize;
        System.Windows.Forms.Label lblFilename;
        public System.Windows.Forms.NumericUpDown nudSize;
        public System.Windows.Forms.ComboBox cboSize;
        public System.Windows.Forms.CheckBox chkSize;
        public System.Windows.Forms.TextBox txtFilename;
        public System.Windows.Forms.CheckBox chkFilename;
        public System.Windows.Forms.CheckBox chkIncludeNonRecoverable;
    }
}