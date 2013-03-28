using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace FileUndelete
{
    /// <summary>
    /// Main form for File Undelete
    /// </summary>
    public partial class FormMain : Form
    {
        #region Properties

        readonly SortedList extensionList = new SortedList();
        readonly List<ulong> foundIDs = new List<ulong>();
        readonly IconListManager iconListManager;
        readonly List<string> processedPaths = new List<string>();
        readonly ListViewColumnSorter sorter = new ListViewColumnSorter();

        readonly frmFilter frmFilter;
        readonly FormProgress frmProgress;
        readonly ScanManager scanMgr = new ScanManager();
        string currentDrive;
        int currentProgress;
        List<DriveInfo> drives;
        IList<FileToRestore> filesToRestore = new List<FileToRestore>();
        string filterName = "*";
        string filterPath = string.Empty;

        uint filterSize;
        bool filterSizeSmall;
        bool isFilterName;
        bool isFilterSize;
        bool NeedAbort;
        FormBusy frmBusy = new FormBusy();

        #endregion

        #region Constructors

        public FormMain()
        {
            InitializeComponent();
            lvFiles.ListViewItemSorter = sorter;
            frmFilter = new frmFilter();
            frmProgress = new FormProgress();
            new FormResults();

            imlFolders.ColorDepth = ColorDepth.Depth32Bit;
            imlFiles.ColorDepth = ColorDepth.Depth32Bit;

            imlFolders.ImageSize = new Size(16, 16);
            imlFiles.ImageSize = new Size(32, 32);

            iconListManager = new IconListManager(imlFolders, imlFiles);
            trvFolders.ImageList = imlFolders;
        }

        #endregion

        #region methods
        /// <summary>
        /// Main form load event handler
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        void frmMain_Load(object sender, EventArgs e)
        {
            SetCulture(new CultureInfo(CfgFile.Get("Lang")));

            try
            {
                drives = new List<DriveInfo>();
                foreach (DriveInfo nfo in DriveInfo.GetDrives())
                {
                    try
                    {
                        if (nfo.DriveType == DriveType.Fixed)
                        {
                            cboDrives.Items.Add(string.IsNullOrEmpty(nfo.VolumeLabel)
                                                    ? nfo.Name.Replace(Path.DirectorySeparatorChar.ToString(), "")
                                                    : nfo.VolumeLabel + " (" + nfo.Name.Replace(Path.DirectorySeparatorChar.ToString(), "") + ")");
                            drives.Add(nfo);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch
            {
            }
            if (cboDrives.Items.Count != 0)
                cboDrives.SelectedIndex = 0;
            else btnScan.Enabled = false;

            tslFilter.Text = rm.GetString("filter_off");
            tslResults.Text = string.Format(rm.GetString("found"), 0);

            Width += 1;
            Width -= 1;

            trvExt.Nodes.Add(rm.GetString("all_extensions"), rm.GetString("all_extensions"), 0);
        }

        /// <summary>
        /// Sets a specified <paramref Name="Culture"/> to a current thread
        /// </summary>
        /// <param Name="Culture"></param>
        void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;
            dbcRestoreButton.HeaderText = rm.GetString("action");
            dbcRestoreButton.Text = rm.GetString("restore");
            dccIsRecoverable.HeaderText = rm.GetString("recoverable");
            dtcSize.HeaderText = rm.GetString("size");
            txcFilePath.HeaderText = rm.GetString("file_path");
            txcFileId.HeaderText = rm.GetString("file_id");
            txtFilter.Text = rm.GetString("no_filter");
            chkAll.Text = rm.GetString("check_all");
            chkPreview.Text = rm.GetString("quick_panel");
            btnRestore.Text = rm.GetString("restore");
            clhName.Text = rm.GetString("name");
            clhState.Text = rm.GetString("state");
            clhOriginalPath.Text = rm.GetString("original_path");
            clhSize.Text = rm.GetString("size");
            clhTag.Text = rm.GetString("tag");
            tbpFolders.Text = rm.GetString("folders");
            tbpTypes.Text = rm.GetString("file_types");
            btnFilter.Text = rm.GetString("filter");
            btnScan.Text = rm.GetString("search");
            Text = rm.GetString("file_undelete");

            ucTop.Text = rm.GetString("file_undelete");
        }

        /// <summary>
        /// Updates a <c>CurrentProgress</c> value with a provided <paramref Name="progress"/>
        /// </summary>
        /// <param Name="progress">Progress value</param>
        /// <returns>True if <c>NeedAbort</c> set to false and <c>CurrentProgress</c> value was updated succesfully</returns>
        bool UpdateProgress(int progress)
        {
            if (NeedAbort)
                return false;

            currentProgress = progress;
            return !NeedAbort;
        }

        /// <summary>
        /// Updates <c>tslResults</c> text with a currrent <c>lvFiles</c> items Count
        /// </summary>
        void UpdateListView()
        {
            tslResults.Text = string.Format(Constants.Found, lvFiles.Items.Count);
        }

        /// <summary>
        /// Checks for an Item
        /// </summary>
        /// <param Name="filePath">File Path</param>
        /// <param Name="fileId">File ID</param>
        /// <param Name="flag">Flag</param>
        /// <param Name="size">File size</param>
        /// <returns>True if the Item was found</returns>
        bool ItemFound(string filePath, ulong fileId, bool flag, uint size)
        {
            if (NeedAbort)
            {
                return false;
            }

            bool add = string.IsNullOrEmpty(filterPath) || Helper.PathMatch(filterPath.ToLower(), filePath.ToLower());

            // process filters
            if (!add) return !NeedAbort;

            if (isFilterName && !string.IsNullOrEmpty(filterName))
            {
                add = Regex.IsMatch(filePath.ToLower(), Helper.WildcardToRegex(filterName.ToLower()));
            }
            if (add && isFilterSize)
            {
                add = (size < filterSize && filterSizeSmall) || (size >= filterSize && !filterSizeSmall);
            }
            if (add)
            {
                add = flag || filterLost;
            }
            if (add)
            {
                lock (filesToRestore)
                {
                    filesToRestore.Add(new FileToRestore(filePath, fileId, flag, size));
                    foundIDs.Add(fileId);
                }
            }
            return !NeedAbort;
        }

        /// <summary>
        /// Controls UI elements state based on <paramref Name="isRunning"/> value
        /// </summary>
        /// <param Name="isRunning">True if a scan is running</param>
        void SwitchMode(bool isRunning)
        {
            if (isRunning)
            {
                btnScan.Enabled = false;
                frmProgress.Text = rm.GetString("searching");
                frmProgress.labelProgress.Text = string.Empty;
                frmProgress.Show();
            }
            else
            {
                btnScan.Enabled = true;
                frmProgress.Hide();
            }
        }

        /// <summary>
        /// Abort scan operation
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        void btnAbort_Click(object sender, EventArgs e)
        {
            NeedAbort = true;
            if (frmProgress.Text != rm.GetString("searching"))
            {
                return;
            }
            scanMgr.Abort();
            trvFolders.Nodes.Clear();
            trvExt.Nodes.Clear();
            HideProcessing();
        }

        /// <summary>
        /// Start scan operation
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        void btnScan_Click(object sender, EventArgs e)
        {
            NeedAbort = false;

            frmProgress.progressBar.Value = 0;
            currentProgress = 0;
            filesToRestore = new List<FileToRestore>();
            filterPath = string.Empty;
            currentDrive = cboDrives.SelectedItem.ToString();
            if (currentDrive.IndexOf(":)") != -1)
            {
                currentDrive = currentDrive.Remove(0, currentDrive.IndexOf(":)") - 2);
                currentDrive = currentDrive.Remove(currentDrive.IndexOf(')'));
                currentDrive = currentDrive.Replace("(", "");
            }
            if (currentDrive.Length == 2) currentDrive += Path.DirectorySeparatorChar;
            else if (currentDrive.Length > 3)
            {
                filterPath = currentDrive + Path.DirectorySeparatorChar;
                currentDrive = currentDrive.Remove(3);
            }
            SwitchMode(true);

            trvFolders.Nodes.Clear();
            trvExt.Nodes.Clear();
            lvFiles.Items.Clear();
            processedPaths.Clear();
            foundIDs.Clear();

            scanMgr.StartScan(
                currentDrive,
                UpdateProgress,
                ItemFound,
                null,
                false, //advanced
                false); //recycle bin
            tmrScan.Start();
        }

        /// <summary>
        /// Add the searched file extension in sorted list only if the same extension does not exist in the same sorted list.
        /// </summary>
        /// <param Name="Path">Path to exclude extension from it</param>
        void UpdateExtList(string path)
        {
            string ext;
            try
            {
                ext = Path.GetExtension(path);
            }
            catch
            {
                return;
            }
            if (string.IsNullOrEmpty(ext)) return;
            if (extensionList.IndexOfKey(ext) < 0)
            {
                extensionList.Add(ext, ext);
            }
        }

        /// <summary>
        /// Add each Item of extesions list to treeview.
        /// </summary>
        void UpdateExtensionTree()
        {
            int count = 0;
            trvExt.ImageList.ImageSize = new Size(16, 16);
            foreach (DictionaryEntry item in extensionList)
            {
                TreeNode node = new TreeNode(item.Key.ToString()) { Name = item.Key.ToString() };
                node.ImageIndex =
                    trvExt.ImageList.Images.Add(IconReader.GetFileIcon(node.Name, IconReader.IconSize.Small, false).ToBitmap(),
                                                Color.Transparent);
                node.SelectedImageIndex = node.ImageIndex;
                trvExt.Nodes[0].Nodes.Add(node);

                if (count == 200)
                {
                    Application.DoEvents();
                    Thread.Sleep(1);
                    count = 0;
                }
                count++;
            }
        }

        /// <summary>
        /// Update Node with a data
        /// </summary>
        /// <param Name="data">New data</param>
        /// <param Name="parent">Node parent</param>
        /// <param Name="root">True if the Node is root</param>
        void updateNode(string data, TreeNode parent, bool root)
        {
            if (root)
            {
                if (processedPaths.IndexOf(data) != -1) return;
                processedPaths.Add(data);
            }
            string[] Datas = data.Split(Path.DirectorySeparatorChar);
            if (Datas.Length == 0 || Datas[0] == string.Empty) return;
            TreeNode Node = parent.Nodes[Datas[0]];
            if (Node == null)
            {
                Node = new TreeNode(Datas[0]);
                Node.Name = Node.Text;
                Node.ImageIndex = Node.SelectedImageIndex = iconListManager.AddFileIcon(string.Empty);
                parent.Nodes.Add(Node);
            }
            if (Datas.Length == 1) return;
            updateNode(data.Replace(Datas[0] + Path.DirectorySeparatorChar, string.Empty), Node, false);
        }

        /// <summary>
        /// Updates a <c>frmProgress</c> UI elements with a current progress state
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        void scanTimer_Tick(object sender, EventArgs e)
        {
            if (!NeedAbort)
            {
                frmProgress.labelProgress.Text = string.Format(
                    rm.GetString("current_progress") + ": {0}%, {1} " + rm.GetString("files_found") + ".",
                    currentProgress, filesToRestore.Count);
                if (currentProgress == -1)
                {
                    currentProgress = 100;
                }
                frmProgress.progressBar.Value = currentProgress;
            }
            if (scanMgr.IsFinished)
            {
                OnFinish();
            }
        }

        /// <summary>
        /// Scan process finalization
        /// </summary>
        void OnFinish()
        {
            tmrScan.Stop();

            if (!NeedAbort)
            {
                ShowProcessing();
            }

            SwitchMode(false);

            if (scanMgr.InnerException != null)
            {
                // ToDo: display a localized message to the user that the program could not be started!
                //MessageBox.Show(rm.GetString("scan_finished_no_data_collected") + ".", rm.GetString("scan_finished"),
                //                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            lvFiles.Items.Clear();
            lvFiles.BeginUpdate();
            trvFolders.Nodes.Clear();
            trvFolders.Nodes.Add(string.Empty, cboDrives.SelectedIndex < drives.Count
                                        ? drives[cboDrives.SelectedIndex].Name.Substring(0, 2)
                                        : cboDrives.SelectedItem.ToString().Substring(0, 2), iconListManager.AddFileIconhdd("D:"));
            trvExt.Nodes.Clear();
            trvExt.Nodes.Add(rm.GetString("all_extensions"), rm.GetString("all_extensions"), 0);

            if (NeedAbort)
            {
                lvFiles.EndUpdate();
                NeedAbort = false;
                return;
            }

            string name, location;
            int count = 0;
            foreach (FileToRestore file in filesToRestore)
            {
                try
                {
                    name = Path.GetFileName(file.FilePath);
                }
                catch
                {
                    name = file.FilePath;
                }
                try
                {
                    location = Path.GetDirectoryName(file.FilePath);
                }
                catch
                {
                    location = string.Empty;
                }
                ListViewItem Item = new ListViewItem(name);
                Item.SubItems.Add(file.IsRecoverable ? rm.GetString("recoverable") : rm.GetString("lost"));
                Item.SubItems.Add(location);
                Item.SubItems.Add(((double)(file.Size / 1024)).ToString("N2"));
                Item.SubItems.Add(file.FileId.ToString());
                Item.Checked = chkAll.Checked && file.IsRecoverable;
                lvFiles.Items.Add(Item);
                if (tcMain.Visible)
                {
                    if (!string.IsNullOrEmpty(location) && location.IndexOf(trvFolders.Nodes[0].Text) == 0)
                    {
                        updateNode(location.Remove(0, 3), trvFolders.Nodes[0], true);
                    }
                    UpdateExtList(name);
                }
                count++;
                if (count == 50)
                {
                    Application.DoEvents();
                    Thread.Sleep(1);
                    count = 0;
                }
            }

            try
            {
                UpdateExtensionTree();
                lvFiles.EndUpdate();
                UpdateListView();
                if (filesToRestore.Count == 0)
                {
                    trvExt.Nodes.Clear();
                    trvFolders.Nodes.Clear();
                    MessageBox.Show(rm.GetString("scan_finished_no_data_collected"), rm.GetString("scan_finished"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                Application.DoEvents();

                HideProcessing();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Process operation
        /// </summary>
        /// <param Name="func">Operation delegate</param>
        /// <param Name="args">Operation arguments</param>
        /// <returns></returns>
        public List<ListViewItem> Process(Delegate func, params object[] args)
        {
            List<ListViewItem> Item = new List<ListViewItem>();
            ShowProcessing();

            ThreadStart ThreadStart = delegate
                                        {
                                            Item = (List<ListViewItem>)func.DynamicInvoke(args);
                                        };

            Thread Thread = new Thread(ThreadStart);
            Thread.SetApartmentState(ApartmentState.STA);
            Thread.Start();

            return Item;
        }

        /// <summary>
        /// Display filtered file list from <c>FilesToRestore</c> in a <c>lvFiles</c>
        /// </summary>
        /// <param Name="Path">Filter Path</param>
        /// <param Name="extension">Filter extension</param>
        void DisplayFiles(string path, string extension)
        {
            bool Add = true;
            ShowProcessing();
            string Name, Ext, Location;
            lvFiles.BeginUpdate();
            lvFiles.Items.Clear();
            int counter = 0;
            foreach (FileToRestore file in filesToRestore)
            {
                counter++;
                try
                {
                    Name = Path.GetFileName(file.FilePath);
                }
                catch
                {
                    Name = file.FilePath;
                }
                try
                {
                    Location = Path.GetDirectoryName(file.FilePath);
                }
                catch
                {
                    Location = string.Empty;
                }
                try
                {
                    Ext = Path.GetExtension(Name);
                }
                catch
                {
                    Ext = string.Empty;
                }

                // filters here
                if (!string.IsNullOrEmpty(path))
                {
                    Add = Location.Contains(path);
                    if (!Add) continue;
                }

                if (!string.IsNullOrEmpty(extension))
                {
                    Add = Ext == extension;
                    if (!Add) continue;
                }

                if (isFilterName && !string.IsNullOrEmpty(filterName))
                {
                    Add = Regex.IsMatch(file.FilePath, Helper.WildcardToRegex(filterName));
                }
                if (Add && isFilterSize)
                {
                    uint _filesize = file.Size;

                    if (filterSizeSmall)
                    {
                        Add = (_filesize < filterSize);
                    }
                    else
                    {
                        Add = (_filesize >= filterSize);
                    }
                }
                if (Add)
                {
                    Add = file.IsRecoverable || filterLost;
                }
                if (!Add) continue;

                ListViewItem Item = new ListViewItem(Name);
                Item.SubItems.Add(file.IsRecoverable ? rm.GetString("recoverable") : rm.GetString("lost"));
                Item.SubItems.Add(Location);
                Item.SubItems.Add(((double)(file.Size / 1024)).ToString("N2"));
                Item.SubItems.Add(file.FileId.ToString());
                Item.Checked = chkAll.Checked && file.IsRecoverable;
                lvFiles.Items.Add(Item);
                if (counter == 50)
                {
                    Application.DoEvents();
                    counter = 0;
                }
            }
            lvFiles.EndUpdate();
            HideProcessing();
            Application.DoEvents();
        }

        /// <summary>
        /// Main form closing event
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            NeedAbort = true;
            scanMgr.Abort();
            tmrScan.Stop();
        }

        /// <summary>
        /// Preview checkbox checked changed event
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        void chkPreview_CheckedChanged(object sender, EventArgs e)
        {
            tcMain.Visible = chkPreview.Checked;
            if (tcMain.Visible)
            {
                lvFiles.Left += 256;
                lvFiles.Width -= 256;
            }
            else
            {
                lvFiles.Left -= 256;
                lvFiles.Width += 256;
            }
        }

        /// <summary>
        /// Do filtering
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        void btnFilter_Click(object sender, EventArgs e)
        {
            string Filter = string.Empty;
            frmFilter.chkFilename.Checked = isFilterName;
            frmFilter.txtFilename.Text = filterName;
            frmFilter.chkSize.Checked = isFilterSize;
            frmFilter.cboSize.SelectedIndex = filterSizeSmall ? 1 : 0;
            frmFilter.nudSize.Value = filterSize / 1024;
            frmFilter.chkIncludeNonRecoverable.Checked = filterLost;
            if (frmFilter.ShowDialog() != DialogResult.OK) return;

            isFilterName = frmFilter.chkFilename.Checked;
            filterName = frmFilter.txtFilename.Text;
            isFilterSize = frmFilter.chkSize.Checked;
            filterSizeSmall = frmFilter.cboSize.SelectedIndex == 1;
            filterSize = (uint)frmFilter.nudSize.Value * 1024;
            filterLost = frmFilter.chkIncludeNonRecoverable.Checked;

            Filter = isFilterName ? filterName : string.Empty;
            Filter += isFilterSize ? (!string.IsNullOrEmpty(Filter) ? ", Size Filter" : "Size Filter") : string.Empty;
            txtFilter.Text = string.IsNullOrEmpty(Filter) ? rm.GetString("no_filter") : Filter;
            tslFilter.Text =
                isFilterName || isFilterSize || filterLost ? rm.GetString("filter_on") : rm.GetString("filter_off");

            if (filesToRestore.Count != 0)
            {
                DisplayFiles(cboDrives.SelectedItem.ToString(), string.Empty);
            }
        }

        /// <summary>
        /// Main form Layout event
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        void frmMain_Layout(object sender, LayoutEventArgs e)
        {
            if (e.AffectedControl == frmProgress)
            {
                if (e.AffectedProperty == "close")
                {
                    btnAbort_Click(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Main form Activated event
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        void frmMain_Activated(object sender, EventArgs e)
        {
            if (frmProgress.Visible)
                frmProgress.Activate();

            if (frmBusy.Visible)
                frmBusy.Activate();
        }

        /// <summary>
        /// Files ListView column click event
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        void lvFiles_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            sorter.SortNumeric = e.Column == 3;
            sorter.SortDatetime = null;
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == sorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (sorter.Order == SortOrder.Ascending)
                {
                    sorter.Order = SortOrder.Descending;
                }
                else
                {
                    sorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.SortColumn = e.Column;
                sorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            lvFiles.Sort();
        }

        /// <summary>
        /// Folders TreeView after select event
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        void trvFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string Path = e.Node.FullPath;
            DisplayFiles(Path, string.Empty);
        }

        /// <summary>
        /// Strats restore operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnRestore_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(rm.GetString("LongOperationQuestion"), string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.No)
                return;

            ListView.CheckedIndexCollection CheckedIndexCollection = lvFiles.CheckedIndices;
            if (CheckedIndexCollection.Count == 0) return;
            if (fbdMain.ShowDialog() != DialogResult.OK) return;

            if (fbdMain.SelectedPath.IndexOf(currentDrive) == 0)
            {
                if (MessageBox.Show(rm.GetString("restoring_same_drive"),
                                    Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes) return;
            }

            if (!Directory.Exists(fbdMain.SelectedPath)) return;
            lvFiles.BeginUpdate();

            string Name = string.Empty;
            string Where = string.Empty;
            ulong Id = 0;
            List<int> Indices = new List<int>();
            ulong Recovered = 0, Failed = 0;

            frmProgress.Text = rm.GetString("restoring");
            frmProgress.labelProgress.Text = string.Empty;
            frmProgress.progressBar.Maximum = CheckedIndexCollection.Count;
            frmProgress.progressBar.Value = 0;
            frmProgress.Show();

            NeedAbort = false;

            foreach (int index in CheckedIndexCollection)
            {
                Id = ulong.Parse(lvFiles.Items[index].SubItems[4].Text);
                Name = Where = lvFiles.Items[index].Text;
                Where = fbdMain.SelectedPath + Path.DirectorySeparatorChar + Where;
                try
                {
                    bool b = CSWrapper.RecoverFile(Id, Where);
                    if (b)
                    {
                        Recovered++;
                        Indices.Add(index);
                        filesToRestore.RemoveAt(foundIDs.IndexOf(Id));
                        foundIDs.Remove(Id);
                    }
                    else Failed++;
                }
                catch
                {
                    Failed++;
                }

                frmProgress.labelProgress.Text = lvFiles.Items[index].Text;
                Application.DoEvents();
                frmProgress.progressBar.PerformStep();
                Application.DoEvents();

                if (NeedAbort)
                    break;
            }

            while (Indices.Count != 0)
            {
                lvFiles.Items.RemoveAt(Indices[Indices.Count - 1]);
                Indices.RemoveAt(Indices.Count - 1);
                Application.DoEvents();
            }

            lvFiles.EndUpdate();

            if (frmProgress.Visible)
                frmProgress.Hide();
            frmProgress.progressBar.Value = 0;
            frmProgress.progressBar.Maximum = 100;


            MessageBox.Show(
                string.Format("{0} {1}, {2} {3}.", Recovered, rm.GetString("files_recovered"), Failed, rm.GetString("failed")),
                Text,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Extensions TreeView after select event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void trvExt_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string Path = e.Node.Text;
            if (Path.IndexOf(".") == -1)
            {
                Path = string.Empty;
            }
            DisplayFiles(string.Empty, Path);
        }

        /// <summary>
        /// Browse action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnBrowse_Click(object sender, EventArgs e)
        {
            if (fbdMain.ShowDialog() != DialogResult.OK) return;
            if (cboDrives.Items.IndexOf(fbdMain.SelectedPath) == -1)
                cboDrives.Items.Add(fbdMain.SelectedPath);
            cboDrives.SelectedIndex = cboDrives.Items.IndexOf(fbdMain.SelectedPath);
        }

        /// <summary>
        /// Used to check/uncheck all items in a <c>lvFiles</c>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvFiles.Items)
            {
                item.Checked = chkAll.Checked;
            }
        }

        /// <summary>
        /// Shows Processing form
        /// </summary>
        public void ShowProcessing()
        {
            frmBusy = new FormBusy();
            frmBusy.Show();
            frmBusy.BringToFront();
            Application.DoEvents();
        }

        /// <summary>
        /// Hides Processing form
        /// </summary>
        public void HideProcessing()
        {
            frmBusy.Close();
        }

        #endregion
    }
}