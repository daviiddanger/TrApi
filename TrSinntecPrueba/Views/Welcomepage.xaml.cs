using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrSinntecPrueba.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrSinntecPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Welcomepage : ContentPage
    {
        public Welcomepage()
        {
            InitializeComponent();
            BindingContext = new VMwelcomepage(Navigation);


        }
    }
}