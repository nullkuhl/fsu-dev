namespace FreemiumUtilities.StartupManager
{
	partial class FrmDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetails));
            this.listview = new System.Windows.Forms.ListView();
            this.FileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listview
            // 
            this.listview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listview.BackColor = System.Drawing.SystemColors.Window;
            this.listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileName,
            this.Type});
            this.listview.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listview.ForeColor = System.Drawing.SystemColors.ControlText;
            this.listview.FullRowSelect = true;
            this.listview.LabelWrap = false;
            this.listview.Location = new System.Drawing.Point(12, 12);
            this.listview.MultiSelect = false;
            this.listview.Name = "listview";
            this.listview.Size = new System.Drawing.Size(527, 188);
            this.listview.TabIndex = 50;
            this.listview.UseCompatibleStateImageBehavior = false;
            this.listview.View = System.Windows.Forms.View.Details;
            // 
            // FileName
            // 
            this.FileName.Text = "File Name";
            this.FileName.Width = 205;
            // 
            // Type
            // 
            this.Type.Text = "Location";
            this.Type.Width = 2000;
            // 
            // FrmDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 218);
            this.Controls.Add(this.listview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDetails";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Problem Details";
            this.Load += new System.EventHandler(this.frmDetails_Load);
            this.ResumeLayout(false);

		}

		#endregion

		System.Windows.Forms.ListView listview;
		System.Windows.Forms.ColumnHeader FileName;
		System.Windows.Forms.ColumnHeader Type;


	}
}