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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            var user = DatabaseConnectionClass.DatabaseConnection.Users
                .FirstOrDefault(u => u.Username == username && u.Parol == password);

            if (user != null)
            {
                NavigationService.Navigate(new PagePets(user.Username));
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль");
            }
        }
    }
}
