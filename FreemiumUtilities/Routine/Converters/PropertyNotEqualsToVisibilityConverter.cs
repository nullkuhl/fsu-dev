using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using FreemiumUtilities.Models;

namespace FreemiumUtilities.Routine
{
    /// <summary>
    /// Used to convert two <see cref="OneClickAppStatus"/> values into the <c>Visibility</c>
    /// </summary>
    public sealed class PropertyNotEqualsToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Converts two <see cref="OneClickAppStatus"/> values into the <c>Visibility</c>
        /// </summary>
        /// <param name="value"><see cref="OneClickAppStatus"/> value</param>
        /// <param name="targetType">Target type</param>
        /// <param name="parameter">Converter parameter</param>
        /// <param name="culture">Culture</param>
        /// <returns><c>Visibility.Visible</c> if <paramref name="value"/> not equals to the <paramref name="parameter"/></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            OneClickAppStatus s1 = (OneClickAppStatus)value;
            OneClickAppStatus s2 = (OneClickAppStatus)parameter;
            return s1 != s2 ? Visibility.Visible : Visibility.Collapsed;
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