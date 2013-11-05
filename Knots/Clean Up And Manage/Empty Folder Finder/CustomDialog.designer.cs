namespace EmptyFolderFinder
{
	sealed partial class CustomDialog
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
            this.spcMain = new System.Windows.Forms.SplitContainer();
            this.tlpRight = new System.Windows.Forms.TableLayoutPanel();
            this.lblCaption = new System.Windows.Forms.Label();
            this.tlpLeft = new System.Windows.Forms.TableLayoutPanel();
            this.pcbMain = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnIgnoreAll = new System.Windows.Forms.Button();
            this.btnIgnore = new System.Windows.Forms.Button();
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.spcMain.Panel1.SuspendLayout();
            this.spcMain.Panel2.SuspendLayout();
            this.spcMain.SuspendLayout();
            this.tlpRight.SuspendLayout();
            this.tlpLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // spcMain
            // 
            this.spcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spcMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spcMain.IsSplitterFixed = true;
            this.spcMain.Location = new System.Drawing.Point(0, 0);
            this.spcMain.Name = "spcMain";
            this.spcMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcMain.Panel1
            // 
            this.spcMain.Panel1.Controls.Add(this.tlpRight);
            this.spcMain.Panel1.Controls.Add(this.tlpLeft);
            // 
            // spcMain.Panel2
            // 
            this.spcMain.Panel2.Controls.Add(this.btnIgnoreAll);
            this.spcMain.Panel2.Controls.Add(this.btnIgnore);
            this.spcMain.Panel2.Controls.Add(this.btnRetry);
            this.spcMain.Panel2.Controls.Add(this.btnAbort);
            this.spcMain.Size = new System.Drawing.Size(435, 170);
            this.spcMain.SplitterDistance = 129;
            this.spcMain.SplitterWidth = 1;
            this.spcMain.TabIndex = 13;
            // 
            // tlpRight
            // 
            this.tlpRight.BackColor = System.Drawing.SystemColors.Window;
            this.tlpRight.ColumnCount = 2;
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRight.Controls.Add(this.lblCaption, 1, 0);
            this.tlpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRight.Location = new System.Drawing.Point(0, 52);
            this.tlpRight.Name = "tlpRight";
            this.tlpRight.RowCount = 1;
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRight.Size = new System.Drawing.Size(435, 77);
            this.tlpRight.TabIndex = 4;
            // 
            // lblCaption
            // 
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCaption.Location = new System.Drawing.Point(68, 1);
            this.lblCaption.Margin = new System.Windows.Forms.Padding(3, 1, 16, 10);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(351, 66);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "Caption";
            // 
            // tlpLeft
            // 
            this.tlpLeft.BackColor = System.Drawing.SystemColors.Window;
            this.tlpLeft.ColumnCount = 2;
            this.tlpLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlpLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLeft.Controls.Add(this.pcbMain, 0, 0);
            this.tlpLeft.Controls.Add(this.lblMessage, 1, 0);
            this.tlpLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpLeft.Location = new System.Drawing.Point(0, 0);
            this.tlpLeft.Name = "tlpLeft";
            this.tlpLeft.RowCount = 1;
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tlpLeft.Size = new System.Drawing.Size(435, 52);
            this.tlpLeft.TabIndex = 3;
            // 
            // pcbMain
            // 
            this.pcbMain.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pcbMain.Location = new System.Drawing.Point(30, 12);
            this.pcbMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 8);
            this.pcbMain.Name = "pcbMain";
            this.pcbMain.Size = new System.Drawing.Size(32, 32);
            this.pcbMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbMain.TabIndex = 0;
            this.pcbMain.TabStop = false;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.Location = new System.Drawing.Point(68, 4);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(3, 4, 16, 10);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(351, 48);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Dialog message.";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnIgnoreAll
            // 
            this.btnIgnoreAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIgnoreAll.Location = new System.Drawing.Point(319, 11);
            this.btnIgnoreAll.Name = "btnIgnoreAll";
            this.btnIgnoreAll.Size = new System.Drawing.Size(95, 27);
            this.btnIgnoreAll.TabIndex = 16;
            this.btnIgnoreAll.Text = "Ignore All";
            this.btnIgnoreAll.UseVisualStyleBackColor = true;
            this.btnIgnoreAll.Click += new System.EventHandler(this.btnIgnoreAll_Click);
            // 
            // btnIgnore
            // 
            this.btnIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIgnore.Location = new System.Drawing.Point(218, 11);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(95, 27);
            this.btnIgnore.TabIndex = 15;
            this.btnIgnore.Text = "Ignore";
            this.btnIgnore.UseVisualStyleBackColor = true;
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // btnRetry
            // 
            this.btnRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRetry.Location = new System.Drawing.Point(117, 11);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(95, 27);
            this.btnRetry.TabIndex = 14;
            this.btnRetry.Text = "Retry";
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAbort.Location = new System.Drawing.Point(16, 11);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(95, 27);
            this.btnAbort.TabIndex = 13;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            // 
            // CustomDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(435, 178);
            this.Controls.Add(this.spcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Custom Dialog";
            this.Load += new System.EventHandler(this.CustomDialog_Load);
            this.spcMain.Panel1.ResumeLayout(false);
            this.spcMain.Panel2.ResumeLayout(false);
            this.spcMain.ResumeLayout(false);
            this.tlpRight.ResumeLayout(false);
            this.tlpLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbMain)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		System.Windows.Forms.SplitContainer spcMain;
		System.Windows.Forms.TableLayoutPanel tlpLeft;
		System.Windows.Forms.PictureBox pcbMain;
		System.Windows.Forms.Label lblMessage;
		System.Windows.Forms.Button btnIgnoreAll;
		System.Windows.Forms.Button btnIgnore;
		System.Windows.Forms.Button btnRetry;
		System.Windows.Forms.Button btnAbort;
		System.Windows.Forms.TableLayoutPanel tlpRight;
		System.Windows.Forms.Label lblCaption;

	}
}