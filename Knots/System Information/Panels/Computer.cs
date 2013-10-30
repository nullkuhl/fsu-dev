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
using System.Security;
using System.Windows.Forms;

namespace SystemInformation
{
	/// <summary>
	/// Computer panel of the System Information utility
	/// </summary>
	public partial class Computer : SystemInformation.TaskPanelBase
	{
		static Computer panelInstance;
		static InformationClass info = new InformationClass();

		static string CompManufacturer;
		static string CompModel;
		static string MainBoardManufacturer;
		static string MainBoardModel;
		static string CompDescription;
		static string CompSystemType;
		static string OSBootupState;
		static string MemTotalPhysical;
		static string MemAvailPhysical;
		static string MemTotalVirtual;
		static string MemAvailVirtual;

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static Computer CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new Computer();

				// Get Information
				info.Initialize(InformationClass.Initializers.GetNone);

				CompManufacturer = info.CompManufacturer;
				CompModel = info.CompModel;
				MainBoardManufacturer = info.MainBoardManufacturer;
				MainBoardModel = info.MainBoardModel;
				CompDescription = info.CompDescription;
				CompSystemType = info.CompSystemType;
				OSBootupState = info.OSBootupState;

				MemTotalPhysical = info.MemTotalPhysical;
				MemAvailPhysical = info.MemAvailPhysical;
				MemTotalVirtual = info.MemTotalVirtual;
				MemAvailVirtual = info.MemAvailVirtual;
			}
			return panelInstance;
		}

		#region " Computer Events "

		void Computer_Load(object sender, System.EventArgs e)
		{
			this.labelTitle.Text = rm.GetString("node_computer");
			this.labelGeneral.Text = rm.GetString("computer_general");
			this.labelMemory.Text = rm.GetString("computer_mem");
			this.labelTPDesc.Text = rm.GetString("computer_mem_tot_physical");
			this.labelAPDesc.Text = rm.GetString("computer_mem_av_physical");
			this.labelTVDesc.Text = rm.GetString("computer_mem_tot_virtual");
			this.labelComputerMfgDesc.Text = rm.GetString("computer_chipset_man") + ":";
			this.labelComputerModelDesc.Text = rm.GetString("computer_chipset_model") + ":";
			this.labelMainboardMfgDesc.Text = rm.GetString("computer_board_man") + ":";
			this.labelMainboardModelDesc.Text = rm.GetString("computer_board_model") + ":";
			this.labelDescriptionDesc.Text = rm.GetString("computer_description") + ":";
			this.labelSystemTypeDesc.Text = rm.GetString("computer_system_type") + ":";
			this.labelBootupStateDesc.Text = rm.GetString("computer_bootup_state") + ":";
			this.labelUpTimeDesc.Text = rm.GetString("computer_up_time") + ":";
			this.labelAVDesc.Text = rm.GetString("computer_mem_av_virtual");

			try
			{
				Application.DoEvents();

				// Fill in general informat5ion.
				labelCompManufacturer.Text = CompManufacturer;
				labelCompModel.Text = CompModel;
				labelMBManufacturer.Text = MainBoardManufacturer;
				labelMBModel.Text = MainBoardModel;
				labelDescription.Text = CompDescription;
				labelSystemType.Text = CompSystemType;
				labelBootupState.Text = OSBootupState;

				// Fill in Memory Information.
				labelTotalPhysical.Text = MemTotalPhysical;
				labelAvailablePhysical.Text = MemAvailPhysical;
				labelTotalVirtual.Text = MemTotalVirtual;
				labelAvailableVirtual.Text = MemAvailVirtual;

				// enable timer
				timerTimeUp.Enabled = true;
			}
			catch { }
		}

		#endregion

		#region " Timer Events "

		void timerTimeUp_Tick(System.Object sender, System.EventArgs e)
		{
			labelUpTime.Text = info.OSUpTime;
		}

		#endregion

	}

}
