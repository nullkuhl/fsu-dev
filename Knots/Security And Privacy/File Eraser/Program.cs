using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using FileEraser.Properties;
using FreemiumUtil;
using System.IO;
using System.Reflection;

/// <summary>
/// The <see cref="FileEraser"/> namespace defines a Encrypt and Decrypt knot
/// </summary>
namespace FileEraser
{
    internal static class Program
    {
        static Mutex mutex;
        static bool created;

        /// <summary>
        /// Paranoid mode
        /// </summary>
        public static bool ParanoidMode { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            mutex = new Mutex(true, Process.GetCurrentProcess().ProcessName, out created);
            if (created)
            {
                //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //Application.ThreadException += Application_ThreadException;
                //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                // As all first run initialization is done in the main project,
                // we need to make sure the user does not start a different knot first.
                if (CfgFile.Get("FirstRun") != "0")
                {
                    try
                    {
                        ProcessStartInfo process = new ProcessStartInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\FreemiumUtilities.exe");
                        Process.Start(process);
                    }
                    catch (Exception)
                    {
                    }

                    Application.Exit();
                    return;
                }

                ImageResources.Culture = new CultureInfo(CfgFile.Get("Lang"));

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                try
                {
                    if (args.Length != 0)
                    {
                        if (args[0] == @"/p")
                        {
                            ParanoidMode = true;
                            Console.WriteLine("Caution: paranoid mode has been activated");
                        }
                    }
                }
                catch
                {
                }
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