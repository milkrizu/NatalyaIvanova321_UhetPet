using NatalyaIvanova321_UhetPet.Classes;
using NatalyaIvanova321_UhetPet.DataBaseUhet;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for PageAddPet.xaml
    /// </summary>
    public partial class PageAddPet : Page
    {
        public PageAddPet()
        {
            InitializeComponent();
            LoadPhotosFromResources();
        }

        private void LoadPhotosFromResources()
        {
            var resourcesFolderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\milkr\\source\\repos\\NatalyaIvanova321_UhetPet\\NatalyaIvanova321_UhetPet\\Resources");
            if (Directory.Exists(resourcesFolderPath))
            {
                var imageFiles = Directory.GetFiles(resourcesFolderPath, "*.jpg")
                                           .Concat(Directory.GetFiles(resourcesFolderPath, "*.png"));

                foreach (var imagePath in imageFiles)
                {
                    var fileName = System.IO.Path.GetFileName(imagePath);
                    PhotoComboBox.Items.Add(fileName);
                }
            }
            else
            {
                MessageBox.Show("Папка Resources не найдена. Убедитесь, что папка существует в корне проекта.");
            }
        }

        private void SavePetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PetComboBox.SelectedItem == null || PhotoComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите питомца и фото.");
                    return;
                }

             
                var selectedPet = (PetComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
                var fileName = PhotoComboBox.SelectedItem?.ToString();

                if (selectedPet == null || fileName == null)
                {
                    MessageBox.Show("Не удалось получить выбранные данные.");
                    return;
                }

                var imagePathInResources = $"/Resources/{fileName}";

                var newPet = new Pets
                {
                    Names = selectedPet,
                    Opisanie = DescriptionTextBox.Text,
                    ImagePath = imagePathInResources
                };

                DatabaseConnectionClass.DatabaseConnection.Pets.Add(newPet);
                DatabaseConnectionClass.DatabaseConnection.SaveChanges();

                MessageBox.Show("Питомец добавлен!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                
                if (ex.InnerException != null)
                {
                    MessageBox.Show($"Ошибка при сохранении питомца: {ex.InnerException.Message}\n\nStackTrace: {ex.InnerException.StackTrace}");
                }
                else
                {
                    MessageBox.Show($"Ошибка при сохранении питомца: {ex.Message}\n\nStackTrace: {ex.StackTrace}");
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddDescriptionPlaceholder(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                DescriptionTextBox.Text = "Введите описание";
                DescriptionTextBox.Foreground = Brushes.Gray;
            }
        }

        private void RemoveDescriptionPlaceholder(object sender, RoutedEventArgs e)
        {
            if (DescriptionTextBox.Text == "Введите описание")
            {
                DescriptionTextBox.Text = string.Empty;
                DescriptionTextBox.Foreground = Brushes.Black;
            }
        }

    }
}
