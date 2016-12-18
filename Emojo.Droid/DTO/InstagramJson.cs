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
    class Insta
    {
        [JsonProperty("data")]
        public List<Media> Data { get; set; }
    }

    class Media
    {
        [JsonProperty("images")]
        public Images Images { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    class Images
    {
        [JsonProperty("low_resolution")]
        public Resolution LowResolution { get; set; }
        [JsonProperty("thumbnail")]
        public Resolution Thumbnail { get; set; }
        [JsonProperty("standard_resolution")]
        public Resolution StandartResoulution { get; set; }
    }

    class Resolution
    {
        [JsonProperty("url")]
        public string Uri { get; set; }
    }
}