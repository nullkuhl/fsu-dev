using System;
using System.Collections.ObjectModel;
using DiskAnalysis.Helper;

namespace DiskAnalysis.Entities
{
    /// <summary>
    /// Class to store Folder information
    /// </summary>
    public class AppFolder
    {
        /// <summary>
        /// Constructor to initialize subfolders and files lists
        /// </summary>
        public AppFolder()
        {
            SubFolders = new ObservableCollection<AppFolder>();
            Files = new ObservableCollection<AppFile>();
        }

        /// <summary>
        /// Subfolders collection
        /// </summary>
        public ObservableCollection<AppFolder> SubFolders { get; set; }
        /// <summary>
        /// Files collection
        /// </summary>
        public ObservableCollection<AppFile> Files { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Full path
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        public long LSize
        {
            get
            {
                long totalFileSize = 0;
                Utility.CalculateFileSizes(this, ref totalFileSize);
                return totalFileSize;
            }
        }

        /// <summary>
        /// Formatted size
        /// </summary>
        public string Size // Size in Byte, KB, MB or GB
        {
            get { return Utility.GetSizeAsString(LSize); }
        }

        /// <summary>
        /// Percent
        /// </summary>
        public decimal dPercent { get; set; }
        /// <summary>
        /// String percent
        /// </summary>
        public int DisplayPercent
        {
            get { return Convert.ToInt32(dPercent / 2); }
        }

        /// <summary>
        /// Formatted percent
        /// </summary>
        public string Percent
        {
            get { return dPercent.ToString("00.00") + " %"; }
        }

        /// <summary>
        /// Folders count
        /// </summary>
        public int SubFoldersCount
        {
            get { return SubFolders.Count; }
        }

        /// <summary>
        /// Files count
        /// </summary>
        public int FilesCount
        {
            get { return Files.Count; }
        }

        /// <summary>
        /// Total folders count
        /// </summary>
        public int TotalFolderCount
        {
            get
            {
                int folderCount = 0;
                Utility.CalculateFolderCounts(this, ref folderCount);
                return folderCount;
            }
        }

        /// <summary>
        /// Total files count
        /// </summary>
        public int TotalFileCount
        {
            get
            {
                int fileCount = 0;
                Utility.CalculateFileCounts(this, ref fileCount);
                return fileCount;
            }
        }

        /// <summary>
        /// Is root directory
        /// </summary>
        public bool IsRootDirectory { get; set; }

        /// <summary>
        /// Total count of folders and subfolders
        /// </summary>
        public int FinalFolderCount
        {
            get { return TotalFolderCount + SubFolders.Count; }
        }

        /// <summary>
        /// Returns final files count
        /// </summary>
        public int FinalFileCount
        {
            get
            {
                return TotalFileCount + FilesCount;
            }
        }
    }
}