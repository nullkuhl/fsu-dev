using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using FreemiumUtilities.Infrastructure;
using RegistryCleanerCore;

namespace FreemiumUtilities.RegCleaner
{
    /// <summary>
    /// RegCleaner 1 Click-Maintenance application main form
    /// </summary>
    public partial class FormRegCleaner : Form
    {
        ObservableCollection<ScanData> problems;

        /// <summary>
        /// Registry cleaner form constructor
        /// </summary>
        public FormRegCleaner()
        {
            InitializeComponent();
            UpdateUILocalization();
        }

        /// <summary>
        /// A collection of the found registry problems
        /// </summary>
        public ObservableCollection<ScanData> Problems
        {
            get { return problems; }
            set
            {
                problems = value;

                if (badRegLst.InvokeRequired)
                {
                }
                else
                {
                    badRegLst.Items.Clear();
                    foreach (ScanData problem in value)
                    {
                        ListViewItem listItem = new ListViewItem(new[]
						                                	{
						                                		problem.Key,
						                                		WPFLocalizeExtensionHelpers.GetUIString("EntryPointsTo") + " " + problem.Name
						                                	});
                        badRegLst.Items.Add(listItem);
                    }
                }
            }
        }

        /// <summary>
        /// Applies localized strings to the UI
        /// </summary>
        void UpdateUILocalization()
        {
            Text = WPFLocalizeExtensionHelpers.GetUIString("RegistryCleaner");
            lblRegKeys.Text = WPFLocalizeExtensionHelpers.GetUIString("RegistryKeysToDeleteText");
            colProblem.Text = WPFLocalizeExtensionHelpers.GetUIString("ColumnProblem");
            colRegKey.Text = WPFLocalizeExtensionHelpers.GetUIString("ColumnRegKey");
        }

        /// <summary>
        /// Registry cleaner form load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmRegCleaner_Load(object sender, EventArgs e)
        {
            UpdateUILocalization();
        }
    }
}