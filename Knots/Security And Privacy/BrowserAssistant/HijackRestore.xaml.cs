using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Res = BrowserAssistant.Properties.Resources;

namespace BrowserAssistant
{
    /// <summary>
    /// Interaction logic for IeHijackRestore.xaml
    /// </summary>
    public partial class HijackRestore
    {
        /// <summary>
        /// constructor for HijackRestore
        /// </summary>
        public HijackRestore()
        {
            InitializeComponent();
        }

        /// <summary>
        /// update hijack list
        /// </summary>
        public void Bind()
        {
            if (SettingsList.ItemsSource != null) return;

            try
            {
                SettingsList.ItemsSource = List();
            }
            catch
            {
            }
        }

        /// <summary>
        /// handle Click event to check hijack item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            RestoreBtn.IsEnabled = SettingsList.ItemsSource.Cast<IHijackSetting>().Any(s => s.Restore);
        }

        /// <summary>
        /// handle Click event to restore hijack items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RestoreBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (IHijackSetting setting in SettingsList.ItemsSource.Cast<IHijackSetting>().Where(s => s.Restore))
                    setting.DoRestore();

                FfHijackSetting.Save();
                ChHijackSetting.Save();

                MessageBox.Show(Res.Done);
                RestoreBtn.IsEnabled = false;

                SettingsList.ItemsSource = List();
            }
            catch
            {
            }
        }

        /// <summary>
        /// get a list of all hijack items
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IHijackSetting> List()
        {
            IEnumerable<IHijackSetting> result = ListIe();
            try
            {
                if (result != null) result = result.Union(ListFf());
            }
            catch
            {
            }
            try
            {
                if (result != null) result = result.Union(ListCh());
            }
            catch
            {
            }

            if (result != null) return result;
            return null;
        }

        /// <summary>
        /// get a list of internet explorer hijack items
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IHijackSetting> ListIe()
        {
            return new[]
					{
						new IeHijackSetting("IE: " + Res.StartPage, "http://www.msn.com",
											@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main", "Start Page"),
						new IeHijackSetting("IE: " + Res.SearchPage, "http://www.microsoft.com/isapi/redir.dll?prd=ie&ar=iesearch",
											@"HKEY_CURRENT_USER\Soft ware\Microsoft\Internet Explorer\Main", "Search Page"),
						new IeHijackSetting("IE: " + Res.DefaultPageURL,
											"http://www.microsoft.com/isapi/redir.dll?prd=ie&pver=6&ar=msnhome",
											@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main", "Default_Page_URL"),
						new IeHijackSetting("IE: " + Res.LocalPage, @"C:\WINDOWS\system32\blank.htm",
											@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main", "Local Page"),
						new IeHijackSetting("IE: " + Res.SearchBar, "http://home.microsoft.com/search/lobby/search.asp",
											@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main", "Search Bar"),
						new IeHijackSetting("IE: " + Res.DefaultSearchURL, "http://home.microsoft.com/search/search.asp",
											@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main", "Default_Search_URL"),
						new IeHijackSetting("IE: " + Res.StartPage + " (" + Res.AllUsers + ")", "http://www.msn.com",
											@"HKEY_LOCAL_MACHINE\Software\Microsoft\Internet Explorer\Main", "Start Page"),
						new IeHijackSetting("IE: " + Res.SearchPage + " (" + Res.AllUsers + ")",
											"http://www.microsoft.com/isapi/redir.dll?prd=ie&ar=iesearch",
											@"HKEY_LOCAL_MACHINE\Software\Microsoft\Internet Explorer\Main", "Search Page"),
						new IeHijackSetting("IE: " + Res.DefaultPageURL + " (" + Res.AllUsers + ")",
											"http://www.microsoft.com/isapi/redir.dll?prd=ie&pver=6&ar=msnhome",
											@"HKEY_LOCAL_MACHINE\Software\Microsoft\Internet Explorer\Main", "Default_Page_URL"),
						new IeHijackSetting("IE: " + Res.LocalPage + " (" + Res.AllUsers + ")", @"C:\WINDOWS\system32\blank.htm",
											@"HKEY_LOCAL_MACHINE\Software\Microsoft\Internet Explorer\Main", "Local Page"),
						new IeHijackSetting("IE: " + Res.SearchBar + " (" + Res.AllUsers + ")",
											"http://home.microsoft.com/search/lobby/search.asp",
											@"HKEY_LOCAL_MACHINE\Software\Microsoft\Internet Explorer\Main", "Search Bar"),
						new IeHijackSetting("IE: " + Res.DefaultSearchURL + " (" + Res.AllUsers + ")",
											"http://home.microsoft.com/search/search.asp",
											@"HKEY_LOCAL_MACHINE\Software\Microsoft\Internet Explorer\Main", "Default_Search_URL"),
						new IeHijackSetting("IE: " + Res.CustomizeSearch,
											"http://ie.search.msn.com/{SUB_RFC1766}/srchasst/srchcust.htm",
											@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\Search", "CustomizeSearch"),
						new IeHijackSetting("IE: " + Res.SearchAssistant,
											"http://ie.search.msn.com/{SUB_RFC1766}/srchasst/srchasst.htm",
											@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\Search", "SearchAssistant"),
						new IeHijackSetting("IE: " + Res.SearchURL, "http://home.microsoft.com/access/autosearch.asp?p=%s",
											@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\SearchUrl", ""),
						new IeHijackSetting("IE: " + Res.SearchURL + " (" + Res.AllUsers + ")",
											"http://home.microsoft.com/access/autosearch.asp?p=%s",
											@"HKEY_LOCAL_MACHINE\Software\Microsoft\Internet Explorer\SearchUrl", ""),
						new IeHijackSetting("IE " + Res.AboutURL + ": " + Res.Blank, "res://mshtml.dll/blank.htm",
											@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\AboutURLs", "blank"),
						new IeHijackSetting("IE " + Res.AboutURL + ": " + Res.DesktopNavigation, "res://shdoclc.dll/navcancl.htm",
											@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\AboutURLs",
											"DesktopItemNavigationFailure"),
						new IeHijackSetting("IE " + Res.AboutURL + ": " + Res.InPrivate, "res://ieframe.dll/inprivate.htm",
											@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\AboutURLs", "InPrivate"),
						new IeHijackSetting("IE " + Res.AboutURL + ": " + Res.NavigationCanceled, "res://shdoclc.dll/navcancl.htm",
											@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\AboutURLs",
											"NavigationCanceled"),
						new IeHijackSetting("IE " + Res.AboutURL + ": " + Res.NavigationFailure, "res://shdoclc.dll/navcancl.htm",
											@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\AboutURLs",
											"NavigationFailure"),
						new IeHijackSetting("IE " + Res.AboutURL + ": " + Res.NoAddons, "res://ieframe.dll/noaddon.htm",
											@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\AboutURLs", "NoAdd-ons"),
						new IeHijackSetting("IE " + Res.AboutURL + ": " + Res.NoAddonsInfo, "res://ieframe.dll/noaddoninfo.htm",
											@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\AboutURLs", "NoAdd-onsInfo"),
						new IeHijackSetting("IE " + Res.AboutURL + ": " + Res.OfflineInformation, "res://shdoclc.dll/offcancl.htm",
											@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\AboutURLs",
											"OfflineInformation"),
						new IeHijackSetting("IE " + Res.AboutURL + ": " + Res.PostNotCached, "res://mshtml.dll/repost.htm",
											@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\AboutURLs", "PostNotCached"),
						new IeHijackSetting("IE " + Res.AboutURL + ": " + Res.SecurityRisk, "res://ieframe.dll/securityatrisk.htm",
											@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\AboutURLs", "SecurityRisk")
					};
        }

        /// <summary>
        /// get a list of firefox hijack items
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IHijackSetting> ListFf()
        {
            return new[]
					{
						new FfHijackSetting("Firefox: " + Res.HomePage, new[] {"browser.startup.homepage"},
											new[] {"http://www.google.com/firefox"}),
						new FfHijackSetting("Firefox: " + Res.SearchProvider,
											new[] {"browser.search.defaulturl", "browser.search.defaultenginename"},
											new[] {"http://www.google.com/search?&q=", "Google"}),
						new FfHijackSetting("Firefox: Keyword Search URL", new[] {"keyword.URL"},
											new[] {"http://www.google.com/search?&q="})
					};
        }

        /// <summary>
        /// get a list of chrome hijack items
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IHijackSetting> ListCh()
        {
            return new[]
					{
						new ChHijackSetting("Chrome: " + Res.HomePage, "homepage", null, "\"http://www.google.com\""),
						new ChHijackSetting("Chrome: " + Res.SearchProvider, "default_search_provider", "keyword",
											@"{
					  ""enabled"": true,
					  ""encodings"": ""UTF-8"",
					  ""icon_url"": ""http://www.google.com/favicon.ico"",
					  ""id"": ""2"",
					  ""instant_url"": ""{google:baseURL}webhp?{google:RLZ}sourceid=chrome-instant&ie={inputEncoding}&ion=1{searchTerms}&nord=1"",
					  ""keyword"": ""google.com"",
					  ""name"": ""Google"",
					  ""prepopulate_id"": ""1"",
					  ""search_url"": ""{google:baseURL}search?{google:RLZ}{google:acceptedSuggestion}{google:originalQueryForSuggestion}sourceid=chrome&ie={inputEncoding}&q={searchTerms}"",
					  ""suggest_url"": ""{google:baseSuggestURL}search?client=chrome&hl={language}&q={searchTerms}""
				   }")
					};
        }

        /// <summary>
        /// handle Click event to close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}