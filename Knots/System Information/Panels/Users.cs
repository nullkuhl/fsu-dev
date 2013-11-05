//
// Copyright © 2006 Herbert N Swearengen III (hswear3@swbell.net)
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//   - Redistributions of source code must retain the above copyright notice, 
//     this list of conditions and the following disclaimer.
//
//   - Redistributions in binary form must reproduce the above copyright notice, 
//     this list of conditions and the following disclaimer in the documentation 
//     and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
// IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
// OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.
//

using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Management;
using System.Resources;
using System.Security;
using System.Windows.Forms;

namespace SystemInformation
{
    /// <summary>
    /// Users panel of the System Information utility
    /// </summary>
    public partial class Users : SystemInformation.TaskPanelBase
    {
        static Users panelInstance;
        static InformationClass info = new InformationClass();

        static bool UserIsAdministrator;
        static string OSMachineName;
        static Collection<string> UserAccounts;
        static Collection<string> UserFullName;
        static Collection<string> UserPrivilege;
        static Collection<int> UserFlags;
        static string UserRegisteredOrganization;
        static string UserRegisteredName;

        /// <summary>
        /// Create a global instance of this panel
        /// </summary>>
        public static Users CreateInstance()
        {
            if (panelInstance == null)
            {
                panelInstance = new Users();
                UserIsAdministrator = info.UserIsAdministrator;
                OSMachineName = info.OSMachineName;
                UserAccounts = info.UserAccounts;
                UserFullName = info.UserFullName;
                UserPrivilege = info.UserPrivilege;
                UserFlags = info.UserFlags;
                UserRegisteredOrganization = info.UserRegisteredOrganization;
                UserRegisteredName = info.UserRegisteredName;
            }
            return panelInstance;
        }

        // Set to true after load. Prevents false TextBox changed results.
        bool loaded;

        // Bit masks for UserFlags.
        const int accountDisabled = 0x2;
        const int accountLockedOut = 0x10;
        const int passwordNotRequired = 0x20;
        const int passwordCanNotChange = 0x40;
        const int passwordDoesNotExpire = 0x10000;

        #region " Users Events "

        void Users_Load(object sender, System.EventArgs e)
        {
            ResourceManager rm = new ResourceManager("SystemInformation.Resources", System.Reflection.Assembly.GetExecutingAssembly());

            this.labelTitle.Text = rm.GetString("node_useraccounts");
            this.labelRegistration.Text = rm.GetString("user_registration");
            this.UserAccount.Text = rm.GetString("node_useraccounts");
            this.FullName.Text = rm.GetString("user_fullname");
            this.AccountType.Text = rm.GetString("user_account_type");
            this.Notes.Text = rm.GetString("user_notes");
            this.labelUserList.Text = rm.GetString("user_list_master");
            this.btnSaveRegisteredOrganization.Text = rm.GetString("user_save");
            this.btnSaveRegisteredUser.Text = rm.GetString("user_save");
            this.labelRegisteredOrganization.Text = rm.GetString("user_reg_org") + ":";
            this.labelRegisteredUser.Text = rm.GetString("user_reg_user") + ":";

            try
            {
                Application.DoEvents();

                Collection<string> users = new Collection<string>();
                Collection<string> fullNames = new Collection<string>();
                Collection<string> privileges = new Collection<string>();
                Collection<int> flags = new Collection<int>();

                // Lock TextBoxes and hide save buttons if user is not an administrator.
                // But display a normal background.
                if (!UserIsAdministrator)
                {
                    btnSaveRegisteredOrganization.Visible = false;
                    btnSaveRegisteredUser.Visible = false;
                    textboxRegisteredOrganization.ReadOnly = true;
                    textboxRegisteredOrganization.BackColor = Color.FromKnownColor(KnownColor.Window);
                    textboxRegisteredUser.ReadOnly = true;
                    textboxRegisteredUser.BackColor = Color.FromKnownColor(KnownColor.Window);
                }

                // Add computer name to label.
                labelUserList.Text = rm.GetString("user_list") + ": " + OSMachineName;

                // Clear ListView.
                listviewUsers.Items.Clear();

                // Get Information.
                users = UserAccounts;
                fullNames = UserFullName;
                privileges = UserPrivilege;
                flags = UserFlags;

                // Populate Listview.
                for (int i = 0; i < users.Count; i++)
                {
                    // Show disabled users as greyed.
                    if (Convert.ToBoolean(flags[i] & accountDisabled))
                    {
                        listviewUsers.Items.Add(users[i], 1);
                    }
                    else
                    {
                        listviewUsers.Items.Add(users[i], 0);
                    }

                    listviewUsers.Items[i].SubItems.Add(fullNames[i]);
                    listviewUsers.Items[i].SubItems.Add(privileges[i]);

                    ManagementObject userObject = NativeMethods.GetUserObject(users[i]);

                    // Show miscellaneous flags.
                    if (Convert.ToBoolean(userObject["Disabled"]))
                    {
                        listviewUsers.Items[i].SubItems.Add(rm.GetString("user_acc_disabled"));
                    }
                    else if (Convert.ToBoolean(userObject["Lockout"]))
                    {
                        listviewUsers.Items[i].SubItems.Add(rm.GetString("user_acc_locked"));
                    }
                    else if (!Convert.ToBoolean(userObject["PasswordRequired"]))
                    {
                        listviewUsers.Items[i].SubItems.Add(rm.GetString("user_pwd_not_required"));
                    }
                    else if (!Convert.ToBoolean(userObject["PasswordChangeable"]))
                    {
                        listviewUsers.Items[i].SubItems.Add(rm.GetString("user_pwd_not_changeable"));
                    }
                    else if (!Convert.ToBoolean(userObject["PasswordExpires"]))
                    {
                        listviewUsers.Items[i].SubItems.Add(rm.GetString("user_pwd_doesnt_expire"));
                    }

                    // Fill in registration information.
                    textboxRegisteredOrganization.Text = UserRegisteredOrganization;
                    textboxRegisteredUser.Text = UserRegisteredName;

                    // Flag that panel has loaded; enables TextChanged events.
                    loaded = true;
                }

            }
            catch { }

        }

        #endregion

        #region " Button Events "

        void btnSaveRegisteredUser_Click(object sender, EventArgs e)
        {
            try
            {
                info.UserRegisteredName = textboxRegisteredUser.Text;
                btnSaveRegisteredUser.Enabled = false;
            }
            catch (SecurityException ex)
            {
                MessageBox.Show(String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", rm.GetString("user_unable_save_info"),
                    Environment.NewLine, rm.GetString("user_action_admin"), Environment.NewLine, rm.GetString("error_return"),
                    Environment.NewLine, rm.GetString("error_description"), ex.Message),
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void btnRegisteredOrganization_Click(object sender, EventArgs e)
        {
            try
            {
                info.UserRegisteredOrganization = textboxRegisteredOrganization.Text;
                btnSaveRegisteredOrganization.Enabled = false;
            }
            catch (SecurityException ex)
            {
                MessageBox.Show(String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", rm.GetString("user_unable_save_info"),
                    Environment.NewLine, rm.GetString("user_action_admin"), Environment.NewLine, rm.GetString("error_return"),
                    Environment.NewLine, rm.GetString("error_description"), ex.Message),
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region " TextBox Events "

        void textboxRegisteredUser_TextChanged(object sender, EventArgs e)
        {
            if (loaded && info.UserIsAdministrator)
            {
                btnSaveRegisteredUser.Enabled = true;
            }
        }

        void textboxRegisteredOrganization_TextChanged(object sender, EventArgs e)
        {
            if (loaded && info.UserIsAdministrator)
            {
                btnSaveRegisteredOrganization.Enabled = true;
            }
        }

        #endregion

    }

}
