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
using Microsoft.Win32;
using UrlHistoryLibrary;
using Knots.Security.TracksEraserCore;
using FreemiumUtil;

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
        /// Shell query for the Windows Recycle Bin
        /// </summary>
        /// <param name="pszRootPath"></param>
        /// <param name="pSHQueryRBInfo"></param>
        /// <returns></returns>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHQueryRecycleBin(string pszRootPath, ref SHQUERYRBINFO pSHQueryRBInfo);

        [DllImport("shell32.dll")]
        private static extern int SHEmptyRecycleBin(IntPtr hWnd, string pszRootPath, uint dwFlags);

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
        List<string> regValuesToDelete;

        private bool isCancel = false;
        BackgroundWorker scanBackgroundWorker;

        ulong recycleBinCount;
        ulong recycleBinSize;

        //Now the Win32 API

        /// <summary>
        /// constructor for FormMain
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

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

                trvTracks.ExpandAll();
                trvTracks.SelectedNode = trvTracks.Nodes[0];

                CheckNotInstalledApps();

                Process[] pname = Process.GetProcessesByName("iexplore");
                if (pname.Length > 0)
                {
                    trvTracks.Nodes[1].ForeColor = Color.DarkGray;
                    for (int i = 0; i < trvTracks.Nodes[1].Nodes.Count; i++)
                    {
                        trvTracks.Nodes[1].Nodes[i].ForeColor = Color.DarkGray;
                        trvTracks.Nodes[1].Nodes[i].ImageIndex = 7;
                    }
                }

                pname = Process.GetProcessesByName("firefox");
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

        #region SetCulture
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
            Text = rm.GetString("tracks_eraser");
            ucTop.Text = rm.GetString("tracks_eraser");
        }

        #endregion

        /// <summary>
        /// handle click event to analyse system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsbAnalyse_Click(object sender, EventArgs e)
        {
            RunScan();
        }

        void btnAnalyse_Click(object sender, EventArgs e)
        {
            if (btnAnalyse.Text == rm.GetString("cancel"))
            {
                CancelScan();
            }
            else
            {
                RunScan();
            }
        }

        /// <summary>
        /// Runs Scanning process
        /// </summary>
        private void RunScan()
        {
            filesToDelete = new List<string>();
            regValuesToDelete = new List<string>();
            foldersToDelete = new List<string>();
            UrlHistory = new UrlHistoryWrapperClass();
            Enumerator = UrlHistory.GetEnumerator();
            CheckNotInstalledApps();

            tsbAnalyse.Enabled = false;
            tsbErase.Enabled = false;
            tsbCheck.Enabled = false;
            btnAnalyse.Text = rm.GetString("cancel");
            tsbAnalyse.Text = rm.GetString("scanning");

            Scanninglbl.Visible = true;
            Scanninglbl.Text = rm.GetString("scanning_tracks");
            pcbScanning.Visible = true;
            scanBackgroundWorker = new BackgroundWorker();
            scanBackgroundWorker.WorkerSupportsCancellation = true;
            scanBackgroundWorker.ProgressChanged += ProgressChanged;
            scanBackgroundWorker.DoWork += DoWork;
            scanBackgroundWorker.RunWorkerCompleted += WorkerCompleted;
            scanBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// cancel the scanning process
        /// </summary>
        public void CancelScan()
        {
            isCancel = true;
            if (scanBackgroundWorker.IsBusy)
                scanBackgroundWorker.CancelAsync(); //makes the backgroundworker stop
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
            if (isCancel || e.Cancelled)
            {
                btnAnalyse.Text = "Analyse";
                tsbAnalyse.Text = "Analyse";
                txtResults.Text = string.Empty;
                btnAnalyse.Enabled = true;
                tsbAnalyse.Enabled = true;
                tsbErase.Enabled = true;
                tsbCheck.Enabled = true;
                pcbScanning.Visible = false;
                Scanninglbl.Visible = false;
                isCancel = false;
            }
            else
            {
                BackgroundWorker bgWorker = new BackgroundWorker();
                bgWorker.DoWork += bgWorker_DoWork;
                bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
                bgWorker.RunWorkerAsync();
            }
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
                    MessageBox.Show(rm.GetString("error_select"), rm.GetString("warning"), MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    ClearResultBox();
                    return;
                }

                filesToDelete = Helper.GetFilesAvailableForDrop(filesToDelete);

                string sizeGained = Helper.FormatSize(Helper.CalcFilesToDelSize(filesToDelete) + recycleBinSize);
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

                String filestodelete_str = string.Empty;
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
                        DirectoryInfo d = new DirectoryInfo(
                            Environment.GetFolderPath(Environment.SpecialFolder.Recent));

                        MarkDirToDelete(d);
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
                        string sWinTempFilesAddress = Environment.GetEnvironmentVariable("temp");

                        DirectoryInfo dirWindowsTempFiles = new DirectoryInfo(sWinTempFilesAddress);
                        MarkDirToDelete(dirWindowsTempFiles);

                        sWinTempFilesAddress = Environment.GetEnvironmentVariable("windir");
                        sWinTempFilesAddress = sWinTempFilesAddress + "\\temp";

                        dirWindowsTempFiles = new DirectoryInfo(sWinTempFilesAddress);
                        MarkDirToDelete(dirWindowsTempFiles);

                        string windir = Environment.GetEnvironmentVariable("windir");

                        filesToDelete.Add(windir + @"\Memory.dmp");

                        try
                        {
                            filesToDelete.AddRange(Directory.GetFiles(windir, "*.log", SearchOption.AllDirectories));
                        }
                        catch
                        {
                        }
                        try
                        {
                            filesToDelete.AddRange(Directory.GetFiles(windir + @"\minidump", "*", SearchOption.AllDirectories));
                        }
                        catch
                        {
                        }

                        try
                        {
                            DirectoryInfo d = new DirectoryInfo(
                             Environment.GetEnvironmentVariable("PROGRAMDATA") + "\\Microsoft\\Windows\\WER\\ReportArchive");

                            MarkDirToDelete(d);
                        }
                        catch
                        {
                        }
                        try
                        {
                            DirectoryInfo d = new DirectoryInfo(
                             Environment.GetEnvironmentVariable("PROGRAMDATA") + "\\Microsoft\\Windows\\WER\\ReportQueue");

                            MarkDirToDelete(d);
                        }
                        catch
                        {
                        }
                        try
                        {
                            DirectoryInfo d = new DirectoryInfo(
                                Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\ElevatedDiagnostics");

                            MarkDirToDelete(d);
                        }
                        catch
                        {
                        }
                        try
                        {
                            DirectoryInfo d = new DirectoryInfo(
                                Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Microsoft\\Windows\\WER\\ReportQueue");

                            MarkDirToDelete(d);
                        }
                        catch
                        {
                        }
                        try
                        {
                            DirectoryInfo d = new DirectoryInfo(
                                Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Microsoft\\Windows\\WER\\ReportArchive");

                            MarkDirToDelete(d);
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
                        SHQUERYRBINFO sqrbi = new SHQUERYRBINFO();
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
                        DirectoryInfo dirInfoStartMenu = new DirectoryInfo(
                            Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
                        MarkDirToDelete(dirInfoStartMenu);
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
                        DirectoryInfo ieHistory1 =
                            new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                              @"\Microsoft\Windows\History");
                        MarkDirToDelete(ieHistory1);

                        DirectoryInfo ieHistory2 =
                            new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                              @"\Microsoft\Internet Explorer\Recovery");
                        MarkDirToDelete(ieHistory2);
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
                        DirectoryInfo dIECookies = new DirectoryInfo(
                            Environment.GetFolderPath(Environment.SpecialFolder.Cookies));

                        MarkDirToDelete(dIECookies);
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
                        DirectoryInfo diInfoIECache = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache));
                        MarkDirToDelete(diInfoIECache);

                        diInfoIECache = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Feeds Cache");
                        MarkDirToDelete(diInfoIECache);
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
                        DirectoryInfo diInfoIEIndexDatCookies = new DirectoryInfo(sIndexDat);
                        FileInfo[] fileInfoIndexDatCookies = diInfoIEIndexDatCookies.GetFiles("index.dat");
                        filesToDelete.Add(fileInfoIndexDatCookies[0].FullName);

                        sIndexDat = Environment.GetFolderPath(Environment.SpecialFolder.History);
                        DirectoryInfo diInfoIEIndexDatHistory = new DirectoryInfo(sIndexDat);
                        FileSystemInfo[] fileSysInfoHistory = diInfoIEIndexDatHistory.GetDirectories();
                        foreach (DirectoryInfo diNext in fileSysInfoHistory)
                        {
                            MarkFilesToDelete(diNext, "index.dat");
                        }

                        sIndexDat = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                        DirectoryInfo diInfoIEIndexDatCache = new DirectoryInfo(sIndexDat);
                        FileSystemInfo[] fileSysInfoCache = diInfoIEIndexDatCache.GetDirectories();
                        foreach (DirectoryInfo diNext in fileSysInfoCache)
                        {
                            MarkFilesToDelete(diNext, "index.dat");
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
                            DirectoryInfo mediaPlayerDir = new DirectoryInfo(localData + "\\Microsoft\\Media Player");
                            foreach (DirectoryInfo d in mediaPlayerDir.GetDirectories())
                            {
                                if (d.Name.ToLower().Contains("cache"))
                                {
                                    MarkDirToDelete(d);
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
                            DirectoryInfo quickTimeDir = new DirectoryInfo(localLowData + "\\Apple Computer\\quicktime\\downloads");
                            MarkDirToDelete(quickTimeDir);
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
                            DirectoryInfo macromediaFlashDir = new DirectoryInfo(roamingData + "\\Macromedia\\Flash Player");
                            MarkDirToDelete(macromediaFlashDir);
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
                            DirectoryInfo officeDir = new DirectoryInfo(roamingData + "\\Microsoft\\Office");
                            MarkDirToDelete(officeDir);
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
                            DirectoryInfo javaDir = new DirectoryInfo(localData + "\\Sun\\Java\\Deployment\\cache");
                            MarkDirToDelete(javaDir);
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
                            DirectoryInfo officeDir = new DirectoryInfo(programData + "\\Microsoft\\Windows Defender\\Scans\\History\\Results\\Quick");
                            MarkDirToDelete(officeDir);
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
                                DirectoryInfo diInfoFirefoxHistory = new DirectoryInfo(sMozillaHistory);
                                DirectoryInfo[] fileSysFirefoxHistory = diInfoFirefoxHistory.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxHistory)
                                {
                                    MarkFilesToDelete(diNext, "places.sqlite");
                                    MarkFilesToDelete(diNext, "session");
                                    MarkFilesToDelete(diNext, "history.dat");
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
                                DirectoryInfo diInfoFirefoxCookies = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxCookies = diInfoFirefoxCookies.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxCookies)
                                {
                                    MarkFilesToDelete(diNext, "cookies.sqlite");
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
                                DirectoryInfo diInfoFirefoxCookies = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxCookies = diInfoFirefoxCookies.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxCookies)
                                {
                                    DirectoryInfo cacheFolder1 = diNext.GetDirectories("OfflineCache").FirstOrDefault();
                                    MarkFilesToDelete(cacheFolder1);

                                    DirectoryInfo cacheFolder2 = diNext.GetDirectories("Cache").FirstOrDefault();
                                    MarkFilesToDelete(cacheFolder2);
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
                                DirectoryInfo diInfoFirefoxformHistory = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxformHistory = diInfoFirefoxformHistory.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxformHistory)
                                {
                                    MarkFilesToDelete(diNext, "formhistory.sqlite");
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
                                DirectoryInfo diInfoFirefoxformPwd = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxformPwd = diInfoFirefoxformPwd.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxformPwd)
                                {
                                    MarkFilesToDelete(diNext, "key3.db");
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
                                DirectoryInfo diInfoFirefoxformDownload = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxformDownload = diInfoFirefoxformDownload.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxformDownload)
                                {
                                    MarkFilesToDelete(diNext, "downloads.sqlite");
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
                                    MarkFilesToDelete(diNext, "search.sqlite");
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
                                DirectoryInfo diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
                                DirectoryInfo[] profileDirs = diInfoChromeHistory.GetDirectories();
                                foreach (DirectoryInfo dir in profileDirs)
                                {
                                    MarkFilesToDelete(dir, "History");                            
                                    MarkFilesToDelete(dir, "*Visited Links*");
                                    MarkFilesToDelete(dir, "*Current Tabs*");
                                    MarkFilesToDelete(dir, "*Top Sites");
                                    MarkFilesToDelete(dir, "*Network Action Predictor");
                                }
                                diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath + @"\Default\JumpListIcons");
                                MarkDirToDelete(diInfoChromeHistory);
                                diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath + @"\Default\JumpListIconsOld");
                                MarkDirToDelete(diInfoChromeHistory);
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
                                DirectoryInfo diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
                                DirectoryInfo[] profileDirs = diInfoChromeHistory.GetDirectories();

                                foreach (DirectoryInfo dir in profileDirs)
                                {
                                    MarkFilesToDelete(dir, "*Cookies*");
                                }

                                diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath + "\\Default\\Local Storage");
                                MarkDirToDelete(diInfoChromeHistory);
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
                                MarkFilesToDelete(cache, "*");
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
                                DirectoryInfo diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
                                DirectoryInfo[] profileDirs = diInfoChromeHistory.GetDirectories();

                                foreach (DirectoryInfo dir in profileDirs)
                                {
                                    MarkFilesToDelete(dir, "*Web Data*");
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
                                DirectoryInfo diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
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
                                                            txtResults.Text = string.Empty;
                                                            txtResults.SelectionStart = txtResults.Text.Length;
                                                            txtResults.ScrollToCaret();
                                                            txtResults.Refresh();
                                                        }));
            }
        }

        /// <summary>
        /// handle click event to erase tracks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsbErase_Click(object sender, EventArgs e)
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
                            if (n2.Text.ToLower().Contains("internet explorer"))
                            {
                                Process[] p = Process.GetProcessesByName("iexplore");
                                if (p.Length > 0)
                                {
                                    MessageBox.Show(rm.GetString("close_ie_to_erase"), rm.GetString("tracks_eraser"), MessageBoxButtons.OK,
                                                    MessageBoxIcon.Warning);
                                    return;
                                }
                            }

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
                MessageBox.Show(String.Format("{0}{1}{2}", rm.GetString("error_no_tracks_checked"), Environment.NewLine,
                                rm.GetString("error_check_tracks")),
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
                        Browser.ClearHistory(BrowserType.FireFox);
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

            MessageBox.Show(rm.GetString("error_erased_succ"), rm.GetString("info"),
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
            frmOptions frmOptions = new frmOptions();
            frmOptions.ShowDialog();
        }

        /// <summary>
        /// handle click event to check all items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tmiCheckAll_Click(object sender, EventArgs e)
        {
            CheckUncheckAllNodes(true);
        }

        /// <summary>
        /// handle click event to check none items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tmiUncheckAll_Click(object sender, EventArgs e)
        {
            CheckUncheckAllNodes(false);
        }

        /// <summary>
        /// Checks or unchecks all nodes in treeview
        /// </summary>
        /// <param name="isChecked">true if we need to check, false for unchecking</param>
        void CheckUncheckAllNodes(bool isChecked)
        {
            for (int i = 0; i < trvTracks.Nodes.Count; i++)
            {
                trvTracks.Nodes[i].Checked = isChecked;
            }
        }

        /// <summary>
        /// Mark not installed apps in form
        /// </summary>
        public void CheckNotInstalledApps()
        {
            try
            {
                if (!Helper.IsApplictionInstalled("firefox"))
                {
                    trvTracks.Nodes[3].ForeColor = Color.DarkGray;
                    for (int i = 0; i < trvTracks.Nodes[3].Nodes.Count; i++)
                    {
                        trvTracks.Nodes[3].Nodes[i].ForeColor = Color.DarkGray;
                        trvTracks.Nodes[3].Nodes[i].ImageIndex = 7;
                    }
                }

                if (!Helper.IsApplictionInstalled("google chrome") && !Helper.IsBrowserInstalled("chrome"))
                {
                    trvTracks.Nodes[4].ForeColor = Color.DarkGray;
                    for (int i = 0; i < trvTracks.Nodes[4].Nodes.Count; i++)
                    {
                        trvTracks.Nodes[4].Nodes[i].ForeColor = Color.DarkGray;
                        trvTracks.Nodes[4].Nodes[i].ImageIndex = 7;
                    }
                }

                if (!Helper.IsApplictionInstalled("quicktime"))
                {
                    trvTracks.Nodes[2].Nodes[1].ForeColor = Color.DarkGray;
                    trvTracks.Nodes[2].Nodes[1].ImageIndex = 7;
                }

                if (!Helper.IsApplictionInstalled("adobe flash player"))
                {
                    trvTracks.Nodes[2].Nodes[2].ForeColor = Color.DarkGray;
                    trvTracks.Nodes[2].Nodes[2].ImageIndex = 7;
                }

                if (!Helper.IsMSOfficeInstalled())
                {
                    trvTracks.Nodes[2].Nodes[3].ForeColor = Color.DarkGray;
                    trvTracks.Nodes[2].Nodes[3].ImageIndex = 7;
                }

                if (!Helper.IsApplictionInstalled("winrar"))
                {
                    trvTracks.Nodes[2].Nodes[7].ForeColor = Color.DarkGray;
                    trvTracks.Nodes[2].Nodes[7].ImageIndex = 7;
                }

                if (!Helper.IsApplictionInstalled("java "))
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

        /// <summary>
        /// Marks all files and directories in directory to delete
        /// </summary>
        /// <param name="dir">Directory</param>
        private void MarkDirToDelete(DirectoryInfo dir)
        {
            foreach (FileInfo fi in dir.GetFiles("*", SearchOption.AllDirectories))
            {
                filesToDelete.Add(fi.FullName);
            }
            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                foldersToDelete.Add(di.FullName);
            }
        }

        /// <summary>
        /// Marks all files in directory to delete
        /// </summary>
        /// <param name="di">Directory</param>
        private void MarkFilesToDelete(DirectoryInfo di)
        {
            if (di != null)
            {
                foreach (FileInfo file in di.GetFiles("*", SearchOption.AllDirectories))
                {
                    filesToDelete.Add(file.FullName);
                }
            }
        }

        /// <summary>
        /// Marks files match of specific pattern in directory to delete
        /// </summary>
        /// <param name="di">Directory</param>
        /// <param name="pattern">Patern</param>
        private void MarkFilesToDelete(DirectoryInfo di, string pattern)
        {
            if (di != null)
            {
                foreach (FileInfo file in di.GetFiles(pattern))
                {
                    filesToDelete.Add(file.FullName);
                }
            }
        }
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