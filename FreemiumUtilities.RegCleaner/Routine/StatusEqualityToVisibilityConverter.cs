using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using FreemiumUtilities.RegCleaner.Models;

namespace FreemiumUtilities.RegCleaner.Routine
{
	/// <summary>
	/// Returns Visibility.Visible if the passed parameters are equal and Visibility.Collapsed otherwise
	/// </summary>
	public sealed class StatusEqualityToVisibilityConverter : IValueConverter
	{
		/// <summary>
		/// Converts two <see cref="OperationStatus"/> values into the <c>Visibility</c>
		/// </summary>
		/// <param name="value"><see cref="OperationStatus"/></param>
		/// <param name="targetType">Target type</param>
		/// <param name="parameter">Converter parameter</param>
		/// <param name="culture">Culture</param>
		/// <returns><c>Visibility.Visible</c> if <paramref name="value"/> equals to the <paramref name="parameter"/></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var s1 = (OperationStatus)value;
			var s2 = (OperationStatus)parameter;
			return s1 == s2 ? Visibility.Visible : Visibility.Collapsed;
		}

		/// <summary>
		/// Back converter (not implemented)
		/// </summary>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
