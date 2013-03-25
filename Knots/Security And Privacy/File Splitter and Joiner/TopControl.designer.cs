namespace FileSplitterAndJoiner
{
    partial class TopControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopControl));
            this.tlpLeft = new System.Windows.Forms.TableLayoutPanel();
            this.lblBottomStrip = new System.Windows.Forms.Label();
            this.tlpCenter = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRight = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            this.lblBar = new System.Windows.Forms.Label();
            this.tlpLeft.SuspendLayout();
            this.tlpCenter.SuspendLayout();
            this.tlpRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpLeft
            // 
            this.tlpLeft.ColumnCount = 1;
            this.tlpLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLeft.Controls.Add(this.lblBottomStrip, 0, 1);
            this.tlpLeft.Controls.Add(this.tlpCenter, 0, 0);
            this.tlpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLeft.Location = new System.Drawing.Point(0, 0);
            this.tlpLeft.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLeft.Name = "tlpLeft";
            this.tlpLeft.RowCount = 2;
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tlpLeft.Size = new System.Drawing.Size(615, 64);
            this.tlpLeft.TabIndex = 1;
            // 
            // lblBottomStrip
            // 
            this.lblBottomStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBottomStrip.Image = ((System.Drawing.Image)(resources.GetObject("lblBottomStrip.Image")));
            this.lblBottomStrip.Location = new System.Drawing.Point(0, 57);
            this.lblBottomStrip.Margin = new System.Windows.Forms.Padding(0);
            this.lblBottomStrip.Name = "lblBottomStrip";
            this.lblBottomStrip.Size = new System.Drawing.Size(615, 7);
            this.lblBottomStrip.TabIndex = 0;
            // 
            // tlpCenter
            // 
            this.tlpCenter.ColumnCount = 2;
            this.tlpCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tlpCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCenter.Controls.Add(this.tlpRight, 1, 0);
            this.tlpCenter.Controls.Add(this.lblBar, 0, 0);
            this.tlpCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCenter.Location = new System.Drawing.Point(0, 0);
            this.tlpCenter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpCenter.Name = "tlpCenter";
            this.tlpCenter.RowCount = 1;
            this.tlpCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCenter.Size = new System.Drawing.Size(615, 57);
            this.tlpCenter.TabIndex = 1;
            // 
            // tlpRight
            // 
            this.tlpRight.ColumnCount = 2;
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpRight.Controls.Add(this.lblHeader, 0, 1);
            this.tlpRight.Controls.Add(this.lblHelp, 1, 1);
            this.tlpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRight.Location = new System.Drawing.Point(51, 0);
            this.tlpRight.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRight.Name = "tlpRight";
            this.tlpRight.RowCount = 2;
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRight.Size = new System.Drawing.Size(564, 57);
            this.tlpRight.TabIndex = 3;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.lblHeader.Location = new System.Drawing.Point(0, 10);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(529, 47);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHelp
            // 
            this.lblHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHelp.Image = global::FileSplitterAndJoiner.Properties.Resources.help_off;
            this.lblHelp.Location = new System.Drawing.Point(529, 10);
            this.lblHelp.Margin = new System.Windows.Forms.Padding(0);
            this.lblHelp.MaximumSize = new System.Drawing.Size(27, 24);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(27, 24);
            this.lblHelp.TabIndex = 1;
            this.lblHelp.Click += new System.EventHandler(this.lblHelp_Click);
            this.lblHelp.MouseEnter += new System.EventHandler(this.lblHelp_MouseEnter);
            this.lblHelp.MouseLeave += new System.EventHandler(this.lblHelp_MouseLeave);
            // 
            // lblBar
            // 
            this.lblBar.AutoSize = true;
            this.lblBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBar.Image = global::FileSplitterAndJoiner.Properties.Resources.icon_split;
            this.lblBar.Location = new System.Drawing.Point(2, 5);
            this.lblBar.Margin = new System.Windows.Forms.Padding(2, 5, 0, 0);
            this.lblBar.Name = "lblBar";
            this.lblBar.Size = new System.Drawing.Size(49, 52);
            this.lblBar.TabIndex = 4;
            this.lblBar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TopControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.tlpLeft);
            this.Name = "TopControl";
            this.Size = new System.Drawing.Size(615, 64);
            this.tlpLeft.ResumeLayout(false);
            this.tlpCenter.ResumeLayout(false);
            this.tlpCenter.PerformLayout();
            this.tlpRight.ResumeLayout(false);
            this.tlpRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        System.Windows.Forms.TableLayoutPanel tlpLeft;
        System.Windows.Forms.Label lblBottomStrip;
        System.Windows.Forms.TableLayoutPanel tlpCenter;
        System.Windows.Forms.TableLayoutPanel tlpRight;
        System.Windows.Forms.Label lblHeader;
        System.Windows.Forms.Label lblHelp;
        System.Windows.Forms.Label lblBar;
    }
}
