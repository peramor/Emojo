using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Lib.EmotionAPI {
    interface IEmotionsAPIGetter {
        Task<Photo> GetEmotionRatings(APIModels.InstagramPhoto photo);
        Task<List<int>> GetFaceCounts(List<APIModels.InstagramPhoto> photos);
        Task<List<int>> GetFaceCounts(List<Photo> photos);
    }
}
