using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using FreeToolbarRemover.Properties;
using System.Threading;
using System.Globalization;
using FreemiumUtil;
using FreeToolbarRemover.Properties;

namespace FreeToolbarRemover
{
	/// <summary>
	/// Interaction logic for BusyWindow.xaml
	/// </summary>
	public partial class BusyWindow : Window
	{
		/// <summary>
		/// Resource manager
		/// </summary>
        public ResourceManager rm = new ResourceManager("FreeToolbarRemover.Properties.Resources", typeof(Resources).Assembly);

		// Loading image
		readonly DispatcherTimer timer = new DispatcherTimer();
		readonly List<BitmapImage> images;

		/// <summary>
		/// constructor for BusyWindow
		/// </summary>
		public BusyWindow()
		{
			InitializeComponent();

			timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
			images = new List<BitmapImage>();

			var image = new BitmapImage();
			image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/FreeToolbarRemover;component/Images/frame0.png");
			image.EndInit();
			images.Add(image);
			image = new BitmapImage();
			image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/FreeToolbarRemover;component/Images/frame1.png");
			image.EndInit();
			images.Add(image);
			image = new BitmapImage();
			image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/FreeToolbarRemover;component/Images/frame2.png");
			image.EndInit();
			images.Add(image);
			image = new BitmapImage();
			image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/FreeToolbarRemover;component/Images/frame3.png");
			image.EndInit();
			images.Add(image);
			image = new BitmapImage();
			image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/FreeToolbarRemover;component/Images/frame4.png");
			image.EndInit();
			images.Add(image);
			image = new BitmapImage();
			image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/FreeToolbarRemover;component/Images/frame5.png");
			image.EndInit();
			images.Add(image);
			image = new BitmapImage();
			image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/FreeToolbarRemover;component/Images/frame6.png");
			image.EndInit();
			images.Add(image);
			image = new BitmapImage();
			image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/FreeToolbarRemover;component/Images/frame7.png");
			image.EndInit();
			images.Add(image);
		}

		/// <summary>
		/// initialize BusyWindow
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BusyWindow_Loaded(object sender, RoutedEventArgs e)
		{
			int i = 0;
			timer.Tick += (o, args) =>
							{
								Loading.Source = images[i];
								i = i < 6 ? i + 1 : 0;
							};
			timer.Start();

			SetCulture(new CultureInfo(CfgFile.Get("Lang")));
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
			Thread.CurrentThread.CurrentUICulture = culture;
			label.Content = rm.GetString("CreatingRestorePoint");
		}
	}
}
