using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace FileEraser
{
    /// <summary>
    /// Encrypt and Decrypt knot main form
    /// </summary>
    public partial class FormMain : Form
    {
        #region Declarations

        readonly cShredder oShredder = new cShredder();
        int deletedFilesCount;
        bool blnKillProcess;
        bool isScanning;
        HelpProvider nshredHelp;

        bool IsScanning
        {
            get { return isScanning; }
            set
            {
                isScanning = value;
                if (optFolder.Checked)
                {
                    cmdShred.Text = rm.GetString(isScanning ? "abort_it" : "shred_it");
                }
                else
                {
                    cmdShred.Enabled = isScanning ? false : true;
                }
                grbSelectFile.Enabled = isScanning ? false : true;
            }
        }

        #endregion

        #region Help

        /// <summary>
        /// setup help messages
        /// </summary>
        void HelpSetup()
        {
            nshredHelp = new HelpProvider();
            nshredHelp.SetShowHelp(tbPath, true);
            nshredHelp.SetHelpString(tbPath, rm.GetString("enter_file_name"));

            nshredHelp.SetShowHelp(cmdSelect, true);
            nshredHelp.SetHelpString(cmdSelect, rm.GetString("open_file_select"));

            nshredHelp.SetShowHelp(optFile, true);
            nshredHelp.SetHelpString(optFile, rm.GetString("use_shred_file"));

            nshredHelp.SetShowHelp(optFolder, true);
            nshredHelp.SetHelpString(optFolder, rm.GetString("use_shred_files"));

            nshredHelp.SetShowHelp(chkCloseInstance, true);
            nshredHelp.SetHelpString(chkCloseInstance, rm.GetString("terminate_running"));

            nshredHelp.SetShowHelp(chkSubfolders, true);
            nshredHelp.SetHelpString(chkSubfolders, rm.GetString("delete_files_parent"));

            nshredHelp.SetShowHelp(cmdShred, true);
            nshredHelp.SetHelpString(cmdShred, rm.GetString("begin_shredding"));
        }

        #endregion

        #region StartUp

        /// <summary>
        /// constructor for frmMain
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// initialize frmMain
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmMain_Load(object sender, EventArgs e)
        {
            SetCulture(new CultureInfo(CfgFile.Get("Lang")));

            cWin32 oWin32 = new cWin32();
            if (!oWin32.VersionCheck())
            {
                MessageBox.Show(rm.GetString("system_not_supported"), rm.GetString("os_not_supported"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            if (!oShredder.IsAdmin())
            {
                MessageBox.Show(rm.GetString("no_sufficient_access"), rm.GetString("contact_admin"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            oShredder.FileCount += evtFileCount;
            oShredder.Status += EvtStatus;
            stInfo.Items.Add(rm.GetString("select_file") + "...");
            HelpSetup();
            if (Program.ParanoidMode)
            {
                Text += " - " + rm.GetString("pnm_enabled");
            }

            string[] args = Environment.GetCommandLineArgs();
            bool wipePassedFile = false;
            foreach (string arg in args)
            {
                if (wipePassedFile)
                {
                    tbPath.Text = arg;

                    chkCloseInstance.Enabled = arg.EndsWith(".exe");
                    if (!chkCloseInstance.Enabled) chkCloseInstance.Checked = false;

                    if (tbPath.Text.Length > 3)
                        stInfo.Items[0].Text = rm.GetString("prepared_shred") + " " + tbPath.Text;
                }

                if (arg == "WIPE")
                {
                    wipePassedFile = true;
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
            tmiAbout.Text = rm.GetString("about_us");
            tmiVisit.Text = rm.GetString("visit_us");
            tmiExit.Text = rm.GetString("exit");
            grbSelectFile.Text = rm.GetString("file_selection");
            optFile.Text = rm.GetString("delete_file");
            optFolder.Text = rm.GetString("delete_files_directory");
            grbOptions.Text = rm.GetString("deletion_options");
            chkCloseInstance.Text = rm.GetString("close_running_instances");
            chkSubfolders.Text = rm.GetString("parse_subfolders");
            cmdShred.Text = rm.GetString("shred_it");
            Text = rm.GetString("file_shredder");
            ucTop.Text = rm.GetString("file_shredder");
            chkDelDir.Text = rm.GetString("eliminate_folders");
        }

        #endregion

        #region Events

        /// <summary>
        /// increase progress bar value by 1
        /// </summary>
        void evtProgressTick()
        {
            pbProgress.Value += 1;
        }

        /// <summary>
        /// change progress bar value to minimum value
        /// </summary>
        void evtComplete()
        {
            pbProgress.Value = pbProgress.Minimum;
        }

        /// <summary>
        /// show a confirmation message to delete files
        /// </summary>
        /// <param name="count"></param>
        /// <param name="cancel"></param>
        void evtFileCount(Int32 count, ref Boolean cancel)
        {
            if (count > 10)
                if (MessageBox.Show(string.Format("{0} {1} {2}. {3}", rm.GetString("attempting"), count.ToString(), rm.GetString("files"), rm.GetString("proceed")),
                    rm.GetString("deleting_files"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cancel = false;
                }
        }

        void EvtStatus(Int32 status, string message)
        {
            stInfo.Items[0].Text = message;
        }

        #endregion

        #region Methods

        /// <summary>
        /// handle Click event to select file or folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmdSelect_Click(object sender, EventArgs e)
        {
            if (optFile.Checked)
            {
                try
                {
                    OpenFileDialog ofdFile = new OpenFileDialog();
                    ofdFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    ofdFile.Filter = "any file (*.*)|*.*";
                    ofdFile.FilterIndex = 1;
                    ofdFile.RestoreDirectory = true;
                    if (ofdFile.ShowDialog() == DialogResult.OK)
                    {
                        tbPath.Text = ofdFile.FileName;
                        chkCloseInstance.Enabled = ofdFile.FileName.EndsWith(".exe");
                        if (!chkCloseInstance.Enabled) chkCloseInstance.Checked = false;
                    }
                    if (tbPath.Text.Length > 3)
                        stInfo.Items[0].Text = rm.GetString("prepared_shred") + " " + tbPath.Text;
                }
                catch (Exception)
                {
                    // ToDo: send exception details via SmartAssembly bug reporting!
                }
            }
            else
            {
                var fbFolder = new FolderBrowserDialog();
                fbFolder.ShowNewFolderButton = false;
                if (fbFolder.ShowDialog() == DialogResult.OK)
                {
                    tbPath.Text = fbFolder.SelectedPath;
                }
                if (tbPath.Text.Length > 3)
                    stInfo.Items[0].Text = rm.GetString("prepared_process") + " " + tbPath.Text;
            }
        }

        /// <summary>
        /// handle Click event to delete file or folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmdShred_Click(object sender, EventArgs e)
        {
            if (IsScanning)
            {
                blnKillProcess = true;
            }
            else
            {
                Func(tbPath.Text);
                tbPath.Text = String.Empty;
            }
        }

        /// <summary>
        /// delete file or folder
        /// </summary>
        /// <param name="txtPath"></param>
        void Func(string txtPath)
        {
            try
            {
                if (txtPath.Length == 0)
                {
                    MessageBox.Show(rm.GetString("select_file_folder"), rm.GetString("invalid_path"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                stInfo.Items[0].Text = rm.GetString("scanning") + "...";
                IsScanning = true;
                pbProgress.Visible = true;
                try
                {
                    pbProgress.Maximum = GetTotalNumberFilesToBeDeleted(txtPath);
                }
                catch (Exception)
                {
                    IsScanning = false;
                    pbProgress.Maximum = 1;
                }
                if (blnKillProcess)
                {
                    FinalizeShredProcess();
                    return;
                }
                FileAttributes attr = File.GetAttributes(txtPath);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    var path = new DirectoryInfo(txtPath);
                    if (chkSubfolders.Checked)
                    {
                        DeleteSubDirectories(path);
                        if (chkDelDir.Checked)
                        {
                            for (int tries = 0; tries < 3; tries++)
                            {
                                try
                                {
                                    path.Delete(true);
                                    Thread.Sleep(100);
                                }
                                catch { }
                            }
                        }
                    }
                    else
                    {
                        foreach (FileInfo finfo in path.GetFiles())
                        {
                            if (blnKillProcess)
                            {
                                FinalizeShredProcess();
                                return;
                            }
                            DeleteFile(finfo.FullName);
                            evtProgressTick();
                        }
                    }
                }
                else
                {
                    DeleteFile(txtPath);
                }
                FinalizeShredProcess();
            }
            catch (Exception)
            {
                FinalizeShredProcess();
            }
        }

        /// <summary>
        /// delete all files in specific directory
        /// </summary>
        /// <param name="path"></param>
        void DeleteSubDirectories(DirectoryInfo path)
        {
            try
            {
                foreach (FileInfo finfo in path.GetFiles())
                {
                    if (blnKillProcess)
                    {
                        FinalizeShredProcess();
                        return;
                    }
                    DeleteFile(finfo.FullName);
                    evtProgressTick();
                }
                foreach (DirectoryInfo dinfo in path.GetDirectories())
                {
                    if (blnKillProcess)
                    {
                        return;
                    }
                    DeleteSubDirectories(dinfo);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Finalize Shred Process.
        /// </summary>
        void FinalizeShredProcess()
        {
            blnKillProcess = false;
            lblFileName.Text = string.Empty;
            stInfo.Items.Clear();
            stInfo.Items.Add(rm.GetString("select_file") + "...");
            evtComplete();
            pbProgress.Visible = false;
            pbProgress.Value = 0;
            IsScanning = false;
            MessageBox.Show(String.Format(rm.GetString("file_shredded"), deletedFilesCount),
                            String.Format(rm.GetString("file_shredded"), deletedFilesCount),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            deletedFilesCount = 0;
        }

        /// <summary>
        /// count the number of files to be deleted
        /// </summary>
        /// <param name="txtPath"></param>
        /// <returns></returns>
        int GetTotalNumberFilesToBeDeleted(String txtPath)
        {
            int totalNumberOfFiles = 0;
            try
            {
                DirectoryInfo path = new DirectoryInfo(txtPath);
                Application.DoEvents();
                if (chkSubfolders.Checked)
                {
                    foreach (DirectoryInfo dinfo in path.GetDirectories())
                    {
                        if (blnKillProcess)
                        {
                            return -1;
                        }
                        totalNumberOfFiles += GetTotalNumberFilesToBeDeleted(dinfo.FullName);
                    }
                }
                totalNumberOfFiles += path.GetFiles().Length;
            }
            catch
            {
            }
            return totalNumberOfFiles;
        }

        /// <summary>
        /// Get File name to be shown in status bar
        /// </summary>
        string GetStatusBarFilePath(string filePath)
        {
            StringBuilder strfilePath = new StringBuilder();
            if (filePath.Length <= 60)
            {
                strfilePath.Append(filePath);
            }
            else
            {
                string lastString = string.Empty;
                string[] strArr = filePath.Split('\\');

                if (strArr.Length > 2)
                {
                    lastString = strArr[strArr.Length - 2] + "\\" + strArr[strArr.Length - 1];
                }

                foreach (string t in strArr)
                {
                    if (strfilePath.Length + lastString.Length + t.Length > 60)
                    {
                        strfilePath.Append("...\\" + lastString);
                        break;
                    }
                    strfilePath.Append(t + "\\");
                }
            }
            return strfilePath.ToString();
        }

        /// <summary>
        /// delete specific file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        void DeleteFile(string filePath)
        {
            if (optFolder.Checked)
                lblFileName.Text = GetStatusBarFilePath(filePath);
            Application.DoEvents();
            oShredder.AnyAttribute = true;
            oShredder.DeleteDirectory = false;
            oShredder.DeleteSubDirectories = (chkSubfolders.Checked);
            oShredder.CloseInstance = (chkCloseInstance.Checked);
            oShredder.FilePath = filePath;

            if (oShredder.startShredder())
            {
                deletedFilesCount++;
                return;
            }
            return;
        }

        /// <summary>
        /// handle CheckedChanged event to change help text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void optFolder_CheckedChanged(object sender, EventArgs e)
        {
            tbPath.Text = String.Empty;
            chkSubfolders.Enabled = true;
            chkDelDir.Enabled = chkSubfolders.Checked;
            stInfo.Items[0].Text = rm.GetString("select_folder") + "..";
        }

        /// <summary>
        /// handle CheckedChanged event to change help text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void optFile_CheckedChanged(object sender, EventArgs e)
        {
            tbPath.Text = String.Empty;
            chkSubfolders.Enabled = false;
            chkDelDir.Enabled = false;
            stInfo.Items[0].Text = rm.GetString("select_file") + "..";
        }

        /// <summary>
        /// handle CheckedChanged to enable/disable eliminate folders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSubfolders_CheckedChanged(object sender, EventArgs e)
        {
            chkDelDir.Enabled = chkSubfolders.Checked;
        }

        #endregion
    }
}