using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using RegistryCompactor.Properties;

namespace RegistryCompactor
{
	/// <summary>
	/// Interaction logic for CompactingProgress.xaml
	/// </summary>
	public partial class CompactingProgress : Window
	{
		#region Properties

		Thread threadCurrent;
		Thread threadScan;

		/// <summary>
		/// ShutdownBlockReasonCreate
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="reason"></param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool ShutdownBlockReasonCreate(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string reason);

		/// <summary>
		/// ShutdownBlockReasonDestroy
		/// </summary>
		/// <param name="hWnd"></param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool ShutdownBlockReasonDestroy(IntPtr hWnd);

		#endregion

		#region Constructors

		/// <summary>
		/// <see cref="CompactingProgress"/> constructor
		/// </summary>
		public CompactingProgress()
		{
			InitializeComponent();
			CurrentStep.Content = String.Format(Properties.Resources.StepXofY, 3, 3);
		}

		#endregion

        /// <summary>
        /// Compact registry
        /// </summary>
		void CompactRegistry()
		{
			long lSeqNum;
			long lSpaceSaved = 0;

			Thread.BeginCriticalRegion();

			SysRestore.StartRestore("Before Registry Optimization", out lSeqNum);

			foreach (Hive h in Compactor.RegistryHives)
			{
                SetStatusText(string.Format("{0}: {1}", Properties.Resources.Compacting, Properties.Resources.RegHive + " \"" + h.RegistryHive + "\""));

				threadCurrent = new Thread(h.CompactHive);
				threadCurrent.Start();
				threadCurrent.Join();

				lSpaceSaved += h.OldHiveSize - h.NewHiveSize;

				IncrementProgressBar();
			}

			SysRestore.EndRestore(lSeqNum);
			Thread.EndCriticalRegion();

			// Set IsCompacted
			MainWindow.IsCompacted = true;

			// Update last scan stats
			long elapsedTime = DateTime.Now.Subtract(Utilites.ScanStartTime).Ticks;

			Settings.Default.lastScan = DateTime.Now.ToBinary();
			Settings.Default.lastScanSaved = lSpaceSaved;
			Settings.Default.lastScanElapsed = elapsedTime;

			// Update total scans
			Settings.Default.totalScans++;
			Settings.Default.totalSaved += lSpaceSaved;
			Settings.Default.totalElapsed += elapsedTime;

			SetDialogResult(true);
			Dispatcher.BeginInvoke(new Action(Close));
		}

        /// <summary>
        /// Sets dialog result
        /// </summary>
        /// <param name="bResult"></param>
		void SetDialogResult(bool bResult)
		{
			if (Dispatcher.Thread != Thread.CurrentThread)
			{
				Dispatcher.BeginInvoke(new Action<bool>(SetDialogResult), bResult);
				return;
			}

			DialogResult = bResult;
		}

        /// <summary>
        /// Updates status text
        /// </summary>
        /// <param name="statusText"></param>
		void SetStatusText(string statusText)
		{
			if (Dispatcher.Thread != Thread.CurrentThread)
			{
				Dispatcher.BeginInvoke(new Action<string>(SetStatusText), statusText);
				return;
			}

			CurrentValue.Text = statusText;
		}

        /// <summary>
        /// Increments progress bar value
        /// </summary>
		void IncrementProgressBar()
		{
			if (Dispatcher.Thread != Thread.CurrentThread)
			{
				Dispatcher.BeginInvoke(new Action(IncrementProgressBar));
				return;
			}

			progressBar.Value++;
		}

        /// <summary>
        /// Handles window loaded event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void Window_Loaded(object sender, RoutedEventArgs e)
		{
			progressBar.Minimum = 0;
			progressBar.Maximum = Compactor.RegistryHives.Count;
			progressBar.Value = 0;

			threadScan = new Thread(CompactRegistry);
			threadScan.Start();

			Color color = Color.FromArgb(240, 240, 240);
			Brush solidBrush = new SolidBrush(color);
            AnimatedImageControl animatedImageControl =
				new AnimatedImageControl(this, Properties.Resources.ajax_loader, solidBrush);
			CompactingTitle.Children.Insert(0, animatedImageControl);
		}
	}
}