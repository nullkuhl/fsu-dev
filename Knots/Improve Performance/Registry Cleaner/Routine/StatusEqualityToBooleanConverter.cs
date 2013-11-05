using System;
using System.Globalization;
using System.Windows.Data;
using RegistryCleaner.Models;

namespace RegistryCleaner.Routine
{
	/// <summary>
	/// Used to convert two types of the <see cref="OperationStatus"/> values into the boolean
	/// </summary>
	public sealed class StatusEqualityToBooleanConverter : IValueConverter
	{
		#region IValueConverter Members

		/// <summary>
		/// Converts two types of the <see cref="OperationStatus"/> values into the boolean
		/// </summary>
		/// <param name="value"><see cref="OperationStatus"/> value</param>
		/// <param name="targetType">Target type</param>
		/// <param name="parameter">Converter parameter</param>
		/// <param name="culture">Culture</param>
		/// <returns>True if <paramref name="value"/> not equals to the <paramref name="parameter"/></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var s1 = (OperationStatus) value;
			var s2 = (OperationStatus) parameter;
			return s1 != s2;
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