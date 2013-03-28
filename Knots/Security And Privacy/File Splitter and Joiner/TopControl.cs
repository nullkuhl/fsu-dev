using System;
using System.Diagnostics;
using System.Windows.Forms;
using FileSplitterAndJoiner.Properties;
using System.Resources;
using FreemiumUtil;

namespace FileSplitterAndJoiner
{
    /// <summary>
    /// Top styling control
    /// </summary>
    public partial class TopControl : UserControl
    {
        /// <summary>
        /// TopControl constructor
        /// </summary>
        public TopControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the header text
        /// </summary>
        public override string Text
        {
            get { return lblHeader.Text; }
            set { lblHeader.Text = value; }
        }

        /// <summary>
        /// Sets the hover state image for a help label at mouse enter event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblHelp_MouseEnter(object sender, EventArgs e)
        {
            lblHelp.Image = Resources.help_on;
        }

        /// <summary>
        /// Sets the normal state image for a help label at mouse leave event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblHelp_MouseLeave(object sender, EventArgs e)
        {
            lblHelp.Image = Resources.help_off;
        }

        /// <summary>
        /// Goes to the help webpage referenced in a <c>HelpUrl</c> constant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblHelp_Click(object sender, EventArgs e)
        {
            try
            {
                ResourceManager resourceManager = new ResourceManager("FileSplitterAndJoiner.Resources", typeof(TopControl).Assembly);
                CommonOperations.OpenUrl(resourceManager.GetString("HelpUrl"));
            }
            catch (Exception)
            { }
        }
    }
}