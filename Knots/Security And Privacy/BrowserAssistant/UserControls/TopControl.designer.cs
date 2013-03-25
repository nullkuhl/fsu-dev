namespace BrowserAssistant
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblBar = new System.Windows.Forms.Label();
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRight = new System.Windows.Forms.TableLayoutPanel();
            this.lblText = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            this.lblIcon = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.tlpRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lblBar, 0, 1);
            this.tlpMain.Controls.Add(this.tlpTop, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tlpMain.Size = new System.Drawing.Size(615, 64);
            this.tlpMain.TabIndex = 1;
            // 
            // lblBar
            // 
            this.lblBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBar.Image = ((System.Drawing.Image)(resources.GetObject("lblBar.Image")));
            this.lblBar.Location = new System.Drawing.Point(0, 57);
            this.lblBar.Margin = new System.Windows.Forms.Padding(0);
            this.lblBar.Name = "lblBar";
            this.lblBar.Size = new System.Drawing.Size(615, 7);
            this.lblBar.TabIndex = 0;
            // 
            // tlpTop
            // 
            this.tlpTop.ColumnCount = 2;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.Controls.Add(this.tlpRight, 1, 0);
            this.tlpTop.Controls.Add(this.lblIcon, 0, 0);
            this.tlpTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTop.Location = new System.Drawing.Point(0, 0);
            this.tlpTop.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 1;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.Size = new System.Drawing.Size(615, 57);
            this.tlpTop.TabIndex = 1;
            // 
            // tlpRight
            // 
            this.tlpRight.ColumnCount = 2;
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpRight.Controls.Add(this.lblText, 0, 1);
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
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblText.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.lblText.Location = new System.Drawing.Point(0, 10);
            this.lblText.Margin = new System.Windows.Forms.Padding(0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(529, 47);
            this.lblText.TabIndex = 0;
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHelp
            // 
            this.lblHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHelp.Image = global::BrowserAssistant.Properties.Resources.help_off;
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
            // lblIcon
            // 
            this.lblIcon.AutoSize = true;
            this.lblIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIcon.Image = global::BrowserAssistant.Properties.Resources.icon_browser_manager;
            this.lblIcon.Location = new System.Drawing.Point(5, 5);
            this.lblIcon.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(46, 52);
            this.lblIcon.TabIndex = 4;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TopControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.tlpMain);
            this.Name = "TopControl";
            this.Size = new System.Drawing.Size(615, 64);
            this.tlpMain.ResumeLayout(false);
            this.tlpTop.ResumeLayout(false);
            this.tlpTop.PerformLayout();
            this.tlpRight.ResumeLayout(false);
            this.tlpRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        System.Windows.Forms.TableLayoutPanel tlpMain;
        System.Windows.Forms.Label lblBar;
        System.Windows.Forms.TableLayoutPanel tlpTop;
        System.Windows.Forms.TableLayoutPanel tlpRight;
        System.Windows.Forms.Label lblText;
        System.Windows.Forms.Label lblHelp;
        System.Windows.Forms.Label lblIcon;
    }
}
