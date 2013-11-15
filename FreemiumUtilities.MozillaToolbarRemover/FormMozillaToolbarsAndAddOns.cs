using System.Collections.Generic;
using System.Windows.Forms;
using FreemiumUtilities.Infrastructure;

namespace FreemiumUtilities.MozillaToolbarRemover
{
    public partial class FormMozillaToolbarsAndAddOns : Form
    {
        public FormMozillaToolbarsAndAddOns()
        {
            InitializeComponent();
            UpdateUILocalization();
        }

        /// <summary>
        /// Sets the list of spyware found
        /// </summary>
        public List<FirefoxExtension> MozillaToolbarAndAddOnFound
        {
            set
            {
                lvMozillaTools.BeginUpdate();
                foreach (FirefoxExtension MozillaTool in value)
                {
                    ListViewItem lvi = new ListViewItem(MozillaTool.Name);
                    lvi.Checked = MozillaTool.IsEnabled;
                    lvi.Tag = MozillaTool;
                    lvMozillaTools.Items.Add(lvi);
                }
                lvMozillaTools.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvMozillaTools.EndUpdate();
            }
        }

        /// <summary>
        /// Problems collection
        /// </summary>
        public List<ListViewItem> Problems
        {
            get
            {
                List<ListViewItem> problemList = new List<ListViewItem>();
                foreach (ListViewItem item in lvMozillaTools.Items)
                {
                    problemList.Add(item);
                }

                return problemList;
            }
        }


        /// <summary>
        /// Applies localized strings to the UI
        /// </summary>
        void UpdateUILocalization()
        {
            Text = WPFLocalizeExtensionHelpers.GetUIString("SpywareRemover");
            //colFilePath.Text = WPFLocalizeExtensionHelpers.GetUIString("ColumnSpywareFile");
            //colSpyware.Text = WPFLocalizeExtensionHelpers.GetUIString("ColumnSpyware");
        }

        private void lvMozillaTools_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            FirefoxExtension FFTool = e.Item.Tag as FirefoxExtension;
            FFTool.IsEnabled = e.Item.Checked;
        }
    }
}
