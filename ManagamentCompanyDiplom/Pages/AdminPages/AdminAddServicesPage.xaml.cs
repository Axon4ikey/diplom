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
    public partial class AdminAddServicesPage : Page
    {
        public AdminAddServicesPage()
        {
            InitializeComponent();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void txtAdminServicesName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbAdminServicesName.Focus();
        }

        private void btnAdminServicesAddSer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var nameser = AppData.db.Services.FirstOrDefault(g => g.NameServices == txbAdminServicesName.Text);
                Services servicesadd = new Services();
                servicesadd.NameServices = txbAdminServicesName.Text;
                servicesadd.Rate = Convert.ToDecimal(txbAdminServicesRate.Text);
                
                if (txbAdminServicesName.Text == "" &&
                    txbAdminServicesRate.Text == "")
                {
                    MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (nameser != null)
                {
                    MessageBox.Show("Такая услуга уже зарегистрирована в системе!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    AppData.db.Services.Add(servicesadd);
                    AppData.db.SaveChanges();
                    MessageBox.Show("Услуга успешно добавлена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txbAdminServicesName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbAdminServicesName.Text) && txbAdminServicesName.Text.Length > 0)
            {
                txtAdminServicesName.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtAdminServicesName.Visibility = Visibility.Visible;
            }
        }

        private void txbAdminServicesRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbAdminServicesRate.Text) && txbAdminServicesRate.Text.Length > 0)
            {
                txtAdminServicesRate.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtAdminServicesRate.Visibility = Visibility.Visible;
            }
        }

        private void txtAdminServicesRate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbAdminServicesRate.Focus();
        }

        private void Image_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AdminServicesPage());
        }
    }
}
