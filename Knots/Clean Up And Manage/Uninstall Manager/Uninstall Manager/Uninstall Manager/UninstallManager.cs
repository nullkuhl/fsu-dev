using System;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using TAFactory.IconPack;
using System.Globalization;
using System.Resources;
using System.Threading;
using FreemiumUtils;


namespace Uninstall_Manager
{

    public partial class UninstallManager : Form
    {
        ResourceManager rm = new ResourceManager("Uninstall_Manager.Resources", System.Reflection.Assembly.GetExecutingAssembly());

        private DataTable dtInstalledApps = new DataTable("InstalledApplications");

        private const int dtInstalledApps_ImgInd = 0;
        private const int dtInstalledApps_Name = 1;
        private const int dtInstalledApps_Publisher = 2;
        private const int dtInstalledApps_InstallDate = 3;
        private const int dtInstalledApps_Size = 4;
        private const int dtInstalledApps_Version = 5;
        private const int dtInstalledApps_IsUpdate = 6;

        private const int dgvInstalledApps_CheckBox = 0;
        private const int dgvInstalledApps_ImgInd = 1;
        private const int dgvInstalledApps_Name = 2;
        private const int dgvInstalledApps_Publisher = 3;
        private const int dgvInstalledApps_InstallDate = 4;
        private const int dgvInstalledApps_Size = 5;
        private const int dgvInstalledApps_Version = 6;

        public UninstallManager()
        {
            InitializeComponent();
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern uint ExtractIconEx(string szFileName, int nIconIndex,
           IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

        [DllImport("user32.dll", EntryPoint = "DestroyIcon", SetLastError = true)]
        private static unsafe extern int DestroyIcon(IntPtr hIcon);

        public static Icon ExtractIconFromExe(string file, bool large)
        {
            unsafe
            {
                uint readIconCount = 0;
                IntPtr[] zhDummy = new IntPtr[2] { IntPtr.Zero, IntPtr.Zero };
                IntPtr[] zhIconEx = new IntPtr[2] { IntPtr.Zero, IntPtr.Zero };

                try
                {

                    if (large)
                        readIconCount = ExtractIconEx(file, 0, zhIconEx, zhDummy, 1);
                    else
                        readIconCount = ExtractIconEx(file, 0, zhDummy, zhIconEx, 1);

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
        }
        private void UninstallManager_Load(object sender, EventArgs e)
        {
            setculture(new CultureInfo(CfgFile.Get("Lang")));

            removeEntryToolStripMenuItem.Enabled = false;
            //uninstallThisProgramToolStripMenuItem1.Enabled = false;
            uninstallThisProgramToolStripMenuItem.Enabled = false;
            mnuRefreshApps.Enabled = false;

            CreateInstalledAppTableTable();
            dgvInstalledApps.Rows[1].Selected = false;
        }

        private void CreateInstalledAppTableTable()
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

            UpdateInstalledApplications();
        }

        private void EnableUninstallCheck(bool enable)
        {
            dgvInstalledApps.Columns[dgvInstalledApps_CheckBox].Visible = enable;
        }

        private void UpdateInstalledApplications()
        {
            dtInstalledApps.Rows.Clear();

            RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\");

            foreach (string subKeyName in rk.GetSubKeyNames())
            {
                using (RegistryKey tempKey = rk.OpenSubKey(subKeyName))
                {
                    try
                    {
                        if (tempKey.ValueCount != 0)
                        {
                            string displayName = tempKey.GetValue("DisplayName").ToString();

                            if (displayName.Contains("(KB") && !showUpdatesChk.Checked)
                                continue;

                            DataRow rowInstallApps = dtInstalledApps.NewRow();

                            rowInstallApps[dtInstalledApps_Name] = displayName;
                            rowInstallApps[dtInstalledApps_Publisher] = (string)tempKey.GetValue("Publisher");

                            try
                            {
                                string installDate = tempKey.GetValue("InstallDate", "").ToString();
                                if (installDate != "")
                                    rowInstallApps[dtInstalledApps_InstallDate] = DateTime.ParseExact(installDate, "yyyyMMdd", null).ToShortDateString();

                                var size = tempKey.GetValue("EstimatedSize", "").ToString();
                                if (size != "")
                                    rowInstallApps[dtInstalledApps_Size] = (double.Parse(size) / 1024.0).ToString("0.00") + " MB";

                                var version = tempKey.GetValue("DisplayVersion", "").ToString();
                                rowInstallApps[dtInstalledApps_Version] = version;
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message);
                            }

                            string iconFile = GetIconStringFromKey(tempKey);
                            rowInstallApps[dtInstalledApps_ImgInd] = iconFile;

                            rowInstallApps["regKey"] = tempKey.Name;

                            rowInstallApps[dtInstalledApps_IsUpdate] = IsUpdate(tempKey, tempKey.GetValue("DisplayName").ToString()) ? Boolean.TrueString : Boolean.FalseString;
                            dtInstalledApps.Rows.Add(rowInstallApps);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(rm.GetString("error_occurred") + ": " + ex.Message);
                    }
                }
            }

            //UpdateGrid(showWindowsUpdateToolStripMenuItem.Checked);
            UpdateGrid(true);
            UpdateToolStrip();
            dgvInstalledApps.Sort(dgvInstalledApps.Columns[dgvInstalledApps_Name], System.ComponentModel.ListSortDirection.Ascending);
            dgvInstalledApps.ClearSelection();
        }

        private void UpdateGrid(bool showWindowsUpdate)
        {
            dgvInstalledApps.Rows.Clear();

            foreach (DataRow row in dtInstalledApps.Rows)
            {
                if (!showWindowsUpdate && (Boolean.TrueString.Equals(row[dtInstalledApps_IsUpdate].ToString())))
                {
                    continue;
                }

                String iconFile = row[dtInstalledApps_ImgInd].ToString();
                Icon newIco = Resource.install;
                if (String.IsNullOrEmpty(iconFile))
                {
                    newIco = Resource.install;
                }
                else
                {
                    Bitmap tmpImg = Icon.ExtractAssociatedIcon(iconFile).ToBitmap();
                    Bitmap result = new Bitmap(16, 16);
                    using (Graphics g = Graphics.FromImage(result))
                    {
                        //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g.DrawImage(tmpImg, 0, 0, 16, 16);

                    }
                    newIco = Icon.FromHandle(result.GetHicon());

                }
                dgvInstalledApps.Rows.Add(
                                new object[] 
                                    {
                                        false,
                                        //(String.IsNullOrEmpty(iconFile) ? Resource.install : Icon.ExtractAssociatedIcon(iconFile)), 
                                         newIco,
                                        row[dtInstalledApps_Name].ToString(), 
                                        row[dtInstalledApps_Publisher].ToString(),
                                        row[dtInstalledApps_InstallDate].ToString(),
                                        row[dtInstalledApps_Size].ToString(),
                                        row[dtInstalledApps_Version].ToString(),
                                        row["regKey"].ToString()
                                    });
                /*
                Icon ico = null; 
                if(String.IsNullOrEmpty(iconFile))
                {
                    ico = Resource.install;
                   
                }
                else
                {
                    //    ico = Icon.ExtractAssociatedIcon(iconFile);
                    //ico = ExtractIconFromExe(row[dtInstalledApps_ImgInd].ToString(), true);
                    try
                    {
                        string fname = row[dtInstalledApps_ImgInd].ToString();
                        ico = IconHelper.GetAssociatedSmallIcon(fname);
                        ico = IconHelper.ExtractIcon(fname, 4);
                        
                    }
                    catch (Exception e)
                    {
                        // MessageBox.Show(e.Message);

                    }
                }
             
                dgvInstalledApps.Rows.Add(
                    new object[] 
                          {
                                false,
                                ico,
                                row[dtInstalledApps_Name].ToString(), 
                                row[dtInstalledApps_Publisher].ToString(),
                                row[dtInstalledApps_InstallDate].ToString(),
                                row[dtInstalledApps_Size].ToString(),
                                row[dtInstalledApps_Version].ToString(),
                          });*/
            }
        }

        private bool IsUpdate(RegistryKey tempKey, string displayName)
        {
            String regValue = String.Empty;
            bool isUpdate = false;

            if (tempKey.GetValue("ReleaseType") != null)
            {
                regValue = tempKey.GetValue("ReleaseType").ToString();
            }

            isUpdate = (regValue.ToLower().Contains("update") || regValue.ToLower().Contains("hotfix"));

            if (!isUpdate)
            {
                regValue = String.Empty;

                if (tempKey.GetValue("ParentKeyName") != null)
                {
                    regValue = tempKey.GetValue("ParentKeyName").ToString();
                }
                if (!String.IsNullOrEmpty(regValue) && (regValue.ToLower().Contains("operatingsystem")))
                    return true;

                if (tempKey.GetValue("ParentDisplayName") != null)
                {
                    regValue = tempKey.GetValue("ParentDisplayName").ToString();
                }
                if (!String.IsNullOrEmpty(regValue) && (regValue.ToLower().Contains("updates")))
                    return true;

                if (displayName.ToLower().Contains("update") && displayName.ToLower().Contains("windows"))
                    return true;
            }

            return isUpdate;
        }

        private String GetIconStringFromKey(RegistryKey tempKey)
        {
            String iconFile = String.Empty;
            if ((tempKey.GetValue("DisplayIcon") != null) && !String.IsNullOrEmpty(tempKey.GetValue("DisplayIcon").ToString()))
            {
                iconFile = tempKey.GetValue("DisplayIcon").ToString();
            }

            if (String.IsNullOrEmpty(iconFile))
            {
                String tempIconFile = tempKey.GetValue("UninstallString").ToString();
                if (tempIconFile.ToLower().StartsWith("msiexec.exe"))
                {
                    RegistryKey rk = Registry.ClassesRoot.OpenSubKey(@"Msi.Package\DefaultIcon");

                    //foreach (string subKeyName in rk.GetSubKeyNames())
                    {
                        //using (RegistryKey tk = rk.OpenSubKey(subKeyName))
                        {
                            if (rk.GetValue(String.Empty) != null)
                            {
                                tempIconFile = rk.GetValue(String.Empty).ToString();
                                if (!tempIconFile.ToLower().Contains("msiexec.exe"))
                                {
                                    tempIconFile = String.Empty;
                                }
                            }
                        }
                    }
                }
                iconFile = tempIconFile;
            }

            if (iconFile.IndexOfAny(new char[] { '$' }) >= 0)
            {
                return String.Empty;
            }

            if (!String.IsNullOrEmpty(iconFile))
            {
                int commaIndex = iconFile.IndexOfAny(new char[] { ',', ';' });
                iconFile = (commaIndex < 0) ? iconFile : iconFile.Substring(0, commaIndex);

                if (iconFile.EndsWith(@"\"))
                {
                    iconFile = iconFile.Substring(0, iconFile.Length - 1);
                }

                try
                {
                    if (!File.Exists(iconFile))
                    {
                        iconFile = String.Empty;
                    }
                }
                catch
                {
                    iconFile = String.Empty;
                }
            }

            return iconFile;
        }
        private void UpdateToolStrip()
        {
            toolStripStatusLabel1.Text = rm.GetString("total") + " " + dgvInstalledApps.Rows.Count + " " + rm.GetString("programs");
        }

        private void exportApplicationListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFD.Title = rm.GetString("save");
                saveFD.Filter = "Text files|*.txt";
                if (saveFD.ShowDialog() == DialogResult.OK)
                {
                    TextWriter tw = new StreamWriter(saveFD.FileName, true);
                    tw.WriteLine(rm.GetString("report_created_on") + " \n");
                    tw.WriteLine("hi");
                    tw.WriteLine(rm.GetString("programs_installed_total") + dgvInstalledApps.Rows.Count + ") " + rm.GetString("programs"));
                    tw.WriteLine("----------------------------------------------------------------------");

                    foreach (DataGridViewRow row in dgvInstalledApps.Rows)
                    {
                        tw.WriteLine(row.Cells[dtInstalledApps_Name].Value);
                    }

                    tw.WriteLine("----------------------------------------------------------------------");
                    tw.Close();
                    MessageBox.Show(rm.GetString("saved_to") + " " + saveFD.FileName);
                }
                else
                {
                    MessageBox.Show(rm.GetString("operation_cancelled") + ".");
                }

                saveFD.Dispose();
                saveFD = null;
            }
            catch { }
        }

        private void dgvInstalledApps_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvInstalledApps.SelectedRows)
            {
                try
                {
                    row.Height = 40;
                    String appName = row.Cells["regKey"].Value.ToString();
                    RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\");

                    var tempKeyName = rk.GetSubKeyNames().First(s => (string)rk.OpenSubKey(s).Name == appName);

                    //foreach (string subKeyName in rk.GetSubKeyNames())
                    //{
                    using (RegistryKey tempKey = rk.OpenSubKey(tempKeyName))
                    {
                        try
                        {
                            //String R_p = tempKey.GetValue("DisplayName").ToString();
                            //if (R_p.Equals(appName))
                            //{
                            string str = tempKey.GetValue("UninstallString").ToString();
                            if (uninstallThisProgramToolStripMenuItem.Enabled == true)
                                txtCommandLine.Text = str.Replace("\"", "");
                            else
                            {
                                uninstallThisProgramToolStripMenuItem.Enabled = true;
                                removeEntryToolStripMenuItem.Enabled = true;
                            }
                            //uninstallThisProgramToolStripMenuItem1.Enabled = true;
                            //#Mark1  
                            // uninstallThisProgramToolStripMenuItem.Enabled = true;
                            //   removeEntryToolStripMenuItem.Enabled = true;
                            mnuRefreshApps.Enabled = true;
                            //}
                        }
                        catch { }
                    }
                    //}
                }
                catch
                {
                    txtCommandLine.Text = "";
                }

            }
        }

        private void batchUninstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool setCheckBoxState = false;
            if (dgvInstalledApps.Columns[dgvInstalledApps_CheckBox].Visible)
            {
                dgvInstalledApps.Columns[dgvInstalledApps_CheckBox].Visible = false;
                uninstallThisProgramToolStripMenuItem.Text = rm.GetString("uninstall_program");
                removeEntryToolStripMenuItem.Text = rm.GetString("remove_entry");
                //uninstallThisProgramToolStripMenuItem1.Text = "Uninstall this program";                
            }
            else
            {
                dgvInstalledApps.Columns[dgvInstalledApps_CheckBox].Visible = true;
                uninstallThisProgramToolStripMenuItem.Text = rm.GetString("uninstall_programs");
                removeEntryToolStripMenuItem.Text = rm.GetString("remove_entries");
                //uninstallThisProgramToolStripMenuItem1.Text = "Uninstall Items";
            }
            foreach (DataGridViewRow row in dgvInstalledApps.Rows)
            {
                row.Cells[dgvInstalledApps_CheckBox].Value = setCheckBoxState;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            saveFD.Filter = "Reg files|*.reg";
            if (saveFD.ShowDialog() == DialogResult.OK)
            {
                string strCmdLine;

                strCmdLine = "/C regedit.exe /e " + saveFD.FileName + " HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
                Process ps = new Process();
                ps.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                System.Diagnostics.Process.Start("CMD.exe", strCmdLine);
                MessageBox.Show(rm.GetString("saved_to") + " " + saveFD.FileName);
            }
            else
            {
                MessageBox.Show(rm.GetString("operation_cancelled") + ".");
            }
        }

        private void showCommadLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //pnlCommandLine.Visible = showCommadLineToolStripMenuItem.Checked;
            pnlCommandLine.Visible = true;
            dgvInstalledApps.Size = pnlCommandLine.Visible ? new Size(dgvInstalledApps.Size.Width, 413) : new Size(dgvInstalledApps.Size.Width, 448);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateInstalledApplications();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void uninstallThisProgramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\");

            StringCollection selectedApps = new StringCollection();

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
                    foreach (string subKeyName in rk.GetSubKeyNames())
                    {
                        using (RegistryKey tempKey = rk.OpenSubKey(subKeyName))
                        {
                            String R_p = String.Empty;
                            String uninstallString = String.Empty;

                            if (tempKey.GetValue("DisplayName") != null)
                            {
                                R_p = tempKey.GetValue("DisplayName").ToString();
                            }

                            if (R_p.Equals(app))
                            {
                                string unins = String.Empty;
                                if (tempKey.GetValue("UninstallString") != null)
                                {
                                    unins = tempKey.GetValue("UninstallString").ToString();
                                }
                                uninstallString = unins.Replace("\"", "");

                                if (unins.StartsWith("MsiExec.exe") == true)
                                {
                                    unins = unins.Replace("MsiExec.exe", "");
                                    Process p = new Process();
                                    p.StartInfo.FileName = "MsiExec.exe";
                                    p.StartInfo.Arguments = unins;
                                    p.StartInfo.UseShellExecute = false;
                                    p.StartInfo.RedirectStandardOutput = true;
                                    p.Start();
                                    p.WaitForExit();
                                }
                                else if (unins.StartsWith("msiexec") == true)
                                {
                                    unins = unins.Replace("msiexec", "");
                                    Process p = new Process();
                                    p.StartInfo.FileName = "MsiExec.exe";
                                    p.StartInfo.Arguments = unins;
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
                                        string tmp1 = unins1.Replace(@"\INSTALL.LOG", "");
                                        tmp1 = tmp1.Replace(@"\install.log", "");
                                        string tmp2 = "";
                                        string tmp3;
                                        string s = tmp1;
                                        int i = 0;

                                        while ((i = s.IndexOf(' ', i)) != -1)
                                        {
                                            tmp2 = ("" + s.Substring(i));

                                            i++;
                                        }

                                        tmp3 = tmp1.Replace(tmp2, ""); // UNWISE.EXE
                                        tmp3 = tmp3.Replace(@"/U", "");
                                        Process p = new Process();
                                        p.StartInfo.FileName = tmp3;
                                        p.Start();
                                        p.WaitForExit();
                                    }
                                    else
                                    {
                                        int exeEnds = unins1.IndexOf(".exe") + 4;
                                        String exe = unins1.Substring(0, exeEnds);
                                        String arguments = exeEnds == unins1.Length ? "" : unins1.Substring(exeEnds + 1).Trim();

                                        Process p = new Process();
                                        p.StartInfo.FileName = exe;
                                        p.StartInfo.Arguments = arguments;
                                        p.StartInfo.UseShellExecute = false;
                                        p.StartInfo.RedirectStandardOutput = true;
                                        p.Start();
                                        p.WaitForExit();
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
            }

            //removeEntryToolStripMenuItem.PerformClick();
            UpdateInstalledApplications();
            dgvInstalledApps.Sort(dgvInstalledApps.Columns["colName"], System.ComponentModel.ListSortDirection.Ascending);
            dgvInstalledApps.Rows[1].Selected = false;
            txtCommandLine.Clear();
        }


        //private void uninstallThisProgramToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //if (c_flag == 0)
        //{
        //    string unins = textBox1.Text;

        //    if (unins.StartsWith("MsiExec.exe") == true)
        //    {
        //        MessageBox.Show("");
        //        unins = unins.Replace("MsiExec.exe", "");

        //        Process p = new Process();
        //        p.StartInfo.FileName = "MsiExec.exe";
        //        p.StartInfo.Arguments = unins;
        //        p.StartInfo.UseShellExecute = false;
        //        p.StartInfo.RedirectStandardOutput = true;
        //        p.Start();

        //    }
        //    if (unins.StartsWith("msiexec") == true)
        //    {
        //        unins = unins.Replace("msiexec", "");
        //        Process p = new Process();
        //        p.StartInfo.FileName = "MsiExec.exe";
        //        p.StartInfo.Arguments = unins;
        //        p.StartInfo.UseShellExecute = false;
        //        p.StartInfo.RedirectStandardOutput = true;
        //        p.Start();
        //    }
        //    else
        //    {

        //        // try
        //        {
        //            string unins1 = textBox1.Text;

        //            if (unins1.IndexOf(@"\INSTALL.") != -1)
        //            {
        //                string tmp1 = unins1.Replace(@"\INSTALL.LOG", "");
        //                tmp1 = tmp1.Replace(@"\install.log", "");
        //                string tmp2 = "";
        //                string tmp3;
        //                string s = tmp1;
        //                int i = 0;

        //                while ((i = s.IndexOf(' ', i)) != -1)
        //                {
        //                    tmp2 = ("" + s.Substring(i));

        //                    i++;
        //                }

        //                tmp3 = tmp1.Replace(tmp2, ""); // UNWISE.EXE
        //                tmp3 = tmp3.Replace(@"/U", "");
        //                Process.Start(tmp3);
        //            }
        //            else
        //            {
        //                Process.Start(unins1);
        //            }



        //        }
        //        // catch { }

        //    }

        //}
        //else
        //{

        //    int cds = listView2.Items.Count - 1;
        //    for (int I = 0; I <= cds; I++)
        //    {

        //        if (listView2.Items[I].Checked == true)
        //        {

        //            string product;


        //            product = listView2.Items[I].Text;

        //            RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\");

        //            foreach (string subKeyName in rk.GetSubKeyNames())
        //            {
        //                using (RegistryKey tempKey = rk.OpenSubKey(subKeyName))
        //                {
        //                    try
        //                    {
        //                        string R_p;
        //                        R_p = tempKey.GetValue("DisplayName").ToString();
        //                        if (R_p == product)
        //                        {
        //                            // MessageBox.Show("" + subKeyName);

        //                            //tempKey.DeleteSubKeyTree(product);

        //                            string str = tempKey.GetValue("UninstallString").ToString();
        //                            str = str.Replace("\"", "");
        //                            str = str.Replace("msiExec", "MsiExec");




        //                            if (str.StartsWith("MsiExec.exe") == true)
        //                            {
        //                                Process p = new Process();
        //                                p.StartInfo.FileName = "MsiExec.exe";
        //                                p.StartInfo.Arguments = str.Replace("MsiExec.exe", "");
        //                                p.StartInfo.UseShellExecute = false;
        //                                p.StartInfo.RedirectStandardOutput = true;
        //                                p.Start();
        //                                p.WaitForExit();

        //                            }
        //                            else
        //                            {

        //                                try
        //                                {
        //                                    Process p = new Process();
        //                                    p.StartInfo.FileName = str;
        //                                    p.Start();
        //                                    p.WaitForExit();

        //                                }
        //                                catch { }

        //                            }





        //                        }
        //                    }
        //                    catch { }
        //                }
        //            }
        //        }
        //    }
        //}
        //}

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                string strX = OpenFD.FileName;
                OpenFD.Filter = "Reg files|*.reg";
                if (OpenFD.ShowDialog() == DialogResult.OK)
                {
                    string strCmdLine;

                    strCmdLine = "REG IMPORT " + OpenFD.FileName;
                    Process ps = new Process();
                    ps.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    System.Diagnostics.Process.Start("CMD.exe", strCmdLine);

                    MessageBox.Show(rm.GetString("file_imported") + " ");
                }
                else
                {
                    MessageBox.Show(rm.GetString("operation_cancelled") + ".");
                }
            }
            catch { }
        }

        private void removeEntryToolStripMenuItem_Click(object sender, EventArgs e)
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

            foreach (String appName in selectedApps)
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\");
                foreach (string subKeyName in rk.GetSubKeyNames())
                {
                    using (RegistryKey tempKey = rk.OpenSubKey(subKeyName))
                    {
                        try
                        {
                            string R_p = tempKey.GetValue("DisplayName").ToString();
                            if (R_p == appName)
                            {
                                RegistryKey registrykeyHKLM = Registry.LocalMachine;

                                string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + subKeyName;
                                registrykeyHKLM.DeleteSubKey(keyPath);
                                registrykeyHKLM.Close();

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
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }

            if (appsRemoved == 1)
                MessageBox.Show(rm.GetString("1_entry_removed") + ".", rm.GetString("uninstall_manager"), MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            else
                MessageBox.Show(appsRemoved + " " + rm.GetString("entries_removed") + ".", rm.GetString("uninstall_manager"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            UpdateInstalledApplications();
        }

        private void showWindowsUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //showWindowsUpdateToolStripMenuItem.Checked = !showWindowsUpdateToolStripMenuItem.Checked;

            //UpdateGrid(showWindowsUpdateToolStripMenuItem.Checked);
            UpdateGrid(true);
        }

        private void autofixInvalidToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String searchStr = txtSearch.Text.Trim();
            dgvInstalledApps.Rows.Clear();

            foreach (DataRow row in dtInstalledApps.Rows)
            {
                //if (!showWindowsUpdateToolStripMenuItem.Checked && (Boolean.TrueString.Equals(row[dtInstalledApps_IsUpdate].ToString())))
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
                        Bitmap tmpImg = Icon.ExtractAssociatedIcon(iconFile).ToBitmap();
                        Bitmap result = new Bitmap(16, 16);
                        using (Graphics g = Graphics.FromImage(result))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                            g.DrawImage(tmpImg, 0, 0, 16, 16);

                        }
                        newIco = Icon.FromHandle(result.GetHicon());

                    }
                    dgvInstalledApps.Rows.Add(
                                    new object[] 
                                    {
                                        false,
                                        //(String.IsNullOrEmpty(iconFile) ? Resource.install : Icon.ExtractAssociatedIcon(iconFile)), 
                                         newIco,
                                        row[dtInstalledApps_Name].ToString(), 
                                        row[dtInstalledApps_Publisher].ToString(),
                                        row[dtInstalledApps_InstallDate].ToString()
                                    });
                }
            }
        }

        private void dgvInstalledApps_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvInstalledApps.Rows[e.RowIndex].Height = 20;
        }

        private void mnuRefreshApps_Click(object sender, EventArgs e)
        {
            UpdateInstalledApplications();
            dgvInstalledApps.Sort(dgvInstalledApps.Columns["colName"], System.ComponentModel.ListSortDirection.Ascending);
            dgvInstalledApps.Rows[1].Selected = false;
            txtCommandLine.Clear();
        }

        private void dgvInstalledApps_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            uninstallThisProgramToolStripMenuItem.Enabled = true;
            removeEntryToolStripMenuItem.Enabled = true;

            if (e.ColumnIndex == dgvInstalledApps_CheckBox)
            {
                dgvInstalledApps[e.ColumnIndex, e.RowIndex].Value = !Convert.ToBoolean(dgvInstalledApps[e.ColumnIndex, e.RowIndex].Value);
                dgvInstalledApps.UpdateCellValue(e.ColumnIndex, e.RowIndex);
                dgvInstalledApps.EndEdit();
            }
        }

        private void showUpdatesChk_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInstalledApplications();
            dgvInstalledApps.Sort(dgvInstalledApps.Columns["colName"], System.ComponentModel.ListSortDirection.Ascending);
            dgvInstalledApps.Rows[1].Selected = false;
            txtCommandLine.Clear();
        }

        private void dgvInstalledApps_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            removeEntryToolStripMenuItem.Enabled = true;
            uninstallThisProgramToolStripMenuItem.Enabled = true;
        }

        private void setculture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;

            this.batchUninstallToolStripMenuItem.Text = rm.GetString("batch_uninstall");
            this.uninstallThisProgramToolStripMenuItem.Text = rm.GetString("uninstall_program");
            this.removeEntryToolStripMenuItem.Text = rm.GetString("remove_entry");
            this.mnuRefreshApps.Text = rm.GetString("refresh");
            this.label1.Text = rm.GetString("command_line") + ":";
            this.saveFD.Title = rm.GetString("save");
            this.label3.Text = rm.GetString("current_installed_progs") + ":";
            this.label4.Text = rm.GetString("search_program") + " :";
            this.showUpdatesChk.Text = rm.GetString("show_win_updates");
            this.colName.HeaderText = rm.GetString("name");
            this.colPublisher.HeaderText = rm.GetString("publisher");
            this.colInstalledOn.HeaderText = rm.GetString("installed_on");
            this.colSize.HeaderText = rm.GetString("size");
            this.colVersion.HeaderText = rm.GetString("version");
            this.regKey.HeaderText = rm.GetString("reg_key");
            this.Text = rm.GetString("uninstall_manager");

            this.topControl1.Text = rm.GetString("uninstall_manager");

        }
    }
}





