using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using System.IO;
using System.Reflection;

/// <summary>
/// The <see cref="Uninstall_Manager"/> namespace defines a Uninstall Manager knot
/// </summary>
namespace Uninstall_Manager
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
                //  AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                // Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                // Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

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

                Properties.Resources.Culture = new CultureInfo(CfgFile.Get("Lang"));

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new UninstallManager());
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