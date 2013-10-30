using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace RegistryCleanerCore
{
	/// <summary>
	/// Restore utility
	/// </summary>
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	public class cRestore
	{
		#region Constants
		const int APPLICATION_INSTALL = 0;
		const int APPLICATION_UNINSTALL = 1;
		const int DEVICE_DRIVER_INSTALL = 10;
		const int MODIFY_SETTINGS = 12;
		const int CANCELLED_OPERATION = 13;
		const int FO_DELETE = 0x3;
		const int FOF_ALLOWUNDO = 0x40;
		const int FOF_NOCONFIRMATION = 0x10;
		const int BEGIN_SYSTEM_CHANGE = 100;
		const int END_SYSTEM_CHANGE = 101;
		const int BEGIN_NESTED_SYSTEM_CHANGE = 102;
		const int END_NESTED_SYSTEM_CHANGE = 103;
		const int DESKTOP_SETTING = 2;
		const int ACCESSIBILITY_SETTING = 3;
		const int OE_SETTING = 4;
		const int APPLICATION_RUN = 5;
		const int WINDOWS_SHUTDOWN = 8;
		const int WINDOWS_BOOT = 9;
		const int MAX_DESC = 64;
		const int MAX_DESC_W = 256;
		const string RESTORE_KEY = @"Software\Microsoft\Windows NT\CurrentVersion\SystemRestore";
		const string RESTORE_VALUE = @"SystemRestorePointCreationFrequency";
		#endregion

		#region Enum
		enum RESTORE_TYPE
		{
			APPLICATION_INSTALL = 0,
			APPLICATION_UNINSTALL = 1,
			MODIFY_SETTINGS = 12,
			CANCELLED_OPERATION = 13,
			RESTORE = 6,
			CHECKPOINT = 7,
			DEVICE_DRIVER_INSTALL = 10,
			FIRSTRUN = 11,
			BACKUP_RECOVERY = 14,
		}
		#endregion

		#region Structs
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct RESTOREPTINFO
		{
			/// <summary>
			/// Event type
			/// </summary>
			public int dwEventType;
			/// <summary>
			/// Restore point type
			/// </summary>
			public int dwRestorePtType;
			/// <summary>
			/// Sequence number
			/// </summary>
			public Int64 llSequenceNumber;

			/// <summary>
			/// Description
			/// </summary>
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_DESC_W + 1)]
			public string szDescription;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct SMGRSTATUS
		{
			/// <summary>
			/// Status
			/// </summary>
			public int nStatus;

			/// <summary>
			/// Sequence number
			/// </summary>
			public Int64 llSequenceNumber;
		}
		#endregion

		#region API
		[DllImport("srclient.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool SRSetRestorePointW(ref RESTOREPTINFO pRestorePtSpec, out SMGRSTATUS pSMgrStatus);
		#endregion

		#region Fields
		long _lSeqNum = 0;
		int _iRestInt = 0;
		#endregion

		#region Methods

		/// <summary>
		/// Starts restore
		/// </summary>
		/// <param name="Description"></param>
		/// <returns></returns>
		public bool StartRestore(string Description)
		{
			int maj = Environment.OSVersion.Version.Major;
			int min = Environment.OSVersion.Version.Minor;
			RESTOREPTINFO tRPI = new RESTOREPTINFO();
			SMGRSTATUS tStatus = new SMGRSTATUS();

			// compatability
			if (!(maj == 4 && min == 90 || maj > 4))
			{
				return false;
			}

			tRPI.dwEventType = BEGIN_SYSTEM_CHANGE;
			tRPI.dwRestorePtType = (int)RESTORE_TYPE.MODIFY_SETTINGS;
			tRPI.llSequenceNumber = 0;
			tRPI.szDescription = Description;

			// test for key that defines multiple restores per cycle
			cLightning cl = new cLightning();
			if (cl.ValueExists(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, RESTORE_KEY, RESTORE_VALUE))
			{
				_iRestInt = cl.ReadDword(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, RESTORE_KEY, RESTORE_VALUE);
			}
			// set to 2 minutes
			cl.WriteDword(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, RESTORE_KEY, RESTORE_VALUE, 2);
			if (SRSetRestorePointW(ref tRPI, out tStatus))
			{
				_lSeqNum = tStatus.llSequenceNumber;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Ends restore
		/// </summary>
		/// <param name="Cancel"></param>
		/// <returns></returns>
		public bool EndRestore(bool Cancel)
		{
			RESTOREPTINFO tRPI = new RESTOREPTINFO();
			SMGRSTATUS tStatus = new SMGRSTATUS();
			bool success = false;

			tRPI.dwEventType = END_SYSTEM_CHANGE;
			tRPI.llSequenceNumber = _lSeqNum;

			if (Cancel == true)
			{
				tRPI.dwRestorePtType = CANCELLED_OPERATION;
			}

			try
			{
				success = (SRSetRestorePointW(ref tRPI, out tStatus));
			}
			finally
			{
				// reset
				cLightning cl = new cLightning();
				cl.WriteDword(cLightning.ROOT_KEY.HKEY_LOCAL_MACHINE, RESTORE_KEY, RESTORE_VALUE, _iRestInt);
			}
			return success;
		}
		#endregion
	}
}
