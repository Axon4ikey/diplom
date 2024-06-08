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
    public partial class UserMyPaymentPage : Page
    {
        public UserMyPaymentPage()
        {
            InitializeComponent();
            var users = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
            txbName.Text = users.Firstname;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnUserMPPaymentsPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserProfilePage());
        }

        private void btnUserMPServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserServicesPage());
        }

        private void btnUserMPPaymentPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserPaymentPage());
        }

        private void btnUserMPMyServicesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserMyServicesPage());
        }

        private void btnUserMyPaymentExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var currentUser = AppData.db.Users.FirstOrDefault(u => u.ID_Users == UserControlClass.IDUsers);
            if (currentUser != null && !string.IsNullOrEmpty(currentUser.UsersImagePath))
            {
                ImageHelper.UpdateProfileImageInWindow(Window.GetWindow(this), currentUser.UsersImagePath);
            }
            var users = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
            var flat = AppData.db.Flat.FirstOrDefault(x => x.ID_Users == users.ID_Users);
            var personalAccount = AppData.db.PersonalАccount.FirstOrDefault(pa => pa.ID_Flat == flat.ID_Flat);

            ListViewMyPayment.ItemsSource = AppData.db.Invoice
                .Where(x => x.ID_PersonalAccount == personalAccount.ID_PersonalAccount)
                .Select(x => new
                {
                    x.ID_Invoice,
                    x.TotalAmount,
                    x.InvoiceDate,
                    PersonalAccount = x.PersonalАccount,
                    x.InvoicePath
                })
                .ToList();
        }

        private void txbSearchMP_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbUserMyPaymentPageSearch.Focus();
        }

        private void txbUserMyPaymentPageSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbUserMyPaymentPageSearch.Text) && txbUserMyPaymentPageSearch.Text.Length > 0)
            {
                txbSearchMP.Visibility = Visibility.Collapsed;
            }
            else
            {
                txbSearchMP.Visibility = Visibility.Visible;
            }
            try
            {
                    var users = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
                    if (users == null)
                    {
                        MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var flat = AppData.db.Flat.FirstOrDefault(x => x.ID_Users == users.ID_Users);
                    if (flat == null)
                    {
                        MessageBox.Show("Квартира не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var personalAccount = AppData.db.PersonalАccount.FirstOrDefault(pa => pa.ID_Flat == flat.ID_Flat);
                    if (personalAccount == null)
                    {
                        MessageBox.Show("Лицевой счет не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    string searchText = txbUserMyPaymentPageSearch.Text;
                    var filteredInvoices = AppData.db.Invoice
                        .Where(x => x.ID_PersonalAccount == personalAccount.ID_PersonalAccount &&
                                    (x.TotalAmount.ToString().Contains(searchText) ||
                                     x.InvoiceDate.ToString().Contains(searchText) ||
                                     personalAccount.Number_PersonalAccount.Contains(searchText)))
                        .Select(x => new
                        {
                            Number_PersonalAccount = personalAccount.Number_PersonalAccount,
                            x.TotalAmount,
                            x.InvoiceDate
                        })
                        .ToList();
                    ListViewMyPayment.ItemsSource = filteredInvoices;
                }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnUserMyPayment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button)
                {
                    string invoicePath = button.Tag as string;

                    if (string.IsNullOrEmpty(invoicePath))
                    {
                        MessageBox.Show("По данному запросу нет квитанции", "Нет квитанции", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    if (!System.IO.File.Exists(invoicePath))
                    {
                        MessageBox.Show("Файл квитанции не найден", "Файл не найден", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(invoicePath) { UseShellExecute = true });
                }
                else
                {
                    MessageBox.Show("Отправитель не является кнопкой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии квитанции: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
