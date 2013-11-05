using System.ComponentModel;

namespace ShortcutsFixer.Models
{
	/// <summary>
	/// The <see cref="ShortcutsFixer.Models"/> namespace contains a model classes of the Shortcuts fixer knot
	/// </summary>

	/// <summary>
	/// Shortcut item model
	/// </summary>
	public class ShortcutItem : INotifyPropertyChanged
	{
		bool isChecked;
		string linksTo;

		string name;
		string path;

		/// <summary>
		/// Is checked
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
		/// Name
		/// </summary>
		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				OnPropertyChanged("Name");
			}
		}

		/// <summary>
		/// Links to
		/// </summary>
		public string LinksTo
		{
			get { return linksTo; }
			set
			{
				linksTo = value;
				OnPropertyChanged("LinksTo");
			}
		}

		/// <summary>
		/// Path
		/// </summary>
		public string Path
		{
			get { return path; }
			set
			{
				path = value;
				OnPropertyChanged("Path");
			}
		}

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