using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace WindowsStandardTools
{
	/// <summary>
	/// Windows Standard Tools knot main form
	/// </summary>
	public partial class FormMain : Form
	{
		/// <summary>
		/// constructor for FrmWindowStdTools
		/// </summary>
		public FormMain()
		{
			InitializeComponent();
		}

		/// <summary>
		/// check if the current operating system is windows xp
		/// </summary>
		/// <returns></returns>
		public static Boolean OSisXp()
		{
			// Get OperatingSystem information from the system namespace.
			OperatingSystem osInfo = Environment.OSVersion;

			// Determine the platform.
			switch (osInfo.Platform)
			{
				case PlatformID.Win32NT:

					switch (osInfo.Version.Major)
					{
						case 4:
							return false;

						case 5:
							return true;
					}
					break;
			}
			return false;
		}

		/// <summary>
		/// initialize FrmWindowStdTools
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FrmWindowStdTools_Load(object sender, EventArgs e)
		{
			string lang = CfgFile.Get("Lang");
			SetCulture(new CultureInfo(lang));
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
			var rm = new ResourceManager("WindowsStandardTools.Resources", typeof (FormMain).Assembly);
			Thread.CurrentThread.CurrentUICulture = culture;

			CheckDisk.Text = rm.GetString("checkdisk");
			CheckDiskNote.Text = rm.GetString("checkdisk_note") + ".";
			DiskDefrag.Text = rm.GetString("disk_defragmenter");
			DiskDefragNote.Text = rm.GetString("disk_defragmenter_note") + ".";
			SysRestore.Text = rm.GetString("system_restore");
			SysRestoreNote.Text = rm.GetString("system_restore_note") + ".";
			SysFileChecker.Text = rm.GetString("system_file_checker");
			SysFileCheckerNote.Text = rm.GetString("system_file_checker_note") + ".";
			;
			Backup.Text = rm.GetString("backup");
			BackupNote.Text = rm.GetString("backup_note") + ".";
			Text = rm.GetString("window_title");
			ucTop.Text = rm.GetString("window_title");
		}

		/// <summary>
		/// handle Click event to run check disk
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void CheckDisk_Click(object sender, EventArgs e)
		{
            try
            {
                var file = new StreamWriter("script.cmd");
                file.WriteLine("@echo off");
                file.WriteLine("chkdsk /f /r");
                file.WriteLine("pause");
                file.Close();

                var process = new Process();
                process.StartInfo.FileName = "script.cmd";
                process.StartInfo.Arguments = "";
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = false;
                process.Start();
            }
            catch (Exception)
            {
                // ToDo: display a localized message to the user that the program could not be started!
            }
		}


		/// <summary>
		/// handle Click event to run disk defragment
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void DiskDefrag_Click(object sender, EventArgs e)
		{
            try
            {
                if (OSisXp())
                {
                    Process.Start("dfrg.msc");
                }
                else
                {
                    var process = new Process();
                    process.StartInfo.FileName = "dfrgui";
                    process.StartInfo.Arguments = "";
                    process.StartInfo.CreateNoWindow = false;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                }
            }
            catch (Exception)
            {
                // ToDo: display a localized message to the user that the program could not be started!
            }
		}

		/// <summary>
		/// handle Click event to run system restore
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void SysRestore_Click(object sender, EventArgs e)
		{
            try
            {
                if (OSisXp())
                {
                    Process.Start(Environment.ExpandEnvironmentVariables("%SystemRoot%") + "\\system32\\restore\\rstrui.exe ");
                }
                else
                {
                    var process = new Process();
                    process.StartInfo.FileName = "rstrui";
                    process.StartInfo.Arguments = "";
                    process.StartInfo.CreateNoWindow = false;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                }
            }
            catch (Exception)
            {
                // ToDo: display a localized message to the user that the program could not be started!
            }
		}

		/// <summary>
		/// handle Click event to run system file checker
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void SysFileChecker_Click(object sender, EventArgs e)
		{
            try
            {
                if (OSisXp())
                {
                    MessageBox.Show(
                        "Windows System File Checker might ask for your windows installation disc to continue, please prepare your disc",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    var process = new Process();
                    process.StartInfo.FileName = "sfc";
                    process.StartInfo.Arguments = "/scannow";
                    process.StartInfo.CreateNoWindow = false;
                    process.StartInfo.UseShellExecute = true;
                    process.Start();
                }
                else
                {
                    var file = new StreamWriter("script.cmd");
                    file.WriteLine("@echo off");
                    file.WriteLine("sfc /scannow");
                    file.WriteLine("pause");
                    file.Close();

                    var process = new Process();
                    process.StartInfo.FileName = "script.cmd";
                    process.StartInfo.Arguments = "";
                    process.StartInfo.CreateNoWindow = false;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                }
            }
            catch (Exception)
            {
                // ToDo: display a localized message to the user that the program could not be started!
            }
		}

		/// <summary>
		/// handle Click event to run backup
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Backup_Click(object sender, EventArgs e)
		{
            try
            {
                if (OSisXp())
                {
                    string curFile = Environment.ExpandEnvironmentVariables("%SystemRoot%") + "\\system32\\ntbackup.exe ";
                    if (File.Exists(curFile))
                    {
                        //or try process.start if this doesnt work
                        Process.Start(curFile);
                    }
                    else
                    {
                        MessageBox.Show(rm.GetString("tool_not_found") + ".",
                                        rm.GetString("critical_warning"),
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    Process.Start("sdclt.exe");
                }
            }
            catch (Exception)
            {
                // ToDo: display a localized message to the user that the program could not be started!
            }
		}
	}
}