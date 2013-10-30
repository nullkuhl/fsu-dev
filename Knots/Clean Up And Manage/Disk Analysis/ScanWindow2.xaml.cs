using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;
using DiskAnalysis.Entities;
using DiskAnalysis.Helper;
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
using CheckBox = System.Windows.Controls.CheckBox;
using DataGrid = Microsoft.Windows.Controls.DataGrid;
using DataGridCell = Microsoft.Windows.Controls.DataGridCell;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Resources;
using DiskAnalysis.Properties;
using System.Threading;
using System.Globalization;
using FreemiumUtil;

namespace DiskAnalysis
{
    /// <summary>
    /// Interaction logic for ScanWindow.xaml
    /// </summary>
    public partial class ScanWindow2
    {
        BackgroundWorker bwDiskScanner;

        /// <summary>
        /// Drive data
        /// </summary>
        public ObservableCollection<DriveData> DriveData;

        ResourceManager rm = new ResourceManager("DiskAnalysis.Properties.Resources", typeof(Resources).Assembly);

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        /// <summary>
        /// constructor for ScanWindow
        /// </summary>
        public ScanWindow2(bool isFirstRun)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "ctor");
            InitializeComponent();
            var culture = new CultureInfo(CfgFile.Get("Lang"));
            Thread.CurrentThread.CurrentUICulture = culture;
            Loaded += ScanWindow_Loaded;
            //Closing += new CancelEventHandler(ScanWindow_Closing);
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "ctor");

            if (isFirstRun)
            {
                btnCancel.Content = rm.GetString("Close");
                //this.btnCancel.Visibility = System.Windows.Visibility.Hidden;
                //this.btnCancel
            }
            else
            {
                btnCancel.Content = rm.GetString("Cancel");
                //this.btnCancel.Visibility = System.Windows.Visibility.Visible;
            }
        }


        void ScanWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

                LogClass.AddInfoToLog(LogClass.LogInfo.Start, "ScanWindow_Loaded");

                DriveData = new ObservableCollection<DriveData>();
                bwDiskScanner = new BackgroundWorker();
                bwDiskScanner.DoWork += bwDiskScanner_DoWork;
                bwDiskScanner.RunWorkerCompleted += bwDiskScanner_RunWorkerCompleted;
                bwDiskScanner.RunWorkerAsync();

                LogClass.AddInfoToLog(LogClass.LogInfo.End, "ScanWindow_Loaded");


            }
            catch (Exception ex)
            {
                if (IntPtr.Size == 8)   // 64bit machines are unable to properly throw the errors during a Page_Loaded event.
                {
                    BackgroundWorker loaderExceptionWorker = new BackgroundWorker();
                    loaderExceptionWorker.DoWork += ((exceptionWorkerSender, runWorkerCompletedEventArgs) => { runWorkerCompletedEventArgs.Result = runWorkerCompletedEventArgs.Argument; });
                    loaderExceptionWorker.RunWorkerCompleted += ((exceptionWorkerSender, runWorkerCompletedEventArgs) => { throw (Exception)runWorkerCompletedEventArgs.Result; });
                    loaderExceptionWorker.RunWorkerAsync(ex);
                }
                else
                    throw;
            }


        }

        void bwDiskScanner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "bwDiskScanner_RunWorkerCompleted");

            rbAllDrives.IsChecked = true;

            if (!App.PassedDir)
            {
                LogClass.AddInfoToLog(LogClass.LogInfo.End, "bwDiskScanner_RunWorkerCompleted");
                return;
            }
            rbFolder.IsChecked = true;
            tbFolder.Text = App.PassDir;
            DialogResult = true;

            LogClass.AddInfoToLog(LogClass.LogInfo.End, "bwDiskScanner_RunWorkerCompleted");
        }

        void bwDiskScanner_DoWork(object sender, DoWorkEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "bwDiskScanner_DoWork");
            // Set Default control states
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(GetDriveInfo));
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "bwDiskScanner_DoWork");
        }

        /// <summary>
        /// get the list of logical drives on computer
        /// </summary>
        void GetDriveInfo()
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "GetDriveInfo");

            try
            {
                DriveInfo[] drives = null;

                try
                {
                    drives = DriveInfo.GetDrives();
                }
                catch (IOException ex)
                {
                    LogClass.AddErrorToLog(" Method - GetDriveInfo - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    LogClass.AddErrorToLog(" Method - GetDriveInfo - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                }

                if (drives != null)
                {
                    foreach (DriveInfo drive in drives)
                    {
                        try
                        {
                            if ((drive.DriveType == DriveType.Fixed || drive.DriveType == DriveType.Removable) && drive.IsReady)
                            {
                                var d = new DriveData(drive);
                                DriveData.Add(d);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogClass.AddErrorToLog(" Method - GetDriveInfo - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                        }
                    }
                   // dgDrives.ItemsSource = DriveData;
                }
            }
            catch (Exception ex)
            {
                LogClass.AddErrorToLog(" Method - GetDriveInfo - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
                // ToDo: send exception details via SmartAssembly bug reporting!
            }

            LogClass.AddInfoToLog(LogClass.LogInfo.End, "GetDriveInfo");
        }

        /// <summary>
        /// handle Click event to analyse checked locations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOK_Click(object sender, RoutedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "btnOK_Click");

            try
            {
                if ((bool)rbAllDrives.IsChecked ||
                    ((bool)rbDrives.IsChecked && IsAnyDriveSelected()) ||
                    ((bool)rbFolder.IsChecked && Directory.Exists(tbFolder.Text)))
                {
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                LogClass.AddErrorToLog(" Method - btnOK_Click - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
            }

            LogClass.AddInfoToLog(LogClass.LogInfo.End, "btnOK_Click");
        }

        /// <summary>
        /// check if any logical drive is checked
        /// </summary>
        /// <returns></returns>
        bool IsAnyDriveSelected()
        {
            bool res = false;
            try
            {
                //res = dgDrives.ItemsSource.Cast<DriveData>().Any(item => item.IsChecked);
            }
            catch (Exception ex)
            {
                LogClass.AddErrorToLog(" Method - IsAnyDriveSelected - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
            }

            return res;
        }

        /// <summary>
        /// initialize ScanWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Content moved to bwDiskScanner_RunWorkerCompleted event:
            // otherwise event rbFolder_Checked may be called before background worker has finished.
            // => NullPointerException
        }

        /// <summary>
        /// handle Click event to cancel form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Info, "btnCancel_Click");
            if (btnCancel.Content.ToString() == rm.GetString("Cancel"))
                DialogResult = false;
            else
                System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// handle Checked event to check all logical drives
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rbAllDrives_Checked(object sender, RoutedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "rbAllDrives_Checked");

            try
            {
                if (DriveData != null)
                {
                    foreach (DriveData d in DriveData)
                    {
                        d.IsChecked = true;

                        //var row = (DataGridRow)dgDrives.ItemContainerGenerator.ContainerFromItem(d);
                        //SetSelectionInGrid(row, d.IsChecked);
                    }
                }
            }
            catch (Exception ex)
            {
                LogClass.AddErrorToLog(" Method - rbAllDrives_Checked - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
            }

            LogClass.AddInfoToLog(LogClass.LogInfo.End, "rbAllDrives_Checked");
        }

        /// <summary>
        /// handle Checked event to choose individual drives
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rbDrives_Checked(object sender, RoutedEventArgs e)
        {
            //foreach (DriveData d in driveData)
            //{
            //    d.IsChecked = false;
            //    DataGridRow row = (DataGridRow)dgDrives.ItemContainerGenerator.ContainerFromItem(d);
            //    SetSelectionInGrid(row, d.IsChecked);
            //}
        }

        static void SetSelectionInGrid(DataGridRow row, bool isChecked)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "SetSelectionInGrid");

            try
            {
                if (row != null)
                {
                    var presenter = WpfExtensions.GetVisualChild<DataGridCellsPresenter>(row);
                    var cell = presenter.ItemContainerGenerator.ContainerFromIndex(0) as DataGridCell;
                    if (cell != null)
                    {
                        ((CheckBox)cell.Content).IsChecked = isChecked;
                    }
                }
            }
            catch (Exception ex)
            {
                LogClass.AddErrorToLog(" Method - SetSelectionInGrid - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
            }

            LogClass.AddInfoToLog(LogClass.LogInfo.End, "SetSelectionInGrid");
        }

        void dgDrives_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "dgDrives_SelectionChanged");

            try
            {
                var dataGrid = sender as DataGrid;
                if (dataGrid == null) return;
                var d = (dataGrid.SelectedItem as DriveData);
                if (d != null) d.IsChecked = !d.IsChecked;
                rbDrives.IsChecked = true;

                //var _row = ((DataGridRow)dgDrives.CurrentItem).
                var row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(d);
                SetSelectionInGrid(row, d.IsChecked);
                //((DriveData)dgDrives.CurrentItem).IsChecked = d.IsChecked;
            }
            catch (Exception ex)
            {
                LogClass.AddErrorToLog(" Method - dgDrives_SelectionChanged - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
            }

            LogClass.AddInfoToLog(LogClass.LogInfo.End, "dgDrives_SelectionChanged");
        }

        /// <summary>
        /// handle Click event to use folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rbFolder_Checked(object sender, RoutedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "rbFolder_Checked");
            try
            {
                if (DriveData != null)
                {
                    foreach (DriveData d in DriveData)
                    {
                        d.IsChecked = false;
                        //var row = (DataGridRow)dgDrives.ItemContainerGenerator.ContainerFromItem(d);
                        //SetSelectionInGrid(row, d.IsChecked);
                    }
                }
            }
            catch (Exception ex)
            {
                LogClass.AddErrorToLog(" Method - rbFolder_Checked - Exeption [" + ex.GetType().Name + "] - " + ex.Message);
            }
            LogClass.AddInfoToLog(LogClass.LogInfo.End, "rbFolder_Checked");
        }

        /// <summary>
        /// handle Click event to show folder browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            LogClass.AddInfoToLog(LogClass.LogInfo.Start, "btnBrowse_Click");

            var dlg = new FolderBrowserDialog();
            DialogResult result = dlg.ShowDialog(this.GetIWin32Window());

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                rbFolder.IsChecked = true;
                tbFolder.Text = dlg.SelectedPath;
            }

            LogClass.AddInfoToLog(LogClass.LogInfo.End, "btnBrowse_Click");
        }

        /// <summary>
        /// handle TextChanged event to choose folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tbFolder_TextChanged(object sender, TextChangedEventArgs e)
        {
            rbFolder.IsChecked = true;
        }
    }
}