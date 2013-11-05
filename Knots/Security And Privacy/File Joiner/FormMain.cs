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
				var execFileInfo = new FileInfo(execFilePath);
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
			FileStream part1File = File.Open(baseFileName + ".1", FileMode.Open);
			var binaryReader = new BinaryReader(part1File);
			partsCount = binaryReader.ReadUInt16();
			fileSize = binaryReader.ReadUInt64();

			infoTxt.Text =
				"Filename: " + baseFileName + "\r\n" +
				"Size: " + FormatSize(fileSize) +
				(fileSize >= 1024 ? " (" + fileSize.ToString("##,#") + " byte)" : "") + "\r\n" +
				"Pieces count: " + partsCount;

			part1File.Close();
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
			if (!CheckDestinationPath())
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

            if (!IsEnoughSpace(extractToTxt.Text, baseFileName, partsCount))
            {
                MessageBox.Show("Not enough disk space. Please specify another folder.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

			joinBtn.Enabled = false;

			FileStream outStream = File.Open(destFile, FileMode.Create);

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
				outStream.Close();
				isJoining = false;
				Close();
			}
		}

        /// <summary>
        ///  Verify that we have enough space on driver before joining
        /// </summary>
        /// <param name="destFolder"></param>
        /// <param name="sourceFileName"></param>
        /// <param name="pieceCount"></param>
        /// <returns></returns>
        bool IsEnoughSpace(string destFolder, string sourceFileName, int pieceCount)
        {
            DriveInfo driveInfo = new DriveInfo(destFolder);
            long freeSpace = driveInfo.AvailableFreeSpace;
            FileInfo fi = new FileInfo(sourceFileName);
            long spaceNeeded = fi.Length * pieceCount;
            if (spaceNeeded > freeSpace)
                return false;
            else
                return true;
        }

		/// <summary>
		/// Joins a part with the so-far joined parts.
		/// </summary>
		/// <param name="outStream">Stream to append the part file to</param>
		/// <param name="part">Part number</param>
		/// <param name="bytesWritten">Number of bytes written so far (to update progress bar)</param>
		void JoinPart(FileStream outStream, int part, ref ulong bytesWritten)
		{
			FileStream partFile = File.OpenRead(baseFileName + "." + part);
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

				var readCount = (int) Math.Min(BUFFER_SIZE, partSize - i);
				partFile.Read(buffer, 0, readCount);
				outStream.Write(buffer, 0, readCount);

				bytesWritten += (ulong) readCount;

				// update progress bar
				processPrgrss.Value = (int) (100.0*bytesWritten/fileSize);
				processPrgrss.Update();
				Application.DoEvents();
			}

			outStream.Flush();
			partFile.Close();
		}

		/// <summary>
		/// Checks if extract to path is valid
		/// </summary>
		/// <returns></returns>
		bool CheckDestinationPath()
		{
			try
			{
				new Uri(extractToTxt.Text);
			}
			catch
			{
				MessageBox.Show("The destination path is not valid!");
				return false;
			}

			if (!Directory.Exists(extractToTxt.Text))
			{
				MessageBox.Show("The destination path is not valid!");
				return false;
			}

			return true;
		}

		/// <summary>
		/// Formats size for display
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		string FormatSize(ulong bytes)
		{
			double size = bytes;
			string unit = " bytes";
			if ((int) (size/1024) > 0)
			{
				size /= 1024.0;
				unit = " KB";
			}
			if ((int) (size/1024) > 0)
			{
				size /= 1024.0;
				unit = " MB";
			}
			if ((int) (size/1024) > 0)
			{
				size /= 1024.0;
				unit = " MB";
			}
			if ((int) (size/1024) > 0)
			{
				size /= 1024.0;
				unit = " TB";
			}

			return size.ToString("0.##") + unit;
		}
	}
}