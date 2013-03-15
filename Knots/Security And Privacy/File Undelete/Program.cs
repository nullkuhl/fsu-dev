using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using FileUndelete.Properties;
using FreemiumUtil;

namespace FileUndelete
{
    /// <summary>
    /// The <see cref="FileUndelete"/> namespace defines a File Undelete knot
    /// </summary>

    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc { }

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
                //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //Application.ThreadException += Application_ThreadException;
                //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                Resources.Culture = new CultureInfo(CfgFile.Get("Lang"));

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Reporting.Report(e.Exception);
            Process.GetCurrentProcess().Kill();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Reporting.Report((Exception)(e.ExceptionObject));
            Process.GetCurrentProcess().Kill();
        }
    }
}