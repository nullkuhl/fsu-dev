using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FreemiumUtils;
using System.Diagnostics;

namespace Uninstall_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            Properties.Resources.Culture = new System.Globalization.CultureInfo(CfgFile.Get("Lang"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UninstallManager());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
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
