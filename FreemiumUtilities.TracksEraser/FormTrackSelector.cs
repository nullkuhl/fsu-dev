using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using FreemiumUtilities.Infrastructure;
using Microsoft.Win32;
using UrlHistoryLibrary;
using Knots.Security.TracksEraserCore;

namespace FreemiumUtilities.TracksEraser
{
    /// <summary>
    /// Tracks Eraser 1 Click-Maintenance application track selector form
    /// </summary>
    public partial class frmTrackSelector : Form
    {
        /// <summary>
        /// Empty Recycle Bin flags
        /// </summary>
        const int SHERB_NOCONFIRMATION = 0x00000001;

        const int SHERB_NOPROGRESSUI = 0x00000002;
        const int SHERB_NOSOUND = 0x00000004;

        /// <summary>
        /// <see cref="UrlHistoryWrapperClass.STATURLEnumerator"/> instance
        /// </summary>
        public static UrlHistoryWrapperClass.STATURLEnumerator Enumerator;

        /// <summary>
        /// <see cref="UrlHistoryWrapperClass"/> instance
        /// </summary>
        public static UrlHistoryWrapperClass UrlHistory;

        bool ABORT;

        /// <summary>
        /// <see cref="ProgressUpdate"/> callback
        /// </summary>
        public ProgressUpdate Callback;

        /// <summary>
        /// <see cref="Infrastructure.CancelComplete"/> callback
        /// </summary>
        public CancelComplete CancelComplete;

        /// <summary>
        /// <see cref="ScanComplete"/> callback
        /// </summary>
        public ScanComplete Complete;

        /// <summary>
        /// Files to delete count
        /// </summary>
        public int FilesToDeletedCount;

        public int ItemsToDeleteAll;
        public int ItemsToDeleteAvailable;

        /// <summary>
        /// Resource manager instance
        /// </summary>
        public ResourceManager ResourceManager = new ResourceManager("TracksEraser", Assembly.GetExecutingAssembly());

        string analysisReport;
        BackgroundWorker bgWorker;

        string detailsFilesTobeDel;
        string detailsRegTobeDel;
        string error;
        string files;
        List<string> filesToDelete;
        bool fixAfterScan;
        List<string> foldersToDelete;
        int maxCnt;
        ulong recycleBinCount;
        ulong recycleBinSize;
        List<string> regValuesToDelete;

        // We still use ResourceManager for localization inside BackgroundWorkers

        string tobeRemoved;
        string tobeRemovedWinReg;
        BackgroundWorker worker;

        private bool isCancel = false;
        //Now the Win32 API

        /// <summary>
        /// constructor for frmTrackSelector
        /// </summary>
        public frmTrackSelector()
        {
            InitializeComponent();
            InitFormClassParams();
            // Check all nodes on app start
            GetInstalledApps();
            CheckAllNodes();
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
        /// initialize frmTrackSelector
        /// </summary>
        void InitFormClassParams()
        {
            var culture = new CultureInfo(CfgFile.Get("Lang"));
            SetCulture(culture);

            filesToDelete = new List<string>();
            foldersToDelete = new List<string>();
            regValuesToDelete = new List<string>();
            UrlHistory = new UrlHistoryWrapperClass();
            Enumerator = UrlHistory.GetEnumerator();
        }

        /// <summary>
        /// check all items
        /// </summary>
        void CheckAllNodes()
        {
            Process[] chrome = Process.GetProcessesByName("chrome");
            Process[] firefox = Process.GetProcessesByName("firefox");
            Process[] iexplore = Process.GetProcessesByName("iexplore");

            foreach (TreeNode node in trvMain.Nodes)
            {
                // All nodes inside will be checked too as we have helper for it

                if (chrome.Length > 0)
                {
                    if (node.Text.Contains("Google Chrome"))
                    {
                        continue;
                    }
                }

                if (firefox.Length > 0)
                {
                    if (node.Text.Contains("Mozilla Firefox"))
                    {
                        continue;
                    }
                }

                if (iexplore.Length > 0)
                {
                    if (node.Text.Contains("Internet Explorer"))
                    {
                        continue;
                    }
                }

                node.Checked = node.ForeColor != Color.DarkGray;
            }
        }

        /// <summary>
        /// check if any item is checked
        /// </summary>
        /// <returns></returns>
        bool IsAnyNodeChecked()
        {
            foreach (TreeNode n1 in trvMain.Nodes)
            {
                if (n1.Checked)
                    return true;
                foreach (TreeNode n2 in n1.Nodes)
                {
                    if (n2.Checked)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Show TreeView
        /// </summary>
        public void ShowTreeView()
        {
            spcMain.Panel1Collapsed = false;
            spcMain.Panel2Collapsed = true;
        }

        /// <summary>
        /// Show all panels
        /// </summary>
        public void ShowAll()
        {
            spcMain.Panel1Collapsed = false;
            spcMain.Panel2Collapsed = false;
            resultsTxt.Visible = true;
        }

        /// <summary>
        /// initialize frmTrackSelector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmTrackSelector_Load(object sender, EventArgs e)
        {
            InitFormClassParams();

            //TreeNode node = new TreeNode("Win");
            //this.treeView1.Nodes.Add(node);

            trvMain.ExpandAll();
            trvMain.SelectedNode = trvMain.Nodes[0];


            Process[] pname = Process.GetProcessesByName("iexplore");
            if (pname.Length > 0)
            {
                trvMain.Nodes[1].ForeColor = Color.DarkGray;
                for (int i = 0; i < trvMain.Nodes[1].Nodes.Count; i++)
                {
                    trvMain.Nodes[1].Nodes[i].ForeColor = Color.DarkGray;
                    trvMain.Nodes[1].Nodes[i].ImageIndex = 7;
                }
            }

            pname = Process.GetProcessesByName("firefox");
            if (pname.Length > 0)
            {
                trvMain.Nodes[3].ForeColor = Color.DarkGray;
                for (int i = 0; i < trvMain.Nodes[3].Nodes.Count; i++)
                {
                    trvMain.Nodes[3].Nodes[i].ForeColor = Color.DarkGray;
                    trvMain.Nodes[3].Nodes[i].ImageIndex = 7;
                }
            }

            pname = Process.GetProcessesByName("chrome");
            if (pname.Length > 0)
            {
                trvMain.Nodes[4].ForeColor = Color.DarkGray;
                for (int i = 0; i < trvMain.Nodes[4].Nodes.Count; i++)
                {
                    trvMain.Nodes[4].Nodes[i].ForeColor = Color.DarkGray;
                    trvMain.Nodes[4].Nodes[i].ImageIndex = 7;
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

            trvMain.Nodes[0].Nodes[0].Text = WPFLocalizeExtensionHelpers.GetUIString("win_rec_doc");
            trvMain.Nodes[0].Nodes[1].Text = WPFLocalizeExtensionHelpers.GetUIString("win_st_menu_run");
            trvMain.Nodes[0].Nodes[2].Text = WPFLocalizeExtensionHelpers.GetUIString("file_folder_list");
            trvMain.Nodes[0].Nodes[3].Text = WPFLocalizeExtensionHelpers.GetUIString("win_clipboard");
            trvMain.Nodes[0].Nodes[4].Text = WPFLocalizeExtensionHelpers.GetUIString("win_temp_files");
            trvMain.Nodes[0].Nodes[5].Text = WPFLocalizeExtensionHelpers.GetUIString("win_rec_bin");
            trvMain.Nodes[0].Nodes[6].Text = WPFLocalizeExtensionHelpers.GetUIString("win_map_drives");
            trvMain.Nodes[0].Nodes[7].Text = WPFLocalizeExtensionHelpers.GetUIString("start_menu_history");
            trvMain.Nodes[0].Nodes[8].Text = WPFLocalizeExtensionHelpers.GetUIString("network_places");
            trvMain.Nodes[0].Text = WPFLocalizeExtensionHelpers.GetUIString("win");

            trvMain.Nodes[1].Nodes[0].Text = WPFLocalizeExtensionHelpers.GetUIString("ie_url_history");
            trvMain.Nodes[1].Nodes[1].Text = WPFLocalizeExtensionHelpers.GetUIString("ie_history");
            trvMain.Nodes[1].Nodes[2].Text = WPFLocalizeExtensionHelpers.GetUIString("ie_cookies");
            trvMain.Nodes[1].Nodes[3].Text = WPFLocalizeExtensionHelpers.GetUIString("auto_pass");
            trvMain.Nodes[1].Nodes[4].Text = WPFLocalizeExtensionHelpers.GetUIString("temp_internet_files");
            trvMain.Nodes[1].Nodes[5].Text = WPFLocalizeExtensionHelpers.GetUIString("index_files");
            trvMain.Nodes[1].Text = WPFLocalizeExtensionHelpers.GetUIString("ie");

            trvMain.Nodes[2].Nodes[0].Text = WPFLocalizeExtensionHelpers.GetUIString("mp");
            trvMain.Nodes[2].Nodes[1].Text = WPFLocalizeExtensionHelpers.GetUIString("qp");
            trvMain.Nodes[2].Nodes[2].Text = WPFLocalizeExtensionHelpers.GetUIString("mmfp");
            trvMain.Nodes[2].Nodes[3].Text = WPFLocalizeExtensionHelpers.GetUIString("office");
            trvMain.Nodes[2].Nodes[4].Text = WPFLocalizeExtensionHelpers.GetUIString("ms_management_console");
            trvMain.Nodes[2].Nodes[5].Text = WPFLocalizeExtensionHelpers.GetUIString("ms_wordpad");
            trvMain.Nodes[2].Nodes[6].Text = WPFLocalizeExtensionHelpers.GetUIString("ms_paint");
            trvMain.Nodes[2].Nodes[7].Text = WPFLocalizeExtensionHelpers.GetUIString("winrar");
            trvMain.Nodes[2].Nodes[8].Text = WPFLocalizeExtensionHelpers.GetUIString("sun_java");
            trvMain.Nodes[2].Nodes[9].Text = WPFLocalizeExtensionHelpers.GetUIString("win_def");
            trvMain.Nodes[2].Text = WPFLocalizeExtensionHelpers.GetUIString("plugins");

            trvMain.Nodes[3].Nodes[0].Text = WPFLocalizeExtensionHelpers.GetUIString("firefox_history");
            trvMain.Nodes[3].Nodes[1].Text = WPFLocalizeExtensionHelpers.GetUIString("firefox_cookies");
            trvMain.Nodes[3].Nodes[2].Text = WPFLocalizeExtensionHelpers.GetUIString("firefox_cache");
            trvMain.Nodes[3].Nodes[3].Text = WPFLocalizeExtensionHelpers.GetUIString("firefox_saved_info");
            trvMain.Nodes[3].Nodes[4].Text = WPFLocalizeExtensionHelpers.GetUIString("firefox_saved_pass");
            trvMain.Nodes[3].Nodes[5].Text = WPFLocalizeExtensionHelpers.GetUIString("firefox_download_history");
            trvMain.Nodes[3].Nodes[6].Text = WPFLocalizeExtensionHelpers.GetUIString("firefox_search_history");
            trvMain.Nodes[3].Text = WPFLocalizeExtensionHelpers.GetUIString("firefox");

            trvMain.Nodes[4].Nodes[0].Text = WPFLocalizeExtensionHelpers.GetUIString("google_history");
            trvMain.Nodes[4].Nodes[1].Text = WPFLocalizeExtensionHelpers.GetUIString("google_cookies");
            trvMain.Nodes[4].Nodes[2].Text = WPFLocalizeExtensionHelpers.GetUIString("google_cache");
            trvMain.Nodes[4].Nodes[3].Text = WPFLocalizeExtensionHelpers.GetUIString("google_saved_info");
            trvMain.Nodes[4].Nodes[4].Text = WPFLocalizeExtensionHelpers.GetUIString("google_download_history");
            trvMain.Nodes[4].Text = WPFLocalizeExtensionHelpers.GetUIString("google");
            Text = WPFLocalizeExtensionHelpers.GetUIString("tracks_eraser");
            lblSelect.Text = WPFLocalizeExtensionHelpers.GetUIString("SelectTracksToRemove");

            analysisReport = WPFLocalizeExtensionHelpers.GetUIString("analysis_report");
            tobeRemovedWinReg = WPFLocalizeExtensionHelpers.GetUIString("tobe_removed_win_reg");
            files = WPFLocalizeExtensionHelpers.GetUIString("files");
            tobeRemoved = WPFLocalizeExtensionHelpers.GetUIString("tobe_removed");
            detailsRegTobeDel = WPFLocalizeExtensionHelpers.GetUIString("details_reg_tobe_del");
            detailsFilesTobeDel = WPFLocalizeExtensionHelpers.GetUIString("details_files_tobe_del");
            error = WPFLocalizeExtensionHelpers.GetUIString("error");
        }

        /// <summary>
        /// Starts scanning files
        /// </summary>
        /// <param name="fixErrorsAfterScan">Delete tracks after scanning</param>
        public void ScanFiles(bool fixErrorsAfterScan)
        {
            filesToDelete = new List<string>();
            foldersToDelete = new List<string>();
            regValuesToDelete = new List<string>();
            UrlHistory = new UrlHistoryWrapperClass();
            Enumerator = UrlHistory.GetEnumerator();

            fixAfterScan = fixErrorsAfterScan;
            if (!IsAnyNodeChecked())
            {
                // Check all nodes if none selected
                CheckAllNodes();
            }
            StartScan();
        }

        void StartScan()
        {
            worker = new BackgroundWorker
                        {
                            WorkerSupportsCancellation = true,
                            WorkerReportsProgress = true
                        };
            worker.ProgressChanged += ProgressChanged;
            maxCnt = 1;
            isCancel = false;
            foreach (TreeNode n1 in trvMain.Nodes)
            {
                foreach (TreeNode n2 in n1.Nodes)
                {
                    if (n2.Checked)
                    {
                        maxCnt++;
                    }
                }
            }

            worker.DoWork += DoWork;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// Cancel scanning files
        /// </summary>
        public void CancelScan()
        {
            isCancel = true;
            if (worker.IsBusy)
                worker.CancelAsync();
            if (bgWorker.IsBusy)
                bgWorker.CancelAsync();
        }

        /// <summary>
        /// handle progress changed event to show current progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!isCancel)
            {
                Callback(e.ProgressPercentage, e.UserState.ToString());
            }
        }

        void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int prgTracker = 0;
                if (maxCnt == 0)
                {
                    MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("error_select") + "!",
                                    WPFLocalizeExtensionHelpers.GetUIString("warning"), MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    Complete(fixAfterScan);

                    return;
                }
                //Update progress bar
                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "");

                if (resultsTxt.InvokeRequired)
                {
                    resultsTxt.Invoke(new MethodInvoker(delegate { resultsTxt.Text = ""; }));
                }
                else
                {
                    resultsTxt.Text = "";
                }


                if (filesToDelete != null)
                    filesToDelete.Clear();

                if (regValuesToDelete != null)
                    regValuesToDelete.Clear();

                if (foldersToDelete != null)
                    foldersToDelete.Clear();

                recycleBinCount = 0;
                recycleBinSize = 0;

                bool res = false;
                bool res1 = false;

                #region Windows Recent Documents

                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[0].Nodes[0].Checked; }));
                }
                else
                {
                    res = trvMain.Nodes[0].Nodes[0].Checked;
                }

                if (res)
                {
                    try
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Windows Recent Documents");


                        var d = new DirectoryInfo(
                            Environment.GetFolderPath(Environment.SpecialFolder.Recent));

                        DirectoryInfo[] dires = d.GetDirectories("*", SearchOption.AllDirectories);
                        FileInfo[] files = d.GetFiles();

                        foreach (DirectoryInfo dir in dires)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }
                            foldersToDelete.Add(dir.FullName);
                            filesToDelete.Add(dir.FullName);

                            //Update progress bar
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), dir.FullName);

                        }
                        foreach (FileInfo f in files)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }
                            filesToDelete.Add(f.FullName);

                            //Update progress bar
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Start Menu Run

                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[0].Nodes[1].Checked; }));
                }
                else
                {
                    res = trvMain.Nodes[0].Nodes[1].Checked;
                }

                if (res)
                {
                    try
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Start Menu Run");

                        string runRegKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU\";
                        using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(runRegKeyPath))
                        {
                            if (regKey != null)
                            {
                                foreach (string valueName in regKey.GetValueNames())
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                    //Update progress bar
                                    worker.ReportProgress((int)((float)(int)((float)prgTracker / maxCnt * 100)), valueName);

                                    if (valueName.ToLower() != "default")
                                    {
                                        regValuesToDelete.Add(@"Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU\" + valueName);

                                        //Update progress bar
                                        worker.ReportProgress((int)((float)(int)((float)prgTracker / maxCnt * 100)), regValuesToDelete[regValuesToDelete.Count - 1]);
                                    }
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

                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[0].Nodes[2].Checked; }));
                }
                else
                {
                    res = trvMain.Nodes[0].Nodes[2].Checked;
                }

                if (res)
                {
                    try
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Common Dialog File/Folder List");


                        string runRegKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\";
                        using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(runRegKeyPath))
                        {
                            foreach (string subKeyName in regKey.GetSubKeyNames())
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }
                                //Update progress bar
                                worker.ReportProgress((int)((float)(int)((float)prgTracker / maxCnt * 100)), subKeyName);


                                if (subKeyName == "LastVisitedPidlMRU" || subKeyName == "OpenSavePidlMRU")
                                {
                                    regValuesToDelete.Add(@"Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\" + subKeyName);

                                    //Update progress bar
                                    worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), regValuesToDelete[regValuesToDelete.Count - 1]);

                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Windows Clipboard

                //Clipboard.SetText("Hello");
                //Clipboard.Clear();
                try
                {
                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[0].Nodes[3].Checked; }));
                    }
                    else
                    {
                        res = trvMain.Nodes[0].Nodes[3].Checked;
                    }

                    if (res)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Windows Clipboard");

                        Object obj = Clipboard.GetData("*");
                    }
                }
                catch
                {
                }

                #endregion

                #region Windows Temporary Files

                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[0].Nodes[4].Checked; }));
                }
                else
                {
                    res = trvMain.Nodes[0].Nodes[4].Checked;
                }

                if (res)
                {
                    try
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Windows Temporary Files");

                        //string sWinTempFilesAddress = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).ToString();
                        string sWinTempFilesAddress = Environment.GetEnvironmentVariable("temp"); //sWinTempFilesAddress + "\\Temp";

                        var dirWindowsTempFiles = new DirectoryInfo(sWinTempFilesAddress);
                        FileSystemInfo[] fileSysInfoWinTemp = dirWindowsTempFiles.GetFiles("*", SearchOption.AllDirectories);
                        foreach (FileInfo f in fileSysInfoWinTemp)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            filesToDelete.Add(f.FullName);

                            //Update progress bar
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);

                        }
                        foreach (DirectoryInfo di in dirWindowsTempFiles.GetDirectories())
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            foldersToDelete.Add(di.FullName);
                        }

                        //TODO : Find Windows folder
                        sWinTempFilesAddress = Environment.GetEnvironmentVariable("windir");
                        sWinTempFilesAddress = sWinTempFilesAddress + "\\temp";

                        dirWindowsTempFiles = new DirectoryInfo(sWinTempFilesAddress);
                        fileSysInfoWinTemp = dirWindowsTempFiles.GetFiles("*", SearchOption.AllDirectories);
                        foreach (FileInfo f in fileSysInfoWinTemp)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            filesToDelete.Add(f.FullName);

                            //Update progress bar
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);

                        }
                        foreach (DirectoryInfo di in dirWindowsTempFiles.GetDirectories())
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            foldersToDelete.Add(di.FullName);
                        }

                        string windir = Environment.GetEnvironmentVariable("windir");

                        filesToDelete.Add(windir + @"\Memory.dmp");
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), filesToDelete[filesToDelete.Count - 1]);

                        try
                        {
                            foreach (string f in Directory.GetFiles(windir, "*.log", SearchOption.AllDirectories))
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(f);

                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f);

                            }
                        }
                        catch
                        {
                        }

                        try
                        {
                            foreach (string f in Directory.GetFiles(windir + @"\minidump", "*", SearchOption.AllDirectories))
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(f);
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f);

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
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(f);
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f);

                            }
                            var folders =
                                new DirectoryInfo(Environment.GetEnvironmentVariable("PROGRAMDATA") + "\\Microsoft\\Windows\\WER\\ReportArchive");
                            foreach (DirectoryInfo di in folders.GetDirectories())
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

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
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(f);
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f);

                            }
                            var folders =
                                new DirectoryInfo(Environment.GetEnvironmentVariable("PROGRAMDATA") + "\\Microsoft\\Windows\\WER\\ReportQueue");
                            foreach (DirectoryInfo di in folders.GetDirectories())
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

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
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(f);
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f);

                            }
                            var folders = new DirectoryInfo(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\ElevatedDiagnostics");
                            foreach (DirectoryInfo di in folders.GetDirectories())
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

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
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(f);
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f);

                            }
                            var folders =
                                new DirectoryInfo(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Microsoft\\Windows\\WER\\ReportQueue");
                            foreach (DirectoryInfo di in folders.GetDirectories())
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

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
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(f);
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f);

                            }
                            var folders =
                                new DirectoryInfo(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Microsoft\\Windows\\WER\\ReportArchive");
                            foreach (DirectoryInfo di in folders.GetDirectories())
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

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

                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[0].Nodes[5].Checked; }));
                }
                else
                {
                    res = trvMain.Nodes[0].Nodes[5].Checked;
                }

                if (res)
                {
                    try
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Windows recycle bin");


                        var sqrbi = new SHQUERYRBINFO();
                        sqrbi.cbSize = Marshal.SizeOf(typeof(SHQUERYRBINFO));
                        int result = SHQueryRecycleBin("", ref sqrbi);
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

                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[0].Nodes[6].Checked; }));
                }
                else
                {
                    res = trvMain.Nodes[0].Nodes[6].Checked;
                    res1 = trvMain.Nodes[0].Nodes[8].Checked;
                }
                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res1 = trvMain.Nodes[0].Nodes[8].Checked; }));
                }
                if (res || res1)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (res && res1)
                        prgTracker = prgTracker + 2;
                    else
                        prgTracker++;

                    worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Mapped Drives");
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
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            try
                            {
                                //Update progress bar
                                if (drive.DriveType == DriveType.Network)
                                {
                                    filesToDelete.Add(drive.Name);

                                    //Update progress bar
                                    worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), drive.Name);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }

                #endregion

                #region Start Menu Click History

                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[0].Nodes[7].Checked; }));
                }
                else
                {
                    res = trvMain.Nodes[0].Nodes[7].Checked;
                }

                if (res)
                {
                    try
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Start Menu Click History");

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

                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[1].Nodes[0].Checked; }));
                }
                else
                {
                    res = trvMain.Nodes[1].Nodes[0].Checked;
                }

                if (res)
                {
                    try
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Internet Explorer URL History");

                        while (Enumerator.MoveNext())
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            string s = Enumerator.Current.pwcsUrl;
                            if (s.StartsWith("http"))
                            {
                                filesToDelete.Add(s);

                                //Update progress bar
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), s);
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Internet Explorer History

                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[1].Nodes[1].Checked; }));
                }
                else
                {
                    res = trvMain.Nodes[1].Nodes[1].Checked;
                }
                if (res)
                {
                    try
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Internet Explorer History");
                        var ieHistory1 =
                            new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                              @"\Microsoft\Windows\History");
                        foreach (FileInfo fi in ieHistory1.GetFiles("*", SearchOption.AllDirectories))
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            filesToDelete.Add(fi.FullName);
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), fi.FullName);
                        }
                        foreach (DirectoryInfo di in ieHistory1.GetDirectories())
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            foldersToDelete.Add(di.FullName);
                        }

                        var ieHistory2 =
                            new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                              @"\Microsoft\Internet Explorer\Recovery");
                        foreach (FileInfo fi in ieHistory2.GetFiles("*", SearchOption.AllDirectories))
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            filesToDelete.Add(fi.FullName);
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), fi.FullName);
                        }
                        foreach (DirectoryInfo di in ieHistory2.GetDirectories())
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            foldersToDelete.Add(di.FullName);
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Internet Explorer Cache

                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[1].Nodes[2].Checked; }));
                }
                else
                {
                    res = trvMain.Nodes[1].Nodes[2].Checked;
                }

                if (res)
                {
                    try
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;

                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Internet Explorer Cache");

                        var dIECookies = new DirectoryInfo(
                            Environment.GetFolderPath(Environment.SpecialFolder.Cookies));

                        DirectoryInfo[] dirCookies = dIECookies.GetDirectories("*", SearchOption.AllDirectories);
                        FileInfo[] filesCookies = dIECookies.GetFiles("*", SearchOption.AllDirectories);

                        foreach (DirectoryInfo dir in dirCookies)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            foldersToDelete.Add(dir.FullName);

                            //Update progress bar
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), dir.FullName);
                        }
                        foreach (FileInfo f in filesCookies)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            filesToDelete.Add(f.FullName);

                            //Update progress bar
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region AutoComplete Passwords

                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[1].Nodes[3].Checked; }));
                }
                else
                {
                    res = trvMain.Nodes[1].Nodes[3].Checked;
                }

                if (res)
                {
                    try
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "AutoComplete Passwords");

                        string uninstallKey = @"Software\Microsoft\Internet Explorer\TypedURLs";
                        using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(uninstallKey))
                        {
                            foreach (string skName in rk.GetValueNames())
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                //Update progress bar
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), skName);

                                if (skName.ToLower() != "default")
                                {
                                    regValuesToDelete.Add(@"Software\Microsoft\Internet Explorer\TypedURLs\" + skName);

                                    //Update progress bar
                                    worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), regValuesToDelete[regValuesToDelete.Count - 1]);
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

                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[1].Nodes[4].Checked; }));
                }
                else
                {
                    res = trvMain.Nodes[1].Nodes[4].Checked;
                }

                if (res)
                {
                    try
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Temporary Internet Files");

                        string tempDir = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                        var diInfoIECache = new DirectoryInfo(tempDir);
                        DirectoryInfo[] dirCache = diInfoIECache.GetDirectories("*", SearchOption.AllDirectories);
                        FileInfo[] filesCache = diInfoIECache.GetFiles();

                        foreach (FileInfo fi in filesCache)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            filesToDelete.Add(fi.FullName);

                            //Update progress bar
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), fi.FullName);
                        }

                        foreach (DirectoryInfo dir in dirCache)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            foldersToDelete.Add(dir.FullName);
                        }

                        tempDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Feeds Cache";
                        diInfoIECache = new DirectoryInfo(tempDir);

                        dirCache = diInfoIECache.GetDirectories("*", SearchOption.AllDirectories);
                        filesCache = diInfoIECache.GetFiles();

                        foreach (FileInfo fi in filesCache)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            filesToDelete.Add(fi.FullName);

                            //Update progress bar
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), fi.FullName);
                        }

                        foreach (DirectoryInfo dir in dirCache)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            foldersToDelete.Add(dir.FullName);
                        }
                    }
                    catch
                    {
                    }
                }

                #endregion

                #region Index.dat

                if (trvMain.InvokeRequired)
                {
                    trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[1].Nodes[5].Checked; }));
                }
                else
                {
                    res = trvMain.Nodes[1].Nodes[5].Checked;
                }

                if (res)
                {
                    try
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Index.dat");

                        string sIndexDat = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
                        var diInfoIEIndexDatCookies = new DirectoryInfo(sIndexDat);
                        FileInfo[] fileInfoIndexDatCookies = diInfoIEIndexDatCookies.GetFiles("index.dat");
                        filesToDelete.Add(fileInfoIndexDatCookies[0].FullName);

                        sIndexDat = Environment.GetFolderPath(Environment.SpecialFolder.History);
                        var diInfoIEIndexDatHistory = new DirectoryInfo(sIndexDat);
                        FileSystemInfo[] fileSysInfoHistory = diInfoIEIndexDatHistory.GetDirectories();
                        foreach (DirectoryInfo diNext in fileSysInfoHistory)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            //Update progress bar
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);

                            FileInfo[] datfiles = diNext.GetFiles("index.dat");
                            foreach (FileInfo f in datfiles)
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(f.FullName);

                                //Update progress bar
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);
                            }
                        }

                        // prgTracker++;

                        sIndexDat = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                        var diInfoIEIndexDatCache = new DirectoryInfo(sIndexDat);
                        FileSystemInfo[] fileSysInfoCache = diInfoIEIndexDatCache.GetDirectories();
                        foreach (DirectoryInfo diNext in fileSysInfoCache)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            //Update progress bar
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);

                            FileInfo[] datfiles = diNext.GetFiles("index.dat");
                            foreach (FileInfo f in datfiles)
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(f.FullName);
                                //Update progress bar
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);
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
                        if (trvMain.InvokeRequired)
                        {
                            trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[2].Nodes[0].Checked; }));
                        }
                        else
                        {
                            res = trvMain.Nodes[2].Nodes[0].Checked;
                        }
                        if (res)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            prgTracker++;
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Windows Media Player");
                            var mediaPlayerDir = new DirectoryInfo(localData + "\\Microsoft\\Media Player");
                            foreach (DirectoryInfo d in mediaPlayerDir.GetDirectories())
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                if (d.Name.ToLower().Contains("cache"))
                                {
                                    foreach (FileInfo fi in d.GetFiles("*", SearchOption.AllDirectories))
                                    {
                                        if (worker.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }

                                        filesToDelete.Add(fi.FullName);
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), fi.FullName);
                                    }
                                    foreach (DirectoryInfo di in d.GetDirectories())
                                    {
                                        if (worker.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }
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
                        if (trvMain.InvokeRequired)
                        {
                            trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[2].Nodes[1].Checked; }));
                        }
                        else
                        {
                            res = trvMain.Nodes[2].Nodes[1].Checked;
                        }
                        if (res)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            prgTracker++;
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Quick Time Player");

                            var quickTimeDir = new DirectoryInfo(localLowData + "\\Apple Computer\\quicktime\\downloads");
                            foreach (FileInfo fi in quickTimeDir.GetFiles("*", SearchOption.AllDirectories))
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(fi.FullName);
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), fi.FullName);
                            }
                            foreach (DirectoryInfo di in quickTimeDir.GetDirectories())
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

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
                        if (trvMain.InvokeRequired)
                        {
                            trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[2].Nodes[2].Checked; }));
                        }
                        else
                        {
                            res = trvMain.Nodes[2].Nodes[2].Checked;
                        }
                        if (res)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            prgTracker++;
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Macromedia Flash Player");

                            var macromediaFlashDir = new DirectoryInfo(roamingData + "\\Macromedia\\Flash Player");
                            foreach (FileInfo fi in macromediaFlashDir.GetFiles("*", SearchOption.AllDirectories))
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(fi.FullName);
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), fi.FullName);
                            }
                            foreach (DirectoryInfo di in macromediaFlashDir.GetDirectories())
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

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
                        if (trvMain.InvokeRequired)
                        {
                            trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[2].Nodes[3].Checked; }));
                        }
                        else
                        {
                            res = trvMain.Nodes[2].Nodes[3].Checked;
                        }
                        if (res)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            prgTracker++;
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Microsoft Office");

                            var officeDir = new DirectoryInfo(roamingData + "\\Microsoft\\Office");
                            foreach (FileInfo fi in officeDir.GetFiles("*", SearchOption.AllDirectories))
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(fi.FullName);
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), fi.FullName);
                            }
                            foreach (DirectoryInfo di in officeDir.GetDirectories())
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

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
                        if (trvMain.InvokeRequired)
                        {
                            trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[2].Nodes[4].Checked; }));
                        }
                        else
                        {
                            res = trvMain.Nodes[2].Nodes[4].Checked;
                        }
                        if (res)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            prgTracker++;
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "MS Management console");

                            using (
                                RegistryKey regKey =
                                    Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Microsoft Management Console\Recent File List\"))
                            {
                                foreach (string subKeyName in regKey.GetValueNames())
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }

                                    if (subKeyName.ToLower() != "default")
                                    {
                                        string name = @"Software\Microsoft\Microsoft Management Console\Recent File List\" + subKeyName;
                                        regValuesToDelete.Add(name);
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), name);
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
                        if (trvMain.InvokeRequired)
                        {
                            trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[2].Nodes[5].Checked; }));
                        }
                        else
                        {
                            res = trvMain.Nodes[2].Nodes[5].Checked;
                        }
                        if (res)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            prgTracker++;
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "MS Wordpad");
                            using (
                                RegistryKey regKey =
                                    Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Applets\Wordpad\Recent File List\"))
                            {
                                foreach (string subKeyName in regKey.GetValueNames())
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }

                                    if (subKeyName.ToLower() != "default")
                                    {
                                        string name = @"Software\Microsoft\Windows\CurrentVersion\Applets\Wordpad\Recent File List\" + subKeyName;
                                        regValuesToDelete.Add(name);
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), name);
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
                        if (trvMain.InvokeRequired)
                        {
                            trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[2].Nodes[6].Checked; }));
                        }
                        else
                        {
                            res = trvMain.Nodes[2].Nodes[6].Checked;
                        }
                        if (res)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            prgTracker++;
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "MS Paint");

                            using (
                                RegistryKey regKey =
                                    Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Applets\Paint\Recent File List\"))
                            {
                                foreach (string subKeyName in regKey.GetValueNames())
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                    if (subKeyName.ToLower() != "default")
                                    {
                                        string name = @"Software\Microsoft\Windows\CurrentVersion\Applets\Paint\Recent File List\" + subKeyName;
                                        regValuesToDelete.Add(name);
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), name);
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
                        if (trvMain.InvokeRequired)
                        {
                            trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[2].Nodes[7].Checked; }));
                        }
                        else
                        {
                            res = trvMain.Nodes[2].Nodes[7].Checked;
                        }
                        if (res)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            prgTracker++;
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Winrar");

                            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software\WinRAR\ArcHistory\"))
                            {
                                foreach (string subKeyName in regKey.GetValueNames())
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }

                                    if (subKeyName.ToLower() != "default")
                                    {
                                        string name = @"Software\WinRAR\ArcHistory\" + subKeyName;
                                        regValuesToDelete.Add(name);
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), name);
                                    }
                                }
                            }
                            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software\WinRAR\DialogEditHistory\ArcName\"))
                            {
                                foreach (string subKeyName in regKey.GetValueNames())
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }

                                    if (subKeyName.ToLower() != "default")
                                    {
                                        string name = @"Software\WinRAR\DialogEditHistory\ArcName\" + subKeyName;
                                        regValuesToDelete.Add(name);
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), name);
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
                        if (trvMain.InvokeRequired)
                        {
                            trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[2].Nodes[8].Checked; }));
                        }
                        else
                        {
                            res = trvMain.Nodes[2].Nodes[8].Checked;
                        }
                        if (res)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            prgTracker++;
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Sun Java");

                            var javaDir = new DirectoryInfo(localData + "\\Sun\\Java\\Deployment\\cache");
                            foreach (FileInfo fi in javaDir.GetFiles("*", SearchOption.AllDirectories))
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(fi.FullName);
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), fi.FullName);
                            }
                            foreach (DirectoryInfo di in javaDir.GetDirectories())
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

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
                        if (trvMain.InvokeRequired)
                        {
                            trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[2].Nodes[9].Checked; }));
                        }
                        else
                        {
                            res = trvMain.Nodes[2].Nodes[9].Checked;
                        }
                        if (res)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            prgTracker++;
                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Windows Defender");
                            var officeDir = new DirectoryInfo(programData + "\\Microsoft\\Windows Defender\\Scans\\History\\Results\\Quick");
                            foreach (FileInfo fi in officeDir.GetFiles("*", SearchOption.AllDirectories))
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                filesToDelete.Add(fi.FullName);
                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), fi.FullName);
                            }
                            foreach (DirectoryInfo di in officeDir.GetDirectories())
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

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

                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[3].Nodes[0].Checked; }));
                    }
                    else
                    {
                        res = trvMain.Nodes[3].Nodes[0].Checked;
                    }

                    if (res)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Mozilla Firefox History");

                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxHistory = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxHistory = diInfoFirefoxHistory.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxHistory)
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }

                                    //Update progress bar
                                    worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);

                                    foreach (FileInfo f in diNext.GetFiles("places.sqlite"))
                                    {
                                        if (worker.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }

                                        filesToDelete.Add(f.FullName);

                                        //Update progress bar
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);
                                    }
                                    foreach (FileInfo f in diNext.GetFiles("session*"))
                                    {
                                        if (worker.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }

                                        filesToDelete.Add(f.FullName);

                                        //Update progress bar
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);
                                    }
                                    foreach (FileInfo f in diNext.GetFiles("history.dat"))
                                    {
                                        if (worker.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }

                                        filesToDelete.Add(f.FullName);

                                        //Update progress bar
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);
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

                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[3].Nodes[1].Checked; }));
                    }
                    else
                    {
                        res = trvMain.Nodes[3].Nodes[1].Checked;
                    }

                    if (res)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Mozilla Firefox Cookies");
                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxCookies = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxCookies = diInfoFirefoxCookies.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxCookies)
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                    //Update progress bar
                                    worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);

                                    FileInfo[] datfiles = diNext.GetFiles("cookies.sqlite");
                                    foreach (FileInfo f in datfiles)
                                    {
                                        if (worker.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }

                                        filesToDelete.Add(f.FullName);

                                        //Update progress bar
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);
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

                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[3].Nodes[2].Checked; }));
                    }
                    else
                    {
                        res = trvMain.Nodes[3].Nodes[2].Checked;
                    }

                    if (res)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Mozilla Firefox Internet Cache");
                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxCookies = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxCookies = diInfoFirefoxCookies.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxCookies)
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }

                                    //Update progress bar
                                    worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);

                                    DirectoryInfo cacheFolder1 = diNext.GetDirectories("OfflineCache").FirstOrDefault();
                                    if (cacheFolder1 != null)
                                    {
                                        foreach (FileInfo file in cacheFolder1.GetFiles("*", SearchOption.AllDirectories))
                                        {
                                            if (worker.CancellationPending)
                                            {
                                                e.Cancel = true;
                                                return;
                                            }

                                            filesToDelete.Add(file.FullName);
                                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), file.FullName);
                                        }
                                    }

                                    DirectoryInfo cacheFolder2 = diNext.GetDirectories("Cache").FirstOrDefault();
                                    if (cacheFolder2 != null)
                                    {
                                        foreach (FileInfo file in cacheFolder2.GetFiles("*", SearchOption.AllDirectories))
                                        {
                                            if (worker.CancellationPending)
                                            {
                                                e.Cancel = true;
                                                return;
                                            }

                                            filesToDelete.Add(file.FullName);
                                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), file.FullName);
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

                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[3].Nodes[3].Checked; }));
                    }
                    else
                    {
                        res = trvMain.Nodes[3].Nodes[3].Checked;
                    }

                    if (res)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Mozilla Firefox Saved Form information");

                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxformHistory = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxformHistory = diInfoFirefoxformHistory.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxformHistory)
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                    //Update progress bar
                                    worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);

                                    FileInfo[] datfiles = diNext.GetFiles("formhistory.sqlite");
                                    foreach (FileInfo f in datfiles)
                                    {
                                        if (worker.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }

                                        filesToDelete.Add(f.FullName);

                                        //Update progress bar
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);
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

                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[3].Nodes[4].Checked; }));
                    }
                    else
                    {
                        res = trvMain.Nodes[3].Nodes[4].Checked;
                    }

                    if (res)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Mozilla Firefox Saved Passwords");

                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxformPwd = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxformPwd = diInfoFirefoxformPwd.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxformPwd)
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }

                                    //Update progress bar
                                    worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);

                                    FileInfo[] datfiles = diNext.GetFiles("key3.db");
                                    foreach (FileInfo f in datfiles)
                                    {
                                        if (worker.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }

                                        filesToDelete.Add(f.FullName);

                                        //Update progress bar
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);
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

                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[3].Nodes[5].Checked; }));
                    }
                    else
                    {
                        res = trvMain.Nodes[3].Nodes[5].Checked;
                    }
                    if (res)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Mozilla Firefox Download Manager History");

                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxformDownload = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxformDownload = diInfoFirefoxformDownload.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxformDownload)
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }

                                    //Update progress bar
                                    worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);

                                    FileInfo[] datfiles = diNext.GetFiles("downloads.sqlite");
                                    foreach (FileInfo f in datfiles)
                                    {
                                        if (worker.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }

                                        filesToDelete.Add(f.FullName);

                                        //Update progress bar
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);
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

                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[3].Nodes[6].Checked; }));
                    }
                    else
                    {
                        res = trvMain.Nodes[3].Nodes[6].Checked;
                    }

                    if (res)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Mozilla Firefox Search History");

                        if (Directory.Exists(sMozillaHistory))
                        {
                            try
                            {
                                var diInfoFirefoxformSearch = new DirectoryInfo(sMozillaHistory);
                                FileSystemInfo[] fileSysFirefoxformSearch = diInfoFirefoxformSearch.GetDirectories();
                                foreach (DirectoryInfo diNext in fileSysFirefoxformSearch)
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }

                                    //Update progress bar
                                    worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);

                                    FileInfo[] datfiles = diNext.GetFiles("search.sqlite");
                                    foreach (FileInfo f in datfiles)
                                    {
                                        if (worker.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }

                                        filesToDelete.Add(f.FullName);

                                        //Update progress bar
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), f.FullName);
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

                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[4].Nodes[0].Checked; }));
                    }
                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res1 = trvMain.Nodes[4].Nodes[4].Checked; }));
                    }
                    else
                    {
                        res = trvMain.Nodes[4].Nodes[0].Checked;
                        res1 = trvMain.Nodes[4].Nodes[4].Checked;
                    }
                    if (res || res1)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Google Chrome History");
                        if (Directory.Exists(sGoogleChromePath))
                        {
                            try
                            {
                                DirectoryInfo diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
                                DirectoryInfo[] profileDirs = diInfoChromeHistory.GetDirectories();
                                foreach (DirectoryInfo dir in profileDirs)
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
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

                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[4].Nodes[1].Checked; }));
                    }
                    else
                    {
                        res = trvMain.Nodes[4].Nodes[1].Checked;
                    }
                    if (res)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Google Chrome Cookies");
                        if (Directory.Exists(sGoogleChromePath))
                        {
                            try
                            {
                                var diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
                                DirectoryInfo[] profileDirs = diInfoChromeHistory.GetDirectories();
                                foreach (DirectoryInfo dir in profileDirs)
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }

                                    FileSystemInfo[] fileSysChromeHistory = dir.GetFiles("*Cookies*");
                                    foreach (FileInfo diNext in fileSysChromeHistory)
                                    {
                                        if (worker.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }

                                        filesToDelete.Add(diNext.FullName);

                                        //Update progress bar
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);
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

                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[4].Nodes[2].Checked; }));
                    }
                    else
                    {
                        res = trvMain.Nodes[4].Nodes[2].Checked;
                    }

                    if (res)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Google Chrome Internet Cache");
                        try
                        {
                            var diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
                            DirectoryInfo[] profileDirs = diInfoChromeHistory.GetDirectories();

                            foreach (DirectoryInfo dir in profileDirs)
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                DirectoryInfo cache = dir.GetDirectories("Cache").FirstOrDefault();
                                if (cache != null)
                                {
                                    FileInfo[] files = cache.GetFiles();
                                    foreach (FileInfo diNext in files)
                                    {
                                        if (worker.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }
                                        filesToDelete.Add(diNext.FullName);
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);
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

                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[4].Nodes[3].Checked; }));
                    }
                    else
                    {
                        res = trvMain.Nodes[4].Nodes[3].Checked;
                    }

                    if (res)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Google Chrome Saved Form Information");

                        if (Directory.Exists(sGoogleChromePath))
                        {
                            try
                            {
                                var diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
                                DirectoryInfo[] profileDirs = diInfoChromeHistory.GetDirectories();
                                foreach (DirectoryInfo dir in profileDirs)
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }

                                    FileSystemInfo[] fileSysChromeHistory = dir.GetFiles("*Web Data*");
                                    foreach (FileInfo diNext in fileSysChromeHistory)
                                    {
                                        if (worker.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }

                                        filesToDelete.Add(diNext.FullName);

                                        //Update progress bar
                                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);
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

                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res = trvMain.Nodes[4].Nodes[0].Checked; }));
                    }
                    if (trvMain.InvokeRequired)
                    {
                        trvMain.Invoke(new MethodInvoker(delegate { res1 = trvMain.Nodes[4].Nodes[4].Checked; }));
                    }
                    else
                    {
                        res = trvMain.Nodes[4].Nodes[0].Checked;
                        res = trvMain.Nodes[4].Nodes[4].Checked;
                    }
                    if (res || res1)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        prgTracker++;
                        worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), "Google Chrome Download History");
                        if (Directory.Exists(sGoogleChromePath))
                        {
                            try
                            {
                                var diInfoChromeHistory = new DirectoryInfo(sGoogleChromePath);
                                DirectoryInfo[] profileDirs = diInfoChromeHistory.GetDirectories();
                                FileSystemInfo[] fileSysChromeHistory;
                                foreach (DirectoryInfo dir in profileDirs)
                                {
                                    if (worker.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }

                                    try
                                    {
                                        fileSysChromeHistory = dir.GetFiles("*history*");
                                        foreach (FileInfo diNext in fileSysChromeHistory)
                                        {
                                            if (worker.CancellationPending)
                                            {
                                                e.Cancel = true;
                                                return;
                                            }

                                            //Update progress bar
                                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);

                                            if (!filesToDelete.Contains(diNext.FullName))
                                            {
                                                filesToDelete.Add(diNext.FullName);

                                                //Update progress bar
                                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        fileSysChromeHistory = dir.GetFiles("*Visited Links*");
                                        foreach (FileInfo diNext in fileSysChromeHistory)
                                        {
                                            if (worker.CancellationPending)
                                            {
                                                e.Cancel = true;
                                                return;
                                            }
                                            //Update progress bar
                                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);

                                            if (!filesToDelete.Contains(diNext.FullName))
                                            {
                                                filesToDelete.Add(diNext.FullName);

                                                //Update progress bar
                                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        fileSysChromeHistory = dir.GetFiles("*Current Tabs*");
                                        foreach (FileInfo diNext in fileSysChromeHistory)
                                        {
                                            if (worker.CancellationPending)
                                            {
                                                e.Cancel = true;
                                                return;
                                            }
                                            //Update progress bar
                                            worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);

                                            if (!filesToDelete.Contains(diNext.FullName))
                                            {
                                                filesToDelete.Add(diNext.FullName);

                                                //Update progress bar
                                                worker.ReportProgress((int)((float)prgTracker / maxCnt * 100), diNext.FullName);
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

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
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
                if (worker.CancellationPending)
                    return;
                filesToDelete.Add(fi.FullName);
            }
            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                if (worker.CancellationPending)
                    return;
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
                    if (worker.CancellationPending)
                        return;
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
                    if (worker.CancellationPending)
                        return;
                    filesToDelete.Add(file.FullName);
                }
            }
        }



        /// <summary>
        /// handle run worker completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            filesToDelete = GetFilesAvailableForDrop(filesToDelete);
            FilesToDeletedCount = filesToDelete.Count + (int)recycleBinCount;

            ItemsToDeleteAll = FilesToDeletedCount + regValuesToDelete.Count;
            ItemsToDeleteAvailable = ItemsToDeleteAll;

            if (e.Cancelled || isCancel == true)
            {
                CancelComplete();
            }
            else if (e.Error != null)
            {
                //MessageBox.Show(error + e.Error.Message);
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
            else
            {
                Complete(fixAfterScan);
                bgWorker = new BackgroundWorker
                            {
                                WorkerSupportsCancellation = true
                            };
                bgWorker.DoWork += bgWorker_DoWork;
                bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
                bgWorker.RunWorkerAsync();
            }
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
                if (resultsTxt.InvokeRequired)
                {
                    resultsTxt.Invoke(new MethodInvoker(delegate { resultsTxt.UseWaitCursor = true; }));
                }
                else
                {
                    resultsTxt.UseWaitCursor = true;
                }

                string sizeGained = FormatSize(CalcFilesToDelSize() + recycleBinSize);
                AppendLineToResult(analysisReport);
                AppendLineToResult("------------------------------------------------------------------------------------------");
                AppendLineToResult(regValuesToDelete.Count + " " + tobeRemovedWinReg + ".");
                AppendLineToResult(sizeGained + " (" + ((ulong)filesToDelete.Count + recycleBinCount) + " " + files + ") " +
                                   tobeRemoved + ".");
                AppendLineToResult("------------------------------------------------------------------------------------------");
                AppendLineToResult("");

                if (regValuesToDelete.Count > 0)
                {
                    AppendLineToResult(detailsRegTobeDel + ":");
                    AppendLineToResult("------------------------------------------------------------------------------------------");

                    foreach (string reg in regValuesToDelete)
                        AppendLineToResult(@"RegKey: HKEY_CURRENT_USER\" + reg);
                    AppendLineToResult("");
                }

                String filestodeleteStr = "";
                if (filesToDelete.Count > 0)
                {
                    AppendLineToResult(detailsFilesTobeDel + ":");
                    AppendLineToResult("------------------------------------------------------------------------------------------");

                    foreach (string item in filesToDelete)
                    {
                        string file = item;
                        if (file.Substring(0, 8) == @"file:///")
                        {
                            file = file.Substring(8).Replace('/', '\\');
                        }
                        filestodeleteStr += file + "\r\n";
                    }
                }
                AppendLineToResult(filestodeleteStr);

                if (resultsTxt.InvokeRequired)
                {
                    resultsTxt.Invoke(new MethodInvoker(delegate { resultsTxt.UseWaitCursor = false; }));
                }
                else
                {
                    resultsTxt.UseWaitCursor = false;
                }
            }
            catch
            {
            }
        }

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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
        /// Formats size for display
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        static string FormatSize(ulong bytes)
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
                if (resultsTxt.InvokeRequired)
                {
                    resultsTxt.Invoke(new MethodInvoker(delegate
                                                            {
                                                                resultsTxt.Text += line + "\r\n";
                                                                resultsTxt.SelectionStart = resultsTxt.Text.Length;
                                                                resultsTxt.ScrollToCaret();
                                                                resultsTxt.Refresh();
                                                            }));
                }
                else
                {
                    resultsTxt.Text += line + "\r\n";
                    resultsTxt.SelectionStart = resultsTxt.Text.Length;
                    resultsTxt.ScrollToCaret();
                    resultsTxt.Refresh();
                }
            }
            catch (Exception ex)
            {
                Debug.Write("*********************************");
                Debug.Write(ex);
            }
        }

        /// <summary>
        /// Starts deleting track files
        /// </summary>
        public void StartFix()
        {
            try
            {
                Callback(10, WPFLocalizeExtensionHelpers.GetUIString("GettingTracks"));
                int tracksCount = 0;
                foreach (TreeNode n1 in trvMain.Nodes)
                {
                    foreach (TreeNode n2 in n1.Nodes)
                    {
                        if (ABORT)
                        {
                            CancelComplete();
                            return;
                        }

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
                                        MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("close_chrome_to_erase"),
                                                        WPFLocalizeExtensionHelpers.GetUIString("tracks_eraser"), MessageBoxButtons.OK,
                                                        MessageBoxIcon.Warning);
                                        foreach (Process pid in p)
                                        {
                                            try
                                            {
                                                pid.Kill();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        Thread.Sleep(200);
                                    }
                                }
                                if (n2.Text.ToLower().Contains("firefox"))
                                {
                                    Process[] p = Process.GetProcessesByName("firefox");
                                    if (p.Length > 0)
                                    {
                                        MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("close_firefox_to_erase"),
                                                        WPFLocalizeExtensionHelpers.GetUIString("tracks_eraser"), MessageBoxButtons.OK,
                                                        MessageBoxIcon.Warning);
                                        foreach (Process pid in p)
                                        {
                                            try
                                            {
                                                pid.Kill();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        Thread.Sleep(200);
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
                    MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("error_no_tracks_checked") + "\r\n" +
                                    WPFLocalizeExtensionHelpers.GetUIString("error_check_tracks") + ".",
                                    WPFLocalizeExtensionHelpers.GetUIString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    Complete(fixAfterScan);
                    return;
                }

                Callback(97, WPFLocalizeExtensionHelpers.GetUIString("DeletingFolders"));

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


                Callback(20, WPFLocalizeExtensionHelpers.GetUIString("DeletingFiles"));

                foreach (string filename in filesToDelete)
                {
                    if (ABORT)
                    {
                        CancelComplete();
                        return;
                    }

                    try
                    {
                        if (File.Exists(filename))
                        {
                            var file = new FileInfo(filename);
                            if (file.IsReadOnly || IOUtils.IsFileLocked(file))
                            {
                                // If file can not be deleted - decrease the ItemsToDeleteAvailable
                                ItemsToDeleteAvailable = ItemsToDeleteAvailable - 1;
                                continue;
                            }
                            try
                            {
                                File.Delete(filename);
                                Application.DoEvents();
                            }
                            catch
                            {
                                // If file can not be deleted - decrease the ItemsToDeleteAvailable
                                ItemsToDeleteAvailable = ItemsToDeleteAvailable - 1;
                                continue;
                            }

                            if (File.Exists(filename))
                            {
                                // If file still exists - decrease the ItemsToDeleteAvailable
                                ItemsToDeleteAvailable = ItemsToDeleteAvailable - 1;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // ToDo: send exception details via SmartAssembly bug reporting!
                    }
                }

                Callback(50, WPFLocalizeExtensionHelpers.GetUIString("CleaningRecycleBin"));
                try
                {
                    bool clearRecycleBin = trvMain.Nodes[0].Nodes[5].Checked;
                    if (clearRecycleBin)
                    {
                        SHEmptyRecycleBin(IntPtr.Zero, "", SHERB_NOCONFIRMATION | SHERB_NOPROGRESSUI | SHERB_NOSOUND);
                    }
                }
                catch
                {
                }

                Callback(80, WPFLocalizeExtensionHelpers.GetUIString("CleaningRegistry"));
                foreach (string regKey in regValuesToDelete)
                {
                    if (ABORT)
                    {
                        CancelComplete();
                        return;
                    }

                    try
                    {
                        string subKey = regKey.Remove(regKey.LastIndexOf("\\"));
                        string value = regKey.Substring(regKey.LastIndexOf("\\") + 1);

                        RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(subKey);
                        if (registryKey != null)
                        {
                            registryKey.DeleteValue(value);
                        }
                        else
                        {
                            // If registry key can not be deleted - decrease the ItemsToDeleteAvailable
                            ItemsToDeleteAvailable = ItemsToDeleteAvailable - 1;
                        }
                    }
                    catch
                    {
                        // If registry key can not be deleted - decrease the ItemsToDeleteAvailable
                        ItemsToDeleteAvailable = ItemsToDeleteAvailable - 1;
                    }
                }

                Callback(90, WPFLocalizeExtensionHelpers.GetUIString("CleaningClipboard"));
                if (trvMain.Nodes[0].Nodes[3].Checked)
                {
                    try
                    {
                        Clipboard.Clear();
                    }
                    catch
                    {
                    }
                }

                Callback(95, WPFLocalizeExtensionHelpers.GetUIString("CleaningUrlHistory"));
                if (trvMain.Nodes[1].Nodes[0].Checked)
                {
                    try
                    {
                        UrlHistory.ClearHistory();
                    }
                    catch
                    {
                    }
                }

                if (trvMain.Nodes[3].Nodes[0].Checked)
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
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }

            resultsTxt.Text = WPFLocalizeExtensionHelpers.GetUIString("after_scan_msg");
            Complete(fixAfterScan);
        }

        /// <summary>
        /// Cancel deleting track files
        /// </summary>
        public void CancelFix()
        {
            ABORT = true;
        }

        void trvMain_AfterCheck(object sender, TreeViewEventArgs e)
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
                        MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("google_chrome_running"),
                                        WPFLocalizeExtensionHelpers.GetUIString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        messageFired = true;
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
                            MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("mozilla_firefox_running"),
                                            WPFLocalizeExtensionHelpers.GetUIString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            messageFired = true;
                        }
                    }
                    catch
                    {
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
                            MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("ie_running"),
                                WPFLocalizeExtensionHelpers.GetUIString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("item_running_or_installed"),
                                WPFLocalizeExtensionHelpers.GetUIString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// update installed apps in form
        /// </summary>
        public void GetInstalledApps()
        {
            if (IsApplictionInstalled("firefox") == false)
            {
                trvMain.Nodes[3].ForeColor = Color.DarkGray;
                for (int i = 0; i < trvMain.Nodes[3].Nodes.Count; i++)
                {
                    trvMain.Nodes[3].Nodes[i].ForeColor = Color.DarkGray;
                    trvMain.Nodes[3].Nodes[i].ImageIndex = 7;
                }
            }

            if (IsApplictionInstalled("google chrome") == false && IsBrowserInstalled("chrome") == false)
            {
                trvMain.Nodes[4].ForeColor = Color.DarkGray;
                for (int i = 0; i < trvMain.Nodes[4].Nodes.Count; i++)
                {
                    trvMain.Nodes[4].Nodes[i].ForeColor = Color.DarkGray;
                    trvMain.Nodes[4].Nodes[i].ImageIndex = 7;
                }
            }

            if (IsApplictionInstalled("quicktime") == false)
            {
                trvMain.Nodes[2].Nodes[1].ForeColor = Color.DarkGray;
                trvMain.Nodes[2].Nodes[1].ImageIndex = 7;
            }

            if (IsApplictionInstalled("adobe flash player") == false)
            {
                trvMain.Nodes[2].Nodes[2].ForeColor = Color.DarkGray;
                trvMain.Nodes[2].Nodes[2].ImageIndex = 7;
            }

            if (IsMSOfficeInstalled() == false)
            {
                trvMain.Nodes[2].Nodes[3].ForeColor = Color.DarkGray;
                trvMain.Nodes[2].Nodes[3].ImageIndex = 7;
            }

            if (IsApplictionInstalled("winrar") == false)
            {
                trvMain.Nodes[2].Nodes[7].ForeColor = Color.DarkGray;
                trvMain.Nodes[2].Nodes[7].ImageIndex = 7;
            }

            if (IsApplictionInstalled("java ") == false)
            {
                trvMain.Nodes[2].Nodes[8].ForeColor = Color.DarkGray;
                trvMain.Nodes[2].Nodes[8].ImageIndex = 7;
            }
        }

        /// <summary>
        /// check if specific app is installed
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pSubname"></param>
        /// <returns></returns>
        bool IsApplictionInstalled(string pName, string pSubname = null)
        {
            try
            {
                // search in: CurrentUser
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                string displayName;
                if (key != null)
                    foreach (String keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        displayName = subkey != null && subkey.GetValue("DisplayName") != null
                                        ? subkey.GetValue("DisplayName").ToString()
                                        : "";
                        if (displayName.ToLower().Contains(pName))
                        {
                            if (pSubname == null || (displayName.ToLower().Contains(pSubname)))
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
                        if (displayName.ToLower().Contains(pName))
                        {
                            if (pSubname == null || (displayName.ToLower().Contains(pSubname)))
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
                        if (displayName.ToLower().Contains(pName))
                        {
                            if (pSubname == null || (displayName.ToLower().Contains(pSubname)))
                                return true;
                        }
                    }
                }
                var roamingData = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                foreach (DirectoryInfo d in roamingData.GetDirectories())
                {
                    if (d.Name.ToLower() == pName)
                    {
                        return true;
                    }
                }
                var localData = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
                foreach (DirectoryInfo d in localData.GetDirectories())
                {
                    if (d.Name.ToLower() == pName)
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

            const bool res = true;
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
        /// handle Click event to hide form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void button_OK_Click(object sender, EventArgs e)
        {
            //this.Close();
            Hide();
        }

        void trvMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        #region Checking file access

        const int _numberOfTries = 3;
        const int _timeIntervalBetweenTries = 100;

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
                    if (++tries > _numberOfTries)
                        throw new Exception("The file is locked too long: " + e.Message, e);
                    Thread.Sleep(_timeIntervalBetweenTries);
                }
            }
        }

        /// <summary>
        /// check if specific file is locked
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        static bool IsFileLocked(IOException exception)
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
        /// cbSize
        /// </summary>
        public Int32 cbSize;

        /// <summary>
        /// i64Size
        /// </summary>
        public UInt64 i64Size;

        /// <summary>
        /// i64NumItems
        /// </summary>
        public UInt64 i64NumItems;
    }
}