using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharp;
using System.Diagnostics;
using Emojo.Lib.ViewModels;

namespace Emojo.Lib.Instagram {
    public class InstagramGetter : IInstagramGetter {
        private InstagramConfig config;
        private OAuth oauth;
        private InstaSharp.Models.Responses.OAuthResponse token;



        public InstaSharp.Models.Responses.OAuthResponse Token {
            get {
               return token;
            }
        }

        public InstagramGetter() {
            config = new InstagramConfig(Properties.Resources.APIKey, Properties.Resources.APISecret, Properties.Resources.RedirectURI);
            oauth = new OAuth(config);
        }

        public InstagramGetter(InstaSharp.Models.Responses.OAuthResponse token) : this() {
            this.token = token;
        }


        public string GetAuthLink() {
            var scopes = new List<OAuth.Scope> { OAuth.Scope.Basic, OAuth.Scope.Follower_List };
            return OAuth.AuthLink(config, scopes, OAuth.ResponseType.Code);

        }

        public async Task GetToken(string code) {
            try {
                token = await oauth.RequestToken(code);
            } catch {
                throw new OperationCanceledException("Wasn't able to authenticate.");
            }            
        }

        public void BuildToken(OAuthBuildModel model) {
            token = new InstaSharp.Models.Responses.OAuthResponse {
                AccessToken = model.AccessToken,
                User = new InstaSharp.Models.UserInfo {
                    Id = model.Id,
                    Username = model.Username,
                    FullName = model.FullName,
                    ProfilePicture = model.ProfilePicture
                }
            };
        }

        public async Task<User> GetUser() {
            var userInfo = new InstaSharp.Endpoints.Users(config, token);
            var user = await userInfo.GetSelf();
            return new User {
                UserId = user.Data.Id,
                UserName = user.Data.Username,
                FullName = user.Data.FullName,
                ProfilePhoto = user.Data.ProfilePicture
            };
        }

        public async Task<List<APIModels.InstagramPhoto>> GetRecentMedia() {
            var userInfo = new InstaSharp.Endpoints.Users(config, token);
            var user = await GetUser();
            var mediaInfo = await userInfo.RecentSelf();
            return (from m in mediaInfo.Data
                    where m.Type == "image"
                    select new APIModels.InstagramPhoto {
                        PhotoId = m.Id,
                        LinkStandard = m.Images.StandardResolution.Url,
                        LinkLow = m.Images.LowResolution.Url,
                        LinkThumbnail = m.Images.Thumbnail.Url,
                        User = user
                    }).ToList();
        }
    }
}
