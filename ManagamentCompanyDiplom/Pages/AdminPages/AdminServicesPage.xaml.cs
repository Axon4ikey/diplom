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
    public partial class AdminServicesPage : Page
    {
        public AdminServicesPage()
        {
            InitializeComponent();
            var adminUser = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
            txbAdminName.Text = adminUser.Firstname;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAdminServicesProfilePage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminProfilePage());
        }

        private void btnAdminServicesApplicationPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminApplicationPage());
        }

        private void btnAdminServicesUsersPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminUsersPage());
        }

        private void btnAdminServicesCreateUsersPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminCreateUsersPage());
        }

        private void btnAdminServicesChatPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdminServicesnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

        private void txbAdminServicesSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbAdminServicesPageSearch.Focus();
        }

        private void txbAdminServicesPageSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbAdminServicesPageSearch.Text) && txbAdminServicesPageSearch.Text.Length > 0)
            {
                txbAdminServicesSearch.Visibility = Visibility.Collapsed;
            }
            else
            {
                txbAdminServicesSearch.Visibility = Visibility.Visible;
            }
            try
            {
                {
                    ListViewAdminServices.ItemsSource = AppData.db.Services.Where(Item => Item.NameServices.Contains(txbAdminServicesPageSearch.Text) ||
                    Item.Rate.ToString().Contains(txbAdminServicesPageSearch.Text)).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbAdminServicesSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewAdminServices.ItemsSource = AppData.db.Services.ToList();

            var filtred = AppData.db.Services.ToList();
            filtred = filtred.Where(x => x.Rate.ToString().Contains(txbAdminServicesPageSearch.Text)).ToList();

            var sort = filtred.ToList();


            switch (cmbAdminServicesSort.SelectedIndex)
            {
                case 0: { sort = filtred.OrderByDescending(x => x.Rate).ToList(); break; }
                case 1: { sort = filtred.OrderBy(x => x.Rate).ToList(); break; }
            }
            ListViewAdminServices.ItemsSource = sort.ToList();
        }

        private void btnAdminServicesAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminAddServicesPage());
        }

        private void btnAdminServicesDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var remove = ListViewAdminServices.SelectedItems.Cast<Services>().ToList();
                if (remove.Count == 0)
                {
                    MessageBox.Show("Не выбрано ни одной услуги для удаления", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (System.Windows.MessageBox.Show($"Вы действительно хотите удалить выбранную услугу?", "Уведомление",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AppData.db.Services.RemoveRange(remove);
                    AppData.db.SaveChanges();
                    MessageBox.Show("Услуга успешно удалена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    ListViewAdminServices.ItemsSource = AppData.db.Services.ToList();
                }
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Ошибка, строка пустая!",  "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewAdminServices.ItemsSource = AppData.db.Services.ToList();
        }

        private void Image_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            cmbAdminServicesSort.SelectedIndex = -1;
        }

        private void btnAdminServicesApplicationServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminApplicationServicesPage());
        }
    }
}
