using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace RegistryCompactor
{
    /// <summary>
    /// Permissions
    /// </summary>
    public class Permissions
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AdjustTokenPrivileges(IntPtr tokenHandle,
           [MarshalAs(UnmanagedType.Bool)] bool disableAllPrivileges,
           ref TokPriv1Luid newState,
           UInt32 zero,
           IntPtr null1,
           IntPtr null2);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool CloseHandle(IntPtr handle);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            /// <summary>
            /// Count
            /// </summary>
            public int Count;

            /// <summary>
            /// LUID
            /// </summary>
            public long Luid;

            /// <summary>
            /// Attr
            /// </summary>
            public int Attr;
        }

        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int SE_PRIVILEGE_REMOVED = 0x00000004;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;

        /// <summary>
        /// Sets privileges
        /// </summary>
        /// <param name="enabled"></param>
        public static void SetPrivileges(bool enabled)
        {
            SetPrivilege("SeShutdownPrivilege", enabled);
            SetPrivilege("SeBackupPrivilege", enabled);
            SetPrivilege("SeRestorePrivilege", enabled);
            SetPrivilege("SeDebugPrivilege", enabled);
        }

        /// <summary>
        /// Sets privilege
        /// </summary>
        public static bool SetPrivilege(string privilege, bool enabled)
        {
            bool result = false;
            try
            {
                var tp = new TokPriv1Luid();
                IntPtr hproc = Process.GetCurrentProcess().Handle;
                IntPtr htok = IntPtr.Zero;

                if (OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok))
                {
                    tp.Count = 1;
                    tp.Luid = 0;
                    tp.Attr = ((enabled) ? (SE_PRIVILEGE_ENABLED) : (SE_PRIVILEGE_REMOVED));

                    if (!LookupPrivilegeValue(null, privilege, ref tp.Luid))
                        return false;

                    result = (AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero));

                    // Cleanup
                    CloseHandle(htok);
                }
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// Checks if the user is an admin
        /// </summary>
        /// <returns>True if it is in admin group</returns>
        public static bool IsUserAdministrator
        {
            get
            {
                //bool value to hold our return value
                bool isAdmin = false;
                try
                {
                    //get the currently logged in user
                    WindowsIdentity user = WindowsIdentity.GetCurrent();
                    if (user != null)
                    {
                        var principal = new WindowsPrincipal(user);
                        isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                }
                catch (Exception)
                {
                }
                return isAdmin;
            }
        }
    }
}
