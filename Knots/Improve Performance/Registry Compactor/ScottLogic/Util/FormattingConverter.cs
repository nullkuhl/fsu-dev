using System;
using System.Globalization;
using System.Windows.Data;

namespace ScottLogic.Util
{
    /// <summary>
    /// A value converter that delegates to String.Format
    /// </summary>
    [ValueConversion(typeof(object), typeof(string))]
    public class FormattingConverter : IValueConverter
    {
		/// <summary>
		/// Delegates to String.Format
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            string formatString = parameter as string;
            return formatString != null ? string.Format(culture, formatString, value) : value.ToString();
        }

		/// <summary>
		/// Back converter (not implemented)
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
