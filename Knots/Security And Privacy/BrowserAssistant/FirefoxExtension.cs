using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace BrowserAssistant
{
    /// <summary>
    /// Firefox extension model
    /// </summary>
    public class FirefoxExtension : IBrowserExtension
    {
        static string defProfilePath;
        static bool ff4Mode;
        static XDocument extensionsRdf;
        static SQLiteConnection sqliteConn;

        readonly XElement descElement;
        readonly string userDisabled;
        string changeRequired;
        bool isEnabled;

        /// <summary>
        /// constructor for FirefoxExtension
        /// </summary>
        /// <param name="desc"></param>
        public FirefoxExtension(XElement desc)
        {
            descElement = desc;
            Id = desc.Attributes().First(a => a.Name.LocalName == "about").Value.Substring(17);
            Name = desc.Attributes().First(a => a.Name.LocalName == "name").Value;
            Version = desc.Attributes().First(a => a.Name.LocalName == "version").Value;
            try
            {
                userDisabled = desc.Attributes().First(a => a.Name.LocalName == "userDisabled").Value;
            }
            catch
            {
                userDisabled = "false";
            }
            isEnabled = userDisabled == "false" || userDisabled == "needs-enable";
        }

        /// <summary>
        /// constructor for FirefoxExtension
        /// </summary>
        /// <param name="r"></param>
        public FirefoxExtension(DbDataRecord r)
        {
            Id = r["id"].ToString();
            Name = r["name"].ToString();
            Version = r["version"].ToString();
            userDisabled = (long)r["userDisabled"] == 1 ? "true" : "false";
            isEnabled = userDisabled == "false";
        }

        /// <summary>
        /// Gets the Firefox profile path
        /// </summary>
        public static string DefProfilePath
        {
            get
            {
                if (string.IsNullOrEmpty(defProfilePath))
                {
                    try
                    {
                        string appDataPath = Environment.GetEnvironmentVariable("AppData");
                        defProfilePath = File.ReadAllLines(appDataPath + @"\Mozilla\Firefox\profiles.ini")
                            .First(s => s.StartsWith("Path=")).Substring(5).Replace('/', '\\')
                            .Insert(0, appDataPath + @"\Mozilla\Firefox\");
                    }
                    catch
                    {
                    }
                }

                return defProfilePath;
            }
        }

        #region IBrowserExtension Members

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Is enabled
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (value && userDisabled == "true")
                    changeRequired = "needs-enable";
                else if (value && userDisabled == "needs-disable")
                    changeRequired = "false";
                else if (!value && userDisabled == "false")
                    changeRequired = "needs-disable";
                else if (!value && userDisabled == "needs-enable")
                    changeRequired = "true";
                else
                    changeRequired = null;

                isEnabled = value;
            }
        }

        #endregion

        /// <summary>
        /// get a list of firefox extensions
        /// </summary>
        /// <returns></returns>
        public static FirefoxExtension[] List()
        {
            try
            {
                ff4Mode = File.Exists(DefProfilePath + "\\extensions.sqlite");
                if (ff4Mode)
                {
                    if (sqliteConn == null)
                    {
                        sqliteConn = new SQLiteConnection("Data Source=" + DefProfilePath + "\\extensions.sqlite;Version=3;Compress=True;");
                        sqliteConn.Open();
                    }

                    SQLiteCommand command = sqliteConn.CreateCommand();
                    command.CommandText = "SELECT addon.id, locale.name, addon.version, addon.userDisabled "
                                          +
                                          "FROM addon INNER JOIN locale ON addon.defaultLocale = locale.id WHERE addon.type != 'theme'";

                    return command.ExecuteReader().Cast<DbDataRecord>().Select(r => new FirefoxExtension(r)).ToArray();
                }
                extensionsRdf = XDocument.Parse(File.ReadAllText(DefProfilePath + "\\extensions.rdf"));

                if (extensionsRdf.Root != null)
                {
                    string[] allAddonIds = extensionsRdf.Root.Elements().First(el => el.Name.LocalName == "Seq")
                        .Elements().Select(el => el.FirstAttribute.Value).ToArray();

                    FirefoxExtension[] allAddons = extensionsRdf.Root.Elements()
                        .Where(el => allAddonIds.Contains(el.Attributes().First(a => a.Name.LocalName == "about").Value))
                        .Select(el => new FirefoxExtension(el)).ToArray();

                    return allAddons;
                }
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }

            return new FirefoxExtension[] { };
        }

        /// <summary>
        /// check if firefox installed
        /// </summary>
        /// <returns></returns>
        public static bool IsBrowserInstalled()
        {
            string strFilepath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) +
                                 "\\Mozilla Firefox\\firefox.exe";
            return File.Exists(strFilepath);
        }

        /// <summary>
        /// save changes to firefox
        /// </summary>
        /// <param name="extensions"></param>
        public static void SaveChanges(IEnumerable<FirefoxExtension> extensions)
        {
            if (ff4Mode)
            {
                foreach (FirefoxExtension a in extensions.Where(ex => ex.changeRequired != null))
                {
                    SQLiteCommand command = sqliteConn.CreateCommand();
                    command.CommandText = "UPDATE addon SET userDisabled=" + (a.IsEnabled ? "0" : "1")
                                          + ",active=" + (a.IsEnabled ? "1" : "0") + " WHERE addon.id=='" + a.Id + "'";
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                foreach (FirefoxExtension a in extensions.Where(ex => ex.changeRequired != null))
                {
                    if (a.changeRequired == "false")
                        a.descElement.Attributes().First(at => at.Name.LocalName == "userDisabled").Remove();
                    else if (a.userDisabled != "false")
                        a.descElement.Attributes().First(at => at.Name.LocalName == "userDisabled").Value = a.changeRequired;
                    else
                        a.descElement.Add(new XAttribute("{http://www.mozilla.org/2004/em-rdf#}userDisabled", a.changeRequired));
                }
                extensionsRdf.Save(DefProfilePath + "\\extensions.rdf");
            }

            try
            {
                File.Delete(DefProfilePath + "\\extensions.ini");
                File.Delete(DefProfilePath + "\\extensions.cache");
            }
            catch
            {
            }
        }
    }
}