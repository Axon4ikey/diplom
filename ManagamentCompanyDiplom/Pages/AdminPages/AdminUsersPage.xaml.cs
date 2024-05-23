using ManagamentCompanyDiplom.Classes;
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
using ManagamentCompanyDiplom.ModelBD;
using System.Collections.ObjectModel;

namespace ManagamentCompanyDiplom.Pages.AdminPages
{
    public partial class AdminUsersPage : Page
    {
        private bool isSortedBySurname = false;
        public AdminUsersPage()
        {
            InitializeComponent();
            var adminUser = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
            txbAdminName.Text = adminUser.Firstname;
        }

        private void cmbAdminUsersSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filtred = AppData.db.Users.Where(l => l.ID_Roles == 2);
            var sort = filtred.ToList();

            switch (cmbAdminUsersSort.SelectedIndex)
            {
                case 0: { sort = filtred.OrderBy(x => x.Surname).ToList(); break; }
                case 1: { sort = filtred.OrderByDescending(x => x.Surname).ToList(); break; }
            }

            ListViewAdminUsers.ItemsSource = sort.ToList();
        }

        private void txbAdminUsersSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbAdminUsersPageSearch.Focus();
        }

        private void txbAdminUsersPageSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbAdminUsersPageSearch.Text) && txbAdminUsersPageSearch.Text.Length > 0)
            {
                txbAdminUsersSearch.Visibility = Visibility.Collapsed;

                var searchText = txbAdminUsersPageSearch.Text.ToLower();

                var users = AppData.db.Users.Where(u =>
                    u.ID_Roles == 2 && 
                    (
                        u.Surname.ToLower().Contains(searchText) ||
                        u.Firstname.ToLower().Contains(searchText) ||
                        u.Patronymic.ToLower().Contains(searchText) ||
                        u.Phone.ToString().ToLower().Contains(searchText) ||
                        u.Login.ToLower().Contains(searchText) ||
                        u.Password.ToLower().Contains(searchText)
                    )
                ).ToList();

                if (users.Count == 0)
                {
                    txbAdminUsersSearch.Text = "";
                    txbAdminUsersSearch.Visibility = Visibility.Visible;
                }
                else
                {
                    // ... (обработка результатов поиска)
                }

                if (users.Count > 0)
                {
                    ListViewAdminUsers.ItemsSource = users;
                }
                else
                {
                    ListViewAdminUsers.ItemsSource = null; 
                    txbAdminUsersSearch.Visibility = Visibility.Visible;
                }
            }
            else
            {
                var users = AppData.db.Users.Where(u => u.ID_Roles == 2).ToList();
                if (users.Count > 0)
                {
                    ListViewAdminUsers.ItemsSource = users;
                }
                else
                {
                    // ... (обработка результатов поиска)
                }
                txbAdminUsersSearch.Visibility = Visibility.Visible;
            }
        }

        private void btnAdminUsersProfilePage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminProfilePage());
        }

        private void btnAdminUsersApplicationPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminApplicationPage());
        }

        private void btnAdminUsersCreateUsersPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminCreateUsersPage());
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAdminUsersChatPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdminUsersExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var role = AppData.db.Users.Where(x => x.ID_Roles == 2).ToList();

            if (role.Count > 0)
            {
                ListViewAdminUsers.ItemsSource = role;
            }
        }

        private void btnAdminUsersServcesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminServicesPage());
        }

        private void btnAdminUsersDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var remove = ListViewAdminUsers.SelectedItems.Cast<Users>().ToList();
                if (remove.Count == 0)
                {
                    MessageBox.Show("Не выбрано ни одного пользователя", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (System.Windows.MessageBox.Show($"Вы действительно хотите удалить данного пользователя?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    // Проверка наличия связанных записей в таблице квартиры
                    foreach (var user in remove)
                    {
                        var relatedFlats = AppData.db.Flat.Where(f => f.ID_Users == user.ID_Users).ToList();

                        if (relatedFlats.Count > 0)
                        {
                            MessageBox.Show($"Невозможно удалить пользователя {user.Firstname}. За ним закреплена {relatedFlats.Count} квартира", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    AppData.db.Users.RemoveRange(remove);
                    AppData.db.SaveChanges();
                    MessageBox.Show("Пользователь успешно удалён!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    ListViewAdminUsers.ItemsSource = AppData.db.Services.ToList();
                }
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Ошибка, строка пустая!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Image_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            cmbAdminUsersSort.SelectedIndex = -1;
        }

        private void btnAdminUsersApplicationServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminApplicationServicesPage());
        }
    }
}
