using System;
using System.Collections;
using System.Windows.Forms;

namespace EmptyFolderFinder
{
	/// <summary>
	/// ListView item comparer
	/// </summary>
	public class ListViewItemComparer : IComparer
	{
		bool descending;

		/// <summary>
		/// ListViewItemComparer constructor
		/// </summary>
		/// <param name="columnIndex">Column index</param>
		public ListViewItemComparer(int columnIndex)
		{
			Column = columnIndex;
		}

		/// <summary>
		/// Column index
		/// </summary>
		public int Column { get; set; }

		/// <summary>
		/// Is numeric
		/// </summary>
		public bool Numeric { get; set; }

		/// <summary>
		/// Is descending
		/// </summary>
		public bool Descending
		{
			get { return descending; }
			set { descending = value; }
		}

		#region IComparer Members

		/// <summary>
		/// Compare
		/// </summary>
		/// <param name="x">x</param>
		/// <param name="y">y</param>
		/// <returns>Result of comparing <paramref name="x"/> and <paramref name="y"/></returns>
		public int Compare(object x, object y)
		{
			ListViewItem listX, listY;
			if (descending)
			{
				listY = (ListViewItem) x;
				listX = (ListViewItem) y;
			}
			else
			{
				listX = (ListViewItem) x;
				listY = (ListViewItem) y;
			}
			if (Numeric)
			{
				// Convert column text to numbers before comparing.
				// If the conversion fails, the value defaults to 0.
				decimal valX, valY;
				Decimal.TryParse(listX.SubItems[Column].Text, out valX);
				Decimal.TryParse(listY.SubItems[Column].Text, out valY);
				// Perform a numeric comparison.
				return Decimal.Compare(valX, valY);
			}
			// Perform an alphabetic comparison.
			return String.Compare(
				listX.SubItems[Column].Text, listY.SubItems[Column].Text);
		}

		#endregion
	}
}