using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;

namespace Knots.Security.TracksEraserCore
{
    public enum BrowserType { Chrome, FireFox }

    public class Browser
    {
        public static string GetHistoryPath(BrowserType browserType)
        {
            if (browserType == BrowserType.FireFox)
            {
                string x = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData);
                x += @"\Mozilla\Firefox\Profiles\";
                var di = new DirectoryInfo(x);
                DirectoryInfo[] dir = di.GetDirectories("*.default");
                if (dir.Length != 1)
                    return string.Empty;

                x += dir[0].Name + @"\" + "places.sqlite";

                if (!File.Exists(x))
                    return string.Empty;

                return x;
            }           
            else
            {
                throw new NotImplementedException();
            }
        }

        public static void ClearHistory(BrowserType browserType)
        {
            if (browserType == BrowserType.FireFox)
            {
                try
                {
                    using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + GetHistoryPath(browserType)))
                    {
                        using (SQLiteCommand cmd = conn.CreateCommand())
                        {
                            try
                            {
                                cmd.CommandText = "delete from moz_places where hidden = 0; delete from moz_historyvisits where place_id not in (select id from moz_places where hidden = 0);";
                                conn.Open();
                                int res = cmd.ExecuteNonQuery();
                            }
                            finally
                            {
                                cmd.Dispose();
                                conn.Close();
                            }
                        }
                    }
                }
                catch
                { }
            }          
            else
            {
                throw new NotImplementedException();
            }
        }

        public static string GetCookiePath(BrowserType browserType)
        {
            string x;
            if (browserType == BrowserType.FireFox)
            {
                x = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData);
                x += @"\Mozilla\Firefox\Profiles\";
                DirectoryInfo di = new DirectoryInfo(x);
                DirectoryInfo[] dir = di.GetDirectories("*.default");
                if (dir.Length != 1)
                    return string.Empty;

                x += dir[0].Name + @"\" + "cookies.sqlite";
            }
            else if (browserType == BrowserType.Chrome)
            {
                x = Environment.GetFolderPath(
                               Environment.SpecialFolder.LocalApplicationData);
                x += @"\Google\Chrome\User Data\Default\";

                x += "Cookies";
            }
            else
            {
                throw new NotImplementedException();
            }
            return !File.Exists(x) ? string.Empty : x;
        }

        public static List<CookieData> GetCookies(BrowserType browserType)
        {
            List<CookieData> cookies = new List<CookieData>();
            string tabName = string.Empty;
            if (browserType == BrowserType.FireFox)
                tabName = "moz_cookies";
            else if (browserType == BrowserType.Chrome)
                tabName = "cookies";
            else
                throw new NotImplementedException();

            string cookiePath = GetCookiePath(browserType);
            if (cookiePath != string.Empty)
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + cookiePath))
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select * from " + tabName;
                        try
                        {
                            conn.Open();
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    CookieData data = new CookieData();
                                    data.Name = reader["name"].ToString();
                                    data.Path = reader["path"].ToString();
                                    if (browserType == BrowserType.FireFox)
                                    {
                                        data.ID = reader["id"].ToString();
                                        data.Host = reader["host"].ToString();
                                    }
                                    else if (browserType == BrowserType.Chrome)
                                    {
                                        data.ID = reader["creation_utc"].ToString();
                                        data.Host = reader["host_key"].ToString();
                                    }
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

        public static void DeleteCookie(BrowserType browserType, object id)
        {
            string cookiePath = GetCookiePath(browserType);
            if (cookiePath != string.Empty)
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + cookiePath))
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        try
                        {
                            string sql = string.Empty;

                            if (browserType == BrowserType.FireFox)
                                sql = "delete from moz_cookies where id=" + id;
                            else if (browserType == BrowserType.Chrome)
                                sql = "delete from cookies where creation_utc=" + id;

                            cmd.CommandText = sql;
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
}
