using System.Windows.Media;
using ScottLogic.ScottLogic.PieChart;
using System.Windows;

namespace ScottLogic.Controls.PieChart
{
    /// <summary>
    /// Selects a colour purely based on its location within a collection.
    /// </summary>
    public class IndexedColourSelector : DependencyObject, IColorSelector
    {
        /// <summary>
        /// An array of brushes.
        /// </summary>
        public Brush[] Brushes
        {
            get { return (Brush[])GetValue(BrushesProperty); }
            set { SetValue(BrushesProperty, value); }
        }

		/// <summary>
		/// Brushes
		/// </summary>
        public static readonly DependencyProperty BrushesProperty =
                       DependencyProperty.Register("BrushesProperty", typeof(Brush[]), typeof(IndexedColourSelector), new UIPropertyMetadata(null));


		/// <summary>
		/// Selects the brush
		/// </summary>
		/// <param name="item"></param>
		/// <param name="index"></param>
		/// <returns></returns>
        public Brush SelectBrush(object item, int index)
        {
            if (Brushes == null || Brushes.Length == 0)
            {
                return System.Windows.Media.Brushes.Black;
            }
            return Brushes[index % Brushes.Length];
        }
    }
}
