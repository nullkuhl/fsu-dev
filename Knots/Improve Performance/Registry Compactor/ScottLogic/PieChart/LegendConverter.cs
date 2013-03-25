using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Controls;
using BaseWPFHelpers;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;

namespace ScottLogic.Controls.PieChart
{
    /// <summary>
    /// Obtain the value of the property from the item, which is currently displayed by the pie chart.
    /// </summary>
    [ValueConversion(typeof(object), typeof(string))]
    public class LegendConverter : IValueConverter
    {
		/// <summary>
		/// Converts the value of the property from the item, which is currently displayed by the pie chart
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            // the item which we are displaying is bound to the Tag property
            TextBlock label = (TextBlock)value;
            object item = label.Tag;

            // find the item container
            DependencyObject container = Helpers.FindElementOfTypeUp((Visual)value, typeof(ListBoxItem));

            // locate the items control which it belongs to
            ItemsControl owner = ItemsControl.ItemsControlFromItemContainer(container);

            // locate the legend
            var legend = (Legend)Helpers.FindElementOfTypeUp(owner, typeof(Legend));
            
            PropertyDescriptorCollection filterPropDesc = TypeDescriptor.GetProperties(item);
            object itemValue = filterPropDesc[legend.PlottedProperty].GetValue(item);
            return itemValue;
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
