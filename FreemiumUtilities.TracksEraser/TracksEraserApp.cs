using System.Windows.Forms;
using FreemiumUtilities.Infrastructure;

/// <summary>
/// The <see cref="FreemiumUtilities.TracksEraser"/> namespace defines a Tracks Eraser 1 Click-Maintenance application
/// </summary>
namespace FreemiumUtilities.TracksEraser
{
	/// <summary>
	/// Tracks Eraser 1 Click-Maintenance application <see cref="OneClickApp"/> implementation
	/// </summary>
	public class TracksEraserApp : OneClickApp
	{
		/// <summary>
		/// <see cref="FrmTrackOptions"/> instance
		/// </summary>
		public frmTrackOptions FrmTrackOptions = new frmTrackOptions();

		/// <summary>
		/// <see cref="frmTrackSelector"/> instance
		/// </summary>
		public frmTrackSelector FrmTrackSel;

		/// <summary>
		/// Needed for displaying TreeView icons for Windows XP
		/// </summary>
		public TracksEraserApp()
		{
			Application.EnableVisualStyles();
			FrmTrackSel = new frmTrackSelector();
		}

		/// <summary>
		/// Problems count
		/// </summary>
		public override int ProblemsCount { get; set; }

		/// <summary>
		/// start scanning
		/// </summary>
		/// <param name="callback"></param>
		/// <param name="complete"></param>
		/// <param name="cancelComplete"></param>
		/// <param name="fixAfterScan"></param>
		public override void StartScan(ProgressUpdate callback, ScanComplete complete, CancelComplete cancelComplete,
		                               bool fixAfterScan)
		{
			FrmTrackSel.Callback = callback;
			FrmTrackSel.Complete = complete;
			FrmTrackSel.CancelComplete = cancelComplete;
			FrmTrackSel.ScanFiles(fixAfterScan);
		}

		/// <summary>
		/// cancel scanning
		/// </summary>
		public override void CancelScan()
		{
			FrmTrackSel.CancelScan();
		}

		/// <summary>
		/// start erasing
		/// </summary>
		/// <param name="callback"></param>
		public override void StartFix(ProgressUpdate callback)
		{
			FrmTrackSel.StartFix();
		}

		/// <summary>
		/// cancel erasing
		/// </summary>
		public override void CancelFix()
		{
			FrmTrackSel.CancelFix();
		}
	}
}