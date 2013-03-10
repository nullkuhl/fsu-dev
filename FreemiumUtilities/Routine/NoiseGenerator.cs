using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Tuples;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FreemiumUtilities
{
	/// <summary>
	/// WPF noise generator for backgrounds
	/// </summary>
	public class NoiseGenerator : DependencyObject
	{
		static readonly DependencyPropertyKey noiseImageKey = DependencyProperty.RegisterReadOnly("NoiseImage",
		                                                                                          typeof (ImageSource),
		                                                                                          typeof (NoiseGenerator),
		                                                                                          new PropertyMetadata(null));

		ObservableCollection<ColorFrequency> colorFrequency;

		//bool pendingChange = false;

		public NoiseGenerator()
		{
			Colors = new ObservableCollection<ColorFrequency>();
			Size = 100;
			GenerateNoiseBitmap();
		}

		public ImageSource NoiseImage
		{
			get { return (ImageSource) GetValue(noiseImageKey.DependencyProperty); }
		}

		public int Size { get; set; }

		public ObservableCollection<ColorFrequency> Colors
		{
			get { return colorFrequency; }
			set
			{
				if (colorFrequency == value)
				{
					return;
				}
				if (colorFrequency != null)
				{
					Colors.CollectionChanged -= Colors_CollectionChanged;
				}
				colorFrequency = value;
				if (colorFrequency != null)
				{
					Colors.CollectionChanged += Colors_CollectionChanged;
				}
			}
		}

		void Colors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			GenerateNoiseBitmap();
		}


		// BitmapSource generation code coutesy of http://social.msdn.microsoft.com/forums/en-US/wpf/thread/56364b28-1277-4be8-8d45-78788cc4f2d7/
		void GenerateNoiseBitmap()
		{
			if (Colors == null || Colors.Count == 0)
			{
				SetValue(noiseImageKey, null);
				return;
			}

			try
			{
				var rnd = new Random();
				List<Color> colors = Colors.Select(value => value.Color).ToList();
				int totalFrequency = Colors.Sum(a => a.Frequency);
				List<Tuple<int, Color>> frequencyMap = GetFrequencyMap();


				var palette = new BitmapPalette(colors);

				PixelFormat pf = PixelFormats.Bgra32;
				int width = Size;
				int height = width;

				int stride = (width*pf.BitsPerPixel)/8;

				var pixels = new byte[height*stride];


				for (int i = 0; i < height*stride; i += (pf.BitsPerPixel/8))
				{
					Color color = GetWeightedRandomColor(totalFrequency, frequencyMap, rnd);

					pixels[i] = color.B;
					pixels[i + 1] = color.G;
					pixels[i + 2] = color.R;
					pixels[i + 3] = color.A;
				}


				BitmapSource image = BitmapSource.Create(width, height, 96, 96, pf, palette, pixels, stride);
				SetValue(noiseImageKey, image);
			}
			catch
			{
			}
		}

		Color GetWeightedRandomColor(int totalFrequency, List<Tuple<int, Color>> frequencyMap, Random rnd)
		{
			int value = rnd.Next(0, totalFrequency);
			for (int i = 0; i < frequencyMap.Count - 2; i ++)
			{
				if (frequencyMap[i].Element1 < value && frequencyMap[i + 1].Element1 >= value)
				{
					return frequencyMap[i].Element2;
				}
			}
			return frequencyMap.Last().Element2;
		}

		List<Tuple<int, Color>> GetFrequencyMap()
		{
			var frequencyMap = new List<Tuple<int, Color>>();
			int counter = 0;
			foreach (ColorFrequency item in Colors)
			{
				frequencyMap.Add(new Tuple<int, Color>(counter, item.Color));
				counter += item.Frequency;
			}
			return frequencyMap;
		}
	}

	public class ColorFrequency
	{
		public Color Color { get; set; }
		public int Frequency { get; set; }
	}
}