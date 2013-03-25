using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using FreemiumUtil;
using Microsoft.Win32;
using System.IO;
using System.Reflection;

namespace RegistryCompactor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        static Mutex mutex;
        static bool created;

        // All WPF applications should execute on a single-threaded apartment (STA) thread
        [STAThread]
        public static void Main()
        {
            mutex = new Mutex(true, Process.GetCurrentProcess().ProcessName, out created);
            
            if (!created)
            {
                return;
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
            }
            else
            {
                App app = new App();
                Permissions.SetPrivileges(true);
                app.InitializeComponent();
                app.Run();
                Permissions.SetPrivileges(false);
            }
        }

        void Application_Startup(object sender, StartupEventArgs e)
        {
            RegistryKey hives;
            Compactor.RegistryHives = new ObservableCollection<Hive>();
            try
            {
                using (hives = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\hivelist"))
                {
                    if (hives == null)
                        throw new ApplicationException(RegistryCompactor.Properties.Resources.HiveListError);

                    foreach (string valueName in hives.GetValueNames())
                    {
                        // Don't touch these hives because they are critical for Windows
                        if (valueName.Contains("BCD") || valueName.Contains("HARDWARE"))
                            continue;

                        string hivePath = hives.GetValue(valueName) as string;

                        if (hivePath != null && hivePath[hivePath.Length - 1] == 0)
                            hivePath = hivePath.Substring(0, hivePath.Length - 1);

                        if (!string.IsNullOrEmpty(valueName) && !string.IsNullOrEmpty(hivePath))
                            Compactor.RegistryHives.Add(new Hive(valueName, hivePath));
                    }
                }
            }
            catch
            {
            }
            RegistryCompactor.Properties.Resources.Culture = new CultureInfo(CfgFile.Get("Lang"));
        }
    }
}