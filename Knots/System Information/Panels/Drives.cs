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
using System.Resources;
using System.Windows.Forms;

namespace SystemInformation
{
	/// <summary>
	/// Drives panel of the System Information utility
	/// </summary>
	public partial class Drives : SystemInformation.TaskPanelBase
	{
		static Drives panelInstance;
		static InformationClass info = new InformationClass();

		static System.Collections.ObjectModel.Collection<string> VolumeLetter;
		static System.Collections.ObjectModel.Collection<string> VolumeType;
		static System.Collections.ObjectModel.Collection<string> _VolumeLabel;
		static System.Collections.ObjectModel.Collection<string> VolumeFileSystem;
		static System.Collections.ObjectModel.Collection<string> VolumeTotalSize;
		static System.Collections.ObjectModel.Collection<string> VolumeUsedSpace;
		static System.Collections.ObjectModel.Collection<string> VolumeFreeSpace;
		static System.Collections.ObjectModel.Collection<string> VolumePercentFreeSpace;
		static System.Collections.ObjectModel.Collection<bool> VolumeReady;
		static System.Collections.ObjectModel.Collection<string> DriveCapacity;
		static System.Collections.ObjectModel.Collection<string> DriveInterface;
		static System.Collections.ObjectModel.Collection<string> DriveModelNo;
		static System.Collections.ObjectModel.Collection<string> DriveStatus;
		static System.Collections.ObjectModel.Collection<string> CDDrive;
		static System.Collections.ObjectModel.Collection<string> CDMediaSize;
		static System.Collections.ObjectModel.Collection<string> CDModel;
		static System.Collections.ObjectModel.Collection<string> CDStatus;

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static Drives CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new Drives();

				// Get Information
				info.Initialize(InformationClass.Initializers.GetDriveInformation);
				info.Initialize(InformationClass.Initializers.GetVolumeInfo);
				VolumeLetter = info.VolumeLetter;
				VolumeType = info.VolumeType;
				_VolumeLabel = info.VolumeLabel;
				VolumeFileSystem = info.VolumeFileSystem;
				VolumeTotalSize = info.VolumeTotalSize;
				VolumeUsedSpace = info.VolumeUsedSpace;
				VolumeFreeSpace = info.VolumeFreeSpace;
				VolumePercentFreeSpace = info.VolumePercentFreeSpace;
				VolumeReady = info.VolumeReady;

				DriveCapacity = info.DriveCapacity;
				DriveInterface = info.DriveInterface;
				DriveModelNo = info.DriveModelNo;
				DriveStatus = info.DriveStatus;

				CDDrive = info.CDDrive;
				CDMediaSize = info.CDMediaSize;
				CDModel = info.CDModel;
				CDStatus = info.CDStatus;
			}
			return panelInstance;
		}

		#region " Drives Events "

		void Drives_Load(object sender, System.EventArgs e)
		{
			ResourceManager rm = new ResourceManager("SystemInformation.Resources", System.Reflection.Assembly.GetExecutingAssembly());

			this.labelTitle.Text = rm.GetString("node_drives");
			this.labelVolumes.Text = rm.GetString("drives_volumes");
			this.Drive.Text = rm.GetString("drives_drive");
			this.VolumeLabel.Text = rm.GetString("drives_volume_label");
			this.FileSystem.Text = rm.GetString("drives_filesystem_abbr");
			this.TotalSize.Text = rm.GetString("drives_tot_size");
			this.UsedSpace.Text = rm.GetString("drives_usedspace");
			this.FreeSpace.Text = rm.GetString("drives_freespace");
			this.PercentFree.Text = "% " + rm.GetString("drives_free");
			this.Ready.Text = rm.GetString("drives_ready_abbr");
			this.PhysDrive.Text = rm.GetString("drives_drive");
			this.Type.Text = rm.GetString("drives_type");
			this.Capacity.Text = rm.GetString("drives_capacity");
			this.ModelNumber.Text = rm.GetString("drives_model_num");
			this.Status.Text = rm.GetString("drives_status");
			this.labelPhysHD.Text = rm.GetString("drives_physical_drives");
			this.labelLegend.Text = rm.GetString("drives_filesystem_abbr") + " = " + rm.GetString("drives_filesystem") + ", " + rm.GetString("drives_ready_abbr") + " = " + rm.GetString("drives_ready");
			try
			{
				int X = 0;
				int Y = 0;
				int index = 0;

				Application.DoEvents();

				// Clear ListViews
				listviewVolumes.Items.Clear();
				listviewDrives.Items.Clear();

				// Fill Volume ListView
				if (VolumeLetter != null)
				{
					for (X = 0; X <= VolumeLetter.Count - 1; X++)
					{
						// Get image list index for drive type
						index = ReturnImageIndex(VolumeType[X]);
						listviewVolumes.Items.Add(VolumeLetter[X], index);

						if (_VolumeLabel[X] != null)
						{
							listviewVolumes.Items[X].SubItems
								.Add(_VolumeLabel[X]);
						}
						else
						{
							listviewVolumes.Items[X].SubItems.Add(rm.GetString("drives_ready_n/a"));
						}

						if (VolumeFileSystem[X] != null)
						{
							listviewVolumes.Items[X].SubItems
								.Add(VolumeFileSystem[X]);
						}
						else
						{
							listviewVolumes.Items[X].SubItems.Add(rm.GetString("drives_ready_n/a"));
						}

						if (VolumeTotalSize[X] != null)
						{
							listviewVolumes.Items[X].SubItems
								.Add(VolumeTotalSize[X]);
						}
						else
						{
							listviewVolumes.Items[X].SubItems.Add(rm.GetString("drives_ready_n/a"));
						}

						if (VolumeUsedSpace[X] != null)
						{
							listviewVolumes.Items[X].SubItems
								.Add(VolumeUsedSpace[X]);
						}
						else
						{
							listviewVolumes.Items[X].SubItems.Add(rm.GetString("drives_ready_n/a"));
						}

						if (VolumeFreeSpace[X] != null)
						{
							listviewVolumes.Items[X].SubItems
								.Add(VolumeFreeSpace[X]);
						}
						else
						{
							listviewVolumes.Items[X].SubItems.Add(rm.GetString("drives_ready_n/a"));
						}

						if (VolumePercentFreeSpace[X] != null)
						{
							listviewVolumes.Items[X].SubItems
								.Add(VolumePercentFreeSpace[X]);
						}
						else
						{
							listviewVolumes.Items[X].SubItems.Add(rm.GetString("drives_ready_n/a"));
						}

						if (VolumeReady[X])
						{
							listviewVolumes.Items[X].SubItems.Add(rm.GetString("drives_ready_y"));
						}
						else
						{
							listviewVolumes.Items[X].SubItems.Add(rm.GetString("drives_ready_n"));
						}

					}
				}
				else
				{
					listviewVolumes.Items.Add("");
					listviewVolumes.Items[0].SubItems.Add(rm.GetString("drives_volumes_none"));
				}

				// Add physical hard drive information
				if (DriveCapacity != null)
				{
					for (X = 0; X <= DriveCapacity.Count - 1; X++)
					{

						if (DriveInterface[X].ToString().ToUpper().Contains("USB"))
						{
							// If the drive model includes "USB" use USB image.
							listviewDrives.Items.Add(X.ToString(), 4);
						}
						else
						{
							// Otherwise use hard drive image.
							listviewDrives.Items.Add(X.ToString(), 1);
						}

						if (DriveInterface[X] != null)
						{
							listviewDrives.Items[X].SubItems.Add(DriveInterface[X]);
						}
						else
						{
							listviewDrives.Items[X].SubItems.Add(rm.GetString("drives_ready_n/a"));
						}

						if (DriveCapacity[X] != null)
						{
							listviewDrives.Items[X].SubItems.Add(DriveCapacity[X]);
						}
						else
						{
							listviewDrives.Items[X].SubItems.Add(rm.GetString("drives_ready_n/a"));
						}

						if (DriveModelNo[X] != null)
						{
							listviewDrives.Items[X].SubItems.Add(DriveModelNo[X]);
						}
						else
						{
							listviewDrives.Items[X].SubItems.Add(rm.GetString("drives_ready_n/a"));
						}

						if (DriveStatus[X] != null)
						{
							listviewDrives.Items[X].SubItems.Add(DriveStatus[X]);
						}
						else
						{
							listviewDrives.Items[X].SubItems.Add(rm.GetString("drives_ready_n/a"));
						}

					}
				}
				else
				{
					listviewDrives.Items.Add("");
					listviewDrives.Items[0].SubItems.Add(rm.GetString("drives_notavailable"));
				}

				// Add CDROM drive information to the end of hard drives
				if (CDDrive != null)
				{
					for (Y = 0; Y <= CDDrive.Count - 1; Y++)
					{

						listviewDrives.Items.Add(Convert.ToString(Y + X), 3);

						// Drive type (interface) is undefined for CD drives.
						listviewDrives.Items[Y + X].SubItems.Add("");

						if (CDMediaSize[Y] != null)
						{
							listviewDrives.Items[Y + X].SubItems.Add(CDMediaSize[Y]);
						}
						else
						{
							listviewDrives.Items[Y + X].SubItems.Add(rm.GetString("drives_ready_n/a"));
						}

						if (CDModel[Y] != null)
						//if (info.CDModel != null)
						{
							listviewDrives.Items[Y + X].SubItems.Add(CDModel[Y]);
						}
						else
						{
							listviewDrives.Items[Y + X].SubItems.Add(rm.GetString("drives_ready_n/a"));
						}

						if (CDStatus[Y] != null)
						{
							listviewDrives.Items[Y + X].SubItems.Add(CDStatus[Y]);
						}
						else
						{
							listviewDrives.Items[Y + X].SubItems.Add(rm.GetString("drives_ready_n/a"));
						}

					}
				}
			}
			catch { }
		}

		#endregion

		#region " Methods "

		static int ReturnImageIndex(string strDriveType)
		{
			int retValue;

			switch (strDriveType.ToLower())
			{
				case "removable":
					retValue = 0;
					break;
				case "fixed":
					retValue = 1;
					break;
				case "network":
					retValue = 2;
					break;
				case "cdrom":
					retValue = 3;
					break;
				case "usb":
					retValue = 4;
					break;
				default:
					retValue = 5;
					break;
			}

			return retValue;
		}

		#endregion

	}

}
