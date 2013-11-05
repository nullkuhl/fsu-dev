using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls;

/// <summary>
/// The <see cref="RegistryCompactor"/> namespace defines a RegistryOptimizer knot
/// </summary>
namespace RegistryCompactor
{
	internal static class PInvoke
	{
		#region HKEY enum

		/// <summary>
		/// Registry keys
		/// </summary>
		public enum HKEY : uint
		{
			HKEY_CLASSES_ROOT = 0x80000000,
			HKEY_CURRENT_USER = 0x80000001,
			HKEY_LOCAL_MACHINE = 0x80000002,
			HKEY_USERS = 0x80000003,
			HKEY_PERFORMANCE_DATA = 0x80000004,
			HKEY_PERFORMANCE_TEXT = 0x80000050,
			HKEY_PERFORMANCE_NLSTEXT = 0x80000060,
			HKEY_CURRENT_CONFIG = 0x80000005,
		}

		#endregion

		/// <summary>
		/// Major operating system
		/// </summary>
		public const uint MajorOperatingSystem = 0x00020000;

		/// <summary>
		/// Minor reconfig
		/// </summary>
		public const uint MinorReconfig = 0x00000004;

		/// <summary>
		/// Flag planned
		/// </summary>
		public const uint FlagPlanned = 0x80000000;

		/// <summary>
		/// RegOpenKeyA
		/// </summary>
		/// <param name="hKey"></param>
		/// <param name="lpSubKey"></param>
		/// <param name="phkResult"></param>
		/// <returns></returns>
		[DllImport("advapi32.dll", EntryPoint = "RegOpenKey", SetLastError = true)]
		public static extern int RegOpenKeyA(uint hKey, string lpSubKey, ref int phkResult);

		/// <summary>
		/// RegReplaceKeyA
		/// </summary>
		/// <param name="hKey"></param>
		/// <param name="lpSubKey"></param>
		/// <param name="lpNewFile"></param>
		/// <param name="lpOldFile"></param>
		/// <returns></returns>
		[DllImport("advapi32.dll", EntryPoint = "RegReplaceKey", SetLastError = true)]
		public static extern int RegReplaceKeyA(int hKey, string lpSubKey, string lpNewFile, string lpOldFile);

		/// <summary>
		/// RegSaveKeyA
		/// </summary>
		/// <param name="hKey"></param>
		/// <param name="lpFile"></param>
		/// <param name="lpSecurityAttributes"></param>
		/// <returns></returns>
		[DllImport("advapi32.dll", EntryPoint = "RegSaveKey", SetLastError = true)]
		public static extern int RegSaveKeyA(int hKey, string lpFile, int lpSecurityAttributes);

		/// <summary>
		/// RegCloseKey
		/// </summary>
		/// <param name="hKey"></param>
		/// <returns></returns>
		[DllImport("advapi32.dll")]
		public static extern int RegCloseKey(int hKey);

		/// <summary>
		/// RegFlushKey
		/// </summary>
		/// <param name="hKey"></param>
		/// <returns></returns>
		[DllImport("advapi32.dll")]
		public static extern int RegFlushKey(int hKey);

		/// <summary>
		/// RegSaveKeyEx
		/// </summary>
		/// <param name="hKey"></param>
		/// <param name="lpFile"></param>
		/// <param name="lpSecurityAttributes"></param>
		/// <param name="flags"></param>
		/// <returns></returns>
		[DllImport("advapi32.dll")]
		public static extern int RegSaveKeyEx(IntPtr hKey, string lpFile, IntPtr lpSecurityAttributes, int flags);

		/// <summary>
		/// ExitWindowsEx
		/// </summary>
		/// <param name="uFlags"></param>
		/// <param name="dwReason"></param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

		/// <summary>
		/// IsUserAnAdmin
		/// </summary>
		/// <returns></returns>
		[DllImport("shell32.dll")]
		public static extern bool IsUserAnAdmin();
	}

	/// <summary>
	/// Registry hive
	/// </summary>
	public class Hive : IDisposable
	{
		readonly FileInfo fileInfo;
		readonly long lOldHiveSize;
		readonly string strHiveName, strHivePath;

		/// <summary>
		/// Registry hive properties
		/// </summary>
		public volatile bool Anaylized, Compacted;
		bool disposed;
		volatile int hKey;
		volatile uint lNewHiveSize;

		volatile string strKeyName;
		volatile string strNewHivePath = Utilites.GetTempHivePath();

		volatile string strOldHivePath = Utilites.GetTempHivePath();
		volatile string strRootKey;

		/// <summary>
		/// Constructor for Hive class
		/// </summary>
		/// <param name="hiveName">Name of Hive (\REGISTRY\USER\...)</param>
		/// <param name="hivePath">Path to Hive (\Device\HarddiskVolumeX\Windows\System32\config\... or C:\Windows\System32\config\...)</param>
		public Hive(string hiveName, string hivePath)
		{
			strHiveName = hiveName;
			strHivePath = File.Exists(hivePath) ? hivePath : Utilites.ConvertDeviceToMSDOSName(hivePath);
			try
			{
				fileInfo = new FileInfo(strHivePath);
				lOldHiveSize = GetFileSize(strHivePath);
			}
			catch
			{ }
		}

		/// <summary>
		/// Old hive path
		/// </summary>
		public string OldHivePath
		{
			get { return strOldHivePath; }
		}

		/// <summary>
		/// New hive path
		/// </summary>
		public string NewHivePath
		{
			get { return strNewHivePath; }
		}

		/// <summary>
		/// Hive file info
		/// </summary>
		public FileInfo HiveFileInfo
		{
			get { return fileInfo; }
		}

		/// <summary>
		/// Old hive size
		/// </summary>
		public long OldHiveSize
		{
			get { return lOldHiveSize; }
		}

		/// <summary>
		/// New hive size
		/// </summary>
		public long NewHiveSize
		{
			get { return lNewHiveSize; }
		}

		#region ListView Properties

		/// <summary>
		/// Registry hive
		/// </summary>
		public string RegistryHive
		{
			get { return HiveFileInfo.Name; }
		}

		/// <summary>
		/// Registry hive path
		/// </summary>
		public string RegistryHivePath
		{
			get { return strHivePath; }
		}

		/// <summary>
		/// Current hive size
		/// </summary>
		public string CurrentSize
		{
			get { return Utilites.ConvertSizeToString(lOldHiveSize); }
		}

		/// <summary>
		/// Compact hive size
		/// </summary>
		public string CompactSize
		{
			get { return Utilites.ConvertSizeToString(lNewHiveSize); }
		}

		/// <summary>
		/// Image
		/// </summary>
		public Image Image { get; set; }

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			if (!disposed)
			{
				if (hKey != 0)
					PInvoke.RegCloseKey(hKey);

				hKey = 0;

				if (File.Exists(strOldHivePath))
					File.Delete(strOldHivePath);

				disposed = true;
			}

			GC.SuppressFinalize(this);
		}

		#endregion

		/// <summary>
		/// Resets hive data
		/// </summary>
		public void Reset()
		{
			try
			{
				// Clear HKey
				if (hKey != 0)
					PInvoke.RegCloseKey(hKey);

				hKey = 0;

				// Remove hives
				if (File.Exists(strOldHivePath))
					File.Delete(strOldHivePath);

				if (File.Exists(strNewHivePath))
					File.Delete(strNewHivePath);

				// Reset registry size
				lNewHiveSize = 0;

				// Reset analyzed
				Anaylized = false;
			}
			catch
			{
			}
		}

		/// <summary>
		/// Uses Windows RegSaveKeyA API to rewrite registry hive
		/// </summary>
		public void AnalyzeHive()
		{
			try
			{
				Reset();
				if (Anaylized)
				{
					/*
					 *  In a perfect world there should be an exception as commented, not a return
					 */
					//throw new Exception(Properties.Resources.RegistryValueAlreadyAnalyzedError);
					return;
				}

				int nRet = 0, hkey = 0;

				strRootKey = strHiveName.ToLower();
				strKeyName = strRootKey.Substring(strRootKey.LastIndexOf('\\') + 1);

				// Open Handle to registry key
				if (strRootKey.StartsWith(@"\registry\machine"))
					nRet = PInvoke.RegOpenKeyA((uint)PInvoke.HKEY.HKEY_LOCAL_MACHINE, strKeyName, ref hkey);
				if (strRootKey.StartsWith(@"\registry\user"))
					nRet = PInvoke.RegOpenKeyA((uint)PInvoke.HKEY.HKEY_USERS, strKeyName, ref hkey);

				if (nRet != 0)
					return;

				hKey = hkey;

				// Begin Critical Region
				Thread.BeginCriticalRegion();

				// Flush hive key
				PInvoke.RegFlushKey(hkey);

				// Function will fail if file already exists
				if (File.Exists(strNewHivePath))
					File.Delete(strNewHivePath);

				// Use API to rewrite the registry hive
				nRet = PInvoke.RegSaveKeyA(hkey, strNewHivePath, 0);
				if (nRet != 0)
					throw new Win32Exception(nRet);

				lNewHiveSize = (uint)GetFileSize(strNewHivePath);

				if (File.Exists(strNewHivePath))
					Anaylized = true;

				// End Critical Region
				Thread.EndCriticalRegion();
			}
			catch
			{
			}
		}

		/// <summary>
		/// Compacts the registry hive
		/// </summary>
		public void CompactHive()
		{
            try
            {
                if (Anaylized == false || hKey <= 0 || !File.Exists(strNewHivePath))
                    /*
                    *  In a perfect world there should be an exception as commented, not a return
                    */
                    //throw new Exception(Properties.Resources.AnalyzeBeforeCompactError);
                    return;

                if (Compacted)
                    /*
                    *  In a perfect world there should be an exception as commented, not a return
                    */
                    //throw new Exception(Properties.Resources.RegistryValueAlreadyCompactedError);
                    return;

                // Begin Critical Region
                Thread.BeginCriticalRegion();

                // Old hive cant exist or function will fail
                if (File.Exists(strOldHivePath))
                    File.Delete(strOldHivePath);

                // Replace hive with compressed hive
                int ret = PInvoke.RegReplaceKeyA(hKey, null, strNewHivePath, strOldHivePath);
                try
                {
                    if (ret != 0)
                        throw new Win32Exception(ret);

                    // Hive should now be replaced with temporary hive
                    PInvoke.RegCloseKey(hKey);
                }
                catch
                {
                }
                // End Critical Region
                Thread.EndCriticalRegion();
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }

			Compacted = true;
		}

		/// <summary>
		/// Gets the file size
		/// </summary>
		/// <param name="filePath">Path to the filename</param>
		/// <returns>File Size</returns>
		long GetFileSize(string filePath)
		{
            long result = 0;
			try
			{
                FileInfo fi = new FileInfo(filePath);
                result = fi.Length;
			}
			catch (Exception)
			{
			}
            return result;
		}

		/// <summary>
		/// Returns the filename of the hive
		/// </summary>
		/// <returns>Hive filename</returns>
		public override string ToString()
		{
			return (string)fileInfo.Name.Clone();
		}
	}
}