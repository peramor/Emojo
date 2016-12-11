using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Lib.ViewModels {
    public class OAuthBuildModel {
        public string AccessToken { get; set; }
        public long Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
    }
}
