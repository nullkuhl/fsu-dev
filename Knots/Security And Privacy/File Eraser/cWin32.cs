using System;
using System.Runtime.InteropServices;

namespace FileEraser
{
    /// <summary>
    /// cWin32
    /// </summary>
    public class cWin32
    {
        const int VER_PLATFORM_WIN32_NT = 0x2;

        /// <summary>
        /// Shell execute
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpOperation"></param>
        /// <param name="lpFile"></param>
        /// <param name="lpParameters"></param>
        /// <param name="lpDirectory"></param>
        /// <param name="nShowCmd"></param>
        /// <returns></returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters,
                                              string lpDirectory, int nShowCmd);


        [DllImport("kernel32", EntryPoint = "GetVersionEx")]
        static extern bool GetVersionEx(ref OSVERSIONINFO osvi);

        /// <summary>
        /// Tests minimun OS required
        /// </summary>
        /// <returns>bool</returns>
        public bool VersionCheck()
        {
            OSVERSIONINFO tVer = new OSVERSIONINFO();
            tVer.dwVersionInfoSize = Marshal.SizeOf(tVer);
            GetVersionEx(ref tVer);
            if ((tVer.dwPlatformId & VER_PLATFORM_WIN32_NT) == VER_PLATFORM_WIN32_NT)
            {
                return true;
            }
            return false;
        }

        #region Nested type: OSVERSIONINFO

        [StructLayout(LayoutKind.Sequential)]
        struct OSVERSIONINFO
        {
            internal int dwVersionInfoSize;
            readonly int dwMajorVersion;
            readonly int dwMinorVersion;
            readonly int dwBuildNumber;
            internal readonly int dwPlatformId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 127)]
            readonly byte[] szCSDVersion;
        }

        #endregion
    }
}