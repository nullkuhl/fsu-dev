/// <summary>
/// The <see cref="RegistryCleaner.Models"/> namespace contains the model classes of the Registry Cleaner knot
/// </summary>
namespace RegistryCleaner.Models
{
    /// <summary>
    /// Operation statuses
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