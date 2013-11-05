using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FreemiumUtilities.Infrastructure;
using Microsoft.Win32;
using Knots.Security.TracksEraserCore;

namespace FreemiumUtilities.TracksEraser
{
	/// <summary>
	/// Tracks Eraser 1 Click-Maintenance application options form
	/// </summary>
	public partial class frmTrackOptions : Form
	{
		readonly string cookiePath1 = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
		readonly string cookiePath2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Cookies), "low");

		/// <summary>
		/// constructor for frmTrackOptions
		/// </summary>
		public frmTrackOptions()
		{
			InitializeComponent();
			UpdateUILocalization();
		}

		/// <summary>
		/// Applies localized strings to the UI
		/// </summary>
		public void UpdateUILocalization()
		{
			tbpIE.Text = WPFLocalizeExtensionHelpers.GetUIString("ie_cookie_mngr");
			btnIECheckInvert.Text = WPFLocalizeExtensionHelpers.GetUIString("check_invert");
			btnIECheckNone.Text = WPFLocalizeExtensionHelpers.GetUIString("check_none");
			btnIECheckAll.Text = WPFLocalizeExtensionHelpers.GetUIString("check_all");
			btnDeleteCookies.Text = WPFLocalizeExtensionHelpers.GetUIString("del_cookies");
			Websites.Text = WPFLocalizeExtensionHelpers.GetUIString("cookies");
			lblIECookies.Text = WPFLocalizeExtensionHelpers.GetUIString("check_mark_cookies");
			tbpFF.Text = WPFLocalizeExtensionHelpers.GetUIString("ff_cookie_manager");
			btnFFCheckInvert.Text = WPFLocalizeExtensionHelpers.GetUIString("check_invert");
			btnFFCheckNone.Text = WPFLocalizeExtensionHelpers.GetUIString("check_none");
			btnFFCheckAll.Text = WPFLocalizeExtensionHelpers.GetUIString("check_all");
			lblFFCookies.Text = WPFLocalizeExtensionHelpers.GetUIString("check_mark_cookies_ff");
			btnDelFFCookies.Text = WPFLocalizeExtensionHelpers.GetUIString("del_cookies");
			clhCookies.Text = WPFLocalizeExtensionHelpers.GetUIString("cookies");
			tbpChrome.Text = WPFLocalizeExtensionHelpers.GetUIString("google_cookie_manager");
			btnChrCheckInvert.Text = WPFLocalizeExtensionHelpers.GetUIString("check_invert");
			btnChrCheckNone.Text = WPFLocalizeExtensionHelpers.GetUIString("check_none");
			btnChrCheckAll.Text = WPFLocalizeExtensionHelpers.GetUIString("check_all");
			lblChromeCookies.Text = WPFLocalizeExtensionHelpers.GetUIString("check_mark_cookies_google");
			btnDelChromeCookies.Text = WPFLocalizeExtensionHelpers.GetUIString("del_cookies");
			clhCookiesTemp.Text = WPFLocalizeExtensionHelpers.GetUIString("cookies");
			tbpIEURLs.Text = WPFLocalizeExtensionHelpers.GetUIString("ie_typed_urls");
			btnUrlCheckInvert.Text = WPFLocalizeExtensionHelpers.GetUIString("check_invert");
			btnUrlCheckNone.Text = WPFLocalizeExtensionHelpers.GetUIString("check_none");
			btnUrlCheckAll.Text = WPFLocalizeExtensionHelpers.GetUIString("check_all");
			btnDeleteCheckedURLs.Text = WPFLocalizeExtensionHelpers.GetUIString("del_urls");
			URLs.Text = WPFLocalizeExtensionHelpers.GetUIString("urls");
			lblCheck.Text = WPFLocalizeExtensionHelpers.GetUIString("check_mark_urls_ie");
			btnCancel.Text = WPFLocalizeExtensionHelpers.GetUIString("Cancel");
			btnClose.Text = WPFLocalizeExtensionHelpers.GetUIString("OK");
			Text = WPFLocalizeExtensionHelpers.GetUIString("Options");
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
		void btnDeleteCookies_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("CloseIE"),
			                                  WPFLocalizeExtensionHelpers.GetUIString("warning"), MessageBoxButtons.YesNo,
			                                  MessageBoxIcon.Warning);

			if (dr == DialogResult.Yes)
			{
                for (int i = 0; i < lsvIECookies.Items.Count; i++)
                {
                    Application.DoEvents();
                    if (lsvIECookies.Items[i].Checked)
                    {
                        try
                        {
                            var fileInfo = new FileInfo(lsvIECookies.Items[i].Tag.ToString());
                            fileInfo.Delete();
                            //listView1.Items[i].Remove();
                        }
                        catch
                        {
                            //  MessageBox.Show(ex.Message);
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
		void btnDeleteCheckedURLs_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (ListViewItem item in lvIETypedURLs.CheckedItems)
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
				lvIETypedURLs.Items.Clear();
				using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\TypedURLs", true))
				{
					foreach (string name in rk.GetValueNames())
					{
						lvIETypedURLs.Items.Add(rk.GetValue(name).ToString());
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
			// Always select first tab to prevent showing FF/IE/Chrome tabs without check if the browser runned
			tbcMain.SelectedIndex = 0;

			UpdateUILocalization();

			new List<string>();
			new List<string>();

			//GetCookies();

			new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Cookies));
			string[] files;
			try
			{
				files = Directory.GetFiles(cookiePath1, "*.txt");
				lsvIECookies.Items.Clear();

				foreach (string sFile in files)
				{
					var li = new ListViewItem();
					var fi = new FileInfo(sFile);
					var cView = new CCookieView(sFile);
					//Add item to listview
					li.Text = cView.Domain;
					li.Tag = fi.FullName;
					li.SubItems.Add(Environment.UserName);
					li.SubItems.Add(fi.Length.ToString());
					li.SubItems.Add(cView.Secure);
					li.SubItems.Add(fi.CreationTime.ToString());
					li.SubItems.Add(fi.Name);
					li.ImageIndex = 0;
					lsvIECookies.Items.Add(li);
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
					var cView = new CCookieView(sFile);
					//Add item to listview
					li.Text = cView.Domain;
					li.Tag = fi.FullName;
					li.SubItems.Add(Environment.UserName);
					li.SubItems.Add(fi.Length.ToString());
					li.SubItems.Add(cView.Secure);
					li.SubItems.Add(fi.CreationTime.ToString());
					li.SubItems.Add(fi.Name);
					li.ImageIndex = 0;
					lsvIECookies.Items.Add(li);
				}
			}
			catch
			{
			}
			foreach (ListViewItem item in lsvIECookies.Items)
				item.Checked = true;
			LoadIEURLs();
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
					List<CookieData> cookies = Browser.GetCookies(BrowserType.FireFox);
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
					MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("mozilla_firefox_running"),
					                WPFLocalizeExtensionHelpers.GetUIString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception)
			{
				MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("cookie_info_locked_ff"),
				                WPFLocalizeExtensionHelpers.GetUIString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
				tbcMain.SelectedIndex = 0;
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
					List<CookieData> cookies = Browser.GetCookies(BrowserType.Chrome);
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
					MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("google_chrome_running"),
					                WPFLocalizeExtensionHelpers.GetUIString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception)
			{
				MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("cookie_info_locked_google"),
				                WPFLocalizeExtensionHelpers.GetUIString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
				tbcMain.SelectedIndex = 0;
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
					if (s1.Any(s => s.ToLower().Contains(browser)))
					{
						return true;
					}
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
					if (s2.Any(s => s.ToLower().Contains(browser)))
					{
						return true;
					}
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
                        Browser.DeleteCookie(BrowserType.FireFox, item.Tag);
                        ListViewFF.Items.Remove(item);
                    }

					LoadFFCookies();
				}
				else
				{
					MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("mozilla_firefox_running"),
					                WPFLocalizeExtensionHelpers.GetUIString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
							Browser.DeleteCookie(BrowserType.Chrome, ListViewChrome.Items[i].Tag);
							ListViewChrome.Items[i].Remove();
						}
					}
					LoadChromeCookies();
				}
				else
				{
					MessageBox.Show(WPFLocalizeExtensionHelpers.GetUIString("google_chrome_running"),
					                WPFLocalizeExtensionHelpers.GetUIString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
		void tbcMain_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tbcMain.SelectedIndex == 1 && ListViewFF.Items.Count == 0)
			{
				LoadFFCookies();
			}
			if (tbcMain.SelectedIndex == 2 && ListViewChrome.Items.Count == 0)
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
			foreach (ListViewItem item in lsvIECookies.Items)
				item.Checked = true;
		}

		/// <summary>
		/// handle click event to check none items
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnCheckNone_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in lsvIECookies.Items)
				item.Checked = false;
		}

		/// <summary>
		/// handle click event to invert checked items
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnCheckInvert_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in lsvIECookies.Items)
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
			foreach (ListViewItem item in lvIETypedURLs.Items)
				item.Checked = true;
		}

		/// <summary>
		/// handle click event to check none items
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnUrlCheckNone_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in lvIETypedURLs.Items)
				item.Checked = false;
		}

		/// <summary>
		/// handle click event to invert checked items
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnUrlCheckInvert_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in lvIETypedURLs.Items)
				item.Checked = !item.Checked;
		}
	}
}