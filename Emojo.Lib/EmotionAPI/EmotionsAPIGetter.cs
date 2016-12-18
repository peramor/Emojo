using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion;

namespace Emojo.Lib.EmotionAPI {
    public class EmotionsAPIGetter : IEmotionsAPIGetter {
        private EmotionServiceClient recognizer;

        public EmotionsAPIGetter() {
            recognizer = new EmotionServiceClient(Properties.Resources.EmotionSubscriptionKey);
        }

        public async Task<Dictionary<Emotions, double>> GetEmotionsByUrl(string url)
        {
            var emotions = await recognizer.RecognizeAsync(url);
            if (emotions.Length == 0)
                throw new ArgumentException("There are no faces on the photo, unable to recognize");

            double[] emotionarr = new double[5];
            emotionarr[0] = emotions.Average(e => e.Scores.Anger);
            emotionarr[1] = emotions.Average(e => e.Scores.Happiness);
            emotionarr[2] = emotions.Average(e => e.Scores.Fear);
            emotionarr[3] = emotions.Average(e => e.Scores.Sadness);
            emotionarr[4] = emotions.Average(e => e.Scores.Surprise);
            var sum = emotionarr.Sum();
            for (int i = 0; i < 5; i++)
            {
                emotionarr[i] /= sum;
            }

            return new Dictionary<Emotions, double>
            {
                {Emotions.Anger, emotionarr[0]},
                {Emotions.Happiness, emotionarr[1]},
                {Emotions.Fear, emotionarr[2]},
                {Emotions.Sadness, emotionarr[3]},
                {Emotions.Surprise, emotionarr[4]},
            };
        }

        public async Task<Photo> GetEmotionRatings (APIModels.InstagramPhoto photo) {
            var emotions = await recognizer.RecognizeAsync(photo.LinkStandard);
            if (emotions.Length == 0) {
                throw new ArgumentException("There are no faces on the photo, unable to recognize");
            }
            double[] emotionarr = new double[5];
            emotionarr[0] = emotions.Average(e => e.Scores.Anger);
            emotionarr[1] = emotions.Average(e => e.Scores.Happiness);
            emotionarr[2] = emotions.Average(e => e.Scores.Fear);
            emotionarr[3] = emotions.Average(e => e.Scores.Sadness);
            emotionarr[4] = emotions.Average(e => e.Scores.Surprise);
            var sum = emotionarr.Sum();
            for (int i = 0; i < 5; i++) {
                emotionarr[i] /= sum;
            }
            Photo finalPhoto = new Photo {
                PhotoId = photo.PhotoId,
                LinkStandard = photo.LinkStandard,
                LinkLow = photo.LinkLow,
                LinkThumbnail = photo.LinkThumbnail,
                UserId = photo.User.UserId,
                Anger = emotionarr[0],
                Happiness = emotionarr[1],
                Fear = emotionarr[2],
                Sadness = emotionarr[3],
                Surprise = emotionarr[4]
            };
            return finalPhoto;
        }

        public async Task<List<int>> GetFaceCounts(List<APIModels.InstagramPhoto> photos) {
            int countWithout = 0;
            int countAlone = 0;
            int countMany = 0;
            foreach (var photo in photos) {
                var emotions = await recognizer.RecognizeAsync(photo.LinkStandard);
                if (emotions.Length == 0) {
                    countWithout++;
                } else if (emotions.Length == 1) {
                    countAlone++;
                } else {
                    countMany++;
                }
            }
            return new List<int> { countWithout, countAlone, countMany };
        }

        public async Task<List<int>> GetFaceCounts(List<Photo> photos) {
            int countWithout = 0;
            int countAlone = 0;
            int countMany = 0;
            foreach (var photo in photos) {
                var emotions = await recognizer.RecognizeAsync(photo.LinkStandard);
                if (emotions.Length == 0) {
                    countWithout++;
                } else if (emotions.Length == 1) {
                    countAlone++;
                } else {
                    countMany++;
                }
            }
            return new List<int> { countWithout, countAlone, countMany };
        }

    }
}
