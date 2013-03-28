using System;
using System.IO;

/// <summary>
/// The <see cref="FreemiumUtilities.RegCleaner.Implementation"/> namespace contains a Logging class
/// </summary>
namespace FreemiumUtilities.RegCleaner.Implementation
{
    /// <summary>
    /// Provides a methods to create txt log files
    /// </summary>
    public class Logging
    {
        /// <summary>
        /// Creates a new <c>Logging</c> class instance and defines it's <c>FilePath</c> property with a specified <paramref name="path"/>
        /// </summary>
        /// <param name="path">Path to the txt log file</param>
        public Logging(string path)
        {
            FilePath = path;
        }

        /// <summary>
        /// Path to the txt log file
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Writes a new line in the txt log file
        /// </summary>
        /// <param name="message">Log message</param>
        public void WriteLine(string message)
        {
            try
            {
                using (StreamWriter writeStream = File.AppendText(FilePath))
                {
                    writeStream.WriteLine("  :{0}", message);
                    writeStream.Flush();
                    writeStream.Close();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Writes a new message in the txt log file
        /// </summary>
        /// <param name="message">Log message</param>
        public void WriteLog(string message)
        {
            if (FilePath.Length == 0)
                return;
            try
            {
                using (StreamWriter writeStream = File.AppendText(FilePath))
                {
                    writeStream.Write("\r\nLog Entry : ");
                    writeStream.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    writeStream.WriteLine("  :");
                    writeStream.WriteLine("  :{0}", message);
                    writeStream.WriteLine("-------------------------------");
                    writeStream.Flush();
                    writeStream.Close();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Reads txt log file content
        /// </summary>
        /// <returns>Txt log file content</returns>
        public string FetchLog()
        {
            string line = String.Empty;

            if (FilePath.Length == 0)
                return line;
            try
            {
                using (StreamReader readStream = File.OpenText(FilePath))
                {
                    line = readStream.ReadLine();
                    readStream.Close();
                }
            }
            catch
            {
            }
            return line;
        }
    }
}