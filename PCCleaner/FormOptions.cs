using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;
using FreemiumUtil;
using FreemiumUtilities.Infrastructure;
using FreemiumUtilities.RegCleaner;
using FreemiumUtilities.ShortcutFixer;
using FreemiumUtilities.StartupManager;
using FreemiumUtilities.TempCleaner;
using PCCleaner.ViewModels;
using Microsoft.Win32;
using WPFLocalizeExtension.Engine;
using Application = System.Windows.Application;
using PCCleaner.Routine;

namespace PCCleaner
{
    /// <summary>
    /// Provides a tabbed UI to manage app preferences
    /// </summary>
    public partial class FormOptions : Form
    {
		readonly Dictionary<string, int> langIndex = new Dictionary<string, int> { { "en", 0 }, { "de", 1 }, { "es", 2 }, { "pt", 3 }, { "it", 4 }, { "fr", 5 } };

        readonly Dictionary<string, int> themeIndex = new Dictionary<string, int> { { "Blue", 0 }, { "Green", 1 }, { "Red", 1 } };

        /// <summary>
        /// <c>FormOptions</c> constructor. Initializes default values.
        /// </summary>
        public FormOptions()
        {
            InitializeComponent();
            UpdateUILocalization();

            var lang = CfgFile.Get("Lang");
            if (!langIndex.ContainsKey(lang)) lang = "en";

            Languages.SelectedIndex = langIndex[lang];
            cboThemes.SelectedIndex = themeIndex[CfgFile.Get("Theme")];
            chkMinToTray.Checked = CfgFile.Get("MinimizeToTray") == "1";
            chkShowBaloon.Checked = CfgFile.Get("ShowBaloon") == "1";
            SetContextMenuOptionsFromCfg();
        }

        /// <summary>
        /// Gets the context menu options from the config file and applies it to the system
        /// </summary>
        void SetContextMenuOptionsFromCfg()
        {
            clbContextMenuOptions.SetItemChecked(0, CfgFile.Get("ShowContextMenuFindEmptyFolders") == "1");            
        }

        /// <summary>
        /// Applies localized strings to the UI
        /// </summary>
        void UpdateUILocalization()
        {
            clbContextMenuOptions.Items.Clear();
            clbContextMenuOptions.Items.Add(WPFLocalizeExtensionHelpers.GetUIString("FindEmptyFolders"));            
            SetContextMenuOptionsFromCfg();

            this.Text = WPFLocalizeExtensionHelpers.GetUIString("Options");
            Language.Text = WPFLocalizeExtensionHelpers.GetUIString("Language");
            Theme.Text = WPFLocalizeExtensionHelpers.GetUIString("Theme");
            chkMinToTray.Text = WPFLocalizeExtensionHelpers.GetUIString("MinimiseToTrayCheckboxText");
            chkShowBaloon.Text = WPFLocalizeExtensionHelpers.GetUIString("ShowBaloonCheckBoxText");
            contextMenu.Text = WPFLocalizeExtensionHelpers.GetUIString("contextMenu");
            SelectContextMenus.Text = WPFLocalizeExtensionHelpers.GetUIString("SelectContextMenus");
            btnOK.Text = WPFLocalizeExtensionHelpers.GetUIString("OK");
        }

        /// <summary>
        /// Fires when a new language is selected and applies it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Languages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Languages.SelectedIndex == 0)
            {
                LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo("en");
                UpdateUILocalization();
                CfgFile.Set("Lang", "en");
            }
			if (Languages.SelectedIndex == 1)
			{
				LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo("de");
				UpdateUILocalization();
				CfgFile.Set("Lang", "de");
			}
			if (Languages.SelectedIndex == 2)
			{
				LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo("es");
				UpdateUILocalization();
				CfgFile.Set("Lang", "es");
			}
			if (Languages.SelectedIndex == 3)
			{
				LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo("pt");
				UpdateUILocalization();
				CfgFile.Set("Lang", "pt");
			}
			if (Languages.SelectedIndex == 4)
			{
				LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo("it");
				UpdateUILocalization();
				CfgFile.Set("Lang", "it");
			}
			if (Languages.SelectedIndex == 5)
			{
				LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo("fr");
				UpdateUILocalization();
				CfgFile.Set("Lang", "fr");
			}

            OneClickAppsViewModel oneClickAppsViewModel = (OneClickAppsViewModel)Application.Current.MainWindow.DataContext;
            foreach (OneClickAppViewModel item in oneClickAppsViewModel.OneClickApps)
            {
                Type currentAppInstanceType = item.Instance.GetType();
                if (currentAppInstanceType == typeof(RegistryCleanerApp))
                {
                    item.Title = WPFLocalizeExtensionHelpers.GetUIString("RegistryCleaner");
                    item.Description = WPFLocalizeExtensionHelpers.GetUIString("RegistryCleanerOneClickDesc");
                }
                if (currentAppInstanceType == typeof(TempCleanerApp))
                {
                    item.Title = WPFLocalizeExtensionHelpers.GetUIString("TemporaryFilesCleaner");
                    item.Description = WPFLocalizeExtensionHelpers.GetUIString("TemporaryFilesCleanerDesc");
                }                
                if (currentAppInstanceType == typeof(ShortcutFixerApp))
                {
                    item.Title = WPFLocalizeExtensionHelpers.GetUIString("ShortcutFixer");
                    item.Description = WPFLocalizeExtensionHelpers.GetUIString("ShortcutFixerOneClickDesc");
                }
                if (currentAppInstanceType == typeof(StartupManagerApp))
                {
                    item.Title = WPFLocalizeExtensionHelpers.GetUIString("StartupManager");
                    item.Description = WPFLocalizeExtensionHelpers.GetUIString("StartupManagerOneClickDesc");
                }
           
                if (item.StatusText.IndexOf("\\") != -1)
                {
                    item.StatusText = item.StatusText;
                }
                else
                {
                    switch (item.StatusTextKey)
                    {
                        case "Recoverable":
                            {
                                item.StatusText = item.StatusText.Split(' ')[0] + " " + WPFLocalizeExtensionHelpers.GetUIString("Recoverable");
                                break;
                            }
                        case "ProblemsFound":
                            {
                                item.StatusText = item.StatusText.Split(' ')[0] + " " + WPFLocalizeExtensionHelpers.GetUIString("ProblemsFound");
                                break;
                            }
                        case "Recovered":
                            {
                                item.StatusText = item.StatusText.Split(' ')[0] + " " + WPFLocalizeExtensionHelpers.GetUIString("Recovered");
                                break;
                            }
                        case "ProblemsFixed":
                            {
                                item.StatusText = item.StatusText.Split(' ')[0] + " " + WPFLocalizeExtensionHelpers.GetUIString("ProblemsFixed");
                                break;
                            }
                        case "Analyzing":
                            {
                                item.StatusText = WPFLocalizeExtensionHelpers.GetUIString("Analyzing");
                                break;
                            }
                        case "NoProblemsFound":
                            {
                                item.StatusText = WPFLocalizeExtensionHelpers.GetUIString("NoProblemsFound");
                                break;
                            }
                        case "IssuesFound":
                            {
                                item.StatusText = WPFLocalizeExtensionHelpers.GetUIString("IssuesFound");
                                break;
                            }
                        case "":
                            {
                                item.StatusText = "";
                                break;
                            }
                        default:
                            {
                                item.StatusText = item.Description;
                                break;
                            }
                    }
                }
            }

            switch (oneClickAppsViewModel.StatusTitleKey)
            {
                case "NowScanning":
                    {
                        oneClickAppsViewModel.StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("NowScanning");
                        break;
                    }
                case "NowFixing":
                    {
                        oneClickAppsViewModel.StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("NowFixing");
                        break;
                    }
                case "ScanComplete":
                    {
                        oneClickAppsViewModel.StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("ScanComplete");
                        break;
                    }
                case "IssuesFound":
                    {
                        oneClickAppsViewModel.StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("IssuesFound");
                        oneClickAppsViewModel.StatusTextKey = "";
                        break;
                    }
                case "RepairComplete":
                    {
                        oneClickAppsViewModel.StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("RepairComplete");
                        break;
                    }
                case "Cancelling":
                    {
                        oneClickAppsViewModel.StatusTitle = WPFLocalizeExtensionHelpers.GetUIString("Cancelling");
                        break;
                    }
                default:
                    {
                        oneClickAppsViewModel.StatusTitle = string.Empty;
                        break;
                    }
            }

            switch (oneClickAppsViewModel.StatusTextKey)
            {
                case "NoProblemsFound":
                    {
                        oneClickAppsViewModel.StatusText = WPFLocalizeExtensionHelpers.GetUIString("NoProblemsFound");
                        break;
                    }
                case "IssuesFound":
                    {
                        oneClickAppsViewModel.StatusText = WPFLocalizeExtensionHelpers.GetUIString("IssuesFound");
                        break;
                    }
                default:
                    {
                        oneClickAppsViewModel.StatusText = "";
                        break;
                    }
            }

            // Create task manager form to get current task localized description
            FormTaskManager taskManager = new FormTaskManager();
            taskManager.btnOK_Click(null, null);
        }

        /// <summary>
        /// Fires when a new theme is selected and applies it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboThemes.SelectedIndex == -1) return;
            string path = String.Format(@"Themes\{0}\Theme.xaml", cboThemes.Text);
            using (var fs = new FileStream(FileRW.GetAssemblyDirectory() + path, FileMode.Open))
            {
                ResourceDictionary dic = (ResourceDictionary)XamlReader.Load(fs);
                Application.Current.MainWindow.Resources.MergedDictionaries.Clear();
                Application.Current.MainWindow.Resources.MergedDictionaries.Add(dic);
            }
            CfgFile.Set("Theme", cboThemes.Text);
        }

        /// <summary>
        /// Applies all selected options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOK_Click(object sender, EventArgs e)
        {
            // ENCRYPT
            if (clbContextMenuOptions.CheckedIndices.Contains(2))
            {
                SetContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\*\shell\FreemiumEncrypt\",
                                          clbContextMenuOptions.Items[2].ToString(), "ENCRYPT");
                SetContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\*\shell\FreemiumEncrypt\",
                                          clbContextMenuOptions.Items[2].ToString(), "ENCRYPT");
                CfgFile.Set("ShowContextMenuEncrypt", "1");
            }
            else
            {
                RemoveContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\*\shell\FreemiumEncrypt\");
                RemoveContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\*\shell\FreemiumEncrypt\");
                CfgFile.Set("ShowContextMenuEncrypt", "0");
            }

            // WIPE
            if (clbContextMenuOptions.CheckedIndices.Contains(5))
            {
                SetContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\*\shell\FreemiumWipe\",
                                          clbContextMenuOptions.Items[5].ToString(), "WIPE");
                SetContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\*\shell\FreemiumWipe\",
                                          clbContextMenuOptions.Items[5].ToString(), "WIPE");
                CfgFile.Set("ShowContextMenuWipe", "1");
            }
            else
            {
                RemoveContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\*\shell\FreemiumWipe\");
                RemoveContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\*\shell\FreemiumWipe\");
                CfgFile.Set("ShowContextMenuWipe", "0");
            }

            // SPLIT
            if (clbContextMenuOptions.CheckedIndices.Contains(4))
            {
                SetContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\*\shell\FreemiumSplit\",
                                          clbContextMenuOptions.Items[4].ToString(), "SPLIT");
                SetContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\*\shell\FreemiumSplit\",
                                          clbContextMenuOptions.Items[4].ToString(), "SPLIT");
                CfgFile.Set("ShowContextMenuSplit", "1");
            }
            else
            {
                RemoveContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\*\shell\FreemiumSplit\");
                RemoveContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\*\shell\FreemiumSplit\");
                CfgFile.Set("ShowContextMenuSplit", "0");
            }

            // ANALYSE
            if (clbContextMenuOptions.CheckedIndices.Contains(0))
            {
                SetContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\Drive\shell\FreemiumAnalyze\",
                                          clbContextMenuOptions.Items[0].ToString(), "ANALYSE");
                SetContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\Drive\shell\FreemiumAnalyze\",
                                          clbContextMenuOptions.Items[0].ToString(), "ANALYSE");
                SetContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\Directory\shell\FreemiumAnalyze\",
                                          clbContextMenuOptions.Items[0].ToString(), "ANALYSE");
                SetContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\Directory\shell\FreemiumAnalyze\",
                                          clbContextMenuOptions.Items[0].ToString(), "ANALYSE");
                CfgFile.Set("ShowContextMenuAnalyze", "1");
            }
            else
            {
                RemoveContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\Drive\shell\FreemiumAnalyze\");
                RemoveContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\Drive\shell\FreemiumAnalyze\");
                RemoveContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\Directory\shell\FreemiumAnalyze\");
                RemoveContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\Directory\shell\FreemiumAnalyze\");
                CfgFile.Set("ShowContextMenuAnalyze", "0");
            }

            // EMPTYFOLDERS
            if (clbContextMenuOptions.CheckedIndices.Contains(3))
            {
                SetContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\Drive\shell\FreemiumFindEmptyFolders\",
                                          clbContextMenuOptions.Items[3].ToString(), "EMPTYFOLDERS");
                SetContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\Drive\shell\FreemiumFindEmptyFolders\",
                                          clbContextMenuOptions.Items[3].ToString(), "EMPTYFOLDERS");
                SetContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\Directory\shell\FreemiumFindEmptyFolders\",
                                          clbContextMenuOptions.Items[3].ToString(), "EMPTYFOLDERS");
                SetContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\Directory\shell\FreemiumFindEmptyFolders\",
                                          clbContextMenuOptions.Items[3].ToString(), "EMPTYFOLDERS");
                CfgFile.Set("ShowContextMenuFindEmptyFolders", "1");
            }
            else
            {
                RemoveContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\Drive\shell\FreemiumFindEmptyFolders\");
                RemoveContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\Drive\shell\FreemiumFindEmptyFolders\");
                RemoveContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\Directory\shell\FreemiumFindEmptyFolders\");
                RemoveContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\Directory\shell\FreemiumFindEmptyFolders\");
                CfgFile.Set("ShowContextMenuFindEmptyFolders", "0");
            }

            // DECRYPT
            if (clbContextMenuOptions.CheckedIndices.Contains(1))
            {
                RegistryKey contextMenuKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\.cov");
                if (contextMenuKey != null)
                {
                    contextMenuKey.SetValue("", "freemiumencfile");
                    contextMenuKey.Close();
                }
                SetContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\freemiumencfile\shell\FreemiumDecrypt\",
                                          clbContextMenuOptions.Items[1].ToString(), "DECRYPT");
                contextMenuKey = Registry.LocalMachine.CreateSubKey(@"Software\Classes\.cov");
                if (contextMenuKey != null)
                {
                    contextMenuKey.SetValue("", "freemiumencfile");
                    contextMenuKey.Close();
                }
                SetContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\freemiumencfile\shell\FreemiumDecrypt\",
                                          clbContextMenuOptions.Items[1].ToString(), "DECRYPT");
                CfgFile.Set("ShowContextMenuDecrypt", "1");
            }
            else
            {
                RemoveContextMenuRegistryValue(Registry.CurrentUser, @"Software\Classes\.cov", "");
                RemoveContextMenuRegistryKey(Registry.CurrentUser, @"Software\Classes\freemiumencfile\");
                RemoveContextMenuRegistryValue(Registry.LocalMachine, @"Software\Classes\.cov", "");
                RemoveContextMenuRegistryKey(Registry.LocalMachine, @"SOFTWARE\Classes\freemiumencfile\");
                CfgFile.Set("ShowContextMenuDecrypt", "0");
            }

            Hide();
        }

        /// <summary>
        /// MinimizeToTray checkbox checked state shanged event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void chkMinToTray_CheckedChanged(object sender, EventArgs e)
        {
            CfgFile.Set("MinimizeToTray", chkMinToTray.Checked == false ? "0" : "1");
        }

        /// <summary>
        /// ShowBaloon checkbox checked state shanged event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void chkShowBaloon_CheckedChanged(object sender, EventArgs e)
        {
            CfgFile.Set("ShowBaloon", chkShowBaloon.Checked == false ? "0" : "1");
        }

        #region Context menu items

        /// <summary>
        /// Sets the registry key for the selected context menu item
        /// </summary>
        /// <param name="root">Root <c>RegistryKey</c> where the context menu item's key will be stored</param>
        /// <param name="titleNode">Context menu item's registry node title</param>
        /// <param name="title">Context menu item's registry node value. The actual OS context menu text</param>
        /// <param name="knotCodeName">Knot code name</param>
        void SetContextMenuRegistryKey(RegistryKey root, string titleNode, string title, string knotCodeName)
        {
            try
            {
                using (RegistryKey contextMenuKey = root.CreateSubKey(titleNode))
                {
                    if (contextMenuKey != null)
                    {
                        contextMenuKey.SetValue("", title);
                        // Remove all existing subkeys (including the ones at another language)
                        foreach (string subkey in contextMenuKey.GetSubKeyNames())
                        {
                            contextMenuKey.DeleteSubKey(subkey);
                        }
                        using (RegistryKey registryKey = contextMenuKey.CreateSubKey("command"))
                        {
                            if (registryKey != null)
                                registryKey.SetValue("", String.Format(@"{0}\freemiumContext.exe {1} %1",
                                                         Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath),
                                                         knotCodeName));
                            contextMenuKey.Close();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Removes provided registry key from the system registry
        /// </summary>
        /// <param name="root">Root key where the removing key is located</param>
        /// <param name="subkey">Removing key title</param>
        void RemoveContextMenuRegistryKey(RegistryKey root, string subkey)
        {
            try
            {
                root.DeleteSubKeyTree(subkey);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Removes provided registry value from the system registry
        /// </summary>
        /// <param name="root">Root key where the key with the removing value is located</param>
        /// <param name="subkey">Registry subkey name</param>
        /// <param name="valueName">Registry key value to delete</param>
        void RemoveContextMenuRegistryValue(RegistryKey root, string subkey, string valueName)
        {
            try
            {
                using (RegistryKey openSubKey = root.OpenSubKey(subkey, true))
                {
                    if (openSubKey != null)
                        openSubKey.DeleteValue(valueName);
                }
            }
            catch
            {
            }
        }

        #endregion
    }
}