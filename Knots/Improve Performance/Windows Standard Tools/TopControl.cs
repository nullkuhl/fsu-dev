using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WindowsStandardTools.Properties;
using System.Resources;
using FreemiumUtil;

namespace WindowsStandardTools
{
    /// <summary>
    /// Top control
    /// </summary>
    public partial class TopControl : UserControl
    {
        /// <summary>
        /// constructor for TopControl
        /// </summary>
        public TopControl()
        {
            InitializeComponent();
            SetImage();
        }

        /// <summary>
        /// Top control text
        /// </summary>
        public override string Text
        {
            get { return lblText.Text; }
            set { lblText.Text = value; }
        }

        /// <summary>
        /// handle MouseEnter event to change help icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblHelp_MouseEnter(object sender, EventArgs e)
        {
            lblHelp.Image = Resources.help_on;
        }

        /// <summary>
        /// handle MouseLeave event to change help icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblHelp_MouseLeave(object sender, EventArgs e)
        {
            lblHelp.Image = Resources.help_off;
        }

        /// <summary>
        /// change image for TopControl
        /// </summary>
        void SetImage()
        {
            Image image;
            try
            {
                string path = Path.GetDirectoryName(GetType().Assembly.Location) + "\\Skins\\blue\\thinblue.png";
                image = Resources.thinBar;
                if (File.Exists(path))
                {
                    image = Image.FromFile(path);
                }
            }
            catch (Exception)
            {
                image = Resources.thinBar;
            }

            if (image == null)
            {
                image = Resources.thinBar;
            }

            lblBar.Image = image;
        }

        /// <summary>
        /// handle Click event to open help url
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblHelp_Click(object sender, EventArgs e)
        {
            try
            {
                ResourceManager resourceManager = new ResourceManager("WindowsStandardTools.Resources", typeof(TopControl).Assembly);
                CommonOperations.OpenUrl(resourceManager.GetString("HelpUrl"));
            }
            catch (Exception)
            { }
        }
    }
}