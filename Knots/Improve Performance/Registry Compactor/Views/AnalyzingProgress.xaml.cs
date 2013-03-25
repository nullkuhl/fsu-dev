using System;
using System.Drawing;
using System.Threading;
using System.Windows;

namespace RegistryCompactor
{
	/// <summary>
	/// Interaction logic for AnalyzingProgress.xaml
	/// </summary>
	public partial class AnalyzingProgress
	{
		#region Properties

		Thread threadCurrent, threadScan;

		#endregion

		#region Constructors

		/// <summary>
		/// <see cref="AnalyzingProgress"/> constructor
		/// </summary>
		public AnalyzingProgress()
		{
			InitializeComponent();
			CurrentStep.Content = String.Format(Properties.Resources.StepXofY, 1, 3);
		}

		#endregion

        /// <summary>
        /// Handles Loaded window event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Set start time
			Utilites.ScanStartTime = DateTime.Now;

			// Set progress bar
			progressBar.Minimum = 0;
			progressBar.Maximum = Compactor.RegistryHives.Count;
			progressBar.Value = 0;

			threadScan = new Thread(Analyze);
			threadScan.Start();

			Color color = Color.FromArgb(240, 240, 240);
			Brush solidBrush = new SolidBrush(color);
            AnimatedImageControl animatedImageControl =
				new AnimatedImageControl(this, Properties.Resources.ajax_loader, solidBrush);
			AnalyzingTitle.Children.Insert(0, animatedImageControl);
		}

        /// <summary>
        /// Analyzing process
        /// </summary>
		void Analyze()
		{
			Thread.BeginCriticalRegion();
			foreach (Hive h in Compactor.RegistryHives)
			{
				IncrementProgressBar(h.RegistryHive);
				// Analyze RegistryCompactorUnit
				threadCurrent = new Thread(h.AnalyzeHive);
				threadCurrent.Start();
				threadCurrent.Join();
			}
			Thread.EndCriticalRegion();
			Dispatcher.BeginInvoke(new Action(Close));
		}

        /// <summary>
        /// Increments progress bar value 
        /// </summary>
        /// <param name="currentHive"></param>
		void IncrementProgressBar(string currentHive)
		{
			if (Dispatcher.Thread != Thread.CurrentThread)
			{
                Dispatcher.BeginInvoke(new Action<string>(IncrementProgressBar), currentHive);
				return;
			}
			progressBar.Value++;
            CurrentValue.Text = string.Format("{0}: {1}", Properties.Resources.Analyzing, Properties.Resources.RegHive + " \"" + currentHive + "\"");
		}
	}
}