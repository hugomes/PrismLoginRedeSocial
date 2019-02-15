

using Newtonsoft.Json;
using PrismLoginRedeSocial.Model;
using PrismLoginRedeSocial.UWP;
using PrismLoginRedeSocial.Views;
using System;
using Xamarin.Auth;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(FacebookLoginPage), typeof(LoginFacebookRenderer))]
namespace PrismLoginRedeSocial.UWP
{
    public class LoginFacebookRenderer : PageRenderer
    {
        public LoginFacebookRenderer()
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

                    PrismLoginRedeSocial.App.GoToStartPage(obj.Name, obj.Email);
                }
                else
                {
                    PrismLoginRedeSocial.App.GoToStartPage("Não foi possível fazer o login");
                }
            };
            Windows.UI.Xaml.Controls.Frame rootFrame = Windows.UI.Xaml.Window.Current.Content as Windows.UI.Xaml.Controls.Frame;
            rootFrame.Navigate(oauth.GetUI(), oauth);
        }
    }
}