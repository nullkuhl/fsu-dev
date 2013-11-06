using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using Microsoft.VisualBasic.Devices;
using Timer = System.Threading.Timer;

namespace ProcessManager
{
    /// <summary>
    /// Main form of the Process Manager knot
    /// </summary>
    public partial class FormProcessManager : Form
    {
        public enum ListCol { Name, ID, Priority, NoOfThreads, CPUTime, Memory, StartTime, FilePath, User };

        const int SW_SHOW = 5;
        const uint SEE_MASK_INVOKEIDLIST = 12;
        /// <summary>
        /// Icon
        /// </summary>
        public const uint SHGFI_ICON = 0x100;
        /// <summary>
        /// Large icon
        /// </summary>
        public const uint SHGFI_LARGEICON = 0x0;
        /// <summary>
        /// Small icon
        /// </summary>
        public const uint SHGFI_SMALLICON = 0x1;
        /// <summary>
        /// Is button enabled
        /// </summary>
        public bool ButtonEnable = true;
        string CPUUsage;
        /// <summary>
        /// File path
        /// </summary>
        public string FilePath = "";
        string memUsage;
        string numberOfProccesses;
        /// <summary>
        /// Selected process ID
        /// </summary>
        public int SelectedProcessID = -1;
        CultureInfo culture;
        ContextMenuStrip lvcxtmnu;
        ToolStripMenuItem mitSetPriority;
        ToolStripMenuItem mitHigh;
        ToolStripMenuItem mitNormal;
        ToolStripMenuItem mitLow;
        ToolStripMenuItem mitRealTime;
        ToolStripMenuItem mitAboveNormal;
        ToolStripMenuItem mitBelowNormal;
        ToolStripMenuItem mitEnd;
        StatusBarPanel processcount;
        StatusBar stbMain;

        /// <summary>
        /// Timer
        /// </summary>
        public Timer t;
        /// <summary>
        /// Timer
        /// </summary>
        public Timer t1;
        /// <summary>
        /// Timer
        /// </summary>
        public Timer t2;
        NotifyIcon taskmgrnotify;
        StatusBarPanel threadcount;

        #region User-defined variables

        /// <summary>
        /// Params
        /// </summary>
        public static string Newprocpathandparm, Mcname;
        /// <summary>
        /// <see cref="FormNewPCDetails"/> instance
        /// </summary>
        public static FormNewPCDetails ObjNewProcess;
        /// <summary>
        /// Is blocked process added
        /// </summary>
        public static bool IsBlockProcessAdded = true;
        /// <summary>
        /// Is details form alive
        /// </summary>
        public static bool IsFormShowDetailsAlive;
        bool loading;
        /// <summary>
        /// Is error occured
        /// </summary>
        public bool IsErrorOccured;
        FormShowDetails frmShowDetails = new FormShowDetails();
        /// <summary>
        /// Present process details
        /// </summary>
        public Hashtable PresentProcessDetails;
        /// <summary>
        /// Processes collection
        /// </summary>
        public Process[] Processes;
        string[] words;

        #endregion

        #region User-Defined Methods

        /// <summary>
        /// Load all processes at startup
        /// </summary>
        public void LoadAllProcessesOnStartup()
        {
            if (loading)
                return;
            loading = true;

            Process[] processes = Process.GetProcesses(Mcname);
            Application.DoEvents();
            foreach (Process t3 in processes)
            {
                try
                {
                    //The list view item
                    string[] items;
                    string fileName = string.Empty;
                    try
                    {
                        fileName = t3.MainModule.FileName.TrimStart("\\??\\".ToCharArray());
                    }
                    catch
                    {
                        fileName = "";
                    }

                    items = new[]
						        	{
						        		t3.ProcessName,
						        		t3.Id.ToString(),
						        		rm.GetString(t3.PriorityClass.ToString()),
						        		t3.Threads.Count.ToString(),
						        		t3.TotalProcessorTime.Duration().Hours.ToString() + ":" +
						        		t3.TotalProcessorTime.Duration().Minutes.ToString() + ":" +
						        		t3.TotalProcessorTime.Duration().Seconds.ToString(),
						        		(t3.WorkingSet64/1024).ToString() + "k",
						        		t3.StartTime.ToShortDateString() + " " + t3.StartTime.ToShortTimeString(),
						        		fileName,
                                        ProcessExtension.GetProcessOwner(t3.Id)
						        	};

                    ListViewItem lvi = new ListViewItem(items);
                    try
                    {
                        Icon icon = GetSmallIcon(t3.MainModule.FileName);
                        procImageList.Images.Add(t3.Id.ToString(), icon);
                        lvi.ImageIndex = procImageList.Images.Count - 1;
                    }
                    catch
                    {
                    }

                    lvi.Tag = "_";

                    lvprocesslist.Items.Add(lvi);
                }
                catch
                {
                }
            }

            loading = false;
        }

        void LoadProcesses(Object temp)
        {
            if (lvprocesslist.InvokeRequired)
            {
                lvprocesslist.BeginInvoke(new MethodInvoker(() => LoadAllProcesses(temp)));
            }
            else
            {
                LoadAllProcesses(temp);
            }
        }

        /// <summary>
        /// Load all processes
        /// </summary>
        /// <param name="temp"></param>
        public void LoadAllProcesses(Object temp)
        {
            if (loading)
                return;
            loading = true;

            try
            {
                //List of all running processes
                //Get the processes
                Process[] processes = Process.GetProcesses(Mcname);
                Application.DoEvents();

                foreach (Process t3 in processes)
                {
                    //The list view item
                    ListViewItem lvi = null;

                    //See if this process is already in the list
                    Application.DoEvents();
                    foreach (ListViewItem l in lvprocesslist.Items)
                    {
                        //Match the process id
                        if (int.Parse(l.SubItems[(int)ListCol.ID].Text) == t3.Id)
                        {
                            lvi = l;
                            break;
                        }
                    }

                    //If this is a new process, add to the list
                    if (lvi == null)
                    {
                        lvi = new ListViewItem(t3.ProcessName);
                        try
                        {
                            Icon icon = GetSmallIcon(t3.MainModule.FileName);
                            procImageList.Images.Add(t3.Id.ToString(), icon);
                            lvi.ImageIndex = procImageList.Images.IndexOfKey(t3.Id.ToString());
                        }
                        catch
                        {
                        }
                        for (int j = 0; j < 7; j++)
                            lvi.SubItems.Add(string.Empty);

                        lvi.SubItems[(int)ListCol.ID].Text = t3.Id.ToString();

                        lvprocesslist.Items.Add(lvi);
                    }
                    else
                    {
                        try
                        {
                            int idx = procImageList.Images.IndexOfKey(t3.Id.ToString());

                            if (lvi.ImageIndex != idx || idx == -1)
                            {
                                Icon icon = GetSmallIcon(t3.MainModule.FileName);
                                procImageList.Images.Add(t3.Id.ToString(), icon);
                                lvi.ImageIndex = procImageList.Images.IndexOfKey(t3.Id.ToString());
                            }
                            else
                            {
                                lvi.ImageIndex = procImageList.Images.IndexOfKey(t3.Id.ToString());
                            }
                        }
                        catch { }
                    }


                    //The _ tag tells me this is a running process, used later
                    //to weed out processes that have exited.
                    lvi.Tag = "_";

                    //Only update the listview if needed 
                    //this function is kind of memory intensive!

                    try
                    {
                        if (lvi.SubItems[(int)ListCol.Priority].Text != rm.GetString(t3.PriorityClass.ToString()))
                            lvi.SubItems[(int)ListCol.Priority].Text = rm.GetString(t3.PriorityClass.ToString());
                    }
                    catch
                    {
                        lvi.SubItems[(int)ListCol.Priority].Text = string.Empty;
                    }

                    if (lvi.SubItems[(int)ListCol.NoOfThreads].Text != t3.Threads.Count.ToString())
                        lvi.SubItems[(int)ListCol.NoOfThreads].Text = t3.Threads.Count.ToString();
                    try
                    {
                        if (lvi.SubItems[(int)ListCol.CPUTime].Text !=
                            t3.TotalProcessorTime.Duration().Hours.ToString() + ":" +
                            t3.TotalProcessorTime.Duration().Minutes.ToString() + ":" +
                            t3.TotalProcessorTime.Duration().Seconds.ToString())
                            lvi.SubItems[(int)ListCol.CPUTime].Text = t3.TotalProcessorTime.Duration().Hours.ToString() + ":" +
                                                   t3.TotalProcessorTime.Duration().Minutes.ToString() + ":" +
                                                   t3.TotalProcessorTime.Duration().Seconds.ToString();
                    }
                    catch
                    {
                    }
                    if (lvi.SubItems[(int)ListCol.Memory].Text != (t3.WorkingSet / 1024).ToString() + "k")
                        lvi.SubItems[(int)ListCol.Memory].Text = (t3.WorkingSet / 1024).ToString() + "k";
                    try
                    {
                        if (lvi.SubItems[(int)ListCol.StartTime].Text !=
                            t3.StartTime.ToShortDateString() + " " + t3.StartTime.ToShortTimeString())
                            lvi.SubItems[(int)ListCol.StartTime].Text = t3.StartTime.ToShortDateString() + " " +
                                                   t3.StartTime.ToShortTimeString();
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (lvi.SubItems[(int)ListCol.FilePath].Text != t3.MainModule.FileName)
                            lvi.SubItems[(int)ListCol.FilePath].Text = t3.MainModule.FileName.TrimStart("\\??\\".ToCharArray());
                    }
                    catch
                    {
                    }
                    try
                    {
                        string processOwner = ProcessExtension.GetProcessOwner(t3.Id);
                        if (lvi.SubItems[(int)ListCol.User].Text != processOwner)
                            lvi.SubItems[(int)ListCol.User].Text = processOwner;
                    }
                    catch
                    {
                    }
                }
                //Now check for items we need to remove from the list
                var remove = new List<ListViewItem>();
                Application.DoEvents();
                for (int i = 0; i < lvprocesslist.Items.Count; i++)
                {
                    //If the tag = _, the process is still running.
                    //Otherwise, add to the remove list
                    if ((string)lvprocesslist.Items[i].Tag == "_")
                        lvprocesslist.Items[i].Tag = string.Empty;
                    else
                        remove.Add(lvprocesslist.Items[i]);
                }
                Application.DoEvents();
                foreach (ListViewItem lvi in remove)
                {
                    //Remove all dead processes from the listview
                    lvprocesslist.Items.Remove(lvi);
                    try
                    {
                        if (lvi.ImageIndex != -1)
                            procImageList.Images.RemoveAt(procImageList.Images.IndexOfKey(lvi.SubItems[(int)ListCol.ID].Text));
                    }
                    catch { }
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }

            //And allow the timer function to reenter.
            loading = false;
        }


        void UpdateStatusBar(Object temp)
        {
            //Setup the status bar info

            Thread.CurrentThread.CurrentUICulture = culture;

            try
            {
                var processor = new ManagementObject("Win32_PerfFormattedData_PerfOS_Processor.Name='_Total'");
                processor.Get();
                numberOfProccesses = string.Format(rm.GetString("processes"), lvprocesslist.Items.Count);
                CPUUsage = string.Format(rm.GetString("cpu"), processor.Properties["PercentProcessorTime"].Value);
                var statusBarCounter = new PerformanceCounter("Memory", "Available MBytes");
                memUsage = string.Format(rm.GetString("memory"),
                                         ((((new ComputerInfo().TotalPhysicalMemory) / 1024) / 1024) -
                                          (ulong)statusBarCounter.NextValue()));
                if (IsBlockProcessAdded)
                {
                    try
                    {
                        words = BlockedProcessesManager.GetBlockedProcesses();
                        IsBlockProcessAdded = false;
                    }
                    catch
                    {
                    }
                }

                Application.DoEvents();
                foreach (string s in words)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(s) && s != string.Empty)
                        {
                            Process[] processToKill = Process.GetProcessesByName(s);

                            foreach (Process p in processToKill)
                            {
                                p.Kill();
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        void SetProcessPriority(ToolStripMenuItem item)
        {
            try
            {
                int selectedpid = Convert.ToInt32(lvprocesslist.SelectedItems[0].SubItems[(int)ListCol.ID].Text);
                Process selectedprocess = Process.GetProcessById(selectedpid, Mcname);
                if (item.Text.ToUpper() == "HIGH")
                    selectedprocess.PriorityClass = ProcessPriorityClass.High;
                else if (item.Text.ToUpper() == "LOW")
                    selectedprocess.PriorityClass = ProcessPriorityClass.Idle;
                else if (item.Text.ToUpper() == "REALTIME")
                    selectedprocess.PriorityClass = ProcessPriorityClass.RealTime;
                else if (item.Text.ToUpper() == "ABOVE NORMAL")
                    selectedprocess.PriorityClass = ProcessPriorityClass.AboveNormal;
                else if (item.Text.ToUpper() == "BELOW NORMAL")
                    selectedprocess.PriorityClass = ProcessPriorityClass.BelowNormal;
                else if (item.Text.ToUpper() == "NORMAL")
                    selectedprocess.PriorityClass = ProcessPriorityClass.Normal;
                foreach (ToolStripMenuItem mnuitem in mitSetPriority.DropDownItems)
                {
                    mnuitem.Checked = false;
                }
                item.Checked = true;
            }
            catch (Exception ex)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        #endregion

        /// <summary>
        /// <see cref="FormProcessManager"/> constructor
        /// </summary>
        public FormProcessManager()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            toolStripBtnBlockProcess.Enabled = false;
            toolStripBtnEndProcess.Enabled = false;
            toolStripBtnGoogleIt.Enabled = false;
            toolStripBtnProperties.Enabled = false;
            toolStripBtnShowDetails.Enabled = false;
            toolStripBtnUBlck.Enabled = true;
        }

        //Variables For Process Details


        void FrmProcessManager_Load(object sender, EventArgs e)
        {
            if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\FreeGamingBooster.exe"))
            {
                this.Icon = Properties.Resources.GBicon;
            }
            else
            {
                this.Icon = Properties.Resources.FSUIcon;
            }

            string lang = CfgFile.Get("Lang");
            SetCulture(new CultureInfo(lang));

            Mcname = ".";
            PresentProcessDetails = new Hashtable();
            lvprocesslist.ListViewItemSorter = new Sorter();
            LoadAllProcessesOnStartup();

            TimerCallback timerDelegate = LoadProcesses;
            t = new Timer(timerDelegate, null, 1000, 1000);

            TimerCallback timerDelegate1 = UpdateStatusBar;
            t1 = new Timer(timerDelegate1, null, 10, 10);

            //  System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(Update));
            //  timer.Change(0, 1000);

            numberOfProccesses = rm.GetString("gathering_data");
        }

        void UpdateProcessDetails()
        {
            frmShowDetails = new FormShowDetails();
            try
            {
                if (SelectedProcessID >= 0)
                {
                    int selectedpid = SelectedProcessID;
                    Process p = Process.GetProcessById(selectedpid);
                    var dictGeneralInfo = new Dictionary<string, string>();

                    try
                    {
                        if (String.IsNullOrEmpty(FilePath))
                        {
                            ButtonEnable = false;

                            return;
                        }
                        else
                        {
                            ButtonEnable = true;
                        }

                        dictGeneralInfo.Add(rm.GetString("process_information"), "");
                        dictGeneralInfo.Add("ID", p.Id.ToString());
                        dictGeneralInfo.Add(rm.GetString("file"), p.MainModule.ModuleName);
                        dictGeneralInfo.Add(rm.GetString("arguments"), p.StartInfo.Arguments);
                        dictGeneralInfo.Add(rm.GetString("folder"), p.MainModule.FileName.TrimEnd(p.MainModule.ModuleName.ToCharArray()));
                        dictGeneralInfo.Add(rm.GetString("threads"), p.Threads.Count.ToString());
                        dictGeneralInfo.Add(rm.GetString("priority"), rm.GetString(p.PriorityClass.ToString()));
                        dictGeneralInfo.Add(rm.GetString("memory_usage"), p.WorkingSet64 / 1024 + "K");
                        dictGeneralInfo.Add(rm.GetString("started_on"), p.StartTime.ToString());
                        try
                        {
                            Process parent = p.Parent();
                            dictGeneralInfo.Add(rm.GetString("started_by"),
                                                (parent != null) ? parent.ProcessName + " (" + parent.MainModule.ModuleName + ")" : "");
                        }
                        catch
                        {
                            dictGeneralInfo.Add(rm.GetString("started_by"), "");
                        }
                        dictGeneralInfo.Add(" ", " ");

                        dictGeneralInfo.Add(rm.GetString("version_information"), "");
                        dictGeneralInfo.Add(rm.GetString("file_type"), rm.GetString("application"));
                        dictGeneralInfo.Add(rm.GetString("company"), p.MainModule.FileVersionInfo.CompanyName);
                        dictGeneralInfo.Add(rm.GetString("description"), p.MainModule.FileVersionInfo.FileDescription);
                        dictGeneralInfo.Add(rm.GetString("version"), p.MainModule.FileVersionInfo.FileVersion);
                        dictGeneralInfo.Add(rm.GetString("internal_name"), p.MainModule.FileVersionInfo.InternalName);
                        dictGeneralInfo.Add(rm.GetString("copyright"), p.MainModule.FileVersionInfo.LegalCopyright);
                        dictGeneralInfo.Add(rm.GetString("trademark"), p.MainModule.FileVersionInfo.LegalTrademarks);
                        dictGeneralInfo.Add(rm.GetString("original_file_name"), p.MainModule.FileVersionInfo.OriginalFilename);
                        dictGeneralInfo.Add(rm.GetString("product_name"), p.MainModule.FileVersionInfo.ProductName);
                        dictGeneralInfo.Add(rm.GetString("product_version"), p.MainModule.FileVersionInfo.ProductVersion);
                        dictGeneralInfo.Add(rm.GetString("comments"), p.MainModule.FileVersionInfo.Comments);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        frmShowDetails.UpdateGeneralInformation(dictGeneralInfo);

                        frmShowDetails.UpdateModulesUsedInformation(p);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        void tmrMain_Tick(object sender, EventArgs e)
        {
            stbMain.Panels[0].Text = numberOfProccesses;
            stbMain.Panels[1].Text = CPUUsage;
            stbMain.Panels[2].Text = memUsage;
        }

        /// <summary>
        /// Open process
        /// </summary>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="bInheritHandle"></param>
        /// <param name="dwProcessId"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        /// <summary>
        /// Query full process image name
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="dwFlags"></param>
        /// <param name="lpExeName"></param>
        /// <param name="lpdwSize"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", ExactSpelling = true, EntryPoint = "QueryFullProcessImageNameW", CharSet = CharSet.Unicode)]
        internal static extern bool QueryFullProcessImageName(IntPtr hProcess, uint dwFlags, StringBuilder lpExeName,
                                                              out uint lpdwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// Show file properties
        /// </summary>
        /// <param name="filename"></param>
        public static void ShowFileProperties(string filename)
        {
            var info = new SHELLEXECUTEINFO();
            info.cbSize = Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = filename;
            info.nShow = SW_SHOW;
            info.fMask = SEE_MASK_INVOKEIDLIST;
            ShellExecuteEx(ref info);
        }

        /// <summary>
        /// Gets small icon for the specified <paramref name="fileName"/>
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Small icon for the specified <paramref name="fileName"/></returns>
        public static Icon GetSmallIcon(string fileName)
        {
            var shinfo = new SHFILEINFO();
            SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON);
            return Icon.FromHandle(shinfo.hIcon);
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

        /// <summary>
        /// Gets file info
        /// </summary>
        /// <param name="pszPath"></param>
        /// <param name="dwFileAttributes"></param>
        /// <param name="psfi"></param>
        /// <param name="cbSizeFileInfo"></param>
        /// <param name="uFlags"></param>
        /// <returns>File info</returns>
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi,
                                                  uint cbSizeFileInfo, uint uFlags);

        void lvprocesslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvprocesslist.SelectedItems.Count == 0)
                {
                    tlsMain.Enabled = false;
                }
                else
                {
                    if (tlsMain.Enabled == false)
                        tlsMain.Enabled = true;

                    SelectedProcessID = Convert.ToInt32(lvprocesslist.SelectedItems[0].SubItems[(int)ListCol.ID].Text);
                    FilePath = lvprocesslist.SelectedItems[0].SubItems[(int)ListCol.FilePath].Text;
                    toolStripBtnProperties.Enabled = ButtonEnable;
                    toolStripBtnShowDetails.Enabled = ButtonEnable;
                    toolStripBtnBlockProcess.Enabled = ButtonEnable;
                    toolStripBtnEndProcess.Enabled = ButtonEnable;
                    toolStripBtnGoogleIt.Enabled = ButtonEnable;
                }
            }
            catch
            {
            }
        }


        void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;
            this.culture = culture;

            taskmgrnotify.Text = rm.GetString("taskmngr_visible", culture);
            rm.GetString("end_process", culture);
            mitSetPriority.Text = rm.GetString("set_priority", culture);
            mitHigh.Text = rm.GetString("High", culture);
            mitAboveNormal.Text = rm.GetString("AboveNormal", culture);
            mitBelowNormal.Text = rm.GetString("BelowNormal", culture);
            mitNormal.Text = rm.GetString("Normal", culture);
            mitLow.Text = rm.GetString("Low", culture);
            mitRealTime.Text = rm.GetString("RealTime", culture);
            processcount.Text = rm.GetString("gathering_data", culture);
            threadcount.Text = rm.GetString("threads", culture);
            tlsMain.Text = rm.GetString("tool_strip", culture);
            toolStripBtnEndProcess.Text = rm.GetString("end_process", culture);
            toolStripBtnBlockProcess.Text = rm.GetString("block_process", culture);
            toolStripBtnShowDetails.Text = rm.GetString("show_detail", culture);
            toolStripBtnProperties.Text = rm.GetString("properties", culture);
            toolStripBtnGoogleIt.Text = rm.GetString("search_internet", culture);
            toolStripBtnUBlck.Text = rm.GetString("unblock", culture);
            procname.Text = rm.GetString("name", culture);
            procuser.Text = rm.GetString("user", culture);
            columnHeaderPID.Text = rm.GetString("proc_id", culture);
            columnHeaderPriorty.Text = rm.GetString("priority", culture);
            nonofthreads.Text = rm.GetString("num_threads", culture);
            proccputime.Text = rm.GetString("cpu_time", culture);
            procFilePath.Text = rm.GetString("file_path", culture);
            Text = rm.GetString("window_title", culture);
            memusage.Text = rm.GetString("mem", culture);
            columnHeaderStartTime.Text = rm.GetString("start_time", culture);
            ucTop.Text = rm.GetString("window_title", culture);
        }

        #region //Click Events

        void taskmgrnotify_Click(object sender, EventArgs e)
        {
            if (Visible)
            {
                Visible = false;
                taskmgrnotify.Text = rm.GetString("taskmngr_invisible");
            }
            else
            {
                Visible = true;
                taskmgrnotify.Text = rm.GetString("taskmngr_visible");
            }
        }

        void toolStripBtnEndProcess_Click(object sender, EventArgs e)
        {
            //Here,We are going to kill selected Process by getting ID...
            if (lvprocesslist.SelectedItems.Count >= 1)
            {
                try
                {
                    int selectedpid = Convert.ToInt32(lvprocesslist.SelectedItems[0].SubItems[(int)ListCol.ID].Text);
                    string processName = lvprocesslist.SelectedItems[(int)ListCol.Name].Text;
                    if (processName == Process.GetCurrentProcess().ProcessName)
                    {
                        MessageBox.Show(rm.GetString("cant_end_processmanager"), rm.GetString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        Process tmp = Process.GetProcessById(selectedpid, Mcname);

                        if (MessageBox.Show(string.Format(rm.GetString("want_end_process"), processName), rm.GetString("error"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            if (GetProcessOwner(selectedpid).Contains("SYSTEM"))
                            {
                                if (MessageBox.Show(string.Format(rm.GetString("system_process_end"), processName), rm.GetString("error"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                {
                                    return;
                                }
                            }
                        }
                    }
                    Process.GetProcessById(selectedpid, Mcname).Kill();
                    MessageBox.Show(rm.GetString("ended_process"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    toolStripBtnBlockProcess.Enabled = false;
                    toolStripBtnEndProcess.Enabled = false;
                    toolStripBtnGoogleIt.Enabled = false;
                    toolStripBtnProperties.Enabled = false;
                    toolStripBtnShowDetails.Enabled = false;
                }
                catch
                {
                    IsErrorOccured = true;
                }
            }
        }

        private static string GetProcessOwner(int processId)
        {
            string query = "Select * From Win32_Process Where ProcessID = " + processId;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach (ManagementObject obj in processList)
            {
                string[] argList = new string[] { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    // return DOMAIN\user
                    return argList[1] + "\\" + argList[0];
                }
            }

            return "NO OWNER";
        }

        void mitEnd_Click(object sender, EventArgs e)
        {
            toolStripBtnEndProcess_Click(sender, e);
        }

        void mitSetPriority_Popup(object sender, EventArgs e)
        {
            try
            {
                int selectedpid = Convert.ToInt32(lvprocesslist.SelectedItems[0].SubItems[(int)ListCol.ID].Text);
                Process selectedprocess = Process.GetProcessById(selectedpid, Mcname);
                string priority = rm.GetString(selectedprocess.PriorityClass.ToString());
                foreach (ToolStripMenuItem mnuitem in mitSetPriority.DropDownItems)
                {
                    string mnutext = mnuitem.Text.ToUpper().Replace(" ", "");
                    if (mnutext == "LOW")
                        mnutext = "IDLE";
                    if (priority != null) mnuitem.Checked = mnutext == priority.ToUpper();
                }
            }
            catch (Exception ex)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        void mitHigh_Click(object sender, EventArgs e)
        {
            SetProcessPriority(mitHigh);
        }

        void mitRealTime_Click(object sender, EventArgs e)
        {
            SetProcessPriority(mitRealTime);
        }

        void mitLow_Click(object sender, EventArgs e)
        {
            SetProcessPriority(mitLow);
        }

        void mitNormal_Click(object sender, EventArgs e)
        {
            SetProcessPriority(mitNormal);
        }

        void mitBelowNormal_Click(object sender, EventArgs e)
        {
            SetProcessPriority(mitBelowNormal);
        }

        void mitAboveNormal_Click(object sender, EventArgs e)
        {
            SetProcessPriority(mitAboveNormal);
        }

        void lvprocesslist_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var s = (Sorter)lvprocesslist.ListViewItemSorter;
            s.Column = e.Column;

            s.Order = s.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvprocesslist.Sort();
        }

        void toolStripBtnBlockProcess_Click(object sender, EventArgs e)
        {
            //Here,We are going to kill selected Process by getting ID...
            if (lvprocesslist.SelectedItems.Count >= 1)
            {
                try
                {
                    int selectedpid = Convert.ToInt32(lvprocesslist.SelectedItems[0].SubItems[(int)ListCol.ID].Text);
                    string processName = lvprocesslist.SelectedItems[0].Text;
                    if (processName == Process.GetCurrentProcess().ProcessName)
                    {
                        MessageBox.Show(rm.GetString("cant_block_processmanager"), rm.GetString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show(string.Format(rm.GetString("want_block_process"), processName), rm.GetString("error"), MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            if (GetProcessOwner(selectedpid).Contains("SYSTEM"))
                            {
                                if (MessageBox.Show(string.Format(rm.GetString("system_process_block"), processName), rm.GetString("error"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                {
                                    return;
                                }
                            }
                        }
                    }
                    //Add process to the blocking list
                    BlockedProcessesManager.AddBlockedProcess(processName);
                    IsBlockProcessAdded = true;
                    Process.GetProcessById(selectedpid, Mcname).Kill();
                    toolStripBtnBlockProcess.Enabled = false;
                    toolStripBtnEndProcess.Enabled = false;
                    toolStripBtnGoogleIt.Enabled = false;
                    toolStripBtnProperties.Enabled = false;
                    toolStripBtnShowDetails.Enabled = false;
                }
                catch (Exception)
                {
                    IsErrorOccured = true;
                }
            }
        }

        void toolStripBtnGoogleIt_Click(object sender, EventArgs e)
        {
            //Create a new process.
            Process proc = new Process { EnableRaisingEvents = false };

            //Here you can also specify a html page on local machine
            //such as C:\Test\default.html
            if (lvprocesslist.SelectedItems.Count >= 1)
            {
                string sSearchItem = lvprocesslist.SelectedItems[0].SubItems[(int)ListCol.FilePath].Text;
                string[] processdata = sSearchItem.Split('\\');
                sSearchItem = processdata[processdata.Length - 1];
                proc.StartInfo.FileName = "http://www.google.com/search?q=" + sSearchItem + " process";
            }
            else
            {
                proc.StartInfo.FileName = "http://www.google.com";
            }

            proc.Start();
        }

        void toolStripBtnShowDetails_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            UpdateProcessDetails();
            Cursor = Cursors.Default;
            IsFormShowDetailsAlive = true;
            frmShowDetails.ShowDialog();
        }

        void toolStripBtnProperties_Click(object sender, EventArgs e)
        {
            if (lvprocesslist.SelectedItems.Count >= 1)
            {
                string processFilePath = lvprocesslist.SelectedItems[0].SubItems[(int)ListCol.FilePath].Text;
                ShowFileProperties(processFilePath);
            }
        }

        #endregion

        #region Nested type: SHELLEXECUTEINFO

        /// <summary>
        /// Shell execute info
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHELLEXECUTEINFO
        {
            /// <summary>
            /// Size
            /// </summary>
            public int cbSize;
            /// <summary>
            /// Mask
            /// </summary>
            public uint fMask;
            /// <summary>
            /// hwnd
            /// </summary>
            public IntPtr hwnd;
            /// <summary>
            /// Verb
            /// </summary>
            public string lpVerb;
            /// <summary>
            /// File
            /// </summary>
            public string lpFile;
            /// <summary>
            /// Parameters
            /// </summary>
            public string lpParameters;
            /// <summary>
            /// Directory
            /// </summary>
            public string lpDirectory;
            /// <summary>
            /// Show
            /// </summary>
            public int nShow;
            /// <summary>
            /// hInstApp
            /// </summary>
            public IntPtr hInstApp;
            /// <summary>
            /// IDList
            /// </summary>
            public IntPtr lpIDList;
            /// <summary>
            /// Class
            /// </summary>
            public string lpClass;
            /// <summary>
            /// Key class
            /// </summary>
            public IntPtr hkeyClass;
            /// <summary>
            /// HotKey
            /// </summary>
            public uint dwHotKey;
            /// <summary>
            /// IconMonitorUnion
            /// </summary>
            public IntPtr hIconMonitorUnion;
            /// <summary>
            /// Process
            /// </summary>
            public IntPtr hProcess;
        }

        #endregion

        #region Nested type: SHFILEINFO

        /// <summary>
        /// File info
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            /// <summary>
            /// Icon
            /// </summary>
            public IntPtr hIcon;
            /// <summary>
            /// Icon
            /// </summary>
            public IntPtr iIcon;
            /// <summary>
            /// Attributes
            /// </summary>
            public uint dwAttributes;

            /// <summary>
            /// Display name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;

            /// <summary>
            /// Type name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        #endregion

        /// <summary>
        /// handle Click event to unblock blocked processes
        /// </summary>
        /// <param name="sender"></param>ock
        /// <param name="e"></param>
        private void toolStripBtnUBlck_Click(object sender, EventArgs e)
        {
            FormBlockedProcesses frmBlockedProcess = new FormBlockedProcesses();
            frmBlockedProcess.ShowDialog();
        }

        private void lvprocesslist_Leave(object sender, EventArgs e)
        {
            toolStripBtnBlockProcess.Enabled = false;
            toolStripBtnEndProcess.Enabled = false;
            toolStripBtnGoogleIt.Enabled = false;
            toolStripBtnProperties.Enabled = false;
            toolStripBtnShowDetails.Enabled = false;
            toolStripBtnUBlck.Enabled = true;
        }
    }
}