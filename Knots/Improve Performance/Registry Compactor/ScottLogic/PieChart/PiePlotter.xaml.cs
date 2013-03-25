using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Collections.Specialized;
using System.ComponentModel;
using ScottLogic.Shapes;
using System.Windows.Media.Animation;
using ScottLogic.ScottLogic.PieChart;


namespace ScottLogic.Controls.PieChart
{
	/// <summary>
	/// Renders a bound dataset as a pie chart
	/// </summary>
	public partial class PiePlotter
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


		/// <summary>
		/// The size of the hole in the centre of circle (as a percentage)
		/// </summary>
		public double HoleSize
		{
			get { return (double)GetValue(HoleSizeProperty); }
			set
			{
				SetValue(HoleSizeProperty, value);
				ConstructPiePieces();
			}
		}

		/// <summary>
		/// Hole size <c>DependencyProperty</c>
		/// </summary>
		public static readonly DependencyProperty HoleSizeProperty =
					   DependencyProperty.Register("HoleSize", typeof(double), typeof(PiePlotter), new UIPropertyMetadata(0.0));


		#endregion


		/// <summary>
		/// A list which contains the current piece pieces, where the piece index
		/// is the same as the index of the item within the collection view which 
		/// it represents.
		/// </summary>
		readonly List<PiePiece> piePieces = new List<PiePiece>();

		/// <summary>
		/// <see cref="PiePlotter"/> constructor
		/// </summary>
		public PiePlotter()
		{
			// register any dependency property change handlers
			DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(PieChartLayout.PlottedPropertyProperty, typeof(PiePlotter));
			dpd.AddValueChanged(this, PlottedPropertyChanged);

			InitializeComponent();

			DataContextChanged += DataContextChangedHandler;
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

			// handle the selection change events
			var collectionView = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
			collectionView.CurrentChanged += CollectionViewCurrentChanged;
			collectionView.CurrentChanging += CollectionViewCurrentChanging;

			ConstructPiePieces();
			ObserveBoundCollectionChanges();
		}

		/// <summary>
		/// Handles changes to the PlottedProperty property.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void PlottedPropertyChanged(object sender, EventArgs e)
		{
			ConstructPiePieces();
		}

		#endregion

		#region event handlers

		///// <summary>
		///// Handles the MouseUp event from the individual Pie Pieces
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//void PiePieceMouseUp(object sender, MouseButtonEventArgs e)
		//{
		//    CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(this.DataContext);
		//    if (collectionView == null)
		//        return;

		//    PiePiece piece = sender as PiePiece;
		//    if (piece == null)
		//        return;

		//    // select the item which this pie piece represents
		//    int index = (int)piece.Tag;
		//    collectionView.MoveCurrentToPosition(index);
		//}

		/// <summary>
		/// Handles the event which occurs when the selected item is about to change
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void CollectionViewCurrentChanging(object sender, CurrentChangingEventArgs e)
		{
			var collectionView = (CollectionView)sender;

			if (collectionView != null && collectionView.CurrentPosition >= 0 && collectionView.CurrentPosition <= piePieces.Count)
			{
				var piece = piePieces[collectionView.CurrentPosition];
				var a = new DoubleAnimation { To = 0, Duration = new Duration(TimeSpan.FromMilliseconds(200)) };
				piece.BeginAnimation(PiePiece.PushOutProperty, a);
			}
		}

		/// <summary>
		/// Handles the event which occurs when the selected item has changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void CollectionViewCurrentChanged(object sender, EventArgs e)
		{
			var collectionView = (CollectionView)sender;

			if (collectionView != null && collectionView.CurrentPosition >= 0 && collectionView.CurrentPosition <= piePieces.Count)
			{
				PiePiece piece = piePieces[collectionView.CurrentPosition];
				var a = new DoubleAnimation { To = 0, Duration = new Duration(TimeSpan.FromMilliseconds(200)) };
				//a.To = 10;
				piece.BeginAnimation(PiePiece.PushOutProperty, a);
			}
		}

		/// <summary>
		/// Handles events which are raised when the bound collection changes (i.e. items added/removed)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BoundCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			ConstructPiePieces();
			ObserveBoundCollectionChanges();
		}

		/// <summary>
		/// Iterates over the items inthe bound collection, adding handlers for PropertyChanged events
		/// </summary>
		void ObserveBoundCollectionChanges()
		{
			var myCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(this.DataContext);
			foreach (var item in myCollectionView)
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
				ConstructPiePieces();
			}
		}

		#endregion

		double GetPlottedPropertyValue(object item)
		{
			PropertyDescriptorCollection filterPropDesc = TypeDescriptor.GetProperties(item);
			object itemValue = filterPropDesc[PlottedProperty].GetValue(item);

			//TODO possibel type conversion?
			return Convert.ToDouble(itemValue);
			//return (double)itemValue;
		}

		/// <summary>
		/// Constructs pie pieces and adds them to the visual tree for this control's canvas
		/// </summary>
		void ConstructPiePieces()
		{
			var myCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(this.DataContext);
			if (myCollectionView == null)
				return;

			double halfWidth = Width / 2;
			double innerRadius = halfWidth * HoleSize;

			// compute the total for the property which is being plotted
			double total = 0;
			foreach (Object item in myCollectionView)
			{
				total += GetPlottedPropertyValue(item);
			}

			// add the pie pieces
			canvas.Children.Clear();
			piePieces.Clear();

			double accumulativeAngle = 0;
			foreach (Object item in myCollectionView)
			{
				bool selectedItem = item == myCollectionView.CurrentItem;

				double wedgeAngle = GetPlottedPropertyValue(item) * 359.999 / total;

				var piece = new PiePiece
				            	{
						Radius = halfWidth,
						InnerRadius = innerRadius,
						CentreX = halfWidth,
						CentreY = halfWidth,
						PushOut = (selectedItem ? 10.0 : 0),
						WedgeAngle = wedgeAngle,
						PieceValue = GetPlottedPropertyValue(item),
						RotationAngle = accumulativeAngle,
						Fill = ColorSelector != null ? ColorSelector.SelectBrush(item, myCollectionView.IndexOf(item)) : Brushes.Black,
						// record the index of the item which this pie slice represents
						Tag = myCollectionView.IndexOf(item),
						ToolTip = new ToolTip()
					};

				piece.ToolTipOpening += PiePieceToolTipOpening;
				piece.ToolTipOpening += PiePieceToolTipOpening;
				//piece.MouseUp += new MouseButtonEventHandler(PiePieceMouseUp);

				piePieces.Add(piece);
				canvas.Children.Insert(0, piece);

				accumulativeAngle += wedgeAngle;
			}
		}

		/// <summary>
		/// Handles the event which occurs just before a pie piece tooltip opens
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void PiePieceToolTipOpening(object sender, ToolTipEventArgs e)
		{
			var piece = (PiePiece)sender;

			var collectionView = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
			if (collectionView == null)
				return;

			// select the item which this pie piece represents
			var index = (int)piece.Tag;
			if (piece.ToolTip != null)
			{
				var tip = (ToolTip)piece.ToolTip;
				tip.DataContext = collectionView.GetItemAt(index);
			}
		}

	}
}
