using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using FreeToolbarRemover.Models;

namespace FreeToolbarRemover.Routine
{
	/// <summary>
	/// Used to convert two <see cref="OneClickAppStatus"/> values into the <c>Visibility</c>
	/// </summary>
	public sealed class AnyPropertyEqualsToVisibilityConverter : IMultiValueConverter
	{
		#region IMultiValueConverter Members

		/// <summary>
		/// Converts two <see cref="OneClickAppStatus"/> values into the <c>Visibility</c>
		/// </summary>
		/// <param name="values">Two <see cref="OneClickAppStatus"/> values</param>
		/// <param name="targetType">Target type</param>
		/// <param name="parameter">Converter parameter</param>
		/// <param name="culture">Culture</param>
		/// <returns><c>Visibility.Visible</c> if any of <paramref name="values"/> equals to the <paramref name="parameter"/></returns>
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			Visibility result = Visibility.Hidden;

            OneClickAppStatus s1 = (OneClickAppStatus)parameter;

			foreach (object val in values)
			{
				if (val != null && (OneClickAppStatus) val == s1)
				{
					result = Visibility.Visible;
				}
			}

			if (values[0] != null && values[1] != null)
			{
                OneClickAppStatus s2 = (OneClickAppStatus)values[0];
                OneClickAppStatus s3 = (OneClickAppStatus)values[1];

				if (s2 == s3)
				{
					result = Visibility.Visible;
				}
			}

			return result;
		}

		/// <summary>
		/// Back converter (not implemented)
		/// </summary>
		public object[] ConvertBack(object values, Type[] targetTypes,
		                            object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}