using Emojo.Lib.ViewModels;
using InstaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Lib {
    public class AndroidRepository {
        public Repository repo;

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
        
        public AndroidRepository(OAuthBuildModel model) {
            var token = BuildToken(model);
            repo = new Repository(InterfaceFactory.GetInstagramInterface(token), InterfaceFactory.GetEmotionsInterface());
        }
        
    }
}
