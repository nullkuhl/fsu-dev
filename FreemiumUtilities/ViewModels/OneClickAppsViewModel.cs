﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using FreemiumUtilities.Infrastructure;
using FreemiumUtilities.Models;
using FreemiumUtilities.RegCleaner;
using FreemiumUtilities.Routine;
using FreemiumUtilities.ShortcutFixer;
using FreemiumUtilities.Spyware;
using FreemiumUtilities.StartupManager;
using FreemiumUtilities.TempCleaner;
using FreemiumUtilities.TracksEraser;
using Microsoft.Win32.TaskScheduler;
using Action = System.Action;

namespace FreemiumUtilities.ViewModels
{
    /// <summary>
    /// The <see cref="FreemiumUtilities.ViewModels"/> namespace contains a set of viewmodel classes
    /// of the <see cref="FreemiumUtilities"/> project
    /// </summary>
    [CompilerGenerated]
    internal class NamespaceDoc
    {
    }

    internal class OneClickAppsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Static method to update OneClickAppsRunningQueue
        /// </summary>
        public static void UpdateOneClickAppsRunningQueue(OneClickAppViewModel unckeckedOneClickApp)
        {
            //OneClickAppsRunningQueue.Remove(unckeckedOneClickApp);

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

            showTrackCleanerOptionsCommand = new SimpleCommand
                                                {
                                                    CanExecuteDelegate = x => oneClickApps.Count != 0,
                                                    ExecuteDelegate = x => ShowTrackCleanerOptions()
                                                };

            showTrackCleanerTracksCommand = new SimpleCommand
                                                {
                                                    CanExecuteDelegate = x => oneClickApps.Count != 0,
                                                    ExecuteDelegate = x => ShowTrackCleanerTracks()
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
                if (TaskManager.IsTaskScheduled("Freemium1ClickMaint"))
                {
                    Task task = TaskManager.GetTaskByName("Freemium1ClickMaint");
                    IsScheduled = task.Enabled;
                }
                else
                {
                    IsScheduled = false;
                    TaskManager.CreateDefaultTask("Freemium1ClickMaint", false);
                }
                string taskDescription = TaskManager.GetTaskDescription("Freemium1ClickMaint");

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

        //change this, it shouldn't load this all now
        FrmShortcutFixer frmShortcut;
        FrmSpyware frmSpyware;
        FrmTempCleaner frmTempCleaner;

        #endregion

        /// <summary>
        /// Contains all selected apps
        /// </summary>
        static readonly List<OneClickAppViewModel> OneClickAppsRunningQueue = new List<OneClickAppViewModel>();

        /// <summary>
        /// Specifies a set of OneClick applications available for action
        /// </summary>
        readonly ObservableCollection<OneClickAppViewModel> oneClickApps = new ObservableCollection<OneClickAppViewModel>();

        public TracksEraserApp TrackEraser;

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
                //Instance.Status = CurrentApp.Status;
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
            //foreach (
            //    OneClickAppViewModel oneClickApp in
            //        selectedApps.Where(oneClickApp => OneClickAppsRunningQueue.IndexOf(oneClickApp) == -1))
            //{
            //    OneClickAppsRunningQueue.Add(oneClickApp);
            //}
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

                if (OneClickAppsRunningQueue[0].Instance.GetType() == typeof(TracksEraserApp))
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
                }
                else
                {
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
            }
            else
                Instance.Status = OneClickAppStatus.NotStarted;
        }

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
            if (currentAppInstanceType == typeof(ShortcutFixerApp) && frmShortcut != null)
            {
                frmShortcut.ShowDialog();
            }
            if (currentAppInstanceType == typeof(StartupManagerApp) && ((StartupManagerApp)appInstance).FrmDetails != null)
            {
                ((StartupManagerApp)appInstance).FrmDetails.ShowDialog();
            }
            if (currentAppInstanceType == typeof(TempCleanerApp) && frmTempCleaner != null)
            {
                frmTempCleaner.ShowDialog();
            }
            if (currentAppInstanceType == typeof(RegistryCleanerApp) && ((RegistryCleanerApp)appInstance).FrmRegCleaner != null)
            {
                ((RegistryCleanerApp)appInstance).FrmRegCleaner.ShowDialog();
            }
            if (currentAppInstanceType == typeof(TracksEraserApp))
            {
                ((TracksEraserApp)appInstance).FrmTrackSel.ShowAll();
                ((TracksEraserApp)appInstance).FrmTrackSel.ShowDialog();
            }
        }

        void ShowTrackCleanerOptions()
        {
            TrackEraser.FrmTrackOptions.ShowDialog();
        }

        void ShowTrackCleanerTracks()
        {
            // Show Track Cleaner tracks to select
            TrackEraser.FrmTrackSel.ShowTreeView();
            TrackEraser.FrmTrackSel.ShowDialog();
        }

        void ChangeSchedule()
        {
            var taskManager = new FormTaskManager();
            taskManager.ShowDialog();
            //string taskDescription = TaskManager.GetTaskDescription("Freemium1ClickMaint");
            //now can we make a check here , to see if checkbox is empty or not, 
            // and if its empty it disabled the task in task scheduler ??
            //SchedulerText = taskDescription;
            /*
            if (taskDescription.IndexOf(", starting") > 0)
                SchedulerText = taskDescription.Substring(0, (taskDescription.Length - taskDescription.IndexOf(", starting")) + 1);
            else
                SchedulerText = taskDescription;
            */
        }

        #endregion

        #region Spyware Remover

        void SpywareScanFinished(bool fixAfterScan)
        {
            var spywareRemover = ((SpywareRemoverApp)CurrentApp.Instance);
            frmSpyware = new FrmSpyware { SpywareFound = spywareRemover.SpywareFound };
            CurrentAppScanFinished(CurrentApp.Instance.ProblemsCount.ToString(), CurrentApp.Instance.ProblemsCount == 0,
                                    fixAfterScan);
        }

        #endregion

        #region Shortcut Fixer

        void ShortcutFixerScanFinished(bool fixAfterScan)
        {
            var shortcutFixer = ((ShortcutFixerApp)CurrentApp.Instance);
            frmShortcut = new FrmShortcutFixer { BrokenShortcuts = shortcutFixer.BrokenShortcuts };
            CurrentAppScanFinished(CurrentApp.Instance.ProblemsCount.ToString(), CurrentApp.Instance.ProblemsCount == 0,
                                    fixAfterScan);
        }

        #endregion

        #region Startup Manager

        void StartupManagerScanFinished(bool fixAfterScan)
        {
            CurrentAppScanFinished(CurrentApp.Instance.ProblemsCount.ToString(), CurrentApp.Instance.ProblemsCount == 0,
                                    fixAfterScan);
        }

        #endregion

        #region Registry Cleaner

        void RegistryCleanerScanFinished(bool fixAfterScan)
        {
            CurrentAppScanFinished(CurrentApp.Instance.ProblemsCount.ToString(), CurrentApp.Instance.ProblemsCount == 0,
                                    fixAfterScan);
        }

        #endregion

        #region Track Eraser

        void TrackEraserScanFinished(bool fixAfterScan)
        {
            try
            {
                var trackEraser = (TracksEraserApp)CurrentApp.Instance;
                //CurrentAppScanFinished(trackEraser.FrmTrackSel.FilesToDeletedCount.ToString(CultureInfo.InvariantCulture), trackEraser.FrmTrackSel.FilesToDeletedCount == 0, fixAfterScan);
                CurrentAppScanFinished(trackEraser.FrmTrackSel.ItemsToDeleteAvailable.ToString(CultureInfo.InvariantCulture),
                                        trackEraser.FrmTrackSel.ItemsToDeleteAvailable == 0, fixAfterScan);
            }
            catch
            {
            }
        }

        #endregion

        #region Temp Cleaner

        void TempCleanerScanFinished(bool fixAfterScan)
        {
            var tempCleaner = ((TempCleanerApp)CurrentApp.Instance);
            frmTempCleaner = new FrmTempCleaner
                                {
                                    TmpSize = (ulong)tempCleaner.TmpSize,
                                    TmpFiles = tempCleaner.TmpFiles,
                                    WinSize = (ulong)tempCleaner.WinSize,
                                    WinFiles = tempCleaner.WinFiles,
                                    IESize = (ulong)tempCleaner.IESize,
                                    IEFiles = tempCleaner.IEFiles,
                                    FFSize = (ulong)tempCleaner.FFSize,
                                    FFFiles = tempCleaner.FFFiles,
                                    ChromeSize = (ulong)tempCleaner.ChromeSize,
                                    ChromeFiles = tempCleaner.ChromeFiles
                                };

            ulong recoverableSize = frmTempCleaner.TmpSize + frmTempCleaner.WinSize +
                                    frmTempCleaner.IESize + frmTempCleaner.FFSize +
                                    frmTempCleaner.ChromeSize;

            bool scanResultOK = frmTempCleaner.TmpFiles.Count +
                                frmTempCleaner.WinFiles.Count +
                                frmTempCleaner.IEFiles.Count +
                                frmTempCleaner.FFFiles.Count +
                                frmTempCleaner.ChromeFiles.Count == 0;

            if (scanResultOK)
            {
                ProgressValue = 0;
                CurrentApp.Status = OneClickAppStatus.ScanFinishedOK;
                Instance.Status = CurrentApp.Status;
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

                const bool tmp = true; // frmTempCleaner.ClearTmp;
                const bool win = true; //frmTempCleaner.ClearWin;
                const bool ie = true; //frmTempCleaner.ClearIE;
                const bool ff = true; //frmTempCleaner.ClearFF;
                const bool chrome = true; //frmTempCleaner.ClearChrome;

                tempCleaner.SetParams(tmp, win, ie, ff, chrome);

                CurrentApp.StatusText = String.Format("{0} " + WPFLocalizeExtensionHelpers.GetUIString("Recoverable"),
                                                        frmTempCleaner.FormatSize(recoverableSize));
                CurrentApp.StatusTextKey = "Recoverable";
                StatusTitle = "";
                StatusTitleKey = "";
                StatusText = WPFLocalizeExtensionHelpers.GetUIString("IssuesFound");
                StatusTextKey = "IssuesFound";
            }

            ProcessOneClickAppsRunningQueue(fixAfterScan);
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

                    // Start fix after scan completes if there is such option
                    if (fixAfterScan)
                    {
                        //CurrentApp.Status = OneClickAppStatus.FixStarted;
                        //CurrentApp.Instance.StartFix(new ProgressUpdate(UpdateProgressBar));
                        //StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("NowFixing;
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
            CurrentApp.Status = OneClickAppStatus.FixFinished;
            Instance.Status = CurrentApp.Status;

            if (CurrentApp.Instance.GetType() == typeof(TempCleanerApp))
            {
                ulong recoverableSize = frmTempCleaner.TmpSize + frmTempCleaner.WinSize +
                                        frmTempCleaner.IESize + frmTempCleaner.FFSize +
                                        frmTempCleaner.ChromeSize;
                CurrentApp.StatusText = String.Format("{0} " + WPFLocalizeExtensionHelpers.GetUIString("Recovered"),
                                                        frmTempCleaner.FormatSize(recoverableSize));
                CurrentApp.StatusTextKey = "Recovered";
            }
            else
            {
                if (CurrentApp.Instance.GetType() == typeof(TracksEraserApp))
                {
                    CurrentApp.StatusText = String.Format("{0}/{1} " + WPFLocalizeExtensionHelpers.GetUIString("ProblemsFixed"),
                                                            TrackEraser.FrmTrackSel.ItemsToDeleteAvailable,
                                                            TrackEraser.FrmTrackSel.ItemsToDeleteAll);

                    CurrentApp.StatusTextKey = "ProblemsFixed";
                }
                else
                {
                    CurrentApp.StatusText = String.Format("{0} " + WPFLocalizeExtensionHelpers.GetUIString("ProblemsFixed"),
                                                            CurrentApp.Instance.ProblemsCount);
                    CurrentApp.StatusTextKey = "ProblemsFixed";
                }
            }

            int newAppIndex = OneClickAppsRunningQueue.IndexOf(CurrentApp) + 1;
            if (newAppIndex < OneClickAppsRunningQueue.Count)
            {
                CurrentApp = OneClickAppsRunningQueue[newAppIndex];
                CurrentApp.Status = OneClickAppStatus.FixStarted;
                try
                {
                    CurrentApp.Instance.StartFix(UpdateProgressBar);
                }
                catch (Exception)
                {
                    AppScanComplete(false); // This function is used also after fix (rename?).
                    // ToDo: send exception details via SmartAssembly bug reporting!
                }
                StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("NowFixing");
                StatusTitleKey = "NowFixing";
            }
            else
            // This code runs only after all apps in OneClickAppsRunningQueue iterated
            {
                Instance.Status = OneClickAppStatus.FixFinished;
                StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("RepairComplete");
                StatusTitleKey = "RepairComplete";
                StatusText = String.Empty;
            }
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
            //DispatcherOperation op = currentDispatcher.BeginInvoke((Action)(() =>
            //{
                try
                {
                    if (progressPercentage > 0)
                        ProgressValue = progressPercentage;

                    if (fileName.IndexOf("\\") != -1 && fileName.Length > 25)
                    {
                        int index1 = fileName.IndexOf("\\");
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
            //}), DispatcherPriority.Normal);
            //while (op.Status != DispatcherOperationStatus.Completed)
            //{
            //    DoEvents();
            //}
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
                        if (currentAppInstanceType == typeof(SpywareRemoverApp))
                        {
                            SpywareScanFinished(fixAfterScan);
                        }
                        if (currentAppInstanceType == typeof(ShortcutFixerApp))
                        {
                            ShortcutFixerScanFinished(fixAfterScan);
                        }
                        if (currentAppInstanceType == typeof(StartupManagerApp))
                        {
                            StartupManagerScanFinished(fixAfterScan);
                        }
                        if (currentAppInstanceType == typeof(TempCleanerApp))
                        {
                            TempCleanerScanFinished(fixAfterScan);
                        }
                        if (currentAppInstanceType == typeof(RegistryCleanerApp))
                        {
                            RegistryCleanerScanFinished(fixAfterScan);
                        }
                        if (currentAppInstanceType == typeof(TracksEraserApp))
                        {
                            TrackEraserScanFinished(fixAfterScan);
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