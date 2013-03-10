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
    public partial class ButtomControl : UserControl
    {
        public ButtomControl()
        {
            InitializeComponent();
            SetImage();
        }

        private void SetImage()
        {
            Image image = null;
            try
            {
                string path = Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\Skins\\blue\\bottomblue.png";
                image = Properties.Resources.bottomBar;
                if (File.Exists(path))
                {

                    image = Image.FromFile(path);
                }
            }
            catch (Exception)
            {
                image = Properties.Resources.bottomBar;
            }

            if (image == null)
            {
                image = Properties.Resources.bottomBar;
            }

            lblMain.Image = image;

        }
    }
}
