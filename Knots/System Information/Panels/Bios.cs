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
using System.Reflection;
using System.Resources;

namespace SystemInformation
{
	/// <summary>
	/// Bios panel of the System Information utility
	/// </summary>
	public partial class Bios : TaskPanelBase
	{
		static Bios panelInstance;
		static InformationClass info = new InformationClass();

		static string BiosManufacturer;
		static string BiosName;
		static string BiosReleaseDate;
		static bool BiosSMBiosPresent;
		static string BiosSMBiosVersion;
		static string BiosVersion;
		static System.Collections.ObjectModel.Collection<string> BiosFeatures;

		#region " Bios Events "

		void Bios_Load(object sender, EventArgs e)
		{
			var rm = new ResourceManager("SystemInformation.Resources",
										 Assembly.GetExecutingAssembly());

			labelTitle.Text = rm.GetString("node_bios");
			labelSMPresentDesc.Text = rm.GetString("bios_smbios_present") + ":";
			labelReleaseDateDesc.Text = rm.GetString("bios_release_date") + ":";
			labelManDesc.Text = rm.GetString("bios_man") + ":";
			labelDescription.Text = rm.GetString("bios_general");
			labelVersionDesc.Text = rm.GetString("bios_version") + ":";
			labelSMVerDesc.Text = rm.GetString("bios_smbios_version") + ":";
			labelFeatures.Text = rm.GetString("bios_features");
			labelModelDesc.Text = rm.GetString("bios_model") + ":";
			try
			{
				object feature;

				// Fill in labels
				labelManufacturer.Text = BiosManufacturer;
				labelModel.Text = BiosName;
				labelReleaseDate.Text = BiosReleaseDate;
				labelSMBIOSPresent.Text = BiosSMBiosPresent.ToString();
				labelSMBIOSVersion.Text = BiosSMBiosVersion;
				labelVersion.Text = BiosVersion;

				// Clear listview items
				listviewFeatures.Items.Clear();

				// Fill in listview with features, if present
				if (BiosFeatures != null)
				{
					foreach (object tempLoopVar_feature in BiosFeatures)
					{
						feature = tempLoopVar_feature;
						listviewFeatures.Items.Add(feature.ToString());
					}
				}
				else
				{
					listviewFeatures.Items.Add(rm.GetString("bios_unavailable"));
				}
			}
			catch { }
		}

		#endregion

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static Bios CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new Bios();

				// Get Information
				info.Initialize(InformationClass.Initializers.GetBiosInfo);
				BiosManufacturer = info.BiosManufacturer;
				BiosName = info.BiosName;
				BiosReleaseDate = info.BiosReleaseDate;
				BiosSMBiosPresent = info.BiosSMBiosPresent;
				BiosSMBiosVersion = info.BiosSMBiosVersion;
				BiosVersion = info.BiosVersion;
				BiosFeatures = info.BiosFeatures;
			}
			return panelInstance;
		}
	}
}