using System.Collections.Generic;
using System.Windows.Forms;
using FreemiumUtilities.Infrastructure;

namespace FreemiumUtilities.IEToolbarRemover
{
    public partial class FormIEToolbarsAndAddOns : Form
    {
        public FormIEToolbarsAndAddOns()
        {
            InitializeComponent();
            UpdateUILocalization();
        }

        /// <summary>
        /// Sets the list of spyware found
        /// </summary>
        public List<ExplorerToolbarAndAddOn> ExplorerToolbarAndAddOnFound
        {
            set
            {
                lvIETools.BeginUpdate();
                foreach (ExplorerToolbarAndAddOn IETool in value)
                {
                    ListViewItem lvi = new ListViewItem(IETool.Name);
                    lvi.Checked = IETool.IsEnabled;
                    lvi.Tag = IETool;
                    lvIETools.Items.Add(lvi);
                }
                lvIETools.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvIETools.EndUpdate();
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
                foreach (ListViewItem item in lvIETools.Items)
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

        private void lvIETools_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ExplorerToolbarAndAddOn IETool = e.Item.Tag as ExplorerToolbarAndAddOn;
            IETool.IsEnabled = e.Item.Checked;
        }
    }
}
