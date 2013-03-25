using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace RegistryCompactor
{
	/// <summary>
	/// Interaction logic for RegistryInfo.xaml
	/// </summary>
	public partial class RegistryInfo
	{
		readonly ObservableCollection<KeyValuePair<string, decimal>> registryFiles = new ObservableCollection<KeyValuePair<string, decimal>>();

		/// <summary>
		/// Registry files collection
		/// </summary>
		public ObservableCollection<KeyValuePair<string, decimal>> RegistryFiles
		{
			get { return registryFiles; }
		}

		/// <summary>
		/// <see cref="RegistryInfo"/> constructor
		/// </summary>
		public RegistryInfo()
		{
			InitializeComponent();

			// Get size of registry
			long totalRegSize = Utilites.GetOldRegistrySize();

			if (totalRegSize <= 0)
			{
				MessageBox.Show(this, Properties.Resources.RegistryHiveLoadError, Properties.Resources.RegistryCompactor, MessageBoxButton.OK, MessageBoxImage.Error);
				Close();
				return;
			}

			operatingSys.Text = OperationSystemInfo.GetVersion();
			registrySize.Text = Utilites.ConvertSizeToString(totalRegSize);

			foreach (Hive h in Compactor.RegistryHives)
			{
				decimal hiveSizeMB = decimal.Round(Convert.ToDecimal(h.OldHiveSize) / 1024 / 1024, 2);

				string hiveInfo = h.HiveFileInfo.ToString();
				string[] info = hiveInfo.Split('\\');

				hiveInfo = info[info.Length - 2] + "\\"+  info[info.Length - 1];
				string hiveName = string.Format("{0} ({1} MB)", hiveInfo, hiveSizeMB);

				RegistryFiles.Add(new KeyValuePair<string, decimal>(hiveName, hiveSizeMB));
			}
			piePlotter.DataContext = RegistryFiles;
		}
	}
}
