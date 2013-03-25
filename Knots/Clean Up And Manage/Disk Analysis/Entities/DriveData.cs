using System.IO;

namespace DiskAnalysis.Entities
{
    /// <summary>
    /// Drive data class
    /// </summary>
    public class DriveData
    {
        readonly string freeSpace;
        readonly decimal freeSpaceValue;
        readonly string name;
        readonly DirectoryInfo rootPath;
        readonly string totalSize;

        readonly decimal totalSizeValue;
        readonly string usedPercent;
        readonly decimal usedPercentValue;

        /// <summary>
        /// Drive data constructor
        /// </summary>
        /// <param name="drive">Drive info</param>
        public DriveData(DriveInfo drive)
        {
            name = drive.VolumeLabel + " (" + drive.RootDirectory.Name + ")";
            rootPath = drive.RootDirectory;

            totalSizeValue = decimal.Round(drive.TotalSize / 1024 / 1024, 2);
            totalSizeValue = decimal.Round(totalSizeValue / 1024, 2);
            totalSize = totalSizeValue.ToString() + " GB";

            LUsedSizeValue = drive.TotalSize - drive.TotalFreeSpace;

            freeSpaceValue = decimal.Round(drive.AvailableFreeSpace / 1024 / 1024, 2);
            freeSpaceValue = decimal.Round(freeSpaceValue / 1024, 2);
            freeSpace = freeSpaceValue.ToString() + " GB";

            usedPercentValue = decimal.Round((totalSizeValue - freeSpaceValue) / totalSizeValue * 100, 2);
            usedPercent = usedPercentValue.ToString() + " %";
        }

        /// <summary>
        /// Is drive checked
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Total size
        /// </summary>
        public string TotalSize
        {
            get { return totalSize; }
        }

        /// <summary>
        /// Free space
        /// </summary>
        public string FreeSpace
        {
            get { return freeSpace; }
        }

        /// <summary>
        /// Used percent
        /// </summary>
        public string UsedPercent
        {
            get { return usedPercent; }
        }

        /// <summary>
        /// Used size value
        /// </summary>
        public long LUsedSizeValue { get; set; }

        /// <summary>
        /// Total size value
        /// </summary>
        public decimal TotalSizeValue
        {
            get { return totalSizeValue; }
        }

        /// <summary>
        /// Free space value
        /// </summary>
        public decimal FreeSpaceValue
        {
            get { return freeSpaceValue; }
        }

        /// <summary>
        /// Used percent value
        /// </summary>
        public decimal UsedPercentValue
        {
            get { return usedPercentValue; }
        }

        /// <summary>
        /// Root path
        /// </summary>
        public DirectoryInfo RootPath
        {
            get { return rootPath; }
        }
    }
}