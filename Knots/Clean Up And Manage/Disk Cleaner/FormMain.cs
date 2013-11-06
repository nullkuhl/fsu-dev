using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace Disk_Cleaner
{
    /// <summary>
    /// Main form of the Disk cleaner knot
    /// </summary>
    public partial class FormMain : Form
    {
        readonly frmView formView = new frmView();
        readonly ArrayList junk = new ArrayList();
        readonly ArrayList scan = new ArrayList();
        readonly ArrayList zero = new ArrayList();
        bool ABORT;
        FormPreferences formPref;
        decimal binsize;
        decimal junksize;
        DirectoryInfo temp;
        string windowsDir = (Path.GetPathRoot(Environment.SystemDirectory) + "Windows").ToLower();
        BackgroundWorker bgWorker;
        ulong gainedSizeBytes;
        FormProcessing frmProcessing;

        /// <summary>
        /// constructor for FormMain
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
           
            Clean.OnProgress += Clean_OnProgress;
        }

        void Clean_OnProgress(object sender, EventArgs e)
        {
            if (ProcessedFiles.InvokeRequired)
            {
                ProcessedFiles.Invoke(new MethodInvoker(delegate { ProcessedFiles.Items.Add(Path.GetFileName(Clean.Current.Name)); }));
            }
            else
            {
                ProcessedFiles.Items.Add(Path.GetFileName(Clean.Current.Name));
            }
            Application.DoEvents();
            Clean.ABORT = ABORT;
        }

        void DoSearchVisible(int id)
        {
            foreach (Control ctrl in tabPageCleanup.Controls.Cast<Control>().Where(ctrl => ctrl.Tag != null))
                ctrl.Visible = ctrl.Tag.ToString() == id.ToString();
            buttonBack.Enabled = id != 1;
            buttonNext.Enabled = id != 2;
        }

        /// <summary>
        /// handle Click event to close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// handle Shown event to load drives
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormMain_Shown(object sender, EventArgs e)
        {
            LoadDrives();
        }

        /// <summary>
        /// check if any item is checked in the list
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        bool IsAnyItemChecked(ListView.ListViewItemCollection items)
        {
            return items.Cast<ListViewItem>().Any(item => item.Checked);
        }

        /// <summary>
        /// handle Click event to search for items and delete them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonNext_Click(object sender, EventArgs e)
        {
            if (IsAnyItemChecked(listViewDrives.Items))
            {
                try
                {
                    refreshBtn.Visible = false;

                    if (buttonNext.Text == rm.GetString("finish"))
                    {
                        ProcessedFiles.Hide();
                        refreshBtn.Visible = true;
                        buttonBack.Visible = true;
                        buttonBack.PerformClick();
                        buttonNext.Text = rm.GetString("next");
                        return;
                    }
                    if (listViewJunk.Visible)
                    {
                        if (IsAnyItemChecked(listViewJunk.Items))
                        {
                            DoSearchVisible(4);
                            buttonNext.Enabled = false;
                            ProcessedFiles.Clear();
                            ProcessedFiles.View = View.Details;
                            ProcessedFiles.Columns.Add(rm.GetString("cleaning_status"), 350);
                            ProcessedFiles.Show();
                            labelClean.Text = rm.GetString("cleaning_started") + ".";
                            ProcessedFiles.Items.Add(rm.GetString("cleaning_started"));
                            ShowProcessing();
                            bgWorker = new BackgroundWorker
                            {
                                WorkerSupportsCancellation = true
                            };
                            bgWorker.DoWork += bgWorker_DoWork;
                            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
                            bgWorker.RunWorkerAsync();
                        }
                        else
                        {
                            MessageBox.Show(rm.GetString("select_any_item"),
                                            rm.GetString("critical_warning"),
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                        }
                        return;
                    }

                    scan.Clear();
                    listViewScanning.Items.Clear();
                    ListView.CheckedIndexCollection check = listViewDrives.CheckedIndices;
                    if (check.Count == 0 && !Preferences.ScanTemporaryFolder) return;
                    if (Preferences.ScanTemporaryFolder)
                    {
                        /*
                        scan.Add(temp);
                        var item = new ListViewItem(rm.GetString("temporary_folders")) { Tag = temp };
                        item.SubItems.Add(rm.GetString("pending"));
                        listViewScanning.Items.Add(item);
                        */
                    }
                    foreach (int i in check)
                    {
                        try
                        {
                            DriveInfo drive = listViewDrives.Items[i].Tag as DriveInfo;
                            if (drive != null)
                            {
                                ListViewItem item = new ListViewItem(
                                    string.IsNullOrEmpty(drive.VolumeLabel) ? drive.Name : drive.VolumeLabel + " (" + drive.Name + ")") { Tag = drive };
                                item.SubItems.Add(rm.GetString("pending"));
                                listViewScanning.Items.Add(item);
                                scan.Add(drive);
                            }
                        }
                        catch { }
                    }
                    DoSearchVisible(2);
                    Application.DoEvents();
                    Start();
                }
                catch (Exception)
                {
                    // ToDo: send exception details via SmartAssembly bug reporting!
                }
            }
            else
            {
                MessageBox.Show(rm.GetString("select_any_drive"),
                                rm.GetString("critical_warning"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// handle do work event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            gainedSizeBytes = 0;
            int lvCount = 0;
            if (listViewJunk.InvokeRequired)
            {
                listViewJunk.Invoke(new MethodInvoker(delegate { lvCount = listViewJunk.Items.Count; }));
            }
            else
            {
                lvCount = listViewJunk.Items.Count;
            }

            bool res = false;
            object tag;
            for (int i = 0; i < lvCount; i++)
            {
                res = false;
                tag = null;
                if (listViewJunk.InvokeRequired)
                {
                    listViewJunk.Invoke(new MethodInvoker(delegate { res = listViewJunk.Items[i].Checked; }));
                    listViewJunk.Invoke(new MethodInvoker(delegate { tag = listViewJunk.Items[i].Tag; }));
                }
                else
                {
                    res = listViewJunk.Items[i].Checked;
                    tag = listViewJunk.Items[i].Tag;
                }

                if (res)
                {
                    if (Equals(tag, 1))
                    {
                        if (ProcessedFiles.InvokeRequired)
                        {
                            ProcessedFiles.Invoke(new MethodInvoker(delegate { ProcessedFiles.Items.Add(string.Empty); }));
                            ProcessedFiles.Invoke(new MethodInvoker(delegate { ProcessedFiles.Items.Add("emptying_recycle_bin"); }));
                        }
                        else
                        {
                            ProcessedFiles.Items.Add(string.Empty);
                            ProcessedFiles.Items.Add("emptying_recycle_bin");
                        }
                        gainedSizeBytes += Clean.ProcessRecycleBin();
                    }
                    else if (Equals(tag, 2))
                    {
                        if (ProcessedFiles.InvokeRequired)
                        {
                            ProcessedFiles.Invoke(new MethodInvoker(delegate { ProcessedFiles.Items.Add(string.Empty); }));
                            ProcessedFiles.Invoke(new MethodInvoker(delegate { ProcessedFiles.Items.Add("processing_junk_files"); }));
                        }
                        else
                        {
                            ProcessedFiles.Items.Add(string.Empty);
                            ProcessedFiles.Items.Add("processing_junk_files");
                        }
                        gainedSizeBytes += Clean.ProcessJunk(junk);
                    }
                    else if (Equals(tag, 3))
                    {
                        if (ProcessedFiles.InvokeRequired)
                        {
                            ProcessedFiles.Invoke(new MethodInvoker(delegate { ProcessedFiles.Items.Add(string.Empty); }));
                            ProcessedFiles.Invoke(new MethodInvoker(delegate { ProcessedFiles.Items.Add("processing_zero_byte_files"); }));
                        }
                        else
                        {
                            ProcessedFiles.Items.Add(string.Empty);
                            ProcessedFiles.Items.Add("processing_zero_byte_files");
                        }
                        Clean.ProcessZero(zero);
                    }
                }
            }
        }

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            HideProcessing();
            if (labelClean.InvokeRequired)
            {
                labelClean.Invoke(new MethodInvoker(delegate
                {
                    labelClean.Text = rm.GetString("cleaning_completed") + ".";
                }));
            }
            else
            {
                labelClean.Text = rm.GetString("cleaning_completed") + ".";
            }
            string sizeGained = gainedSizeBytes == 0
                                    ? rm.GetString("no_extra_space")
                                    : rm.GetString("you_gained") + " " + FormatSize(gainedSizeBytes);

            if (ProcessedFiles.InvokeRequired)
            {
                ProcessedFiles.Invoke(new MethodInvoker(delegate
                {
                    ProcessedFiles.Items.Add(string.Empty);
                    ProcessedFiles.Items.Add(rm.GetString("cleaning_completed") + ".");
                    ProcessedFiles.Items.Add(sizeGained + "!");
                    ProcessedFiles.Items.Add(rm.GetString("click_finish_return") + ".");
                }));
            }
            else
            {
                ProcessedFiles.Items.Add(string.Empty);
                ProcessedFiles.Items.Add(rm.GetString("cleaning_completed") + ".");
                ProcessedFiles.Items.Add(sizeGained + "!");
                ProcessedFiles.Items.Add(rm.GetString("click_finish_return") + ".");
            }

            if (buttonNext.InvokeRequired)
            {
                buttonNext.Invoke(new MethodInvoker(delegate
                {
                    buttonNext.Text = rm.GetString("finish");
                    buttonNext.Enabled = true;
                }));
            }
            else
            {
                buttonNext.Text = rm.GetString("finish");
                buttonNext.Enabled = true;
            }

            if (buttonBack.InvokeRequired)
            {
                buttonBack.Invoke(new MethodInvoker(delegate
                {
                    buttonBack.Visible = false;
                }));
            }
            else
            {
                buttonBack.Visible = false;
            }
        }

        /// <summary>
        /// Shows Processing form
        /// </summary>
        public void ShowProcessing()
        {
            frmProcessing = new FormProcessing();
            frmProcessing.Show();
            frmProcessing.BringToFront();
            Application.DoEvents();
        }

        /// <summary>
        /// Hides Processing form
        /// </summary>
        public void HideProcessing()
        {
            if (frmProcessing.InvokeRequired)
            {
                frmProcessing.Invoke(new MethodInvoker(delegate
                {
                    frmProcessing.Close();
                }));
            }
            else
                frmProcessing.Close();
        }

        /// <summary>
        /// handle Click event to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonBack_Click(object sender, EventArgs e)
        {
            refreshBtn.Visible = true;
            ABORT = true;
            buttonNext.Text = rm.GetString("next");
            DoSearchVisible(1);
        }

        /// <summary>
        /// handle Click event to show options form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonOptions_Click(object sender, EventArgs e)
        {
            formPref.listViewExclude.ItemChecked -= formPref.listViewGeneral_ItemChecked;
            formPref.listViewGeneral.ItemChecked -= formPref.listViewGeneral_ItemChecked;
            formPref.listViewInclude.ItemChecked -= formPref.listViewGeneral_ItemChecked;

            formPref.listViewGeneral.Items.Clear();
            foreach (Option opt in Preferences.FileExtensions)
            {
                ListViewItem item = new ListViewItem(opt.Value);
                item.SubItems.Add(opt.Description);
                item.Checked = opt.Checked;
                item.Tag = opt;
                formPref.listViewGeneral.Items.Add(item);
            }
            formPref.listViewExclude.Items.Clear();
            foreach (Option opt in Preferences.PathExcluded)
            {
                ListViewItem item = new ListViewItem(opt.Value);
                item.SubItems.Add(opt.Description);
                item.Checked = opt.Checked;
                item.Tag = opt;
                formPref.listViewExclude.Items.Add(item);
            }
            formPref.listViewInclude.Items.Clear();
            foreach (Option opt in Preferences.PathIncluded)
            {
                ListViewItem item = new ListViewItem(opt.Value);
                item.SubItems.Add(opt.Description);
                item.Checked = opt.Checked;
                item.Tag = opt;
                formPref.listViewInclude.Items.Add(item);
            }
            if (formPref.ShowDialog() == DialogResult.OK)
            {
                Preferences.FileExtensions.Clear();
                Preferences.PathExcluded.Clear();
                Preferences.PathIncluded.Clear();
                foreach (ListViewItem item in formPref.listViewGeneral.Items)
                    Preferences.FileExtensions.Add(item.Tag as Option);
                foreach (ListViewItem item in formPref.listViewExclude.Items)
                    Preferences.PathExcluded.Add(item.Tag as Option);
                foreach (ListViewItem item in formPref.listViewInclude.Items)
                    Preferences.PathIncluded.Add(item.Tag as Option);
                Preferences.Save(Preferences.Filename);
            }
        }

        /// <summary>
        /// handle ItemChecked event to update the search locations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void listViewDrives_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                DriveInfo driveInfo = e.Item.Tag as DriveInfo;
                if (driveInfo != null) Preferences.CheckedNames += driveInfo.Name + "|";
            }
            else
            {
                DriveInfo info = e.Item.Tag as DriveInfo;
                if (info != null)
                    Preferences.CheckedNames = Preferences.CheckedNames.Replace(
                        info.Name + "|", string.Empty);
            }
        }

        /// <summary>
        /// handle FormClosing event to abort search before closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (this.RUN)
            {
                e.Cancel = true;
                return;
            }
             */
            ABORT = true;
            Preferences.Save(Preferences.Filename);
        }

        /// <summary>
        /// load logical drives on the computer
        /// </summary>
        void LoadDrives()
        {
            try
            {
                listViewDrives.Items.Clear();

                formPref = new FormPreferences();
                DoSearchVisible(1);
                Preferences.Load(Preferences.Filename);

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
                        if (drive.IsReady && (drive.DriveType == DriveType.Fixed || drive.DriveType == DriveType.Removable))
                        {
                            ListViewItem item = new ListViewItem(
                                string.IsNullOrEmpty(drive.VolumeLabel) ? drive.Name : drive.VolumeLabel + " (" + drive.Name + ")");
                            item.SubItems.Add((drive.TotalSize / Math.Pow(1024, 3)).ToString("N2") + " GB");
                            item.SubItems.Add((drive.AvailableFreeSpace / Math.Pow(1024, 3)).ToString("N2") + " GB");
                            item.Tag = drive;
                            listViewDrives.Items.Add(item);
                        }
                    }

                    if (!string.IsNullOrEmpty(Preferences.CheckedNames))
                    {
                        foreach (ListViewItem item in listViewDrives.Items)
                        {
                            DriveInfo driveInfo = item.Tag as DriveInfo;
                            if (driveInfo != null)
                                item.Checked = Preferences.CheckedNames.IndexOf(driveInfo.Name + "|") != -1;
                        }
                    }
                }

                listViewDrives.ItemChecked += listViewDrives_ItemChecked;
                temp = new DirectoryInfo(Path.GetTempPath());
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        void GetFilesRecursive(string b)
        {
            if (!DirectoryAllowed(b)) return;
            Stack<string> stack = new Stack<string>();
            stack.Push(b);
            while (stack.Count > 0)
            {
                if (ABORT) return;
                string dir = stack.Pop();
                try
                {
                    foreach (string filename in Directory.GetFiles(dir, "*"))
                    {
                        if (ABORT) return;
                        ProcessFile(filename);
                        Application.DoEvents();
                    }
                    foreach (string dn in Directory.GetDirectories(dir))
                    {
                        if (ABORT) return;
                        if (DirectoryAllowed(dn))
                            stack.Push(dn);
                        Application.DoEvents();
                    }
                }
                catch
                {
                }
                Application.DoEvents();
            }
        }

        void Start()
        {
            junk.Clear();
            zero.Clear();
            junksize = binsize = 0;
            ABORT = false;
            progressBarSearch.Maximum = 10000;
            progressBarSearch.Value = 0;

            buttonNext.Enabled = false;

            ListViewItem found;
            foreach (object info in scan)
            {
                try
                {
                    progressBarSearch.Value = 0;
                    found = null;
                    foreach (ListViewItem item in listViewScanning.Items)
                        if (Equals(item.Tag, info))
                        {
                            found = item;
                            found.SubItems[1].Text = rm.GetString("scanning") + " ... ";
                            break;
                        }
                    if (info is DriveInfo)
                    {
                        DriveInfo nfo = info as DriveInfo;
                        progressBarSearch.Maximum = (int)((nfo.TotalSize - nfo.TotalFreeSpace) / (1024 * 512));
                        GetFilesRecursive((info as DriveInfo).RootDirectory.FullName);
                    }
                    if (info is DirectoryInfo)
                    {
                        progressBarSearch.Maximum = 500;
                        GetFilesRecursive((info as DirectoryInfo).FullName);
                    }
                    if (found != null)
                        found.SubItems[1].Text = rm.GetString("done");
                    if (ABORT) break;
                }
                catch { }
            }
            //this.RUN = false;
            progressBarSearch.Value = 0;
            labelProgress.Text = rm.GetString("n/a");

            listViewJunk.Items.Clear();

            if (!ABORT)
            {
                DoSearchVisible(3);

                ListViewItem item;
                listViewJunk.ItemChecked -= listViewJunk_ItemChecked;

                ulong total, size;
                RecycleBin.GetRecycleBinSize(out total, out size);
                if (total != 0)
                {
                    binsize = size;
                    item = new ListViewItem(rm.GetString("recycle_bin"));
                    item.SubItems.Add(total.ToString());
                    item.SubItems.Add(GetSizeInMB(binsize));
                    item.Tag = 1;
                    item.Checked = true;
                    listViewJunk.Items.Add(item);
                }

                item = new ListViewItem(rm.GetString("temporary_files"));
                item.SubItems.Add(junk.Count.ToString());
                item.SubItems.Add(GetSizeInMB(junksize));
                item.Tag = 2;
                item.Checked = true;
                listViewJunk.Items.Add(item);

                if (Preferences.FileSelectZero)
                {
                    item = new ListViewItem(rm.GetString("zero_byte_files"));
                    item.SubItems.Add(zero.Count.ToString());
                    item.SubItems.Add(GetSizeInMB(0));
                    item.Tag = 3;
                    item.Checked = false;
                    listViewJunk.Items.Add(item);
                }

                listViewJunk.ItemChecked += listViewJunk_ItemChecked;
                labelGain.Text = GetSizeInMB(junksize + binsize);
                listViewJunk.SelectedItems.Clear();
                listViewJunk.Items[0].Selected = true;

                labelFinal.Text = rm.GetString("you_can_use_cleanup") + " " + GetSizeInMB(binsize + junksize) + rm.GetString("of_disk_space") + " " + Preferences.CheckedNames.Replace('|', ' ');
            }
            ABORT = false;

            buttonNext.Enabled = true;
        }

        string GetSizeInMB(decimal size)
        {
            return (size / (decimal)Math.Pow(1024, 2)).ToString("N2") + " MB";
        }

        void ProcessFile(string filename)
        {
            labelProgress.Text = filename;
            progressBarSearch.Value += 1;
            if (progressBarSearch.Value == progressBarSearch.Maximum)
                progressBarSearch.Value = 0;

            bool found = false;
            if (Preferences.ScanTemporaryFolder &&
                filename.ToLower().IndexOf(temp.FullName.ToLower()) != -1)
                found = true;
            if (!found)
            {
                string file = filename;
                if (file.Contains('\\'))
                {
                    file = file.Substring(file.LastIndexOf('\\') + 1);
                }
                foreach (Option opt in Preferences.FileExtensions)
                    if (opt.Checked)
                        if (Regex.IsMatch(file, WildcardToRegex(opt.Value)))
                        {
                            found = true;
                            break;
                        }
            }
            if (!found)
                foreach (Option opt in Preferences.PathIncluded)
                    if (opt.Checked)
                        if (filename.ToLower().StartsWith(opt.Value.ToLower()))
                        {
                            found = true;
                            break;
                        }
            if (!found && filename.ToLower().EndsWith(".log") && filename.ToLower().StartsWith(windowsDir))
            {
                found = true;
            }
            FileInfo info = null;
            try
            {
                info = new FileInfo(filename);
            }
            catch
            {
            }
            if (!found && Preferences.FileSelectZero && info != null)
                found = info.Length == 0;

            if (found)
            {
                if (info != null)
                {
                    if (info.Length != 0)
                    {
                        junk.Add(new DeleteFile(info, true));
                        junksize += info.Length;
                    }
                    else
                    {
                        zero.Add(new DeleteFile(info, false));
                    }
                }
                else junk.Add(new DeleteFile(filename, true));
            }

            Application.DoEvents();
        }

        /// <summary>
        /// convert input pattern to regular expression
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        static string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern).
                            Replace("\\*", ".*").
                            Replace("\\?", ".") + "$";
        }

        static bool DirectoryAllowed(string dir)
        {
            bool trailingdelimiter = dir.LastIndexOf(Path.DirectorySeparatorChar) == dir.Length - 1;

            string compare = Path.GetDirectoryName(Application.ExecutablePath);
            if (trailingdelimiter) compare += Path.DirectorySeparatorChar;
            if (Regex.IsMatch(dir, WildcardToRegex(compare), RegexOptions.IgnoreCase)) return false;

            foreach (Option opt in Preferences.PathExcluded)
                if (opt.Checked)
                {
                    compare = opt.Value;
                    if (trailingdelimiter)
                    {
                        if (compare.LastIndexOf(Path.DirectorySeparatorChar) != compare.Length - 1)
                            compare += Path.DirectorySeparatorChar;
                    }
                    else
                    {
                        if (compare.LastIndexOf(Path.DirectorySeparatorChar) == compare.Length - 1)
                            compare = compare.Remove(compare.Length - 1);
                    }
                    if (Regex.IsMatch(dir, WildcardToRegex(compare), RegexOptions.IgnoreCase))
                        return false;
                }
            return true;
        }

        /// <summary>
        /// format size for display
        /// </summary>
        /// <param name="gainedSizeBytes"></param>
        /// <returns></returns>
        string FormatSize(ulong gainedSizeBytes)
        {
            double size = gainedSizeBytes;
            string unit = " Bytes";
            if ((int)(size / 1024) > 0)
            {
                size /= 1024;
                unit = " KB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024;
                unit = " MB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024;
                unit = " GB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024;
                unit = " TB";
            }

            return size.ToString("0.##") + unit;
        }

        /// <summary>
        /// handle SelectedIndexChanged event to update item description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void listViewJunk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewJunk.SelectedItems.Count == 0)
            {
                labelGroup.Text = string.Empty;
                checkBoxGroup.Visible = false;
                buttonViewFiles.Visible = false;
                return;
            }
            ListViewItem item = listViewJunk.SelectedItems[0];
            if (Equals(item.Tag, 1))
            {
                // recycle bin
                labelGroup.Text = rm.GetString("recycle_bin_def");
                checkBoxGroup.Visible = false;
                buttonViewFiles.Visible = true;
                buttonViewFiles.Tag = item.Tag;
            }
            else if (Equals(item.Tag, 2))
            {
                // temporary
                labelGroup.Text = rm.GetString("temp_files_def") + ".";
                buttonViewFiles.Tag = item.Tag;
                checkBoxGroup.Visible = true;
                checkBoxGroup.Text = rm.GetString("backup_temp_files") + ".";
                checkBoxGroup.Checked = Preferences.BackupJunk;
                buttonViewFiles.Visible = true;
            }
            else
            {
                // zero-byte
                labelGroup.Text = rm.GetString("zero_files_def") + ".";
                buttonViewFiles.Tag = item.Tag;
                checkBoxGroup.Visible = true;
                checkBoxGroup.Text = rm.GetString("backup_zero_files") + ".";
                checkBoxGroup.Checked = Preferences.BackupZero;
                buttonViewFiles.Visible = true;
            }
        }

        /// <summary>
        /// handle ItemChecked event to update items to be deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void listViewJunk_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            decimal gain = 0;
            foreach (ListViewItem item in listViewJunk.Items)
            {
                if (item.Checked)
                {
                    if (Equals(item.Tag, 1)) gain += binsize;
                    if (Equals(item.Tag, 2)) gain += junksize;
                }
                if (Equals(item.Tag, 2))
                {
                    foreach (DeleteFile del in junk)
                        del.Delete = item.Checked;
                    item.Text = item.Text.Replace(" " + rm.GetString("partial"), string.Empty);
                }
                if (Equals(item.Tag, 3))
                {
                    foreach (DeleteFile del in zero)
                        del.Delete = item.Checked;
                    item.Text = item.Text.Replace(" " + rm.GetString("partial"), string.Empty);
                }
            }
            labelGain.Text = GetSizeInMB(gain);
        }

        /// <summary>
        /// handle CheckedChanged event to
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkBoxGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (Equals(buttonViewFiles.Tag, 2))
                Preferences.BackupJunk = checkBoxGroup.Checked;
            else
                Preferences.BackupZero = checkBoxGroup.Checked;
        }

        /// <summary>
        /// handle Click event to view files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonViewFiles_Click(object sender, EventArgs e)
        {
            if (Equals(buttonViewFiles.Tag, 1))
            {
                RecycleBin.Open();
            }
            else if (Equals(buttonViewFiles.Tag, 2))
            {
                formView.lvMain.Items.Clear();
                foreach (DeleteFile data in junk)
                {
                    try
                    {
                        ListViewItem item = new ListViewItem(Path.GetFileNameWithoutExtension(data.Name));
                        item.SubItems.Add(Path.GetExtension(data.Name));
                        item.SubItems.Add(Path.GetDirectoryName(data.Name));
                        item.SubItems.Add(data.Info != null ? GetSizeInMB(data.Info.Length) : "N/A");
                        item.Tag = data;
                        item.Checked = data.Delete;
                        formView.lvMain.Items.Add(item);
                    }
                    catch { }
                }
                if (formView.ShowDialog() == DialogResult.OK)
                {
                    int check = junk.Cast<DeleteFile>().Count(file => file.Delete);
                    listViewJunk.ItemChecked -= listViewJunk_ItemChecked;
                    foreach (ListViewItem item in listViewJunk.Items)
                        if (Equals(item.Tag, buttonViewFiles.Tag))
                        {
                            if (check != 0)
                            {
                                item.Checked = true;
                                if (check != junk.Count)
                                    if (item.Text.IndexOf(" " + rm.GetString("partial")) == -1) item.Text += " " + rm.GetString("partial");
                                    else
                                        item.Text = item.Text.Replace(" " + rm.GetString("partial"), string.Empty);
                            }
                            else
                            {
                                item.Checked = false;
                                item.Text = item.Text.Replace(" " + rm.GetString("partial"), string.Empty);
                            }
                        }
                    listViewJunk.ItemChecked += listViewJunk_ItemChecked;
                }
            }
            else
            {
                formView.lvMain.Items.Clear();
                foreach (DeleteFile data in zero)
                {
                    try
                    {
                        ListViewItem item = new ListViewItem(Path.GetFileNameWithoutExtension(data.Name));
                        item.SubItems.Add(Path.GetExtension(data.Name));
                        item.SubItems.Add(Path.GetDirectoryName(data.Name));
                        item.SubItems.Add(data.Info != null ? GetSizeInMB(data.Info.Length) : "N/A");
                        item.Tag = data;
                        item.Checked = data.Delete;
                        formView.lvMain.Items.Add(item);
                    }
                    catch { }
                }
                if (formView.ShowDialog() == DialogResult.OK)
                {
                    int check = zero.Cast<DeleteFile>().Count(file => file.Delete);
                    listViewJunk.ItemChecked -= listViewJunk_ItemChecked;
                    foreach (ListViewItem item in listViewJunk.Items)
                        if (Equals(item.Tag, buttonViewFiles.Tag))
                        {
                            if (check != 0)
                            {
                                item.Checked = true;
                                if (check != zero.Count)
                                    if (item.Text.IndexOf(" " + rm.GetString("partial")) == -1) item.Text += " " + rm.GetString("partial");
                                    else
                                        item.Text = item.Text.Replace(" " + rm.GetString("partial"), string.Empty);
                            }
                            else
                            {
                                item.Checked = false;
                                item.Text = item.Text.Replace(" " + rm.GetString("partial"), string.Empty);
                            }
                        }
                    listViewJunk.ItemChecked += listViewJunk_ItemChecked;
                }
            }
        }

        /// <summary>
        /// handle Click event to show list of backup items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonRestore_Click(object sender, EventArgs e)
        {
            Clean.OnProgress -= Clean_OnProgress;
            FormRestore formRest = new FormRestore();
            formRest.ShowDialog();
            Clean.OnProgress += Clean_OnProgress;
        }

        /// <summary>
        /// handle Click event to show windows components
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonCleanWin_Click(object sender, EventArgs e)
        {
            if (OSisXp())
            {
                string curFile = Environment.ExpandEnvironmentVariables("%SystemRoot%") + "\\system32\\sysocmgr.exe ";
                if (File.Exists(curFile))
                {
                    Process.Start(curFile, "/i:c:\\" + Environment.ExpandEnvironmentVariables("%SystemRoot%") + "\\inf\\sysoc.inf");
                }
                else
                {
                    MessageBox.Show(rm.GetString("tool_not_found"),
                                    rm.GetString("critical_warning"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            else
            {
                string curFile = Environment.ExpandEnvironmentVariables("%SystemRoot%") + "\\system32\\OptionalFeatures.exe ";
                if (File.Exists(curFile))
                {
                    Process.Start(curFile);
                }
            }
        }

        /// <summary>
        /// check if the current operating system is windows xp
        /// </summary>
        /// <returns></returns>
        public static Boolean OSisXp()
        {
            // Get OperatingSystem information from the system namespace.
            OperatingSystem osInfo = Environment.OSVersion;

            // Determine the platform.
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
            return false;
        }

        /// <summary>
        /// handle Click event to reload logical drives
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void refreshBtn_Click(object sender, EventArgs e)
        {
            LoadDrives();
        }

        /// <summary>
        /// initialize FormMain
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormMain_Load(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo(CfgFile.Get("Lang"));
            SetCulture(culture);
            if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\FreeGamingBooster.exe"))
            {
                this.Icon = Properties.Resources.GBicon;
            }
            else if (!File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\FreemiumUtilities.exe"))
            {
                this.Icon = Properties.Resources.PCCleanerIcon;
            }
            else
            {
                this.Icon = Properties.Resources.FSUIcon;
            }
        }

        /// <summary>
        /// change current language
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            ResourceManager rm = new ResourceManager("Disk_Cleaner.Resources", typeof(FormMain).Assembly);
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FormMain));
            Thread.CurrentThread.CurrentUICulture = culture;

            tabPageCleanup.Text = rm.GetString("disk_cleanup");
            refreshBtn.Text = rm.GetString("refresh");
            labelFinal.Text = rm.GetString("n/a");
            lblDiskSpace.Text = rm.GetString("total_space_gain") + ":";
            grbDescription.Text = rm.GetString("description");
            buttonViewFiles.Text = rm.GetString("view_files");
            labelGain.Text = rm.GetString("n/a");
            clhObjects.Text = rm.GetString("objects");
            clhSize.Text = rm.GetString("size");
            labelProgress.Text = rm.GetString("n/a");
            clhNames.Text = rm.GetString("name");
            clhStatus.Text = rm.GetString("status");
            lblWait.Text = rm.GetString("wait_until_scanning") + ".";
            buttonNext.Text = rm.GetString("next");
            buttonBack.Text = rm.GetString("back");
            buttonOptions.Text = rm.GetString("options");
            lblClickNext.Text = rm.GetString("click_next_after_selecting") + ".";
            clhName.Text = rm.GetString("name");
            clhTotalSize.Text = rm.GetString("total_size");
            clhFree.Text = rm.GetString("free_size");
            lblSelectDrives.Text = rm.GetString("select_drives_for_cleanup");
            tabPageOptions.Text = rm.GetString("more_options");
            grbRestore.Text = rm.GetString("restore");
            buttonRestore.Text = rm.GetString("restore") + "...";
            lblRestore.Text = rm.GetString("you_can_restore") + ".";
            grbWinComponents.Text = rm.GetString("windows_components");
            buttonCleanWin.Text = rm.GetString("cleanup") + "...";
            lblFreeSpace.Text = rm.GetString("you_can_free_more_space") + ".";
            buttonClose.Text = rm.GetString("close");
            Icon = ((Icon)(resources.GetObject("$this.Icon")));
            Text = rm.GetString("disk_cleaner");
            ucTop.Text = rm.GetString("disk_cleaner");
        }
    }
}