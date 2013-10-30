namespace FreemiumUtilities.Infrastructure
{
	/// <summary>
	/// Progress update delegate
	/// </summary>
	/// <param name="progressPercentage"></param>
	/// <param name="fileName"></param>
	public delegate void ProgressUpdate(int progressPercentage, string fileName);
}