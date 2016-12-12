using Emojo.Lib.ViewModels;
using InstaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Lib {
    class AndroidGetter {

        private InstaSharp.Models.Responses.OAuthResponse BuildToken (OAuthBuildModel model) {
            return new InstaSharp.Models.Responses.OAuthResponse {
                AccessToken = model.AccessToken,
                User = new InstaSharp.Models.UserInfo {
                    Id = model.Id,
                    Username = model.Username,
                    FullName = model.FullName,
                    ProfilePicture = model.ProfilePicture
                }
            };
        }

        public async Task<List<Photo>> GetRecentPhotosRecognized (OAuthBuildModel model) {          
            var config = new InstagramConfig(Properties.Resources.APIKey, Properties.Resources.APISecret, Properties.Resources.RedirectURI);
            var token = BuildToken(model);
            var user = new User {
                UserId = token.User.Id,
                Username = token.User.Username,
                FullName = token.User.FullName,
                ProfilePhoto = token.User.ProfilePicture
            };
            var userInfo = new InstaSharp.Endpoints.Users(config, token);
            var mediaInfo = await userInfo.RecentSelf();
            var raw_photos = (from m in mediaInfo.Data
                    where m.Type == "image"
                    select new APIModels.InstagramPhoto {
                        PhotoId = m.Id,
                        LinkStandard = m.Images.StandardResolution.Url,
                        LinkLow = m.Images.LowResolution.Url,
                        LinkThumbnail = m.Images.Thumbnail.Url,
                        User = user
                    }).ToList();
            var emgetter = new EmotionsAPIGetter();
            return await emgetter.GetEmotionRatings(raw_photos);
        }

    }
}
