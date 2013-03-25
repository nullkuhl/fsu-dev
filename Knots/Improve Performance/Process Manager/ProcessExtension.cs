using System;
using System.Diagnostics;
using System.Management;

namespace ProcessManager
{
	/// <summary>
	/// Process extension
	/// </summary>
	public static class ProcessExtension
	{
		/// <summary>
		/// Gets parent process for a specified <paramref name="process"/>
		/// </summary>
		/// <param name="process">Process instance</param>
		/// <returns>Parent process for a specified <paramref name="process"/></returns>
		public static Process Parent(this Process process)
		{
			try
			{
				var parentProcId = new PerformanceCounter("Process", "Creating Process ID", process.ProcessName);
				return Process.GetProcessById((int) parentProcId.NextValue());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}

        public static string GetProcessOwner(int processId)
        {
            string query = "Select * From Win32_Process Where ProcessID = " + processId;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach (ManagementObject obj in processList)
            {
                string[] argList = new string[] { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    // return DOMAIN\user
                    //return argList[1] + "\\" + argList[0];
                    return argList[0];
                }
            }

            return "NO OWNER";
        }
	}
}