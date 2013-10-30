using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace FreemiumUtilities.Infrastructure
{
    /// <summary>
    /// Provides the methods to operate with a CSV files
    /// </summary>
    public static class FileRW
    {
        /// <summary>
        /// Reads the CSV file and return it's content as a <c>List<string></c>
        /// </summary>
        /// <param name="filePath">CSV file path relative to execution assembly file path</param>
        /// <returns>CSV file content as a <c>List<string></c></returns>
        public static List<string> ReadFile(string filePath)
        {
            try
            {
                string[] linesInFile = File.ReadAllLines(GetAssemblyDirectory() + filePath);
                List<string> listOfLines = new List<string>(linesInFile.Length);

                //check file size is zero
                if (linesInFile.Length == 0)
                {
                    MessageBox.Show("File (" + filePath + ") contains no data",
                                    "File read error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
                else //valid file
                {
                    for (int lineNum = 0; lineNum < linesInFile.Length; lineNum++)
                    {
                        listOfLines.Add(linesInFile[lineNum]);
                    }
                }
                return listOfLines;
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception error (" + e.Message + ")",
                                "File read Exception",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Gets the directory where the main assembly is located (not dependent on the working directory). 
        /// </summary>
        /// <returns>The directory where the main assembly is located.</returns>
        public static string GetAssemblyDirectory()
        {
            try
            {
                string currentFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

                string uriStart = "file:\\";
                if (currentFolder.StartsWith(uriStart)) currentFolder = currentFolder.Substring(uriStart.Length);

                // We need to make sure that we always get the main assembly directory and not a subfolder!

                string endsWith = "Applications";
                if (currentFolder.EndsWith(endsWith)) currentFolder = currentFolder.Substring(0, currentFolder.Length - endsWith.Length);

                endsWith = "Libraries";
                if (currentFolder.EndsWith(endsWith)) currentFolder = currentFolder.Substring(0, currentFolder.Length - endsWith.Length);

                currentFolder += "\\";

                return currentFolder;
            }
            catch (Exception)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
                return string.Empty;
            }
        }
    }
}