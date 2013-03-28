using System;
using System.Diagnostics;
using System.Resources;
using System.Windows.Forms;
using FreemiumUtilities.TracksEraser.Properties;
using FreemiumUtil;

namespace FreemiumUtilities.TracksEraser
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
        /// handle mouse enter event to change help icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblHelp_MouseEnter(object sender, EventArgs e)
        {
            lblHelp.Image = Resources.help_on;
        }

        /// <summary>
        /// handle mouse leave event to change help icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblHelp_MouseLeave(object sender, EventArgs e)
        {
            lblHelp.Image = Resources.help_off;
        }

        /// <summary>
        /// handle click event to open help url
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblHelp_Click(object sender, EventArgs e)
        {
            try
            {
                ResourceManager resourceManager = new ResourceManager("TracksEraser.Resources", typeof(TopControl).Assembly);
                CommonOperations.OpenUrl(resourceManager.GetString("HelpUrl"));
            }
            catch (Exception)
            { }
        }
    }
}