using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.Windows.Forms;
using FreemiumUtil.ReportingServiceReference;

namespace FreemiumUtil
{
	/// <summary>
	/// Class that contains a methods for updating the main app
	/// </summary>
	public class Updater
	{
		#region Delegates

		public delegate void ProgressCallback(int percent, long bytesReceived, long totalBytes);

		#endregion

		#region Events

		public event EventHandler DownloadFileCompleted;

		#endregion

		#region Properties

		public ProgressCallback ProgressListener { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// check if app is up to date
		/// </summary>
		/// <param name="latestVerLink"></param>
		/// <returns></returns>
		public bool IsUpToDate(out string latestVerLink)
		{
			string currVersion = CfgFile.Get("Version");

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://service.freemiumlab.com/ReportService.svc");

            ReportServiceClient client = new ReportServiceClient(binding, address);
			string[] latestVersion;
			try
			{
				latestVersion = client.GetLatestVersion();
				client.Close();
			}
			catch
			{
				latestVerLink = string.Empty;
				return true;
			}
			latestVerLink = latestVersion[1];
			return latestVersion[0] == currVersion;
		}

		/// <summary>
		/// download update
		/// </summary>
		/// <param name="downloadLink"></param>
		public void Download(string downloadLink)
		{
            WebClient webClient = new WebClient();

			webClient.DownloadFileCompleted += webClient_DownloadFileCompleted;
			webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;

			string saveDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FreemiumDownloads";
			string saveFile = downloadLink.Substring(downloadLink.LastIndexOf("/") + 1);

			if (!Directory.Exists(saveDir))
			{
				Directory.CreateDirectory(saveDir);
			}

			webClient.DownloadFileAsync(new Uri(downloadLink), saveDir + "\\" + saveFile);
		}

		#endregion

		#region Internal Methods

		/// <summary>
		/// run app update
		/// </summary>
		internal void RunUpdater()
		{
			string downloadLink;
			if (IsUpToDate(out downloadLink))
			{
				MessageBox.Show("Freemium Utilities is up-to-date!", "Freemium Utilities Update",
								MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				Download(downloadLink);
			}
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// handle DownloadProgressChanged event to show download progress
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			ProgressListener(e.ProgressPercentage, e.BytesReceived, e.TotalBytesToReceive);
		}

		/// <summary>
		/// handle DownloadFileCompleted event to complete update
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			DownloadFileCompleted.Invoke(this, null);
		}

		#endregion
	}
}