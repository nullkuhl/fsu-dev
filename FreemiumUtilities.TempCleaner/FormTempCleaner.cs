using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using FreemiumUtilities.Infrastructure;

namespace FreemiumUtilities.TempCleaner
{
    /// <summary>
    /// TempCleaner 1 Click-Maintenance application main form
    /// </summary>
    public partial class FrmTempCleaner : Form
    {
        List<FileInfo> chromeFiles;
        List<FileInfo> ffFiles;
        List<FileInfo> ieFiles;
        List<FileInfo> tmpFiles;
        List<FileInfo> winFiles;

        /// <summary>
        /// constructor for frmTempCleaner
        /// </summary>
        public FrmTempCleaner()
        {
            InitializeComponent();
            UpdateUILocalization();
        }

        /// <summary>
        /// Temp files size
        /// </summary>
        public ulong TmpSize { get; set; }

        /// <summary>
        /// Temp files collection
        /// </summary>
        public List<FileInfo> TmpFiles
        {
            get { return tmpFiles; }
            set
            {
                tmpFiles = value;
                tmpDetailsLbl.Text = WPFLocalizeExtensionHelpers.GetUIString("TempDetailsTotal") + " " + value.Count + " " +
                                     WPFLocalizeExtensionHelpers.GetUIString("TempDetailsFiles") + ", " + FormatSize(TmpSize) + " " +
                                     WPFLocalizeExtensionHelpers.GetUIString("TempDetailsRecoverable");
            }
        }

        /// <summary>
        /// Windows temp files size
        /// </summary>
        public ulong WinSize { get; set; }

        /// <summary>
        /// Windows temp files collection
        /// </summary>
        public List<FileInfo> WinFiles
        {
            get { return winFiles; }
            set
            {
                winFiles = value;
                winDetailsLbl.Text = WPFLocalizeExtensionHelpers.GetUIString("TempDetailsTotal") + " " + value.Count + " " +
                                     WPFLocalizeExtensionHelpers.GetUIString("TempDetailsFiles") + ", " + FormatSize(WinSize) + " " +
                                     WPFLocalizeExtensionHelpers.GetUIString("TempDetailsRecoverable");
            }
        }

        /// <summary>
        /// IE temp files size
        /// </summary>
        public ulong IESize { get; set; }

        /// <summary>
        /// IE temp files collection
        /// </summary>
        public List<FileInfo> IEFiles
        {
            get { return ieFiles; }
            set
            {
                ieFiles = value;
                ieDetailsLbl.Text = WPFLocalizeExtensionHelpers.GetUIString("TempDetailsTotal") + " " + value.Count + " " +
                                    WPFLocalizeExtensionHelpers.GetUIString("TempDetailsFiles") + ", " + FormatSize(IESize) + " " +
                                    WPFLocalizeExtensionHelpers.GetUIString("TempDetailsRecoverable");
            }
        }

        /// <summary>
        /// Firefox temp files size
        /// </summary>
        public ulong FFSize { get; set; }

        /// <summary>
        /// Firefox temp files collection
        /// </summary>
        public List<FileInfo> FFFiles
        {
            get { return ffFiles; }
            set
            {
                ffFiles = value;
                ffDetailsLbl.Text = WPFLocalizeExtensionHelpers.GetUIString("TempDetailsTotal") + " " + value.Count + " " +
                                    WPFLocalizeExtensionHelpers.GetUIString("TempDetailsFiles") + ", " + FormatSize(FFSize) + " " +
                                    WPFLocalizeExtensionHelpers.GetUIString("TempDetailsRecoverable");
            }
        }

        /// <summary>
        /// Chrome temp files size
        /// </summary>
        public ulong ChromeSize { get; set; }

        /// <summary>
        /// Chrome temp files collection
        /// </summary>
        public List<FileInfo> ChromeFiles
        {
            get { return chromeFiles; }
            set
            {
                chromeFiles = value;
                chromeDetailsLbl.Text = WPFLocalizeExtensionHelpers.GetUIString("TempDetailsTotal") + " " + value.Count + " " +
                                        WPFLocalizeExtensionHelpers.GetUIString("TempDetailsFiles") + ", " + FormatSize(ChromeSize) + " " +
                                        WPFLocalizeExtensionHelpers.GetUIString("TempDetailsRecoverable");
            }
        }

        /// <summary>
        /// formats size for display
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string FormatSize(ulong bytes)
        {
            double size = bytes;
            string unit = " Bytes";
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " KB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " MB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " GB";
            }
            if ((int)(size / 1024) > 0)
            {
                size /= 1024.0;
                unit = " TB";
            }

            return size.ToString("0.##") + unit;
        }

        /// <summary>
        /// handle Click event to show temp files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void winBtn_Click(object sender, EventArgs e)
        {
            string winTempPath = Environment.GetEnvironmentVariable("Temp");
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            var process = new Process();
            process.StartInfo.FileName = windir + @"\explorer.exe";
            process.StartInfo.Arguments = winTempPath;
            process.Start();
        }

        /// <summary>
        /// handle Click event to show internet explorer temp files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ieBtn_Click(object sender, EventArgs e)
        {
            string ieTempPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            var process = new Process();
            process.StartInfo.FileName = windir + @"\explorer.exe";
            process.StartInfo.Arguments = ieTempPath;
            process.Start();
        }

        /// <summary>
        /// handle Click event to show firefox cache files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ffBtn_Click(object sender, EventArgs e)
        {
            string ffCachePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                 "\\Mozilla\\Firefox\\Profiles";
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            Process process = new Process();
            process.StartInfo.FileName = windir + @"\explorer.exe";
            process.StartInfo.Arguments = ffCachePath;
            process.Start();
        }

        /// <summary>
        /// handle Click event to show chrome files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void chromeBtn_Click(object sender, EventArgs e)
        {
            string chromeCachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                     "\\Google\\Chrome\\User Data\\Default";
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            Process process = new Process();
            process.StartInfo.FileName = windir + @"\explorer.exe";
            process.StartInfo.Arguments = chromeCachePath;
            process.Start();
        }

        /// <summary>
        /// refresh results
        /// </summary>
        void RefreshResults()
        {
            if (TmpFiles.Count > 0)
                tmpDetailsLbl.Text = WPFLocalizeExtensionHelpers.GetUIString("TempDetailsTotal") + " " + TmpFiles.Count + " " +
                                     WPFLocalizeExtensionHelpers.GetUIString("TempDetailsFiles") + ", " + FormatSize(TmpSize) + " " +
                                     WPFLocalizeExtensionHelpers.GetUIString("TempDetailsRecoverable");
            if (WinFiles.Count > 0)
                winDetailsLbl.Text = WPFLocalizeExtensionHelpers.GetUIString("TempDetailsTotal") + " " + WinFiles.Count + " " +
                                     WPFLocalizeExtensionHelpers.GetUIString("TempDetailsFiles") + ", " + FormatSize(WinSize) + " " +
                                     WPFLocalizeExtensionHelpers.GetUIString("TempDetailsRecoverable");
            if (IEFiles.Count > 0)
                ieDetailsLbl.Text = WPFLocalizeExtensionHelpers.GetUIString("TempDetailsTotal") + " " + IEFiles.Count + " " +
                                    WPFLocalizeExtensionHelpers.GetUIString("TempDetailsFiles") + ", " + FormatSize(IESize) + " " +
                                    WPFLocalizeExtensionHelpers.GetUIString("TempDetailsRecoverable");
            if (FFFiles.Count > 0)
                ffDetailsLbl.Text = WPFLocalizeExtensionHelpers.GetUIString("TempDetailsTotal") + " " + FFFiles.Count + " " +
                                    WPFLocalizeExtensionHelpers.GetUIString("TempDetailsFiles") + ", " + FormatSize(FFSize) + " " +
                                    WPFLocalizeExtensionHelpers.GetUIString("TempDetailsRecoverable");
            if (ChromeFiles.Count > 0)
                chromeDetailsLbl.Text = WPFLocalizeExtensionHelpers.GetUIString("TempDetailsTotal") + " " + ChromeFiles.Count + " " +
                                        WPFLocalizeExtensionHelpers.GetUIString("TempDetailsFiles") + ", " + FormatSize(ChromeSize) + " " +
                                        WPFLocalizeExtensionHelpers.GetUIString("TempDetailsRecoverable");
        }

        /// <summary>
        /// initialize frmTempCleaner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmTempCleaner_Load(object sender, EventArgs e)
        {
            UpdateUILocalization();
            RefreshResults();

            string IETempPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            string ffCachePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                 "\\Mozilla\\Firefox\\Profiles";
            string chromeCachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                     "\\Google\\Chrome\\User Data\\Default";

            DirectoryInfo IETemp = Directory.Exists(IETempPath) ? new DirectoryInfo(IETempPath) : null;
            DirectoryInfo ffCahce = Directory.Exists(ffCachePath) ? new DirectoryInfo(ffCachePath) : null;
            DirectoryInfo chromeCache = Directory.Exists(chromeCachePath) ? new DirectoryInfo(chromeCachePath) : null;

            if (IETemp == null)
            {
                winBtn.Enabled = false;
                winDetailsLbl.Visible = false;
            }

            if (ffCahce == null)
            {
                ffBtn.Enabled = false;
                ffDetailsLbl.Visible = false;
            }

            if (chromeCache == null)
            {
                chromeBtn.Enabled = false;
                ffDetailsLbl.Visible = false;
            }
        }

        /// <summary>
        /// Applies localized strings to the UI
        /// </summary>
        void UpdateUILocalization()
        {
            lblTmp.Text = WPFLocalizeExtensionHelpers.GetUIString("LabelTmpFiles");
            lblWinTmp.Text = WPFLocalizeExtensionHelpers.GetUIString("LabelWindowsTemporaryFiles");
            lblIE.Text = WPFLocalizeExtensionHelpers.GetUIString("LabelIETempraryFiles");
            lblFF.Text = WPFLocalizeExtensionHelpers.GetUIString("LabelFFCache");
            lblChrome.Text = WPFLocalizeExtensionHelpers.GetUIString("LabelChromeCache");
            chromeBtn.Text = WPFLocalizeExtensionHelpers.GetUIString("ButtonViewFilesContent");
            ieBtn.Text = WPFLocalizeExtensionHelpers.GetUIString("ButtonViewFilesContent");
            winBtn.Text = WPFLocalizeExtensionHelpers.GetUIString("ButtonViewFilesContent");
            ffBtn.Text = WPFLocalizeExtensionHelpers.GetUIString("ButtonViewFilesContent");
        }
    }
}