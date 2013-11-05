using System.Resources;
using System.Globalization;
using System.Threading;

namespace Context_Menu_Manager
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

		public ResourceManager rm = new ResourceManager("Context_Menu_Manager.Resources",
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tabPageFilesFolders = new System.Windows.Forms.TabPage();
            this.filesFoldersListView = new System.Windows.Forms.ListView();
            this.clhFirst = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhSecond = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileDescriptionListView = new System.Windows.Forms.ListView();
            this.clhThird = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhFourth = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblCheck = new System.Windows.Forms.Label();
            this.SaveFilesFolders = new System.Windows.Forms.Button();
            this.removeFilesFoldersButton = new System.Windows.Forms.Button();
            this.tabPageNew = new System.Windows.Forms.TabPage();
            this.SaveNew = new System.Windows.Forms.Button();
            this.newListView = new System.Windows.Forms.ListView();
            this.clhFifth = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblSelect = new System.Windows.Forms.Label();
            this.removeNewButton = new System.Windows.Forms.Button();
            this.tabPageSendTo = new System.Windows.Forms.TabPage();
            this.SaveSend = new System.Windows.Forms.Button();
            this.sendToListView = new System.Windows.Forms.ListView();
            this.clhSix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblChoose = new System.Windows.Forms.Label();
            this.removeSendToButton = new System.Windows.Forms.Button();
            this.ucBottom = new Context_Menu_Manager.BottomControl();
            this.ucTop = new Context_Menu_Manager.TopControl();
            this.tbcMain.SuspendLayout();
            this.tabPageFilesFolders.SuspendLayout();
            this.tabPageNew.SuspendLayout();
            this.tabPageSendTo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcMain
            // 
            this.tbcMain.Controls.Add(this.tabPageFilesFolders);
            this.tbcMain.Controls.Add(this.tabPageNew);
            this.tbcMain.Controls.Add(this.tabPageSendTo);
            this.tbcMain.Location = new System.Drawing.Point(6, 69);
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(485, 470);
            this.tbcMain.TabIndex = 0;
            // 
            // tabPageFilesFolders
            // 
            this.tabPageFilesFolders.Controls.Add(this.filesFoldersListView);
            this.tabPageFilesFolders.Controls.Add(this.fileDescriptionListView);
            this.tabPageFilesFolders.Controls.Add(this.lblCheck);
            this.tabPageFilesFolders.Controls.Add(this.SaveFilesFolders);
            this.tabPageFilesFolders.Controls.Add(this.removeFilesFoldersButton);
            this.tabPageFilesFolders.Location = new System.Drawing.Point(4, 22);
            this.tabPageFilesFolders.Name = "tabPageFilesFolders";
            this.tabPageFilesFolders.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFilesFolders.Size = new System.Drawing.Size(477, 444);
            this.tabPageFilesFolders.TabIndex = 0;
            this.tabPageFilesFolders.Text = "files_folders";
            this.tabPageFilesFolders.UseVisualStyleBackColor = true;
            this.tabPageFilesFolders.Enter += new System.EventHandler(this.tabPageFilesFolders_Enter);
            // 
            // filesFoldersListView
            // 
            this.filesFoldersListView.CheckBoxes = true;
            this.filesFoldersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhFirst,
            this.clhSecond});
            this.filesFoldersListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.filesFoldersListView.Location = new System.Drawing.Point(8, 47);
            this.filesFoldersListView.MultiSelect = false;
            this.filesFoldersListView.Name = "filesFoldersListView";
            this.filesFoldersListView.Size = new System.Drawing.Size(461, 200);
            this.filesFoldersListView.TabIndex = 8;
            this.filesFoldersListView.UseCompatibleStateImageBehavior = false;
            this.filesFoldersListView.View = System.Windows.Forms.View.Details;
            this.filesFoldersListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.filesFoldersListView_ItemChecked);
            this.filesFoldersListView.SelectedIndexChanged += new System.EventHandler(this.filesFoldersListView_SelectedIndexChanged);
            this.filesFoldersListView.Click += new System.EventHandler(this.filesFoldersListView_Click);
            // 
            // clhFirst
            // 
            this.clhFirst.Text = "column_header";
            this.clhFirst.Width = 324;
            // 
            // clhSecond
            // 
            this.clhSecond.Text = "column_header";
            this.clhSecond.Width = 133;
            // 
            // fileDescriptionListView
            // 
            this.fileDescriptionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhThird,
            this.clhFourth});
            this.fileDescriptionListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.fileDescriptionListView.Location = new System.Drawing.Point(8, 255);
            this.fileDescriptionListView.Name = "fileDescriptionListView";
            this.fileDescriptionListView.Size = new System.Drawing.Size(461, 153);
            this.fileDescriptionListView.TabIndex = 7;
            this.fileDescriptionListView.UseCompatibleStateImageBehavior = false;
            this.fileDescriptionListView.View = System.Windows.Forms.View.Details;
            // 
            // clhThird
            // 
            this.clhThird.Width = 139;
            // 
            // clhFourth
            // 
            this.clhFourth.Width = 300;
            // 
            // lblCheck
            // 
            this.lblCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblCheck.Location = new System.Drawing.Point(8, 10);
            this.lblCheck.Name = "lblCheck";
            this.lblCheck.Size = new System.Drawing.Size(461, 35);
            this.lblCheck.TabIndex = 2;
            this.lblCheck.Text = "click_check_mark1";
            // 
            // SaveFilesFolders
            // 
            this.SaveFilesFolders.Location = new System.Drawing.Point(301, 414);
            this.SaveFilesFolders.Name = "SaveFilesFolders";
            this.SaveFilesFolders.Size = new System.Drawing.Size(82, 24);
            this.SaveFilesFolders.TabIndex = 1;
            this.SaveFilesFolders.Text = "save";
            this.SaveFilesFolders.UseVisualStyleBackColor = true;
            this.SaveFilesFolders.Visible = false;
            this.SaveFilesFolders.Click += new System.EventHandler(this.SaveFilesAndFolders_Click);
            // 
            // removeFilesFoldersButton
            // 
            this.removeFilesFoldersButton.Location = new System.Drawing.Point(389, 414);
            this.removeFilesFoldersButton.Name = "removeFilesFoldersButton";
            this.removeFilesFoldersButton.Size = new System.Drawing.Size(82, 24);
            this.removeFilesFoldersButton.TabIndex = 1;
            this.removeFilesFoldersButton.Text = "remove";
            this.removeFilesFoldersButton.UseVisualStyleBackColor = true;
            this.removeFilesFoldersButton.Click += new System.EventHandler(this.removeFilesFoldersButton_Click);
            // 
            // tabPageNew
            // 
            this.tabPageNew.Controls.Add(this.SaveNew);
            this.tabPageNew.Controls.Add(this.newListView);
            this.tabPageNew.Controls.Add(this.lblSelect);
            this.tabPageNew.Controls.Add(this.removeNewButton);
            this.tabPageNew.Location = new System.Drawing.Point(4, 22);
            this.tabPageNew.Name = "tabPageNew";
            this.tabPageNew.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNew.Size = new System.Drawing.Size(477, 444);
            this.tabPageNew.TabIndex = 1;
            this.tabPageNew.Text = "new";
            this.tabPageNew.UseVisualStyleBackColor = true;
            this.tabPageNew.Enter += new System.EventHandler(this.tabPageNew_Enter);
            // 
            // SaveNew
            // 
            this.SaveNew.Location = new System.Drawing.Point(301, 414);
            this.SaveNew.Name = "SaveNew";
            this.SaveNew.Size = new System.Drawing.Size(82, 24);
            this.SaveNew.TabIndex = 8;
            this.SaveNew.Text = "save";
            this.SaveNew.UseVisualStyleBackColor = true;
            this.SaveNew.Visible = false;
            this.SaveNew.Click += new System.EventHandler(this.SaveNew_Click);
            // 
            // newListView
            // 
            this.newListView.CheckBoxes = true;
            this.newListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhFifth});
            this.newListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.newListView.Location = new System.Drawing.Point(8, 47);
            this.newListView.MultiSelect = false;
            this.newListView.Name = "newListView";
            this.newListView.Size = new System.Drawing.Size(461, 361);
            this.newListView.TabIndex = 7;
            this.newListView.UseCompatibleStateImageBehavior = false;
            this.newListView.View = System.Windows.Forms.View.Details;
            this.newListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.newListView_ItemChecked);
            this.newListView.Click += new System.EventHandler(this.newListView_Click);
            // 
            // clhFifth
            // 
            this.clhFifth.Width = 440;
            // 
            // lblSelect
            // 
            this.lblSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblSelect.Location = new System.Drawing.Point(8, 10);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(461, 35);
            this.lblSelect.TabIndex = 3;
            this.lblSelect.Text = "click_check_mark1\r\nclick_check_mark2.\r\n";
            // 
            // removeNewButton
            // 
            this.removeNewButton.Location = new System.Drawing.Point(389, 414);
            this.removeNewButton.Name = "removeNewButton";
            this.removeNewButton.Size = new System.Drawing.Size(82, 24);
            this.removeNewButton.TabIndex = 1;
            this.removeNewButton.Text = "remove";
            this.removeNewButton.UseVisualStyleBackColor = true;
            this.removeNewButton.Click += new System.EventHandler(this.removeNewButton_Click);
            // 
            // tabPageSendTo
            // 
            this.tabPageSendTo.Controls.Add(this.SaveSend);
            this.tabPageSendTo.Controls.Add(this.sendToListView);
            this.tabPageSendTo.Controls.Add(this.lblChoose);
            this.tabPageSendTo.Controls.Add(this.removeSendToButton);
            this.tabPageSendTo.Location = new System.Drawing.Point(4, 22);
            this.tabPageSendTo.Name = "tabPageSendTo";
            this.tabPageSendTo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSendTo.Size = new System.Drawing.Size(477, 444);
            this.tabPageSendTo.TabIndex = 2;
            this.tabPageSendTo.Text = "send_to";
            this.tabPageSendTo.UseVisualStyleBackColor = true;
            this.tabPageSendTo.Enter += new System.EventHandler(this.tabPageSendTo_Enter);
            // 
            // SaveSend
            // 
            this.SaveSend.Location = new System.Drawing.Point(301, 414);
            this.SaveSend.Name = "SaveSend";
            this.SaveSend.Size = new System.Drawing.Size(82, 24);
            this.SaveSend.TabIndex = 9;
            this.SaveSend.Text = "save";
            this.SaveSend.UseVisualStyleBackColor = true;
            this.SaveSend.Visible = false;
            this.SaveSend.Click += new System.EventHandler(this.SaveSend_Click);
            // 
            // sendToListView
            // 
            this.sendToListView.CheckBoxes = true;
            this.sendToListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhSix});
            this.sendToListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.sendToListView.Location = new System.Drawing.Point(8, 47);
            this.sendToListView.Name = "sendToListView";
            this.sendToListView.Size = new System.Drawing.Size(461, 361);
            this.sendToListView.TabIndex = 8;
            this.sendToListView.UseCompatibleStateImageBehavior = false;
            this.sendToListView.View = System.Windows.Forms.View.Details;
            this.sendToListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.sendToListView_ItemChecked);
            // 
            // clhSix
            // 
            this.clhSix.Width = 457;
            // 
            // lblChoose
            // 
            this.lblChoose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblChoose.Location = new System.Drawing.Point(8, 10);
            this.lblChoose.Name = "lblChoose";
            this.lblChoose.Size = new System.Drawing.Size(461, 35);
            this.lblChoose.TabIndex = 3;
            this.lblChoose.Text = "click_check_mark1\r\nclick_check_mark2.\r\n";
            // 
            // removeSendToButton
            // 
            this.removeSendToButton.Location = new System.Drawing.Point(389, 414);
            this.removeSendToButton.Name = "removeSendToButton";
            this.removeSendToButton.Size = new System.Drawing.Size(82, 24);
            this.removeSendToButton.TabIndex = 0;
            this.removeSendToButton.Text = "remove";
            this.removeSendToButton.UseVisualStyleBackColor = true;
            this.removeSendToButton.Click += new System.EventHandler(this.removeSendToButton_Click3);
            // 
            // ucBottom
            // 
            this.ucBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucBottom.Location = new System.Drawing.Point(0, 542);
            this.ucBottom.Margin = new System.Windows.Forms.Padding(0);
            this.ucBottom.MaximumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.MinimumSize = new System.Drawing.Size(0, 31);
            this.ucBottom.Name = "ucBottom";
            this.ucBottom.Size = new System.Drawing.Size(496, 31);
            this.ucBottom.TabIndex = 2;
            // 
            // ucTop
            // 
            this.ucTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ucTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTop.Location = new System.Drawing.Point(0, 0);
            this.ucTop.Name = "ucTop";
            this.ucTop.Size = new System.Drawing.Size(496, 64);
            this.ucTop.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(496, 573);
            this.Controls.Add(this.ucBottom);
            this.Controls.Add(this.ucTop);
            this.Controls.Add(this.tbcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "context_menu_manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tbcMain.ResumeLayout(false);
            this.tabPageFilesFolders.ResumeLayout(false);
            this.tabPageNew.ResumeLayout(false);
            this.tabPageSendTo.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		System.Windows.Forms.TabControl tbcMain;
		System.Windows.Forms.TabPage tabPageFilesFolders;
		System.Windows.Forms.TabPage tabPageNew;
		System.Windows.Forms.TabPage tabPageSendTo;
		System.Windows.Forms.Button removeSendToButton;
		System.Windows.Forms.Button removeFilesFoldersButton;
		System.Windows.Forms.Button removeNewButton;
		System.Windows.Forms.Label lblCheck;
		System.Windows.Forms.Label lblSelect;
		System.Windows.Forms.Label lblChoose;
		System.Windows.Forms.ListView fileDescriptionListView;
		System.Windows.Forms.ColumnHeader clhFirst;
		System.Windows.Forms.ColumnHeader clhSecond;
		System.Windows.Forms.ColumnHeader clhThird;
		System.Windows.Forms.ColumnHeader clhFourth;
		System.Windows.Forms.ListView filesFoldersListView;
		System.Windows.Forms.ListView newListView;
		System.Windows.Forms.ColumnHeader clhFifth;
		System.Windows.Forms.ListView sendToListView;
		System.Windows.Forms.ColumnHeader clhSix;
		TopControl ucTop;
		BottomControl ucBottom;
		System.Windows.Forms.Button SaveFilesFolders;
		System.Windows.Forms.Button SaveNew;
        System.Windows.Forms.Button SaveSend;
	}
}

