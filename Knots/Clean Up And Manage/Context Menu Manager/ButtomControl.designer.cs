namespace Context_Menu_Manager
{
    partial class BottomControl
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
			this.lblText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblText
			// 
			this.lblText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblText.Location = new System.Drawing.Point(0, 0);
			this.lblText.Margin = new System.Windows.Forms.Padding(0);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(0, 31);
			this.lblText.TabIndex = 0;
			// 
			// BottomControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblText);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MaximumSize = new System.Drawing.Size(0, 31);
			this.MinimumSize = new System.Drawing.Size(0, 31);
			this.Name = "BottomControl";
			this.Size = new System.Drawing.Size(0, 31);
			this.ResumeLayout(false);

        }

        #endregion

        System.Windows.Forms.Label lblText;
    }
}
