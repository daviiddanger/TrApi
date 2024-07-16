using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TrSinntecPrueba.Models;
using TrSinntecPrueba.ViewModel;

namespace TrSinntecPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registerpage : ContentPage
    {
        public Registerpage()
        {
            InitializeComponent();
            BindingContext = new VMwelcomepage(Navigation);
        }

        private async void Gologin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }
        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // Verificar que todos los campos no estén vacíos
            if (string.IsNullOrEmpty(NameEntry.Text) ||
                string.IsNullOrEmpty(EmailEntry.Text) ||
                string.IsNullOrEmpty(PasswordEntry.Text) ||
                string.IsNullOrEmpty(PasswordConfirmationEntry.Text))
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }

            // Verificar que las contraseñas coincidan
            if (PasswordEntry.Text != PasswordConfirmationEntry.Text)
            {
                await DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
                return;
            }

            var registerRequest = new RegisterRequest
            {
                name = NameEntry.Text,
                email = EmailEntry.Text,
                password = PasswordEntry.Text,
                password_confirmation = PasswordConfirmationEntry.Text
            };

            await RegisterUserAsync(registerRequest);
        }

        private async Task RegisterUserAsync(RegisterRequest request)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync("https://practice.sinntec.com.mx/api/v1/register", content);

                if (response.IsSuccessStatusCode)
                {
                    // Registro exitoso
                    var responseContent = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Bienvenido", "¡Registro exitoso!", "OK");
                    Limpiar();
                    await Navigation.PopAsync();
                }
                else
                {
                    // Manejo de errores de la solicitud
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Limpiar();
                    await DisplayAlert("Error", "Registro fallido: " + errorContent, "OK");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores de conexión
                await DisplayAlert("Error", "Ocurrió un error: " + ex.Message, "OK");
            }
        }
        public void Limpiar()
        {
            NameEntry.Text = "";
            EmailEntry.Text = "";
            PasswordEntry.Text = "";
            PasswordConfirmationEntry.Text = "";
        }
    
}
}