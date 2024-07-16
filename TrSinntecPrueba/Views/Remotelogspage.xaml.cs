using System;
using System.Threading.Tasks;
using TrSinntecPrueba.Models;
using TrSinntecPrueba.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrSinntecPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Remotelogspage : ContentPage
    {
        private readonly ApiService _apiService;
        private int? _packageId; // Almacenar el ID del paquete si se está actualizando

        public Remotelogspage()
        {
            InitializeComponent();
            BindingContext = new VMwelcomepage(Navigation);
            _apiService = new ApiService();
            Location();

        }

        private async void OnRegisterPackageClicked(object sender, EventArgs e)
        {
            if (ValidateEntries())
            {
                var package = CreatePackageFromEntries();
                var success = await _apiService.RegisterPackageAsync(package);

                if (success)
                {
                    await DisplayAlert("Perfecto", "¡Package registrado exitosamente!", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Fallo el registro package.", "OK");
                }
            }
        }

        private async void OnGetPackageDataClicked(object sender, EventArgs e)
        {
            if (int.TryParse(NameId.Text, out int packageId))
            {
                var package = await _apiService.GetPackageAsync(packageId);

                if (package != null)
                {
                    _packageId = packageId;
                    FillEntriesWithPackageData(package);
                }
                else
                {
                    await DisplayAlert("Error", "No se encontró el paquete.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "ID de paquete inválido ingrese por ejemplo 43,52,54 etc.", "OK");
            }
        }

        private async void OnUpdatePackageClicked(object sender, EventArgs e)
        {
            if (_packageId.HasValue && ValidateEntries())
            {
                var package = CreatePackageFromEntries();
                var success = await _apiService.UpdatePackageAsync(_packageId.Value, package);

                if (success)
                {
                    await DisplayAlert("Perfecto", "¡Package actualizado exitosamente!", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Fallo la actualización del package.", "OK");
                }
            }
        }

        private async void OnDeletePackageClicked(object sender, EventArgs e)
        {
            if (_packageId.HasValue)
            {
                var success = await _apiService.DeletePackageAsync(_packageId.Value);

                if (success)
                {
                    await DisplayAlert("Perfecto", "¡Package eliminado exitosamente!", "OK");
                    ClearEntries();
                }
                else
                {
                    await DisplayAlert("Error", "Fallo la eliminación del package.", "OK");
                }
            }
        }

        private bool ValidateEntries()
        {
            if (string.IsNullOrEmpty(NameEntry.Text) || string.IsNullOrEmpty(DescriptionEntry.Text) ||
                string.IsNullOrEmpty(PlatformIdEntry.Text) || string.IsNullOrEmpty(WidthEntry.Text) ||
                string.IsNullOrEmpty(HeightEntry.Text) || string.IsNullOrEmpty(LargeEntry.Text) ||
                string.IsNullOrEmpty(WeightEntry.Text) || string.IsNullOrEmpty(IsFragileEntry.Text) ||
                string.IsNullOrEmpty(LatitudeEntry.Text) || string.IsNullOrEmpty(LongitudeEntry.Text))
            {
                DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return false;
            }

            return true;
        }

        private Data CreatePackageFromEntries()
        {
            return new Data
            {
                name = NameEntry.Text,
                description = DescriptionEntry.Text,
                platform_id = int.Parse(PlatformIdEntry.Text),
                characteristic = new characteristic
                {
                    width = int.Parse(WidthEntry.Text),
                    height = int.Parse(HeightEntry.Text),
                    large = int.Parse(LargeEntry.Text),
                    weight = double.Parse(WeightEntry.Text),
                    is_fragile = int.Parse(IsFragileEntry.Text)
                },
                location = new location
                {
                    latitude = LatitudeEntry.Text,
                    longitude = LongitudeEntry.Text
                }
            };
        }

        private void FillEntriesWithPackageData(Data package)
        {
            NameEntry.Text = package.name;
            DescriptionEntry.Text = package.description;
            PlatformIdEntry.Text = package.platform_id.ToString();
            WidthEntry.Text = package.characteristic.width.ToString();
            HeightEntry.Text = package.characteristic.height.ToString();
            LargeEntry.Text = package.characteristic.large.ToString();
            WeightEntry.Text = package.characteristic.weight.ToString();
            IsFragileEntry.Text = package.characteristic.is_fragile.ToString();
            LatitudeEntry.Text = package.location.latitude;
            LongitudeEntry.Text = package.location.longitude;
        }

        private void ClearEntries()
        {
            _packageId = null;
            NameId.Text = string.Empty;
            NameEntry.Text = string.Empty;
            DescriptionEntry.Text = string.Empty;
            PlatformIdEntry.Text = string.Empty;
            WidthEntry.Text = string.Empty;
            HeightEntry.Text = string.Empty;
            LargeEntry.Text = string.Empty;
            WeightEntry.Text = string.Empty;
            IsFragileEntry.Text = string.Empty;
            LatitudeEntry.Text = string.Empty;
            LongitudeEntry.Text = string.Empty;
        }
        public async void Location()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            if (status == PermissionStatus.Granted)
            {
                // Permisos concedidos - puedes acceder a la ubicación
            }
            else
            {
                // Permisos denegados
            }
        }
        }
    }
