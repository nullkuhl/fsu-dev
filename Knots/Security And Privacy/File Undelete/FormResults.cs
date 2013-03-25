using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace FileUndelete
{
    /// <summary>
    /// Results form of the File Undelete knot
    /// </summary>
    public partial class FormResults : Form
    {
        /// <summary>
        /// constructor for FormResults
        /// </summary>
        public FormResults()
        {
            InitializeComponent();
            SetCulture(new CultureInfo(CfgFile.Get("Lang")));
        }

        /// <summary>
        /// change current language
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;

            btnOK.Text = rm.GetString("ok");
            Text = rm.GetString("recovery_results");
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
