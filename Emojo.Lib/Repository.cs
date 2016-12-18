using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emojo.Lib.Instagram;
using Emojo.Lib.EmotionAPI;
using Emojo.Lib.ViewModels;

namespace Emojo.Lib {
    public class Repository {
        private IInstagramGetter getter;
        private IEmotionsAPIGetter emgetter;

        public User User { get; set; }
        public List<Photo> Photos { get; set; }
        public List<int> PeopleCounts { get; set; }

        public Repository(IInstagramGetter getter, IEmotionsAPIGetter emgetter) {
            while (getter.Token == null) {
            }
            User = new User {
                UserId = getter.Token.User.Id,
                FullName = getter.Token.User.FullName,
                UserName = getter.Token.User.Username,
                ProfilePhoto = getter.Token.User.ProfilePicture
            };
            this.getter = getter;
            this.emgetter = emgetter;
        }

        public async Task LoadUserPhotosAsync() {
            if (! await DB.InsertUserAsync(User)) {
                Photos = await DB.GetUserPhotosAsync(User);
                var recent_photos = await getter.GetRecentMedia();
                foreach (var rec_photo in recent_photos) {
                    if (!Photos.Any(x => x.PhotoId == rec_photo.PhotoId)) {
                        try {
                            var new_photo = await emgetter.GetEmotionRatings(rec_photo);
                            await DB.InsertPhotoAsync(new_photo);
                            Photos.Add(new_photo);
                        } catch { }
                    }
                }
            } else {
                var recent_photos = await getter.GetRecentMedia();
                foreach (var rec_photo in recent_photos) {
                    try {
                        var new_photo = await emgetter.GetEmotionRatings(rec_photo);
                        await DB.InsertPhotoAsync(new_photo);
                        Photos.Add(new_photo);
                    } catch { }
                }
            }
            PeopleCounts = await emgetter.GetFaceCounts(Photos);
        }

        public Dictionary<string, List<Emotions>> GetMaxByEmotion() {
            var dict = new Dictionary<string, List<Emotions>>();            
            var best_photos = (from p in Photos
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

        public Dictionary<Emotions, double> GetEmotionDictionary() {
            var dict = new Dictionary<Emotions, double>();
            var averages = (from p in Photos
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

    }
}
