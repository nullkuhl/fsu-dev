using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;
using RegistryCompactor.Properties;
using ListView = System.Windows.Controls.ListView;

namespace RegistryCompactor
{
	/// <summary>
	/// Utilites
	/// </summary>
	internal static class Utilites
	{
		/// <summary>
		/// Returns true if the OS is 64 bit
		/// </summary>
		public static bool Is64BitOS
		{
			get { return OperationSystemInfo.IsX64(); }
		}

		/// <summary>
		/// Returns product name
		/// </summary>
		public static string ProductName
		{
			get { return Application.ProductName; }
		}

		/// <summary>
		/// Gets/Sets last update time
		/// </summary>
		public static DateTime LastUpdate
		{
			get
			{
				long o = Settings.Default.lastUpdate;
				if (o != 0)
					return DateTime.FromBinary(o);
				return DateTime.MinValue;
			}
			set { Settings.Default.lastUpdate = value.ToBinary(); }
		}

		/// <summary>
		/// Scan start date
		/// </summary>
		public static DateTime ScanStartTime { get; set; }

		/// <summary>
		/// Look up for a specified <paramref name="strPath"/>
		/// </summary>
		/// <param name="strPath"></param>
		/// <param name="strFileName"></param>
		/// <param name="strExtension"></param>
		/// <param name="nBufferLength"></param>
		/// <param name="strBuffer"></param>
		/// <param name="strFilePart"></param>
		/// <returns></returns>
		[DllImport("kernel32.dll")]
		public static extern int SearchPath(string strPath, string strFileName, string strExtension, uint nBufferLength,
		                                    StringBuilder strBuffer, string strFilePart);

		/// <summary>
		/// Makes a query to the DOS services
		/// </summary>
		/// <param name="lpDeviceName"></param>
		/// <param name="lpTargetPath"></param>
		/// <param name="ucchMax"></param>
		/// <returns></returns>
		[DllImport("kernel32.dll")]
		public static extern uint QueryDosDevice([In, Optional] string lpDeviceName, [Out] StringBuilder lpTargetPath,
		                                         [In] int ucchMax);

		/// <summary>
		/// Converts the size in bytes to a formatted string
		/// </summary>
		/// <param name="length">Size in bytes</param>
		/// <returns>Formatted String</returns>
		public static string ConvertSizeToString(long length)
		{
			if (length < 0)
				return "";

			float nSize;
			string strSizeFmt, strUnit;

			if (length < 1000) // 1KB
			{
				nSize = length;
				strUnit = " B";
			}
			else if (length < 1000000) // 1MB
			{
				nSize = length/(float) 0x400;
				strUnit = " KB";
			}
			else if (length < 1000000000) // 1GB
			{
				nSize = length/(float) 0x100000;
				strUnit = " MB";
			}
			else
			{
				nSize = length/(float) 0x40000000;
				strUnit = " GB";
			}

			if (nSize == (int) nSize)
				strSizeFmt = nSize.ToString("0");
			else if (nSize < 10)
				strSizeFmt = nSize.ToString("0.00");
			else if (nSize < 100)
				strSizeFmt = nSize.ToString("0.0");
			else
				strSizeFmt = nSize.ToString("0");

			return strSizeFmt + strUnit;
		}

		/// <summary>
		/// Auto resize columns
		/// </summary>
		public static void AutoResizeColumns(ListView listView)
		{
			GridView gv = listView.View as GridView;

			if (gv != null)
			{
				foreach (GridViewColumn gvc in gv.Columns)
				{
					// Set width to max value because actual width doesn't include margins
					gvc.Width = double.MaxValue;

					// Set it to NaN to remove white space
					gvc.Width = double.NaN;
				}

				listView.UpdateLayout();
			}
		}

		/// <summary>
		/// Checks for default program then launches URI
		/// </summary>
		/// <param name="webAddress">The address to launch</param>
		public static void LaunchURI(string webAddress)
		{
			Help.ShowHelp(Form.ActiveForm, string.Copy(webAddress));
		}

		/// <summary>
		/// Searches for a provided <paramref name="fileName"/>
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static bool SearchPath(string fileName)
		{
			string retPath;
			return SearchPath(fileName, null, out retPath);
		}

		/// <summary>
		/// Searches for a provided <paramref name="fileName"/>
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="path"></param>
		/// <returns></returns>
		public static bool SearchPath(string fileName, string path)
		{
			string retPath;
			return SearchPath(fileName, path, out retPath);
		}

		/// <summary>
		/// Checks for the file using the specified path and/or %PATH% variable
		/// </summary>
		/// <param name="fileName">The name of the file for which to search</param>
		/// <param name="path">The path to be searched for the file (searches %path% variable if null)</param>
		/// <param name="retPath">The path containing the file</param>
		/// <returns>True if it was found</returns>
		public static bool SearchPath(string fileName, string path, out string retPath)
		{
			StringBuilder strBuffer = new StringBuilder(260);

			int ret = SearchPath(((!string.IsNullOrEmpty(path)) ? (path) : (null)), fileName, null, 260, strBuffer, null);

			if (ret != 0)
			{
				retPath = strBuffer.ToString();
				return true;
			}
			retPath = "";

			return false;
		}

		/// <summary>
		/// Gets a temporary path for a registry hive
		/// </summary>
		public static string GetTempHivePath()
		{
			try
			{
				string tempPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

				// File cant exists, keep retrying until we get a file that doesnt exist
				if (File.Exists(tempPath))
					return GetTempHivePath();
                else
    				return tempPath;
			}
			catch (IOException)
			{
				return GetTempHivePath();
			}
		}

		/// <summary>
		/// Converts \\Device\\HarddiskVolumeX\... to X:\...
		/// </summary>
		/// <param name="DevicePath">Device name with path</param>
		/// <returns>Drive path</returns>
		public static string ConvertDeviceToMSDOSName(string DevicePath)
		{
			string strDevicePath = string.Copy(DevicePath.ToLower());
			string strRetVal = "";

			// Convert \Device\HarddiskVolumeX\... to X:\...
			foreach (var kvp in QueryDosDevice())
			{
				string strDrivePath = kvp.Key.ToLower();
				string strDeviceName = kvp.Value.ToLower();

				if (strDevicePath.StartsWith(strDeviceName))
				{
					strRetVal = strDevicePath.Replace(strDeviceName, strDrivePath);
					break;
				}
			}

			return strRetVal;
		}

		static Dictionary<string, string> QueryDosDevice()
		{
			var ret = new Dictionary<string, string>();

            DriveInfo[] drives = null;

            try
            {
                drives = DriveInfo.GetDrives();
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }

            if (drives != null)
            {
                foreach (DriveInfo drive in drives)
                {
                    if (drive.IsReady)
                    {
                        string strDrivePath = drive.Name.Substring(0, 2);
                        StringBuilder strDeviceName = new StringBuilder(260);

                        // Convert C: to \Device\HarddiskVolume1
                        if (QueryDosDevice(strDrivePath, strDeviceName, 260) > 0)
                            ret.Add(strDrivePath, strDeviceName.ToString());
                    }
                }
            }

			return ret;
		}

		/// <summary>
		/// Gets the current size of the registry
		/// </summary>
		/// <returns>Registry size (in bytes)</returns>
		public static long GetOldRegistrySize()
		{
			if (Compactor.RegistryHives == null)
				return 0;

			if (Compactor.RegistryHives.Count == 0)
				return 0;

			return Compactor.RegistryHives.Sum(h => h.OldHiveSize);
		}

		/// <summary>
		/// Gets the compacted size of the registry
		/// </summary>
		/// <returns>Registry size (in bytes)</returns>
		public static long GetNewRegistrySize()
		{
			if (Compactor.RegistryHives == null)
				return 0;

			if (Compactor.RegistryHives.Count == 0)
				return 0;

			return Compactor.RegistryHives.Sum(h => h.NewHiveSize);
		}
	}
}