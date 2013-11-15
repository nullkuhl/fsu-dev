namespace FreemiumUtilities.IEToolbarRemover
{
    public class ExplorerToolbarAndAddOn
    {
        readonly bool enabledOld;

        /// <summary>
        /// constructor for ExplorerToolbar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="enabled"></param>
        public ExplorerToolbarAndAddOn(string id, string name, string path, bool enabled, string typeName)
        {
            Id = id;
            Name = name;
            Path = path;
            IsEnabled = enabledOld = enabled;
            TypeName = typeName;
        }

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Path        
        public string Path { get; set; }

        /// <summary>
        /// Is enabled
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Type Name
        /// </summary>
        public string TypeName { get; set; }
    }
}
