using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrismLoginRedeSocial.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage : ContentPage
	{
		public StartPage (params string[] parametros)
		{
			InitializeComponent ();

            foreach(var para in parametros)
            {
                lbInformacao.Text += para + "\n";
            }
		}
	}
}