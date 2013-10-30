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
	/// Designer for VisualStyles.
	/// </summary>
    public partial class VisualStyles : SystemInformation.TaskPanelBase
    {

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        public VisualStyles()
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
            this.labelVisualStyles = new System.Windows.Forms.Label();
            this.pictureCPU = new System.Windows.Forms.PictureBox();
            this.labelCompanyDesc = new System.Windows.Forms.Label();
            this.labelCurrentVisualStyle = new System.Windows.Forms.Label();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelCopyrightDesc = new System.Windows.Forms.Label();
            this.labelAuthorDesc = new System.Windows.Forms.Label();
            this.labelVersionDesc = new System.Windows.Forms.Label();
            this.labelDisplayNameDesc = new System.Windows.Forms.Label();
            this.labelDescriptionDesc = new System.Windows.Forms.Label();
            this.labelColorSchemeDesc = new System.Windows.Forms.Label();
            this.labelEnabeUserDesc = new System.Windows.Forms.Label();
            this.labelSuportOSDesc = new System.Windows.Forms.Label();
            this.labelMinColorDepDesc = new System.Windows.Forms.Label();
            this.labelCompany = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelDisplayName = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelColorScheme = new System.Windows.Forms.Label();
            this.labelIsEnabledByUser = new System.Windows.Forms.Label();
            this.labelIsSupportedByOS = new System.Windows.Forms.Label();
            this.labelMinimumColorDepth = new System.Windows.Forms.Label();
            this.labelCntrlHiLiteHotDesc = new System.Windows.Forms.Label();
            this.labelTextCtrlBrdrDesc = new System.Windows.Forms.Label();
            this.pictureControlHighlightHot = new System.Windows.Forms.PictureBox();
            this.pictureTextControlBorder = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCPU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureControlHighlightHot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureTextControlBorder)).BeginInit();
            this.SuspendLayout();
            // 
            // labelVisualStyles
            // 
            this.labelVisualStyles.AutoSize = true;
            this.labelVisualStyles.BackColor = System.Drawing.Color.Transparent;
            this.labelVisualStyles.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVisualStyles.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelVisualStyles.Location = new System.Drawing.Point(80, 28);
            this.labelVisualStyles.Name = "labelVisualStyles";
            this.labelVisualStyles.Size = new System.Drawing.Size(0, 25);
            this.labelVisualStyles.TabIndex = 7;
            // 
            // pictureCPU
            // 
            this.pictureCPU.BackColor = System.Drawing.Color.Transparent;
            this.pictureCPU.Image = global::SystemInformation.Properties.Resources.Video_48x48;
            this.pictureCPU.Location = new System.Drawing.Point(16, 16);
            this.pictureCPU.Name = "pictureCPU";
            this.pictureCPU.Size = new System.Drawing.Size(48, 48);
            this.pictureCPU.TabIndex = 6;
            this.pictureCPU.TabStop = false;
            // 
            // labelCompanyDesc
            // 
            this.labelCompanyDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelCompanyDesc.Location = new System.Drawing.Point(19, 204);
            this.labelCompanyDesc.Name = "labelCompanyDesc";
            this.labelCompanyDesc.Size = new System.Drawing.Size(173, 15);
            this.labelCompanyDesc.TabIndex = 14;
            // 
            // labelCurrentVisualStyle
            // 
            this.labelCurrentVisualStyle.AutoSize = true;
            this.labelCurrentVisualStyle.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentVisualStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentVisualStyle.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelCurrentVisualStyle.Location = new System.Drawing.Point(16, 72);
            this.labelCurrentVisualStyle.Name = "labelCurrentVisualStyle";
            this.labelCurrentVisualStyle.Size = new System.Drawing.Size(0, 17);
            this.labelCurrentVisualStyle.TabIndex = 13;
            // 
            // labelSeparator
            // 
            this.labelSeparator.BackColor = System.Drawing.Color.Black;
            this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator.Location = new System.Drawing.Point(16, 94);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(477, 3);
            this.labelSeparator.TabIndex = 12;
            // 
            // labelCopyrightDesc
            // 
            this.labelCopyrightDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelCopyrightDesc.Location = new System.Drawing.Point(19, 224);
            this.labelCopyrightDesc.Name = "labelCopyrightDesc";
            this.labelCopyrightDesc.Size = new System.Drawing.Size(173, 15);
            this.labelCopyrightDesc.TabIndex = 15;
            // 
            // labelAuthorDesc
            // 
            this.labelAuthorDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelAuthorDesc.Location = new System.Drawing.Point(19, 244);
            this.labelAuthorDesc.Name = "labelAuthorDesc";
            this.labelAuthorDesc.Size = new System.Drawing.Size(173, 15);
            this.labelAuthorDesc.TabIndex = 16;
            // 
            // labelVersionDesc
            // 
            this.labelVersionDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelVersionDesc.Location = new System.Drawing.Point(19, 264);
            this.labelVersionDesc.Name = "labelVersionDesc";
            this.labelVersionDesc.Size = new System.Drawing.Size(173, 15);
            this.labelVersionDesc.TabIndex = 18;
            // 
            // labelDisplayNameDesc
            // 
            this.labelDisplayNameDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelDisplayNameDesc.Location = new System.Drawing.Point(19, 284);
            this.labelDisplayNameDesc.Name = "labelDisplayNameDesc";
            this.labelDisplayNameDesc.Size = new System.Drawing.Size(173, 15);
            this.labelDisplayNameDesc.TabIndex = 19;
            // 
            // labelDescriptionDesc
            // 
            this.labelDescriptionDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelDescriptionDesc.Location = new System.Drawing.Point(19, 304);
            this.labelDescriptionDesc.Name = "labelDescriptionDesc";
            this.labelDescriptionDesc.Size = new System.Drawing.Size(173, 15);
            this.labelDescriptionDesc.TabIndex = 20;
            // 
            // labelColorSchemeDesc
            // 
            this.labelColorSchemeDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelColorSchemeDesc.Location = new System.Drawing.Point(19, 324);
            this.labelColorSchemeDesc.Name = "labelColorSchemeDesc";
            this.labelColorSchemeDesc.Size = new System.Drawing.Size(173, 15);
            this.labelColorSchemeDesc.TabIndex = 21;
            // 
            // labelEnabeUserDesc
            // 
            this.labelEnabeUserDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelEnabeUserDesc.Location = new System.Drawing.Point(19, 124);
            this.labelEnabeUserDesc.Name = "labelEnabeUserDesc";
            this.labelEnabeUserDesc.Size = new System.Drawing.Size(173, 15);
            this.labelEnabeUserDesc.TabIndex = 22;
            // 
            // labelSuportOSDesc
            // 
            this.labelSuportOSDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelSuportOSDesc.Location = new System.Drawing.Point(19, 104);
            this.labelSuportOSDesc.Name = "labelSuportOSDesc";
            this.labelSuportOSDesc.Size = new System.Drawing.Size(173, 15);
            this.labelSuportOSDesc.TabIndex = 23;
            // 
            // labelMinColorDepDesc
            // 
            this.labelMinColorDepDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelMinColorDepDesc.Location = new System.Drawing.Point(19, 144);
            this.labelMinColorDepDesc.Name = "labelMinColorDepDesc";
            this.labelMinColorDepDesc.Size = new System.Drawing.Size(173, 15);
            this.labelMinColorDepDesc.TabIndex = 24;
            // 
            // labelCompany
            // 
            this.labelCompany.AutoSize = true;
            this.labelCompany.BackColor = System.Drawing.Color.Transparent;
            this.labelCompany.Location = new System.Drawing.Point(195, 204);
            this.labelCompany.Name = "labelCompany";
            this.labelCompany.Size = new System.Drawing.Size(0, 15);
            this.labelCompany.TabIndex = 25;
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.BackColor = System.Drawing.Color.Transparent;
            this.labelCopyright.Location = new System.Drawing.Point(195, 224);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(0, 15);
            this.labelCopyright.TabIndex = 26;
            // 
            // labelAuthor
            // 
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.BackColor = System.Drawing.Color.Transparent;
            this.labelAuthor.Location = new System.Drawing.Point(195, 244);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(0, 15);
            this.labelAuthor.TabIndex = 27;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.Location = new System.Drawing.Point(195, 264);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(0, 15);
            this.labelVersion.TabIndex = 29;
            // 
            // labelDisplayName
            // 
            this.labelDisplayName.AutoSize = true;
            this.labelDisplayName.BackColor = System.Drawing.Color.Transparent;
            this.labelDisplayName.Location = new System.Drawing.Point(195, 284);
            this.labelDisplayName.Name = "labelDisplayName";
            this.labelDisplayName.Size = new System.Drawing.Size(0, 15);
            this.labelDisplayName.TabIndex = 30;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelDescription.Location = new System.Drawing.Point(195, 304);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(0, 15);
            this.labelDescription.TabIndex = 31;
            // 
            // labelColorScheme
            // 
            this.labelColorScheme.AutoSize = true;
            this.labelColorScheme.BackColor = System.Drawing.Color.Transparent;
            this.labelColorScheme.Location = new System.Drawing.Point(195, 324);
            this.labelColorScheme.Name = "labelColorScheme";
            this.labelColorScheme.Size = new System.Drawing.Size(0, 15);
            this.labelColorScheme.TabIndex = 32;
            // 
            // labelIsEnabledByUser
            // 
            this.labelIsEnabledByUser.AutoSize = true;
            this.labelIsEnabledByUser.BackColor = System.Drawing.Color.Transparent;
            this.labelIsEnabledByUser.Location = new System.Drawing.Point(195, 124);
            this.labelIsEnabledByUser.Name = "labelIsEnabledByUser";
            this.labelIsEnabledByUser.Size = new System.Drawing.Size(0, 15);
            this.labelIsEnabledByUser.TabIndex = 33;
            // 
            // labelIsSupportedByOS
            // 
            this.labelIsSupportedByOS.AutoSize = true;
            this.labelIsSupportedByOS.BackColor = System.Drawing.Color.Transparent;
            this.labelIsSupportedByOS.Location = new System.Drawing.Point(195, 104);
            this.labelIsSupportedByOS.Name = "labelIsSupportedByOS";
            this.labelIsSupportedByOS.Size = new System.Drawing.Size(0, 15);
            this.labelIsSupportedByOS.TabIndex = 34;
            // 
            // labelMinimumColorDepth
            // 
            this.labelMinimumColorDepth.AutoSize = true;
            this.labelMinimumColorDepth.BackColor = System.Drawing.Color.Transparent;
            this.labelMinimumColorDepth.Location = new System.Drawing.Point(195, 144);
            this.labelMinimumColorDepth.Name = "labelMinimumColorDepth";
            this.labelMinimumColorDepth.Size = new System.Drawing.Size(0, 15);
            this.labelMinimumColorDepth.TabIndex = 35;
            // 
            // labelCntrlHiLiteHotDesc
            // 
            this.labelCntrlHiLiteHotDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelCntrlHiLiteHotDesc.Location = new System.Drawing.Point(19, 164);
            this.labelCntrlHiLiteHotDesc.Name = "labelCntrlHiLiteHotDesc";
            this.labelCntrlHiLiteHotDesc.Size = new System.Drawing.Size(173, 15);
            this.labelCntrlHiLiteHotDesc.TabIndex = 36;
            // 
            // labelTextCtrlBrdrDesc
            // 
            this.labelTextCtrlBrdrDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelTextCtrlBrdrDesc.Location = new System.Drawing.Point(19, 184);
            this.labelTextCtrlBrdrDesc.Name = "labelTextCtrlBrdrDesc";
            this.labelTextCtrlBrdrDesc.Size = new System.Drawing.Size(173, 15);
            this.labelTextCtrlBrdrDesc.TabIndex = 37;
            // 
            // pictureControlHighlightHot
            // 
            this.pictureControlHighlightHot.BackColor = System.Drawing.Color.Transparent;
            this.pictureControlHighlightHot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureControlHighlightHot.Location = new System.Drawing.Point(197, 164);
            this.pictureControlHighlightHot.Name = "pictureControlHighlightHot";
            this.pictureControlHighlightHot.Size = new System.Drawing.Size(157, 13);
            this.pictureControlHighlightHot.TabIndex = 38;
            this.pictureControlHighlightHot.TabStop = false;
            // 
            // pictureTextControlBorder
            // 
            this.pictureTextControlBorder.BackColor = System.Drawing.Color.Transparent;
            this.pictureTextControlBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureTextControlBorder.Location = new System.Drawing.Point(197, 184);
            this.pictureTextControlBorder.Name = "pictureTextControlBorder";
            this.pictureTextControlBorder.Size = new System.Drawing.Size(157, 13);
            this.pictureTextControlBorder.TabIndex = 39;
            this.pictureTextControlBorder.TabStop = false;
            // 
            // VisualStyles
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.pictureTextControlBorder);
            this.Controls.Add(this.pictureControlHighlightHot);
            this.Controls.Add(this.labelTextCtrlBrdrDesc);
            this.Controls.Add(this.labelCntrlHiLiteHotDesc);
            this.Controls.Add(this.labelMinimumColorDepth);
            this.Controls.Add(this.labelIsSupportedByOS);
            this.Controls.Add(this.labelIsEnabledByUser);
            this.Controls.Add(this.labelColorScheme);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelDisplayName);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.labelCompany);
            this.Controls.Add(this.labelMinColorDepDesc);
            this.Controls.Add(this.labelSuportOSDesc);
            this.Controls.Add(this.labelEnabeUserDesc);
            this.Controls.Add(this.labelColorSchemeDesc);
            this.Controls.Add(this.labelDescriptionDesc);
            this.Controls.Add(this.labelDisplayNameDesc);
            this.Controls.Add(this.labelVersionDesc);
            this.Controls.Add(this.labelAuthorDesc);
            this.Controls.Add(this.labelCopyrightDesc);
            this.Controls.Add(this.labelCompanyDesc);
            this.Controls.Add(this.labelCurrentVisualStyle);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.labelVisualStyles);
            this.Controls.Add(this.pictureCPU);
            this.Name = "VisualStyles";
            this.Load += new System.EventHandler(this.VisualStyles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCPU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureControlHighlightHot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureTextControlBorder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        System.Windows.Forms.Label labelVisualStyles;
        System.Windows.Forms.PictureBox pictureCPU;
        System.Windows.Forms.Label labelCompanyDesc;
        System.Windows.Forms.Label labelCurrentVisualStyle;
        System.Windows.Forms.Label labelSeparator;
        System.Windows.Forms.Label labelCopyrightDesc;
        System.Windows.Forms.Label labelAuthorDesc;
        System.Windows.Forms.Label labelVersionDesc;
        System.Windows.Forms.Label labelDisplayNameDesc;
        System.Windows.Forms.Label labelDescriptionDesc;
        System.Windows.Forms.Label labelColorSchemeDesc;
        System.Windows.Forms.Label labelEnabeUserDesc;
        System.Windows.Forms.Label labelSuportOSDesc;
        System.Windows.Forms.Label labelMinColorDepDesc;
        System.Windows.Forms.Label labelCompany;
        System.Windows.Forms.Label labelCopyright;
        System.Windows.Forms.Label labelAuthor;
        System.Windows.Forms.Label labelVersion;
        System.Windows.Forms.Label labelDisplayName;
        System.Windows.Forms.Label labelDescription;
        System.Windows.Forms.Label labelColorScheme;
        System.Windows.Forms.Label labelIsEnabledByUser;
        System.Windows.Forms.Label labelIsSupportedByOS;
        System.Windows.Forms.Label labelMinimumColorDepth;
        System.Windows.Forms.Label labelCntrlHiLiteHotDesc;
        System.Windows.Forms.Label labelTextCtrlBrdrDesc;
        System.Windows.Forms.PictureBox pictureControlHighlightHot;
        System.Windows.Forms.PictureBox pictureTextControlBorder;

        #endregion
		
	}
	
}
