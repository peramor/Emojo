using System.Collections.Generic;
using System.Linq;
using Emojo.Lib;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Emojo.Lib.EmotionAPI;
using System;

namespace Emojo.Droid
{
    class Repository
    {
        public static User User { get; set; }
        public static List<Photo> Photos { get; set; }
        private string access_token;

        private Action<int, int, int> _loading { get; set; }

        private static Dictionary<string, string> _smileDic = new Dictionary<string, string>
        {
            {"Anger", "😡" },
            {"Fear", "😱" },
            {"Happiness", "😁" },
            {"Sadness", "😳" },
            {"Surprise", "😳" }
        }; // 😡 😱 😁 😭 😳

        public static Dictionary<string, string> SmileDic
        {
            get { return _smileDic; }
        }

        public static string SmileMaxOverall
        {
            get { return _smileDic[GetMax().Result.Key]; }
        }

        public static async Task<Dictionary<string, double>> GetOverall()
        {
            return new Dictionary<string, double>
            {
                {"Anger", Photos.Average(p => p.Anger) },
                {"Fear", Photos.Average(p => p.Fear) },
                {"Happiness", Photos.Average(p => p.Happiness) },
                {"Sadness", Photos.Average(p => p.Sadness) },
                {"Surprise", Photos.Average(p => p.Surprise) },
            };
        }

        public static async Task<KeyValuePair<string, double>> GetMax()
        {
            var dic = await GetOverall();
            var max = dic.Max(d => d.Value);
            return dic.First(p => p.Value == max);
        }

        public static async Task<Dictionary<string, double>> GetOne(Photo p)
        {
            return new Dictionary<string, double>
            {
                {"Anger", p.Anger },
                {"Fear", p.Fear },
                {"Happiness", p.Happiness },
                {"Sadness", p.Sadness },
                {"Surprise", p.Surprise },
            };
        }

        public static async Task<string> GetSmile(Photo p)
        {
            var dic = await GetOne(p);
            var max = dic.Max(d => d.Value);
            return _smileDic[dic.First(d => d.Value == max).Key];
        }

        public Repository(User user, string access_token) 
            : this (user, access_token, null)
        { }

        public Repository(User user, string access_token, Action<int,int,int> loading)
        {
            User = user;
            this.access_token = access_token;
            _loading = loading;

            LoadData();
        }

        private void LoadData()
        {
            if (DB.InsertUserAsync(User).Result)
            {
                List<Photo> photos = new List<Photo>();
                using (var client = new HttpClient())
                {
                    var responseStr = client.GetStringAsync($"https://api.instagram.com/v1/users/{User.UserId}/media/recent/" +
                                                              $"?access_token={access_token}&Count=30").Result;
                    var insta = JsonConvert.DeserializeObject<DTO.Insta>(responseStr);
                    var api = new EmotionsAPIGetter();
                    int yes = 0, no = 0;
                    foreach (var i in insta.Data)
                    {
                        try
                        {
                            var uri = i.Images.StandartResoulution.Uri;
                            var emo = api.GetEmotionsByUrl(uri).Result;
                            Photo photo = new Photo
                            {
                                PhotoId = i.Id,
                                UserId = User.UserId,

                                LinkLow = i.Images.LowResolution.Uri,
                                LinkStandard = i.Images.StandartResoulution.Uri,
                                LinkThumbnail = i.Images.Thumbnail.Uri,

                                Anger = emo[Emotions.Anger],
                                Fear = emo[Emotions.Fear],
                                Happiness = emo[Emotions.Happiness],
                                Sadness = emo[Emotions.Sadness],
                                Surprise = emo[Emotions.Sadness]
                            };
                            photos.Add(photo);
                            if (!DB.InsertPhotoAsync(photo).Result)
                                throw new ArgumentException("Cannot add photo of new user into db");
                            yes++;
                        }
                        catch { no++; }

                        _loading?.Invoke(yes, no, insta.Data.Count);
                    }
                    Photos = photos;
                }
            }
            else
            {
                Photos = DB.GetUserPhotosAsync(User).Result;
            }
        }
    }
}