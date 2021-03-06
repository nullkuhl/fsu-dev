using System;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace FileSplitterAndJoiner
{
    /// <summary>
    /// File splitter and joiner main form
    /// </summary>
    public partial class FormMain : Form
    {
        #region Properties

        BinaryReader binaryReader;
        BinaryWriter binaryWriter;
        bool closeStreamReader;
        bool closeStreamWriter;
        long fileSize;
        string inputFileName = string.Empty;
        int lastSelectedIndex;
        string outputFileName = string.Empty;
        int part = 1;
        int partIndex = 1;
        int partsQuantity;
        bool pause;
        long pieceIndex = 5 * 1024 * 1024;
        long pieceSize;
        int smallPartsCount;
        StreamReader streamReader;
        StreamWriter streamWriter;
        string[] toJoin = new string[2];
        private string lastNumOfBytesText;
        #endregion

        #region constants
        const int headerSize = 10; //bytes
        #endregion

        #region Constructors

        /// <summary>
        /// FileSplitterAndJoiner constructor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            object[] sizes = { "Byte", "KB", "MB", "GB" };

            cboFileSizes.Items.AddRange(sizes);
            cboFileSizes.SelectedIndex = 0;

            txtSplitFileName.DragDrop += txtSplitFileName_DragDrop;
            txtJoinFileName.DragDrop += txtJoinFileName_DragDrop;
            //DragDrop may require
            txtSplitFileName.DragEnter += txtSplitFileName_DragEnter;
            txtJoinFileName.DragEnter += txtJoinFileName_DragEnter;
        }

        #endregion

        #region methods

        /// <summary>
        /// Main form load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmMain_Load(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo(CfgFile.Get("Lang"));
            SetCulture(culture);

            CenterToScreen();
            txtSplitFileName.AllowDrop = true;
            txtJoinFileName.AllowDrop = true;
            string[] s = new string[cboDoEvents.Items.Count];
            cboDoEvents.Items.CopyTo(s, 0);
            cboDoEvents.Text = s[0];

            string[] args = Environment.GetCommandLineArgs();
            bool splitPassedFile = false;
            foreach (string arg in args)
            {
                if (splitPassedFile)
                {
                    OnLoad(arg);
                }

                if (arg == "SPLIT")
                {
                    splitPassedFile = true;
                }
            }
        }

        /// <summary>
        /// txtNumberOfBytesAfterSplit MouseDown event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtNumberOfBytesAfterSplit_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                cmnSize.Show(MousePosition.X, MousePosition.Y);
        }

        /// <summary>
        /// Custom piece size item mouse click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmiCustom_Click(object sender, EventArgs e)
        {
            FormPieceSizeDialog pieceSizeDialog = new FormPieceSizeDialog();
            if (pieceSizeDialog.ShowDialog() == DialogResult.OK)
                txtNumberOfBytesAfterSplit.Text = Convert.ToString(pieceSizeDialog.PieceSize);
        }

        /// <summary>
        /// 360KB piece size item mouse click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmi360KB_Click(object sender, EventArgs e)
        {
            const double d = 360 * 1024; //minimum
            pieceSize = Convert.ToInt64(d);
            if (pieceSize > fileSize)
                pieceSize = fileSize;
            txtNumberOfBytesAfterSplit.Text = Convert.ToString(pieceSize);
        }

        /// <summary>
        /// 720KB piece size item mouse click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmi720KB_Click(object sender, EventArgs e)
        {
            const double d = 720 * 1024;
            pieceSize = Convert.ToInt64(d);
            if (pieceSize > fileSize)
                pieceSize = fileSize;
            txtNumberOfBytesAfterSplit.Text = Convert.ToString(pieceSize);
        }

        /// <summary>
        /// 1.2MB piece size item mouse click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmi12MB_Click(object sender, EventArgs e)
        {
            const double d = 12 * 1024 * 1024 / 10;
            pieceSize = Convert.ToInt64(d);
            if (pieceSize > fileSize)
                pieceSize = fileSize;
            txtNumberOfBytesAfterSplit.Text = Convert.ToString(pieceSize);
        }

        /// <summary>
        /// 1.33MB piece size item mouse click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmi13MB_Click(object sender, EventArgs e)
        {
            const double d = 138 * 1024 * 1024 / 100; //3.5 inch floppy disk
            pieceSize = Convert.ToInt64(d);
            if (pieceSize > fileSize)
                pieceSize = fileSize;
            txtNumberOfBytesAfterSplit.Text = Convert.ToString(pieceSize);
        }

        /// <summary>
        /// On file load event handler
        /// </summary>
        /// <param name="filename"></param>
        void OnLoad(string filename)
        {
            try
            {
                txtSplitFileName.Text = filename;
                txtSplitFolder.Text = GetParentFolderPath(filename, "\\");
                if (txtSplitFolder.Text.Length < 3)
                    txtSplitFolder.Text += "\\";

                using (StreamReader reader = new StreamReader(filename))
                {
                    fileSize = reader.BaseStream.Length;
                    lblSplitFileSize.Text = rm.GetString("file_size") + ": " + fileSize.ToString();
                    reader.Close();
                }

                long newFileZise = ConvertToBytes(fileSize, true);

                txtNumberOfBytesAfterSplit.Text = newFileZise.ToString();
                txtNumberOfFiles.Text = "1";
                lblSplitNumberOfPieces.Text = rm.GetString("num_of_pieces") + ": 1";
            }
            catch
            {
            }
        }

        /// <summary>
        /// btnBrowseSplitFile mouse click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnBrowseSplitFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
                                    {
                                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                                        Title = rm.GetString("select_file_split")
                                    };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OnLoad(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// Returns filename path to the parent folder for specified file
        /// </summary>
        /// <param name="filename">Full path to filename file</param>
        /// <param name="separator">Separator used in the file path</param>
        /// <returns>A path to the parent folder for specified file</returns>
        string GetParentFolderPath(string filename, string separator)
        {
            string result = string.Empty;
            for (int i = filename.Length - separator.Length; i > 0; i--)
                if (filename.Substring(i, separator.Length) == separator)
                {
                    result = filename.Substring(0, i);
                    break;
                }
            return result;
        }

        /// <summary>
        /// Returns a file name for the specified file path
        /// </summary>
        /// <param name="filename">Full path to filename file</param>
        /// <param name="separator">Separator used in the file path</param>
        /// <returns>A file name for the specified file path</returns>
        string GetFileName(string filename, string separator)
        {
            string result = string.Empty;
            for (int i = filename.Length - separator.Length; i > 0; i--)
                if (filename.Substring(i, separator.Length) == separator)
                {
                    result = filename.Substring(i + 1);
                    break;
                }
            return result;
        }

        /// <summary>
        /// Converts passed filesize in KB, MB or GB to bytes
        /// </summary>
        /// <param name="filesize">filesize in KB, MB or GB</param>
        /// <param name="toKMG"></param>
        /// <returns>Filesize in bytes</returns>
        long ConvertToBytes(long filesize, bool toKMG)
        {
            //1 Kilobyte = 1024 Bytes
            //1 Megabyte = 1048576 Bytes
            //1 Gigabyte = 1073741824 Bytes

            long newSize;

            switch (cboFileSizes.SelectedItem.ToString())
            {
                case "KB":
                    {
                        if (toKMG)
                            newSize = filesize / 1024;
                        else
                        {
                            newSize = filesize * 1024;
                        }
                        break;
                    }
                case "MB":
                    {
                        if (toKMG)
                            newSize = filesize / 1048576;
                        else
                        {
                            newSize = filesize * 1048576;
                        }
                        break;
                    }
                case "GB":
                    {
                        if (toKMG)
                            newSize = filesize / 1073741824;
                        else
                        {
                            newSize = filesize * 1073741824;
                        }
                        break;
                    }
                default:
                    {
                        newSize = filesize;
                        break;
                    }
            }

            return newSize;
        }

        /// <summary>
        /// Calculates a number of parts for provided overall and piece size
        /// </summary>
        /// <param name="size">Overall size</param>
        /// <param name="piecesize">Piece size</param>
        /// <returns>Number of parts</returns>
        static int GetNumberOfParts(long size, long piecesize)
        {
            return (int)(Math.Ceiling(1.0 * size / piecesize));
        }

        /// <summary>
        /// Sets a number of files after the file is splitted for a specified piece size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtNumberOfBytesAfterSplit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtNumberOfBytesAfterSplit.Focused)
                {
                    if (!string.IsNullOrEmpty(txtNumberOfBytesAfterSplit.Text))
                    {
                        string partBytes = txtNumberOfBytesAfterSplit.Text;
                        if (partBytes.EndsWith("."))
                            partBytes += "0";

                        pieceSize = (long)(Convert.ToDouble(partBytes) * Math.Pow(1024, cboFileSizes.SelectedIndex));
                        if (fileSize < pieceSize)
                            pieceSize = 0;

                        if (pieceSize != 0)
                        {
                            partsQuantity = (int)Math.Ceiling(1.0 * fileSize / pieceSize);

                            if (fileSize < pieceSize)
                            {
                                txtNumberOfBytesAfterSplit.Text = fileSize.ToString();
                            }
                            //This is really not good code, cause TextBoxNumberOfFiles is going to fire an event now
                            lblSplitNumberOfPieces.Text = rm.GetString("num_pieces") + ": " + partsQuantity;
                            txtNumberOfFiles.Text = partsQuantity.ToString();
                        }
                        else
                        {
                            lblSplitNumberOfPieces.Text = rm.GetString("num_pieces") + ": 0";
                            txtNumberOfFiles.Text = "0";
                        }
                    }
                }
            }
            catch
            {
            }

            if (txtNumberOfFiles.Text == "1")
            {
                txtNumberOfFiles.Text = "0";
            }
        }

        /// <summary>
        /// Sets a number of files after the file is splitted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtNumberOfFiles_TextChanged(object sender, EventArgs e)
        {
            if (txtNumberOfFiles.Focused)
            {
                if (!string.IsNullOrEmpty(txtNumberOfFiles.Text))
                {
                    partsQuantity = Convert.ToInt32(txtNumberOfFiles.Text);
                    if (partsQuantity == 0)
                        partsQuantity = 1;
                    pieceSize = fileSize / partsQuantity;

                    if (fileSize % partsQuantity != 0)
                        pieceSize++;

                    txtNumberOfBytesAfterSplit.Text = ((headerSize + pieceSize) / Math.Pow(1024, cboFileSizes.SelectedIndex)).ToString("0.##"); // 10 for header

                    lblSplitNumberOfPieces.Text = rm.GetString("num_of_pieces") + ": " + (pieceSize != 0 ? partsQuantity.ToString() : "0");
                }
                else
                {
                    txtNumberOfBytesAfterSplit.Text = fileSize.ToString();
                }
            }
        }

        /// <summary>
        /// Supress some keys on txtNumberOfBytesAfterSplit KeyDown event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtNumberOfBytesAfterSplit_KeyDown(object sender, KeyEventArgs e)
        {
            lastNumOfBytesText = txtNumberOfBytesAfterSplit.Text;
            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                case Keys.Decimal:
                case Keys.Back:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.Delete:
                    break;
                case Keys.Enter:
                    break;
                default:
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        /// <summary>
        /// Supress some keys on txtNumberOfFiles KeyDown event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtNumberOfFiles_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                case Keys.Back:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.Delete:
                    break;
                case Keys.Enter:
                    break;
                default:
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        /// <summary>
        /// Browses for a folder for storing the files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnBrowseSplitFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog { ShowNewFolderButton = true, Description = rm.GetString("select_create_folder") };
            if (fbd.ShowDialog() == DialogResult.OK)
                txtSplitFolder.Text = fbd.SelectedPath;
        }

        /// <summary>
        /// Creates a directory for a specified path
        /// </summary>
        /// <param name="dirPath">path to the directory to create</param>
        /// <returns>True if directory exists or was created successfully, False if directory can not be created in a specified path</returns>
        bool CreateDir(string dirPath)
        {
            bool result = false;
            try
            {
                DirectoryInfo di = new DirectoryInfo(dirPath);
                if (di.Exists)
                    result = true;
                else if (
                    MessageBox.Show(rm.GetString("create_dictionary"), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    di.Create();
                    di = new DirectoryInfo(dirPath);
                    if (di.Exists)
                        result = true;
                    else
                    {
                        MessageBox.Show(rm.GetString("invalid_path"), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// Starts split operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSplitFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumberOfFiles.Text))
            {
                MessageBox.Show(rm.GetString("specify_parts"), Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (partsQuantity <= 1)
                    MessageBox.Show(rm.GetString("too_large"), Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (tslStatus.Text == rm.GetString("idle") & txtSplitFileName.Text != "" & txtSplitFolder.Text != "" &
                    partsQuantity > 1 & txtNumberOfBytesAfterSplit.Text != "")
                {

                    if (!Helper.IsEnoughSpace(txtSplitFolder.Text, txtSplitFileName.Text))
                    {
                        MessageBox.Show(rm.GetString("not_enough_disk_space"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    long size = (long)(Convert.ToDouble(txtNumberOfBytesAfterSplit.Text) * Math.Pow(1024, cboFileSizes.SelectedIndex));

                    if (CreateDir(txtSplitFolder.Text))
                    {
                        btnPauseSplitting.Enabled = true;
                        txtNumberOfBytesAfterSplit.Enabled = false;
                        cboFileSizes.Enabled = false;
                        txtNumberOfFiles.Enabled = false;
                        btnJoinFile.Enabled = false;
                        btnJoinBrowseFile.Enabled = false;
                        btnJoinBrowseFolder.Enabled = false;
                        try
                        {
                            Split();
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show(rm.GetString("split_interrupted"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            MessageBox.Show(rm.GetString("access_denied"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception)
                        {
                            // ToDo: send exception details via SmartAssembly bug reporting!
                        }

                        if (!pause)
                            ResetSplitForm();
                    }
                }
            }
        }

        /// <summary>
        /// Resets UI elements at the split form
        /// </summary>
        void ResetSplitForm()
        {
            // Reset TextBoxes
            txtSplitFileName.Text = string.Empty;
            txtSplitFolder.Text = string.Empty;
            txtNumberOfBytesAfterSplit.Text = string.Empty;
            txtNumberOfFiles.Text = string.Empty;
            txtNumberOfFiles.Enabled = false;
            txtNumberOfBytesAfterSplit.Enabled = false;

            // Reset Labels
            lblSplitFileSize.Text = rm.GetString("file_size") + ":";
            lblSplitNumberOfPieces.Text = rm.GetString("num_of_pieces") + ":";
            tslStatus.Text = rm.GetString("idle");

            // Reset CheckBoxes
            chkIsJoinFileGenerating.Checked = false;

            // Reset ComboBoxes
            cboFileSizes.SelectedIndex = 0;
            cboFileSizes.Enabled = false;
            cboDoEvents.Enabled = true;

            // Reset Buttons
            btnSplitFile.Enabled = false;
            btnPauseSplitting.Enabled = false;
            btnPauseSplitting.Text = rm.GetString("pause");
            btnJoinBrowseFile.Enabled = true;
            btnJoinBrowseFolder.Enabled = true;

            // Reset Progress
            prbSplitting.Value = 0;

            pause = false;
            part = 1;
            partIndex = 1;
        }

        /// <summary>
        /// First steps of a joining operation for a specified <paramref name="filename"/>
        /// </summary>
        /// <param name="filename"></param>
        void OnJoin(string filename)
        {
            txtJoinFileName.Text = filename;
            if (!File.Exists(filename))
                return;

            try
            {
                FileInfo fileInfo = new FileInfo(filename);
                binaryReader = new BinaryReader(File.OpenRead(filename));
                toJoin = new[]
			         	{
			         		fileInfo.Name.Remove(fileInfo.Name.LastIndexOf(fileInfo.Extension)),
			         		binaryReader.ReadUInt16().ToString()
			         	};
                binaryReader.Close();

                lblJoinFileName.Text = rm.GetString("file_name") + ": " + toJoin[0];
                lblJoinNumberOfPieces.Text = rm.GetString("num_pieces") + ": " + toJoin[1];

                txtJoinFolder.Text = GetParentFolderPath(filename, "\\");
                if (txtJoinFolder.Text.Length < 3)
                    txtJoinFolder.Text += "\\";
            }
            catch
            {
            }
        }

        /// <summary>
        /// Browses for the join file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnJoinBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
                            {
                                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                                Title = rm.GetString("select_join_file")
                            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.FileName;

                int ext = 0;
                string extension = Path.GetExtension(fileName).TrimStart('.');

                if (fileName.ToLower().EndsWith(".exe"))
                {
                    string part1FileName = fileName.Substring(0, fileName.LastIndexOf('.') + 1) + "1";
                    OnJoin(part1FileName);
                }
                else if (Int32.TryParse(extension, out ext))
                {
                    if (Helper.VerifyMinFilesCount(fileName))
                        OnJoin(fileName);
                    else
                        MessageBox.Show(rm.GetString("min_parts_count"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(rm.GetString("part_not_found"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Execute file joining
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnJoinFile_Click(object sender, EventArgs e)
        {
            if (tslStatus.Text == rm.GetString("idle") & !string.IsNullOrEmpty(txtJoinFileName.Text) & !string.IsNullOrEmpty(txtJoinFolder.Text))
            {
                short pieceCount = 0;
                Int16.TryParse(toJoin[1], out pieceCount);

                if (!Helper.IsEnoughSpace(txtJoinFolder.Text, txtJoinFileName.Text, pieceCount))
                {
                    MessageBox.Show(rm.GetString("not_enough_disk_space"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (CreateDir(txtJoinFolder.Text))
                {
                    // Check overwrite
                    string outPath = Path.Combine(txtJoinFolder.Text, lblJoinFileName.Text.Split(':')[1].Trim());
                    if (File.Exists(outPath))
                    {
                        DialogResult result = MessageBox.Show(rm.GetString("overwrite_confirm"), Text, MessageBoxButtons.YesNo);
                        if (result == System.Windows.Forms.DialogResult.No)
                            return;
                    }

                    btnPauseJoining.Enabled = true;
                    btnSplitFile.Enabled = false;
                    btnBrowseSplitFile.Enabled = false;
                    btnBrowseSplitFolder.Enabled = false;

                    try
                    {
                        Join();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(rm.GetString("split_interrupted"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        MessageBox.Show(rm.GetString("access_denied"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception)
                    {
                        // ToDo: send exception details via SmartAssembly bug reporting!
                    }

                    if (!pause)
                        ResetJoinForm();
                }
            }
        }

        /// <summary>
        /// Resets UI elements at the join form
        /// </summary>
        void ResetJoinForm()
        {
            // Reset TextBoxes
            txtJoinFileName.Text = string.Empty;
            txtJoinFolder.Text = string.Empty;

            // Reset Labels
            lblJoinFileName.Text = rm.GetString("file_name") + ":";
            lblJoinNumberOfPieces.Text = rm.GetString("num_of_pieces") + ":";
            tslStatus.Text = rm.GetString("idle");
            btnPauseJoining.Text = rm.GetString("pause");

            // Reset CheckBoxes
            cboIsPiecesDeletedAfterJoining.Checked = true;

            // Reset Buttons
            btnJoinFile.Enabled = false;
            btnPauseJoining.Enabled = false;
            btnBrowseSplitFile.Enabled = true;
            btnBrowseSplitFolder.Enabled = true;

            // Reset Comboboxes
            cboDoEvents.Enabled = true;

            // Reset Progress
            prbJoining.Value = 0;

            pause = false;
            part = 1;
            partIndex = 1;
        }

        /// <summary>
        /// Reads a file to join
        /// </summary>
        /// <param name="path"></param>
        /// <returns>File content as a <c>string[]</c></returns>
        string[] LoadFile(string path)
        {
            int linesCount = 0;
            string[] file;

            using (StreamReader streamReader = new StreamReader(path, true)) ;
            {
                while ((streamReader.ReadLine()) != null)
                    linesCount++;
                streamReader.Close();
            }

            using (StreamReader streamReader = new StreamReader(path, true))
            {
                streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                file = new string[linesCount];
                for (int i = 0; i < file.Length; i++)
                {
                    file[i] = streamReader.ReadLine();
                }
                streamReader.Close();
            }

            return file;
        }

        /// <summary>
        /// txtSplitFileName DragDrop event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtSplitFileName_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text)) return;
            e.Effect = DragDropEffects.None;
            string[] filenames = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (filenames != null) OnLoad(filenames[0]);
        }

        /// <summary>
        /// txtJoinFileName DragDrop event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtJoinFileName_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.None;
                string[] filenames = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (filenames != null)
                {
                    string filetype = filenames[0].Substring(filenames[0].LastIndexOf(@"\") + 1);
                    if (filetype.IndexOf(".Join") != -1)
                        OnJoin(filenames[0]);
                    else
                        MessageBox.Show(rm.GetString("not_join_file"), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// Makes txtSplitFileName readonly without setting its readonly property to true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtSplitFileName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        /// <summary>
        /// Makes txtBoxJoinFileName readonly without setting its readonly property to true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtBoxJoinFileName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        /// <summary>
        /// Saves a join file in a folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnJoinBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog { ShowNewFolderButton = true, Description = rm.GetString("select_create_folder_join") };
            if (fbd.ShowDialog() == DialogResult.OK)
                txtJoinFolder.Text = fbd.SelectedPath;
        }

        /// <summary>
        /// Pauses a split operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSplitPause_Click(object sender, EventArgs e)
        {
            if (btnSplitFile.Enabled) return;
            if (btnPauseSplitting.Text == rm.GetString("pause"))
            {
                pause = true;
                btnPauseSplitting.Text = rm.GetString("resume");
                tslStatus.Text = rm.GetString("paused");
            }
            else
            {
                if (tslStatus.Text == rm.GetString("paused"))
                {
                    pause = false;
                    btnPauseSplitting.Text = rm.GetString("pause");
                    try
                    {
                        Split();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(rm.GetString("split_interrupted"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        MessageBox.Show(rm.GetString("access_denied"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (!pause)
                        ResetSplitForm();
                }
            }
        }

        /// <summary>
        /// Pauses a join operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnJoinPause_Click(object sender, EventArgs e)
        {
            if (!btnJoinFile.Enabled)
            {
                if (btnPauseJoining.Text == rm.GetString("pause"))
                {
                    pause = true;
                    btnPauseJoining.Text = rm.GetString("resume");
                    tslStatus.Text = rm.GetString("paused");
                }
                else
                {
                    pause = false;
                    if (tslStatus.Text == rm.GetString("paused"))
                    {
                        btnPauseJoining.Text = rm.GetString("pause");
                        try
                        {
                            Join();
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show(rm.GetString("join_interrupted"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            MessageBox.Show(rm.GetString("access_denied"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        if (!pause)
                            ResetJoinForm();
                    }
                }
            }
        }

        /// <summary>
        /// txtSplitFileName DragEnter event handler.
        /// Can be dragdrop required if autodragdrop = false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtSplitFileName_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// txtJoinFileName DragEnter event handler.
        /// Can be dragdrop required if autodragdrop = false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtJoinFileName_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// Used to free system resources at main form closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tslStatus.Text == rm.GetString("idle")) return;
            if (tslStatus.Text == rm.GetString("busy"))
            {
                btnSplitPause_Click(sender, e);
                btnJoinPause_Click(sender, e);
            }
            if (
                MessageBox.Show(rm.GetString("application_paused"), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
            if (!e.Cancel)
            {
                if (closeStreamReader)
                    streamReader.Close();
                if (closeStreamWriter)
                    streamWriter.Close();
            }
        }

        /// <summary>
        /// Split procedure with pause capabilities
        /// </summary>
        void Split()
        {
            try
            {
                tslStatus.Text = rm.GetString("busy");
                if (part == 1 & partIndex == 1 & !pause)
                {
                    cboDoEvents.Enabled = false;
                    btnSplitFile.Enabled = false;
                    streamReader = new StreamReader(txtSplitFileName.Text);
                    closeStreamReader = true;
                    outputFileName = txtSplitFolder.Text;
                    if (outputFileName.Length > 0)
                        if (outputFileName.Substring(outputFileName.Length - 1) == "\\")
                            outputFileName = outputFileName.Substring(0, outputFileName.Length - 1);
                }
                if (streamReader.BaseStream.Length == fileSize)
                {
                    //in caz ca fisierul de impartit nu StreamReader-filename modificat
                    while (part <= partsQuantity && !pause)
                    {
                        pieceSize = (long)(double.Parse(txtNumberOfBytesAfterSplit.Text) * Math.Pow(1024, cboFileSizes.SelectedIndex) - headerSize);
                        if (fileSize < pieceSize)
                            pieceSize = 0;

                        if (!pause)
                        {
                            if (part * pieceSize > fileSize)
                                pieceSize = fileSize - (part - 1) * pieceSize;
                            //refresh la fiecare 5 MB sau la valoarea ipieces
                            if (partIndex == 1)
                            {
                                FileStream outStream =
                                    File.Open(outputFileName + "\\" + GetFileName(txtSplitFileName.Text, "\\") + "." + part.ToString(),
                                              FileMode.Create);
                                streamWriter = new StreamWriter(outStream);
                                binaryWriter = new BinaryWriter(outStream);
                                binaryWriter.Write(Convert.ToInt16(partsQuantity));
                                binaryWriter.Write(Convert.ToUInt64(fileSize));
                                closeStreamWriter = true;
                                smallPartsCount = GetNumberOfParts(pieceSize, pieceIndex);
                            }

                            while (partIndex <= smallPartsCount)
                            {
                                if (!pause)
                                {
                                    long trueipiece = pieceIndex;
                                    if (partIndex * pieceIndex > pieceSize)
                                        pieceIndex = pieceSize - (partIndex - 1) * pieceIndex;
                                    for (long ii = 0; ii < pieceIndex; ii++)
                                        streamWriter.BaseStream.WriteByte((byte)streamReader.BaseStream.ReadByte());
                                    pieceIndex = trueipiece;
                                    prbSplitting.Value =
                                        Convert.ToInt32((part - 1) * 100 / partsQuantity + (partIndex * 100 / smallPartsCount) / partsQuantity);
                                    Application.DoEvents();
                                    partIndex++;
                                }
                                else
                                    break;
                            }

                            if (!pause)
                            {
                                partIndex = 1;
                                streamWriter.Close();
                                closeStreamWriter = false;
                                prbSplitting.Value = Convert.ToInt32(part * 100 / partsQuantity);
                                Application.DoEvents();
                                part++;
                            }
                        }
                        else
                            break;
                    }
                    //code below this line will execute only at the end
                    if (!pause)
                    {
                        streamReader.Close();
                        closeStreamReader = false;
                        MessageBox.Show(rm.GetString("splitted"), Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (chkIsJoinFileGenerating.Checked)
                        {
                            if (File.Exists(outputFileName + "\\" + GetFileName(txtSplitFileName.Text, "\\") + ".exe"))
                                File.Delete(outputFileName + "\\" + GetFileName(txtSplitFileName.Text, "\\") + ".exe");

                            string filepath = AppDomain.CurrentDomain.BaseDirectory;

                            File.Copy(filepath + "\\Joiner.exe", outputFileName + "\\" + GetFileName(txtSplitFileName.Text, "\\") + ".exe");
                            MessageBox.Show(rm.GetString("self_joining_created"), Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                    MessageBox.Show(rm.GetString("file_size_changed"), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch
            {
            }
        }

        //Split succeded

        /// <summary>
        /// Join procedure with refresh capabilities
        /// </summary>
        void Join()
        {
            try
            {
                tslStatus.Text = rm.GetString("busy"); //must do this every time
                if (part == 1 & partIndex == 1 & !pause) //do just first time
                {
                    cboDoEvents.Enabled = false;
                    btnJoinFile.Enabled = false;
                    inputFileName = GetParentFolderPath(txtJoinFileName.Text, "\\") + "\\";
                    string saveFolder = txtJoinFolder.Text;
                    if (saveFolder.Substring(saveFolder.Length - 1) != "\\")
                        saveFolder += "\\";
                    streamWriter = new StreamWriter(saveFolder + toJoin[0]);
                    closeStreamWriter = true;
                    partsQuantity = Convert.ToInt32(toJoin[1]);
                }

                while (part <= partsQuantity && !pause)
                {
                    if (!pause)
                    {
                        //vreau un refresh o dată la fiecare 5 MB valoarea lui PieceIndex
                        //iar cod ce trebuie executat doar la inceputul bucatii
                        if (partIndex == 1)
                        {
                            FileStream inStream = File.OpenRead(inputFileName + toJoin[0] + "." + part.ToString());
                            streamReader = new StreamReader(inStream);
                            inStream.Seek(headerSize, SeekOrigin.Begin); // skip header
                            closeStreamReader = true;
                            pieceSize = streamReader.BaseStream.Length - headerSize;
                            smallPartsCount = GetNumberOfParts(pieceSize, pieceIndex);
                        }

                        while (partIndex <= smallPartsCount)
                        {
                            if (!pause)
                            {
                                long truePiece = pieceIndex;
                                if (partIndex * pieceIndex > pieceSize)
                                    pieceIndex = pieceSize - (partIndex - 1) * pieceIndex;
                                for (long ii = 0; ii < pieceIndex; ii++)
                                    streamWriter.BaseStream.WriteByte((byte)streamReader.BaseStream.ReadByte());
                                pieceIndex = truePiece;
                                prbJoining.Value = Convert.ToInt32((part - 1) * 100 / partsQuantity + (partIndex * 100 / smallPartsCount) / partsQuantity);
                                Application.DoEvents();
                                partIndex++;
                            }
                            else
                                break;
                        }
                        if (!pause) //cod ce se executa la finalul fiecarei parti
                        {
                            partIndex = 1;
                            streamReader.Close();
                            closeStreamReader = false;
                            prbJoining.Value = Convert.ToInt32(part * 100 / partsQuantity);
                            Application.DoEvents();
                            part++;
                        }
                    }
                    else
                        break;
                }
                //code below this will execute only at the end of the process
                if (!pause)
                {
                    streamWriter.Close();
                    closeStreamWriter = false;
                    MessageBox.Show(rm.GetString("joined"), Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (cboIsPiecesDeletedAfterJoining.Checked)
                    {
                        for (int i = 1; i <= partsQuantity; i++)
                            File.Delete(inputFileName + toJoin[0] + "." + i.ToString());
                        File.Delete(outputFileName + "\\" + GetFileName(txtSplitFileName.Text, "\\") + ".exe");
                        File.Delete(outputFileName + "\\" + toJoin[0] + ".exe");
                        MessageBox.Show(rm.GetString("pieces_deleted"), Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtJoinFileName.Text = string.Empty;
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(rm.GetString("part_not_found"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //streamWriter.Close();
                //closeStreamWriter = false;
            }
            //Debug();
        }

        //join succeded

        /// <summary>
        /// Manage speed and refresh rate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboDoEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboDoEvents.Text)
            {
                case " 1 MB":
                    pieceIndex = 1024 * 1024;
                    break;
                case " 5 MB":
                    pieceIndex = 5 * 1024 * 1024;
                    break;
                case "15 MB":
                    pieceIndex = 15 * 1024 * 1024;
                    break;
                case "25 MB":
                    pieceIndex = 25 * 1024 * 1024;
                    break;
                case "35 MB":
                    pieceIndex = 35 * 1024 * 1024;
                    break;
                case "45 MB":
                    pieceIndex = 45 * 1024 * 1024;
                    break;
            }
        }

        /// <summary>
        /// cboFileSizes SelectedIndexChanged event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboFileSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumberOfBytesAfterSplit.Text))
            {
                pieceSize = (long)(Convert.ToDouble(txtNumberOfBytesAfterSplit.Text) * Math.Pow(1024, lastSelectedIndex));
                if (fileSize < pieceSize)
                    pieceSize = 0;

                double factor = Math.Pow(1024, cboFileSizes.SelectedIndex);
                txtNumberOfBytesAfterSplit.Text = (pieceSize / factor).ToString("0.##");

                if (pieceSize != 0)
                {
                    partsQuantity = (int)Math.Ceiling(1.0 * fileSize / pieceSize);

                    if (fileSize < pieceSize)
                        txtNumberOfBytesAfterSplit.Text = fileSize.ToString();

                    //This is really not good code, cause TextBoxNumberOfFiles is going to fire an event now
                    lblSplitNumberOfPieces.Text = rm.GetString("num_pieces") + ": " + partsQuantity;
                    txtNumberOfFiles.Text = partsQuantity.ToString();
                }
                else
                {
                    lblSplitNumberOfPieces.Text = rm.GetString("num_pieces") + ": 0";
                    txtNumberOfFiles.Text = "0";
                }
            }
            lastSelectedIndex = cboFileSizes.SelectedIndex;
        }

        /// <summary>
        /// Pauses/resumes split and join operations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnPauseResume_Click(object sender, EventArgs e)
        {
            btnSplitPause_Click(sender, e);
            btnJoinPause_Click(sender, e);
        }

        /// <summary>
        /// Refresh debug info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ButtonRefreshDebugInfo_Click(object sender, EventArgs e)
        {
            //Debug();
        }

        /// <summary>
        /// txtSplitFileName TextChanged event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtSplitFileName_TextChanged(object sender, EventArgs e)
        {
            bool isEnable = true;
            if (string.IsNullOrEmpty(txtSplitFileName.Text) || string.IsNullOrEmpty(txtSplitFolder.Text))
                isEnable = false;

            btnSplitFile.Enabled = isEnable;
            txtNumberOfBytesAfterSplit.Enabled = isEnable;
            txtNumberOfFiles.Enabled = isEnable;
            cboFileSizes.Enabled = isEnable;
        }

        /// <summary>
        /// txtJoinFileName TextChanged event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtJoinFileName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJoinFileName.Text) ||
                string.IsNullOrEmpty(txtJoinFolder.Text))
                btnJoinFile.Enabled = false;
            else
                btnJoinFile.Enabled = true;
        }

        /// <summary>
        /// Sets a specified <paramref name="culture"/> to a current thread
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            var rm = new ResourceManager("FileSplitterAndJoiner.Resources", typeof(FormMain).Assembly);
            Thread.CurrentThread.CurrentUICulture = culture;

            lblBiggerIsFaster.Text = rm.GetString("bigger_faster");
            btnRefreshDebugInfo.Text = rm.GetString("refresh_debug_info");
            lblNotRespond.Text = rm.GetString("may_not_respond");
            lblDoEvents.Text = rm.GetString("do_events") + ":";
            btnPauseResume.Text = rm.GetString("pause_resume");
            grbDebugVariables.Text = rm.GetString("debug_vars");
            tslStatus.Text = rm.GetString("idle");
            chkIsJoinFileGenerating.Text = rm.GetString("add_self_joining");
            btnSplitFile.Text = rm.GetString("split_file");
            lblSplitNumberOfPieces.Text = rm.GetString("num_of_pieces") + ":";
            lblSplitFileSize.Text = rm.GetString("file_size") + ":";
            cmi360KB.Text = rm.GetString("s360_min");
            cmiCustom.Text = rm.GetString("custom");
            grbSplitFile.Text = rm.GetString("select_file_split");
            grbSplitFolder.Text = rm.GetString("save_pieces");
            grbNumberOfBytes.Text = rm.GetString("data_size_splits_after");
            grbSplitInfo.Text = rm.GetString("split_info");
            tbpFileSplitter.Text = rm.GetString("file_splitter");
            grbNumberOfFiles.Text = rm.GetString("num_of_files");
            btnPauseSplitting.Text = rm.GetString("pause");
            tbpFileJoiner.Text = rm.GetString("file_joiner");
            btnPauseJoining.Text = rm.GetString("pause");
            grbJoinInfo.Text = rm.GetString("joining_info");
            lblJoinFileName.Text = rm.GetString("file_name") + ":";
            lblJoinNumberOfPieces.Text = rm.GetString("num_of_pieces") + ":";
            cboIsPiecesDeletedAfterJoining.Text = rm.GetString("delete_pieces");
            grbJoinFolder.Text = rm.GetString("save_joined_file");
            grbJoinFile.Text = rm.GetString("browse_for_part");
            btnJoinFile.Text = rm.GetString("join_file");
            Text = rm.GetString("file_splitter_joiner");
            ucTop.Text = rm.GetString("file_splitter_joiner_title");
        }

        #endregion

        private void txtNumberOfBytesAfterSplit_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtNumberOfBytesAfterSplit.Text == string.Empty)
                return;

            double dummy;
            if (!double.TryParse(txtNumberOfBytesAfterSplit.Text, out dummy))
            {
                txtNumberOfBytesAfterSplit.Text = lastNumOfBytesText;
            }
        }
    }
}