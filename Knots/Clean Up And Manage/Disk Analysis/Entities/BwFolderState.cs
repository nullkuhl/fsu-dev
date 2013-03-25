namespace DiskAnalysis.Entities
{
    /// <summary>
    /// Folder state model
    /// </summary>
    public class BwFolderState
    {
        /// <summary>
        /// Current folder
        /// </summary>
        public AppFolder CurrentFolder { get; set; }
        /// <summary>
        /// Subfolder
        /// </summary>
        public AppFolder SubFolder { get; set; }
    }
}