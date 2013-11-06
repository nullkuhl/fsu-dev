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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using Microsoft.Win32;

namespace StartupManager
{
    /// <summary>
    /// Main form of the Startup Manager knot
    /// </summary>
    public partial class FormMain : Form
    {
        //Import DLL for All User StartUp

        int count = 0;
        string strRegPath = "_Disabled";
        const int CSIDL_COMMON_STARTMENU = 0x16; // \Windows\Start Menu\Programs

        readonly ResourceManager resourceManager =
            new ResourceManager("StartupManager.Properties.Resources", typeof(FormMain).Assembly);

        readonly NativeMethods native = new NativeMethods();
        RegistryKey HLM = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Shared Tools\\MSConfig\\startupreg", true);


        #region Constants and Variables

        const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
        const string WowRunKey = @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Run";
        readonly string allUsersStartup = String.Empty;
        readonly string currentUserStartup = Environment.GetFolderPath(Environment.SpecialFolder.Programs) + @"\Startup\";
        bool admin; // True if user has administrative privileges.
        bool _Ascending; // Used to toggle column sort.
        int currentIndex; // Index of entry that is currently selected.
        bool is64Bit; // True if 64-Bit OS.
        int i; // Counter for registry startup entries.

        // Listview column constants.
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

        #region MainForm Events

        /// <summary>
        /// initialize MainForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_Load(Object sender, EventArgs e)
        {
            try
            {
                CultureInfo culture = new CultureInfo(CfgFile.Get("Lang"));
                SetCulture(culture);
                // Get user privilege level.
                WindowsPrincipal wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                admin = wp.IsInRole(WindowsBuiltInRole.Administrator);

                if (!admin)
                {
                    // Display message if user is not an administrator.
                    DialogResult result = MessageBox.Show(string.Format("{0}{1}{2}{3}{4}",
                        rm.GetString("run_as_admin"), Environment.NewLine,
                        rm.GetString("if_you_continue"), Environment.NewLine,
                        rm.GetString("do_you_wish")), rm.GetString("startup_manager"),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    // Exit if user does not want to continue as a non-admin.
                    if (result == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
                if (HLM == null)
                    HLM = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Shared Tools\\MSConfig\\startupreg");
                // First try to determine if the 32-bit program files environment variable exists.
                if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("ProgramFiles(x86)")))
                {
                    is64Bit = true;
                }

                // Display startup items.
                FillListview();
            }
            catch (Exception ex)
            {
                if (ex is Win32Exception)
                {
                    MessageBox.Show(string.Format("{0} {1}{2}{3} {4}", rm.GetString("error_occurred"), rm.GetString("startup_manager"),
                                                                       Environment.NewLine, rm.GetString("description"), ex.Message),
                                    rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex is SecurityException)
                {
                    MessageBox.Show(string.Format("{0} {1}{2}{3} {4}", rm.GetString("error_occurred"), rm.GetString("startup_manager"),
                                                       Environment.NewLine, rm.GetString("description"), ex.Message),
                                    rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\FreeGamingBooster.exe"))
            {
                this.Icon = Properties.Resources.GBicon;
            }
            else if (!File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\FreemiumUtilities.exe"))
            {
                this.Icon = Properties.Resources.PCCleanerIcon;
            }
            else
            {
                this.Icon = Properties.Resources.FSUIcon;
            }
        }

        /// <summary>
        /// resize MainForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_Resize(object sender, EventArgs e)
        {
            // Resize listview columns based upon new form width.
            listviewStartup.Columns[0].Width = (int)Math.Round(listviewStartup.Width * .347, 0);
            listviewStartup.Columns[1].Width = (int)Math.Round(listviewStartup.Width * .296, 0);
            listviewStartup.Columns[2].Width = (int)Math.Round(listviewStartup.Width * .238, 0);
            listviewStartup.Columns[3].Width = (int)Math.Round(listviewStartup.Width * .090, 0);
        }

        #endregion

        #region ListView Events

        /// <summary>
        /// Handles listViewStartup ItemSelectionChanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void listviewStartup_ItemSelectionChanged(object sender,
                                                  ListViewItemSelectionChangedEventArgs e)
        {
            //Show Details Section
            firstCover.Visible = false;

            string command = String.Empty;
            string filePath = String.Empty;

            try
            {
                if (e.IsSelected)
                {
                    // Make the selected index available.
                    currentIndex = e.ItemIndex;

                    // Get command and file path stored in listview and make available panel wide.
                    command = listviewStartup.Items[e.ItemIndex].SubItems[(int)ListCol.Command].Text;

                    // Trim command if it is too long
                    if (command.Length > 128)
                    {
                        string[] splittedCommand = command.Split('\\');
                        if (splittedCommand.Length > 3)
                        {
                            command = String.Format(@"{0}\{1}\...\{2}\{3}", splittedCommand[0], splittedCommand[1],
                                                    splittedCommand[splittedCommand.Length - 2], splittedCommand[splittedCommand.Length - 1]);
                        }
                    }

                    filePath = listviewStartup.Items[e.ItemIndex].SubItems[(int)ListCol.Path].Text;

                    // Make sure the filePath exists.
                    if (File.Exists(filePath))
                    {
                        // Determine if file has hidden or system attribute.
                        if (((File.GetAttributes(filePath) & FileAttributes.Hidden) == FileAttributes.Hidden) ||
                            ((File.GetAttributes(filePath) & FileAttributes.System) == FileAttributes.System))
                        {
                            // Change to normal.
                            File.SetAttributes(filePath, FileAttributes.Normal);
                        }

                        // Display the file information.
                        if (filePath.Contains("cmd.exe"))
                        {
                            // Since this is a command window, we will not be able to resolve any properties.
                            labelCompany.Text = string.Empty;
                            labelProductName.Text = string.Empty;
                            labelDescription.Text = string.Empty;
                            labelFileVersion.Text = string.Empty;
                            labelCommand.Text = command;

                            // Only display arguments for shortcuts.
                            labelArguments.Visible = false;
                            labelArguments.Text = string.Empty;
                        }
                        else if (Path.GetExtension(filePath) == ".lnk")
                        {
                            // Resolve the shortcut.
                            ShortcutClass sc = new ShortcutClass(filePath);

                            // Get the file version information.
                            FileVersionInfo selectedFileVersionInfo = FileVersionInfo.GetVersionInfo(sc.Path);

                            // Display the resolved shortcut properties.
                            labelCompany.Text = selectedFileVersionInfo.CompanyName;
                            labelProductName.Text = selectedFileVersionInfo.ProductName;
                            labelDescription.Text = selectedFileVersionInfo.FileDescription;
                            labelFileVersion.Text = selectedFileVersionInfo.FileVersion;
                            labelCommand.Text = command;

                            // Display arguments for shortcuts, but only if present.
                            if (string.IsNullOrEmpty(sc.Arguments))
                            {
                                labelArguments.Visible = false;
                            }
                            else
                            {
                                labelArguments.Visible = true;
                                labelArguments.Text = sc.Arguments;
                            }

                            //Takal

                            // Attempt to get application image (icon).
                            if (sc.Icon != null)
                            {
                                // First try getting icon from shortcut.
                                pictureBoxPanel.Image = GetBitmap(sc.Icon);
                            }
                            else if (native.GetIcon(sc.Path) != null)
                            {
                                // Then try getting icon from the resolved path.
                                pictureBoxPanel.Image = GetBitmap(native.GetIcon(sc.Path));
                            }

                            // Dispose of the class instance.
                            sc.Dispose();
                        }
                        else
                        {
                            // Get the file version information.
                            FileVersionInfo selectedFileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);

                            // Display the file properties.
                            labelCompany.Text = selectedFileVersionInfo.CompanyName;
                            labelProductName.Text = selectedFileVersionInfo.ProductName;
                            labelDescription.Text = selectedFileVersionInfo.FileDescription;
                            labelFileVersion.Text = selectedFileVersionInfo.FileVersion;
                            labelCommand.Text = command;

                            //Takal
                            if (native.GetIcon(filePath) != null)
                            {
                                pictureBoxPanel.Image = GetBitmap(native.GetIcon(filePath));
                            }
                            else
                            {
                                pictureBoxPanel.Image = null;
                            }
                            // Only display arguments for shortcuts.
                            labelArguments.Visible = false;
                            labelArguments.Text = string.Empty;
                        }


                        // Set state of context menu and tool strip buttons depending upon location and user rights.
                        if ((listviewStartup.Items[e.ItemIndex].SubItems[(int)ListCol.Type].Text.Contains("Current User"))
                            || (listviewStartup.Items[e.ItemIndex].SubItems[(int)ListCol.Type].Text.Contains("Aktueller Benutzer")))
                        {
                            SetItems(e.ItemIndex, "Current User");
                        }
                        else if ((listviewStartup.Items[e.ItemIndex].SubItems[(int)ListCol.Type].Text.Contains("All Users"))
                                 || (listviewStartup.Items[e.ItemIndex].SubItems[(int)ListCol.Type].Text.Contains("Alle Benutzer")))
                        {
                            // Do not allow non admins to do anything in All Users.
                            if (admin == false)
                            {
                                toolStripMenuItemEnable.Enabled = false;
                                toolStripButtonEnable.Enabled = false;
                                toolStripMenuItemDisable.Enabled = false;
                                toolStripButtonDisable.Enabled = false;
                                toolStripMenuItemDelete.Enabled = false;
                                toolStripButtonDelete.Enabled = false;
                                toolStripMenuItemExecute.Enabled = false;
                                toolStripButtonExecute.Enabled = false;
                                toolStripMenuItemOpen.Enabled = false;
                                toolStripButtonOpen.Enabled = false;
                                toolStripMenuItemMoveToAllUsers.Enabled = false;
                                toolStripButtonMoveToAllUsers.Enabled = false;
                                toolStripMenuItemMoveToCurrentUser.Enabled = false;
                                toolStripButtonMoveToCurrentUser.Enabled = false;
                            }
                            else
                            {
                                SetItems(e.ItemIndex, "All Users");
                            }
                        }
                    }
                    else
                    {
                        // Disable all context menu items and buttons except delete.
                        toolStripMenuItemEnable.Enabled = false;
                        toolStripButtonEnable.Enabled = false;
                        toolStripMenuItemDisable.Enabled = false;
                        toolStripButtonDisable.Enabled = false;
                        toolStripMenuItemDelete.Enabled = true;
                        toolStripButtonDelete.Enabled = true;
                        toolStripMenuItemExecute.Enabled = false;
                        toolStripButtonExecute.Enabled = false;
                        toolStripMenuItemOpen.Enabled = false;
                        toolStripButtonOpen.Enabled = false;
                        toolStripMenuItemMoveToAllUsers.Enabled = false;
                        toolStripButtonMoveToAllUsers.Enabled = false;
                        toolStripMenuItemMoveToCurrentUser.Enabled = false;
                        toolStripButtonMoveToCurrentUser.Enabled = false;

                        // Clear all information, since the file is missing or invalid.
                        labelCompany.Text = String.Empty;
                        labelProductName.Text = String.Empty;
                        labelDescription.Text = String.Empty;
                        labelFileVersion.Text = String.Empty;
                        labelCommand.Text = String.Empty;

                        // Only display arguments for shortcuts.
                        labelArguments.Visible = false;
                        labelArguments.Text = String.Empty;

                        // Display a message to the user indicating that the file does not exist.
                        MessageBox.Show(string.Format("{0}{1}{2}", rm.GetString("file_invalid"), Environment.NewLine, rm.GetString("commands_disabled")),
                            rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SecurityException ex)
            {
                MessageBox.Show(string.Format("{0}{1}{2}{3}{4} {5}", rm.GetString("no_permission"), Environment.NewLine,
                                               rm.GetString("system_returned"), Environment.NewLine, rm.GetString("description"), ex.Message),
                                rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FileNotFoundException)
            {

                MessageBox.Show(string.Format("{0}{1}{2} {3}{4}{5}: {6}", rm.GetString("file_not_found"), Environment.NewLine,
                                              rm.GetString("file_path"), filePath, Environment.NewLine, rm.GetString("command"), command),
                                rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (IOException exc)
            {
                MessageBox.Show(exc.Message, rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException excp)
            {
                MessageBox.Show(string.Format("{0}{1}{2} {3}{4}{5}: {6}", excp.Message, Environment.NewLine,
                                              rm.GetString("file_path"), filePath, Environment.NewLine, rm.GetString("command"), command),
                                rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Set the ListViewItemSorter property to a new ListViewItemComparer 
        /// object. Setting this property immediately sorts the 
        /// ListView using the ListViewItemComparer object.
        /// </summary>
        void listviewStartup_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Toggle sort order.
            if (_Ascending == false)
            {
                _Ascending = true;
            }
            else
            {
                _Ascending = false;
            }

            // Perform sort of items in specified column.
            listviewStartup.ListViewItemSorter = new ListViewItemComparer(e.Column, _Ascending);
        }

        #endregion

        #region Context Tool Strip Menu Events

        void toolStripMenuItemDisable_Click(object sender, EventArgs e)
        {
            DisableItem(currentIndex);
        }

        void toolStripMenuItemEnable_Click(object sender, EventArgs e)
        {
            EnableItem(currentIndex);
        }

        void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            // Display a warning message before deleting an item.
            DialogResult result = MessageBox.Show(string.Format("{0}{1}{2}{3}", rm.GetString("warning"), Environment.NewLine, Environment.NewLine,
                                                                rm.GetString("delete_selected_item")),
                                                  rm.GetString("startup_manager"), MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                  MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                return;
            }
            DeleteItem(currentIndex);
        }

        void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            OpenFolder(currentIndex);
        }

        void toolStripMenuItemExecute_Click(object sender, EventArgs e)
        {
            ExecuteCommand(currentIndex);
        }

        void toolStripMenuItemMoveToCurrentUser_Click(object sender, EventArgs e)
        {
            MoveToCurrentUser(currentIndex);
        }

        void toolStripMenuItemMoveToAllUsers_Click(object sender, EventArgs e)
        {
            MoveToAllUsers(currentIndex);
        }

        #endregion

        #region Tool Strip Button Events

        void toolStripButtonDisable_Click(object sender, EventArgs e)
        {
            DisableItem(currentIndex);
        }

        void toolStripButtonEnable_Click(object sender, EventArgs e)
        {
            EnableItem(currentIndex);
        }

        void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            // Display a warning message before deleting an item.
            DialogResult result = MessageBox.Show(string.Format("{0}{1}{2}{3}", rm.GetString("warning"), Environment.NewLine, Environment.NewLine,
                                                                 rm.GetString("delete_selected_item")),
                                                  rm.GetString("startup_manager"), MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                  MessageBoxDefaultButton.Button2);

            if (result == DialogResult.No)
            {
                return;
            }
            DeleteItem(currentIndex);
        }

        void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFolder(currentIndex);
        }

        void toolStripButtonExecute_Click(object sender, EventArgs e)
        {
            ExecuteCommand(currentIndex);
        }

        void toolStripButtonMoveToCurrentUser_Click(object sender, EventArgs e)
        {
            MoveToCurrentUser(currentIndex);
        }

        void toolStripButtonMoveToAllUsers_Click(object sender, EventArgs e)
        {
            if (!IsUserAdministrator())
            {
                MessageBox.Show(rm.GetString("AdminRightsNeeded"), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MoveToAllUsers(currentIndex);
            }
        }

        /// <summary>
        /// Checks if user is admin
        /// </summary>
        /// <returns></returns>
        public bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                isAdmin = false;
            }
            catch (Exception)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            FillListview();
        }

        #endregion

        #region Listview Methods

        /// <summary>
        /// Fills list view
        /// </summary>
        void FillListview()
        {
            try
            {
                // Clear image list and listview.
                listviewStartup.Items.Clear();
                imageListStartupManager.Images.Clear();
                i = 0;

                // Get all startup programs in HHEY_CURRENT_USER
                DisplayRegistryStartupEntries(rm.GetString("HKCU"));

                // Get all startup programs in HHEY_CURRENT_USER\Wow6432Node
                if (is64Bit)
                {
                    DisplayRegistryStartupEntries(rm.GetString("WHKCU"));
                }

                // Get all startup programs in HKEY_LOCAL_MACHINE
                DisplayRegistryStartupEntries(rm.GetString("HKLM"));

                // Get all startup programs in HKEY_LOCAL_MACHINE\Wow6432Node
                if (is64Bit)
                {
                    DisplayRegistryStartupEntries(rm.GetString("WHKLM"));
                }

                // Get all startup shortcuts and programs in the Current User's Startup Folder.
                DisplayStartupShortcuts(rm.GetString("StartupCurrentUser"));

                // Get all startup shortcuts and programs in the All User's Startup Folder.
                DisplayStartupShortcuts(rm.GetString("StartupAllUsers"));

                //Get all startup disabled shortcuts
                FillDisabledItems();
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        void FillDisabledItems()
        {
            count = 0;
            string command = string.Empty;
            string filePath;
            try
            {
                foreach (string key in HLM.GetSubKeyNames())
                {
                    try
                    {
                        if (count < HLM.GetSubKeyNames().Length)
                        {
                            count++;
                            RegistryKey entry = HLM.OpenSubKey(key);
                            if (entry != null)
                            {
                                if (entry.ValueCount > 0)
                                {
                                    if (entry.GetValue("command") != null)
                                    {
                                        command = entry.GetValue("command").ToString();
                                    }

                                    if (command.StartsWith("\""))
                                    {

                                    }
                                    if (command.StartsWith(":"))
                                    {
                                        // Remove colon so that path command works and save file path.
                                        filePath = ReturnFilePath(command.Remove(0, 1));
                                    }

                                    else
                                    {
                                        // Save file path.
                                        filePath = ReturnFilePath(command);
                                    }

                                    // Attempt to get application image (icon).
                                    if (native.GetIcon(filePath) != null)
                                    {
                                        // Add the icon to the image list so that the listview can access it.
                                        imageListStartupManager.Images.Add(native.GetIcon(filePath));
                                    }
                                    else
                                    {
                                        // If there is no icon, just add a blank image from resources to keep the indexes proper.
                                        imageListStartupManager.Images.Add((Image)resourceManager.GetObject("Blank"));
                                    }

                                    string value = entry.GetValue("item").ToString();
                                    if (File.Exists(filePath))
                                    {
                                        FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(filePath);

                                        if (fileInfo != null)
                                        {
                                            if (fileInfo.FileDescription != null && fileInfo.FileDescription.Length != 0)
                                            {
                                                listviewStartup.Items.Add(fileInfo.FileDescription, i);
                                                listviewStartup.Items[listviewStartup.Items.Count - 1].Tag = value;
                                            }
                                            else
                                            {
                                                listviewStartup.Items.Add(value, i);
                                                listviewStartup.Items[listviewStartup.Items.Count - 1].Tag = value;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        listviewStartup.Items.Add(value, i);
                                        listviewStartup.Items[listviewStartup.Items.Count - 1].Tag = value;
                                    }

                                    // Add file name (without path) to listview.
                                    string p = Path.GetFileName(filePath);
                                    if (p != null || p != string.Empty)
                                    {
                                        listviewStartup.Items[i].SubItems.Add(p);
                                        string hkey = rm.GetString("HKCU");
                                        if (entry.GetValue("hkey") != null)
                                            hkey = rm.GetString(entry.GetValue("hkey").ToString());
                                        listviewStartup.Items[i].SubItems.Add(hkey);

                                        // Add status information.
                                        listviewStartup.Items[i].SubItems.Add(true ? rm.GetString("disabled") : rm.GetString("enabled"));

                                        // Add command.
                                        listviewStartup.Items[i].SubItems.Add(command);

                                        // Add file path.
                                        listviewStartup.Items[i].SubItems.Add(filePath);

                                        i++;
                                    }
                                    else
                                    {
                                        string[] temp = filePath.Split('\\');
                                        listviewStartup.Items[i].SubItems.Add(temp[temp.Length - 1]);

                                        listviewStartup.Items[i].SubItems.Add(rm.GetString("HKCU"));


                                        // Add status information.
                                        listviewStartup.Items[i].SubItems.Add(true ? rm.GetString("disabled") : rm.GetString("enabled"));

                                        // Add command.
                                        listviewStartup.Items[i].SubItems.Add(command);

                                        // Add file path.
                                        listviewStartup.Items[i].SubItems.Add(filePath);

                                        i++;

                                    }
                                }
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        // throw exc;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        void SetItems(int index, string context)
        {
            // Set state of "Enable" and "Disable" menus according to enabled state of selected item.
            if (listviewStartup.Items[index].SubItems[(int)ListCol.Status].Text == rm.GetString("enabled"))
            {
                toolStripMenuItemEnable.Enabled = false;
                toolStripButtonEnable.Enabled = false;
                toolStripMenuItemDisable.Enabled = true;
                toolStripButtonDisable.Enabled = true;
                toolStripMenuItemDelete.Enabled = true; // Only allow enabled items to be deleted.
                toolStripButtonDelete.Enabled = true;
                toolStripMenuItemExecute.Enabled = true; // Only allow enabled items to be executed.
                toolStripButtonExecute.Enabled = true;
                toolStripMenuItemOpen.Enabled = true; // Only allow enabled items to have folder opened.
                toolStripButtonOpen.Enabled = true;

                // Disable items based on context.
                if (context == "Current User")
                {
                    toolStripMenuItemMoveToAllUsers.Enabled = true;
                    toolStripButtonMoveToAllUsers.Enabled = true;
                    toolStripMenuItemMoveToCurrentUser.Enabled = false; // Item is already in Current User.
                    toolStripButtonMoveToCurrentUser.Enabled = false;
                }
                else if (context == "Registry: x86 Current User")
                {
                    toolStripMenuItemMoveToAllUsers.Enabled = true;
                    toolStripButtonMoveToAllUsers.Enabled = true;
                    toolStripMenuItemMoveToCurrentUser.Enabled = false; // Item is already in Current User.
                    toolStripButtonMoveToCurrentUser.Enabled = false;
                }
                else if (context == "All Users")
                {
                    toolStripMenuItemMoveToAllUsers.Enabled = false; // Itme is already in All Users.
                    toolStripButtonMoveToAllUsers.Enabled = false;
                    toolStripMenuItemMoveToCurrentUser.Enabled = true;
                    toolStripButtonMoveToCurrentUser.Enabled = true;
                }
                else if (context == "Registry: x86 All Users")
                {
                    toolStripMenuItemMoveToAllUsers.Enabled = false; // Itme is already in All Users.
                    toolStripButtonMoveToAllUsers.Enabled = false;
                    toolStripMenuItemMoveToCurrentUser.Enabled = true;
                    toolStripButtonMoveToCurrentUser.Enabled = true;
                }
            }
            else
            {
                toolStripMenuItemEnable.Enabled = true;
                toolStripButtonEnable.Enabled = true;
                toolStripMenuItemDisable.Enabled = false;
                toolStripButtonDisable.Enabled = false;
                toolStripMenuItemDelete.Enabled = false; // Do not allow disabled items to be deleted.
                toolStripButtonDelete.Enabled = false;
                toolStripMenuItemExecute.Enabled = false; // Do not allow disabled items to be executed.
                toolStripButtonExecute.Enabled = false;
                toolStripMenuItemOpen.Enabled = false; // Do not allow disabled items to have folder opened.
                toolStripButtonOpen.Enabled = false;
                toolStripMenuItemMoveToAllUsers.Enabled = false; // Do not allow disabled item to be moved.
                toolStripButtonMoveToAllUsers.Enabled = false;
                toolStripMenuItemMoveToCurrentUser.Enabled = false; // Item is already in Current User.
                toolStripButtonMoveToCurrentUser.Enabled = false;
            }
        }

        #endregion

        #region Display Registry Startup Entries Method

        /// <summary>
        /// Displays registry startup entries
        /// </summary>
        /// <param name="hive"></param>
        void DisplayRegistryStartupEntries(string hive)
        {
            RegistryKey rk = null;
            RegistryKey rkDisabled = null;
            try
            {
                if (hive == rm.GetString("HKCU"))
                {
                    // Get all startup programs in HKEY_CURRENT_USER.
                    rk = Registry.CurrentUser.OpenSubKey(RunKey);
                    rkDisabled = Registry.CurrentUser.OpenSubKey(RunKey + strRegPath);
                }
                else if (hive == rm.GetString("HKLM"))
                {
                    // Get all startup programs in HKEY_LOCAL_MACHINE.
                    rk = Registry.LocalMachine.OpenSubKey(RunKey);
                    rkDisabled = Registry.LocalMachine.OpenSubKey(RunKey + strRegPath);
                }
                else if (hive == rm.GetString("WHKCU"))
                {
                    // Get all the startup programs in HKEY_CURRENT_USER\Wow6432Node.
                    rk = Registry.CurrentUser.OpenSubKey(WowRunKey);
                    rkDisabled = Registry.CurrentUser.OpenSubKey(WowRunKey + strRegPath);
                }
                else if (hive == rm.GetString("WHKLM"))
                {
                    // Get all the startup programs in HKEY_LOCAL_MACHINE\Wow6432Node.
                    rk = Registry.LocalMachine.OpenSubKey(WowRunKey);
                    rkDisabled = Registry.LocalMachine.OpenSubKey(WowRunKey + strRegPath);
                }

                // Get all of the entries.
                if (rk != null)
                {
                    FillListviewWithRegItems(hive, rk, false);
                }
                if (rkDisabled != null)
                {
                    FillListviewWithRegItems(hive, rkDisabled, true);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (rk != null)
                    rk.Close();
                if (rkDisabled != null)
                    rkDisabled.Close();
            }
        }

        private void FillListviewWithRegItems(string hive, RegistryKey regKey, bool isDisabled)
        {
            string command = string.Empty;
            string filePath;
            bool disabled;
            foreach (string value in regKey.GetValueNames())
            {
                // Reset disabled flag.
                disabled = isDisabled;
                // Save complete command.
                command = regKey.GetValue(value).ToString();

                // Check if command is valid
                if (command.Length < 1)
                    continue;

                // Check if command is disabled (begins with a ":")
                if (command.StartsWith(":"))
                {
                    // Flag this entry as disabled.
                    disabled = true;

                    // Remove colon so that path command works and save file path.
                    filePath = ReturnFilePath(command.Remove(0, 1));
                }
                else
                {
                    // Save file path.
                    filePath = ReturnFilePath(command);
                }

                filePath = ReturnFilePath(command);

                // Attempt to get application image (icon).
                if (native.GetIcon(filePath) != null)
                {
                    // Add the icon to the image list so that the listview can access it.
                    imageListStartupManager.Images.Add(native.GetIcon(filePath));
                }
                else
                {
                    // If there is no icon, just add a blank image from resources to keep the indexes proper.
                    imageListStartupManager.Images.Add((Image)resourceManager.GetObject("Blank"));
                }

                // Add entry description to listview.
                ListViewItem lvi;

                if (File.Exists(filePath))
                {
                    FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(filePath);

                    if (fileInfo.FileDescription.Length != 0)
                    {
                        lvi = new ListViewItem(fileInfo.FileDescription);
                    }
                    else
                    {
                        lvi = new ListViewItem(value);
                    }
                }
                else
                {
                    lvi = new ListViewItem(value);
                }

                lvi.ImageIndex = i;
                lvi.Tag = value;

                // Add file name (without path) to listview.
                lvi.SubItems.Add(Path.GetFileName(filePath));

                // Add location (type) information to listview.
                if (hive == rm.GetString("HKCU"))
                {
                    lvi.SubItems.Add(rm.GetString("HKCU"));
                }
                else if (hive == rm.GetString("HKLM"))
                {
                    lvi.SubItems.Add(rm.GetString("HKLM"));
                }
                else if (hive == rm.GetString("WHKCU"))
                {
                    lvi.SubItems.Add(rm.GetString("WHKCU"));
                }
                else if (hive == rm.GetString("WHKLM"))
                {
                    lvi.SubItems.Add(rm.GetString("WHKLM"));
                }

                // Add status information.
                lvi.SubItems.Add(disabled ? rm.GetString("disabled") : rm.GetString("enabled"));

                // Add command.
                lvi.SubItems.Add(command);

                // Add file path.
                lvi.SubItems.Add(filePath);
                listviewStartup.Items.Add(lvi);
                i++;
            }
        }

        #endregion

        #region Display Startup Shortcuts Method

        /// <summary>
        /// Fills a list with shortcuts items
        /// </summary>
        /// <param name="type"></param>
        void DisplayStartupShortcuts(string type)
        {
            bool disabled;
            string command;
            string filePath;
            string folder;

            if (type == rm.GetString("StartupCurrentUser"))
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
                foreach (string shortcut in Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories))
                {
                    // Only process shortcuts, executibles, or disabled shortcuts.
                    if (Path.GetExtension(shortcut) == ".lnk" || Path.GetExtension(shortcut) == ".exe")
                    {
                        // Set diabled flag.
                        if (Path.GetDirectoryName(shortcut).EndsWith("~Disabled"))
                        {
                            disabled = true;
                        }
                        else
                        {
                            disabled = false;
                        }

                        // Save complete command.
                        command = shortcut;

                        // Save file path.
                        filePath = ReturnFilePath(command);

                        // Resolve the shortcut.
                        var sc = new ShortcutClass(filePath);

                        // Attempt to get application image (icon).
                        if (sc.Icon != null)
                        {
                            // First try getting icon from shortcut.
                            imageListStartupManager.Images.Add(sc.Icon);
                        }
                        else if (native.GetIcon(sc.Path) != null)
                        {
                            // Then try getting icon from the resolved path.
                            imageListStartupManager.Images.Add(native.GetIcon(sc.Path));
                        }
                        else
                        {
                            // If both methods fail, display a blank icon.
                            imageListStartupManager.Images.Add((Image)resourceManager.GetObject("Blank"));
                        }

                        // Add entry description to listview.
                        ListViewItem lvi = new ListViewItem(Path.GetFileNameWithoutExtension(shortcut));
                        lvi.ImageIndex = i;

                        // Add file name (without path) to listview.
                        lvi.SubItems.Add(Path.GetFileName(filePath));

                        // Add type information to listview.
                        lvi.SubItems.Add(type == rm.GetString("StartupCurrentUser")
                                                                ? rm.GetString("StartupCurrentUser")
                                                                : rm.GetString("StartupAllUsers"));

                        // Add status information.
                        lvi.SubItems.Add(disabled ? rm.GetString("disabled") : rm.GetString("enabled"));

                        // Add command.
                        lvi.SubItems.Add(command);

                        // Add file path.
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

        #region FindPathFromEnvironment Method

        /// <summary>
        /// Used when only the file name is given (no path). This means either that the file name is
        /// on a path defined by the PATH environment variable, or that the entry is in error.
        /// </summary>
        /// <param name="partialPath">File name without path.</param>
        /// <returns>Complete path to file or blank if it cannot be resolved.</returns>
        static string FindPathFromEnvironment(string partialPath)
        {
            // Get all defined paths for all users.
            string definedPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);

            // Combine with all defined paths for current user.
            definedPath = definedPath + Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);

            try
            {
                // Remove any quote characters.
                definedPath = definedPath.Replace("\"", string.Empty);

                // Get all of the defined paths.
                string[] paths = definedPath.Split(new[] { ';' });

                // Check all of the paths. Return complete path if found, otherwise a blank.
                foreach (string path in paths)
                {
                    if (File.Exists(Path.Combine(path, partialPath)))
                    {
                        definedPath = Path.Combine(path, partialPath);
                        break;
                    }
                    definedPath = string.Empty;
                }
            }
            catch (ArgumentException)
            {
                return string.Empty;
            }

            return definedPath;
        }

        #endregion

        #region Return FilePath Method

        /// <summary>
        /// Transforms value to the valid file path
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string ReturnFilePath(string value)
        {
            try
            {
                int p;
                // Check for quotes, and if present, remove them.
                if (value.Contains("\"")) // quote character 34, 22H
                {
                    value = value.Replace("\"", string.Empty);
                }

                // Check for brackets, and if present, remove them.
                if (value.Contains("["))
                {
                    value = value.Replace("[", string.Empty);
                }

                if (value.Contains("]"))
                {
                    value = value.Replace("]", string.Empty);
                }

                // Check for hyphens, and if present, return the part before first one.
                if (value.Contains(" -"))
                {
                    p = value.IndexOf(" -");
                    value = value.Substring(0, p);
                }

                // Check for forward slashes, and if present, return the part before first one.
                if (value.Contains("/"))
                {
                    p = value.IndexOf("/");
                    value = value.Substring(0, p - 1);
                }

                // Check for a space followed by a percent sign, and if present, return the part before the first one.
                if (value.Contains(" %"))
                {
                    p = value.IndexOf(" %");
                    value = value.Substring(0, p);
                }

                // Check for "rundll32" or "rundll32.exe"
                if (value.ToLower().Contains("rundll32"))
                {
                    value = "rundll32.exe";
                }

                // If path does not contain ":\", then it is not complete: check PATH environment variable.
                if (!value.Contains(@":\"))
                {
                    if (!string.IsNullOrEmpty(FindPathFromEnvironment(value)))
                    {
                        value = FindPathFromEnvironment(value);
                        return value;
                    }
                    return string.Empty;
                }
                if (!string.IsNullOrEmpty(value))
                {
                    return Path.GetFullPath(value);
                }
                return string.Empty;
            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show(string.Format("{0}{1}{2}{3}{4}{5}: {6}", rm.GetString("folder_not_found"), Environment.NewLine,
                                              rm.GetString("description"), e.Message, Environment.NewLine, rm.GetString("command"), value),
                                rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(string.Format("{0}{1}{2}{3}{4}{5}: {6}", rm.GetString("file_not_found_warning"), Environment.NewLine,
                              rm.GetString("description"), ex.Message, Environment.NewLine, rm.GetString("command"), value),
                rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        #endregion


        #region Enable Item Method

        /// <summary>
        /// Enables item in registry
        /// </summary>
        /// <param name="regDir"></param>
        /// <param name="subKeyName"></param>
        /// <returns></returns>
        bool EnableItemInReg(string regDir, string subKeyName)
        {
            bool result = false;
            RegistryKey regKey = null;
            string command = string.Empty;
            try
            {
                RegistryKey msConfigKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Shared Tools\\MSConfig\\startupreg", true);

                if (regDir == rm.GetString("HKCU"))
                {
                    regKey = Registry.CurrentUser.OpenSubKey(RunKey, true);
                }
                else if (regDir == rm.GetString("WHKCU"))
                {
                    // Open "Run" key in HKEY_CURRENT_USER\Wow6432Node and get value for this entry.
                    regKey = Registry.CurrentUser.OpenSubKey(WowRunKey, true);
                }
                else if (regDir == rm.GetString("HKLM"))
                {
                    // Open "Run" key in HKEY_LOCAL_MACHINE and get value for this entry.
                    regKey = Registry.LocalMachine.OpenSubKey(RunKey, true);
                }
                if (regDir == rm.GetString("WHKLM"))
                {
                    // Open "Run" key in HKEY_LOCAL_MACHINE\Wow6432Node and get value for this entry.
                    regKey = Registry.LocalMachine.OpenSubKey(WowRunKey, true);
                }

                if (regKey != null)
                {
                    if (regKey.GetValue(subKeyName) == null)
                    {
                        using (RegistryKey _rk = msConfigKey.OpenSubKey(subKeyName))
                        {
                            regKey.SetValue(subKeyName, _rk.GetValue("command"));
                            msConfigKey.DeleteSubKey(subKeyName);
                        }
                    }
                    else
                    {
                        command = regKey.GetValue(subKeyName).ToString();
                        regKey.SetValue(subKeyName, command);

                        regKey.DeleteValue(subKeyName);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Close registry key.
                if (regKey != null)
                {
                    regKey.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Enables item for user
        /// </summary>
        /// <param name="filePath">filepath where disable item located</param>
        /// <param name="newFilePath">filepath where the item will be located after enabling</param>
        /// <returns></returns>
        bool EnableItemForUser(string filePath, string newFilePath)
        {
            bool result = false;

            if (File.Exists(filePath))
            {
                // Remove any attributes.
                File.SetAttributes(filePath, FileAttributes.Normal);
                // Make sure this is a .lnk file.
                if (Path.GetDirectoryName(filePath).EndsWith("~Disabled"))
                {
                    if (File.Exists(newFilePath))
                        File.Delete(newFilePath);

                    File.Move(filePath, newFilePath);
                    result = true;
                }
                else
                {
                    MessageBox.Show(rm.GetString("file_appear_not_valid"),
                                    rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                //  Cannot find file.
                MessageBox.Show(rm.GetString("file_folder_not_found"), rm.GetString("startup_manager"),
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }

        /// <summary>
        /// Enables item
        /// </summary>
        /// <param name="index">ittem index in list</param>
        void EnableItem(int index)
        {
            string filePath = string.Empty;
            string newFilePath = string.Empty;

            try
            {
                // Get type (location) of startup item.
                string text = listviewStartup.Items[index].SubItems[(int)ListCol.Type].Text;

                if (text == rm.GetString("HKCU") || text == rm.GetString("WHKCU") || text == rm.GetString("HKLM") || text == rm.GetString("WHKLM"))
                {
                    if (EnableItemInReg(text, listviewStartup.Items[index].Tag.ToString()))
                    {
                        // Change the listview to indicate that this item is now enabled.
                        listviewStartup.Items[index].SubItems[(int)ListCol.Status].Text = rm.GetString("enabled");

                        // Set context menu and tool strip buttons.
                        if (text == rm.GetString("HKCU") || text == rm.GetString("WHKCU"))
                            SetItems(index, "Current User");
                        else
                            SetItems(index, "All Users");
                    }
                    return;
                }
                else if (text == rm.GetString("StartupCurrentUser") || text == rm.GetString("StartupAllUsers"))
                {
                    // Get the path.
                    filePath = listviewStartup.Items[index].SubItems[(int)ListCol.Path].Text;
                    newFilePath = filePath.Replace("~Disabled", string.Empty);

                    if (EnableItemForUser(filePath, newFilePath))
                    {
                        listviewStartup.Items[index].SubItems[(int)ListCol.Status].Text = rm.GetString("enabled");

                        // Store the new path in the listview.
                        listviewStartup.Items[index].SubItems[(int)ListCol.Path].Text = newFilePath;

                        // Store the new filename in the listview.
                        listviewStartup.Items[index].SubItems[(int)ListCol.FileName].Text = Path.GetFileName(newFilePath);

                        // Set context menu and tool strip buttons.
                        if (text == rm.GetString("StartupCurrentUser"))
                            SetItems(index, "Current User");
                        else
                            SetItems(index, "All User");
                    };

                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex is Win32Exception)
                {
                    //  Cannot find file.
                    MessageBox.Show(rm.GetString("file_folder_not_found"), rm.GetString("startup_manager"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //UnauthorizedAccessException, ArgumentException, is SecurityException etc                    
                    MessageBox.Show(string.Format("{0}{1}{2}{3}{4}", rm.GetString("unable_to_enable"), Environment.NewLine,
                                rm.GetString("system_returned"), Environment.NewLine, ex.Message), rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Disable Item Method
        /// <summary>
        /// Disables item in registry
        /// </summary>
        /// <param name="regDir">registry directory name</param>
        /// <param name="subKeyName">subkey to delete</param>
        /// <returns>true - if success, false - otherwise</returns>
        bool DisableItemInReg(string regDir, string subKeyName)
        {
            bool result = false;
            string command = string.Empty;
            RegistryKey regKey = null;
            try
            {
                RegistryKey msConfigKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Shared Tools\\MSConfig\\startupreg", true);

                if (regDir == rm.GetString("HKCU"))
                {
                    regKey = Registry.CurrentUser.OpenSubKey(RunKey, true);
                }

                if (regDir == rm.GetString("WHKCU"))
                {
                    regKey = Registry.CurrentUser.OpenSubKey(WowRunKey, true);
                }

                if (regDir == rm.GetString("HKLM"))
                {
                    // Open "Run" key in HKEY_LOCAL_MACHINE and get value for this entry.
                    regKey = Registry.LocalMachine.OpenSubKey(RunKey, true);
                }

                if (regDir == rm.GetString("WHKLM"))
                {
                    // Open "Run" key in HKEY_LOCAL_MACHINE\Wow6432Node and get value for this entry.
                    regKey = Registry.LocalMachine.OpenSubKey(WowRunKey, true);
                }

                if (regKey.GetValue(subKeyName) != null)
                {
                    command = regKey.GetValue(subKeyName).ToString();
                    // Check that entry does not begins with a colon (:), which would indicate it is already disabled
                    using (RegistryKey newKey = msConfigKey.CreateSubKey(subKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        //msConfigKey.OpenSubKey(subKeyName, true);
                        newKey.SetValue("item", subKeyName);
                        newKey.SetValue("command", command);
                        newKey.SetValue("hkey", "HKCU");
                        newKey.SetValue("key", RunKey);
                        DateTime dt = DateTime.Now;

                        newKey.SetValue("DAY", int.Parse(dt.Date.Day.ToString()), RegistryValueKind.DWord);
                        newKey.SetValue("HOUR", int.Parse(dt.Date.Hour.ToString()), RegistryValueKind.DWord);
                        newKey.SetValue("MINUTE", int.Parse(dt.Date.Minute.ToString()), RegistryValueKind.DWord);
                        newKey.SetValue("MONTH", int.Parse(dt.Date.Month.ToString()), RegistryValueKind.DWord);
                        newKey.SetValue("SECOND", int.Parse(dt.Date.Second.ToString()), RegistryValueKind.DWord);
                        newKey.SetValue("YEAR", int.Parse(dt.Date.Year.ToString()), RegistryValueKind.DWord);
                        newKey.SetValue("inimapping", 0);
                    }
                    regKey.DeleteValue(subKeyName);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Close registry key.
                if (regKey != null)
                    regKey.Close();
            }
            return result;
        }

        /// <summary>
        /// Deletes item for user
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="newFilePath"></param>
        /// <returns>true - if success, false - otherwise</returns>
        bool DisableItemForUser(string filePath, out string newFilePath)
        {
            bool result = false;
            newFilePath = string.Empty;
            // Make sure the shortcut exists.
            if (File.Exists(filePath))
            {
                // Remove any attributes.
                File.SetAttributes(filePath, FileAttributes.Normal);

                // Make sure this is a .lnk file.
                if (Path.GetExtension(filePath) == ".lnk")
                {
                    string disabledFolder = Path.GetDirectoryName(filePath) + @"\~Disabled";

                    if (!Directory.Exists(disabledFolder))
                    {
                        // Create subdirectory.
                        Directory.CreateDirectory(disabledFolder);

                        // Make it hidden.
                        File.SetAttributes(disabledFolder, FileAttributes.Hidden);
                    }

                    // Move the .lnk file to the disabled folder.
                    string tmpNewFilePath = disabledFolder + @"\" + Path.GetFileName(filePath);

                    if (File.Exists(tmpNewFilePath))
                        File.Delete(tmpNewFilePath);

                    File.Move(filePath, tmpNewFilePath);
                    result = true;
                    newFilePath = tmpNewFilePath;
                }
                else
                {
                    MessageBox.Show(rm.GetString("file_appear_not_valid"),
                                    rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                //  Cannot find file.
                MessageBox.Show(rm.GetString("file_folder_not_found"), rm.GetString("startup_manager"),
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }

        /// <summary>
        /// Disables item
        /// </summary>
        /// <param name="index">item index in list</param>
        void DisableItem(int index)
        {

            string filePath = string.Empty;
            string newFilePath = string.Empty;

            try
            {
                // Get type (location) of startup item.
                string text = listviewStartup.Items[index].SubItems[(int)ListCol.Type].Text;

                if (text == rm.GetString("HKCU") || text == rm.GetString("WHKCU") || text == rm.GetString("HKLM") || text == rm.GetString("WHKLM"))
                {
                    if (DisableItemInReg(text, listviewStartup.Items[index].Tag.ToString()))
                    {
                        listviewStartup.Items[index].SubItems[(int)ListCol.Status].Text = rm.GetString("disabled");

                        // Set context menu and tool strip buttons.
                        if (text == rm.GetString("HKCU") || text == rm.GetString("WHKCU"))
                            SetItems(index, "Current User");
                        else
                            SetItems(index, "All Users");
                    }
                    return;
                }
                else if (text == rm.GetString("StartupCurrentUser") || text == rm.GetString("StartupAllUsers"))
                {
                    // Get the path.
                    filePath = listviewStartup.Items[index].SubItems[(int)ListCol.Path].Text;
                    if (DisableItemForUser(filePath, out newFilePath))
                    {
                        // Change the listview to indicate that this item is now disabled.
                        listviewStartup.Items[index].SubItems[(int)ListCol.Status].Text = rm.GetString("disabled");

                        // Store the new path in the listview.File
                        listviewStartup.Items[index].SubItems[(int)ListCol.Path].Text = newFilePath;

                        // Store the new filename in the listview.
                        listviewStartup.Items[index].SubItems[(int)ListCol.FileName].Text = Path.GetFileName(newFilePath);

                        // Set context menu and tool strip buttons.
                        if (text == rm.GetString("StartupCurrentUser"))
                            SetItems(index, "Current User");
                        else
                            SetItems(index, "All User");
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex is Win32Exception)
                {
                    //  Cannot find file.
                    MessageBox.Show(rm.GetString("file_folder_not_found"), rm.GetString("startup_manager"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (ex is NullReferenceException)
                {
                    MessageBox.Show(ex.Source, rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(string.Format("{0}{1}{2}{3}{4}", rm.GetString("unable_to_disable"), Environment.NewLine,
                    rm.GetString("system_returned"), Environment.NewLine, ex.Message),
                    rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Delete Item Method

        /// <summary>
        /// Deletes item in registry
        /// </summary>
        /// <param name="regDir">registry directory name</param>
        /// <param name="subKeyName">subkey to delete</param>
        /// <returns>true - if success, false - otherwise</returns>
        bool DeleteItemInReg(string regDir, string subKeyName)
        {
            bool result = false;
            RegistryKey regKey = null;
            try
            {
                if (regDir == rm.GetString("HKCU"))
                {
                    // Open "Run" key in HKEY_CURRENT_USER.
                    regKey = Registry.CurrentUser.OpenSubKey(RunKey, true);
                }

                else if (regDir == rm.GetString("WHKCU"))
                {
                    // Open "Run" key in HKEY_CURRENT_USER\Wow6432Node.
                    regKey = Registry.CurrentUser.OpenSubKey(WowRunKey, true);
                }
                else if (regDir == rm.GetString("HKLM"))
                {
                    // Open "Run" key in HKEY_LOCAL_MACHINE.
                    regKey = Registry.LocalMachine.OpenSubKey(RunKey, true);
                }
                else if (regDir == rm.GetString("WHKLM"))
                {
                    // Open "Run" key in HKEY_LOCAL_MACHINE\Wow6432Node.
                    regKey = Registry.LocalMachine.OpenSubKey(WowRunKey, true);
                }
                // Attempt to delete the value.
                regKey.DeleteValue(subKeyName);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Close registry key.
                if (regKey != null)
                {
                    regKey.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes item
        /// </summary>
        /// <param name="fileName">full path to file</param>
        /// <returns>true - if success, false - otherwise</returns>
        bool DeleteItemForUser(string fileName)
        {
            bool result = false;
            // Make sure file exists.
            if (File.Exists(fileName))
            {
                // Remove attributes.
                File.SetAttributes(fileName, FileAttributes.Normal);

                // Delete file.
                File.Delete(fileName);
                result = true;
            }
            else
            {
                MessageBox.Show(rm.GetString("file_not_found"), rm.GetString("startup_manager"),
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            return result;
        }

        /// <summary>
        /// Deletes a startup item
        /// </summary>
        /// <param name="index"></param>
        void DeleteItem(int index)
        {
            string fileName;

            try
            {
                // Get type (location) of startup item.
                string text = listviewStartup.Items[index].SubItems[(int)ListCol.Type].Text;

                if (text == rm.GetString("HKCU") || text == rm.GetString("WHKCU") || text == rm.GetString("HKLM") || text == rm.GetString("WHKLM"))
                {
                    if (DeleteItemInReg(text, listviewStartup.Items[index].Tag.ToString()))
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

                    return;
                }

                if (text == rm.GetString("StartupCurrentUser") || text == rm.GetString("StartupAllUsers"))
                {
                    // Get the file name.
                    fileName = listviewStartup.Items[index].SubItems[(int)ListCol.Path].Text;

                    if (DeleteItemForUser(fileName))
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
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}{1}{2}{3}{4}", rm.GetString("unable_to_delete"), Environment.NewLine,
                rm.GetString("system_returned"), Environment.NewLine, ex.Message),
                rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Open Folder Method

        /// <summary>
        /// Opens folder where the current item is located in Windows Explorer
        /// </summary>
        /// <param name="index">index of item</param>
        void OpenFolder(int index)
        {
            try
            {
                string folder;

                // Get path of the startup item.
                folder = listviewStartup.Items[index].SubItems[(int)ListCol.Path].Text;

                // If path exists, open the folder with Explorer.
                if (File.Exists(folder))
                {
                    // Get just the directory.
                    folder = Path.GetDirectoryName(folder);

                    // Start Explorer.
                    ProcessStartInfo startInfo =
                        new ProcessStartInfo(Environment.GetEnvironmentVariable("windir") + "\\explorer.exe");
                    startInfo.Arguments = " /root, " + folder;
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    Process.Start(startInfo);
                }
                else
                {
                    //  Cannot find file.
                    MessageBox.Show(rm.GetString("file_folder_not_found"), rm.GetString("startup_manager"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                if (ex is Win32Exception)
                {
                    //  Cannot find file.
                    MessageBox.Show(rm.GetString("file_folder_not_found"), rm.GetString("startup_manager"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(string.Format("{0}{1}{2}{3}{4}", rm.GetString("unable_to_open"), Environment.NewLine,
                              rm.GetString("system_returned"), Environment.NewLine, ex.Message),
                    rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Execute Command Method
        /// <summary>
        /// Runs current item
        /// </summary>
        /// <param name="index">item index in list</param>
        void ExecuteCommand(int index)
        {
            try
            {
                string fileName;
                string arguments;

                // Get the file name with path.
                fileName = listviewStartup.Items[index].SubItems[(int)ListCol.Path].Text;

                // If the file is a shortcut, resolve it; otherwise use filename.
                if (Path.GetExtension(fileName) == ".lnk")
                {
                    var sc = new ShortcutClass(fileName);
                    fileName = sc.Path;
                    arguments = sc.Arguments;

                    sc.Dispose();
                }
                else
                {
                    // Get the complete command.
                    string command = listviewStartup.Items[index].SubItems[(int)ListCol.Command].Text;

                    // If the command contains a complete path and filename, remove to get the arguments.
                    if (command.Contains(@"\:"))
                    {
                        arguments = command.Replace(fileName, string.Empty);
                    }
                    // Check if filename in "command" does not contain an extension.
                    // Just remove the filename to get the arguments.
                    else if (!command.Substring(0, command.IndexOf(" ") - 1).Contains("."))
                    {
                        arguments = command.Replace(Path.GetFileNameWithoutExtension(fileName), string.Empty);
                    }
                    else
                    {
                        arguments = command.Replace(Path.GetFileName(fileName), string.Empty);
                    }

                    // Remove all quotes.
                    arguments = arguments.Replace("\"", string.Empty);
                }

                if (File.Exists(fileName))
                {
                    // Start program.
                    var startInfo = new ProcessStartInfo(fileName);
                    startInfo.Arguments = arguments;
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    Process.Start(startInfo);
                }
                else
                {
                    //  Cannot find file.
                    MessageBox.Show(rm.GetString("file_folder_not_found"), rm.GetString("startup_manager"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                if (ex is Win32Exception)
                {
                    MessageBox.Show(rm.GetString("file_folder_not_found"), rm.GetString("startup_manager"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(string.Format("{0}{1}{2}{3}{4}", rm.GetString("unable_to_execute"), Environment.NewLine,
                    rm.GetString("system_returned"), Environment.NewLine, ex.Message),
                    rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Move to All Users Method

        /// <summary>
        /// Moves an item in registry so that it will be available for 'All Users'
        /// </summary>
        /// <param name="regDir">current directory in registry where the item is located</param>
        /// <param name="subKeyName">subKey name</param>
        /// <returns>true if success, false - otherwise</returns>
        bool MoveToAllUsersInReg(string regDir, string subKeyName)
        {
            bool result = false;
            RegistryKey regKey = null;
            string command = string.Empty;
            try
            {
                if (regDir == rm.GetString("HKCU"))
                {
                    // Open "Run" key in HKEY_CURRENT_USER and get value.
                    regKey = Registry.CurrentUser.OpenSubKey(RunKey, true);
                }
                else if (regDir == rm.GetString("WHKCU"))
                {
                    regKey = Registry.CurrentUser.OpenSubKey(WowRunKey, true);
                }
                else
                    return result;

                command = regKey.GetValue(subKeyName).ToString();

                // Delete the value in HKEY_LOCAL_MACHINE.
                regKey.DeleteValue(subKeyName);

                regKey.Close();
                if (regDir == "HKCU")
                    // Open "Run" key in HKEY_LOCAL_MACHINE and set the value.
                    regKey = Registry.LocalMachine.OpenSubKey(RunKey, true);
                else
                    // Open "Run" key in HKEY_LOCAL_MACHINE\Wow6432Node and set the value.
                    regKey = Registry.LocalMachine.OpenSubKey(WowRunKey, true);
                regKey.SetValue(subKeyName, command);

                regKey.Close();
                result = true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                // Close registry key.
                if (regKey != null)
                {
                    regKey.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Moves executable file for the item so that it will be available for 'All Users'
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="newFilePath"></param>
        /// <returns>true - if success, false - otherwise</returns>
        bool MoveToAllUsersForUser(string filePath, string newFilePath)
        {
            bool result = false;

            if (File.Exists(filePath))
            {
                // Remove any attributes.
                File.SetAttributes(filePath, FileAttributes.Normal);

                // Move the shortcut from Current User to All Users.   

                if (File.Exists(newFilePath))
                    File.Delete(newFilePath);

                File.Move(filePath, newFilePath);

                result = true;
            }
            else
            {
                MessageBox.Show(rm.GetString("file_folder_not_found"), rm.GetString("startup_manager"),
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }

        /// <summary>
        /// Moves item to be available for 'All Users'
        /// </summary>
        /// <param name="index"></param>
        void MoveToAllUsers(int index)
        {
            try
            {
                // Get type (location) of startup item.
                string text = listviewStartup.Items[index].SubItems[(int)ListCol.Type].Text;
                if (text == rm.GetString("HKCU") || text == rm.GetString("WHKCU"))
                {
                    if (MoveToAllUsersInReg(text, listviewStartup.Items[index].Tag.ToString()))
                    {
                        // Change the listview to indicate that this item is now disabled.
                        listviewStartup.Items[index].SubItems[(int)ListCol.Type].Text = rm.GetString("HKLM");

                        // Set context menu and tool strip buttons.
                        SetItems(index, "All Users");
                    }
                    return;
                }
                else if (text == rm.GetString("HKLM") || text == rm.GetString("WHKLM"))
                {
                    // Do nothing. Startup item is already in All Users.
                    return;
                }
                else if (text == rm.GetString("StartupCurrentUser"))
                {
                    string filePath;
                    string newFilePath;

                    // Get the file name.
                    string path = listviewStartup.Items[index].SubItems[(int)ListCol.FileName].Text;

                    // Get the current path.
                    filePath = currentUserStartup + path;

                    // Get the new path.
                    newFilePath = allUsersStartup + path;

                    if (MoveToAllUsersForUser(filePath, newFilePath))
                    {
                        // Change the location in the listview.
                        listviewStartup.Items[index].SubItems[(int)ListCol.Type].Text = rm.GetString("StartupAllUsers");

                        // Set context menu and tool strip buttons.
                        SetItems(index, "All Users");
                    }

                    return;
                }
                else if (text == rm.GetString("StartupAllUsers"))
                {
                    // Do nothing. Startup item is already in All Users.
                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex is Win32Exception)
                {
                    MessageBox.Show(rm.GetString("file_folder_not_found"), rm.GetString("startup_manager"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(string.Format("{0}{1}{2}{3}{4}", rm.GetString("unable_to_move"), Environment.NewLine,
                        rm.GetString("system_returned"), Environment.NewLine, ex.Message),
                        rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                //Update List
                FillListview();
            }
        }

        #endregion

        #region Move to Current User Method

        /// <summary>
        /// Moves item in registry so that it will be available for 'Current User'
        /// </summary>
        /// <param name="regDir"></param>
        /// <param name="subKeyName"></param>
        /// <returns></returns>
        bool MoveToCurrentUserInReg(string regDir, string subKeyName)
        {
            bool result = false;
            RegistryKey regKey = null;
            string command = string.Empty;
            try
            {
                if (regDir == rm.GetString("HKLM"))
                {
                    // Open "Run" key in HKEY_LOCAL_MACHINE and get value.
                    regKey = Registry.LocalMachine.OpenSubKey(RunKey, true);
                }
                else if (regDir == rm.GetString("WHKLM"))
                {
                    // Open "Run" key in HKEY_LOCAL_MACHINE\Wow6432Node and get value.
                    regKey = Registry.LocalMachine.OpenSubKey(WowRunKey, true);
                }
                else
                    return result;

                command = regKey.GetValue(subKeyName).ToString();

                // Delete the value in HKEY_LOCAL_MACHINE.
                regKey.DeleteValue(subKeyName);

                regKey.Close();

                regKey = Registry.CurrentUser.OpenSubKey(RunKey, true);
                regKey.SetValue(subKeyName, command);

                regKey.Close();
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Close registry key.
                if (regKey != null)
                {
                    regKey.Close();
                }
            }
            return result;
        }

        /// Moves executable file for the item so that it will be available for 'Current User'
        bool MoveToCurrentUsersForUser(string filePath, string newFilePath)
        {
            bool result = false;
            if (File.Exists(filePath))
            {
                // Remove any attributes.
                File.SetAttributes(filePath, FileAttributes.Normal);

                // Move the shortcut from Current User to All Users.   

                if (File.Exists(newFilePath))
                    File.Delete(newFilePath);

                File.Move(filePath, newFilePath);

                result = true;
            }
            else
            {
                MessageBox.Show(rm.GetString("file_folder_not_found"), rm.GetString("startup_manager"),
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }

        /// <summary>
        /// Moves item to be available for 'Current User'
        /// </summary>
        /// <param name="index"></param>
        void MoveToCurrentUser(int index)
        {
            try
            {
                // Get type (location) of startup item.
                string text = listviewStartup.Items[index].SubItems[(int)ListCol.Type].Text;

                if (text == rm.GetString("HKCU") || text == rm.GetString("WHKCU"))
                {
                    // Do nothing. Startup item is already in Current User.
                    return;
                }
                else if (text == rm.GetString("HKLM") || text == rm.GetString("WHKLM"))
                {
                    if (MoveToCurrentUserInReg(text, listviewStartup.Items[index].Tag.ToString()))
                    {
                        // Change the listview to indicate that this item is now disabled.                        
                        listviewStartup.Items[index].SubItems[(int)ListCol.Type].Text = rm.GetString("HKCU");

                        // Set context menu and tool strip buttons.
                        SetItems(index, "Current User");
                    }
                    return;
                }

                if (text == rm.GetString("StartupCurrenUser"))
                {
                    // Do nothing. Startup item is already in Current User.
                    return;
                }
                if (text == rm.GetString("StartupAllUsers"))
                {
                    string filePath, newFilePath;
                    // Get the file name.
                    string path = listviewStartup.Items[index].SubItems[(int)ListCol.FileName].Text;

                    // Get the current path.
                    filePath = allUsersStartup + path;
                    // Get the new path.
                    newFilePath = currentUserStartup + path;
                    if (MoveToCurrentUsersForUser(filePath, newFilePath))
                    {
                        // Change the listview to indicate that this item is now disabled.
                        listviewStartup.Items[index].SubItems[(int)ListCol.Type].Text = rm.GetString("StartupCurrentUser");

                        // Set context menu and tool strip buttons.
                        SetItems(index, "Current User");
                    }

                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex is Win32Exception)
                {
                    MessageBox.Show(rm.GetString("file_folder_not_found"), rm.GetString("startup_manager"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(string.Format("{0}{1}{2}{3}{4}", rm.GetString("unable_to_move"), Environment.NewLine,
                                            rm.GetString("system_returned"), Environment.NewLine, ex.Message),
                              rm.GetString("startup_manager"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                //Update List
                FillListview();
            }
        }

        #endregion

        /// <summary>
        /// constructor for MainForm
        /// </summary>
        public FormMain()
        {
            InitializeComponent();


            //Set All User Startup Path
            var path = new StringBuilder(260);
            SHGetSpecialFolderPath(IntPtr.Zero, path, CSIDL_COMMON_STARTMENU, false);
            allUsersStartup = path + "\\Programs\\Startup\\";
        }

        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner,
                                                  [Out] StringBuilder lpszPath, int nFolder, bool fCreate);

        /// <summary>
        /// Gets <c>Bitmap</c> for a specified <paramref name="icon"/>
        /// </summary>
        /// <param name="icon">Icon</param>
        /// <returns><c>Bitmap</c> for a specified <paramref name="icon"/></returns>
        public Bitmap GetBitmap(Icon icon)
        {
            var bmp = new Bitmap(icon.Width, icon.Height);

            //Create temporary graphics
            var gxMem = Graphics.FromImage(bmp);

            //Draw the icon
            gxMem.DrawIcon(icon, 0, 0);

            //Clean up
            gxMem.Dispose();

            return bmp;
        }

        /// <summary>
        /// Sets culture for the form
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            var resourceManager = new ResourceManager("StartupManager.Resources", typeof(FormMain).Assembly);
            Thread.CurrentThread.CurrentUICulture = culture;
            toolStripButtonDisable.Text = resourceManager.GetString("disable");
            toolStripButtonDisable.ToolTipText = resourceManager.GetString("disable_startup_item");
            toolStripButtonEnable.Text = resourceManager.GetString("enable");
            toolStripButtonEnable.ToolTipText = resourceManager.GetString("enable_startup_item");
            toolStripButtonDelete.Text = resourceManager.GetString("delete");
            toolStripButtonDelete.ToolTipText = resourceManager.GetString("delete_startup_item");
            toolStripButtonOpen.Text = resourceManager.GetString("open_folder");
            toolStripButtonOpen.ToolTipText = resourceManager.GetString("open_folder_tip");
            toolStripButtonExecute.Text = resourceManager.GetString("execute");
            toolStripButtonExecute.ToolTipText = resourceManager.GetString("execute_command");
            toolStripButtonMoveToCurrentUser.Text = resourceManager.GetString("move_to_uesr");
            toolStripButtonMoveToCurrentUser.ToolTipText = resourceManager.GetString("move_to_user_tip");
            toolStripButtonMoveToAllUsers.Text = resourceManager.GetString("move_to_all");
            toolStripButtonMoveToAllUsers.ToolTipText = resourceManager.GetString("move_to_all_tip");
            toolStripButtonRefresh.Text = resourceManager.GetString("refresh");
            toolStripButtonRefresh.ToolTipText = resourceManager.GetString("refresh_tip");
            toolStripMenuItemEnable.Text = "&" + resourceManager.GetString("enable");
            toolStripMenuItemDisable.Text = "&" + resourceManager.GetString("disable");
            toolStripMenuItemDelete.Text = resourceManager.GetString("delete");
            toolStripMenuItemOpen.Text = "&" + resourceManager.GetString("open_file_folder");
            toolStripMenuItemExecute.Text = resourceManager.GetString("execute_command");
            toolStripMenuItemMoveToCurrentUser.Text = resourceManager.GetString("move_to_current_user");
            toolStripMenuItemMoveToAllUsers.Text = resourceManager.GetString("move_to_all_users");
            ItemName.Text = resourceManager.GetString("item_name");
            FileName.Text = resourceManager.GetString("file_name");
            Type.Text = resourceManager.GetString("location");
            Status.Text = resourceManager.GetString("status");
            labelCommandDesc.Text = resourceManager.GetString("command") + ":";
            labelFileVersionDesc.Text = resourceManager.GetString("full_version") + ":";
            labelDescriptionDesc.Text = resourceManager.GetString("description");
            labelCompanyDesc.Text = resourceManager.GetString("company") + ":";
            labelDetails.Text = resourceManager.GetString("details");
            Text = resourceManager.GetString("startup_manager");
            labelProductNameDesc.Text = resourceManager.GetString("product") + ":";
            ucTop.Text = resourceManager.GetString("startup_manager");
        }
    }
}