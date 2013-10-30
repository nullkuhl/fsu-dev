using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace FreemiumUtilities.TracksEraser
{
	/// <summary>
	/// Firefox cookies manager
	/// </summary>
	public class FFCookieManager
	{
		/// <summary>
		/// get firefox cookies path
		/// </summary>
		/// <returns></returns>
		static string GetFFCookiePath()
		{
			string x = Environment.GetFolderPath(
				Environment.SpecialFolder.ApplicationData);
			x += @"\Mozilla\Firefox\Profiles\";
			var di = new DirectoryInfo(x);
			DirectoryInfo[] dir = di.GetDirectories("*.default");
			if (dir.Length != 1)
				return string.Empty;

			x += dir[0].Name + @"\" + "cookies.sqlite";

			if (!File.Exists(x))
				return string.Empty;

			return x;
		}

		/// <summary>
		/// get list of firefox cookies
		/// </summary>
		/// <returns></returns>
		public static List<CookieData> GetCookies()
		{
			var cookies = new List<CookieData>();
			if (GetFFCookiePath() != "")
			{
				using (var conn = new SQLiteConnection("Data Source=" + GetFFCookiePath()))
				{
					using (SQLiteCommand cmd = conn.CreateCommand())
					{
						cmd.CommandText = "select * from moz_cookies";
						try
						{
							conn.Open();
							using (SQLiteDataReader reader = cmd.ExecuteReader())
							{
								while (reader.Read())
								{
									var data = new CookieData
									           	{
									           		ID = reader["id"].ToString(),
									           		Name = reader["name"].ToString(),
									           		Host = reader["host"].ToString(),
									           		Path = reader["path"].ToString()
									           	};
									cookies.Add(data);
								}
							}
						}
						finally
						{
							cmd.Dispose();
							conn.Close();
						}
					}
				}
			}

			return cookies;
		}

		/// <summary>
		/// delete specific cookie
		/// </summary>
		/// <param name="id"></param>
		public static void DeleteCookie(object id)
		{
			if (GetFFCookiePath() != "")
			{
				using (var conn = new SQLiteConnection("Data Source=" + GetFFCookiePath()))
				{
					using (SQLiteCommand cmd = conn.CreateCommand())
					{
						try
						{
							cmd.CommandText = "delete from moz_cookies where id=" + id;
							conn.Open();
							cmd.ExecuteNonQuery();
						}
						finally
						{
							cmd.Dispose();
							conn.Close();
						}
					}
				}
			}
		}
	}

	/// <summary>
	/// Cookies data model
	/// </summary>
	public class CookieData
	{
		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Path
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// Host
		/// </summary>
		public string Host { get; set; }

		/// <summary>
		/// ID
		/// </summary>
		public string ID { get; set; }
	}
}