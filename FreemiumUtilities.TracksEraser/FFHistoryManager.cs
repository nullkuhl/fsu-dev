using System;
using System.Data.SQLite;
using System.IO;

namespace FreemiumUtilities.TracksEraser
{
    /// <summary>
    /// Firefox history manager
    /// </summary>
    public class FFHistoryManager
    {
        static string GetFFHistoryPath()
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


        public static void ClearHistory()
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + GetFFHistoryPath()))
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
    }
}
