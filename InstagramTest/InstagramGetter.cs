using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharp;
using System.Diagnostics;

namespace InstagramTest {
    class InstagramGetter {
        InstagramConfig config;
        OAuth oauth;
        InstaSharp.Models.Responses.OAuthResponse token;


        public InstagramGetter() {
            config = new InstagramConfig(Properties.Resources.APIKey, Properties.Resources.APISecret, Properties.Resources.RedirectURI);
            oauth = new OAuth(config);
        }

        public string GetAuthLink() {
            var scopes = new List<OAuth.Scope> { OAuth.Scope.Basic, OAuth.Scope.Follower_List };
            return OAuth.AuthLink(config, scopes, OAuth.ResponseType.Code);

        }

        public async void GetToken(string code) {
            token = await oauth.RequestToken(code);
        }

        public async Task<long> GetUserId() {
            var userInfo = new InstaSharp.Endpoints.Users(config, token);
            var user = await userInfo.GetSelf();
            return user.Data.Id;
        }

        public async Task<string> GetUserName() {
            var userInfo = new InstaSharp.Endpoints.Users(config, token);
            var user = await userInfo.GetSelf();
            return user.Data.Username;
        }

        public async Task<InstaSharp.Models.User> GetUser() {
            var userInfo = new InstaSharp.Endpoints.Users(config, token);
            var user = await userInfo.GetSelf();
        }

        public async Task<List<InstaSharp.Models.Media>> GetRecentMedia() {
            var userInfo = new InstaSharp.Endpoints.Users(config, token);
            var mediaInfo = await userInfo.RecentSelf();
            return (from m in mediaInfo.Data
                    where m.Type == "image"
                    select m).ToList();
        }

        public async Task<List<InstaSharp.Models.User>> GetFollowedAll() {
            var relationshipsInfo = new InstaSharp.Endpoints.Relationships(config, token);
            return await relationshipsInfo.FollowsAll();
        }

        public async Task<InstaSharp.Models.User> GetUserById(long userId) {
            var userInfo = new InstaSharp.Endpoints.Users(config, token);
            var user = await userInfo.Get(userId);
            return user.Data;
        }

    }
}
