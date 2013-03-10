using System;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace ProcessManager
{
	/// <summary>
	/// Blocked processes form of the Process Manager knot
	/// </summary>
	public partial class FormBlockedProcesses : Form
	{
		string sStringToWrite;
		string[] words;

		/// <summary>
		/// constructor for FrmBlockedProcesses
		/// </summary>
		public FormBlockedProcesses()
		{
			InitializeComponent();
		}

		/// <summary>
		/// initialize FrmBlockedProcesses
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FrmBlockedProcesses_Load(object sender, EventArgs e)
		{
			SetCulture(new CultureInfo(CfgFile.Get("Lang")));
			PopulateListBox();
		}

		/// <summary>
		/// update the list of blocked processes
		/// </summary>
		public void PopulateListBox()
		{
			listView1.Items.Clear();

			var sr = new StreamReader("BlockedProcesses.txt");
			string str = sr.ReadLine();
			sr.Close();
			if (str != null)
			{
				const string delimStr = "~";
				char[] delimiter = delimStr.ToCharArray();

				words = str.Split(delimiter);

                foreach (string s in words)
                {
                    if (s != "" && s != String.Empty && s != "xml" && s != "processes" && !listView1.Items.ContainsKey(s))
                    {
                        ListViewItem lvi = new ListViewItem(s);
                        lvi.Name = s;
                        listView1.Items.Add(lvi);
                    }
                }
			}			
		}

		/// <summary>
		/// handle Click event to close form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// handle Click event to unblock process
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnRemove_Click(object sender, EventArgs e)
		{
			if (listView1.SelectedItems.Count > 0)
			{
				string processName = listView1.SelectedItems[0].Text;

				var sr = new StreamReader("BlockedProcesses.txt");
				string str = sr.ReadLine();
				sr.Close();
				if (str != null)
				{
					const string delimStr = "~";
					char[] delimiter = delimStr.ToCharArray();

					words = str.Split(delimiter);
				}
				foreach (string s in words)
				{
					try
					{
						if (!string.IsNullOrEmpty(s) && s != string.Empty && s != processName)
						{
							sStringToWrite = sStringToWrite + s + "~";
						}
					}
					catch
					{
					}
				}
				var sw = new StreamWriter("BlockedProcesses.txt");
				sw.Write(sStringToWrite);
				sw.Close();
				PopulateListBox();
				FormProcessManager.IsBlockProcessAdded = true;
			}
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
			var rm = new ResourceManager("ProcessManager.Resources", typeof (FormBlockedProcesses).Assembly);
			Thread.CurrentThread.CurrentUICulture = culture;
			btnClose.Text = rm.GetString("close", culture);
			btnRemove.Text = rm.GetString("remove", culture);
			Text = rm.GetString("blocked_proc", culture);
		}
	}
}