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
    /// <summary>
    /// Логика взаимодействия для AdminApplicationServicesPage.xaml
    /// </summary>
    public partial class AdminApplicationServicesPage : Page
    {
        public AdminApplicationServicesPage()
        {
            InitializeComponent();
            var adminUser = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
            txbAdminName.Text = adminUser.Firstname;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAdminApplicationServicesProfilePage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminProfilePage());
        }

        private void btnAdminApplicationServicesApplicationPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminApplicationPage());
        }

        private void btnAdminApplicationServicesUsersPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminUsersPage());
        }

        private void btnAdminApplicationServicesCreateUsersPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminCreateUsersPage());
        }

        private void btnAdminApplicationServicesServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminServicesPage());
        }

        private void btnAdminApplicationServicesPaymentPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdminApplicationServicesExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

        private void txbAdminApplicationServicesSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbAdminApplicationServicesPageSearch.Focus();
        }

        private void txbAdminApplicationServicesPageSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbAdminApplicationServicesPageSearch.Text) && txbAdminApplicationServicesPageSearch.Text.Length > 0)
            {
                txbAdminApplicationServicesSearch.Visibility = Visibility.Collapsed;
            }
            else
            {
                txbAdminApplicationServicesSearch.Visibility = Visibility.Visible;
            }
            try
            {
                {
                    ListViewAdminApplicationServices.ItemsSource = AppData.db.ApplicationsForServices.Where(Item =>
                    Item.Users.Surname.Contains(txbAdminApplicationServicesPageSearch.Text) ||
                    Item.Services.NameServices.Contains(txbAdminApplicationServicesPageSearch.Text)).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Image_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            cmbAdminApplicationServicesFiltred.SelectedIndex = -1;
        }

        private void cmbAdminApplicationServicesFiltred_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFilter = cmbAdminApplicationServicesFiltred.SelectedItem as ComboBoxItem;
            if (selectedFilter != null)
            {
                var filteredApplications = AppData.db.ApplicationsForServices.ToList();


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

                ListViewAdminApplicationServices.ItemsSource = filteredApplications;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewAdminApplicationServices.ItemsSource = AppData.db.ApplicationsForServices.ToList();
        }

        private void btnAdminApplicationServicesYes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedApplications = ListViewAdminApplicationServices.SelectedItems.Cast<ApplicationsForServices>().ToList();
                if (selectedApplications.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите заявку, с которой хотите взаимодействовать!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                foreach (var application in selectedApplications)
                {
                    if (application.ID_StatusApplicationForServices == 2)
                    {
                        MessageBox.Show("Данная заявка уже имеет статус - 'Одобрено!'. " +
                            "Выберите заявку, которая находится на рассмотрении!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (System.Windows.MessageBox.Show($"Вы действительно хотите одобрить выбранную заявку?", "Внимание",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        application.DateOfApplicationChange = DateTime.Now;
                        application.ID_StatusApplicationForServices = 2;
                        AppData.db.SaveChanges();
                        MessageBox.Show("Заявка на услугу успешно одобрена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        ListViewAdminApplicationServices.ItemsSource = AppData.db.ApplicationsForServices.ToList();

                    }
                }
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Error", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
