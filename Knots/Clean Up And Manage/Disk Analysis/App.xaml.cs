using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using FreemiumUtil;
using System;
using System.IO;
using System.Reflection;

/// <summary>
/// The <see cref="DiskAnalysis"/> namespace defines a Disk Analysis knot
/// </summary>
namespace DiskAnalysis
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        static Mutex mutex;
        static bool created;

        /// <summary>
        /// Is directory passed
        /// </summary>
        public static bool PassedDir;
        /// <summary>
        /// Passed directory
        /// </summary>
        public static string PassDir = "";

        /// <summary>
        /// App class constructor
        /// </summary>
        public App()
        {
            new HwndSource(new HwndSourceParameters());
        }

        void Application_Startup(object sender, StartupEventArgs e)
        {
            mutex = new Mutex(true, Process.GetCurrentProcess().ProcessName, out created);
            if (!created)
            {
                Current.Shutdown();
            }

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

                Application.Current.Shutdown();
                return;
            }

            DiskAnalysis.Properties.Resources.Culture = new CultureInfo(CfgFile.Get("Lang"));

            for (int i = 0; i != e.Args.Length; ++i)
            {
                if (PassedDir)
                {
                    PassDir = e.Args[i];
                }
                if (e.Args[i] == "ANALYSE")
                {
                    PassedDir = true;
                }
            }
        }
    }
}