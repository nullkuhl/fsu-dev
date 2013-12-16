using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ScanFiles
{
    /// <summary>
    /// Wraps calls to unmanaged Undelete.dll
    /// </summary>
    public class CSWrapper
    {
        public const string DllName = "Undelete.dll";
        public static string[] ImagesFilter = new string[]
            {
                "bmp", "dib", "rle",
                "cr2",
                "crw",
                "dcr",
                "djv", "djvu", "iw4",
                "dng",
                "emf",
                "eps",
                "erf",
                "fpx",
                "gif",
                "icl",
                "icn",
                "ico", "cur", "ani",
                "iff", "lbm", "ilbm",
                "jp2", "jpx", "jpk", "j2k",
                "jpc", "j2c",
                "jpg", "jpeg", "jpe", "jif", "jfif", "thm",
                "mrw",
                "nef",
                "orf",
                "pbm",
                "pcd",
                "pcx", "dcx",
                "pef",
                "pgm",
                "pic",
                "pict", "pct",
                "pix",
                "png",
                "ppm",
                "psd",
                "psp",
                "raf",
                "ras",
                "032", "raw",
                "mos", "fff", "cs1",
                "bay",
                "rsb",
                "sgi", "rgb", "rgba", "bw", "int", "inta",
                "srf", "sr2",
                "tga",
                "tif", "tiff", "xif",
                "ttf", "ttc",
                "wbm", "wbmp",
                "wmf",
                "xbm", 
                "xpm",
            };

        /*
         * Attributes in DllImport:
         * SetLastError = true, The default is false //Marshal.GetLastWin32Error
         * CharSet = CharSet.Unicode //The default enumeration member for C# and Visual Basic is CharSet.Ansi
         * CallingConvention = CallingConvention.StdCall //The default value for the CallingConvention field is Winapi, which in turn defaults to StdCall convention.
         * EntryPoint = "MessageBox"
         */

        public class UndeleteWrapperException : ApplicationException
        {
            public UndeleteWrapperException()
            {
            }

            public UndeleteWrapperException(string message) : base(message)
            {
            }
        }

        /// <summary>
        /// Recovers the requested file - according to llFileId to the requested path.
        /// </summary>
        /// <param name="fileId">Unique file id</param>
        /// <param name="recoveryPath">Path, where file recovered</param>
        /// <returns>return FALSE If error occurs</returns>
        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern bool RecoverFile(ulong fileId, string recoveryPath);

        /// <summary>
        /// Callback function for notify application about progress.
        /// typedef BOOL (CALLBACK*UpdateProgressCallback)(IN DWORD dwProgress);
        /// </summary>
        /// <param name="progress">Progress of scanning</param>
        /// <returns>FALSE  If scan process must terminated</returns>
        public delegate bool UpdateProgressCallback(int progress);

        /// <summary>
        /// Callback function for notify that a deleted file was found.
        /// </summary>
        /// <param name="filePath">Path of restorable file</param>
        /// <param name="fileId">A unique id that will be used later to restore the file</param>
        /// <returns>FALSE  If scan process must terminated</returns>
        public delegate bool RestorableItemFoundCallback0([MarshalAs(UnmanagedType.LPWStr)]string filePath, ulong fileId);

        public delegate bool RestorableItemFoundCallback([MarshalAs(UnmanagedType.LPWStr)]string filePath, 
            ulong fileId, bool isRecoverable, uint size);

        /*
        typedef BOOL (CALLBACK*RestorableItemFoundCallback)(IN LPCWSTR sFilePath, IN LONGLONG llFileId,
                                                        IN BOOL bRecoverable, IN DWORD dwFileSize);
         */

        /// <summary>
        /// Scans the requested drive for deleted files.
        /// Calls UpdateProgressCallback to notify application about progress.
        /// Calls RestorableItemFoundCallback to notify that a deleted file was found.
        /// 
        /// extern UNDELETE_API BOOL WINAPI ScanDrive(IN LPCWSTR sDriveToScan, 
        /// IN UpdateProgressCallback pProgressCallback, 
        /// IN RestorableItemFoundCallbackW fFoundCallback);
        /// </summary>
        /// <param name="driveToScan">Name of drive for scanning</param>
        /// <param name="updateProgress">Pointer to a progress callback function</param>
        /// <param name="itemFound">Pointer to a 'file found' callback function</param>
        /// <param name="extFilter">Extensions filter string in format "ext1|ext2|ex3"</param>
        /// <param name="advancedSearch">Search in all sectors of the drive</param>
        /// <returns>FALSE If error occurs</returns>
        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern bool ScanDrive(string driveToScan,
            UpdateProgressCallback updateProgress, 
            RestorableItemFoundCallback itemFound,
            string extFilter,
            bool advancedSearch);

        /// <summary>
        // Sets flat that indicates whether Recycle Bin should be processed
        /// </summary>
        /// <param name="bSearchInRecycledBin">Search in RecycledBin flag</param>
        /// <returns>nothing</returns>
        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern void SetSearchInRecycledBin(bool bSearchInRecycledBin);

        /// <summary>
        /// Retrieves a list of drives from which to search for deleted files (hard drives and flash drives).
        /// Returns drives in format 'C:\0D:\0\0' - list of zero-terminated strings with zero in end of list.
        /// Function called two time:
        /// 1. GetDriveList(NULL, 0, &dwReturn) - for requesting size of buffer
        /// 2. GetDriveList(pBuffer, dwLength, &dwReturn) - for requesting drives
        /// 
        /// extern UNDELETE_API BOOL WINAPI GetDriveList(IN LPWSTR pBuffer, IN DWORD dwLength, OUT DWORD *pdwReturn);
        /// </summary>
        /// <param name="buffer">Output buffer</param>
        /// <param name="length">Length of output buffer</param>
        /// <param name="ret">Expected output buffer length</param>
        /// <returns>FALSE  If error occurs</returns>
        [DllImport(DllName, SetLastError = true)]
        private static extern bool GetDriveList(byte[] buffer, int length, out int ret);

        /// <summary>
        /// Retrieves a list of drives from which to search for deleted files (hard drives and flash drives).
        /// </summary>
        /// <returns>List of drives</returns>
        public static string[] GetDrivesList()
        {
            byte[] bDrives = null;
            int length;

            if(!GetDriveList(bDrives, 0, out length))
            {
                throw new UndeleteWrapperException("Can not get drive list");
            }

            bDrives = new byte[length * 2];
            if (!GetDriveList(bDrives, length, out length))
            {
                throw new UndeleteWrapperException("Can not get drive list");
            }
            string drivesList = Encoding.Unicode.GetString(bDrives);
            return drivesList.Split(new char[] { (char)0 }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
