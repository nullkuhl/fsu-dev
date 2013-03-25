using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FreemiumUtilities.Infrastructure;

namespace FreemiumUtilities.StartupManager
{
    /// <summary>
    /// Startup Manager 1 Click-Maintenance application details form
    /// </summary>
    public partial class FrmDetails : Form
    {
        /// <summary>
        /// constructor for frmDetails
        /// </summary>
        /// <param name="LstVwItemLst"></param>
        public FrmDetails(IEnumerable<ListViewItem> LstVwItemLst)
        {
            InitializeComponent();

            int i = 0;

            foreach (ListViewItem item in LstVwItemLst)
            {
                listview.Items.Add(new ListViewItem { Text = item.SubItems[1].Text });
                listview.Items[i].SubItems.Add(item.SubItems[4]);
                i++;
            }
        }

        /// <summary>
        /// initialize frmDetails
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmDetails_Load(object sender, EventArgs e)
        {
            UpdateUILocalization();
        }

        /// <summary>
        /// Applies localized strings to the UI
        /// </summary>
        void UpdateUILocalization()
        {
            FileName.Text = WPFLocalizeExtensionHelpers.GetUIString("ColumnDetailsFile");
            Type.Text = WPFLocalizeExtensionHelpers.GetUIString("ColumnDetailsLocation");
        }
    }
}