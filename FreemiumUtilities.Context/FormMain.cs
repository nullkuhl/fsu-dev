using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

/// <summary>
/// The <see cref="FreemiumUtilities.Context"/> namespace contains a set of classes to operate with Windows Explorer context menu
/// </summary>
namespace FreemiumUtilities.Context
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// constructor for FormMain
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// initialize FormMain
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                Process process = new Process();
                string location = Assembly.GetExecutingAssembly().Location;
                string str = location.Substring(0, location.LastIndexOf(@"\"));
                process.StartInfo.WorkingDirectory = str;
                string[] commandLineArgs = Environment.GetCommandLineArgs();
                if (commandLineArgs.Length > 1)
                {
                    bool flag = commandLineArgs[1] != "ENCRYPT";
                    if (flag)
                    {
                        flag = commandLineArgs[1] != "DECRYPT";
                        if (flag)
                        {
                            flag = commandLineArgs[1] != "WIPE";
                            if (flag)
                            {
                                flag = commandLineArgs[1] != "SPLIT";
                                if (flag)
                                {
                                    flag = commandLineArgs[1] != "ANALYSE";
                                    if (flag)
                                    {
                                        flag = commandLineArgs[1] != "EMPTYFOLDERS";
                                        if (!flag)
                                        {
                                            process.StartInfo.FileName = string.Concat(str, "\\EmptyFolderFinder.exe");
                                            process.StartInfo.Arguments = commandLineArgs[2];
                                        }
                                    }
                                    else
                                    {
                                        process.StartInfo.FileName = string.Concat(str, "\\DiskAnalysis.exe");
                                        process.StartInfo.Arguments = string.Concat(commandLineArgs[1], " ", commandLineArgs[2]);
                                    }
                                }
                                else
                                {
                                    process.StartInfo.FileName = string.Concat(str, "\\FileSplitterJoiner.exe");
                                    process.StartInfo.Arguments = string.Concat(commandLineArgs[1], " ", commandLineArgs[2]);
                                }
                            }
                            else
                            {
                                process.StartInfo.FileName = string.Concat(str, "\\FileEraser.exe");
                                process.StartInfo.Arguments = string.Concat(commandLineArgs[1], " ", commandLineArgs[2]);
                            }
                        }
                        else
                        {
                            process.StartInfo.FileName = string.Concat(str, "\\EncryptDecrypt.exe");
                            process.StartInfo.Arguments = string.Concat(commandLineArgs[1], " ", commandLineArgs[2]);
                        }
                    }
                    else
                    {
                        process.StartInfo.FileName = string.Concat(str, "\\EncryptDecrypt.exe");
                        process.StartInfo.Arguments = string.Concat(commandLineArgs[1], " ", commandLineArgs[2]);
                    }
                    process.Start();
                }
            }
            catch
            {
            }
            Application.Exit();
        }
    }
}