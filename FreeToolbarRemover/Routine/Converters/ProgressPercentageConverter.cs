using System;
using System.Globalization;
using System.Windows.Data;

namespace FreeToolbarRemover.Routine
{
	/// <summary>
	/// Used to convert <c>int</c> value into the <c>double</c> progress with a UI related multiplier
	/// </summary>
	public sealed class ProgressPercentageConverter : IValueConverter
	{
		#region IValueConverter Members

		/// <summary>
		/// Converts <c>int</c> value into the <c>double</c> progress with a UI related multiplier
		/// </summary>
		/// <param name="value"><c>int</c> progress value</param>
		/// <param name="targetType">Target type</param>
		/// <param name="parameter">Converter parameter</param>
		/// <param name="culture">Culture</param>
		/// <returns><c>double</c> progress with a UI related multiplier</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double v = int.Parse(value.ToString())*3.94;
			return v;
		}

		/// <summary>
		/// Back converter (not implemented)
		/// </summary>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}