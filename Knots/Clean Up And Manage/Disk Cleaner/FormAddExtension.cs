using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace Disk_Cleaner
{
	/// <summary>
	/// Adding extension form of the Disk cleaner knot 
	/// </summary>
	public partial class FormAddExtension : Form
	{
		/// <summary>
		/// constructor for FormAddExtension
		/// </summary>
		public FormAddExtension()
		{
			InitializeComponent();
		}

		/// <summary>
		/// handle Click event to add extension
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonOK_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxExt.Text)) return;
			DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
			var resourceManager = new ResourceManager("Disk_Cleaner.Resources", typeof (FormAddExtension).Assembly);
			Thread.CurrentThread.CurrentUICulture = culture;

			label1.Text = resourceManager.GetString("use_wildcards");
			label2.Text = resourceManager.GetString("filename_ends_with");
			label3.Text = resourceManager.GetString("description");
			buttonOK.Text = resourceManager.GetString("ok");
			buttonCancel.Text = resourceManager.GetString("cancel");
			Text = resourceManager.GetString("add_junk_file");
		}

		/// <summary>
		/// initialize FormAddExtension
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FormAddExtension_Load(object sender, EventArgs e)
		{
			var culture = new CultureInfo(CfgFile.Get("Lang"));
			SetCulture(culture);
		}
	}
}