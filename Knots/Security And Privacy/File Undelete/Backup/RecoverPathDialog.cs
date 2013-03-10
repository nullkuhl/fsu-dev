using System;
using System.IO;
using System.Windows.Forms;

namespace ScanFiles
{
    public partial class RecoverPathDialog : Form
    {
        private string defaultPath;

        public RecoverPathDialog(string defaultPath)
        {
            this.defaultPath = defaultPath;

            InitializeComponent();
        }

        private void RestorePathDialog_Load(object sender, EventArgs e)
        {
            string filename = defaultPath;
            try
            {
                filename = Path.GetFileName(defaultPath);
            }
            catch
            {
                //nothing
            }
            label1.Text = "Are you sure to restore " + filename + "?";
            pathTbx.Text = defaultPath;
        }

        public string SelectedPath
        {
            get
            {
                return pathTbx.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            try
            {
                dlg.FileName = new FileInfo(SelectedPath).FullName;
            }
            catch{ /* nothing */}

            DialogResult dlgResult = dlg.ShowDialog();
            if (dlgResult == DialogResult.Yes || dlgResult == DialogResult.OK)
            {
                pathTbx.Text = dlg.FileName;
            }
        }
    }
}