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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace SystemInformation
{
	/// <summary>
	/// Introduction panel of the System Information utility
	/// </summary>
	public partial class Introduction : TaskPanelBase
	{
		static Introduction panelInstance;
		static InformationClass info = new InformationClass();

		static string AppCopyright;
		static string AppDescription;
		static string AppBuild;
		static string AppShortVersion;
		static string UserRegisteredName;
		static string UserRegisteredOrganization;
		static string AppTitle;
		static string AppCompanyName;
		static string AppDirectory;

		#region " Link Labels "

		void llbEULA_LinkClicked(Object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (File.Exists(AppDirectory + "\\EULA.pdf"))
			{
				try
				{
					// Now display EULA
                    ProcessStartInfo startInfo = new ProcessStartInfo(AppDirectory + "\\EULA.pdf");
					startInfo.WindowStyle = ProcessWindowStyle.Normal;
					Process.Start(startInfo);
				}
				catch (IOException)
				{
					// cannot find file
					MessageBox.Show(rm.GetString("pdf_notfound"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				// cannot find file
				MessageBox.Show(rm.GetString("eula_notfound"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		#endregion

		#region " Introduction Events "

		void Introduction_Load(object sender, EventArgs e)
		{
            ResourceManager rm = new ResourceManager("SystemInformation.Resources", Assembly.GetExecutingAssembly());

			labelTitle.Text = rm.GetString("root");
			labelDirections.Text = rm.GetString("root_note");
			try
			{
				// Allow panel to paint.
				Application.DoEvents();

				string build;

				labelAppCopyright.Text = AppCopyright;
				labelAppDescription.Text = AppDescription;

				if (AppBuild == "0" || String.IsNullOrEmpty(AppBuild))
				{
					build = "";
				}
				else
				{
					build = " " + rm.GetString("build") + " " + AppBuild;
				}

				labelAppVersion.Text = rm.GetString("version") + " " + AppShortVersion + build;
				labelCustomerName.Text = UserRegisteredName;
				labelCustomerOrganization.Text = UserRegisteredOrganization;
				labelTitleCompany.Text = AppTitle + " " + rm.GetString("is_a_product_of") + " " + AppCompanyName;
			}
			catch { }
		}

		#endregion

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static Introduction CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new Introduction();

				AppCopyright = info.AppCopyright;
				AppDescription = info.AppDescription;
				AppBuild = info.AppBuild;
				AppShortVersion = info.AppShortVersion;
				UserRegisteredName = info.UserRegisteredName;
				UserRegisteredOrganization = info.UserRegisteredOrganization;
				AppTitle = info.AppTitle;
				AppCompanyName = info.AppCompanyName;
				AppDirectory = info.AppDirectory;
			}
			return panelInstance;
		}
	}
}