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
using Microsoft.Win32;
using WPFLocalizeExtension.Engine;
using ContextMenu = System.Windows.Controls.ContextMenu;
using MessageBox = System.Windows.MessageBox;
using FreemiumUtilities.Infrastructure;

namespace PCCleaner
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

        readonly string[] languages = { "en", "de" };

        /// <summary>
        /// startup application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Startup(object sender, StartupEventArgs e)
        {
            //Reducing the frames per second on the WPF animations update
            //Report Unhandled Exceptions to bug reporter
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Timeline.DesiredFrameRateProperty.OverrideMetadata(
                typeof(Timeline),
                new FrameworkPropertyMetadata { DefaultValue = 10 }
                );


            string culture;

            if (CfgFile.Get("FirstRun") == "0")
            {
                culture = CfgFile.Get("Lang");
            }
            else
            {
                string currentUICulture = CultureInfo.CurrentUICulture.Name.Split('-')[0];
                culture = Array.IndexOf(languages, currentUICulture) != -1 ? currentUICulture : "en";
                CfgFile.Set("Lang", culture);
            }

            LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = LocalizeDictionary.Instance.Culture;

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

            mutex = new Mutex(true, Process.GetCurrentProcess().ProcessName, out created);
            if (!created)
            {
                // If there is more than one, than it is already running.
                Environment.Exit(0);
            }

            StartMinimized = false;
            Click1 = false;
            for (int i = 0; i != e.Args.Length; ++i)
            {
                if ((e.Args[i] == "StartHidden") || (e.Args[i] == "Click1"))
                {
                    StartMinimized = true;
                    break;
                }
            }

            notifyIcon = new NotifyIcon
                            {
                                Icon = PCCleaner.Properties.Resources.PCCleanerIcon,
                                Text = "PCCleaner",
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
            bool result = false;
            try
            {
                using (RegistryKey installedVersions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP"))
                {
                    if (installedVersions != null)
                    {
                        string[] versionNames = installedVersions.GetSubKeyNames();
                        int index = Array.IndexOf(versionNames, "v3.5");
                        if (index != -1)
                        {
                            using (RegistryKey openSubKey = installedVersions.OpenSubKey(versionNames[index]))
                            {
                                if (openSubKey != null && openSubKey.GetValue("SP", 0).ToString() == "1")
                                    result = true;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return result;
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
                ContextMenu menu = (ContextMenu)MainWindow.FindResource("RightClickSystemTray");
                menu.IsOpen = true;
            }
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
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
                    WindowsPrincipal principal = new WindowsPrincipal(user);
                    isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
            }
            catch
            {
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