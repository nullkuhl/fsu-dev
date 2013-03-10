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
		internal bool Loop;
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
			RegistryKey key = Registry.CurrentUser;
			key = key.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
			if (key != null)
			{
				object val = key.GetValue("MemoryOptimizer");
				bool exists = false;
				if (val != null)
					exists = val.ToString().ToLower() == Runexe.ToLower();
				key.Close();
				return exists;
			}
			return false;
		}

		internal bool SetStartup()
		{
			RegistryKey key = Registry.CurrentUser;
			key = key.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			if (key != null) key.SetValue("MemoryOptimizer", Runexe, RegistryValueKind.String);
			return true;
		}

		internal bool ClrStartup()
		{
			RegistryKey key = Registry.CurrentUser;
			key = key.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			if (key != null)
			{
				key.DeleteValue("MemoryOptimizer", false);
				key.Close();
			}
			return true;
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

			CPUUsage = Convert.ToInt32(CPU.NextValue());
			prbCPU.Value = CPUUsage;
			prbCPU.PerformStep();
			lblStatus.Text = CPUUsage + " %";
		}

		/// <summary>
		/// Calc free RAM
		/// </summary>
		/// <returns></returns>
		public int CalcFreeRAM()
		{
			int ramUsage = Convert.ToInt32(RAM.NextValue());
			return ramUsage;
		}

		void tmrMain_Tick(object sender, EventArgs e)
		{
			var statusBarCounter = new PerformanceCounter("Memory", "Available MBytes");
			lblMemoryUsage.Text = ((((new ComputerInfo().TotalPhysicalMemory)/1024)/1024) - (ulong) statusBarCounter.NextValue()) + " MB";
			ulong memusage = ((((new ComputerInfo().TotalPhysicalMemory)/1024)/1024) - (ulong) statusBarCounter.NextValue());
			if (chtMemory.Series[0].Points.Count < 39)
			{
				chtMemory.Series[0].Points.Add(memusage);
			}
			else
			{
				chtMemory.Series[0].Points.Add(memusage);
				chtMemory.Series[0].Points.RemoveAt(0);
			}

			CalcCPUUsage();
			totalRam =
				Convert.ToInt32(((((new ComputerInfo().TotalPhysicalMemory)/1024)/1024) - (ulong) statusBarCounter.NextValue())) +
				CalcFreeRAM();
			lblMemoryTotal.Text = totalRam.ToString() + " MB";
			chtMemory.ChartAreas[0].AxisY.Maximum = totalRam;
			chtMemory.ChartAreas[0].AxisY.Minimum = 0;
			chtMemory.ChartAreas[0].AxisX.Maximum = 39;
			// this.chart1.ChartAreas[0].AxisX.Minimum = 20;

			if (track == false)
			{
				trackBarMemoryAmount.Maximum = totalRam - 94;
				trackBarMemoryAmount.Value = trackBarMemoryAmount.Maximum;
				track = true;

				//Recommended();
				//loadSetting();
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

			var stats = new MEMORYSTATUS();
			GlobalMemoryStatus(stats);
			bool autooptimize = nudOptimizeIfMemory.Value != 0 && stats.availPhys/Math.Pow(1024, 2) <= (double) nudOptimizeIfMemory.Value;
			if (!autooptimize) return;
			autooptimize = nudIncreaseMemory.Value != 0;
			if (!autooptimize) return;
			autooptimize = nudOptimizeIfCPU.Value == 0 || CPUUsage < (int) nudOptimizeIfCPU.Value;
			if (!autooptimize) return;


			// run resource
			if (ProcessExited)
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
					if (prsMain.Start())
					{
						ProcessExited = false;
						prbMemory.Step = 5;
						prbMemory.Value = 0;
						LastAuto = DateTime.Now;
					}
				}
				catch
				{
				}
			if (!ProcessExited) return;


			prbMemory.Step = 10;
			prbMemory.Value = 0;
			Trackbar = (int) nudIncreaseMemory.Value;
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
			Loop = true;
			bgwMemory = new BackgroundWorker {WorkerReportsProgress = true, WorkerSupportsCancellation = false};
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

		void trackBarMemoryAmount_ValueChanged(object sender, EventArgs e)
		{
			TrackValue = trackBarMemoryAmount.Value;
			lblMemory.Text = rm.GetString("memory_amount") + " : " + TrackValue.ToString() + " MB";
		}

		void btnOptimize_Click(object sender, EventArgs e)
		{
			/*
			Process proc = new Process();
			proc.StartInfo.FileName = "my02.exe";
			proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			proc.Start();
			while (!proc.HasExited)
			{
				Application.DoEvents();
				System.Threading.Thread.Sleep(1);
			}
			proc.Dispose();
			return;
			 */

			/*
			if (Convert.ToInt32(cpu.NextValue()) < numericUpDown3.Value)
			{
				
				//mc.Start();
			}

			mc.Start();
			isMemOptimzeRequested = true;
			 */
			showed = false;
			btnOptimize.Enabled = false;
			initialMemUsage = CalcFreeRAM();
			CalcCPUUsage();

			if (!ProcessExited) return;
			try
			{
				Optexe = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Path.DirectorySeparatorChar +
				         "memopti.exe"; //"memopti.exe";
				var stream = new FileStream(Optexe, FileMode.Create, FileAccess.Write);
				stream.Write(Resources.memopti, 0, Resources.memopti.Length);
				stream.Flush();
				stream.Close();
				stream.Dispose();
				prsMain.StartInfo.FileName = Optexe;
				if (trackBarMemoryAmount.Value != trackBarMemoryAmount.Maximum)
					prsMain.StartInfo.Arguments = trackBarMemoryAmount.Value.ToString();
				if (prsMain.Start())
					ProcessExited = false;
			}
			catch
			{
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
			Loop = true;
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
			//cleanProcesses();
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
			//notifyIcon1.Visible = false;
			//mc.Stop();
			if (!W1Done) e.Cancel = true;
		}

		void btnOK_Click(object sender, EventArgs e)
		{
			//this.WindowState = FormWindowState.Minimized;
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
			bool tominimize = false;
			string[] args = Environment.GetCommandLineArgs();
			foreach (string arg in args)
			{
				if (arg.Contains("/minimized"))
				{
					tominimize = true;
				}
			}

			if (tominimize)
			{
				Visible = false;
				Hide();
				WindowState = FormWindowState.Minimized;
				ShowInTaskbar = false;
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
			nudOptimizeIfMemory.Value = totalRam*10/100;
			nudIncreaseMemory.Value = totalRam*25/100;
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
			//Application.Exit();
			W1Done = true;
			if (!Silent)
			{
				BeginInvoke(new DelegateStep(DoStep), new object[] {false});
				//MessageBox.Show(rm.GetString("optimization_complete")+"!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			GC.Collect(0, GCCollectionMode.Forced);
			GC.GetTotalMemory(true);
			if (!Silent)
			{
				BeginInvoke(new DelegateStep(DoStep), new object[] {true});
			}
			Silent = false;
		}

		void bgwMemory_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			//this.progressBar2.Value = e.ProgressPercentage;
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
						//this.BeginInvoke(new delegateStep(this.doStep), new object[1] { false });
					}
					catch
					{
					}
				Thread.Sleep(1000);
			}
			Loop = false;
		}


		void bgwMemory_DoWork(object sender, DoWorkEventArgs e)
		{
			var status = new MEMORYSTATUS();

			GlobalMemoryStatus(status);

			//int mem = (int)status.totalPhys;
			var mem = (int) (Trackbar*Math.Pow(1024, 2));
			if (mem <= 0) mem = int.MaxValue;

			bool bContinue = true;
			do
			{
				try
				{
					var src = new byte[mem];
					int len = src.Length;

					while (true) // && bLoop)
						try
						{
							len--;
							if (len <= 0) break;
							//return;

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
					mem = (int) (mem*0.99);
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
				MessageBox.Show(
					rm.GetString("optimization_complete") + ". " + rm.GetString("memory_freed") + " " + gainedMemory + " " +
					rm.GetString("memory") + ".", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				btnOptimize.Enabled = true;
			}
		}

		void SetCulture(CultureInfo culture)
		{
			var rm = new ResourceManager("MemoryOptimizer.Resources", typeof (FormMain).Assembly);
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