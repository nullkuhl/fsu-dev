using System;
using System.Diagnostics;

namespace FreemiumUtil
{
	/// <summary>
	/// Reporting class that contains a methods for working with the bug reports
	/// </summary>
	public static class Reporting
	{
		// The path for bug reporter executable
		static string bugReporterPath = "BugReporter.exe";        
		public static string BugReporterPath
		{
			get { return bugReporterPath; }
			set { bugReporterPath = value; }
		}

		/// <summary>
		/// Reports the given exception to the server by starting the bug reporter and passing the exception details.
		/// </summary>
		/// <param name="ex"></param>
		public static void Report(Exception ex)
		{
			string version = CfgFile.Get("Version");
            ProcessStartInfo procInfo = new ProcessStartInfo
			               	{
			               		FileName = BugReporterPath,
			               		Arguments = version +
			               		            " \"" + ex.StackTrace + "\"" +
			               		            " \"" + ex.GetType().Name + "\"" +
			               		            " \"" + ex.Message + "\"" +
			               		            " \"" + ex.TargetSite + "\"" +
			               		            " \"" + ex.Source + "\""
			               	};
			Process.Start(procInfo);
		}
	}
}