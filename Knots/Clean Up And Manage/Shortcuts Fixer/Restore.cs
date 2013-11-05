using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace ShortcutsFixer
{
	/// <summary>
	/// Shortcuts fixer knot restore form
	/// </summary>
	public partial class Restore : Form
	{
		public string Datekey;
		DataTable myTable;

		/// <summary>
		/// Constructor for the restore form
		/// </summary>
		public Restore()
		{
			InitializeComponent();
		}

		/// <summary>
		/// initialize Restore
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Restore_Load(object sender, EventArgs e)
		{
			var culture = new CultureInfo(CfgFile.Get("Lang"));
			SetCulture(culture);
			Shown += Restore_Shown;
			//backobj = new Backup();
			//backobj.ShowDialog();
			FillTable();
		}

		void Restore_Shown(object sender, EventArgs e)
		{
			dgwMain.Columns[1].Width = 230;
			dgwMain.Columns[0].HeaderText = "";
			dgwMain.Columns[2].Width = 230;
			dgwMain.Columns[3].Width = 230;
			dgwMain.Columns[0].Width = 30;
			dgwMain.Columns[2].ReadOnly = true;
			dgwMain.Columns[3].ReadOnly = true;
			dgwMain.Columns[1].ReadOnly = true;
		}

		void FillTable()
		{
			myTable = new DataTable("dataTable");
			string checkDate = Datekey;

			myTable.ReadXmlSchema(@"schemefile");
			myTable.ReadXml(@"datafile");

			dgwMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			//myTable.Columns.Add("Select", typeof(bool));
			//myTable.Columns[3].DefaultValue = true;

			//for (int i = 0; i < myTable.Rows.Count; i++)
			//{
			//    //MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString());
			//    myTable.Rows[i][3] = false;

			//}

			for (int i = 0; i < myTable.Rows.Count; i++)
			{
				//MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString());
				if (Convert.ToString(myTable.Rows[i][3].ToString()) == checkDate)
				{
					//dataGridView1.Rows.RemoveAt(i);
					//myTable.Rows.RemoveAt(i);
					//i--;
					myTable.Rows[i][0] = true;
				}
			}

			////dataGridView1.Columns[2].Visible = false;

			//myTable.DefaultView.RowFilter = "true";
			//myTable.DefaultView.FindRows(Convert.ToInt32(checkDate));

			string expression = "Date = '" + Convert.ToDateTime(checkDate) + "'";

			// Use the Select method to find all rows matching the filter.

			//myTable.Select(expression);
			dgwMain.DataSource = myTable;

			((DataTable) dgwMain.DataSource).DefaultView.RowFilter = expression;
		}

		void buttonRestore_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < dgwMain.Rows.Count; i++)
			{
				if (!Convert.ToBoolean(dgwMain.Rows[i].Cells[0].Value)) continue;
				try
				{
					string path = dgwMain.Rows[i].Cells[1].Value.ToString();
					File.Copy(@"Data\\" + path, dgwMain.Rows[i].Cells[2].Value.ToString(), true);
					File.Delete(@"Data\\" + path);
				}
				catch (Exception)
				{
					MessageBox.Show("Restore isn't a valid option, can't restore this file.");
				}
			}

			for (int i = 0; i < dgwMain.Rows.Count; i++)
			{
				//MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString());
				if (Convert.ToBoolean(dgwMain.Rows[i].Cells[0].Value))
				{
					dgwMain.Rows.RemoveAt(i);
					i--;
				}
			}
			myTable.WriteXml(@"datafile");
		}

		void buttonDelete_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < dgwMain.Rows.Count; i++)
			{
				if (!Convert.ToBoolean(dgwMain.Rows[i].Cells[0].Value)) continue;
				string path = dgwMain.Rows[i].Cells[0].Value.ToString();
				File.Delete(@"Data\\" + path);
			}

			for (int i = 0; i < dgwMain.Rows.Count; i++)
			{
				//MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString());
				if (Convert.ToBoolean(dgwMain.Rows[i].Cells[0].Value))
				{
					dgwMain.Rows.RemoveAt(i);
					i--;
				}
			}
			myTable.WriteXml(@"datafile");
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
			var rm = new ResourceManager("ShortcutsFixer.Resources", typeof(Restore).Assembly);
			Thread.CurrentThread.CurrentUICulture = culture;
			buttonRestore.Text = rm.GetString("restore");
			buttonDelete.Text = rm.GetString("delete");
			Text = rm.GetString("restore");
		}
	}
}