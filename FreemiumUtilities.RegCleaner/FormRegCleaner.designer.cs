namespace FreemiumUtilities.RegCleaner
{
    partial class FormRegCleaner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegCleaner));
            this.badRegLst = new System.Windows.Forms.ListView();
            this.colRegKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProblem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblRegKeys = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // badRegLst
            // 
            resources.ApplyResources(this.badRegLst, "badRegLst");
            this.badRegLst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRegKey,
            this.colProblem});
            this.badRegLst.Name = "badRegLst";
            this.badRegLst.UseCompatibleStateImageBehavior = false;
            this.badRegLst.View = System.Windows.Forms.View.Details;
            // 
            // colRegKey
            // 
            resources.ApplyResources(this.colRegKey, "colRegKey");
            // 
            // colProblem
            // 
            resources.ApplyResources(this.colProblem, "colProblem");
            // 
            // lblRegKeys
            // 
            resources.ApplyResources(this.lblRegKeys, "lblRegKeys");
            this.lblRegKeys.Name = "lblRegKeys";
            // 
            // FormRegCleaner
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.badRegLst);
            this.Controls.Add(this.lblRegKeys);
            this.Name = "FormRegCleaner";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmRegCleaner_Load);
            this.ResumeLayout(false);

        }

        #endregion

        System.Windows.Forms.ListView badRegLst;
        System.Windows.Forms.Label lblRegKeys;
        System.Windows.Forms.ColumnHeader colRegKey;
        System.Windows.Forms.ColumnHeader colProblem;
    }
}