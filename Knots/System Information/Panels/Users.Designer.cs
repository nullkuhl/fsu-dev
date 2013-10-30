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
	/// Designer for Users.
	/// </summary>
    public partial class Users : SystemInformation.TaskPanelBase
    {

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        public Users()
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Users));
            this.imagelistUsers = new System.Windows.Forms.ImageList(this.components);
            this.btnSaveRegisteredOrganization = new System.Windows.Forms.Button();
            this.btnSaveRegisteredUser = new System.Windows.Forms.Button();
            this.textboxRegisteredOrganization = new System.Windows.Forms.TextBox();
            this.textboxRegisteredUser = new System.Windows.Forms.TextBox();
            this.labelRegisteredOrganization = new System.Windows.Forms.Label();
            this.labelRegisteredUser = new System.Windows.Forms.Label();
            this.listviewUsers = new System.Windows.Forms.ListView();
            this.UserAccount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FullName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AccountType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Notes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelUserList = new System.Windows.Forms.Label();
            this.labelSeparator2 = new System.Windows.Forms.Label();
            this.labelRegistration = new System.Windows.Forms.Label();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.picturePanel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).BeginInit();
            this.SuspendLayout();
            // 
            // imagelistUsers
            // 
            this.imagelistUsers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagelistUsers.ImageStream")));
            this.imagelistUsers.TransparentColor = System.Drawing.Color.Transparent;
            this.imagelistUsers.Images.SetKeyName(0, "User_16x16.png");
            this.imagelistUsers.Images.SetKeyName(1, "UserDisabled_16x16.png");
            // 
            // btnSaveRegisteredOrganization
            // 
            this.btnSaveRegisteredOrganization.Enabled = false;
            this.btnSaveRegisteredOrganization.Location = new System.Drawing.Point(425, 389);
            this.btnSaveRegisteredOrganization.Name = "btnSaveRegisteredOrganization";
            this.btnSaveRegisteredOrganization.Size = new System.Drawing.Size(73, 23);
            this.btnSaveRegisteredOrganization.TabIndex = 32;
            this.btnSaveRegisteredOrganization.UseVisualStyleBackColor = true;
            this.btnSaveRegisteredOrganization.Click += new System.EventHandler(this.btnRegisteredOrganization_Click);
            // 
            // btnSaveRegisteredUser
            // 
            this.btnSaveRegisteredUser.Enabled = false;
            this.btnSaveRegisteredUser.Location = new System.Drawing.Point(425, 362);
            this.btnSaveRegisteredUser.Name = "btnSaveRegisteredUser";
            this.btnSaveRegisteredUser.Size = new System.Drawing.Size(73, 23);
            this.btnSaveRegisteredUser.TabIndex = 31;
            this.btnSaveRegisteredUser.UseVisualStyleBackColor = true;
            this.btnSaveRegisteredUser.Click += new System.EventHandler(this.btnSaveRegisteredUser_Click);
            // 
            // textboxRegisteredOrganization
            // 
            this.textboxRegisteredOrganization.AcceptsReturn = true;
            this.textboxRegisteredOrganization.Location = new System.Drawing.Point(156, 389);
            this.textboxRegisteredOrganization.Name = "textboxRegisteredOrganization";
            this.textboxRegisteredOrganization.Size = new System.Drawing.Size(263, 23);
            this.textboxRegisteredOrganization.TabIndex = 30;
            this.textboxRegisteredOrganization.TextChanged += new System.EventHandler(this.textboxRegisteredOrganization_TextChanged);
            // 
            // textboxRegisteredUser
            // 
            this.textboxRegisteredUser.Location = new System.Drawing.Point(156, 361);
            this.textboxRegisteredUser.Name = "textboxRegisteredUser";
            this.textboxRegisteredUser.Size = new System.Drawing.Size(263, 23);
            this.textboxRegisteredUser.TabIndex = 29;
            this.textboxRegisteredUser.TextChanged += new System.EventHandler(this.textboxRegisteredUser_TextChanged);
            // 
            // labelRegisteredOrganization
            // 
            this.labelRegisteredOrganization.AutoSize = true;
            this.labelRegisteredOrganization.BackColor = System.Drawing.Color.Transparent;
            this.labelRegisteredOrganization.Location = new System.Drawing.Point(16, 393);
            this.labelRegisteredOrganization.Name = "labelRegisteredOrganization";
            this.labelRegisteredOrganization.Size = new System.Drawing.Size(0, 15);
            this.labelRegisteredOrganization.TabIndex = 28;
            // 
            // labelRegisteredUser
            // 
            this.labelRegisteredUser.AutoSize = true;
            this.labelRegisteredUser.BackColor = System.Drawing.Color.Transparent;
            this.labelRegisteredUser.Location = new System.Drawing.Point(16, 365);
            this.labelRegisteredUser.Name = "labelRegisteredUser";
            this.labelRegisteredUser.Size = new System.Drawing.Size(0, 15);
            this.labelRegisteredUser.TabIndex = 27;
            // 
            // listviewUsers
            // 
            this.listviewUsers.BackColor = System.Drawing.SystemColors.Window;
            this.listviewUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UserAccount,
            this.FullName,
            this.AccountType,
            this.Notes});
            this.listviewUsers.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewUsers.ForeColor = System.Drawing.SystemColors.ControlText;
            this.listviewUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listviewUsers.Location = new System.Drawing.Point(16, 112);
            this.listviewUsers.Name = "listviewUsers";
            this.listviewUsers.ShowGroups = false;
            this.listviewUsers.Size = new System.Drawing.Size(482, 194);
            this.listviewUsers.SmallImageList = this.imagelistUsers;
            this.listviewUsers.TabIndex = 19;
            this.listviewUsers.UseCompatibleStateImageBehavior = false;
            this.listviewUsers.View = System.Windows.Forms.View.Details;
            // 
            // UserAccount
            // 
            this.UserAccount.Width = 125;
            // 
            // FullName
            // 
            this.FullName.Width = 135;
            // 
            // AccountType
            // 
            this.AccountType.Width = 90;
            // 
            // Notes
            // 
            this.Notes.Width = 115;
            // 
            // labelUserList
            // 
            this.labelUserList.AutoSize = true;
            this.labelUserList.BackColor = System.Drawing.Color.Transparent;
            this.labelUserList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUserList.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelUserList.Location = new System.Drawing.Point(16, 79);
            this.labelUserList.Name = "labelUserList";
            this.labelUserList.Size = new System.Drawing.Size(0, 17);
            this.labelUserList.TabIndex = 18;
            // 
            // labelSeparator2
            // 
            this.labelSeparator2.BackColor = System.Drawing.Color.Black;
            this.labelSeparator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator2.Location = new System.Drawing.Point(16, 345);
            this.labelSeparator2.Name = "labelSeparator2";
            this.labelSeparator2.Size = new System.Drawing.Size(482, 3);
            this.labelSeparator2.TabIndex = 17;
            // 
            // labelRegistration
            // 
            this.labelRegistration.AutoSize = true;
            this.labelRegistration.BackColor = System.Drawing.Color.Transparent;
            this.labelRegistration.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRegistration.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelRegistration.Location = new System.Drawing.Point(16, 322);
            this.labelRegistration.Name = "labelRegistration";
            this.labelRegistration.Size = new System.Drawing.Size(0, 17);
            this.labelRegistration.TabIndex = 15;
            // 
            // labelSeparator
            // 
            this.labelSeparator.BackColor = System.Drawing.Color.Black;
            this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator.Location = new System.Drawing.Point(16, 100);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(482, 3);
            this.labelSeparator.TabIndex = 14;
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
            this.picturePanel.Image = global::SystemInformation.Properties.Resources.Users_48x48;
            this.picturePanel.Location = new System.Drawing.Point(16, 16);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(48, 48);
            this.picturePanel.TabIndex = 6;
            this.picturePanel.TabStop = false;
            // 
            // Users
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.btnSaveRegisteredOrganization);
            this.Controls.Add(this.btnSaveRegisteredUser);
            this.Controls.Add(this.textboxRegisteredOrganization);
            this.Controls.Add(this.textboxRegisteredUser);
            this.Controls.Add(this.labelRegisteredOrganization);
            this.Controls.Add(this.labelRegisteredUser);
            this.Controls.Add(this.listviewUsers);
            this.Controls.Add(this.labelUserList);
            this.Controls.Add(this.labelSeparator2);
            this.Controls.Add(this.labelRegistration);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.picturePanel);
            this.Name = "Users";
            this.Load += new System.EventHandler(this.Users_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        System.Windows.Forms.Label labelTitle;
        System.Windows.Forms.PictureBox picturePanel;
        System.Windows.Forms.Label labelRegistration;
        System.Windows.Forms.Label labelSeparator;
        System.Windows.Forms.ImageList imagelistUsers;
        System.Windows.Forms.ListView listviewUsers;
        System.Windows.Forms.Label labelUserList;
        System.Windows.Forms.Label labelSeparator2;
        System.Windows.Forms.ColumnHeader UserAccount;
        System.Windows.Forms.ColumnHeader FullName;
        System.Windows.Forms.ColumnHeader AccountType;
        Button btnSaveRegisteredOrganization;
        Button btnSaveRegisteredUser;
        TextBox textboxRegisteredOrganization;
        TextBox textboxRegisteredUser;
        Label labelRegisteredOrganization;
        Label labelRegisteredUser;
        ColumnHeader Notes;

        #endregion
		
	}
	
}
