using System.IO;

namespace ProcessManager
{
    class BlockedProcessesManager
    {
        /// <summary>
        /// Gets blocked processes
        /// </summary>
        /// <returns>An array of blocked processes</returns>
        public static string[] GetBlockedProcesses()
        {
            string str;
            string[] words = new string[] { };
            try
            {
                using (StreamReader sr = new StreamReader("BlockedProcesses.txt"))
                {
                    str = sr.ReadLine();
                    sr.Close();
                }

                if (!string.IsNullOrEmpty(str))
                {
                    const string delimStr = "~";
                    char[] delimiter = delimStr.ToCharArray();

                    words = str.Split(delimiter);
                }
            }
            catch 
            { 
            }
            return words;
        }

        /// <summary>
        /// Writes content to the BlockedProcesses file
        /// </summary>
        /// <param name="content">content of the file</param>
        public static void WriteBlockedProcesses(string content)
        {
            using (StreamWriter sw = new StreamWriter("BlockedProcesses.txt"))
            {
                sw.Write(content);
                sw.Close();
            }
        }

        /// <summary>
        /// Adds blocked process
        /// </summary>
        /// <param name="processName">Process name</param>
        public static void AddBlockedProcess(string processName)
        {
            string str;
            using (StreamReader sr = new StreamReader("BlockedProcesses.txt"))
            {
                str = sr.ReadLine();
                sr.Close();
            }

            WriteBlockedProcesses(str + processName + "~");

            //using (StreamWriter sw = new StreamWriter("BlockedProcesses.txt"))
            //{
            //    sw.WriteLine(str + processName + "~");
            //    sw.Close();
            //}
        }
    }
}
