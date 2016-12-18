using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emojo.Lib.Models;

namespace Emojo.Lib.EmotionAPI {
    public interface IEmotionsAPIGetter {
        Task<Photo> GetEmotionRatings(InstagramPhoto photo);
        Task<List<int>> GetFaceCounts(List<InstagramPhoto> photos);
        Task<List<int>> GetFaceCounts(List<Photo> photos);
    }
}
