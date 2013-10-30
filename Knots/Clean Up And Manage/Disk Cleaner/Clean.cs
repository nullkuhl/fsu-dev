using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Disk_Cleaner
{
    /// <summary>
    /// Contains methods to clean the disk
    /// </summary>
    public static class Clean
    {
        /// <summary>
        /// Current file to delete
        /// </summary>
        public static DeleteFile Current;
        /// <summary>
        /// App terminating flag
        /// </summary>
        public static bool ABORT;
        /// <summary>
        /// OnProcess event handler
        /// </summary>
        public static event EventHandler OnProgress;

        /// <summary>
        /// empty recycle bin and return size of deleted items
        /// </summary>
        /// <returns></returns>
        public static ulong ProcessRecycleBin()
        {
            ulong gainedSize;
            ulong count;

            ABORT = false;
            RecycleBin.GetRecycleBinSize(out count, out gainedSize);
            if (RecycleBin.EmptyRecycleBin() == 0) // if recycle bin emptied successfully
                return gainedSize;
            return 0;
        }

        /// <summary>
        /// delete junk files
        /// </summary>
        /// <param name="junk"></param>
        /// <returns></returns>
        public static ulong ProcessJunk(ArrayList junk)
        {
            ulong gainedSize = 0;

            ABORT = false;
            string backto = string.Empty;
            if (Preferences.BackupJunk)
            {
                backto = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar +
                         "backup" + Path.DirectorySeparatorChar +
                         DateTime.Now.ToString("yyyyMMddHHmmss") + Path.DirectorySeparatorChar;
                try
                {
                    Directory.CreateDirectory(backto);
                }
                catch
                {
                    backto = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar;
                }
                if (!Directory.Exists(backto)) backto = string.Empty;
            }
            bool back = backto != string.Empty;
            Dictionary<DeleteFile, string> success = new Dictionary<DeleteFile, string>();
            foreach (DeleteFile del in junk)
            {
                if (ABORT) break;

                if (del.Delete)
                {
                    string succ = string.Empty;
                    string from = del.Name;
                    if (back)
                    {
                        Guid guid = Guid.NewGuid();
                        string to = backto + guid;
                        try
                        {
                            File.Copy(from, to);
                            succ = guid.ToString();
                        }
                        catch
                        {
                            succ = "FB";
                        } // failed backup
                    }
                    if ((back && string.IsNullOrEmpty(succ)) || !back)
                    {
                        try
                        {
                            File.Delete(from);
                            gainedSize += (ulong)(del.Info.Length);
                        }
                        catch
                        {
                            succ = "FD";
                        } // failed copy
                    }
                    success.Add(del, succ);
                    if (OnProgress != null)
                    {
                        Current = del;
                        OnProgress(null, EventArgs.Empty);
                    }
                }
            }
            if (Preferences.BackupJunk && !string.IsNullOrEmpty(backto))
            {
                XmlDocument xml = new XmlDocument();
                XmlElement element = xml.CreateElement("DiskCleanerBackup");
                foreach (DeleteFile del in success.Keys)
                {
                    XmlElement subelement = xml.CreateElement("item");
                    Preferences.AddToXML("filename", del.Name, ref xml, ref subelement);
                    Preferences.AddToXML("guid", success[del], ref xml, ref subelement);
                    element.AppendChild(subelement);
                }
                xml.AppendChild(element);
                try
                {
                    xml.Save(backto + "backupinfo.xml");
                }
                catch
                {
                }
            }

            return gainedSize;
        }

        /// <summary>
        /// restore deleted folder form backup
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="overwrite"></param>
        public static void RestoreFolder(string folder, bool overwrite)
        {
            ABORT = false;
            string restorefrom = Directory.Exists(folder)
                                    ? folder
                                    : Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar +
                                      "backup" + Path.DirectorySeparatorChar +
                                      folder + Path.DirectorySeparatorChar;
            if (!Directory.Exists(restorefrom)) return;
            if (!File.Exists(restorefrom + "backupinfo.xml")) return;
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(restorefrom + "backupinfo.xml");
            }
            catch
            {
                return;
            }
            if (!xml.HasChildNodes || xml.FirstChild.Name != "DiskCleanerBackup" || !xml.FirstChild.HasChildNodes) return;
            List<XmlNode> nodes = new List<XmlNode>();

            if (xml.DocumentElement != null)
            {
                XmlNode elem = xml.DocumentElement.FirstChild;
                while (elem != null)
                {
                    if (elem.Name == "item" && elem.HasChildNodes && elem.ChildNodes.Count == 2)
                        nodes.Add(elem);
                    elem = elem.NextSibling;
                }
            }

            string from = string.Empty;
            Dictionary<XmlNode, string> result = new Dictionary<XmlNode, string>();

            foreach (XmlNode node in nodes)
            {
                string succ;
                XmlElement element = node["guid"];
                if (element != null)
                    from = restorefrom + element.InnerText;
                XmlElement xmlElement = node["filename"];
                string to = string.Empty;
                if (xmlElement != null)
                    to = xmlElement.InnerText;
                if (!File.Exists(from))
                    succ = "FO"; // failed original
                else
                {
                    if (File.Exists(to) && !overwrite)
                    {
                        succ = "FW"; // failed overwrite not allowed
                    }
                    else
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
                                File.Copy(from, to, overwrite);

                            succ = "OK";
                        }
                        catch
                        {
                            succ = "FC";
                        } // failed to copy
                    }
                }
                result.Add(node, succ);


                if (OnProgress != null)
                {
                    Current = new DeleteFile(from, false);
                    OnProgress(null, EventArgs.Empty);
                    if (ABORT) break;
                }
            }


            foreach (XmlNode node in result.Keys)
                if (result[node] == "OK")
                {
                    if (xml.DocumentElement != null)
                        xml.DocumentElement.RemoveChild(node);
                    try
                    {
                        XmlNode xmlElement = node["guid"];
                        if (xmlElement != null)
                            File.Delete(restorefrom + xmlElement.InnerText);
                    }
                    catch
                    {
                    }
                }

            if (xml.DocumentElement.HasChildNodes)
            {
                try
                {
                    xml.Save(restorefrom + "backupinfo.xml");
                }
                catch
                {
                }
            }


            try
            {
                Directory.Delete(restorefrom, true);
            }
            catch
            {
            }
        }

        /// <summary>
        /// delete zero bytes files
        /// </summary>
        /// <param name="zero"></param>
        public static void ProcessZero(ArrayList zero)
        {
            ABORT = false;
            string backto = string.Empty;
            if (Preferences.BackupZero)
            {
                backto = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar +
                         "backup" + Path.DirectorySeparatorChar +
                         DateTime.Now.ToString("yyyyMMddHHmmss") + Path.DirectorySeparatorChar;
                try
                {
                    Directory.CreateDirectory(backto);
                }
                catch
                {
                    backto = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar;
                }
                if (!Directory.Exists(backto)) backto = string.Empty;
            }
            bool back = backto != string.Empty;
            Dictionary<DeleteFile, string> success = new Dictionary<DeleteFile, string>();
            foreach (DeleteFile del in zero)
                if (del.Delete)
                {
                    string succ = string.Empty;
                    string from = del.Name;
                    if (back)
                    {
                        Guid guid = Guid.NewGuid();
                        string to = backto + guid.ToString();
                        try
                        {
                            File.Copy(from, to);
                            succ = guid.ToString();
                        }
                        catch
                        {
                            succ = "FB";
                        } // failed backup
                    }
                    if ((back && succ != string.Empty) || !back)
                    {
                        try
                        {
                            File.Delete(from);
                        }
                        catch
                        {
                            succ = "FD";
                        } // failed copy
                    }
                    success.Add(del, succ);
                    if (OnProgress != null)
                    {
                        Current = del;
                        OnProgress(null, EventArgs.Empty);
                        if (ABORT) break;
                    }
                }
            if (Preferences.BackupZero && !string.IsNullOrEmpty(backto))
            {
                XmlDocument xml = new XmlDocument();
                XmlElement element = xml.CreateElement("DiskCleanerBackup");
                foreach (DeleteFile del in success.Keys)
                {
                    XmlElement subelement = xml.CreateElement("item");
                    Preferences.AddToXML("filename", del.Name, ref xml, ref subelement);
                    Preferences.AddToXML("guid", success[del], ref xml, ref subelement);
                    element.AppendChild(subelement);
                }
                xml.AppendChild(element);
                try
                {
                    xml.Save(backto + "backupinfo.xml");
                }
                catch
                {
                }
            }
        }
    }
}