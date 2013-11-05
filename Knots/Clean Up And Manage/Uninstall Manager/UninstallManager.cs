using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using Microsoft.Win32;
using System.IO;

namespace Uninstall_Manager
{
    /// <summary>
    /// Uninstall Manager knot main form
    /// </summary>
    public partial class UninstallManager : Form
    {
        const int dtInstalledApps_ImgInd = 0;
        const int dtInstalledApps_Name = 1;
        const int dtInstalledApps_Publisher = 2;
        const int dtInstalledApps_InstallDate = 3;
        const int dtInstalledApps_Size = 4;
        const int dtInstalledApps_Version = 5;
        const int dtInstalledApps_IsUpdate = 6;

        const int dgvInstalledApps_CheckBox = 0;
        const int dgvInstalledApps_ImgInd = 1;
        const int dgvInstalledApps_Name = 2;
        const int dgvInstalledApps_Publisher = 3;
        const int dgvInstalledApps_InstallDate = 4;
        const int dgvInstalledApps_Size = 5;

        readonly DataTable dtInstalledApps = new DataTable("InstalledApplications");
        readonly InstallDateComparer mInstallDateCompare = new InstallDateComparer(SortOrder.Ascending);
        readonly InstallSizeComparer mInstallSizeCompare = new InstallSizeComparer(SortOrder.Ascending);
        readonly ResourceManager rm = new ResourceManager("Uninstall_Manager.Resources", Assembly.GetExecutingAssembly());
        CultureInfo culture;

        /// <summary>
        /// constructor for UninstallManager
        /// </summary>
        public UninstallManager()
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

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern uint ExtractIconEx(string szFileName, int nIconIndex,
                                         IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

        [DllImport("user32.dll", EntryPoint = "DestroyIcon", SetLastError = true)]
        static extern int DestroyIcon(IntPtr hIcon);

        /// <summary>
        /// Exctracts icon from the exe file
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="large">Is large icon needed</param>
        /// <returns><c>Icon</c></returns>
        public static Icon ExtractIconFromExe(string file, bool large)
        {
            IntPtr[] zhDummy = new IntPtr[2] { IntPtr.Zero, IntPtr.Zero };
            IntPtr[] zhIconEx = new IntPtr[2] { IntPtr.Zero, IntPtr.Zero };

            try
            {
                uint readIconCount = large ? ExtractIconEx(file, 0, zhIconEx, zhDummy, 1) : ExtractIconEx(file, 0, zhDummy, zhIconEx, 1);

                if (readIconCount > 0 && zhIconEx[0] != IntPtr.Zero)
                {
                    // GET FIRST EXTRACTED ICON
                    Icon extractedIcon = (Icon)Icon.FromHandle(zhIconEx[0]).Clone();

                    return extractedIcon;
                }
                else // NO ICONS READ
                    return null;
            }
            catch (Exception ex)
            {
                /* EXTRACT ICON ERROR */

                // BUBBLE UP
                throw new ApplicationException("Could not extract icon", ex);
            }
            finally
            {
                // RELEASE RESOURCES
                foreach (IntPtr ptr in zhIconEx)
                    if (ptr != IntPtr.Zero)
                        DestroyIcon(ptr);

                foreach (IntPtr ptr in zhDummy)
                    if (ptr != IntPtr.Zero)
                        DestroyIcon(ptr);
            }
        }

        /// <summary>
        /// initialize UninstallManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UninstallManager_Load(object sender, EventArgs e)
        {
            culture = new CultureInfo(CfgFile.Get("Lang"));
            ІetСulture(culture);

            removeEntryToolStripMenuItem.Enabled = false;
            uninstallThisProgramToolStripMenuItem.Enabled = false;

            Thread bgthread = new Thread(GetPrograms) { IsBackground = true };
            bgthread.Start();
            txtCommandLine.Clear();
            removeEntryToolStripMenuItem.Enabled = false;
            uninstallThisProgramToolStripMenuItem.Enabled = false;
        }

        void GetPrograms()
        {
            Thread.CurrentThread.CurrentUICulture = culture;
            List<InstalledProgram> lstInstalledPrograms = InstalledProgram.GetInstalledPrograms(String.Empty, showUpdatesChk.Checked);
            CreateInstalledAppTableTable(lstInstalledPrograms);
        }

        void CreateInstalledAppTableTable(List<InstalledProgram> lstInstalledPrograms)
        {
            try
            {
                dgvInstalledApps.Invoke(new MethodInvoker(delegate
                                                            {
                                                                dtInstalledApps.Columns.Clear();

                                                                dtInstalledApps.Columns.Add(rm.GetString("image"));
                                                                dtInstalledApps.Columns.Add(rm.GetString("name"));
                                                                dtInstalledApps.Columns.Add(rm.GetString("publisher"));
                                                                dtInstalledApps.Columns.Add(rm.GetString("installed_date"));
                                                                dtInstalledApps.Columns.Add(rm.GetString("size"));
                                                                dtInstalledApps.Columns.Add(rm.GetString("version"));
                                                                dtInstalledApps.Columns.Add(rm.GetString("is_update"));
                                                                dtInstalledApps.Columns.Add(rm.GetString("reg_key"));

                                                                dgvInstalledApps.Columns[dgvInstalledApps_CheckBox].Width = 20;
                                                                dgvInstalledApps.Columns[dgvInstalledApps_ImgInd].Width = 20;
                                                                dgvInstalledApps.Columns[dgvInstalledApps_Name].Width = 350;
                                                                dgvInstalledApps.Columns[dgvInstalledApps_Publisher].Width = 205;
                                                                dgvInstalledApps.Columns[dgvInstalledApps_InstallDate].Width = 100;

                                                                dgvInstalledApps.Columns[dgvInstalledApps_CheckBox].ReadOnly = false;
                                                                dgvInstalledApps.Columns[dgvInstalledApps_ImgInd].ReadOnly = true;
                                                                dgvInstalledApps.Columns[dgvInstalledApps_Name].ReadOnly = true;
                                                                dgvInstalledApps.Columns[dgvInstalledApps_Publisher].ReadOnly = true;
                                                                dgvInstalledApps.Columns[dgvInstalledApps_InstallDate].ReadOnly = true;

                                                                dgvInstalledApps.Columns[dgvInstalledApps_InstallDate].SortMode =
                                                                    DataGridViewColumnSortMode.Programmatic;
                                                                dgvInstalledApps.Columns[dgvInstalledApps_Size].SortMode =
                                                                    DataGridViewColumnSortMode.Programmatic;

                                                                dgvInstalledApps.ColumnHeaderMouseClick +=
                                                                    dgvInstalledApps_ColumnHeaderMouseClick;

                                                                UpdateInstalledApps(lstInstalledPrograms);
                                                            }));
            }
            catch
            {
            }
        }

        /// <summary>
        /// Handles the click on Install Date and Size columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgvInstalledApps_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 4) //Install Date
            {
                try
                {
                    mInstallDateCompare.Order();
                    dgvInstalledApps.Sort(mInstallDateCompare);
                }
                catch
                {
                }
            }
            else if (e.ColumnIndex == 5) //Size
            {
                try
                {
                    mInstallSizeCompare.Order();
                    dgvInstalledApps.Sort(mInstallSizeCompare);
                }
                catch
                {
                }
            }
        }

        void UpdateInstalledApps(IList<InstalledProgram> lstInstalledPrograms)
        {
            dtInstalledApps.Rows.Clear();

            try
            {
                if ((lstInstalledPrograms != null))
                {
                    foreach (InstalledProgram prog in lstInstalledPrograms)
                    {
                        DataRow rowInstallApps = dtInstalledApps.NewRow();
                        rowInstallApps[dtInstalledApps_ImgInd] = prog.DisplayIconPath;
                        rowInstallApps[dtInstalledApps_Name] = prog.DisplayName;
                        rowInstallApps[dtInstalledApps_Publisher] = prog.Publisher;
                        rowInstallApps[dtInstalledApps_InstallDate] = prog.InstallDate;
                        rowInstallApps[dtInstalledApps_Size] = prog.InstallSize;
                        rowInstallApps[dtInstalledApps_Version] = prog.Version;
                        dtInstalledApps.Rows.Add(rowInstallApps);
                    }
                }
                Enabled = true;
                UpdateGrid(true);
                UpdateToolStrip();
                dgvInstalledApps.Sort(dgvInstalledApps.Columns[dgvInstalledApps_Name], ListSortDirection.Ascending);
                dgvInstalledApps.ClearSelection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(rm.GetString("error_occurred") + ": " + ex.Message);
            }

            dgvInstalledApps.Invoke(new MethodInvoker(delegate
                                                        {
                                                            if (dgvInstalledApps.SelectedRows.Count > 0)
                                                            {
                                                                dgvInstalledApps.Refresh();
                                                                dgvInstalledApps.SelectedRows[0].Selected = false;
                                                            }
                                                        }));
            txtCommandLine.Invoke(new MethodInvoker(delegate
                                                        {
                                                            txtCommandLine.Clear();
                                                            removeEntryToolStripMenuItem.Enabled = false;
                                                            uninstallThisProgramToolStripMenuItem.Enabled = false;
                                                        }));
            mnuRefreshApps.Enabled = true;
            mnuRefreshApps.Visible = true;
        }

        void UpdateGrid(bool showWindowsUpdate)
        {
            dgvInstalledApps.Rows.Clear();

            Icon newIco;
            Graphics g;

            foreach (DataRow row in dtInstalledApps.Rows)
            {
                if (!showWindowsUpdate && (Boolean.TrueString.Equals(row[dtInstalledApps_IsUpdate].ToString())))
                {
                    continue;
                }
                try
                {
                    String iconFile = row[dtInstalledApps_ImgInd].ToString();
                    newIco = Resource.install;

                    try
                    {
                        if (String.IsNullOrEmpty(iconFile))
                        {
                            newIco = Resource.install;
                        }
                        else if (iconFile.Contains(","))
                        {
                        }
                        else
                        {
                            Icon extractAssociatedIcon = Icon.ExtractAssociatedIcon(iconFile);
                            if (extractAssociatedIcon != null)
                            {
                                Bitmap tmpImg = extractAssociatedIcon.ToBitmap();
                                Bitmap result = new Bitmap(16, 16);
                                using (g = Graphics.FromImage(result))
                                {
                                    g.DrawImage(tmpImg, 0, 0, 16, 16);
                                }
                                newIco = Icon.FromHandle(result.GetHicon());
                            }
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        DataRow row1 = row;
                        Icon ico = newIco;
                        dgvInstalledApps.Invoke(new MethodInvoker(() => dgvInstalledApps.Rows.Add(
                            new object[]
								{
									false,									
									ico,
									row1[dtInstalledApps_Name].ToString(),
									row1[dtInstalledApps_Publisher].ToString(),
									row1[dtInstalledApps_InstallDate].ToString(),
									row1[dtInstalledApps_Size].ToString(),
									row1[dtInstalledApps_Version].ToString(),
									row1[regKey.HeaderText].ToString()
								})));
                    }
                    catch
                    {
                    }
                }
                catch
                {
                }
            }
        }

        void UpdateToolStrip()
        {
            tslTotal.Text = rm.GetString("total") + " " + dgvInstalledApps.Rows.Count + " " +
                                         rm.GetString("programs");
        }

        void dgvInstalledApps_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvInstalledApps.SelectedRows)
            {
                try
                {
                    row.Height = 40;
                    row.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    string sUninstallString = InstalledProgram.UninstallKeys[row.Cells[2].Value.ToString()];
                    if (sUninstallString.StartsWith("MsiExec.exe"))
                    {
                        sUninstallString = sUninstallString.Replace("MsiExec.exe", String.Empty);
                    }
                    else if (sUninstallString.StartsWith("msiexec"))
                    {
                        sUninstallString = sUninstallString.Replace("msiexec", String.Empty);
                    }
                    else
                    {
                        string unins1 = sUninstallString;

                        if (unins1.IndexOf(@"\INSTALL.") != -1)
                        {
                            string tmp1 = unins1.Replace(@"\INSTALL.LOG", String.Empty);
                            tmp1 = tmp1.Replace(@"\install.log", String.Empty);
                            string tmp2 = String.Empty;
                            string tmp3;
                            string s = tmp1;
                            int i = 0;

                            while ((i = s.IndexOf(' ', i)) != -1)
                            {
                                tmp2 = (String.Empty + s.Substring(i));

                                i++;
                            }

                            tmp3 = tmp1.Replace(tmp2, String.Empty); // UNWISE.EXE
                            sUninstallString = tmp3.Replace(@"/U", String.Empty);
                        }
                        else
                        {
                            int exeEnds = unins1.IndexOf(".exe") + 4;
                            sUninstallString = unins1.Substring(0, exeEnds);
                        }
                    }
                    txtCommandLine.Text = sUninstallString.Replace("\"", String.Empty);
                }
                catch
                {
                    txtCommandLine.Text = String.Empty;
                }
            }
        }

        void batchUninstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const bool setCheckBoxState = false;
            if (dgvInstalledApps.Columns[dgvInstalledApps_CheckBox].Visible)
            {
                dgvInstalledApps.Columns[dgvInstalledApps_CheckBox].Visible = false;
                uninstallThisProgramToolStripMenuItem.Text = rm.GetString("uninstall_program");
                removeEntryToolStripMenuItem.Text = rm.GetString("remove_entry");
            }
            else
            {
                dgvInstalledApps.Columns[dgvInstalledApps_CheckBox].Visible = true;
                uninstallThisProgramToolStripMenuItem.Text = rm.GetString("uninstall_programs");
                removeEntryToolStripMenuItem.Text = rm.GetString("remove_entries");
            }
            foreach (DataGridViewRow row in dgvInstalledApps.Rows)
            {
                row.Cells[dgvInstalledApps_CheckBox].Value = setCheckBoxState;
            }
        }

        void uninstallThisProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedApps = new StringCollection();

            if (dgvInstalledApps.Columns[dgvInstalledApps_CheckBox].Visible)
            {
                foreach (DataGridViewRow row in dgvInstalledApps.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[dgvInstalledApps_CheckBox].Value))
                    {
                        selectedApps.Add(row.Cells[dgvInstalledApps_Name].Value.ToString());
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgvInstalledApps.SelectedRows)
                {
                    selectedApps.Add(row.Cells[dgvInstalledApps_Name].Value.ToString());
                }
            }

            foreach (String app in selectedApps)
            {
                try
                {
                    string sUninstallString = InstalledProgram.UninstallKeys[app];

                    string uninstallString = sUninstallString.Replace("\"", String.Empty);

                    if (sUninstallString.StartsWith("MsiExec.exe") || sUninstallString.StartsWith("msiexec"))
                    {
                        if (sUninstallString.StartsWith("MsiExec.exe"))
                            sUninstallString = sUninstallString.Replace("MsiExec.exe", String.Empty);
                        else
                            sUninstallString = sUninstallString.Replace("msiexec", String.Empty);
                        Process p = new Process();
                        p.StartInfo.FileName = "MsiExec.exe";
                        p.StartInfo.Arguments = sUninstallString;
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.Start();
                        p.WaitForExit();
                    }
                    else
                    {
                        string unins1 = uninstallString;

                        if (unins1.IndexOf(@"\INSTALL.") != -1)
                        {
                            string tmp1 = unins1.Replace(@"\INSTALL.LOG", String.Empty);
                            tmp1 = tmp1.Replace(@"\install.log", String.Empty);
                            string tmp2 = String.Empty;
                            string s = tmp1;
                            int i = 0;

                            while ((i = s.IndexOf(' ', i)) != -1)
                            {
                                tmp2 = (String.Empty + s.Substring(i));

                                i++;
                            }

                            string tmp3 = tmp1.Replace(tmp2, String.Empty);
                            tmp3 = tmp3.Replace(@"/U", String.Empty);
                            var p = new Process { StartInfo = { FileName = tmp3 } };
                            p.Start();
                            p.WaitForExit();
                        }
                        else
                        {
                            int exeEnds = unins1.IndexOf(".exe") + 4;
                            String exe = unins1.Substring(0, exeEnds);
                            String arguments = exeEnds == unins1.Length ? String.Empty : unins1.Substring(exeEnds + 1).Trim();

                            Process p = new Process
                                        {
                                            StartInfo = { FileName = exe, Arguments = arguments, UseShellExecute = false, RedirectStandardOutput = true }
                                        };
                            p.Start();
                            p.WaitForExit();
                        }
                    }
                }
                catch
                {
                    if (MessageBox.Show(string.Format(rm.GetString("unistaller_not_found"), app),
                                        rm.GetString("error"), MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        try
                        {
                            RegistryKey wow64UninstallKey = Registry.LocalMachine.OpenSubKey(@"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\");
                            LookForTheApp(app, Registry.LocalMachine, wow64UninstallKey, @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\", "DisplayName");

                            RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\");
                            LookForTheApp(app, Registry.LocalMachine, rk, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\", "DisplayName");

                            foreach (string userSid in Registry.Users.GetSubKeyNames())
                            {
                                RegistryKey cuUnInstallKey = Registry.Users.OpenSubKey(userSid + @"\Software\Microsoft\Windows\CurrentVersion\Uninstall\");
                                if (cuUnInstallKey != null)
                                {
                                    LookForTheApp(app, Registry.Users, cuUnInstallKey, userSid + @"\Software\Microsoft\Windows\CurrentVersion\Uninstall\", "DisplayName");
                                }

                                RegistryKey cuInstallerKey = Registry.Users.OpenSubKey(userSid + @"\Software\Microsoft\Installer\Products\");
                                if (cuInstallerKey != null)
                                {
                                    LookForTheApp(app, Registry.Users, cuInstallerKey, userSid + @"\Software\Microsoft\Installer\Products\", "ProductName");
                                }
                            }
                        }
                        catch { }
                    }
                }
            }

            txtCommandLine.Clear();
            removeEntryToolStripMenuItem.Enabled = false;
            uninstallThisProgramToolStripMenuItem.Enabled = false;
            mnuRefreshApps_Click(null, null);
        }

        void removeEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(rm.GetString("entry_remove_confirm"), rm.GetString("uninstall_manager"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes
              )
            {


                StringCollection selectedApps = new StringCollection();
                if (dgvInstalledApps.Columns[dgvInstalledApps_CheckBox].Visible)
                {
                    foreach (DataGridViewRow row in dgvInstalledApps.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[dgvInstalledApps_CheckBox].Value))
                            selectedApps.Add(row.Cells[dgvInstalledApps_Name].Value.ToString());
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dgvInstalledApps.SelectedRows)
                    {
                        selectedApps.Add(row.Cells[dgvInstalledApps_Name].Value.ToString());
                    }
                }

                int appsRemoved = 0;

                RegistryKey wow64UninstallKey =
                    Registry.LocalMachine.OpenSubKey(@"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\");
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\");

                foreach (String appName in selectedApps)
                {
                    appsRemoved += LookForTheApp(
                        appName,
                        Registry.LocalMachine,
                        wow64UninstallKey,
                        @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\",
                        "DisplayName");

                    appsRemoved += LookForTheApp(
                        appName,
                        Registry.LocalMachine,
                        rk,
                        @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\",
                        "DisplayName");

                    foreach (string userSid in Registry.Users.GetSubKeyNames())
                    {
                        RegistryKey cuUnInstallKey = Registry.Users.OpenSubKey(userSid + @"\Software\Microsoft\Windows\CurrentVersion\Uninstall\");
                        if (cuUnInstallKey != null)
                        {
                            appsRemoved += LookForTheApp(
                                appName,
                                Registry.Users,
                                cuUnInstallKey,
                                userSid + @"\Software\Microsoft\Windows\CurrentVersion\Uninstall\",
                                "DisplayName");
                        }

                        RegistryKey cuInstallerKey = Registry.Users.OpenSubKey(userSid + @"\Software\Microsoft\Installer\Products\");
                        if (cuInstallerKey != null)
                        {
                            appsRemoved += LookForTheApp(
                                appName,
                                Registry.Users,
                                cuInstallerKey,
                                userSid + @"\Software\Microsoft\Installer\Products\",
                                "ProductName");
                        }
                    }
                }

                if (appsRemoved > 0)
                {
                    if (appsRemoved == 1)
                    {
                        MessageBox.Show(rm.GetString("one_entry_removed"), rm.GetString("uninstall_manager"), MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0} {1}", appsRemoved, rm.GetString("entries_removed")), rm.GetString("uninstall_manager"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(rm.GetString("no_one_removed"), rm.GetString("uninstall_manager"), MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

                Thread bgthread = new Thread(GetPrograms);
                bgthread.IsBackground = true;
                bgthread.Start();
            }
        }

        int LookForTheApp(string appName, RegistryKey baseKey, RegistryKey rk, string registryKeyPath, string nameField)
        {
            int appsRemoved = 0;
            string R_p;
            string keyPath;

            if (rk != null)
            {
                try
                {
                    foreach (string subKeyName in rk.GetSubKeyNames())
                    {
                        using (RegistryKey tempKey = rk.OpenSubKey(subKeyName))
                        {
                            if (tempKey != null && tempKey.GetValue(nameField) != null)
                            {
                                Debug.WriteLine(tempKey.GetValue(nameField));

                                R_p = tempKey.GetValue(nameField).ToString();
                                if (R_p == appName)
                                {
                                    keyPath = registryKeyPath + subKeyName;
                                    baseKey.DeleteSubKeyTree(keyPath);
                                    baseKey.Close();

                                    foreach (DataGridViewRow dtRow in dgvInstalledApps.Rows)
                                    {
                                        if (dtRow.Cells[dtInstalledApps_Name].Value.ToString().Equals(appName))
                                        {
                                            dgvInstalledApps.Rows.Remove(dtRow);
                                            break;
                                        }
                                    }

                                    appsRemoved++;
                                    break;
                                }
                            }
                        }

                    }
                }
                catch
                {
                }
            }
            return appsRemoved;
        }

        void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String searchStr = txtSearch.Text.Trim();
            dgvInstalledApps.Rows.Clear();

            Graphics g;

            foreach (DataRow row in dtInstalledApps.Rows)
            {
                try
                {
                    if ((Boolean.TrueString.Equals(row[dtInstalledApps_IsUpdate].ToString())))
                    {
                        continue;
                    }
                    if (row[dtInstalledApps_Name].ToString().ToLower().Contains(searchStr.ToLower()))
                    {
                        String iconFile = row[dtInstalledApps_ImgInd].ToString();
                        Icon newIco = Resource.install;
                        if (String.IsNullOrEmpty(iconFile))
                        {
                            newIco = Resource.install;
                        }
                        else
                        {
                            try
                            {
                                Bitmap tmpImg = Icon.ExtractAssociatedIcon(iconFile).ToBitmap();
                                Bitmap result = new Bitmap(16, 16);
                                using (g = Graphics.FromImage(result))
                                {
                                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                                    g.DrawImage(tmpImg, 0, 0, 16, 16);
                                }
                                newIco = Icon.FromHandle(result.GetHicon());
                            }
                            catch { }
                        }
                        dgvInstalledApps.Rows.Add(
                            new object[]
								{
									false,									
									newIco,
									row[dtInstalledApps_Name].ToString(),
									row[dtInstalledApps_Publisher].ToString(),
									row[dtInstalledApps_InstallDate].ToString()
								});
                    }
                }
                catch
                {
                }
            }
        }

        void dgvInstalledApps_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvInstalledApps.Rows[e.RowIndex].Height = 20;
            dgvInstalledApps.Rows[e.RowIndex].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        }

        void mnuRefreshApps_Click(object sender, EventArgs e)
        {
            try
            {
                GetPrograms();
                txtCommandLine.Clear();
                removeEntryToolStripMenuItem.Enabled = false;
                uninstallThisProgramToolStripMenuItem.Enabled = false;
            }
            catch (Exception ex)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        void dgvInstalledApps_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                uninstallThisProgramToolStripMenuItem.Enabled = true;
                removeEntryToolStripMenuItem.Enabled = true;

                if (e.ColumnIndex == dgvInstalledApps_CheckBox)
                {
                    dgvInstalledApps[e.ColumnIndex, e.RowIndex].Value =
                        !Convert.ToBoolean(dgvInstalledApps[e.ColumnIndex, e.RowIndex].Value);
                    dgvInstalledApps.UpdateCellValue(e.ColumnIndex, e.RowIndex);
                    dgvInstalledApps.EndEdit();
                }
            }
            catch { }
        }

        void showUpdatesChk_CheckedChanged(object sender, EventArgs e)
        {
            List<InstalledProgram> lstInstalledPrograms = InstalledProgram.GetInstalledPrograms(String.Empty, showUpdatesChk.Checked);

            CreateInstalledAppTableTable(lstInstalledPrograms);
            dgvInstalledApps.Sort(dgvInstalledApps.Columns[dgvInstalledApps_Name], ListSortDirection.Ascending);
            dgvInstalledApps.ClearSelection();
            txtCommandLine.Clear();
            removeEntryToolStripMenuItem.Enabled = false;
            uninstallThisProgramToolStripMenuItem.Enabled = false;
        }

        void dgvInstalledApps_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            removeEntryToolStripMenuItem.Enabled = true;
            uninstallThisProgramToolStripMenuItem.Enabled = true;
        }

        void ІetСulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;

            batchUninstallToolStripMenuItem.Text = rm.GetString("batch_uninstall");
            uninstallThisProgramToolStripMenuItem.Text = rm.GetString("uninstall_program");
            removeEntryToolStripMenuItem.Text = rm.GetString("remove_entry");
            mnuRefreshApps.Text = rm.GetString("refresh");
            lblCommand.Text = rm.GetString("command_line") + ":";
            saveFD.Title = rm.GetString("save");
            lblPrograms.Text = rm.GetString("current_installed_progs") + ":";
            lblSearch.Text = rm.GetString("search_program") + " :";
            showUpdatesChk.Text = rm.GetString("show_win_updates");
            colName.HeaderText = rm.GetString("name");
            colPublisher.HeaderText = rm.GetString("publisher");
            colInstalledOn.HeaderText = rm.GetString("installed_on");
            colSize.HeaderText = rm.GetString("size");
            colVersion.HeaderText = rm.GetString("version");
            regKey.HeaderText = rm.GetString("reg_key");
            Text = rm.GetString("uninstall_manager");

            ucTop.Text = rm.GetString("uninstall_manager");
        }
    }

    /// <summary>
    /// Compares install dates
    /// </summary>
    public class InstallDateComparer : IComparer
    {
        /// <summary>
        /// Sort order modifier
        /// </summary>
        public static int sortOrderModifier = -1; //Asc

        /// <summary>
        /// InstallDateComparer constructor
        /// </summary>
        /// <param name="sortOrder"></param>
        public InstallDateComparer(SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Descending:
                    sortOrderModifier = -1;
                    break;
                case SortOrder.Ascending:
                    sortOrderModifier = 1;
                    break;
            }
        }

        #region IComparer Members

        /// <summary>
        /// Compares install dates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            var row1 = (DataGridViewRow)x;
            var row2 = (DataGridViewRow)y;

            // InstallDate : index==4
            int order;
            DateTime time1 = new DateTime(1, 1, 1), time2 = new DateTime(1, 1, 1);
            String s1 = row1.Cells[4].Value.ToString();
            String s2 = row2.Cells[4].Value.ToString();
            if (s1.Length <= 0)
                order = -1;
            else if (s2.Length <= 0)
                order = 1;
            else
            {
                time1 = DateTime.ParseExact(s1, CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, null);
                time2 = DateTime.ParseExact(s2, CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, null);
                order = time1.CompareTo(time2);
            }
            return order * sortOrderModifier;
        }

        #endregion

        /// <summary>
        /// Order
        /// </summary>
        public void Order()
        {
            if (sortOrderModifier == 1)
                sortOrderModifier = -1;
            else if (sortOrderModifier == -1)
                sortOrderModifier = 1;
        }
    }

    /// <summary>
    /// Compares install sizes
    /// </summary>
    public class InstallSizeComparer : IComparer
    {
        /// <summary>
        /// Sort order modifier
        /// </summary>
        public static int SortOrderModifier = -1; //Desc

        /// <summary>
        /// Compares install sizes
        /// </summary>
        /// <param name="sortOrder"></param>
        public InstallSizeComparer(SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Descending)
            {
                SortOrderModifier = -1;
            }
            else if (sortOrder == SortOrder.Ascending)
            {
                SortOrderModifier = 1;
            }
        }

        #region IComparer Members

        /// <summary>
        /// Compares install sizes
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            var row1 = (DataGridViewRow)x;
            var row2 = (DataGridViewRow)y;

            // InstallSize : index==5
            int order;
            String s1 = row1.Cells[5].Value != null ? Regex.Replace(row1.Cells[5].Value.ToString(), "MB", String.Empty).Trim() : "0";
            String s2 = row2.Cells[5].Value != null ? Regex.Replace(row2.Cells[5].Value.ToString(), "MB", String.Empty).Trim() : "0";
            if (s1.Length <= 0)
                order = -1;
            else if (s2.Length <= 0)
                order = 1;
            else
            {
                double size1 = Convert.ToDouble(s1);
                double size2 = Convert.ToDouble(s2);
                order = size1.CompareTo(size2);
            }
            return order * SortOrderModifier;
        }

        #endregion

        /// <summary>
        /// Order
        /// </summary>
        public void Order()
        {
            if (SortOrderModifier == 1)
                SortOrderModifier = -1;
            else if (SortOrderModifier == -1)
                SortOrderModifier = 1;
        }
    }
}