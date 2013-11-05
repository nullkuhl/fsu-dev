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
	/// Services panel of the System Information utility
	/// </summary>
	public partial class Services : SystemInformation.TaskPanelBase
	{
		static Services panelInstance;
		static InformationClass info = new InformationClass();
		System.Resources.ResourceManager ResourceManager = new System.Resources.ResourceManager("SystemInformation.Properties.Resources", typeof(Services).Assembly);

		static System.Collections.ObjectModel.Collection<string> ServiceDisplayName;
		static System.Collections.ObjectModel.Collection<string> ServiceStartMode;
		static System.Collections.ObjectModel.Collection<string> ServiceState;
		static System.Collections.ObjectModel.Collection<string> ServicePathName;
		static System.Collections.ObjectModel.Collection<string> ServiceDescription;

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static Services CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new Services();

				// Get services info.
				info.Initialize(InformationClass.Initializers.GetServiceInfo);

				ServiceDisplayName = info.ServiceDisplayName;
				ServiceStartMode = info.ServiceStartMode;
				ServiceState = info.ServiceState;
				ServicePathName = info.ServicePathName;
				ServiceDescription = info.ServiceDescription;
			}
			return panelInstance;
		}

		#region Constants and Variables

		bool ascending;         // Used to toggle column sort.

		// Listview column constants.
		enum ListCol
		{
			DisplayName = 0,
			StartMode = 1,
			State = 2,
			PathName = 3,
			Description = 4
		}

		#endregion

		#region " Services Events "

		void Services_Load(System.Object sender, System.EventArgs e)
		{
			ResourceManager rm = new ResourceManager("SystemInformation.Resources", System.Reflection.Assembly.GetExecutingAssembly());

			this.labelTitle.Text = rm.GetString("node_services");
			this.labelServicesDescription.Text = rm.GetString("services_description");
			this.DisplayName.Text = rm.GetString("services_display_name");
			this.StartMode.Text = rm.GetString("services_start_mode");
			this.State.Text = rm.GetString("services_state");
			this.PathName.Text = rm.GetString("services_pathname");
			this.Description.Text = rm.GetString("services_details_description");
			this.labelPathName.Text = rm.GetString("services_path") + ":";
			this.labelDetails.Text = rm.GetString("services_details");
			this.labelDescription.Text = rm.GetString("services_details_description") + ":";

			try
			{
				int i = 0;

				Application.DoEvents();

				// Clear listview.
				listviewServices.Items.Clear();

				// Add services info to listview.
				foreach (string displayName in ServiceDisplayName)
				{
					listviewServices.Items.Add(ServiceDisplayName[i]);
					listviewServices.Items[i].SubItems.Add(ServiceStartMode[i]);
					listviewServices.Items[i].SubItems.Add(ServiceState[i]);
					listviewServices.Items[i].SubItems.Add(ServicePathName[i]);
					listviewServices.Items[i].SubItems.Add(ServiceDescription[i]);

					i++;
				}

				// Sort the listview.
				listviewServices.Sorting = SortOrder.Ascending;
				listviewServices.Sort();
			}
			catch { }
		}

		#endregion

		#region " ListView Events "

		void listviewServices_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			try
			{
				if (e.IsSelected)
				{
					// Display path name and description.
					textboxPathName.Text = listviewServices.Items[e.ItemIndex].SubItems[(int)ListCol.PathName].Text;
					textboxDescription.Text = listviewServices.Items[e.ItemIndex].SubItems[(int)ListCol.Description].Text;
				}
			}
			catch (Exception) { }

		}

		/// <summary>
		/// Set the ListViewItemSorter property to a new ListViewItemComparer 
		/// object. Setting this property immediately sorts the 
		/// ListView using the ListViewItemComparer object.
		/// </summary>
		void listviewServices_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
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
			listviewServices.ListViewItemSorter = new ListViewItemComparer(e.Column, ascending);

		}

		#endregion

	}

}
