using ManagamentCompanyDiplom.Classes;
using ManagamentCompanyDiplom.ModelBD;
using ManagamentCompanyDiplom.Pages.UserPages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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

namespace ManagamentCompanyDiplom.Pages
{
    public partial class UserProfilePage : Page
    {
        int ID;
        public UserProfilePage()
        {
            InitializeComponent();

            var users = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
            if (users != null)
            {
                txbName.Text = users.Firstname;
                txbUserProfileSurname.Text = users.Surname;
                txbUserProfileFirstname.Text = users.Firstname;
                ID = UserControlClass.IDUsers;
                txbUserProfilePatronymic.Text = users.Patronymic;
                txbUserProfilePhone.Text = Convert.ToString(users.Phone);
                txbUserProfileLogin.Text = users.Login;
                psbUserProfilePassword.Password = users.Password;
                var flat = AppData.db.Flat.FirstOrDefault(x => x.ID_Users == users.ID_Users);
                if (flat != null)
                {
                    txbUserMSFlat.Text = Convert.ToString(flat.NumberFlat);

                    var house = AppData.db.House.FirstOrDefault(h => h.ID_House == flat.ID_House);
                    if (house != null)
                    {
                        txbUserMSHouse.Text = Convert.ToString(house.NumberHouse);
                    }

                    var personalAccount = AppData.db.PersonalАccount.FirstOrDefault(pa => pa.ID_Flat == flat.ID_Flat);
                    if (personalAccount != null)
                    {
                        txbUserMSPersonalAccount.Text = Convert.ToString(personalAccount.Number_PersonalAccount);
                    }
                    else
                    {
                        txbUserMSPersonalAccount.Text = "Лицевой счёт не найден";
                    }
                }
                else
                {
                    txbUserMSFlat.Text = "Квартира не найдена";
                    txbUserMSHouse.Text = "Дом не найден";
                    txbUserMSPersonalAccount.Text = "Лицевой счёт не найден";
                }
            }
            else
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUserServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserServicesPage());
        }

        private void btnUserPaymentPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserPaymentPage());
        }

        private void btnUserChatPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserMyServicesPage());
        }

        private void btnUserProfileExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnUserProfileSaveChanged_Click(object sender, RoutedEventArgs e)
        {
            var editUsers = AppData.db.Users.
                FirstOrDefault(x => x.ID_Users == ID);
            try
            {
                if (txbUserProfileSurname.Text != "" &&
                    txbUserProfileFirstname.Text != "" &&
                    txbUserProfilePatronymic.Text != "" &&
                    txbUserProfilePhone.Text != "" &&
                    txbUserProfileLogin.Text != "" ||
                    txtPasswordBox.Text != "" ||
                    psbUserProfilePassword.Password != "")
                {

                    editUsers.Surname = txbUserProfileSurname.Text;
                    editUsers.Firstname = txbUserProfileFirstname.Text;
                    editUsers.Patronymic = txbUserProfilePatronymic.Text;
                    editUsers.Phone = txbUserProfilePhone.Text;
                    editUsers.Login = txbUserProfileLogin.Text;
                    editUsers.Password = psbUserProfilePassword.Password;
                    editUsers.Password = txtPasswordBox.Text;


                    AppData.db.SaveChanges();
                    MessageBox.Show("Информация успешно изменена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Пустые поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void psbUserProfilePassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPasswordBox.Visibility == Visibility.Visible)
            {
                txtPasswordBox.Text = psbUserProfilePassword.Password;
            }
        }

        private void txtPasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (psbUserProfilePassword.Visibility == Visibility.Visible)
            {
                psbUserProfilePassword.Password = txtPasswordBox.Text;
            }
        }

        private void imgClosedEye_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPasswordBox.Text = psbUserProfilePassword.Password;
            imgClosedEye.Visibility = Visibility.Collapsed;
            imgOpenEye.Visibility = Visibility.Visible;
            psbUserProfilePassword.Visibility = Visibility.Collapsed;
            txtPasswordBox.Visibility = Visibility.Visible;
        }

        private void imgOpenEye_MouseDown(object sender, MouseButtonEventArgs e)
        {
            psbUserProfilePassword.Password = txtPasswordBox.Text;
            imgOpenEye.Visibility = Visibility.Collapsed;
            imgClosedEye.Visibility = Visibility.Visible;
            txtPasswordBox.Visibility = Visibility.Collapsed;
            psbUserProfilePassword.Visibility = Visibility.Visible;
        }

        private void btnUserProfileSaveChangedImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUserMyPaymentPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserMyPaymentPage());
        }
    }
}
