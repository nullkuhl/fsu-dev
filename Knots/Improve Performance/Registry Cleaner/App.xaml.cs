using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using FreemiumUtil;

namespace RegistryCleaner
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		static Mutex mutex;
		static bool created;

		void Application_Startup(object sender, StartupEventArgs e)
		{
			//AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			mutex = new Mutex(true, Process.GetCurrentProcess().ProcessName, out created);
			if (!created)
			{
				Current.Shutdown();
			}
			RegistryCleaner.Properties.Resources.Culture = new CultureInfo(CfgFile.Get("Lang"));

			if (!IsUserAdministrator())
			{
				MessageBox.Show(RegistryCleaner.Properties.Resources.AdminRightsNeeded,
								RegistryCleaner.Properties.Resources.RegistryCleaner, MessageBoxButton.OK, MessageBoxImage.Error);
				Shutdown();
			}
		}

		/// <summary>
		/// Determains is the current user is administrator
		/// </summary>
		/// <returns></returns>
		public bool IsUserAdministrator()
		{
			bool isAdmin = false;
			try
			{
				//get the currently logged in user
				WindowsIdentity user = WindowsIdentity.GetCurrent();
				if (user != null)
				{
					var principal = new WindowsPrincipal(user);
					isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
				}
			}
			catch (UnauthorizedAccessException)
			{
				isAdmin = false;
			}
			catch (Exception)
			{
				isAdmin = false;
			}
			return isAdmin;
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