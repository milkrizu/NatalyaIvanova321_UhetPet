using NatalyaIvanova321_UhetPet.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NatalyaIvanova321_UhetPet
{
    /// <summary>
    /// Interaction logic for PagePets.xaml
    /// </summary>
    public partial class PagePets : Page
    {
        private string _currentUser;
        public PagePets(string currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            LoadPets();
        }

        private void LoadPets()
        {
            var pets = DatabaseConnectionClass.DatabaseConnection.Pets.ToList();

            if (_currentUser == "Andrey pirokinezis")
            {
                pets = pets.Where(p => p.Names == "Ra").ToList();
            }
            else if (_currentUser == "Delya")
            {
                pets = pets.Where(p => p.Names == "Nubi").ToList();
            }

            PetsListView.ItemsSource = pets;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredPets = DatabaseConnectionClass.DatabaseConnection.Pets
                .Where(p => p.Names.ToLower().Contains(searchText) || p.Opisanie.ToLower().Contains(searchText))
                .ToList();

            
            if (_currentUser == "Andrey pirokinezis")
            {
                filteredPets = filteredPets.Where(p => p.Names == "Ra").ToList();
            }
            else if (_currentUser == "Delya")
            {
                filteredPets = filteredPets.Where(p => p.Names == "Nubi").ToList();
            }

            PetsListView.ItemsSource = filteredPets;
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSort = SortComboBox.SelectedItem as ComboBoxItem;

            if (selectedSort != null)
            {
                var pets = DatabaseConnectionClass.DatabaseConnection.Pets.AsQueryable();

                switch (selectedSort.Content.ToString())
                {
                    case "Имя (А-Я)":
                        pets = pets.OrderBy(p => p.Names);
                        break;
                    case "Имя (Я-А)":
                        pets = pets.OrderByDescending(p => p.Names);
                        break;
                    case "Описание (А-Я)":
                        pets = pets.OrderBy(p => p.Opisanie);
                        break;
                    case "Описание (Я-А)":
                        pets = pets.OrderByDescending(p => p.Opisanie);
                        break;
                }

                if (_currentUser == "Andrey pirokinezis")
                {
                    pets = pets.Where(p => p.Names == "Ra");
                }
                else if (_currentUser == "Delya")
                {
                    pets = pets.Where(p => p.Names == "Nubi");
                }

                PetsListView.ItemsSource = pets.ToList();
            }
        }

        private void AddPetButton_Click(object sender, RoutedEventArgs e)
        {
            PageAddPet pageAddPet = new PageAddPet();
            NavigationService.Navigate(pageAddPet);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
