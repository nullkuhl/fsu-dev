#define _MyType
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
using System.IO;
using System.Security;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Win32;
using System.Resources;

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


namespace SystemInformation
{
	/// <summary>
	/// Installed programs panel of the System Information utility
	/// </summary>
	public partial class InstalledPrograms : SystemInformation.TaskPanelBase
	{
		static InstalledPrograms panelInstance;
		System.Resources.ResourceManager ResourceManager = new System.Resources.ResourceManager("SystemInformation.Properties.Resources", typeof(InstalledPrograms).Assembly);
		NativeMethods native = new NativeMethods();

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static InstalledPrograms CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new InstalledPrograms();
			}
			return panelInstance;
		}

		#region " Constants and Variables "

		const string uninstallKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
		const int maxPath = 255;
		bool ascending; // Used to toggle listview sorting.

		#endregion

		#region " InstalledPrograms Events "

		void InstalledPrograms_Load(System.Object sender, System.EventArgs e)
		{
			ResourceManager rm = new ResourceManager("SystemInformation.Resources", System.Reflection.Assembly.GetExecutingAssembly());

			this.labelTitle.Text = rm.GetString("node_installedprogs");
			this.DisplayName.Text = rm.GetString("installedprogs_name");
			this.Publisher.Text = rm.GetString("installedprogs_publisher");
			this.DisplayVersion.Text = rm.GetString("installedprogs_version");
			this.HelpLink.Text = rm.GetString("installedprogs_helplink");
			this.HelpTelephone.Text = rm.GetString("installedprogs_helptelephone");
			this.InstallDate.Text = rm.GetString("installedprogs_installdate");
			this.EstimatedSize.Text = rm.GetString("installedprogs_estimatesize");
			this.IconIndex.Text = rm.GetString("installedprogs_iconindex");
			this.labelInstalledProgramsDescription.Text = rm.GetString("installedprogs_description") + ".";
			this.labelDetails.Text = rm.GetString("installedprogs_details");
			this.labelHelpTelephoneDesc.Text = rm.GetString("installedprogs_helptelephone") + ":";
			this.labelHelpLinkDesc.Text = rm.GetString("installedprogs_helplink") + ":";
			this.labelInstallDateDesc.Text = rm.GetString("installedprogs_installdate") + ":";
			this.labelEstSizeDesc.Text = rm.GetString("installedprogs_estimatesize") + ":";
			this.labelDisplayVersionDesc.Text = rm.GetString("installedprogs_version") + ":";

			RegistryKey rk = null;
			RegistryKey subKey = null;
			int count = 0;                          // Count of installed programs.
			string subKeyName;
			string[] valueNames = new string[101];  // It is very, very unlikely that there will be 100 values.
			Icon appIcon;                           // Temporary variable for application icon.
			string iconPath;                        // Path to application icon.
			int iconIndex;                          // Index to application icon in a dll or exe.
			bool iconFound;                         // Indicates icon for application has been found.
			string folder;                          // Temporary variable for folders.

			// Installed program data.
			string displayName;
			string publisher;
			string displayVersion;
			string helpLink;
			string helpTelephone;
			string installDate;
			string estimatedSize;

			// Programmer's comment: Wouldn't it be nice if everyone used the same installation standard?

			Application.DoEvents();

			try
			{

				// Image list and listview.
				listviewPrograms.Items.Clear();
				SmallImageList.Images.Clear();

				// Open the uninstall key.
				rk = Registry.LocalMachine.OpenSubKey(uninstallKey, false);

				// Get all installed programs.
				foreach (string tempLoopVar_subKeyName in rk.GetSubKeyNames())
				{
					subKeyName = tempLoopVar_subKeyName;

					// Clear the system array holding the value names.
					//Array.Clear(valueNames, 0, valueNames.Length);

					// Open the sub key.
					subKey = Registry.LocalMachine.OpenSubKey(uninstallKey + "\\" + subKeyName, false);

					// Iterate through all of the values
					if (subKey.ValueCount != 0)
					{

						// Save the value names in a system array.
						valueNames = subKey.GetValueNames();

						// Only display entries that are not system components.
						//if (Array.IndexOf(valueNames, "SystemComponent") == - 1)
						//{

						// Only add values that have a display name.
						//if (Array.IndexOf(valueNames, "DisplayName") > 0)
						try
						{

							// Set iconFound to false since we have not found an icon for this item.
							iconFound = false;

							// Get the display name.
							displayName = subKey.GetValue("DisplayName").ToString();
							if (displayName.Contains("(KB"))
								continue;
							// Get the publisher.
							if (Array.IndexOf(valueNames, "Publisher") > 0)
							{
								publisher = subKey.GetValue("Publisher").ToString();
							}
							else
							{
								publisher = "";
							}

							// Get the display version.
							if (Array.IndexOf(valueNames, "DisplayVersion") > 0)
							{
								displayVersion = subKey.GetValue("DisplayVersion").ToString();
							}
							else
							{
								displayVersion = "";
							}

							// Get the help link.
							if (Array.IndexOf(valueNames, "HelpLink") > 0)
							{
								helpLink = subKey.GetValue("HelpLink").ToString();
							}
							else
							{
								helpLink = "";
							}

							// Get the help telephone.
							if (Array.IndexOf(valueNames, "HelpTelephone") > 0)
							{
								helpTelephone = subKey.GetValue("HelpTelephone").ToString();
							}
							else
							{
								helpTelephone = "";
							}

							// Get the install desiredDate.
							if (Array.IndexOf(valueNames, "InstallDate") > 0)
							{
								installDate = subKey.GetValue("InstallDate").ToString();

								if (!string.IsNullOrEmpty(installDate) && !(installDate.Contains(",") || installDate.Contains("/")))
								{
									// If the install desiredDate does not contains "/" or ",",
									// assume that the desiredDate format is YYYYMMDD.
									installDate = string.Format("{0:D}",
										System.Convert.ToDateTime(installDate.Substring(4, 2) +
										"/" + installDate.Substring(6, 2) + "/" + installDate.Substring(0, 4)));
									// Remove the day of the week.
									installDate = installDate.Remove(0, installDate.IndexOf(",") + 2);
								}
								else if (installDate.Contains("/"))
								{
									// Just format as long desiredDate.
									installDate = string.Format("{0:D}", System.Convert.ToDateTime(installDate));
									// Remove the day of the week.
									installDate = installDate.Remove(0, installDate.IndexOf(",") + 2);
								}

							}
							else
							{
								installDate = "";
							}

							// Get the estimated size.
							if (Array.IndexOf(valueNames, "EstimatedSize") > 0)
							{
								// EstimateSize is a DWORD, so it needs to be formatted as megabytes.
								estimatedSize = string.Format("{0:N2}",
									System.Convert.ToDouble(subKey.GetValue("EstimatedSize")) / 1024) + " MB";
							}
							else
							{
								estimatedSize = "";
							}

							// If there is a display icon, and add to image list.
							if (Array.IndexOf(valueNames, "DisplayIcon") > 0)
							{

								iconPath = subKey.GetValue("DisplayIcon").ToString();

								if (string.IsNullOrEmpty(iconPath))
								{

									iconFound = false;

								}
								else if (iconPath.Contains(","))
								{

									// Check if DisplayIcon contain an icon indes.
									iconIndex = System.Convert.ToInt32(iconPath.Substring(iconPath.IndexOf(",") + 1, 1));
									iconPath = iconPath.Substring(0, iconPath.IndexOf(",") - 1);

									// Get the icon.
									appIcon = native.GetIcon(iconPath, iconIndex);

									if (appIcon != null)
									{
										// Add the icon to the both the small and image lists.
										AddIcon(appIcon);
										iconFound = true;
									}
								}
								else
								{
									// Get the icon.
									appIcon = native.GetIcon(iconPath);

									if (appIcon != null)
									{
										// Add the icon to the both the small and image lists.
										AddIcon(appIcon);
										iconFound = true;
									}
								}

							} // If there is a display icon, and add to image list.

							// First try searching in Program Files for the Publisher\Display HolidayName.
							folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
								+ "\\" + publisher.Trim(null) + "\\" + displayName.Trim(null);

							if (iconFound == false)
							{
								// Look for executible files first.
								iconFound = SearchFolder(folder, "*.exe", true);
							}

							if (iconFound == false)
							{
								// Look for icon files next.
								iconFound = SearchFolder(folder, "*.ico", true);
							}

							// Next try searching in Program Files for the  just the Display HolidayName.
							folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
								+ "\\" + displayName.Trim(null);

							if (iconFound == false)
							{
								// Look for executible files first.
								iconFound = SearchFolder(folder, "*.exe", true);
							}

							if (iconFound == false)
							{
								// Look for icon files next.
								iconFound = SearchFolder(folder, "*.ico", true);
							}

							// Check uninstall key that begin with "{" because icons for
							// Windows Installer installations may be stored in WINDOWS\Installer\KeyName.
							if (subKeyName.StartsWith("{") && iconFound == false)
							{

								folder = Environment.GetEnvironmentVariable("windir") + "\\Installer\\"
									+ subKeyName.Trim(null);

								if (Directory.Exists(folder))
								{
									if (iconFound == false)
									{
										// Look for an executible file first.
										iconFound = SearchFolder(folder, "*.exe", false);
									}

									if (iconFound == false)
									{
										// Look for an an icon file next.
										iconFound = SearchFolder(folder, "*.ico", false);
									}

									if (iconFound == false)
									{
										// Look for an an file that contains "ArpIco" next.
										iconFound = SearchFolder(folder, "*ArpIco*", false);
									}

								} // Check uninstall key that begin with "{".
							}

							// If no icon was found, add a blank icon.
							if (iconFound == false)
							{
								AddBlankIcon();
							}

							// Add the display name and icon, if present, to the listview.
							listviewPrograms.Items.Add(displayName, count);

							// Add the other entries as subitems.
							listviewPrograms.Items[count].SubItems.Add(publisher);
							listviewPrograms.Items[count].SubItems.Add(displayVersion);
							listviewPrograms.Items[count].SubItems.Add(helpLink);
							listviewPrograms.Items[count].SubItems.Add(helpTelephone);
							listviewPrograms.Items[count].SubItems.Add(installDate);
							listviewPrograms.Items[count].SubItems.Add(estimatedSize);
							listviewPrograms.Items[count].SubItems.Add(count.ToString()); // Icon Number.

							// Bump count of programs.
							count++;

						} // Only add values that have a display name.
						catch { }
						//} // Only display entries that are not system components.
					} // Iterate through all of the values
                    // Process UI events.
                    Application.DoEvents();
				} // Get all installed programs.

				// Close the sub key.
				subKey.Close();

				// Display total items.
				labelNumberPrograms.Text = (count - 1).ToString() + " " + rm.GetString("installedprogs_items");

				// Sort the listview.
				listviewPrograms.Sorting = SortOrder.Ascending;
				listviewPrograms.Sort();
			}
			catch
			{

			}
			finally
			{

				if (rk != null)
				{
					// Close the registry key.
					rk.Close();
				}

				if (subKey != null)
				{
					//Close the registry key.
					subKey.Close();
				}
			}

		}

		#endregion

		#region " ListView Events "

		void listviewStartup_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{

			try
			{

				if (e.IsSelected)
				{

					// Get the index of the selected item in the listview.
					int i = e.ItemIndex;

					// All of the information is stored in the listview.
					// Some items have 0 width, so they do not display.
					labelDisplayName.Text = listviewPrograms.Items[i].Text;

					// Hide values if they are not available.
					if (string.IsNullOrEmpty(listviewPrograms.Items[i].SubItems[2].Text.Trim(null)))
					{
						labelDisplayVersionDesc.Visible = false;
						labelDisplayVersion.Visible = false;
						labelDisplayVersion.Text = "";
					}
					else
					{
						labelDisplayVersionDesc.Visible = true;
						labelDisplayVersion.Visible = true;
						labelDisplayVersion.Text = listviewPrograms.Items[i].SubItems[2].Text;
					}

					if (string.IsNullOrEmpty(listviewPrograms.Items[i].SubItems[3].Text.Trim(null)))
					{
						labelHelpLinkDesc.Visible = false;
						labelHelpLink.Visible = false;
						labelHelpLink.Text = "";
					}
					else
					{
						labelHelpLinkDesc.Visible = true;
						labelHelpLink.Visible = true;
						labelHelpLink.Text = listviewPrograms.Items[i].SubItems[3].Text;
					}

					if (string.IsNullOrEmpty(listviewPrograms.Items[i].SubItems[4].Text.Trim(null)))
					{
						labelHelpTelephoneDesc.Visible = false;
						labelHelpTelephone.Visible = false;
						labelHelpTelephone.Text = "";
					}
					else
					{
						labelHelpTelephoneDesc.Visible = true;
						labelHelpTelephone.Visible = true;
						labelHelpTelephone.Text = listviewPrograms.Items[i].SubItems[4].Text;
					}

					if (string.IsNullOrEmpty(listviewPrograms.Items[i].SubItems[5].Text.Trim(null)))
					{
						labelInstallDateDesc.Visible = false;
						labelInstallDate.Visible = false;
						labelInstallDate.Text = "";
					}
					else
					{
						labelInstallDateDesc.Visible = true;
						labelInstallDate.Visible = true;
						labelInstallDate.Text = listviewPrograms.Items[i].SubItems[5].Text;
					}

					if (string.IsNullOrEmpty(listviewPrograms.Items[i].SubItems[6].Text.Trim(null)))
					{
						labelEstSizeDesc.Visible = false;
						labelEstimatedSize.Visible = false;
						labelEstimatedSize.Text = "";
					}
					else
					{
						labelEstSizeDesc.Visible = true;
						labelEstimatedSize.Visible = true;
						labelEstimatedSize.Text = listviewPrograms.Items[i].SubItems[6].Text;
					}

					// Display the picture.
					pictureProgram.BackgroundImageLayout = ImageLayout.Stretch;
					pictureProgram.BackgroundImage =
						LargeImageList.Images[System.Convert.ToInt32(listviewPrograms.Items[i].SubItems[7].Text)];

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
			listviewPrograms.ListViewItemSorter = new ListViewItemComparer(e.Column, ascending);

		}

		#endregion

		#region " Methods "

		/// <summary>
		/// Adds a icon to both image lists.
		/// </summary>
		/// <param name="newIcon">Icon to be added.</param>
		void AddIcon(Icon newIcon)
		{

			// Add the icon to the small image list.
			SmallImageList.Images.Add(newIcon);

			// Also add the icon to the large image list.
			LargeImageList.Images.Add(newIcon);

		}

		/// <summary>
		/// Adds a blank icon to both image lists.
		/// </summary>
		void AddBlankIcon()
		{

			// Add a blank icon to the small image list.
			SmallImageList.Images.Add((System.Drawing.Image)ResourceManager.GetObject("Blank"));

			// Add a blank icon to the large image list.
			LargeImageList.Images.Add((System.Drawing.Image)ResourceManager.GetObject("Blank"));

		}

		/// <summary>
		/// Search folder using search pattern. If icon is found, it is added to both image lists.
		/// </summary>
		/// <param name="folder">Folder to be searched.</param>
		/// <param name="searchPattern">Matching pattern</param>
		/// <param name="searchSubDirs">If true subdirectories are also searched.</param>
		/// <returns>Return true if icon is found.</returns>
		bool SearchFolder(string folder, string searchPattern, bool searchSubDirs)
		{

			Icon appIcon;
			bool found = false;
			SearchOption searchOpt;

			// Change a boolean method parameter to a SearchOption.
			if (searchSubDirs)
			{
				searchOpt = SearchOption.AllDirectories;
			}
			else
			{
				searchOpt = SearchOption.TopDirectoryOnly;
			}

			// Verify that the folder exists.
			if (Directory.Exists(folder))
			{

				foreach (string iconPath in Directory.GetFiles(folder, searchPattern, searchOpt))
				{

					if (iconPath.Contains("Adobe.ico") || iconPath.Contains("GAC.exe"))
					{
						// Skip these files that are common on my computer. Add additional ones as you desire.
					}
					else if (iconPath.Length <= maxPath && File.Exists(iconPath))
					{
						// Check to make sure the file is a valid icon.
						appIcon = native.GetIcon(iconPath);
						if (appIcon != null)
						{
							AddIcon(appIcon);
							found = true;
							break;              // This break must be here for this to work.
						}
						else
						{
							found = false;
						}
					}
				}

			}
			else
			{
				// If directory is not valid, return false.
				found = false;
			}

			// Return result
			return found;

		}

		#endregion

		#region " Link Label Events "

		void labelHelpLink_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{

			// Open the link in the default browser.
			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo(labelHelpLink.Text);
				startInfo.WindowStyle = ProcessWindowStyle.Normal;
				Process.Start(startInfo);
			}
			catch
			{
				// cannot find file
				MessageBox.Show(rm.GetString("installedprogs_unable_to_open_website"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

		}

		#endregion

	}

}
