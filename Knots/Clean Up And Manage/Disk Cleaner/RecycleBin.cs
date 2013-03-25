using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Disk_Cleaner
{
	/// <summary>
	/// Contains metods to operate with a Windows Recycle bin
	/// </summary>
	public static class RecycleBin
	{
		/// <summary>
		/// Windows Recycle bin
		/// </summary>
		/// <param name="pszRootPath">pszRootPath</param>
		/// <param name="pSHQueryRBInfo">pSHQueryRBInfo</param>
		/// <returns>SHQueryRecycleBin</returns>
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		public static extern int SHQueryRecycleBin(string pszRootPath, ref SHQUERYRBINFO pSHQueryRBInfo);

		/// <summary>
		/// method for getting total files in the recycle bin and it's overall size
		/// </summary>
		/// <returns></returns>
		public static void GetRecycleBinSize(out ulong count, out ulong size)
		{
			count = size = 0;
            SHQUERYRBINFO query = new SHQUERYRBINFO { cbSize = Marshal.SizeOf(typeof(SHQUERYRBINFO)) };			
			try
			{
				var result = SHQueryRecycleBin(null, ref query);

				if (result == 0)
				{
					count = query.i64NumItems;
					size = query.i64Size;				
				}
			}
			catch
			{
			}			
		}

		[DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
		static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

		/// <summary>
		/// Empty Windows Recycle bin
		/// </summary>
		/// <returns></returns>
		public static uint EmptyRecycleBin()
		{
			return SHEmptyRecycleBin(IntPtr.Zero, null,
			                         RecycleFlags.SHERB_NOCONFIRMATION | RecycleFlags.SHERB_NOPROGRESSUI |
			                         RecycleFlags.SHERB_NOSOUND);
		}

		/// <summary>
		/// Converts an item identifier list to a file system path. (Note: SHGetPathFromIDList calls the ANSI version, must call SHGetPathFromIDListW for .NET)
		/// </summary>
		/// <param name="pidl">Address of an item identifier list that specifies a file or directory location relative to the root of the namespace (the desktop).</param>
		/// <param name="pszPath">Address of a buffer to receive the file system path. This buffer must be at least MAX_PATH characters in size.</param>
		/// <returns>Returns TRUE if successful, or FALSE otherwise. </returns>
		[DllImport("shell32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SHGetPathFromIDListW(IntPtr pidl, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder pszPath);

		/// <summary>
		/// Open Windows Recycle bin
		/// </summary>
		public static void Open()
		{
			var proc = new Process {StartInfo = {FileName = "::{645FF040-5081-101B-9F08-00AA002F954E}"}};
			try
			{
				proc.Start();
			}
			catch
			{
			}
		}

		/// <summary>
		/// Clean Windows Recycle bin
		/// </summary>
		public static void Clean()
		{
			var proc = new Process {StartInfo = {FileName = "cleanmgr.exe"}};
			try
			{
				proc.Start();
			}
			catch
			{
			}
		}

		#region Nested type: RecycleFlags

		[Flags]
		enum RecycleFlags : uint
		{
			SHERB_NOCONFIRMATION = 0x00000001,
			SHERB_NOPROGRESSUI = 0x00000002,
			SHERB_NOSOUND = 0x00000004
		}

		#endregion

		#region Nested type: SHQUERYRBINFO

		/// <summary>
		/// struct representing the SHQUERYRBINFO structure
		/// </summary>
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
		public struct SHQUERYRBINFO
		{
			public Int32 cbSize;
			public UInt64 i64Size;
			public UInt64 i64NumItems;
		}

		#endregion
	}
}