using System;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;
using System.Text;

namespace ProcessManager
{
    /// <summary>
    /// Blocked processes form of the Process Manager knot
    /// </summary>
    public partial class FormBlockedProcesses : Form
    {
        string sStringToWrite;
        string[] words;

        /// <summary>
        /// constructor for FrmBlockedProcesses
        /// </summary>
        public FormBlockedProcesses()
        {
            InitializeComponent();
        }

        /// <summary>
        /// initialize FrmBlockedProcesses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmBlockedProcesses_Load(object sender, EventArgs e)
        {
            SetCulture(new CultureInfo(CfgFile.Get("Lang")));
            PopulateListBox();
        }

        /// <summary>
        /// update the list of blocked processes
        /// </summary>
        public void PopulateListBox()
        {
            lvBlockedProcesses.Items.Clear();
            foreach (string s in BlockedProcessesManager.GetBlockedProcesses())
            {
                if (!string.IsNullOrEmpty(s) && s != "xml" && s != "processes" && !lvBlockedProcesses.Items.ContainsKey(s))
                {
                    ListViewItem lvi = new ListViewItem(s);
                    lvi.Name = s;
                    lvBlockedProcesses.Items.Add(lvi);
                }
            }
        }

        /// <summary>
        /// handle Click event to close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// handle Click event to unblock process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvBlockedProcesses.SelectedItems.Count > 0)
            {
                string processName = lvBlockedProcesses.SelectedItems[0].Text;
                StringBuilder sb = new StringBuilder();

                foreach (string s in BlockedProcessesManager.GetBlockedProcesses())
                {
                    if (!string.IsNullOrEmpty(s) && s != processName)
                    {
                        sb.Append(s);
                        sb.Append("~");
                    }
                }

                BlockedProcessesManager.WriteBlockedProcesses(sb.ToString());
                PopulateListBox();
                FormProcessManager.IsBlockProcessAdded = true;
            }
        }

        /// <summary>
        /// change current language
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            var rm = new ResourceManager("ProcessManager.Resources", typeof(FormBlockedProcesses).Assembly);
            Thread.CurrentThread.CurrentUICulture = culture;
            btnClose.Text = rm.GetString("close", culture);
            btnRemove.Text = rm.GetString("remove", culture);
            Text = rm.GetString("blocked_proc", culture);
        }
    }
}