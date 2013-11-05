using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Management;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace SystemInformation
{
    /// <summary>
    /// Native methods
    /// </summary>
    public class NativeMethods
    {
        /// <summary>
        /// Resource manager
        /// </summary>
        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
                                                        Assembly.GetExecutingAssembly());

        #region " Public constructor "

        #endregion

        #region " Get Volume Serial Number "

        #region " API Declarations and Constants "

        [DllImport("kernel32", EntryPoint = "GetVolumeInformationA", ExactSpelling = true, CharSet = CharSet.Ansi,
            SetLastError = true)]
        static extern int GetVolumeInformation(string lpRootPathName, string lpVolumeNameBuffer,
                                               int nVolumeNameSize, ref int lpVolumeSerialNumber,
                                               ref int lpMaximumComponentLength,
                                               ref int lpFileSystemFlags, string lpFileSystemNameBuffer,
                                               int nFileSystemNameSize);

        #endregion

        /// <summary>
        /// Returns hexadecimal volume serial number.
        /// </summary>
        /// <param name="volume">volume "drive letter"</param>
        public static string GetVolumeSerialNumber(string volume)
        {
            string result = string.Empty;
            int volumeSerialNumber = 0;
            int componentLength = 0;
            int flags = 0;
            const string unused = "                                ";
            const string volumeName = "              ";

            int check = GetVolumeInformation(volume, volumeName, volumeName.Length, ref volumeSerialNumber,
                                             ref componentLength, ref flags, unused, unused.Length);

            // Error check.
            if (check != 0)
            {
                result = String.Format("{0:x4}", volumeSerialNumber);
            }
            return result;
        }

        #endregion

        #region " Get Memory Information "

        #region " API Declaration and Structure "

        // API declarations
        [DllImport("kernel32", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GlobalMemoryStatusEx([MarshalAs(UnmanagedType.Struct)] ref MEMORYSTATUSEX lpBuffer);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct MEMORYSTATUSEX
        {
            internal int dwLength;
            internal int dwMemoryLoad;
            internal ulong ullTotalPhys;
            internal ulong ullAvailPhys;
            internal ulong ullTotalPageFile;
            internal ulong ullAvailPageFile;
            internal ulong ullTotalVirtual;
            internal ulong ullAvailVirtual;
            internal ulong ullAvailExtendedVirtual;
        }

        #endregion

        // object declarations
        static MEMORYSTATUSEX memoryStatus;

        /// <summary>
        /// Gets available virtual RAM
        /// </summary>
        /// <returns></returns>
        [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        public static string GetMemAvailVirtual()
        {
            // Call API
            memoryStatus.dwLength = Marshal.SizeOf(memoryStatus);
            GlobalMemoryStatusEx(ref memoryStatus);

            // return formatted string
            return FormatBytes(Convert.ToDouble(memoryStatus.ullAvailVirtual));
        }

        /// <summary>
        /// Gets available physical RAM
        /// </summary>
        /// <returns></returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static string GetMemAvailPhysical()
        {
            // Call API
            memoryStatus.dwLength = Marshal.SizeOf(memoryStatus);
            GlobalMemoryStatusEx(ref memoryStatus);

            // return formatted string
            return FormatBytes(Convert.ToDouble(memoryStatus.ullAvailPhys));
        }

        /// <summary>
        /// Gets total RAM
        /// </summary>
        /// <returns></returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static string GetMemTotalVirtual()
        {
            // Call API
            memoryStatus.dwLength = Marshal.SizeOf(memoryStatus);
            GlobalMemoryStatusEx(ref memoryStatus);

            // return formatted string
            return FormatBytes(Convert.ToDouble(memoryStatus.ullTotalVirtual));
        }

        /// <summary>
        /// Gets total physical RAM
        /// </summary>
        /// <returns></returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static string GetMemTotalPhysical()
        {
            // Call API
            memoryStatus.dwLength = Marshal.SizeOf(memoryStatus);
            GlobalMemoryStatusEx(ref memoryStatus);

            // return formatted string
            return FormatBytes(Convert.ToDouble(memoryStatus.ullTotalPhys));
        }

        #endregion

        #region " Get User Information "

        #region " API Declaration and Structures "

        //  Bit masks for field usriX_flags of USER_INFO_X (X = 0/1).
        const int UF_ACCOUNTDISABLE = 0x0002;
        const int UF_LOCKOUT = 0x0010;
        const int UF_PASSWD_NOTREQD = 0x0020;
        const int UF_PASSWD_CANT_CHANGE = 0x0040;
        const int UF_DONT_EXPIRE_PASSWD = 0x10000;

        // USER_INF0_2 - Structure to hold advanced user information.

        // NetUserGetInfo - Returns to a struct Information about the specified user
        [DllImport("Netapi32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        static extern int NetUserGetInfo([MarshalAs(UnmanagedType.LPWStr)] string servername,
                                         [MarshalAs(UnmanagedType.LPWStr)] string username, int level, out IntPtr bufptr);

        // NetUserEnum - Obtains a list of all users on local machine or network
        [DllImport("Netapi32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        static extern int NetUserEnum(string servername, int level, int filter, out IntPtr bufptr,
                                      int prefmaxlen, out int entriesread, out int totalentries, out int resume_handle);

        // NetAPIBufferFree - Used to clear the Network buffer after NetUserEnum
        [DllImport("Netapi32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        static extern int NetApiBufferFree(IntPtr Buffer);

        #region Nested type: USER_INFO_0

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct USER_INFO_0
        {
            internal string Username;
        }

        #endregion

        #region Nested type: USER_INFO_2

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct USER_INFO_2
        {
            //internal string usri2_name;
            //internal string usri2_password;
            internal int usri2_password_age;
            internal int usri2_priv;
            //internal string usri2_home_dir;
            //internal string usri2_comment;
            internal int usri2_flags;
            //internal string usri2_script_path;
            internal int usri2_auth_flags;
            //internal string usri2_full_name;
            //internal string usri2_usr_comment;
            //internal string usri2_parms;
            //internal string usri2_workstations;
            internal int usri2_last_logon;
            internal int usri2_last_logoff;
            internal int usri2_acct_expires;
            internal int usri2_max_storage;
            internal int usri2_units_per_week;
            internal byte usri2_logon_hours;
            internal int usri2_bad_pw_count;
            internal int usri2_num_logons;
            //internal string usri2_logon_server;
            internal int usri2_country_code;
            internal int usri2_code_page;
        }

        #endregion

        #endregion

        /// <summary>
        /// Users collection
        /// </summary>
        /// <returns></returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static Collection<string> EnumerateUsers()
        {
            int entriesRead;
            int totalEntries;
            int resume;
            IntPtr bufPtr;
            var userName = new Collection<string>();

            NetUserEnum(null, 0, 2, out bufPtr, -1, out entriesRead, out totalEntries, out resume);

            if (entriesRead > 0)
            {
                var users = new USER_INFO_0[entriesRead];
                IntPtr iter = bufPtr;
                for (int i = 0; i < entriesRead; i++)
                {
                    users[i] = (USER_INFO_0)Marshal.PtrToStructure(iter, typeof(USER_INFO_0));
                    iter = (IntPtr)((int)iter + Marshal.SizeOf(typeof(USER_INFO_0)));
                    userName.Add(users[i].Username);
                }
                NetApiBufferFree(bufPtr);
            }
            return userName;
        }

        /// <summary>
        /// Gets user priviledges
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static string GetUserPrivledge(string userName)
        {
            string retValue;
            IntPtr bufPtr;
            new USER_INFO_2();

            NetUserGetInfo(null, userName, 2, out bufPtr);

            switch (userName)
            {
                case "Guest":
                    retValue = "Guest";
                    break;

                case "Administrator":
                    retValue = "Administrator";
                    break;
                default:
                    retValue = "Limited User";
                    break;
            }

            return retValue;
        }

        /// <summary>
        /// Gets user full name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static string GetUserFullName(string userName)
        {
            IntPtr bufPtr;
            new USER_INFO_2();
            NetUserGetInfo(null, userName, 2, out bufPtr);
            return GetUsers(userName);
        }

        /// <summary>
        /// Gets user flags
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static int GetUserFlags(string userName)
        {
            IntPtr bufPtr;

            NetUserGetInfo(null, userName, 2, out bufPtr);
            USER_INFO_2 currentUser = (USER_INFO_2)Marshal.PtrToStructure(bufPtr, typeof(USER_INFO_2));

            return currentUser.usri2_flags;
        }

        /// <summary>
        /// Gets users
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string GetUsers(string userName)
        {
            var sQuery = new SelectQuery("Win32_UserAccount", "Domain='" + Environment.MachineName + "'");
            string struserName = "";
            try
            {
                var mSearcher = new ManagementObjectSearcher(sQuery);

                foreach (ManagementObject mObject in mSearcher.Get())
                {
                    if (userName == mObject["Name"].ToString())
                    {
                        struserName = mObject["FullName"].ToString();
                        break;
                    }
                }
                return struserName;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Gets user objects
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static ManagementObject GetUserObject(string userName)
        {
            var sQuery = new SelectQuery("Win32_UserAccount", "Domain='" + Environment.MachineName + "'");
            try
            {
                var mSearcher = new ManagementObjectSearcher(sQuery);
                var userObject = new ManagementObject();
                foreach (ManagementObject mObject in mSearcher.Get())
                {
                    if (userName == mObject["Name"].ToString())
                    {
                        userObject = mObject;
                        break;
                    }
                }
                return userObject;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region " Extract Icon "

        #region " API Declarations "

        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr ExtractIcon
            (IntPtr hInst, string lpszExeFileName, Int32 nIconIndex);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean DestroyIcon(IntPtr hIcon);

        #endregion

        /// <summary>
        /// Return first icon for an executible file.
        /// </summary>
        /// <remarks>
        /// This overload does not require an icon index.
        /// </remarks>
        public Icon GetIcon(string filePath)
        {
            try
            {
                // Extract icon 0 from filePath.
                const int iconIndex = 0;

                IntPtr hInst = Marshal.GetHINSTANCE(GetType().Module);
                IntPtr iconHandle = ExtractIcon(hInst, filePath, iconIndex);

                // Return a cloned Icon, because we have to free the original ourselves.
                Icon ico = Icon.FromHandle(iconHandle);
                Icon clone = (Icon)ico.Clone();
                ico.Dispose();
                DestroyIcon(iconHandle);
                return clone;
            }
            catch
            {
                // Silently fail and return a null.
                return null;
            }
        }

        /// <summary>
        /// Return first icon for an executible file.
        /// </summary>
        /// <remarks>
        /// This overload requires an icon index.
        /// </remarks>
        public Icon GetIcon(string filePath, int iconIndex)
        {
            try
            {
                IntPtr hInst = Marshal.GetHINSTANCE(GetType().Module);
                IntPtr iconHandle = ExtractIcon(hInst, filePath, iconIndex);

                // Return a cloned Icon, because we have to free the original ourselves.
                var ico = Icon.FromHandle(iconHandle);
                var clone = (Icon)ico.Clone();
                ico.Dispose();
                DestroyIcon(iconHandle);
                return clone;
            }
            catch
            {
                // Silently fail and return a null.
                return null;
            }
        }

        #endregion

        #region " Formatting Subroutines "

        /// <summary>
        /// Format bytes presentation
        /// </summary>
        /// <param name="bytes">bytes count</param>
        /// <returns></returns>
        static string FormatBytes(double bytes)
        {
            double temp;

            if (bytes >= 1073741824)
            {
                temp = bytes / 1073741824; // GB
                return String.Format("{0:N2}", temp) + " GB";
            }
            if (bytes >= 1048576 && bytes <= 1073741823)
            {
                temp = bytes / 1048576; //MB
                return String.Format("{0:N0}", temp) + " MB";
            }
            if (bytes >= 1024 && bytes <= 1048575)
            {
                temp = bytes / 1024; // KB
                return String.Format("{0:N0}", temp) + " KB";
            }
            if (bytes == 0 && bytes <= 1023)
            {
                temp = bytes; // bytes
                return String.Format("{0:N0}", temp) + " bytes";
            }
            return string.Empty;
        }

        #endregion
    }
}