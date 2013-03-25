namespace FreemiumUtilities.Infrastructure
{
	/// <summary>
	/// One Click Application abstract class that defines a basic fields and methods which every One Click Application implements
	/// </summary>
	public abstract class OneClickApp
	{
		public abstract int ProblemsCount { get; set; }

		public abstract void StartScan(ProgressUpdate callback, ScanComplete complete, CancelComplete cancelComplete,
		                               bool fixAfterScan);

		public abstract void CancelScan();
		public abstract void StartFix(ProgressUpdate callback);
		public abstract void CancelFix();
	}
}