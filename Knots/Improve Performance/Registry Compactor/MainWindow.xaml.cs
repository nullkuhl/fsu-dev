using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Interop;

namespace RegistryCompactor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		#region Properties

		/// <summary>
		/// True if the registry has been compacted and is waiting for a reboot
		/// </summary>
		// TODO: Find all references of IsCompacted and implement its setter
		public static bool IsCompacted { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// constructor for MainWindow
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
          
		}

		#endregion

		#region methods Main View

		/// <summary>
		/// handle Click event to started optimizing the registry
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Start_Click(object sender, RoutedEventArgs e)
		{
			if (
				MessageBox.Show(Application.Current.MainWindow, Properties.Resources.CloseProgramsBeforeOptimizing,
								Properties.Resources.RegistryCompactor, MessageBoxButton.OKCancel, MessageBoxImage.Information) !=
				MessageBoxResult.OK)
				return;

            SecureDesktop secureDesktop = new SecureDesktop();
			secureDesktop.Show();

            AnalyzingProgress progressWindow = new AnalyzingProgress();
			progressWindow.ShowDialog();

			secureDesktop.Close();

			// Check registry size before continuing
			if (Utilites.GetNewRegistrySize() <= 0 || IsCompacted)
			{
				MessageBox.Show(Application.Current.MainWindow, Properties.Resources.RegistryAlreadyCompacted,
								Properties.Resources.RegistryCompactor, MessageBoxButton.OK, MessageBoxImage.Information);
				return;
			}

			// Showing Analyzed Result
			Init();
			gridAnalyzingResults.Visibility = Visibility.Visible;
			gridMainView.Visibility = Visibility.Collapsed;
		}

		/// <summary>
		/// handle Click event to view system information
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ViewSystemInfo_Click(object sender, RoutedEventArgs e)
		{
			var regInfoWnd = new RegistryInfo();
			regInfoWnd.ShowDialog();
		}

		#endregion

		#region methods Analyzing Results

        /// <summary>
        /// Window Initialization
        /// </summary>
		void Init()
		{
			double oldRegistrySize = Utilites.GetOldRegistrySize(), newRegistrySize = Utilites.GetNewRegistrySize();

			decimal oldRegistrySizeMB = decimal.Round(Convert.ToDecimal(oldRegistrySize) / 1024 / 1024, 2);
			decimal diffRegistrySizeMB = decimal.Round((Convert.ToDecimal(oldRegistrySize - newRegistrySize)) / 1024 / 1024, 2);
			diffRegistrySizeMB = diffRegistrySizeMB > 0 ? diffRegistrySizeMB : 0;

			piePlotter.DataContext = new ObservableCollection<KeyValuePair<string, decimal>>
			                         	{
			                         		new KeyValuePair<string, decimal>(
			                         			string.Format("{0} ({1}MB)", Properties.Resources.RegistrySize, oldRegistrySizeMB),
			                         			oldRegistrySizeMB - diffRegistrySizeMB),
			                         		new KeyValuePair<string, decimal>(
			                         			string.Format("{0} ({1}MB)", Properties.Resources.Saving, diffRegistrySizeMB),
			                         			diffRegistrySizeMB)
			                         	};

            BitmapImage logo = new BitmapImage();

			if ((100 - ((newRegistrySize / oldRegistrySize) * 100)) >= 5)
			{
				lblStatus.Content = Properties.Resources.RegistryNeedsCompact;
				logo.BeginInit();
				logo.UriSource = new Uri("pack://application:,,,/RegistryOptimizer;component/Images/warning.png");
				logo.EndInit();
				StatusImage.Source = logo;
			}
			else
			{
				lblStatus.Content = Properties.Resources.RegistryDoesntNeedCompact;
				logo.BeginInit();
				logo.UriSource = new Uri("pack://application:,,,/RegistryOptimizer;component/Images/yes.png");
				logo.EndInit();
				StatusImage.Source = logo;
			}

			CurrentStep.Content = String.Format(Properties.Resources.StepXofY, 2, 3);
		}

        /// <summary>
        /// Handles click event of Compact button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void buttonCompact_Click(object sender, RoutedEventArgs e)
		{
			if (
				MessageBox.Show(Application.Current.MainWindow, Properties.Resources.RegistryCompactQuestion,
								Properties.Resources.RegistryCompactor, MessageBoxButton.YesNo, MessageBoxImage.Question) ==
				MessageBoxResult.No)
				return;

            SecureDesktop secureDesktop = new SecureDesktop();
			secureDesktop.Show();

            CompactingProgress compactingProgress = new CompactingProgress();
			compactingProgress.ShowDialog();

			secureDesktop.Close();

			if (
				MessageBox.Show(Application.Current.MainWindow, Properties.Resources.RestartComputerQuestion,
								Properties.Resources.RegistryCompactor, MessageBoxButton.YesNo, MessageBoxImage.Question) ==
				MessageBoxResult.Yes)
				// Restart computer
				PInvoke.ExitWindowsEx(0x02, PInvoke.MajorOperatingSystem | PInvoke.MinorReconfig | PInvoke.FlagPlanned);

			Close();
		}

        /// <summary>
        /// Handles click event of Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void buttonCancel_Click(object sender, RoutedEventArgs e)
		{
			gridAnalyzingResults.Visibility = Visibility.Collapsed;
			gridMainView.Visibility = Visibility.Visible;
		}

		#endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\FreeGamingBooster.exe"))
            {
                this.Icon = BitmapFrame.Create(Application.GetResourceStream(new Uri(@"pack://application:,,/Images/GBicon.ico", UriKind.RelativeOrAbsolute)).Stream);
            }
            else if (!File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\FreemiumUtilities.exe"))
            {
                this.Icon = BitmapFrame.Create(Application.GetResourceStream(new Uri(@"pack://application:,,/Images/PCCleanerIcon.ico", UriKind.RelativeOrAbsolute)).Stream);
            }
            else
            {
                this.Icon = BitmapFrame.Create(Application.GetResourceStream(new Uri(@"pack://application:,,/Images/FSUIcon.ico", UriKind.RelativeOrAbsolute)).Stream);
            }

        }
	}
}