using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace FileUndelete
{
    /// <summary>
    /// Contains UI elements to filter files
    /// </summary>
    public partial class frmFilter : Form
    {
        /// <summary>
        /// Contains UI elements to filter files
        /// </summary>
        public frmFilter()
        {
            InitializeComponent();
            SetCulture(new CultureInfo(CfgFile.Get("Lang")));
        }

        /// <summary>
        /// Fires when user acceps entered file filter
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Sets a specified <paramref Name="Culture"/> to a current thread
        /// </summary>
        /// <param Name="Culture"></param>
        void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;

            grbMain.Text = rm.GetString("filter");
            chkIncludeNonRecoverable.Text = rm.GetString("include_non_recoverable");
            cboSize.Items.Clear();
            cboSize.Items.AddRange(new object[]
			                       	{
			                       		rm.GetString("more_than"),
			                       		rm.GetString("less_than")
			                       	});
            chkSize.Text = rm.GetString("by_size");
            lblFilename.Text = rm.GetString("all_or_part_of_filename1") + "\r\n" +
                               rm.GetString("all_or_part_of_filename2") + "\r\n" +
                               rm.GetString("all_or_part_of_filename3");
            chkFilename.Text = rm.GetString("by_filename");
            btnOK.Text = rm.GetString("ok");
            btnCancel.Text = rm.GetString("cancel");
            Text = rm.GetString("filter");
        }
    }
}