using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DiskAnalysis
{
    

    public static class LogClass
    {
        public enum LogInfo { Start, End , Info};
        private static object tt = new object();

        public static void AddErrorToLog(string msg)
        {
            string fName = "DiskAnalysis.log";
            lock (tt)
            {
                try
                {
                    StreamWriter writer;
                    if (!File.Exists(fName))
                        writer = File.CreateText(fName);
                    else
                        writer = File.AppendText(fName);

                    using (writer)
                    {
                        writer.WriteLine(string.Format("{0} Error message: {1}", DateTime.Now, msg));
                        writer.Close();
                    }
                }
                catch
                { }
            }
        }

        public static void AddInfoToLog(LogInfo time, string msg)
        {
            string fName = "DiskAnalysis.log";
            lock (tt)
            {
                try
                {
                    StreamWriter writer;
                    if (!File.Exists(fName))
                        writer = File.CreateText(fName);
                    else
                        writer = File.AppendText(fName);

                    using (writer)
                    {
                        writer.WriteLine(string.Format("{0} {1} {2}", DateTime.Now, time, msg));
                        writer.Close();
                    }
                }
                catch
                { }
            }
        }
    }
}
