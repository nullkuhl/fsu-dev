using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StartupManager
{
    /// <summary>
    /// Bottom control
    /// </summary>
    public partial class BottomControl : UserControl
    {
        /// <summary>
        /// constructor for BottomControl
        /// </summary>
        public BottomControl()
        {
            InitializeComponent();
            SetImage();
        }

        /// <summary>
        /// change image for BottomControl
        /// </summary>
        void SetImage()
        {
            Image image;
            try
            {
                string path = Path.GetDirectoryName(GetType().Assembly.Location) + "\\Skins\\blue\\bottomblue.png";
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