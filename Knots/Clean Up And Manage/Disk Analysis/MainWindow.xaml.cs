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
                FileInfo[] tempFiles = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FreeSystemUtilities\\Icons").GetFiles();

                try
                {
                    foreach (FileInfo t in tempFiles)
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
                try
                {
                    DirectoryInfo curDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FreeSystemUtilities\\");
                    curDir.CreateSubdirectory("Icons");
                }
                catch
                {
                }
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
            lnkAbort.Visibility = Visibility.Hidden;
            rtvFolderList.DataLoadMode = DataLoadMode.Synchronous;

            // Initialize global variables and control defaults
            isAborted = false;
            txtFileLabel.Text = "";
            txtStatus.Text = "";
            progressBar1.Visibility = Visibility.Hidden;
            scannedFileSize = 0;

            //Event handlers
            this.ContentRendered += new EventHandler(MainWindow_ContentRendered);
        }

        /// <summary>
        /// Handles ContentRendered event of Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            scanDialog = new ScanWindow(true);
            bool? result = scanDialog.ShowDialog();
            if (result.HasValue && result == true)
            {
                isNewScanStarted = true;
                ScanSelectedDrivesOrFolders(true);
            }
        }

        /// <summary>
        /// Scans selected drives or folders
        /// </summary>
        /// <param name="result"></param>
        void ScanSelectedDrivesOrFolders(bool? result)
        {
            if (result.HasValue && result == true)
            {
                Refresh.IsEnabled = false;
                progressBar1.Value = 0;
                totalFileSizeToScan = 0;
                scannedFileSize = 0;
                ind = -1;

                directories = new ObservableCollection<AppFolder>();
                scanDirs = new List<DirectoryInfo>();

                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bwCalculateFileSize_DoWork;
                bw.RunWorkerCompleted += bwCalculateFileSize_RunWorkerCompleted;

                if (scanDialog.rbAllDrives.IsChecked == true || scanDialog.rbDrives.IsChecked == true)
                {
                    foreach (DriveData drive in scanDialog.DriveData)
                    {
                        if (scanDialog.rbAllDrives.IsChecked == true || (scanDialog.rbDrives.IsChecked == true && drive.IsChecked))
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
        }

        /// <summary>
        /// Starts analyzing work
        /// </summary>
        void StartWork()
        {
            EnableButtons(false);            ;
            if (d != null)
            {
                // Initialize the Root folders and files
                // And then call background worker
                AppFolder f = new AppFolder { Name = d.Name, FullPath = d.FullName, IsRootDirectory = true };
                directories.Add(f);

                FileSystemInfo[] filesNFolders = new FileSystemInfo[] { };
                try
                {
                    filesNFolders = d.GetFileSystemInfos();
                }
                catch
                {
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
                        DirectoryInfo dir = fileInfo as DirectoryInfo;
                        if (dir != null)
                            f.SubFolders.Add(new AppFolder
                                                {
                                                    Name = dir.Name,
                                                    FullPath = dir.FullName
                                                });
                    }
                    else if (t == typeof(FileInfo))
                    {
                        FileInfo file = fileInfo as FileInfo;
                        if (file != null)
                        {
                            AppFile af = new AppFile
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
        }

        /// <summary>
        /// Changes buttons states
        /// </summary>
        /// <param name="isEnabled">true - if the buttons should be enabled, false - otherwise</param>
        void EnableButtons(bool isEnabled)
        {
            Analyze.IsEnabled = isEnabled;
            Explorer.IsEnabled = isEnabled;
            CommandPrompt.IsEnabled = isEnabled;
            Properties.IsEnabled = isEnabled;
            Delete.IsEnabled = isEnabled;
        }

        /// <summary>
        /// Turns on/off column.CanUserSort property
        /// Needed to prevent app crash in Window XP,
        /// happens on hovering sortable column header when directories is null or empty
        /// </summary>
        void ControlSortingAbility()
        {
            bool enableUserSort = false;
            if (directories != null && directories.Count > 0)
                enableUserSort = true;

            collFileType.CanUserSort = enableUserSort;
            collPercent.CanUserSort = enableUserSort;
            collSize.CanUserSort = enableUserSort;
            collFiles.CanUserSort = enableUserSort;
            collFolderFileName.CanUserSort = enableUserSort;
            collFolderAttribute.CanUserSort = enableUserSort;
            collFolderFolder.CanUserSort = enableUserSort;
            collFolderModified.CanUserSort = enableUserSort;
            collFolderSize.CanUserSort = enableUserSort;
        }

        /// <summary>
        /// BackgroundWorker Completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bwCalculateFileSize_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StartWork();
        }

        /// <summary>
        /// BackgroundWorker job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    catch
                    {
                    }
                }
            }
            catch
            {
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
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.WorkerReportsProgress = true;

            progressBar1.IsIndeterminate = true;
            txtStatus.Text = Resx.processing;
            txtFileLabel.Text = "";
            backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Handles BackgroundWorker Completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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
            EnableButtons(true);
        }

        /// <summary>
        /// BackgroundWorker job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            FinalWork();
        }

        /// <summary>
        /// Handles ProgressChanged event of BackgroundWorker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            AppFileType folderState = e.UserState as DiskAnalysis.Entities.AppFileType;
            if (folderState != null)
            {
                txtFileLabel.Text = folderState.FileType;
            }
            progressBar1.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Does final work after the Analyzing Background completes its job
        /// </summary>
        public void FinalWork()
        {
            if (isNewScanStarted)
            {
                fileTypeList = new ObservableCollection<AppFileType>();
                isNewScanStarted = false;
            }

            GetFileTypeData(directories[ind]);
            // Calculate Totals for File Type
            long fileSizesByType = fileTypeList.Sum(fileType => fileType.lTotalFileSize);

            foreach (AppFileType fileType in fileTypeList)
            {
                try
                {
                    decimal d = Convert.ToDecimal(fileType.lTotalFileSize * 100.0);
                    fileType.dPercentage = decimal.Round(d / fileSizesByType, 2);
                }
                catch
                {
                }
            }

            // Calculate Totals for Folders
            directories[currentD].dPercent = 100;
            foreach (AppFolder fol in directories[currentD].SubFolders)
            {
                GetPercentage(fol, directories[currentD].LSize);
            }
        }

        /// <summary>
        /// Gets percentage 
        /// </summary>
        /// <param name="fol"></param>
        /// <param name="totalSize"></param>
        void GetPercentage(AppFolder fol, long totalSize)
        {
            try
            {
                decimal d = Convert.ToDecimal(fol.LSize * 100.0);
                fol.dPercent = totalSize == 0 ? 100 : decimal.Round(d / totalSize, 2);
            }
            catch
            {
            }

            foreach (AppFolder fol1 in fol.SubFolders)
            {
                GetPercentage(fol1, totalSize);
            }
        }

        /// <summary>
        /// Gets file type data
        /// </summary>
        /// <param name="f"></param>
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
                    AppFileType fileType = new AppFileType(file.FileType);
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
            BwFolderState folderState = e.UserState as BwFolderState;
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
            ind++;
            foreach (AppFolder f in directories[ind].SubFolders)
            {
                GetFilesAndFolders(f);
            }
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
            try
            {
                DirectoryInfo searchDir = new DirectoryInfo(folder.FullPath);
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
                            continue;

                        BwFolderState folderStateData = null;
                        Type t = fileInfo.GetType();
                        if (t == typeof(DirectoryInfo))
                        {
                            DirectoryInfo dir = fileInfo as DirectoryInfo;
                            AppFolder subFolder = new AppFolder { Name = dir.Name, FullPath = dir.FullName };
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
                            FileInfo file = fileInfo as FileInfo;
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
                                catch
                                {
                                }

                            }
                            try
                            {
                                if (af != null)
                                    folder.Files.Add(af);

                                if (file != null)
                                    scannedFileSize += file.Length;
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Initial data loaded event handler for the treeview to handle expansion of first element
        /// </summary>
        void rtvFolderList_DataLoaded(object sender, EventArgs e)
        {
            ICollection source = rtvFolderList.ItemsSource as ICollection;
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
            scanDialog = new ScanWindow(false);
            bool? result = scanDialog.ShowDialog();
            if (result.HasValue && result == true)
            {
                isNewScanStarted = true;
                ScanSelectedDrivesOrFolders(true);
            }
        }

        /// <summary>
        /// Rescan selected folders
        /// </summary>
        void Refresh_Click(object sender, RoutedEventArgs e)
        {
            isNewScanStarted = true;
            ScanSelectedDrivesOrFolders(true);
        }

        /// <summary>
        /// Open Windows Explorer for the selected folder
        /// </summary>
        void Explorer_Click(object sender, RoutedEventArgs e)
        {
            AppFile selectedFile = dgFileListByFolder.SelectedItem as AppFile;
            AppFolder selectedFolder = rtvFolderList.SelectedItem as AppFolder;

            if (lastFocusedList != null && selectedFile != null)
            {
                if (lastFocusedList == dgFileListByFolder)
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
                else if (lastFocusedList == rtvFolderList)
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
            }
        }

        /// <summary>
        /// Handles click event of CommandPrompt button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CommandPrompt_Click(object sender, RoutedEventArgs e)
        {
            AppFile selectedFile = dgFileListByFolder.SelectedItem as AppFile;
            AppFolder selectedFolder = rtvFolderList.SelectedItem as AppFolder;

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
        }

        /// <summary>
        /// Handles Click event of Properties button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Properties_Click(object sender, RoutedEventArgs e)
        {
            AppFile selectedFile = dgFileListByFolder.SelectedItem as AppFile;
            AppFolder selectedFolder = rtvFolderList.SelectedItem as AppFolder;

            if (lastFocusedList == dgFileListByFolder && selectedFile != null)
            {
                try
                {
                    string filePath = selectedFile.FolderPath + "\\" + selectedFile.FileName;
                    filePath = filePath.Replace("\\\\", "\\");

                    Utility.ShowFileProperties(filePath);
                }
                catch
                {
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
                catch
                {
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
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Handles click event of delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lastFocusedList == rtvFolderList && rtvFolderList.SelectedItem != null)
                {
                    AppFolder selectedFolder = rtvFolderList.SelectedItem as AppFolder;
                    try
                    {
                        DirectoryInfo d = new DirectoryInfo(selectedFolder.FullPath);
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
                            catch
                            {
                                MessageBox.Show(Resx.NoRightsToDelete, Resx.DiskAnalysis, MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                else if (lastFocusedList == dgFileListByFolder && dgFileListByFolder.SelectedItem != null)
                {
                    AppFile selectedFile = dgFileListByFolder.SelectedItem as AppFile;
                    if (selectedFile != null)
                    {
                        FileInfo f = new FileInfo(selectedFile.FilePath);

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

                        if ((result == MessageBoxResult.Yes) && (qSystem == MessageBoxResult.Yes) && (qHidden == MessageBoxResult.Yes))
                        {
                            int fileIndex = GetFileIndex(f);
                            if (fileIndex != -1)
                            {
                                try
                                {
                                    AppFolder selectedFolder = rtvFolderList.SelectedItem as AppFolder;
                                    f.Delete();
                                    if (selectedFolder != null)
                                    {
                                        AppFile fileToRemove = selectedFolder.Files.Where(fl => fl.FileName == f.Name).FirstOrDefault();
                                        // Delete the file from rtvFolderList itemsource
                                        selectedFolder.Files.Remove(fileToRemove);
                                    }

                                    if (selectedFolder != null) dgFileListByFolder.ItemsSource = selectedFolder.Files;
                                    dgFileListByFolder.UpdateLayout();
                                    string filePath = f.DirectoryName;
                                    AppFolder currentDir = new AppFolder { SubFolders = directories };

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

                                    AppFileType extensionList = fileTypeList.Where(ext => ext.FileType == f.Extension.ToLower()).FirstOrDefault();
                                    if (extensionList != null)
                                    {
                                        List<AppFile> extensionFiles = extensionList.Files;
                                        AppFile fileToRemoveExt =
                                            extensionFiles.Where(fl => fl.FolderPath == f.DirectoryName && fl.FileName == f.Name).FirstOrDefault();
                                        extensionFiles.Remove(fileToRemoveExt);
                                    }
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
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Finds and deletes directory
        /// </summary>
        /// <param name="d"></param>
        /// <param name="context"></param>
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

        /// <summary>
        /// Gets file index
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        int GetFileIndex(FileInfo f)
        {
            int i = 0;
            foreach (AppFile item in dgFileListByFolder.Items)
            {
                if (item.FilePath == f.FullName)
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Handles Window Closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Window_Closing(object sender, CancelEventArgs e)
        {
            scanDialog = null;
            directories = null;
            rtvFolderList.ItemsSource = null;
            dgFileListByFolder.ItemsSource = null;
            isAborted = true;
            bwDiskScanner.CancelAsync();
        }

        /// <summary>
        /// Handles GotFocus event of datagrid of File list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            AppFolder selectedFolder = rtvFolderList.SelectedItem as AppFolder;
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
                AppFileType selectedFileType = dgFileTypeList.SelectedItem as AppFileType;
                dgFileListByFolder.ItemsSource = selectedFileType.Files.OrderByDescending(f => f.LFileSize);
                dgFileListByFolder.UpdateLayout();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Handles GotFocus event of datagrid of File Types
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgFileTypeList_GotFocus(object sender, RoutedEventArgs e)
        {
            rtvFolderList.UnselectAll();
        }

        /// <summary>
        /// Handles GotFocus event of Folder List column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rtvFolderList_GotFocus(object sender, RoutedEventArgs e)
        {
            dgFileTypeList.UnselectAll();
        }

        /// <summary>
        /// Handles MouseDoubleClick event of file list datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgFileListByFolder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AppFile selectedFile = dgFileListByFolder.SelectedItem as AppFile;
            try
            {
                if (selectedFile != null) Process.Start(selectedFile.FilePath);
            }
            catch
            {
            }
        }
    }
}