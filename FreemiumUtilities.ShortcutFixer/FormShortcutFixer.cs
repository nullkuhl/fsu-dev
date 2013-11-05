using System.Collections.Generic;
using System.Windows.Forms;
using FreemiumUtilities.Infrastructure;

namespace FreemiumUtilities.ShortcutFixer
{
	/// <summary>
	/// ShortcutFixer 1 Click-Maintenance application main form
	/// </summary>
	public partial class FrmShortcutFixer : Form
	{
		/// <summary>
		/// constructor for frmShortcutFixer
		/// </summary>
		public FrmShortcutFixer()
		{
			InitializeComponent();
			UpdateUILocalization();
		}

		/// <summary>
		/// Broken shortcuts collection
		/// </summary>
		public List<Shortcut> BrokenShortcuts
		{
			set
			{
				shortcutsLst.BeginUpdate();
				shortcutsLst.Items.Clear();

				foreach (Shortcut shortcut in value)
				{
                    ListViewItem listItem = new ListViewItem(new[]
					                                	{
					                                		shortcut.Name,
					                                		shortcut.Target,
					                                		shortcut.Location,
					                                		shortcut.Description
					                                	});
					shortcutsLst.Items.Add(listItem);
				}

				shortcutsLst.EndUpdate();
			}
		}

		/// <summary>
		/// Problems collection
		/// </summary>
		public List<ListViewItem> Problems
		{
			get
			{
                List<ListViewItem> problemList = new List<ListViewItem>();
				foreach (ListViewItem item in shortcutsLst.Items)
				{
					problemList.Add(item);
				}

				return problemList;
			}
		}

		/// <summary>
		/// Applies localized strings to the UI
		/// </summary>
		void UpdateUILocalization()
		{
			colDescription.Text = WPFLocalizeExtensionHelpers.GetUIString("ColumnShortcutDescription");
			colLocation.Text = WPFLocalizeExtensionHelpers.GetUIString("ColumnShortcutLocation");
			colName.Text = WPFLocalizeExtensionHelpers.GetUIString("ColumnShortcutName");
			colTarget.Text = WPFLocalizeExtensionHelpers.GetUIString("ColumnShortcutTarget");
		}
	}
}