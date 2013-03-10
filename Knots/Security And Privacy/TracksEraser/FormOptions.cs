using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using Microsoft.Win32;

namespace FreemiumUtilities.TracksEraser
{
    /// <summary>
    /// Options form of the Tracks Eraser knot
    /// </summary>
    public partial class frmOptions : Form
    {
        readonly string cookiePath1 = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
        readonly string cookiePath2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Cookies), "low");

        /// <summary>
        /// constructor for FrmOptions
        /// </summary>
        public frmOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// handle click event to close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// handle click event to delete cookies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnDeleteIECookies_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(rm.GetString("CloseIE"),
                                              rm.GetString("warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                for (int i = 0; i < lvCookiesIE.Items.Count; i++)
                {
                    Application.DoEvents();
                    if (lvCookiesIE.Items[i].Checked)
                    {
                        try
                        {
                            var fileInfo = new FileInfo(lvCookiesIE.Items[i].Tag.ToString());
                            fileInfo.Delete();
                            //listView1.Items[i].Remove();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                FrmOptions_Load(null, null);
            }
        }

        /// <summary>
        /// handle click event to delete history
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnDeleteIEURLs_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem item in lvIEURLs.CheckedItems)
                {
                    Application.DoEvents();
                    using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\TypedURLs", true))
                    {
                        foreach (string name in rk.GetValueNames())
                        {
                            string value = rk.GetValue(name).ToString();
                            if (item.Text == value)
                            {
                                rk.DeleteValue(name);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            LoadIEURLs();
        }

        /// <summary>
        /// load internet explorer typed url
        /// </summary>
        void LoadIEURLs()
        {
            try
            {
                lvIEURLs.Items.Clear();
                using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\TypedURLs", true))
                {
                    foreach (string name in rk.GetValueNames())
                    {
                        lvIEURLs.Items.Add(rk.GetValue(name).ToString());
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// load internet explorer cookies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmOptions_Load(object sender, EventArgs e)
        {
            SetCulture(new CultureInfo(CfgFile.Get("Lang")));

            //GetCookies();

            string[] files;
            try
            {
                files = Directory.GetFiles(cookiePath1, "*.txt");
                lvCookiesIE.Items.Clear();

                foreach (string sFile in files)
                {
                    var li = new ListViewItem();
                    var fi = new FileInfo(sFile);
                    var cView = new cCookieView(sFile);
                    //Add item to listview
                    li.Text = cView.Domain;
                    li.Tag = fi.FullName;
                    li.SubItems.Add(Environment.UserName);
                    li.SubItems.Add(fi.Length.ToString());
                    li.SubItems.Add(cView.Secure);
                    li.SubItems.Add(fi.CreationTime.ToString());
                    li.SubItems.Add(fi.Name);
                    li.ImageIndex = 0;
                    lvCookiesIE.Items.Add(li);
                }
            }
            catch
            {
            }

            try
            {
                files = Directory.GetFiles(cookiePath2, "*.txt");
                foreach (string sFile in files)
                {
                    var li = new ListViewItem();
                    var fi = new FileInfo(sFile);
                    var cView = new cCookieView(sFile);
                    //Add item to listview
                    li.Text = cView.Domain;
                    li.Tag = fi.FullName;
                    li.SubItems.Add(Environment.UserName);
                    li.SubItems.Add(fi.Length.ToString());
                    li.SubItems.Add(cView.Secure);
                    li.SubItems.Add(fi.CreationTime.ToString());
                    li.SubItems.Add(fi.Name);
                    li.ImageIndex = 0;
                    lvCookiesIE.Items.Add(li);
                }
            }
            catch
            {
            }
            foreach (ListViewItem item in lvCookiesIE.Items)
                item.Checked = true;

            LoadIEURLs();
        }

        /// <summary>
        /// change current language
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;

            tbpCookiesIE.Text = rm.GetString("ie_cookie_mngr");
            btnIECheckInvert.Text = rm.GetString("check_invert");
            btnIECheckNone.Text = rm.GetString("check_none");
            btnIECheckAll.Text = rm.GetString("check_all");
            btnDeleteIECookies.Text = rm.GetString("del_cookies");
            Websites.Text = rm.GetString("cookies");
            lblCheckIE.Text = rm.GetString("check_mark_cookies");
            tbpCookiesFF.Text = rm.GetString("ff_cookie_manager");
            btnFFCheckInvert.Text = rm.GetString("check_invert");
            btnFFCheckNone.Text = rm.GetString("check_none");
            btnFFCheckAll.Text = rm.GetString("check_all");
            lblCheckFF.Text = rm.GetString("check_mark_cookies_ff");
            btnDelFFCookies.Text = rm.GetString("del_cookies");
            clhCookies.Text = rm.GetString("cookies");
            tbpCookiesChrome.Text = rm.GetString("google_cookie_manager");
            btnChrCheckInvert.Text = rm.GetString("check_invert");
            btnChrCheckNone.Text = rm.GetString("check_none");
            btnChrCheckAll.Text = rm.GetString("check_all");
            lblCheckChrome.Text = rm.GetString("check_mark_cookies_google");
            btnDelChromeCookies.Text = rm.GetString("del_cookies");
            clhCookiesAll.Text = rm.GetString("cookies");
            tbpIEURLs.Text = rm.GetString("ie_typed_urls");
            btnUrlCheckInvert.Text = rm.GetString("check_invert");
            btnUrlCheckNone.Text = rm.GetString("check_none");
            btnUrlCheckAll.Text = rm.GetString("check_all");
            btnDeleteIEURLs.Text = rm.GetString("del_urls");
            URLs.Text = rm.GetString("urls");
            lblCheckIEURLs.Text = rm.GetString("check_mark_urls_ie");
            btnCancel.Text = rm.GetString("cancel");
            btnOK.Text = rm.GetString("ok");
            Text = rm.GetString("options");
        }

        /// <summary>
        /// load firefox cookies
        /// </summary>
        void LoadFFCookies()
        {
            try
            {
                Process[] pname = Process.GetProcessesByName("firefox");
                if (pname.Length == 0)
                {
                    if (IsBrowserInstalled("firefox") == false)
                        throw new Exception();
                    List<CookieData> cookies = FFCookieManager.GetCookies();
                    ListViewFF.Items.Clear();
                    foreach (CookieData data in cookies)
                    {
                        Application.DoEvents();
                        ListViewFF.Items.Add(data.Name + "-" + data.Host).Checked = true;
                        ListViewFF.Items[ListViewFF.Items.Count - 1].Tag = data.ID;
                    }
                }
                else
                {
                    MessageBox.Show(rm.GetString("mozilla_firefox_running"), rm.GetString("warning"), MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(rm.GetString("cookie_info_locked_ff"), rm.GetString("warning"), MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                tcMain.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// load chrome cookies
        /// </summary>
        void LoadChromeCookies()
        {
            try
            {
                Process[] pname = Process.GetProcessesByName("chrome");
                if (pname.Length == 0)
                {
                    if (IsBrowserInstalled("chrome") == false)
                        throw new Exception();
                    List<CookieData> cookies = ChromeCookieManager.GetChromeCookies();
                    ListViewChrome.Items.Clear();
                    foreach (CookieData data in cookies)
                    {
                        Application.DoEvents();
                        ListViewChrome.Items.Add(data.Name + "-" + data.Host).Checked = true;
                        ListViewChrome.Items[ListViewChrome.Items.Count - 1].Tag = data.ID;
                    }
                }
                else
                {
                    MessageBox.Show(rm.GetString("google_chrome_running"), rm.GetString("warning"), MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(rm.GetString("cookie_info_locked_google"), rm.GetString("warning"), MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                tcMain.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// check if specific browser is installed
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        bool IsBrowserInstalled(string browser)
        {
            try
            {
                RegistryKey openSubKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
                if (openSubKey != null)
                {
                    string[] s1 = openSubKey.GetSubKeyNames();
                    foreach (string s in s1)
                        if (s.ToLower().Contains(browser))
                            return true;
                }
            }
            catch
            {
            }
            try
            {
                RegistryKey openSubKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Clients\StartMenuInternet");
                if (openSubKey != null)
                {
                    string[] s2 = openSubKey.GetSubKeyNames();
                    foreach (string s in s2)
                        if (s.ToLower().Contains(browser))
                            return true;
                }
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        /// handle click event to delete firefox cookies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnDelFFCookies_Click(object sender, EventArgs e)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("firefox");
                if (processes.Length == 0)
                {
                    foreach (ListViewItem item in ListViewFF.CheckedItems)
                    {
                        Application.DoEvents();
                        FFCookieManager.DeleteCookie(item.Tag);
                        ListViewFF.Items.Remove(item);
                    }

                    LoadFFCookies();
                }
                else
                {
                    MessageBox.Show(rm.GetString("mozilla_firefox_running"), rm.GetString("warning"), MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// handle click event to delete chrome cookies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnDelChromeCookies_Click(object sender, EventArgs e)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("chrome");
                if (processes.Length == 0)
                {
                    for (int i = 0; i < ListViewChrome.Items.Count; i++)
                    {
                        Application.DoEvents();
                        if (ListViewChrome.Items[i].Checked)
                        {
                            ChromeCookieManager.DeleteCookie(ListViewChrome.Items[i].Tag);
                            ListViewChrome.Items[i].Remove();
                        }
                    }
                    LoadChromeCookies();
                }
                else
                {
                    MessageBox.Show(rm.GetString("google_chrome_running"), rm.GetString("warning"), MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// handle selected index changed event to load current tab cookies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain.SelectedIndex == 1 && ListViewFF.Items.Count == 0)
            {
                LoadFFCookies();
            }
            if (tcMain.SelectedIndex == 2 && ListViewChrome.Items.Count == 0)
            {
                LoadChromeCookies();
            }
        }

        /// <summary>
        /// handle click event to check all items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCheckAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvCookiesIE.Items)
                item.Checked = true;
        }

        /// <summary>
        /// handle click event to check none items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCheckNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvCookiesIE.Items)
                item.Checked = false;
        }

        /// <summary>
        /// handle click event to invert checked items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCheckInvert_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvCookiesIE.Items)
                item.Checked = !item.Checked;
        }

        /// <summary>
        /// handle click event to check all firefox items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnFFCheckAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewFF.Items)
                item.Checked = true;
        }

        /// <summary>
        /// handle click event to check none firefox items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnFFCheckNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewFF.Items)
                item.Checked = false;
        }

        /// <summary>
        /// handle click event to invert checked firefox items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnFFCheckInvert_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewFF.Items)
                item.Checked = !item.Checked;
        }

        /// <summary>
        /// handle click event to check all chrome items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnChrCheckAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewChrome.Items)
                item.Checked = true;
        }

        /// <summary>
        /// handle click event to check none chrome items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnChrCheckNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewChrome.Items)
                item.Checked = false;
        }

        /// <summary>
        /// handle click event to invert checked chrome items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnChrCheckInvert_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewChrome.Items)
                item.Checked = !item.Checked;
        }

        /// <summary>
        /// handle click event to check all items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnUrlCheckAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvIEURLs.Items)
                item.Checked = true;
        }

        /// <summary>
        /// handle click event to check none items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnUrlCheckNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvIEURLs.Items)
                item.Checked = false;
        }

        /// <summary>
        /// handle click event to invert checked items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnUrlCheckInvert_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvIEURLs.Items)
                item.Checked = !item.Checked;
        }
    }
}