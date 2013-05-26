using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Disk_Cleaner.Properties;
using FreemiumUtil;

/// <summary>
/// The <see cref="Disk_Cleaner"/> namespace defines a Disk cleaner knot
/// </summary>
namespace Disk_Cleaner
{
    internal static class Program
    {
        static Mutex mutex;
        static bool created;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            mutex = new Mutex(true, Process.GetCurrentProcess().ProcessName, out created);
            if (created)
            {
                //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //Application.ThreadException += Application_ThreadException;

                // As all first run initialization is done in the main project,
                // we need to make sure the user does not start a different knot first.
                if (CfgFile.Get("FirstRun") != "0")
                {
                    try
                    {
#if PCCleaner
                        ProcessStartInfo process = new ProcessStartInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\PCCleaner.exe");
#else
                        ProcessStartInfo process = new ProcessStartInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\FreemiumUtilities.exe");
#endif
                        Process.Start(process);
                    }
                    catch (Exception)
                    {
                    }

                    Application.Exit();
                    return;
                }

                Resources.Culture = new CultureInfo(CfgFile.Get("Lang"));
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}