using System;
using System.IO;
using System.Windows.Forms;

namespace Joiner
{
    /// <summary>
    /// Joiner utility main form
    /// </summary>
    public partial class FormMain : Form
    {
        static int BUFFER_SIZE = 1024;

        string baseFileName;
        ulong fileSize; // bytes

        bool isJoining;
        int partsCount;
        bool stop;

        /// <summary>
        /// Constructor for the form.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void JoinerFrm_Load(object sender, EventArgs e)
        {
            try
            {
                string execFilePath = Application.ExecutablePath;
                FileInfo execFileInfo = new FileInfo(execFilePath);
                extractToTxt.Text = execFileInfo.DirectoryName;

                baseFileName = execFileInfo.Name.Remove(execFileInfo.Name
                                                            .LastIndexOf(execFileInfo.Extension));

                LoadMetaData();
            }
            catch
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Loads and displays the details about the file to be joined.
        /// </summary>
        void LoadMetaData()
        {
            using (FileStream part1File = File.Open(baseFileName + ".1", FileMode.Open))
            {
                BinaryReader binaryReader = new BinaryReader(part1File);
                partsCount = binaryReader.ReadUInt16();
                fileSize = binaryReader.ReadUInt64();

                infoTxt.Text =
                    "Filename: " + baseFileName + "\r\n" +
                    "Size: " + Helper.FormatSize(fileSize) +
                    (fileSize >= 1024 ? " (" + fileSize.ToString("##,#") + " byte)" : "") + "\r\n" +
                    "Pieces count: " + partsCount;

                part1File.Close();
            }
        }

        /// <summary>
        /// Handles cancel button to stop joining and close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cancelBtn_Click(object sender, EventArgs e)
        {
            if (isJoining)
                stop = true;
            else
                Close();
        }

        /// <summary>
        /// Handles browse button to choose destination for the joined file.
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
        /// Starts joining the file and closes the form in the end.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void joinBtn_Click(object sender, EventArgs e)
        {
            if (!Helper.CheckDestinationPath(extractToTxt.Text))
                return;

            string destFile =
                new Uri(new Uri(extractToTxt.Text.TrimEnd('\\') + '\\'), baseFileName).LocalPath;
            if (File.Exists(destFile))
            {
                DialogResult result = MessageBox.Show("File already exists. Overwrite existing file?", "Information",
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.No)
                    return;
            }

            if (!Helper.IsEnoughSpace(extractToTxt.Text, baseFileName, partsCount))
            {
                MessageBox.Show("Not enough disk space. Please specify another folder.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            joinBtn.Enabled = false;

            using (FileStream outStream = File.Open(destFile, FileMode.Create))
            {
                try
                {
                    isJoining = true;
                    ulong bytesWritten = 0; // to update progress bar
                    for (int i = 1; isJoining && i <= partsCount; ++i)
                        JoinPart(outStream, i, ref bytesWritten);

                    if (!isJoining)
                    {
                        File.Delete(destFile);
                    }
                    else
                    {
                        MessageBox.Show("File joined successfully!");
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Missing part file. Cannot continue!");
                }
                finally
                {
                    isJoining = false;
                    Close();
                }
            }
        }

        /// <summary>
        /// Joins a part with the so-far joined parts.
        /// </summary>
        /// <param name="outStream">Stream to append the part file to</param>
        /// <param name="part">Part number</param>
        /// <param name="bytesWritten">Number of bytes written so far (to update progress bar)</param>
        void JoinPart(FileStream outStream, int part, ref ulong bytesWritten)
        {
            using (FileStream partFile = File.OpenRead(baseFileName + "." + part))
            {
                partFile.Seek(10, SeekOrigin.Begin); // skip header

                long partSize = partFile.Length - 10;

                var buffer = new byte[BUFFER_SIZE];
                for (int i = 0; i < partSize; i += BUFFER_SIZE)
                {
                    if (stop)
                    {
                        isJoining = false;
                        break;
                    }

                    int readCount = (int)Math.Min(BUFFER_SIZE, partSize - i);
                    partFile.Read(buffer, 0, readCount);
                    outStream.Write(buffer, 0, readCount);

                    bytesWritten += (ulong)readCount;

                    // update progress bar
                    processPrgrss.Value = (int)(100.0 * bytesWritten / fileSize);
                    processPrgrss.Update();
                    Application.DoEvents();
                }
                outStream.Flush();
                partFile.Close();
            }
        }
    }
}