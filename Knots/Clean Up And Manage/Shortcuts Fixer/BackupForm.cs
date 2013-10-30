using System;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace ShortcutsFixer
{
	/// <summary>
	/// Shortcuts fixer knot backup form
	/// </summary>
	public partial class BackupForm : Form
	{
		Restore restobj;

		/// <summary>
		/// constructor for Backup
		/// </summary>
		public BackupForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// initialize Backup
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Backup_Load(object sender, EventArgs e)
		{
			var culture = new CultureInfo(CfgFile.Get("Lang"));
			SetCulture(culture);

			restobj = new Restore();
			var myTable = new DataTable("dataTable");
			myTable.Columns.Add(rm.GetString("name"), typeof (string));
			myTable.Columns.Add(rm.GetString("path"), typeof (string));			
			myTable.Columns.Add("Date", typeof (DateTime));
			myTable.Columns.Add(rm.GetString("sel"), typeof (bool));

			myTable.ReadXmlSchema(@"schemefile");
			myTable.ReadXml(@"datafile");
			string str = null;

			for (int i = 0; i < myTable.Rows.Count; i++)
			{
				string item = Convert.ToString(myTable.Rows[i][2]);
				if (str != item)
				{
					lsbMain.Items.Add(item);
					str = item;
				}
			}
		}

		/// <summary>
		/// handle MouseDoubleClick event to restore system
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void lsbMain_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				restobj.Datekey = lsbMain.SelectedItem.ToString();
				restobj.ShowDialog();
				Close();
			}
			catch
			{
			}
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
            ResourceManager rm = new ResourceManager("ShortcutsFixer.Resources", typeof(BackupForm).Assembly);
			Thread.CurrentThread.CurrentUICulture = culture;
			lblRestorePoints.Text = rm.GetString("following_restore_points");
			Text = rm.GetString("backup");
		}
	}
}