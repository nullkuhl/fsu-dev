using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using Microsoft.VisualBasic.ApplicationServices;

namespace EncryptDecrypt
{
    /// <summary>
    /// Encrypt and Decrypt knot main form
    /// </summary>
    public partial class frmMain : Form
    {
        private byte[] DECRYPTER_TERMINATION = { 35, 35, 35, 68, 69, 67, 82, 89, 80, 84, 69, 82, 84, 69, 82, 77, 73, 78, 65, 84, 73, 79, 78, 35, 35, 35 };

        string dcpath = String.Empty;
        string encryptPath = String.Empty;
        string enpath = String.Empty;
        string extractpath = String.Empty;
        string password = String.Empty;

        /// <summary>
        /// <see cref="frmMain"/> constructor
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// initialize Form1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmMain_Load(object sender, EventArgs e)
        {
            SetCulture(new CultureInfo(CfgFile.Get("Lang")));
            string[] args = Environment.GetCommandLineArgs();
            bool encryptPassedFile = false;
            bool decryptPassedFile = false;
            foreach (string arg in args)
            {
                if (encryptPassedFile)
                {
                    enPath_textbox.Text = arg;
                    enpath = arg;
                    encryptPath = arg;
                    if (enpath.Contains('\\'))
                        encryptToTxt.Text = enpath.Substring(0, enpath.LastIndexOf('\\'));
                    if (encryptToTxt.Text.EndsWith(":"))
                        encryptToTxt.Text += "\\";
                }
                if (decryptPassedFile)
                {
                    tcMain.SelectTab(1);
                    dcPath_textbox.Text = arg;
                    dcpath = arg;
                    extractpath = arg;
                    if (dcpath.Contains('\\'))
                        extrect_Textbox.Text = dcpath.Substring(0, dcpath.LastIndexOf('\\'));

                    if (extrect_Textbox.Text.EndsWith(":"))
                        extrect_Textbox.Text += "\\";
                }
                if (arg == "ENCRYPT")
                {
                    encryptPassedFile = true;
                }
                if (arg == "DECRYPT")
                {
                    decryptPassedFile = true;
                }
            }
        }

        public void NewInstance(StartupNextInstanceEventArgs e)
        {
            var args = e.CommandLine;
            bool encryptPassedFile = false;
            bool decryptPassedFile = false;
            foreach (string arg in args)
            {
                if (encryptPassedFile)
                {
                    enPath_textbox.Text = arg;
                    enpath = arg;
                    encryptPath = arg;
                    if (enpath.Contains('\\'))
                        encryptToTxt.Text = enpath.Substring(0, enpath.LastIndexOf('\\'));
                    if (encryptToTxt.Text.EndsWith(":"))
                        encryptToTxt.Text += "\\";
                }
                if (decryptPassedFile)
                {
                    tcMain.SelectTab(1);
                    dcPath_textbox.Text = arg;
                    dcpath = arg;
                    extractpath = arg;
                    if (dcpath.Contains('\\'))
                        extrect_Textbox.Text = dcpath.Substring(0, dcpath.LastIndexOf('\\'));

                    if (extrect_Textbox.Text.EndsWith(":"))
                        extrect_Textbox.Text += "\\";
                }
                if (arg == "ENCRYPT")
                {
                    encryptPassedFile = true;
                }
                if (arg == "DECRYPT")
                {
                    decryptPassedFile = true;
                }
            }
        }


        /// <summary>
        /// change current language
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;

            grbPath.Text = rm.GetString("path_selection");
            lblSaveToEncrypt.Text = rm.GetString("save_to") + ":";
            encToBrowseBtn.Text = rm.GetString("browse") + "...";
            lblSelectEncrypt.Text = rm.GetString("select_file_encrypt") + ":";
            btnOpenFileToEncrypt.Text = rm.GetString("browse") + "...";
            grbPassword.Text = rm.GetString("pass");
            lblPasswdEncrypt.Text = rm.GetString("pass") + ":";
            lblPasswdConfirm.Text = rm.GetString("confirm_pass") + ":";
            Encrypt_btn.Text = rm.GetString("encrypt");
            EncryptionTab.Text = rm.GetString("encryption");
            chkCreateAuto.Text = rm.GetString("create_auto_file") + ".";
            chkDeleteAfterEncryption.Text = rm.GetString("del_original_file_encryption") + ".";
            DecryptionTab.Text = rm.GetString("decryption");
            chkOpenAfterDecryption.Text = rm.GetString("open_file_decrypt") + ".";
            chkDeleteAfterDecryption.Text = rm.GetString("del_after_decryption") + ".";
            grbPasswordDecr.Text = rm.GetString("pass");
            lblPasswdDecr.Text = rm.GetString("pass") + ":";
            grbPathDecr.Text = rm.GetString("path_selection");
            lblSaveToDecr.Text = rm.GetString("save_to") + ":";
            btnSaveTo.Text = rm.GetString("browse") + "...";
            lblSelectDecr.Text = rm.GetString("select_file_decrypt") + ":";
            btnOpenFileToDecrypt.Text = rm.GetString("browse") + "...";
            Decrypt_btn.Text = rm.GetString("decrypt");
            Text = rm.GetString("file_encrypter_decrypter");
            ucTop.Text = rm.GetString("file_encrypter_decrypter_title");
        }

        /// <summary>
        /// handle Click event to select file for encryption
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOpenFileToEncrypt_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Select All Files (*.*) | *.*";
            fd.Title = rm.GetString("select_file");
            fd.ShowDialog();
            if (!string.IsNullOrEmpty(fd.FileName))
            {
                enPath_textbox.Text = fd.FileName;
                enpath = fd.FileName;
                encryptPath = fd.FileName;
                if (enpath.Contains('\\'))
                    encryptToTxt.Text = enpath.Substring(0, enpath.LastIndexOf('\\'));
                if (encryptToTxt.Text.EndsWith(":"))
                    encryptToTxt.Text += "\\";
            }
        }

        /// <summary>
        /// handle Click event to encrypt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Encrypt_btn_Click(object sender, EventArgs e)
        {
            if (enpath == string.Empty)
            {
                MessageBox.Show(rm.GetString("select_file_first"));
                return;
            }

            try
            {
                new Uri(encryptToTxt.Text);
            }
            catch
            {
                MessageBox.Show(rm.GetString("select_destination_first"));
                return;
            }

            if (!Directory.Exists(encryptToTxt.Text))
            {
                MessageBox.Show(rm.GetString("select_destination_first"));
                return;
            }

            if (txtPasswdEncrypt.Text == txtPasswdConfirm.Text)
            {
                if (txtPasswdEncrypt.Text != String.Empty)
                {
                    password = txtPasswdConfirm.Text;
                    Encrypt_btn.Enabled = false;

                    DialogResult originalDelete = DialogResult.Yes;
                    if (chkDeleteAfterEncryption.Checked)
                    {
                        originalDelete = MessageBox.Show(rm.GetString("original_delete"), rm.GetString("del_original_file_encryption"), MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Exclamation);
                    }
                    //ENCRYPTION CODE HERE
                    if (originalDelete == DialogResult.Yes)
                    {
                        try
                        {
                            int a = enpath.Length - enpath.LastIndexOf('\\');
                            encryptPath = encryptToTxt.Text + enpath.Substring(enpath.LastIndexOf('\\'), a);

                            bool successful = true;
                            if (chkCreateAuto.Checked == false)
                                CryptoHelp.EncryptFile(enpath, encryptPath + ".cov", password, UpdateEncryptProgress);
                            else
                                successful = PatchDecrypter(enpath, encryptPath, password);

                            // If encryption was not successful, we should not delete the original file,
                            // don't display and useless success messages and also not reset the form field
                            // to give the user the option to try again.
                            if (successful)
                            {
                                if (chkDeleteAfterEncryption.Checked)
                                    File.Delete(enpath);

                                MessageBox.Show(rm.GetString("encrypted_succ"), rm.GetString("succ"), MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                                if (chkCreateAuto.Checked)
                                    MessageBox.Show(rm.GetString("auto_encrypt_created"), rm.GetString("succ"), MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);
                                txtPasswdEncrypt.Text = String.Empty;
                                txtPasswdConfirm.Text = String.Empty;
                                enPath_textbox.Text = String.Empty;
                                encryptToTxt.Text = String.Empty;
                                chkDeleteAfterEncryption.Checked = false;
                                chkCreateAuto.Checked = false;
                            }
                        }
                        catch
                        {
                            MessageBox.Show(rm.GetString("cannot_be_encrypted"));
                        }
                    }
                }
                else
                    MessageBox.Show(rm.GetString("enter_pass"));
            }
            else
            {
                MessageBox.Show(rm.GetString("pass_must_match"));
                txtPasswdConfirm.Clear();
            }

            Encrypt_btn.Enabled = true;
            prbEncrypting.Value = 0;
        }

        bool PatchDecrypter(string srcpath, string encpath, string password)
        {
            try
            {
                string execPath = Application.ExecutablePath;
                string filepath = AppDomain.CurrentDomain.BaseDirectory;
                string decrypterPath = filepath + "Decrypter.exe";

                if (!File.Exists(decrypterPath))
                {
                    MessageBox.Show(string.Format("{0} {1}; {2}", rm.GetString("cannot_find"), decrypterPath, rm.GetString("cannot_create")),
                                    rm.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string outpath = encpath + ".exe";
                File.Copy(decrypterPath, outpath, true);
                FileStream execFile = File.Open(outpath, FileMode.Append);
                // Adding the decrypter termination string before writing the encrypted file data.
                execFile.Write(DECRYPTER_TERMINATION, 0, DECRYPTER_TERMINATION.Count());
                CryptoHelp.EncryptFile(srcpath, execFile, password, UpdateEncryptProgress);
                return true;
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
                return false;
            }
        }

        private void UpdateEncryptProgress(long min, long max, long value)
        {
            double progress = (double)(1.0 * value / (max - min));

            prbEncrypting.Minimum = 0;
            prbEncrypting.Maximum = 100;
            prbEncrypting.Value = (int)(100 * progress);
            Application.DoEvents();
        }

        /*********************************
                DECRYPTION TAB
         ********************************/
        //select file for decryption
        /// <summary>
        /// handle Click event to select file for decryption
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOpenFileToDecrypt_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Select Encrypted Files (.cov) | *.cov*";
            fd.Title = rm.GetString("select_file");
            fd.ShowDialog();
            if (!string.IsNullOrEmpty(fd.FileName))
            {
                dcPath_textbox.Text = fd.FileName;
                dcpath = fd.FileName;
                extractpath = fd.FileName;
                if (dcpath.Contains('\\'))
                    extrect_Textbox.Text = dcpath.Substring(0, dcpath.LastIndexOf('\\'));
                if (extrect_Textbox.Text.EndsWith(":"))
                    extrect_Textbox.Text += "\\";
            }
        }

        /// <summary>
        /// handle Click event to decrypt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Decrypt_btn_Click(object sender, EventArgs e)
        {
            if (dcpath == String.Empty)
            {
                MessageBox.Show(rm.GetString("select_file_first"));
                return;
            }

            try
            {
                new Uri(extrect_Textbox.Text);
            }
            catch
            {
                MessageBox.Show(rm.GetString("select_destination_first"));
                return;
            }

            if (!Directory.Exists(extrect_Textbox.Text))
            {
                MessageBox.Show(rm.GetString("select_destination_first"));
                return;
            }

            if (txtPasswdDecrypt.Text != String.Empty)
            {
                password = txtPasswdDecrypt.Text;
                Decrypt_btn.Enabled = false;

                //DECRYPTION CODE HERE
                string output = String.Empty;
                try
                {
                    dcpath = Path.GetFullPath(dcpath);
                    int a = dcpath.Length - dcpath.LastIndexOf('\\');
                    extractpath = extrect_Textbox.Text + dcpath.Substring(dcpath.LastIndexOf('\\'), a);

                    if (extractpath.EndsWith(".cov"))
                        output = extractpath.Substring(0, extractpath.Length - 4);
                    if (extractpath.EndsWith(".cov.EXE"))
                        output = extractpath.Substring(0, extractpath.Length - 7);

                    DialogResult continueDecrypt = DialogResult.Yes;
                    bool isEncryptedFileInSameDir = false;
                    if (File.Exists(output))
                    {
                        continueDecrypt = MessageBox.Show(rm.GetString("output_exists_message").Replace("%1", output), rm.GetString("output_exists_title"),
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        isEncryptedFileInSameDir = true;
                    }

                    if (continueDecrypt == DialogResult.Yes)
                    {
                        bool successful = false;
                        try
                        {
                            successful = CryptoHelp.DecryptFile(dcpath, output, password, UpdateDecryptProgress);
                        }
                        catch (Exception)
                        {
                        }

                        // If decryption was not successful, we should not delete the original file,
                        // don't display and useless success messages and also not reset the form field
                        // to give the user the option to try again.
                        if (successful)
                        {
                            if (chkDeleteAfterDecryption.Checked)
                            {
                                try
                                {
                                    File.Delete(dcpath);
                                }
                                catch
                                {
                                }
                            }

                            MessageBox.Show(rm.GetString("decrypted_succ"));
                            if (chkOpenAfterDecryption.Checked)
                            {
                                try
                                {
                                    Process.Start(output);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show(rm.GetString("file_open_failed"));
                                }
                            }

                            txtPasswdDecrypt.Text = String.Empty;
                            extrect_Textbox.Text = String.Empty;
                            dcPath_textbox.Text = String.Empty;
                            extrect_Textbox.Text = String.Empty;
                            chkDeleteAfterDecryption.Checked = false;
                            chkOpenAfterDecryption.Checked = false;
                        }
                        else
                        {
                            try
                            {
                                if (File.Exists(output) && !isEncryptedFileInSameDir)
                                    File.Delete(output);
                            }
                            catch
                            {
                            }

                            MessageBox.Show(rm.GetString("wrong_pass"));
                            txtPasswdDecrypt.Text = String.Empty;
                        }
                    }
                }
                catch (Exception)
                {
                    // ToDo: send exception details via SmartAssembly bug reporting!
                }
            }
            else
            {
                MessageBox.Show(rm.GetString("enter_pass"));
            }

            Decrypt_btn.Enabled = true;
            prbDecrypting.Value = 0;
        }

        //for extract folder
        /// <summary>
        /// handle Click event to show folder browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSaveTo_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog { Description = rm.GetString("select_path_decrypt") + "." };
            fd.ShowDialog();
            if (!string.IsNullOrEmpty(fd.SelectedPath))
            {
                extrect_Textbox.Text = fd.SelectedPath;
            }
        }

        /// <summary>
        /// handle Click event to show folder browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void encToBrowseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog { Description = rm.GetString("select_folder_encrypt") + "." };
            fd.ShowDialog();
            if (!string.IsNullOrEmpty(fd.SelectedPath))
            {
                encryptToTxt.Text = fd.SelectedPath;
            }
        }

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