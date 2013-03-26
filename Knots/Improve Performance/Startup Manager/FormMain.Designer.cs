using System.Resources;
using System.Globalization;
using System.Threading;

namespace StartupManager
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

        public ResourceManager rm = new ResourceManager("StartupManager.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());


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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStripStartupManager = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonDisable = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEnable = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExecute = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMoveToCurrentUser = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMoveToAllUsers = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStripStartupManager = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExecute = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemMoveToCurrentUser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMoveToAllUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListStartupManager = new System.Windows.Forms.ImageList(this.components);
            this.listviewStartup = new System.Windows.Forms.ListView();
            this.ItemName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolTipStartupManager = new System.Windows.Forms.ToolTip(this.components);
            this.panelDetails = new System.Windows.Forms.Panel();
            this.firstCover = new System.Windows.Forms.Label();
            this.labelArguments = new System.Windows.Forms.Label();
            this.labelProductNameDesc = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelCommandDesc = new System.Windows.Forms.Label();
            this.labelFileVersionDesc = new System.Windows.Forms.Label();
            this.labelDescriptionDesc = new System.Windows.Forms.Label();
            this.labelCompanyDesc = new System.Windows.Forms.Label();
            this.labelCommand = new System.Windows.Forms.Label();
            this.labelFileVersion = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelCompany = new System.Windows.Forms.Label();
            this.labelDetails = new System.Windows.Forms.Label();
            this.pictureBoxPanel = new System.Windows.Forms.PictureBox();
            this.ucTop = new StartupManager.TopControl();
            this.ucBottom = new StartupManager.BottomControl();
            this.toolStripStartupManager.SuspendLayout();
            this.contextMenuStripStartupManager.SuspendLayout();
            this.panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripStartupManager
            // 
            this.toolStripStartupManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.toolStripStartupManager.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonDisable,
            this.toolStripButtonEnable,
            this.toolStripButtonDelete,
            this.toolStripButtonOpen,
            this.toolStripButtonExecute,
            this.toolStripButtonMoveToCurrentUser,
            this.toolStripButtonMoveToAllUsers,
            this.toolStripButtonRefresh});
            this.toolStripStartupManager.Location = new System.Drawing.Point(0, 64);
            this.toolStripStartupManager.MinimumSize = new System.Drawing.Size(792, 0);
            this.toolStripStartupManager.Name = "toolStripStartupManager";
            this.toolStripStartupManager.Size = new System.Drawing.Size(929, 25);
            this.toolStripStartupManager.Stretch = true;
            this.toolStripStartupManager.TabIndex = 20;
            this.toolStripStartupManager.Text = "toolStrip1";
            // 
            // toolStripButtonDisable
            // 
            this.toolStripButtonDisable.Enabled = false;
            this.toolStripButtonDisable.Image = global::StartupManager.Properties.Resources.Disable;
            this.toolStripButtonDisable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDisable.Name = "toolStripButtonDisable";
            this.toolStripButtonDisable.Size = new System.Drawing.Size(76, 22);
            this.toolStripButtonDisable.Text = "Disabled";
            this.toolStripButtonDisable.ToolTipText = "Disable Startup Item";
            this.toolStripButtonDisable.Click += new System.EventHandler(this.toolStripButtonDisable_Click);
            // 
            // toolStripButtonEnable
            // 
            this.toolStripButtonEnable.Enabled = false;
            this.toolStripButtonEnable.Image = global::StartupManager.Properties.Resources.Enable;
            this.toolStripButtonEnable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEnable.Name = "toolStripButtonEnable";
            this.toolStripButtonEnable.Size = new System.Drawing.Size(66, 22);
            this.toolStripButtonEnable.Text = "Enable";
            this.toolStripButtonEnable.ToolTipText = "Enable Startup Item";
            this.toolStripButtonEnable.Click += new System.EventHandler(this.toolStripButtonEnable_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.Enabled = false;
            this.toolStripButtonDelete.Image = global::StartupManager.Properties.Resources._1303026989_101;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(63, 22);
            this.toolStripButtonDelete.Text = "Delete";
            this.toolStripButtonDelete.ToolTipText = "Delete Startup Item";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.Enabled = false;
            this.toolStripButtonOpen.Image = global::StartupManager.Properties.Resources._1303028470_folder_horizontal_open;
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(95, 22);
            this.toolStripButtonOpen.Text = "Open Folder";
            this.toolStripButtonOpen.ToolTipText = "Open Folder Containing Startup Item";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // toolStripButtonExecute
            // 
            this.toolStripButtonExecute.Enabled = false;
            this.toolStripButtonExecute.Image = global::StartupManager.Properties.Resources.Execute;
            this.toolStripButtonExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExecute.Name = "toolStripButtonExecute";
            this.toolStripButtonExecute.Size = new System.Drawing.Size(71, 22);
            this.toolStripButtonExecute.Text = "Execute";
            this.toolStripButtonExecute.ToolTipText = "Execute Command";
            this.toolStripButtonExecute.Click += new System.EventHandler(this.toolStripButtonExecute_Click);
            // 
            // toolStripButtonMoveToCurrentUser
            // 
            this.toolStripButtonMoveToCurrentUser.Enabled = false;
            this.toolStripButtonMoveToCurrentUser.Image = global::StartupManager.Properties.Resources.Current_User;
            this.toolStripButtonMoveToCurrentUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMoveToCurrentUser.Name = "toolStripButtonMoveToCurrentUser";
            this.toolStripButtonMoveToCurrentUser.Size = new System.Drawing.Size(99, 22);
            this.toolStripButtonMoveToCurrentUser.Text = "Move to User";
            this.toolStripButtonMoveToCurrentUser.ToolTipText = "Move Item to Current User";
            this.toolStripButtonMoveToCurrentUser.Click += new System.EventHandler(this.toolStripButtonMoveToCurrentUser_Click);
            // 
            // toolStripButtonMoveToAllUsers
            // 
            this.toolStripButtonMoveToAllUsers.Enabled = false;
            this.toolStripButtonMoveToAllUsers.Image = global::StartupManager.Properties.Resources.All_Users;
            this.toolStripButtonMoveToAllUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMoveToAllUsers.Name = "toolStripButtonMoveToAllUsers";
            this.toolStripButtonMoveToAllUsers.Size = new System.Drawing.Size(86, 22);
            this.toolStripButtonMoveToAllUsers.Text = "Move to All";
            this.toolStripButtonMoveToAllUsers.ToolTipText = "Move Item to All Users";
            this.toolStripButtonMoveToAllUsers.Click += new System.EventHandler(this.toolStripButtonMoveToAllUsers_Click);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.Image = global::StartupManager.Properties.Resources._1303027013_gtk_refresh;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(70, 22);
            this.toolStripButtonRefresh.Text = "Refresh";
            this.toolStripButtonRefresh.ToolTipText = "Refresh Display";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // contextMenuStripStartupManager
            // 
            this.contextMenuStripStartupManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.contextMenuStripStartupManager.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDisable,
            this.toolStripMenuItemEnable,
            this.toolStripMenuItemDelete,
            this.toolStripMenuItemOpen,
            this.toolStripMenuItemExecute,
            this.toolStripSeparator1,
            this.toolStripMenuItemMoveToCurrentUser,
            this.toolStripMenuItemMoveToAllUsers});
            this.contextMenuStripStartupManager.Name = "ContextMenuStrip";
            this.contextMenuStripStartupManager.Size = new System.Drawing.Size(190, 164);
            // 
            // toolStripMenuItemDisable
            // 
            this.toolStripMenuItemDisable.Enabled = false;
            this.toolStripMenuItemDisable.Image = global::StartupManager.Properties.Resources.Disable;
            this.toolStripMenuItemDisable.Name = "toolStripMenuItemDisable";
            this.toolStripMenuItemDisable.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItemDisable.Text = "&Disable";
            this.toolStripMenuItemDisable.Click += new System.EventHandler(this.toolStripMenuItemDisable_Click);
            // 
            // toolStripMenuItemEnable
            // 
            this.toolStripMenuItemEnable.Enabled = false;
            this.toolStripMenuItemEnable.Image = global::StartupManager.Properties.Resources.Enable;
            this.toolStripMenuItemEnable.Name = "toolStripMenuItemEnable";
            this.toolStripMenuItemEnable.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItemEnable.Text = "&Enable";
            this.toolStripMenuItemEnable.Click += new System.EventHandler(this.toolStripMenuItemEnable_Click);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Enabled = false;
            this.toolStripMenuItemDelete.Image = global::StartupManager.Properties.Resources.Delete;
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItemDelete.Text = "Delete";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.Enabled = false;
            this.toolStripMenuItemOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.toolStripMenuItemOpen.Image = global::StartupManager.Properties.Resources.Folder;
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItemOpen.Text = "&Open File Folder";
            this.toolStripMenuItemOpen.Click += new System.EventHandler(this.toolStripMenuItemOpen_Click);
            // 
            // toolStripMenuItemExecute
            // 
            this.toolStripMenuItemExecute.Enabled = false;
            this.toolStripMenuItemExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.toolStripMenuItemExecute.Image = global::StartupManager.Properties.Resources.Execute;
            this.toolStripMenuItemExecute.Name = "toolStripMenuItemExecute";
            this.toolStripMenuItemExecute.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItemExecute.Text = "Execute Command";
            this.toolStripMenuItemExecute.Click += new System.EventHandler(this.toolStripMenuItemExecute_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // toolStripMenuItemMoveToCurrentUser
            // 
            this.toolStripMenuItemMoveToCurrentUser.Enabled = false;
            this.toolStripMenuItemMoveToCurrentUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.toolStripMenuItemMoveToCurrentUser.Image = global::StartupManager.Properties.Resources.Current_User;
            this.toolStripMenuItemMoveToCurrentUser.Name = "toolStripMenuItemMoveToCurrentUser";
            this.toolStripMenuItemMoveToCurrentUser.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItemMoveToCurrentUser.Text = "Move to Current &User";
            this.toolStripMenuItemMoveToCurrentUser.Click += new System.EventHandler(this.toolStripMenuItemMoveToCurrentUser_Click);
            // 
            // toolStripMenuItemMoveToAllUsers
            // 
            this.toolStripMenuItemMoveToAllUsers.Enabled = false;
            this.toolStripMenuItemMoveToAllUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.toolStripMenuItemMoveToAllUsers.Image = global::StartupManager.Properties.Resources.All_Users;
            this.toolStripMenuItemMoveToAllUsers.Name = "toolStripMenuItemMoveToAllUsers";
            this.toolStripMenuItemMoveToAllUsers.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItemMoveToAllUsers.Text = "Move to &All Users";
            this.toolStripMenuItemMoveToAllUsers.Click += new System.EventHandler(this.toolStripMenuItemMoveToAllUsers_Click);
            // 
            // imageListStartupManager
            // 
            this.imageListStartupManager.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListStartupManager.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListStartupManager.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listviewStartup
            // 
            this.listviewStartup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listviewStartup.BackColor = System.Drawing.SystemColors.Window;
            this.listviewStartup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ItemName,
            this.FileName,
            this.Type,
            this.Status});
            this.listviewStartup.ContextMenuStrip = this.contextMenuStripStartupManager;
            this.listviewStartup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listviewStartup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.listviewStartup.FullRowSelect = true;
            this.listviewStartup.LabelWrap = false;
            this.listviewStartup.Location = new System.Drawing.Point(0, 90);
            this.listviewStartup.MultiSelect = false;
            this.listviewStartup.Name = "listviewStartup";
            this.listviewStartup.Size = new System.Drawing.Size(929, 302);
            this.listviewStartup.SmallImageList = this.imageListStartupManager;
            this.listviewStartup.TabIndex = 49;
            this.listviewStartup.UseCompatibleStateImageBehavior = false;
            this.listviewStartup.View = System.Windows.Forms.View.Details;
            this.listviewStartup.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listviewStartup_ColumnClick);
            this.listviewStartup.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listviewStartup_ItemSelectionChanged);
            // 
            // ItemName
            // 
            this.ItemName.Text = "Item Name";
            this.ItemName.Width = 221;
            // 
            // FileName
            // 
            this.FileName.Text = "File Name";
            this.FileName.Width = 205;
            // 
            // Type
            // 
            this.Type.Text = "Location";
            this.Type.Width = 333;
            // 
            // Status
            // 
            this.Status.Text = "status";
            this.Status.Width = 135;
            // 
            // panelDetails
            // 
            this.panelDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetails.Controls.Add(this.firstCover);
            this.panelDetails.Controls.Add(this.labelArguments);
            this.panelDetails.Controls.Add(this.labelProductNameDesc);
            this.panelDetails.Controls.Add(this.labelProductName);
            this.panelDetails.Controls.Add(this.labelCommandDesc);
            this.panelDetails.Controls.Add(this.labelFileVersionDesc);
            this.panelDetails.Controls.Add(this.labelDescriptionDesc);
            this.panelDetails.Controls.Add(this.labelCompanyDesc);
            this.panelDetails.Controls.Add(this.labelCommand);
            this.panelDetails.Controls.Add(this.labelFileVersion);
            this.panelDetails.Controls.Add(this.labelDescription);
            this.panelDetails.Controls.Add(this.labelCompany);
            this.panelDetails.Controls.Add(this.labelDetails);
            this.panelDetails.Controls.Add(this.pictureBoxPanel);
            this.panelDetails.Location = new System.Drawing.Point(0, 391);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(929, 101);
            this.panelDetails.TabIndex = 2;
            // 
            // firstCover
            // 
            this.firstCover.BackColor = System.Drawing.SystemColors.Control;
            this.firstCover.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.firstCover.Location = new System.Drawing.Point(0, 0);
            this.firstCover.Name = "firstCover";
            this.firstCover.Size = new System.Drawing.Size(752, 101);
            this.firstCover.TabIndex = 88;
            // 
            // labelArguments
            // 
            this.labelArguments.BackColor = System.Drawing.Color.Transparent;
            this.labelArguments.ForeColor = System.Drawing.Color.Black;
            this.labelArguments.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelArguments.Location = new System.Drawing.Point(170, 75);
            this.labelArguments.Name = "labelArguments";
            this.labelArguments.Size = new System.Drawing.Size(488, 15);
            this.labelArguments.TabIndex = 87;
            this.labelArguments.Visible = false;
            // 
            // labelProductNameDesc
            // 
            this.labelProductNameDesc.AutoSize = true;
            this.labelProductNameDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelProductNameDesc.ForeColor = System.Drawing.Color.Black;
            this.labelProductNameDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelProductNameDesc.Location = new System.Drawing.Point(93, 33);
            this.labelProductNameDesc.Name = "labelProductNameDesc";
            this.labelProductNameDesc.Size = new System.Drawing.Size(52, 15);
            this.labelProductNameDesc.TabIndex = 85;
            this.labelProductNameDesc.Text = "Product:";
            // 
            // labelProductName
            // 
            this.labelProductName.BackColor = System.Drawing.Color.Transparent;
            this.labelProductName.ForeColor = System.Drawing.Color.Black;
            this.labelProductName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelProductName.Location = new System.Drawing.Point(170, 33);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(217, 17);
            this.labelProductName.TabIndex = 84;
            // 
            // labelCommandDesc
            // 
            this.labelCommandDesc.AutoSize = true;
            this.labelCommandDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelCommandDesc.ForeColor = System.Drawing.Color.Black;
            this.labelCommandDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCommandDesc.Location = new System.Drawing.Point(93, 55);
            this.labelCommandDesc.Name = "labelCommandDesc";
            this.labelCommandDesc.Size = new System.Drawing.Size(71, 15);
            this.labelCommandDesc.TabIndex = 83;
            this.labelCommandDesc.Text = "Command :";
            // 
            // labelFileVersionDesc
            // 
            this.labelFileVersionDesc.AutoSize = true;
            this.labelFileVersionDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelFileVersionDesc.ForeColor = System.Drawing.Color.Black;
            this.labelFileVersionDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelFileVersionDesc.Location = new System.Drawing.Point(393, 33);
            this.labelFileVersionDesc.Name = "labelFileVersionDesc";
            this.labelFileVersionDesc.Size = new System.Drawing.Size(74, 15);
            this.labelFileVersionDesc.TabIndex = 82;
            this.labelFileVersionDesc.Text = "File Version:";
            // 
            // labelDescriptionDesc
            // 
            this.labelDescriptionDesc.AutoSize = true;
            this.labelDescriptionDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelDescriptionDesc.ForeColor = System.Drawing.Color.Black;
            this.labelDescriptionDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDescriptionDesc.Location = new System.Drawing.Point(393, 13);
            this.labelDescriptionDesc.Name = "labelDescriptionDesc";
            this.labelDescriptionDesc.Size = new System.Drawing.Size(72, 15);
            this.labelDescriptionDesc.TabIndex = 81;
            this.labelDescriptionDesc.Text = "Description:";
            // 
            // labelCompanyDesc
            // 
            this.labelCompanyDesc.AutoSize = true;
            this.labelCompanyDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelCompanyDesc.ForeColor = System.Drawing.Color.Black;
            this.labelCompanyDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCompanyDesc.Location = new System.Drawing.Point(93, 13);
            this.labelCompanyDesc.Name = "labelCompanyDesc";
            this.labelCompanyDesc.Size = new System.Drawing.Size(62, 15);
            this.labelCompanyDesc.TabIndex = 80;
            this.labelCompanyDesc.Text = "Company:";
            // 
            // labelCommand
            // 
            this.labelCommand.BackColor = System.Drawing.Color.Transparent;
            this.labelCommand.ForeColor = System.Drawing.Color.Black;
            this.labelCommand.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCommand.Location = new System.Drawing.Point(170, 55);
            this.labelCommand.Name = "labelCommand";
            this.labelCommand.Size = new System.Drawing.Size(552, 35);
            this.labelCommand.TabIndex = 79;
            // 
            // labelFileVersion
            // 
            this.labelFileVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelFileVersion.ForeColor = System.Drawing.Color.Black;
            this.labelFileVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelFileVersion.Location = new System.Drawing.Point(510, 33);
            this.labelFileVersion.Name = "labelFileVersion";
            this.labelFileVersion.Size = new System.Drawing.Size(238, 17);
            this.labelFileVersion.TabIndex = 78;
            // 
            // labelDescription
            // 
            this.labelDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelDescription.ForeColor = System.Drawing.Color.Black;
            this.labelDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDescription.Location = new System.Drawing.Point(510, 13);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(238, 17);
            this.labelDescription.TabIndex = 77;
            // 
            // labelCompany
            // 
            this.labelCompany.BackColor = System.Drawing.Color.Transparent;
            this.labelCompany.ForeColor = System.Drawing.Color.Black;
            this.labelCompany.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCompany.Location = new System.Drawing.Point(170, 13);
            this.labelCompany.Name = "labelCompany";
            this.labelCompany.Size = new System.Drawing.Size(217, 17);
            this.labelCompany.TabIndex = 76;
            // 
            // labelDetails
            // 
            this.labelDetails.AutoSize = true;
            this.labelDetails.BackColor = System.Drawing.Color.Transparent;
            this.labelDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelDetails.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelDetails.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDetails.Location = new System.Drawing.Point(10, 12);
            this.labelDetails.Name = "labelDetails";
            this.labelDetails.Size = new System.Drawing.Size(57, 16);
            this.labelDetails.TabIndex = 64;
            this.labelDetails.Text = "Details";
            // 
            // pictureBoxPanel
            // 
            this.pictureBoxPanel.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxPanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBoxPanel.Location = new System.Drawing.Point(21, 39);
            this.pictureBoxPanel.Name = "pictureBoxPanel";
            this.pictureBoxPanel.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxPanel.TabIndex = 7;
            this.pictureBoxPanel.TabStop = false;
            // 
            // ucTop
            // 
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTop.Location = new System.Drawing.Point(0, 0);
            this.ucTop.Name = "ucTop";
            this.ucTop.Size = new System.Drawing.Size(929, 64);
            this.ucTop.TabIndex = 0;
            // 
            // ucBottom
            // 
            this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucBottom.Location = new System.Drawing.Point(0, 495);
            this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
            this.ucBottom.MaximumSize = new System.Drawing.Size(0, 27);
            this.ucBottom.MinimumSize = new System.Drawing.Size(0, 27);
            this.ucBottom.Name = "ucBottom";
            this.ucBottom.Size = new System.Drawing.Size(929, 27);
            this.ucBottom.TabIndex = 51;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 522);
            this.Controls.Add(this.toolStripStartupManager);
            this.Controls.Add(this.ucTop);
            this.Controls.Add(this.listviewStartup);
            this.Controls.Add(this.panelDetails);
            this.Controls.Add(this.ucBottom);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Startup Manager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.toolStripStartupManager.ResumeLayout(false);
            this.toolStripStartupManager.PerformLayout();
            this.contextMenuStripStartupManager.ResumeLayout(false);
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.ToolStrip toolStripStartupManager;
        System.Windows.Forms.Panel panelDetails;
        System.Windows.Forms.ContextMenuStrip contextMenuStripStartupManager;
        System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDisable;
        System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEnable;
        System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
        System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExecute;
        System.Windows.Forms.ImageList imageListStartupManager;
        System.Windows.Forms.ListView listviewStartup;
        System.Windows.Forms.ColumnHeader ItemName;
        System.Windows.Forms.ColumnHeader FileName;
        System.Windows.Forms.ColumnHeader Type;
        System.Windows.Forms.ColumnHeader Status;
        System.Windows.Forms.PictureBox pictureBoxPanel;
        System.Windows.Forms.Label labelDetails;
        System.Windows.Forms.Label labelArguments;
        System.Windows.Forms.Label labelProductNameDesc;
        System.Windows.Forms.Label labelProductName;
        System.Windows.Forms.Label labelCommandDesc;
        System.Windows.Forms.Label labelFileVersionDesc;
        System.Windows.Forms.Label labelDescriptionDesc;
        System.Windows.Forms.Label labelCompanyDesc;
        System.Windows.Forms.Label labelCommand;
        System.Windows.Forms.Label labelFileVersion;
        System.Windows.Forms.Label labelDescription;
        System.Windows.Forms.Label labelCompany;
        System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMoveToCurrentUser;
        System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMoveToAllUsers;
        System.Windows.Forms.ToolStripButton toolStripButtonDisable;
        System.Windows.Forms.ToolStripButton toolStripButtonEnable;
        System.Windows.Forms.ToolTip toolTipStartupManager;
        System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        System.Windows.Forms.ToolStripButton toolStripButtonExecute;
        System.Windows.Forms.ToolStripButton toolStripButtonMoveToCurrentUser;
        System.Windows.Forms.ToolStripButton toolStripButtonMoveToAllUsers;
        System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        System.Windows.Forms.Label firstCover;
        TopControl ucTop;
        BottomControl ucBottom;
    }
}

