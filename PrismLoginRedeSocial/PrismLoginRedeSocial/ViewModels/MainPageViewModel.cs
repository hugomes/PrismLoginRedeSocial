using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrismLoginRedeSocial.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            FacebookLoginCommand = new DelegateCommand(FacebookLogin);
        }

        public DelegateCommand FacebookLoginCommand { get; set; }

        private void FacebookLogin()
        {
            App.Current.MainPage = new Views.FacebookLoginPage();
        }
    }
}
