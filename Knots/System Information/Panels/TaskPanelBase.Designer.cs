using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

namespace SystemInformation
{
	/// <summary>
	/// Designer for TaskPanelBase.
	/// </summary>
    public partial class TaskPanelBase : System.Windows.Forms.UserControl
	{

        public TaskPanelBase()
        {

            // This call is required by the Windows Form Designer.
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
            this.SuspendLayout();
            // 
            // TaskPanelBase
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "TaskPanelBase";
            this.Size = new System.Drawing.Size(515, 425);
            this.ResumeLayout(false);

        }

        #endregion
		
	}
	
}
