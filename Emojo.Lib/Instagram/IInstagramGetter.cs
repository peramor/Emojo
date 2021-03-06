﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emojo.Lib.Models;

namespace Emojo.Lib.Instagram {
    public interface IInstagramGetter {
        InstaSharp.Models.Responses.OAuthResponse Token { get; }
        Task<User> GetUser();
        Task<List<InstagramPhoto>> GetRecentMedia();
        string GetAuthLink();
        Task GetToken(string code);
    }
}
