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
	/// Designer for OperatingSystem.
	/// </summary>
    public partial class OperatingSystem : SystemInformation.TaskPanelBase
    {

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        public OperatingSystem()
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
            this.labelServicePack = new System.Windows.Forms.Label();
            this.textboxFrameworkShortVersion = new System.Windows.Forms.TextBox();
            this.textboxFrameworkFullVersion = new System.Windows.Forms.TextBox();
            this.textboxFrameworkServicePack = new System.Windows.Forms.TextBox();
            this.textboxServicePack = new System.Windows.Forms.TextBox();
            this.textboxInstallDate = new System.Windows.Forms.TextBox();
            this.textboxUserName = new System.Windows.Forms.TextBox();
            this.textboxMachineName = new System.Windows.Forms.TextBox();
            this.textboxCodeName = new System.Windows.Forms.TextBox();
            this.textboxType = new System.Windows.Forms.TextBox();
            this.textboxFullVersion = new System.Windows.Forms.TextBox();
            this.textboxVersion = new System.Windows.Forms.TextBox();
            this.textboxFullName = new System.Windows.Forms.TextBox();
            this.textboxProductKey = new System.Windows.Forms.TextBox();
            this.textboxProductID = new System.Windows.Forms.TextBox();
            this.labelInstallDate = new System.Windows.Forms.Label();
            this.labelProductID = new System.Windows.Forms.Label();
            this.labelProductKey = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.labelFrameworkServicePack = new System.Windows.Forms.Label();
            this.labelFrameworkFullVersion = new System.Windows.Forms.Label();
            this.labelFrameworkShortVersion = new System.Windows.Forms.Label();
            this.labelFramework = new System.Windows.Forms.Label();
            this.labelSeperatorBottom = new System.Windows.Forms.Label();
            this.labelFullVersion = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelMachineName = new System.Windows.Forms.Label();
            this.labelCodeName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelFullName = new System.Windows.Forms.Label();
            this.labelWindows = new System.Windows.Forms.Label();
            this.labelSeparatorTop = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.picturePanel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).BeginInit();
            this.SuspendLayout();
            // 
            // labelServicePack
            // 
            this.labelServicePack.BackColor = System.Drawing.Color.Transparent;
            this.labelServicePack.Location = new System.Drawing.Point(13, 164);
            this.labelServicePack.Name = "labelServicePack";
            this.labelServicePack.Size = new System.Drawing.Size(130, 15);
            this.labelServicePack.TabIndex = 69;
            this.labelServicePack.Visible = false;
            // 
            // textboxFrameworkShortVersion
            // 
            this.textboxFrameworkShortVersion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxFrameworkShortVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxFrameworkShortVersion.Location = new System.Drawing.Point(149, 360);
            this.textboxFrameworkShortVersion.Name = "textboxFrameworkShortVersion";
            this.textboxFrameworkShortVersion.ReadOnly = true;
            this.textboxFrameworkShortVersion.Size = new System.Drawing.Size(350, 16);
            this.textboxFrameworkShortVersion.TabIndex = 68;
            // 
            // textboxFrameworkFullVersion
            // 
            this.textboxFrameworkFullVersion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxFrameworkFullVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxFrameworkFullVersion.Location = new System.Drawing.Point(149, 380);
            this.textboxFrameworkFullVersion.Name = "textboxFrameworkFullVersion";
            this.textboxFrameworkFullVersion.ReadOnly = true;
            this.textboxFrameworkFullVersion.Size = new System.Drawing.Size(350, 16);
            this.textboxFrameworkFullVersion.TabIndex = 67;
            // 
            // textboxFrameworkServicePack
            // 
            this.textboxFrameworkServicePack.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxFrameworkServicePack.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxFrameworkServicePack.Location = new System.Drawing.Point(149, 400);
            this.textboxFrameworkServicePack.Name = "textboxFrameworkServicePack";
            this.textboxFrameworkServicePack.ReadOnly = true;
            this.textboxFrameworkServicePack.Size = new System.Drawing.Size(350, 16);
            this.textboxFrameworkServicePack.TabIndex = 66;
            // 
            // textboxServicePack
            // 
            this.textboxServicePack.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxServicePack.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxServicePack.Location = new System.Drawing.Point(149, 164);
            this.textboxServicePack.Name = "textboxServicePack";
            this.textboxServicePack.ReadOnly = true;
            this.textboxServicePack.Size = new System.Drawing.Size(350, 16);
            this.textboxServicePack.TabIndex = 65;
            // 
            // textboxInstallDate
            // 
            this.textboxInstallDate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxInstallDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxInstallDate.Location = new System.Drawing.Point(149, 264);
            this.textboxInstallDate.Name = "textboxInstallDate";
            this.textboxInstallDate.ReadOnly = true;
            this.textboxInstallDate.Size = new System.Drawing.Size(350, 16);
            this.textboxInstallDate.TabIndex = 64;
            // 
            // textboxUserName
            // 
            this.textboxUserName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxUserName.Location = new System.Drawing.Point(149, 244);
            this.textboxUserName.Name = "textboxUserName";
            this.textboxUserName.ReadOnly = true;
            this.textboxUserName.Size = new System.Drawing.Size(350, 16);
            this.textboxUserName.TabIndex = 63;
            // 
            // textboxMachineName
            // 
            this.textboxMachineName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxMachineName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxMachineName.Location = new System.Drawing.Point(149, 224);
            this.textboxMachineName.Name = "textboxMachineName";
            this.textboxMachineName.ReadOnly = true;
            this.textboxMachineName.Size = new System.Drawing.Size(350, 16);
            this.textboxMachineName.TabIndex = 62;
            // 
            // textboxCodeName
            // 
            this.textboxCodeName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxCodeName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxCodeName.Location = new System.Drawing.Point(149, 204);
            this.textboxCodeName.Name = "textboxCodeName";
            this.textboxCodeName.ReadOnly = true;
            this.textboxCodeName.Size = new System.Drawing.Size(350, 16);
            this.textboxCodeName.TabIndex = 61;
            // 
            // textboxType
            // 
            this.textboxType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxType.Location = new System.Drawing.Point(149, 184);
            this.textboxType.Name = "textboxType";
            this.textboxType.ReadOnly = true;
            this.textboxType.Size = new System.Drawing.Size(350, 16);
            this.textboxType.TabIndex = 60;
            // 
            // textboxFullVersion
            // 
            this.textboxFullVersion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxFullVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxFullVersion.Location = new System.Drawing.Point(149, 144);
            this.textboxFullVersion.Name = "textboxFullVersion";
            this.textboxFullVersion.ReadOnly = true;
            this.textboxFullVersion.Size = new System.Drawing.Size(350, 16);
            this.textboxFullVersion.TabIndex = 59;
            // 
            // textboxVersion
            // 
            this.textboxVersion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxVersion.Location = new System.Drawing.Point(149, 124);
            this.textboxVersion.Name = "textboxVersion";
            this.textboxVersion.ReadOnly = true;
            this.textboxVersion.Size = new System.Drawing.Size(350, 16);
            this.textboxVersion.TabIndex = 58;
            // 
            // textboxFullName
            // 
            this.textboxFullName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxFullName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxFullName.Location = new System.Drawing.Point(149, 103);
            this.textboxFullName.Name = "textboxFullName";
            this.textboxFullName.ReadOnly = true;
            this.textboxFullName.Size = new System.Drawing.Size(350, 16);
            this.textboxFullName.TabIndex = 57;
            // 
            // textboxProductKey
            // 
            this.textboxProductKey.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxProductKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxProductKey.Location = new System.Drawing.Point(149, 304);
            this.textboxProductKey.Name = "textboxProductKey";
            this.textboxProductKey.ReadOnly = true;
            this.textboxProductKey.Size = new System.Drawing.Size(350, 16);
            this.textboxProductKey.TabIndex = 56;
            // 
            // textboxProductID
            // 
            this.textboxProductID.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textboxProductID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxProductID.Location = new System.Drawing.Point(149, 284);
            this.textboxProductID.Name = "textboxProductID";
            this.textboxProductID.ReadOnly = true;
            this.textboxProductID.Size = new System.Drawing.Size(350, 16);
            this.textboxProductID.TabIndex = 55;
            // 
            // labelInstallDate
            // 
            this.labelInstallDate.BackColor = System.Drawing.Color.Transparent;
            this.labelInstallDate.Location = new System.Drawing.Point(13, 264);
            this.labelInstallDate.Name = "labelInstallDate";
            this.labelInstallDate.Size = new System.Drawing.Size(130, 15);
            this.labelInstallDate.TabIndex = 39;
            // 
            // labelProductID
            // 
            this.labelProductID.BackColor = System.Drawing.Color.Transparent;
            this.labelProductID.Location = new System.Drawing.Point(13, 284);
            this.labelProductID.Name = "labelProductID";
            this.labelProductID.Size = new System.Drawing.Size(130, 15);
            this.labelProductID.TabIndex = 36;
            // 
            // labelProductKey
            // 
            this.labelProductKey.BackColor = System.Drawing.Color.Transparent;
            this.labelProductKey.Location = new System.Drawing.Point(13, 304);
            this.labelProductKey.Name = "labelProductKey";
            this.labelProductKey.Size = new System.Drawing.Size(130, 15);
            this.labelProductKey.TabIndex = 35;
            // 
            // labelType
            // 
            this.labelType.BackColor = System.Drawing.Color.Transparent;
            this.labelType.Location = new System.Drawing.Point(13, 184);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(130, 15);
            this.labelType.TabIndex = 32;
            // 
            // labelFrameworkServicePack
            // 
            this.labelFrameworkServicePack.BackColor = System.Drawing.Color.Transparent;
            this.labelFrameworkServicePack.Location = new System.Drawing.Point(13, 400);
            this.labelFrameworkServicePack.Name = "labelFrameworkServicePack";
            this.labelFrameworkServicePack.Size = new System.Drawing.Size(130, 15);
            this.labelFrameworkServicePack.TabIndex = 29;
            this.labelFrameworkServicePack.Visible = false;
            // 
            // labelFrameworkFullVersion
            // 
            this.labelFrameworkFullVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelFrameworkFullVersion.Location = new System.Drawing.Point(13, 380);
            this.labelFrameworkFullVersion.Name = "labelFrameworkFullVersion";
            this.labelFrameworkFullVersion.Size = new System.Drawing.Size(130, 15);
            this.labelFrameworkFullVersion.TabIndex = 27;
            // 
            // labelFrameworkShortVersion
            // 
            this.labelFrameworkShortVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelFrameworkShortVersion.Location = new System.Drawing.Point(13, 360);
            this.labelFrameworkShortVersion.Name = "labelFrameworkShortVersion";
            this.labelFrameworkShortVersion.Size = new System.Drawing.Size(130, 15);
            this.labelFrameworkShortVersion.TabIndex = 26;
            // 
            // labelFramework
            // 
            this.labelFramework.AutoSize = true;
            this.labelFramework.BackColor = System.Drawing.Color.Transparent;
            this.labelFramework.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFramework.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelFramework.Location = new System.Drawing.Point(13, 330);
            this.labelFramework.Name = "labelFramework";
            this.labelFramework.Size = new System.Drawing.Size(0, 17);
            this.labelFramework.TabIndex = 25;
            // 
            // labelSeperatorBottom
            // 
            this.labelSeperatorBottom.BackColor = System.Drawing.Color.Black;
            this.labelSeperatorBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeperatorBottom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeperatorBottom.Location = new System.Drawing.Point(13, 350);
            this.labelSeperatorBottom.Name = "labelSeperatorBottom";
            this.labelSeperatorBottom.Size = new System.Drawing.Size(486, 3);
            this.labelSeperatorBottom.TabIndex = 24;
            // 
            // labelFullVersion
            // 
            this.labelFullVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelFullVersion.Location = new System.Drawing.Point(13, 144);
            this.labelFullVersion.Name = "labelFullVersion";
            this.labelFullVersion.Size = new System.Drawing.Size(130, 15);
            this.labelFullVersion.TabIndex = 23;
            // 
            // labelUserName
            // 
            this.labelUserName.BackColor = System.Drawing.Color.Transparent;
            this.labelUserName.Location = new System.Drawing.Point(13, 244);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(130, 15);
            this.labelUserName.TabIndex = 21;
            // 
            // labelMachineName
            // 
            this.labelMachineName.BackColor = System.Drawing.Color.Transparent;
            this.labelMachineName.Location = new System.Drawing.Point(13, 224);
            this.labelMachineName.Name = "labelMachineName";
            this.labelMachineName.Size = new System.Drawing.Size(130, 15);
            this.labelMachineName.TabIndex = 20;
            // 
            // labelCodeName
            // 
            this.labelCodeName.BackColor = System.Drawing.Color.Transparent;
            this.labelCodeName.Location = new System.Drawing.Point(13, 204);
            this.labelCodeName.Name = "labelCodeName";
            this.labelCodeName.Size = new System.Drawing.Size(130, 15);
            this.labelCodeName.TabIndex = 19;
            // 
            // labelVersion
            // 
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.Location = new System.Drawing.Point(13, 124);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(130, 15);
            this.labelVersion.TabIndex = 18;
            // 
            // labelFullName
            // 
            this.labelFullName.BackColor = System.Drawing.Color.Transparent;
            this.labelFullName.Location = new System.Drawing.Point(13, 104);
            this.labelFullName.Name = "labelFullName";
            this.labelFullName.Size = new System.Drawing.Size(130, 15);
            this.labelFullName.TabIndex = 17;
            // 
            // labelWindows
            // 
            this.labelWindows.AutoSize = true;
            this.labelWindows.BackColor = System.Drawing.Color.Transparent;
            this.labelWindows.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWindows.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelWindows.Location = new System.Drawing.Point(13, 73);
            this.labelWindows.Name = "labelWindows";
            this.labelWindows.Size = new System.Drawing.Size(0, 17);
            this.labelWindows.TabIndex = 16;
            // 
            // labelSeparatorTop
            // 
            this.labelSeparatorTop.BackColor = System.Drawing.Color.Black;
            this.labelSeparatorTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparatorTop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparatorTop.Location = new System.Drawing.Point(13, 94);
            this.labelSeparatorTop.Name = "labelSeparatorTop";
            this.labelSeparatorTop.Size = new System.Drawing.Size(486, 3);
            this.labelSeparatorTop.TabIndex = 15;
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTitle.Location = new System.Drawing.Point(80, 28);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(170, 25);
            this.labelTitle.TabIndex = 3;
            // 
            // picturePanel
            // 
            this.picturePanel.BackColor = System.Drawing.Color.Transparent;
            this.picturePanel.Location = new System.Drawing.Point(16, 16);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(48, 48);
            this.picturePanel.TabIndex = 2;
            this.picturePanel.TabStop = false;
            // 
            // OperatingSystem
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.labelServicePack);
            this.Controls.Add(this.textboxFrameworkShortVersion);
            this.Controls.Add(this.textboxFrameworkFullVersion);
            this.Controls.Add(this.textboxFrameworkServicePack);
            this.Controls.Add(this.textboxServicePack);
            this.Controls.Add(this.textboxInstallDate);
            this.Controls.Add(this.textboxUserName);
            this.Controls.Add(this.textboxMachineName);
            this.Controls.Add(this.textboxCodeName);
            this.Controls.Add(this.textboxType);
            this.Controls.Add(this.textboxFullVersion);
            this.Controls.Add(this.textboxVersion);
            this.Controls.Add(this.textboxFullName);
            this.Controls.Add(this.textboxProductKey);
            this.Controls.Add(this.textboxProductID);
            this.Controls.Add(this.labelInstallDate);
            this.Controls.Add(this.labelProductID);
            this.Controls.Add(this.labelProductKey);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.labelFrameworkServicePack);
            this.Controls.Add(this.labelFrameworkFullVersion);
            this.Controls.Add(this.labelFrameworkShortVersion);
            this.Controls.Add(this.labelFramework);
            this.Controls.Add(this.labelSeperatorBottom);
            this.Controls.Add(this.labelFullVersion);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.labelMachineName);
            this.Controls.Add(this.labelCodeName);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelFullName);
            this.Controls.Add(this.labelWindows);
            this.Controls.Add(this.labelSeparatorTop);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.picturePanel);
            this.Name = "OperatingSystem";
            this.Load += new System.EventHandler(this.OperatingSystem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.Label labelTitle;
        System.Windows.Forms.PictureBox picturePanel;
        System.Windows.Forms.Label labelCodeName;
        System.Windows.Forms.Label labelVersion;
        System.Windows.Forms.Label labelFullName;
        System.Windows.Forms.Label labelWindows;
        System.Windows.Forms.Label labelSeparatorTop;
        System.Windows.Forms.Label labelMachineName;
        System.Windows.Forms.Label labelUserName;
        System.Windows.Forms.Label labelFullVersion;
        System.Windows.Forms.Label labelFrameworkServicePack;
        System.Windows.Forms.Label labelFrameworkFullVersion;
        System.Windows.Forms.Label labelFrameworkShortVersion;
        System.Windows.Forms.Label labelFramework;
        System.Windows.Forms.Label labelSeperatorBottom;
        Label labelType;
        Label labelProductID;
        Label labelProductKey;
        Label labelInstallDate;
        internal TextBox textboxServicePack;
        internal TextBox textboxInstallDate;
        internal TextBox textboxUserName;
        internal TextBox textboxMachineName;
        internal TextBox textboxCodeName;
        internal TextBox textboxType;
        internal TextBox textboxFullVersion;
        internal TextBox textboxVersion;
        internal TextBox textboxFullName;
        internal TextBox textboxProductKey;
        internal TextBox textboxProductID;
        internal TextBox textboxFrameworkShortVersion;
        internal TextBox textboxFrameworkFullVersion;
        internal TextBox textboxFrameworkServicePack;
        Label labelServicePack;
		
	}
	
}
