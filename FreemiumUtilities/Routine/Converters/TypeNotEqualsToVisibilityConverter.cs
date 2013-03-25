using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using FreemiumUtilities.Infrastructure;
using FreemiumUtilities.TracksEraser;

namespace FreemiumUtilities.Routine
{
    /// <summary>
    /// Used to convert two types of the <see cref="OneClickApp"/> values into the <c>Visibility</c>
    /// </summary>
    public sealed class TypeNotEqualsToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Converts two types of the <see cref="OneClickApp"/> values into the <c>Visibility</c>
        /// </summary>
        /// <param name="value"><see cref="OneClickApp"/> value</param>
        /// <param name="targetType">Target type</param>
        /// <param name="parameter">Converter parameter</param>
        /// <param name="culture">Culture</param>
        /// <returns><c>Visibility.Visible</c> if <paramref name="value"/> type not equals to the type of <see cref="TracksEraserApp"/></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.GetType() != typeof(TracksEraserApp) ? Visibility.Visible : Visibility.Collapsed;
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