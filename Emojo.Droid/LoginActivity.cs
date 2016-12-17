using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Xamarin.Auth;
using Android.Content.Res;
using System.IO;
using Android.Support.V7.App;

namespace Emojo.Droid
{
    [Activity(Label = "Login", MainLauncher = true
        , ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait
        , Theme = "@style/MyTheme.Login")]
    public class LoginActivity : AppCompatActivity
    {
        private string client_id;
        private string client_secret;
        Button btnLogin;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);            

            AssetManager assets = Assets;
            using (var sr = new StreamReader(assets.Open("credential.txt")))
            {
                client_id = sr.ReadLine();
                client_secret = sr.ReadLine();
            }
           
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);

            btnLogin = FindViewById<Button>(Resource.Id.buttonLogin);
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var auth = new OAuth2Authenticator(client_id
                                              , client_secret
                                              , "basic follower_list"
                                              , new Uri("https://api.instagram.com/oauth/authorize/")
                                              , new Uri($"https://emojo.azurewebsites.net/auth?client={client_id}&secret={client_secret}")
                                              , new Uri("https://api.instagram.com/oauth/access_token"));

            auth.Completed += (s, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    btnLogin.Enabled = false;

                    var loggedInAccount = eventArgs.Account;
                    AccountStore.Create(this).Save(loggedInAccount, "Instagram");
                    GoToMainActivity(loggedInAccount);
                    if (eventArgs.IsAuthenticated) {
                        auth = null;
                    };
                }
            };

            var ui = auth.GetUI(this);
            StartActivityForResult(ui, -1);
        }

        public async void GoToMainActivity(Xamarin.Auth.Account account)
        {
            
            //string access_token = account.Properties["access_token"];
            //string userStr = account.Properties["user"];
            //var user = JsonConvert.DeserializeObject<DTO.User>(userStr);

            //var photos = await getter.GetRecentPhotosRecognized(new OAuthBuildModel
            //{
            //    AccessToken = access_token,
            //    FullName = user.FullName,
            //    ProfilePicture = user.ProfilePicture,
            //    Id = user.Id,
            //    Username = user.UserName
            //});

            //Intent intent = new Intent(this, typeof(MainActivity));

            //var thumbNailPhotos = photos.Select(p => p.LinkThumbnail).ToList();
            //var thumbNailPhotosArr = new string[thumbNailPhotos.Count];
            //for (int i = 0; i < thumbNailPhotosArr.Length; i++)
            //{
            //    thumbNailPhotosArr[i] = thumbNailPhotos[i];
            //}

            //intent.PutExtra("photos", thumbNailPhotosArr);
            //StartActivity(intent);
        }
    }
}