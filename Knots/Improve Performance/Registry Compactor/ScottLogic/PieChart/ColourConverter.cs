using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Controls;
using BaseWPFHelpers;
using System.Windows.Media;
using System.Windows;

/// <summary>
/// The <see cref="ScottLogic.Controls.PieChart"/> namespace defines the ScottLogic PieChart
/// </summary>
namespace ScottLogic.Controls.PieChart
{
    /// <summary>
    /// Converter which uses the IColorSelector associated with the Legend to
    /// select a suitable color for rendering an item.
    /// </summary>
    [ValueConversion(typeof(object), typeof(Brush))]
    public class ColourConverter : IValueConverter
    {
		/// <summary>
		/// Converts the <paramref name="value"/> to select a suitable color for rendering an item
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            // find the item 
            var element = (FrameworkElement)value;
            object item = element.Tag;

            // find the item container
            var container = (DependencyObject)Helpers.FindElementOfTypeUp(element, typeof(ListBoxItem));

            // locate the items control which it belongs to
            ItemsControl owner = ItemsControl.ItemsControlFromItemContainer(container);

            // locate the legend
            var legend = (Legend)Helpers.FindElementOfTypeUp(owner, typeof(Legend));

            var collectionView = (CollectionView)CollectionViewSource.GetDefaultView(owner.DataContext);

            // locate this item (which is bound to the tag of this element) within the collection
            int index = collectionView.IndexOf(item);

            return legend.ColorSelector != null ? legend.ColorSelector.SelectBrush(item, index) : Brushes.Black;
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
