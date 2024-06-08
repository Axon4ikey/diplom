using ManagamentCompanyDiplom.Classes;
using ManagamentCompanyDiplom.ModelBD;
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

namespace ManagamentCompanyDiplom.Pages.AdminPages
{
    public partial class AdminApplicationPage : Page
    {
        public AdminApplicationPage()
        {
            InitializeComponent();
            var adminUser = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
            txbAdminName.Text = adminUser.Firstname;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAdminProfilePage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminProfilePage());
        }

        private void btnAdminApplicationExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

        private void txbAdminApplicationSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbAdminApplicationPageSearch.Focus();
        }

        private void txbAdminApplicationPageSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbAdminApplicationPageSearch.Text) && txbAdminApplicationPageSearch.Text.Length > 0)
            {
                txbAdminApplicationSearch.Visibility = Visibility.Collapsed;
            }
            else
            {
                txbAdminApplicationSearch.Visibility = Visibility.Visible;
            }
            try
            {
                {
                    ListViewAdminApplication.ItemsSource = AppData.db.Flat.Where(Item => 
                    Item.Users.Surname.Contains(txbAdminApplicationPageSearch.Text) ||
                    Item.Users.Firstname.Contains(txbAdminApplicationPageSearch.Text) || 
                    Item.Users.Patronymic.Contains(txbAdminApplicationPageSearch.Text) || 
                    Item.Users.Phone.ToString().Contains(txbAdminApplicationPageSearch.Text)).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbFiltred_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterApplications();
        }

        private void FilterApplications()
        {
            try
            {
                var filteredList = from u in AppData.db.Users
                                   join f in AppData.db.Flat on u.ID_Users equals f.ID_Users
                                   join h in AppData.db.House on f.ID_House equals h.ID_House
                                   select new
                                   {
                                       Users = u,
                                       Flat = f,
                                       House = h
                                   };

                if (cmbFiltred.SelectedIndex == 0) 
                {
                    filteredList = filteredList.Where(x => x.Users.ID_StatusConfirmation == 1);
                }
                else if (cmbFiltred.SelectedIndex == 1) 
                {
                    filteredList = filteredList.Where(x => x.Users.ID_StatusConfirmation == 2);
                }

                ListViewAdminApplication.ItemsSource = filteredList.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var currentUser = AppData.db.Users.FirstOrDefault(u => u.ID_Users == UserControlClass.IDUsers);
            if (currentUser != null && !string.IsNullOrEmpty(currentUser.UsersImagePath))
            {
                ImageHelper.UpdateProfileImageInWindow(Window.GetWindow(this), currentUser.UsersImagePath);
            }
            LoadApplications();
        }

        private void LoadApplications()
        {
            var allApplications = from u in AppData.db.Users
                                  join f in AppData.db.Flat on u.ID_Users equals f.ID_Users
                                  join h in AppData.db.House on f.ID_House equals h.ID_House
                                  select new
                                  {
                                      Users = u,
                                      Flat = f,
                                      House = h
                                  };
            ListViewAdminApplication.ItemsSource = allApplications.ToList();
        }
        private void btnAdminApplicationCreateNewProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminCreateUsersPage());
        }

        private void btnAdminApplicationYes_Click(object sender, RoutedEventArgs e)
        {
            try
                {
                var selectedItems = ListViewAdminApplication.SelectedItems.Cast<dynamic>().ToList();
                if (selectedItems.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите заявку, с которой хотите взаимодействовать!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                bool isUpdated = false;
                foreach (var item in selectedItems)
                {
                    Users selectedUser = item.Users;
                    if (selectedUser.ID_StatusConfirmation == 1)
                    {
                        MessageBox.Show("Данная заявка уже имеет статус - 'Одобрено!'. Выберите заявку, которая находится на рассмотрении!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        continue;
                    }
                    if (System.Windows.MessageBox.Show($"Вы действительно хотите одобрить выбранную заявку?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        selectedUser.ID_StatusConfirmation = 1;
                        AppData.db.SaveChanges();

                        MessageBox.Show("Вы успешно одобрили заявку на создание учётной записи", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        isUpdated = true;
                    }
                }
                if (isUpdated)
                {
                    ListViewAdminApplication.ItemsSource = (from u in AppData.db.Users
                                                            join f in AppData.db.Flat on u.ID_Users equals f.ID_Users
                                                            join h in AppData.db.House on f.ID_House equals h.ID_House
                                                            select new
                                                            {
                                                                Users = u,
                                                                Flat = f,
                                                                House = h
                                                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnAdminApplicationCreateUsersPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminCreateUsersPage());
        }

        private void btnAdminApplicationUsersPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminUsersPage());
        }

        private void btnAdminApplicationServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminServicesPage());
        }

        private void Image_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            cmbFiltred.SelectedIndex = -1;
        }

        private void btnAdminApplicationApplicationServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminApplicationServicesPage());
        }
    }
}
