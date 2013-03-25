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
	/// CPU panel of the System Information utility
	/// </summary>
	public partial class Cpu : SystemInformation.TaskPanelBase
	{
		static Cpu panelInstance;
		static InformationClass info = new InformationClass();

		static string CpuAddressWidth;
		static string CpuDescription;
		static string CpuFsbSpeed;
		static string CpuL2CacheSize;
		static string CpuL2CacheSpeed;
		static string CpuManufacturer;
		static string CpuName;
		static string CpuSocket;
		static string CpuSpeed;
		static string CpuProcessorId;
		static bool CpuPowerManagementSupported;
		static string CpuPowerManagementCapabilities;
		static int OSMajorVersion;
		static int CpuNumberOfCores;
		static int CpuNumberOfLogicalProcessors;
		static int CpuNumberOfProcessors;

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static Cpu CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new Cpu();

				// Get Information
				info.Initialize(InformationClass.Initializers.GetCpuInfo);

				CpuAddressWidth = info.CpuAddressWidth;
				CpuDescription = info.CpuDescription;
				CpuFsbSpeed = info.CpuFsbSpeed;
				CpuL2CacheSize = info.CpuL2CacheSize;
				CpuL2CacheSpeed = info.CpuL2CacheSpeed;
				CpuManufacturer = info.CpuManufacturer;
				CpuName = info.CpuName;
				CpuSocket = info.CpuSocket;
				CpuSpeed = info.CpuSpeed;
				CpuProcessorId = info.CpuProcessorId;
				CpuPowerManagementSupported = info.CpuPowerManagementSupported;
				CpuPowerManagementCapabilities = info.CpuPowerManagementCapabilities;
				OSMajorVersion = info.OSMajorVersion;
				CpuNumberOfCores = info.CpuNumberOfCores;
				CpuNumberOfLogicalProcessors = info.CpuNumberOfLogicalProcessors;
				CpuNumberOfProcessors = info.CpuNumberOfProcessors;
			}
			return panelInstance;
		}

		#region " Cpu Events "

		void Cpu_Load(System.Object sender, System.EventArgs e)
		{
			ResourceManager rm = new ResourceManager("SystemInformation.Resources", System.Reflection.Assembly.GetExecutingAssembly());

			this.labelTitle.Text = rm.GetString("node_cpu");
			this.labelManDesc.Text = rm.GetString("cpu_man") + ":";
			this.labelProcessor.Text = rm.GetString("cpu_proc");
			this.labelNameDesc.Text = rm.GetString("cpu_model") + ":";
			this.labelDescDesc.Text = rm.GetString("cpu_description") + ":";
			this.labelProcSpdDesc.Text = rm.GetString("cpu_proc_speed") + ":";
			this.labelFSBSpdDesc.Text = rm.GetString("cpu_fsb_speed") + ":";
			this.labelL2CacheSzDesc.Text = rm.GetString("cpu_cache_size") + ":";
			this.labelL2CacheSpdDesc.Text = rm.GetString("cpu_cache_speed") + ":";
			this.labelProcSockDesc.Text = rm.GetString("cpu_proc_socket") + ":";
			this.labelNumCoresDesc.Text = rm.GetString("cpu_num_core") + ":";
			this.labelPowerManagementCapabilitiesDesc.Text = rm.GetString("cpu_power_management_cap") + ":";
			this.labelPowerManagementSupportedDesc.Text = rm.GetString("cpu_power_management_supp") + ":";
			this.labelAddrWdthDesc.Text = rm.GetString("cpu_address_width") + ":";
			this.labelNumLogicalProcDesc.Text = rm.GetString("cpu_num_logic_proc") + ":";
			this.labelProcessorIdDesc.Text = rm.GetString("cpu_proc_id") + ":";
			try
			{
				Application.DoEvents();

				// Fill in labels
				labelAddressWidth.Text = CpuAddressWidth;
				labelDescription.Text = CpuDescription;
				labelFSBSpeed.Text = CpuFsbSpeed;
				labelL2CacheSize.Text = CpuL2CacheSize;
				labelL2CacheSpeed.Text = CpuL2CacheSpeed;
				labelManufacturer.Text = CpuManufacturer;
				labelName.Text = CpuName.Trim();
				labelProcessorSocket.Text = CpuSocket;
				labelProcessorSpeed.Text = CpuSpeed;
				labelProcessorId.Text = CpuProcessorId;

				// Fill in Power Management Info.
				if (CpuPowerManagementSupported)
				{
					labelPowerManagementSupported.Text = rm.GetString("yes");
					labelPowerManagementCapabilities.Visible = true;
					labelPowerManagementCapabilities.Text = CpuPowerManagementCapabilities;
				}
				else
				{
					labelPowerManagementSupported.Text = rm.GetString("no");
					labelPowerManagementCapabilities.Visible = false;
					labelPowerManagementCapabilities.Text = "";
				}

				// Beginning with Windows Vista, NumberOfCores and NumberOfLogicalProcessors are available.
				if (OSMajorVersion > 5)
				{
					labelNumCoresDesc.Visible = true;
					labelNumCoresDesc.Text = rm.GetString("cpu_num_core") + ":";
					labelNumberCores.Text = CpuNumberOfCores.ToString();
					labelNumLogicalProcDesc.Visible = true;
					labelNumLogicalProc.Visible = true;
					labelNumLogicalProc.Text = CpuNumberOfLogicalProcessors.ToString();
				}
				else
				{
					labelNumCoresDesc.Visible = true;
					labelNumCoresDesc.Text = rm.GetString("cpu_num_processors") + ":";
					labelNumberCores.Text = CpuNumberOfProcessors.ToString();
					labelNumLogicalProcDesc.Visible = false;
					labelNumLogicalProc.Visible = false;
				}
			}
			catch { }
		}

		#endregion

	}

}
