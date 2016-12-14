using Finisar.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDb
{
    class CreateDb
    {
        public static void CreateLocalDb()
        {
            SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=EmojoLocalDb.db;Version=3;New=True;Compress=True");
            SQLiteCommand sqlite_cmd = new SQLiteCommand(@"CREATE TABLE [AppUser] ( 
                                                            [UserName] varchar(100) NOT NULL, 
                                                            [FullName] varchar(100), 
                                                            [ProfilePhoto] varchar(500) 
                                                            ); 
                                                            CREATE TABLE [Photo] ( 
                                                            [PhotoId] varchar(500) PRIMARY KEY NOT NULL, 
                                                            [LinkStandard] varchar(500) NOT NULL, 
                                                            [LinkLow] varchar(500) NOT NULL, 
                                                            [LinkThumbnail] varchar(500) NOT NULL, 
                                                            [Anger] float NOT NULL, 
                                                            [Fear] float NOT NULL, 
                                                            [Happiness] float NOT NULL, 
                                                            [Sadness] float NOT NULL, 
                                                            [Surprise] float NOT NULL, 
                                                            [UserId] int NOT NULL 
                                                            );", sqlite_conn);
            sqlite_conn.Open();
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }
    }
}
