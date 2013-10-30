/// <summary>
/// The <see cref="FreemiumUtilities.Models"/> namespace contains a set of model classes
/// of the <see cref="FreemiumUtilities"/> project
/// </summary>
namespace FreemiumUtilities.Models
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