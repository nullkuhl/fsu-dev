using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace Disk_Cleaner
{
	/// <summary>
	/// View form of the Disk cleaner knot
	/// </summary>
	public partial class frmView : Form
	{
		readonly Dictionary<int, bool> checks;
		readonly ListViewColumnSorter sorter = new ListViewColumnSorter();

		/// <summary>
		/// constructor for FormView
		/// </summary>
		public frmView()
		{
			InitializeComponent();
			checks = new Dictionary<int, bool>();
			lvMain.ListViewItemSorter = sorter;
		}

		/// <summary>
		/// handle SelectedIndexChanged to view item details
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void lvMain_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lvMain.SelectedItems.Count != 0)
			{
				ListViewItem item = lvMain.SelectedItems[0];
				labelName.Text = rm.GetString("name") + ": " + item.Text;
				labelExt.Text = rm.GetString("ext") + ": " + item.SubItems[3].Text;
				labelSize.Text = rm.GetString("size") + ": " + item.SubItems[2].Text;
				FileInfo info = item.Tag != null && item.Tag is DeleteFile ? (item.Tag as DeleteFile).Info : null;
				labelCreated.Text = rm.GetString("created") + ": " + (info != null ? info.CreationTime.ToString() : "");
				labelAccess.Text = rm.GetString("last_access") + ": " + (info != null ? info.LastAccessTime.ToString() : "");
				labelModified.Text = rm.GetString("modified") + ": " + (info != null ? info.LastWriteTime.ToString() : "");
			}
			else
			{
				labelAccess.Text = labelCreated.Text = labelExt.Text =
				                                       labelModified.Text = labelName.Text = labelSize.Text = "";
				return;
			}
		}

		/// <summary>
		/// initialize FormView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FormView_Load(object sender, EventArgs e)
		{
			var culture = new CultureInfo(CfgFile.Get("Lang"));
			SetCulture(culture);

			lvMain.ItemChecked += lvMain_ItemChecked;
			checks.Clear();
		}

		/// <summary>
		/// handle ItemChecked event to update the state of item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void lvMain_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (checks.ContainsKey(e.Item.Index))
				checks[e.Item.Index] = e.Item.Checked;
			else
				checks.Add(e.Item.Index, e.Item.Checked);
		}


		/// <summary>
		/// handle FormClosing event to close form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FormView_FormClosing(object sender, FormClosingEventArgs e)
		{
			lvMain.ItemChecked -= lvMain_ItemChecked;
			e.Cancel = false;
		}

		/// <summary>
		/// handle Click event to update the list of items to be deleted
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonOK_Click(object sender, EventArgs e)
		{
			foreach (int index in checks.Keys)
			{
                DeleteFile deleteFile = lvMain.Items[index].Tag as DeleteFile;
				if (deleteFile != null && deleteFile.Delete != checks[index])
					(lvMain.Items[index].Tag as DeleteFile).Delete = checks[index];
			}
			DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// handel ColumnClick event to sort list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void lvMain_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			sorter.SortNumeric = e.Column == 2;
			// Determine if clicked column is already the column that is being sorted.
			if (e.Column == sorter.SortColumn)
			{
				// Reverse the current sort direction for this column.
				if (sorter.Order == SortOrder.Ascending)
				{
					sorter.Order = SortOrder.Descending;
				}
				else
				{
					sorter.Order = SortOrder.Ascending;
				}
			}
			else
			{
				// Set the column number that is to be sorted; default to ascending.
				sorter.SortColumn = e.Column;
				sorter.Order = SortOrder.Ascending;
			}

			// Perform the sort with these new sort options.
			lvMain.Sort();
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
			var resourceManager = new ResourceManager("Disk_Cleaner.Resources", typeof (FormMain).Assembly);
			Thread.CurrentThread.CurrentUICulture = culture;
			lblSelectPath.Text = resourceManager.GetString("select_files_to_delete");
			clhName.Text = resourceManager.GetString("name");
			clhPath.Text = resourceManager.GetString("path");
			clhSize.Text = resourceManager.GetString("size");
			clhType.Text = resourceManager.GetString("file_type");
			grbMain.Text = resourceManager.GetString("details");
            labelAccess.Text = string.Empty;
            labelModified.Text = string.Empty;
            labelCreated.Text = string.Empty;
            labelSize.Text = string.Empty;
            labelExt.Text = string.Empty;
			labelName.Text = string.Empty;
			buttonOK.Text = resourceManager.GetString("ok");
			buttonCancel.Text = resourceManager.GetString("cancel");
			Text = resourceManager.GetString("details");
		}
	}

	#region sorter class

	/// <summary>
	/// This class is an implementation of the 'IComparer' interface.
	/// </summary>
	public class ListViewColumnSorter : IComparer
	{
		/// <summary>
		/// Case insensitive comparer object
		/// </summary>
		readonly CaseInsensitiveComparer objectCompare;

		/// <summary>
		/// Specifies the column to be sorted
		/// </summary>
		int columnToSort;

		/// <summary>
		/// Specifies if sorting is on a numeric column
		/// </summary>
		bool numericSort;

		/// <summary>
		/// Specifies the order in which to sort (i.e. 'Ascending').
		/// </summary>
		SortOrder orderOfSort;

		/// <summary>
		/// Class constructor.  Initializes various elements
		/// </summary>
		public ListViewColumnSorter()
		{
			// Initialize the column to '0'
			columnToSort = 0;

			// Initialize the sort order to 'none'
			orderOfSort = SortOrder.None;

			// Initialize the CaseInsensitiveComparer object
			objectCompare = new CaseInsensitiveComparer();
		}

		/// <summary>
		/// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
		/// </summary>
		public int SortColumn
		{
			set { columnToSort = value; }
			get { return columnToSort; }
		}

		/// <summary>
		/// Gets or sets a value indicating if it is a numeric sort or not.
		/// </summary>
		public bool SortNumeric
		{
			set { numericSort = value; }
			get { return numericSort; }
		}

		/// <summary>
		/// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
		/// </summary>
		public SortOrder Order
		{
			set { orderOfSort = value; }
			get { return orderOfSort; }
		}

		#region IComparer Members

		/// <summary>
		/// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
		/// </summary>
		/// <param name="x">First object to be compared</param>
		/// <param name="y">Second object to be compared</param>
		/// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
		public int Compare(object x, object y)
		{
			int compareResult;

			// Cast the objects to be compared to ListViewItem objects
			var listviewX = (ListViewItem) x;
			var listviewY = (ListViewItem) y;

			var del1 = listviewX.Tag as DeleteFile;
			var del2 = listviewY.Tag as DeleteFile;

			// Compare the two items
			if (numericSort)
				compareResult = objectCompare.Compare(del1 != null && del1.Info != null ? del1.Info.Length : 0,
				                                      del2 != null && del2.Info != null ? del2.Info.Length : 0);
			else
				compareResult = objectCompare.Compare(
					listviewX.SubItems[columnToSort].Text,
					listviewY.SubItems[columnToSort].Text);

			// Calculate correct return value based on object comparison
			if (orderOfSort == SortOrder.Ascending)
			{
				// Ascending sort is selected, return normal result of compare operation
				return compareResult;
			}
			if (orderOfSort == SortOrder.Descending)
			{
				// Descending sort is selected, return negative result of compare operation
				return (-compareResult);
			}
			// Return '0' to indicate they are equal
			return 0;
		}

		#endregion
	}

	#endregion
}