using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using DiskAnalysis.Helper;

/// <summary>
/// The <see cref="DiskAnalysis.Entities"/> namespace defines an entity classes of the Disk Analysis knot
/// </summary>
namespace DiskAnalysis.Entities
{
    /// <summary>
    /// Class to store File information
    /// </summary>
    public class AppFile
    {
        string fileType;
        string iconLocation;

        /// <summary>
        /// File name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// File size
        /// </summary>
        public long LFileSize { get; set; }

        /// <summary>
        /// Folder path
        /// </summary>
        public string FolderPath { get; set; }

        /// <summary>
        /// Modified date
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Attributes
        /// </summary>
        public string Attributes { get; set; }

        /// <summary>
        /// File type
        /// </summary>
        public string FileType
        {
            get { return fileType; }
            set { fileType = value; }
        }

        /// <summary>
        /// File path
        /// </summary>
        public string FilePath
        {
            get
            {
                string path = FolderPath + "\\" + FileName;
                path = path.Replace("\\\\", "\\");
                return path;
            }
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
                    string path =  Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FreeSystemUtilities\\Icons\\DA" + fileType + ".bmp";
                    FileInfo fileInfo = new FileInfo(path);
                    if (fileInfo.Exists)
                    {
                        iconLocation = path;
                    }
                    else
                    {
                        Icon icon = Icon.ExtractAssociatedIcon(FilePath);
                        if (icon != null)
                        {
                            Bitmap b = icon.ToBitmap();
                            Bitmap thumb = new Bitmap(16, 16);
                            Graphics g = Graphics.FromImage(thumb);
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                            g.DrawImage(b, new Rectangle(0, 0, 16, 16), new Rectangle(0, 0, b.Width, b.Height), GraphicsUnit.Pixel);
                            g.Dispose();
                            b.Dispose();
                            thumb.Save(path);
                            thumb.Dispose();
                        }
                        iconLocation = path;
                    }
                }
                return iconLocation;
            }
        }

        /// <summary>
        /// File size
        /// </summary>
        public string FileSize // Size in Byte, KB, MB or GB
        {
            get { return Utility.GetSizeAsString(LFileSize); }
        }
    }
}