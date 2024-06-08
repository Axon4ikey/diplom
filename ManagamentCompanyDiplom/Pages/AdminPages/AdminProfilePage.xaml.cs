using ManagamentCompanyDiplom.Classes;
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

namespace ManagamentCompanyDiplom.Pages.AdminPages
{
    public partial class AdminProfilePage : Page
    {
        int ID;
        public AdminProfilePage()
        {
            InitializeComponent();

            var adminUser = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
            txbAdminName.Text = adminUser.Firstname;
            txbAdminProfileSurname.Text = adminUser.Surname;
            txbAdminProfileFirstname.Text = adminUser.Firstname;
            ID = UserControlClass.IDUsers;
            txbAdminProfilePatronymic.Text = adminUser.Patronymic;
            txbAdminProfilePhone.Text = Convert.ToString(adminUser.Phone);
            txbAdminProfileLogin.Text = adminUser.Login;
            psbAdminProfilePassword.Password = adminUser.Password;

        }

        private void btnAdminProfileExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

        private void btnAdminProfileSaveChangedImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                // Сохраняем изображение в папку приложения и обновляем в базе данных
                string imagesFolderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProfileImages");
                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                string destFilePath = System.IO.Path.Combine(imagesFolderPath, System.IO.Path.GetFileName(selectedFilePath));
                File.Copy(selectedFilePath, destFilePath, true);

                // Обновляем изображение в базе данных
                var currentUser = AppData.db.Users.FirstOrDefault(u => u.ID_Users == UserControlClass.IDUsers);
                if (currentUser != null)
                {
                    currentUser.UsersImagePath = destFilePath;
                    AppData.db.SaveChanges();
                }

                // Обновляем изображение на всех формах
                ImageHelper.UpdateProfileImageOnAllForms(destFilePath);

                // Обновляем изображение в текущем профиле
                ProfileImage.Source = new BitmapImage(new Uri(destFilePath));
                ProfileImageMenu.Source = new BitmapImage(new Uri(destFilePath));
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAdminProfileSaveChanged_Click(object sender, RoutedEventArgs e)
        {
            var editAdminUsers = AppData.db.Users.FirstOrDefault(x => x.ID_Users == ID);
            try
            {
                if (txbAdminProfileSurname.Text != "" &&
                    txbAdminProfileFirstname.Text != "" &&
                    txbAdminProfilePatronymic.Text != "" &&
                    txbAdminProfilePhone.Text != "" ||
                    txtPasswordBox.Text != "" ||
                    txbAdminProfileLogin.Text != "" &&
                    psbAdminProfilePassword.Password != "")
                {

                    editAdminUsers.Surname = txbAdminProfileSurname.Text;
                    editAdminUsers.Firstname = txbAdminProfileFirstname.Text;
                    editAdminUsers.Patronymic = txbAdminProfilePatronymic.Text;
                    editAdminUsers.Phone = txbAdminProfilePhone.Text;
                    editAdminUsers.Login = txbAdminProfileLogin.Text;
                    editAdminUsers.Password = psbAdminProfilePassword.Password;
                    editAdminUsers.Password = txtPasswordBox.Text;


                    AppData.db.SaveChanges();
                    MessageBox.Show("Информация успешно изменена!", "Уведомление", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void btnAdminApplicationPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminApplicationPage());
        }

        private void btnAdminUsersPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminUsersPage());
        }

        private void btnAdminCreateUsersPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminCreateUsersPage());
        }

        private void btnAdminServcesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminServicesPage());
        }

        private void btnAdminApplicationServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminApplicationServicesPage());
        }

        private void psbAdminProfilePassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPasswordBox.Visibility == Visibility.Visible)
            {
                txtPasswordBox.Text = psbAdminProfilePassword.Password;
            }
        }

        private void txtPasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (psbAdminProfilePassword.Visibility == Visibility.Visible)
            {
                psbAdminProfilePassword.Password = txtPasswordBox.Text;
            }
        }

        private void imgOpenEye_MouseDown(object sender, MouseButtonEventArgs e)
        {
            psbAdminProfilePassword.Password = txtPasswordBox.Text;
            imgOpenEye.Visibility = Visibility.Collapsed;
            imgClosedEye.Visibility = Visibility.Visible;
            txtPasswordBox.Visibility = Visibility.Collapsed;
            psbAdminProfilePassword.Visibility = Visibility.Visible;
        }

        private void imgClosedEye_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPasswordBox.Text = psbAdminProfilePassword.Password;
            imgClosedEye.Visibility = Visibility.Collapsed;
            imgOpenEye.Visibility = Visibility.Visible;
            psbAdminProfilePassword.Visibility = Visibility.Collapsed;
            txtPasswordBox.Visibility = Visibility.Visible;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var user = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
            if (user != null && !string.IsNullOrEmpty(user.UsersImagePath))
            {
                ProfileImage.Source = new BitmapImage(new Uri(user.UsersImagePath));
                ImageHelper.UpdateProfileImageOnAllForms(user.UsersImagePath);
            }
        }
    }
}
