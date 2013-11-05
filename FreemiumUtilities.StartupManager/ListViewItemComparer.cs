using System;
using System.Collections;
using System.Windows.Forms;

namespace FreemiumUtilities.StartupManager
{
    /// <summary>
    /// ListViewItem comparer class
    /// </summary>
    public class ListViewItemComparer : IComparer
    {
        readonly int col;
        readonly bool sortOrder;

        #region Constructor

        /// <summary>
        /// constructor for ListViewItemComparer
        /// </summary>
        /// <param name="column"></param>
        /// <param name="order"></param>
        public ListViewItemComparer(int column, bool order)
        {
            col = column;
            sortOrder = order;
        }

        #endregion

        #region IComparer Members

        /// <summary>
        /// compare two objects
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            if (x is ListViewItem && y is ListViewItem)
            {
                try
                {
                    // Validate Inputs.
                    {
                        // Convert x and y to a and b as ListViewItem.
                        var a = (ListViewItem)x;
                        var b = (ListViewItem)y;

                        if (sortOrder)
                        {
                            return String.Compare(a.SubItems[col].Text, b.SubItems[col].Text);
                        }
                        return String.Compare(b.SubItems[col].Text, a.SubItems[col].Text);
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    return 0;
                }
                catch (ArgumentException)
                {
                    return 0;
                }
            }
            throw new ArgumentException("Object is not a ListViewItem");
        }

        #endregion
    }
}