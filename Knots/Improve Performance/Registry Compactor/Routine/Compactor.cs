using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace RegistryCompactor
{
	/// <summary>
	/// RegistryOptimizer UserControl
	/// </summary>
	public class Compactor : UserControl
	{
		public static ObservableCollection<Hive> RegistryHives { get; set; }
	}
}