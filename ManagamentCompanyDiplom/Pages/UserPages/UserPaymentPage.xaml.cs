using ManagamentCompanyDiplom.Classes;
using ManagamentCompanyDiplom.ModelBD;
using ManagamentCompanyDiplom.Pages.UserPages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Linq;

namespace ManagamentCompanyDiplom.Pages
{
    public partial class UserPaymentPage : Page
    {
        private const decimal ColdWaterRate = 20.5m;
        private const decimal HotWaterRate = 35.0m;  
        private const decimal HeatingRate = 50.0m;  
        private const decimal ElectricityRate = 5.0m;

        private decimal? lastColdWaterReading;
        private decimal? lastHotWaterReading;
        private decimal? lastHeatingReading;
        private decimal? lastElectricityReading;

        public UserPaymentPage()
        {
            InitializeComponent();
            var users = AppData.db.Users.FirstOrDefault(x => x.ID_Users == UserControlClass.IDUsers);
            txbName.Text = users.Firstname;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnUserPaymentsPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserProfilePage());
        }

        private void btnUserPaymenPaymentPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserPaymentPage());
        }

        private void btnUserServicesPaymentPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserServicesPage());
        }

        private void btnUserChatPaymentPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserMyServicesPage());
        }

        private void btnUserPaymentExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

        private void txtUserMetersHotWater_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbUserMetersHotWater.Focus();
        }

        private void txbUserMetersHotWater_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbUserMetersHotWater.Text) && txbUserMetersHotWater.Text.Length > 0)
            {
                txtUserMetersHotWater.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtUserMetersHotWater.Visibility = Visibility.Visible;
            }
            if (decimal.TryParse(txbUserMetersHotWater.Text, out decimal coldWaterReading))
            {
                lblHotWaterAmount.Text = (coldWaterReading * ColdWaterRate).ToString("C");
            }
            else
            {
                lblHotWaterAmount.Text = string.Empty;
            }
        }

        private void txtUserMetersColdWater_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbUserMetersColdWater.Focus();
        }

        private void txbUserMetersColdWater_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbUserMetersColdWater.Text) && txbUserMetersColdWater.Text.Length > 0)
            {
                txtUserMetersColdWater.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtUserMetersColdWater.Visibility = Visibility.Visible;
            }
            if (decimal.TryParse(txbUserMetersColdWater.Text, out decimal hotWaterReading))
            {
                lblColdWaterAmount.Text = (hotWaterReading * HotWaterRate).ToString("C");
            }
            else
            {
                lblColdWaterAmount.Text = string.Empty;
            }
        }

        private void txtUserMetersElectry_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbUserMetersElectry.Focus();
        }

        private void txbUserMetersElectry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbUserMetersElectry.Text) && txbUserMetersElectry.Text.Length > 0)
            {
                txtUserMetersElectry.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtUserMetersElectry.Visibility = Visibility.Visible;
            }
            if (decimal.TryParse(txbUserMetersElectry.Text, out decimal heatingReading))
            {
                lblElectryAmount.Text = (heatingReading * HeatingRate).ToString("C");
            }
            else
            {
                lblElectryAmount.Text = string.Empty;
            }
        }

        private void txbUserMetersHeating_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbUserMetersHeating.Text) && txbUserMetersHeating.Text.Length > 0)
            {
                txtUserMetersHeating.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtUserMetersHeating.Visibility = Visibility.Visible;
            }
            if (decimal.TryParse(txbUserMetersHeating.Text, out decimal electricityReading))
            {
                lblHeatingAmount.Text = (electricityReading * ElectricityRate).ToString("C");
            }
            else
            {
                lblHeatingAmount.Text = string.Empty;
            }
        }
        private void btnUsersImvoicePage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal coldWaterReading = decimal.Parse(txbUserMetersColdWater.Text);
                decimal hotWaterReading = decimal.Parse(txbUserMetersHotWater.Text);
                decimal heatingReading = decimal.Parse(txbUserMetersHeating.Text);
                decimal electricityReading = decimal.Parse(txbUserMetersElectry.Text);

                if (lastColdWaterReading == coldWaterReading &&
                    lastHotWaterReading == hotWaterReading &&
                    lastHeatingReading == heatingReading &&
                    lastElectricityReading == electricityReading)
                {
                    MessageBox.Show("Квитанция уже отправлена в PDF файл", "Повторная отправка", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                decimal coldWaterAmount = decimal.Parse(lblColdWaterAmount.Text, System.Globalization.NumberStyles.Currency);
                decimal hotWaterAmount = decimal.Parse(lblHotWaterAmount.Text, System.Globalization.NumberStyles.Currency);
                decimal heatingAmount = decimal.Parse(lblHeatingAmount.Text, System.Globalization.NumberStyles.Currency);
                decimal electryAmount = decimal.Parse(lblElectryAmount.Text, System.Globalization.NumberStyles.Currency);

                decimal totalAmount = coldWaterAmount + hotWaterAmount + heatingAmount + electryAmount;
                txtTotalAmount.Text = $"Итоговая сумма за коммунальные услуги = {totalAmount} руб.";

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

                var metersData = new MetersData
                {
                    ElectricityReading = decimal.Parse(txbUserMetersElectry.Text),
                    ColdWaterReading = decimal.Parse(txbUserMetersColdWater.Text),
                    HotWaterReading = decimal.Parse(txbUserMetersHotWater.Text),
                    HeatingReading = decimal.Parse(txbUserMetersHeating.Text),
                    DateOfTestimony = DateTime.Now,
                    ID_PersonalAccount = personalAccount.ID_PersonalAccount
                };
                AppData.db.MetersData.Add(metersData);

                var accruals = new Accruals
                {
                    PeriodAccruals = DateTime.Now,
                    Arrears = 0,
                    Sum = 0
                };
                AppData.db.Accruals.Add(accruals);
                AppData.db.SaveChanges();

                if (personalAccount != null)
                {
                    personalAccount.ID_Accruals = accruals.ID_Accruals;
                    personalAccount.ID_TypeOfPersonalAccount = new Random().Next(1, 4);
                    AppData.db.SaveChanges();
                }

                var payment = new Payments
                {
                    Sum = totalAmount,
                    DataPayments = DateTime.Now,
                    ID_Accruals = accruals.ID_Accruals
                };
                AppData.db.Payments.Add(payment);

                // Создание квитанции PDF
                string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Invoice");
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }

                string fileName = $"Invoice_{personalAccount.Number_PersonalAccount}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string filePath = System.IO.Path.Combine(folderPath, fileName);

                PdfSharp.Pdf.PdfDocument document = new PdfSharp.Pdf.PdfDocument();
                document.Info.Title = "Квитанция";

                PdfSharp.Pdf.PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Verdana", 20);
                gfx.DrawString("Квитанция на оплату коммунальных услуг", font, XBrushes.Black,
                  new XRect(0, 0, page.Width, page.Height / 2),
                  XStringFormats.Center);

                font = new XFont("Verdana", 12);
                gfx.DrawString($"Лицевой счет: {personalAccount.Number_PersonalAccount}", font, XBrushes.Black,
                  new XRect(0, page.Height / 2, page.Width, page.Height / 2),
                  XStringFormats.Center);

                gfx.DrawString($"Дата: {DateTime.Now:dd.MM.yyyy}", font, XBrushes.Black,
                  new XRect(0, (page.Height / 2) + 20, page.Width, page.Height / 2),
                  XStringFormats.Center);

                gfx.DrawString($"Итоговая сумма к оплате: {totalAmount} руб.", font, XBrushes.Black,
                  new XRect(0, (page.Height / 2) + 40, page.Width, page.Height / 2),
                  XStringFormats.Center);

                document.Save(filePath);
                document.Close();

                var invoice = new Invoice
                {
                    InvoiceDate = DateTime.Now,
                    TotalAmount = totalAmount,
                    InvoicePath = filePath,
                    ID_PersonalAccount = personalAccount.ID_PersonalAccount
                };

                AppData.db.Invoice.Add(invoice);

                lastColdWaterReading = coldWaterReading;
                lastHotWaterReading = hotWaterReading;
                lastHeatingReading = heatingReading;
                lastElectricityReading = electricityReading;

                AppData.db.SaveChanges();
                MessageBox.Show("Квитанция успешно сохранена в PDF файл", "Успех", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                txtTotalAmount.Text = "Не удалось рассчитать общую стоимость";
                MessageBox.Show($"Ошибка при формировании квитанции: {ex.Message}", 
                    "Ошибка", MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }
        
        private void txtUserMetersHeating_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbUserMetersHeating.Focus();
        }

        private void btnUserMyPaymentPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserMyPaymentPage());
        }
    }
}
