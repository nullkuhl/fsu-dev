using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using EncryptDecrypt.Properties;
using FreemiumUtil;
using System.IO;
using System.Reflection;
using Microsoft.VisualBasic.ApplicationServices;

/// <summary>
/// The <see cref="EncryptDecrypt"/> namespace defines a Encrypt and Decrypt knot
/// </summary>
namespace EncryptDecrypt
{
    internal static class Program
    {
        //static Mutex mutex;
        //static bool created;
        static frmMain MainForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new frmMain();
            SingleInstanceApplication.Run(MainForm, NewInstanceHandler);    
        }

        public static void NewInstanceHandler(object sender, StartupNextInstanceEventArgs e)
        {
            MainForm.NewInstance(e);
            e.BringToForeground = true;
        }        

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        static void CurrentDomain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// Class for instance management
        /// </summary>
        public class SingleInstanceApplication : WindowsFormsApplicationBase
        {
            private SingleInstanceApplication()
            {
                base.IsSingleInstance = true;
            }

            public static void Run(Form f, StartupNextInstanceEventHandler startupHandler)
            {
                SingleInstanceApplication app = new SingleInstanceApplication();
                app.MainForm = f;
                app.StartupNextInstance += startupHandler;
                app.Run(Environment.GetCommandLineArgs());
            }
        }
    }
}