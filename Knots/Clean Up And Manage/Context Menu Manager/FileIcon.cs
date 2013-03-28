using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Drawing;
namespace Context_Menu_Manager
{
    public static class FileIcon
    {
        [DllImport("shell32.dll")]
        static extern IntPtr ExtractAssociatedIcon(IntPtr hinst, string file,
        ref int index);

        /// <summary>
        /// Gets file info
        /// </summary>
        /// <param name="pszPath"></param>
        /// <param name="dwFileAttributes"></param>
        /// <param name="psfi"></param>
        /// <param name="cbSizeFileInfo"></param>
        /// <param name="uFlags"></param>
        /// <returns>File info</returns>
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi,
                                                  uint cbSizeFileInfo, uint uFlags);

        /// <summary>
        /// Icon
        /// </summary>
        public const uint SHGFI_ICON = 0x100;
        /// <summary>
        /// Large icon
        /// </summary>
        public const uint SHGFI_LARGEICON = 0x0;
        /// <summary>
        /// Small icon
        /// </summary>
        public const uint SHGFI_SMALLICON = 0x1;
        /// <summary>
        /// Small icon
        /// </summary>
        public const uint SHGFI_SYSICONINDEX = 0x4000;


        #region Nested type: SHFILEINFO

        /// <summary>
        /// File info
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            /// <summary>
            /// Icon
            /// </summary>
            public IntPtr hIcon;
            /// <summary>
            /// Icon
            /// </summary>
            public IntPtr iIcon;
            /// <summary>
            /// Attributes
            /// </summary>
            public uint dwAttributes;

            /// <summary>
            /// Display name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;

            /// <summary>
            /// Type name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        #endregion

        /// <summary>
        /// Gets small icon for the specified <paramref name="fileName"/>
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Small icon for the specified <paramref name="fileName"/></returns>
        public static Icon GetSmallIcon(string fileName)
        {
            var shinfo = new SHFILEINFO();
            SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON | SHGFI_LARGEICON | SHGFI_SYSICONINDEX);
            if (shinfo.hIcon != IntPtr.Zero)
                return Icon.FromHandle(shinfo.hIcon);
            else return null;
        }



        public static System.Drawing.Icon FindAssocIcon(string ext)
        {
            try
            {
                //string ext = System.IO.Path.GetExtension(filename);

                RegistryKey key = Registry.ClassesRoot.OpenSubKey(ext);

                if (key != null)
                {
                    string val = (string)key.GetValue("");

                    if (val != null && val != string.Empty)
                    {
                        key = Registry.ClassesRoot.OpenSubKey(val);

                        if (key != null)
                        {
                            key = key.OpenSubKey("DefaultIcon");

                            if (key != null)
                            {
                                val = (string)key.GetValue("");

                                if (val != null && val != string.Empty)
                                {
                                    string name;
                                    int icoIndex = -1;
                                    int index = val.LastIndexOf(',');

                                    if (index > 0)
                                    {
                                        name = val.Substring(0, index);

                                        if (index < val.Length - 1)
                                        {
                                            icoIndex = Convert.ToInt32(val.Substring(index + 1));
                                        }
                                    }//index
                                    else
                                        name = val;

                                    Icon tmpIcon = GetSmallIcon(name);

                                    if (tmpIcon == null)
                                    {
                                        System.Reflection.Module[] mod =
                                        System.Reflection.Assembly.GetCallingAssembly().GetModules();
                                        IntPtr ptr =
                                        ExtractAssociatedIcon(Marshal.GetHINSTANCE(mod[0]), name, ref icoIndex);

                                        if (ptr != IntPtr.Zero)
                                        {
                                            return System.Drawing.Icon.FromHandle(ptr);
                                        }
                                    }
                                    else
                                        return tmpIcon;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }



    }
}
