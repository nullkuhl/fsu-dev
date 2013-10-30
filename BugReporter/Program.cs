/*
 * This tool was developed mainly as a bug reporter for the freemium system Utilities software, 
 * it was designed specifically to interact with the freemium system Utilities webservice, 
 * in order to provide the user with a friendly interface for bug submission.
 */

using System;
using System.Windows.Forms;

namespace BugReporter
{
	static class Program
	{
		public static string Version { get; set; }
		public static string BugStackTrace { get; set; }
		public static string BugType { get; set; }
		public static string BugMessage { get; set; }
		public static string BugTargetSite { get; set; }
		public static string BugSource { get; set; }

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				Version = args[0];
				BugStackTrace = args[1];
				BugType = args[2];
				BugMessage = args[3];
				BugTargetSite = args[4];
				BugSource = args[5];
			}
			catch (IndexOutOfRangeException)
			{ }

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormMain());
		}
	}
}
