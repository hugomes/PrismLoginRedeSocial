using Newtonsoft.Json;
using PrismLoginRedeSocial.iOS;
using PrismLoginRedeSocial.Model;
using PrismLoginRedeSocial.Views;
using System;
using System.Runtime.Remoting.Contexts;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FacebookLoginPage), typeof(LoginFacebookRenderer))]
namespace PrismLoginRedeSocial.iOS
{
    public class LoginFacebookRenderer : PageRenderer
    {
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            var oauth = new OAuth2Authenticator(
                clientId: "2251808024876470",
                scope: "email", //https://developers.facebook.com/docs/facebook-login/permissions/
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html")
            );

            oauth.Completed += async (sender, args) =>
            {
                DismissViewController(true, null);
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

            PresentViewController(oauth.GetUI(), true, null);
            
        }
    }
}