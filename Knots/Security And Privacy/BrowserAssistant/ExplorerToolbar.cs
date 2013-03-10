using System.Collections.Generic;
using Microsoft.Win32;

namespace BrowserAssistant
{
	/// <summary>
	/// IE Toolbar model
	/// </summary>
	public class ExplorerToolbar
	{
		readonly bool enabledOld;

		/// <summary>
		/// constructor for ExplorerToolbar
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		/// <param name="path"></param>
		/// <param name="enabled"></param>
		public ExplorerToolbar(string id, string name, string path, bool enabled)
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
		/// get a list of internet explorer toolbars
		/// </summary>
		/// <returns></returns>
		public static List<ExplorerToolbar> List()
		{
            var ieRegKey = Registry.LocalMachine.OpenSubKey("Software")
                .OpenSubKey("Microsoft").OpenSubKey("Internet Explorer", true);
            if (ieRegKey.OpenSubKey("Toolbar") == null)
                ieRegKey.CreateSubKey("Toolbar");
            if (ieRegKey.OpenSubKey("ToolbarDisabled") == null)
                ieRegKey.CreateSubKey("ToolbarDisabled");

            string[] enabledCLSIDs = ieRegKey.OpenSubKey("Toolbar").GetValueNames();
            string[] disabledCLSIDs = ieRegKey.OpenSubKey("ToolbarDisabled").GetValueNames();

			var result = new List<ExplorerToolbar>();

			foreach (string clsid in enabledCLSIDs)
			{
				try
				{
					string name = Helper.GetNameFromClsid(clsid);
					if (string.IsNullOrEmpty(name)) continue;

					string path = Helper.GetPathFromClsid(clsid);

					result.Add(new ExplorerToolbar(clsid, name, path, true));
				}
				catch
				{
				}
			}
            foreach (string clsid in disabledCLSIDs)
            {
                try
                {
                    string name = Helper.GetNameFromClsid(clsid);
                    if (string.IsNullOrEmpty(name)) continue;

                    string path = Helper.GetPathFromClsid(clsid);

                    result.Add(new ExplorerToolbar(clsid, name, path, false));
                }
                catch
                {
                }
            }

			return result;
		}

		/// <summary>
		/// save changes to internet explorer toolbar
		/// </summary>
		/// <param name="toolbars"></param>
		public static void SaveChanges(IEnumerable<ExplorerToolbar> toolbars)
		{
			foreach (ExplorerToolbar toolbar in toolbars)
			{
                RegistryKey ieKey = Registry.LocalMachine.OpenSubKey("Software")
                .OpenSubKey("Microsoft").OpenSubKey("Internet Explorer");

                if (toolbar.IsEnabled)
                    moveToEnabled(ieKey, toolbar.Id);
                else
                    moveToDisabled(ieKey, toolbar.Id);
			}
		}

        /// <summary>
        /// moves reg value from Subkey of disabled entries to Subkey of enabled entries
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="valName"></param>
        static void moveToEnabled(RegistryKey parent, string valName)
        {
            var enabledKey = parent.OpenSubKey("Toolbar", true);
            var disabledKey = parent.OpenSubKey("ToolbarDisabled", true);

            var valValue = disabledKey.GetValue(valName);
            var valKind = disabledKey.GetValueKind(valName);

            enabledKey.SetValue(valName, valValue, valKind);
            disabledKey.DeleteValue(valName);
        }

        /// <summary>
        /// moves reg value from Subkey of enabled entries to Subkey of disabled entries
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="valName"></param>
        static void moveToDisabled(RegistryKey parent, string valName)
        {
            var enabledKey = parent.OpenSubKey("Toolbar", true);
            var disabledKey = parent.OpenSubKey("ToolbarDisabled", true);

            var valValue = enabledKey.GetValue(valName);
            var valKind = enabledKey.GetValueKind(valName);

            disabledKey.SetValue(valName, valValue, valKind);
            enabledKey.DeleteValue(valName);
        }

		/// <summary>
		/// rename registry value [depricated]
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="oldName"></param>
		/// <param name="newName"></param>
		static void RenameVal(RegistryKey parent, string oldName, string newName)
		{
            var val = parent.GetValue(oldName);
            var kind = parent.GetValueKind(oldName);
            
            parent.SetValue(newName, val, kind);
            parent.DeleteValue(oldName);
		}
	}
}