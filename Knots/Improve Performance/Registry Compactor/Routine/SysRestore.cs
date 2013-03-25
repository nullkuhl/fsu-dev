using System;
using System.Runtime.InteropServices;
using RegistryCompactor.Properties;

namespace RegistryCompactor
{
	/// <summary>
	/// Contains methods to work with system restore
	/// </summary>
	public class SysRestore
	{
		// Type of restorations

		#region RestoreType enum

		/// <summary>
		/// System restore types
		/// </summary>
		public enum RestoreType
		{
			ApplicationInstall = 0, // Installing a new application
			ApplicationUninstall = 1, // An application has been uninstalled
			ModifySettings = 12, // An application has had features added or removed
			CancelledOperation = 13, // An application needs to delete the restore point it created
			Restore = 6, // System Restore
			Checkpoint = 7, // Checkpoint
			DeviceDriverInstall = 10, // Device driver has been installed
			FirstRun = 11, // Program used for 1st time 
			BackupRecovery = 14 // Restoring a backup
		}

		#endregion

		/// <summary>
		/// Start of operation
		/// </summary>
		public const Int16 BeginSystemChange = 100;

		/// <summary>
		/// End of operation
		/// </summary>
		public const Int16 EndSystemChange = 101;
		
		/// <summary>
		/// Windows XP only - used to prevent the restore points intertwined
		/// </summary>
		public const Int16 BeginNestedSystemChange = 102;

		/// <summary>
		/// End of nested system change
		/// </summary>
		public const Int16 EndNestedSystemChange = 103;

		internal const Int16 DesktopSetting = 2; /* not implemented */
		internal const Int16 AccessibilitySetting = 3; /* not implemented */
		internal const Int16 OeSetting = 4; /* not implemented */
		internal const Int16 ApplicationRun = 5; /* not implemented */
		internal const Int16 WindowsShutdown = 8; /* not implemented */
		internal const Int16 WindowsBoot = 9; /* not implemented */
		internal const Int16 MaxDesc = 64;
		internal const Int16 MaxDescW = 256;

		/// <summary>
		/// Sets restore point
		/// </summary>
		/// <param name="pRestorePtSpec"></param>
		/// <param name="pSMgrStatus"></param>
		/// <returns></returns>
		[DllImport("srclient.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SRSetRestorePointW(ref RestorePointInfo pRestorePtSpec, out STATEMGRSTATUS pSMgrStatus);

		/// <summary>
		/// Verifies that the OS can do system restores
		/// </summary>
		/// <returns>True if OS is either ME,XP,Vista</returns>
		public static bool SysRestoreAvailable()
		{
			int majorVersion = Environment.OSVersion.Version.Major;
			int minorVersion = Environment.OSVersion.Version.Minor;

			// See if it is enabled
			if (!Settings.Default.optionsSysRestore)
				return false;

			// See if DLL exists
			if (Utilites.SearchPath("srclient.dll"))
				return true;

			// Windows ME
			if (majorVersion == 4 && minorVersion == 90)
				return true;

			// Windows XP
			if (majorVersion == 5 && minorVersion == 1)
				return true;

			// Windows Vista
			if (majorVersion == 6 && minorVersion == 0)
				return true;

			// Windows 7
			if (majorVersion == 6 && minorVersion == 1)
				return true;

			// Windows 8
			if (majorVersion == 6 && minorVersion == 2)
				return true;

			// All others : Win 95, 98, 2000, Server
			return false;
		}

		/// <summary>
		/// Starts system restore
		/// </summary>
		/// <param name="strDescription">The description of the restore</param>
		/// <param name="lSeqNum">Returns the sequence number</param>
		/// <returns>The status of call</returns>
		public static int StartRestore(string strDescription, out long lSeqNum)
		{
			var rpInfo = new RestorePointInfo();
			STATEMGRSTATUS rpStatus;

			if (!SysRestoreAvailable())
			{
				lSeqNum = 0;
				return 0;
			}

			try
			{
				// Prepare Restore Point
				rpInfo.dwEventType = BeginSystemChange;
				// By default we create a verification system
				rpInfo.dwRestorePtType = (int) RestoreType.Restore;
				rpInfo.llSequenceNumber = 0;
				rpInfo.szDescription = strDescription;

				SRSetRestorePointW(ref rpInfo, out rpStatus);
			}
			catch (DllNotFoundException)
			{
				lSeqNum = 0;
				return 0;
			}

			lSeqNum = rpStatus.llSequenceNumber;

			return rpStatus.nStatus;
		}

		/// <summary>
		/// Ends system restore call
		/// </summary>
		/// <param name="lSeqNum">The restore sequence number</param>
		/// <returns>The status of call</returns>
		public static int EndRestore(long lSeqNum)
		{
			var rpInfo = new RestorePointInfo();
			STATEMGRSTATUS rpStatus;

			if (!SysRestoreAvailable())
				return 0;

			try
			{
				rpInfo.dwEventType = EndSystemChange;
				rpInfo.llSequenceNumber = lSeqNum;

				SRSetRestorePointW(ref rpInfo, out rpStatus);
			}
			catch (DllNotFoundException)
			{
				return 0;
			}

			return rpStatus.nStatus;
		}

		/// <summary>
		/// Cancels restore call
		/// </summary>
		/// <param name="lSeqNum">The restore sequence number</param>
		/// <returns>The status of call</returns>
		public static int CancelRestore(long lSeqNum)
		{
			var rpInfo = new RestorePointInfo();
			STATEMGRSTATUS rpStatus;

			if (!SysRestoreAvailable())
				return 0;

			try
			{
				rpInfo.dwEventType = EndSystemChange;
				rpInfo.dwRestorePtType = (int) RestoreType.CancelledOperation;
				rpInfo.llSequenceNumber = lSeqNum;

				SRSetRestorePointW(ref rpInfo, out rpStatus);
			}
			catch (DllNotFoundException)
			{
				return 0;
			}

			return rpStatus.nStatus;
		}

		#region Nested type: RestorePointInfo

		/// <summary>
		/// Contains information used by the SRSetRestorePoint function
		/// </summary>
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct RestorePointInfo
		{
			/// <summary>
			/// The type of event
			/// </summary>
			public int dwEventType;

			/// <summary>
			/// The type of restore point
			/// </summary>
			public int dwRestorePtType;

			/// <summary>
			/// The sequence number of the restore point
			/// </summary>
			public Int64 llSequenceNumber;

			/// <summary>
			/// The description to be displayed so the user can easily identify a restore point
			/// </summary>
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxDescW + 1)]
			public string szDescription;
		}

		#endregion

		#region Nested type: STATEMGRSTATUS

		/// <summary>
		/// Contains status information used by the SRSetRestorePoint function
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct STATEMGRSTATUS
		{
			/// <summary>
			/// The status code
			/// </summary>
			public int nStatus;

			/// <summary>
			/// The sequence number of the restore point
			/// </summary>
			public Int64 llSequenceNumber;
		}

		#endregion
	}
}