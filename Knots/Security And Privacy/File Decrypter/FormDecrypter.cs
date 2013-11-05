using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Decrypter
{
    /// <summary>
    /// Decrypter utility main form
    /// </summary>
    public partial class FormDecrypter : Form
    {
        // Defining the decrypter termination as int array (instead of byte array or string) to be sure this declaration itself is not accidentally found as termination.
        private int[] DECRYPTER_TERMINATION = { 35, 35, 35, 68, 69, 67, 82, 89, 80, 84, 69, 82, 84, 69, 82, 77, 73, 78, 65, 84, 73, 79, 78, 35, 35, 35 };

        #region Constructors

        /// <summary>
        /// constructor for DecryptFrm
        /// </summary>
        public FormDecrypter()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// initialize DecryptFrm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DecryptFrm_Load(object sender, EventArgs e)
        {
            extractToTxt.Text = new FileInfo(Application.ExecutablePath).DirectoryName;
        }

        /// <summary>
        /// handle Click event to show folder browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void browseBtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                extractToTxt.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// handle Click event to close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// handle Click event to decrypt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void decryptBtn_Click(object sender, EventArgs e)
        {
            // Check destination path
            try
            {
                new Uri(extractToTxt.Text);
            }
            catch
            {
                MessageBox.Show("The destination path is not valid !");
                return;
            }

            if (!Directory.Exists(extractToTxt.Text))
            {
                MessageBox.Show("The destination path is not valid !");
                return;
            }

            // Get this executable path
            string execPath = Application.ExecutablePath;

            // Get executable file name
            var execFileInfo = new FileInfo(execPath);
            string execFileName = execFileInfo.Name;

            // Open the executable as a stream and seek to the encrypted payload
            FileStream execFile = File.OpenRead(execPath);

            // Searching for the termination string:
            // scanning byte per byte is not the fastest way obviously,
            // but as the decrypter executable is only around 80 KB in size,
            // this won't be an issue here.
            bool found = false;
            if (execFile.CanRead)
            {
                List<int> bufferList = new List<int>();
                int byteRead;
                while (execFile.CanRead)
                {
                    byteRead = execFile.ReadByte();
                    if (byteRead == -1) break;

                    bufferList.Add(byteRead);
                    if (bufferList.Count == DECRYPTER_TERMINATION.Length)
                    {
                        found = true;
                        for (int i = 0; i < bufferList.Count; i++)
                        {
                            if (bufferList[i] != DECRYPTER_TERMINATION[i])
                            {
                                found = false;
                                break;
                            }
                        }

                        if (found) break;
                        bufferList.RemoveAt(0);
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show("The file is corrupted and connot be decrypted!");
                return;
            }

            // Specify the output file path
            var destDirInfo = new DirectoryInfo(extractToTxt.Text);
            string outPath = new Uri(new Uri(destDirInfo.FullName + "\\"),
                                     execFileName.Remove(execFileName.LastIndexOf(execFileInfo.Extension))).LocalPath;

            if (File.Exists(outPath))
            {
                DialogResult result = MessageBox.Show("File already exists. Overwrite existing file ?", "Information",
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.No)
                {
                    execFile.Close();
                    return;
                }
            }

            // Get the password
            string password = passwordTxt.Text;

            // Try to decrypt
            try
            {
                CryptoHelp.DecryptFile(execFile, outPath, password, UpdateProgress);
                MessageBox.Show("File decrypted successfully !",
                                "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (CryptographicException)
            {
                MessageBox.Show("Incorrect password, please try again.",
                                "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (CryptoHelpException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                execFile.Close();
                if (File.Exists(outPath) && new FileInfo(outPath).Length == 0)
                    File.Delete(outPath);
            }
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// update progress bar
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="value"></param>
        void UpdateProgress(long min, long max, long value)
        {
            double progress = (double)(1.0 * value / (max - min));

            processPrgrss.Minimum = 0;
            processPrgrss.Maximum = 100;
            processPrgrss.Value = (int)(100 * progress);
            processPrgrss.Update();
            Application.DoEvents();
        }

        #endregion
    }
}