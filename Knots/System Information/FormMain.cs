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

#region " Imported Namespaces "

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using System.IO;

#endregion

namespace SystemInformation
{
	/// <summary>
	/// Main form of the System Information utility
	/// </summary>
	public partial class FormMain
	{
		readonly ResourceManager resourceManager = new ResourceManager("SystemInformation.Properties.Resources", typeof(FormMain).Assembly);
		readonly InformationClass info = new InformationClass();
		FormBusy formBusy = new FormBusy();

		#region " TreeView Select "

        /// <summary>
        /// Display the correct panel based on the node that was selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void treeviewSystemInfo_AfterSelect(object sender, TreeViewEventArgs e)
		{
			BackgroundWorker worker = new BackgroundWorker();
			worker.DoWork += new DoWorkEventHandler(worker_DoWork);
			worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
			worker.RunWorkerAsync(e.Node.Name);
			formBusy.ShowDialog();
		}

        /// <summary>
        /// Shows the result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			SplitContainer.Panel2.Controls.Clear();
			SplitContainer.Panel2.Controls.Add((Control)e.Result);
			formBusy.Close();
		}

        /// <summary>
        /// Fills information on tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.Sleep(400);

            try
            {
                switch ((string)e.Argument)
                {
                    case "Computer":
                        e.Result = Computer.CreateInstance();
                        break;
                    case "CPU":
                        e.Result = Cpu.CreateInstance();
                        break;
                    case "BIOS":
                        e.Result = Bios.CreateInstance();
                        break;
                    case "Drives":
                        e.Result = Drives.CreateInstance();
                        break;
                    case "Network":
                        e.Result = Network.CreateInstance();
                        break;
                    case "Sound":
                        e.Result = Sound.CreateInstance();
                        break;
                    case "Video":
                        e.Result = Video.CreateInstance();
                        break;
                    case "OperatingSystem":
                        e.Result = OperatingSystem.CreateInstance();
                        break;
                    case "DateTime":
                        e.Result = DateAndTime.CreateInstance();
                        break;
                    case "Installed Programs":
                        e.Result = InstalledPrograms.CreateInstance();
                        break;
                    case "SpecialFolders":
                        e.Result = SpecialFolders.CreateInstance();
                        break;
                    case "Services":
                        e.Result = Services.CreateInstance();
                        break;
                    case "Startup":
                        e.Result = StartupPrograms.CreateInstance();
                        break;
                    case "UserAccounts":
                        e.Result = Users.CreateInstance();
                        break;
                    case "VisualStyles":
                        e.Result = VisualStyles.CreateInstance();
                        break;
                    default:
                        e.Result = Introduction.CreateInstance();
                        break;
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
		}

		#endregion

		#region " Form Events "

        /// <summary>
        /// Handles Load event of Main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void MainForm_Load(Object sender, EventArgs e)
		{
			try
			{
				SetCulture(new CultureInfo(CfgFile.Get("Lang")));

				// The last image in the image list holds the os image.
				int index = imagelistTree.Images.Count - 1;

				// Set OS icon on treeview
				switch (info.OSShortVersion)
				{
					case "5.0": // Windows 2000
						imagelistTree.Images[index] =
							(Image)resourceManager.GetObject("Windows2000_16x16");
						break;
					case "5.1": // Windows XP
						imagelistTree.Images[index] =
							(Image)resourceManager.GetObject("Windows_XP_16x16");
						break;
					case "6.0": // Windows Vista
						imagelistTree.Images[index] =
							(Image)resourceManager.GetObject("Windows_Vista_16x16");
						break;
					case "6.1": // Windows 7
						imagelistTree.Images[index] =
							(Image)resourceManager.GetObject("Windows_Vista_16x16");
						break;
					default:
						imagelistTree.Images[index] =
							(Image)resourceManager.GetObject("Windows_16x16");
						break;
				}

				// Start with the tree fully expanded
				treeviewSystemInfo.ExpandAll();
				treeviewSystemInfo.Refresh();

				// Add first panel
				SplitContainer.Panel2.Controls.Clear();
				SplitContainer.Panel2.Controls.Add(Introduction.CreateInstance());

				// enable timer
				timerTimeUp.Enabled = true;
			}
			catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
		}

		#endregion

		#region " Timer Events "

		void timerTime_Tick(Object sender, EventArgs e)
		{
			// Update HolidayDate and Time.
            tsslDateTime.Text = DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToLongTimeString();
		}

		void timerTimeUp_Tick(Object sender, EventArgs e)
		{
			// Update Up Time.
			tsslUpTime.Text = rm.GetString("computer_up_time") + ": " + info.OSUpTime;
		}

		#endregion

		/// <summary>
		/// <see cref="FormMain"/> constructor
		/// </summary>
		public FormMain()
		{
			InitializeComponent();
            if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\FreeToolbarRemover.exe"))
            {
                this.Icon = Properties.Resources.GBicon;
            }
            else if (!File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\FreemiumUtilities.exe"))
            {
                this.Icon = Properties.Resources.PCCleanerIcon;
            }
            else
            {
                this.Icon = Properties.Resources.FSUIcon;
            }
		}

        /// <summary>
        /// Sets culture
        /// </summary>
        /// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
            ResourceManager rm = new ResourceManager("SystemInformation.Resources", typeof(FormMain).Assembly);

			var resources = new ComponentResourceManager(typeof(FormMain));
			Thread.CurrentThread.CurrentUICulture = culture;

			treeviewSystemInfo.Nodes[0].Nodes[0].Nodes[0].Text = rm.GetString("node_bios");
			treeviewSystemInfo.Nodes[0].Nodes[0].Nodes[0].ToolTipText = rm.GetString("node_bios_tip");
			treeviewSystemInfo.Nodes[0].Nodes[0].Nodes[1].Text = rm.GetString("node_cpu");
			treeviewSystemInfo.Nodes[0].Nodes[0].Nodes[1].ToolTipText = rm.GetString("node_cpu_tip");
			treeviewSystemInfo.Nodes[0].Nodes[0].Nodes[2].Text = rm.GetString("node_drives");
			treeviewSystemInfo.Nodes[0].Nodes[0].Nodes[2].ToolTipText = rm.GetString("node_drives_tip");
			treeviewSystemInfo.Nodes[0].Nodes[0].Nodes[3].Text = rm.GetString("node_network");
			treeviewSystemInfo.Nodes[0].Nodes[0].Nodes[3].ToolTipText = rm.GetString("node_network_tip");
			treeviewSystemInfo.Nodes[0].Nodes[0].Nodes[4].Text = rm.GetString("node_sound");
			treeviewSystemInfo.Nodes[0].Nodes[0].Nodes[4].ToolTipText = rm.GetString("node_sound_tip");
			treeviewSystemInfo.Nodes[0].Nodes[0].Nodes[5].Text = rm.GetString("node_video");
			treeviewSystemInfo.Nodes[0].Nodes[0].Nodes[5].ToolTipText = rm.GetString("node_video_tip");
			treeviewSystemInfo.Nodes[0].Nodes[0].ToolTipText = rm.GetString("node_computer_tip");

			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[0].Text = rm.GetString("node_datetime");
			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[0].ToolTipText = rm.GetString("node_datetime_tip");
			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[1].Text = rm.GetString("node_installedprogs");
			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[1].ToolTipText = rm.GetString("node_installedprogs_tip");
			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[2].Text = rm.GetString("node_services");
			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[2].ToolTipText = rm.GetString("node_services_tip");
			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[3].Text = rm.GetString("node_spfolders");
			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[3].ToolTipText = rm.GetString("node_spfolders_tip");
			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[4].Text = rm.GetString("node_startupprogs");
			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[4].ToolTipText = rm.GetString("node_startupprogs_tip");
			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[5].Text = rm.GetString("node_useraccounts");
			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[5].ToolTipText = rm.GetString("node_useraccounts_tip");
			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[6].Text = rm.GetString("node_visualstyles");
			treeviewSystemInfo.Nodes[0].Nodes[1].Nodes[6].ToolTipText = rm.GetString("node_visualstyles_tip");
			treeviewSystemInfo.Nodes[0].Nodes[1].Text = rm.GetString("node_os");
			treeviewSystemInfo.Nodes[0].Nodes[1].ToolTipText = rm.GetString("node_os_tip");

			treeviewSystemInfo.Nodes[0].Text = rm.GetString("root");


			//treeNode16.Text = rm.GetString("root", culture);
			StatusStrip.Text = rm.GetString("main_statusstrip", culture);
           // Icon = Properties.Resources.PCCleanerIcon;
            
			Text = rm.GetString("window_title", culture);
			ucTop.Text = rm.GetString("window_title", culture);
		}
	}
}