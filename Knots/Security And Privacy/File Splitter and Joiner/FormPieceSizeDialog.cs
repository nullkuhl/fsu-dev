using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FreemiumUtil;

namespace FileSplitterAndJoiner
{
    /// <summary>
    /// Form that contains a UI for entering a piece size
    /// </summary>
    public partial class FormPieceSizeDialog : Form
    {
        #region Properties

        /// <summary>
        /// Piece size in bytes
        /// </summary>
        public long PieceSize;

        #endregion

        #region Constructors

        /// <summary>
        /// frmPieceSizeDialog constructor
        /// </summary>
        public FormPieceSizeDialog()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// frmPieceSizeDialog Load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmPieceSizeDialog_Load(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo(CfgFile.Get("Lang"));
            SetCulture(culture);
            CenterToParent();
        }

        /// <summary>
        /// Supresses some keys at txtPieceSize KeyDown event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtPieceSize_KeyDown(object sender, KeyEventArgs e)
        {
            bool supper = false;
            for (int i = 0; i < txtPieceSize.Text.Length; i++)
                if (txtPieceSize.Text.Substring(i, 1) == ".")
                    supper = true;
            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                case Keys.Back:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.Delete:
                    break;
                case Keys.Enter:
                    break;
                case Keys.Decimal:
                case Keys.OemPeriod:
                    e.SuppressKeyPress = supper;
                    break;
                default:
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        /// <summary>
        /// Calculates a piece size in a bytes for a specified size in KB, MB or GB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPieceSize.Text))
            {
                double d = Convert.ToDouble(txtPieceSize.Text);
                if (rdbKB.Checked)
                    d *= 1024;
                else if (rdbMB.Checked)
                    d *= 1024 * 1024;
                else if (rdbGB.Checked)
                    d *= 1024 * 1024 * 1024;
                PieceSize = Convert.ToInt64(Math.Truncate(d));
            }
        }

        /// <summary>
        /// Sets a specified <paramref name="culture"/> to the current thread
        /// </summary>
        /// <param name="culture"></param>
        void SetCulture(CultureInfo culture)
        {
            ResourceManager rm = new ResourceManager("FileSplitterAndJoiner.Resources", typeof(FormPieceSizeDialog).Assembly);
            Thread.CurrentThread.CurrentUICulture = culture;

            btnOK.Text = rm.GetString("ok");
            Text = rm.GetString("specify_piece_size");
        }
    }
}