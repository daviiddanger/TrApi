using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrSinntecPrueba.Models;
using TrSinntecPrueba.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrSinntecPrueba.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Localrecordspage : ContentPage
    {
        private List<Languages> languagesList;
        public Localrecordspage ()
		{
			InitializeComponent ();
            LoadData();
            SetupFilters();
        }
        private void LoadData()
        {
            // Cargar datos de languages
            languagesList = App.Database.GetConnection().Table<Languages>().ToList();
            languagesListView.ItemsSource = languagesList;

            // Cargar datos de frameworks
            var frameworks2 = App.Database.GetConnection().Table<Frameworks>().ToList();
            frameworksListView2.ItemsSource = frameworks2;

            // Cargar datos de frameworks con nombres de lenguajes
            var frameworks = App.Database.GetConnection().Table<Frameworks>().ToList();
            var frameworksWithLanguages = from framework in frameworks
                                          join language in languagesList
                                          on framework.language_id equals language.id
                                          select new FrameworkWithLanguage
                                          {
                                              FrameworkName = framework.name,
                                              LanguageName = language.name
                                          };
            frameworksListView.ItemsSource = frameworksWithLanguages.ToList();

            // Cargar lenguajes en el picker
            languagePicker.ItemsSource = languagesList.Select(l => l.name).ToList();
        }
      
        private void SetupFilters()
        {
            // Configurar evento para el filtro por nombre de framework
            searchEntry.TextChanged += (sender, e) => FilterByName(searchEntry.Text);

            // Configurar evento para el filtro por lenguaje
            languagePicker.SelectedIndexChanged += (sender, e) =>
            {
                var selectedLanguage = languagePicker.SelectedItem as string;
                FilterByLanguage(selectedLanguage);
            };
        }

        private void FilterByName(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                frameworksListView.ItemsSource = GetFrameworksWithLanguages();
            }
            else
            {
                var filteredFrameworks = GetFrameworksWithLanguages()
                    .Where(f => f.FrameworkName.ToLower().Contains(searchText.ToLower()));
                frameworksListView.ItemsSource = filteredFrameworks.ToList();
            }
        }

        private void FilterByLanguage(string languageName)
        {
            if (string.IsNullOrEmpty(languageName) || languageName == "Todos")
            {
                frameworksListView.ItemsSource = GetFrameworksWithLanguages();
            }
            else
            {
                var language = languagesList.FirstOrDefault(l => l.name == languageName);
                if (language != null)
                {
                    var filteredFrameworks = GetFrameworksWithLanguages()
                        .Where(f => f.LanguageName == language.name);
                    frameworksListView.ItemsSource = filteredFrameworks.ToList();
                }
            }
        }
        private List<FrameworkWithLanguage> GetFrameworksWithLanguages()
        {
            var frameworks = App.Database.GetConnection().Table<Frameworks>().ToList();
            var frameworksWithLanguages = from framework in frameworks
                                          join language in languagesList
                                          on framework.language_id equals language.id
                                          select new FrameworkWithLanguage
                                          {
                                              FrameworkName = framework.name,
                                              LanguageName = language.name
                                          };
            return frameworksWithLanguages.ToList();
        }

        private void OnAddSampleDataClicked(object sender, EventArgs e)
        {
            var database = App.Database.GetConnection();

            // Verificar si la tabla languages ya tiene datos
            if (!database.Table<Languages>().Any())
            {
                var languages = new List<Languages>
                {
                    new Languages { name = "JavaScript" },
                    new Languages { name = "Python" },
                    new Languages { name = "Java" },
                    new Languages { name = "Ruby" },
                    new Languages { name = "PHP" },
                    new Languages { name = "C#" },
                    new Languages { name = "Swift" }
                };
                database.InsertAll(languages);
            }

            // Verificar si la tabla frameworks ya tiene datos
            if (!database.Table<Frameworks>().Any())
            {
                var frameworks = new List<Frameworks>
                {
                    new Frameworks { name = "React", language_id = 1 },
                    new Frameworks { name = "Angular", language_id = 1 },
                    new Frameworks { name = "Vue.js", language_id = 1 },
                    new Frameworks { name = "Django", language_id = 2 },
                    new Frameworks { name = "Flask", language_id = 2 },
                    new Frameworks { name = "Spring", language_id = 3 },
                    new Frameworks { name = "Ruby on Rails", language_id = 4 },
                    new Frameworks { name = "Laravel", language_id = 5 },
                    new Frameworks { name = ".NET Core", language_id = 6 },
                    new Frameworks { name = "Vapor", language_id = 7 }
                };
                database.InsertAll(frameworks);
            }

            // Recargar los datos en las listas
            LoadData();
        }
        private void OnSearchClicked(object sender, EventArgs e)
        {
            FilterByName(searchEntry.Text);
        }

    }
}