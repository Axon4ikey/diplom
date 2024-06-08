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
using ManagamentCompanyDiplom.Pages;
using ManagamentCompanyDiplom.Pages.UserPages;

namespace ManagamentCompanyDiplom.Pages
{
    public partial class UserServicesPage : Page
    {
        public UserServicesPage()
        {
            InitializeComponent();
            var users = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
            txbName.Text = users.Firstname;
        }

        private void btnUserProfileServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserProfilePage());
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnUserServicesServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserServicesPage());
        }

        private void btnUserPaymentServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserPaymentPage());
        }

        private void btnUserServicesExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewServices.ItemsSource = AppData.db.Services.ToList();
        }

        private void btnUserProfileServicesApplication_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int serviceId = (int)((Button)sender).Tag;
                string serviceName = GetServiceName(serviceId);
                int userId = UserControlClass.IDUsers;

                ApplicationsForServices order = new ApplicationsForServices();

                bool orderExists = AppData.db.ApplicationsForServices
                    .Any(a => a.ID_Users == userId && a.ID_Services == serviceId);

                bool orderExistsTwo = AppData.db.ApplicationsForServices
                    .Any(b => b.ID_Users == userId && b.ID_Services == serviceId && b.ID_StatusApplicationForServices == 1);

                if (orderExists && System.Windows.MessageBox.Show($"Вы действительно хотите оформить заказ на услугу - " + serviceName + "?", "Внимание",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (orderExistsTwo)
                    {
                        MessageBox.Show("Вы уже оформили заказ на данную услугу!" +
                            "Заявка находится в обработке!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        ApplicationsForServices serv = new ApplicationsForServices();
                        serv.ID_StatusApplicationForServices = 1;
                        serv.DateOfApplication = DateTime.Now;
                        serv.ID_Users = userId;
                        serv.ID_Services = serviceId;

                        AppData.db.ApplicationsForServices.Add(serv);
                        AppData.db.SaveChanges();
                        ListViewServices.Items.Refresh();

                        MessageBox.Show("Вы успешно оформили заказ на услугу - " + serviceName + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else if (!orderExists && System.Windows.MessageBox.Show($"Вы действительно хотите оформить заказ на услугу - " + serviceName + "?", "Внимание",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ApplicationsForServices serv = new ApplicationsForServices();
                    serv.ID_StatusApplicationForServices = 1;
                    serv.DateOfApplication = DateTime.Now;
                    serv.ID_Users = userId;
                    serv.ID_Services = serviceId;

                    AppData.db.ApplicationsForServices.Add(serv);
                    AppData.db.SaveChanges();
                    ListViewServices.Items.Refresh();

                    MessageBox.Show("Вы успешно оформили заказ на услугу - " + serviceName + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Вы уже оформили заказ на данную услугу!" +
                        "Заявка находится в обработке!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критический сбор в работе приложения" + ex.Message.ToString(),
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private string GetServiceName(int serviceId)
        {
            string serviceName = AppData.db.Services.Where(s => s.ID_Services == serviceId).Select(s => s.NameServices).FirstOrDefault();

            if (serviceName == null)
            {
                serviceName = "Неизвестная услуга";
            }

            return serviceName;
        }

        private void txbSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbUserServicesPageSearch.Focus();
        }

        private void txbUserServicesPageSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbUserServicesPageSearch.Text) && txbUserServicesPageSearch.Text.Length > 0)
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
                    ListViewServices.ItemsSource = AppData.db.Services.Where(Item => Item.NameServices.Contains(txbUserServicesPageSearch.Text) ||
                    Item.Rate.ToString().Contains(txbUserServicesPageSearch.Text)).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbSortirovka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListViewServices.ItemsSource = AppData.db.Services.ToList();

            var filtred = AppData.db.Services.ToList();
            filtred = filtred.Where(x => x.Rate.ToString().Contains(txbUserServicesPageSearch.Text)).ToList();

            var sort = filtred.ToList();


            switch (cmbSortirovka.SelectedIndex)
            {
                case 0: { sort = filtred.OrderByDescending(x => x.Rate).ToList(); break; }
                case 1: { sort = filtred.OrderBy(x => x.Rate).ToList(); break; }
            }
            ListViewServices.ItemsSource = sort.ToList();
        }

        private void Image_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            cmbSortirovka.SelectedIndex = -1;
        }

        private void btnUserMyServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserMyServicesPage());
        }

        private void btnSeeMyServicesUser_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserMyServicesPage());
        }

        private void btnUserMyPaymentPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserMyPaymentPage());
        }
    }
}
