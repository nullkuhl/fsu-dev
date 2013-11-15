using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FreeToolbarRemover.Routine
{
	/// <summary>
	/// Used to get the <c>BitmapImage</c> object from the <c>Uri</c> provided by the <c>value</c>
	/// </summary>
	public sealed class ImageConverter : IValueConverter
	{
		#region IValueConverter Members

		/// <summary>
		/// Converts the <c>Uri</c> from the <paramref name="value"/> to the <c>BitmapImage</c> object
		/// </summary>
		/// <param name="value"><c>Uri</c></param>
		/// <param name="targetType">Target type</param>
		/// <param name="parameter">Converter parameter</param>
		/// <param name="culture">Culture</param>
		/// <returns><c>BitmapImage</c> object from the <c>Uri</c> provided by the <c>value</c></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				return new BitmapImage(new Uri(value.ToString(), UriKind.Relative));
			}
			catch
			{
				return new BitmapImage();
			}
		}

		/// <summary>
		/// Back converter (not implemented)
		/// </summary>
		public object ConvertBack(object value, Type targetType,
		                          object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}