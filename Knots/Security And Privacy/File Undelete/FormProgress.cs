using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace FileUndelete
{
    /// <summary>
    /// Progress form of the File Undelete knot
    /// </summary>
    public partial class FormProgress : Form
    {
        bool canclose;

        /// <summary>
        /// constructor for FormProgress
        /// </summary>
        public FormProgress()
        {
            InitializeComponent();
        }

        /// <summary>
        /// handle Click event to cancel the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCancel_Click(object sender, EventArgs e)
        {
            canclose = true;
            Application.OpenForms[0].PerformLayout(this, "close");
        }

        /// <summary>
        /// initialize FormProgress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormProgress_Load(object sender, EventArgs e)
        {
            canclose = false;
            SetCulture(new CultureInfo(CfgFile.Get("Lang")));
        }

        /// <summary>
        /// change current language
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;

            btnCancel.Text = rm.GetString("cancel");
            Text = rm.GetString("searching");
        }

        /// <summary>
        /// handle FormClosing to close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (canclose)
            {
                Application.OpenForms[0].PerformLayout(this, "close");
                Hide();
            }
        }
    }
}