using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace FreemiumUtilities.TracksEraser
{
	/// <summary>
	/// Chrome cookie manager
	/// </summary>
	public class ChromeCookieManager
	{
		/// <summary>
		/// get chrome cookies path
		/// </summary>
		/// <returns></returns>
		static string GetCHromePath()
		{
			string x = Environment.GetFolderPath(
				Environment.SpecialFolder.LocalApplicationData);
			x += @"\Google\Chrome\User Data\Default\";

			x += "Cookies";

			return !File.Exists(x) ? string.Empty : x;
		}

		/// <summary>
		/// get list of chrome cookies
		/// </summary>
		/// <returns></returns>
		public static List<CookieData> GetChromeCookies()
		{
            List<CookieData> cookies = new List<CookieData>();
			if (!string.IsNullOrEmpty(GetCHromePath()))
			{
				using (var conn = new SQLiteConnection("Data Source=" + GetCHromePath()))
				{
					using (SQLiteCommand cmd = conn.CreateCommand())
					{
						cmd.CommandText = "select * from cookies"; // where host like '%" + host + "%';";
						try
						{
							conn.Open();
							using (SQLiteDataReader reader = cmd.ExecuteReader())
							{
								while (reader.Read())
								{
									{
										var data = new CookieData
										           	{
										           		ID = reader["creation_utc"].ToString(),
										           		Name = reader["name"].ToString(),
										           		Host = reader["host_key"].ToString(),
										           		Path = reader["path"].ToString()
										           	};
										cookies.Add(data);
									}
								}
							}
						}
						catch
						{
						}
						finally
						{
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
			if (!string.IsNullOrEmpty(GetCHromePath()))
			{
				using (var conn = new SQLiteConnection("Data Source=" + GetCHromePath()))
				{
					using (SQLiteCommand cmd = conn.CreateCommand())
					{
						try
						{
							cmd.CommandText = "delete from cookies where creation_utc=" + id;
							conn.Open();
							cmd.ExecuteNonQuery();
						}
						finally
						{
							conn.Close();
						}
					}
				}
			}
		}
	}
}