using System.IO;
using System;

namespace FileSplitterAndJoiner
{
    class Helper
    {
        /// <summary>
        /// Verify that at least 2 part files (result of splitting operation) exist before joining
        /// </summary>
        /// <param name="filename"></param>
        public static bool VerifyMinFilesCount(string filename)
        {
            bool result = false;
            try
            {
                FileInfo original = new FileInfo(filename);
                int extension = int.Parse(original.Extension.TrimStart('.'));

                FileInfo[] filesColl = new DirectoryInfo(filename).Parent.GetFiles();
                int ext = 0;
                foreach (FileInfo f in filesColl)
                {
                    if (Int32.TryParse(f.Extension.TrimStart('.'), out ext))
                        if (!int.Equals(ext, extension))
                            result = true;
                }
            }
            catch
            {
            }

            return result;
        }


        /// <summary>
        ///  Verify that we have enough space on driver before joining
        /// </summary>
        /// <param name="destFolder"></param>
        /// <param name="sourceFileName"></param>
        /// <param name="pieceCount"></param>
        /// <returns></returns>
        public static bool IsEnoughSpace(string destFolder, string sourceFileName, short pieceCount)
        {
            bool result = false;
            try
            {
                DriveInfo driveInfo = new DriveInfo(destFolder);
                long freeSpace = driveInfo.AvailableFreeSpace;
                FileInfo fi = new FileInfo(sourceFileName);
                long spaceNeeded = fi.Length * pieceCount;
                if (freeSpace >= spaceNeeded)
                    result = true;
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        ///  Verify that we have enough space on driver before splitting
        /// </summary>
        /// <param name="destFolder"></param>
        /// <param name="sourceFileName"></param>
        /// <param name="pieceCount"></param>
        /// <returns></returns>
        public static bool IsEnoughSpace(string destFolder, string sourceFileName)
        {
            return IsEnoughSpace(destFolder, sourceFileName, 1);
        }

    }
}
