using System.Windows.Forms;
using System.Globalization;
using FreemiumUtil;
using System.Threading;
using System.Resources;
using System.Reflection;

namespace Disk_Cleaner
{
	/// <summary>
	/// Processing form of the Disk cleaner knot
	/// </summary>
	public partial class FormProcessing : Form
	{
        /// <summary>
        /// Resource manager
        /// </summary>
        public ResourceManager rm = new ResourceManager("Disk_Cleaner.Resources",
                                                        Assembly.GetExecutingAssembly());

		/// <summary>
		/// constructor for FrmProcessing
		/// </summary>
		public FormProcessing()
		{
			InitializeComponent();
		}

        private void FormProcessing_Load(object sender, System.EventArgs e)
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