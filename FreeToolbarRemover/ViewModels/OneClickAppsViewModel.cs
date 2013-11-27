using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using FreemiumUtilities.Infrastructure;
using FreeToolbarRemover.Models;
using FreeToolbarRemover.Routine;
using Microsoft.Win32.TaskScheduler;
using Action = System.Action;
using FreemiumUtilities.IEToolbarRemover;
using FreemiumUtilities.MozillaToolbarRemover;
using FreemiumUtilities.ChromeToolbarRemover;
using FreemiumUtilities.Spyware;

namespace FreeToolbarRemover.ViewModels
{
    /// <summary>
    /// The <see cref="FreeToolbarRemover.ViewModels"/> namespace contains a set of viewmodel classes
    /// of the <see cref="FreeToolbarRemover"/> project
    /// </summary>

    internal class OneClickAppsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Static method to update OneClickAppsRunningQueue
        /// </summary>
        public static void UpdateOneClickAppsRunningQueue(OneClickAppViewModel unckeckedOneClickApp)
        {
            if (!OneClickAppsRunningQueue.Any() ||
                (OneClickAppsRunningQueue.Any(a => a.Status == OneClickAppStatus.ScanFinishedOK) &&
                    OneClickAppsRunningQueue.All(a => a.Status != OneClickAppStatus.ScanFinishedError)))
            {
                Instance.Status = OneClickAppStatus.NotStarted;
            }
        }

        #region Constructors

        static readonly OneClickAppsViewModel instance = new OneClickAppsViewModel();

        readonly Dispatcher currentDispatcher;

        /// <summary>
        /// Contains a set of OneClick applications available for action and methods for working with it
        /// </summary>
        public OneClickAppsViewModel()
        {
            currentDispatcher = Dispatcher.CurrentDispatcher;
            runSelectedAppsCommand = new SimpleCommand
                                        {
                                            CanExecuteDelegate = x => oneClickApps.Count != 0,
                                            ExecuteDelegate = x => RunSelectedApps()
                                        };

            cancelScanCommand = new SimpleCommand
                                    {
                                        CanExecuteDelegate = x => oneClickApps.Count != 0,
                                        ExecuteDelegate = x => CancelScan()
                                    };

            runFixCommand = new SimpleCommand
                                {
                                    CanExecuteDelegate = x => oneClickApps.Count != 0,
                                    ExecuteDelegate = x => RunFix()
                                };

            cancelFixCommand = new SimpleCommand
                                {
                                    CanExecuteDelegate = x => oneClickApps.Count != 0,
                                    ExecuteDelegate = x => CancelFix()
                                };

            appsDoneCommand = new SimpleCommand
                                {
                                    CanExecuteDelegate = x => oneClickApps.Count != 0,
                                    ExecuteDelegate = x => AppsDone()
                                };

            showAppScanDetailsCommand = new SimpleCommand
                                            {
                                                CanExecuteDelegate = x => oneClickApps.Count != 0,
                                                ExecuteDelegate = ShowAppScanDetails
                                            };

            changeScheduleCommand = new SimpleCommand
                                        {
                                            CanExecuteDelegate = x => oneClickApps.Count != 0,
                                            ExecuteDelegate = x => ChangeSchedule()
                                        };

            /*
            * Schedule 
            */
            try
            {
                if (TaskManager.IsTaskScheduled("FreeToolbarRemover1ClickMaint"))
                {
                    Task task = TaskManager.GetTaskByName("FreeToolbarRemover1ClickMaint");
                    IsScheduled = task.Enabled;
                }
                else
                {
                    IsScheduled = false;
                    TaskManager.CreateDefaultTask("FreeToolbarRemover1ClickMaint", false);
                }
                string taskDescription = TaskManager.GetTaskDescription("FreeToolbarRemover1ClickMaint");

                SchedulerText = taskDescription.IndexOf(", starting") > 0
                                    ? taskDescription.Substring(0, (taskDescription.Length - taskDescription.IndexOf(", starting")) + 1)
                                    : taskDescription;
            }
            catch (Exception)
            {
            }
        }

        public static OneClickAppsViewModel Instance
        {
            get { return instance; }
        }

        #endregion

        #region Properties

        #region Apllication popup forms

        FrmSpyware frmSpyware;
        FormIEToolbarsAndAddOns frmIEToolbar;
        FormChromeToolbarsAndAddOns frmChromeToolbar;
        FormMozillaToolbarsAndAddOns frmMozillaToolbar;

        #endregion

        /// <summary>
        /// Contains all selected apps
        /// </summary>
        static readonly List<OneClickAppViewModel> OneClickAppsRunningQueue = new List<OneClickAppViewModel>();

        /// <summary>
        /// Specifies a set of OneClick applications available for action
        /// </summary>
        readonly ObservableCollection<OneClickAppViewModel> oneClickApps = new ObservableCollection<OneClickAppViewModel>();

        /// <summary>
        /// Current app instance
        /// </summary>
        OneClickAppViewModel currentApp;

        bool isScheduled;

        /// <summary>
        /// Gets and sets current operation progress value
        /// </summary>
        double progressValue;

        string schedulerText;

        /// <summary>
        /// Gets and sets current scan info
        /// </summary>
        OneClickAppStatus status;

        /// <summary>
        /// Gets and sets current scan info
        /// </summary>
        string statusText;

        /// <summary>
        /// Gets and sets current scan info
        /// </summary>
        string statusTextKey;

        /// <summary>
        /// Gets and sets current scan info
        /// </summary>
        string statusTitle;

        /// <summary>
        /// Gets and sets current scan info
        /// </summary>
        string statusTitleKey;

        public OneClickAppViewModel CurrentApp
        {
            get { return currentApp; }
            set
            {
                currentApp = value;
                OnPropertyChanged("CurrentApp");
            }
        }

        public bool IsScheduled
        {
            get { return isScheduled; }
            set
            {
                isScheduled = value;
                OnPropertyChanged("IsScheduled");
            }
        }

        public string SchedulerText
        {
            get { return schedulerText; }
            set
            {
                schedulerText = value;
                OnPropertyChanged("SchedulerText");
            }
        }

        public double ProgressValue
        {
            get { return progressValue; }
            set
            {
                progressValue = value;
                OnPropertyChanged("ProgressValue");
            }
        }

        public OneClickAppStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public string StatusText
        {
            get { return statusText; }
            set
            {
                statusText = value;
                OnPropertyChanged("StatusText");
            }
        }

        public string StatusTextKey
        {
            get { return statusTextKey; }
            set
            {
                statusTextKey = value;
                OnPropertyChanged("StatusTextKey");
            }
        }

        public string StatusTitle
        {
            get { return statusTitle; }
            set
            {
                statusTitle = value;
                OnPropertyChanged("StatusTitle");
            }
        }

        public string StatusTitleKey
        {
            get { return statusTitleKey; }
            set
            {
                statusTitleKey = value;
                OnPropertyChanged("StatusTitleKey");
            }
        }

        public ObservableCollection<OneClickAppViewModel> OneClickApps
        {
            get { return oneClickApps; }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Apps done
        /// </summary>
        readonly ICommand appsDoneCommand;

        /// <summary>
        /// Cancel OneClick app fix
        /// </summary>
        readonly ICommand cancelFixCommand;

        /// <summary>
        /// Cancel OneClick app execution
        /// </summary>
        readonly ICommand cancelScanCommand;

        /// <summary>
        /// Show Schedule popup
        /// </summary>
        readonly ICommand changeScheduleCommand;

        /// <summary>
        /// Run OneClick apps action
        /// </summary>
        readonly ICommand runFixCommand;

        /// <summary>
        /// Run OneClick apps checked in UI
        /// </summary>
        readonly ICommand runSelectedAppsCommand;

        /// <summary>
        /// Show app scan details
        /// </summary>
        readonly ICommand showAppScanDetailsCommand;

        /// <summary>
        /// Show Track Eraser Options popup
        /// </summary>
        readonly ICommand showTrackCleanerOptionsCommand;

        /// <summary>
        /// Show Track Eraser Tracks popup
        /// </summary>
        readonly ICommand showTrackCleanerTracksCommand;

        public ICommand RunSelectedAppsCommand
        {
            get { return runSelectedAppsCommand; }
        }

        public ICommand CancelScanCommand
        {
            get { return cancelScanCommand; }
        }

        public ICommand RunFixCommand
        {
            get { return runFixCommand; }
        }

        public ICommand CancelFixCommand
        {
            get { return cancelFixCommand; }
        }

        public ICommand AppsDoneCommand
        {
            get { return appsDoneCommand; }
        }

        public ICommand ShowAppScanDetailsCommand
        {
            get { return showAppScanDetailsCommand; }
        }

        public ICommand ShowTrackCleanerOptionsCommand
        {
            get { return showTrackCleanerOptionsCommand; }
        }

        public ICommand ShowTrackCleanerTracksCommand
        {
            get { return showTrackCleanerTracksCommand; }
        }

        public ICommand ChangeScheduleCommand
        {
            get { return changeScheduleCommand; }
        }

        void ThreadRunSelectedApps()
        {
            // Start queue from first app
            CurrentApp = OneClickAppsRunningQueue[0];
            CurrentApp.Status = OneClickAppStatus.ScanStarted;
            CurrentApp.StatusText = WPFLocalizeExtensionHelpers.GetUIString("Analyzing");
            CurrentApp.StatusTextKey = "Analyzing";

            Instance.Status = OneClickAppStatus.ScanStarted;
            StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("NowScanning");
            StatusTitleKey = "NowScanning";

            ThreadPool.QueueUserWorkItem(
                x =>
                {
                    if (CurrentApp != null)
                    {
                        try
                        {
                            CurrentApp.Instance.StartScan(UpdateProgressBar, AppScanComplete, CancelComplete, false);
                        }
                        catch (Exception)
                        {
                            AppScanComplete(false);
                            // ToDo: send exception details via SmartAssembly bug reporting!
                        }
                    }
                });
        }

        void RunSelectedApps()
        {
            ProgressValue = 0;
            IEnumerable<OneClickAppViewModel> selectedApps = OneClickApps.Where(a => a.Selected);
            OneClickAppsRunningQueue.Clear();
            foreach (OneClickAppViewModel oneClickApp in selectedApps)
            {
                OneClickAppsRunningQueue.Add(oneClickApp);
            }
            if (selectedApps.Any())
            {
                ThreadRunSelectedApps();
            }
            else
            {
                MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("SelectItemsToStartScanText"),
                                WPFLocalizeExtensionHelpers.GetUIString("SelectItemsToStartScanCaption"), MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        void CancelScan()
        {
            DoCancel();
        }

        void RunFix()
        {
            ProgressValue = 0;
            MessageBoxResult chc = MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("WouldYouLikeToCreateRestorePoint"),
                                       WPFLocalizeExtensionHelpers.GetUIString("SystemRestore"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (chc == MessageBoxResult.Yes)
            {
                try
                {
                    RestoreProgressStart();

                    if (!_bRestoreSucess)
                    {
                        MessageBoxResult msg = MessageBox.Show(
                            WPFLocalizeExtensionHelpers.GetUIString("SystemRestoreUnavailableRunFixAnyway"),
                            WPFLocalizeExtensionHelpers.GetUIString("RestoreDisabled"),
                            MessageBoxButton.YesNo);
                        if (msg != MessageBoxResult.Yes)
                        {
                            CancelComplete();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // ToDo: send exception details via SmartAssembly bug reporting!
                }
            }

            foreach (OneClickAppViewModel oneClickApp in OneClickAppsRunningQueue.ToList())
            {
                if (oneClickApp.Status != OneClickAppStatus.ScanFinishedError)
                {
                    OneClickAppsRunningQueue.Remove(oneClickApp);
                }
            }

            if (OneClickAppsRunningQueue.Any())
            {
                //Set CurrentApp to the first element of OneClickAppsRunningQueue, becouse we will need it in the Repair process
                CurrentApp = OneClickAppsRunningQueue.FirstOrDefault();

                StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("NowFixing");
                StatusTitleKey = "NowFixing";

                Instance.Status = OneClickAppStatus.FixStarted;
                OneClickAppsRunningQueue[0].Status = OneClickAppStatus.FixStarted;

                ThreadPool.QueueUserWorkItem(
                    x =>
                    {
                        try
                        {
                            OneClickAppsRunningQueue[0].Instance.StartFix(UpdateProgressBar);
                        }
                        catch (Exception)
                        {
                            AppScanComplete(false); // This function is used also after fix (rename?).
                            // ToDo: send exception details via SmartAssembly bug reporting!
                        }
                    });

            }
            else
                Instance.Status = OneClickAppStatus.NotStarted;
        }

        #region System Restore

        BusyWindow busyWindow;
        Thread busyThread;
        static long lSeqNum;
        static DateTime _dTime;
        static TimeSpan _tTimeElapsed;
        static bool _bRestoreComplete;
        static bool _bRestoreSucess;
        BackgroundWorker _oProcessAsyncBackgroundWorker;

        void RestoreProgressStart()
        {
            try
            {
                ShowProcessing();
                _bRestoreComplete = false;
                _bRestoreSucess = false;

                _oProcessAsyncBackgroundWorker = new BackgroundWorker { WorkerSupportsCancellation = true };
                _oProcessAsyncBackgroundWorker.DoWork += _oProcessAsyncBackgroundWorker_DoWork;
                _oProcessAsyncBackgroundWorker.RunWorkerCompleted += _oProcessAsyncBackgroundWorker_RunWorkerCompleted;
                _oProcessAsyncBackgroundWorker.RunWorkerAsync();

                _dTime = DateTime.Now;
                while (_bRestoreComplete == false)
                {
                    DoEvents();
                    _tTimeElapsed = DateTime.Now.Subtract(_dTime);
                    double safe = _tTimeElapsed.TotalSeconds;
                    // break at 5 minutes, something has gone wrong
                    if (safe > 300)
                    {
                        break;
                    }
                }

                HideProcessing();
            }
            catch (Exception ex)
            {
            }
        }

        static void _oProcessAsyncBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _bRestoreComplete = true;
            //SysRestore.EndRestore(lSeqNum);
            Thread.EndCriticalRegion();
        }

        static void _oProcessAsyncBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _bRestoreComplete = false;
            //_bRestoreSucess = _Restore.StartRestore("Registry Cleaner Restore Point");
            Thread.BeginCriticalRegion();
            //SysRestore.StartRestore("Free Toolbar Remover " + DateTime.Now, out lSeqNum);

            // ToDo: only set this to true if it really was successful!
            _bRestoreSucess = true;
        }

        /// <summary>
        /// Shows Processing form
        /// </summary>
        public void ShowProcessing()
        {
            busyThread = new Thread(() =>
            {
                busyWindow = new BusyWindow();
                busyWindow.Show();
                busyWindow.Closed += (s, e) => busyWindow.Dispatcher.InvokeShutdown();
                Dispatcher.Run();
            });
            busyThread.SetApartmentState(ApartmentState.STA);
            busyThread.Start();
        }

        /// <summary>
        /// Hides Processing form
        /// </summary>
        public void HideProcessing()
        {
            busyThread.Abort();
        }

        #endregion

        void CancelFix()
        {
            DoCancel();
        }

        void AppsDone()
        {
            foreach (OneClickAppViewModel oneClickApp in OneClickApps)
            {
                oneClickApp.ResetStatus();
            }
            Instance.Status = OneClickAppStatus.NotStarted;
        }

        void ShowAppScanDetails(object appInstance)
        {
            Type currentAppInstanceType = appInstance.GetType();
            if (currentAppInstanceType == typeof(SpywareRemoverApp) && frmSpyware != null)
            {
                frmSpyware.ShowDialog();
            }
            else if (currentAppInstanceType == typeof(IEToolbarAndAddOnRemoverApp) && frmIEToolbar != null)
            {
                frmIEToolbar.ShowDialog();
            }
            else if (currentAppInstanceType == typeof(MozillaToolbarAndAddOnRemoverApp) && frmMozillaToolbar != null)
            {
                frmMozillaToolbar.ShowDialog();
            }
            else if (currentAppInstanceType == typeof(ChromeToolbarAndAddOnRemoverApp) && frmChromeToolbar != null)
            {
                frmChromeToolbar.ShowDialog();
            }
        }

        void ChangeSchedule()
        {
            FormTaskManager taskManager = new FormTaskManager();
            taskManager.ShowDialog();
        }

        #endregion

        #region IE Toolbar Remover

        void IEToolbarRemoverScanFinished(bool fixAfterScan)
        {
            IEToolbarAndAddOnRemoverApp ieToolbarRemover = ((IEToolbarAndAddOnRemoverApp)CurrentApp.Instance);
            frmIEToolbar = new FormIEToolbarsAndAddOns { ExplorerToolbarAndAddOnFound = ieToolbarRemover.IEToolbarAndAddOnFound };
            CurrentAppScanFinished(CurrentApp.Instance.ProblemsCount.ToString(), CurrentApp.Instance.ProblemsCount == 0,
                                    fixAfterScan);
        }

        #endregion

        #region Mozilla Toolbar Remover

        void MozillaToolbarRemoverScanFinished(bool fixAfterScan)
        {
            MozillaToolbarAndAddOnRemoverApp mozillaToolbarRemover = ((MozillaToolbarAndAddOnRemoverApp)CurrentApp.Instance);
            frmMozillaToolbar = new FormMozillaToolbarsAndAddOns { MozillaToolbarAndAddOnFound = mozillaToolbarRemover.MozillaToolbarAndAddOnFound };
            CurrentAppScanFinished(CurrentApp.Instance.ProblemsCount.ToString(), CurrentApp.Instance.ProblemsCount == 0,
                                    fixAfterScan);
        }

        #endregion

        #region Chrome Toolbar Remover

        void ChromeToolbarRemoverScanFinished(bool fixAfterScan)
        {
            ChromeToolbarAndAddOnRemoverApp chromeToolbarRemover = ((ChromeToolbarAndAddOnRemoverApp)CurrentApp.Instance);
            frmChromeToolbar = new FormChromeToolbarsAndAddOns { ChromeToolbarAndAddOnFound = chromeToolbarRemover.ChromeToolbarAndAddOnFound };
            CurrentAppScanFinished(CurrentApp.Instance.ProblemsCount.ToString(), CurrentApp.Instance.ProblemsCount == 0,
                                    fixAfterScan);
        }

        #endregion

        #region Spyware Remover

        void SpywareScanFinished(bool fixAfterScan)
        {
            SpywareRemoverApp spywareRemover = ((SpywareRemoverApp)CurrentApp.Instance);
            frmSpyware = new FrmSpyware { SpywareFound = spywareRemover.SpywareFound };
            CurrentAppScanFinished(CurrentApp.Instance.ProblemsCount.ToString(), CurrentApp.Instance.ProblemsCount == 0,
                                    fixAfterScan);
        }

        #endregion

        #region Methods

        void CurrentAppScanFinished(string problemsCount, bool scanResultOK, bool fixAfterScan)
        {
            if (scanResultOK)
            {
                ProgressValue = 0;
                CurrentApp.Status = OneClickAppStatus.ScanFinishedOK;
                CurrentApp.StatusText = WPFLocalizeExtensionHelpers.GetUIString("NoProblemsFound");
                CurrentApp.StatusTextKey = "NoProblemsFound";
                StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("ScanComplete");
                StatusTitleKey = "ScanComplete";
                StatusText = CurrentApp.StatusText;
                StatusTextKey = "NoProblemsFound";
            }
            else
            {
                CurrentApp.Status = OneClickAppStatus.ScanFinishedError;
                CurrentApp.StatusText = String.Format("{0} " + WPFLocalizeExtensionHelpers.GetUIString("ProblemsFound"),
                                                        problemsCount);
                CurrentApp.StatusTextKey = "ProblemsFound";
                StatusTitle = "";
                StatusTitleKey = "";
                StatusText = WPFLocalizeExtensionHelpers.GetUIString("IssuesFound");
                StatusTextKey = "IssuesFound";
            }

            ProcessOneClickAppsRunningQueue(fixAfterScan);
        }

        void ProcessOneClickAppsRunningQueue(bool fixAfterScan)
        {
            int newAppIndex = OneClickAppsRunningQueue.IndexOf(CurrentApp) + 1;
            if (newAppIndex < OneClickAppsRunningQueue.Count)
            {
                CurrentApp = OneClickAppsRunningQueue[newAppIndex];
                CurrentApp.Status = OneClickAppStatus.ScanStarted;
                CurrentApp.StatusText = WPFLocalizeExtensionHelpers.GetUIString("Analyzing");
                CurrentApp.StatusTextKey = "Analyzing";
                ThreadPool.QueueUserWorkItem(
                    x =>
                    {
                        if (CurrentApp != null)
                        {
                            try
                            {
                                CurrentApp.Instance.StartScan(UpdateProgressBar, AppScanComplete, CancelComplete, fixAfterScan);
                            }
                            catch (Exception)
                            {
                                AppScanComplete(fixAfterScan);
                                // ToDo: send exception details via SmartAssembly bug reporting!
                            }
                        }
                    });
                StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("NowScanning");
                StatusTitleKey = "NowScanning";
            }
            else
            // This code runs only after all apps in OneClickAppsRunningQueue iterated
            {
                IEnumerable<OneClickAppViewModel> oneClickAppsToFix =
                    OneClickAppsRunningQueue.Where(a => a.Status == OneClickAppStatus.ScanFinishedError);
                if (oneClickAppsToFix.Any())
                {
                    Instance.Status = OneClickAppStatus.ScanFinishedError;
                    StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("IssuesFound");
                    StatusTitleKey = "IssuesFound";
                    StatusText = String.Empty;
                    if (fixAfterScan)
                    {
                        RunFix();
                    }
                }
                else
                {
                    Instance.Status = OneClickAppStatus.ScanFinishedOK;
                    StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("RepairComplete");
                    StatusTitleKey = "RepairComplete";
                    StatusText = String.Empty;
                }
            }
        }

        void CurrentAppFixFinished()
        {
            CurrentApp.StatusText = String.Format("{0} " + WPFLocalizeExtensionHelpers.GetUIString("ProblemsFixed"),
                                                            CurrentApp.Instance.ProblemsCount);
            CurrentApp.StatusTextKey = "ProblemsFixed";
            CurrentApp.Status = OneClickAppStatus.FixFinished;

            int newAppIndex = OneClickAppsRunningQueue.IndexOf(CurrentApp) + 1;
            if (newAppIndex < OneClickAppsRunningQueue.Count)
            {
                CurrentApp = OneClickAppsRunningQueue[newAppIndex];
                CurrentApp.Status = OneClickAppStatus.FixStarted;
                try
                {
                    StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("NowFixing");
                    StatusTitleKey = "NowFixing";
                    CurrentApp.Instance.StartFix(UpdateProgressBar);
                }
                catch (Exception)
                {
                    AppScanComplete(false); // This function is used also after fix (rename?).
                    // ToDo: send exception details via SmartAssembly bug reporting!
                }
            }
            else
            // This code runs only after all apps in OneClickAppsRunningQueue iterated
            {
                    Instance.Status = OneClickAppStatus.FixFinished;
                StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("RepairComplete");
                StatusTitleKey = "RepairComplete";
                StatusText = String.Empty;
            }

            //CurrentApp.Status = OneClickAppStatus.FixFinished;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Selects all apps and runs Scan on it
        /// </summary>
        public void SelectAllAndRun(bool fixAfterScan)
        {
            if (Instance.Status != OneClickAppStatus.ScanStarted && Instance.Status != OneClickAppStatus.FixStarted)
            {
                Instance.Status = OneClickAppStatus.NotStarted;
                foreach (OneClickAppViewModel oneClickApp in OneClickApps)
                {
                    oneClickApp.ResetStatus();
                    oneClickApp.Selected = true;
                    if (OneClickAppsRunningQueue.IndexOf(oneClickApp) == -1)
                    {
                        OneClickAppsRunningQueue.Add(oneClickApp);
                    }
                }

                CurrentApp = OneClickApps[0];
                CurrentApp.Status = OneClickAppStatus.ScanStarted;
                CurrentApp.StatusText = WPFLocalizeExtensionHelpers.GetUIString("Analyzing");
                CurrentApp.StatusTextKey = "Analyzing";

                Instance.Status = OneClickAppStatus.ScanStarted;
                StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("NowScanning");
                StatusTitleKey = "NowScanning";

                ThreadPool.QueueUserWorkItem(
                    x =>
                    {
                        if (CurrentApp != null)
                        {
                            try
                            {
                                CurrentApp.Instance.StartScan(UpdateProgressBar, AppScanComplete, CancelComplete, fixAfterScan);
                            }
                            catch (Exception)
                            {
                                AppScanComplete(fixAfterScan);
                                // ToDo: send exception details via SmartAssembly bug reporting!
                            }
                        }
                    });
            }
        }

        /// <summary>
        /// Updates current operation progress value 
        /// </summary>
        /// <param name="progressPercentage"></param>
        /// <param name="fileName"></param>
        public void UpdateProgressBar(int progressPercentage, string fileName)
        {
            try
            {
                if (progressPercentage > 0)
                    ProgressValue = progressPercentage;

                int index1 = fileName.IndexOf("\\");
                if (index1 != -1 && fileName.Length > 25)
                {
                    index1 = fileName.IndexOf("\\", index1 + 1);
                    int index2 = fileName.LastIndexOf("\\");
                    StatusText = fileName.Substring(0, index1 + 1) + "..." + fileName.Substring(index2, fileName.Length - index2);
                }
                else
                {
                    StatusText = fileName;
                }
            }
            catch
            {
            }
        }

        static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame(true);
            Dispatcher.CurrentDispatcher.BeginInvoke
            (
                DispatcherPriority.Background, (SendOrPostCallback)delegate(object arg)
                                                                        {
                                                                            DispatcherFrame f = arg as DispatcherFrame;
                                                                            f.Continue = false;
                                                                        },
                frame
            );
            Dispatcher.PushFrame(frame);
        }

        public void AppScanComplete(bool fixAfterScan)
        {
            DispatcherOperation op = currentDispatcher.BeginInvoke((Action)(() =>
            {
                try
                {
                    if (CurrentApp.Status == OneClickAppStatus.ScanStarted)
                    {
                        Type currentAppInstanceType = CurrentApp.Instance.GetType();
                        if (currentAppInstanceType == typeof(IEToolbarAndAddOnRemoverApp))
                        {
                            IEToolbarRemoverScanFinished(fixAfterScan);
                        }
                        else if (currentAppInstanceType == typeof(MozillaToolbarAndAddOnRemoverApp))
                        {
                            MozillaToolbarRemoverScanFinished(fixAfterScan);
                        }
                        else if (currentAppInstanceType == typeof(ChromeToolbarAndAddOnRemoverApp))
                        {
                            ChromeToolbarRemoverScanFinished(fixAfterScan);
                        }
                        else if (currentAppInstanceType == typeof(SpywareRemoverApp))
                        {
                            SpywareScanFinished(fixAfterScan);
                        }
                    }
                    if (CurrentApp.Status == OneClickAppStatus.FixStarted)
                    {
                        CurrentAppFixFinished();
                    }
                }
                catch
                {
                }
            }), DispatcherPriority.Normal);

            while (op.Status != DispatcherOperationStatus.Completed)
            {
                DoEvents();
            }
        }

        public void DoCancel()
        {
            StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("Cancelling");
            StatusTitleKey = "Cancelling";
            StatusText = String.Empty;

            if (OneClickAppsRunningQueue.Count > 0)
            {
                DispatcherOperation op = currentDispatcher.BeginInvoke((Action)(() =>
                {

                    foreach (OneClickAppViewModel oneClickApp in OneClickAppsRunningQueue.ToList())
                    {
                        try
                        {
                            if (oneClickApp.Status == OneClickAppStatus.ScanStarted)
                            {
                                oneClickApp.Instance.CancelScan();
                            }
                            if (oneClickApp.Status == OneClickAppStatus.FixStarted)
                            {
                                oneClickApp.Instance.CancelFix();
                            }
                        }
                        catch
                        {
                        }
                    }

                }), DispatcherPriority.Normal);

                while (op.Status != DispatcherOperationStatus.Completed)
                {
                    DoEvents();
                }
            }

        }

        public void CancelComplete()
        {
            DispatcherOperation op = currentDispatcher.BeginInvoke((Action)(() =>
                {
                    try
                    {
                        foreach (
                            OneClickAppViewModel oneClickApp in OneClickAppsRunningQueue.ToList())
                        {
                            oneClickApp.Status = OneClickAppStatus.NotStarted;
                            oneClickApp.StatusText = oneClickApp.Description;
                            oneClickApp.StatusTextKey = oneClickApp.GetType().ToString();
                            oneClickApp.Instance.ProblemsCount = 0;
                        }
                    }
                    catch
                    {
                    }
                    Instance.Status = OneClickAppStatus.NotStarted;
                }));

            while (op.Status != DispatcherOperationStatus.Completed)
            {
                DoEvents();
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion
    }
}