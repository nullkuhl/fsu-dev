using System;
using System.IO;
using System.Windows.Forms;

namespace Joiner
{
    public class Helper
    {
        /// <summary>
        /// Formats size for display
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatSize(ulong bytes)
        {
            double size = bytes;
            string unit = " bytes";
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " KB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " MB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " MB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " TB";
            }

            return size.ToString("0.##") + unit;
        }

        /// <summary>
        /// Checks if extract to path is valid
        /// </summary>
        /// <returns></returns>
        public static bool CheckDestinationPath(string path)
        {
            try
            {
                new Uri(path);
            }
            catch
            {
                MessageBox.Show("The destination path is not valid!");
                return false;
            }

            if (!Directory.Exists(path))
            {
                MessageBox.Show("The destination path is not valid!");
                return false;
            }

            return true;
        }

        /// <summary>
        ///  Verify that we have enough space on driver before joining
        /// </summary>
        /// <param name="destFolder"></param>
        /// <param name="sourceFileName"></param>
        /// <param name="pieceCount"></param>
        /// <returns></returns>
        public static bool IsEnoughSpace(string destFolder, string sourceFileName, int pieceCount)
        {
            DriveInfo driveInfo = new DriveInfo(destFolder);
            long freeSpace = driveInfo.AvailableFreeSpace;
            FileInfo fi = new FileInfo(sourceFileName);
            long spaceNeeded = fi.Length * pieceCount;
            if (spaceNeeded > freeSpace)
                return false;
            else
                return true;
        }    
    }
}
