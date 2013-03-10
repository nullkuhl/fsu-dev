using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Uninstall_Manager
{
    public partial class TopControl : UserControl
    {
        public TopControl()
        {
            InitializeComponent();
        }

        private void lblHelp_MouseEnter(object sender, EventArgs e)
        {
            lblHelp.Image = Properties.Resources.HelpButton_On;
        }

        private void lblHelp_MouseLeave(object sender, EventArgs e)
        {
            lblHelp.Image = Properties.Resources.HelpButton;
        }

        private void SetImage()
        {
            Image image = null;
            try
            {
                string path = Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\Skins\\blue\\thinblue.png";
                image = Properties.Resources.thinBar;
                if (File.Exists(path))
                {

                    image = Image.FromFile(path);
                }
            }
            catch (Exception)
            {
                image = Properties.Resources.thinBar;
            }

            if (image == null)
            {
                image = Properties.Resources.thinBar;
            }

            lblBar.Image = image;

        }

        public string Text
        {
            get { return lblText.Text; }
            set { lblText.Text = value; }
        }



    }
}
