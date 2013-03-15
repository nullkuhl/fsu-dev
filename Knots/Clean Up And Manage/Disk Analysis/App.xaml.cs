using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using FreemiumUtil;

namespace DiskAnalysis
{
	/// <summary>
	/// The <see cref="DiskAnalysis"/> namespace defines a Disk Analysis knot
	/// </summary>

	[System.Runtime.CompilerServices.CompilerGenerated]
	class NamespaceDoc { }

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
			DiskAnalysis.Properties.Resources.Culture = new CultureInfo(CfgFile.Get("Lang"));
			new HwndSource(new HwndSourceParameters());
		}

		void Application_Startup(object sender, StartupEventArgs e)
		{
            // AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			mutex = new Mutex(true, Process.GetCurrentProcess().ProcessName, out created);
			if (!created)
			{
				Current.Shutdown();
			}			
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

		/* void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{ 
			Reporting.Report((Exception)(e.ExceptionObject));
			Process.GetCurrentProcess().Kill();
		}

		void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			e.Handled = true;
			Reporting.Report(e.Exception);
			Process.GetCurrentProcess().Kill();
		}*/
	}
}