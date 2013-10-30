using System.Collections.Generic;
using System.Windows.Forms;
using FreemiumUtilities.Infrastructure;

namespace FreemiumUtilities.Spyware
{
	/// <summary>
	/// Spyware remover 1 Click-Maintenance application main form
	/// </summary>
	public partial class FrmSpyware : Form
	{
		/// <summary>
		/// construtor for frmSpyware
		/// </summary>
		public FrmSpyware()
		{
			InitializeComponent();
			UpdateUILocalization();
		}

		/// <summary>
		/// Sets the list of spyware found
		/// </summary>
		public List<SpywareInfo> SpywareFound
		{
			set
			{
				spywareLst.BeginUpdate();
				foreach (SpywareInfo spyware in value)
				{
                    ListViewItem listItem = new ListViewItem(new[]
					                                	{
					                                		spyware.Spyware,
					                                		spyware.FilePath
					                                	});
					spywareLst.Items.Add(listItem);
				}
				spywareLst.EndUpdate();
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
				foreach (ListViewItem item in spywareLst.Items)
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
            Text = WPFLocalizeExtensionHelpers.GetUIString("SpywareRemover");
            colFilePath.Text = WPFLocalizeExtensionHelpers.GetUIString("ColumnSpywareFile");
			colSpyware.Text = WPFLocalizeExtensionHelpers.GetUIString("ColumnSpyware");
		}
	}
}