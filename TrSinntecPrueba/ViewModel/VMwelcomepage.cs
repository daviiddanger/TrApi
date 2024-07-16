using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using TrSinntecPrueba.Views;

namespace TrSinntecPrueba.ViewModel
{
    public class VMwelcomepage : INotifyPropertyChanged
    {
        #region VARIABLES
        public INavigation Navigation;
        private bool _isBusy;
        #endregion
        #region CONSTRUCTOR
        public VMwelcomepage(INavigation navigation)
        {

            Navigation = navigation;
        }
        #endregion
        #region OBJETOS

        #endregion
        #region PROCESOS
        public async Task Gologin()
        {
            IsBusy = true;

            // Simular una operación de larga duración
            await Task.Delay(3000);

            IsBusy = false;

            // Navegar a SecondPage
              await Navigation.PushAsync(new Loginpage());

        }
        public async Task Loading()
        {
            IsBusy = true;

            // Simular una operación de larga duración
            await Task.Delay(3000);

            IsBusy = false;
        }
        #endregion

        #region COMANDOS
        public ICommand Gologincommand => new Command(async () => await Gologin());
        public ICommand loadingcommand => new Command(async () => await Loading());
        #endregion


        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }


        

      
       
       
      








        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
