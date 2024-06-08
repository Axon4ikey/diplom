
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using ManagamentCompanyDiplom.Classes;
using ManagamentCompanyDiplom.Pages;
using ManagamentCompanyDiplom.Pages.AdminPages;

namespace ManagamentCompanyDiplom.Pages
{
    public partial class Autorization : Page
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void txtLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbLogin.Focus();
        }

        private void txbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbLogin.Text) && txbLogin.Text.Length > 0)
            {
                txtLogin.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtLogin.Visibility = Visibility.Visible;   
            }
        }

        private void txtPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbPassword.Focus();
        }

        private void txbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPasswordBox.Visibility == Visibility.Visible)
            {
                txtPasswordBox.Text = txbPassword.Password;
            }
            else if (!string.IsNullOrEmpty(txbPassword.Password) && txbPassword.Password.Length > 0)
            {
                txtPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtPassword.Visibility = Visibility.Visible;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.ChangedButton == MouseButton.Left)
            //{
            //    this.DragMove();
            //}
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnSignUPLeft_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ApplicationForm());
        }

        private void btnAutorization_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var UsersObj = AppData.db.Users.FirstOrDefault(
                    x => x.Login == txbLogin.Text && (x.Password == txbPassword.Password ||
                    x.Password == txtPasswordBox.Text));
                string login = txbLogin.Text;
                string pass = txbPassword.Password;
                if (txbLogin.Text == "" || txbPassword.Password == "")
                {
                    MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (UsersObj == null)
                {
                    MessageBox.Show("Такого человека не существует в базе данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    UserControlClass.IDUsers = UsersObj.ID_Users;
                    AppData.db.SaveChanges();

                    switch (UsersObj.ID_Roles)
                    {
                        case 1:
                            NavigationService.Navigate(new AdminProfilePage());
                            MessageBox.Show("Здравствуйте, Администратор - " + UsersObj.Firstname + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        case 2:
                            NavigationService.Navigate(new UserProfilePage());
                            MessageBox.Show("Здравствуйте, Пользователь - " + UsersObj.Firstname + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критический сбор в работе приложения" + ex.Message.ToString(),
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void imgOpenEye_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbPassword.Password = txtPasswordBox.Text;
            imgOpenEye.Visibility = Visibility.Collapsed;
            imgClosedEye.Visibility = Visibility.Visible;
            txtPasswordBox.Visibility = Visibility.Collapsed;
            txbPassword.Visibility = Visibility.Visible;
        }

        private void imgClosedEye_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPasswordBox.Text = txbPassword.Password;
            imgClosedEye.Visibility = Visibility.Collapsed;
            imgOpenEye.Visibility = Visibility.Visible;
            txbPassword.Visibility = Visibility.Collapsed;
            txtPasswordBox.Visibility = Visibility.Visible;
        }

        private void txtPasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbPassword.Visibility == Visibility.Visible)
            {
                txbPassword.Password = txtPasswordBox.Text;
            }
        }
    }
}
