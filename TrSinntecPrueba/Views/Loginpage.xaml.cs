using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TrSinntecPrueba.Models;
using System.Collections.Generic;
using System.Linq;
using TrSinntecPrueba.ViewModel;


namespace TrSinntecPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loginpage : ContentPage
    {
        public Loginpage()
        {
            InitializeComponent();
            BindingContext = new VMwelcomepage(Navigation);
        }

        private async void Goregister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registerpage());

        }

        private async void Gohome_Clicked(object sender, EventArgs e)
        {
            // Verificar que los campos no estén vacíos
            if (string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
            {
                await DisplayAlert("Error", "Ambos campos son obligatorios.", "OK");
                return;
            }

            var loginRequest = new LoginRequest
            {
                email = EmailEntry.Text,
                password = PasswordEntry.Text
            };

            await LoginUserAsync(loginRequest);
        }
        private async Task LoginUserAsync(LoginRequest request)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync("https://practice.sinntec.com.mx/api/v1/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                    if (loginResponse != null && loginResponse.success)
                    {
                        var token = loginResponse.data.access_token;
                        await SaveTokenAsync(token);
                        await DisplayAlert("¡Qué gusto verte!", "Inicio de sesión exitoso!", "OK");
                        await Navigation.PushAsync(new MainPage(token));
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error de inicio de sesión: " + string.Join(", ", loginResponse.message), "OK");
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", "Error de inicio de sesión: " + errorContent, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Ocurrió un error: " + ex.Message, "OK");
            }
        }

        private async Task SaveTokenAsync(string token)
        {
            try
            {
                await SecureStorage.SetAsync("access_token", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se puede guardar el token: {ex.Message}");
            }
        }

        private async Task<string> GetTokenAsync()
        {
            try
            {
                return await SecureStorage.GetAsync("access_token");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se puede obtener el token: {ex.Message}");
                return null;
            }
        }

    }
   
}
