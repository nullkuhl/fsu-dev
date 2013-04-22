using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.Resources;
using System.Windows.Forms;

namespace FileEraser
{
    /// <summary>
    /// cShredder
    /// </summary>
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class cShredder
    {
        /// <summary>
        /// Resource manager
        /// </summary>
        public ResourceManager rm = new ResourceManager("FileEraser.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        #region Constants
        // crypto
        const Int32 ALG_TYPE_ANY = 0x0;
        const Int32 ALG_SID_MD5 = 0x3;
        const Int32 ALG_CLASS_HASH = 0x32768;
        const Int32 HP_HASHVAL = 0x2;
        const Int32 HP_HASHSIZE = 0x4;
        const UInt32 CRYPT_VERIFYCONTEXT = 0xF0000000;
        const Int32 PROV_RSA_FULL = 0x1;
        const string MS_ENHANCED_PROV = "Microsoft Enhanced Cryptographic Provider v1.0";
        // findfile
        const Int32 MAX_PATH = 260;
        const Int32 MAX_ALTERNATE = 14;
        const Int32 SYNCHRONIZE = 0x100000;
        const Int32 STANDARD_RIGHTS_REQUIRED = 0xF0000;
        const Int32 FILE_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0xFFF);
        const Int32 PROCESS_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0xFFF);
        // attributes
        const Int32 FILE_ATTRIBUTE_READONLY = 0x00000001;
        const Int32 FILE_ATTRIBUTE_HIDDEN = 0x00000002;
        const Int32 FILE_ATTRIBUTE_SYSTEM = 0x00000004;
        const Int32 FILE_ATTRIBUTE_ARCHIVE = 0x00000020;
        const Int32 FILE_ATTRIBUTE_ENCRYPTED = 0x00000040;
        const Int32 FILE_ATTRIBUTE_NORMAL = 0x00000080;
        const Int32 FILE_ATTRIBUTE_TEMPORARY = 0x00000100;
        const Int32 FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200;
        const Int32 FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400;
        const Int32 FILE_ATTRIBUTE_COMPRESSED = 0x00000800;
        const Int32 FILE_ATTRIBUTE_OFFLINE = 0x00001000;
        const Int32 FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
        const UInt32 WRITE_THROUGH = 0x80000000;
        // file flags
        const Int32 GENERIC_ALL = 0x10000000;
        const Int32 GENERIC_WRITE = 0x40000000;
        const Int32 FILE_SHARE_NONE = 0x0;
        const Int32 OPEN_EXISTING = 3;
        const Int32 FILE_BEGIN = 0;
        const Int32 FILE_CURRENT = 1;
        const Int32 FILE_END = 2;
        const UInt32 FILE_MOVE_FAILED = 0xFFFFFFFF;
        // mem flags
        const UInt32 MEM_COMMIT = 0x1000;
        const UInt32 PAGE_READWRITE = 0x04;
        const UInt32 MEM_RELEASE = 0x8000;
        // movefileex
        const Int32 MOVEFILE_REPLACE_EXISTING = 0x1;
        const Int32 MOVEFILE_DELAY_UNTIL_REBOOT = 0x4;
        const Int32 MOVEFILE_WRITE_THROUGH = 0x8;
        // local
        const UInt32 BUFFER_SIZE = 65536;
        const string UNICODE_PREFIX = @"\\?\";
        // deviveIO
        const UInt32 FsctlDeleteObjectId = (0x00000009 << 16) | (40 << 2) | 0 | (0 << 14);
        // process
        const Int32 PROCESS_VM_READ = 0x16;
        const Int32 PROCESS_SET_INFORMATION = 0x200;
        const Int32 PROCESS_QUERY_INFORMATION = 0x400;
        const Int32 PROCESS_TERMINATE = 0x1;
        const Int32 WM_CLOSE = 0x10;
        const Int32 WAIT_OBJECT_0 = 0x00000000;
        const Int32 WAIT_TIMEOUT = 258;
        const Int32 STILL_ACTIVE = 259;
        const Int32 QS_ALLINPUT = 0x04FF;
        // privilege
        const Int32 SE_PRIVILEGE_ENABLED = 0x00000002;
        const Int32 TOKEN_QUERY = 0x00000008;
        const Int32 TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        const string SE_ASSIGNPRIMARYTOKEN_NAME = "SeAssignPrimaryTokenPrivilege";
        const string SE_AUDIT_NAME = "SeAuditPrivilege";
        const string SE_BACKUP_NAME = "SeBackupPrivilege";
        const string SE_CHANGE_NOTIFY_NAME = "SeChangeNotifyPrivilege";
        const string SE_CREATE_GLOBAL_NAME = "SeCreateGlobalPrivilege";
        const string SE_CREATE_PAGEFILE_NAME = "SeCreatePagefilePrivilege";
        const string SE_CREATE_PERMANENT_NAME = "SeCreatePermanentPrivilege";
        const string SE_CREATE_SYMBOLIC_LINK_NAME = "SeCreateSymbolicLinkPrivilege";
        const string SE_CREATE_TOKEN_NAME = "SeCreateTokenPrivilege";
        const string SE_DEBUG_NAME = "SeDebugPrivilege";
        const string SE_ENABLE_DELEGATION_NAME = "SeEnableDelegationPrivilege";
        const string SE_IMPERSONATE_NAME = "SeImpersonatePrivilege";
        const string SE_INC_BASE_PRIORITY_NAME = "SeIncreaseBasePriorityPrivilege";
        const string SE_INCREASE_QUOTA_NAME = "SeIncreaseQuotaPrivilege";
        const string SE_INC_WORKING_SET_NAME = "SeIncreaseWorkingSetPrivilege";
        const string SE_LOAD_DRIVER_NAME = "SeLoadDriverPrivilege";
        const string SE_LOCK_MEMORY_NAME = "SeLockMemoryPrivilege";
        const string SE_MACHINE_ACCOUNT_NAME = "SeMachineAccountPrivilege";
        const string SE_MANAGE_VOLUME_NAME = "SeManageVolumePrivilege";
        const string SE_PROF_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";
        const string SE_RELABEL_NAME = "SeRelabelPrivilege";
        const string SE_REMOTE_SHUTDOWN_NAME = "SeRelabelPrivilege";
        const string SE_RESTORE_NAME = "SeRestorePrivilege";
        const string SE_SECURITY_NAME = "SeRestorePrivilege";
        const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        const string SE_SYNC_AGENT_NAME = "SeSyncAgentPrivilege";
        const string SE_SYSTEM_ENVIRONMENT_NAME = "SeSystemEnvironmentPrivilege";
        const string SE_SYSTEM_PROFILE_NAME = "SeSystemProfilePrivilege";
        const string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";
        const string SE_TAKE_OWNERSHIP_NAME = "SeTakeOwnershipPrivilege";
        const string SE_TCB_NAME = "SeTcbPrivilege";
        const string SE_TIME_ZONE_NAME = "SeTimeZonePrivilege";
        const string SE_TRUSTED_CREDMAN_ACCESS_NAME = "SeTrustedCredManAccessPrivilege";
        const string SE_UNDOCK_NAME = "SeUndockPrivilege";
        const string SE_UNSOLICITED_INPUT_NAME = "SeUnsolicitedInputPrivilege";
        #endregion

        #region Enum

        /// <summary>
        /// Move methods
        /// </summary>
        public enum EMoveMethod : uint
        {
            Begin = 0,
            Current = 1,
            End = 2
        }

        enum ETOKEN_PRIVILEGES : uint
        {
            ASSIGN_PRIMARY = 0x1,
            TOKEN_DUPLICATE = 0x2,
            TOKEN_IMPERSONATE = 0x4,
            TOKEN_QUERY = 0x8,
            TOKEN_QUERY_SOURCE = 0x10,
            TOKEN_ADJUST_PRIVILEGES = 0x20,
            TOKEN_ADJUST_GROUPS = 0x40,
            TOKEN_ADJUST_DEFAULT = 0x80,
            TOKEN_ADJUST_SESSIONID = 0x100
        }

        enum SYSTEM_INFORMATION_CLASS
        {
            SystemInformationClassMin = 0,
            SystemBasicInformation = 0,
            SystemProcessorInformation = 1,
            SystemPerformanceInformation = 2,
            SystemTimeOfDayInformation = 3,
            SystemPathInformation = 4,
            SystemNotImplemented1 = 4,
            SystemProcessInformation = 5,
            SystemProcessesAndThreadsInformation = 5,
            SystemCallCountInfoInformation = 6,
            SystemCallCounts = 6,
            SystemDeviceInformation = 7,
            SystemConfigurationInformation = 7,
            SystemProcessorPerformanceInformation = 8,
            SystemProcessorTimes = 8,
            SystemFlagsInformation = 9,
            SystemGlobalFlag = 9,
            SystemCallTimeInformation = 10,
            SystemNotImplemented2 = 10,
            SystemModuleInformation = 11,
            SystemLocksInformation = 12,
            SystemLockInformation = 12,
            SystemStackTraceInformation = 13,
            SystemNotImplemented3 = 13,
            SystemPagedPoolInformation = 14,
            SystemNotImplemented4 = 14,
            SystemNonPagedPoolInformation = 15,
            SystemNotImplemented5 = 15,
            SystemHandleInformation = 16,
            SystemObjectInformation = 17,
            SystemPageFileInformation = 18,
            SystemPagefileInformation = 18,
            SystemVdmInstemulInformation = 19,
            SystemInstructionEmulationCounts = 19,
            SystemVdmBopInformation = 20,
            SystemInvalidInfoClass1 = 20,
            SystemFileCacheInformation = 21,
            SystemCacheInformation = 21,
            SystemPoolTagInformation = 22,
            SystemInterruptInformation = 23,
            SystemProcessorStatistics = 23,
            SystemDpcBehaviourInformation = 24,
            SystemDpcInformation = 24,
            SystemFullMemoryInformation = 25,
            SystemNotImplemented6 = 25,
            SystemLoadImage = 26,
            SystemUnloadImage = 27,
            SystemTimeAdjustmentInformation = 28,
            SystemTimeAdjustment = 28,
            SystemSummaryMemoryInformation = 29,
            SystemNotImplemented7 = 29,
            SystemNextEventIdInformation = 30,
            SystemNotImplemented8 = 30,
            SystemEventIdsInformation = 31,
            SystemNotImplemented9 = 31,
            SystemCrashDumpInformation = 32,
            SystemExceptionInformation = 33,
            SystemCrashDumpStateInformation = 34,
            SystemKernelDebuggerInformation = 35,
            SystemContextSwitchInformation = 36,
            SystemRegistryQuotaInformation = 37,
            SystemLoadAndCallImage = 38,
            SystemPrioritySeparation = 39,
            SystemPlugPlayBusInformation = 40,
            SystemNotImplemented10 = 40,
            SystemDockInformation = 41,
            SystemNotImplemented11 = 41,
            SystemInvalidInfoClass2 = 42,
            SystemProcessorSpeedInformation = 43,
            SystemInvalidInfoClass3 = 43,
            SystemCurrentTimeZoneInformation = 44,
            SystemTimeZoneInformation = 44,
            SystemLookasideInformation = 45,
            SystemSetTimeSlipEvent = 46,
            SystemCreateSession = 47,
            SystemDeleteSession = 48,
            SystemInvalidInfoClass4 = 49,
            SystemRangeStartInformation = 50,
            SystemVerifierInformation = 51,
            SystemAddVerifier = 52,
            SystemSessionProcessesInformation = 53,
            SystemInformationClassMax
        }
        #endregion

        #region Struct

        /// <summary>
        /// File times
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct FILETIME
        {
            /// <summary>
            /// LowDateTime
            /// </summary>
            public UInt32 dwLowDateTime;
            /// <summary>
            /// HighDateTime
            /// </summary>
            public UInt32 dwHighDateTime;
        };

        /// <summary>
        /// Win32 find data
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WIN32_FIND_DATAA
        {
            /// <summary>
            /// File attributes
            /// </summary>
            public Int32 dwFileAttributes;
            /// <summary>
            /// Creation time
            /// </summary>
            public FILETIME ftCreationTime;
            /// <summary>
            /// Last access time
            /// </summary>
            public FILETIME ftLastAccessTime;
            /// <summary>
            /// Last write time
            /// </summary>
            public FILETIME ftLastWriteTime;
            /// <summary>
            /// File size high
            /// </summary>
            public Int32 nFileSizeHigh;
            /// <summary>
            /// File size low
            /// </summary>
            public Int32 nFileSizeLow;
            /// <summary>
            /// Reserved0
            /// </summary>
            public Int32 dwReserved0;
            /// <summary>
            /// Reserved1
            /// </summary>
            public Int32 dwReserved1;

            /// <summary>
            /// File name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string cFileName;

            /// <summary>
            /// Alternate file name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ALTERNATE)]
            public string cAlternate;
        }

        /// <summary>
        /// Win32 find data
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WIN32_FIND_DATAW
        {
            /// <summary>
            /// File attributes
            /// </summary>
            public Int32 dwFileAttributes;
            /// <summary>
            /// Creation time
            /// </summary>
            public FILETIME ftCreationTime;
            /// <summary>
            /// Last access time
            /// </summary>
            public FILETIME ftLastAccessTime;
            /// <summary>
            /// Last write time
            /// </summary>
            public FILETIME ftLastWriteTime;
            /// <summary>
            /// File size high
            /// </summary>
            public Int32 nFileSizeHigh;
            /// <summary>
            /// File size low
            /// </summary>
            public Int32 nFileSizeLow;
            /// <summary>
            /// Reserved0
            /// </summary>
            public Int32 dwReserved0;
            /// <summary>
            /// Reserved1
            /// </summary>
            public Int32 dwReserved1;

            /// <summary>
            /// File name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string cFileName;

            /// <summary>
            /// Alternate file name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ALTERNATE)]
            public string cAlternateFileName;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct LUID
        {
            readonly Int32 LowPart;
            readonly Int32 HighPart;
        }

        /// <summary>
        /// LUID and attributes
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct LUID_AND_ATTRIBUTES
        {
            /// <summary>
            /// LUID
            /// </summary>
            public LUID pLuid;

            /// <summary>
            /// Attributes
            /// </summary>
            public Int32 Attributes;
        }

        /// <summary>
        /// Token privileges
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct TOKEN_PRIVILEGES
        {
            /// <summary>
            /// Privileges count
            /// </summary>
            public Int32 PrivilegesCount;

            /// <summary>
            /// Privileges
            /// </summary>
            public LUID_AND_ATTRIBUTES Privileges;
        }
        #endregion

        #region API
        // crypto
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean CryptAcquireContextW(ref IntPtr hProv, [MarshalAs(UnmanagedType.LPWStr)]string pszContainer, [MarshalAs(UnmanagedType.LPWStr)]string pszProvider, UInt32 dwProvType, UInt32 dwFlags);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean CryptGenRandom(IntPtr hProv, UInt32 dwLen, IntPtr pbBuffer);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean CryptReleaseContext(IntPtr hProv, UInt32 dwFlags);

        // file management
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern IntPtr CreateFileW(IntPtr lpFileName, Int32 dwDesiredAccess, Int32 dwShareMode, IntPtr lpSecurityAttributes,
        Int32 dwCreationDisposition, UInt32 dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean SetFilePointerEx(IntPtr hFile, long liDistanceToMove, [Out, Optional] IntPtr lpNewFilePointer, UInt32 dwMoveMethod);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern Int32 WriteFile(IntPtr hFile, IntPtr lpBuffer, UInt32 nNumberOfBytesToWrite, ref UInt32 lpNumberOfBytesWritten, IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern Int32 ReadFile(IntPtr hFile, IntPtr lpBuffer, UInt32 nNumberOfBytesToRead, ref UInt32 lpNumberOfBytesRead, IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern Int32 CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern Int32 FlushFileBuffers(IntPtr hFile);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern Int32 SetFileAttributesW([MarshalAs(UnmanagedType.LPWStr)]string lpFileName, Int32 dwFileAttributes);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern Int32 GetShortPathNameW([MarshalAs(UnmanagedType.LPWStr)]string lLongPath, [MarshalAs(UnmanagedType.LPWStr)]string lShortPath, Int32 lBuffer);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean GetFileSizeEx(IntPtr hFile, out UInt32 lpFileSize);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern Int32 MoveFileExW([MarshalAs(UnmanagedType.LPWStr)]string lpExistingFileName, [MarshalAs(UnmanagedType.LPWStr)]string lpNewFileName, Int32 dwFlags);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern Int32 DeleteFileW([MarshalAs(UnmanagedType.LPWStr)]string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern Int32 RemoveDirectoryW([MarshalAs(UnmanagedType.LPWStr)]string lpPathName);

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean DeviceIoControl(IntPtr hDevice, UInt32 dwIoControlCode, IntPtr lpInBuffer, UInt32 nInBufferSize,
        IntPtr lpOutBuffer, [Optional] UInt32 nOutBufferSize, out UInt32 lpBytesReturned, IntPtr lpOverlapped);

        // findfile
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern IntPtr FindFirstFileW([MarshalAs(UnmanagedType.LPWStr)]string lpFileName, out WIN32_FIND_DATAW lpFindFileData);

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean FindNextFileW(IntPtr hFindFile, out WIN32_FIND_DATAW lpFindFileData);

        /// <summary>
        /// Find close
        /// </summary>
        /// <param name="hFindFile"></param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean FindClose(IntPtr hFindFile);

        // memory allocation
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, UInt32 dwSize, UInt32 flAllocationType, UInt32 flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean VirtualFree(IntPtr lpAddress, UInt32 dwSize, UInt32 dwFreeType);

        [DllImport("kernel32.dll", SetLastError = false)]
        static extern void RtlZeroMemory(IntPtr dest, IntPtr size);

        // ntapi
        [DllImport("ntdll.dll", SetLastError = false)]
        static extern Int32 RtlFillMemory([In] IntPtr Destination, UInt32 length, byte fill);

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern void RtlZeroMemory(IntPtr Destination, uint length);

        [DllImport("ntdll.dll", SetLastError = false)]
        static extern UInt32 RtlCompareMemory(IntPtr Source1, IntPtr Source2, UInt32 length);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern Int32 RtlMoveMemory(ref byte Destination, ref byte Source, IntPtr Length);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern Int32 RtlMoveMemory(ref byte Destination, ref IntPtr Source, IntPtr Length);

        // process
        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean EnumProcesses([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] [In][Out] UInt32[] processIds,
          UInt32 arraySizeBytes, [MarshalAs(UnmanagedType.U4)] out UInt32 bytesCopied);

        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean EnumProcessModules(IntPtr hProcess, [MarshalAs(UnmanagedType.LPArray,
        ArraySubType = UnmanagedType.U4)] [In][Out] UInt32[] lphModule, UInt32 cb, [MarshalAs(UnmanagedType.U4)] out UInt32 lpcbNeeded);

        [DllImport("psapi.dll", SetLastError = true)]
        static extern UInt32 GetModuleFileNameExA(IntPtr hProcess, IntPtr hModule,
        [Out] StringBuilder lpBaseName, [In] [MarshalAs(UnmanagedType.U4)] UInt32 nSize);

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern UInt32 NtTerminateProcess(IntPtr ProcessHandle, UInt32 ExitStatus);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean GetExitCodeProcess(IntPtr hProcess, out UInt32 lpExitCode);

        [DllImport("kernel32.dll")]
        static extern uint WaitForMultipleObjects(uint nCount, IntPtr[] pHandles,
        Boolean bWaitAll, uint dwMilliseconds);

        // privilege - needed changes
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, [MarshalAs(UnmanagedType.Bool)]bool DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState,
        UInt32 BufferLength, IntPtr PreviousState, IntPtr ReturnLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern UInt32 GetCurrentProcessId();

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern Int32 OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, ref IntPtr TokenHandle);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern Int32 LookupPrivilegeValueW(Int32 lpSystemName, [MarshalAs(UnmanagedType.LPWStr)]string lpName, ref LUID lpLuid);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(Int32 dwDesiredAccess, Int32 blnheritHandle, UInt32 dwAppProcessId);
        #endregion

        #region Events
        /// <summary>
        /// Error delegate
        /// </summary>
        /// <param name="ErrType"></param>
        /// <param name="Err"></param>
        public delegate void ErrorDelegate(Int32 ErrType, String Err);

        /// <summary>
        /// Error event
        /// </summary>
        public event ErrorDelegate InError;

        /// <summary>
        /// File count delegate
        /// </summary>
        /// <param name="Count"></param>
        /// <param name="Cancel"></param>
        public delegate void FileCountDelegate(Int32 Count, ref Boolean Cancel);

        /// <summary>
        /// File count event
        /// </summary>
        public event FileCountDelegate FileCount;

        /// <summary>
        /// Status delegate
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="Message"></param>
        public delegate void StatusDelegate(Int32 Status, string Message);

        /// <summary>
        /// Status event
        /// </summary>
        public event StatusDelegate Status;
        #endregion

        #region Declarations
        Boolean bAnyAttribute = false;
        Boolean bCloseInstance = false;
        Boolean bDeleteDirectory = false;
        Boolean bDeleteFolders = false;
        Boolean bDeleteSubDirectories = false;
        Boolean bParanoid = false;
        string sFilePath = String.Empty;
        string[] tkRights = { SE_ASSIGNPRIMARYTOKEN_NAME, SE_BACKUP_NAME, SE_DEBUG_NAME, SE_INC_BASE_PRIORITY_NAME, SE_RESTORE_NAME, SE_SECURITY_NAME, SE_TCB_NAME };

        #endregion

        #region Properties
        /// <summary>
        /// Delete a file regardless of its attributes
        /// </summary>
        public Boolean AnyAttribute
        {
            get
            {
                return bAnyAttribute;
            }
            set
            {
                bAnyAttribute = value;
            }
        }

        /// <summary>
        /// Terminate any running instances of the file
        /// </summary>
        public Boolean CloseInstance
        {
            get
            {
                return bCloseInstance;
            }
            set
            {
                bCloseInstance = value;
            }
        }

        /// <summary>
        /// Delete all files within a folder
        /// </summary>
        public Boolean DeleteDirectory
        {
            get
            {
                return bDeleteDirectory;
            }
            set
            {
                bDeleteDirectory = value;
            }
        }

        /// <summary>
        /// Delete files and their parent folder
        /// </summary>
        public Boolean DeleteFolders
        {
            get
            {
                return bDeleteFolders;
            }
            set
            {
                bDeleteFolders = value;
            }
        }

        /// <summary>
        /// Delete files within subdirectories
        /// </summary>
        public Boolean DeleteSubDirectories
        {
            get
            {
                return bDeleteSubDirectories;
            }
            set
            {
                bDeleteSubDirectories = value;
            }
        }

        /// <summary>
        /// File name or start path
        /// </summary>
        public string FilePath
        {
            get
            {
                return sFilePath;
            }
            set
            {
                sFilePath = value;
            }
        }

        /// <summary>
        /// Enable paranoid mode
        /// </summary>
        public Boolean ParanoidMode
        {
            get
            {
                return bParanoid;
            }
            set
            {
                bParanoid = value;
            }
        }
        #endregion

        #region Core Methods
        /// <summary>
        /// Entry point - starts the shredder
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean startShredder()
        {
            // reset collections
            new ArrayList();
            new ArrayList();

            try
            {
                //add rights back to token
                adjustToken(true, tkRights);
                if (DeleteDirectory)
                {
                    if (FilePath.Length < 4)
                    {
                        // can't shred the drive
                        if (InError != null)
                            InError(001, rm.GetString("shredding_drive"));
                        return false;
                    }
                    if (!FilePath.EndsWith(@"\"))
                        FilePath += @"\";

                    removeDirectory(FilePath);

                    return true;
                }
                else
                {
                    if (!fileExists(UNICODE_PREFIX + FilePath))
                    {
                        if (InError != null)
                            InError(003, rm.GetString("file_path_invalid") + "!");
                    }
                    if (CloseInstance)
                        closeProcess(FilePath);
                    // relay processing status
                    if (shredFile(UNICODE_PREFIX + FilePath))
                    {
                        return true;
                    }
                    else
                    {
                        adjustToken(false, tkRights);
                        return false;
                    }
                }
            }
            finally
            {
                // return token to normal
                adjustToken(false, tkRights);
                FilePath = String.Empty;
            }
        }

        /// <summary>
        /// Primary worker
        /// </summary>
        /// <param name="sPath">string - file name</param>
        /// <returns>Boolean</returns>
        Boolean shredFile(string sPath)
        {
            IntPtr hFile = IntPtr.Zero;
            IntPtr pBuffer = IntPtr.Zero;
            UInt32 nFileLen = 0;
            UInt32 dwSize = BUFFER_SIZE;
            byte bR = 0;

            try
            {                
                IntPtr pName = Marshal.StringToHGlobalAuto(sPath);            

                // reset attributes
                if (AnyAttribute)
                    stripAttributes(sPath);
                hFile = CreateFileW(pName, GENERIC_ALL, FILE_SHARE_NONE, IntPtr.Zero, OPEN_EXISTING, WRITE_THROUGH, IntPtr.Zero);
                nFileLen = fileSize(hFile);
                if (nFileLen > BUFFER_SIZE)
                    nFileLen = BUFFER_SIZE;
                if (hFile.ToInt32() == -1)
                    return false;
                // set the table
                SetFilePointerEx(hFile, 0, IntPtr.Zero, FILE_BEGIN);
                pBuffer = VirtualAlloc(IntPtr.Zero, ((nFileLen == 0) ? 1 : nFileLen), MEM_COMMIT, PAGE_READWRITE);
                if (pBuffer == IntPtr.Zero)
                    return false;
                // first pass all zeros
                RtlZeroMemory(pBuffer, nFileLen);
                if (overwriteFile(hFile, pBuffer) != true)
                    return false;
                if (writeVerify(hFile, pBuffer, nFileLen) != true)
                    return false;

                bR = 0xFF;
                RtlFillMemory(pBuffer, nFileLen, bR);
                if (overwriteFile(hFile, pBuffer) != true)
                    return false;
                if (writeVerify(hFile, pBuffer, nFileLen) != true)
                    return false;

                randomData(pBuffer, nFileLen);
                if (overwriteFile(hFile, pBuffer) != true)
                    return false;
                if (writeVerify(hFile, pBuffer, nFileLen) != true)
                    return false;

                RtlZeroMemory(pBuffer, nFileLen);
                overwriteFile(hFile, pBuffer);
                bR = 0;
                if (writeVerify(hFile, pBuffer, nFileLen) != true)
                    return false;

                if (CloseHandle(hFile) != 0)
                    hFile = IntPtr.Zero;
                // reduce to zero bytes
                if (zeroFile(pName) != true)
                    if (InError != null)
                        InError(005, rm.GetString("emptying_content_failed") + ".");
                // paranoid mode
                if (ParanoidMode)
                    orphanFile(pName);
                // rename the file
                if (renameFile(sPath) != true)
                    if (InError != null)
                        InError(006, rm.GetString("file_not_renamed") + ".");

                return true;
            }

            finally
            {
                if (hFile != IntPtr.Zero)
                    CloseHandle(hFile);
                if (pBuffer != IntPtr.Zero)
                    VirtualFree(pBuffer, dwSize, MEM_RELEASE);
            }
        }

        /// <summary>
        /// Overwrite the file
        /// </summary>
        /// <param name="hFile">IntPtr - file handle</param>
        /// <param name="pBuffer">IntPtr - buffer address</param>
        /// <returns>Boolean</returns>
        Boolean overwriteFile(IntPtr hFile, IntPtr pBuffer)
        {
            UInt32 nFileLen = fileSize(hFile);
            UInt32 dwSeek = 0;
            UInt32 btWritten = 0;

            try
            {
                if (nFileLen < BUFFER_SIZE)
                {
                    SetFilePointerEx(hFile, dwSeek, IntPtr.Zero, FILE_BEGIN);
                    WriteFile(hFile, pBuffer, nFileLen, ref btWritten, IntPtr.Zero);
                }
                else
                {
                    do
                    {
                        SetFilePointerEx(hFile, dwSeek, IntPtr.Zero, FILE_BEGIN);
                        WriteFile(hFile, pBuffer, BUFFER_SIZE, ref btWritten, IntPtr.Zero);
                        dwSeek += btWritten;
                        Application.DoEvents();
                    } while ((nFileLen - dwSeek) > BUFFER_SIZE);
                    WriteFile(hFile, pBuffer, (nFileLen - dwSeek), ref btWritten, IntPtr.Zero);
                }
                // reset file pointer
                SetFilePointerEx(hFile, 0, IntPtr.Zero, FILE_BEGIN);
                // add it up
                if ((btWritten + dwSeek) == nFileLen)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verify that the file has been overwritten
        /// </summary>
        /// <param name="hFile">IntPtr - file handle</param>
        /// <param name="pCompare">IntPtr - buffer address</param>
        /// <param name="pSize">UIntPtr - buffer size</param>
        /// <returns>Boolean</returns>
        Boolean writeVerify(IntPtr hFile, IntPtr pCompare, UInt32 pSize)
        {
            IntPtr pBuffer = IntPtr.Zero;
            UInt32 iRead = 0;

            try
            {
                pBuffer = VirtualAlloc(IntPtr.Zero, pSize, MEM_COMMIT, PAGE_READWRITE);
                SetFilePointerEx(hFile, 0, IntPtr.Zero, FILE_BEGIN);
                if (ReadFile(hFile, pBuffer, pSize, ref iRead, IntPtr.Zero) == 0)
                {
                    if (InError != null)
                        InError(004, rm.GetString("failed_verification") + ".");
                    return false; // bad read
                }
                if (RtlCompareMemory(pCompare, pBuffer, pSize) == pSize)
                    return true; // equal
                return false;
            }
            finally
            {
                if (pBuffer != IntPtr.Zero)
                    VirtualFree(pBuffer, pSize, MEM_RELEASE);
            }
        }
        #endregion

        #region Process
        void closeProcess(string sTarget)
        {
            UInt32 arraySize = 96;
            UInt32 arrayBytesSize = arraySize * sizeof(UInt32);
            UInt32[] processIds = new UInt32[arraySize];
            UInt32[] processMods = new UInt32[1024];
            UInt32 bytesCopied = 0;
            UInt32 dwSize = 0;
            Boolean success = false;
            UInt32 ret = 0;
            IntPtr hProcess = IntPtr.Zero;
            System.Text.StringBuilder pvName = new System.Text.StringBuilder(260);

            do
            {
                arrayBytesSize *= 2;
                processIds = new UInt32[arrayBytesSize / 4];
                success = EnumProcesses(processIds, arrayBytesSize, out bytesCopied);
            } while (arrayBytesSize <= bytesCopied);

            if (!success)
                return;

            UInt32 numIdsCopied = bytesCopied >> 2; ;
            for (UInt32 index = 0; index < numIdsCopied; index++)
            {
                try
                {
                    if (processIds[index] != 0)
                    {
                        hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, 0, processIds[index]);
                        if (hProcess != IntPtr.Zero)
                        {
                            if (EnumProcessModules(hProcess, processMods, 1024, out dwSize))
                                success = (EnumProcessModules(hProcess, processMods, dwSize, out dwSize));
                            if (success)
                                ret = GetModuleFileNameExA(hProcess, (IntPtr)processMods[0], pvName, dwSize);
                            if (pvName.ToString() == sTarget)
                            {
                                killProcess(processIds[index]);
                                CloseHandle(hProcess);
                                return;
                            }
                            CloseHandle(hProcess);
                        }
                    }
                }
                catch
                {
                    if (hProcess != IntPtr.Zero)
                        CloseHandle(hProcess);
                }
            }
            return;
        }

        /// <summary>
        /// Terminate process
        /// </summary>
        /// <param name="ProcessId">uint - process id</param>
        /// <returns>Boolean</returns>
        Boolean killProcess(UInt32 ProcessId)
        {
            UInt32 lpExitCode = 0;
            UInt32 ret = 0;
            UInt32 usafe = 0;
            IntPtr[] hProcess = new IntPtr[1];
            hProcess[0] = IntPtr.Zero;

            hProcess[0] = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_TERMINATE | SYNCHRONIZE, 0, ProcessId);
            if (hProcess[0] == IntPtr.Zero)
                return false;
            // ask nice
            GetExitCodeProcess(hProcess[0], out lpExitCode);
            ret = (NtTerminateProcess(hProcess[0], lpExitCode));
            // wait for it
            do
            {
                ret = (WaitForMultipleObjects(1, hProcess, false, 100));
                usafe++;
            }
            while ((ret != (WAIT_OBJECT_0)) && (usafe < 100));
            return true;
        }
        #endregion

        #region Security
        /// <summary>
        /// Change process security token access rights
        /// </summary>
        /// <param name="Enable">Boolean - Ebnable or disable a privilege</param>
        /// <returns>Boolean</returns>
        Boolean adjustToken(Boolean Enable, string[] rights)
        {
            IntPtr hToken = IntPtr.Zero;
            IntPtr hProcess = IntPtr.Zero;
            LUID tLuid = new LUID();
            TOKEN_PRIVILEGES NewState = new TOKEN_PRIVILEGES();
            UInt32 uPriv = (UInt32)(ETOKEN_PRIVILEGES.TOKEN_ADJUST_PRIVILEGES | ETOKEN_PRIVILEGES.TOKEN_QUERY | ETOKEN_PRIVILEGES.TOKEN_QUERY_SOURCE);

            try
            {
                hProcess = OpenProcess(PROCESS_ALL_ACCESS, 0, GetCurrentProcessId());
                if (hProcess == IntPtr.Zero)
                    return false;
                if (OpenProcessToken(hProcess, uPriv, ref hToken) == 0)
                    return false;
                for (Int32 i = 0; i < rights.Length; i++)
                {
                    // Get the local unique id for the privilege.
                    if (LookupPrivilegeValueW(0, rights[i], ref tLuid) == 0)
                        return false;
                }
                // Assign values to the TOKEN_PRIVILEGE structure.
                NewState.PrivilegesCount = 1;
                NewState.Privileges.pLuid = tLuid;
                NewState.Privileges.Attributes = (Enable ? SE_PRIVILEGE_ENABLED : 0);
                // Adjust the token privilege
                return (AdjustTokenPrivileges(hToken, false, ref NewState, (uint)Marshal.SizeOf(NewState), IntPtr.Zero, IntPtr.Zero));
            }
            finally
            {
                if (hToken != IntPtr.Zero)
                    CloseHandle(hToken);
                if (hProcess != IntPtr.Zero)
                    CloseHandle(hProcess);
            }
        }

        /// <summary>
        /// Checks if the user is admin
        /// </summary>
        /// <returns></returns>
        public Boolean IsAdmin()
        {
            IntPtr hToken = IntPtr.Zero;
            IntPtr hProcess = IntPtr.Zero;
            LUID tLuid = new LUID();
            UInt32 uPriv = (UInt32)(ETOKEN_PRIVILEGES.TOKEN_ADJUST_PRIVILEGES | ETOKEN_PRIVILEGES.TOKEN_QUERY | ETOKEN_PRIVILEGES.TOKEN_QUERY_SOURCE);

            try
            {
                hProcess = OpenProcess(PROCESS_ALL_ACCESS, 0, GetCurrentProcessId());
                if (hProcess == IntPtr.Zero)
                    return false;
                if (OpenProcessToken(hProcess, uPriv, ref hToken) == 0)
                    return false;
                return (LookupPrivilegeValueW(0, SE_TCB_NAME, ref tLuid) != 0);
            }
            finally
            {
                if (hToken != IntPtr.Zero)
                    CloseHandle(hToken);
                if (hProcess != IntPtr.Zero)
                    CloseHandle(hProcess);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Delete the directory
        /// </summary>
        /// <param name="sDir">unicode string - directory name</param>
        /// <returns>Boolean</returns>
        Boolean removeDirectory(string sDir)
        {
            if (RemoveDirectoryW(sDir) == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="hFile">unicode string - file handle</param>
        /// <returns>Boolean</returns>
        Boolean deleteFile(string FileName)
        {
            if (DeleteFileW(FileName) == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Destroy file when computer restarts
        /// </summary>
        /// <param name="sPath">void</param>
        Boolean destroyOnRestart(string sPath)
        {
            string sSource = shortPath(sPath);
            if (sSource.Length == 0)
                sSource = sPath;
            if (MoveFileExW(sSource, String.Empty, MOVEFILE_DELAY_UNTIL_REBOOT) == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Test file path
        /// </summary>
        /// <param name="sPath">string - unicode file path</param>
        /// <returns>Boolean</returns>
        Boolean fileExists(string sPath)
        {
            WIN32_FIND_DATAW WFD;
            IntPtr hFile = IntPtr.Zero;

            try
            {
                hFile = FindFirstFileW(sPath, out WFD);
                if (hFile.ToInt32() == -1)
                    return false;
                return true;
            }
            finally
            {
                if (hFile != IntPtr.Zero)
                    FindClose(hFile);
            }
        }

        /// <summary>
        /// Get the file size in bytes
        /// </summary>
        /// <param name="hFile">IntPtr - file handle</param>
        /// <returns>Int32 - file length</returns>
        UInt32 fileSize(IntPtr hFile)
        {
            UInt32 nFileLen = 0;
            GetFileSizeEx(hFile, out nFileLen);
            return nFileLen;
        }

        /// <summary>
        /// Fill a buffer with random data
        /// </summary>
        /// <param name="pBuffer">IntPtr - buffer address</param>
        /// <param name="nSize">UInt32 - length of buffer</param>
        /// <returns>Boolean</returns>
        Boolean randomData(IntPtr pBuffer, UInt32 nSize)
        {
            IntPtr iProv = IntPtr.Zero;

            try
            {
                // acquire context
                if (CryptAcquireContextW(ref iProv, "", MS_ENHANCED_PROV, PROV_RSA_FULL, CRYPT_VERIFYCONTEXT) != true)
                    return false;
                // generate random block
                if (CryptGenRandom(iProv, nSize, pBuffer) != true)
                    return false;
                return true;
            }
            finally
            {
                // release crypto engine
                if (iProv != IntPtr.Zero)
                    CryptReleaseContext(iProv, 0);
            }
        }

        /// <summary>
        /// Rename a file 30 times
        /// </summary>
        /// <param name="sPath">string - file name</param>
        /// <returns>Boolean</returns>
        Boolean renameFile(string sPath)
        {
            string sNewName = String.Empty;
            string sPartial = sPath.Substring(0, sPath.LastIndexOf(@"\") + 1);
            Int32 nLen = 10;
            char[] cName = new char[nLen];
            for (Int32 i = 0; i < 30; i++)
            {
                for (Int32 j = 97; j < 123; j++)
                {
                    for (Int32 k = 0; k < nLen; k++)
                    {
                        if (k == (nLen - 4))
                            sNewName += ".";
                        else
                            sNewName += (char)j;
                    }
                    if (MoveFileExW(sPath, sPartial + sNewName, MOVEFILE_REPLACE_EXISTING | MOVEFILE_WRITE_THROUGH) != 0)
                        sPath = sPartial + sNewName;
                    sNewName = String.Empty;
                }
            }
            // last step: delete the file
            if (deleteFile(sPath) != true)
                return false;
            return true;
        }

        /// <summary>
        /// retrieve short path name
        /// </summary>
        /// <param name="sPath">string - path</param>
        /// <returns>string</returns>
        string shortPath(string sPath)
        {
            Int32 iLen;
            string sBuffer = String.Empty;
            sBuffer.PadRight(255, char.MinValue);
            iLen = GetShortPathNameW(sPath, sBuffer, 255);
            return sBuffer.Trim();
        }

        /// <summary>
        /// Reset a files attributes
        /// </summary>
        /// <param name="sPath">void</param>
        void stripAttributes(string sPath)
        {
            SetFileAttributesW(sPath, FILE_ATTRIBUTE_NORMAL);
        }

        /// <summary>
        /// Delete the files object Id
        /// </summary>
        /// <param name="sPath">IntPtr - unicode file path</param>
        /// <returns>Boolean</returns>
        Boolean orphanFile(IntPtr pName)
        {
            UInt32 lpBytesReturned = 0;
            IntPtr hFile = CreateFileW(pName, GENERIC_WRITE, FILE_SHARE_NONE, IntPtr.Zero, OPEN_EXISTING, WRITE_THROUGH, IntPtr.Zero);
            if (DeviceIoControl(hFile, FsctlDeleteObjectId, IntPtr.Zero, 0, IntPtr.Zero, 0, out lpBytesReturned, IntPtr.Zero))
                return false;
            return true;
        }

        /// <summary>
        /// Reduce file to zero bytes and flush buffer
        /// </summary>
        /// <param name="hFile">file handle</param>
        /// <returns>Boolean</returns>
        Boolean zeroFile(IntPtr pName)
        {
            for (Int32 i = 0; i < 10; i++)
            {
                IntPtr hFile = CreateFileW(pName, GENERIC_ALL, FILE_SHARE_NONE, IntPtr.Zero, OPEN_EXISTING, WRITE_THROUGH, IntPtr.Zero);
                if (hFile == IntPtr.Zero)
                    return false;
                SetFilePointerEx(hFile, 0, IntPtr.Zero, FILE_BEGIN);
                // unnecessary but..
                FlushFileBuffers(hFile);
                CloseHandle(hFile);
            }
            return true;
        }
        #endregion
    }
}