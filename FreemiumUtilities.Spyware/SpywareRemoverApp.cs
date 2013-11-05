using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using FreemiumUtilities.Infrastructure;

/// <summary>
/// The <see cref="FreemiumUtilities.Spyware"/> namespace defines a Spyware remover 1 Click-Maintenance application
/// </summary>
namespace FreemiumUtilities.Spyware
{
    /// <summary>
    /// A Spyware Remover tool
    /// </summary>
    public class SpywareRemoverApp : OneClickApp
    {
        #region Instance Variables

        // List of spyware file names to look for

        ProgressUpdate callback;
        CancelComplete cancelComplete;
        ScanComplete complete;
        bool fixAfterScan;
        BackgroundWorker fixBackgroundWorker;
        int progressMax;
        private bool isCancel = false;

        BackgroundWorker scanBackgroundWorker;
        List<string> spywareLst = new List<string>();

        /// <summary>
        /// Problems count
        /// </summary>
        public override int ProblemsCount { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the spyware files found on the system. It should be called
        /// after <code>StartScan</code>method.
        /// </summary>
        public List<SpywareInfo> SpywareFound { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of spyware remover tool
        /// </summary>
        /// <param name="defFilePath">The path to the file containing spyware file names to check for</param>
        public SpywareRemoverApp(string defFilePath)
        {
            SpywareFound = new List<SpywareInfo>();
            ReadDefinitions(defFilePath);
        }

        #endregion

        #region Methods

        void ReadDefinitions(string defFilePath)
        {
            spywareLst = FileRW.ReadFile(defFilePath);
        }

        #region Scan

        /// <summary>
        /// handle DoWork event to start scanning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ScanBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SpywareFound.Clear();

                List<DirectoryInfo> directoriesToScan = new List<DirectoryInfo>();

                // Windows directory
                string winDirPath = Environment.GetEnvironmentVariable("windir");
                if (!string.IsNullOrEmpty(winDirPath))
                {
                    DirectoryInfo winDirInfo = new DirectoryInfo(winDirPath);

                    // SysWOW64
                    string sysWow64Path = winDirPath + "\\SysWOW64";
                    DirectoryInfo sysWow64Info = new DirectoryInfo(sysWow64Path);
                    if (sysWow64Info.Exists)
                        directoriesToScan.Add(sysWow64Info);

                    // System32 path
                    string win32Path = winDirPath + "\\System32";
                    directoriesToScan.Add(new DirectoryInfo(win32Path));

                    directoriesToScan.Add(winDirInfo);

                    // User directory
                    string userDirPath = new Uri(new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Personal)), ".").LocalPath;
                    directoriesToScan.Add(new DirectoryInfo(userDirPath));

                    progressMax = 4;

                    byte i = 0;
                    foreach (DirectoryInfo directoryToScan in directoriesToScan)
                    {
                        if (scanBackgroundWorker.CancellationPending) //checks for cancel request
                        {
                            e.Cancel = true;
                            return;
                        }
                        ScanDir(directoryToScan, directoryToScan == winDirInfo, i++, e);
                    }
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        /// <summary>
        /// handle ProgressChanged event to show progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void scanBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!isCancel)
            {
                callback(e.ProgressPercentage, e.UserState.ToString());
            }
        }

        /// <summary>
        /// handle RunWorkerCompleted to finish scanning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void scanBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || isCancel)
            {
                cancelComplete();
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message);
            }
            else
            {
                complete(fixAfterScan);
            }
        }

        void ScanDir(DirectoryInfo dirInfo, bool filesOnly, long sizeScanned, DoWorkEventArgs e)
        {
            try
            {
                // Get files and dirs
                if (dirInfo.FullName.Contains(@"System32\"))
                {
                    return;
                }
                FileInfo[] filesInDir = dirInfo.GetFiles();

                // Scan files
                foreach (FileInfo file in filesInDir)
                {
                    if (scanBackgroundWorker.CancellationPending) //checks for cancel request
                    {
                        e.Cancel = true;
                        return;
                    }

                    try
                    {
                        if (spywareLst.Contains(file.Name))
                        {
                            SpywareInfo spyware = new SpywareInfo
                                            {
                                                Spyware = file.Name,
                                                FilePath = file.FullName
                                            };
                            SpywareFound.Add(spyware);
                            ProblemsCount++;
                        }

                        int progressPercentage = (int)((double)sizeScanned / progressMax * 100);

                        scanBackgroundWorker.ReportProgress(progressPercentage, file.FullName); //reports a percentage between 0 and 100
                    }
                    catch { }
                }


                // Scan subdirectories
                if (filesOnly)
                    return;

                DirectoryInfo[] subDirs = dirInfo.GetDirectories();
                foreach (DirectoryInfo dir in subDirs)
                {
                    if (scanBackgroundWorker.CancellationPending) //checks for cancel request
                    {
                        e.Cancel = true;
                        return;
                    }

                    ScanDir(dir, filesOnly, sizeScanned, e);
                }
            }
            catch { }
        }

        #endregion

        #region Fix

        /// <summary>
        /// handle DoWork event to start fixing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FixBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int SpywareFoundCount = SpywareFound.Count;
                for (int i = 0; i < SpywareFoundCount; i++)
                {
                    if (fixBackgroundWorker.CancellationPending) //checks for cancel request
                    {
                        e.Cancel = true;
                        return;
                    }
                    SpywareInfo spyware = SpywareFound[i];
                    int progressPercentage = (int)((double)(i + 1) / SpywareFoundCount * 100);
                    fixBackgroundWorker.ReportProgress(progressPercentage, spyware.FilePath); //reports a percentage between 0 and 100
                    if (File.Exists(spyware.FilePath))
                    {
                        try
                        {
                            File.Delete(spyware.FilePath);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        /// <summary>
        /// handle ProgressChanged event to show progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fixBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!isCancel)
            {
                callback(e.ProgressPercentage, e.UserState.ToString());
            }
        }

        /// <summary>
        /// handle RunWorkerCompleted event to finish fixing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fixBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || isCancel)
            {
                //TODO: Think is it needed here!
                cancelComplete();
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message);
            }
            else
            {
                complete(fixAfterScan);
            }
        }

        #endregion

        #endregion

        #region Public Methods

        #region Scan

        /// <summary>
        /// Starts the scanning process
        /// </summary>
        /// <param name="callback">The callback method to update progress</param>
        public override void StartScan(ProgressUpdate callback, ScanComplete complete, CancelComplete cancelComplete,
                                       bool fixAfterScan)
        {
            this.callback = callback;
            this.complete = complete;
            this.cancelComplete = cancelComplete;
            this.fixAfterScan = fixAfterScan;

            isCancel = false;
            scanBackgroundWorker = new BackgroundWorker();
            scanBackgroundWorker.WorkerSupportsCancellation = true;
            scanBackgroundWorker.WorkerReportsProgress = true;
            scanBackgroundWorker.DoWork += ScanBackgroundWorkerDoWork;
            scanBackgroundWorker.RunWorkerCompleted += scanBackgroundWorker_RunWorkerCompleted;
            scanBackgroundWorker.ProgressChanged += scanBackgroundWorker_ProgressChanged;

            scanBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// cancel the scanning process
        /// </summary>
        public override void CancelScan()
        {
            isCancel = true;
            if (scanBackgroundWorker.IsBusy)
                scanBackgroundWorker.CancelAsync(); //makes the backgroundworker stop
        }

        #endregion

        #region Fix

        /// <summary>
        /// Deletes found spyware
        /// </summary>
        public override void StartFix(ProgressUpdate callback)
        {
            isCancel = false;
            this.callback = callback;
            fixBackgroundWorker = new BackgroundWorker();
            fixBackgroundWorker.WorkerSupportsCancellation = true;
            fixBackgroundWorker.WorkerReportsProgress = true;
            fixBackgroundWorker.DoWork += FixBackgroundWorkerDoWork;
            fixBackgroundWorker.RunWorkerCompleted += fixBackgroundWorker_RunWorkerCompleted;
            fixBackgroundWorker.ProgressChanged += fixBackgroundWorker_ProgressChanged;

            fixBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// cancel spyware deleting
        /// </summary>
        public override void CancelFix()
        {
            isCancel = true;
            if (fixBackgroundWorker.IsBusy)
                fixBackgroundWorker.CancelAsync(); //makes the backgroundworker stop
        }

        #endregion

        #endregion
    }
}