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

        public InstagramGetter() {
            config = new InstagramConfig("41bb2944418b4e0d97cee44411b35261", "b9edde48cbe24132819414d1919d433c", "http://localhost");
            oauth = new OAuth(config);
        }

        public string GetAuthLink() {
            var scopes = new List<OAuth.Scope> { OAuth.Scope.Basic };
            return OAuth.AuthLink(config, scopes, OAuth.ResponseType.Code);

        }

        public async Task<List<InstagramData>> GetData(string code) {
            var authInfo = await oauth.RequestToken(code);
            var userInfo = new InstaSharp.Endpoints.Users(config, authInfo);
            var user = await userInfo.GetSelf();
            return new List<InstagramData> { new InstagramData { Name = user.Data.Username} };
        }


    }
}
