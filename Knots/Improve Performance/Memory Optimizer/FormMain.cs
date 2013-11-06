using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using MemoryOptimizer.Properties;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;

namespace MemoryOptimizer
{
    /// <summary>
    /// Memory Optimizer knot main form
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// Track value
        /// </summary>
        public static int TrackValue;
        internal int CPUUsage;
        protected PerformanceCounter CPU = new PerformanceCounter();
        int finalMemUsage;
        int initialMemUsage;
        internal DateTime LastAuto = DateTime.MinValue;

        internal string Optexe;
        internal bool ProcessExited = true;

        protected PerformanceCounter RAM = new PerformanceCounter("Memory", "Available MBytes");
        internal string Runexe;
        bool showed;
        internal bool Silent;
        int totalRam;
        bool track;
        internal int Trackbar;
        internal bool W1Done = true;

        /// <summary>
        /// Main form constructor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            Runexe = Application.ExecutablePath;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GlobalMemoryStatus(MEMORYSTATUS buffer);

        internal bool IsStartup()
        {
            bool result = false;
            RegistryKey key = Registry.CurrentUser;
            try
            {
                key = key.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                if (key != null)
                {
                    object val = key.GetValue("MemoryOptimizer");
                    if (val != null && (val.ToString().ToLower() == Runexe.ToLower()))
                        result = true;
                    key.Close();
                }

            }
            catch
            {
            }

            return result;
        }

        internal bool SetStartup()
        {
            bool result = false;
            RegistryKey key = Registry.CurrentUser;
            try
            {
                key = key.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (key != null)
                {
                    key.SetValue("MemoryOptimizer", Runexe, RegistryValueKind.String);
                    key.Close();
                    result = true;
                }
            }
            catch
            {
            }

            return result;
        }

        internal bool ClrStartup()
        {
            bool result = false;
            RegistryKey key = Registry.CurrentUser;
            try
            {
                key = key.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (key != null)
                {
                    key.DeleteValue("MemoryOptimizer", false);
                    key.Close();
                }
                result = true;
            }
            catch
            {
            }

            return result;
        }

        void btnClearClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
        }

        /// <summary>
        /// Calc CPU usage
        /// </summary>
        public void CalcCPUUsage()
        {
            CPU.CategoryName = "Processor";
            CPU.CounterName = "% Processor Time";
            CPU.InstanceName = "_Total";

            try
            {
                CPUUsage = Convert.ToInt32(CPU.NextValue());
                prbCPU.Value = CPUUsage;
                prbCPU.PerformStep();
                lblStatus.Text = CPUUsage + " %";
            }
            catch
            {
            }
        }

        /// <summary>
        /// Calc free RAM
        /// </summary>
        /// <returns></returns>
        public int CalcFreeRAM()
        {
            int ramUsage = 0;
            try
            {
                ramUsage = Convert.ToInt32(RAM.NextValue());
            }
            catch
            {
            }

            return ramUsage;
        }

        void tmrMain_Tick(object sender, EventArgs e)
        {
            ulong memusage = 0;
            try
            {
                PerformanceCounter statusBarCounter = new PerformanceCounter("Memory", "Available MBytes");
                memusage = ((((new ComputerInfo().TotalPhysicalMemory) / 1024) / 1024) - (ulong)statusBarCounter.NextValue());
            }
            catch
            {
            }

            lblMemoryUsage.Text = memusage + " MB";

            if (chtMemory.Series[0].Points.Count >= 39)
            {
                chtMemory.Series[0].Points.RemoveAt(0);
            }

            chtMemory.Series[0].Points.Add(memusage);

            CalcCPUUsage();
            totalRam = Convert.ToInt32(memusage) + CalcFreeRAM();
            lblMemoryTotal.Text = totalRam.ToString() + " MB";

            chtMemory.ChartAreas[0].AxisY.Maximum = totalRam;
            chtMemory.ChartAreas[0].AxisY.Minimum = 0;
            chtMemory.ChartAreas[0].AxisX.Maximum = 39;

            if (track == false)
            {
                trackBarMemoryAmount.Maximum = totalRam - 94;
                trackBarMemoryAmount.Value = trackBarMemoryAmount.Maximum;
                track = true;
            }

            if (!ProcessExited)
            {
                if (prsMain.HasExited)
                {
                    if (File.Exists(Optexe))
                        prsMain_Exited(prsMain, e);
                }
                else
                {
                    prbMemory.PerformStep();
                    if (prbMemory.Value >= prbMemory.Maximum)
                    {
                        try
                        {
                            prsMain.Kill();
                        }
                        catch
                        {
                        }
                        prsMain_Exited(prsMain, e);
                    }
                }
            }

            if (Visible) return;
            if (!W1Done) return;
            if ((DateTime.Now.Subtract(LastAuto)).Minutes < 5) return;

            MEMORYSTATUS stats = new MEMORYSTATUS();
            GlobalMemoryStatus(stats);

            if (!(nudOptimizeIfMemory.Value != 0 && stats.availPhys / Math.Pow(1024, 2) <= (double)nudOptimizeIfMemory.Value)
             || (nudIncreaseMemory.Value == 0) || !(nudOptimizeIfCPU.Value == 0 || CPUUsage < (int)nudOptimizeIfCPU.Value))
            {
                return;
            }

            // run resource
            if (ProcessExited)
            {
                if (RunMemOpti(null))
                {
                    ProcessExited = false;
                    prbMemory.Step = 5;
                    prbMemory.Value = 0;
                    LastAuto = DateTime.Now;
                }
            }

            if (!ProcessExited) return; //??

            prbMemory.Step = 10;
            prbMemory.Value = 0;
            Trackbar = (int)nudIncreaseMemory.Value;

            if (bgwMemory != null)
            {
                bgwMemory.Dispose();
                bgwMemory = null;
            }
            if (bgwCounter != null)
            {
                bgwCounter.Dispose();
                bgwCounter = null;
            }

            bgwMemory = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = false };
            bgwMemory.ProgressChanged += bgwMemory_ProgressChanged;
            bgwMemory.DoWork += bgwMemory_DoWork;
            bgwMemory.RunWorkerCompleted += bgwMemory_RunWorkerCompleted;

            bgwCounter = new BackgroundWorker();
            bgwCounter.DoWork += bgwCounter_DoWork;

            W1Done = false;
            Silent = true;
            LastAuto = DateTime.Now;
            bgwMemory.RunWorkerAsync();
            bgwCounter.RunWorkerAsync();
        }

        /// <summary>
        /// Run Memory Optimizer
        /// </summary>
        /// <param name="args">startup arguments</param>
        /// <returns></returns>
        private bool RunMemOpti(string args)
        {
            bool result = false;
            try
            {
                Optexe = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Path.DirectorySeparatorChar +
                            "memopti.exe";
                var stream = new FileStream(Optexe, FileMode.Create, FileAccess.Write);
                stream.Write(Resources.memopti, 0, Resources.memopti.Length);
                stream.Flush();
                stream.Close();
                stream.Dispose();
                prsMain.StartInfo.FileName = Optexe;
                if (!string.IsNullOrEmpty(args))
                    prsMain.StartInfo.Arguments = args;

                if (prsMain.Start())
                    result = true;
            }
            catch
            {
            }

            return result;
        }


        void trackBarMemoryAmount_ValueChanged(object sender, EventArgs e)
        {
            TrackValue = trackBarMemoryAmount.Value;
            lblMemory.Text = rm.GetString("memory_amount") + " : " + TrackValue.ToString() + " MB";
        }

        void btnOptimize_Click(object sender, EventArgs e)
        {
            showed = false;
            btnOptimize.Enabled = false;
            initialMemUsage = CalcFreeRAM();
            CalcCPUUsage();

            if (!ProcessExited) return;

            string args = string.Empty;

            if (trackBarMemoryAmount.Value != trackBarMemoryAmount.Maximum)
                args = trackBarMemoryAmount.Value.ToString();

            if (RunMemOpti(args))
            {
                ProcessExited = false;
            }

            if (!ProcessExited)
            {
                prbMemory.Step = 5;
                prbMemory.Value = 0;
                return;
            }

            if (!W1Done) return;
            prbMemory.Value = 0;
            prbMemory.Step = 10;
            Trackbar = trackBarMemoryAmount.Value;
            if (bgwMemory != null)
            {
                bgwMemory.Dispose();
                bgwMemory = null;
            }
            if (bgwCounter != null)
            {
                bgwCounter.Dispose();
                bgwCounter = null;
            }
            bgwMemory = new BackgroundWorker();
            bgwCounter = new BackgroundWorker();
            bgwMemory.WorkerReportsProgress = true;
            bgwMemory.WorkerSupportsCancellation = false;
            bgwMemory.ProgressChanged += bgwMemory_ProgressChanged;
            bgwMemory.DoWork += bgwMemory_DoWork;
            bgwCounter.DoWork += bgwCounter_DoWork;
            bgwMemory.RunWorkerCompleted += bgwMemory_RunWorkerCompleted;

            W1Done = false;
            Silent = false;
            bgwMemory.RunWorkerAsync();
            bgwCounter.RunWorkerAsync();
        }

        /// <summary>
        /// Empty working set
        /// </summary>
        /// <param name="hProcess">hProcess</param>
        /// <returns></returns>
        [DllImport("psapi.dll")]
        public static extern bool EmptyWorkingSet(IntPtr hProcess);

        /// <summary>
        /// Cleans processes
        /// </summary>
        public static void CleanProcesses()
        {
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    EmptyWorkingSet(process.Handle);
                }
                catch
                {
                }
            }
        }

        void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!W1Done) e.Cancel = true;
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            Hide();
            // Save Setting to App.Config
            Settings.Default.OptimizeAutomaticallyWhenCPUBelow = nudOptimizeIfCPU.Value;
            Settings.Default.OptimizeAutomaticallyWhenFreeMemory = nudOptimizeIfMemory.Value;
            Settings.Default.increaseFreeMemory = nudIncreaseMemory.Value;

            Settings.Default.Save();
            Settings.Default.Upgrade();
            Settings.Default.Reload();
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void FormMain_Load(object sender, EventArgs e)
        {
            if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\FreeGamingBooster.exe"))
            {
                this.Icon = Properties.Resources.GBicon;
            }
            else
            {
                this.Icon = Properties.Resources.FSUIcon;
            }
            
            bool tominimize = false;
            string[] args = Environment.GetCommandLineArgs();

            foreach (string arg in args)
            {
                if (arg.Contains("/minimized"))
                {
                    tominimize = true;
                    break;
                }
            }

            if (tominimize)
            {
                Hide();
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                Visible = false;
            }
            else
            {
                Show();
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
                Visible = true;
            }

            SetCulture(new CultureInfo(CfgFile.Get("Lang")));

            prbCPU.Maximum = 100;
            prbCPU.Minimum = 0;
            prbCPU.Step = 1;
            tmrMain.Enabled = true;

            chbLoadOnStartup.Checked = IsStartup();

            LoadSetting();
        }

        void LoadSetting()
        {
            Settings.Default.Reload();

            nudOptimizeIfCPU.Value = Settings.Default.OptimizeAutomaticallyWhenCPUBelow;
            nudOptimizeIfMemory.Value = Settings.Default.OptimizeAutomaticallyWhenFreeMemory;
            nudIncreaseMemory.Value = Settings.Default.increaseFreeMemory;
        }

        void btnRecommended_Click(object sender, EventArgs e)
        {
            Recommended();
        }

        /// <summary>
        /// Gets recommended CPU and RAM values
        /// </summary>
        public void Recommended()
        {
            nudOptimizeIfMemory.Value = totalRam * 10 / 100;
            nudIncreaseMemory.Value = totalRam * 25 / 100;
            nudOptimizeIfCPU.Value = 10;
        }

        void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        void showMainWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;

            Activate();
        }

        void clearClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
        }

        void DoStep(bool clear)
        {
            prbMemory.Value = !clear ? prbMemory.Maximum : 0;
        }

        void bgwMemory_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            W1Done = true;
            if (!Silent)
            {
                BeginInvoke(new DelegateStep(DoStep), new object[] { false });
            }
            GC.Collect(0, GCCollectionMode.Forced);
            GC.GetTotalMemory(true);
            if (!Silent)
            {
                BeginInvoke(new DelegateStep(DoStep), new object[] { true });
            }
            Silent = false;
        }

        void bgwMemory_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!Silent) prbMemory.PerformStep();
        }

        void bgwCounter_DoWork(object sender, DoWorkEventArgs e)
        {
            int iCount = 0;
            while (iCount < 10)
            {
                iCount++;
                if (!W1Done)
                    try
                    {
                        bgwMemory.ReportProgress(iCount, null);
                    }
                    catch
                    {
                    }
                Thread.Sleep(1000);
            }
        }


        void bgwMemory_DoWork(object sender, DoWorkEventArgs e)
        {
            // MEMORYSTATUS status = new MEMORYSTATUS();

            //GlobalMemoryStatus(status);

            int mem = (int)(Trackbar * Math.Pow(1024, 2));
            if (mem <= 0) mem = int.MaxValue;

            bool bContinue = true;
            do
            {
                try
                {
                    byte[] src = new byte[mem];
                    int len = src.Length;

                    while (true)
                        try
                        {
                            len--;
                            if (len <= 0) break;

                            src[len] = 0;
                        }
                        catch
                        {
                            break;
                        }

                    bContinue = false;
                }
                catch (OutOfMemoryException)
                {
                    mem = (int)(mem * 0.99);
                }
            } while (bContinue);
            bgwMemory.ReportProgress(10);
            if (bgwCounter != null)
                if (bgwCounter.IsBusy)
                    try
                    {
                        bgwCounter.CancelAsync();
                    }
                    catch
                    {
                    }

            GC.Collect();
            GC.GetTotalMemory(false);
            GC.Collect();
        }

        void niMain_DoubleClick(object sender, EventArgs e)
        {
            if (Visible)
            {
                WindowState = FormWindowState.Minimized;
                Hide();
                ShowInTaskbar = false;
                Visible = false;
            }
            else
            {
                Show();
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
                Visible = true;
            }
        }

        void prsMain_Exited(object sender, EventArgs e)
        {
            ProcessExited = true;
            prbMemory.Value = 0;
            try
            {
                File.Delete(Optexe);
            }
            catch
            {
            }

            if (!showed)
            {
                showed = true;
                CleanProcesses();
                finalMemUsage = CalcFreeRAM();

                int gainedMemory = (finalMemUsage - initialMemUsage) > 0 ? (finalMemUsage - initialMemUsage) : 2;
                MessageBox.Show(String.Format(rm.GetString("optimization_complete"), gainedMemory.ToString()), Text, 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOptimize.Enabled = true;
            }
        }

        void SetCulture(CultureInfo culture)
        {
            var rm = new ResourceManager("MemoryOptimizer.Resources", typeof(FormMain).Assembly);
            Thread.CurrentThread.CurrentUICulture = culture;

            ucTop.Text = rm.GetString("memory_optimizer", culture);

            btnRecommended.Text = rm.GetString("recommended_settings", culture);
            btnClearClipboard.Text = rm.GetString("clear_clipboard", culture);
            grbMemory.Text = rm.GetString("memory_optimization", culture);
            btnOptimize.Text = rm.GetString("optimize", culture);
            lblMemory.Text = rm.GetString("memory_amount", culture) + ":";
            lblSelect.Text = rm.GetString("select_to_optimize", culture) + ".";
            grbResources.Text = rm.GetString("resources_usage", culture);
            lblTotalMemoryUsage.Text = rm.GetString("memory_usage", culture) + ":";
            grbClipboard.Text = rm.GetString("clear_clipboard", culture);
            lblClearClipboard.Text = rm.GetString("clear_clipboard_clear", culture) + ".";
            tbpOptions.Text = rm.GetString("options", culture);
            chbLoadOnStartup.Text = rm.GetString("load_on_startup", culture);
            grbSettings.Text = rm.GetString("auto_optimization_settings", culture);
            lblOptimizeIf.Text = rm.GetString("only_optimize_if", culture) + ": (%)";
            lblIncreaseMemory.Text = rm.GetString("increase_free_memory", culture) + ": (MB)";
            lblOptimize.Text = rm.GetString("optimize_automatically_at", culture) + ": (MB)";

            niMain.BalloonTipText = rm.GetString("memory_optimizer_running", culture);
            showMainWindowToolStripMenuItem.Text = rm.GetString("show_main_window", culture);
            clearClipboardToolStripMenuItem.Text = rm.GetString("clear_clipboard", culture);
            exitToolStripMenuItem.Text = rm.GetString("exit", culture);
            Text = rm.GetString("memory_optimizer", culture);
            lblCPUUsage.Text = rm.GetString("cpu_usage", culture);
            tbpOptimization.Text = rm.GetString("optimization", culture);
            grbResources.Text = rm.GetString("resources_usage", culture);
            grbClipboard.Text = rm.GetString("clear_clipboard", culture);
            btnOK.Text = rm.GetString("ok", culture);
            btnClose.Text = rm.GetString("close", culture);
            tcMain.Text = rm.GetString("memory_optimizer", culture);
        }

        void chbLoadOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (chbLoadOnStartup.Checked)
            {
                SetStartup();
            }
            else
            {
                ClrStartup();
            }
        }

        #region Nested type: MEMORYSTATUS

        [StructLayout(LayoutKind.Sequential)]
        internal class MEMORYSTATUS
        {
            internal int length;
            internal int memoryLoad;
            internal uint totalPhys;
            internal uint availPhys;
            internal uint totalPageFile;
            internal uint availPageFile;
            internal uint totalVirtual;
            internal uint availVirtual;
        }

        #endregion

        #region Nested type: delegateStep

        delegate void DelegateStep(bool clear);

        #endregion

        private void ucTop_Load(object sender, EventArgs e)
        {

        }
    }

    /// <summary>
    /// Vertical progressbar
    /// </summary>
    public class VerticalProgressBar : ProgressBar
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x04;
                return cp;
            }
        }
    }
}