using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

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
		/// <param name="filePath">CSV file path</param>
		/// <returns>CSV file content as a <c>List<string></c></returns>
		public static List<string> ReadFile(string filePath)
		{
			try
			{
				string[] linesInFile = File.ReadAllLines(filePath);
				var listOfLines = new List<string>(linesInFile.Length);

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
	}
}