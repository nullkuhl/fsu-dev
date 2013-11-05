using System;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace EmptyFolderFinder
{
	/// <summary>
	/// An improved dialog, with OS styling, an icon, a large header, and smaller explanation text.
	/// </summary>
	public sealed partial class CustomDialog : Form
	{
		/// <summary>
		/// Dialog result
		/// </summary>
		public int Result;

		/// <summary>
		/// The constructor. This is only called by the static method ShowDialog.
		/// </summary>
		public CustomDialog(string title, string largeHeading, string smallExplanation, Image iconSet)
		{
			Font = SystemFonts.MessageBoxFont;
			ForeColor = SystemColors.WindowText;

			InitializeComponent();

			using (Graphics graphics = CreateGraphics())
			{
				if (string.IsNullOrEmpty(smallExplanation) == false)
				{
					{
						// might want to special case the old, MS Sans Serif font.
						// use the regular font but bold it for XP, etc.
						lblMessage.Font = new Font(SystemFonts.MessageBoxFont.FontFamily.Name, 8.0f,
						                       FontStyle.Bold, GraphicsUnit.Point);

						var smallSize = graphics.MeasureString(smallExplanation, Font, lblCaption.Width);
						Height = tlpLeft.Height + spcMain.Panel2.Height + (int) smallSize.Height - 10;
							// went from 164 to 168 to improve bottom space on XP.
					}
				}
				else
				{
					// hide the second table, which is used for the small text, but we don't have any.
					tlpRight.Visible = false;
					tlpRight.Height = 0;
				}
			}

			// set our text
			Text = title;
			lblMessage.Text = largeHeading;

			if (smallExplanation != null && (smallExplanation.Length > 82 && smallExplanation.IndexOf("\\") != -1 && smallExplanation.IndexOf(":") != -1 &&
			                                 smallExplanation.Split(':').Length > 1))
			{
				int index1 = smallExplanation.IndexOf("\\");
				index1 = smallExplanation.IndexOf("\\", index1 + 1);
				int index2 = smallExplanation.Split(':')[0].Length + 1 + smallExplanation.Split(':')[1].LastIndexOf("\\");
				smallExplanation = smallExplanation.Substring(0, index1 + 1) + "..." +
				                   smallExplanation.Substring(index2, smallExplanation.Length - index2);
			}
			lblCaption.Text = string.IsNullOrEmpty(smallExplanation) ? string.Empty : smallExplanation;

			AcceptButton = btnRetry;
			pcbMain.Image = iconSet;
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
            ResourceManager rm = new ResourceManager("EmptyFolderFinder.Resources", typeof(CustomDialog).Assembly);
			Thread.CurrentThread.CurrentUICulture = culture;

			btnAbort.Text = rm.GetString("abort_button");
			btnRetry.Text = rm.GetString("retry");
			btnIgnore.Text = rm.GetString("ignore");
			btnIgnoreAll.Text = rm.GetString("ignore_all");
		}

		/// <summary>
		/// handle Click event to retry
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnRetry_Click(object sender, EventArgs e)
		{
			Result = 4;
			Close();
		}

		/// <summary>
		/// handle Click event to ignore
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnIgnore_Click(object sender, EventArgs e)
		{
			Result = 5;
			Close();
		}

		/// <summary>
		/// handle Click event to ignore all
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnIgnoreAll_Click(object sender, EventArgs e)
		{
			Result = 8;
			Close();
		}

		/// <summary>
		/// initialize CustomDialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void CustomDialog_Load(object sender, EventArgs e)
		{
            CultureInfo culture = new CultureInfo(CfgFile.Get("Lang"));
			SetCulture(culture);
		}
	}
}