using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Emojo.Droid.DTO
{
    class User
    {
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("profile_picture")]
        public string ProfilePicture { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
    }
}