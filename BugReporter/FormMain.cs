using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Windows.Forms;
using BugReporter.FreemiumWebService;

namespace BugReporter
{
	/// <summary>
	/// BugReporter main form
	/// </summary>
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
		/// Sets the <c>txtException</c> text to the <c>Program.BugStackTrace</c>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FormMain_Load(object sender, EventArgs e)
		{

            try
            {
                txtException.Text = Program.BugStackTrace;
                
            }
            catch (Exception ex)
            {
                this.Close();
            }
		}

		/// <summary>
		/// Closes the app
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		/// <summary>
		/// Used to detect is the Windows version x64
		/// </summary>
		/// <param name="hProcess"></param>
		/// <param name="lpSystemInfo"></param>
		/// <returns></returns>
		[DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool lpSystemInfo);

		/// <summary>
		/// Checks if on 64-bit version of Windows.
		/// </summary>
		/// <returns></returns>
		public static bool Is64Bit()
		{
			bool retVal;

			IsWow64Process(Process.GetCurrentProcess().Handle, out retVal);

			return retVal;
		}

		/// <summary>
		/// Handles send button click event to send bug details to the server.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnSend_Click(object sender, EventArgs e)
		{
			// OS info
			string os = Environment.OSVersion.VersionString;
			if (Is64Bit())
				os += " 64-bit";
			else
				os += " 32-bit";

			// MAC address
			string mac = String.Empty;
			NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface intrfce in interfaces)
			{
				mac = intrfce.GetPhysicalAddress().ToString();
				if (!string.IsNullOrEmpty(mac))
					break;
			}

			// IP address
			IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
			string ip = ipEntry.AddressList[0].ToString();

			// Host name
			string hostName = Dns.GetHostName();

			string stackTraceTrimmed;
			try
			{
				// Bug stack trace
				int trimLimit = Program.BugStackTrace.IndexOf("line");
				stackTraceTrimmed = Program.BugStackTrace.Substring(0, trimLimit + 10);
			}
			catch (Exception)
			{
				stackTraceTrimmed = Program.BugStackTrace.Length > 100
				                    	? Program.BugStackTrace.Substring(0, 100)
				                    	: Program.BugStackTrace;
			}

			// Hide window
			ShowInTaskbar = false;
			Hide();

			// Submit report
			var binding = new BasicHttpBinding();
			var address = new EndpointAddress("http://service.freemiumlab.com/ReportService.svc");

			ReportServiceClient client = null;
			try
			{
				client = new ReportServiceClient(binding, address);
			}
			catch
			{
				// MessageBox.Show(ex.Message + " " + ex.Source + " " + ex.InnerException);
			}

			try
			{
				string str_ver = String.Empty;
				string str_mac = String.Empty;
				string str_ip = String.Empty;
				string str_os = String.Empty;
				string str_host = String.Empty;
				string str_stacktrace = String.Empty;
				string str_bugtype = String.Empty;
				string str_bugmsg = String.Empty;
				string str_userinput = String.Empty;
				string str_target = String.Empty;
				string str_source = String.Empty;

				try
				{
					if (Program.Version.Length > 0) str_ver = Program.Version;
				}
				catch
				{
				}
				try
				{
					if (mac.Length > 0) str_mac = mac;
				}
				catch
				{
				}
				try
				{
					if (ip.Length > 0) str_ip = ip;
				}
				catch
				{
				}
				try
				{
					if (os.Length > 0) str_os = os;
				}
				catch
				{
				}
				try
				{
					if (hostName.Length > 0) str_host = hostName;
				}
				catch
				{
				}
				try
				{
					if (stackTraceTrimmed.Length > 0) str_stacktrace = stackTraceTrimmed;
				}
				catch
				{
				}
				try
				{
					if (Program.BugType.Length > 0) str_bugtype = Program.BugType;
				}
				catch
				{
				}
				try
				{
					if (Program.BugMessage.Length > 0) str_bugmsg = Program.BugMessage;
				}
				catch
				{
				}
				try
				{
					if (txtContext.Text.Length > 0) str_userinput = txtContext.Text;
				}
				catch
				{
				}
				try
				{
					if (Program.BugTargetSite.Length > 0) str_target = Program.BugTargetSite;
				}
				catch
				{
				}
				try
				{
					if (Program.BugSource.Length > 0) str_source = Program.BugSource;
				}
				catch
				{
				}

				client.SubmitBugReport(
					str_ver,
					str_mac,
					str_ip,
					str_os,
					str_host,
					str_stacktrace,
					str_bugtype,
					str_bugmsg,
					str_userinput,
					str_target,
					str_source
					);
			}
			catch
			{
				// MessageBox.Show(ex.Message + " " + ex.Source + " " + ex.InnerException);
			}
			client.Close();

			// Close program
			Application.Exit();
		}

		/// <summary>
		/// Handles the details button click event to show the details of the bug.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void detailsBtn_Click(object sender, EventArgs e)
		{
			if ((string) (btnDetails.Tag) == "Show")
			{
				Height = 333;
				btnDetails.Tag = "Hide";
				btnDetails.Text = "<< &Details";
			}
			else
			{
				Height = 190;
				btnDetails.Tag = "Show";
				btnDetails.Text = "&Details >>";
			}
		}
	}
}