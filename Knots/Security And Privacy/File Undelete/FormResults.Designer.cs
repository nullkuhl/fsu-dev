using System.Resources;
using System.Globalization;
using System.Threading;

namespace FileUndelete
{
    partial class FormResults
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
			this.txtResults = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtResults
			// 
			this.txtResults.AcceptsReturn = true;
			this.txtResults.AcceptsTab = true;
			this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtResults.HideSelection = false;
			this.txtResults.Location = new System.Drawing.Point(12, 12);
			this.txtResults.Multiline = true;
			this.txtResults.Name = "txtResults";
			this.txtResults.ReadOnly = true;
			this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtResults.Size = new System.Drawing.Size(568, 313);
			this.txtResults.TabIndex = 0;
			this.txtResults.WordWrap = false;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(12, 331);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// FormResults
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(592, 366);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.txtResults);
			this.MinimizeBox = false;
			this.Name = "FormResults";
			this.ShowInTaskbar = false;
			this.Text = "Recovery Results";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtResults;
        System.Windows.Forms.Button btnOK;

    }
}