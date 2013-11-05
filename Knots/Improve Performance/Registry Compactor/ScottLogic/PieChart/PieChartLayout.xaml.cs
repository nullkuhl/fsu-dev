using System;
using System.Windows;
using ScottLogic.ScottLogic.PieChart;

namespace ScottLogic.Controls.PieChart
{
    /// <summary>
    /// Defines the layout of the pie chart
    /// </summary>
    public partial class PieChartLayout
    {
        #region dependency properties

        /// <summary>
        /// The property of the bound object that will be plotted (CLR wrapper)
        /// </summary>
        public String PlottedProperty
        {
            get { return GetPlottedProperty(this); }
            set { SetPlottedProperty(this, value); }
        }

        /// <summary>
		/// PlottedProperty dependency property
        /// </summary>
        public static readonly DependencyProperty PlottedPropertyProperty =
                       DependencyProperty.RegisterAttached("PlottedProperty", typeof(String), typeof(PieChartLayout),
                       new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
		/// PlottedProperty attached property accessors
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetPlottedProperty(UIElement element, String value)
        {
            element.SetValue(PlottedPropertyProperty, value);
        }

		/// <summary>
		/// Gets the value of the <see cref="PlottedPropertyProperty"/>
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
        public static String GetPlottedProperty(UIElement element)
        {
            return (String)element.GetValue(PlottedPropertyProperty);
        }

        /// <summary>
        /// A class which selects a color based on the item being rendered.
        /// </summary>
        public IColorSelector ColorSelector
        {
            get { return GetColorSelector(this); }
            set { SetColorSelector(this, value); }
        }

        /// <summary>
		/// ColorSelector dependency property
        /// </summary>
        public static readonly DependencyProperty ColorSelectorProperty =
                       DependencyProperty.RegisterAttached("ColorSelectorProperty", typeof(IColorSelector), typeof(PieChartLayout),
                       new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
		/// ColorSelector attached property accessors
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetColorSelector(UIElement element, IColorSelector value)
        {
            element.SetValue(ColorSelectorProperty, value);
        }

		/// <summary>
		/// Gets the value of the <see cref="ColorSelectorProperty"/>
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
        public static IColorSelector GetColorSelector(UIElement element)
        {
            return (IColorSelector)element.GetValue(ColorSelectorProperty);
        }


        #endregion

		/// <summary>
		/// <see cref="PieChartLayout"/> constructor
		/// </summary>
        public PieChartLayout()
        {
            InitializeComponent();
        }
    }
}
