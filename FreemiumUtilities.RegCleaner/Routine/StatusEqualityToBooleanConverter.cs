using System;
using System.Globalization;
using System.Windows.Data;
using FreemiumUtilities.RegCleaner.Models;

/// <summary>
/// The <see cref="FreemiumUtilities.RegCleaner.Routine"/> namespace contains a helper classes
/// used in the <see cref="FreemiumUtilities.RegCleaner"/> project
/// </summary>
namespace FreemiumUtilities.RegCleaner.Routine
{
    /// <summary>
    /// Returns true if the passed parameters are not equal
    /// </summary>
    public sealed class StatusEqualityToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts two <see cref="OperationStatus"/> values into the boolean
        /// </summary>
        /// <param name="value"><see cref="OperationStatus"/></param>
        /// <param name="targetType">Target type</param>
        /// <param name="parameter">Converter parameter</param>
        /// <param name="culture">Culture</param>
        /// <returns><c>True</c> if <paramref name="value"/> not equals to the <paramref name="parameter"/></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            OperationStatus s1 = (OperationStatus)value;
            OperationStatus s2 = (OperationStatus)parameter;
            return s1 != s2;
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
