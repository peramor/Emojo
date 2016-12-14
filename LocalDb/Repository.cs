using Emojo.Lib;
using Finisar.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDb
{
    class Repository
    {
        public enum Emotions { Anger, Fear, Happiness, Sadness, Surprise }
        static SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=EmojoLocalDb.db;Version=3;");

        public static Dictionary<Emotions, double> GetEmotionDictionaryAsync(User user)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;

            var dict = new Dictionary<Emotions, double>();

            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT Anger, Fear, Happiness, Sadness, Surprise FROM AppUser u INNER JOIN Photo p on u.rowid = p.UserId WHERE u.rowid = '"+ user.UserId +"'";

            sqlite_conn.Open();
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                dict[Emotions.Anger] = double.Parse(sqlite_datareader.GetString(0));
                dict[Emotions.Fear] = double.Parse(sqlite_datareader.GetString(1));
                dict[Emotions.Happiness] = double.Parse(sqlite_datareader.GetString(2));
                dict[Emotions.Sadness] = double.Parse(sqlite_datareader.GetString(3));
                dict[Emotions.Surprise] = double.Parse(sqlite_datareader.GetString(4));
            }
            sqlite_conn.Close();
            return dict;
        }
        public static void InsertData(User u)
        {
            string sql = String.Format(@"INSERT INTO AppUser (UserName, FullName, ProfilePhoto) 
                                       VALUES ('{0}', '{1}', {2})", u.UserName, u.FullName, u.ProfilePhoto);
            SQLiteCommand sqlite_cmd = new SQLiteCommand(sql, sqlite_conn);
            sqlite_conn.Open();
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }
        public static void InsertData(Photo p)
        {
            string sql = String.Format(@"INSERT INTO Photo (PhotoId, LinkStandard, LinkLow, LinkThumbnail, Anger, Fear, Happiness, Sadness, Surprise, UserId)
                                       VALUES ('{0}', '{1}', '{2}','{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",
                                       p.PhotoId, p.LinkStandard, p.LinkLow, p.LinkThumbnail, p.Anger, p.Fear, p.Happiness, p.Sadness, p.Surprise, p.UserId);
            SQLiteCommand sqlite_cmd = new SQLiteCommand(sql, sqlite_conn);
            sqlite_conn.Open();
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }

    }
}
