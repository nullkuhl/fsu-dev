using System;
using System.Windows;
using System.Windows.Data;
using ScottLogic.ScottLogic.PieChart;
using System.ComponentModel;
using System.Collections.Specialized;

namespace ScottLogic.Controls.PieChart
{
    /// <summary>
    /// A pie chart legend
    /// </summary>
    public partial class Legend
    {
        #region dependency properties

        /// <summary>
        /// The property of the bound object that will be plotted
        /// </summary>
        public String PlottedProperty
        {
            get { return PieChartLayout.GetPlottedProperty(this); }
            set { PieChartLayout.SetPlottedProperty(this, value); }
        }

        /// <summary>
        /// A class which selects a color based on the item being rendered.
        /// </summary>
        public IColorSelector ColorSelector
        {
            get { return PieChartLayout.GetColorSelector(this); }
            set { PieChartLayout.SetColorSelector(this, value); }
        }

        #endregion

		/// <summary>
		/// Legend constructor
		/// </summary>
        public Legend()
        {
            // register any dependency property change handlers
            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(PieChartLayout.PlottedPropertyProperty, typeof(PiePlotter));
            dpd.AddValueChanged(this, PlottedPropertyChanged);
            DataContextChanged += DataContextChangedHandler;
            InitializeComponent();
        }


        #region property change handlers

        /// <summary>
        /// Handle changes in the datacontext. When a change occurs handlers are registered for events which
        /// occur when the collection changes or any items within teh collection change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataContextChangedHandler(object sender, DependencyPropertyChangedEventArgs e)
        {
            // handle the events that occur when the bound collection changes
        	var observable = DataContext as INotifyCollectionChanged;
        	if (observable != null)
        	{
        		observable.CollectionChanged += BoundCollectionChanged;
        	}

            ObserveBoundCollectionChanges();
        }

        #endregion

        #region event handlers

        /// <summary>
        /// Handles events which are raised when the bound collection changes (i.e. items added/removed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BoundCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshView();
            ObserveBoundCollectionChanges();
        }

        /// <summary>
        /// Handles changes to the PlottedProperty property.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PlottedPropertyChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        /// <summary>
        /// Iterates over the items inthe bound collection, adding handlers for PropertyChanged events
        /// </summary>
        void ObserveBoundCollectionChanges()
        {
            var myCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);

            foreach (object item in myCollectionView)
            {
            	var observable = item as INotifyPropertyChanged;
            	if (observable != null)
            	{
            		observable.PropertyChanged += ItemPropertyChanged;
            	}
            }
        }

        /// <summary>
        /// Handles events which occur when the properties of bound items change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // if the property which this pie chart represents has changed, re-construct the pie
            if (e.PropertyName.Equals(PlottedProperty))
            {
                RefreshView();
            }
        }

        #endregion

        /// <summary>
        /// Refreshes the view, re-computing any value which is derived from the data bindings
        /// </summary>
        void RefreshView()
        {
            // when the PlottedProperty changes we need to recompute our bindings. However,
            // the legend is bound to the collection items, the properties of which have not changes.
            // Therefore, we use a bit of an ugly hack to fool the legend into thinking the datacontext
            // has changed which causes it to replot itself.
            object context = legend.DataContext;
            if (context != null)
            {
                legend.DataContext = null;
                legend.DataContext = context;
            }
        }

    }
}
