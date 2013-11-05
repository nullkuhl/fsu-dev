using System.Collections.Generic;
using Microsoft.Win32;

namespace BrowserAssistant
{
    /// <summary>
    /// IE BHO model
    /// </summary>
    public class ExplorerBHO
    {
        readonly bool enabledOld;

        /// <summary>
        /// constructor for ExplorerBHO
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="enabled"></param>
        public ExplorerBHO(string id, string name, string path, bool enabled)
        {
            Id = id;
            Name = name;
            Path = path;
            IsEnabled = enabledOld = enabled;
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
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Is enabled
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Has changed
        /// </summary>
        public bool HasChanged
        {
            get { return IsEnabled != enabledOld; }
        }

        /// <summary>
        /// get a list of internet explorer bho
        /// </summary>
        /// <returns></returns>
        public static List<ExplorerBHO> List()
        {
            List<ExplorerBHO> result = new List<ExplorerBHO>();
            try
            {
                string[] CLSIDs = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows")
                    .OpenSubKey("CurrentVersion").OpenSubKey("explorer").OpenSubKey("Browser Helper Objects").GetSubKeyNames();

                foreach (string clsid in CLSIDs)
                {
                    try
                    {
                        bool enabled = !clsid.StartsWith("Disabled:");

                        string name = Helper.GetNameFromClsid(enabled ? clsid : clsid.Substring(9));
                        if (string.IsNullOrEmpty(name)) continue;

                        string path = Helper.GetPathFromClsid(enabled ? clsid : clsid.Substring(9));

                        result.Add(new ExplorerBHO(enabled ? clsid : clsid.Substring(9), name, path, enabled));
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// save changes to internet explorer bho
        /// </summary>
        /// <param name="bhos"></param>
        public static void SaveChanges(IEnumerable<ExplorerBHO> bhos)
        {
            foreach (ExplorerBHO bho in bhos)
            {
                try
                {
                    using (RegistryKey bhosKey = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows")
                        .OpenSubKey("CurrentVersion").OpenSubKey("explorer").OpenSubKey("Browser Helper Objects", true))
                    {
                        if (bho.IsEnabled)
                            RenameKey(bhosKey, "Disabled:" + bho.Id, bho.Id);
                        else
                            RenameKey(bhosKey, bho.Id, "Disabled:" + bho.Id);
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// rename registry entry
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        static void RenameKey(RegistryKey parent, string oldName, string newName)
        {
            try
            {
                using (RegistryKey oldKey = parent.OpenSubKey(oldName))
                {
                    using (RegistryKey newKey = parent.CreateSubKey(newName))
                    {

                        if (oldKey != null)
                            foreach (string valueName in oldKey.GetValueNames())
                            {
                                object value = oldKey.GetValue(valueName);
                                RegistryValueKind kind = oldKey.GetValueKind(valueName);

                                if (newKey != null) newKey.SetValue(valueName, value, kind);
                            }

                        parent.DeleteSubKeyTree(oldName);
                    }
                }
            }
            catch
            {
            }
        }
    }
}