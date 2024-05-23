using ManagamentCompanyDiplom.Classes;
using ManagamentCompanyDiplom.ModelBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Xml.Linq;

namespace ManagamentCompanyDiplom.Pages.AdminPages
{
    public partial class AdminCreateUsersPage : Page
    {
        public AdminCreateUsersPage()
        {
            InitializeComponent();
            var adminUser = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
            txbAdminName.Text = adminUser.Firstname;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAdminCreateUsersProfilePage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminProfilePage());
        }

        private void btnAdminCreateUsersApplicationPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminApplicationPage());
        }

        private void btnAdminCreateUsersUsersPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminUsersPage());
        }

        private void btnAdminCreateUsersChatPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdminCreateUsersExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

        private void btnAdminCreateUsersSaveChanged_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var existingUser = AppData.db.Users.FirstOrDefault(f => f.Login == txbAdminCreateUsersLogin.Text);

                var userToUpdate = AppData.db.Users.FirstOrDefault(d => d.Surname == txbAdminUsersCreateSurname.Text &&
                                                                   d.Firstname == txbAdminUsersCreateFirstname.Text &&
                                                                   d.Patronymic == txbAdminCreateUsersPatronymic.Text);

                if (txbAdminUsersCreateSurname.Text == "" ||
                    txbAdminUsersCreateFirstname.Text == "" ||
                    txbAdminCreateUsersPatronymic.Text == "" ||
                    txbAdminCreateUsersLogin.Text == "" ||
                    txtPasswordBox.Text == "" ||
                    psbAdminCreateUsersPassword.Password == "")
                {
                    MessageBox.Show("Заполните все поля", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (userToUpdate != null && userToUpdate.ID_StatusConfirmation == 2)
                {
                    MessageBox.Show("Ошибка! Заявка пользователя находится на рассмотрении!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (existingUser != null)
                {
                    MessageBox.Show("Такой логин уже существует, придумайте новый!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (userToUpdate != null && userToUpdate.Login != null && userToUpdate.Password != null)
                {
                    MessageBox.Show("Данный пользователь уже имеет логин и пароль! " +
                        "Невозможно внести изменения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (userToUpdate != null && userToUpdate.Login == null && userToUpdate.Password == null)
                    {
                        userToUpdate.Login = txbAdminCreateUsersLogin.Text;
                        userToUpdate.Password = psbAdminCreateUsersPassword.Password;
                        userToUpdate.Password = txtPasswordBox.Text;
                        AppData.db.SaveChanges();
                        MessageBox.Show("Логин и пароль успешно выданы пользователю - " + userToUpdate.Firstname, "Уведомление",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtAdminUsersCreateSurname_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbAdminUsersCreateSurname.Focus();
        }

        private void txbAdminUsersCreateSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbAdminUsersCreateSurname.Text) && txbAdminUsersCreateSurname.Text.Length > 0)
            {
                txtAdminUsersCreateSurname.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtAdminUsersCreateSurname.Visibility = Visibility.Visible;
            }
        }

        private void txtAdminUsersCreateFirstname_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbAdminUsersCreateFirstname.Focus();
        }

        private void txbAdminUsersCreateFirstname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbAdminUsersCreateFirstname.Text) && txbAdminUsersCreateFirstname.Text.Length > 0)
            {
                txtAdminUsersCreateFirstname.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtAdminUsersCreateFirstname.Visibility = Visibility.Visible;
            }
        }

        private void txtAdminCreateUsersPatronymic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbAdminCreateUsersPatronymic.Focus();
        }

        private void txbAdminCreateUsersPatronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbAdminCreateUsersPatronymic.Text) && txbAdminCreateUsersPatronymic.Text.Length > 0)
            {
                txtAdminCreateUsersPatronymic.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtAdminCreateUsersPatronymic.Visibility = Visibility.Visible;
            }
        }

        private void txtAdminCreateUsersLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbAdminCreateUsersLogin.Focus();
        }

        private void txbAdminCreateUsersLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbAdminCreateUsersLogin.Text) && txbAdminCreateUsersLogin.Text.Length > 0)
            {
                txtAdminCreateUsersLogin.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtAdminCreateUsersLogin.Visibility = Visibility.Visible;
            }
        }

        private void psbAdminCreateUsersPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPasswordBox.Visibility == Visibility.Visible)
            {
                txtPasswordBox.Text = psbAdminCreateUsersPassword.Password;
            }
            else if (!string.IsNullOrEmpty(psbAdminCreateUsersPassword.Password) && psbAdminCreateUsersPassword.Password.Length > 0)
            {
                txtAdminCreateUsersPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtAdminCreateUsersPassword.Visibility = Visibility.Visible;
            }
        }

        private void txtAdminCreateUsersPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            psbAdminCreateUsersPassword.Focus();
        }

        private void btnAdminCreateUsersServcesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminServicesPage());
        }

        private void btnAdminCreateUsersApplicationServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminApplicationServicesPage());
        }

        private void imgOpenEye_MouseDown(object sender, MouseButtonEventArgs e)
        {
            psbAdminCreateUsersPassword.Password = txtPasswordBox.Text;
            imgOpenEye.Visibility = Visibility.Collapsed;
            imgClosedEye.Visibility = Visibility.Visible;
            txtPasswordBox.Visibility = Visibility.Collapsed;
            psbAdminCreateUsersPassword.Visibility = Visibility.Visible;
        }

        private void imgClosedEye_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPasswordBox.Text = psbAdminCreateUsersPassword.Password;
            imgClosedEye.Visibility = Visibility.Collapsed;
            imgOpenEye.Visibility = Visibility.Visible;
            psbAdminCreateUsersPassword.Visibility = Visibility.Collapsed;
            txtPasswordBox.Visibility = Visibility.Visible;
        }

        private void txtPasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (psbAdminCreateUsersPassword.Visibility == Visibility.Visible)
            {
                psbAdminCreateUsersPassword.Password = txtPasswordBox.Text;
            }
        }
        private string GenerateRandomPassword(int minLength, int maxLength)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder result = new StringBuilder();
            Random random = new Random();
            int length = random.Next(minLength, maxLength + 1);

            for (int i = 0; i < length; i++)
            {
                result.Append(validChars[random.Next(validChars.Length)]);
            }

            return result.ToString();
        }

        private void btnGeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            string generatedPassword = GenerateRandomPassword(1,12); 
            psbAdminCreateUsersPassword.Password = generatedPassword;
            txtPasswordBox.Text = generatedPassword;
        }
    }
}
