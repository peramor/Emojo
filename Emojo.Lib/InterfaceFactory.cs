using Emojo.Lib.Instagram;
using Emojo.Lib.EmotionAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Lib {
    public static class InterfaceFactory {
        public static IInstagramGetter GetInstagramInterface() {
            return new InstagramGetter();
        }
        public static IInstagramGetter GetInstagramInterface(InstaSharp.Models.Responses.OAuthResponse token) {
            return new InstagramGetter(token);
        }
        public static IEmotionsAPIGetter GetEmotionsInterface() {
            return new EmotionsAPIGetter();
        }
    }
}
