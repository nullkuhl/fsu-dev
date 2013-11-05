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
	/// Video panel of the System Information utility
	/// </summary>
	public partial class Video : SystemInformation.TaskPanelBase
	{
		static Video panelInstance;
		static InformationClass info = new InformationClass();

		private static int VidNumberOfControllers;
		private static string VidPrimaryScreenDimensions;
		private static string VidPrimaryScreenWorkingArea;
		private static System.Collections.ObjectModel.Collection<string> VidController;
		private static System.Collections.ObjectModel.Collection<string> VidScreenColors;
		private static System.Collections.ObjectModel.Collection<string> VidRefreshRate;
		private static System.Collections.ObjectModel.Collection<string> VidRam;

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static Video CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new Video();

				// Get Information
				info.Initialize(InformationClass.Initializers.GetVideoInfo);
				VidNumberOfControllers = info.VidNumberOfControllers;
				VidPrimaryScreenDimensions = info.VidPrimaryScreenDimensions;
				VidPrimaryScreenWorkingArea = info.VidPrimaryScreenWorkingArea;
				VidController = info.VidController;
				VidScreenColors = info.VidScreenColors;
				VidRefreshRate = info.VidRefreshRate;
				VidRam = info.VidRam;
			}
			return panelInstance;
		}

		#region " Video Events "

		void Video_Load(object sender, System.EventArgs e)
		{
			ResourceManager rm = new ResourceManager("SystemInformation.Resources", System.Reflection.Assembly.GetExecutingAssembly());

			this.labelTitle.Text = rm.GetString("node_video");
			this.labelControllers.Text = rm.GetString("video_controllers");
			this.labelNumberControllers.Text = rm.GetString("video_num_controllers");
			this.labelScreenDimensions.Text = rm.GetString("video_current_scr_dim") + ":";
			this.labelScreenWorkingArea.Text = rm.GetString("video_current_scr_work_area") + ":";
			this.labelControllerInfo.Text = rm.GetString("video_controller_info") + ":";
			try
			{
				// Allow panel to paint.
				Application.DoEvents();
				int X;
				// Display number of adaptors
				labelNumberControllers.Text = rm.GetString("video_num_vid_controllers") + ": " + VidNumberOfControllers.ToString();
				// Display dimensions
				if (VidNumberOfControllers > 0)
				{
					this.labelScreenDimensions.Text = rm.GetString("video_primary_scr_dim") + ": " + VidPrimaryScreenDimensions;
					this.labelScreenWorkingArea.Text = rm.GetString("video_primary_scr_work_area") + ": " + VidPrimaryScreenWorkingArea;
				}

				// Clear listview
				listviewAdaptors.Items.Clear();
				// Add information to listview
				if (VidNumberOfControllers > 0)
				{
					for (X = 0; X <= VidNumberOfControllers - 1; X++)
					{
						try
						{
							listviewAdaptors.Items.Add(rm.GetString("video_vid_controller") + ": " + VidController[X].ToString());
						}
						catch
						{
							listviewAdaptors.Items.Add(rm.GetString("video_vid_controller") + ": " + rm.GetString("video_n/a"));
						}

						try
						{
							listviewAdaptors.Items.Add(rm.GetString("video_num_colors") + ": " + VidScreenColors[X].ToString());
						}
						catch
						{
							listviewAdaptors.Items.Add(rm.GetString("video_num_colors") + ": " + rm.GetString("video_n/a"));
						}

						try
						{
							listviewAdaptors.Items.Add(rm.GetString("video_refresh_rate") + ": " + VidRefreshRate[X].ToString());
						}
						catch
						{
							listviewAdaptors.Items.Add(rm.GetString("video_refresh_rate") + ": " + rm.GetString("video_n/a"));
						}

						try
						{
							listviewAdaptors.Items.Add(rm.GetString("video_ram") + ": " + VidRam[X].ToString());
						}
						catch
						{
							listviewAdaptors.Items.Add(rm.GetString("video_ram") + ": " + rm.GetString("video_n/a"));
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
