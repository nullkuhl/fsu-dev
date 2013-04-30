using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

/// <summary>
/// The <see cref="ClickMaint"/> namespace contains classes for the 1ClickMessageSender
/// </summary>
namespace ClickMaint
{
    /// <summary>
    /// 1ClickMessageSender main form
    /// </summary>
    public class frmMain : Form
    {
        //const int RF_TESTMESSAGE = 0xA123;
        const int MaintMsg = 0xC100;
        const int MaintFixMsg = 0xC111;
        const string ScanAndFixCommandLineArg = "ScanFix";

        public const int HwndBroadcast = 0xFFFF;

        public static readonly int WmActivateapp = RegisterWindowMessage("WM_MDIRESTORE");
        GroupBox grbMain;
        Label lblMessages;
        Label lblProcID;
        ListBox lbxMain;

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
        
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        const UInt32 WS_MINIMIZE = 0x20000000;



        /// <summary>
        /// constructor for FormMain
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            lblProcID.Text = string.Format("This process ID: {0}", Process.GetCurrentProcess().Id);
            try
            {
                Process[] processes = Process.GetProcessesByName("FreemiumUtilities");

                string[] args = Environment.GetCommandLineArgs();
                
                if (processes.Length > 0)
                {
                    SendMessage((IntPtr)HwndBroadcast, Array.IndexOf(args, ScanAndFixCommandLineArg) != -1 ? MaintFixMsg : MaintMsg,
                                IntPtr.Zero, IntPtr.Zero);
                }
                else
                {
                    Process p = new Process
                                {
                                    StartInfo = { FileName = Environment.CurrentDirectory + "\\FreemiumUtilities.exe" }
                                };
                    p.Start();
                   //Thread.Sleep(5000);
                    //while (FindWindowByCaption(IntPtr.Zero, "Free System Utilities") == IntPtr.Zero)
                    while(( GetWindowLong(p.MainWindowHandle,  -16) & WS_MINIMIZE  ) == WS_MINIMIZE)
                    {
                        Thread.Sleep(500);
                    }
                    
                    
                    Thread.Sleep(4000);
                    SendMessage((IntPtr)HwndBroadcast, Array.IndexOf(args, ScanAndFixCommandLineArg) == -1 ? MaintMsg : MaintFixMsg,
                                IntPtr.Zero, IntPtr.Zero);
                }
            }
            catch (Exception ex)
            {
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            this.lbxMain = new System.Windows.Forms.ListBox();
            this.grbMain = new System.Windows.Forms.GroupBox();
            this.lblMessages = new System.Windows.Forms.Label();
            this.lblProcID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbxMain
            // 
            this.lbxMain.Location = new System.Drawing.Point(8, 72);
            this.lbxMain.Name = "lbxMain";
            this.lbxMain.Size = new System.Drawing.Size(280, 173);
            this.lbxMain.TabIndex = 1;
            // 
            // grbMain
            // 
            this.grbMain.Location = new System.Drawing.Point(8, 40);
            this.grbMain.Name = "grbMain";
            this.grbMain.Size = new System.Drawing.Size(280, 2);
            this.grbMain.TabIndex = 2;
            this.grbMain.TabStop = false;
            // 
            // lblMessages
            // 
            this.lblMessages.Location = new System.Drawing.Point(8, 56);
            this.lblMessages.Name = "lblMessages";
            this.lblMessages.Size = new System.Drawing.Size(112, 16);
            this.lblMessages.TabIndex = 3;
            this.lblMessages.Text = "Received Messages:";
            // 
            // lblProcID
            // 
            this.lblProcID.Location = new System.Drawing.Point(104, 248);
            this.lblProcID.Name = "lblProcID";
            this.lblProcID.Size = new System.Drawing.Size(184, 16);
            this.lblProcID.TabIndex = 5;
            this.lblProcID.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(296, 267);
            this.Controls.Add(this.lblProcID);
            this.Controls.Add(this.lblMessages);
            this.Controls.Add(this.grbMain);
            this.Controls.Add(this.lbxMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Communicating via Messages Demo";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SendMessage(IntPtr hwnd, [MarshalAs(UnmanagedType.U4)] int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);

        [MTAThread]
        static void Main()
        {
            new frmMain();
            Environment.Exit(0);
        }

        /// <summary>
        /// initialize FormMain
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmMain_Load(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        protected override void WndProc(ref Message message)
        {
            //filter the RF_TESTMESSAGE
            if (message.Msg == MaintMsg)
            {
                //display that we recieved the message, of course we could do
                //something else more important here.
                Environment.Exit(0);
            }
            //be sure to pass along all messages to the base also
            base.WndProc(ref message);
        }
    }
}