using System;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using FreemiumUtil;

namespace ProcessManager
{
	/// <summary>
	/// New PC details form of the Process Manager knot
	/// </summary>
	public partial class FormNewPCDetails : Form
	{
		Button btnbrowse;
		Button btncancel;
		Button btnok;
		CultureInfo culture;
		Label lblOpen;
		OpenFileDialog ofdMain;
		TextBox txtapplnpath;

		/// <summary>
		/// constructor for frmnewprcdetails
		/// </summary>
		public FormNewPCDetails()
		{
			InitializeComponent();
		}

		/// <summary>
		/// constructor for frmnewprcdetails
		/// </summary>
		/// <param name="mcname"></param>
		public FormNewPCDetails(string mcname)
		{
			InitializeComponent();
			lblOpen.Text = mcname;
			if (lblOpen.Text.ToUpper() == "Enter Machine Name".ToUpper())
			{
				Height = 100;
				Width = 400;
				txtapplnpath.Left = 140;
				lblOpen.Width = 120;
				txtapplnpath.Width = 250;
				btnbrowse.Visible = false;
				btnok.Left = btnok.Left + 80;
				btncancel.Left = btncancel.Left + 90;
			}
		}

		/// <summary>
		/// handle Click event to cancel form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btncancel_Click(object sender, EventArgs e)
		{
			FormProcessManager.Newprocpathandparm = "";
			FormProcessManager.ObjNewProcess.Close();
			DialogResult = DialogResult.Cancel;
		}

		/// <summary>
		/// handle Click event to show file browser
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnbrowse_Click(object sender, EventArgs e)
		{
			try
			{
				ofdMain.CheckFileExists = false;
				ofdMain.ShowDialog();
				txtapplnpath.Text = ofdMain.FileName;
			}

			catch (Exception ex)
			{
                // ToDo: send exception details via SmartAssembly bug reporting!
			}
		}

		void btnok_Click(object sender, EventArgs e)
		{
			if (lblOpen.Text.ToUpper() == "Enter Machine Name".ToUpper())
			{
				if (txtapplnpath.Text.Trim() == "")
				{
					txtapplnpath.Text = ".";
				}
				FormProcessManager.Mcname = txtapplnpath.Text.Trim();
			}
			else
			{
				FormProcessManager.Newprocpathandparm = txtapplnpath.Text.Trim();
			}
			DialogResult = DialogResult.OK;
			FormProcessManager.ObjNewProcess.Close();
		}

		/// <summary>
		/// initialize frmnewprcdetails
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void frmnewprcdetails_Load(object sender, EventArgs e)
		{
			culture = new CultureInfo(CfgFile.Get("Lang"));
			SetCulture(culture);
		}

		/// <summary>
		/// change currnet language
		/// </summary>
		/// <param name="culture"></param>
		void SetCulture(CultureInfo culture)
		{
			var rm = new ResourceManager("ProcessManager.Resources", typeof (FormNewPCDetails).Assembly);
			btnok.Text = rm.GetString("&ok", culture);
			btncancel.Text = rm.GetString("&cancel", culture);
			btnbrowse.Text = rm.GetString("browse", culture);
			lblOpen.Text = rm.GetString("open", culture) + " : ";
		}
	}
}