using System;
using System.IO;

/// <summary>
/// The <see cref="RegistryCleaner.Implementation"/> namespace contains the methods for logging operations at the Registry Cleaner knot
/// </summary>
namespace RegistryCleaner.Implementation
{
	/// <summary>
	/// Provides methods for logging operations
	/// </summary>
	public class Logging
	{
		/// <summary>
		/// Logging class constructor
		/// </summary>
		/// <param name="path"></param>
		public Logging(string path)
		{
			FilePath = path;
		}

		/// <summary>
		/// File path
		/// </summary>
		public string FilePath { get; set; }

		/// <summary>
		/// Writes a logging message to a new line
		/// </summary>
		/// <param name="message">Logging message</param>
		public void WriteLine(string message)
		{
			try
			{
				StreamWriter writeStream = File.AppendText(FilePath);
				writeStream.WriteLine("  :{0}", message);
				writeStream.Flush();
				writeStream.Close();
			}
			catch
			{
			}
		}

		/// <summary>
		/// Writes a logging message
		/// </summary>
		/// <param name="message">Logging message</param>
		public void WriteLog(string message)
		{
			if (FilePath.Length == 0)
				return;
			try
			{
				StreamWriter writeStream = File.AppendText(FilePath);
				writeStream.Write("\r\nLog Entry : ");
				writeStream.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
				writeStream.WriteLine("  :");
				writeStream.WriteLine("  :{0}", message);
				writeStream.WriteLine("-------------------------------");
				writeStream.Flush();
				writeStream.Close();
			}
			catch
			{
			}
		}

		/// <summary>
		/// Fetches log
		/// </summary>
		/// <returns>Log content</returns>
		public string FetchLog()
		{
			string line = String.Empty;

			if (FilePath.Length == 0)
				return line;
			try
			{
				StreamReader readStream = File.OpenText(FilePath);
				line = readStream.ReadLine();
				readStream.Close();
			}
			catch
			{
			}
			return line;
		}
	}
}