namespace FreemiumUtilities.MozillaToolbarRemover
{
    partial class FormMozillaToolbarsAndAddOns
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        private void InitializeComponent()
        {
            this.lblDescription = new System.Windows.Forms.Label();
            this.lvMozillaTools = new System.Windows.Forms.ListView();
            this.colAddOn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 14);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(152, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "These add-ons will be disabled";
            // 
            // lvMozillaTools
            // 
            this.lvMozillaTools.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMozillaTools.CheckBoxes = true;
            this.lvMozillaTools.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAddOn});
            this.lvMozillaTools.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMozillaTools.Location = new System.Drawing.Point(15, 35);
            this.lvMozillaTools.Name = "lvMozillaTools";
            this.lvMozillaTools.Size = new System.Drawing.Size(384, 326);
            this.lvMozillaTools.TabIndex = 2;
            this.lvMozillaTools.UseCompatibleStateImageBehavior = false;
            this.lvMozillaTools.View = System.Windows.Forms.View.Details;
            this.lvMozillaTools.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvMozillaTools_ItemChecked);
            // 
            // colAddOn
            // 
            this.colAddOn.Text = "Add-On";
            // 
            // FormMozillaToolbarsAndAddOns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 373);
            this.Controls.Add(this.lvMozillaTools);
            this.Controls.Add(this.lblDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormMozillaToolbarsAndAddOns";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mozilla Toolbars And Add Ons Remover";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.ListView lvMozillaTools;
        private System.Windows.Forms.ColumnHeader colAddOn;

    }
}