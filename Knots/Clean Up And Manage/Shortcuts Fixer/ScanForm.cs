using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using IWshRuntimeLibrary;

namespace ShortcutsFixer
{
	/// <summary>
	/// Shortcuts fixer knot scan form
	/// </summary>
	public partial class ScanForm : Form
	{
		/// <summary>
		/// WshShellClass
		/// </summary>
		public WshShellClass Obj;
		/// <summary>
		/// Close scan window
		/// </summary>
		public bool CloseScanWindow;
		/// <summary>
		/// current directory
		/// </summary>
		public int CurrDirectory;
		/// <summary>
		/// Datatable
		/// </summary>
		public DataTable MyTab;
		/// <summary>
		/// IWshShortcut
		/// </summary>
		public IWshShortcut TheLink;

		/// <summary>
		/// constructor for ScanForm
		/// </summary>
		public ScanForm()
		{
			InitializeComponent();
            if (!System.IO.File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\FreemiumUtilities.exe"))
            {
                this.Icon = Properties.Resources.PCCleanerIcon;
            }
            else
            {
                this.Icon = Properties.Resources.FSUIcon;
            }
		}

		/// <summary>
		/// Gets drive type
		/// </summary>
		/// <param name="lpRootPathName">lpRootPathName</param>
		/// <returns>Drive type</returns>
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetDriveType(string lpRootPathName);

		/// <summary>
		/// initialize ScanWindow
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ScanWindow_Load(object sender, EventArgs e)
		{
			var culture = new CultureInfo(CfgFile.Get("Lang"));
			SetCulture(culture);
			checkedListViewDrives.Items.Clear();
			GetDrives();
			MyTab = new DataTable();
			InitList();
			prbMain.Value = 0;
			CloseScanWindow = false;
			fileLabel.Text = string.Empty;
			Obj = new WshShellClass();
			RadioButtonCheckUpdate();
		}

		/// <summary>
		/// initialize the list of logical drives
		/// </summary>
		public void InitList()
		{
			MyTab = new DataTable();

			MyTab.Columns.Add(rm.GetString("sel"), typeof (bool));
			MyTab.Columns.Add(rm.GetString("name"), typeof (string));
			MyTab.Columns.Add(rm.GetString("target"), typeof (string));
			MyTab.Columns.Add(rm.GetString("location"), typeof (string));
		}

		/// <summary>
		/// get a list of all logical drives on computer
		/// </summary>
		/// <returns></returns>
        public CheckedListBox GetDrives()
        {
            /*unkown =0;
             * noroot =1;
             * removable = 2;
             * local disk =3;
             * network = 4;
             * cdrom = 5;
             * ram drive = 6;
             */
            var list = new CheckedListBox();

            var sDriveName = new StringBuilder(2);
            int i = 0;

            DriveInfo[] drives = null;

            try
            {
                drives = DriveInfo.GetDrives();
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }

            if (drives != null)
            {
                foreach (DriveInfo drive in drives)
                {
                    try
                    {
                        if (drive.DriveType == DriveType.Fixed)
                        {
                            sDriveName.Append(drive.Name, 0, 2);
                            checkedListViewDrives.Items.Add(drive.VolumeLabel + " (" + sDriveName + ")");
                            checkedListViewDrives.Items[i].ImageIndex = 0;
                            checkedListViewDrives.Items[i].Name = drive.Name;
                            i++;
                            sDriveName.Remove(0, 2);                      
                        }
                    }
                    catch
                    {
                        sDriveName.Remove(0, 2);
                    }
                }
            }

            return list;
        }

		/// <summary>
		/// handle Click event to scan for shortcuts
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonScan_Click(object sender, EventArgs e)
		{
			if (radioButtonDrives.Checked && checkedListViewDrives.CheckedItems.Count < 1)
			{
				MessageBox.Show(rm.GetString("select_drive_scan"));
				return;
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		/// <summary>
		/// handle Click event to cancel scan for shortcuts
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
			CloseScanWindow = true;
			return;
		}

		/// <summary>
		/// handle CheckedChanged event to change scan location
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void radioButtonDesktop_CheckedChanged(object sender, EventArgs e)
		{
			RadioButtonCheckUpdate();
		}

		/// <summary>
		/// change current language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
            ResourceManager rm = new ResourceManager("ShortcutsFixer.Resources", typeof(ScanForm).Assembly);
			Thread.CurrentThread.CurrentUICulture = culture;

			radioButtonDesktop.Text = rm.GetString("scan_start_desktop");
			radioButtonDrives.Text = rm.GetString("check_drives");
			buttonScan.Text = rm.GetString("scan_now");
			Text = rm.GetString("scan_now");
			buttonCancel.Text = rm.GetString("cancel");
			grbMain.Text = rm.GetString("scan_options");
			grbMain.Text = rm.GetString("scan_options");
		}

		/// <summary>
		/// update drive check box according to scan location
		/// </summary>
		void RadioButtonCheckUpdate()
		{
			try
			{
				if (radioButtonDesktop.Checked)
				{
					checkedListViewDrives.Enabled = false;
					checkedListViewDrives.CheckBoxes = false;

					for (int i = 0; i < checkedListViewDrives.Items.Count; i++)
					{
						checkedListViewDrives.Items[i].Checked = false;
					}
				}
				else if (radioButtonDrives.Checked)
				{
					checkedListViewDrives.Enabled = true;
					checkedListViewDrives.CheckBoxes = true;
				}

				buttonScan.Enabled = true;
			}
			catch (Exception ex)
			{
                // ToDo: send exception details via SmartAssembly bug reporting!
			}
		}
	}
}