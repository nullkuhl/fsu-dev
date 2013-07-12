using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using System.Linq;
using System.Collections.Generic;

namespace EmptyFolderFinder
{
    /// <summary>
    /// Empty folder finder main form
    /// </summary>
    public partial class FormEmptyFolderFinder : Form
    {
        //Folders to exclude
        readonly List<string> excludeFolder = new List<string>();

        bool ABORT;
        int IS_CLEAR_WHOLE_LIST;
        int a;
        /// <summary>
        /// Empty folders count
        /// </summary>
        public int Count;
        ArrayList emptyListArray;
        bool showed;

        /// <summary>
        /// constructor for FrmEmptyFolderFinder
        /// </summary>
        public FormEmptyFolderFinder()
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
        }

        /// <summary>
        /// open dialog to choose a search location
        /// </summary>
        public void OpenCustomFileDialog()
        {
            lvMain.SmallImageList = imagesSmall;
            tslScan.Text = string.Empty;
            ABORT = false;
            LoadExcludeFolder();

            frmCustomFileDialog customDialogbox = new frmCustomFileDialog();
            DialogResult result = customDialogbox.ShowDialog();

            if (result != DialogResult.No)
            {
                tslAbort.Visible = true;
                lvMain.Items.Clear();
            }

            if (result == DialogResult.OK && customDialogbox.EPathType == frmCustomFileDialog.PathType.File) //Search In File
            {
                if (customDialogbox.PathToScan == string.Empty || !Directory.Exists(customDialogbox.PathToScan))
                {
                    MessageBox.Show(rm.GetString("no_drives_selected"));
                    OpenCustomFileDialog();
                }

                FindEmptyFoldersMethod(customDialogbox.PathToScan);
            }
            else if (customDialogbox.EPathType == frmCustomFileDialog.PathType.Disk) //Search in Disk
            {
                if (customDialogbox.Path.Count > 0 && result == DialogResult.OK)
                {
                    tlsMain.Enabled = false;
                    if (!ABORT)
                    {
                        tslScan.Text = rm.GetString("scanning") + " ...";
                        toolStripStatusLabelProgressBarSpacer.Visible = false;
                        tspMain.Visible = true;
                        tspMain.Style = ProgressBarStyle.Marquee;
                        tspMain.Width = 130;
                    }
                    foreach (object t in customDialogbox.Path)
                    {
                        string selection = t.ToString();
                        selection = selection.Substring(selection.IndexOf("(") + 1, 2) + "\\";
                        CollectCount(selection);

                        if (ABORT)
                        {
                            Abort();
                            break;
                        }
                    }

                    tslStatus.Text = string.Empty;
                    if (!ABORT)
                    {
                        tslScan.Text = rm.GetString("scanning") + " ...";
                        toolStripStatusLabelProgressBarSpacer.Visible = false;
                        tspMain.Visible = true;
                        tspMain.Style = ProgressBarStyle.Marquee;
                        tspMain.Width = 130;
                    }

                    if (ABORT)
                    {
                        Abort();
                    }
                }


                if (customDialogbox.Path.Count > 0)
                {
                    foreach (object t in customDialogbox.Path)
                    {
                        FindEmptyFoldersMethod(t + "\\");
                    }

                    if (!ABORT)
                    {
                        tslScan.Text = rm.GetString("completed");
                        tspMain.Value = 0;
                        tspMain.Visible = false;
                        toolStripStatusLabelProgressBarSpacer.Visible = true;
                    }
                    tspMain.Visible = false;
                    toolStripStatusLabelProgressBarSpacer.Visible = true;
                    tslStatus.Text = string.Empty;
                    tslAbort.Visible = false;
                }
                else
                {
                    MessageBox.Show(rm.GetString("no_drives_selected"));
                    OpenCustomFileDialog();
                }
            }
        }

        /// <summary>
        /// search for empty folders
        /// </summary>
        /// <param name="sPathtoScan"></param>
        public void FindEmptyFoldersMethod(string sPathtoScan)
        {
            Cursor = Cursors.WaitCursor;
            tlsMain.Enabled = false;
            try
            {
                if (!string.IsNullOrEmpty(sPathtoScan))
                {
                    if (lvMain.Columns.Count == 0)
                    {
                        lvMain.Columns.Add(rm.GetString("folder_name"), 100, HorizontalAlignment.Left);
                        lvMain.Columns.Add(rm.GetString("folder_path"), 300, HorizontalAlignment.Left);
                    }

                    //My Working
                    emptyListArray = new ArrayList();
                    IS_CLEAR_WHOLE_LIST = 0;
                    a = 0;
                    if (!ABORT)
                    {
                        tspMain.Maximum = CalculateTotalNumberOfFiles(sPathtoScan);
                    }
                    Scan(sPathtoScan);

                    if (!ABORT)
                    {
                        tslScan.Text = rm.GetString("completed");
                        tspMain.Value = 0;
                        tspMain.Visible = false;
                        toolStripStatusLabelProgressBarSpacer.Visible = true;
                    }
                    tspMain.Visible = false;
                    toolStripStatusLabelProgressBarSpacer.Visible = true;
                    tslStatus.Text = string.Empty;
                    tslAbort.Visible = false;
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
            finally
            {
                tlsMain.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Calculates total number of files in a specified <paramref name="path"/>
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>Total number of files in a specified <paramref name="path"/></returns>
        public int CalculateTotalNumberOfFiles(string path)
        {
            if (!ABORT)
                tslScan.Text = rm.GetString("scanning") + " ...";
            toolStripStatusLabelProgressBarSpacer.Visible = false;
            tspMain.Visible = true;
            tspMain.Width = 130;
            tspMain.Style = ProgressBarStyle.Marquee;
            try
            {
                Application.DoEvents();
                DirectoryInfo dInfo = new DirectoryInfo(path);
                a += dInfo.GetDirectories().Length;
                foreach (DirectoryInfo subFolder in dInfo.GetDirectories())
                {
                    Application.DoEvents();
                    CalculateTotalNumberOfFiles(subFolder.FullName);
                }
            }
            catch
            {
            }
            return a;
        }

        /// <summary>
        /// Scan <paramref name="path"/> for the empty folders
        /// </summary>
        /// <param name="path">Path</param>
        public void Scan(string path)
        {
            if (!ABORT)
            {
                toolStripStatusLabelProgressBarSpacer.Visible = false;
                tspMain.Visible = true;
                tspMain.Width = 130;
                tspMain.Style = ProgressBarStyle.Blocks;
                tspMain.ProgressBar.Step = 1;
                tspMain.PerformStep();
            }

            DirectoryInfo dInfo = new DirectoryInfo(path);
            try
            {
                if (!ABORT)
                {
                    tslScan.Text = rm.GetString("processing") + " ...";
                }
                tslStatus.Text = GetStatusBarFilePath(dInfo.FullName);

                if (dInfo.GetFiles().Length == 0 && dInfo.GetDirectories().Length == 0)
                {
                    if (IS_CLEAR_WHOLE_LIST == 0)
                    {
                        lvMain.Items.Clear();
                        IS_CLEAR_WHOLE_LIST = 1;
                    }

                    emptyListArray.Add(dInfo);
                    ListViewItem lstViewItem = new ListViewItem(dInfo.Name) { ImageIndex = 1 };
                    bool ishap = false;

                    if (excludeFolder.Contains(dInfo.FullName))
                        ishap = true;

                    if (!ishap)
                    {
                        lstViewItem.SubItems.Add(dInfo.FullName);
                        lstViewItem.SubItems.Add(dInfo.FullName);
                    }

                    lvMain.CheckBoxes = true;
                    lvMain.Items.Add(lstViewItem);
                }

                foreach (DirectoryInfo subFolder in dInfo.GetDirectories())
                {
                    if (ABORT)
                    {
                        Abort();
                        break;
                    }
                    Application.DoEvents();
                    bool isExclude = false;

                    if (excludeFolder.Contains(subFolder.FullName))
                        isExclude = true;


                    if (isExclude == false)
                        Scan(subFolder.FullName);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Collect empty folders count in a specified <paramref name="path"/>
        /// </summary>
        /// <param name="path">Path</param>
        public void CollectCount(string path)
        {
            Count++;

            var dInfo = new DirectoryInfo(path);

            try //handling unaccessable folders
            {
                tslStatus.Text = GetStatusBarFilePath(dInfo.FullName);

                DirectoryInfo[] dirs = dInfo.GetDirectories();
                foreach (DirectoryInfo subFolder in dirs)
                {
                    if (ABORT)
                    {
                        Abort();
                        break;
                    }

                    Application.DoEvents();
                    bool isExclude = false;

                    if (excludeFolder.Contains(subFolder.FullName))
                        isExclude = true;

                    if (isExclude == false && subFolder.FullName != path)
                        CollectCount(subFolder.FullName);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Get File name to be shown in status bar
        /// </summary>
        string GetStatusBarFilePath(string filePath)
        {
            var strfilePath = new StringBuilder();
            if (filePath.Length <= 60)
            {
                strfilePath.Append(filePath);
            }
            else
            {
                string lastString = string.Empty;
                string[] strArr = filePath.Split('\\');

                if (strArr.Length > 2)
                {
                    lastString = strArr[strArr.Length - 2] + "\\" + strArr[strArr.Length - 1];
                }

                foreach (string t in strArr)
                {
                    if (strfilePath.Length + lastString.Length + t.Length > 60)
                    {
                        strfilePath.Append("...\\" + lastString);
                        break;
                    }
                    strfilePath.Append(t + "\\");
                }
            }
            return strfilePath.ToString();
        }

        /// <summary>
        /// cancel scanning for empty folders
        /// </summary>
        void Abort()
        {
            if (showed) return;
            showed = true;
            tspMain.Visible = false;
            tlsMain.Enabled = true;
            toolStripStatusLabelProgressBarSpacer.Visible = true;
            tslStatus.Text = string.Empty;
            tslScan.Text = rm.GetString("aborted");
            Application.DoEvents();
            MessageBox.Show(rm.GetString("scanning_aborted"), Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// load excluded folders
        /// </summary>
        void LoadExcludeFolder()
        {
            try
            {
                using (StreamReader sr = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                                            "\\FreeSystemUtilities\\Files\\ExcludeFiles.txt"))
                {
                    do
                    {
                        try
                        {
                            excludeFolder.Add(sr.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            // ToDo: send exception details via SmartAssembly bug reporting!
                        }
                    } while (!sr.EndOfStream);

                    sr.Close();
                }
            }
            catch
            {
            }
        }

        void lvMain_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //// Check the current sort.
            var sorter = lvMain.ListViewItemSorter as ListViewItemComparer;
            lvMain.ListViewItemSorter = sorter;
            if (sorter == null)
            {
                sorter = new ListViewItemComparer(e.Column);
                lvMain.ListViewItemSorter = sorter;
            }
            else
            {
                if (sorter.Column == e.Column && !sorter.Descending)
                {
                    // The list is already sorted on this column.
                    // Time to flip the sort.
                    sorter.Descending = true;
                    // Keep the ListView.Sorting property
                    // synchronized, just for tidiness.
                    lvMain.Sorting = SortOrder.Descending;
                }
                else
                {
                    lvMain.Sorting = SortOrder.Ascending;
                    sorter.Descending = false;
                    sorter.Column = e.Column;
                }
            }
            // Perform the sort.
            lvMain.Sort();
        }

        /// <summary>
        /// handle Click event to choose a new search location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsbScan_Click(object sender, EventArgs e)
        {
            //listView1.Items.Clear();
            OpenCustomFileDialog();
        }

        /// <summary>
        /// initialize FrmEmptyFolderFinder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmEmptyFolderFinder_Load(object sender, EventArgs e)
        {
            var culture = new CultureInfo(CfgFile.Get("Lang"));
            SetCulture(culture);
            try
            {
                LoadExcludeFolder();
            }
            catch (Exception ex)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        /// <summary>
        /// handle Click event to delete checked folders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsbDelete_Click(object sender, EventArgs e)
        {
            if (lvMain.CheckedIndices.Count == 0)
            {
                MessageBox.Show(rm.GetString("select_folder"), rm.GetString("del_folder"));
                return;
            }

            bool isShowMsg = true;

            ListViewItem item = new ListViewItem();
            while (lvMain.CheckedIndices.Count > 0)
            {
                try
                {
                    int checkedIndex = lvMain.CheckedIndices[0];

                    item = lvMain.Items[checkedIndex];
                    DirectoryInfo dirinfo = new DirectoryInfo(lvMain.Items[checkedIndex].SubItems[1].Text);
                    dirinfo.Delete();

                    lvMain.Items.Remove(item);
                }
                catch (Exception ex)
                {
                    int result;
                    if (isShowMsg)
                    {
                        var msgbox = new CustomDialog(rm.GetString("error_deleting_file"), rm.GetString("error_deleting_file"), ex.Message,
                                                      SystemIcons.Exclamation.ToBitmap());
                        msgbox.ShowDialog();
                        result = msgbox.Result;
                    }
                    else
                    {
                        result = 5;
                    }

                    if (result == 3)
                    {
                        break;
                    }
                    if (result == 4)
                    {
                        //Do Nothing
                    }
                    else if (result == 8)
                    {
                        isShowMsg = false;
                    }
                    else
                    {
                        lvMain.Items.Remove(item); //Remove that item from list, without deleting it
                    }
                }
            }

            //FindEmptyFoldersMethod(sLastFolderPath);
        }

        /// <summary>
        /// handle Click event to check all folders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tmiCheckAll_Click(object sender, EventArgs e)
        {
            if (lvMain.Items.Count > 0)
            {
                for (int i = 0; i < lvMain.Items.Count; i++)
                {
                    lvMain.Items[i].Checked = true;
                }
            }
        }

        /// <summary>
        /// handle Shown event to clear list and choose new search location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmEmptyFolderFinder_Shown(object sender, EventArgs e)
        {
            lvMain.Items.Clear();
            OpenCustomFileDialog();
        }

        /// <summary>
        /// handle Click event to open checked folders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsbOpen_Click(object sender, EventArgs e)
        {
            ListView.CheckedIndexCollection checkedindexColl = lvMain.CheckedIndices;
            if (checkedindexColl.Count > 0)
            {
                if (checkedindexColl.Count > 9)
                {
                    DialogResult result = MessageBox.Show(String.Format(rm.GetString("ManyFoldersToOpen"), checkedindexColl.Count),
                                                          rm.GetString("OpeningFolders"), MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

                for (int i = 0; i < checkedindexColl.Count; i++)
                {
                    try
                    {
                        Process.Start(lvMain.Items[checkedindexColl[i]].SubItems[1].Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(rm.GetString("FolderCannotBeOpenedMessage"),
                                        rm.GetString("FolderCannotBeOpenedTitle"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        /// <summary>
        /// handle Click event to invert checked folders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tmiCheckInvert_Click(object sender, EventArgs e)
        {
            if (lvMain.Items.Count > 0)
            {
                for (int i = 0; i < lvMain.Items.Count; i++)
                {
                    lvMain.Items[i].Checked = !lvMain.Items[i].Checked;
                }
            }
        }

        /// <summary>
        /// handle Click event to check none folders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tmiCheckNone_Click(object sender, EventArgs e)
        {
            if (lvMain.Items.Count > 0)
            {
                for (int i = 0; i < lvMain.Items.Count; i++)
                {
                    lvMain.Items[i].Checked = false;
                }
            }
        }

        /// <summary>
        /// handle Click event to add checked folders to excluded folders list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsbExclude_Click(object sender, EventArgs e)
        {

            if (lvMain.Items.Count > 0)
            {
                if (lvMain.CheckedItems.Count > 0)
                {
                    try
                    {
                        string exclFilesDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FreeSystemUtilities\\Files";
                        if (!Directory.Exists(exclFilesDir))
                            Directory.CreateDirectory(exclFilesDir);

                        using (StreamWriter sw = new StreamWriter(exclFilesDir + @"\ExcludeFiles.txt", true))
                        {
                            for (int i = lvMain.Items.Count - 1; i >= 0; i--)
                            {
                                if (lvMain.Items[i].Checked)
                                {
                                    sw.WriteLine(lvMain.Items[i].SubItems[1].Text);
                                    lvMain.Items.RemoveAt(i);
                                }
                            }
                            sw.Close();
                        }

                        MessageBox.Show(rm.GetString("excluded"), rm.GetString("operation_done"), MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        // ToDo: send exception details via SmartAssembly bug reporting!
                    }
                }
                else
                {
                    MessageBox.Show(rm.GetString("select_folder_to_remove"), rm.GetString("exclude"), MessageBoxButtons.OK,
                                       MessageBoxIcon.Information);
                }
            }
        }
        /// <summary>
        /// change current language
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            //CultureInfo enUS = new CultureInfo("en-US");
            ResourceManager rm = new ResourceManager("EmptyFolderFinder.Resources", typeof(FormEmptyFolderFinder).Assembly);
            Thread.CurrentThread.CurrentUICulture = culture;
            clhFolder.Text = rm.GetString("folder_name");
            clhPath.Text = rm.GetString("folder_path");
            tslAbort.Text = rm.GetString("abort");

            tsbScan.Text = rm.GetString("scan_for_empty_folders");
            tsbDelete.Text = rm.GetString("delete_checked_folders");
            tsbOpen.Text = rm.GetString("open_folder");
            tsbExclude.Text = rm.GetString("exclude");
            tdbCheck.Text = rm.GetString("check");
            tmiCheckAll.Text = rm.GetString("check_all");
            tmiCheckNone.Text = rm.GetString("check_none");
            tmiCheckInvert.Text = rm.GetString("check_invert");
            Text = rm.GetString("empty_folder_finder");
            ucTop.Text = rm.GetString("empty_folder_finder");
        }

        /// <summary>
        /// handle Click event to abort searching for empty folders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolStripStatusLabelAbort_Click(object sender, EventArgs e)
        {
            ABORT = true;
            showed = false;
            tslAbort.Visible = false;
            tspMain.Value = 0;
            tspMain.Visible = false;
            toolStripStatusLabelProgressBarSpacer.Visible = true;
        }

        /// <summary>
        /// handle FormClosing event to abort search before closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmEmptyFolderFinder_FormClosing(object sender, FormClosingEventArgs e)
        {
            ABORT = true;
        }

        private void lvMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}