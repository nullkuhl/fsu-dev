using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using FreemiumUtil;
using FreemiumUtilities.Infrastructure;
using FreemiumUtilities.RegCleaner.Models;
using FreemiumUtilities.RegCleaner.Properties;
using RegistryCleanerCore;

/// <summary>
/// The <see cref="FreemiumUtilities.RegCleaner"/> namespace defines a RegCleaner 1 Click-Maintenance application
/// </summary>
namespace FreemiumUtilities.RegCleaner
{
    /// <summary>
    /// RegCleaner 1 Click-Maintenance application <see cref="OneClickApp"/> implementation
    /// </summary>
    public class RegistryCleanerApp : OneClickApp
    {
        bool ABORT;
        ProgressUpdate callback;
        CancelComplete cancelComplete;
        ScanComplete complete;
        bool fixAfterScan;
        /// <summary>
        /// RegCleaner main form
        /// </summary>
        public FormRegCleaner FrmRegCleaner = new FormRegCleaner();

        /// <summary>
        /// constructor for RegistryCleanerApp
        /// </summary>
        public RegistryCleanerApp()
        {
            InitRegistryCategories();
            InitFields();
        }

        /// <summary>
        /// Found registry problems count
        /// </summary>
        public override int ProblemsCount { get; set; }

        /// <summary>
        /// Starts scanning
        /// </summary>
        /// <param name="callback"><see cref="ProgressUpdate"/> callback</param>
        /// <param name="complete"><see cref="ScanComplete"/> callback</param>
        /// <param name="cancelComplete"><see cref="CancelComplete"/> callback</param>
        /// <param name="fixAfterScan">A bool flag that determines is the fixing needed after finishing the scan</param>
        public override void StartScan(ProgressUpdate callback, ScanComplete complete, CancelComplete cancelComplete, bool fixAfterScan)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(CfgFile.Get("Lang"));
            if (SelectedRegistryCategoriesCount() > 0)
            {
                this.callback = callback;
                this.complete = complete;
                this.cancelComplete = cancelComplete;
                this.fixAfterScan = fixAfterScan;
                Reset();
                ABORT = false;
                ScanStart();
            }
            else
            {
                MessageBox.Show(Resources.SelectAtLeastOneItem,
                                Resources.InvalidSelection,
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// cancel scanning
        /// </summary>
        public override void CancelScan()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(CfgFile.Get("Lang"));
            ABORT = true;
            if (IsTimerOn)
            {
                ScanCancel();
            }
        }

        /// <summary>
        /// start fixing
        /// </summary>
        /// <param name="callback"><see cref="ProgressUpdate"/> callback</param>
        public override void StartFix(ProgressUpdate callback)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(CfgFile.Get("Lang"));
            this.callback = callback;

            CheckItems(true);

            RemoveItems();
            IsResetPending = true;
        }

        /// <summary>
        /// cancel fixing
        /// </summary>
        public override void CancelFix()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(CfgFile.Get("Lang"));
            ABORT = true;
        }

        #region Fields

        #region Delegates

        /// <summary>
        /// ProcessCompleted event handler 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ProcessCompletedEventHandler(object sender, RunWorkerCompletedEventArgs e);

        #endregion

        static readonly cRestore _Restore = new cRestore();
        static DispatcherTimer _aRestoreTimer;
        static int _iRestoreCounter;
        static int _iKeyCounter;
        static int lastResultsCounter;
        static int currentCategoryItemsCounter;
        static int _iSegmentCounter;
        static int _iProgressMax;
        static int removedItemsCount;
        static string _sLabel = "";
        static string _sPhase = "";
        static string _sMatch = "";
        static string _sPath = "";
        static string _sHive = "";
        static string _sSegment = "";
        static int[] _aSubScan;
        static int[] SubCategoryItemCounts;
        static DateTime _dTime;

        readonly string ControlScanTitle = Resources.ControlScanTitle;
        readonly string DeepScanTitle = Resources.DeepScanTitle;
        readonly string FontScanTitle = Resources.FontScanTitle;
        readonly string HelpScanTitle = Resources.HelpScanTitle;
        readonly string HistoryScanTitle = Resources.HistoryScanTitle;
        readonly string LibraryScanTitle = Resources.LibraryScanTitle;
        readonly string MRUScanTitle = Resources.MRUScanTitle;
        readonly string SoftwareScanTitle = Resources.SoftwareScanTitle;
        readonly string StartupScanTitle = Resources.StartupScanTitle;
        readonly string UninstallScanTitle = Resources.UninstallScanTitle;
        readonly string UserScanTitle = Resources.UserScanTitle;
        readonly string VdmScanTitle = Resources.VdmScanTitle;

        cRegScan _RegScan;
        DispatcherTimer _aUpdateTimer;
        int progressBarValue;

        /// <summary>
        /// <see cref="ProcessCompletedEventHandler"/> callback
        /// </summary>
        public event ProcessCompletedEventHandler ProcessCompleted;

        #endregion

        #region Properties

        /// <summary>
        /// Registry categories to scan & fix
        /// </summary>
        public ObservableCollection<RegistryCategory> RegistryCategories = new ObservableCollection<RegistryCategory>();
        /// <summary>
        /// RegistrySubCategories to scan & fix
        /// </summary>
        public ObservableCollection<ScanData> RegistrySubCategories = new ObservableCollection<ScanData>();
        bool IsTimerOn { get; set; }
        bool ControlScan { get; set; }
        bool UserScan { get; set; }
        bool SoftwareScan { get; set; }
        bool FontScan { get; set; }
        bool HelpScan { get; set; }
        bool LibraryScan { get; set; }
        bool StartupScan { get; set; }
        bool UninstallScan { get; set; }
        bool VdmScan { get; set; }
        bool HistoryScan { get; set; }
        bool DeepScan { get; set; }
        bool MRUScan { get; set; }
        bool IsScanLoaded { get; set; }
        bool IsResetPending { get; set; }

        #endregion

        #region Library Events

        void RegScan_CurrentPath(string hive, string path)
        {
            _sHive = hive;
            _sPath = path;
        }

        void RegScan_KeyCount()
        {
            _iKeyCounter += 1;
        }

        void RegScan_LabelChange(string phase, string label)
        {
            _sPhase = phase;
            _sLabel = label;
        }

        void RegScan_MatchItem(cLightning.ROOT_KEY root, string key, string value, string data, RESULT_TYPE id)
        {
            _sMatch = data;
            ProblemsCount += 1;
        }

        void RegScan_ProcessChange()
        {
            _iSegmentCounter += 1;
        }

        void RegScan_ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _sSegment = "Scan Complete!";
        }

        void RegScan_ScanComplete()
        {
            IsTimerOn = false;

            complete(fixAfterScan);
        }

        void RegScan_ScanCount(int count)
        {
            _iProgressMax = count;
        }

        void RegScan_StatusChange(string label)
        {
            _sSegment = label;
        }

        void RegScan_SubScanComplete(string id)
        {
            //SubProgressCounter(id);
            SubCategoryScanned(id);
        }

        void _aRestoreTimer_Tick(object sender, EventArgs e)
        {
            _iRestoreCounter += 1;
            if (_iRestoreCounter > 1000)
            {
                _iRestoreCounter = 1;
            }
        }

        void _aUpdateTimer_Tick(object sender, EventArgs e)
        {
            progressBarValue = (int)Math.Round((float)_iSegmentCounter / _iProgressMax * 100, 0);

            callback(progressBarValue, progressBarValue != 100 ? _sPath : "");

            SubProgressUpdate();

            if (IsTimerOn == false)
            {
                callback(100, "");

                ScanStop();
            }
        }

        #endregion


        #region Helpers

        void InitRegistryCategories()
        {
            RegistryCategories.Add(new RegistryCategory(ControlScanTitle));
            RegistryCategories.Add(new RegistryCategory(UserScanTitle));
            RegistryCategories.Add(new RegistryCategory(SoftwareScanTitle));
            RegistryCategories.Add(new RegistryCategory(FontScanTitle));
            RegistryCategories.Add(new RegistryCategory(HelpScanTitle));
            RegistryCategories.Add(new RegistryCategory(LibraryScanTitle));
            RegistryCategories.Add(new RegistryCategory(StartupScanTitle));
            RegistryCategories.Add(new RegistryCategory(UninstallScanTitle));
            RegistryCategories.Add(new RegistryCategory(VdmScanTitle));
            RegistryCategories.Add(new RegistryCategory(HistoryScanTitle));
            RegistryCategories.Add(new RegistryCategory(DeepScanTitle));
            RegistryCategories.Add(new RegistryCategory(MRUScanTitle));
        }

        void CheckItems(bool check)
        {
            if (RegistrySubCategories != null)
            {
                foreach (ScanData o in RegistrySubCategories)
                {
                    o.Check = check;
                }
            }
        }

        static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame(true);
            Dispatcher.CurrentDispatcher.BeginInvoke(
                DispatcherPriority.Background, (SendOrPostCallback)delegate(object arg)
                {
                    DispatcherFrame f = arg as DispatcherFrame;
                    f.Continue = false;
                },
                frame
                );
            Dispatcher.PushFrame(frame);
        }

        void InitFields()
        {
            _RegScan = new cRegScan();
            _RegScan.CurrentPath += RegScan_CurrentPath;
            _RegScan.KeyCount += RegScan_KeyCount;
            _RegScan.LabelChange += RegScan_LabelChange;
            _RegScan.MatchItem += RegScan_MatchItem;
            _RegScan.ProcessChange += RegScan_ProcessChange;
            _RegScan.ProcessCompleted += RegScan_ProcessCompleted;
            _RegScan.ScanComplete += RegScan_ScanComplete;
            _RegScan.SubScanComplete += RegScan_SubScanComplete;
            _RegScan.ScanCount += RegScan_ScanCount;
            _RegScan.StatusChange += RegScan_StatusChange;
            // text updates
            _aUpdateTimer = new DispatcherTimer { Interval = new TimeSpan(1000), IsEnabled = false };
            _aUpdateTimer.Tick += _aUpdateTimer_Tick;
            // restore timer
            _aRestoreTimer = new DispatcherTimer { Interval = new TimeSpan(5000), IsEnabled = false };
            _aRestoreTimer.Tick += _aRestoreTimer_Tick;
        }

        void ModSecVal(cLightning.ROOT_KEY RootKey, string SubKey, cSecurity.InheritenceFlags flags)
        {
            string sKey = RootKey.ToString();
            cSecurity sec = new cSecurity();
            string name = sec.UserName(cSecurity.EXTENDED_NAME_FORMAT.NameSamCompatible) ?? sec.UserName();

            sKey += @"\" + SubKey;
            sec.ChangeObjectOwnership(sKey, cSecurity.SE_OBJECT_TYPE.SE_REGISTRY_KEY);
            sec.ChangeKeyPermissions((cSecurity.ROOT_KEY)RootKey, SubKey, name, cSecurity.RegistryAccess.Registry_Full_Control,
                                     cSecurity.AccessTypes.Access_Allowed, flags);
        }

        void RemoveItems()
        {
            ABORT = false;
            bool result = false;
            try
            {
                cLightning lightning = new cLightning();
                // iterate through and remove
                ObservableCollection<ScanData> itemsToDelete = new ObservableCollection<ScanData>();
                int i = 0;
                foreach (ScanData o in RegistrySubCategories)
                {
                    i++;
                    if (ABORT)
                    {
                        cancelComplete();
                        return;
                    }
                    if (o.Check)
                    {
                        switch (o.Id)
                        {
                            // delete value
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 7:
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                            case 26:
                            case 27:
                                {
                                    if (o.Value == "Default")
                                    {
                                        o.Value = string.Empty;
                                    }

                                    result = lightning.DeleteValue(o.Root, o.Key, o.Value);
                                    if (result == false)
                                    {
                                        ModSecVal(o.Root, o.Key, cSecurity.InheritenceFlags.Child_Inherit_Level);
                                        result = lightning.DeleteValue(o.Root, o.Key, o.Value);
                                    }

                                    callback((int)((double)i / RegistrySubCategories.Count() * 100), o.Key);

                                    itemsToDelete.Add(o);
                                    break;
                                }
                            // delete key
                            case 6:
                            case 8:
                                {
                                    result = (lightning.DeleteKey(o.Root, o.Key));
                                    if (result == false)
                                    {
                                        ModSecVal(o.Root, o.Key, cSecurity.InheritenceFlags.Container_Inherit);
                                        result = lightning.DeleteValue(o.Root, o.Key, o.Value);
                                    }

                                    callback((int)((double)i / RegistrySubCategories.Count() * 100), o.Key);

                                    itemsToDelete.Add(o);
                                    break;
                                }
                            // recreate value
                            case 20:
                                {
                                    result = (lightning.DeleteValue(o.Root, o.Key, o.Value));
                                    lightning.WriteMulti(o.Root, o.Key, "VDD", string.Empty);

                                    callback((int)((double)i / RegistrySubCategories.Count() * 100), o.Key);

                                    itemsToDelete.Add(o);
                                    break;
                                }
                        }
                    }
                }

                foreach (ScanData o in itemsToDelete)
                {
                    if (ABORT)
                    {
                        cancelComplete();
                        return;
                    }

                    RegistrySubCategories.Remove(o);
                }

                removedItemsCount = itemsToDelete.Count;
            }
            catch (Exception ex)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }

            complete(fixAfterScan);
        }

        void Reset()
        {
            ResetEngine();
            ResetTimer();
            ResetData();
            ResetContext();
            ResetProgressBars();
        }

        void ResetContext()
        {
            SubCategoryItemCounts = new int[12];
            _sLabel = string.Empty;
            _sPath = string.Empty;
            _sHive = string.Empty;
            _iKeyCounter = 0;
            _sMatch = string.Empty;
            ProblemsCount = 0;
            lastResultsCounter = 0;
            _iSegmentCounter = 0;
            // reset counters
            IsTimerOn = false;
            _iKeyCounter = 0;
            _iProgressMax = 0;
            _sSegment = string.Empty;

            IsScanLoaded = false;
        }

        void ResetData()
        {
            RegistrySubCategories.Clear();
            if (_RegScan.Data.Count > 0)
            {
                RegistrySubCategories = new ObservableCollection<ScanData>(_RegScan.Data);
            }
        }

        void ResetEngine()
        {
            _RegScan.CancelProcessAsync();
            _RegScan.Data.Clear();

            IsResetPending = true;
        }

        void ResetProgressBars()
        {
            callback(0, string.Empty);
        }

        void ResetTimer()
        {
            _aUpdateTimer.IsEnabled = false;
        }

        void ScanCancel()
        {
            Reset();
            TogglePanels("btnRegscan");
            cancelComplete();
        }

        void ScanComplete()
        {
            TogglePanels("btnRegscan");
        }

        int SelectedRegistryCategoriesCount()
        {
            return RegistryCategories.Count(rc => rc.IsChecked);
        }

        void ScanSetup()
        {
            ControlScan = RegistryCategories.First(rc => rc.Title == ControlScanTitle).IsChecked;
            UserScan = RegistryCategories.First(rc => rc.Title == UserScanTitle).IsChecked;
            SoftwareScan = RegistryCategories.First(rc => rc.Title == SoftwareScanTitle).IsChecked;
            FontScan = RegistryCategories.First(rc => rc.Title == FontScanTitle).IsChecked;
            HelpScan = RegistryCategories.First(rc => rc.Title == HelpScanTitle).IsChecked;
            LibraryScan = RegistryCategories.First(rc => rc.Title == LibraryScanTitle).IsChecked;
            StartupScan = RegistryCategories.First(rc => rc.Title == StartupScanTitle).IsChecked;
            UninstallScan = RegistryCategories.First(rc => rc.Title == UninstallScanTitle).IsChecked;
            VdmScan = RegistryCategories.First(rc => rc.Title == VdmScanTitle).IsChecked;
            HistoryScan = RegistryCategories.First(rc => rc.Title == HistoryScanTitle).IsChecked;
            DeepScan = RegistryCategories.First(rc => rc.Title == DeepScanTitle).IsChecked;
            MRUScan = RegistryCategories.First(rc => rc.Title == MRUScanTitle).IsChecked;

            if (SelectedRegistryCategoriesCount() == 0)
            {
                return;
            }
            IsScanLoaded = true;

            _aSubScan = new int[12];
            SubCategoryItemCounts = new int[12];

            ResetProgressBars();

            _RegScan.ScanControl = ControlScan;
            _RegScan.ScanUser = UserScan;
            _RegScan.ScanFile = SoftwareScan;
            _RegScan.ScanFont = FontScan;
            _RegScan.ScanHelp = HelpScan;
            _RegScan.ScanSharedDll = LibraryScan;
            _RegScan.ScanStartupEntries = StartupScan;
            _RegScan.ScanUninstallStrings = UninstallScan;
            _RegScan.ScanVDM = VdmScan;
            _RegScan.ScanHistory = HistoryScan;
            _RegScan.ScanDeep = DeepScan;
            _RegScan.ScanMru = MRUScan;
        }

        void ScanStart()
        {
            ProblemsCount = 0;
            _dTime = DateTime.Now;
            IsTimerOn = true;
            _aUpdateTimer.IsEnabled = true;
            TogglePanels("Active");
            ScanSetup();
            _RegScan.AsyncScan();
        }

        void ScanStop()
        {
            _aUpdateTimer.IsEnabled = false;

            if (!ABORT)
            {
                ScanComplete();
                SetSubCategoryScannedCounts();
            }
            else
            {
                cancelComplete();
                return;
            }

            Settings.Default.LastScan = DateTime.Now;
            Settings.Default.Save();

            ResetData();
            FrmRegCleaner.Problems = RegistrySubCategories;
        }

        static void SubProgressUpdate()
        {
        }

        void SubCategoryScanned(string id)
        {
            currentCategoryItemsCounter = ProblemsCount - lastResultsCounter;
            lastResultsCounter = ProblemsCount;

            switch (id)
            {
                case "CONTROL":
                    SubCategoryItemCounts[0] = currentCategoryItemsCounter;
                    break;
                case "USER":
                    SubCategoryItemCounts[1] = currentCategoryItemsCounter;
                    break;
                case "SOFTWARE":
                    SubCategoryItemCounts[2] = currentCategoryItemsCounter;
                    break;
                case "FONT":
                    SubCategoryItemCounts[3] = currentCategoryItemsCounter;
                    break;
                case "HELP":
                    SubCategoryItemCounts[4] = currentCategoryItemsCounter;
                    break;
                case "SHAREDDLL":
                    SubCategoryItemCounts[5] = currentCategoryItemsCounter;
                    break;
                case "STARTUP":
                    SubCategoryItemCounts[6] = currentCategoryItemsCounter;
                    break;
                case "UNINSTALL":
                    SubCategoryItemCounts[7] = currentCategoryItemsCounter;
                    break;
                case "VDM":
                    SubCategoryItemCounts[8] = currentCategoryItemsCounter;
                    break;
                case "HISTORY":
                    SubCategoryItemCounts[9] = currentCategoryItemsCounter;
                    break;
                case "DEEP":
                    SubCategoryItemCounts[10] = currentCategoryItemsCounter;
                    break;
                case "MRU":
                    SubCategoryItemCounts[11] = currentCategoryItemsCounter;
                    break;
            }
        }

        void SetSubCategoryScannedCounts()
        {
            foreach (RegistryCategory category in RegistryCategories)
            {
                if (category.Title == ControlScanTitle)
                {
                    category.ItemsCount = SubCategoryItemCounts[0] != 0 ? String.Format("({0})", SubCategoryItemCounts[0]) : string.Empty;
                    continue;
                }
                if (category.Title == UserScanTitle)
                {
                    category.ItemsCount = SubCategoryItemCounts[1] != 0 ? String.Format("({0})", SubCategoryItemCounts[1]) : string.Empty;
                    continue;
                }
                if (category.Title == SoftwareScanTitle)
                {
                    category.ItemsCount = SubCategoryItemCounts[2] != 0 ? String.Format("({0})", SubCategoryItemCounts[2]) : string.Empty;
                    continue;
                }
                if (category.Title == FontScanTitle)
                {
                    category.ItemsCount = SubCategoryItemCounts[3] != 0 ? String.Format("({0})", SubCategoryItemCounts[3]) : string.Empty;
                    continue;
                }
                if (category.Title == HelpScanTitle)
                {
                    category.ItemsCount = SubCategoryItemCounts[4] != 0 ? String.Format("({0})", SubCategoryItemCounts[4]) : string.Empty;
                    continue;
                }
                if (category.Title == LibraryScanTitle)
                {
                    category.ItemsCount = SubCategoryItemCounts[5] != 0 ? String.Format("({0})", SubCategoryItemCounts[5]) : string.Empty;
                    continue;
                }
                if (category.Title == StartupScanTitle)
                {
                    category.ItemsCount = SubCategoryItemCounts[6] != 0 ? String.Format("({0})", SubCategoryItemCounts[6]) : string.Empty;
                    continue;
                }
                if (category.Title == UninstallScanTitle)
                {
                    category.ItemsCount = SubCategoryItemCounts[7] != 0 ? String.Format("({0})", SubCategoryItemCounts[7]) : string.Empty;
                    continue;
                }
                if (category.Title == VdmScanTitle)
                {
                    category.ItemsCount = SubCategoryItemCounts[8] != 0 ? String.Format("({0})", SubCategoryItemCounts[8]) : string.Empty;
                    continue;
                }
                if (category.Title == HistoryScanTitle)
                {
                    category.ItemsCount = SubCategoryItemCounts[9] != 0 ? String.Format("({0})", SubCategoryItemCounts[9]) : string.Empty;
                    continue;
                }
                if (category.Title == DeepScanTitle)
                {
                    category.ItemsCount = SubCategoryItemCounts[10] != 0 ? String.Format("({0})", SubCategoryItemCounts[10]) : string.Empty;
                    continue;
                }
                if (category.Title == MRUScanTitle)
                {
                    category.ItemsCount = SubCategoryItemCounts[11] != 0 ? String.Format("({0})", SubCategoryItemCounts[11]) : string.Empty;
                }
            }
        }

        void TogglePanels(string name)
        {
            //0-_pnlRegScan
            //1-_pnlRegScanActive
            //2-_pnlScanResults
            //3-_pnlOptions
            //4-_pnlHelp
            if (IsScanLoaded)
            {
                //ResetContext();
            }
            switch (name)
            {
                case "btnRegscan":
                    if (!IsResetPending && RegistrySubCategories.Count > 0)
                    {
                    }
                    else
                    {
                        IsResetPending = false;
                    }
                    break;
                case "Active":
                    break;
                case "Results":
                    break;
                case "btnOptions":
                    break;
                case "btnHelp":
                    break;
            }
        }
        #endregion
    }
}