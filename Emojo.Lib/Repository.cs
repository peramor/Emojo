using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Emojo.Lib {

    public enum Emotions { Anger, Happiness, Fear, Sadness, Surprise}
    
    public class Repository {

        public async Task<Dictionary<Emotions,double>> GetEmotionDictionary(User user) {
            var dict = new Dictionary<Emotions, double>();
            string json_string;
            using (var client = new HttpClient()) {
                var query = string.Format("SELECT * FROM Photos WHERE userId = {0}",user.UserId);
                var uri = string.Format("http://emojo.azurewebsites.net/db/query?sql={0}&appId=qwertyuiop", query);
                var message = await client.GetAsync(uri);
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
        
        public async Task<Dictionary<Emotions,double>> GetEmotionDictionary(Photo photo) {
            var dict = new Dictionary<Emotions, double>();
            string json_string;
            using (var client = new HttpClient()) {
                var query = string.Format("SELECT * FROM Photos WHERE PhotoId = \'{0}\'", photo.PhotoId);
                var uri = string.Format("http://emojo.azurewebsites.net/db/query?sql={0}&appId=qwertyuiop", query);
                var message = await client.GetAsync(uri);
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
        
        
        public async Task<Dictionary<string,List<Emotions>>> GetMaxByEmotion(User user) {
            var dict = new Dictionary<string,List<Emotions>>();
            string json_string;
            using (var client = new HttpClient()) {
                var query = string.Format("SELECT * FROM Photos WHERE userId = {0}", user.UserId);
                var uri = string.Format("http://emojo.azurewebsites.net/db/query?sql={0}&appId=qwertyuiop", query);
                var message = await client.GetAsync(uri);
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
            }
            else {
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
        
    }
}
