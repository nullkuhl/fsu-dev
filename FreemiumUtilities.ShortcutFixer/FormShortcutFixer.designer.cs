namespace FreemiumUtilities.ShortcutFixer
{
    partial class FrmShortcutFixer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShortcutFixer));
            this.shortcutsLst = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTarget = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // shortcutsLst
            // 
            resources.ApplyResources(this.shortcutsLst, "shortcutsLst");
            this.shortcutsLst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colTarget,
            this.colLocation,
            this.colDescription});
            this.shortcutsLst.Name = "shortcutsLst";
            this.shortcutsLst.UseCompatibleStateImageBehavior = false;
            this.shortcutsLst.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            // 
            // colTarget
            // 
            resources.ApplyResources(this.colTarget, "colTarget");
            // 
            // colLocation
            // 
            resources.ApplyResources(this.colLocation, "colLocation");
            // 
            // colDescription
            // 
            resources.ApplyResources(this.colDescription, "colDescription");
            // 
            // FrmShortcutFixer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.shortcutsLst);
            this.Name = "FrmShortcutFixer";
            this.ShowIcon = false;
            this.ResumeLayout(false);

        }

        #endregion

        System.Windows.Forms.ListView shortcutsLst;
        System.Windows.Forms.ColumnHeader colName;
        System.Windows.Forms.ColumnHeader colTarget;
        System.Windows.Forms.ColumnHeader colLocation;
        System.Windows.Forms.ColumnHeader colDescription;
    }
}