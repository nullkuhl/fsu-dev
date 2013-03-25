using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace SystemInformation
{
	/// <summary>
	/// Designer for Network.
	/// </summary>
    public partial class Network : SystemInformation.TaskPanelBase
    {

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        public Network()
        {

            //This call is required by the Windows Form Designer.
            InitializeComponent();

        }

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
            this.labelTitle = new System.Windows.Forms.Label();
            this.picturePanel = new System.Windows.Forms.PictureBox();
            this.labelNetworkId = new System.Windows.Forms.Label();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelInterface = new System.Windows.Forms.Label();
            this.listviewInterface = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cboInterface = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTitle.Location = new System.Drawing.Point(80, 28);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(0, 25);
            this.labelTitle.TabIndex = 7;
            // 
            // picturePanel
            // 
            this.picturePanel.BackColor = System.Drawing.Color.Transparent;
            this.picturePanel.Image = global::SystemInformation.Properties.Resources.Network_48x48;
            this.picturePanel.Location = new System.Drawing.Point(16, 16);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(48, 48);
            this.picturePanel.TabIndex = 6;
            this.picturePanel.TabStop = false;
            // 
            // labelNetworkId
            // 
            this.labelNetworkId.AutoSize = true;
            this.labelNetworkId.BackColor = System.Drawing.Color.Transparent;
            this.labelNetworkId.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNetworkId.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelNetworkId.Location = new System.Drawing.Point(16, 73);
            this.labelNetworkId.Name = "labelNetworkId";
            this.labelNetworkId.Size = new System.Drawing.Size(32, 17);
            this.labelNetworkId.TabIndex = 15;
            this.labelNetworkId.Text = "      ";
            // 
            // labelSeparator
            // 
            this.labelSeparator.BackColor = System.Drawing.Color.Black;
            this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator.Location = new System.Drawing.Point(16, 94);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(482, 3);
            this.labelSeparator.TabIndex = 14;
            // 
            // labelInterface
            // 
            this.labelInterface.AutoSize = true;
            this.labelInterface.BackColor = System.Drawing.Color.Transparent;
            this.labelInterface.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInterface.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelInterface.Location = new System.Drawing.Point(16, 108);
            this.labelInterface.Name = "labelInterface";
            this.labelInterface.Size = new System.Drawing.Size(0, 17);
            this.labelInterface.TabIndex = 17;
            // 
            // listviewInterface
            // 
            this.listviewInterface.BackColor = System.Drawing.SystemColors.Window;
            this.listviewInterface.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1});
            this.listviewInterface.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewInterface.ForeColor = System.Drawing.SystemColors.ControlText;
            this.listviewInterface.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listviewInterface.LabelWrap = false;
            this.listviewInterface.Location = new System.Drawing.Point(16, 156);
            this.listviewInterface.Name = "listviewInterface";
            this.listviewInterface.ShowGroups = false;
            this.listviewInterface.Size = new System.Drawing.Size(482, 264);
            this.listviewInterface.TabIndex = 45;
            this.listviewInterface.UseCompatibleStateImageBehavior = false;
            this.listviewInterface.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Width = 400;
            // 
            // cboInterface
            // 
            this.cboInterface.FormattingEnabled = true;
            this.cboInterface.Location = new System.Drawing.Point(16, 128);
            this.cboInterface.Name = "cboInterface";
            this.cboInterface.Size = new System.Drawing.Size(482, 23);
            this.cboInterface.TabIndex = 47;
            this.cboInterface.SelectedIndexChanged += new System.EventHandler(this.cboInterface_SelectedIndexChanged);
            // 
            // Network
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.cboInterface);
            this.Controls.Add(this.listviewInterface);
            this.Controls.Add(this.labelInterface);
            this.Controls.Add(this.labelNetworkId);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.picturePanel);
            this.Name = "Network";
            this.Load += new System.EventHandler(this.Network_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        System.Windows.Forms.Label labelTitle;
        System.Windows.Forms.PictureBox picturePanel;
        System.Windows.Forms.Label labelNetworkId;
        System.Windows.Forms.Label labelSeparator;
        System.Windows.Forms.Label labelInterface;
        System.Windows.Forms.ListView listviewInterface;
        System.Windows.Forms.ColumnHeader ColumnHeader1;
        ComboBox cboInterface;

        #endregion
		
	}
	
}
