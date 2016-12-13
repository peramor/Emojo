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

        public static async Task<Dictionary<Emotions, double>> GetEmotionDictionaryAsync(User user) {
            var dict = new Dictionary<Emotions, double>();
            string json_string;
            using (var client = new HttpClient()) {
                var sql = string.Format("SELECT * FROM Photos WHERE userId = {0}", user.UserId);
                var message = await client.GetAsync($"{uri}?sql={sql}&appId={Properties.Resources.AppId}");
                json_string = await message.Content.ReadAsStringAsync();
            }
            var photos = JsonConvert.DeserializeObject<List<Photo>>(json_string);
            var averages = (from p in photos
                            group p by 1 into g
                            select new {
                                Anger = g.Average(x => x.Anger),
                                Happiness = g.Average(x => x.Happiness),
                                Fear = g.Average(x => x.Fear),
                                Sadness = g.Average(x => x.Sadness),
                                Surprise = g.Average(x => x.Surprise)
                            }).First();
            //В случае, если фото не найдено (нет фото для данного пользователя в базе), здесь будет брошено исключение
            dict[Emotions.Anger] = averages.Anger;
            dict[Emotions.Fear] = averages.Fear;
            dict[Emotions.Happiness] = averages.Happiness;
            dict[Emotions.Sadness] = averages.Sadness;
            dict[Emotions.Surprise] = averages.Surprise;
            return dict;
        }

        public static async Task<Dictionary<Emotions, double>> GetEmotionDictionaryAsync(Photo photo) {
            var dict = new Dictionary<Emotions, double>();
            string json_string;
            using (var client = new HttpClient()) {
                var sql = string.Format("SELECT * FROM Photos WHERE PhotoId = \'{0}\'", photo.PhotoId);
                var message = await client.GetAsync($"{uri}?sql={sql}&appId={Properties.Resources.AppId}");
                json_string = await message.Content.ReadAsStringAsync();
            }
            var image = JsonConvert.DeserializeObject<List<Photo>>(json_string).FirstOrDefault();
            // В случае, если в базе нет такого изображения, здесь должно быть брошено исключение.
            dict[Emotions.Anger] = image.Anger;
            dict[Emotions.Fear] = image.Fear;
            dict[Emotions.Happiness] = image.Happiness;
            dict[Emotions.Sadness] = image.Sadness;
            dict[Emotions.Surprise] = image.Surprise;
            return dict;
        }


        public static async Task<Dictionary<string, List<Emotions>>> GetMaxByEmotionAsync(User user) {
            var dict = new Dictionary<string, List<Emotions>>();
            string json_string;
            using (var client = new HttpClient()) {
                var sql = string.Format("SELECT * FROM Photos WHERE userId = {0}", user.UserId);
                var message = await client.GetAsync($"{uri}?sql={sql}&appId={Properties.Resources.AppId}");
                json_string = await message.Content.ReadAsStringAsync();
            }
            var photos = JsonConvert.DeserializeObject<List<Photo>>(json_string);
            var best_photos = (from p in photos
                               group p by 1 into g
                               select new {
                                   Anger = g.Where(x => x.Anger == g.Max(y => y.Anger)).FirstOrDefault().LinkStandard,
                                   Happiness = g.Where(x => x.Happiness == g.Max(y => y.Happiness)).FirstOrDefault().LinkStandard,
                                   Fear = g.Where(x => x.Fear == g.Max(y => y.Fear)).FirstOrDefault().LinkStandard,
                                   Sadness = g.Where(x => x.Sadness == g.Max(y => y.Sadness)).FirstOrDefault().LinkStandard,
                                   Surprise = g.Where(x => x.Surprise == g.Max(y => y.Surprise)).FirstOrDefault().LinkStandard
                               }).First();
            //В случае, если фото не найдено (нет фото для данного пользователя в базе), здесь будет брошено исключение
            if (dict.ContainsKey(best_photos.Anger)) {
                dict[best_photos.Anger].Add(Emotions.Anger);
            } else {
                dict[best_photos.Anger] = new List<Emotions> { Emotions.Anger };
            }

            if (dict.ContainsKey(best_photos.Fear)) {
                dict[best_photos.Fear].Add(Emotions.Fear);
            } else {
                dict[best_photos.Fear] = new List<Emotions> { Emotions.Fear };
            }

            if (dict.ContainsKey(best_photos.Happiness)) {
                dict[best_photos.Happiness].Add(Emotions.Happiness);
            } else {
                dict[best_photos.Happiness] = new List<Emotions> { Emotions.Happiness };
            }

            if (dict.ContainsKey(best_photos.Sadness)) {
                dict[best_photos.Sadness].Add(Emotions.Sadness);
            } else {
                dict[best_photos.Sadness] = new List<Emotions> { Emotions.Sadness };
            }

            if (dict.ContainsKey(best_photos.Surprise)) {
                dict[best_photos.Surprise].Add(Emotions.Surprise);
            } else {
                dict[best_photos.Surprise] = new List<Emotions> { Emotions.Surprise };
            }

            return dict;
        }

        public static async Task<bool> CheckUserAsync (User user) {
            string json_string;
            using (var client = new HttpClient()) {
                var sql = string.Format("SELECT * FROM Users WHERE userId = {0}", user.UserId);
                var message = await client.GetAsync($"{uri}?sql={sql}&appId={Properties.Resources.AppId}");
                json_string = await message.Content.ReadAsStringAsync();
            }

            var users = JsonConvert.DeserializeObject<List<User>>(json_string);
            return users.Count != 0;
        }

    }
}
