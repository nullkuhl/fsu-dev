/*
 * This is a dll was designed mainly as a set of Utilities regarding bug reporting, and application paths,
 * that was designed mainly for the freemium system Utilities, and any of its derivatives 
 */

using System.IO;

namespace FreemiumUtil
{
    /// <summary>
    /// The <see cref="FreemiumUtil"/> namespace contains a set of Utilities regarding bug reporting, and application paths, 
    /// that was designed mainly for the freemium system Utilities, and any of its derivatives 
    /// </summary>

    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc { }

    /// <summary>
    /// Contains methods to operate with a txt config file
    /// </summary>
    public static class CfgFile
    {
        static string cfgFilePath = /*GetInstallPath() +*/ "freemium.cfg";
        public static string CfgFilePath
        {
            get { return cfgFilePath; }
            set { cfgFilePath = value; }
        }

        /// <summary>
        /// Reads the given key from the config file and returns its value.
        /// </summary>
        /// <param name="key">Configuration entry key</param>
        /// <returns></returns>
        public static string Get(string key)
        {
            int dummy;
            return Get(key, out dummy);
        }

        static string Get(string key, out int lineNum)
        {
            if (File.Exists(CfgFilePath))
            {
                using (StreamReader reader = new StreamReader(CfgFilePath))
                {
                    string line;
                    for (lineNum = 0; (line = reader.ReadLine()) != null; lineNum++)
                    {
                        string[] entry = line.Split(new[] { ' ', '=' });
                        if (entry.Length > 1 && entry[0] == key)
                            return entry[entry.Length - 1];
                    }
                }
            }

            lineNum = -1;
            return null;
        }

        /// <summary>
        /// Sets the given key and value in the config file.
        /// </summary>
        /// <param name="key">Configuration entry key</param>
        /// <param name="value">Configuration entry value</param>
        public static void Set(string key, string value)
        {
            int lineNum;
            if (Get(key, out lineNum) == null)
            {
                using (StreamWriter writer = File.AppendText(CfgFilePath))
                {
                    writer.WriteLine(key + "=" + value);
                    writer.Close();
                }
            }
            else
            {
                string cfg = string.Empty;
                using (StreamReader reader = new StreamReader(CfgFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var entry = line.Split(new[] { ' ', '=' });
                        if (entry[0] == key)
                            cfg += key + "=" + value + "\r\n";
                        else
                            cfg += line + "\r\n";
                    }
                    reader.Close();
                }

                using (StreamWriter writer = new StreamWriter(CfgFilePath))
                {
                    writer.Write(cfg);
                    writer.Close();
                }
            }
        }
    }
}
