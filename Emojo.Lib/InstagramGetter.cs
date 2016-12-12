using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharp;
using System.Diagnostics;
using Emojo.Lib.ViewModels;

namespace Emojo.Lib {
    public class InstagramGetter {
        private InstagramConfig config;
        private OAuth oauth;
        public InstaSharp.Models.Responses.OAuthResponse token;


        public InstagramGetter() {
            config = new InstagramConfig(Properties.Resources.APIKey, Properties.Resources.APISecret, Properties.Resources.RedirectURI);
            oauth = new OAuth(config);
        }


        public string GetAuthLink() {
            var scopes = new List<OAuth.Scope> { OAuth.Scope.Basic, OAuth.Scope.Follower_List };
            return OAuth.AuthLink(config, scopes, OAuth.ResponseType.Code);

        }

        public async void GetToken(string code) {
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

        public User GetUser() {
            var user = token.User;
            return new User {
                UserId = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                ProfilePhoto = user.ProfilePicture
            };
        }

        public async Task<List<APIModels.InstagramPhoto>> GetRecentMedia() {
            var userInfo = new InstaSharp.Endpoints.Users(config, token);
            var user = GetUser();
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

        public async Task<List<User>> GetFollowRelationships() {
            var relationshipsInfo = new InstaSharp.Endpoints.Relationships(config, token);
            var user = GetUser();
            var follows = await relationshipsInfo.FollowsAll();
            return (from f in follows
                    select new User {
                        UserId = f.Id,
                        Username = f.Username,
                        FullName = f.FullName,
                        ProfilePhoto = f.ProfilePicture
                    }
                    ).ToList();
        }

    }
}
