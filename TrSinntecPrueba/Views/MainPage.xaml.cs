using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrSinntecPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : FlyoutPage
    {
        public MainPage(string token)
        {
            InitializeComponent();
            Flyout = new Nav(); // Define el contenido del menú lateral (flyout)
            Detail = new NavigationPage(new Homepage(token)); // Define el contenido principal de la página
        }
    }
}