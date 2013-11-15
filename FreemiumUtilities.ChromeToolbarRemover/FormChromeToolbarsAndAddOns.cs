using System.Collections.Generic;
using System.Windows.Forms;
using FreemiumUtilities.Infrastructure;

namespace FreemiumUtilities.ChromeToolbarRemover
{
    public partial class FormChromeToolbarsAndAddOns : Form
    {
        public FormChromeToolbarsAndAddOns()
        {
            InitializeComponent();
            UpdateUILocalization();
        }

        /// <summary>
        /// Sets the list of spyware found
        /// </summary>
        public List<ChromeExtension> ChromeToolbarAndAddOnFound
        {
            set
            {
                lvChromeTools.BeginUpdate();
                foreach (ChromeExtension ChromeTool in value)
                {
                    ListViewItem lvi = new ListViewItem(ChromeTool.Name);
                    lvi.Checked = ChromeTool.IsEnabled;
                    lvi.Tag = ChromeTool;
                    lvChromeTools.Items.Add(lvi);
                }
                lvChromeTools.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvChromeTools.EndUpdate();
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
                foreach (ListViewItem item in lvChromeTools.Items)
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

        private void lvChromeTools_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ChromeExtension ChromeTool = e.Item.Tag as ChromeExtension;
            ChromeTool.IsEnabled = e.Item.Checked;
        }
    }
}
