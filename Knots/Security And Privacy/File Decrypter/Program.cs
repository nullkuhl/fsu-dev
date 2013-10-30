using System;
using System.Windows.Forms;

/// <summary>
/// The <see cref="Decrypter"/> namespace defines a Decrypter utility
/// </summary>
namespace Decrypter
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			// Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			// AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			// Properties.Resources.Culture = new CultureInfo("en");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormDecrypter());
		}

		/*
		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			
			Process.GetCurrentProcess().Kill();
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Reporting.Report((Exception)(e.ExceptionObject));
			Process.GetCurrentProcess().Kill();
		}*/
	}
}