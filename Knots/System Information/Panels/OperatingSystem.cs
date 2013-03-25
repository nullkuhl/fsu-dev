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
	/// OS panel of the System Information utility
	/// </summary>
	public partial class OperatingSystem : SystemInformation.TaskPanelBase
	{
		static OperatingSystem panelInstance;
		static InformationClass info = new InformationClass();
		System.Resources.ResourceManager ResourceManager = new System.Resources.ResourceManager("SystemInformation.Properties.Resources", typeof(OperatingSystem).Assembly);

		static string OSShortVersion;
		static PlatformID OSPlatform;
		static string OSFullName;
		static string OSVersion;
		static string OSServicePack;
		static string OSType;
		static string OSCodeName;
		static string OSMachineName;
		static string OSUserName;
		static bool UserIsAdministrator;
		static string OSProductID;
		static string OSProductKey;
		static DateTime OSInstallDate;
		static string FrameworkShortVersion;
		static string FrameworkVersion;
		static string FrameworkServicePack;

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static OperatingSystem CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new OperatingSystem();

				OSShortVersion = info.OSShortVersion;
				OSPlatform = info.OSPlatform;
				OSFullName = info.OSFullName;
				OSVersion = info.OSVersion;
				OSServicePack = info.OSServicePack;
				OSType = info.OSType;
				OSCodeName = info.OSCodeName;
				OSMachineName = info.OSMachineName;
				OSUserName = info.OSUserName;
				UserIsAdministrator = info.UserIsAdministrator;
				OSProductID = info.OSProductID;
				OSProductKey = info.OSProductKey;
				OSInstallDate = info.OSInstallDate;
				FrameworkShortVersion = info.FrameworkShortVersion;
				FrameworkVersion = info.FrameworkVersion;
				FrameworkServicePack = info.FrameworkServicePack;
			}
			return panelInstance;
		}

		#region " Operating System Events "

		void OperatingSystem_Load(object sender, System.EventArgs e)
		{
			ResourceManager rm = new ResourceManager("SystemInformation.Resources", System.Reflection.Assembly.GetExecutingAssembly());

			this.labelTitle.Text = rm.GetString("node_os");
			this.labelCodeName.Text = rm.GetString("os_code_name") + ":";
			this.labelVersion.Text = rm.GetString("os_version") + ":";
			this.labelFullName.Text = rm.GetString("os_name") + ":";
			this.labelWindows.Text = rm.GetString("os_windows_info");
			this.labelMachineName.Text = rm.GetString("os_machine_name") + ":";
			this.labelUserName.Text = rm.GetString("os_user_name") + ":";
			this.labelFullVersion.Text = rm.GetString("os_full_version") + ":";
			this.labelFrameworkServicePack.Text = rm.GetString("os_service_pack") + ":";
			this.labelFrameworkFullVersion.Text = rm.GetString("os_framework_full_version") + ":";
			this.labelFrameworkShortVersion.Text = rm.GetString("os_framework_short_version") + ":";
			this.labelFramework.Text = rm.GetString("os_net_framework_info");
			this.labelType.Text = rm.GetString("os_type") + ":";
			this.labelProductID.Text = rm.GetString("os_product_id") + ":";
			this.labelProductKey.Text = rm.GetString("os_product_key") + ":";
			this.labelInstallDate.Text = rm.GetString("os_install_date") + ":";
			this.labelServicePack.Text = rm.GetString("os_service_pack") + ":";
			try
			{
				Application.DoEvents();

				string version;

				// Set operating system picture
				switch (OSShortVersion)
				{
					case "5.0": // Windows 2000
						picturePanel.Image = (System.Drawing.Image)ResourceManager.GetObject("Windows2000_48x48");
						break;
					case "5.1": //Windows XP
						picturePanel.Image = (System.Drawing.Image)ResourceManager.GetObject("Windows_XP_48x48");
						break;
					case "6.0": // Windows Vista
						picturePanel.Image = (System.Drawing.Image)ResourceManager.GetObject("Windows_Vista_48x48");
						break;
					default:
						picturePanel.Image = (System.Drawing.Image)ResourceManager.GetObject("Windows_48x48");
						break;
				}

				// Build long version string
				switch (OSPlatform)
				{
					case System.PlatformID.Win32NT:
						version = rm.GetString("os_ms_win_nt") + " " + info.OSShortVersion + " " + rm.GetString("os_build") + " " + info.OSBuild + " " + info.OSServicePack;
						break;
					case System.PlatformID.Win32Windows:
						version = rm.GetString("os_ms_win") + " " + info.OSShortVersion;
						break;
					case System.PlatformID.Unix:
						version = rm.GetString("os_unix") + " " + info.OSShortVersion;
						break;
					case PlatformID.Win32S:
						version = rm.GetString("os_win_32s") + " " + "3.1";
						break;
					case PlatformID.WinCE:
						version = rm.GetString("os_ms_win_ce") + " " + info.OSShortVersion + " Build " + info.OSBuild + " " + info.OSServicePack;
						break;
					default:
						version = rm.GetString("os_unknown_os");
						break;
				}

				// Fill in Windows information.
				textboxFullName.Text = OSFullName;
				textboxVersion.Text = version;
				textboxFullVersion.Text = OSVersion;

				// Hide Service Pack if not present.
				if (String.IsNullOrEmpty(OSServicePack))
				{
					labelServicePack.Visible = false;
					textboxServicePack.Text = "";
				}
				else
				{
					labelServicePack.Visible = true;
					textboxServicePack.Text = OSServicePack;
				}

				textboxType.Text = OSType;
				textboxCodeName.Text = OSCodeName;
				textboxMachineName.Text = OSMachineName;
				textboxUserName.Text = OSUserName;

				// Indicate if user is admin or limited.
				if (UserIsAdministrator)
				{
					textboxUserName.Text = textboxUserName.Text + " (" + rm.GetString("os_admin") + ")";
				}
				else
				{
					textboxUserName.Text = textboxUserName.Text + " (" + rm.GetString("os_limited_user") + ")";
				}

				// Fill in product ID and Key.
				textboxProductID.Text = OSProductID;
				textboxProductKey.Text = OSProductKey;

				// Fill in install desiredDate and time.
				if (OSInstallDate != DateTime.Today)
				{
					textboxInstallDate.Text = OSInstallDate.ToShortDateString() + " " + OSInstallDate.ToShortTimeString();
				}
				// Fill in .NET Framework information.
				textboxFrameworkShortVersion.Text = rm.GetString("os_ms_net_framework") + " " + FrameworkShortVersion;
				textboxFrameworkFullVersion.Text = FrameworkVersion;

				// Hide Service Pack if not present.
				if (String.IsNullOrEmpty(FrameworkServicePack))
				{
					labelFrameworkServicePack.Visible = false;
					textboxFrameworkServicePack.Text = "";
				}
				else
				{
					labelFrameworkServicePack.Visible = true;
					textboxFrameworkServicePack.Text = FrameworkServicePack;
				}
			}
			catch { }
		}

		#endregion
	}

}
