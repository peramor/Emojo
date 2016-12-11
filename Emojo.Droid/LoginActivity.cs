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
using InstaSharp;
using System.Threading.Tasks;
using Xamarin.Auth;
using System.Net.Http;

namespace Emojo.Droid
{
    [Activity(Label = "Login", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);

            var btnLogin = FindViewById<Button>(Resource.Id.buttonLogin);
            btnLogin.Click += BtnLogin_Click;
        }

        private const string client_id = "4b929f89088547acb455273d73fe2184";
        private const string client_secret = "c9f9379d17e54c39a441cc9fc9327a96";

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
                    var loggedInAccount = eventArgs.Account;
                    AccountStore.Create(this).Save(loggedInAccount, "Instagram");
                }
            };

            var ui = auth.GetUI(this);
            StartActivityForResult(ui, -1);
        }
    }
}