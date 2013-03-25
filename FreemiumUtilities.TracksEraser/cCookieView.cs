using System;
using System.IO;
using System.Text;

namespace FreemiumUtilities.TracksEraser
{
	/// <summary>
	/// Contains methods to operate with the cookies
	/// </summary>
	public class CCookieView
	{
		static string domain;
		static string secure;

		/// <summary>
		/// constructor for cCookieView
		/// </summary>
		/// <param name="localname"></param>
		public CCookieView(string localname)
		{
			//Get file data
			string fData = GetfData(localname);
			string[] lines = fData.Split(Environment.NewLine.ToCharArray());
			//Get domain name
			domain = lines[2];

			if (domain.EndsWith("/"))
			{
				//Get last / position
				int sPos = domain.LastIndexOf("/");
				//Rip of last /
				domain = domain.Substring(0, sPos);
			}

			//Check if cookie is secure
			secure = lines[3];

			secure = secure == "0" ? "NO" : "YES";
		}

		/// <summary>
		/// Secure
		/// </summary>
		public string Secure
		{
			get { return secure; }
		}

		/// <summary>
		/// Domain
		/// </summary>
		public string Domain
		{
			get { return domain; }
		}

		/// <summary>
		/// get string representation of cookie file
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		static string GetfData(string filename)
		{
			string data;
			try
			{
				using (var br = new BinaryReader(File.OpenRead(filename)))
				{
					byte[] bytes = br.ReadBytes((int) br.BaseStream.Length);
					data = Encoding.Default.GetString(bytes);
				}
			}
			catch
			{
				throw new BadFile("Cookie Not Found");
			}
			return data;
		}
	}

	internal class BadFile : ApplicationException
	{
		/// <summary>
		/// BadFile default constructor
		/// </summary>
		public BadFile()
		{
		}

		/// <summary>
		/// BadFile constructor
		/// </summary>
		/// <param name="s"></param>
		public BadFile(string s) : base(s)
		{
		}

		/// <summary>
		/// BadFile constructor
		/// </summary>
		/// <param name="s"></param>
		/// <param name="ex"></param>
		public BadFile(string s, Exception ex) : base(s, ex)
		{
		}
	}
}