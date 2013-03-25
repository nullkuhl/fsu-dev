using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace FileUndelete
{
    /// <summary>
    /// Recover path form of the File Undelete knot
    /// </summary>
    public partial class RecoverPathDialog : Form
    {
        readonly CultureInfo culture;
        readonly string defaultPath;

        /// <summary>
        /// constructor for RecoverPathDialog
        /// </summary>
        /// <param name="defaultPath"></param>
        public RecoverPathDialog(string defaultPath)
        {
            this.defaultPath = defaultPath;
            culture = new CultureInfo(CfgFile.Get("Lang"));
            InitializeComponent();
        }

        /// <summary>
        /// Selected path
        /// </summary>
        public string SelectedPath
        {
            get { return txtPath.Text; }
        }

        /// <summary>
        /// initialize RecoverPathDialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RestorePathDialog_Load(object sender, EventArgs e)
        {
            SetCulture(culture);

            string filename = defaultPath;
            try
            {
                filename = Path.GetFileName(defaultPath);
            }
            catch
            {
                //nothing
            }
            lblSelect.Text = rm.GetString("sure_restore") + " " + filename + "?";
            txtPath.Text = defaultPath;
        }

        /// <summary>
        /// change current language
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;

            lblSelect.Text = rm.GetString("label1");
            lblPath.Text = rm.GetString("recovery_path");
            btnBrowse.Text = "&" + rm.GetString("browse") + "..";
            btnNo.Text = "&" + rm.GetString("no");
            btnYes.Text = "&" + rm.GetString("yes");
            Text = rm.GetString("confirm_recover");
        }

        /// <summary>
        /// handle Click event to select file path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            try
            {
                dlg.FileName = new FileInfo(SelectedPath).FullName;
            }
            catch
            {
                /* nothing */
            }

            DialogResult dlgResult = dlg.ShowDialog();
            if (dlgResult == DialogResult.Yes || dlgResult == DialogResult.OK)
            {
                txtPath.Text = dlg.FileName;
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            // TODO: implement it
        }
    }
}