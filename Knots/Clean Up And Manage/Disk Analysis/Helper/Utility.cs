using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using DiskAnalysis.Entities;

namespace DiskAnalysis.Helper
{
    /// <summary>
    /// The <see cref="DiskAnalysis.Helper"/> namespace defines an helper classes of the Disk Analysis knot
    /// </summary>

    /// <summary>
    /// Thread safe <c>ObservableCollection<T></c>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ThreadSafeObservableCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// Override the <c>CollectionChanged</c> event so this class can access it
        /// </summary>
        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            // Be nice - use BlockReentrancy like MSDN said
            using (BlockReentrancy())
            {
                NotifyCollectionChangedEventHandler eventHandler = CollectionChanged;
                if (eventHandler == null)
                    return;

                Delegate[] delegates = eventHandler.GetInvocationList();
                // Walk thru invocation list
                foreach (NotifyCollectionChangedEventHandler handler in delegates)
                {
                    DispatcherObject dispatcherObject = handler.Target as DispatcherObject;
                    // If the subscriber is a DispatcherObject and different thread
                    if (dispatcherObject != null && dispatcherObject.CheckAccess() == false)
                    {
                        // Invoke handler in the target dispatcher's thread
                        dispatcherObject.Dispatcher.Invoke(DispatcherPriority.DataBind, handler, this, e);
                    }
                    else // Execute handler as is
                        handler(this, e);
                }
            }
        }
    }

    /// <summary>
    /// File utility class
    /// </summary>
    public static class Utility
    {
        const int SW_SHOW = 5;
        const uint SEE_MASK_INVOKEIDLIST = 12;

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

        /// <summary>
        /// Show file properties
        /// </summary>
        /// <param name="filename"></param>
        public static void ShowFileProperties(string filename)
        {
            SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
            info.cbSize = Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = filename;
            info.nShow = SW_SHOW;
            info.fMask = SEE_MASK_INVOKEIDLIST;
            ShellExecuteEx(ref info);
        }
 
        /// <summary>
        /// Gets file size as a formatted string
        /// </summary>
        /// <param name="lSize">File size</param>
        /// <returns>File size as a formatted string</returns>
        public static string GetSizeAsString(long lSize)
        {
            string size;
            if (lSize > 0)
            {
                decimal dSize = lSize;
                if (dSize > 1024)
                {
                    dSize = decimal.Round(dSize / 1024, 2);
                    if (dSize > 1024)
                    {
                        dSize = decimal.Round(dSize / 1024, 2);
                        if (dSize > 1024)
                        {
                            dSize = decimal.Round(dSize / 1024, 2);
                            size = dSize.ToString() + " GB";
                        }
                        else // MB
                        {
                            size = dSize.ToString() + " MB";
                        }
                    }
                    else // KB
                    {
                        size = dSize.ToString() + " KB";
                    }
                }
                else // BYTES
                {
                    size = lSize.ToString() + " Byte";
                }
            }
            else
            {
                size = "0 Byte";
            }
            return size;
        }

        internal static void CalculateFileCounts(AppFolder appFolder, ref int fileCount)
        {
            foreach (AppFolder f in appFolder.SubFolders)
            {
                fileCount += f.FilesCount;
                CalculateFileCounts(f, ref fileCount);
            }
        }

        internal static void CalculateFolderCounts(AppFolder appFolder, ref int folderCount)
        {
            foreach (AppFolder f in appFolder.SubFolders)
            {
                folderCount += f.SubFoldersCount;
                CalculateFolderCounts(f, ref folderCount);
            }
        }

        internal static void CalculateFileSizes(AppFolder appFolder, ref long totalSize)
        {
            totalSize += appFolder.Files.Sum(file => file.LFileSize);
            foreach (AppFolder f in appFolder.SubFolders)
            {
                CalculateFileSizes(f, ref totalSize);
            }
        }

        internal static void CalculateFileSizesByFileType(AppFileType appFileType, ref long size)
        {
            size += appFileType.Files.Sum(file => file.LFileSize);
        }

        #region Nested type: SHELLEXECUTEINFO

        /// <summary>
        /// SHELLEXECUTEINFO
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHELLEXECUTEINFO
        {
            /// <summary>
            /// cbSize
            /// </summary>
            public int cbSize;
            /// <summary>
            /// fMask
            /// </summary>
            public uint fMask;
            /// <summary>
            /// hwnd
            /// </summary>
            public IntPtr hwnd;

            /// <summary>
            /// lpVerb
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpVerb;
            /// <summary>
            /// lpFile
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpFile;
            /// <summary>
            /// lpParameters
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpParameters;
            /// <summary>
            /// lpDirectory
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpDirectory;
            /// <summary>
            /// nShow
            /// </summary>
            public int nShow;
            /// <summary>
            /// hInstApp
            /// </summary>
            public IntPtr hInstApp;
            /// <summary>
            /// lpIDList
            /// </summary>
            public IntPtr lpIDList;
            /// <summary>
            /// lpClass
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpClass;
            /// <summary>
            /// hkeyClass
            /// </summary>
            public IntPtr hkeyClass;
            /// <summary>
            /// dwHotKey
            /// </summary>
            public uint dwHotKey;
            /// <summary>
            /// hIcon
            /// </summary>
            public IntPtr hIcon;
            /// <summary>
            /// hProcess
            /// </summary>
            public IntPtr hProcess;
        }

        #endregion
    }
}