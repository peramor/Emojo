using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Xamarin.Auth;
using Android.Content.Res;
using System.IO;
using Android.Support.V7.App;
using Emojo.Lib;
using Newtonsoft.Json;
using Emojo.Lib.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;
using Emojo.Lib.EmotionAPI;
using System.Collections.Generic;
using Android.Content;

namespace Emojo.Droid
{
    [Activity(Label = "Emojo", MainLauncher = true
        , ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait
        , Theme = "@style/MyTheme.Login")]
    public class LoginActivity : AppCompatActivity
    {
        private string client_id;
        private string client_secret;
        Button btnLogin;
        TextView status;

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
            status = FindViewById<TextView>(Resource.Id.textViewLogIn);
            btnLogin.Click += BtnLogin_Click;
        }

        public override void OnBackPressed()
        { }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var auth = new OAuth2Authenticator(client_id
                                              , client_secret
                                              , "basic follower_list"
                                              , new Uri("https://api.instagram.com/oauth/authorize/")
                                              , new Uri($"https://emojo.azurewebsites.net/auth?client={client_id}&secret={client_secret}")
                                              , new Uri("https://api.instagram.com/oauth/access_token"));
            auth.AllowCancel = true;
            auth.Completed += (s, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    btnLogin.Enabled = false;

                    var loggedInAccount = eventArgs.Account;
                    GoToMainActivity(loggedInAccount);
                    if (eventArgs.IsAuthenticated)
                        auth = null;
                }
            };

            var ui = auth.GetUI(this);
            StartActivityForResult(ui, -1);
        }

        public async void GoToMainActivity(Xamarin.Auth.Account account)
        {
            btnLogin.Text = "Loading";

            string access_token = account.Properties["access_token"];
            string userStr = account.Properties["user"];
            var userDTO = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<DTO.User>(userStr));
            var user = new User
            {
                FullName = userDTO.FullName,
                ProfilePhoto = userDTO.ProfilePicture,
                UserId = userDTO.Id,
                UserName = userDTO.UserName
            };

            Task task = new Task(() =>
            {
                Repository repo = new Repository(user, access_token
                    ,  (yes, no, total) => 
                    {
                        RunOnUiThread(() =>
                        {
                            status.Text = $"{yes + no} ({yes}) / {total}";
                        });
                    });
            });

            task.Start();

            await task.ContinueWith((t) => {
                StartActivity(new Intent(this, typeof(MainActivity)));
            });
        }
    }
}