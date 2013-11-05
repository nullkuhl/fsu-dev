using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

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
		/// <summary>
		/// Preferences file name
		/// </summary>
		public static readonly string Filename = "configuration.xml";

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
			DefaultExtensions.Add(new Option("*.tmp", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.temp", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.nch", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.---", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.~*", "Temporary file", true));
			DefaultExtensions.Add(new Option("~*.*", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.??$", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.___", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.~mp", "Temporary file", true));
			DefaultExtensions.Add(new Option("*._mp", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.$db", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.?$?", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.??~", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.?~?", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.db$", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.^", "Temporary file", true));
			DefaultExtensions.Add(new Option("*._dd", "Temporary file", true));
			DefaultExtensions.Add(new Option("*._detmp", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.$$$", "Temporary file", true));
			DefaultExtensions.Add(new Option("0*.nch", "Temporary file", true));
			DefaultExtensions.Add(new Option("*.old", "Temporary backup file", true));
			DefaultExtensions.Add(new Option("*.bak", "Temporary backup file", true));
			DefaultExtensions.Add(new Option("*.chk", "Saved disk error data from scandisk", true));
			DefaultExtensions.Add(new Option("*.gid", "Temporary help file search data", true));
			DefaultExtensions.Add(new Option("mscreate.dir", "Temporary directory information", true));
			DefaultExtensions.Add(new Option("*.wbk", "Office backup file", true));
			DefaultExtensions.Add(new Option("*.fts", "Temporary help file search data", true));
			DefaultExtensions.Add(new Option("*.ftg", "Windows help system temp file", true));
			DefaultExtensions.Add(new Option("*log.txt", "Log file", true));
			DefaultExtensions.Add(new Option("*.err", "Error file", true));
			DefaultExtensions.Add(new Option("*.dmp", "Memory dump files", true));
			DefaultExtensions.Add(new Option("*.prv", "Outdated file", true));
			DefaultExtensions.Add(new Option("*.ilk", "Intermediate file", true));
			DefaultExtensions.Add(new Option("*.aps", "Intermediate file", true));
			DefaultExtensions.Add(new Option("*.ncb", "Intermediate file", true));
			DefaultExtensions.Add(new Option("*.pch", "Intermediate file", true));
			DefaultExtensions.Add(new Option("chklist.*", "Temporary file created during setup", true));

			DefaultExclude.Add(new Option("?:\\Program Files\\Common Files", "", true));
			DefaultExclude.Add(new Option("?:\\Program Files\\Uninstall Information", "", true));
			DefaultExclude.Add(new Option("?:\\win*\\security", "", true));
			DefaultExclude.Add(new Option("*\\sendto", "", true));
			DefaultExclude.Add(new Option("*\\i386", "", true));
			DefaultExclude.Add(new Option("*\\resources\\themes", "", true));
			DefaultExclude.Add(new Option("*\\temporary internet files", "", true));
			DefaultExclude.Add(new Option("*\\microsoft\\office\\data", "", true));
			DefaultExclude.Add(new Option("*\\microsoft\\money\\*\\webcache", "", true));
			DefaultExclude.Add(new Option("*\\chaos32", "", true));
			DefaultExclude.Add(new Option("*\\paprport\\data", "", true));
			DefaultExclude.Add(new Option("*\\symantec shared\\virusdefs", "", true));
			DefaultExclude.Add(new Option("?:\\_restore", "", true));
			DefaultExclude.Add(new Option("?:\\recycle?", "", true));
			DefaultExclude.Add(new Option("*\\temp\\incredimail", "", true));
			DefaultExclude.Add(new Option("*\\norton antivirus\\quarantine", "", true));
			DefaultExclude.Add(new Option("*\\system32\\usmt", "", true));
			DefaultExclude.Add(new Option("*\\system32\\catroot2", "", true));
			DefaultExclude.Add(new Option("*\\application data\\microsoft", "", true));
			DefaultExclude.Add(new Option("*\\application data\\aim", "", true));
		}

		/// <summary>
		/// Load file
		/// </summary>
		/// <param name="filename">File name</param>
		public static void Load(string filename)
		{
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
			var xml = new XmlDocument();
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

			string val, desc;
			bool check;
			XmlElement element = xml.DocumentElement["Exclusions"];
			if (element != null && element.HasChildNodes)
			{
				XmlNode node = element.FirstChild;
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
						PathExcluded.Add(new Option(val, desc, check));
					}
					node = node.NextSibling;
				}
			}
			element = xml.DocumentElement["Inclusions"];
			if (element != null && element.HasChildNodes)
			{
				XmlNode node = element.FirstChild;
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
						PathIncluded.Add(new Option(val, desc, check));
					}
					node = node.NextSibling;
				}
			}
			element = xml.DocumentElement["Extensions"];
			if (element != null && element.HasChildNodes)
			{
				XmlNode node = element.FirstChild;
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
						FileExtensions.Add(new Option(val, desc, check));
					}
					node = node.NextSibling;
				}
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
			var xml = new XmlDocument();
			XmlElement element = xml.CreateElement("DiskCleaner");
			xml.AppendChild(element);
			XmlElement item;

			XmlElement subelement = xml.CreateElement("Exclusions");
			foreach (Option opt in PathExcluded)
			{
				item = xml.CreateElement("Item");
				AddToXML("Value", Convert.ToBase64String(Encoding.ASCII.GetBytes(opt.Value)), ref xml, ref item);
				AddToXML("Description", opt.Description, ref xml, ref item);
				AddToXML("Checked", opt.Checked.ToString(), ref xml, ref item);
				subelement.AppendChild(item);
			}
			element.AppendChild(subelement);

			subelement = xml.CreateElement("Inclusions");
			foreach (Option opt in PathIncluded)
			{
				item = xml.CreateElement("Item");
				AddToXML("Value", Convert.ToBase64String(Encoding.ASCII.GetBytes(opt.Value)), ref xml, ref item);
				AddToXML("Description", opt.Description, ref xml, ref item);
				AddToXML("Checked", opt.Checked.ToString(), ref xml, ref item);
				subelement.AppendChild(item);
			}
			element.AppendChild(subelement);

			subelement = xml.CreateElement("Extensions");
			foreach (Option opt in FileExtensions)
			{
				item = xml.CreateElement("Item");
				AddToXML("Value", Convert.ToBase64String(Encoding.ASCII.GetBytes(opt.Value)), ref xml, ref item);
				AddToXML("Description", opt.Description, ref xml, ref item);
				AddToXML("Checked", opt.Checked.ToString(), ref xml, ref item);
				subelement.AppendChild(item);
			}
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
	}
}