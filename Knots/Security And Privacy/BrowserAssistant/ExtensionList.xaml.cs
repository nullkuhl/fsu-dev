using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace BrowserAssistant
{
    /// <summary>
    /// Interaction logic for ExtensionList.xaml
    /// </summary>
    public partial class ExtensionList
    {
        /// <summary>
        /// constructor for ExtensionList
        /// </summary>
        public ExtensionList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Extensions collection
        /// </summary>
        public IEnumerable<IBrowserExtension> Extensions
        {
            get { return (IEnumerable<IBrowserExtension>)ExtList.ItemsSource; }
            set { ExtList.ItemsSource = value; }
        }

        /// <summary>
        /// Save button clicked event handler
        /// </summary>
        public event RoutedEventHandler SaveClicked
        {
            add { SaveBtn.Click += value; }
            remove { SaveBtn.Click -= value; }
        }

        /// <summary>
        /// handle Click event to save extension
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            SaveBtn.IsEnabled = true;
        }

        /// <summary>
        /// handle Click event to close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}