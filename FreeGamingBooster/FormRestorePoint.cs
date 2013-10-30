using System;
using System.Collections;
using System.Windows.Forms;
using RegistryCleanerCore;
using FreemiumUtilities.Infrastructure;

namespace FreeGamingBooster
{
    /// <summary>
    /// System restore operations form
    /// </summary>
    public partial class FormRestorePoint : Form
    {
        readonly SystemRestore myUtil = new SystemRestore();

        /// <summary>
        /// constructor for FormRestorePoint
        /// </summary>
        public FormRestorePoint()
        {
            InitializeComponent();
            UpdateUILocalization();
        }

        /// <summary>
        /// Applies localized strings to the UI
        /// </summary>
        void UpdateUILocalization()
        {
            Text = WPFLocalizeExtensionHelpers.GetUIString("SystemRestore");
            tabPageRestore.Text = WPFLocalizeExtensionHelpers.GetUIString("SystemRestore");
            lblSelectRestorePoint.Text = WPFLocalizeExtensionHelpers.GetUIString("SelectRestorePoint");
            Backups.Text = WPFLocalizeExtensionHelpers.GetUIString("BackupsColumnHeader");
            CreationDate.Text = WPFLocalizeExtensionHelpers.GetUIString("CreationDateColumnHeader");
            btnCreate.Text = WPFLocalizeExtensionHelpers.GetUIString("CreateRestorePoint");
            btnDelete.Text = WPFLocalizeExtensionHelpers.GetUIString("DeleteRestorePoint");
            btnRestore.Text = WPFLocalizeExtensionHelpers.GetUIString("ButtonRestoreText");
            btnClose.Text = WPFLocalizeExtensionHelpers.GetUIString("Close");
        }

        /// <summary>
        /// FormRestorePoint Load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmRestorePoint_Load(object sender, EventArgs e)
        {
            try
            {
                listRestorePoint.View = View.List;
                listRestorePoint.Columns[0].Width = 0;
                listRestorePoint.ColumnWidthChanging += listRestorePoint_ColumnWidthChanging;

                Hashtable result = myUtil.GetRestorePoints();
                FillRestorePoints(result);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Fills the UI with a list of the actual system restore points
        /// </summary>
        /// <param name="restorePoints">System restore points collection</param>
        void FillRestorePoints(Hashtable restorePoints)
        {
            listRestorePoint.Items.Clear();
            listRestorePoint.BeginUpdate();

            foreach (DictionaryEntry item in restorePoints)
            {
                string[] value = item.Value.ToString().Split('|');

                listRestorePoint.View = View.Details;
                ListViewItem listItem = new ListViewItem(item.Key.ToString());
                listItem.SubItems.Add(value[1]);
                listItem.SubItems.Add(value[0]);

                listRestorePoint.Items.Add(listItem);
                listRestorePoint.EndUpdate();
            }

            listRestorePoint.EndUpdate();
        }

        /// <summary>
        /// Closes the FormRestorePoint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// listRestorePoint ColumnWidthChanging event handler.
        /// Sets the [0] column width to 0.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void listRestorePoint_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.NewWidth = 0;
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Creates a new system restore point
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                long lSeqNum;
                SysRestore.StartRestore("Free Gaming Booster " + DateTime.Now, out lSeqNum);

                MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("RestorePointCreated"),
                                WPFLocalizeExtensionHelpers.GetUIString("info"), MessageBoxButtons.OK, MessageBoxIcon.Information);


                Hashtable result = myUtil.GetRestorePoints();
                FillRestorePoints(result);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Restores the system from the selected restore point
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (listRestorePoint.SelectedItems.Count > 0)
                {
                    string currentValue = listRestorePoint.SelectedItems[0].Text;

                    Application.DoEvents();
                    if (
                        MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("ConfirmRestoreDesc"),
                                        WPFLocalizeExtensionHelpers.GetUIString("ConfirmRestore"), MessageBoxButtons.YesNo) ==
                        DialogResult.Yes)
                    {
                        myUtil.RestoreSystem(Convert.ToInt32(currentValue));
                    }
                }
                else
                {
                    MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("SelectBackup"),
                                    WPFLocalizeExtensionHelpers.GetUIString("info"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Deletes the selected system restore point
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (listRestorePoint.SelectedItems.Count > 0)
                {
                    string currentValue = listRestorePoint.SelectedItems[0].Text;

                    Application.DoEvents();
                    if (
                        MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("TryDeleteRestorePoint"),
                                        WPFLocalizeExtensionHelpers.GetUIString("ConfirmDelete"), MessageBoxButtons.YesNo) ==
                        DialogResult.Yes)
                    {
                        myUtil.DeleteRestorePoint(Convert.ToInt32(currentValue));
                        Hashtable result = myUtil.GetRestorePoints();
                        MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("RestorePointDeleted"),
                                        WPFLocalizeExtensionHelpers.GetUIString("info"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillRestorePoints(result);
                    }
                }
                else
                {
                    MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("SelectBackup"),
                                    WPFLocalizeExtensionHelpers.GetUIString("info"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Sorts the restore points UI grid element items by column click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void listRestorePoint_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ItemComparer sorter = listRestorePoint.ListViewItemSorter as ItemComparer;
            if (sorter == null)
            {
                sorter = new ItemComparer(e.Column) { Order = SortOrder.Ascending };
                listRestorePoint.ListViewItemSorter = sorter;
            }
            // if clicked column is already the column that is being sorted
            if (e.Column == sorter.Column)
            {
                // Reverse the current sort direction
                sorter.Order = sorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.Column = e.Column;
                sorter.Order = SortOrder.Ascending;
            }
            listRestorePoint.Sort();
        }
    }

    /// <summary>
    /// Compares two ListViewItems
    /// </summary>
    public class ItemComparer : IComparer
    {
        //column used for comparison
        public ItemComparer(int colIndex)
        {
            Column = colIndex;
            Order = SortOrder.None;
        }

        public int Column { get; set; }
        //Order of sorting
        public SortOrder Order { get; set; }

        #region IComparer Members

        public int Compare(object a, object b)
        {
            ListViewItem itemA = a as ListViewItem;
            ListViewItem itemB = b as ListViewItem;
            if (itemA == null && itemB == null)
                return 0;
            if (itemA == null)
                return -1;
            if (itemB == null)
                return 1;
            if (itemA == itemB)
                return 0;
            //alphabetic comparison
            int result = String.Compare(itemA.SubItems[Column].Text, itemB.SubItems[Column].Text);
            // if sort order is descending.
            if (Order == SortOrder.Descending)
                // Invert the value returned by Compare.
                result *= -1;
            return result;
        }

        #endregion
    }
}