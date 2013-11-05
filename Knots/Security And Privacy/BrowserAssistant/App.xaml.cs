using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using FreemiumUtil;
using System.IO;
using System.Reflection;

/// <summary>
/// The <see cref="BrowserAssistant"/> namespace defines a Browser Assistant knot
/// </summary>
namespace BrowserAssistant
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        static Mutex mutex;
        static bool created;

        /// <summary>
        /// <see cref="App"/> constructor
        /// </summary>
        public App()
        {
            BrowserAssistant.Properties.Resources.Culture = new CultureInfo(CfgFile.Get("Lang"));
        }

        void Application_Startup(object sender, StartupEventArgs e)
        {
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
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
                    var process = new ProcessStartInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\FreemiumUtilities.exe");
                    Process.Start(process);
                }
                catch (Exception)
                {
                }

                Application.Current.Shutdown();
                return;
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
    }
}