namespace FreemiumUtilities.ChromeToolbarRemover
{
	/// <summary>
	/// Browser extension model
	/// </summary>
	public interface IBrowserExtension
	{
		string Id { get; }
		string Name { get; }
		string Version { get; }
		bool IsEnabled { get; set; }
	}
}