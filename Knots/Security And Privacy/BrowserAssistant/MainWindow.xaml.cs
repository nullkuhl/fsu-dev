using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Res = BrowserAssistant.Properties.Resources;

namespace BrowserAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// constructor for MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// check for installed browsers
        /// </summary>
        void CheckBrowserAvailability()
        {
            try
            {
                FfExtTab.IsEnabled = FirefoxExtension.IsBrowserInstalled();
                bool ieExists = ExplorerExtension.IsBrowserInstalled();
                IeAxTab.IsEnabled = ieExists;
                IeBhoTab.IsEnabled = ieExists;
                IeExtTab.IsEnabled = ieExists;
                ChExtTab.IsEnabled = ChromeExtension.IsBrowserInstalled();
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
        }

        /// <summary>
        /// check if specific browser is running
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        bool CheckBrowserClosed(string browser)
        {
            if (Process.GetProcessesByName(browser).Length > 0)
            {
                MessageBox.Show(string.Format(Res.CloseBrowserError, browser), Res.Error, MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// handle GotFocus event to open firefox tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FfExtTab_Focused(object sender, RoutedEventArgs e)
        {
            if (FfExtList.Extensions != null && sender != null) return;

            if (!CheckBrowserClosed("Firefox")) return;

            try
            {
                FfExtList.Extensions = FirefoxExtension.List();
            }
            catch (FileNotFoundException)
            {
                if (FirefoxExtension.IsBrowserInstalled())
                {
                    MessageBox.Show(Res.FirstStart, Res.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    FfExtTab.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show(string.Format("Firefox {0}", Res.NotInstalledError), Res.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    FfExtTab.IsEnabled = false;
                    TabList.SelectedIndex = 0;
                }
            }
            catch (DirectoryNotFoundException)
            {
                if (FirefoxExtension.IsBrowserInstalled())
                {
                    MessageBox.Show(Res.FirstStart, Res.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    FfExtTab.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show(string.Format("Firefox {0}", Res.NotInstalledError), Res.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    FfExtTab.IsEnabled = false;
                    TabList.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// handle Click event to save firefox extensions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FfExtList_SaveClicked(object sender, RoutedEventArgs e)
        {
            if (!CheckBrowserClosed("Firefox")) return;

            try
            {
                FirefoxExtension.SaveChanges(FfExtList.Extensions.Cast<FirefoxExtension>());
                MessageBox.Show(Res.Done);
                FfExtList.SaveBtn.IsEnabled = false;
            }
            catch (Exception)
            {
            }

            FfExtTab_Focused(null, null);
        }

        /// <summary>
        /// handle GotFocus event to open chrome tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ChExtTab_Focused(object sender, RoutedEventArgs e)
        {
            if (ChExtList.Extensions != null && sender != null) return;

            if (!CheckBrowserClosed("Chrome")) return;

            try
            {
                ChExtList.Extensions = ChromeExtension.List();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(string.Format("Chrome {0}", Res.NotInstalledError), Res.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                ChExtTab.IsEnabled = false;
                TabList.SelectedIndex = 0;
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show(string.Format("Chrome {0}", Res.NotInstalledError), Res.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                ChExtTab.IsEnabled = false;
                TabList.SelectedIndex = 0;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// handle Click event to save chrome extensions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ChExtList_SaveClicked(object sender, RoutedEventArgs e)
        {
            if (!CheckBrowserClosed("Chrome")) return;

            try
            {
                ChromeExtension.SaveChanges(ChExtList.Extensions.Cast<ChromeExtension>());
                MessageBox.Show(Res.Done);
                ChExtList.SaveBtn.IsEnabled = false;
            }
            catch (Exception)
            {
            }

            ChExtTab_Focused(null, null);
        }

        /// <summary>
        /// handle GotFocus event to open intern explorer activex tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IeAxTab_Focused(object sender, RoutedEventArgs e)
        {
            if (IeAxList.ItemsSource != null && sender != null) return;

            try
            {
                IeAxList.ItemsSource = ExplorerActiveX.List();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// handle Click event to enable internet explorer activex save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IeAxReBind(object sender, RoutedEventArgs e)
        {
            IeAxSaveBtn.IsEnabled = IeAxList.ItemsSource.Cast<ExplorerActiveX>().Any(a => a.HasChanged);
        }

        /// <summary>
        /// handle Click event to save internet explorer activex
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IeAxSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExplorerActiveX.SaveChanges(IeAxList.ItemsSource.Cast<ExplorerActiveX>().Where(a => a.HasChanged));
                MessageBox.Show(Res.Done);
                IeAxSaveBtn.IsEnabled = false;
            }
            catch (Exception)
            {
            }

            IeAxTab_Focused(null, null);
        }

        /// <summary>
        /// handle GotFocus event to open internet explorer extensions tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IeExtTab_Focused(object sender, RoutedEventArgs e)
        {
            if (IeExtList.ItemsSource != null && sender != null) return;

            try
            {
                IeExtList.ItemsSource = ExplorerExtension.List();
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// handle Click event to check internet explorer extension
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IeExtCheckBox_Click(object sender, RoutedEventArgs e)
        {
            IeExtSaveBtn.IsEnabled = IeExtList.ItemsSource.Cast<ExplorerExtension>().Any(a => a.HasChanged);
        }

        /// <summary>
        /// handle Click event to save internet explorer extensions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IeExtSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExplorerExtension.SaveChanges(IeExtList.ItemsSource.Cast<ExplorerExtension>().Where(a => a.HasChanged));
                MessageBox.Show(Res.Done);
                IeExtSaveBtn.IsEnabled = false;
            }
            catch (Exception)
            {
            }

            IeExtTab_Focused(null, null);
        }

        /// <summary>
        /// handle GotFocus event to open internet explorer bho tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IeBhoTab_Focused(object sender, RoutedEventArgs e)
        {
            if (IeBhoList.ItemsSource != null && sender != null) return;

            try
            {
                IeBhoList.ItemsSource = ExplorerBHO.List();
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// handle Click event to check internet explorer bho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IeBhoCheckBox_Click(object sender, RoutedEventArgs e)
        {
            IeBhoSaveBtn.IsEnabled = IeBhoList.ItemsSource.Cast<ExplorerBHO>().Any(a => a.HasChanged);
        }

        /// <summary>
        /// handle Click event to save internet explorer bho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IeBhoSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExplorerBHO.SaveChanges(IeBhoList.ItemsSource.Cast<ExplorerBHO>().Where(a => a.HasChanged));
                MessageBox.Show(Res.Done);
                IeBhoSaveBtn.IsEnabled = false;
            }
            catch (Exception)
            { }

            IeBhoTab_Focused(null, null);
        }

        /// <summary>
        /// handle GotFocus event to open internet explorer Toolbars tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IeToolbarTab_Focused(object sender, RoutedEventArgs e)
        {
            if (IeToolbarList.ItemsSource != null && sender != null) return;

            try
            {
                IeToolbarList.ItemsSource = ExplorerToolbar.List();
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// handle Click event to check internet explorer Toolbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IeToolbarCheckBox_Click(object sender, RoutedEventArgs e)
        {
            IeToolbarSaveBtn.IsEnabled = IeToolbarList.ItemsSource.Cast<ExplorerToolbar>().Any(a => a.HasChanged);
        }

        /// <summary>
        /// handle Click event to save internet explorer toolbars
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IeToolbarSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExplorerToolbar.SaveChanges(IeToolbarList.ItemsSource.Cast<ExplorerToolbar>().Where(a => a.HasChanged));
                MessageBox.Show(Res.Done);
                IeToolbarSaveBtn.IsEnabled = false;
            }
            catch (Exception)
            { }

            IeToolbarTab_Focused(null, null);
        }

        /// <summary>
        /// handle GotFocus event to open hijack tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HijackTab_Focused(object sender, RoutedEventArgs e)
        {
            HijackList.Bind();
        }

        /// <summary>
        /// handle Click event to close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// initialize form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBrowserAvailability();
        }
    }
}