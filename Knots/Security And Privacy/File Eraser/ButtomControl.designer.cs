namespace FileEraser
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
            this.lblMain = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMain
            // 
            this.lblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMain.Location = new System.Drawing.Point(0, 0);
            this.lblMain.Margin = new System.Windows.Forms.Padding(0);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(0, 31);
            this.lblMain.TabIndex = 0;
            // 
            // BottomControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(0, 31);
            this.MinimumSize = new System.Drawing.Size(0, 31);
            this.Name = "BottomControl";
            this.Size = new System.Drawing.Size(0, 31);
            this.ResumeLayout(false);

        }

        #endregion

        System.Windows.Forms.Label lblMain;
    }
}
