/// <summary>
/// The <see cref="FreemiumUtilities.RegCleaner.Models"/> namespace contains a model classes
/// used in the <see cref="FreemiumUtilities.RegCleaner"/> project
/// </summary>
namespace FreemiumUtilities.RegCleaner.Models
{
    /// <summary>
    /// Operation statuses collection
    /// </summary>
    public enum OperationStatus
    {
        NotStarted,
        Started,
        Canceled,
        ScanFinished,
        CleaningFinished
    };
}
