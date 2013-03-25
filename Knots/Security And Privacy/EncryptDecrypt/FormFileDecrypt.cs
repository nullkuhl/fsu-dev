using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace EncryptDecrypt
{
    /// <summary>
    /// File decrypt form of the Encrypt and Decrypt knot
    /// </summary>
    public partial class frmFileDecrypt : Form
    {
        CultureInfo culture;
        string dcpath = string.Empty;

        /// <summary>
        /// File extract path
        /// </summary>
        public string Extractpath = String.Empty;

        /// <summary>
        /// Output
        /// </summary>
        public string Output;
        string password = String.Empty;

        /// <summary>
        /// constructor for frmFileDecrypt
        /// </summary>
        public frmFileDecrypt()
        {
            InitializeComponent();
        }

        /// <summary>
        /// initialize Form2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmFileDecrypt_Load(object sender, EventArgs e)
        {
            culture = new CultureInfo(CfgFile.Get("Lang"));
            SetCulture(culture);

            dcpath = Output;
            Extractpath = Output;
            if (dcpath.Contains('\\'))
                txtExtractTo.Text = dcpath.Substring(0, dcpath.LastIndexOf('\\'));
        }

        /// <summary>
        /// change current language
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;

            chkOpenAfterDecryption.Text = rm.GetString("open_file_decrypt");
            chkDeleteEncrypted.Text = rm.GetString("del_after_decryption") + "...!";
            grbPassword.Text = rm.GetString("pass");
            lblPassword.Text = rm.GetString("pass") + ":";
            grbPath.Text = rm.GetString("path_selection");
            lblExtractTo.Text = rm.GetString("extract_to") + ":";
            btnBrowseToExtract.Text = rm.GetString("browse") + "...";
            btnDecrypt.Text = rm.GetString("decrypt");
            Text = rm.GetString("file_decryption");
        }

        /// <summary>
        /// handle FormClosed event to close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmFileDecrypt_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// handle Click event to show folder browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnBrowseToExtract_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog { Description = rm.GetString("select_path_decrypt") + "." };
            fd.ShowDialog();
            if (!string.IsNullOrEmpty(fd.SelectedPath))
            {
                txtExtractTo.Text = fd.SelectedPath;

                int a = dcpath.Length - dcpath.LastIndexOf('\\');
                Extractpath = txtExtractTo.Text + dcpath.Substring(dcpath.LastIndexOf('\\'), a);
            }
        }

        /// <summary>
        /// handle Click event to decrypt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dcpath))
            {
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    password = txtPassword.Text;
                    //DECRYPTION CODE HERE
                    try
                    {
                        Output = Extractpath.Substring(0, Extractpath.Length - 7);
                        MessageBox.Show(Output);
                        CryptoHelp.DecryptFile(dcpath, Output, password, UpdateDecryptProgress);
                        //IF CHECKED
                        if (chkDeleteEncrypted.Checked)
                            File.Delete(dcpath);

                        MessageBox.Show(rm.GetString("decrypted_succ"));

                        if (chkOpenAfterDecryption.Checked)
                            Process.Start(Output);

                        txtPassword.Text = String.Empty;
                        txtExtractTo.Text = String.Empty;
                        chkDeleteEncrypted.Checked = false;
                        chkOpenAfterDecryption.Checked = false;
                        Application.Exit();
                    }
                    catch
                    {
                        MessageBox.Show(rm.GetString("wrong_pass"));
                        txtPassword.Text = String.Empty;
                    }
                }
                else
                {
                    MessageBox.Show(rm.GetString("enter_pass"));
                }
            }
            else
            {
                MessageBox.Show(rm.GetString("select_file_first"));
            }
        }

        /// <summary>
        /// Update decrypt progress
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="value"></param>
        private void UpdateDecryptProgress(long min, long max, long value)
        {
            double progress = (double)(1.0 * value / (max - min));

            prbDecrypting.Minimum = 0;
            prbDecrypting.Maximum = 100;
            prbDecrypting.Value = (int)(100 * progress);
            Application.DoEvents();
        }
    }
}