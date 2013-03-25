using System.ComponentModel;

namespace FreemiumUtilities.RegCleaner.Models
{
    /// <summary>
    /// Registry category model
    /// </summary>
    public class RegistryCategory : INotifyPropertyChanged
    {
        #region Properties

        bool isChecked = true;
        string itemsCount;

        string title;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

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

        public RegistryCategory(string title)
        {
            Title = title;
        }

        #endregion

        #region INotifyPropertyChanged

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