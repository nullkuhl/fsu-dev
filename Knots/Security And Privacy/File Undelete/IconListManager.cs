using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FileUndelete
{
    /// <summary>
    /// Maintains a list of currently added file extensions
    /// </summary>
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    };

    /// <summary>
    /// Folder types
    /// </summary>
    public enum FolderType
    {
        Closed,
        Open
    }

    /// <summary>
    /// Icon sizes
    /// </summary>
    public enum IconSize
    {
        Large,
        Small
    }

    /// <summary>
    /// IconList manager
    /// </summary>
    public class IconListManager
    {
        public const uint SHGFI_ICON = 0x000000100;
        public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
        public const uint SHGFI_OPENICON = 0x000000002;
        public const uint SHGFI_SMALLICON = 0x000000001;
        public const uint SHGFI_LARGEICON = 0x000000000;
        public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
        readonly Hashtable _extensionList = new Hashtable();
        readonly ArrayList _imageLists = new ArrayList(); //will hold ImageList objects

        /// <summary>
        /// Creates an instance of <c>IconListManager</c> that will Add icons to a single <c>ImageList</c> using the
        /// specified <c>IconSize</c>.
        /// </summary>
        /// <param Name="imageList"><c>ImageList</c> to Add icons to.</param>
        /// <param Name="iconSize">Size to use (either 32 or 16 pixels).</param>
        public IconListManager(ImageList imageList)
        {
            // Initialise the members of the class that will hold the image list we're
            // targeting, as well as the icon size (32 or 16)
            _imageLists.Add(imageList);
        }

        /// <summary>
        /// Creates an instance of IconListManager that will Add icons to two <c>ImageList</c> types. The two
        /// image lists are intended to be one for large icons, and the other for small icons.
        /// </summary>
        /// <param Name="smallImageList">The <c>ImageList</c> that will hold small icons.</param>
        /// <param Name="largeImageList">The <c>ImageList</c> that will hold large icons.</param>
        public IconListManager(ImageList smallImageList, ImageList largeImageList)
        {
            //Add both our image lists
            _imageLists.Add(smallImageList);
            _imageLists.Add(largeImageList);
        }

        /// <summary>
        /// Shell get file info method
        /// </summary>
        /// <param name="pszPath"></param>
        /// <param name="dwFileAttributes"></param>
        /// <param name="psfi"></param>
        /// <param name="cbFileInfo"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbFileInfo,
                                                  uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DestroyIcon(IntPtr hIcon);

        /// <summary>
        /// Gets folder icon
        /// </summary>
        /// <param name="size"></param>
        /// <param name="folderType"></param>
        /// <returns></returns>
        public static Icon GetFolderIconI(IconSize size, FolderType folderType)
        {
            // Need to Add size check, although errors generated at present!    
            uint flags = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES;

            if (FolderType.Open == folderType)
            {
                flags += SHGFI_OPENICON;
            }
            if (IconSize.Small == size)
            {
                flags += SHGFI_SMALLICON;
            }
            else
            {
                flags += SHGFI_LARGEICON;
            }
            // Get the folder icon    
            SHFILEINFO shfi = new SHFILEINFO();

            IntPtr res = SHGetFileInfo(@"C:\Windows",
                                       FILE_ATTRIBUTE_DIRECTORY,
                                       out shfi,
                                       (uint)Marshal.SizeOf(shfi),
                                       flags);

            if (res == IntPtr.Zero)
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());

            // Load the icon from an HICON handle  
            Icon.FromHandle(shfi.hIcon);

            // Now clone the icon, so that it can be successfully stored in an ImageList
            Icon icon = (Icon)Icon.FromHandle(shfi.hIcon).Clone();
            DestroyIcon(shfi.hIcon); // Cleanup    
            return icon;
        }

        /// <summary>
        /// Used internally, adds the extension to the hashtable, so that its value can then be returned.
        /// </summary>
        /// <param Name="Extension"><c>String</c> of the file's extension.</param>
        /// <param Name="ImageListPosition">Position of the extension in the <c>ImageList</c>.</param>
        void AddExtension(string Extension, int ImageListPosition)
        {
            _extensionList.Add(Extension, ImageListPosition);
        }

        /// <summary>
        /// Called publicly to Add a file's icon to the ImageList.
        /// </summary>
        /// <param Name="filePath">Full Path to the file.</param>
        /// <returns>Integer of the icon's position in the ImageList</returns>
        public int AddFileIcon(string filePath)
        {
            int pos = ((ImageList)_imageLists[0]).Images.Count; //store current Count -- new Item's index
            ((ImageList)_imageLists[0]).Images.Add(GetFolderIconI(IconSize.Small, FolderType.Open)); //Add to image list           
            return pos;
        }

        /// <summary>
        /// Adds hdd icon file 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public int AddFileIconhdd(string filePath)
        {
            int pos = ((ImageList)_imageLists[0]).Images.Count;
            ((ImageList)_imageLists[0]).Images.Add(IconReader.GetFolderIcon()); //Add to image list
            return pos;
        }
    }
}