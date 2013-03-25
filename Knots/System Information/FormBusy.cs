using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace SystemInformation
{
	/// <summary>
	/// Busy form of the System Information knot
	/// </summary>
	public partial class FormBusy : Form
	{
		/// <summary>
		/// Resource manager
		/// </summary>
		public ResourceManager rm = new ResourceManager("SystemInformation.Resources", Assembly.GetExecutingAssembly());

		/// <summary>
		/// constructor for FormBusy
		/// </summary>
		public FormBusy()
		{
			InitializeComponent();
		}

		/// <summary>
		/// initialize FormBusy
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
			lblWait.Text = rm.GetString("processing");
		}
	}
}