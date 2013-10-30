using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using DiskAnalysis.Entities;
using DiskAnalysis.Helper;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Resx = DiskAnalysis.Properties.Resources;

namespace DiskAnalysis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        // Global variables

        readonly BackgroundWorker bwDiskScanner; //Background worker for Scanning
        private BackgroundWorker backgroundWorker;
        int currentD;
        DirectoryInfo d;

        ObservableCollection<AppFolder> directories = new ObservableCollection<AppFolder>();
        // Contains the scanned data (folders, files and related information)

        ObservableCollection<AppFileType> fileTypeList = new ObservableCollection<AppFileType>();
        //Contains Filelist by File type

        string folderToScan;
        int ind; //index of drive being scanned

        bool isAborted; // flag for aborting the scan
        bool isNewScanStarted; // flag for new scan started
        FrameworkElement lastFocusedList;
        ScanWindow scanDialog; // ScanWindow Dialog

        /// <summary>
        /// Scan drives or folders selected in Scan Window
        /// </summary>
        List<DirectoryInfo> scanDirs = new List<DirectoryInfo>();

        long scannedFileSize;
        long totalFileSizeToScan;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            collFileType.CanUserSort = false;
            collPercent.CanUserSort = false;
            collSize.CanUserSort = false;
            collFiles.CanUserSort = false;
            collFolderFileName.CanUserSort = false;
            collFolderAttribute.CanUserSort = false;
            collFolderFolder.CanUserSort = false;
            collFolderModified.CanUserSort = false;
            collFolderSize.CanUserSort = false;
            ind = -1;
            // Check and Clear Temp Data
            try
            {
                FileInfo[] tempFiles = new DirectoryInfo(Environment.CurrentDirectory + "\\Icons").GetFiles();

                try
                {
                    foreach (var t in tempFiles)
                    {
                        t.Delete();
                    }
                }
                catch
                {
                }
            }
            catch (DirectoryNotFoundException)
            {
                var curDir = new DirectoryInfo(Environment.CurrentDirectory);
                curDir.CreateSubdirectory("Icons");
            }

            StyleManager.ApplicationTheme = new Windows7Theme();

            // Initialize Background worker
            bwDiskScanner = new BackgroundWorker();
            bwDiskScanner.DoWork += bwDiskScanner_DoWork;
            bwDiskScanner.ProgressChanged += bwDiskScanner_ProgressChanged;
            bwDiskScanner.RunWorkerCompleted += bwDiskScanner_RunWorkerCompleted;
            bwDiskScanner.WorkerReportsProgress = true;
            bwDiskScanner.WorkerSupportsCancellation = true;

            // Set Widths & Heights
            //rtvFolderList.Width = (Width * 3 / 5) - 6;
            lnkAbort.Visibility = Visibility.Hidden;
            rtvFolderList.DataLoadMode = DataLoadMode.Synchronous;

            // Initialize global variables and control defaults
            isAborted = false;
            txtFileLabel.Text = "";
            txtStatus.Text = "";
            progressBar1.Visibility = Visibility.Hidden;
            scannedFileSize = 0;

            //Event handlers
            //Loaded += MainWindow_Loaded;
            this.ContentRendered += new EventHandler(MainWindow_ContentRendered);
        }

        void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            //MenuItem btn = this.Toolbar.Items[0] as MenuItem;
            //btn.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));

            scanDialog = new ScanWindow(true);
            bool? result = scanDialog.ShowDialog();
            if (result.HasValue)
            {
                LogClass.AddInfoToLog(LogClass.LogInfo.Info, result.ToString());
            }
            else
                LogClass.AddInfoToLog(LogClass.LogInfo.Info, "result DON'T HAVE Value");

            if (result.HasValue && result == true)
            {
                isNewScanStarted = true;
                ScanSelectedDrivesOrFolders(true);
            }
        }

        /// <summary>
        /// Loaded Event Handler for Main Window
        /// Calls the Scan Window as modal dialog, and calls the Scan method on return from dailog
        /// </summary>
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            //LogClass.AddInfoToLog(LogClass.LogInfo.Start, "MainWindow_Loaded");
            //scanDialog = new ScanWindow();
            //bool? result = scanDialog.ShowDialog();
            //if (result.HasValue)
            //{
            //    LogClass.AddInfoToLog(LogClass.LogInfo.Info, result.ToString());
            //}
            //else
            //    LogClass.AddInfoToLog(LogClass.LogInfo.Info, "result DON'T HAVE Value");

            //if (result.HasValue && result == true)
            //{
            //    isNewScanStarted = true;
            //    ScanSelectedDrivesOrFolders(true);
            //}
            //LogClass.AddInfoToLog(LogClass.LogInfo.End, "MainWindow_Loaded");
        }

        void ScanSelectedDrivesOrFolders(bool? result)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "ScanSelectedDrivesOrFolders");
            if (result.HasValue && result == true)
            {
                LogClass.AddInfoToLog(LogClass.LogInfo.Info, "Scan Begins");
                Refresh.IsEnabled = false;
                progressBar1.Value = 0;
                totalFileSizeToScan = 0;
                scannedFileSize = 0;
                ind = -1;

                directories = new ObservableCollection<AppFolder>();
                scanDirs = new List<DirectoryInfo>();

                var bw = new BackgroundWorker();
                bw.DoWork += bwCalculateFileSize_DoWork;
                bw.RunWorkerCompleted += bwCalculateFileSize_RunWorkerCompleted;

                //DriveInfo di = new DriveInfo("C");
                //var driveData = new DriveData(di);

                //scanDirs.Add(driveData.RootPath);
                //totalFileSizeToScan += driveData.LUsedSizeValue;
                //d = scanDirs[0];
                //txtStatus.Text = Resx.Scanning;
                //progressBar1.Visibility = Visibility.Visible;
                //progressBar1.IsIndeterminate = true;
                //StartWork();

                if (scanDialog.rbAllDrives.IsChecked == true)
                {
                    foreach (DriveData drive in scanDialog.DriveData)
                    {
                        scanDirs.Add(drive.RootPath);
                        totalFileSizeToScan += drive.LUsedSizeValue;
                    }
                    d = scanDirs[0];

                    txtStatus.Text = Resx.Scanning;
                    progressBar1.Visibility = Visibility.Visible;
                    progressBar1.IsIndeterminate = true;
                    StartWork();
                }
                else if (scanDialog.rbDrives.IsChecked == true)
                {
                    foreach (DriveData drive in scanDialog.DriveData)
                    {
                        if (drive.IsChecked)
                        {
                            scanDirs.Add(drive.RootPath);

                            totalFileSizeToScan += drive.LUsedSizeValue;
                        }
                    }
                    d = scanDirs[0];

                    txtStatus.Text = Resx.Scanning;
                    progressBar1.Visibility = Visibility.Visible;
                    progressBar1.IsIndeterminate = true;

                    StartWork();
                }
                else // Individual folder
                {
                    folderToScan = scanDialog.tbFolder.Text;

                    if (!string.IsNullOrEmpty(folderToScan))
                    {
                        txtStatus.Text = Resx.Scanning;
                        progressBar1.Visibility = Visibility.Visible;
                        progressBar1.IsIndeterminate = true;
                        bw.RunWorkerAsync();
                    }
                }
            }
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "ScanSelectedDrivesOrFolders");
        }

        void StartWork()
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "StartWork");
            Analyze.IsEnabled = false;
            if (d != null)
            {
                // Initialize the Root folders and files
                // And then call background worker

                var f = new AppFolder { Name = d.Name, FullPath = d.FullName, IsRootDirectory = true };
                directories.Add(f);

                FileSystemInfo[] filesNFolders = new FileSystemInfo[] { };
                try
                {
                    filesNFolders = d.GetFileSystemInfos();
                }
                catch (Exception ex)
                {
                    LogClass.AddErrorToLog(" Method - ControlSortingAbility - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                }

                foreach (FileSystemInfo fileInfo in filesNFolders)
                {
                    if ((fileInfo.Attributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
                    {
                        continue;
                    }
                    Type t = fileInfo.GetType();

                    if (t == typeof(DirectoryInfo))
                    {
                        var dir = fileInfo as DirectoryInfo;
                        if (dir != null)
                            f.SubFolders.Add(new AppFolder
                                                {
                                                    Name = dir.Name,
                                                    FullPath = dir.FullName
                                                });
                    }
                    else if (t == typeof(FileInfo))
                    {
                        var file = fileInfo as FileInfo;
                        if (file != null)
                        {
                            var af = new AppFile
                                        {
                                            FileName = file.Name,
                                            FolderPath = file.DirectoryName,
                                            LFileSize = file.Length,
                                            ModifiedDate = file.LastWriteTime,
                                            FileType = file.Extension.ToLower(),
                                            Attributes = file.Attributes.ToString()
                                        };

                            f.Files.Add(af);
                        }
                    }
                }

                txtStatus.Text = Resx.Scanning;
                progressBar1.Visibility = Visibility.Visible;
                progressBar1.IsIndeterminate = false;
                lnkAbort.Visibility = Visibility.Visible;
                bwDiskScanner.RunWorkerAsync();
            }
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "StartWork");
        }

        /// <summary>
        /// Turns on/off column.CanUserSort property
        /// Needed to prevent app crash in Window XP,
        /// happens on hovering sortable column header when directories is null or empty
        /// </summary>
        void ControlSortingAbility()
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "ControlSortingAbility");
            if (directories != null)
            {
                if (directories.Count <= 0)
                {
                    try
                    {
                        collFileType.CanUserSort = false;
                        collPercent.CanUserSort = false;
                        collSize.CanUserSort = false;
                        collFiles.CanUserSort = false;
                        collFolderFileName.CanUserSort = false;
                        collFolderAttribute.CanUserSort = false;
                        collFolderFolder.CanUserSort = false;
                        collFolderModified.CanUserSort = false;
                        collFolderSize.CanUserSort = false;
                    }
                    catch (Exception ex)
                    {
                        LogClass.AddErrorToLog(" Method - ControlSortingAbility - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        collFileType.CanUserSort = true;
                        collPercent.CanUserSort = true;
                        collSize.CanUserSort = true;
                        collFiles.CanUserSort = true;
                        collFolderFileName.CanUserSort = true;
                        collFolderAttribute.CanUserSort = true;
                        collFolderFolder.CanUserSort = true;
                        collFolderModified.CanUserSort = true;
                        collFolderSize.CanUserSort = true;
                    }
                    catch (Exception ex)
                    {
                        LogClass.AddErrorToLog(" Method - ControlSortingAbility - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                    }
                }
            }
            else
            {
                try
                {
                    collFileType.CanUserSort = false;
                    collPercent.CanUserSort = false;
                    collSize.CanUserSort = false;
                    collFiles.CanUserSort = false;
                    collFolderFileName.CanUserSort = false;
                    collFolderAttribute.CanUserSort = false;
                    collFolderFolder.CanUserSort = false;
                    collFolderModified.CanUserSort = false;
                    collFolderSize.CanUserSort = false;
                }
                catch (Exception ex)
                {
                    LogClass.AddErrorToLog(" Method - ControlSortingAbility - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                }
            }
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "ControlSortingAbility");
        }

        void bwCalculateFileSize_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "bwCalculateFileSize_RunWorkerCompleted");
            StartWork();
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "bwCalculateFileSize_RunWorkerCompleted");
        }

        void bwCalculateFileSize_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                d = new DirectoryInfo(folderToScan);
                FileInfo[] files = d.GetFiles();
                foreach (FileInfo f in files)
                {
                    try
                    {
                        totalFileSizeToScan += f.Length;
                    }
                    catch (Exception ex)
                    {
                        LogClass.AddErrorToLog(" Method - bwCalculateFileSize_DoWork - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                    }
                }
            }
            catch(Exception ex) 
            {
                LogClass.AddErrorToLog(" Method - bwCalculateFileSize_DoWork - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
            }
        }

        /// <summary>
        /// Background Worker completed event handler
        /// Reset the progress bar and status labels
        /// Enable menu commands
        /// Data bind the result from scanning
        /// </summary>
        void bwDiskScanner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "bwDiskScanner_RunWorkerCompleted");
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.WorkerReportsProgress = true;

            progressBar1.IsIndeterminate = true;
            txtStatus.Text = Resx.processing;
            txtFileLabel.Text = "";
            backgroundWorker.RunWorkerAsync();
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "bwDiskScanner_RunWorkerCompleted");
        }

        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "backgroundWorker_RunWorkerCompleted");
            rtvFolderList.ItemsSource = directories.OrderByDescending(folder => folder.LSize);
            dgFileListByFolder.ItemsSource = directories[0].Files.OrderByDescending(f => f.LFileSize);
            dgFileTypeList.ItemsSource = fileTypeList.OrderByDescending(f => f.lTotalFileSize);

            rtvFolderList.UpdateLayout();
            dgFileListByFolder.UpdateLayout();
            dgFileTypeList.UpdateLayout();

            ControlSortingAbility();

            if (isAborted)
            {
                txtStatus.Text = Resx.Aborted;
                isAborted = false;

                isNewScanStarted = false;
                lnkAbort.Visibility = Visibility.Hidden;
                bwDiskScanner.Dispose();
                txtFileLabel.Text = "";
                progressBar1.Visibility = Visibility.Hidden;
                progressBar1.Value = 0;
                Refresh.IsEnabled = true;
            }
            else
            {
                txtStatus.Text = Resx.Completed;

                if (++currentD < scanDirs.Count)
                {
                    d = scanDirs[currentD];
                    StartWork();
                }
                else
                {
                    currentD = 0;

                    isNewScanStarted = false;
                    lnkAbort.Visibility = Visibility.Hidden;
                    bwDiskScanner.Dispose();
                    txtFileLabel.Text = "";
                    progressBar1.Visibility = Visibility.Hidden;
                    progressBar1.Value = 0;
                    Refresh.IsEnabled = true;
                }
            }
            Analyze.IsEnabled = true;
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "backgroundWorker_RunWorkerCompleted");
        }

        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "backgroundWorker_DoWork");
            FinalWork();
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "backgroundWorker_DoWork");
        }

        void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var folderState = e.UserState as DiskAnalysis.Entities.AppFileType;
            if (folderState != null)
            {
                txtFileLabel.Text = folderState.FileType;
            }
            progressBar1.Value = e.ProgressPercentage;
        }

        public void FinalWork()
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "FinalWork");
            if (isNewScanStarted)
            {
                fileTypeList = new ObservableCollection<AppFileType>();
                isNewScanStarted = false;
            }

            GetFileTypeData(directories[ind]);

            //new Telerik.Windows.Controls.GridView.
            // Calculate Totals for File Type
            long fileSizesByType = fileTypeList.Sum(fileType => fileType.lTotalFileSize);

            foreach (AppFileType fileType in fileTypeList)
            {
                try
                {
                    decimal d = Convert.ToDecimal(fileType.lTotalFileSize * 100.0);
                    fileType.dPercentage = decimal.Round(d / fileSizesByType, 2);
                }
                catch (Exception ex)
                {
                    LogClass.AddErrorToLog(" Method - FinalWork - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                }
            }

            // Calculate Totals for Folders
            directories[currentD].DPercent = 100;

            foreach (AppFolder fol in directories[currentD].SubFolders)
            {
                GetPercentage(fol, directories[currentD].LSize);
            }
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "FinalWork");
        }

        void GetPercentage(AppFolder fol, long totalSize)
        {
            decimal d = Convert.ToDecimal(fol.LSize * 100.0);
            fol.DPercent = totalSize == 0 ? 100 : decimal.Round(d / totalSize, 2);
            foreach (AppFolder fol1 in fol.SubFolders)
            {
                GetPercentage(fol1, totalSize);
            }
        }

        void GetFileTypeData(AppFolder f)
        {
            foreach (AppFile file in f.Files)
            {
                IEnumerable<AppFileType> isExists = (from ft in fileTypeList
                                                     where ft.FileType == file.FileType.ToLower()
                                                     select ft);

                if (isExists.Count() == 1)
                {
                    isExists.First().Add(file);
                }
                else
                {
                    var fileType = new AppFileType(file.FileType);
                    fileType.Add(file);

                    fileTypeList.Add(fileType);
                }
            }
            foreach (AppFolder folder in f.SubFolders)
            {
                GetFileTypeData(folder);
            }
        }

        /// <summary>
        /// Report the scanning and processing progress
        /// Use the progress bar and the status label for reporting
        /// </summary>
        /// <param name="e">e.UserState contains the progress information</param>
        void bwDiskScanner_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var folderState = e.UserState as BwFolderState;
            if (folderState != null)
            {
                folderState.CurrentFolder.SubFolders.Add(folderState.SubFolder);
                txtFileLabel.Text = folderState.SubFolder.FullPath;
            }
            progressBar1.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Background Worker main method
        /// Processes the scanning loop and reports on progress
        /// </summary>
        void bwDiskScanner_DoWork(object sender, DoWorkEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "bwDiskScanner_DoWork");
            //  foreach (AppFolder d in directories)
            // {
            ind++;

            foreach (AppFolder f in directories[ind].SubFolders)
            {
                GetFilesAndFolders(f);
            }
            //  }
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "bwDiskScanner_DoWork");
        }

        /// <summary>
        /// Recursive scanning for files and folders 
        /// Uses DirectoryInfo.GetFileSystemInfos method to simultaneously get folder and file information
        /// </summary>
        /// <param name="folder">
        /// Folder to be scanned for
        /// Recursively adds subfolders and file information to the folder
        /// </param>
        void GetFilesAndFolders(AppFolder folder)
        {
            var searchDir = new DirectoryInfo(folder.FullPath);
            FileSystemInfo[] filesAndFolders = null;
            try
            {
                if ((searchDir.Attributes & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
                {
                    filesAndFolders = searchDir.GetFileSystemInfos();
                }
            }
            catch
            {
                // Avoid Access Denied exceptions        
            }
            if (filesAndFolders != null)
            {
                foreach (FileSystemInfo fileInfo in filesAndFolders)
                {
                    if ((fileInfo.Attributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
                    {
                        continue;
                    }
                    BwFolderState folderStateData = null;
                    Type t = fileInfo.GetType();

                    if (t == typeof(DirectoryInfo))
                    {
                        var dir = fileInfo as DirectoryInfo;
                        var subFolder = new AppFolder { Name = dir.Name, FullPath = dir.FullName };
                        folderStateData = new BwFolderState
                                            {
                                                CurrentFolder = folder,
                                                SubFolder = subFolder
                                            };
                        if (!bwDiskScanner.CancellationPending)
                        {
                            int prog;

                            prog = totalFileSizeToScan != 0 ? Convert.ToInt32((scannedFileSize * 100) / totalFileSizeToScan) : 1;

                            bwDiskScanner.ReportProgress(prog, folderStateData);

                            GetFilesAndFolders(subFolder);
                        }
                    }
                    else if (t == typeof(FileInfo))
                    {
                        var file = fileInfo as FileInfo;
                        AppFile af = null;
                        try
                        {
                            if (file != null)
                                af = new AppFile
                                        {
                                            FileName = file.Name,
                                            FolderPath = file.DirectoryName,
                                            LFileSize = file.Length,
                                            ModifiedDate = file.LastWriteTime,
                                            FileType = file.Extension.ToLower(),
                                            Attributes = file.Attributes.ToString()
                                        };
                        }
                        catch (Exception ex)
                        {
                            LogClass.AddErrorToLog(" Method - GetFilesAndFolders - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                            try
                            {
                                if (file != null)
                                    af = new AppFile
                                             {
                                                 FileName = file.Name,
                                                 FolderPath = file.DirectoryName,
                                                 LFileSize = file.Length,
                                                 FileType = file.Extension.ToLower(),
                                                 Attributes = file.Attributes.ToString()
                                             };
                            }
                            catch (Exception ex2)
                            {
                                LogClass.AddErrorToLog(" Method - GetFilesAndFolders - Exeption [" + ex2.GetType().Name + "] - " + ex2.Message);
                            }

                        }
                        try
                        {
                            if (af != null) folder.Files.Add(af);

                            if (file != null) scannedFileSize += file.Length;
                        }
                        catch (Exception ex)
                        {
                            LogClass.AddErrorToLog(" Method - GetFilesAndFolders - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Initial data loaded event handler for the treeview to handle expansion of first element
        /// </summary>
        void rtvFolderList_DataLoaded(object sender, EventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "bwDiskScanner_DoWork");
            var source = rtvFolderList.ItemsSource as ICollection;
            if (source != null)
            {
                if (rtvFolderList.Items.Count == source.Count)
                {
                    foreach (object item in source)
                    {
                        rtvFolderList.ExpandHierarchyItem(item);
                    }
                }
                else if (rtvFolderList.Items.Count > source.Count)
                {
                    rtvFolderList.DataLoaded -= rtvFolderList_DataLoaded;
                }
            }
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "bwDiskScanner_DoWork");
        }

        /// <summary>
        /// Abort event handler
        /// </summary>
        void lnkAbort_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bwDiskScanner.CancelAsync();
            isAborted = true;
        }

        /// <summary>
        /// Escape Key - aborts scanning 
        /// </summary>
        void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                LogClass.AddInfoToLog(LogClass.LogInfo.Info, "Aborted");
                bwDiskScanner.CancelAsync();
                isAborted = true;
            }
        }

        /// <summary>
        /// Open MenuItem click event handler
        /// Calls the Scan Window in dialog mode
        /// </summary>
        void Analyze_Click(object sender, RoutedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "Analyze_Click");
            scanDialog = new ScanWindow(false);
            bool? result = scanDialog.ShowDialog();
            if (result.HasValue && result == true)
            {
                LogClass.AddInfoToLog(LogClass.LogInfo.Info, "RESULT = TRUE");
                isNewScanStarted = true;
                ScanSelectedDrivesOrFolders(true);
            }
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "Analyze_Click");
        }

        /// <summary>
        /// Rescan selected folders
        /// </summary>
        void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "Refresh_Click");
            isNewScanStarted = true;
            ScanSelectedDrivesOrFolders(true);
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "Refresh_Click");
        }

        /// <summary>
        /// Open Windows Explorer for the selected folder
        /// </summary>
        void Explorer_Click(object sender, RoutedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "Explorer_Click");
            var selectedFile = dgFileListByFolder.SelectedItem as AppFile;
            var selectedFolder = rtvFolderList.SelectedItem as AppFolder;

            if (lastFocusedList == dgFileListByFolder && selectedFile != null)
            {
                try
                {
                    Process.Start(selectedFile.FolderPath);
                }
                catch
                {
                    Process.Start("explorer.exe");
                }
            }
            else if (lastFocusedList == rtvFolderList && selectedFolder != null)
            {
                try
                {
                    Process.Start(selectedFolder.FullPath);
                }
                catch
                {
                    Process.Start("explorer.exe");
                }
            }
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "Explorer_Click");
        }

        void CommandPrompt_Click(object sender, RoutedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "CommandPrompt_Click");
            var selectedFile = dgFileListByFolder.SelectedItem as AppFile;
            var selectedFolder = rtvFolderList.SelectedItem as AppFolder;

            if (lastFocusedList == dgFileListByFolder && selectedFile != null)
            {
                try
                {
                    Process.Start("CMD.exe", "/k cd /D " + selectedFile.FolderPath);
                }
                catch
                {
                    Process.Start("CMD.exe");
                }
            }
            else if (lastFocusedList == rtvFolderList && selectedFolder != null)
            {
                try
                {
                    Process.Start("CMD.exe", "/k cd /D " + selectedFolder.FullPath);
                }
                catch
                {
                    Process.Start("CMD.exe");
                }
            }
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "CommandPrompt_Click");
        }

        void Properties_Click(object sender, RoutedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "Properties_Click");
            var selectedFile = dgFileListByFolder.SelectedItem as AppFile;
            var selectedFolder = rtvFolderList.SelectedItem as AppFolder;

            if (lastFocusedList == dgFileListByFolder && selectedFile != null)
            {
                try
                {
                    string filePath = selectedFile.FolderPath + "\\" + selectedFile.FileName;
                    filePath = filePath.Replace("\\\\", "\\");

                    Utility.ShowFileProperties(filePath);
                }
                catch (Exception ex)
                {
                    LogClass.AddErrorToLog(" Method - Properties_Click - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                }
            }
            else if (lastFocusedList == rtvFolderList && selectedFolder != null)
            {
                try
                {
                    string folderPath = selectedFolder.FullPath;
                    folderPath = folderPath.Replace("\\\\", "\\");

                    Utility.ShowFileProperties(folderPath);
                }
                catch (Exception ex)
                {
                    LogClass.AddErrorToLog(" Method - Properties_Click - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                }
            }
            else
            {
                if (selectedFile != null)
                {
                    try
                    {
                        string filePath = selectedFile.FolderPath + "\\" + selectedFile.FileName;
                        filePath = filePath.Replace("\\\\", "\\");

                        Utility.ShowFileProperties(filePath);
                    }
                    catch (Exception ex)
                    {
                        LogClass.AddErrorToLog(" Method - Properties_Click - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                    }
                }
            }
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "Properties_Click");
        }

        void Delete_Click(object sender, RoutedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "Delete_Click");
            if (lastFocusedList == rtvFolderList && rtvFolderList.SelectedItem != null)
            {
                var selectedFolder = rtvFolderList.SelectedItem as AppFolder;
                var d = new DirectoryInfo(selectedFolder.FullPath);
                MessageBoxResult result = MessageBox.Show(Resx.DeleteFolderConfirmation.Replace("{1}", d.Name), Resx.DeleteFolder,
                                                          MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        d.Delete(true);
                        FindAndDeleteDirectory(d, directories);
                        dgFileListByFolder.UpdateLayout();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Resx.NoRightsToDelete, Resx.DiskAnalysis, MessageBoxButton.OK, MessageBoxImage.Error);
                        LogClass.AddErrorToLog(" Method - Delete_Click - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                    }
                }
            }
            else if (lastFocusedList == dgFileListByFolder && dgFileListByFolder.SelectedItem != null)
            {
                var selectedFile = dgFileListByFolder.SelectedItem as AppFile;
                if (selectedFile != null)
                {
                    var f = new FileInfo(selectedFile.FilePath);

                    MessageBoxResult result = MessageBox.Show(Resx.DeleteFileConfirmation.Replace("{1}", selectedFile.FileName),
                                                              Resx.DeleteFile, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    string tmp = f.Attributes.ToString();

                    MessageBoxResult qSystem;
                    MessageBoxResult qHidden;

                    if (f.Attributes.ToString().Contains("System"))
                    {
                        qSystem = MessageBox.Show(Resx.DeleteSystemFile,
                                                               Resx.DeleteFile, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    }
                    else
                    {
                        qSystem = MessageBoxResult.Yes;
                    }
                    if ((result == MessageBoxResult.Yes) && f.Attributes.ToString().Contains("Hidden"))
                    {
                        qHidden = MessageBox.Show(Resx.DeleteHiddenFile,
                                                               Resx.DeleteFile, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    }
                    else
                    {
                        qHidden = MessageBoxResult.Yes;
                    }

                    //MessageBoxResult result = MessageBox.Show(Resx.DeleteFileConfirmation + " " + selectedFile.FileName + "?",
                    //                                        Resx.DeleteFile, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if ((result == MessageBoxResult.Yes) && (qSystem == MessageBoxResult.Yes) && (qHidden == MessageBoxResult.Yes))
                    {

                        int fileIndex = GetFileIndex(f);
                        if (fileIndex != -1)
                        {
                            try
                            {
                                var selectedFolder = rtvFolderList.SelectedItem as AppFolder;
                                f.Delete();
                                if (selectedFolder != null)
                                {
                                    AppFile fileToRemove = selectedFolder.Files.Where(fl => fl.FileName == f.Name).FirstOrDefault();
                                    // Delete the file from rtvFolderList itemsource
                                    selectedFolder.Files.Remove(fileToRemove);
                                }

                                if (selectedFolder != null) dgFileListByFolder.ItemsSource = selectedFolder.Files;
                                dgFileListByFolder.UpdateLayout();




                                //    AppFolder fol = directories.Where(dir=>dir.FullPath == f.DirectoryName).FirstOrDefault();
                                //     string tmp = fol.FilesCount +"";

                                //need to delete from directories, the same file being deleted in the rest of the lists
                                // the file is  f  , directories is the obsev collection
                                // filepath is full path
                                string filePath = f.DirectoryName;
                                var currentDir = new AppFolder { SubFolders = directories };

                                while (currentDir != null && currentDir.FullPath != filePath)
                                {
                                    currentDir = currentDir.SubFolders.FirstOrDefault(s => filePath != null && filePath.StartsWith(s.FullPath));
                                }

                                if (currentDir != null)
                                {
                                    AppFile tobeRem = currentDir.Files.Where(file => file.FileName == f.Name).FirstOrDefault();
                                    currentDir.Files.Remove(tobeRem);
                                }
                                rtvFolderList.ItemsSource = directories;
                                rtvFolderList.UpdateLayout();

                                // currentDir here contains the file 

                                //dgFileListByFolder.ItemsSource = selectedFolder.Files.OrderByDescending(files => files.lFileSize);
                                //dgFileListByFolder.UpdateLayout();

                                AppFileType extensionList = fileTypeList.Where(ext => ext.FileType == f.Extension.ToLower()).FirstOrDefault();
                                if (extensionList != null)
                                {
                                    List<AppFile> extensionFiles = extensionList.Files;
                                    AppFile fileToRemoveExt =
                                        extensionFiles.Where(fl => fl.FolderPath == f.DirectoryName && fl.FileName == f.Name).FirstOrDefault();
                                    extensionFiles.Remove(fileToRemoveExt);
                                }


                                //dgFileTypeList.UnselectAll();
                                //remove it from the below grid
                                if (rtvFolderList.SelectedItem == null)
                                {
                                    if (extensionList != null)
                                        dgFileListByFolder.ItemsSource = extensionList.Files.OrderByDescending(file => file.LFileSize);
                                    dgFileListByFolder.UpdateLayout();
                                }
                                dgFileTypeList.ItemsSource = fileTypeList.OrderByDescending(files => files.lTotalFileSize);
                                dgFileTypeList.UpdateLayout();

                                // TODO: Refreshing the info from correspondent element
                            }
                            catch (UnauthorizedAccessException)
                            {
                                MessageBox.Show(Resx.NoRightsToDelete, Resx.DiskAnalysis, MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                            catch (System.IO.IOException exception)
                            {
                                MessageBox.Show(Resx.FileInUse, Resx.DiskAnalysis, MessageBoxButton.OK, MessageBoxImage.Error);
                                LogClass.AddErrorToLog(" Method - Delete_Click - Exeption [" + exception.GetType().Name + "] - " + exception.Message);
                            }
                            catch (Exception ex)
                            {
                                LogClass.AddErrorToLog(" Method - Delete_Click - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                            }
                        }
                    }
                }
            }
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "Delete_Click");
        }

        void FindAndDeleteDirectory(DirectoryInfo d, ObservableCollection<AppFolder> context)
        {
            foreach (AppFolder item in context)
            {
                if (item.FullPath == d.FullName)
                {
                    context.Remove(item);
                    break;
                }
                if (item.SubFolders.Count > 0)
                {
                    FindAndDeleteDirectory(d, item.SubFolders);
                }
            }
        }

        int GetFileIndex(FileInfo f)
        {
            var i = 0;
            foreach (AppFile item in dgFileListByFolder.Items)
            {
                if (item.FilePath == f.FullName)
                    return i;
                i++;
            }
            return -1;
        }

        void Window_Closing(object sender, CancelEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "Window_Closing");
            scanDialog = null;
            directories = null;
            rtvFolderList.ItemsSource = null;
            dgFileListByFolder.ItemsSource = null;
            isAborted = true;
            bwDiskScanner.CancelAsync();
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "Window_Closing");
        }

        void dgFileListByFolder_GotFocus(object sender, RoutedEventArgs e)
        {
            lastFocusedList = sender as FrameworkElement;
        }

        /// <summary>
        /// Updates the file list to show the contents of the selected folder
        /// </summary>
        void rtvFolderList_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            dgFileTypeList.UnselectAll();

            lastFocusedList = sender as FrameworkElement;

            var selectedFolder = rtvFolderList.SelectedItem as AppFolder;
            if (selectedFolder != null)
                dgFileListByFolder.ItemsSource = selectedFolder.Files.OrderByDescending(f => f.LFileSize);
            dgFileListByFolder.UpdateLayout();
        }

        /// <summary>
        /// Updates the file list to show the contents of the selected file type
        /// </summary>
        void dgFileTypeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rtvFolderList.UnselectAll();

            lastFocusedList = sender as FrameworkElement;

            try
            {
                var selectedFileType = dgFileTypeList.SelectedItem as AppFileType;
                dgFileListByFolder.ItemsSource = selectedFileType.Files.OrderByDescending(f => f.LFileSize);
                dgFileListByFolder.UpdateLayout();
            }
            catch (Exception ex)
            {
                LogClass.AddErrorToLog(" Method - dgFileTypeList_SelectionChanged - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
            }
        }

        void dgFileTypeList_GotFocus(object sender, RoutedEventArgs e)
        {
            rtvFolderList.UnselectAll();
        }

        void rtvFolderList_GotFocus(object sender, RoutedEventArgs e)
        {
            dgFileTypeList.UnselectAll();
        }

        void rtvFolderList_MouseEnter(object sender, MouseEventArgs e)
        {
            //	rtvFolderList.Focus();
        }

        void dgFileListByFolder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        void dgFileListByFolder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "dgFileListByFolder_MouseDoubleClick");
            var selectedFile = dgFileListByFolder.SelectedItem as AppFile;
            try
            {
                if (selectedFile != null) Process.Start(selectedFile.FilePath);
            }
            catch (Exception ex)
            {
                LogClass.AddErrorToLog(" Method - SetSelectionInGrid - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
            }
        }
    }
}