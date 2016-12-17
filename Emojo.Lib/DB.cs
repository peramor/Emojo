using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Emojo.Lib
{
    public enum Emotions { Anger, Fear, Happiness, Sadness, Surprise }

    public static class DB
    {
        static string uri = "https://emojo.azurewebsites.net/db/query";

        /// <summary>
        /// Insert photo into table photos
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns> true if query was successed</returns>
        public static async Task<bool> InsertPhotoAsync(Photo p)
        {
            string sql = String.Format("INSERT INTO Photos (PhotoId, LinkStandard, LinkLow, LinkThumbnail, Anger, Fear, Happiness, Sadness, Surprise, UserId) " +
                                       "VALUES ('{0}', '{1}', '{2}','{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",
                                       p.PhotoId, p.LinkStandard, p.LinkLow, p.LinkThumbnail, p.Anger, p.Fear, p.Happiness, p.Sadness, p.Surprise, p.UserId);
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"{uri}?sql={sql}&appId={Properties.Resources.AppId}");
                if (string.IsNullOrEmpty(response))
                    return true;
                else
                    return false;
            }
        }

        public static async Task<bool> InsertUserAsync(User u)
        {
            string sql = String.Format("INSERT INTO Users (UserName, FullName, ProfilePhoto, UserId) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}')", u.UserName, u.FullName, u.ProfilePhoto, u.UserId);
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"{uri}?sql={sql}&appId={Properties.Resources.AppId}");
                if (string.IsNullOrEmpty(response))
                    return true;
                else
                    return false;
            }
        }

        public static async Task<List<Photo>> GetUserPhotosAsync (User user) {
            string json_string;
            using (var client = new HttpClient()) {
                var sql = string.Format("SELECT * FROM Photos WHERE userId = {0}", user.UserId);
                var message = await client.GetAsync($"{uri}?sql={sql}&appId={Properties.Resources.AppId}");
                json_string = await message.Content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<List<Photo>>(json_string);
        }      

        public static async Task<Photo> GetPhotoAsync (string photoId) {
            string json_string;
            using (var client = new HttpClient()) {
                var sql = string.Format("SELECT * FROM Photos WHERE PhotoId = \'{0}\'", photoId);
                var message = await client.GetAsync($"{uri}?sql={sql}&appId={Properties.Resources.AppId}");
                json_string = await message.Content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<List<Photo>>(json_string).FirstOrDefault();
        }

    }
}
