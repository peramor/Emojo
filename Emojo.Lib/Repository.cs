using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Lib {

    public enum Emotions { Anger, Happiness, Fear, Sadness, Surprise}

    public class Repository : IDisposable {
        Context context;

        public Repository() {
            context = new Context();
        }

        public Dictionary<Emotions,double> GetEmotionDictionary(long userId) {
            var dict = new Dictionary<Emotions, double>();
            var averages = (from p in context.Photos
                            where p.User.UserId == userId
                            group p by 1 into g
                            select new {
                                Anger = g.Average(x => x.Anger),
                                Happiness = g.Average(x => x.Happiness),
                                Fear = g.Average(x => x.Fear),
                                Sadness = g.Average(x => x.Sadness),
                                Surprise = g.Average(x => x.Surprise)
                            }).First();
            dict[Emotions.Anger] = averages.Anger;
            dict[Emotions.Fear] = averages.Fear;
            dict[Emotions.Happiness] = averages.Happiness;
            dict[Emotions.Sadness] = averages.Sadness;
            dict[Emotions.Surprise] = averages.Surprise;
            return dict;
        }

        public Dictionary<Emotions,double> GetEmotionDictionary(string imageUrl) {
            var dict = new Dictionary<Emotions, double>();
            var image = (from p in context.Photos
                         where p.LinkStandard == imageUrl
                         select p).First();
            dict[Emotions.Anger] = image.Anger;
            dict[Emotions.Fear] = image.Fear;
            dict[Emotions.Happiness] = image.Happiness;
            dict[Emotions.Sadness] = image.Sadness;
            dict[Emotions.Surprise] = image.Surprise;
            return dict;
        }

        public Dictionary<string,List<Emotions>> GetMaxByEmotion(long userId) {
            var dict = new Dictionary<string,List<Emotions>>();
            var photos = (from p in context.Photos
                         where p.User.UserId == userId
                         group p by 1 into g
                         select new {
                             Anger = g.Where(x => x.Anger == g.Max(y => y.Anger)).FirstOrDefault().LinkStandard,
                             Happiness = g.Where(x => x.Happiness == g.Max(y => y.Happiness)).FirstOrDefault().LinkStandard,
                             Fear = g.Where(x => x.Fear == g.Max(y => y.Fear)).FirstOrDefault().LinkStandard,
                             Sadness = g.Where(x => x.Sadness == g.Max(y => y.Sadness)).FirstOrDefault().LinkStandard,
                             Surprise = g.Where(x => x.Surprise == g.Max(y => y.Surprise)).FirstOrDefault().LinkStandard
                         }).First();

            if (dict.ContainsKey(photos.Anger)) {
                dict[photos.Anger].Add(Emotions.Anger);
            }
            else {
                dict[photos.Anger] = new List<Emotions> { Emotions.Anger };
            }

            if (dict.ContainsKey(photos.Fear)) {
                dict[photos.Fear].Add(Emotions.Fear);
            } else {
                dict[photos.Fear] = new List<Emotions> { Emotions.Fear };
            }

            if (dict.ContainsKey(photos.Happiness)) {
                dict[photos.Happiness].Add(Emotions.Happiness);
            } else {
                dict[photos.Happiness] = new List<Emotions> { Emotions.Happiness };
            }

            if (dict.ContainsKey(photos.Sadness)) {
                dict[photos.Sadness].Add(Emotions.Sadness);
            } else {
                dict[photos.Sadness] = new List<Emotions> { Emotions.Sadness };
            }

            if (dict.ContainsKey(photos.Surprise)) {
                dict[photos.Surprise].Add(Emotions.Surprise);
            } else {
                dict[photos.Surprise] = new List<Emotions> { Emotions.Surprise };
            }

            return dict;
        }

        public void Dispose() {
            context.Dispose();
        }
    }
}
