namespace FreemiumUtilities.Spyware
{
    partial class FrmSpyware
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSpyware));
            this.spywareLst = new System.Windows.Forms.ListView();
            this.colSpyware = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // spywareLst
            // 
            resources.ApplyResources(this.spywareLst, "spywareLst");
            this.spywareLst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSpyware,
            this.colFilePath});
            this.spywareLst.Name = "spywareLst";
            this.spywareLst.UseCompatibleStateImageBehavior = false;
            this.spywareLst.View = System.Windows.Forms.View.Details;
            // 
            // colSpyware
            // 
            resources.ApplyResources(this.colSpyware, "colSpyware");
            // 
            // colFilePath
            // 
            resources.ApplyResources(this.colFilePath, "colFilePath");
            // 
            // FrmSpyware
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spywareLst);
            this.Name = "FrmSpyware";
            this.ShowIcon = false;
            this.ResumeLayout(false);

        }

        #endregion

        System.Windows.Forms.ListView spywareLst;
        System.Windows.Forms.ColumnHeader colSpyware;
        System.Windows.Forms.ColumnHeader colFilePath;
    }
}