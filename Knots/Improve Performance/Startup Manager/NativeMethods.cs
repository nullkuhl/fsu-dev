using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace StartupManager
{
    /// <summary>
    /// Provides native methods
    /// </summary>
    public class NativeMethods
    {
        #region API Declarations

        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, Int32 nIconIndex);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean DestroyIcon(IntPtr hIcon);

        #endregion

        #region Extract Icon
        /// <summary>
        /// Gets the icon from a specified <paramref name="filePath"/>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Icon GetIcon(string filePath)
        {
            try
            {
                // Extract icon 0 from filePath.
                Icon result = Icon.ExtractAssociatedIcon(filePath);
                return result;
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

        #endregion
    }
}