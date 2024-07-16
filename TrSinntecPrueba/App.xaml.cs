using System;
using TrSinntecPrueba.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrSinntecPrueba
{
    public partial class App : Application
    {
        static DatabaseContext databaseContext;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Welcomepage());

        }
        public static DatabaseContext Database
        {
            get
            {
                if (databaseContext == null)
                {
                    databaseContext = new DatabaseContext();
                }
                return databaseContext;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
