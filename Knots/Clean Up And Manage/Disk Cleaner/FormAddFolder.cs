using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace Disk_Cleaner
{
	/// <summary>
	/// Add folder form of the Disk cleaner knot
	/// </summary>
	public partial class FormAddFolder : Form
	{
		/// <summary>
		/// constructor for FormAddFolder
		/// </summary>
		public FormAddFolder()
		{
			InitializeComponent();
		}

		/// <summary>
		/// handle Click event to show folder browser
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonBrowse_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
				textBoxExt.Text = folderBrowserDialog.SelectedPath;
		}

		/// <summary>
		/// handle Click event to choose folder
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonOK_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxExt.Text)) return;
			DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// initialize FormAddFolder
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FormAddFolder_Load(object sender, EventArgs e)
		{
			var culture = new CultureInfo(CfgFile.Get("Lang"));
			SetCulture(culture);
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
			var resourceManager = new ResourceManager("Disk_Cleaner.Resources", typeof (FormAddFolder).Assembly);
			new ComponentResourceManager(typeof (FormAddFolder));
			Thread.CurrentThread.CurrentUICulture = culture;

			label1.Text = resourceManager.GetString("can_use_wildcards") + ".";
			buttonBrowse.Text = resourceManager.GetString("browse") + "...";
			buttonCancel.Text = resourceManager.GetString("cancel");
			buttonOK.Text = resourceManager.GetString("ok");
			label3.Text = resourceManager.GetString("description");
		}
	}
}