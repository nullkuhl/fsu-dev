using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using IWshRuntimeLibrary;
using Shell32;
using ShortcutsFixer.Models;
using File = System.IO.File;
using Folder = Shell32.Folder;

namespace ShortcutsFixer
{
    /// <summary>
    /// Shortcuts fixer knot main form
    /// </summary>
    public partial class FormMain : Form
    {
        BindingList<ShortcutItem> shortcutItems;
        ScanForm obj;

        /// <summary>
        /// Resource manager
        /// </summary>
        public ResourceManager rm = new ResourceManager("ShortcutsFixer.Resources", Assembly.GetExecutingAssembly());

        /// <summary>
        /// constructor for FormMain
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            fileLabel.Text = string.Empty;
            lblStatus.Text = string.Empty;
            Shown += FormMain_Shown;
            if (!File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\FreemiumUtilities.exe"))
            {
                this.Icon = Properties.Resources.PCCleanerIcon;
            }
            else
            {
                this.Icon = Properties.Resources.FSUIcon;
            }
        }

        void InitializeShortcutItems()
        {
            shortcutItems = new BindingList<ShortcutItem>();
            shortcutItems.ListChanged += ShortcutItems_ListChanged;

            shortcutItems.AllowNew = true;
            shortcutItems.AllowEdit = true;
            shortcutItems.AllowRemove = true;

            shortcutItems.RaiseListChangedEvents = true;
        }

        void ShortcutItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            ControlToolStripButtonsState();
        }

        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out] StringBuilder lpszPath, int nFolder, bool fCreate);

        /// <summary>
        /// desktop/start meny folders constants
        /// </summary>
        const int CSIDL_DESKTOP = 0x0000;
        const int CSIDL_DESKTOPDIRECTORY = 0x0010;
        const int CSIDL_COMMON_DESKTOPDIRECTORY = 0x0019;
        const int CSIDL_STARTMENU = 0x000b;
        const int CSIDL_COMMON_STARTMENU = 0x0016;

        /// <summary>
        /// get special desktop/start menu folders path
        /// </summary>
        /// <param name="CSIDL"></param>
        /// <returns></returns>
        string GetSpecialFolderPath(int CSIDL)
        {
            try
            {
                StringBuilder path = new StringBuilder(260);
                SHGetSpecialFolderPath(IntPtr.Zero, path, CSIDL, false);
                return path.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// search desktop for shortcuts
        /// </summary>
        public void SearchDesktop()
        {
            EnableControls(false);
            string root = Path.GetPathRoot(Environment.SystemDirectory);
            string user = Environment.UserName;

            HashSet<string> places = new HashSet<string>();

            if (OSIsXP())
            {
                places.Add(root + @"Documents and Settings\All Users\Desktop");
                places.Add(root + @"Documents and Settings\All Users\Start Menu");
                places.Add(root + @"Documents and Settings\Default User\Desktop");
                places.Add(root + @"Documents and Settings\Default User\Start Menu");
                places.Add(root + @"Documents and Settings\" + user + @"\Desktop");
                places.Add(root + @"Documents and Settings\" + user + @"\Start Menu");
            }

            places.Add(root + @"ProgramData\Desktop");
            places.Add(root + @"ProgramData\Microsoft\Windows\Start Menu");
            places.Add(root + @"ProgramData\Start Menu");
            places.Add(root + @"Users\Default\AppData\Roaming\Microsoft\Windows\Start Menu");
            places.Add(root + @"Users\Default\Desktop");
            places.Add(root + @"Users\Default\Start Menu");
            places.Add(root + @"Users\Public\Desktop");
            places.Add(root + @"Users\" + user + @"\AppData\Roaming\Microsoft\Windows\Start Menu");
            places.Add(root + @"Users\" + user + @"\Desktop");
            places.Add(root + @"Users\" + user + @"\Start Menu");
            places.Add(GetSpecialFolderPath(CSIDL_COMMON_DESKTOPDIRECTORY));
            places.Add(GetSpecialFolderPath(CSIDL_COMMON_STARTMENU));
            places.Add(GetSpecialFolderPath(CSIDL_DESKTOP));
            places.Add(GetSpecialFolderPath(CSIDL_DESKTOPDIRECTORY));
            places.Add(GetSpecialFolderPath(CSIDL_STARTMENU));

            double ratio = 0.0;
            double step = 1.0 / places.Count;

            foreach (string place in places)
            {
                ratio = Math.Min(ratio + step, 1.0);
                try
                {
                    if (Directory.Exists(place))
                    {
                        DirectoryInfo dir = new DirectoryInfo(place);
                        FileAttributes att = dir.Attributes;
                        if ((att & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
                        {
                            ScanningFiles(place, true, ratio);
                        }
                    }
                }
                catch (Exception)
                {
                    // ToDo: send exception details via SmartAssembly bug reporting!
                }
            }

            prbMain.Visible = false;
            EnableControls(true);            
        }

        /// <summary>
        /// Enables or disables controls
        /// </summary>
        /// <param name="isEnabled">true - if the controls should be enabled, false - otherwise</param>
        private void EnableControls(bool isEnabled)
        {
            Abort.Visible = !isEnabled;
            btnFixShortCut.Enabled = isEnabled;
            btnDelete.Enabled = isEnabled;
            btnOpenFolder.Enabled = isEnabled;
            btnProperties.Enabled = isEnabled;
            btnScan.Enabled = isEnabled;
            btnRestore.Enabled = isEnabled;
            tsbCheck.Enabled = isEnabled;
            if (isEnabled)
            {
                lblStatus.Text = String.Empty;
                fileLabel.Text = String.Empty;
            }
        }

        /// <summary>
        /// check if the current windows version is xp
        /// </summary>
        /// <returns></returns>
        private bool OSIsXP()
        {
            try
            {
                OperatingSystem osInfo = Environment.OSVersion;
                switch (osInfo.Platform)
                {
                    case PlatformID.Win32NT:
                        switch (osInfo.Version.Major)
                        {
                            case 4:
                                return false;
                            case 5:
                                return true;
                        }
                        break;
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// search logical drives for shortcuts
        /// </summary>
        public void SearchDrives()
        {
            EnableControls(false);
            var objList = new ListBox();
            foreach (ListViewItem str in obj.checkedListViewDrives.CheckedItems)
            {
                objList.Items.Add(str.Name);
            }
            Application.DoEvents();

            double iCurrentStage = 1;
            double iTotalStages = objList.Items.Count;
            foreach (string list in objList.Items)
            {
                try
                {
                    ScanningFiles(list, true, iCurrentStage / iTotalStages);
                }
                catch (Exception)
                {
                    // ToDo: send exception details via SmartAssembly bug reporting!
                }
                iCurrentStage++;
            }

            try
            {
                prbMain.Visible = false;
            }
            catch
            {
            }

            EnableControls(true);
        }

        /// <summary>
        /// Scanning files
        /// </summary>
        /// <param name="str">Path</param>
        /// <param name="bIsDrive">Is drive</param>
        /// <param name="dPercentageRatio">Percentage ratio</param>
        public void ScanningFiles(string str, bool bIsDrive, double dPercentageRatio)
        {
            var path = new DirectoryInfo(str);
            FileAttributes att = path.Attributes;
            if ((att & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
            {
                return;
            }
            Application.DoEvents();
            foreach (FileInfo shortcutfile in path.GetFiles())
            {
                if (shortcutfile.FullName.Length > 75)
                {
                    string sFilename = shortcutfile.FullName;
                    int index1 = sFilename.IndexOf("\\");
                    index1 = sFilename.IndexOf("\\", index1 + 1);
                    int index2 = sFilename.LastIndexOf("\\");
                    fileLabel.Text = sFilename.Substring(0, index1 + 1) + "..." +
                                     sFilename.Substring(index2, sFilename.Length - index2);
                }
                else
                {
                    fileLabel.Text = shortcutfile.FullName;
                }

                obj.TheLink = null;

                if (shortcutfile.Name.EndsWith(".lnk"))
                {
                    obj.TheLink = (IWshShortcut)obj.Obj.CreateShortcut(shortcutfile.FullName);

                    string linkTarget = obj.TheLink.TargetPath;
                    string linkfile = linkTarget;
                    if (linkTarget.Contains("\\"))
                    {
                        linkfile = linkTarget.Substring(linkTarget.LastIndexOf("\\"));
                    }
                    if (string.IsNullOrEmpty(linkTarget))
                    {
                        continue;
                    }
                    DriveInfo drive = new DriveInfo(linkTarget.Substring(0, 1));
                    if (drive.DriveType == DriveType.CDRom)
                    {
                        continue;
                    }
                    if (!File.Exists(linkTarget) && !Directory.Exists(linkTarget)
                        && !File.Exists(linkTarget.Replace("Program Files (x86)", "Program Files")) && !Directory.Exists(linkTarget.Replace("Program Files (x86)", "Program Files"))
                        && !File.Exists(Environment.ExpandEnvironmentVariables(@"%systemroot%\Sysnative") + "\\" + linkfile)
                        && !File.Exists(Environment.SystemDirectory + "\\" + linkfile)
                        && !File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\" + linkfile)
                        && !obj.TheLink.IconLocation.Contains("\\"))
                    {
                        if (!obj.TheLink.TargetPath.StartsWith(@"\\"))
                            obj.MyTab.Rows.Add(true, shortcutfile.Name, obj.TheLink.TargetPath, shortcutfile.FullName);
                    }
                }
            }

            Application.DoEvents();


            foreach (DirectoryInfo dirInfo in path.GetDirectories())
            {
                try
                {
                    if (bIsDrive)
                    {
                        int iTotDirectories = path.GetDirectories().Length;
                        prbMain.Value = (int)(obj.CurrDirectory * 100 * dPercentageRatio / (iTotDirectories + obj.CurrDirectory));
                        obj.CurrDirectory++;
                    }
                    if (dirInfo.Name != @"Data")
                        ScanningFiles(dirInfo.FullName, false, 0.0);

                    if (obj.CloseScanWindow)
                    {
                        prbMain.Value = 0;
                        fileLabel.Text = String.Empty;
                        prbMain.Visible = false;
                        return;
                    }
                }
                catch (Exception)
                {
                }
            }
            if (bIsDrive)
            {
                try
                {
                    prbMain.Value = (int)(100 * dPercentageRatio);
                }
                catch
                {
                }
            }
        }

        void FormMain_Shown(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"Data");

            dgwMain.DataSource = null;

            obj = new ScanForm();

            DialogResult dr = obj.ShowDialog();

            if (dr == DialogResult.OK)
            {
                prbMain.Visible = true;
                lblStatus.Text = rm.GetString("scanning") + "...";

                if (obj.radioButtonDesktop.Checked)
                    SearchDesktop();
                else if (obj.radioButtonDrives.Checked)
                    SearchDrives();
            }

            try
            {
                if (obj.MyTab.Rows.Count < 1)
                {
                    tlsMain.Items[2].Enabled =
                        tlsMain.Items[3].Enabled =
                        tlsMain.Items[5].Enabled = tlsMain.Items[6].Enabled = tlsMain.Items[7].Enabled = false;
                    MessageBox.Show(rm.GetString("no_shortcuts"), rm.GetString("notice"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    tlsMain.Items[2].Enabled =
                        tlsMain.Items[3].Enabled =
                        tlsMain.Items[5].Enabled = tlsMain.Items[6].Enabled = tlsMain.Items[7].Enabled = true;
                    dgwMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    InitializeShortcutItems();
                    for (int i = 0; i < obj.MyTab.Rows.Count; i++)
                    {
                        shortcutItems.Add(new ShortcutItem
                                            {
                                                IsChecked = obj.MyTab.Rows[i].Field<bool>(0),
                                                Name = obj.MyTab.Rows[i].Field<string>(1),
                                                LinksTo = obj.MyTab.Rows[i].Field<string>(2),
                                                Path = obj.MyTab.Rows[i].Field<string>(3)
                                            });
                    }
                    dgwMain.DataSource = shortcutItems;

                    dgwMain.Columns[0].Width = 30;
                    dgwMain.Columns[0].HeaderText = String.Empty;

                    dgwMain.Columns[1].Width = 230;
                    dgwMain.Columns[1].HeaderText = rm.GetString("name");

                    dgwMain.Columns[2].Width = 230;
                    dgwMain.Columns[2].HeaderText = rm.GetString("target");

                    dgwMain.Columns[3].Width = 230;
                    dgwMain.Columns[3].HeaderText = rm.GetString("path");

                    dgwMain.Columns[1].ReadOnly = true;
                    dgwMain.Columns[2].ReadOnly = true;
                    dgwMain.Columns[3].ReadOnly = true;
                    ControlToolStripButtonsState();
                }
            }
            catch
            {
            }
        }

        void FormMain_Load(object sender, EventArgs e)
        {
            SetCulture(new CultureInfo(CfgFile.Get("Lang")));
            Abort.Visible = false;
        }

        /// <summary>
        /// Is link of the <paramref name="shortcutFilename"/> valid
        /// </summary>
        /// <param name="shortcutFilename">Shortcut file name</param>
        /// <returns>True if link of the <paramref name="shortcutFilename"/> valid</returns>
        public static bool IsLink(string shortcutFilename)
        {
            string pathOnly = Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = Path.GetFileName(shortcutFilename);

            Shell shell = new ShellClass();
            Folder folder = shell.NameSpace(pathOnly);
            FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null)
            {
                return folderItem.IsLink;
            }
            return false; // not found 
        }

        /// <summary>
        /// Gets <paramref name="shortcutFilename"/> target
        /// </summary>
        /// <param name="shortcutFilename">Shortcut file name</param>
        /// <returns><paramref name="shortcutFilename"/> target</returns>
        public static string GetShortcutTarget(string shortcutFilename)
        {
            string pathOnly = Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = Path.GetFileName(shortcutFilename);

            Shell shell = new ShellClass();
            Folder folder = shell.NameSpace(pathOnly);
            FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null)
            {
                if (folderItem.IsLink)
                {
                    var link = (ShellLinkObject)folderItem.GetLink;
                    return link.Path;
                }
                return shortcutFilename;
            }
            return String.Empty; // not found 
        }

        /// <summary>
        /// handle Click event to fix a shortcut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnFix_Click(object sender, EventArgs e)
        {
            // Fix Shortcut
            if (dgwMain.SelectedRows.Count > 0)
            {
                string path = dgwMain.SelectedRows[0].Cells[2].Value.ToString();

                var flInfo = new FileInfo(path);

                if (((flInfo)).Extension != String.Empty)
                {
                    var fd = new OpenFileDialog { Title = rm.GetString("choose_correct_shortcut") };

                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        ShortcutSet(fd.FileName);
                        dgwMain.SelectedRows[0].Cells[2].Value = fd.FileName;
                    }
                }
                else if (((flInfo)).Extension == String.Empty)
                {
                    var fd = new FolderBrowserDialog
                                {
                                    Description = rm.GetString("choose_correct_shortcut"),
                                    ShowNewFolderButton = false,
                                    RootFolder = Environment.SpecialFolder.MyComputer
                                };

                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        ShortcutSet(fd.SelectedPath);
                        dgwMain.SelectedRows[0].Cells[2].Value = fd.SelectedPath;
                    }
                }

                ControlToolStripButtonsState();
                ControlToolStripCheckButtonsState();
            }
        }

        /// <summary>
        /// handle Click event to delete a shortcut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnDelete_Click(object sender, EventArgs e)
        {
            int rowCount = 0;
            for (int iCounter = 0; iCounter < dgwMain.Rows.Count; iCounter++)
            {
                try
                {
                    if (Convert.ToBoolean(dgwMain.Rows[iCounter].Cells[0].Value))
                    {
                        rowCount++;
                    }
                }
                catch
                {
                }

            }

            if (rowCount > 0)
            {
                DialogResult dialogResult =
                    MessageBox.Show(string.Format("{0} {1} {2}", rm.GetString("are_you_sure"), rowCount.ToString(), rm.GetString("are_you_sure_cont")),
                                    rm.GetString("Confirmation"), MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    DataTable myTable = new DataTable("dataTable");

                    try
                    {
                        myTable.ReadXmlSchema(@"schemefile");
                        myTable.ReadXml(@"datafile");

                        string date = Convert.ToString(DateTime.Now);
                        for (int i = 0; i < dgwMain.Rows.Count; i++)
                        {
                            try
                            {
                                string path = dgwMain.Rows[i].Cells[3].Value.ToString();
                                string name = path;
                                name = name.Substring(name.LastIndexOf("\\") + 1, name.Length - name.LastIndexOf("\\") - 1);

                                if (Convert.ToBoolean(dgwMain.Rows[i].Cells[0].Value))
                                {
                                    //enable copy it on Data Folder
                                    File.Copy(path, @"Data\\" + name, true);
                                    myTable.Rows.Add(false, name, path, date);
                                    File.Delete(path);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }

                        for (int i = dgwMain.Rows.Count - 1; i >= 0; i--)
                        {
                            try
                            {
                                string path = dgwMain.Rows[i].Cells[3].Value.ToString();
                                if (!File.Exists(path))
                                {
                                    dgwMain.Rows.RemoveAt(i);
                                }
                            }
                            catch { }
                        }

                        myTable.WriteXml(@"datafile");
                    }

                    catch (Exception)
                    {
                    }
                }

                ControlToolStripButtonsState();
                ControlToolStripCheckButtonsState();
            }
            else
            {
                MessageBox.Show(rm.GetString("atleast_one"), rm.GetString("shortcut_fixer"), MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// handle Click event to show shortcut properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnProperties_Click(object sender, EventArgs e)
        {
            try
            {
                string path = dgwMain.SelectedRows[0].Cells[3].Value.ToString();
                var proc = new Process { StartInfo = { FileName = path, Verb = "properties", UseShellExecute = true } };
                proc.Start();
            }
            catch
            {
                MessageBox.Show(rm.GetString("select"), rm.GetString("shortcut_fixer"));
            }
        }

        /// <summary>
        /// handle Click event to open shortcut location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string path = dgwMain.SelectedRows[0].Cells[3].Value.ToString();
                FileInfo objfile = new FileInfo(path);
                if (objfile.DirectoryName != null) Process.Start(objfile.DirectoryName);
            }
            catch
            {
                MessageBox.Show(rm.GetString("select"), rm.GetString("shortcut_fixer"));
            }
        }

        /// <summary>
        /// handle Click event to restore shortcut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnRestore_Click(object sender, EventArgs e)
        {
            //Restore objRestore = new Restore();
            //objRestore.ShowDialog();

            var objBachup = new BackupForm();
            objBachup.ShowDialog();
        }

        /// <summary>
        /// handle Click event to scan for shortcuts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnScan_Click(object sender, EventArgs e)
        {
            FormMain_Shown(sender, e);
        }

        /// <summary>
        /// Sets shortcut link path
        /// </summary>
        /// <param name="targetPath">Target path</param>
        public void ShortcutSet(string targetPath)
        {
            var objShortcut = new WshShellClass();
            try
            {
                string path = dgwMain.SelectedRows[0].Cells[3].Value.ToString();
                var theLink = (IWshShortcut)objShortcut.CreateShortcut(path);
                theLink.TargetPath = targetPath;
                theLink.Save();
            }
            catch
            {
                MessageBox.Show(rm.GetString("select"), rm.GetString("shortcut_fixer"));
            }
        }

        void ControlToolStripButtonsState()
        {
            bool isAnyItemChecked = false;
            try
            {
                for (int i = 0; i < dgwMain.Rows.Count; i++)
                {
                    if (dgwMain.Rows[i].Cells.Count > 0 && (bool)dgwMain.Rows[i].Cells[0].Value)
                    {
                        isAnyItemChecked = true;
                        break;
                    }
                }
            }
            catch
            {
            }
            btnFixShortCut.Enabled = isAnyItemChecked;
            btnDelete.Enabled = isAnyItemChecked;
            btnOpenFolder.Enabled = isAnyItemChecked;
            btnProperties.Enabled = isAnyItemChecked;
        }

        void ControlToolStripCheckButtonsState()
        {
            tsbCheck.Enabled = dgwMain.Rows.Count != 0;
        }

        /// <summary>
        /// handle Click event to check all shortcuts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BindingContext[dgwMain.DataSource].EndCurrentEdit();
            for (int i = 0; i < dgwMain.Rows.Count; i++)
            {
                dgwMain.Rows[i].Cells[0].Value = true;
            }
            if (dgwMain.CurrentRow != null) dgwMain.CurrentRow.Cells[0].Value = true;
            btnFixShortCut.Enabled = true;
            btnDelete.Enabled = true;
            btnOpenFolder.Enabled = true;
            btnProperties.Enabled = true;
        }

        /// <summary>
        /// handle Click event to check none shortcuts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BindingContext[dgwMain.DataSource].EndCurrentEdit();
            for (int i = 0; i < dgwMain.Rows.Count; i++)
            {
                dgwMain.Rows[i].Cells[0].Value = false;
            }
            if (dgwMain.CurrentRow != null) dgwMain.CurrentRow.Cells[0].Value = false;
            btnFixShortCut.Enabled = false;
            btnDelete.Enabled = false;
            btnOpenFolder.Enabled = false;
            btnProperties.Enabled = false;
        }

        void dgwMain_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgwMain.IsCurrentCellDirty)
            {
                dgwMain.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// handle Click event to invert checked shortcuts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkInvertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BindingContext[dgwMain.DataSource].EndCurrentEdit();

            bool isAnyItemChecked = false;
            for (int i = 0; i < dgwMain.Rows.Count; i++)
            {
                string tmp = dgwMain.Rows[i].Cells[0].Value.ToString();
                if (tmp.Equals("True"))
                {
                    dgwMain.Rows[i].Cells[0].Value = false;
                }
                else
                {
                    isAnyItemChecked = true;
                    dgwMain.Rows[i].Cells[0].Value = true;
                }
            }
            btnFixShortCut.Enabled = isAnyItemChecked;
            btnDelete.Enabled = isAnyItemChecked;
            btnOpenFolder.Enabled = isAnyItemChecked;
            btnProperties.Enabled = isAnyItemChecked;
        }

        /// <summary>
        /// change current language
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;

            btnScan.Text = rm.GetString("scan");
            btnFixShortCut.Text = rm.GetString("fix_shortcut");
            btnDelete.Text = rm.GetString("delete");
            btnProperties.Text = rm.GetString("properties");
            btnOpenFolder.Text = rm.GetString("open_folder");
            tsbCheck.Text = rm.GetString("check");
            checkAllToolStripMenuItem.Text = rm.GetString("check_all");
            checkNoneToolStripMenuItem.Text = rm.GetString("check_none");
            checkInvertToolStripMenuItem.Text = rm.GetString("check_invert");
            btnRestore.Text = rm.GetString("restore");
            Abort.Text = rm.GetString("abort");
            Text = rm.GetString("shortcut_fixer");
            ucTop.Text = rm.GetString("shortcut_fixer");
        }

        void Abort_Click(object sender, EventArgs e)
        {
            obj.CloseScanWindow = true;
            lblStatus.Text = rm.GetString("aborted") + "...";
        }
    }
}