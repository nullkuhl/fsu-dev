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
	/// Sound panel of the System Information utility
	/// </summary>
	public partial class Sound : SystemInformation.TaskPanelBase
	{
		static Sound panelInstance;
		static InformationClass info = new InformationClass();

		static int SndNumberOfControllers;
		static System.Collections.ObjectModel.Collection<string> SndManufacturer;
		static System.Collections.ObjectModel.Collection<string> SndController;
		static System.Collections.ObjectModel.Collection<string> SndDmaBufferSize;

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static Sound CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new Sound();

				// Get information.
				info.Initialize(InformationClass.Initializers.GetSoundInfo);
				SndNumberOfControllers = info.SndNumberOfControllers;
				SndManufacturer = info.SndManufacturer;
				SndController = info.SndController;
				SndDmaBufferSize = info.SndDmaBufferSize;
			}
			return panelInstance;
		}

		#region " Sound Events "

		void Sound_Load(object sender, System.EventArgs e)
		{
			ResourceManager rm = new ResourceManager("SystemInformation.Resources", System.Reflection.Assembly.GetExecutingAssembly());

			this.labelTitle.Text = rm.GetString("node_sound");
			this.labelControllers.Text = rm.GetString("sound_controllers");
			this.labelNumberControllers.Text = rm.GetString("sound_numofcontrollers") + ":";
			this.labelControllerInfo.Text = rm.GetString("sound_controller_info") + ":";
			try
			{
				// Allow panel to paint.
				Application.DoEvents();

				int X;

				// Display number of controllers
				labelNumberControllers.Text = rm.GetString("sound_numofsoundcontrollers") + ": " + SndNumberOfControllers.ToString();

				// Display controller detail
				if (SndNumberOfControllers > 0)
				{
					for (X = 0; X <= SndNumberOfControllers - 1; X++)
					{

						try
						{
							listviewAdaptors.Items.Add(rm.GetString("sound_man") + ": " + SndManufacturer[X].ToString());
						}
						catch
						{
							listviewAdaptors.Items.Add(rm.GetString("sound_man") + ": " + rm.GetString("sound_n/a"));
						}

						try
						{
							listviewAdaptors.Items.Add(rm.GetString("sound_controller") + ": " + SndController[X].ToString());
						}
						catch
						{
							listviewAdaptors.Items.Add(rm.GetString("sound_controller") + ": " + rm.GetString("sound_n/a"));
						}

						try
						{
							listviewAdaptors.Items.Add(rm.GetString("sound_dma_buffer_size") + ": " + SndDmaBufferSize[X].ToString());
						}
						catch
						{
							listviewAdaptors.Items.Add(rm.GetString("sound_dma_buffer_size") + ": " + rm.GetString("sound_n/a"));
						}

						// Add blank line between adaptors
						listviewAdaptors.Items.Add("");
					}
				}

			}
			catch { }
		}

		#endregion
	}

}
