//-
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
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Windows.Forms;
using Microsoft.Win32;

namespace FreemiumUtilities.StartupManager
{
    /// <summary>
    /// Startup Manager 1 Click-Maintenance application main form
    /// </summary>
    public partial class FrmStartupMan : Form
    {
        readonly NativeMethods native = new NativeMethods();

        /// <summary>
        /// Problems collection
        /// </summary>
        public List<ListViewItem> LstProblems = new List<ListViewItem>();

        #region Constants and Variables

        const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
        const string WowRunKey = @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Run";

        // Shortcut type (location) constants.
        const string HKCU = "Registry: Current User";
        const string WHKCU = "Registry: x86 Current User";
        const string HKLM = "Registry: All Users";
        const string WHKLM = "Registry: x86 All Users";
        const string StartupCurrentUser = "Startup Folder: Current User";
        const string StartupAllUsers = "Startup Folder: All Users";
        int i; // Counter for registry startup entries.

        bool Is64Bit; //true if system is x64

        enum ListCol
        {
            ItemName = 0,
            FileName = 1,
            Type = 2,
            Status = 3,
            Command = 4,
            Path = 5
        }

        #endregion

        /// <summary>
        /// constructor for frmStartupMan
        /// </summary>
        public FrmStartupMan()
        {
            InitializeComponent();
        }

        #region Listview Methods

        /// <summary>
        /// Fills the ListView with an actual startup items
        /// </summary>
        public void FillListview()
        {
            i = 0;
            listviewStartup.Items.Clear();
            LstProblems.Clear();

            // Get all startup programs in HHEY_CURRENT_USER
            DisplayRegistryStartupEntries(HKCU);

            // Get all startup programs in HHEY_CURRENT_USER\Wow6432Node
            if (Is64Bit)
            {
                DisplayRegistryStartupEntries(WHKCU);
            }

            // Get all startup programs in HKEY_LOCAL_MACHINE
            DisplayRegistryStartupEntries(HKLM);

            // Get all startup programs in HKEY_LOCAL_MACHINE\Wow6432Node
            if (Is64Bit)
            {
                DisplayRegistryStartupEntries(WHKLM);
            }

            // Get all startup shortcuts and programs in the Current User's Startup Folder.
            DisplayStartupShortcuts(StartupCurrentUser);

            // Get all startup shortcuts and programs in the All User's Startup Folder.
            DisplayStartupShortcuts(StartupAllUsers);
        }

        #endregion

        /// <summary>
        /// Removes the <paramref name="app"/> from the startup items
        /// </summary>
        /// <param name="app">App to remove</param>
        /// <returns></returns>
        public List<ListViewItem> RemoveAppsFromReg(string app)
        {
            foreach (ListViewItem item in listviewStartup.Items)
            {
                //Un-comment this to test "details" button
                if (app == item.SubItems[1].Text)
                {
                    try
                    {
                        LstProblems.Add(item);
                    }
                    catch
                    {
                    }
                }
            }

            return LstProblems;
        }

        /// <summary>
        /// Deletes the <paramref name="app"/> from the startup list
        /// </summary>
        /// <param name="app"></param>
        public void DeleteItem(string app)
        {
            int index = 0;
            foreach (ListViewItem item in listviewStartup.Items)
            {
                //Un-comment this to test "details" button
                if (app == item.SubItems[1].Text)
                {
                    DeleteItem(index);
                    return;
                }
                index++;
            }
        }

        #region Display Registry Startup Entries Method

        void DisplayRegistryStartupEntries(string hive)
        {
            RegistryKey rk = null;
            try
            {
                switch (hive)
                {
                    case HKCU:
                        rk = Registry.CurrentUser.OpenSubKey(RunKey);
                        break;
                    case HKLM:
                        rk = Registry.LocalMachine.OpenSubKey(RunKey);
                        break;
                    case WHKCU:
                        rk = Registry.CurrentUser.OpenSubKey(WowRunKey);
                        break;
                    case WHKLM:
                        rk = Registry.LocalMachine.OpenSubKey(WowRunKey);
                        break;
                }

                // Get all of the entries.
                if (rk != null)
                {
                    string filePath;
                    bool disabled;
                    string command;
                    ListViewItem lvi;
                    foreach (string value in rk.GetValueNames())
                    {
                        disabled = false;
                        command = rk.GetValue(value).ToString();
                        if (command.Length < 1)
                            continue;

                        // Check if command is disabled (begins with a ":")
                        if (command.StartsWith(":"))
                        {                        
                            disabled = true;
                            // Remove colon so that path command works and save file path.
                            filePath = Helper.ReturnFilePath(command.Remove(0, 1));
                        }
                        else
                        {
                            filePath = Helper.ReturnFilePath(command);
                        }

                        lvi = new ListViewItem(value, i);
                        lvi.SubItems.Add(Path.GetFileName(filePath));
                        lvi.SubItems.Add(hive);
                        lvi.SubItems.Add(disabled ? "Disabled" : "Enabled");
                        lvi.SubItems.Add(command);
                        lvi.SubItems.Add(filePath);
                        listviewStartup.Items.Add(lvi);
                        i++;
                    }
                    rk.Close();
                }
            }
            catch
            {
            }
            finally
            {
                if (rk != null)
                {
                    rk.Close();
                }
            }
        }

        #endregion

        #region Display Startup Shortcuts Method

        void DisplayStartupShortcuts(string type)
        {
            string folder;
            bool disabled;
            string command;
            string filePath;
            ListViewItem lvi;
            string extension;

            if (type == StartupCurrentUser)
            {
                // Current users startup folder.
                folder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            }
            else
            {
                // All users startup folder.
                folder = Environment.ExpandEnvironmentVariables("%AllUsersProfile%")
                         + @"\Start Menu\Programs\Startup";
            }

            try
            {
                foreach (string shortcut in Directory.GetFiles(folder, "*.*"))
                {
                    extension = Path.GetExtension(shortcut);

                    // Only process shortcuts, executibles, or disabled shortcuts.
                    if (extension == ".lnk" || extension == ".exe" || extension == ".disabled")
                    {
                        disabled = Path.GetExtension(shortcut) == ".disabled";
                        command = shortcut;
                        filePath = Helper.ReturnFilePath(command);

                        lvi = new ListViewItem(Path.GetFileNameWithoutExtension(shortcut), i);
                        lvi.SubItems.Add(Path.GetFileName(filePath));
                        lvi.SubItems.Add(type == StartupCurrentUser ? StartupCurrentUser : StartupAllUsers);
                        lvi.SubItems.Add(disabled ? "Disabled" : "Enabled");
                        lvi.SubItems.Add(command);
                        lvi.SubItems.Add(filePath);
                        listviewStartup.Items.Add(lvi);
                        i++;
                    }
                }
            }
            catch (Exception)
            {
                //Directory may not exist.
            }
        }

        #endregion

        #region Delete Item Method

        /// <summary>
        /// Deletes item from the start up list
        /// </summary>
        /// <param name="index"></param>
        void DeleteItem(int index)
        {
            try
            {
                // Get type (location) of startup item.
                bool success = false;
                string regDir = listviewStartup.Items[index].SubItems[(int)ListCol.Type].Text;
                if (regDir == "HKCU" || regDir == "WHKCU" || regDir == "HKLM" || regDir == "WHKLM")
                {
                    if (Helper.DeleteItemFromReg(regDir, listviewStartup.Items[index].Text))
                        success = true;
                }
                else if (regDir == "StartupCurrentUser" || regDir == "StartupAllUsers")
                {
                    if (Helper.DeleteItemForUser(listviewStartup.Items[index].SubItems[(int)ListCol.Path].Text))
                        success = true;
                }

                if (success)
                {
                    // Remove item from listview.
                    listviewStartup.Items[index].Remove();

                    // Clear the details labels.
                    labelArguments.Text = string.Empty;
                    labelCommand.Text = string.Empty;
                    labelCompany.Text = string.Empty;
                    labelDescription.Text = string.Empty;
                    labelFileVersion.Text = string.Empty;
                    labelProductName.Text = string.Empty;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Unable to delete this item." + "\r\n" +
                                "The system returned the following information:" + "\r\n" +
                                ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException exc)
            {
                MessageBox.Show("Unable to delete this item." + "\r\n" +
                                "The system returned the following information:" + "\r\n" +
                                exc.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SecurityException excep)
            {
                MessageBox.Show("Unable to delete this item." + "\r\n" +
                                "The system returned the following information:" + "\r\n" +
                                excep.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Win32Exception except)
            {
                MessageBox.Show("Unable to delete this item." + "\r\n" +
                                "The system returned the following information:" + "\r\n" +
                                except.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

    }
}