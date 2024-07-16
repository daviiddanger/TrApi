using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrSinntecPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Nav : ContentPage
    {
        public Nav()
        {
            InitializeComponent();
        }

        private async void Api_Clicked(object sender, EventArgs e)
        {
            var uri = new Uri("https://practice.sinntec.com.mx/");
            await Launcher.OpenAsync(uri);
        }

        private async void Sinntec_Clicked(object sender, EventArgs e)
        {
            var uri = new Uri("https://sinntec.com.mx/");
            await Launcher.OpenAsync(uri);
        }

        private async void Localrecords_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Localrecordspage());

        }

        private async void Remotelogs_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Remotelogspage());
        }
    }
}