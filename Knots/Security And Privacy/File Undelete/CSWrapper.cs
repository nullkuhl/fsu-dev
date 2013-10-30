using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FileUndelete
{
    /// <summary>
    /// Wraps calls to unmanaged Undelete.dll
    /// </summary>
    public class CSWrapper
    {
        #region Delegates

        /// <summary>
        /// RestorableItemFound callback
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileId"></param>
        /// <param name="isRecoverable"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public delegate bool RestorableItemFoundCallback([MarshalAs(UnmanagedType.LPWStr)] string filePath,
                                                         ulong fileId, bool isRecoverable, uint size);

        /// <summary>
        /// Callback function for notify application about progress.
        /// typedef BOOL (CALLBACK*UpdateProgressCallback)(IN DWORD dwProgress);
        /// </summary>
        /// <param Name="progress">Progress of scanning</param>
        /// <returns>FALSE  If scan process must terminated</returns>
        public delegate bool UpdateProgressCallback(int progress);

        #endregion

        /// <summary>
        /// Dll name
        /// </summary>
        public const string DllName = @"Libraries\Undelete.dll";

        /// <summary>
        /// Images filter
        /// </summary>
        public static string[] ImagesFilter = new[]
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
		                                      		"xpm"
		                                      	};

        /*
         * Attributes in DllImport:
         * SetLastError = true, The default is false //Marshal.GetLastWin32Error
         * CharSet = CharSet.Unicode //The default enumeration member for C# and Visual Basic is CharSet.Ansi
         * CallingConvention = CallingConvention.StdCall //The default value for the CallingConvention field is Winapi, which in turn defaults to StdCall convention.
         * EntryPoint = "MessageBox"
         */

        /// <summary>
        /// Recovers the requested file - according to llFileId to the requested Path.
        /// </summary>
        /// <param Name="fileId">Unique file Id</param>
        /// <param Name="recoveryPath">Path, Where file Recovered</param>
        /// <returns>return FALSE If error occurs</returns>
        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern bool RecoverFile(ulong fileId, string recoveryPath);

        /// <summary>
        /// Scans the requested drive for deleted files.
        /// Calls UpdateProgressCallback to notify application about progress.
        /// Calls RestorableItemFoundCallback to notify that a deleted file was found.
        /// 
        /// extern UNDELETE_API BOOL WINAPI ScanDrive(IN LPCWSTR sDriveToScan, 
        /// IN UpdateProgressCallback pProgressCallback, 
        /// IN RestorableItemFoundCallbackW fFoundCallback);
        /// </summary>
        /// <param Name="driveToScan">Name of drive for scanning</param>
        /// <param Name="updateProgress">Pointer to a progress callback function</param>
        /// <param Name="itemFound">Pointer to a 'file found' callback function</param>
        /// <param Name="extFilter">Extensions filter string in format "ext1|ext2|ex3"</param>
        /// <param Name="advancedSearch">Search in all sectors of the drive</param>
        /// <param name="driveToScan"></param>
        /// <param name="updateProgress"></param>
        /// <param name="itemFound"></param>
        /// <param name="extFilter"></param>
        /// <param name="advancedSearch"></param>
        /// <returns>FALSE If error occurs</returns>
        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern bool ScanDrive(string driveToScan,
                                            UpdateProgressCallback updateProgress,
                                            RestorableItemFoundCallback itemFound,
                                            string extFilter,
                                            bool advancedSearch);

        // Sets flat that indicates whether Recycle Bin should be processed
        /// <summary>
        /// </summary>		
        /// <param name="bSearchInRecycledBin">Search in RecycledBin flag</param>
        /// <returns>nothing</returns>
        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern void SetSearchInRecycledBin(bool bSearchInRecycledBin);

        /// <summary>
        /// Retrieves a list of Drives from which to search for deleted files (hard Drives and flash Drives).
        /// Returns Drives in format 'C:\0D:\0\0' - list of zero-terminated strings with zero in end of list.
        /// Function called two time:
        /// 1. GetDriveList(NULL, 0, &dwReturn) - for requesting size of buffer
        /// 2. GetDriveList(pBuffer, dwLength, &dwReturn) - for requesting Drives
        /// 
        /// extern UNDELETE_API BOOL WINAPI GetDriveList(IN LPWSTR pBuffer, IN DWORD dwLength, OUT DWORD *pdwReturn);
        /// </summary>
        /// <param Name="buffer">Output buffer</param>
        /// <param Name="length">Length of output buffer</param>
        /// <param Name="ret">Expected output buffer length</param>
        /// <returns>FALSE  If error occurs</returns>
        [DllImport(DllName, SetLastError = true)]
        static extern bool GetDriveList(byte[] buffer, int length, out int ret);

        /// <summary>
        /// Retrieves a list of Drives from which to search for deleted files (hard Drives and flash Drives).
        /// </summary>
        /// <returns>List of Drives</returns>
        public static string[] GetDrivesList()
        {
            byte[] bDrives = null;
            int length;

            if (!GetDriveList(bDrives, 0, out length))
            {
                throw new UndeleteWrapperException("Can not get drive list");
            }

            bDrives = new byte[length * 2];
            if (!GetDriveList(bDrives, length, out length))
            {
                throw new UndeleteWrapperException("Can not get drive list");
            }
            string drivesList = Encoding.Unicode.GetString(bDrives);
            return drivesList.Split(new[] { (char)0 }, StringSplitOptions.RemoveEmptyEntries);
        }

        #region Nested type: UndeleteWrapperException

        /// <summary>
        /// UndeleteWrapper exception
        /// </summary>
        public class UndeleteWrapperException : ApplicationException
        {
            /// <summary>
            /// <see cref="UndeleteWrapperException"/> constructor
            /// </summary>
            public UndeleteWrapperException()
            {
            }

            /// <summary>
            /// <see cref="UndeleteWrapperException"/> constructor
            /// </summary>
            /// <param name="message"></param>
            public UndeleteWrapperException(string message)
                : base(message)
            {
            }
        }

        #endregion
    }
}