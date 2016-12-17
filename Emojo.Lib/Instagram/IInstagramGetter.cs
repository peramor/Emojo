using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Lib.Instagram {
    public interface IInstagramGetter {
        InstaSharp.Models.Responses.OAuthResponse Token { get; }
        Task<User> GetUser();
        Task<List<APIModels.InstagramPhoto>> GetRecentMedia();
    }
}
