using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace EmptyFolderFinder
{
    /// <summary>
    /// Custom file dialog
    /// </summary>
    public partial class frmCustomFileDialog : Form
    {
        #region enumPathType enum

        /// <summary>
        /// Path types
        /// </summary>
        public enum PathType
        {
            File,
            Disk
        };

        #endregion

        /// <summary>
        /// Path
        /// </summary>
        public ArrayList Path;
        /// <summary>
        /// Path type
        /// </summary>
        public PathType EPathType;
        /// <summary>
        /// Path to scan
        /// </summary>
        public string PathToScan = String.Empty;

        /// <summary>
        /// constructor for FrmCustomFileDialog
        /// </summary>
        public frmCustomFileDialog()
        {
            InitializeComponent();
            if (!File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\FreemiumUtilities.exe"))
            {
                this.Icon = Properties.Resources.PCCleanerIcon;
            }
            else
            {
                this.Icon = Properties.Resources.FSUIcon;
            }
            try
            {
                lvMain.SmallImageList = imagesSmall;
                Path = new ArrayList();
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
                            if (drive.DriveType == DriveType.Fixed || drive.DriveType == DriveType.Removable)
                            {
                                ListViewItem lstViewItemDive = new ListViewItem(drive.VolumeLabel + " (" + drive.Name + ")");
                                lstViewItemDive.ImageIndex = 1;
                                lvMain.Items.Add(lstViewItemDive);
                            }
                        }
                        catch (Exception)
                        {
                            //Console.WriteLine(drive);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        /// <summary>
        /// handle click event to open new location browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSelect_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK) return;
            rdbScanFolder.Checked = true;
            EPathType = PathType.File;
            txtData.Text = dialog.SelectedPath;
            PathToScan = txtData.Text;
        }

        /// <summary>
        /// handle Click event to set the new search location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnScan_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            if (rdbScanDrives.Checked)
            {
                EPathType = PathType.Disk;
                ListView.CheckedIndexCollection checkedindexColl = lvMain.CheckedIndices;
                if (checkedindexColl.Count > 0)
                {
                    for (int i = 0; i < checkedindexColl.Count; i++)
                    {
                        string tmp = lvMain.Items[checkedindexColl[i]].Text;
                        tmp = tmp.Substring(tmp.IndexOf("(") + 1, 2);
                        Path.Add(tmp);
                    }
                    DialogResult = DialogResult.OK;
                }
            }
            else if (rdbScanFolder.Checked)
            {
                PathToScan = txtData.Text;
                EPathType = PathType.File;
                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// handle Click event to cancel location browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        /// <summary>
        /// initialize FrmCustomFileDialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmCustomFileDialog_Load(object sender, EventArgs e)
        {
            var culture = new CultureInfo(CfgFile.Get("Lang"));
            SetCulture(culture);
            txtData.Enabled = false;
            btnSelect.Enabled = false;

            string[] args = Environment.GetCommandLineArgs();            
            if (args.Length == 2)
            {
                rdbScanDrives.Checked = false;
                rdbScanFolder.Checked = true;
                EPathType = PathType.File;
                txtData.Text = args[1];
                PathToScan = txtData.Text;
                btnScan_Click(this, null);
            }
        }

        /// <summary>
        /// handle CheckedChanged event to choose a folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rdbScanFolder_CheckedChanged(object sender, EventArgs e)
        {
            txtData.Enabled = true;
            btnSelect.Enabled = true;

            lvMain.Enabled = false;
            lvMain.CheckBoxes = false;
        }

        /// <summary>
        /// handle CheckedChanged event to choose drives
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rdbScanDrives_CheckedChanged(object sender, EventArgs e)
        {
            txtData.Enabled = false;
            btnSelect.Enabled = false;

            lvMain.CheckBoxes = true;
            lvMain.Enabled = true;
        }

        /// <summary>
        /// change current language
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            var rm = new ResourceManager("EmptyFolderFinder.Resources", typeof(frmCustomFileDialog).Assembly);
            Thread.CurrentThread.CurrentUICulture = culture;
            rdbScanFolder.Text = rm.GetString("scan_folder");
            clhDrives.Text = rm.GetString("drives");
            rdbScanDrives.Text = rm.GetString("scan_drives");
            btnScan.Text = rm.GetString("scan_now");
            btnCancel.Text = rm.GetString("cancel");
            Text = rm.GetString("select_drive_folder");
        }
    }
}