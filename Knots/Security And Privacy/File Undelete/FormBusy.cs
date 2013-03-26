using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace FileUndelete
{
    /// <summary>
    /// Busy form of the File Undelete knot
    /// </summary>
    public partial class FormBusy : Form
    {
        /// <summary>
        /// Resource manager
        /// </summary>
        public ResourceManager rm = new ResourceManager("FileUndelete.Resources", Assembly.GetExecutingAssembly());

        /// <summary>
        /// constructor for FrmBusy
        /// </summary>
        public FormBusy()
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
            lblWait.Text = rm.GetString("processing");
            this.Text = rm.GetString("processing");
        }
    }
}