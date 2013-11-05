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
	/// Network panel of the System Information utility
	/// </summary>
	public partial class Network : SystemInformation.TaskPanelBase
	{
		static Network panelInstance;
		static InformationClass info = new InformationClass();

		static int NetNumberOfAdapters;
		static bool OSPartOfDomain;
		static string OSDomain;
		static string OSMachineName;
		static System.Collections.ObjectModel.Collection<string> NetProductName;
		static System.Collections.ObjectModel.Collection<string> NetManufacturer;
		static System.Collections.ObjectModel.Collection<string> NetAdapterType;
		static System.Collections.ObjectModel.Collection<string> NetAutoSense;
		static System.Collections.ObjectModel.Collection<string> NetMaxSpeed;
		static System.Collections.ObjectModel.Collection<string> NetSpeed;
		static System.Collections.ObjectModel.Collection<string> NetConnectionId;
		static System.Collections.ObjectModel.Collection<string> NetConnectionStatus;
		static System.Collections.ObjectModel.Collection<string> NetMacAddress;
		static System.Collections.ObjectModel.Collection<bool> NetIPEnabled;
		static System.Collections.ObjectModel.Collection<string> NetIPAddress;
		static System.Collections.ObjectModel.Collection<string> NetHostName;
		static System.Collections.ObjectModel.Collection<string> NetDomain;
		static System.Collections.ObjectModel.Collection<string> NetTcpNumConnections;
		static System.Collections.ObjectModel.Collection<string> NetDefaultTtl;
		static System.Collections.ObjectModel.Collection<string> NetMtu;
		static System.Collections.ObjectModel.Collection<string> NetTcpWindowSize;
		static System.Collections.ObjectModel.Collection<bool> NetDhcpEnabled;
		static System.Collections.ObjectModel.Collection<string> NetDhcpLeaseObtained;
		static System.Collections.ObjectModel.Collection<string> NetDhcpLeaseExpires;
		static System.Collections.ObjectModel.Collection<string> NetDhcpServer;

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static Network CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new Network();

				// Get Information.
				info.Initialize(InformationClass.Initializers.GetNetAdaptorInfo);
				info.Initialize(InformationClass.Initializers.GetNetInterfaceInfo);

				NetNumberOfAdapters = info.NetNumberOfAdapters;
				OSPartOfDomain = info.OSPartOfDomain;
				OSDomain = info.OSDomain;
				OSMachineName = info.OSMachineName;
				NetProductName = info.NetProductName;
				NetManufacturer = info.NetManufacturer;
				NetAdapterType = info.NetAdapterType;
				NetAutoSense = info.NetAutoSense;
				NetMaxSpeed = info.NetMaxSpeed;
				NetSpeed = info.NetSpeed;
				NetConnectionId = info.NetConnectionId;
				NetConnectionStatus = info.NetConnectionStatus;
				NetMacAddress = info.NetMacAddress;
				NetIPEnabled = info.NetIPEnabled;
				NetIPAddress = info.NetIPAddress;
				NetHostName = info.NetHostName;
				NetDomain = info.NetDomain;
				NetTcpNumConnections = info.NetTcpNumConnections;
				NetDefaultTtl = info.NetDefaultTtl;
				NetMtu = info.NetMtu;
				NetTcpWindowSize = info.NetTcpWindowSize;
				NetDhcpEnabled = info.NetDhcpEnabled;
				NetDhcpLeaseObtained = info.NetDhcpLeaseObtained;
				NetDhcpLeaseExpires = info.NetDhcpLeaseExpires;
				NetDhcpServer = info.NetDhcpServer;
			}
			return panelInstance;
		}

		#region " Network Events "

		void Network_Load(object sender, System.EventArgs e)
		{
			ResourceManager rm = new ResourceManager("SystemInformation.Resources", System.Reflection.Assembly.GetExecutingAssembly());

			this.labelTitle.Text = rm.GetString("node_network");
			this.labelInterface.Text = rm.GetString("network_interfaces");
			try
			{
				int i;

				Application.DoEvents();

				// Check if network is configured
				if (NetNumberOfAdapters == 0)
				{
					labelNetworkId.Text = rm.GetString("network_nonetwork") + ".";
					listviewInterface.Enabled = false;
					listviewInterface.Visible = false;
					this.Visible = true;
					return;
				}
				else
				{
					if (OSPartOfDomain == true)
					{
						labelNetworkId.Text = rm.GetString("network_domain") + ": " + OSDomain
							+ rm.GetString("network_computer") + " : " + OSMachineName;
					}
					else
					{
						labelNetworkId.Text = rm.GetString("network_workgroup") + ": " + OSDomain
							+ " " + rm.GetString("network_computer") + ": " + OSMachineName;
					}

					// Clear combobox.
					cboInterface.Items.Clear();

					// Add interfaces.
					for (i = 0; i <= NetNumberOfAdapters - 1; i++)
					{
						cboInterface.Items.Add(NetProductName[i]);
					}

					// Clear listview
					listviewInterface.Items.Clear();

					// Select first interface.
					cboInterface.SelectedIndex = 0;
				}
			}
			catch { }
		}

		#endregion

		#region " ComboBox Events "

		void cboInterface_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				// Clear the listview.
				listviewInterface.Items.Clear();

				// Display selected interface.
				int i = cboInterface.SelectedIndex;

				if (NetManufacturer[i] != "N/A")
				{
					listviewInterface.Items.Add(rm.GetString("network_manufacturer") + ": " + NetManufacturer[i]);
				}

				if (NetAdapterType[i] != "N/A")
				{
					listviewInterface.Items.Add(rm.GetString("network_adapter") + ": " + NetAdapterType[i]);
				}

				if (NetAutoSense[i] != "N/A")
				{
					listviewInterface.Items.Add(rm.GetString("network_sense") + ": " + NetAutoSense[i]);
				}

				if (NetMaxSpeed[i] != "N/A")
				{
					listviewInterface.Items.Add(rm.GetString("network_max_speed") + ": " + NetMaxSpeed[i]);
				}

				if (NetSpeed[i] != "N/A")
				{
					listviewInterface.Items.Add(rm.GetString("network_speed") + ": " + NetSpeed[i]);
				}

				if (NetConnectionId[i] != "N/A")
				{
					listviewInterface.Items.Add(rm.GetString("network_connection_id") + ": " + NetConnectionId[i]);
				}

				if (NetConnectionStatus[i] != "N/A")
				{
					listviewInterface.Items.Add(rm.GetString("network_connection_status") + ": " + NetConnectionStatus[i]);
				}

				if (NetMacAddress[i] != "N/A")
				{
					listviewInterface.Items.Add(rm.GetString("network_mac_address") + ": " + NetMacAddress[i]);
				}

				if (NetIPEnabled[i] == true)
				{
					if (NetIPAddress[i] != "N/A")
					{
						listviewInterface.Items.Add(rm.GetString("network_ip_address") + ": " + NetIPAddress[i]);
					}

					if (NetHostName[i] != "N/A")
					{
						listviewInterface.Items.Add(rm.GetString("network_dns_name") + ": " + NetHostName[i]);
					}

					if (NetDomain[i] != "N/A")
					{
						listviewInterface.Items.Add(rm.GetString("network_dns_domain") + ": " + NetDomain[i]);
					}

					if (NetTcpNumConnections[i] != "N/A")
					{
						listviewInterface.Items.Add(rm.GetString("network_numconnections") + ": " +
							NetTcpNumConnections[i]);
					}

					if (NetDefaultTtl[i] != "N/A")
					{
						listviewInterface.Items.Add(rm.GetString("network_ttl") + ": " +
							NetDefaultTtl[i]);
					}

					if (NetMtu[i] != "N/A")
					{
						listviewInterface.Items.Add(rm.GetString("network_mtu") + ": " +
							NetMtu[i]);
					}

					if (NetTcpWindowSize[i] != "N/A")
					{
						listviewInterface.Items.Add(rm.GetString("network_tcp_size") + ": " +
							NetTcpWindowSize[i]);
					}
				}

				if (NetDhcpEnabled[i] == true)
				{
					if (NetDhcpLeaseObtained[i] != "N/A")
					{
						listviewInterface.Items.Add(rm.GetString("network_dhcp_lease_obt") + ": " +
							NetDhcpLeaseObtained[i]);
					}

					if (NetDhcpLeaseExpires[i] != "N/A")
					{
						listviewInterface.Items.Add(rm.GetString("network_dhcp_lease_exp") + ": " +
							NetDhcpLeaseExpires[i]);
					}

					if (NetDhcpServer[i] != "N/A")
					{
						listviewInterface.Items.Add(rm.GetString("network_dhcp_server") + ": " + NetDhcpServer[i]);
					}
				}
			}
			catch { }

		}

		#endregion
	}

}
