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
    public partial class ScanWindow
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
        public ScanWindow(bool isFirstRun)
        {
            InitializeComponent();
            CultureInfo culture = new CultureInfo(CfgFile.Get("Lang"));
            Thread.CurrentThread.CurrentUICulture = culture;
            Loaded += ScanWindow_Loaded;

            if (isFirstRun)
            {
                btnCancel.Content = rm.GetString("Close");
            }
            else
            {
                btnCancel.Content = rm.GetString("Cancel");
            }
        }

        /// <summary>
        /// Handles Window Loaded event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ScanWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
                DriveData = new ObservableCollection<DriveData>();
                bwDiskScanner = new BackgroundWorker();
                bwDiskScanner.DoWork += bwDiskScanner_DoWork;
                bwDiskScanner.RunWorkerCompleted += bwDiskScanner_RunWorkerCompleted;
                bwDiskScanner.RunWorkerAsync();
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

        /// <summary>
        /// Background thread's completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bwDiskScanner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            rbAllDrives.IsChecked = true;

            if (!App.PassedDir)
            {
                return;
            }
            rbFolder.IsChecked = true;
            tbFolder.Text = App.PassDir;
            DialogResult = true;
        }

        /// <summary>
        /// Background thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bwDiskScanner_DoWork(object sender, DoWorkEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(GetDriveInfo));
        }

        /// <summary>
        /// get the list of logical drives on computer
        /// </summary>
        void GetDriveInfo()
        {
            try
            {
                DriveInfo[] drives = null;
                try
                {
                    drives = DriveInfo.GetDrives();
                }
                catch (IOException ex)
                {
                }
                catch (UnauthorizedAccessException ex)
                {
                }

                if (drives != null)
                {
                    foreach (DriveInfo drive in drives)
                    {
                        try
                        {
                            if ((drive.DriveType == DriveType.Fixed || drive.DriveType == DriveType.Removable) && drive.IsReady)
                            {
                                DriveData d = new DriveData(drive);
                                DriveData.Add(d);
                            }
                        }
                        catch
                        {
                        }
                    }
                    dgDrives.ItemsSource = DriveData;
                }
            }
            catch (Exception ex)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        /// <summary>
        /// handle Click event to analyse checked locations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)rbAllDrives.IsChecked ||
                    ((bool)rbDrives.IsChecked && IsAnyDriveSelected()) ||
                    ((bool)rbFolder.IsChecked && Directory.Exists(tbFolder.Text)))
                {
                    DialogResult = true;
                }
            }
            catch
            {
            }
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
                res = dgDrives.ItemsSource.Cast<DriveData>().Any(item => item.IsChecked);
            }
            catch
            {
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
            try
            {
                if (DriveData != null)
                {
                    foreach (DriveData d in DriveData)
                    {
                        d.IsChecked = true;
                        var row = (DataGridRow)dgDrives.ItemContainerGenerator.ContainerFromItem(d);
                        SetSelectionInGrid(row, d.IsChecked);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Sets selection in grid
        /// </summary>
        /// <param name="row">row to select</param>
        /// <param name="isChecked">true if checked, false - otherwise</param>
        static void SetSelectionInGrid(DataGridRow row, bool isChecked)
        {
            try
            {
                if (row != null)
                {
                    var presenter = WpfExtensions.GetVisualChild<DataGridCellsPresenter>(row);
                    DataGridCell cell = presenter.ItemContainerGenerator.ContainerFromIndex(0) as DataGridCell;
                    if (cell != null)
                    {
                        ((CheckBox)cell.Content).IsChecked = isChecked;
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Handles DataGridRow SelectionChanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgDrives_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                if (dataGrid == null) return;
                DriveData d = (dataGrid.SelectedItem as DriveData);
                if (d != null) d.IsChecked = !d.IsChecked;
                rbDrives.IsChecked = true;
                var row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(d);
                SetSelectionInGrid(row, d.IsChecked);
            }
            catch
            {
            }
        }

        /// <summary>
        /// handle Click event to use folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rbFolder_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DriveData != null)
                {
                    foreach (DriveData d in DriveData)
                    {
                        d.IsChecked = false;
                        DataGridRow row = (DataGridRow)dgDrives.ItemContainerGenerator.ContainerFromItem(d);
                        SetSelectionInGrid(row, d.IsChecked);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// handle Click event to show folder browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            DialogResult result = dlg.ShowDialog(this.GetIWin32Window());

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                rbFolder.IsChecked = true;
                tbFolder.Text = dlg.SelectedPath;
            }
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