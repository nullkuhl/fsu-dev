using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using FreemiumUtil;
using FreemiumUtilities.Infrastructure;
using FreemiumUtilities.RegCleaner;
using FreemiumUtilities.ShortcutFixer;
using FreemiumUtilities.StartupManager;
using FreemiumUtilities.TempCleaner;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using PCCleaner.Models;
using PCCleaner.Routine;
using PCCleaner.ViewModels;
using WPFLocalizeExtension.Engine;

/// <summary>
/// The <see cref="PCCleaner"/> is a root namespace of the PCCleaner project
/// </summary>
namespace PCCleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        const string PCCleaner1ClickMaintTaskName = "PCCleaner1ClickMaint";
        readonly string apppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        readonly string[] languages = { "en", "de" };
        readonly Style navigationButtonFirstSelectedStyle;
        readonly Style navigationButtonFirstStyle;
        readonly Style navigationButtonSelectedStyle;
        readonly Style navigationButtonStyle;

        /// <summary>
        /// Main window constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            string culture = CfgFile.Get("Lang");
            LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = LocalizeDictionary.Instance.Culture;
            if (CfgFile.Get("FirstRun") != "0")
            {
                FirstRun();
                CfgFile.Set("FirstRun", "0");
            }

            string path = String.Format(@"Themes\{0}\Theme.xaml", CfgFile.Get("Theme"));
            try
            {
                using (FileStream fs = new FileStream(FileRW.GetAssemblyDirectory() + path, FileMode.Open))
                {
                    var dic = (ResourceDictionary)XamlReader.Load(fs);
                    Application.Current.MainWindow.Resources.MergedDictionaries.Clear();
                    Application.Current.MainWindow.Resources.MergedDictionaries.Add(dic);
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("AdminRightsNeeded"), "Error", MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }

            InitModel();

            // Enable visual styles for WinForms windows
            System.Windows.Forms.Application.EnableVisualStyles();

            navigationButtonStyle = (Style)FindResource("NavigationButton");
            navigationButtonSelectedStyle = (Style)FindResource("NavigationButtonSelected");
            navigationButtonFirstStyle = (Style)FindResource("NavigationButtonFirst");
            navigationButtonFirstSelectedStyle = (Style)FindResource("NavigationButtonFirstSelected");

            OneClickMaintenancePanel.Style = navigationButtonSelectedStyle;
        }

        /// <summary>
        /// Sets the context menu registry key
        /// </summary>
        /// <param name="root">Root registry key where the settings will be stored</param>
        /// <param name="titleNode">Title of the target registry node</param>
        /// <param name="title">Title of the target registry key</param>
        /// <param name="knotCodeName">Knot identification node needed to find correspondent knot on the context menu command execution</param>
        static void SetContextMenuRegistryKey(RegistryKey root, string titleNode, string title, string knotCodeName)
        {
            try
            {
                using (RegistryKey contextMenuKey = root.CreateSubKey(titleNode))
                {
                    if (contextMenuKey != null)
                    {
                        contextMenuKey.SetValue("", title);
                        // Remove all existing subkeys (including the ones at another language)
                        foreach (string subkey in contextMenuKey.GetSubKeyNames())
                        {
                            contextMenuKey.DeleteSubKey(subkey);
                        }
                        using (RegistryKey registryKey = contextMenuKey.CreateSubKey("command"))
                        {
                            if (registryKey != null)
                                registryKey
                                    .SetValue(
                                        "",
                                        String.Format(
                                            @"{0}\freemiumContext.exe {1} %1",
                                            Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath),
                                            knotCodeName
                                            )
                                    );
                        }
                        contextMenuKey.Close();
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("AdminRightsNeeded"), "Error", MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            catch
            {
            }
        }

        /// <summary>
        /// The code runned on a first application run at the specific machine
        /// Adds the Windows Explorer context menu items for a quick launch a few knots
        /// </summary>
        static void FirstRun()
        {
            // ANALYSE
            SetContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\Drive\shell\FreemiumAnalyze\",
                                      WPFLocalizeExtensionHelpers.GetUIString("AnalyzeWithFreemiumTools"), "ANALYSE");
            SetContextMenuRegistryKey(Registry.ClassesRoot, @"Drive\shell\FreemiumAnalyze\",
                                      WPFLocalizeExtensionHelpers.GetUIString("AnalyzeWithFreemiumTools"), "ANALYSE");
            SetContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\Directory\shell\FreemiumAnalyze\",
                                      WPFLocalizeExtensionHelpers.GetUIString("AnalyzeWithFreemiumTools"), "ANALYSE");
            SetContextMenuRegistryKey(Registry.ClassesRoot, @"Directory\shell\FreemiumAnalyze\",
                                      WPFLocalizeExtensionHelpers.GetUIString("AnalyzeWithFreemiumTools"), "ANALYSE");
            CfgFile.Set("ShowContextMenuAnalyze", "1");

            // EMPTYFOLDERS
            SetContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\Drive\shell\FreemiumFindEmptyFolders\",
                                      WPFLocalizeExtensionHelpers.GetUIString("FindEmptyFolders"), "EMPTYFOLDERS");
            SetContextMenuRegistryKey(Registry.ClassesRoot, @"Drive\shell\FreemiumFindEmptyFolders\",
                                      WPFLocalizeExtensionHelpers.GetUIString("FindEmptyFolders"), "EMPTYFOLDERS");
            SetContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\Directory\shell\FreemiumFindEmptyFolders\",
                                      WPFLocalizeExtensionHelpers.GetUIString("FindEmptyFolders"), "EMPTYFOLDERS");
            SetContextMenuRegistryKey(Registry.ClassesRoot, @"Directory\shell\FreemiumFindEmptyFolders\",
                                      WPFLocalizeExtensionHelpers.GetUIString("FindEmptyFolders"), "EMPTYFOLDERS");
            CfgFile.Set("ShowContextMenuFindEmptyFolders", "1");
        }

        /// <summary>
        /// Inits the model
        /// </summary>
        void InitModel()
        {
            // Wiring up View and ViewModel
            OneClickAppsViewModel model = OneClickAppsViewModel.Instance;
            DataContext = model;

            model.OneClickApps.Add(new OneClickAppViewModel(
                                WPFLocalizeExtensionHelpers.GetUIString("RegistryCleaner"),
                                @"Images/icon-registry-cleaner.png",
                                true,
                                OneClickAppStatus.NotStarted,
                                WPFLocalizeExtensionHelpers.GetUIString("RegistryCleanerOneClickDesc"),
                                new RegistryCleanerApp()
                                ));

            model.OneClickApps.Add(new OneClickAppViewModel(
                                    WPFLocalizeExtensionHelpers.GetUIString("TemporaryFilesCleaner"),
                                    @"Images/icon-disc-cleaner.png",
                                    true,
                                    OneClickAppStatus.NotStarted,
                                    WPFLocalizeExtensionHelpers.GetUIString("TemporaryFilesCleanerDesc"),
                                    new TempCleanerApp()
                                    ));

            model.OneClickApps.Add(new OneClickAppViewModel(
                                    WPFLocalizeExtensionHelpers.GetUIString("ShortcutFixer"),
                                    @"Images/icon-shortcuts-fixer.png",
                                    true,
                                    OneClickAppStatus.NotStarted,
                                    WPFLocalizeExtensionHelpers.GetUIString("ShortcutFixerOneClickDesc"),
                                    new ShortcutFixerApp()
                                    ));

            model.OneClickApps.Add(new OneClickAppViewModel(
                                    WPFLocalizeExtensionHelpers.GetUIString("StartupManager"),
                                    @"Images/icon-startup-manager.png",
                                    true,
                                    OneClickAppStatus.NotStarted,
                                    WPFLocalizeExtensionHelpers.GetUIString("StartupManagerOneClickDesc"),
                                    new StartupManagerApp()
                                    ));
        }

        /// <summary>
        /// Needed for communication when the app runs in a silent mode
        /// </summary>
        public void LaunchPipe()
        {
            messagePipe mp = new messagePipe();
            mp.Activate();
            mp.Show();
            mp.Visible = false;

            if (App.StartMinimized)
            {
                Hide();
                WindowState = WindowState.Minimized;
                ShowInTaskbar = false;
                double height = SystemParameters.WorkArea.Height;
                double width = SystemParameters.WorkArea.Width;
                Top = (height - Height) / 2;
                Left = (width - Width) / 2;
            }
        }

        /// <summary>
        /// Options combobox selected item changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OptionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox context = (ComboBox)sender;
            if (context.SelectedIndex == 0)
            {
                FormOptions frmOptions = new FormOptions();
                frmOptions.ShowDialog();
            }
            if (context.SelectedIndex == 1)
            {
                FormRestorePoint frmRestorePoint = new FormRestorePoint();
                frmRestorePoint.ShowDialog();
            }
            if (context.SelectedIndex == 2)
            {
                OpenUrl(WPFLocalizeExtensionHelpers.GetUIString("FeedbackUrl"), e);
            }
            if (context.SelectedIndex == 3)
            {
                //Process sysInfo = new Process { StartInfo = { FileName = "SysInfo.exe" } };
                //sysInfo.Start();
                AppStarter(apppath, "SysInfo");
            }
            if (context.SelectedIndex == 4)
            {
                OpenUrl(WPFLocalizeExtensionHelpers.GetUIString("HelpUrl"), e);
            }
            // This needed to save state unchecked
            context.SelectedIndex = -1;
        }

        /// <summary>
        /// Main window loaded event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (sender != null)
                LaunchPipe();

            if (App.StartMinimized)
            {
                Hide();
                WindowState = WindowState.Minimized;
                ShowInTaskbar = false;
                double height = SystemParameters.WorkArea.Height;
                double width = SystemParameters.WorkArea.Width;
                Top = (height - Height) / 2;
                Left = (width - Width) / 2;
            }

            // Create task manager form to get current task localized description
            FormTaskManager taskManager = new FormTaskManager();
            taskManager.btnOK_Click(null, null);

            if (!App.StartMinimized)
                this.WindowState = WindowState.Normal;
        }

        /// <summary>
        /// Closes Advanced Modules at the "Go home" button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GoHome(object sender, RoutedEventArgs e)
        {
            CloseAdvancedModules(null, null);
        }

        private Point startPoint;

        private void logoButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(logoButton);
        }

        private void logoButton_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var currentPoint = e.GetPosition(logoButton);
            if (e.LeftButton == MouseButtonState.Pressed && logoButton.IsMouseCaptured &&
                (Math.Abs(currentPoint.X - startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(currentPoint.Y - startPoint.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                //Prevent click from firing
                logoButton.ReleaseMouseCapture();
                DragMove();
            }
        }

        #region Task scheduling

        /// <summary>
        /// Task scheduling enabling checkbox checked state changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((CheckBox)sender).IsChecked == true)
                {
                    Task task = TaskManager.GetTaskByName(PCCleaner1ClickMaintTaskName);
                    if (task != null)
                    {
                        TaskManager.UpdateTaskStatus(PCCleaner1ClickMaintTaskName, true);
                    }
                    else
                    {
                        TaskManager.CreateDefaultTask(PCCleaner1ClickMaintTaskName, true);
                    }

                    // Check that task was added/modified
                    task = TaskManager.GetTaskByName(PCCleaner1ClickMaintTaskName);
                    if ((task == null || (task.Enabled == false)) &&
                        (TaskManager.IsTaskScheduled(PCCleaner1ClickMaintTaskName)))
                    {
                        ((CheckBox)sender).IsChecked = false;
                        MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("AdminRightsNeeded"), "Error", MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                }
                else
                {
                    Task task = TaskManager.GetTaskByName(PCCleaner1ClickMaintTaskName);
                    if (task != null)
                    {
                        TaskManager.UpdateTaskStatus(PCCleaner1ClickMaintTaskName, false);
                    }
                    else
                    {
                        TaskManager.CreateDefaultTask(PCCleaner1ClickMaintTaskName, false);
                    }

                    // Check that task was added/modified
                    task = TaskManager.GetTaskByName(PCCleaner1ClickMaintTaskName);
                    if ((task == null || (task.Enabled)) && (TaskManager.IsTaskScheduled(PCCleaner1ClickMaintTaskName)))
                    {
                        ((CheckBox)sender).IsChecked = true;
                        MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("AdminRightsNeeded"), "Error", MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("TaskSchedulingFailedMessage"), WPFLocalizeExtensionHelpers.GetUIString("TaskSchedulingFailedTitle"),
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Scan only/Scan and fix radiobutton state changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            Task task = TaskManager.GetTaskByName(PCCleaner1ClickMaintTaskName);
            if (task != null)
            {
                TaskManager.UpdateTaskActionType(PCCleaner1ClickMaintTaskName, ScanOnly.IsChecked == true);
            }
        }

        #endregion

        #region URL links opening

        /// <summary>
        /// Opens a specified URL with a system default browser
        /// </summary>
        /// <param name="url">URL to open</param>
        /// <param name="e"></param>
        public void OpenUrl(string url, RoutedEventArgs e)
        {
            try
            {
                CommonOperations.OpenUrl(url);
            }
            catch (Exception)
            {
                MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("UrlCannotBeOpenedMessage") + Environment.NewLine + url,
                                WPFLocalizeExtensionHelpers.GetUIString("UrlCannotBeOpenedTitle"), MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            e.Handled = true;
        }

        /// <summary>
        /// Opens an AboutBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenAboutBox(object sender, RoutedEventArgs e)
        {
            AboutBox aboutBox = new AboutBox { Owner = this };

            aboutBox.Left = Left + (Width / 2 - aboutBox.Width / 2);
            int regToolsHeight = (int)aboutBox.Height;
            aboutBox.Height = 0;
            int topStart = (int)(Top + Height) + 30;
            aboutBox.Top = topStart;
            var topFinal = (int)(Top + (Height / 2 - regToolsHeight / 2));

            const int fullAnimationDuration = 300;
            int heightAnimationDuration = (fullAnimationDuration * regToolsHeight / (topStart - topFinal));

            DoubleAnimation slideUp = new DoubleAnimation
                            {
                                From = topStart,
                                To = topFinal,
                                Duration = new Duration(TimeSpan.FromMilliseconds(fullAnimationDuration))
                            };
            aboutBox.BeginAnimation(TopProperty, slideUp);

            DoubleAnimation scaleUp = new DoubleAnimation
                            {
                                From = 0,
                                To = regToolsHeight,
                                Duration = new Duration(TimeSpan.FromMilliseconds(heightAnimationDuration))
                            };
            aboutBox.BeginAnimation(HeightProperty, scaleUp);

            aboutBox.AnimateInnerBox();

            aboutBox.ShowDialog();
        }

        /// <summary>
        /// Opens the root URL of the Freemium Utilities website
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenRootUrl(object sender, RoutedEventArgs e)
        {
            OpenUrl(WPFLocalizeExtensionHelpers.GetUIString("RootUrl"), e);
        }

        /// <summary>
        /// Opens the Freemium Utilities Facebook page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenFB(object sender, RoutedEventArgs e)
        {
            OpenUrl(WPFLocalizeExtensionHelpers.GetUIString("FBUrl"), e);
        }

        /// <summary>
        /// Opens the Freemium Utilities Twitter page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenTwitter(object sender, RoutedEventArgs e)
        {
            OpenUrl(WPFLocalizeExtensionHelpers.GetUIString("TwitterUrl"), e);
        }

        /// <summary>
        /// Opens the Freemium Utilities Google+ page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenGooglePlus(object sender, RoutedEventArgs e)
        {
            OpenUrl(WPFLocalizeExtensionHelpers.GetUIString("GooglePlusUrl"), e);
        }

        #endregion

        #region Window operations

        /// <summary>
        /// Window drag event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            DragMove();
        }

        /// <summary>
        /// Closes the app's main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CloseApp(object sender, RoutedEventArgs e)
        {
            if (CfgFile.Get("MinimizeToTray") == "1")
            {
                Hide();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Closes the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AppExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Minimizes the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MinimizeApp(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            if (CfgFile.Get("MinimizeToTray") == "1")
                Hide();
        }

        #endregion

        #region Panels animation

        /// <summary>
        /// Checks if all animations was completed
        /// </summary>
        /// <returns></returns>
        bool AllAnimationsComplete()
        {
            return (PanelOneClick.Opacity == 0 || PanelOneClick.Opacity == 1) &&
                   (PanelCleanUp.Opacity == 0 || PanelCleanUp.Opacity == 1) &&
                   (PanelOneClick.Margin.Top == 0 || PanelOneClick.Margin.Top == -385) &&
                   (PanelCleanUp.Margin.Top == 0 || PanelCleanUp.Margin.Top == -385);
        }

        /// <summary>
        /// Hides the currently active panel
        /// </summary>
        void HideCurrentPanel()
        {
            FrameworkElement currentPanel = new FrameworkElement();
            if (PanelCleanUp.IsVisible)
            {
                currentPanel = PanelCleanUp;
            }
            currentPanel.Visibility = Visibility.Hidden;
            PanelOneClickShutter.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Closes all Advanced Modules
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CloseAdvancedModules(object sender, RoutedEventArgs e)
        {
            if (AllAnimationsComplete())
            {
                PanelOneClickShutter.Visibility = Visibility.Collapsed;

                ThicknessAnimation scrollPanel = new ThicknessAnimation { Duration = TimeSpan.FromSeconds(0.5), To = new Thickness(0, -385, 0, 0) };
                PanelCleanUp.BeginAnimation(MarginProperty, scrollPanel);

                PanelOneClick.Visibility = Visibility.Visible;

                PanelCleanUpHeader.Visibility = Visibility.Hidden;
                PanelOneClickHeader.Visibility = Visibility.Visible;

                LinkPanelCleanUp.Style = navigationButtonFirstSelectedStyle;
            }
        }

        /// <summary>
        /// Animates Clean Up panel
        /// </summary>
        void AnimatePanelCleanUp()
        {
            var scrollPanel = new ThicknessAnimation { Duration = TimeSpan.FromSeconds(0.5), To = new Thickness(0, 0, 0, 0) };
            PanelCleanUp.BeginAnimation(MarginProperty, scrollPanel);
        }

        /// <summary>
        /// Shows the Clean Up panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShowPanelCleanUp(object sender, RoutedEventArgs e)
        {
            if (AllAnimationsComplete() && LinkPanelCleanUp.Style == navigationButtonFirstStyle)
            {
                PanelOneClickHeader.Visibility = Visibility.Hidden;
                PanelCleanUpHeader.Visibility = Visibility.Visible;
                AnimatePanelCleanUp();
                PanelCleanUp.Visibility = Visibility.Visible;
                LinkPanelCleanUp.Style = navigationButtonFirstSelectedStyle;
                OneClickMaintenancePanel.Style = navigationButtonStyle;
            }
        }

        /// <summary>
        /// Shows the ShowOneClickMaintenancePanel panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShowOneClickMaintenancePanel(object sender, RoutedEventArgs e)
        {
            if (AllAnimationsComplete() && OneClickMaintenancePanel.Style == navigationButtonStyle)
            {
                CloseAdvancedModules(null, null);
                LinkPanelCleanUp.Style = navigationButtonFirstStyle;
                OneClickMaintenancePanel.Style = navigationButtonSelectedStyle;
            }
        }


        /// <summary>
        /// Fires when the UI elemnt is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ElementLoaded(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)sender).SetValue(VisibilityAnimation.AnimationTypeProperty,
                                                 VisibilityAnimation.AnimationType.Fade);
        }

        #endregion

        #region EventHandlers

        [DllImport("User32.dll", EntryPoint = "ShowWindowAsync")]
        static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        /// <summary>
        /// Runs the passed executable file
        /// </summary>
        /// <param name="filepath">Executable file path</param>
        public void AppStarter(string filepath, string filename)
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                if (theprocess.ProcessName == filename)
                {
                    try
                    {
                        Interaction.AppActivate(theprocess.Id);
                    }
                    catch
                    {
                        if (filename == "MemoryOptimizer")
                        {
                            MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("running_tray") + ".",
                                            WPFLocalizeExtensionHelpers.GetUIString("notice"), MessageBoxButton.OK,
                                            MessageBoxImage.Exclamation);
                        }
                    }
                    try
                    {
                        ShowWindowAsync(theprocess.MainWindowHandle, 1);
                    }
                    catch
                    {
                    }
                }
            }
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(filepath + "\\" + filename + ".exe");
                Process.Start(psi);
            }
            catch
            {
            }
        }

        //Cleanup & Manager
        void Hdd_Cleaner_Click(object sender, RoutedEventArgs e)
        {
            AppStarter(apppath, "DiskCleaner");
        }

        void Shortcut_Fixer_Click(object sender, RoutedEventArgs e)
        {
            AppStarter(apppath, "ShortcutsFixer");
        }

        void Uninstall_Manager_Click(object sender, RoutedEventArgs e)
        {
            AppStarter(apppath, "UninstallManager");
        }

        //Improve Performance
        public void Registry_Cleaner_Click(object sender, RoutedEventArgs e)
        {
            AppStarter(apppath, "RegistryCleaner");
        }

        public void Registry_Compactor_Click(object sender, RoutedEventArgs e)
        {
            AppStarter(apppath, "RegistryOptimizer");
        }

        //Privacy & Security
        void Empty_Folders_Finder_Click(object sender, RoutedEventArgs e)
        {
            AppStarter(apppath, "EmptyFolderFinder");
        }
        #endregion
    }
}
