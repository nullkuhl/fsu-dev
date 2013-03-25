using System;
using System.Collections;
using System.Windows.Forms;

namespace SystemInformation
{
	/// <summary>
	/// ListView items comparer
	/// </summary>
	public class ListViewItemComparer : IComparer
	{
		readonly int col;
		readonly bool sortOrder;

		/// <summary>
		/// <see cref="ListViewItemComparer"/> constructor
		/// </summary>
		/// <param name="column"></param>
		/// <param name="order"></param>
		public ListViewItemComparer(int column, bool order)
		{
			col = column;
			sortOrder = order;
		}

		#region IComparer Members

		/// <summary>
		/// Compares ListView items
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public int Compare(object x, object y)
		{
			try
			{
				// Validate Inputs.
				if (x != null && y != null)
				{
					// Convert x and y to a and b as ListViewItem.
					var a = (ListViewItem) x;
					var b = (ListViewItem) y;

					if (sortOrder)
					{
						return String.Compare(a.SubItems[col].Text, b.SubItems[col].Text);
					}
					return String.Compare(b.SubItems[col].Text, a.SubItems[col].Text);
				}
				return 0;
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

		#endregion
	}
}