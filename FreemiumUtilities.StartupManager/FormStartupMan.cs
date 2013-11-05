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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
		//List<string> lstOfAppsToDelete;

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
		bool Is64Bit; // True if 64-Bit OS.
		int i; // Counter for registry startup entries.

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

		#region Main Tool Strip Menu Events

		#endregion

		#region Context Tool Strip Menu Events

		#endregion

		#region Tool Strip Button Events

		#endregion

		#region Listview Methods

		/// <summary>
		/// Fills the ListView with an actual startup items
		/// </summary>
		public void FillListview()
		{
			// Clear image list and listview.
			listviewStartup.Items.Clear();
			//imageListStartupManager.Images.Clear();
			i = 0;

			//Clear list of problems
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

		#region

		//string currentScanApp = "";

		//public string CurrentScanApp
		//{
		//    get { return this.currentScanApp; }
		//    set
		//    {
		//        this.currentScanApp = value;
		//        NotifyPropertyChanged("CurrentScanApp");
		//    }
		//}

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
				//lstProblems.Add(item);
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

		#endregion

		/// <summary>
		/// constructor for frmStartupMan
		/// </summary>
		public FrmStartupMan()
		{
			InitializeComponent();
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
				//lstProblems.Add(item);
				if (app == item.SubItems[1].Text)
				{
					DeleteItem(index);
					return;
				}
				index++;
			}
		}

		/// <summary>
		/// Gets the <c>Bitmap</c> from the passed <paramref name="icon"/>
		/// </summary>
		/// <param name="icon"><c>Icon</c> to get <c>Bitmap</c> from</param>
		/// <returns><c>Bitmap</c> from the passed <paramref name="icon"/></returns>
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
					foreach (string value in rk.GetValueNames())
					{
						// Reset disabled flag.
						var disabled = false;

						// Save complete command.
						string command = rk.GetValue(value).ToString();

						// Check if command is valid
						if (command.Length < 1)
						{
							continue;
						}

						// Check if command is disabled (begins with a ":")
						string filePath;
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

						//// Attempt to get application image (icon).
						//if (native.GetIcon(filePath) != null)
						//{
						//    // Add the icon to the image list so that the listview can access it.
						//    //imageListStartupManager.Images.Add(native.GetIcon(filePath));
						//}
						//else
						//{
						//    // If there is no icon, just add a blank image from resources to keep the indexes proper.
						//    //imageListStartupManager.Images.Add((System.Drawing.Image)ResourceManager.GetObject("Blank"));
						//}

						// Add entry description to listview.
						listviewStartup.Items.Add(value, i);

						// Add file name (without path) to listview.
						listviewStartup.Items[i].SubItems.Add(Path.GetFileName(filePath));

						// Add location (type) information to listview.
						if (hive == HKCU)
						{
							listviewStartup.Items[i].SubItems.Add(HKCU);
						}
						else if (hive == HKLM)
						{
							listviewStartup.Items[i].SubItems.Add(HKLM);
						}
						else if (hive == WHKCU)
						{
							listviewStartup.Items[i].SubItems.Add(WHKCU);
						}
						else if (hive == WHKLM)
						{
							listviewStartup.Items[i].SubItems.Add(WHKLM);
						}

						// Add status information.
						listviewStartup.Items[i].SubItems.Add(disabled ? "Disabled" : "Enabled");

						// Add command.
						listviewStartup.Items[i].SubItems.Add(command);

						// Add file path.
						listviewStartup.Items[i].SubItems.Add(filePath);
						//}
						i++;
					}

					// Close the registry key.
					rk.Close();
				}
			}
			catch (NullReferenceException)
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
					// Only process shortcuts, executibles, or disabled shortcuts.
					if (Path.GetExtension(shortcut) == ".lnk" || Path.GetExtension(shortcut) == ".exe"
						|| Path.GetExtension(shortcut) == ".disabled")
					{
						// Set diabled flag.
						bool disabled = Path.GetExtension(shortcut) == ".disabled";

						// Save complete command.
						string command = shortcut;

						// Save file path.
						string filePath = ReturnFilePath(command);

						// Resolve the shortcut.
						//var sc = new ShortcutClass(filePath);

						//// Attempt to get application image (icon).
						//if (sc.Icon != null)
						//{
						//    // First try getting icon from shortcut.
						//    //imageListStartupManager.Images.Add(sc.Icon);
						//}
						//else if (native.GetIcon(sc.Path) != null)
						//{
						//    // Then try getting icon from the resolved path.
						//    //imageListStartupManager.Images.Add(native.GetIcon(sc.Path));
						//}
						//else
						//{
						//    // If both methods fail, display a blank icon.
						//    //imageListStartupManager.Images.Add((System.Drawing.Image)ResourceManager.GetObject("Blank"));
						//}

						// Add entry description to listview.
						listviewStartup.Items.Add(Path.GetFileNameWithoutExtension(shortcut), i);

						// Add file name (without path) to listview.
						listviewStartup.Items[i].SubItems.Add(Path.GetFileName(filePath));

						// Add type information to listview.
						listviewStartup.Items[i].SubItems.Add(type == StartupCurrentUser ? StartupCurrentUser : StartupAllUsers);

						// Add status information.
						listviewStartup.Items[i].SubItems.Add(disabled ? "Disabled" : "Enabled");

						// Add command.
						listviewStartup.Items[i].SubItems.Add(command);

						// Add file path.
						listviewStartup.Items[i].SubItems.Add(filePath);

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
				definedPath = definedPath.Replace("\"", "");

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
					definedPath = "";
				}
			}
			catch (ArgumentException)
			{
				return "";
			}

			return definedPath;
		}

		#endregion

		#region Return FilePath Method

		string ReturnFilePath(string value)
		{
			try
			{
				int p;

				// Check for quotes, and if present, remove them.
				if (value.Contains("\"")) // quote character 34, 22H
				{
					value = value.Replace("\"", "");
				}

				// Check for brackets, and if present, remove them.
				if (value.Contains("["))
				{
					value = value.Replace("[", "");
				}

				if (value.Contains("]"))
				{
					value = value.Replace("]", "");
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
					return "";
				}
				if (!string.IsNullOrEmpty(value))
				{
					return Path.GetFullPath(value);
				}
				return "";
			}
			catch (DirectoryNotFoundException e)
			{
				MessageBox.Show("The folder was not found." + "\r\n" +
								"Description: " + e.Message + "\r\n" + "Command: " + value,
								Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return "";
			}
			catch (FileNotFoundException ex)
			{
				MessageBox.Show("The file was not found." + "\r\n" +
								"Description: " + ex.Message + "\r\n" + "Command: " + value,
								Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return "";
			}
		}

		#endregion

		#region Delete Item Method

		void DeleteItem(int index)
		{
			RegistryKey rk = null;

			try
			{
				// Get type (location) of startup item.
				string fileName;
				switch (listviewStartup.Items[index].SubItems[(int)ListCol.Type].Text)
				{
					case HKCU:

						// Open "Run" key in HKEY_CURRENT_USER.
						rk = Registry.CurrentUser.OpenSubKey(RunKey, true);

						// Attempt to delete the value.
						if (rk != null) rk.DeleteValue(listviewStartup.Items[index].Text);

						// Remove item from listview.
						listviewStartup.Items[index].Remove();

						// Clear the details labels.
						labelArguments.Text = "";
						labelCommand.Text = "";
						labelCompany.Text = "";
						labelDescription.Text = "";
						labelFileVersion.Text = "";
						labelProductName.Text = "";

						break;

					case WHKCU:

						// Open "Run" key in HKEY_CURRENT_USER\Wow6432Node.
						rk = Registry.CurrentUser.OpenSubKey(WowRunKey, true);

						// Attempt to delete the value.
						rk.DeleteValue(listviewStartup.Items[index].Text);

						// Remove item from listview.
						listviewStartup.Items[index].Remove();

						// Clear the details labels.
						labelArguments.Text = "";
						labelCommand.Text = "";
						labelCompany.Text = "";
						labelDescription.Text = "";
						labelFileVersion.Text = "";
						labelProductName.Text = "";

						break;

					case HKLM:

						// Open "Run" key in HKEY_LOCAL_MACHINE.
						rk = Registry.LocalMachine.OpenSubKey(RunKey, true);

						// Attempt to delete the value.
						rk.DeleteValue(listviewStartup.Items[index].Text);

						// Remove item from listview.
						listviewStartup.Items[index].Remove();

						// Clear the details labels.
						labelArguments.Text = "";
						labelCommand.Text = "";
						labelCompany.Text = "";
						labelDescription.Text = "";
						labelFileVersion.Text = "";
						labelProductName.Text = "";

						break;

					case WHKLM:

						// Open "Run" key in HKEY_LOCAL_MACHINE\Wow6432Node.
						rk = Registry.LocalMachine.OpenSubKey(WowRunKey, true);

						// Attempt to delete the value.
						if (rk != null) rk.DeleteValue(listviewStartup.Items[index].Text);

						// Remove item from listview.
						listviewStartup.Items[index].Remove();

						// Clear the details labels.
						labelArguments.Text = "";
						labelCommand.Text = "";
						labelCompany.Text = "";
						labelDescription.Text = "";
						labelFileVersion.Text = "";
						labelProductName.Text = "";

						break;

					case StartupCurrentUser:

						// Get the file name.
						fileName = listviewStartup.Items[index].SubItems[(int)ListCol.Path].Text;

						// Make sure file exists.
						if (File.Exists(fileName))
						{
							// Remove attributes.
							File.SetAttributes(fileName, FileAttributes.Normal);

							// Delete file.
							File.Delete(fileName);

							// Remove item from listview.
							listviewStartup.Items[index].Remove();

							// Clear the details labels.
							labelArguments.Text = "";
							labelCommand.Text = "";
							labelCompany.Text = "";
							labelDescription.Text = "";
							labelFileVersion.Text = "";
							labelProductName.Text = "";
						}
						else
						{
							MessageBox.Show("The file was not found.", Application.ProductName,
											MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}

						break;

					case StartupAllUsers:

						// Get the file name.
						fileName = listviewStartup.Items[index].SubItems[(int)ListCol.Path].Text;

						// Make sure file exists.
						if (File.Exists(fileName))
						{
							// Remove attributes.
							File.SetAttributes(fileName, FileAttributes.Normal);

							// Delete file.
							File.Delete(fileName);

							// Remove item from listview.
							listviewStartup.Items[index].Remove();

							// Clear the details labels.
							labelArguments.Text = "";
							labelCommand.Text = "";
							labelCompany.Text = "";
							labelDescription.Text = "";
							labelFileVersion.Text = "";
							labelProductName.Text = "";
						}
						else
						{
							MessageBox.Show("The file was not found.", Application.ProductName,
											MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}

						break;
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
			finally
			{
				// Close registry key.
				if (rk != null)
				{
					rk.Close();
				}
			}
		}

		#endregion
	}
}