using System;
using System.Collections.Generic;
using System.ComponentModel;
using DiskAnalysis.Helper;

namespace DiskAnalysis.Entities
{
    /// <summary>
    /// App file type
    /// </summary>
    public class AppFileType : INotifyPropertyChanged
    {
        readonly string fileType;
        string iconLocation;

        /// <summary>
        /// App file type class constructor
        /// </summary>
        /// <param name="fileType">File type</param>
        public AppFileType(string fileType)
        {
            this.fileType = fileType;
            Files = new List<AppFile>();
        }

        /// <summary>
        /// File type
        /// </summary>
        public string FileType
        {
            get { return fileType; }
        }

        /// <summary>
        /// Files collection
        /// </summary>
        public List<AppFile> Files { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Percent to display
        /// </summary>
        public int DisplayPercent
        {
            get { return Convert.ToInt32(dPercentage / 2); }
        }

        /// <summary>
        /// Percentage
        /// </summary>
        public decimal dPercentage { get; set; }

        /// <summary>
        /// String percentage
        /// </summary>
        public string Percentage
        {
            get { return dPercentage + " %"; }
        }

        /// <summary>
        /// Icon location
        /// </summary>
        public string IconLocation
        {
            get
            {
                if (string.IsNullOrEmpty(iconLocation))
                {
                    iconLocation = Files.Count > 0 ? Files[0].IconLocation : "";
                }
                return iconLocation;
            }
        }

        /// <summary>
        /// Total files count
        /// </summary>
        public long lTotalFileCount
        {
            get { return Files.Count; }
            set
            {
                OnPropertyChanged("lTotalFileCount");
            }
        }

        /// <summary>
        /// Total file size
        /// </summary>
        public long lTotalFileSize
        {
            get
            {
                long size = 0;
                Utility.CalculateFileSizesByFileType(this, ref size);
                return size;
            }
            set
            {
                OnPropertyChanged("lTotalFileSize");
            }
        }

        /// <summary>
        /// Total file size
        /// </summary>
        public string TotalFileSize
        {
            get { return Utility.GetSizeAsString(lTotalFileSize); }
            set
            {
                OnPropertyChanged("TotalFileSize");
            }
        }

        /// <summary>
        /// Adds a file to the files collection
        /// </summary>
        /// <param name="file"></param>
        public void Add(AppFile file)
        {
            Files.Add(file);
        }

        #region INotifyPropertyChanged

        /// <summary>
        /// Property value changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion
    }
}