using System;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace Disk_Cleaner
{
	/// <summary>
	/// Restore form of the Disk cleaner knot
	/// </summary>
	public partial class FormRestore : Form
	{
		bool ABORT;
		string backuppath;

		/// <summary>
		/// constructor for FormRestore
		/// </summary>
		public FormRestore()
		{
			InitializeComponent();
		}

		/// <summary>
		/// initialize FormRestore
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FormRestore_Load(object sender, EventArgs e)
		{
            CultureInfo culture = new CultureInfo(CfgFile.Get("Lang"));
			SetCulture(culture);

			lvBackups.Items.Clear();
			backuppath = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar +
			             "backup" + Path.DirectorySeparatorChar;
			if (!Directory.Exists(backuppath)) return;
			string[] dirs = Directory.GetDirectories(backuppath);
			foreach (string str in dirs)
			{
				string fileName = Path.GetFileName(str);
				if (!string.IsNullOrEmpty(fileName) && fileName.Length != 14) continue;
				DateTime val;
				if (!DateTime.TryParseExact(Path.GetFileName(str), "yyyyMMddHHmmss",
				                            CultureInfo.CurrentCulture.DateTimeFormat,
				                            DateTimeStyles.AssumeLocal, out val)) continue;
                ListViewItem item = new ListViewItem(rm.GetString("backup") + " " + val.ToString());
				item.Tag = val;
				string[] files = Directory.GetFiles(str);
				long size = 0;
				foreach (string f in files)
					try
					{
						var info = new FileInfo(f);
						size += info.Length;
					}
					catch
					{
					}
				item.SubItems.Add((size/Math.Pow(1024, 2)).ToString("N2") + " MB");
				lvBackups.Items.Add(item);
			}
		}

		/// <summary>
		/// handle Click event to delete checked items
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Delete_Click(object sender, EventArgs e)
		{
			if (lvBackups.SelectedItems.Count == 0) return;

			//Show Processing
            FormProcessing objProc = new FormProcessing();
			objProc.Show();

			foreach (ListViewItem item in lvBackups.SelectedItems)
			{
				if (ABORT)
				{
					ABORT = false;
					return;
				}				
				Thread.Sleep(3000);
			}

			//Close Processing Form
			objProc.Close();

			FormRestore_Load(this, e);
		}

		/// <summary>
		/// handle Click event to restore checked items
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Restore_Click(object sender, EventArgs e)
		{
			if (lvBackups.SelectedItems.Count == 0) return;

			//Show Processing
			var objProc = new FormProcessing();
			objProc.Show();

			Clean.OnProgress += Clean_OnProgress;
			foreach (ListViewItem item in lvBackups.SelectedItems)
			{
				if (ABORT)
				{
					ABORT = false;
					return;
				}

				Clean.RestoreFolder(((DateTime) item.Tag).ToString("yyyyMMddHHmmss"), chbOwerwrite.Checked);
			}

			//Close Processing Form
			objProc.Close();

			Clean.OnProgress -= Clean_OnProgress;
			FormRestore_Load(this, e);

			MessageBox.Show(rm.GetString("FilesRestored"), rm.GetString("info"), MessageBoxButtons.OK, MessageBoxIcon.Information);
			Close();
		}

		void Clean_OnProgress(object sender, EventArgs e)
		{
			Application.DoEvents();
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
			var resourceManager = new ResourceManager("Disk_Cleaner.Resources", typeof (FormRestore).Assembly);
			Thread.CurrentThread.CurrentUICulture = culture;

			lblSelect.Text = resourceManager.GetString("select_backup") + ".";
			clhDate.Text = resourceManager.GetString("creation_date");
			clhSize.Text = resourceManager.GetString("size");
			chbOwerwrite.Text = resourceManager.GetString("overwrite_existing_files");
			Delete.Text = resourceManager.GetString("delete");
			Restore.Text = resourceManager.GetString("restore");
			Cancel.Text = resourceManager.GetString("cancel");
			Text = resourceManager.GetString("restore");
		}

		/// <summary>
		/// handle Click event to cancel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Cancel_Click(object sender, EventArgs e)
		{
			ABORT = true;
		}
	}
}