using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Threading;
using System.Resources;
using System.Globalization;
using FreemiumUtil;

namespace Disk_Cleaner
{
    /// <summary>
    /// File to delete model
    /// </summary>
    public class DeleteFile
    {
        /// <summary>
        /// Is file choosed to deletion
        /// </summary>
        public bool Delete;
        /// <summary>
        /// File info
        /// </summary>
        public FileInfo Info;
        /// <summary>
        /// File name
        /// </summary>
        public string Name;

        /// <summary>
        /// DeleteFile constructor
        /// </summary>
        /// <param name="info">File info</param>
        /// <param name="delete">Is file choosed to deletion</param>
        public DeleteFile(FileInfo info, bool delete)
        {
            Info = info;
            Name = info.FullName;
            Delete = delete;
        }

        /// <summary>
        /// DeleteFile constructor
        /// </summary>
        /// <param name="name">File name</param>
        /// <param name="delete">Is file choosed to deletion</param>
        public DeleteFile(string name, bool delete)
        {
            Info = null;
            Name = name;
            Delete = delete;
        }
    }

    /// <summary>
    /// Option model
    /// </summary>
    public class Option
    {
        /// <summary>
        /// Is option checked
        /// </summary>
        public bool Checked;
        /// <summary>
        /// Description
        /// </summary>
        public string Description;
        /// <summary>
        /// Value
        /// </summary>
        public string Value;

        /// <summary>
        /// Option constructor
        /// </summary>
        public Option()
        {
            Value = Description = string.Empty;
            Checked = true;
        }

        /// <summary>
        /// Option constructor
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="description">Description</param>
        /// <param name="checked">Is option checked</param>
        public Option(
            string value,
            string description,
            bool @checked
            )
        {
            Value = value;
            Description = description;
            Checked = @checked;
        }
    }

    /// <summary>
    /// Preferences class
    /// </summary>
    public static class Preferences
    {
        public static ResourceManager rm = new ResourceManager("Disk_Cleaner.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        /// <summary>
        /// Preferences file name
        /// </summary>
        public static readonly string Filename = String.Format("configuration-{0}.xml", Thread.CurrentThread.CurrentUICulture.Name);
        /// <summary>
        /// Default extensions collection
        /// </summary>
        public static readonly List<Option> DefaultExtensions = new List<Option>();
        /// <summary>
        /// Default excluded items collection
        /// </summary>
        public static readonly List<Option> DefaultExclude = new List<Option>();
        /// <summary>
        /// Default included items collection
        /// </summary>
        public static readonly List<Option> DefaultInclude = new List<Option>();

        /// <summary>
        /// Excluded item's pathes collection
        /// </summary>
        public static List<Option> PathExcluded = new List<Option>();
        /// <summary>
        /// Included item's pathes collection
        /// </summary>
        public static List<Option> PathIncluded = new List<Option>();
        /// <summary>
        /// File extensions collection
        /// </summary>
        public static List<Option> FileExtensions = new List<Option>();
        /// <summary>
        /// Is temporary folder should be scanned
        /// </summary>
        public static bool ScanTemporaryFolder = true;
        /// <summary>
        /// Is zero files should be selected
        /// </summary>
        public static bool FileSelectZero = true;
        /// <summary>
        /// Checked names
        /// </summary>
        public static string CheckedNames = string.Empty;
        /// <summary>
        /// Backup junk
        /// </summary>
        public static bool BackupJunk;
        /// <summary>
        /// Backup zero
        /// </summary>
        public static bool BackupZero = true;

        static void DefaultsLoad()
        {
            DefaultExtensions.Clear();
            DefaultExtensions.Add(new Option("*.tmp", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.temp", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.nch", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.---", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.~*", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("~*.*", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.??$", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.___", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.~mp", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*._mp", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.$db", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.?$?", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.??~", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.?~?", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.db$", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.^", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*._dd", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*._detmp", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.$$$", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("0*.nch", rm.GetString("temporary_file"), true));
            DefaultExtensions.Add(new Option("*.old", rm.GetString("temp_bkup_file"), true));
            DefaultExtensions.Add(new Option("*.bak", rm.GetString("temp_bkup_file"), true));
            DefaultExtensions.Add(new Option("*.chk", rm.GetString("saved_disk_error_data"), true));
            DefaultExtensions.Add(new Option("*.gid", rm.GetString("temp_help_file_search_data"), true));
            DefaultExtensions.Add(new Option("mscreate.dir", rm.GetString("temp_dir_info"), true));
            DefaultExtensions.Add(new Option("*.wbk", rm.GetString("office_bkup_file"), true));
            DefaultExtensions.Add(new Option("*.fts", rm.GetString("temp_help_file_search_data"), true));           
            DefaultExtensions.Add(new Option("*.ftg", rm.GetString("win_help_sys_temp_file"), true));
            DefaultExtensions.Add(new Option("*log.txt", rm.GetString("log_file"), true));
            DefaultExtensions.Add(new Option("*.err", rm.GetString("error_file"), true));
            DefaultExtensions.Add(new Option("*.dmp", rm.GetString("memory_dump_files"), true));
            DefaultExtensions.Add(new Option("*.prv", rm.GetString("outdated_file"), true));
            DefaultExtensions.Add(new Option("*.ilk", rm.GetString("intermediate_file"), true));
            DefaultExtensions.Add(new Option("*.aps", rm.GetString("intermediate_file"), true));
            DefaultExtensions.Add(new Option("*.ncb", rm.GetString("intermediate_file"), true));
            DefaultExtensions.Add(new Option("*.pch",rm.GetString("intermediate_file"), true));
            DefaultExtensions.Add(new Option("chklist.*", rm.GetString("temporary_file_created_during_setup"), true));

            DefaultExclude.Add(new Option("?:\\Program Files\\Common Files", String.Empty, true));
            DefaultExclude.Add(new Option("?:\\Program Files\\Uninstall Information", String.Empty, true));
            DefaultExclude.Add(new Option("?:\\win*\\security", String.Empty, true));
            DefaultExclude.Add(new Option("*\\sendto", String.Empty, true));
            DefaultExclude.Add(new Option("*\\i386", String.Empty, true));
            DefaultExclude.Add(new Option("*\\resources\\themes", String.Empty, true));
            DefaultExclude.Add(new Option("*\\temporary internet files", String.Empty, true));
            DefaultExclude.Add(new Option("*\\microsoft\\office\\data", String.Empty, true));
            DefaultExclude.Add(new Option("*\\microsoft\\money\\*\\webcache", String.Empty, true));
            DefaultExclude.Add(new Option("*\\chaos32", String.Empty, true));
            DefaultExclude.Add(new Option("*\\paprport\\data", String.Empty, true));
            DefaultExclude.Add(new Option("*\\symantec shared\\virusdefs", String.Empty, true));
            DefaultExclude.Add(new Option("?:\\_restore", String.Empty, true));
            DefaultExclude.Add(new Option("?:\\recycle?", String.Empty, true));
            DefaultExclude.Add(new Option("*\\temp\\incredimail", String.Empty, true));
            DefaultExclude.Add(new Option("*\\norton antivirus\\quarantine", String.Empty, true));
            DefaultExclude.Add(new Option("*\\system32\\usmt", String.Empty, true));
            DefaultExclude.Add(new Option("*\\system32\\catroot2", String.Empty, true));
            DefaultExclude.Add(new Option("*\\application data\\microsoft", String.Empty, true));
            DefaultExclude.Add(new Option("*\\application data\\aim", String.Empty, true));
        }

        /// <summary>
        /// Load file
        /// </summary>
        /// <param name="filename">File name</param>
        public static void Load(string filename)
        {
            CultureInfo culture = new CultureInfo(CfgFile.Get("Lang"));
            Thread.CurrentThread.CurrentUICulture = culture;

            DefaultsLoad();
            PathExcluded.Clear();
            PathIncluded.Clear();
            FileExtensions.Clear();

            if (!File.Exists(filename))
            {
                FileExtensions.AddRange(DefaultExtensions);
                PathExcluded.AddRange(DefaultExclude);
                PathIncluded.AddRange(DefaultInclude);
                return;
            }
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(filename);
            }
            catch
            {
                FileExtensions.AddRange(DefaultExtensions);
                return;
            }
            if (xml.DocumentElement == null ||
                xml.DocumentElement.Name != "DiskCleaner" ||
                !xml.DocumentElement.HasChildNodes) return;

            XmlElement element = xml.DocumentElement["Exclusions"];
            FillOptionList(element, PathExcluded);
            element = xml.DocumentElement["Inclusions"];
            FillOptionList(element, PathIncluded);
            element = xml.DocumentElement["Extensions"];
            if (element != null && element.HasChildNodes)
            {
                FillOptionList(element, FileExtensions);
            }
            else FileExtensions.AddRange(DefaultExtensions);

            element = xml.DocumentElement["Options"];
            if (element != null && element.HasChildNodes)
            {
                XmlElement subelement = element["ScanTemporaryFolder"];
                if (subelement != null && subelement.HasChildNodes)
                    ScanTemporaryFolder = subelement.FirstChild.Value == bool.TrueString;
                subelement = element["FileSelectZero"];
                if (subelement != null && subelement.HasChildNodes)
                    FileSelectZero = subelement.FirstChild.Value == bool.TrueString;
                subelement = element["CheckedNames"];
                if (subelement != null && subelement.HasChildNodes)
                    CheckedNames = Encoding.ASCII.GetString(Convert.FromBase64String(subelement.FirstChild.Value));
                subelement = element["BackupJunk"];
                if (subelement != null && subelement.HasChildNodes)
                    BackupJunk = subelement.FirstChild.Value == bool.TrueString;
                subelement = element["BackupZero"];
                if (subelement != null && subelement.HasChildNodes)
                    BackupZero = subelement.FirstChild.Value == bool.TrueString;
            }
        }

        public static void FillOptionList(XmlElement elem, List<Option> list)
        {
            string val, desc;
            bool check;
            if (elem != null && elem.HasChildNodes)
            {
                XmlNode node = elem.FirstChild;
                while (node != null)
                {
                    if (node.HasChildNodes && node.ChildNodes.Count == 3 &&
                        node.ChildNodes[0].Name == "Value" &&
                        node.ChildNodes[1].Name == "Description" &&
                        node.ChildNodes[2].Name == "Checked" &&
                        node.ChildNodes[0].HasChildNodes &&
                        //node.ChildNodes[1].HasChildNodes &&
                        node.ChildNodes[2].HasChildNodes)
                    {
                        val = node.ChildNodes[0].FirstChild.Value;
                        desc = node.ChildNodes[1].HasChildNodes ? node.ChildNodes[1].FirstChild.Value : string.Empty;
                        val = Encoding.ASCII.GetString(Convert.FromBase64String(val));
                        check = node.ChildNodes[2].FirstChild.Value == bool.TrueString;
                        list.Add(new Option(val, desc, check));
                    }
                    node = node.NextSibling;
                }
            }
        }

        /// <summary>
        /// Add value to the XML file
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="value">Value</param>
        /// <param name="document">XML document instance</param>
        /// <param name="element">XML element</param>
        public static void AddToXML(string name, string value, ref XmlDocument document, ref XmlElement element)
        {
            XmlElement elem = document.CreateElement(name);
            elem.InnerText = value;
            element.AppendChild(elem);
        }

        /// <summary>
        /// Save file
        /// </summary>
        /// <param name="filename">File name</param>
        public static void Save(string filename)
        {
            XmlDocument xml = new XmlDocument();
            XmlElement element = xml.CreateElement("DiskCleaner");
            xml.AppendChild(element);

            XmlElement subelement = xml.CreateElement("Exclusions");
            SaveOptionListToXML(PathExcluded, xml, subelement);
            element.AppendChild(subelement);
            subelement = xml.CreateElement("Inclusions");
            SaveOptionListToXML(PathIncluded, xml, subelement);
            element.AppendChild(subelement);
            subelement = xml.CreateElement("Extensions");
            SaveOptionListToXML(FileExtensions, xml, subelement);
            element.AppendChild(subelement);

            subelement = xml.CreateElement("Options");
            AddToXML("ScanTemporaryFolder", ScanTemporaryFolder.ToString(), ref xml, ref subelement);
            AddToXML("FileSelectZero", FileSelectZero.ToString(), ref xml, ref subelement);
            AddToXML("CheckedNames", Convert.ToBase64String(Encoding.ASCII.GetBytes(CheckedNames)), ref xml, ref subelement);
            AddToXML("BackupJunk", BackupJunk.ToString(), ref xml, ref subelement);
            AddToXML("BackupZero", BackupZero.ToString(), ref xml, ref subelement);
            element.AppendChild(subelement);

            try
            {
                xml.Save(filename);
            }
            catch
            {
            }
        }

        private static void SaveOptionListToXML(List<Option> list, XmlDocument xmlDoc, XmlElement subelement)
        {
            XmlElement item;
            foreach (Option opt in list)
            {
                item = xmlDoc.CreateElement("Item");
                AddToXML("Value", Convert.ToBase64String(Encoding.ASCII.GetBytes(opt.Value)), ref xmlDoc, ref item);
                AddToXML("Description", opt.Description, ref xmlDoc, ref item);
                AddToXML("Checked", opt.Checked.ToString(), ref xmlDoc, ref item);
                subelement.AppendChild(item);
            }
        }
    }
}