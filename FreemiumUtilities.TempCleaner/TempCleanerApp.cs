using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using FreemiumUtilities.Infrastructure;

/// <summary>
/// The <see cref="FreemiumUtilities.TempCleaner"/> namespace defines a TempCleaner 1 Click-Maintenance application
/// </summary>
namespace FreemiumUtilities.TempCleaner
{
    /// <summary>
    /// TempCleaner 1 Click-Maintenance application <see cref="OneClickApp"/> implementation
    /// </summary>
    public class TempCleanerApp : OneClickApp
    {
        #region Properties

        ProgressUpdate callback;
        CancelComplete cancelComplete;
        bool chrome;
        ScanComplete complete;
        bool ff;
        bool fixAfterScan;
        bool ie;
        int processed;
        int scannedFilesCount;
        bool tmp;
        int totalFilesCount;
        bool win;
        /// <summary>
        /// Temp files collection
        /// </summary>
        public List<FileInfo> TmpFiles { get; set; }
        /// <summary>
        /// Windows temp files collection
        /// </summary>
        public List<FileInfo> WinFiles { get; set; }
        /// <summary>
        /// IE temp files collection
        /// </summary>
        public List<FileInfo> IEFiles { get; set; }
        /// <summary>
        /// Firefox temp files collection
        /// </summary>
        public List<FileInfo> FFFiles { get; set; }
        /// <summary>
        /// Chrome temp files collection
        /// </summary>
        public List<FileInfo> ChromeFiles { get; set; }

        /// <summary>
        /// Temp files size
        /// </summary>
        public long TmpSize { get; set; }
        /// <summary>
        /// Windows temp files size
        /// </summary>
        public long WinSize { get; set; }
        /// <summary>
        /// IE temp files size
        /// </summary>
        public long IESize { get; set; }
        /// <summary>
        /// Firefox temp files size
        /// </summary>
        public long FFSize { get; set; }
        /// <summary>
        /// Chrome temp files size
        /// </summary>
        public long ChromeSize { get; set; }
        /// <summary>
        /// App execution termination flag
        /// </summary>
        public bool ABORT { get; set; }
        /// <summary>
        /// Problems count
        /// </summary>
        public override int ProblemsCount { get; set; }

        #endregion

        /// <summary>
        /// constructor for TempCleanerApp
        /// </summary>
        public TempCleanerApp()
        {
            TmpFiles = new List<FileInfo>();
            WinFiles = new List<FileInfo>();
            IEFiles = new List<FileInfo>();
            FFFiles = new List<FileInfo>();
            ChromeFiles = new List<FileInfo>();

            TmpSize = 0;
            WinSize = 0;
            IESize = 0;
            FFSize = 0;
            ChromeSize = 0;
        }

        /// <summary>
        /// start scanning
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="complete"></param>
        /// <param name="cancelComplete"></param>
        /// <param name="fixAfterScan"></param>
        public override void StartScan(ProgressUpdate callback, ScanComplete complete, CancelComplete cancelComplete,
                                       bool fixAfterScan)
        {
            ABORT = false;

            try
            {
                TmpFiles = new List<FileInfo>();
                WinFiles = new List<FileInfo>();
                IEFiles = new List<FileInfo>();
                FFFiles = new List<FileInfo>();
                ChromeFiles = new List<FileInfo>();

                TmpSize = 0;
                WinSize = 0;
                IESize = 0;
                FFSize = 0;
                ChromeSize = 0;

                this.callback = callback;
                this.complete = complete;
                this.cancelComplete = cancelComplete;
                this.fixAfterScan = fixAfterScan;

                string winTempPath = Environment.GetEnvironmentVariable("Temp");
                string IETempPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                string ffCachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                     "\\Mozilla\\Firefox\\Profiles";
                string chromeCachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                         "\\Google\\Chrome\\User Data\\Default";

                DirectoryInfo windowsTemp = Directory.Exists(winTempPath) ? new DirectoryInfo(winTempPath) : null;
                DirectoryInfo IETemp = Directory.Exists(IETempPath) ? new DirectoryInfo(IETempPath) : null;
                DirectoryInfo ffCahce = Directory.Exists(ffCachePath) ? new DirectoryInfo(ffCachePath) : null;
                DirectoryInfo chromeCache = Directory.Exists(chromeCachePath) ? new DirectoryInfo(chromeCachePath) : null;

                int WinFilesCount = windowsTemp != null ? windowsTemp.GetDirectories().Length + windowsTemp.GetFiles().Length : 0;
                int IETempCount = IETemp != null ? IETemp.GetDirectories().Length + IETemp.GetFiles().Length : 0;
                int ffCahceCount = ffCahce != null ? ffCahce.GetDirectories().Length + ffCahce.GetFiles().Length : 0;
                int chromeCacheCount = chromeCache != null ? chromeCache.GetDirectories().Length + chromeCache.GetFiles().Length : 0;

                int filesCount = WinFilesCount + IETempCount + ffCahceCount + chromeCacheCount;

                List<DriveInfo> drives = null;
                try
                {
                    drives = DriveInfo.GetDrives().Where(drive => drive.IsReady && (drive.DriveType == DriveType.Fixed || drive.DriveType == DriveType.Removable)).ToList();
                }
                catch (IOException)
                {
                }
                catch (UnauthorizedAccessException)
                {
                }

                if (drives != null)
                {
                    filesCount += drives.Count;
                    scannedFilesCount = 0;

                    foreach (DriveInfo drive in drives)
                    {
                        if (ABORT)
                        {
                            cancelComplete();
                            return;
                        }
                        try
                        {
                            FileInfo[] tmpFiles = drive.RootDirectory.GetFiles("*.tmp");
                            TmpFiles.AddRange(tmpFiles);
                        }
                        catch
                        {
                        }
                        scannedFilesCount++;
                        callback((int)((scannedFilesCount / (float)filesCount) * 100), drive.RootDirectory.FullName);
                    }
                }

                if (windowsTemp != null)
                {
                    if (ABORT)
                    {
                        cancelComplete();
                        return;
                    }

                    CollectFiles(windowsTemp, "win");

                    int firstLevelSubDirectoriesCount = windowsTemp.GetDirectories().Count() + 1;

                    if (firstLevelSubDirectoriesCount < WinFilesCount)
                    {
                        int filesPerSubDirectory = WinFilesCount / firstLevelSubDirectoriesCount;
                        int filesPerSubDirectoryMod = WinFilesCount % firstLevelSubDirectoriesCount;

                        for (int i = 0; i < firstLevelSubDirectoriesCount - 1; i++)
                        {
                            scannedFilesCount += filesPerSubDirectory;
                            callback((int)((scannedFilesCount / (float)filesCount) * 100), windowsTemp.FullName);
                            Thread.Sleep(20);
                        }
                        scannedFilesCount += filesPerSubDirectory + filesPerSubDirectoryMod;
                        callback((int)((scannedFilesCount / (float)filesCount) * 100), windowsTemp.FullName);
                    }
                    else
                    {
                        scannedFilesCount += WinFilesCount;
                        callback((int)((scannedFilesCount / (float)filesCount) * 100), windowsTemp.FullName);
                    }
                }

                if (IETemp != null)
                {
                    if (ABORT)
                    {
                        cancelComplete();
                        return;
                    }

                    CollectFiles(IETemp, "ie");
                    scannedFilesCount += IETempCount;
                    callback((int)((scannedFilesCount / (float)filesCount) * 100), windowsTemp.FullName);
                }

                if (ffCahce != null)
                {
                    if (ABORT)
                    {
                        cancelComplete();
                        return;
                    }

                    CollectFiles(ffCahce, "ff");
                    scannedFilesCount += ffCahceCount;
                    callback((int)((scannedFilesCount / (float)filesCount) * 100), windowsTemp.FullName);
                }

                if (chromeCache != null)
                {
                    if (ABORT)
                    {
                        cancelComplete();
                        return;
                    }

                    CollectFiles(chromeCache, "chrome");
                    scannedFilesCount += chromeCacheCount;
                    callback((int)((scannedFilesCount / (float)filesCount) * 100), windowsTemp.FullName);
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }

            complete(fixAfterScan);
        }

        /// <summary>
        /// cancel scanning
        /// </summary>
        public override void CancelScan()
        {
            ABORT = true;
        }

        /// <summary>
        /// set scan and fix parameters
        /// </summary>
        /// <param name="tmp"></param>
        /// <param name="win"></param>
        /// <param name="ie"></param>
        /// <param name="ff"></param>
        /// <param name="chrome"></param>
        public void SetParams(bool tmp, bool win, bool ie, bool ff, bool chrome)
        {
            this.tmp = tmp;
            this.win = win;
            this.ie = ie;
            this.ff = ff;
            this.chrome = chrome;
        }

        /// <summary>
        /// start fixing
        /// </summary>
        /// <param name="callback"></param>
        public override void StartFix(ProgressUpdate callback)
        {
            ABORT = false;

            try
            {
                this.callback = callback;

                totalFilesCount = TmpFiles.Count + WinFiles.Count + IEFiles.Count + FFFiles.Count + ChromeFiles.Count;
                processed = 0;

                if (tmp)
                {
                    StartFixSubMethod(TmpFiles);
                }
                else
                    processed += TmpFiles.Count;

                if (win)
                {
                    StartFixSubMethod(WinFiles);
                }
                else
                    processed += WinFiles.Count;

                if (ie)
                {
                    StartFixSubMethod(IEFiles);
                }
                else
                    processed += IEFiles.Count;

                if (ff)
                {
                    StartFixSubMethod(FFFiles);
                }
                else
                    processed += FFFiles.Count;

                if (chrome)
                {
                    StartFixSubMethod(ChromeFiles);
                }
                else
                    processed += ChromeFiles.Count;
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }

            complete(fixAfterScan);
        }


        private void StartFixSubMethod(List<FileInfo> fiList)
        {
            foreach (FileInfo file in fiList)
            {
                if (ABORT)
                {
                    cancelComplete();
                    return;
                }

                processed++;
                try
                {
                    if (file.Exists)
                    {
                        file.Delete();
                        callback((int)((double)processed / totalFilesCount * 100), file.FullName);
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// cancel fixing
        /// </summary>
        public override void CancelFix()
        {
            ABORT = true;
        }

        /// <summary>
        /// Collects files
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="type"></param>
        void CollectFiles(DirectoryInfo dir, string type)
        {
            try
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    if (file.IsReadOnly || IOUtils.IsFileLocked(file))
                        continue;

                    if (ABORT)
                    {
                        cancelComplete();
                        return;
                    }

                    switch (type)
                    {
                        case "win":
                            WinFiles.Add(file);
                            WinSize += file.Length;
                            break;
                        case "ie":
                            IEFiles.Add(file);
                            IESize += file.Length;
                            break;
                        case "ff":
                            FFFiles.Add(file);
                            FFSize += file.Length;
                            break;
                        case "chrome":
                            if (file.Name.StartsWith("f_") || file.Name.EndsWith(".tmp"))
                            {
                                ChromeFiles.Add(file);
                                ChromeSize += file.Length;
                            }
                            break;
                    }
                }

                foreach (DirectoryInfo folder in dir.GetDirectories())
                {
                    CollectFiles(folder, type);
                }
            }
            catch
            {
            }
        }
    }
}