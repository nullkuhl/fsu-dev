using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace BrowserAssistant
{
    /// <summary>
    /// IE extension model
    /// </summary>
    public class ExplorerExtension
    {
        readonly bool enabledOld;

        /// <summary>
        /// constructor for ExplorerExtension
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="enabled"></param>
        public ExplorerExtension(string id, string name, string path, bool enabled)
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
        /// check if internet explorer installed
        /// </summary>
        /// <returns></returns>
        public static bool IsBrowserInstalled()
        {
            string strFilepath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) +
                                 "\\Internet Explorer\\iexplore.exe";
            return File.Exists(strFilepath);
        }

        /// <summary>
        /// get a list of internet explorer extensions
        /// </summary>
        /// <returns></returns>
        public static List<ExplorerExtension> List()
        {
            List<ExplorerExtension> result = new List<ExplorerExtension>();

            try
            {
                if (Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft")
                    .OpenSubKey("Internet Explorer").OpenSubKey("Extensions") == null)
                    return result;

                string[] extIds = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft")
                    .OpenSubKey("Internet Explorer").OpenSubKey("Extensions").GetSubKeyNames();

                foreach (string id in extIds)
                {
                    string name = null;
                    string path = "(unable to retrieve path)";

                    object clsid = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Internet Explorer")
                        .OpenSubKey("Extensions").OpenSubKey(id).GetValue("ClsidExtension");

                    if (clsid == null)
                        clsid = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Internet Explorer")
                        .OpenSubKey("Extensions").OpenSubKey(id).GetValue("CLSID");

                    if (clsid != null)
                    {
                        name = Helper.GetNameFromClsid((string)clsid);
                        path = Helper.GetPathFromClsid((string)clsid);
                    }
                    else
                    {
                        var exec =
                            (string)Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Internet Explorer")
                                        .OpenSubKey("Extensions").OpenSubKey(id).GetValue("Exec");
                        if (exec != null) path = exec;
                    }

                    if (string.IsNullOrEmpty(name))
                        name =
                            (string)Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Internet Explorer")
                                        .OpenSubKey("Extensions").OpenSubKey(id).GetValue("ButtonText");

                    if (name == null)
                        name =
                            (string)Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Internet Explorer")
                                        .OpenSubKey("Extensions").OpenSubKey(id).GetValue("MenuText");

                    RegistryKey winCurrVer = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows")
                                    .OpenSubKey("CurrentVersion", true);

                    if (winCurrVer.OpenSubKey("Ext") == null)
                        winCurrVer.CreateSubKey("Ext");
                    if (winCurrVer.OpenSubKey("Ext").OpenSubKey("Settings") == null)
                        winCurrVer.OpenSubKey("Ext", true).CreateSubKey("Settings");

                    bool enabled = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows")
                                    .OpenSubKey("CurrentVersion").OpenSubKey("Ext").OpenSubKey("Settings").OpenSubKey(id) == null;

                    result.Add(new ExplorerExtension(id, name, path, enabled));
                }
            }
            catch (Exception e)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }

            return result;
        }

        /// <summary>
        /// save change to internet explorer extensions
        /// </summary>
        /// <param name="extensions"></param>
        public static void SaveChanges(IEnumerable<ExplorerExtension> extensions)
        {
            foreach (ExplorerExtension ext in extensions)
            {
                try
                {
                    if (ext.IsEnabled)
                    {
                        Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows")
                            .OpenSubKey("CurrentVersion").OpenSubKey("Ext").OpenSubKey("Settings", true).DeleteSubKeyTree(ext.Id);
                    }
                    else
                    {
                        RegistryKey newKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows")
                            .OpenSubKey("CurrentVersion").OpenSubKey("Ext").OpenSubKey("Settings", true).CreateSubKey(ext.Id);

                        if (newKey != null)
                        {
                            newKey.SetValue("Flags", "1", RegistryValueKind.DWord);
                            newKey.SetValue("Version", "*", RegistryValueKind.String);
                        }
                    }
                }
                catch
                {
                }
            }
        }
    }
}