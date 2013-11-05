/// <summary>
/// The <see cref="PCCleaner.Models"/> namespace contains a set of model classes
/// of the <see cref="PCCleaner"/> project
/// </summary>
namespace PCCleaner.Models
{
    /// <summary>
    /// OneClickApp statuses collection
    /// </summary>
    public enum OneClickAppStatus
    {
        NotStarted,
        ScanStarted,
        ScanFinishedOK,
        ScanFinishedError,
        FixStarted,
        FixFinished
    };
}