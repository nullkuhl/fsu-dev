using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using FreemiumUtil;

namespace BrowserAssistant
{
	/// <summary>
	/// The <see cref="BrowserAssistant"/> namespace defines a Browser Assistant knot
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
		}

		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Reporting.Report((Exception)(e.ExceptionObject));
			Process.GetCurrentProcess().Kill();
		}

		void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			e.Handled = true;
			Reporting.Report(e.Exception);
			Process.GetCurrentProcess().Kill();
		}
	}
}