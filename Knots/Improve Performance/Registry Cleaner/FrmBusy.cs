using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace RegistryCleaner
{
	/// <summary>
	/// Registry Cleaner knot busy form
	/// </summary>
	public partial class FrmBusy : Form
	{
		/// <summary>
		/// Resourse manager
		/// </summary>
		public ResourceManager rm = new ResourceManager("RegistryCleaner.Properties.Resources",
		                                                Assembly.GetExecutingAssembly());

		/// <summary>
		/// constructor for FrmBusy
		/// </summary>
		public FrmBusy()
		{
			InitializeComponent();
		}

		/// <summary>
		/// initialize FrmBusy
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FrmBusy_Load(object sender, EventArgs e)
		{
			SetCulture(new CultureInfo(CfgFile.Get("Lang")));
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
			Thread.CurrentThread.CurrentUICulture = culture;
			lblStatus.Text = rm.GetString("processing");
		}
	}
}