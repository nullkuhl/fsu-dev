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
	/// Special folders panel of the System Information utility
	/// </summary>
	public partial class SpecialFolders : SystemInformation.TaskPanelBase
	{
		static SpecialFolders panelInstance;

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static SpecialFolders CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new SpecialFolders();
			}
			return panelInstance;
		}

		#region " Special Folders Events "

		void SpecialFolders_Load(object sender, System.EventArgs e)
		{
			ResourceManager rm = new ResourceManager("SystemInformation.Resources", System.Reflection.Assembly.GetExecutingAssembly());


			this.labelTitle.Text = rm.GetString("node_spfolders");
			this.labelFolders.Text = rm.GetString("spfolders_win_sp_folders");
			this.ColumnHeader1.Text = rm.GetString("spfolders_sp_folder_name");
			this.ColumnHeader2.Text = rm.GetString("spfolders_path");
			this.labelHint.Text = "(" + rm.GetString("spfolders_click_copy") + ".)";

			try
			{
				// Allow panel to paint.
				Application.DoEvents();

				// Clear listview
				listviewFolders.Items.Clear();

				// Fill in Listview
				listviewFolders.Items.Add(System.Environment.SpecialFolder.ApplicationData.ToString());
				listviewFolders.Items[0].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.ApplicationData));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.CommonApplicationData.ToString());
				listviewFolders.Items[1].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.CommonProgramFiles.ToString());
				listviewFolders.Items[2].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.CommonProgramFiles));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.Cookies.ToString());
				listviewFolders.Items[3].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.Cookies));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.Desktop.ToString());
				listviewFolders.Items[4].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.Desktop));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.DesktopDirectory.ToString());
				listviewFolders.Items[5].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.Favorites.ToString());
				listviewFolders.Items[6].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.Favorites));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.History.ToString());
				listviewFolders.Items[7].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.History));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.InternetCache.ToString());
				listviewFolders.Items[8].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.InternetCache));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.LocalApplicationData.ToString());
				listviewFolders.Items[9].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.MyComputer.ToString());
				listviewFolders.Items[10].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.MyComputer));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.MyDocuments.ToString());
				listviewFolders.Items[11].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.MyDocuments));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.MyMusic.ToString());
				listviewFolders.Items[12].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.MyMusic));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.MyPictures.ToString());
				listviewFolders.Items[13].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.MyPictures));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.Personal.ToString());
				listviewFolders.Items[14].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.Personal));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.ProgramFiles.ToString());
				listviewFolders.Items[15].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.Programs.ToString());
				listviewFolders.Items[16].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.Programs));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.Recent.ToString());
				listviewFolders.Items[17].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.Recent));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.SendTo.ToString());
				listviewFolders.Items[18].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.SendTo));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.StartMenu.ToString());
				listviewFolders.Items[19].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.StartMenu));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.Startup.ToString());
				listviewFolders.Items[20].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.Startup));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.System.ToString());
				listviewFolders.Items[21].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.System));
				listviewFolders.Items.Add(System.Environment.SpecialFolder.Templates.ToString());
				listviewFolders.Items[22].SubItems.Add(System.Environment
					.GetFolderPath(System.Environment.SpecialFolder.Templates));
			}
			catch (Exception) { }
		}

		#endregion

		#region " Listview Events "

		void listviewFolders_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (e.IsSelected)
			{
				try
				{
					// Copy path to clipboard.
					string folder = listviewFolders.Items[e.ItemIndex].SubItems[1].Text;
					Clipboard.SetText(folder, TextDataFormat.Text);
				}
				catch { }
			}
		}

		#endregion

	}

}
