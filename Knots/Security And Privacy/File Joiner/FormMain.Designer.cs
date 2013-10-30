namespace Joiner
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.pieceSetInfoLbl = new System.Windows.Forms.Label();
            this.infoTxt = new System.Windows.Forms.TextBox();
            this.extractToLbl = new System.Windows.Forms.Label();
            this.extractToTxt = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.processLbl = new System.Windows.Forms.Label();
            this.processPrgrss = new System.Windows.Forms.ProgressBar();
            this.joinBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // pieceSetInfoLbl
            // 
            this.pieceSetInfoLbl.AutoSize = true;
            this.pieceSetInfoLbl.Location = new System.Drawing.Point(12, 9);
            this.pieceSetInfoLbl.Name = "pieceSetInfoLbl";
            this.pieceSetInfoLbl.Size = new System.Drawing.Size(111, 13);
            this.pieceSetInfoLbl.TabIndex = 0;
            this.pieceSetInfoLbl.Text = "Piece Set Information:";
            // 
            // infoTxt
            // 
            this.infoTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.infoTxt.Location = new System.Drawing.Point(12, 25);
            this.infoTxt.Multiline = true;
            this.infoTxt.Name = "infoTxt";
            this.infoTxt.ReadOnly = true;
            this.infoTxt.Size = new System.Drawing.Size(519, 79);
            this.infoTxt.TabIndex = 1;
            // 
            // extractToLbl
            // 
            this.extractToLbl.AutoSize = true;
            this.extractToLbl.Location = new System.Drawing.Point(9, 113);
            this.extractToLbl.Name = "extractToLbl";
            this.extractToLbl.Size = new System.Drawing.Size(55, 13);
            this.extractToLbl.TabIndex = 2;
            this.extractToLbl.Text = "Extract to:";
            // 
            // extractToTxt
            // 
            this.extractToTxt.Location = new System.Drawing.Point(74, 110);
            this.extractToTxt.Name = "extractToTxt";
            this.extractToTxt.Size = new System.Drawing.Size(376, 20);
            this.extractToTxt.TabIndex = 3;
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(456, 108);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(75, 23);
            this.browseBtn.TabIndex = 4;
            this.browseBtn.Text = "&Browse...";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // processLbl
            // 
            this.processLbl.AutoSize = true;
            this.processLbl.Location = new System.Drawing.Point(9, 143);
            this.processLbl.Name = "processLbl";
            this.processLbl.Size = new System.Drawing.Size(48, 13);
            this.processLbl.TabIndex = 5;
            this.processLbl.Text = "Process:";
            // 
            // processPrgrss
            // 
            this.processPrgrss.Location = new System.Drawing.Point(12, 159);
            this.processPrgrss.Name = "processPrgrss";
            this.processPrgrss.Size = new System.Drawing.Size(519, 18);
            this.processPrgrss.TabIndex = 6;
            // 
            // joinBtn
            // 
            this.joinBtn.Location = new System.Drawing.Point(456, 188);
            this.joinBtn.Name = "joinBtn";
            this.joinBtn.Size = new System.Drawing.Size(75, 23);
            this.joinBtn.TabIndex = 7;
            this.joinBtn.Text = "&Join Now!";
            this.joinBtn.UseVisualStyleBackColor = true;
            this.joinBtn.Click += new System.EventHandler(this.joinBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(375, 188);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 8;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // JoinerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 223);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.joinBtn);
            this.Controls.Add(this.processPrgrss);
            this.Controls.Add(this.processLbl);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.extractToTxt);
            this.Controls.Add(this.extractToLbl);
            this.Controls.Add(this.infoTxt);
            this.Controls.Add(this.pieceSetInfoLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JoinerFrm";
            this.Text = "Freemium System Utilities - Join Files";
            this.Load += new System.EventHandler(this.JoinerFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.Label pieceSetInfoLbl;
        System.Windows.Forms.TextBox infoTxt;
        System.Windows.Forms.Label extractToLbl;
        System.Windows.Forms.TextBox extractToTxt;
        System.Windows.Forms.Button browseBtn;
        System.Windows.Forms.Label processLbl;
        System.Windows.Forms.ProgressBar processPrgrss;
        System.Windows.Forms.Button joinBtn;
        System.Windows.Forms.Button cancelBtn;
        System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

