/// <summary>
/// The <see cref="FreeToolbarRemover.Models"/> namespace contains a set of model classes
/// of the <see cref="FreeToolbarRemover"/> project
/// </summary>
namespace FreeToolbarRemover.Models
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