using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using FreemiumUtil;

namespace ProcessManager
{
	/// <summary>
	/// Details form of the Process Manager knot
	/// </summary>
	public partial class FormShowDetails : Form
	{
		/// <summary>
		/// constructor for FrmShowDetails
		/// </summary>
		public FormShowDetails()
		{
			InitializeComponent();
		}

		/// <summary>
		/// handle Click event to close form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Updates general information
		/// </summary>
		/// <param name="dictUpdateGeneralInfo"></param>
		public void UpdateGeneralInformation(Dictionary<string, string> dictUpdateGeneralInfo)
		{
			if (dictUpdateGeneralInfo.Keys.Count > 0)
			{
				lvGeneral.Items.Clear();

				foreach (var pair in dictUpdateGeneralInfo)
				{
					var lvItem = new ListViewItem(pair.Key);

					if (pair.Key == rm.GetString("process_information"))
					{
						lvItem.ImageIndex = 0;
					}
					else if (pair.Key == rm.GetString("version_information"))
					{
						lvItem.ImageIndex = 1;
					}

					lvItem.SubItems.Add(pair.Value);
					lvGeneral.Items.Add(lvItem);
				}
			}
			lvGeneral.Columns[0].Width = -1;
			lvGeneral.Columns[1].Width = -1;
		}

		/// <summary>
		/// Updates modules used information
		/// </summary>
		/// <param name="process"></param>
		public void UpdateModulesUsedInformation(Process process)
		{
			lvModuleInfo.Items.Clear();
			foreach (ProcessModule module in process.Modules)
			{
				var lvItem = new ListViewItem(module.ModuleName);
				lvItem.SubItems.Add(module.FileName);
				lvModuleInfo.Items.Add(lvItem);
			}
			lvModuleInfo.Columns[0].Width = -1;
			lvModuleInfo.Columns[1].Width = -1;
		}

		/// <summary>
		/// initialize FrmShowDetails
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FrmShowDetails_Load(object sender, EventArgs e)
		{
			string lang = CfgFile.Get("Lang");
			SetCulture(new CultureInfo(lang));
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
			var rm = new ResourceManager("ProcessManager.Resources", typeof (FormShowDetails).Assembly);
			var resources = new ComponentResourceManager(typeof (FormShowDetails));

			tbpGeneral.Text = rm.GetString("general_info", culture);
			columnData.Text = rm.GetString("data", culture);
			imlMain.ImageStream = ((ImageListStreamer) (resources.GetObject("imageList1.ImageStream", culture)));
			columnValue.Text = rm.GetString("value", culture);
			tbpModuleInfo.Text = rm.GetString("module_info", culture);
			btnClose.Text = rm.GetString("close", culture);
			Text = rm.GetString("show_details", culture);
			columnName.Text = rm.GetString("name", culture);            
			columnExecutable.Text = rm.GetString("executable", culture);
		}
	}
}