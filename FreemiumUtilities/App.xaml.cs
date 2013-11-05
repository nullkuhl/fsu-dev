using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using FreemiumUtil;
using FreemiumUtilities.Infrastructure;
using Microsoft.Win32;
using WPFLocalizeExtension.Engine;
using ContextMenu = System.Windows.Controls.ContextMenu;
using MessageBox = System.Windows.MessageBox;

namespace FreemiumUtilities
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		static Mutex mutex;
		static bool created;

		public static bool StartMinimized;
		public static bool Click1;
		NotifyIcon notifyIcon;

		/// <summary>
		/// startup application
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Application_Startup(object sender, StartupEventArgs e)
		{
			//Reducing the frames per second on the WPF animations update
			Timeline.DesiredFrameRateProperty.OverrideMetadata(
				typeof (Timeline),
				new FrameworkPropertyMetadata {DefaultValue = 10}
				);

			CfgFile.CfgFilePath = "freemium.cfg";
			LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo(CfgFile.Get("Lang"));

			if (!IsAdmin())
			{
				MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("RunAsAdmin"),
				                WPFLocalizeExtensionHelpers.GetUIString("RunAsAdminTitle"),
				                MessageBoxButton.OK, MessageBoxImage.Error);
				Environment.Exit(1);
			}

			if (!IsNET35SP1Installed())
			{
				MessageBoxResult result = MessageBox.Show(
					WPFLocalizeExtensionHelpers.GetUIString("CheckNETVersion"),
					WPFLocalizeExtensionHelpers.GetUIString("CheckNETVersionTitle"),
					MessageBoxButton.OKCancel,
					MessageBoxImage.Error);
				if (result == MessageBoxResult.OK)
				{
					Process.Start(new ProcessStartInfo("http://www.microsoft.com/download/en/details.aspx?id=22"));
				}
				Environment.Exit(0);
			}

			//Report Unhandled Exceptions to bug reporter
			//AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			mutex = new Mutex(true, Process.GetCurrentProcess().ProcessName, out created);
			if (!created)
			{
				// If ther is more than one, than it is already running.
				Environment.Exit(0);
			}

			StartMinimized = false;
			Click1 = false;
			for (int i = 0; i != e.Args.Length; ++i)
			{
				if ((e.Args[i] == "StartHidden") || (e.Args[i] == "Click1"))
				{
					StartMinimized = true;
				}
			}

			notifyIcon = new NotifyIcon
			             	{
			             		Icon = FreemiumUtilities.Properties.Resources.desktop_icon,
			             		Text = "Freemium System Utilities",
			             		Visible = true
			             	};
			notifyIcon.DoubleClick +=
				delegate
					{
						MainWindow.Show();
						MainWindow.ShowInTaskbar = true;
						MainWindow.WindowState = WindowState.Normal;
						MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
					};
			notifyIcon.MouseUp += notifyIcon_MouseClick;
		}

		/// <summary>
		/// check if .net 3.5 sp1 is installed
		/// </summary>
		/// <returns></returns>
		bool IsNET35SP1Installed()
		{
			RegistryKey installedVersions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");
			if (installedVersions != null)
			{
				string[] versionNames = installedVersions.GetSubKeyNames();
				int index = Array.IndexOf(versionNames, "v3.5");
				if (index != -1)
				{
					RegistryKey openSubKey = installedVersions.OpenSubKey(versionNames[index]);
					return openSubKey != null && openSubKey.GetValue("SP", 0).ToString() == "1";
				}
				return false;
			}
			return false;
		}

		/// <summary>
		/// handle MouseClick to show notify icon context menu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				var menu = (ContextMenu) MainWindow.FindResource("RightClickSystemTray");
				menu.IsOpen = true;
			}
		}

		void Application_Deactivated(object sender, EventArgs e)
		{
			if (notifyIcon != null)
			{
				//notifyIcon.Visible = false;
				//notifyIcon.Dispose();
			}
		}

		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			//MessageBox.Show("MSG" +	e.ExceptionObject);
			Reporting.Report((Exception) (e.ExceptionObject));
			Process.GetCurrentProcess().Kill();
		}

		void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			e.Handled = true;
			/*MessageBox.Show("MSG" +
							e.Exception.Message
							+ " \n SRC" +
							e.Exception.Source
							+ " \n STKTRC" +
							e.Exception.StackTrace
							+ " \n Trgetsite" +
							e.Exception.TargetSite
							+ " \n Help" +
							e.Exception.HelpLink
							+ " \n " +
							e.Exception);*/
			Reporting.Report(e.Exception);
			Process.GetCurrentProcess().Kill();
		}

		/// <summary>
		/// check if user is admin
		/// </summary>
		/// <returns></returns>
		bool IsAdmin()
		{
			bool isAdmin = false;
			try
			{
				WindowsIdentity user = WindowsIdentity.GetCurrent();
				if (user != null)
				{
					var principal = new WindowsPrincipal(user);
					isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
				}
			}
			catch
			{
				isAdmin = false;
			}

			return isAdmin;
		}

		/// <summary>
		/// handle Exit event to close app
		/// </summary>
		void OnApplicationExit(object sender, ExitEventArgs e)
		{
			if (notifyIcon != null)
			{
				notifyIcon.Dispose();
			}
		}
	}
}