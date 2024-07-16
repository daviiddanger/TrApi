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
    public partial class Homepage : ContentPage
    {
        public Homepage()
        {
            InitializeComponent();
            
        }
        public Homepage(string token) : this()
        {
            // Constructor que recibe el token y lo muestra en un Label
            TokenLabel.Text = token;
        }
    }
}