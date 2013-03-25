using System;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;

namespace FileUndelete
{
    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        readonly CaseInsensitiveComparer ObjectCompare;

        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        int ColumnToSort;

        /// <summary>
        /// Specifies date and time format if sorting is on a date and time column
        /// </summary>
        string DatetimeSort;

        /// <summary>
        /// Specifies if sorting is on a numeric column
        /// </summary>
        bool NumericSort;

        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        SortOrder OrderOfSort;

        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set { ColumnToSort = value; }
            get { return ColumnToSort; }
        }

        /// <summary>
        /// Gets or sets a value indicating if it is a numeric sort or not.
        /// </summary>
        public bool SortNumeric
        {
            set
            {
                NumericSort = value;
                if (NumericSort) DatetimeSort = null;
            }
            get { return NumericSort; }
        }

        /// <summary>
        /// Gets or sets the date and time format for a date and time sort.
        /// </summary>
        public string SortDatetime
        {
            set { DatetimeSort = value; }
            get { return DatetimeSort; }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set { OrderOfSort = value; }
            get { return OrderOfSort; }
        }

        #region IComparer Members

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param Name="x">First object to be compared</param>
        /// <param Name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // Compare the two items
            decimal d1 = -1, d2 = -1;
            DateTime dt1 = DateTime.MinValue, dt2 = DateTime.MinValue;
            if (NumericSort)
            {
                decimal.TryParse(listviewX.SubItems[ColumnToSort].Text, out d1);
                decimal.TryParse(listviewY.SubItems[ColumnToSort].Text, out d2);
            }
            if (!string.IsNullOrEmpty(DatetimeSort))
            {
                DateTime.TryParseExact(
                    listviewX.SubItems[ColumnToSort].Text,
                    DatetimeSort,
                    CultureInfo.CurrentCulture.DateTimeFormat,
                    DateTimeStyles.AssumeLocal,
                    out dt1);
                DateTime.TryParseExact(
                    listviewY.SubItems[ColumnToSort].Text,
                    DatetimeSort,
                    CultureInfo.CurrentCulture.DateTimeFormat,
                    DateTimeStyles.AssumeLocal,
                    out dt2);
            }
            if (NumericSort && d1 != -1 && d2 != -1)
                compareResult = ObjectCompare.Compare(d1, d2);
            else if (!string.IsNullOrEmpty(DatetimeSort) &&
                     dt1 != DateTime.MinValue &&
                     dt2 != DateTime.MinValue)
                compareResult = ObjectCompare.Compare(dt1, dt2);
            else
                compareResult = ObjectCompare.Compare(
                    listviewX.SubItems[ColumnToSort].Text,
                    listviewY.SubItems[ColumnToSort].Text);

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        #endregion
    }
}
