using System;
using System.Windows.Forms;

/// <summary>
/// The <see cref="Joiner"/> namespace defines a Joiner utility
/// </summary>
namespace Joiner
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}