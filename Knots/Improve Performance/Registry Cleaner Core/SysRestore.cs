using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Management;

namespace RegistryCleanerCore
{
	/// <summary>
	/// Contains methods to work with system restore
	/// </summary>
	public class SysRestore
	{
        [DllImport("kernel32.dll")]
        public static extern int SearchPath(string strPath, string strFileName, string strExtension, uint nBufferLength,
                                            StringBuilder strBuffer, string strFilePart);
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


        public static bool SearchPath(string fileName)
        {
            string retPath;
            return SearchPath(fileName, null, out retPath);
        }
		/// <summary>
		/// Verifies that the OS can do system restores
		/// </summary>
		/// <returns>True if OS is either ME,XP,Vista</returns>
		public static bool SysRestoreAvailable()
		{
			int majorVersion = Environment.OSVersion.Version.Major;
			int minorVersion = Environment.OSVersion.Version.Minor;

		    // See if DLL exists
			if (SearchPath("srclient.dll"))
				return true;
			
			if (majorVersion > 3)
				return true;

			// All others : Win 95, 98, 2000, Server
			return false;
		}

        public static bool SearchPath(string fileName, string path, out string retPath)
        {
            var strBuffer = new StringBuilder(260);

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
		/// Starts system restore
		/// </summary>
		/// <param name="strDescription">The description of the restore</param>
		/// <param name="lSeqNum">Returns the sequence number</param>
		/// <returns>The status of call</returns>
		public static int StartRestore(string strDescription, out long lSeqNum)
		{
            if (Environment.OSVersion.Version.Major > 6 ||
                Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 2)
            {
                // We have Windows 8 and therefore using WMI.

                try
                {
                    ManagementScope oScope = new ManagementScope("\\\\localhost\\root\\default");
                    ManagementPath oPath = new ManagementPath("SystemRestore");
                    ObjectGetOptions oGetOp = new ObjectGetOptions();
                    ManagementClass oProcess = new ManagementClass(oScope, oPath, oGetOp);

                    ManagementBaseObject oInParams = oProcess.GetMethodParameters("CreateRestorePoint");
                    oInParams["Description"] = strDescription;
                    oInParams["RestorePointType"] = 12; // MODIFY_SETTINGS
                    oInParams["EventType"] = 100;

                    ManagementBaseObject oOutParams = oProcess.InvokeMethod("CreateRestorePoint", oInParams, null);
                }
                catch (Exception)
                {
                    // ToDo: send exception details via SmartAssembly bug reporting!
                }

                lSeqNum = 0;
                return 0;
            }
            else
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
                    rpInfo.dwRestorePtType = (int)RestoreType.Restore;
                    rpInfo.llSequenceNumber = 0;
                    rpInfo.szDescription = strDescription;

                    SRSetRestorePointW(ref rpInfo, out rpStatus);

                    lSeqNum = rpStatus.llSequenceNumber;
                    return rpStatus.nStatus;
                }
                catch (DllNotFoundException)
                {
                }
                catch (Exception)
                {
                    // ToDo: send exception details via SmartAssembly bug reporting!
                }

                lSeqNum = 0;
                return 0;
            }
		}

		/// <summary>
		/// Ends system restore call
		/// </summary>
		/// <param name="lSeqNum">The restore sequence number</param>
		/// <returns>The status of call</returns>
		public static int EndRestore(long lSeqNum)
		{
            if (Environment.OSVersion.Version.Major > 6 ||
                    Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 2)
            {
                // There is nothing to do for Windows 8 here.

                return 0;
            }
            else
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
                catch (Exception)
                {
                    // ToDo: send exception details via SmartAssembly bug reporting!
                    return 0;
                }

                return rpStatus.nStatus;
            }
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

        /// <summary>
        /// Create restore point WMI for Windows 8
        /// </summary>
        /// <param name="desc">The restore point description</param>
        /// <returns>The status of call</returns>
        public static bool CreateRestorePoint(string description)
        {
            ManagementScope oScope = new ManagementScope("\\\\localhost\\root\\default");
            ManagementPath oPath = new ManagementPath("SystemRestore");
            ObjectGetOptions oGetOp = new ObjectGetOptions();
            ManagementClass oProcess = new ManagementClass(oScope, oPath, oGetOp);

            ManagementBaseObject oInParams = oProcess.GetMethodParameters("CreateRestorePoint");
            oInParams["Description"] = description;
            oInParams["RestorePointType"] = 12; // MODIFY_SETTINGS
            oInParams["EventType"] = 100;

            try
            {
                ManagementBaseObject oOutParams = oProcess.InvokeMethod("CreateRestorePoint", oInParams, null);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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