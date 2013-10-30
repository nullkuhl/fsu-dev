using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace FreemiumUtilities.StartupManager
{
    internal class ShortcutClass : IDisposable
    {
        #region Constants

        const int INFOTIPSIZE = 1024;
        const int MAX_PATH = 260;

        #endregion

        #region Variables

        IShellLinkW link;

        #endregion

        #region Constructor

        /// <param name='linkPath'>
        /// Path to existing shortcut file (.lnk).
        /// </param>
        public ShortcutClass(string linkPath)
        {
            try
            {
                Guid CLSID_ShellLink = new Guid("00021401-0000-0000-C000-000000000046");

                Type shellLink = Type.GetTypeFromCLSID(CLSID_ShellLink);
                link = (IShellLinkW)(Activator.CreateInstance(shellLink));

                if (File.Exists(linkPath))
                {
                    IPersistFile pfile = (IPersistFile)link;
                    pfile.Load(linkPath, 0);
                }
            }
            catch (COMException)
            {
            }
        }

        #endregion

        #region Destructor

        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
        ~ShortcutClass()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            // free native resources if there are any.
            if (link != null)
            {
                Marshal.ReleaseComObject(link);
                link = null;
            }
        }

        #endregion

        #region Public Properties

        /// <value>
        ///   Gets the argument list of the shortcut.
        /// </value>
        public string Arguments
        {
            get
            {
                StringBuilder sb = new StringBuilder(INFOTIPSIZE);
                link.GetArguments(sb, sb.Capacity);
                return sb.ToString();
            }
        }

        /// <value>
        ///   Gets the shortcut comment.
        /// </value>
        public string Comment
        {
            get
            {
                StringBuilder sb = new StringBuilder(INFOTIPSIZE);
                link.GetDescription(sb, sb.Capacity);
                return sb.ToString();
            }
        }

        /// <value>
        ///   Gets the working directory of the shortcut.
        /// </value>
        public string WorkingDirectory
        {
            get
            {
                StringBuilder sb = new StringBuilder(MAX_PATH);
                link.GetWorkingDirectory(sb, sb.Capacity);
                return sb.ToString();
            }
        }

        /// <value>
        ///   Gets the target path of the shortcut.
        /// </value>
        /// <remarks>
        /// If Path returns an empty string, the shortcut is associated with
        /// a PIDL instead, which can be retrieved with IShellLink.GetIDList().
        /// This is beyond the scope of this wrapper class.
        /// </remarks>
        public string Path
        {
            get
            {
                WIN32_FIND_DATAW wfd;
                StringBuilder sb = new StringBuilder(MAX_PATH);

                link.GetPath(sb, sb.Capacity, out wfd, SLGP_FLAGS.SLGP_UNCPRIORITY);
                return sb.ToString();
            }
        }

        /// <value>
        ///   Gets the path of the <see cref="Icon"/> assigned to the shortcut.
        /// </value>
        /// <summary>
        ///   <seealso cref="IconIndex"/>
        /// </summary>
        public string IconLocation
        {
            get
            {
                StringBuilder sb = new StringBuilder(MAX_PATH);
                int nIconIdx;
                link.GetIconLocation(sb, sb.Capacity, out nIconIdx);
                return sb.ToString();
            }
        }

        /// <value>
        ///   Gets the index of the <see cref="Icon"/> assigned to the shortcut.
        ///   Set to zero when the <see cref="IconPath"/> property specifies a .ICO file.
        /// </value>
        /// <summary>
        ///   <seealso cref="IconPath"/>
        /// </summary>
        public int IconIndex
        {
            get
            {
                StringBuilder sb = new StringBuilder(MAX_PATH);
                int nIconIdx;
                link.GetIconLocation(sb, sb.Capacity, out nIconIdx);
                return nIconIdx;
            }
        }

        /// <value>
        ///   Retrieves the Icon of the shortcut as it will appear in Explorer.
        ///   Use the <see cref="IconPath"/> and <see cref="IconIndex"/>
        ///   properties to change it.
        /// </value>
        public Icon Icon
        {
            get
            {
                StringBuilder sb = new StringBuilder(MAX_PATH);
                int nIconIdx;

                link.GetIconLocation(sb, sb.Capacity, out nIconIdx);
                IntPtr hInst = Marshal.GetHINSTANCE(GetType().Module);
                IntPtr hIcon = ExtractIcon(hInst, sb.ToString(), nIconIdx);
                if (hIcon == IntPtr.Zero)
                    return null;

                // Return a cloned Icon, because we have to free the original ourselves.
                Icon ico = Icon.FromHandle(hIcon);
                Icon clone = (Icon)ico.Clone();
                ico.Dispose();
                DestroyIcon(hIcon);
                return clone;
            }
        }

        #endregion

        #region Native Win32 API functions

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DestroyIcon(IntPtr hIcon);

        #endregion
    }

    #region Flags

    /// <summary>
    /// IShellLink.Resolve fFlags
    /// </summary>
    [Flags]
    internal enum SLR_FLAGS
    {
        SLR_NO_UI = 0x1,
        SLR_ANY_MATCH = 0x2,
        SLR_UPDATE = 0x4,
        SLR_NOUPDATE = 0x8,
        SLR_NOSEARCH = 0x10,
        SLR_NOTRACK = 0x20,
        SLR_NOLINKINFO = 0x40,
        SLR_INVOKE_MSI = 0x80
    }

    /// <summary>
    /// IShellLink.GetPath fFlags
    /// </summary>
    [Flags]
    internal enum SLGP_FLAGS
    {
        SLGP_SHORTPATH = 0x1,
        SLGP_UNCPRIORITY = 0x2,
        SLGP_RAWPATH = 0x4
    }

    #endregion

    #region API Structure

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct WIN32_FIND_DATAW
    {
        internal int dwFileAttributes;
        internal FILETIME ftCreationTime;
        internal FILETIME ftLastAccessTime;
        internal FILETIME ftLastWriteTime;
        internal int nFileSizeHigh;
        internal int nFileSizeLow;
        internal int dwReserved0;
        internal int dwReserved1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
        internal string cFileName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        internal string cAlternateFileName;
        const int MAX_PATH = 260;
    }

    #endregion

    #region Interfaces

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("0000010B-0000-0000-C000-000000000046")]
    public interface IPersistFile
    {
        #region Methods inherited from IPersist

        void GetClassId(out Guid pClassId);

        #endregion

        [PreserveSig]
        int IsDirty();

        void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("000214F9-0000-0000-C000-000000000046")]
    internal interface IShellLinkW
    {
        void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath,
                     out WIN32_FIND_DATAW pfd, SLGP_FLAGS fFlags);

        void GetIDList(out IntPtr ppidl);

        void SetIDList(IntPtr pidl);

        void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);

        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

        void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);

        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

        void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);

        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

        void GetHotkey(out short pwHotkey);

        void SetHotkey(short wHotkey);

        void GetShowCmd(out int piShowCmd);

        void SetShowCmd(int iShowCmd);

        void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath,
                             out int piIcon);

        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

        void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);

        void Resolve(IntPtr hwnd, SLR_FLAGS fFlags);

        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }

    #endregion
}