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
using FreemiumUtilities.StartupManager;
using FreemiumUtilities.TempCleaner;
using FreeGamingBooster.ViewModels;
using Microsoft.Win32;
using WPFLocalizeExtension.Engine;
using Application = System.Windows.Application;

namespace FreeGamingBooster
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
            chkMinToTray.Checked = CfgFile.Get("MinimizeToTray") == "1";
            chkShowBaloon.Checked = CfgFile.Get("ShowBaloon") == "1";
        }

        /// <summary>
        /// Applies localized strings to the UI
        /// </summary>
        void UpdateUILocalization()
        {
            this.Text = WPFLocalizeExtensionHelpers.GetUIString("Options");
            Language.Text = WPFLocalizeExtensionHelpers.GetUIString("Language");
            chkMinToTray.Text = WPFLocalizeExtensionHelpers.GetUIString("MinimiseToTrayCheckboxText");
            chkShowBaloon.Text = WPFLocalizeExtensionHelpers.GetUIString("ShowBaloonCheckBoxText");
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
        /// Applies all selected options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOK_Click(object sender, EventArgs e)
        {
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