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

namespace ManagamentCompanyDiplom.Pages.UserPages
{
    public partial class UserMyServicesPage : Page
    {
        public UserMyServicesPage()
        {
            InitializeComponent();
            var users = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
            txbName.Text = users.Firstname;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnUserProfileMSPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserProfilePage());
        }

        private void btnUserServicesMSPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserServicesPage());
        }

        private void btnUserPaymentMSPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserPaymentPage());
        }

        private void btnUserMyServicesExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

        private void txbUserMyServicesPageSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbUserMyServicesPageSearch.Text) && txbUserMyServicesPageSearch.Text.Length > 0)
            {
                txbSearch.Visibility = Visibility.Collapsed;
            }
            else
            {
                txbSearch.Visibility = Visibility.Visible;
            }
            try
            {
                {
                    ListViewMyServices.ItemsSource = AppData.db.ApplicationsForServices.Where(Item => 
                    (Item.Services.NameServices.Contains(txbUserMyServicesPageSearch.Text) ||
                    Item.Services.Rate.ToString().Contains(txbUserMyServicesPageSearch.Text)) &&
                    Item.ID_Users == UserControlClass.IDUsers).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Image_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            cmbUserMyServicesFiltred.SelectedIndex = -1;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewMyServices.ItemsSource = AppData.db.ApplicationsForServices.
                Where(x => x.ID_Users == UserControlClass.IDUsers).ToList();
        }

        private void txbSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbUserMyServicesPageSearch.Focus();
        }

        private void cmbUserMyServicesFiltred_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFilter = cmbUserMyServicesFiltred.SelectedItem as ComboBoxItem;
            if (selectedFilter != null)
            {
                var filteredApplications = AppData.db.ApplicationsForServices.
                    Where(x => x.ID_Users == UserControlClass.IDUsers).ToList();


                if (selectedFilter.Content.ToString().Equals("Одобренные"))
                {
                    filteredApplications = filteredApplications.Where(x => x.ID_StatusApplicationForServices == 2).ToList();
                }
                else if (selectedFilter.Content.ToString().Equals("Все"))
                {
                    //все выводит
                }
                else if (selectedFilter.Content.ToString().Equals("В обработке"))
                {
                    filteredApplications = filteredApplications.Where(x => x.ID_StatusApplicationForServices == 1).ToList();
                }

                ListViewMyServices.ItemsSource = filteredApplications;
            }
        }

        private void btnUserMyPaymentPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserMyPaymentPage());
        }
    }
}
