/// <summary>
/// The <see cref="FreeGamingBooster.Models"/> namespace contains a set of model classes
/// of the <see cref="FreeGamingBooster"/> project
/// </summary>
namespace FreeGamingBooster.Models
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