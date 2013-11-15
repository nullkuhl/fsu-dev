using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FreeToolbarRemover.ViewModels;
using Application = System.Windows.Application;

namespace FreeToolbarRemover.Routine
{
	/// <summary>
	/// Message pipe main form
	/// </summary>
	public partial class messagePipe : Form
	{
		const int MaintMsg = 0xC100;
		const int MaintFixMsg = 0xC111;

		#region Dll Imports

		[DllImport("user32")]
		static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

		[DllImport("user32")]
		static extern int RegisterWindowMessage(string message);

		#endregion Dll Imports

		public messagePipe()
		{
			InitializeComponent();
		}

		void messagePipe_Load(object sender, EventArgs e)
		{
			Visible = false;
		}

		protected override void WndProc(ref Message message)
		{
			//filter the RF_TESTMESSAGE
			if ((message.Msg == MaintMsg))
			{
				((OneClickAppsViewModel) Application.Current.MainWindow.DataContext).SelectAllAndRun(false);
			}
			else if ((message.Msg == MaintFixMsg))
			{
				((OneClickAppsViewModel) Application.Current.MainWindow.DataContext).SelectAllAndRun(true);
			}
			//be sure to pass along all messages to the base also
			base.WndProc(ref message);
		}
	}
}