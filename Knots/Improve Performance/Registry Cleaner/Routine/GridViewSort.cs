using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

/// <summary>
/// The <see cref="RegistryCleaner.Routine"/> namespace contains the routine classes of the Registry Cleaner knot
/// </summary>
namespace RegistryCleaner.Routine
{
    /// <summary>
    /// Grid view sorting
    /// </summary>
    public class GridViewSort
    {
        #region Public attached properties

        /// <summary>
        /// Command
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(GridViewSort),
                new UIPropertyMetadata(
                    null,
                    (o, e) =>
                    {
                        var listView = o as ItemsControl;
                        if (listView != null)
                        {
                            if (!GetAutoSort(listView)) // Don't change click handler if AutoSort enabled
                            {
                                if (e.OldValue != null && e.NewValue == null)
                                {
                                    listView.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                                }
                                if (e.OldValue == null && e.NewValue != null)
                                {
                                    listView.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                                }
                            }
                        }
                    }
                    )
                );

        /// <summary>
        /// Autosort
        /// </summary>
        public static readonly DependencyProperty AutoSortProperty =
            DependencyProperty.RegisterAttached(
                "AutoSort",
                typeof(bool),
                typeof(GridViewSort),
                new UIPropertyMetadata(
                    false,
                    (o, e) =>
                    {
                        var listView = o as ListView;
                        if (listView != null)
                        {
                            if (GetCommand(listView) == null) // Don't change click handler if a command is set
                            {
                                var oldValue = (bool)e.OldValue;
                                var newValue = (bool)e.NewValue;
                                if (oldValue && !newValue)
                                {
                                    listView.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                                }
                                if (!oldValue && newValue)
                                {
                                    listView.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                                }
                            }
                        }
                    }
                    )
                );

        /// <summary>
        /// Name
        /// </summary>
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.RegisterAttached(
                "PropertyName",
                typeof(string),
                typeof(GridViewSort),
                new UIPropertyMetadata(null)
                );

        /// <summary>
        /// Show sort glyph
        /// </summary>
        public static readonly DependencyProperty ShowSortGlyphProperty =
            DependencyProperty.RegisterAttached("ShowSortGlyph", typeof(bool), typeof(GridViewSort),
                                                new UIPropertyMetadata(true));

        /// <summary>
        /// Sort glyph ascending
        /// </summary>
        public static readonly DependencyProperty SortGlyphAscendingProperty =
            DependencyProperty.RegisterAttached("SortGlyphAscending", typeof(ImageSource), typeof(GridViewSort),
                                                new UIPropertyMetadata(null));

        /// <summary>
        /// Sort glyph descending
        /// </summary>
        public static readonly DependencyProperty SortGlyphDescendingProperty =
            DependencyProperty.RegisterAttached("SortGlyphDescending", typeof(ImageSource), typeof(GridViewSort),
                                                new UIPropertyMetadata(null));

        /// <summary>
        /// Gets a command
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Command instance</returns>
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        /// <summary>
        /// Set command
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...

        /// <summary>
        /// Gets auto sort
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>AutoSort property value</returns>
        public static bool GetAutoSort(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoSortProperty);
        }

        /// <summary>
        /// Sets auto sort
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetAutoSort(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoSortProperty, value);
        }

        // Using a DependencyProperty as the backing store for AutoSort.  This enables animation, styling, binding, etc...

        /// <summary>
        /// Gets property name
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Property name</returns>
        public static string GetPropertyName(DependencyObject obj)
        {
            return (string)obj.GetValue(PropertyNameProperty);
        }

        /// <summary>
        /// Sets property name
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetPropertyName(DependencyObject obj, string value)
        {
            obj.SetValue(PropertyNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for PropertyName.  This enables animation, styling, binding, etc...

        /// <summary>
        /// Gets show sort glyph
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetShowSortGlyph(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowSortGlyphProperty);
        }

        /// <summary>
        /// Sets show sort glyph
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetShowSortGlyph(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowSortGlyphProperty, value);
        }

        // Using a DependencyProperty as the backing store for ShowSortGlyph.  This enables animation, styling, binding, etc...

        /// <summary>
        /// Gets sort glyph ascending
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ImageSource GetSortGlyphAscending(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(SortGlyphAscendingProperty);
        }

        /// <summary>
        /// Sets sort glyph ascending
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetSortGlyphAscending(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(SortGlyphAscendingProperty, value);
        }

        // Using a DependencyProperty as the backing store for SortGlyphAscending.  This enables animation, styling, binding, etc...

        /// <summary>
        /// Gets sort glyph descending
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ImageSource GetSortGlyphDescending(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(SortGlyphDescendingProperty);
        }

        /// <summary>
        /// Sets sort glyph descending
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetSortGlyphDescending(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(SortGlyphDescendingProperty, value);
        }

        // Using a DependencyProperty as the backing store for SortGlyphDescending.  This enables animation, styling, binding, etc...

        #endregion

        #region attached properties

        static readonly DependencyProperty SortedColumnHeaderProperty =
            DependencyProperty.RegisterAttached("SortedColumnHeader", typeof(GridViewColumnHeader), typeof(GridViewSort),
                                                new UIPropertyMetadata(null));

        static GridViewColumnHeader GetSortedColumnHeader(DependencyObject obj)
        {
            return (GridViewColumnHeader)obj.GetValue(SortedColumnHeaderProperty);
        }

        static void SetSortedColumnHeader(DependencyObject obj, GridViewColumnHeader value)
        {
            obj.SetValue(SortedColumnHeaderProperty, value);
        }

        // Using a DependencyProperty as the backing store for SortedColumn.  This enables animation, styling, binding, etc...

        #endregion

        #region Column header click event handler

        static void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked != null && headerClicked.Column != null)
            {
                string propertyName = GetPropertyName(headerClicked.Column);
                if (!string.IsNullOrEmpty(propertyName))
                {
                    var listView = GetAncestor<ListView>(headerClicked);
                    if (listView != null)
                    {
                        ICommand command = GetCommand(listView);
                        if (command != null)
                        {
                            if (command.CanExecute(propertyName))
                            {
                                command.Execute(propertyName);
                            }
                        }
                        else if (GetAutoSort(listView))
                        {
                            ApplySort(listView.Items, propertyName, listView, headerClicked);
                        }
                    }
                }
            }
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Gets ancestor for specified object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reference"></param>
        /// <returns>Ancestor for specified object</returns>
        public static T GetAncestor<T>(DependencyObject reference) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(reference);
            while (!(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return (T)parent;
        }

        /// <summary>
        /// Applies sort
        /// </summary>
        /// <param name="view"></param>
        /// <param name="propertyName"></param>
        /// <param name="listView"></param>
        /// <param name="sortedColumnHeader"></param>
        public static void ApplySort(ICollectionView view, string propertyName, ListView listView, GridViewColumnHeader sortedColumnHeader)
        {
            ListSortDirection direction = ListSortDirection.Ascending;
            if (view.SortDescriptions.Count > 0)
            {
                SortDescription currentSort = view.SortDescriptions[0];
                if (currentSort.PropertyName == propertyName)
                {
                    direction = currentSort.Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                }
                view.SortDescriptions.Clear();

                GridViewColumnHeader currentSortedColumnHeader = GetSortedColumnHeader(listView);
                if (currentSortedColumnHeader != null)
                {
                    RemoveSortGlyph(currentSortedColumnHeader);
                }
            }
            if (!string.IsNullOrEmpty(propertyName))
            {
                view.SortDescriptions.Add(new SortDescription(propertyName, direction));
                if (GetShowSortGlyph(listView))
                    AddSortGlyph(
                        sortedColumnHeader,
                        direction,
                        direction == ListSortDirection.Ascending ? GetSortGlyphAscending(listView) : GetSortGlyphDescending(listView));
                SetSortedColumnHeader(listView, sortedColumnHeader);
            }
        }

        static void AddSortGlyph(GridViewColumnHeader columnHeader, ListSortDirection direction, ImageSource sortGlyph)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(columnHeader);
            adornerLayer.Add(
                new SortGlyphAdorner(
                    columnHeader,
                    direction,
                    sortGlyph
                    ));
        }

        static void RemoveSortGlyph(GridViewColumnHeader columnHeader)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(columnHeader);
            Adorner[] adorners = adornerLayer.GetAdorners(columnHeader);
            if (adorners != null)
            {
                foreach (Adorner adorner in adorners)
                {
                    if (adorner is SortGlyphAdorner)
                        adornerLayer.Remove(adorner);
                }
            }
        }

        #endregion

        #region SortGlyphAdorner nested class

        class SortGlyphAdorner : Adorner
        {
            readonly GridViewColumnHeader columnHeader;
            readonly ListSortDirection direction;
            readonly ImageSource sortGlyph;

            /// <summary>
            /// SortGlyphAdorner constructor
            /// </summary>
            /// <param name="columnHeader"></param>
            /// <param name="direction"></param>
            /// <param name="sortGlyph"></param>
            public SortGlyphAdorner(GridViewColumnHeader columnHeader, ListSortDirection direction, ImageSource sortGlyph)
                : base(columnHeader)
            {
                this.columnHeader = columnHeader;
                this.direction = direction;
                this.sortGlyph = sortGlyph;
            }

            Geometry GetDefaultGlyph()
            {
                double x1 = columnHeader.ActualWidth - 13;
                double x2 = x1 + 10;
                double x3 = x1 + 5;
                double y1 = columnHeader.ActualHeight / 2 - 3;
                double y2 = y1 + 5;

                if (direction == ListSortDirection.Ascending)
                {
                    double tmp = y1;
                    y1 = y2;
                    y2 = tmp;
                }

                var pathSegmentCollection = new PathSegmentCollection { new LineSegment(new Point(x2, y1), true), new LineSegment(new Point(x3, y2), true) };

                var pathFigure = new PathFigure(
                    new Point(x1, y1),
                    pathSegmentCollection,
                    true);

                var pathFigureCollection = new PathFigureCollection { pathFigure };

                var pathGeometry = new PathGeometry(pathFigureCollection);
                return pathGeometry;
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                base.OnRender(drawingContext);

                if (sortGlyph != null)
                {
                    double x = columnHeader.ActualWidth - 13;
                    double y = columnHeader.ActualHeight / 2 - 5;
                    var rect = new Rect(x, y, 10, 10);
                    drawingContext.DrawImage(sortGlyph, rect);
                }
                else
                {
                    drawingContext.DrawGeometry(Brushes.LightGray, new Pen(Brushes.Gray, 1.0), GetDefaultGlyph());
                }
            }
        }

        #endregion
    }
}