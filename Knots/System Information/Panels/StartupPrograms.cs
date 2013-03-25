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
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SystemInformation
{
	/// <summary>
	/// Startup programs panel of the System Information utility
	/// </summary>
	public partial class StartupPrograms : SystemInformation.TaskPanelBase
	{
		static StartupPrograms panelInstance;
		NativeMethods native = new NativeMethods();

		System.Resources.ResourceManager ResourceManager = new System.Resources.ResourceManager("SystemInformation.Properties.Resources", typeof(StartupPrograms).Assembly);

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static StartupPrograms CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new StartupPrograms();
			}
			return panelInstance;
		}

		#region Constants and Variables

		const string runKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
		string allUsersStartup = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\";
		string currentUserStartup = Environment.GetFolderPath(Environment.SpecialFolder.Programs) + @"\Startup\";
		int i;                  // Counter for registry startup entries.
		bool ascending;         // Used to toggle column sort.

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

		// Shortcut type (location) constants.
		const string HKCU = "Registry This User";
		const string HKLM = "Registry All Users";
		const string StartupUser = "Startup This User";
		const string StartupAllUsers = "Startup All Users";

		#endregion

		#region " Startup Events "

		void Startup_Load(System.Object sender, System.EventArgs e)
		{
			ResourceManager rm = new ResourceManager("SystemInformation.Resources", System.Reflection.Assembly.GetExecutingAssembly());

			this.labelTitle.Text = rm.GetString("node_startupprogs");
			this.labelStartupDescription.Text = rm.GetString("startupprogs_description") + ".";
			this.ItemName.Text = rm.GetString("startupprogs_itemname");
			this.FileName.Text = rm.GetString("startupprogs_filename");
			this.Type.Text = rm.GetString("startupprogs_location");
			this.Status.Text = rm.GetString("startupprogs_status");
			this.labelCommandDesc.Text = rm.GetString("startupprogs_command") + ":";
			this.labelFileVersionDesc.Text = rm.GetString("startupprogs_fileversion") + ":";
			this.labelDescriptionDesc.Text = rm.GetString("startupprogs_details_description") + ":";
			this.labelCompanyDesc.Text = rm.GetString("startupprogs_company") + ":";
			this.labelDetails.Text = rm.GetString("startupprogs_details");
			this.labelProductNameDesc.Text = rm.GetString("startupprogs_product") + ":";
			this.labelArgumentsDesc.Text = rm.GetString("startupprogs_args") + ":";


			try
			{
				Application.DoEvents();

				// Clear image list and listview.
				listviewStartup.Items.Clear();
				StartupImageList.Images.Clear();

				// Get all startup programs in HHEY_CURRENT_USER
				DisplayRegistryStartupEntries(HKCU);

				// Get all startup programs in HKEY_LOCAL_MACHINE
				DisplayRegistryStartupEntries(HKLM);

				// Get all startup shortcuts and programs in the Current User's Startup Folder.
				DisplayStartupShortcuts(StartupUser);

				// Get all startup shortcuts and programs in the All User's Startup Folder.
				DisplayStartupShortcuts(StartupAllUsers);
			}
			catch { }

		}

		#endregion

		#region " ListView Events "

		void listviewStartup_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			try
			{
				if (e.IsSelected)
				{
					// Get command and file path stored in listview and make available panel wide.
					string command = listviewStartup.Items[e.ItemIndex].SubItems[(int)ListCol.Command].Text;
					string filePath = listviewStartup.Items[e.ItemIndex].SubItems[(int)ListCol.Path].Text;

					// Display the file information.
					if (filePath.Contains("cmd.exe"))
					{
						// Since this is a command window, we will not be able to resolve any properties.
						labelCompany.Text = "";
						labelProductName.Text = "";
						labelDescription.Text = "";
						labelFileVersion.Text = "";
						labelCommand.Text = command;

						// Only display arguments for shortcuts.
						labelArguments.Visible = false;
						labelArgumentsDesc.Visible = false;
						labelArguments.Text = "";
					}
					else if (Path.GetExtension(filePath) == ".lnk")
					{
						// Resolve the shortcut.
						ShortcutClass sc = new ShortcutClass(filePath);

						// Get the file version information.
						FileVersionInfo selectedFileVersionInfo =
							FileVersionInfo.GetVersionInfo(sc.Path);

						// Display the resolved shortcut properties.
						labelCompany.Text = selectedFileVersionInfo.CompanyName;
						labelProductName.Text = selectedFileVersionInfo.ProductName;
						labelDescription.Text = selectedFileVersionInfo.FileDescription;
						labelFileVersion.Text = selectedFileVersionInfo.FileVersion;
						labelCommand.Text = sc.Path;

						// Display arguments for shortcuts, but only if present.
						if (string.IsNullOrEmpty(sc.Arguments))
						{
							labelArgumentsDesc.Visible = false;
							labelArguments.Visible = false;
						}
						else
						{
							labelArguments.Visible = true;
							labelArgumentsDesc.Visible = true;
							labelArguments.Text = sc.Arguments;
						}
					}
					else
					{
						// Get the file version information.
						FileVersionInfo selectedFileVersionInfo =
							FileVersionInfo.GetVersionInfo(filePath);

						// Display the file properties.
						labelCompany.Text = selectedFileVersionInfo.CompanyName;
						labelProductName.Text = selectedFileVersionInfo.ProductName;
						labelDescription.Text = selectedFileVersionInfo.FileDescription;
						labelFileVersion.Text = selectedFileVersionInfo.FileVersion;
						labelCommand.Text = command;

						// Only display arguments for shortcuts.
						labelArguments.Visible = false;
						labelArgumentsDesc.Visible = false;
						labelArguments.Text = "";
					}
				}
			}
			catch (Exception) { }

		}

		/// <summary>
		/// Set the ListViewItemSorter property to a new ListViewItemComparer 
		/// object. Setting this property immediately sorts the 
		/// ListView using the ListViewItemComparer object.
		/// </summary>
		void listviewStartup_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Toggle sort order.
			if (ascending == false)
			{
				ascending = true;
			}
			else
			{
				ascending = false;
			}

			// Perform sort of items in specified column.
			listviewStartup.ListViewItemSorter = new ListViewItemComparer(e.Column, ascending);

		}

		#endregion

		#region " Methods "

		void DisplayRegistryStartupEntries(string hive)
		{
			bool disabled;
			string command;
			string filePath;
			RegistryKey rk;

			if (hive == HKCU)
			{
				// Get all startup programs in HKEY_CURRENT_USER.
				rk = Registry.CurrentUser.OpenSubKey(runKey);
			}
			else
			{
				// Get all startup programs in HKEY_LOCAL_MACHINE.
				rk = Registry.LocalMachine.OpenSubKey(runKey);
			}

			// Get all of the entries.
			foreach (string value in rk.GetValueNames())
			{
				// Reset disabled flag.
				disabled = false;

				// Save complete command.
				command = rk.GetValue(value).ToString();

				// Check if command is disabled (begins with a ":")
				if (command.StartsWith(":"))
				{
					// Flag this entry as disabled.
					disabled = true;

					// Remove semicolon so that path command works and save file path.
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
					StartupImageList.Images.Add(native.GetIcon(filePath));

				}
				else
				{
					// If there is no icon, just add a blank image from resources to keep the indexes proper.
					StartupImageList.Images.Add((System.Drawing.Image)ResourceManager.GetObject("Blank"));
				}

				// Add entry description to listview.
				listviewStartup.Items.Add(value.ToString(), i);

				// Add file name (without path) to listview.
				listviewStartup.Items[i].SubItems.Add(Path.GetFileName(filePath));

				// Add location (type) information to listview.
				if (hive == HKCU)
				{
					listviewStartup.Items[i].SubItems.Add(HKCU);
				}
				else
				{
					listviewStartup.Items[i].SubItems.Add(HKLM);
				}

				// Add status information.
				if (disabled)
				{
					listviewStartup.Items[i].SubItems.Add(rm.GetString("startupprogs_disabled"));
				}
				else
				{
					listviewStartup.Items[i].SubItems.Add(rm.GetString("startupprogs_enabled"));
				}

				// Add command.
				listviewStartup.Items[i].SubItems.Add(command);

				// Add file path.
				listviewStartup.Items[i].SubItems.Add(filePath);

				i++;
			}

			// Close the registry key.
			rk.Close();
		}

		void DisplayStartupShortcuts(string type)
		{
			bool disabled;
			string command;
			string filePath;
			string folder;

			if (type == StartupUser)
			{
				// Current users startup folder.
				folder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
			}
			else
			{
				// All users startup folder.
				folder = Environment.ExpandEnvironmentVariables("%AllUsersProfile%") + @"\Start Menu\Programs\Startup";
			}
			try
			{

				foreach (string shortcut in Directory.GetFiles(folder, "*.*"))
				{
					// Only process shortcuts or executible files.
					if (Path.GetExtension(shortcut) == ".lnk" || Path.GetExtension(shortcut) == ".exe")
					{
						// Reset disabled flag.
						disabled = false;

						// Save complete command.
						command = shortcut;

						//if (Path.GetExtension(shortcut) == ".lnk")
						//  {
						//       ShortcutClass scut = new ShortcutClass(shortcut);
						//       command = scut.Path;
						//   }

						// Save file path.
						filePath = ReturnFilePath(command);

						// Resolve the shortcut.

						ShortcutClass sc = new ShortcutClass(filePath);

						// Attempt to get application image (icon).
						if (sc.Icon != null)
						{
							// First try getting icon from shortcut.
							StartupImageList.Images.Add(sc.Icon);
						}
						else if (native.GetIcon(sc.Path) != null)
						{
							// Then try getting icon from the resolved path.
							StartupImageList.Images.Add(native.GetIcon(sc.Path));
						}
						else
						{
							// If both methods fail, display a blank icon.
							StartupImageList.Images.Add((System.Drawing.Image)ResourceManager.GetObject("Blank"));
						}

						// Add entry description to listview.
						listviewStartup.Items.Add(Path.GetFileNameWithoutExtension(shortcut), i);

						// Add file name (without path) to listview.
						listviewStartup.Items[i].SubItems.Add(Path.GetFileName(filePath));

						// Add type information to listview.
						if (type == StartupUser)
						{
							listviewStartup.Items[i].SubItems.Add(StartupUser);
						}
						else
						{
							listviewStartup.Items[i].SubItems.Add(StartupAllUsers);
						}

						// Add status information.
						if (disabled)
						{
							listviewStartup.Items[i].SubItems.Add(rm.GetString("startupprogs_disabled"));
						}
						else
						{
							listviewStartup.Items[i].SubItems.Add(rm.GetString("startupprogs_enabled"));
						}

						// Add command.
						listviewStartup.Items[i].SubItems.Add(command);

						// Add file path.
						listviewStartup.Items[i].SubItems.Add(filePath);

						i++;

					}

				}
			}
			catch (Exception e)
			{
				string exp = e.Message;
				//Directory may not exist
			}

		}

		string ReturnFilePath(string value)
		{
			try
			{
				int p;

				// Check for quotes, and if present, remove them.
				if (value.Contains("\""))   // quote character 34, 22H
				{
					value = value.Replace("\"", "");
				}

				// Check for hyphens, and if present, return the part before first one.
				if (value.Contains("-"))
				{
					p = value.IndexOf("-");
					value = value.Substring(0, p - 1);
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


				value = value.ToLower();
				if (value[1] != ':')
				{
					int tmpStart = (value.IndexOf(':') - 1);
					if (tmpStart > 0)
					{
						string tmpString = value.Substring(tmpStart);
						int tmpEnd = tmpString.IndexOf('.') + 4;
						tmpString = tmpString.Substring(0, tmpEnd);
						return tmpString;
					}
					else
					{
						value = value.Substring(value.IndexOf(" "));
						value = value.Substring(0, value.IndexOf(".dll") + 4);
						return Environment.ExpandEnvironmentVariables("%SystemRoot%") + "\\system32\\" + value.TrimStart().TrimEnd();
					}
				}
				else if (value[1] == ':')
				{
					if (value.IndexOf(".lnk") > 0)
					{
						return value.Substring(0, value.IndexOf(".lnk") + 4);
						// Resolve the shortcut.
						//        ShortcutClass sc = new ShortcutClass(value.Substring(0, value.IndexOf(".lnk") + 4));
						//         string path = sc.Path;
						//        return path;
					}
					else
					{
						int tmpEnd = value.IndexOf(".exe") + 4;
						value = value.Substring(0, tmpEnd);
						return value;
					}
				}

				else if (!string.IsNullOrEmpty(value))
				{
					return Path.GetFullPath(value);
				}
				else
				{
					return "";
				}
			}
			catch (Exception)
			{
				return "";
			}

		}

		#endregion

        private void labelCompanyDesc_Click(object sender, EventArgs e)
        {

        }

        private void labelProductNameDesc_Click(object sender, EventArgs e)
        {

        }
	}
}
