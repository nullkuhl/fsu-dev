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
	/// Visual styles panel of the System Information utility
	/// </summary>
	public partial class VisualStyles : SystemInformation.TaskPanelBase
	{
		static VisualStyles panelInstance;
		static InformationClass info = new InformationClass();

		static bool VstIsEnabledByUser;
		static bool VstIsSupportedByOS;
		static string VstCompany;
		static string VstCopyright;
		static string VstAuthor;
		static string VstVersion;
		static string VstDisplayName;
		static string VstDescription;
		static string VstColorScheme;
		static int VstMinimumColorDepth;
		static System.Drawing.Color VstControlHighlightHot;
		static System.Drawing.Color VstTextControlBorder;

		/// <summary>
		/// Create a global instance of this panel
		/// </summary>>
		public static VisualStyles CreateInstance()
		{
			if (panelInstance == null)
			{
				panelInstance = new VisualStyles();

				VstIsEnabledByUser = info.VstIsEnabledByUser;
				VstIsSupportedByOS = info.VstIsSupportedByOS;
				VstCompany = info.VstCompany;
				VstCopyright = info.VstCopyright;
				VstAuthor = info.VstAuthor;
				VstVersion = info.VstVersion;
				VstDisplayName = info.VstDisplayName;
				VstDescription = info.VstDescription;
				VstColorScheme = info.VstColorScheme;
				VstMinimumColorDepth = info.VstMinimumColorDepth;
				VstControlHighlightHot = info.VstControlHighlightHot;
				VstTextControlBorder = info.VstTextControlBorder;
			}
			return panelInstance;
		}

		#region " Visual Styles Events "

		void VisualStyles_Load(System.Object sender, System.EventArgs e)
		{
			ResourceManager rm = new ResourceManager("SystemInformation.Resources", System.Reflection.Assembly.GetExecutingAssembly());

			this.labelVisualStyles.Text = rm.GetString("node_visualstyles");
			this.labelCompanyDesc.Text = rm.GetString("vs_company") + ":";
			this.labelCurrentVisualStyle.Text = rm.GetString("vs_current_vs");
			this.labelCopyrightDesc.Text = rm.GetString("vs_copyright") + ":";
			this.labelVersionDesc.Text = rm.GetString("vs_version") + ":";
			this.labelDisplayNameDesc.Text = rm.GetString("vs_displayname") + ":";
			this.labelDescriptionDesc.Text = rm.GetString("vs_description") + ":";
			this.labelColorSchemeDesc.Text = rm.GetString("vs_color_scheme") + ":";
			this.labelEnabeUserDesc.Text = rm.GetString("vs_enabled_by_user") + ":";
			this.labelSuportOSDesc.Text = rm.GetString("vs_supp_os") + ":";
			this.labelMinColorDepDesc.Text = rm.GetString("vs_min_color_depth") + ":";
			this.labelCntrlHiLiteHotDesc.Text = rm.GetString("vs_control_highlight") + ":";
			this.labelTextCtrlBrdrDesc.Text = rm.GetString("vs_control_border") + ":";
			this.labelAuthorDesc.Text = rm.GetString("vs_author") + ":";
			try
			{
				// Allow panel to paint.
				Application.DoEvents();

				// Modify header label
				if (VstIsEnabledByUser == false)
				{
					labelCurrentVisualStyle.Text = rm.GetString("vs_not_enabled_by_user");
					labelAuthor.Visible = false;
					labelAuthorDesc.Visible = false;
					labelCntrlHiLiteHotDesc.Visible = false;
					pictureControlHighlightHot.Visible = false;
					labelColorScheme.Visible = false;
					labelColorSchemeDesc.Visible = false;
					labelCompany.Visible = false;
					labelColorSchemeDesc.Visible = false;
					labelCompany.Visible = false;
					labelCompanyDesc.Visible = false;
					labelCopyright.Visible = false;
					labelCopyrightDesc.Visible = false;
					labelCurrentVisualStyle.Visible = false;
					labelDescription.Visible = false;
					labelDescriptionDesc.Visible = false;
					labelDisplayName.Visible = false;
					labelDisplayNameDesc.Visible = false;
					labelMinColorDepDesc.Visible = false;
					labelMinimumColorDepth.Visible = false;
					labelTextCtrlBrdrDesc.Visible = false;
					pictureTextControlBorder.Visible = false;
					labelVersionDesc.Visible = false;
					labelVersion.Visible = false;
					labelEnabeUserDesc.Visible = true;
					labelIsEnabledByUser.Visible = true;
					labelIsSupportedByOS.Visible = true;
					labelSuportOSDesc.Visible = true;
				}
				else if (VstIsSupportedByOS == false)
				{
					labelCurrentVisualStyle.Text = rm.GetString("vs_disabled");
					labelAuthor.Visible = false;
					labelAuthorDesc.Visible = false;
					labelCntrlHiLiteHotDesc.Visible = false;
					pictureControlHighlightHot.Visible = false;
					labelColorScheme.Visible = false;
					labelColorSchemeDesc.Visible = false;
					labelCompany.Visible = false;
					labelColorSchemeDesc.Visible = false;
					labelCompany.Visible = false;
					labelCompanyDesc.Visible = false;
					labelCopyright.Visible = false;
					labelCopyrightDesc.Visible = false;
					labelCurrentVisualStyle.Visible = false;
					labelDescription.Visible = false;
					labelDescriptionDesc.Visible = false;
					labelDisplayName.Visible = false;
					labelDisplayNameDesc.Visible = false;
					labelMinColorDepDesc.Visible = false;
					labelMinimumColorDepth.Visible = false;
					labelTextCtrlBrdrDesc.Visible = false;
					pictureTextControlBorder.Visible = false;
					labelVersionDesc.Visible = false;
					labelVersion.Visible = false;
					labelEnabeUserDesc.Visible = true;
					labelIsEnabledByUser.Visible = true;
					labelIsSupportedByOS.Visible = true;
					labelSuportOSDesc.Visible = true;
				}
				else
				{
					labelCurrentVisualStyle.Text = rm.GetString("vs_current_vss");
				}

				// Fill in labels
				labelCompany.Text = VstCompany;
				labelCopyright.Text = VstCopyright;
				labelAuthor.Text = VstAuthor;
				labelVersion.Text = VstVersion;
				labelDisplayName.Text = VstDisplayName;
				labelDescription.Text = VstDescription;
				labelColorScheme.Text = VstColorScheme;
				labelIsEnabledByUser.Text = VstIsEnabledByUser.ToString();
				labelIsSupportedByOS.Text = VstIsSupportedByOS.ToString();
				labelMinimumColorDepth.Text = VstMinimumColorDepth.ToString();
				pictureControlHighlightHot.BackColor = VstControlHighlightHot;
				pictureTextControlBorder.BackColor = VstTextControlBorder;

			}
			catch (Exception) { }

		}

		#endregion
	}

}
