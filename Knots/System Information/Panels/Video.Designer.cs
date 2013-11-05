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
	/// Designer for Video.
	/// </summary>
    public partial class Video : SystemInformation.TaskPanelBase
    {

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        public Video()
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
            this.labelControllers = new System.Windows.Forms.Label();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelNumberControllers = new System.Windows.Forms.Label();
            this.listviewAdaptors = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelScreenDimensions = new System.Windows.Forms.Label();
            this.labelScreenWorkingArea = new System.Windows.Forms.Label();
            this.labelControllerInfo = new System.Windows.Forms.Label();
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
            this.picturePanel.Image = global::SystemInformation.Properties.Resources.Video_48x48;
            this.picturePanel.Location = new System.Drawing.Point(16, 16);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(48, 48);
            this.picturePanel.TabIndex = 6;
            this.picturePanel.TabStop = false;
            // 
            // labelControllers
            // 
            this.labelControllers.AutoSize = true;
            this.labelControllers.BackColor = System.Drawing.Color.Transparent;
            this.labelControllers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControllers.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelControllers.Location = new System.Drawing.Point(13, 72);
            this.labelControllers.Name = "labelControllers";
            this.labelControllers.Size = new System.Drawing.Size(0, 17);
            this.labelControllers.TabIndex = 15;
            // 
            // labelSeparator
            // 
            this.labelSeparator.BackColor = System.Drawing.Color.Black;
            this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator.Location = new System.Drawing.Point(13, 94);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(483, 3);
            this.labelSeparator.TabIndex = 14;
            // 
            // labelNumberControllers
            // 
            this.labelNumberControllers.AutoSize = true;
            this.labelNumberControllers.BackColor = System.Drawing.Color.Transparent;
            this.labelNumberControllers.ForeColor = System.Drawing.Color.Black;
            this.labelNumberControllers.Location = new System.Drawing.Point(13, 108);
            this.labelNumberControllers.Name = "labelNumberControllers";
            this.labelNumberControllers.Size = new System.Drawing.Size(0, 15);
            this.labelNumberControllers.TabIndex = 16;
            // 
            // listviewAdaptors
            // 
            this.listviewAdaptors.BackColor = System.Drawing.SystemColors.Window;
            this.listviewAdaptors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1});
            this.listviewAdaptors.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewAdaptors.ForeColor = System.Drawing.SystemColors.ControlText;
            this.listviewAdaptors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listviewAdaptors.LabelWrap = false;
            this.listviewAdaptors.Location = new System.Drawing.Point(13, 192);
            this.listviewAdaptors.MultiSelect = false;
            this.listviewAdaptors.Name = "listviewAdaptors";
            this.listviewAdaptors.ShowGroups = false;
            this.listviewAdaptors.Size = new System.Drawing.Size(483, 228);
            this.listviewAdaptors.TabIndex = 17;
            this.listviewAdaptors.UseCompatibleStateImageBehavior = false;
            this.listviewAdaptors.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Width = 400;
            // 
            // labelScreenDimensions
            // 
            this.labelScreenDimensions.AutoSize = true;
            this.labelScreenDimensions.BackColor = System.Drawing.Color.Transparent;
            this.labelScreenDimensions.ForeColor = System.Drawing.Color.Black;
            this.labelScreenDimensions.Location = new System.Drawing.Point(13, 128);
            this.labelScreenDimensions.Name = "labelScreenDimensions";
            this.labelScreenDimensions.Size = new System.Drawing.Size(0, 15);
            this.labelScreenDimensions.TabIndex = 18;
            // 
            // labelScreenWorkingArea
            // 
            this.labelScreenWorkingArea.AutoSize = true;
            this.labelScreenWorkingArea.BackColor = System.Drawing.Color.Transparent;
            this.labelScreenWorkingArea.ForeColor = System.Drawing.Color.Black;
            this.labelScreenWorkingArea.Location = new System.Drawing.Point(13, 148);
            this.labelScreenWorkingArea.Name = "labelScreenWorkingArea";
            this.labelScreenWorkingArea.Size = new System.Drawing.Size(0, 15);
            this.labelScreenWorkingArea.TabIndex = 19;
            // 
            // labelControllerInfo
            // 
            this.labelControllerInfo.AutoSize = true;
            this.labelControllerInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelControllerInfo.ForeColor = System.Drawing.Color.Black;
            this.labelControllerInfo.Location = new System.Drawing.Point(13, 172);
            this.labelControllerInfo.Name = "labelControllerInfo";
            this.labelControllerInfo.Size = new System.Drawing.Size(0, 15);
            this.labelControllerInfo.TabIndex = 20;
            // 
            // Video
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.labelControllerInfo);
            this.Controls.Add(this.labelScreenWorkingArea);
            this.Controls.Add(this.labelScreenDimensions);
            this.Controls.Add(this.listviewAdaptors);
            this.Controls.Add(this.labelNumberControllers);
            this.Controls.Add(this.labelControllers);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.picturePanel);
            this.Name = "Video";
            this.Load += new System.EventHandler(this.Video_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        System.Windows.Forms.Label labelTitle;
        System.Windows.Forms.PictureBox picturePanel;
        System.Windows.Forms.Label labelControllers;
        System.Windows.Forms.Label labelSeparator;
        System.Windows.Forms.Label labelNumberControllers;
        System.Windows.Forms.ListView listviewAdaptors;
        System.Windows.Forms.ColumnHeader ColumnHeader1;
        System.Windows.Forms.Label labelScreenDimensions;
        System.Windows.Forms.Label labelScreenWorkingArea;
        System.Windows.Forms.Label labelControllerInfo;

        #endregion
		
	}
	
}
