using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using RegistryCleaner.Implementation;
using RegistryCleaner.Models;
using RegistryCleaner.Properties;
using RegistryCleanerCore;
using System.Windows.Media.Imaging;

namespace RegistryCleaner
{
	/// <summary>
	/// The <see cref="RegistryCleaner"/> namespace defines a Registry Cleaner knot
	/// </summary>

	[System.Runtime.CompilerServices.CompilerGenerated]
	class NamespaceDoc { }

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : INotifyPropertyChanged
	{
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
		static bool _bRestoreComplete;
		static bool _bRestoreSucess;
		static int _iRestoreCounter;
		static int _iResultsCounter;
		static int lastResultsCounter;
		static int currentCategoryItemsCounter;
		static int _iSegmentCounter;
		static int _iProgressMax;
		static int removedItemsCount;
		static string _sLabel = "";
		static string _sPath = "";
		static int[] _aSubScan;
		static int[] SubCategoryItemCounts;
		static DateTime _dTime;
		static TimeSpan _tTimeElapsed;

		readonly string ControlScanTitle = Properties.Resources.ControlScanTitle;
		readonly string DeepScanTitle = Properties.Resources.DeepScanTitle;
		readonly string FontScanTitle = Properties.Resources.FontScanTitle;
		readonly string HelpScanTitle = Properties.Resources.HelpScanTitle;
		readonly string HistoryScanTitle = Properties.Resources.HistoryScanTitle;
		readonly string LibraryScanTitle = Properties.Resources.LibraryScanTitle;
		readonly string MRUScanTitle = Properties.Resources.MRUScanTitle;
		readonly string SoftwareScanTitle = Properties.Resources.SoftwareScanTitle;
		readonly string StartupScanTitle = Properties.Resources.StartupScanTitle;
		readonly string UninstallScanTitle = Properties.Resources.UninstallScanTitle;
		readonly string UserScanTitle = Properties.Resources.UserScanTitle;
		readonly string VdmScanTitle = Properties.Resources.VdmScanTitle;
		cRegScan _RegScan;
		DispatcherTimer _aUpdateTimer;
		BackgroundWorker _oProcessAsyncBackgroundWorker;
		event ProcessCompletedEventHandler ProcessCompleted;

		#region Properties

		bool allCategoriesChecked;
		bool allSubcategoriesChecked;
		string headerSubTitle = "";
		string headerTitle = "";
		ObservableCollection<RegistryCategory> registryCategories = new ObservableCollection<RegistryCategory>();
		ObservableCollection<ScanData> registrySubCategories = new ObservableCollection<ScanData>();
		string scanningTitle = "";
		OperationStatus status;
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

		/// <summary>
		/// Operation status
		/// </summary>
		public OperationStatus Status
		{
			get { return status; }
			set
			{
				status = value;
				OnPropertyChanged("Status");
			}
		}

		/// <summary>
		/// Header title
		/// </summary>
		public string HeaderTitle
		{
			get { return headerTitle; }
			set
			{
				headerTitle = value;
				OnPropertyChanged("HeaderTitle");
			}
		}

		/// <summary>
		/// Header subtitle
		/// </summary>
		public string HeaderSubTitle
		{
			get { return headerSubTitle; }
			set
			{
				headerSubTitle = value;
				OnPropertyChanged("HeaderSubTitle");
			}
		}

		/// <summary>
		/// Scanning title
		/// </summary>
		public string ScanningTitle
		{
			get { return scanningTitle; }
			set
			{
				scanningTitle = value;
				OnPropertyChanged("ScanningTitle");
			}
		}

		/// <summary>
		/// Registry categories collection
		/// </summary>
		public ObservableCollection<RegistryCategory> RegistryCategories
		{
			get { return registryCategories; }
			set
			{
				registryCategories = value;
				OnPropertyChanged("RegistryCategories");
			}
		}

		/// <summary>
		/// Registry subcategories collection
		/// </summary>
		public ObservableCollection<ScanData> RegistrySubCategories
		{
			get { return registrySubCategories; }
			set
			{
				registrySubCategories = value;
				OnPropertyChanged("RegistrySubCategories");
			}
		}

		/// <summary>
		/// Is all subcategories checked
		/// </summary>
		public bool AllSubcategoriesChecked
		{
			get { return allSubcategoriesChecked; }
			set
			{
				allSubcategoriesChecked = value;
				OnPropertyChanged("AllSubcategoriesChecked");
			}
		}

		/// <summary>
		/// Is all categories checked
		/// </summary>
		public bool AllCategoriesChecked
		{
			get { return allCategoriesChecked; }
			set
			{
				allCategoriesChecked = value;
				OnPropertyChanged("AllCategoriesChecked");
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Main window constructor
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

			DataContext = this;
			InitRegistryCategories();
			SetStatus(OperationStatus.NotStarted);

			InitFields();
		}

		#endregion

		#region Library Events

		void RegScan_CurrentPath(string hive, string path)
		{
			if (status != OperationStatus.Canceled)
			{
				_sPath = path;
				HeaderSubTitle = String.Format("{0}: {1}", Properties.Resources.NowScanning, _sPath);
				ScanningTitle = _sLabel;
			}
		}

		void RegScan_KeyCount()
		{
		}

		void RegScan_LabelChange(string phase, string label)
		{
			_sLabel = label;
		}

		void RegScan_MatchItem(cLightning.ROOT_KEY root, string key, string value, string data, RESULT_TYPE id)
		{
			_iResultsCounter += 1;
		}

		void RegScan_ProcessChange()
		{
			_iSegmentCounter += 1;
		}

		void RegScan_ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
		}

		void RegScan_ScanComplete()
		{
			IsTimerOn = false;
		}

		void RegScan_ScanCount(int count)
		{
			_iProgressMax = count;
		}

		void RegScan_StatusChange(string label)
		{
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
			//_pnlRegScanActive.txtScanPhase.Text = _sPhase;
			//_pnlRegScanActive.txtScanDescription.Text = _sLabel;
			//_pnlRegScanActive.txtScanningKey.Text = _sPath;
			//_pnlRegScanActive.txtScanningHive.Text = _sHive;
			//_pnlRegScanActive.txtKeyCount.Text = _iKeyCounter.ToString();
			//if (_sMatch.Length > 0)
			//{
			//    _pnlRegScanActive.txtLastMatch.Text = _sMatch;
			//}
			//_pnlRegScanActive.txtMatchCount.Text = _iResultsCounter.ToString();
			MainProgressBar.Maximum = _iProgressMax;
			MainProgressBar.Value = _iSegmentCounter;
			MainProgressBarText.Text = String.Format("{0}%", Math.Round(((float)_iSegmentCounter / _iProgressMax * 100), 0));
			//_pnlRegScanActive.txtSegmentsRemaining.Text = (_pnlRegScanActive.prgMain.Maximum - _iSegmentCounter).ToString();
			//_pnlRegScanActive.txtSegmentsScanned.Text = _iSegmentCounter.ToString();

			_tTimeElapsed = DateTime.Now.Subtract(_dTime);
			//_pnlRegScanActive.txtTimeElapsed.Text = ((int)_tTimeElapsed.TotalSeconds).ToString() + " seconds";
			if (IsTimerOn == false)
			{
				MainProgressBar.Value = _iProgressMax;
				MainProgressBarText.Text = "100%";
				ScanStop();
			}
		}

		#endregion

		#region Helpers

		void InitRegistryCategories()
		{
			RegistryCategories.Add(new RegistryCategory(ControlScanTitle, "/RegistryCleaner;component/Images/kwikdisk.png"));
			RegistryCategories.Add(new RegistryCategory(UserScanTitle, "/RegistryCleaner;component/Images/people.png"));
			RegistryCategories.Add(new RegistryCategory(SoftwareScanTitle,
														"/RegistryCleaner;component/Images/1330669346_system-software-installer.png"));
			RegistryCategories.Add(new RegistryCategory(FontScanTitle,
														"/RegistryCleaner;component/Images/preferences_desktop_font.png"));
			RegistryCategories.Add(new RegistryCategory(HelpScanTitle, "/RegistryCleaner;component/Images/help_and_support.png"));
			RegistryCategories.Add(new RegistryCategory(LibraryScanTitle,
														"/RegistryCleaner;component/Images/document_library.png"));
			RegistryCategories.Add(new RegistryCategory(StartupScanTitle,
														"/RegistryCleaner;component/Images/1330669473_window_fullscreen.png"));
			RegistryCategories.Add(new RegistryCategory(UninstallScanTitle,
														"/RegistryCleaner;component/Images/folder_actions_setup.png"));
			RegistryCategories.Add(new RegistryCategory(VdmScanTitle, "/RegistryCleaner;component/Images/device_internal.png"));
			RegistryCategories.Add(new RegistryCategory(HistoryScanTitle,
														"/RegistryCleaner;component/Images/1330669522_kmenuedit.png"));
			RegistryCategories.Add(new RegistryCategory(DeepScanTitle, "/RegistryCleaner;component/Images/virtual_pc.png"));
			RegistryCategories.Add(new RegistryCategory(MRUScanTitle, "/RegistryCleaner;component/Images/list.png"));
		}

		static void DoEvents()
		{
			var frame = new DispatcherFrame(true);
			Dispatcher.CurrentDispatcher.BeginInvoke(
				DispatcherPriority.Background, (SendOrPostCallback)delegate(object arg)
																		{
																			var f = arg as DispatcherFrame;
																			if (f != null) f.Continue = false;
																		},
				frame
				);
			Dispatcher.PushFrame(frame);
		}

		string IdToImage(int id)
		{
			if (id < 11) // "Control Scan"
			{
				return "/Images/regctrl.png";
			}
			if (id == 11) // "User Scan"
			{
				return "/Images/reguser.png";
			}
			if (id < 15) // "System Software"
			{
				return "/Images/regsystem.png";
			}
			if (id == 15) // "System Fonts"
			{
				return "/Images/regfont.png";
			}
			if (id == 16) // "System Help Files"
			{
				return "/Images/reghelp.png";
			}
			if (id == 17) // "Shared Libraries"
			{
				return "/Images/reglib.png";
			}
			if (id == 18) // "Startup Entries"
			{
				return "/Images/regstart.png";
			}
			if (id == 19) // "Installation Strings"
			{
				return "/Images/reginst.png";
			}
			if (id == 20) // "Virtual Devices"
			{
				return "/Images/regvdf.png";
			}
			if (id < 25) // "History and Start Menu"
			{
				return "/Images/reghist.png";
			}
			if (id < 27) // "Deep System Scan"
			{
				return "/Images/regdeep.png";
			}
			return "/Images/regmru.png";
		}

		void InitFields()
		{
			//_pnlRegScan = new RegScanPanel();
			//_pnlOptions = new OptionsPanel();
			//_pnlHelp = new HelpPanel();
			//_pnlRegScanActive = new RegScanActivePanel();
			//_pnlScanResults = new ScanResultsPanel();
			//_bmpMonitor = new BitmapImage(new Uri("/Images/monitor.png", UriKind.Relative));
			//_bmpDrive = new BitmapImage(new Uri("/Images/drive.png", UriKind.Relative));
			//_bmpOptions = new BitmapImage(new Uri("/Images/options.png", UriKind.Relative));
			//_bmpAbout = new BitmapImage(new Uri("/Images/about.png", UriKind.Relative));

			_RegScan = new cRegScan { Culture = Properties.Resources.Culture };

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
			// add the panels
			//grdContainer.Children.Add(_pnlRegScanActive);
			//grdContainer.Children.Add(_pnlScanResults);
			//grdContainer.Children.Add(_pnlOptions);
			//grdContainer.Children.Add(_pnlHelp);
		}

		void ModSecVal(cLightning.ROOT_KEY rootKey, string subKey, cSecurity.InheritenceFlags flags)
		{
			string sKey = rootKey.ToString();
			var sec = new cSecurity();
			string name = sec.UserName(cSecurity.EXTENDED_NAME_FORMAT.NameSamCompatible);

			if (name == null)
			{
				name = sec.UserName();
			}
			sKey += @"\" + subKey;
			sec.ChangeObjectOwnership(sKey, cSecurity.SE_OBJECT_TYPE.SE_REGISTRY_KEY);
			sec.ChangeKeyPermissions((cSecurity.ROOT_KEY)rootKey, subKey, name, cSecurity.RegistryAccess.Registry_Full_Control,
									 cSecurity.AccessTypes.Access_Allowed, flags);
		}

		void RemoveItems()
		{
            try
            {
                bool ret = false;

                // test for checked items first
                bool val = RegistrySubCategories.Any(o => o.Check);
                if (val)
                {
                    //set a restore point

                    bool res = Settings.Default.SettingRestore;

                    if (res)
                    {
                        MessageBoxResult chc = MessageBox.Show(Properties.Resources.WouldYouLikeToCreateRestorePoint,
                                                               Properties.Resources.SystemRestore, MessageBoxButton.YesNo,
                                                               MessageBoxImage.Question);
                        if (chc == MessageBoxResult.Yes)
                        {


                            // restore visual
                            RestoreProgressStart();

                            if (!_bRestoreSucess)
                            {
                                RestoreProgressStop();
                                res = false;

                                // Here is a code for a message box that asks a user to launch a system restore config window
                                // However, it needs to be refactored to run a correct system restore config window for every Windows version

                                /*
                                MessageBoxResult msg = MessageBox.Show(Properties.Resources.SystemRestoreUnavailable +
                                    Properties.Resources.SetUpSystemRestore,
                                            Properties.Resources.RestoreDisabled, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                                if (msg == MessageBoxResult.Yes)
                                {
                                    if (!ShowProtection())
                                    {
                                        //_pnlOptions.chkRestore.IsChecked = false;
                                    }
                                    return;
                                }
                                else if (msg == MessageBoxResult.No)
                                {
                                    LogEntry(Properties.Resources.SystemRestoreDeactivated);
                                    //_pnlOptions.chkRestore.IsChecked = false;
                                }
                                else
                                {
                                    return;
                                }
                                 */

                                // Simplified code for a message box that just say: sys restore disabled, please, enable it
                                MessageBoxResult msg = MessageBox.Show(
                                    Properties.Resources.SystemRestoreUnavailableRunFixAnyway,
                                    Properties.Resources.RestoreDisabled,
                                    MessageBoxButton.YesNo);
                                if (msg == MessageBoxResult.Yes)
                                {

                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                RestoreProgressStop();
                            }
                        }
                    }

                    var lightning = new cLightning();

                    // iterate through and remove
                    var itemsToDelete = new ObservableCollection<ScanData>();

                    foreach (ScanData o in RegistrySubCategories)
                    {
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
                                            o.Value = "";
                                        }

                                        ret = lightning.DeleteValue(o.Root, o.Key, o.Value);
                                        if (ret == false)
                                        {
                                            ModSecVal(o.Root, o.Key, cSecurity.InheritenceFlags.Child_Inherit_Level);
                                            ret = lightning.DeleteValue(o.Root, o.Key, o.Value);
                                        }
                                        itemsToDelete.Add(o);
                                        break;
                                    }
                                // delete key
                                case 6:
                                case 8:
                                    {
                                        ret = (lightning.DeleteKey(o.Root, o.Key));
                                        if (ret == false)
                                        {
                                            ModSecVal(o.Root, o.Key, cSecurity.InheritenceFlags.Container_Inherit);
                                            ret = lightning.DeleteValue(o.Root, o.Key, o.Value);
                                        }
                                        itemsToDelete.Add(o);
                                        break;
                                    }
                                // recreate value
                                case 20:
                                    {
                                        ret = (lightning.DeleteValue(o.Root, o.Key, o.Value));
                                        lightning.WriteMulti(o.Root, o.Key, "VDD", "");
                                        itemsToDelete.Add(o);
                                        break;
                                    }
                            }
                        }
                    }

                    foreach (ScanData o in itemsToDelete)
                    {
                        RegistrySubCategories.Remove(o);
                    }

                    // finalize restore
                    if (res)
                    {
                        _Restore.EndRestore(false);
                    }

                    // set AllSubcategoriesChecked to false as we removed all checked items
                    AllSubcategoriesChecked = false;

                    removedItemsCount = itemsToDelete.Count;

                    SetStatus(OperationStatus.CleaningFinished);
                }
                else
                {
                    MessageBoxResult can = MessageBox.Show(Properties.Resources.SelectItemsToRemove,
                                                           Properties.Resources.NoItemsSelected, MessageBoxButton.OK,
                                                           MessageBoxImage.Exclamation);
                    /*if (can == MessageBoxResult.Cancel)
                    {
                        LogEntry(Properties.Resources.ScanResultsReset);
                        ScanCancel();
                    }*/
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
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
			ScanningTitle = "";
			SubCategoryItemCounts = new int[12];
			ResetSubCategoryScannedCounts();
			// reset panel vars
			//_pnlRegScanActive.txtScanPhase.Text = "";
			_sLabel = "";
			//_pnlRegScanActive.txtScanningKey.Text = "";
			_sPath = "";
			//_pnlRegScanActive.txtScanningHive.Text = "";
			//_pnlRegScanActive.txtKeyCount.Text = "";
			//_pnlRegScanActive.txtLastMatch.Text = "";
			//_pnlRegScanActive.txtMatchCount.Text = "";
			_iResultsCounter = 0;
			lastResultsCounter = 0;
			//_pnlRegScanActive.txtSegmentsRemaining.Text = "";
			_iSegmentCounter = 0;
			//_pnlRegScanActive.txtSegmentsScanned.Text = "";
			//_pnlRegScanActive.btnRegScanCancel.Content = "Cancel";
			// reset counters
			IsTimerOn = false;
			_iProgressMax = 0;

			IsScanLoaded = false;
		}

		void ResetData()
		{
			RegistrySubCategories.Clear();
			if (_RegScan.Data.Count > 0)
			{
				RegistrySubCategories = new ObservableCollection<ScanData>(_RegScan.Data);

				foreach (ScanData o in RegistrySubCategories)
				{
					o.ImagePath = IdToImage(o.Id);
				}
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
			MainProgressBar.Value = 0;
			MainProgressBarText.Text = "0%";
		}

		void ResetTimer()
		{
			_aUpdateTimer.IsEnabled = false;
		}

		void RestoreProgressStart()
		{
			var formBusy = new FrmBusy();
			formBusy.Show();

			_dTime = DateTime.Now;
			_bRestoreComplete = false;
			_aRestoreTimer.IsEnabled = true;
			//grdRestore.Visibility = Visibility.Visible;
			//_pnlScanResults.lstResults.IsEnabled = false;
			// launch restore on a new thread
			_oProcessAsyncBackgroundWorker = new BackgroundWorker { WorkerSupportsCancellation = true };
			_oProcessAsyncBackgroundWorker.DoWork += _oProcessAsyncBackgroundWorker_DoWork;
			_oProcessAsyncBackgroundWorker.RunWorkerCompleted += _oProcessAsyncBackgroundWorker_RunWorkerCompleted;
			_oProcessAsyncBackgroundWorker.RunWorkerAsync();

			do
			{
				DoEvents();
				_tTimeElapsed = DateTime.Now.Subtract(_dTime);
				double safe = _tTimeElapsed.TotalSeconds;
				// break at 5 minutes, something has gone wrong
				if (safe > 300)
				{
					break;
				}
			} while (_bRestoreComplete != true);

			formBusy.Close();
		}

		void RestoreProgressStop()
		{
			//grdRestore.Visibility = Visibility.Collapsed;
			//_pnlScanResults.lstResults.IsEnabled = true;
			_aRestoreTimer.IsEnabled = false;
			_iRestoreCounter = 0;
		}

		void _oProcessAsyncBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_bRestoreComplete = true;
		}

		void _oProcessAsyncBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			_bRestoreComplete = false;
			_bRestoreSucess = _Restore.StartRestore("Registry Cleaner Restore Point");
		}

		void ScanCancel()
		{
			Reset();
			TogglePanels("btnRegscan");
		}

		void ScanComplete(int items)
		{
			//_aUpdateTimer.IsEnabled = false;
			//ResetContext();
			TogglePanels("btnRegscan");
		}

		int SelectedRegistryCategoriesCount()
		{
			return RegistryCategories.Where(rc => rc.IsChecked).Count();
		}

		void ScanSetup()
		{
			ControlScan = RegistryCategories.Where(rc => rc.Title == ControlScanTitle).First().IsChecked;
			UserScan = RegistryCategories.Where(rc => rc.Title == UserScanTitle).First().IsChecked;
			SoftwareScan = RegistryCategories.Where(rc => rc.Title == SoftwareScanTitle).First().IsChecked;
			FontScan = RegistryCategories.Where(rc => rc.Title == FontScanTitle).First().IsChecked;
			HelpScan = RegistryCategories.Where(rc => rc.Title == HelpScanTitle).First().IsChecked;
			LibraryScan = RegistryCategories.Where(rc => rc.Title == LibraryScanTitle).First().IsChecked;
			StartupScan = RegistryCategories.Where(rc => rc.Title == StartupScanTitle).First().IsChecked;
			UninstallScan = RegistryCategories.Where(rc => rc.Title == UninstallScanTitle).First().IsChecked;
			VdmScan = RegistryCategories.Where(rc => rc.Title == VdmScanTitle).First().IsChecked;
			HistoryScan = RegistryCategories.Where(rc => rc.Title == HistoryScanTitle).First().IsChecked;
			DeepScan = RegistryCategories.Where(rc => rc.Title == DeepScanTitle).First().IsChecked;
			MRUScan = RegistryCategories.Where(rc => rc.Title == MRUScanTitle).First().IsChecked;

			int c = SelectedRegistryCategoriesCount();
			MainProgressBar.Maximum = c;

			if (SelectedRegistryCategoriesCount() == 0)
			{
				return;
			}
			IsScanLoaded = true;

			_aSubScan = new int[12];
			SubCategoryItemCounts = new int[12];

			ResetProgressBars();

			//_pnlRegScanActive.stkUserScan.Visibility = this.UserScan ? Visibility.Visible : Visibility.Collapsed;
			//_pnlRegScanActive.stkControlScan.Visibility = this.ControlScan ? Visibility.Visible : Visibility.Collapsed;
			//_pnlRegScanActive.stkSoftwareScan.Visibility = this.SoftwareScan ? Visibility.Visible : Visibility.Collapsed;
			//_pnlRegScanActive.stkFontsScan.Visibility = this.FontScan ? Visibility.Visible : Visibility.Collapsed;
			//_pnlRegScanActive.stkHelpScan.Visibility = this.HelpScan ? Visibility.Visible : Visibility.Collapsed;
			//_pnlRegScanActive.stkLibrariesScan.Visibility = this.LibraryScan ? Visibility.Visible : Visibility.Collapsed;
			//_pnlRegScanActive.stkStartupScan.Visibility = this.StartupScan ? Visibility.Visible : Visibility.Collapsed;
			//_pnlRegScanActive.stkUninstall.Visibility = this.UninstallScan ? Visibility.Visible : Visibility.Collapsed;
			//_pnlRegScanActive.stkVdmScan.Visibility = this.VdmScan ? Visibility.Visible : Visibility.Collapsed;
			//_pnlRegScanActive.stkHistoryScan.Visibility = this.HistoryScan ? Visibility.Visible : Visibility.Collapsed;
			//_pnlRegScanActive.stkDeepScan.Visibility = this.DeepScan ? Visibility.Visible : Visibility.Collapsed;
			//_pnlRegScanActive.stkMru.Visibility = this.MRUScan ? Visibility.Visible : Visibility.Collapsed;

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

			return;
		}

		void ScanStart()
		{
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

			if (Status == OperationStatus.Started)
			{
				SetStatus(OperationStatus.ScanFinished);

				ScanComplete(_iResultsCounter);

				SetSubCategoryScannedCounts();
			}

			try
			{
				Settings.Default.LastScan = DateTime.Now;
				Settings.Default.Save();
			}
			catch
			{
			}

			ResetData();
		}

		void SubCategoryScanned(string id)
		{
			currentCategoryItemsCounter = _iResultsCounter - lastResultsCounter;
			lastResultsCounter = _iResultsCounter;

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

		void ResetSubCategoryScannedCounts()
		{
			foreach (RegistryCategory category in RegistryCategories)
			{
				category.ItemsCount = "";
			}
		}

		void SetSubCategoryScannedCounts()
		{
			foreach (RegistryCategory category in RegistryCategories)
			{
				if (category.Title == ControlScanTitle)
				{
					category.ItemsCount = category.IsChecked ? String.Format("({0})", SubCategoryItemCounts[0]) : "";
					continue;
				}
				if (category.Title == UserScanTitle)
				{
					category.ItemsCount = category.IsChecked ? String.Format("({0})", SubCategoryItemCounts[1]) : "";
					continue;
				}
				if (category.Title == SoftwareScanTitle)
				{
					category.ItemsCount = category.IsChecked ? String.Format("({0})", SubCategoryItemCounts[2]) : "";
					continue;
				}
				if (category.Title == FontScanTitle)
				{
					category.ItemsCount = category.IsChecked ? String.Format("({0})", SubCategoryItemCounts[3]) : "";
					continue;
				}
				if (category.Title == HelpScanTitle)
				{
					category.ItemsCount = category.IsChecked ? String.Format("({0})", SubCategoryItemCounts[4]) : "";
					continue;
				}
				if (category.Title == LibraryScanTitle)
				{
					category.ItemsCount = category.IsChecked ? String.Format("({0})", SubCategoryItemCounts[5]) : "";
					continue;
				}
				if (category.Title == StartupScanTitle)
				{
					category.ItemsCount = category.IsChecked ? String.Format("({0})", SubCategoryItemCounts[6]) : "";
					continue;
				}
				if (category.Title == UninstallScanTitle)
				{
					category.ItemsCount = category.IsChecked ? String.Format("({0})", SubCategoryItemCounts[7]) : "";
					continue;
				}
				if (category.Title == VdmScanTitle)
				{
					category.ItemsCount = category.IsChecked ? String.Format("({0})", SubCategoryItemCounts[8]) : "";
					continue;
				}
				if (category.Title == HistoryScanTitle)
				{
					category.ItemsCount = category.IsChecked ? String.Format("({0})", SubCategoryItemCounts[9]) : "";
					continue;
				}
				if (category.Title == DeepScanTitle)
				{
					category.ItemsCount = category.IsChecked ? String.Format("({0})", SubCategoryItemCounts[10]) : "";
					continue;
				}
				if (category.Title == MRUScanTitle)
				{
					category.ItemsCount = category.IsChecked ? String.Format("({0})", SubCategoryItemCounts[11]) : "";
					continue;
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
			// reset visibility
			//foreach (UIElement o in grdContainer.Children)
			//{
			//    o.Visibility = Visibility.Collapsed;
			//}
			// toggle visible panel
			switch (name)
			{
				case "btnRegscan":
					if (!IsResetPending && RegistrySubCategories.Count > 0)
					{
						//grdContainer.Children[2].Visibility = Visibility.Visible;
					}
					else
					{
						IsResetPending = false;
						//Reset();
						//grdContainer.Children[0].Visibility = Visibility.Visible;
						//txtStatusBar.Text = "Registry Scan Pending..";
					}
					break;
				case "Active":
					break;
				case "Results":
					//grdContainer.Children[2].Visibility = Visibility.Visible;
					//txtStatusBar.Text = "Scan Completed!";
					break;
				case "btnOptions":
					//grdContainer.Children[3].Visibility = Visibility.Visible;
					//txtStatusBar.Text = "Review Scanning Options..";
					break;
				case "btnHelp":
					//grdContainer.Children[4].Visibility = Visibility.Visible;
					//txtStatusBar.Text = "Review Help Information..";
					break;
			}
		}

		#endregion

		#region Handlers

		void StartScan(object sender, RoutedEventArgs e)
		{
			if (SelectedRegistryCategoriesCount() > 0)
			{
				Reset();

				SetStatus(OperationStatus.Started);

				ScanStart();
			}
			else
			{
				MessageBox.Show(Properties.Resources.SelectAtLeastOneItem,
								Properties.Resources.InvalidSelection,
								MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		void CancelScan(object sender, RoutedEventArgs e)
		{
			SetStatus(OperationStatus.Canceled);

			if (IsTimerOn)
			{
				ScanCancel();
			}
		}

		void StartClearing(object sender, RoutedEventArgs e)
		{
			RemoveItems();
			IsResetPending = true;
		}

		void Finish(object sender, RoutedEventArgs e)
		{
			Reset();
			SetStatus(OperationStatus.NotStarted);
		}

		void SetStatus(OperationStatus status)
		{
			switch (status)
			{
				case OperationStatus.NotStarted:
					{
						try
						{
							Status = OperationStatus.NotStarted;
							HeaderTitle = Properties.Resources.NotStartedHeader;
							if (Settings.Default.LastScan.Year != 1)
							{
								HeaderSubTitle = String.Format(Properties.Resources.LastCleanTime, Settings.Default.LastScan);
								imageNotStarted.Source = new BitmapImage(new Uri(@"/RegistryCleaner;component/Images/last_scan.png", UriKind.Relative));
							}
						}
						catch { }
						break;
					}

				case OperationStatus.Started:
					{
						Status = OperationStatus.Started;
						HeaderTitle = Properties.Resources.ScanningRegistry;
						break;
					}

				case OperationStatus.Canceled:
					{
						Status = OperationStatus.Canceled;
						HeaderTitle = Properties.Resources.RegistryScanCanceled;
						HeaderSubTitle = String.Format("{0} {1}", _iResultsCounter, Properties.Resources.ProblemsFound);
						break;
					}

				case OperationStatus.ScanFinished:
					{
						Status = OperationStatus.ScanFinished;
						HeaderTitle = Properties.Resources.RegistryScanCompleted;
						HeaderSubTitle = String.Format("{0} {1}", _iResultsCounter, Properties.Resources.ProblemsFoundClickClear);
						break;
					}

				case OperationStatus.CleaningFinished:
					{
						Status = OperationStatus.CleaningFinished;
						HeaderTitle = Properties.Resources.RegistryCleaningCompleted;
						HeaderSubTitle = String.Format("{0} {1}.", removedItemsCount, Properties.Resources.ItemsCleared);
						break;
					}
			}
		}


		void AllCategoriesClick(object sender, RoutedEventArgs e)
		{
			foreach (RegistryCategory category in RegistryCategories)
			{
				category.IsChecked = AllCategoriesChecked;
			}
		}

		void CategoryClick(object sender, RoutedEventArgs e)
		{
			if (RegistryCategories.Where(d => d.IsChecked).Count() == 0)
			{
				AllCategoriesChecked = false;
			}
			else
			{
				if (RegistryCategories.Where(d => !d.IsChecked).Count() == 0)
				{
					AllCategoriesChecked = true;
				}
			}
		}

		void AllSubcategoriesClick(object sender, RoutedEventArgs e)
		{
			foreach (ScanData subCategory in RegistrySubCategories)
			{
				subCategory.Check = AllSubcategoriesChecked;
			}
		}

		void SubcategoryClick(object sender, RoutedEventArgs e)
		{
			if (RegistrySubCategories.Where(d => d.Check).Count() == 0)
			{
				AllSubcategoriesChecked = false;
			}
			else
			{
				if (RegistrySubCategories.Where(d => !d.Check).Count() == 0)
				{
					AllSubcategoriesChecked = true;
				}
			}
		}

		void Window_Loaded(object sender, RoutedEventArgs e)
		{
			pictureBoxLoading.Image = Properties.Resources.ajax_loader;
		}

		#endregion

		#region INotifyPropertyChanged

		/// <summary>
		/// PropertyChanged event handler
		/// </summary>
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