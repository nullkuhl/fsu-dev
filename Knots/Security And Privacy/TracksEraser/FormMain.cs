using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using Microsoft.Win32;
using UrlHistoryLibrary;

namespace FreemiumUtilities.TracksEraser
{
    /// <summary>
    /// Main form of the Tracks Eraser knot
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// Empty Recycle Bin flags
        /// </summary>
        const int SHERB_NOCONFIRMATION = 0x00000001;

        const int SHERB_NOPROGRESSUI = 0x00000002;
        const int SHERB_NOSOUND = 0x00000004;

        /// <summary>
        /// URL history wrapper
        /// </summary>
        public static UrlHistoryWrapperClass.STATURLEnumerator Enumerator;

        /// <summary>
        /// URL history wrapper
        /// </summary>
        public static UrlHistoryWrapperClass UrlHistory;

        CultureInfo culture;

        List<string> filesToDelete;
        List<string> foldersToDelete;

        ulong recycleBinCount;
        ulong recycleBinSize;
        List<string> regValuesToDelete;

        //Now the Win32 API

        /// <summary>
        /// constructor for FormMain
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shell query for the Windows Recycle Bin
        /// </summary>
        /// <param name="pszRootPath"></param>
        /// <param name="pSHQueryRBInfo"></param>
        /// <returns></returns>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHQueryRecycleBin(string pszRootPath, ref SHQUERYRBINFO pSHQueryRBInfo);

        [DllImport("shell32.dll")]
        public static extern int SHEmptyRecycleBin(IntPtr hWnd, string pszRootPath, uint dwFlags);

        /// <summary>
        /// load the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                RunEraser.Enabled = false;
                culture = new CultureInfo(CfgFile.Get("Lang"));
                SetCulture(culture);

                filesToDelete = new List<string>();
                regValuesToDelete = new List<string>();
                foldersToDelete = new List<string>();
                UrlHistory = new UrlHistoryWrapperClass();
                Enumerator = UrlHistory.GetEnumerator();

                //TreeNode node = new TreeNode("Win");
                //this.treeView1.Nodes.Add(node);

                trvTracks.ExpandAll();
                trvTracks.SelectedNode = trvTracks.Nodes[0];

                GetInstalledApps();

                Process[] pname = Process.GetProcessesByName("firefox");
                if (pname.Length > 0)
                {
                    trvTracks.Nodes[3].ForeColor = Color.DarkGray;
                    for (int i = 0; i < trvTracks.Nodes[3].Nodes.Count; i++)
                    {
                        trvTracks.Nodes[3].Nodes[i].ForeColor = Color.DarkGray;
                        trvTracks.Nodes[3].Nodes[i].ImageIndex = 7;
                    }
                }

                pname = Process.GetProcessesByName("chrome");
                if (pname.Length > 0)
                {
                    trvTracks.Nodes[4].ForeColor = Color.DarkGray;
                    for (int i = 0; i < trvTracks.Nodes[4].Nodes.Count; i++)
                    {
                        trvTracks.Nodes[4].Nodes[i].ForeColor = Color.DarkGray;
                        trvTracks.Nodes[4].Nodes[i].ImageIndex = 7;
                    }
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        /// <summary>
        /// change current language
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;

            tlsMain.Text = rm.GetString("toolStrip");
            tsbAnalyse.Text = rm.GetString("analyse");
            tsbErase.Text = rm.GetString("erase_check_track");
            tsbOptions.Text = rm.GetString("options");
            tsbCheck.Text = rm.GetString("check");
            tmiCheckAll.Text = rm.GetString("check_all");
            tmiUncheckAll.Text = rm.GetString("check_none");
            tbpTracks.Text = rm.GetString("tracks");
            trvTracks.Nodes[0].Nodes[0].Text = rm.GetString("win_rec_doc");
            trvTracks.Nodes[0].Nodes[1].Text = rm.GetString("win_st_menu_run");
            trvTracks.Nodes[0].Nodes[2].Text = rm.GetString("file_folder_list");
            trvTracks.Nodes[0].Nodes[3].Text = rm.GetString("win_clipboard");
            trvTracks.Nodes[0].Nodes[4].Text = rm.GetString("win_temp_files");
            trvTracks.Nodes[0].Nodes[5].Text = rm.GetString("win_rec_bin");
            trvTracks.Nodes[0].Nodes[6].Text = rm.GetString("win_map_drives");
            trvTracks.Nodes[0].Nodes[7].Text = rm.GetString("start_menu_history");
            trvTracks.Nodes[0].Nodes[8].Text = rm.GetString("network_places");
            trvTracks.Nodes[0].Text = rm.GetString("win");

            trvTracks.Nodes[1].Nodes[0].Text = rm.GetString("ie_url_history");
            trvTracks.Nodes[1].Nodes[1].Text = rm.GetString("ie_history");
            trvTracks.Nodes[1].Nodes[2].Text = rm.GetString("ie_cookies");
            trvTracks.Nodes[1].Nodes[3].Text = rm.GetString("auto_pass");
            trvTracks.Nodes[1].Nodes[4].Text = rm.GetString("temp_internet_files");
            trvTracks.Nodes[1].Nodes[5].Text = rm.GetString("index_files");
            trvTracks.Nodes[1].Text = rm.GetString("ie");

            trvTracks.Nodes[2].Nodes[0].Text = rm.GetString("mp");
            trvTracks.Nodes[2].Nodes[1].Text = rm.GetString("qp");
            trvTracks.Nodes[2].Nodes[2].Text = rm.GetString("mmfp");
            trvTracks.Nodes[2].Nodes[3].Text = rm.GetString("office");
            trvTracks.Nodes[2].Nodes[4].Text = rm.GetString("ms_management_console");
            trvTracks.Nodes[2].Nodes[5].Text = rm.GetString("ms_wordpad");
            trvTracks.Nodes[2].Nodes[6].Text = rm.GetString("ms_paint");
            trvTracks.Nodes[2].Nodes[7].Text = rm.GetString("winrar");
            trvTracks.Nodes[2].Nodes[8].Text = rm.GetString("sun_java");
            trvTracks.Nodes[2].Nodes[9].Text = rm.GetString("win_def");
            trvTracks.Nodes[2].Text = rm.GetString("plugins");

            trvTracks.Nodes[3].Nodes[0].Text = rm.GetString("firefox_history");
            trvTracks.Nodes[3].Nodes[1].Text = rm.GetString("firefox_cookies");
            trvTracks.Nodes[3].Nodes[2].Text = rm.GetString("firefox_cache");
            trvTracks.Nodes[3].Nodes[3].Text = rm.GetString("firefox_saved_info");
            trvTracks.Nodes[3].Nodes[4].Text = rm.GetString("firefox_saved_pass");
            trvTracks.Nodes[3].Nodes[5].Text = rm.GetString("firefox_download_history");
            trvTracks.Nodes[3].Nodes[6].Text = rm.GetString("firefox_search_history");
            trvTracks.Nodes[3].Text = rm.GetString("firefox");

            trvTracks.Nodes[4].Nodes[0].Text = rm.GetString("google_history");
            trvTracks.Nodes[4].Nodes[1].Text = rm.GetString("google_cookies");
            trvTracks.Nodes[4].Nodes[2].Text = rm.GetString("google_cache");
            trvTracks.Nodes[4].Nodes[3].Text = rm.GetString("google_saved_info");
            trvTracks.Nodes[4].Nodes[4].Text = rm.GetString("google_download_history");
            trvTracks.Nodes[4].Text = rm.GetString("google");
            lblResults.Text = rm.GetString("results");
            RunEraser.Text = rm.GetString("run_eraser");
            btnAnalyse.Text = rm.GetString("analyse");
            //this.statusStrip1.Text = rm.GetString("status_strip");
            //this.toolStripStatusLabel1.Text = rm.GetString("track_eraser");
            Text = rm.GetString("tracks_eraser");
            ucTop.Text = rm.GetString("tracks_eraser");

            //Language.Text = rm.GetString("Language");
        }

        /// <summary>
        /// handle click event to analyse system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsbAnalyse_Click(object sender, EventArgs e)
        {
            filesToDelete = new List<string>();
            regValuesToDelete = new List<string>();
            foldersToDelete = new List<string>();
            UrlHistory = new UrlHistoryWrapperClass();
            Enumerator = UrlHistory.GetEnumerator();
            GetInstalledApps();

            btnAnalyse.Enabled = false;
            tsbAnalyse.Enabled = false;
            tsbErase.Enabled = false;
            tsbCheck.Enabled = false;
            btnAnalyse.Text = rm.GetString("scanning");
            tsbAnalyse.Text = rm.GetString("scanning");

            Scanninglbl.Visible = true;
            Scanninglbl.Text = rm.GetString("scanning_tracks");
            pcbScanning.Visible = true;
            var worker = new BackgroundWorker();
            worker.ProgressChanged += ProgressChanged;
            worker.DoWork += DoWork;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// handle progress changed event to show current progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// handle run worker completed event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// handle run worker completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnAnalyse.Text = "Analyse";
            tsbAnalyse.Text = "Analyse";
            btnAnalyse.Enabled = true;
            tsbAnalyse.Enabled = true;
            tsbErase.Enabled = true;
            tsbCheck.Enabled = true;

            if (filesToDelete.Count > 0 || foldersToDelete.Count > 0 || regValuesToDelete.Count > 0)
                RunEraser.Enabled = true;

            pcbScanning.Visible = false;
            Scanninglbl.Visible = false;
        }

        /// <summary>
        /// get list of files to be deleted
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        List<string> GetFilesAvailableForDrop(IEnumerable<string> files)
        {
            var processedFiles = new List<string>();
            foreach (string file in files)
            {
                try
                {
                    if (file.Substring(1, 2) != @":\")
                    {
                        processedFiles.Add(file);
                        continue;
                    }

                    var rights = new UserFileAccessRights(file);
                    if (rights.CanDelete())
                    {
                        FileStream fileStream = GetStream(FileAccess.ReadWrite, file);
                        processedFiles.Add(file);
                        fileStream.Close();
                    }
                }
                catch
                {
                }
            }

            return processedFiles;
        }

        /// <summary>
        /// handle do work event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = culture;

                bool flag = false;

                foreach (TreeNode n1 in trvTracks.Nodes)
                    foreach (TreeNode n2 in n1.Nodes)
                        if (n2.Checked)
                            flag = true;

                if (!flag)
                {
                    MessageBox.Show(rm.GetString("error_select") + "!", rm.GetString("warning"), MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    ClearResultBox();
                    return;
                }

                filesToDelete = GetFilesAvailableForDrop(filesToDelete);

                string sizeGained = FormatSize(CalcFilesToDelSize() + recycleBinSize);
                ClearResultBox();
                AppendLineToResult(rm.GetString("analysis_report"));
                AppendLineToResult("------------------------------------------------------------------------------------------");
                AppendLineToResult(regValuesToDelete.Count + " " + rm.GetString("tobe_removed_win_reg") + ".");
                AppendLineToResult(sizeGained + " (" + ((ulong)filesToDelete.Count + recycleBinCount) + " " + rm.GetString("files") +
                                   ") " +
                                   rm.GetString("tobe_removed") + ".");
                AppendLineToResult("------------------------------------------------------------------------------------------");
                AppendLineToResult("");

                if (regValuesToDelete.Count > 0)
                {
                    AppendLineToResult(rm.GetString("details_reg_tobe_del") + ":");
                    AppendLineToResult("------------------------------------------------------------------------------------------");

                    foreach (string reg in regValuesToDelete)
                        AppendLineToResult(@"RegKey: HKEY_CURRENT_USER\" + reg);
                    AppendLineToResult("");
                }

                String filestodelete_str = "";
                if (filesToDelete.Count > 0)
                {
                    AppendLineToResult(rm.GetString("details_files_tobe_del") + ":");
                    AppendLineToResult("------------------------------------------------------------------------------------------");

                    string file;
                    foreach (string item in filesToDelete)
                    {
                        file = item;
                        if (file.Substring(0, 8) == @"file:///")
                        {
                            file = rm.GetString("ie_history_item") + " " + file.Substring(8).Replace('/', '\\');
                        }
                        else
                        {
                            if (file.Substring(1, 2) != @":\")
                            {
                                file = rm.GetString("ie_history_item") + " " + file;
                            }
                        }

                        filestodelete_str += file + "\r\n";
                    }
                }
                AppendLineToResult(filestodelete_str);
            }
            catch
            {
            }

            if ((filesToDelete.Count | regValuesToDelete.Count) > 0)
            {
                if (RunEraser.InvokeRequired)
                {
                    RunEraser.Invoke(new MethodInvoker(delegate
                                                        {
                                                            RunEraser.Enabled = true;
                                                            txtResults.SelectionStart = 0;
                                                            txtResults.ScrollToCaret();
                                                        }));
                }
            }
        }

        /// <summary>
        /// handle do work event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = culture;

                if (txtResults.InvokeRequired)
                {
                    txtResults.Invoke(new MethodInvoker(delegate { txtResults.Text = rm.GetString("scan_started"); }));
                }

                filesToDelete.Clear();
                regValuesToDelete.Clear();
                foldersToDelete.Clear();

                recycleBinCount = 0;
                recycleBinSize = 0;

                bool res = false;
                bool res1 = false;

                #region Windows Recent Documents

                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[0].Nodes[0].Checked; }));
                }
                else
                {
                    res = trvTracks.Nodes[0].Nodes[0].Checked;
                }

                if (res)
                {
                    try
                    {
                        var d = new DirectoryInfo(
                            Environment.GetFolderPath(Environment.SpecialFolder.Recent));

                        DirectoryInfo[] dires = d.GetDirectories("*", SearchOption.AllDirectories);
                        FileInfo[] files = d.GetFiles();

                        foreach (DirectoryInfo dir in dires)
                        {
                            foldersToDelete.Add(dir.FullName);
                            filesToDelete.Add(dir.FullName);
                        }
                        foreach (FileInfo f in files)
                        {
                            filesToDelete.Add(f.FullName);
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Start Menu Run

                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[0].Nodes[1].Checked; }));
                }
                else
                {
                    res = trvTracks.Nodes[0].Nodes[1].Checked;
                }

                if (res)
                {
                    try
                    {
                        string runRegKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU\";
                        using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(runRegKeyPath))
                        {
                            if (regKey != null)
                            {
                                foreach (string valueName in regKey.GetValueNames())
                                {
                                    if (valueName.ToLower() != "default")
                                        regValuesToDelete.Add(@"Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU\" + valueName);
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Common Dialog File/Folder List

                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[0].Nodes[2].Checked; }));
                }
                else
                {
                    res = trvTracks.Nodes[0].Nodes[2].Checked;
                }

                if (res)
                {
                    try
                    {
                        string runRegKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\";
                        using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(runRegKeyPath))
                        {
                            foreach (string subKeyName in regKey.GetSubKeyNames())
                            {
                                if (subKeyName == "LastVisitedPidlMRU" || subKeyName == "OpenSavePidlMRU")
                                    regValuesToDelete.Add(@"Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\" + subKeyName);
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Start Menu Search

                #endregion

                #region Windows Clipboard

                //Clipboard.SetText("Hello");
                //Clipboard.Clear();
                try
                {
                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[0].Nodes[3].Checked; }));
                    }
                    else
                    {
                        res = trvTracks.Nodes[0].Nodes[3].Checked;
                    }

                    if (res)
                    {
                        Object obj = Clipboard.GetData("*");
                    }
                }
                catch
                {
                }

                #endregion

                #region Windows Temporary Files

                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[0].Nodes[4].Checked; }));
                }
                else
                {
                    res = trvTracks.Nodes[0].Nodes[4].Checked;
                }

                if (res)
                {
                    try
                    {
                        //string sWinTempFilesAddress = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).ToString();
                        string sWinTempFilesAddress = Environment.GetEnvironmentVariable("temp"); //sWinTempFilesAddress + "\\Temp";

                        var dirWindowsTempFiles = new DirectoryInfo(sWinTempFilesAddress);
                        FileSystemInfo[] fileSysInfoWinTemp = dirWindowsTempFiles.GetFiles("*", SearchOption.AllDirectories);
                        foreach (FileInfo f in fileSysInfoWinTemp)
                        {
                            filesToDelete.Add(f.FullName);
                        }
                        foreach (DirectoryInfo di in dirWindowsTempFiles.GetDirectories())
                        {
                            foldersToDelete.Add(di.FullName);
                        }

                        sWinTempFilesAddress = Environment.GetEnvironmentVariable("windir");
                        sWinTempFilesAddress = sWinTempFilesAddress + "\\temp";

                        dirWindowsTempFiles = new DirectoryInfo(sWinTempFilesAddress);
                        fileSysInfoWinTemp = dirWindowsTempFiles.GetFiles("*", SearchOption.AllDirectories);
                        foreach (FileInfo f in fileSysInfoWinTemp)
                        {
                            filesToDelete.Add(f.FullName);
                        }
                        foreach (DirectoryInfo di in dirWindowsTempFiles.GetDirectories())
                        {
                            foldersToDelete.Add(di.FullName);
                        }

                        string windir = Environment.GetEnvironmentVariable("windir");

                        filesToDelete.Add(windir + @"\Memory.dmp");

                        try
                        {
                            foreach (string f in Directory.GetFiles(windir, "*.log", SearchOption.AllDirectories))
                            {
                                filesToDelete.Add(f);
                            }
                        }
                        catch
                        {
                        }

                        try
                        {
                            foreach (string f in Directory.GetFiles(windir + @"\minidump", "*", SearchOption.AllDirectories))
                            {
                                filesToDelete.Add(f);
                            }
                        }
                        catch
                        {
                        }

                        try
                        {
                            foreach (
                                string f in
                                    Directory.GetFiles(
                                        Environment.GetEnvironmentVariable("PROGRAMDATA") + "\\Microsoft\\Windows\\WER\\ReportArchive", "*",
                                        SearchOption.AllDirectories))
                            {
                                filesToDelete.Add(f);
                            }
                            var folders =
                                new DirectoryInfo(Environment.GetEnvironmentVariable("PROGRAMDATA") + "\\Microsoft\\Windows\\WER\\ReportArchive");
                            foreach (DirectoryInfo di in folders.GetDirectories())
                            {
                                foldersToDelete.Add(di.FullName);
                            }
                        }
                        catch
                        {
                        }
                        try
                        {
                            foreach (
                                string f in
                                    Directory.GetFiles(
                                        Environment.GetEnvironmentVariable("PROGRAMDATA") + "\\Microsoft\\Windows\\WER\\ReportQueue", "*",
                                        SearchOption.AllDirectories))
                            {
                                filesToDelete.Add(f);
                            }
                            var folders =
                                new DirectoryInfo(Environment.GetEnvironmentVariable("PROGRAMDATA") + "\\Microsoft\\Windows\\WER\\ReportQueue");
                            foreach (DirectoryInfo di in folders.GetDirectories())
                            {
                                foldersToDelete.Add(di.FullName);
                            }
                        }
                        catch
                        {
                        }
                        try
                        {
                            foreach (
                                string f in
                                    Directory.GetFiles(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\ElevatedDiagnostics", "*",
                                                       SearchOption.AllDirectories))
                            {
                                filesToDelete.Add(f);
                            }
                            var folders = new DirectoryInfo(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\ElevatedDiagnostics");
                            foreach (DirectoryInfo di in folders.GetDirectories())
                            {
                                foldersToDelete.Add(di.FullName);
                            }
                        }
                        catch
                        {
                        }
                        try
                        {
                            foreach (
                                string f in
                                    Directory.GetFiles(
                                        Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Microsoft\\Windows\\WER\\ReportQueue", "*",
                                        SearchOption.AllDirectories))
                            {
                                filesToDelete.Add(f);
                            }
                            var folders =
                                new DirectoryInfo(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Microsoft\\Windows\\WER\\ReportQueue");
                            foreach (DirectoryInfo di in folders.GetDirectories())
                            {
                                foldersToDelete.Add(di.FullName);
                            }
                        }
                        catch
                        {
                        }
                        try
                        {
                            foreach (
                                string f in
                                    Directory.GetFiles(
                                        Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Microsoft\\Windows\\WER\\ReportArchive", "*",
                                        SearchOption.AllDirectories))
                            {
                                filesToDelete.Add(f);
                            }
                            var folders =
                                new DirectoryInfo(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Microsoft\\Windows\\WER\\ReportArchive");
                            foreach (DirectoryInfo di in folders.GetDirectories())
                            {
                                foldersToDelete.Add(di.FullName);
                            }
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Windows recycle bin

                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[0].Nodes[5].Checked; }));
                }
                else
                {
                    res = trvTracks.Nodes[0].Nodes[5].Checked;
                }

                if (res)
                {
                    try
                    {
                        var sqrbi = new SHQUERYRBINFO();
                        sqrbi.cbSize = Marshal.SizeOf(typeof(SHQUERYRBINFO));
                        int result = SHQueryRecycleBin(string.Empty, ref sqrbi);
                        if (result == 0)
                        {
                            recycleBinCount = sqrbi.i64NumItems;
                            recycleBinSize = sqrbi.i64Size;
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Mapped Drives

                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[0].Nodes[6].Checked; }));
                }
                else
                {
                    res = trvTracks.Nodes[0].Nodes[6].Checked;
                    res1 = trvTracks.Nodes[0].Nodes[8].Checked;
                }
                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res1 = trvTracks.Nodes[0].Nodes[8].Checked; }));
                }
                if (res || res1)
                {
                    DriveInfo[] drives = null;

                    try
                    {
                        drives = DriveInfo.GetDrives();
                    }
                    catch (IOException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }

                    if (drives != null)
                    {
                        foreach (DriveInfo drive in drives)
                        {
                            try
                            {
                                if (drive.DriveType == DriveType.Network)
                                    filesToDelete.Add(drive.Name);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }

                #endregion

                #region Start Menu Click History

                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[0].Nodes[7].Checked; }));
                }
                else
                {
                    res = trvTracks.Nodes[0].Nodes[7].Checked;
                }

                if (res)
                {
                    try
                    {
                        var dirInfoStartMenu = new DirectoryInfo(
                            Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));

                        DirectoryInfo[] dirStartMenu = dirInfoStartMenu.GetDirectories("*", SearchOption.AllDirectories);
                        FileInfo[] filesStartMenu = dirInfoStartMenu.GetFiles();
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Internet Explorer URL History

                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[1].Nodes[0].Checked; }));
                }
                else
                {
                    res = trvTracks.Nodes[1].Nodes[0].Checked;
                }
                if (res)
                {
                    try
                    {
                        while (Enumerator.MoveNext())
                        {
                            string s = Enumerator.Current.pwcsUrl;
                            if (s.StartsWith("http"))
                            {
                                filesToDelete.Add(s);
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Internet Explorer History

                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[1].Nodes[1].Checked; }));
                }
                else
                {
                    res = trvTracks.Nodes[1].Nodes[1].Checked;
                }
                if (res)
                {
                    try
                    {
                        var ieHistory1 =
                            new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                              @"\Microsoft\Windows\History");
                        foreach (FileInfo fi in ieHistory1.GetFiles("*", SearchOption.AllDirectories))
                        {
                            filesToDelete.Add(fi.FullName);
                        }
                        foreach (DirectoryInfo di in ieHistory1.GetDirectories())
                        {
                            foldersToDelete.Add(di.FullName);
                        }

                        var ieHistory2 =
                            new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                              @"\Microsoft\Internet Explorer\Recovery");
                        foreach (FileInfo fi in ieHistory2.GetFiles("*", SearchOption.AllDirectories))
                        {
                            filesToDelete.Add(fi.FullName);
                        }
                        foreach (DirectoryInfo di in ieHistory2.GetDirectories())
                        {
                            foldersToDelete.Add(di.FullName);
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Internet Explorer Cache

                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[1].Nodes[2].Checked; }));
                }
                else
                {
                    res = trvTracks.Nodes[1].Nodes[2].Checked;
                }

                if (res)
                {
                    try
                    {
                        var dIECookies = new DirectoryInfo(
                            Environment.GetFolderPath(Environment.SpecialFolder.Cookies));

                        DirectoryInfo[] dirCookies = dIECookies.GetDirectories("*", SearchOption.AllDirectories);
                        FileInfo[] filesCookies = dIECookies.GetFiles("*", SearchOption.AllDirectories);

                        foreach (DirectoryInfo dir in dirCookies)
                        {
                            foldersToDelete.Add(dir.FullName);
                        }
                        foreach (FileInfo f in filesCookies)
                        {
                            filesToDelete.Add(f.FullName);
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region AutoComplete Passwords

                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[1].Nodes[3].Checked; }));
                }
                else
                {
                    res = trvTracks.Nodes[1].Nodes[3].Checked;
                }

                if (res)
                {
                    try
                    {
                        string uninstallKey = @"Software\Microsoft\Internet Explorer\TypedURLs";
                        using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(uninstallKey))
                        {
                            foreach (string skName in rk.GetValueNames())
                            {
                                if (skName.ToLower() != "default")
                                {
                                    regValuesToDelete.Add(@"Software\Microsoft\Internet Explorer\TypedURLs\" + skName);
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Temporary Internet Files

                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[1].Nodes[4].Checked; }));
                }
                else
                {
                    res = trvTracks.Nodes[1].Nodes[4].Checked;
                }

                if (res)
                {
                    try
                    {
                        string tempDir = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                        var diInfoIECache = new DirectoryInfo(tempDir);
                        foreach (FileInfo fi in diInfoIECache.GetFiles("*", SearchOption.AllDirectories))
                        {
                            filesToDelete.Add(fi.FullName);
                        }

                        foreach (string dir in Directory.GetDirectories(tempDir))
                        {
                            foldersToDelete.Add(dir);
                        }

                        tempDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Feeds Cache";

                        diInfoIECache = new DirectoryInfo(tempDir);
                        foreach (FileInfo fi in diInfoIECache.GetFiles("*", SearchOption.AllDirectories))
                        {
                            filesToDelete.Add(fi.FullName);
                        }

                        foreach (string dir in Directory.GetDirectories(tempDir))
                        {
                            foldersToDelete.Add(dir);
                        }
                        //tempDir = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
                        //diInfoIECache = new DirectoryInfo(tempDir);
                        //foreach (FileInfo fi in diInfoIECache.GetFiles("*", SearchOption.AllDirectories))
                        //{
                        //    filesToDelete.Add(fi.FullName);
                        //}
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Index.dat

                if (trvTracks.InvokeRequired)
                {
                    trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[1].Nodes[5].Checked; }));
                }
                else
                {
                    res = trvTracks.Nodes[1].Nodes[5].Checked;
                }

                if (res)
                {
                    try
                    {
                        string sIndexDat = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
                        var diInfoIEIndexDatCookies = new DirectoryInfo(sIndexDat);
                        FileInfo[] fileInfoIndexDatCookies = diInfoIEIndexDatCookies.GetFiles("index.dat");
                        filesToDelete.Add(fileInfoIndexDatCookies[0].FullName);

                        sIndexDat = Environment.GetFolderPath(Environment.SpecialFolder.History);
                        var diInfoIEIndexDatHistory = new DirectoryInfo(sIndexDat);
                        FileSystemInfo[] fileSysInfoHistory = diInfoIEIndexDatHistory.GetDirectories();
                        foreach (DirectoryInfo diNext in fileSysInfoHistory)
                        {
                            FileInfo[] datfiles = diNext.GetFiles("index.dat");
                            foreach (FileInfo f in datfiles)
                            {
                                filesToDelete.Add(f.FullName);
                            }
                        }

                        sIndexDat = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                        var diInfoIEIndexDatCache = new DirectoryInfo(sIndexDat);
                        FileSystemInfo[] fileSysInfoCache = diInfoIEIndexDatCache.GetDirectories();
                        foreach (DirectoryInfo diNext in fileSysInfoCache)
                        {
                            FileInfo[] datfiles = diNext.GetFiles("index.dat");
                            foreach (FileInfo f in datfiles)
                            {
                                filesToDelete.Add(f.FullName);
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Plugins

                try
                {
                    string appData = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "\\Users\\" +
                                     Environment.UserName + "\\AppData";
                    string localLowData = appData + "\\LocalLow";
                    string localData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string roamingData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

                    // windows media player
                    try
                    {
                        if (trvTracks.InvokeRequired)
                        {
                            trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[2].Nodes[0].Checked; }));
                        }
                        if (res)
                        {
                            var mediaPlayerDir = new DirectoryInfo(localData + "\\Microsoft\\Media Player");
                            foreach (DirectoryInfo d in mediaPlayerDir.GetDirectories())
                            {
                                if (d.Name.ToLower().Contains("cache"))
                                {
                                    foreach (FileInfo fi in d.GetFiles("*", SearchOption.AllDirectories))
                                    {
                                        filesToDelete.Add(fi.FullName);
                                    }
                                    foreach (DirectoryInfo di in d.GetDirectories())
                                    {
                                        foldersToDelete.Add(di.FullName);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }

                    //quick time player
                    try
                    {
                        if (trvTracks.InvokeRequired)
                        {
                            trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[2].Nodes[1].Checked; }));
                        }
                        if (res)
                        {
                            var quickTimeDir = new DirectoryInfo(localLowData + "\\Apple Computer\\quicktime\\downloads");
                            foreach (FileInfo fi in quickTimeDir.GetFiles("*", SearchOption.AllDirectories))
                            {
                                filesToDelete.Add(fi.FullName);
                            }
                            foreach (DirectoryInfo di in quickTimeDir.GetDirectories())
                            {
                                foldersToDelete.Add(di.FullName);
                            }
                        }
                    }
                    catch
                    {
                    }

                    //macromedia flash player
                    try
                    {
                        if (trvTracks.InvokeRequired)
                        {
                            trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[2].Nodes[2].Checked; }));
                        }
                        if (res)
                        {
                            var macromediaFlashDir = new DirectoryInfo(roamingData + "\\Macromedia\\Flash Player");
                            foreach (FileInfo fi in macromediaFlashDir.GetFiles("*", SearchOption.AllDirectories))
                            {
                                filesToDelete.Add(fi.FullName);
                            }
                            foreach (DirectoryInfo di in macromediaFlashDir.GetDirectories())
                            {
                                foldersToDelete.Add(di.FullName);
                            }
                        }
                    }
                    catch
                    {
                    }

                    //microsoft office
                    try
                    {
                        if (trvTracks.InvokeRequired)
                        {
                            trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[2].Nodes[3].Checked; }));
                        }
                        if (res)
                        {
                            var officeDir = new DirectoryInfo(roamingData + "\\Microsoft\\Office");
                            foreach (FileInfo fi in officeDir.GetFiles("*", SearchOption.AllDirectories))
                            {
                                filesToDelete.Add(fi.FullName);
                            }
                            foreach (DirectoryInfo di in officeDir.GetDirectories())
                            {
                                foldersToDelete.Add(di.FullName);
                            }
                        }
                    }
                    catch
                    {
                    }

                    //ms management console
                    try
                    {
                        if (trvTracks.InvokeRequired)
                        {
                            trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[2].Nodes[4].Checked; }));
                        }
                        if (res)
                        {
                            using (
                                RegistryKey regKey =
                                    Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Microsoft Management Console\Recent File List\"))
                            {
                                foreach (string subKeyName in regKey.GetValueNames())
                                {
                                    if (subKeyName.ToLower() != "default")
                                    {
                                        regValuesToDelete.Add(@"Software\Microsoft\Microsoft Management Console\Recent File List\" + subKeyName);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }

                    //ms wordpad
                    try
                    {
                        if (trvTracks.InvokeRequired)
                        {
                            trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[2].Nodes[5].Checked; }));
                        }
                        if (res)
                        {
                            using (
                                RegistryKey regKey =
                                    Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Applets\Wordpad\Recent File List\"))
                            {
                                foreach (string subKeyName in regKey.GetValueNames())
                                {
                                    if (subKeyName.ToLower() != "default")
                                    {
                                        regValuesToDelete.Add(@"Software\Microsoft\Windows\CurrentVersion\Applets\Wordpad\Recent File List\" +
                                                              subKeyName);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }

                    //ms paint
                    try
                    {
                        if (trvTracks.InvokeRequired)
                        {
                            trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[2].Nodes[6].Checked; }));
                        }
                        if (res)
                        {
                            using (
                                RegistryKey regKey =
                                    Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Applets\Paint\Recent File List\"))
                            {
                                foreach (string subKeyName in regKey.GetValueNames())
                                {
                                    if (subKeyName.ToLower() != "default")
                                    {
                                        regValuesToDelete.Add(@"Software\Microsoft\Windows\CurrentVersion\Applets\Paint\Recent File List\" + subKeyName);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }

                    //winrar
                    try
                    {
                        if (trvTracks.InvokeRequired)
                        {
                            trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[2].Nodes[7].Checked; }));
                        }
                        if (res)
                        {
                            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software\WinRAR\ArcHistory\"))
                            {
                                foreach (string subKeyName in regKey.GetValueNames())
                                {
                                    if (subKeyName.ToLower() != "default")
                                    {
                                        regValuesToDelete.Add(@"Software\WinRAR\ArcHistory\" + subKeyName);
                                    }
                                }
                            }
                            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software\WinRAR\DialogEditHistory\ArcName\"))
                            {
                                foreach (string subKeyName in regKey.GetValueNames())
                                {
                                    if (subKeyName.ToLower() != "default")
                                    {
                                        regValuesToDelete.Add(@"Software\WinRAR\DialogEditHistory\ArcName\" + subKeyName);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }

                    //sun java
                    try
                    {
                        if (trvTracks.InvokeRequired)
                        {
                            trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[2].Nodes[8].Checked; }));
                        }
                        if (res)
                        {
                            var javaDir = new DirectoryInfo(localData + "\\Sun\\Java\\Deployment\\cache");
                            foreach (FileInfo fi in javaDir.GetFiles("*", SearchOption.AllDirectories))
                            {
                                filesToDelete.Add(fi.FullName);
                            }
                            foreach (DirectoryInfo di in javaDir.GetDirectories())
                            {
                                foldersToDelete.Add(di.FullName);
                            }
                        }
                    }
                    catch
                    {
                    }

                    //windows defender
                    try
                    {
                        if (trvTracks.InvokeRequired)
                        {
                            trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[2].Nodes[9].Checked; }));
                        }
                        if (res)
                        {
                            var officeDir = new DirectoryInfo(programData + "\\Microsoft\\Windows Defender\\Scans\\History\\Results\\Quick");
                            foreach (FileInfo fi in officeDir.GetFiles("*", SearchOption.AllDirectories))
                            {
                                filesToDelete.Add(fi.FullName);
                            }
                            foreach (DirectoryInfo di in officeDir.GetDirectories())
                            {
                                foldersToDelete.Add(di.FullName);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                catch
                {
                }

                #endregion

                #region Mozilla Firefox

                try
                {
                    string sMozillaHistory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    sMozillaHistory = sMozillaHistory + "\\Mozilla\\Firefox\\Profiles";

                    #region Mozilla Firefox History

                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[3].Nodes[0].Checked; }));
                    }
                    else
                    {
                        res = trvTracks.Nodes[3].Nodes[0].Checked;
                    }

                    if (res)
                    {
                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxHistory = new DirectoryInfo(sMozillaHistory);
                                DirectoryInfo[] fileSysFirefoxHistory = diInfoFirefoxHistory.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxHistory)
                                {
                                    foreach (FileInfo f in diNext.GetFiles("places.sqlite"))
                                    {
                                        filesToDelete.Add(f.FullName);
                                    }
                                    foreach (FileInfo f in diNext.GetFiles("session*"))
                                    {
                                        filesToDelete.Add(f.FullName);
                                    }
                                    foreach (FileInfo f in diNext.GetFiles("history.dat"))
                                    {
                                        filesToDelete.Add(f.FullName);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    #endregion

                    #region Mozilla Firefox Cookies

                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[3].Nodes[1].Checked; }));
                    }
                    else
                    {
                        res = trvTracks.Nodes[3].Nodes[1].Checked;
                    }

                    if (res)
                    {
                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxCookies = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxCookies = diInfoFirefoxCookies.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxCookies)
                                {
                                    FileInfo[] datfiles = diNext.GetFiles("cookies.sqlite");
                                    foreach (FileInfo f in datfiles)
                                    {
                                        filesToDelete.Add(f.FullName);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    #endregion

                    #region Mozilla Firefox Internet Cache

                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[3].Nodes[2].Checked; }));
                    }
                    else
                    {
                        res = trvTracks.Nodes[3].Nodes[2].Checked;
                    }

                    if (res)
                    {
                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxCookies = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxCookies = diInfoFirefoxCookies.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxCookies)
                                {
                                    DirectoryInfo cacheFolder1 = diNext.GetDirectories("OfflineCache").FirstOrDefault();
                                    if (cacheFolder1 != null)
                                    {
                                        foreach (FileInfo file in cacheFolder1.GetFiles("*", SearchOption.AllDirectories))
                                        {
                                            filesToDelete.Add(file.FullName);
                                        }
                                    }

                                    DirectoryInfo cacheFolder2 = diNext.GetDirectories("Cache").FirstOrDefault();
                                    if (cacheFolder2 != null)
                                    {
                                        foreach (FileInfo file in cacheFolder2.GetFiles("*", SearchOption.AllDirectories))
                                        {
                                            filesToDelete.Add(file.FullName);
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    #endregion

                    #region Mozilla Firefox Saved Form information

                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[3].Nodes[3].Checked; }));
                    }
                    else
                    {
                        res = trvTracks.Nodes[3].Nodes[3].Checked;
                    }

                    if (res)
                    {
                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxformHistory = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxformHistory = diInfoFirefoxformHistory.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxformHistory)
                                {
                                    FileInfo[] datfiles = diNext.GetFiles("formhistory.sqlite");
                                    foreach (FileInfo f in datfiles)
                                    {
                                        filesToDelete.Add(f.FullName);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    #endregion

                    #region Mozilla Firefox Saved Passwords

                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[3].Nodes[4].Checked; }));
                    }
                    else
                    {
                        res = trvTracks.Nodes[3].Nodes[4].Checked;
                    }

                    if (res)
                    {
                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxformPwd = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxformPwd = diInfoFirefoxformPwd.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxformPwd)
                                {
                                    FileInfo[] datfiles = diNext.GetFiles("key3.db");
                                    foreach (FileInfo f in datfiles)
                                    {
                                        filesToDelete.Add(f.FullName);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    #endregion

                    #region Mozilla Firefox Download Manager History

                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[3].Nodes[5].Checked; }));
                    }
                    else
                    {
                        res = trvTracks.Nodes[3].Nodes[5].Checked;
                    }
                    if (res)
                    {
                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxformDownload = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxformDownload = diInfoFirefoxformDownload.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxformDownload)
                                {
                                    FileInfo[] datfiles = diNext.GetFiles("downloads.sqlite");
                                    foreach (FileInfo f in datfiles)
                                    {
                                        filesToDelete.Add(f.FullName);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    #endregion

                    #region Mozilla Firefox Search History

                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[3].Nodes[6].Checked; }));
                    }
                    else
                    {
                        res = trvTracks.Nodes[3].Nodes[6].Checked;
                    }

                    if (res)
                    {
                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxformSearch = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxformSearch = diInfoFirefoxformSearch.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxformSearch)
                                {
                                    FileInfo[] datfiles = diNext.GetFiles("search.sqlite");
                                    foreach (FileInfo f in datfiles)
                                    {
                                        filesToDelete.Add(f.FullName);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    #endregion
                }
                catch
                {
                }

                #endregion

                #region Google Chrome

                try
                {
                    string sGoogleChromePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    sGoogleChromePath = sGoogleChromePath + "\\Google\\Chrome\\User Data";

                    #region Google Chrome History

                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[4].Nodes[0].Checked; }));
                    }
                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res1 = trvTracks.Nodes[4].Nodes[4].Checked; }));
                    }
                    else
                    {
                        res = trvTracks.Nodes[4].Nodes[0].Checked;
                        res1 = trvTracks.Nodes[4].Nodes[4].Checked;
                    }
                    if (res || res1)
                    {
                        if (Directory.Exists(sGoogleChromePath))
                        {
                            try
                            {
                                var diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
                                DirectoryInfo[] profileDirs = diInfoChromeHistory.GetDirectories();

                                foreach (DirectoryInfo dir in profileDirs)
                                {
                                    FileSystemInfo[] fileSysChromeHistory = dir.GetFiles("*history*");
                                    foreach (FileInfo diNext in fileSysChromeHistory)
                                    {
                                        filesToDelete.Add(diNext.FullName);
                                    }
                                    fileSysChromeHistory = dir.GetFiles("*Visited Links*");
                                    foreach (FileInfo diNext in fileSysChromeHistory)
                                    {
                                        filesToDelete.Add(diNext.FullName);
                                    }
                                    fileSysChromeHistory = dir.GetFiles("*Current Tabs*");
                                    foreach (FileInfo diNext in fileSysChromeHistory)
                                    {
                                        filesToDelete.Add(diNext.FullName);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    #endregion

                    #region Google Chrome Cookies

                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[4].Nodes[1].Checked; }));
                    }
                    else
                    {
                        res = trvTracks.Nodes[4].Nodes[1].Checked;
                    }
                    if (res)
                    {
                        if (Directory.Exists(sGoogleChromePath))
                        {
                            try
                            {
                                var diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
                                DirectoryInfo[] profileDirs = diInfoChromeHistory.GetDirectories();

                                foreach (DirectoryInfo dir in profileDirs)
                                {
                                    FileSystemInfo[] fileSysChromeHistory = dir.GetFiles("*Cookies*");
                                    foreach (FileInfo diNext in fileSysChromeHistory)
                                    {
                                        filesToDelete.Add(diNext.FullName);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    #endregion

                    #region Google Chrome internet Cache

                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[4].Nodes[2].Checked; }));
                    }
                    else
                    {
                        res = trvTracks.Nodes[4].Nodes[2].Checked;
                    }

                    if (res)
                    {
                        try
                        {
                            var diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
                            DirectoryInfo[] profileDirs = diInfoChromeHistory.GetDirectories();

                            foreach (DirectoryInfo dir in profileDirs)
                            {
                                DirectoryInfo cache = dir.GetDirectories("Cache").FirstOrDefault();
                                if (cache != null)
                                {
                                    FileInfo[] files = cache.GetFiles();
                                    foreach (FileInfo diNext in files)
                                    {
                                        filesToDelete.Add(diNext.FullName);
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }

                    #endregion

                    #region Google Chrome Saved Form Information

                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[4].Nodes[3].Checked; }));
                    }
                    else
                    {
                        res = trvTracks.Nodes[4].Nodes[3].Checked;
                    }

                    if (res)
                    {
                        if (Directory.Exists(sGoogleChromePath))
                        {
                            try
                            {
                                var diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
                                DirectoryInfo[] profileDirs = diInfoChromeHistory.GetDirectories();

                                foreach (DirectoryInfo dir in profileDirs)
                                {
                                    FileSystemInfo[] fileSysChromeHistory = dir.GetFiles("*Web Data*");
                                    foreach (FileInfo diNext in fileSysChromeHistory)
                                    {
                                        filesToDelete.Add(diNext.FullName);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    #endregion

                    #region Google Chrome Download History

                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res = trvTracks.Nodes[4].Nodes[0].Checked; }));
                    }
                    if (trvTracks.InvokeRequired)
                    {
                        trvTracks.Invoke(new MethodInvoker(delegate { res1 = trvTracks.Nodes[4].Nodes[4].Checked; }));
                    }
                    else
                    {
                        res = trvTracks.Nodes[4].Nodes[0].Checked;
                        res1 = trvTracks.Nodes[4].Nodes[4].Checked;
                    }
                    if (res || res1)
                    {
                        if (Directory.Exists(sGoogleChromePath))
                        {
                            try
                            {
                                var diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
                                DirectoryInfo[] profileDirs = diInfoChromeHistory.GetDirectories();

                                foreach (DirectoryInfo dir in profileDirs)
                                {
                                    try
                                    {
                                        FileInfo[] fileSysChromeHistory = dir.GetFiles("*history*");
                                        foreach (FileInfo diNext in fileSysChromeHistory)
                                        {
                                            if (!filesToDelete.Contains(diNext.FullName))
                                            {
                                                filesToDelete.Add(diNext.FullName);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        FileInfo[] fileSysChromeHistory = dir.GetFiles("*Visited Links*");
                                        foreach (FileInfo diNext in fileSysChromeHistory)
                                        {
                                            if (!filesToDelete.Contains(diNext.FullName))
                                            {
                                                filesToDelete.Add(diNext.FullName);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        FileInfo[] fileSysChromeHistory = dir.GetFiles("*Current Tabs*");
                                        foreach (FileInfo diNext in fileSysChromeHistory)
                                        {
                                            if (!filesToDelete.Contains(diNext.FullName))
                                            {
                                                filesToDelete.Add(diNext.FullName);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    #endregion
                }
                catch
                {
                }

                #endregion
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        /// <summary>
        /// calculate size of files to be deleted
        /// </summary>
        /// <returns></returns>
        ulong CalcFilesToDelSize()
        {
            ulong totalBytes = 0UL;

            foreach (string filename in filesToDelete)
            {
                try
                {
                    totalBytes += (ulong)(new FileInfo(filename).Length);
                }
                catch
                {
                }
            }

            return totalBytes;
        }

        /// <summary>
        /// formats size for display
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        string FormatSize(ulong bytes)
        {
            double size = bytes;
            string unit = " Bytes";
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " KB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " MB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " MB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " TB";
            }

            return size.ToString("0.##") + unit;
        }

        /// <summary>
        /// append new line character to result
        /// </summary>
        /// <param name="line"></param>
        void AppendLineToResult(string line)
        {
            try
            {
                if (txtResults.InvokeRequired)
                {
                    txtResults.Invoke(new MethodInvoker(delegate
                                                            {
                                                                txtResults.Text += line + "\r\n";
                                                                txtResults.SelectionStart = txtResults.Text.Length;
                                                                txtResults.ScrollToCaret();
                                                                txtResults.Refresh();
                                                            }));
                }
                else
                {
                    txtResults.Text += line + "\r\n";
                    txtResults.SelectionStart = txtResults.Text.Length;
                    txtResults.ScrollToCaret();
                    txtResults.Refresh();
                }
            }
            catch (Exception ex)
            {
                Debug.Write("*********************************");
                Debug.Write(ex);
            }
        }

        /// <summary>
        /// clear text in result box
        /// </summary>
        void ClearResultBox()
        {
            if (txtResults.InvokeRequired)
            {
                txtResults.Invoke(new MethodInvoker(delegate
                                                        {
                                                            txtResults.Text = "";
                                                            txtResults.SelectionStart = txtResults.Text.Length;
                                                            txtResults.ScrollToCaret();
                                                            txtResults.Refresh();
                                                        }));
            }
            //resultsTxt.Text += line + "\r\n";
        }

        /// <summary>
        /// handle click event to erase tracks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsbErase_Click_1(object sender, EventArgs e)
        {
            int tracksCount = 0;
            foreach (TreeNode n1 in trvTracks.Nodes)
            {
                foreach (TreeNode n2 in n1.Nodes)
                {
                    try
                    {
                        if (n2.Checked)
                        {
                            tracksCount++;
                            if (n2.Text.ToLower().Contains("chrome"))
                            {
                                Process[] p = Process.GetProcessesByName("chrome");
                                if (p.Length > 0)
                                {
                                    MessageBox.Show(rm.GetString("close_chrome_to_erase"), rm.GetString("tracks_eraser"), MessageBoxButtons.OK,
                                                    MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            if (n2.Text.ToLower().Contains("firefox"))
                            {
                                Process[] p = Process.GetProcessesByName("firefox");
                                if (p.Length > 0)
                                {
                                    MessageBox.Show(rm.GetString("close_firefox_to_erase"), rm.GetString("tracks_eraser"), MessageBoxButtons.OK,
                                                    MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // ToDo: send exception details via SmartAssembly bug reporting!
                    }
                }
            }
            if (tracksCount == 0)
            {
                MessageBox.Show(rm.GetString("error_no_tracks_checked") + "\r\n" +
                                rm.GetString("error_check_tracks") + ".",
                                rm.GetString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (regValuesToDelete.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    String.Format(
                        "{0} {1} {2} {3} {4} {5} {6}?",
                        rm.GetString("error_sure"),
                        (ulong)filesToDelete.Count + recycleBinCount,
                        rm.GetString("files_and"),
                        regValuesToDelete.Count,
                        rm.GetString("registries_from"),
                        tracksCount,
                        rm.GetString("error_tracks")
                        ),
                    rm.GetString("warning"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                    );

                switch (result)
                {
                    case DialogResult.No:
                    case DialogResult.Cancel:
                        return;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    String.Format(
                        "{0} {1} {2} {3} {4}?",
                        rm.GetString("error_sure"),
                        (ulong)filesToDelete.Count + recycleBinCount,
                        rm.GetString("files_from"),
                        tracksCount,
                        rm.GetString("error_tracks")
                        ),
                    rm.GetString("warning"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                    );

                switch (result)
                {
                    case DialogResult.No:
                    case DialogResult.Cancel:
                        return;
                }
            }

            bool recycleBin = trvTracks.Nodes[0].Nodes[5].Checked;
            if (recycleBin)
            {
                try
                {
                    SHEmptyRecycleBin(IntPtr.Zero, string.Empty, SHERB_NOCONFIRMATION | SHERB_NOPROGRESSUI | SHERB_NOSOUND);
                }
                catch
                {
                }
            }

            try
            {
                RunEraser.Enabled = false;

                try
                {
                    foreach (string s in foldersToDelete)
                    {
                        try
                        {
                            Directory.Delete(s, true);
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }

                foreach (string filename in filesToDelete)
                    if (File.Exists(filename))
                        try
                        {
                            File.Delete(filename);
                        }
                        catch (Exception ex)
                        {

                        }

                foreach (string regKey in regValuesToDelete)
                {
                    try
                    {
                        string subKey = regKey.Remove(regKey.LastIndexOf("\\"));
                        string value = regKey.Substring(regKey.LastIndexOf("\\") + 1);

                        RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(subKey);
                        if (registryKey != null)
                            registryKey.DeleteValue(value);
                    }
                    catch
                    {
                    }
                }

                if (trvTracks.Nodes[0].Nodes[3].Checked)
                {
                    try
                    {
                        Clipboard.Clear();
                    }
                    catch
                    {
                    }
                }

                if (trvTracks.Nodes[1].Nodes[0].Checked)
                {
                    try
                    {
                        UrlHistory.ClearHistory();
                    }
                    catch
                    {
                    }
                }

                if (trvTracks.Nodes[3].Nodes[0].Checked)
                {
                    try
                    {
                        FFHistoryManager.ClearHistory();
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }

            txtResults.Text = rm.GetString("after_scan_msg");

            MessageBox.Show(rm.GetString("error_erased_succ") + ".", rm.GetString("info"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// handle click event to show options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UrlHistory = new UrlHistoryWrapperClass();
            Enumerator = UrlHistory.GetEnumerator();
            var frmOptions = new frmOptions();
            frmOptions.ShowDialog();
        }

        /// <summary>
        /// handle click event to check all items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tmiCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < trvTracks.Nodes.Count; i++)
            {
                //if (treeView1.Nodes[i].ForeColor != Color.DarkGray)
                //{
                trvTracks.Nodes[i].Checked = true;
                //}
            }
        }

        /// <summary>
        /// handle click event to check none items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tmiUncheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < trvTracks.Nodes.Count; i++)
            {
                trvTracks.Nodes[i].Checked = false;
            }
        }

        /// <summary>
        /// update installed apps in form
        /// </summary>
        public void GetInstalledApps()
        {
            try
            {
                if (IsApplictionInstalled("firefox") == false)
                {
                    trvTracks.Nodes[3].ForeColor = Color.DarkGray;
                    for (int i = 0; i < trvTracks.Nodes[3].Nodes.Count; i++)
                    {
                        trvTracks.Nodes[3].Nodes[i].ForeColor = Color.DarkGray;
                        trvTracks.Nodes[3].Nodes[i].ImageIndex = 7;
                    }
                }

                if (IsApplictionInstalled("google chrome") == false && IsBrowserInstalled("chrome") == false)
                {
                    trvTracks.Nodes[4].ForeColor = Color.DarkGray;
                    for (int i = 0; i < trvTracks.Nodes[4].Nodes.Count; i++)
                    {
                        trvTracks.Nodes[4].Nodes[i].ForeColor = Color.DarkGray;
                        trvTracks.Nodes[4].Nodes[i].ImageIndex = 7;
                    }
                }

                if (IsApplictionInstalled("quicktime") == false)
                {
                    trvTracks.Nodes[2].Nodes[1].ForeColor = Color.DarkGray;
                    trvTracks.Nodes[2].Nodes[1].ImageIndex = 7;
                }

                if (IsApplictionInstalled("adobe flash player") == false)
                {
                    trvTracks.Nodes[2].Nodes[2].ForeColor = Color.DarkGray;
                    trvTracks.Nodes[2].Nodes[2].ImageIndex = 7;
                }

                if (IsMSOfficeInstalled() == false)
                {
                    trvTracks.Nodes[2].Nodes[3].ForeColor = Color.DarkGray;
                    trvTracks.Nodes[2].Nodes[3].ImageIndex = 7;
                }

                if (IsApplictionInstalled("winrar") == false)
                {
                    trvTracks.Nodes[2].Nodes[7].ForeColor = Color.DarkGray;
                    trvTracks.Nodes[2].Nodes[7].ImageIndex = 7;
                }

                if (IsApplictionInstalled("java ") == false)
                {
                    trvTracks.Nodes[2].Nodes[8].ForeColor = Color.DarkGray;
                    trvTracks.Nodes[2].Nodes[8].ImageIndex = 7;
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        /// <summary>
        /// check if specific app is installed
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subname"></param>
        /// <returns></returns>
        bool IsApplictionInstalled(string name, string subname = null)
        {
            try
            {
                // search in: CurrentUser
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                string displayName;
                if (key != null)
                    foreach (string keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        displayName = subkey != null && subkey.GetValue("DisplayName") != null
                                        ? subkey.GetValue("DisplayName").ToString()
                                        : "";
                        if (displayName.ToLower().Contains(name))
                        {
                            if (subname == null || (displayName.ToLower().Contains(subname)))
                                return true;
                        }
                    }

                // search in: LocalMachine_32
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                if (key != null)
                    foreach (String keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        displayName = subkey != null && subkey.GetValue("DisplayName") != null
                                        ? subkey.GetValue("DisplayName").ToString()
                                        : "";
                        if (displayName.ToLower().Contains(name))
                        {
                            if (subname == null || (displayName.ToLower().Contains(subname)))
                                return true;
                        }
                    }

                // search in: LocalMachine_64
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                if (key != null)
                {
                    foreach (String keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        displayName = subkey != null && subkey.GetValue("DisplayName") != null
                                        ? subkey.GetValue("DisplayName").ToString()
                                        : "";
                        if (displayName.ToLower().Contains(name))
                        {
                            if (subname == null || (displayName.ToLower().Contains(subname)))
                                return true;
                        }
                    }
                }
                var roamingData = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                foreach (DirectoryInfo d in roamingData.GetDirectories())
                {
                    if (d.Name.ToLower() == name)
                    {
                        return true;
                    }
                }
                var localData = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
                foreach (DirectoryInfo d in localData.GetDirectories())
                {
                    if (d.Name.ToLower() == name)
                    {
                        return true;
                    }
                }
            }
            catch
            {
            }

            // NOT FOUND
            return false;
        }

        /// <summary>
        /// check if ms office is installed
        /// </summary>
        /// <returns></returns>
        static bool IsMSOfficeInstalled()
        {
            string sPlugins = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            bool res = true;
            var diInfoPlugins = new DirectoryInfo(sPlugins);
            FileSystemInfo[] fileSysInfoPlugins = diInfoPlugins.GetDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo diNext in fileSysInfoPlugins)
            {
                /*if (treeView1.InvokeRequired)
                {
                    treeView1.Invoke(new MethodInvoker(delegate { res = this.treeView1.Nodes[2].Nodes[3].Checked; }));
                }
                else
                {
                    res = this.treeView1.Nodes[2].Nodes[3].Checked;
                }
                */
                if (res)
                {
                    if (diNext.Name == "Microsoft")
                    {
                        try
                        {
                            string sPathMirosoft = diNext.FullName;
                            sPathMirosoft = sPathMirosoft + "\\Office\\Recent";

                            var diInfoMicrosoft = new DirectoryInfo(sPathMirosoft);
                            FileSystemInfo[] fileSysInfoMicrosoft = diInfoMicrosoft.GetFiles();

                            return fileSysInfoMicrosoft.Count() > -1;
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// check if specific browser is installed
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        bool IsBrowserInstalled(string browser)
        {
            try
            {
                RegistryKey openSubKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
                if (openSubKey != null)
                {
                    string[] s1 = openSubKey.GetSubKeyNames();
                    if (s1.Any(s => s.ToLower().Contains(browser)))
                    {
                        return true;
                    }
                }
            }
            catch
            {
            }
            try
            {
                RegistryKey openSubKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Clients\StartMenuInternet");
                if (openSubKey != null)
                {
                    string[] s1 = openSubKey.GetSubKeyNames();
                    if (s1.Any(s => s.ToLower().Contains(browser)))
                    {
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        /// handle after check event to uncheck running apps
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void trvTracks_AfterCheck(object sender, TreeViewEventArgs e)
        {
            bool messageFired = false;

            Process[] pname = Process.GetProcessesByName("chrome");
            if (pname.Length > 0)
            {
                if (e.Node.Text.Contains("Google Chrome"))
                {
                    if (e.Node.Checked)
                    {
                        e.Node.Checked = false;
                        MessageBox.Show(rm.GetString("google_chrome_running"), rm.GetString("warning"), MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        messageFired = true;
                    }
                }
            }

            pname = Process.GetProcessesByName("iexplore");
            if (pname.Length > 0)
            {
                if (e.Node.Text.Contains("Internet Explorer"))
                {
                    try
                    {
                        if (e.Node.Checked)
                        {
                            e.Node.Checked = false;
                            MessageBox.Show(rm.GetString("ie_running"), rm.GetString("warning"), MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                            messageFired = true;
                        }
                    }
                    catch
                    {
                    }
                }
            }

            pname = Process.GetProcessesByName("firefox");
            if (pname.Length > 0)
            {
                if (e.Node.Text.Contains("Mozilla Firefox"))
                {
                    try
                    {
                        if (e.Node.Checked)
                        {
                            e.Node.Checked = false;
                            MessageBox.Show(rm.GetString("mozilla_firefox_running"), rm.GetString("warning"), MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                            messageFired = true;
                        }
                    }
                    catch
                    {
                    }
                }
            }

            if (e.Node.ForeColor == Color.DarkGray && e.Node.Checked && !messageFired)
            {
                e.Node.Checked = false;
                MessageBox.Show(rm.GetString("item_running_or_installed"), rm.GetString("warning"), MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        #region Checking file access

        const int NumberOfTries = 3;
        const int TimeIntervalBetweenTries = 100;

        /// <summary>
        /// try to access specific file
        /// </summary>
        /// <param name="fileAccess"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        FileStream GetStream(FileAccess fileAccess, string fileName)
        {
            int tries = 0;
            while (true)
            {
                try
                {
                    return File.Open(fileName, FileMode.Open, fileAccess, FileShare.None);
                }
                catch (IOException e)
                {
                    if (!IsFileLocked(e))
                        throw;
                    if (++tries > NumberOfTries)
                        throw new Exception("The file is locked too long: " + e.Message, e);
                    Thread.Sleep(TimeIntervalBetweenTries);
                }
            }
        }

        /// <summary>
        /// check if specific file is locked
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        static bool IsFileLocked(Exception exception)
        {
            int errorCode = Marshal.GetHRForException(exception) & ((1 << 16) - 1);
            return errorCode == 32 || errorCode == 33;
        }

        #endregion
    }

    //First the SHQUERYRBINFO struct
    /// <summary>
    /// struct representing the SHQUERYRBINFO structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct SHQUERYRBINFO
    {
        /// <summary>
        /// Size
        /// </summary>
        public Int32 cbSize;

        /// <summary>
        /// i64 size
        /// </summary>
        public UInt64 i64Size;

        /// <summary>
        /// i64 Num items
        /// </summary>
        public UInt64 i64NumItems;
    }
}