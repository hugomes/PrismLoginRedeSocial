using Android.App;
using Android.Content;
using Newtonsoft.Json;
using PrismLoginRedeSocial.Droid;
using PrismLoginRedeSocial.Model;
using PrismLoginRedeSocial.Views;
using System;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FacebookLoginPage), typeof(LoginFacebookRenderer))]
namespace PrismLoginRedeSocial.Droid
{
    public class LoginFacebookRenderer : PageRenderer
    {
        public LoginFacebookRenderer(Context context):base (context)
        {
            var oauth = new OAuth2Authenticator(
                clientId: "2251808024876470",
                scope: "email", //https://developers.facebook.com/docs/facebook-login/permissions/
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html")
            );

            oauth.Completed += async (sender, args) =>
            {
                if (args.IsAuthenticated)
                {
                    var token = args.Account.Properties["access_token"].ToString();

                    var requisicao = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=name,email"), null, args.Account);
                    var resposta = await requisicao.GetResponseAsync();

                    var obj = JsonConvert.DeserializeObject<FacebookLogin>(resposta.GetResponseText());

                    App.GoToStartPage(obj.Name, obj.Email);
                }
                else
                {
                    App.GoToStartPage("Não foi possível fazer o login");
                }
            };

            var activity = this.Context as Activity;
            activity.StartActivity(oauth.GetUI(activity));
        }
    }
}