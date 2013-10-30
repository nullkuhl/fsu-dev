using System.ComponentModel;

namespace RegistryCleaner.Models
{
	/// <summary>
	/// Registry category model
	/// </summary>
	public class RegistryCategory : INotifyPropertyChanged
	{
		#region Properties

		string image;
		bool isChecked;
		string itemsCount;

		string title;

		/// <summary>
		/// Is registry category checked
		/// </summary>
		public bool IsChecked
		{
			get { return isChecked; }
			set
			{
				isChecked = value;
				OnPropertyChanged("IsChecked");
			}
		}

		/// <summary>
		/// Title
		/// </summary>
		public string Title
		{
			get { return title; }
			set
			{
				title = value;
				OnPropertyChanged("Title");
			}
		}

		/// <summary>
		/// Image
		/// </summary>
		public string Image
		{
			get { return image; }
			set
			{
				image = value;
				OnPropertyChanged("Image");
			}
		}

		/// <summary>
		/// Items count
		/// </summary>
		public string ItemsCount
		{
			get { return itemsCount; }
			set
			{
				itemsCount = value;
				OnPropertyChanged("ItemsCount");
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Registry category constructor
		/// </summary>
		/// <param name="title">Title</param>
		/// <param name="image">Image</param>
		public RegistryCategory(string title, string image)
		{
			Title = title;
			Image = image;
		}

		#endregion

		#region INotifyPropertyChanged

		/// <summary>
		/// Property changed event handler
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		#endregion
	}
}