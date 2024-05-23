using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using ManagamentCompanyDiplom.Classes;
using ManagamentCompanyDiplom.ModelBD;
using ManagamentCompanyDiplom.Pages;

namespace ManagamentCompanyDiplom.Pages
{
    public partial class ApplicationForm : Page
    {
        public ApplicationForm()
        {
            InitializeComponent();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnBackApplicationForm_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.ChangedButton == MouseButton.Left)
            //{
            //    this.DragMove();
            //}
        }

        private void txtSurname_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbSurname.Focus();
        }

        private void txbSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbSurname.Text) && txbSurname.Text.Length > 0)
            {
                txtSurname.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtSurname.Visibility = Visibility.Visible;
            }
        }

        private void txtFirstName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbFirstName.Focus();
        }

        private void txbFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbFirstName.Text) && txbFirstName.Text.Length > 0)
            {
                txtFirstName.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtFirstName.Visibility = Visibility.Visible;
            }
        }

        private void txtPatronymic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbPatronymic.Focus();
        }

        private void txbPatronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbPatronymic.Text) && txbPatronymic.Text.Length > 0)
            {
                txtPatronymic.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtPatronymic.Visibility = Visibility.Visible;
            }
        }

        private void txtPhone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbPhone.Focus();
        }

        private void txbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbPhone.Text) && txbPhone.Text.Length > 0)
            {
                txtPhone.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtPhone.Visibility = Visibility.Visible;
            }
        }

        private void txtHouse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbHouse.Focus();
        }

        private void txbHouse_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbHouse.Text) && txbHouse.Text.Length > 0)
            {
                txtHouse.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtHouse.Visibility = Visibility.Visible;
            }
        }

        private void txtFlat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbFlat.Focus();
        }

        private void txbFlat_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbFlat.Text) && txbFlat.Text.Length > 0)
            {
                txtFlat.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtFlat.Visibility = Visibility.Visible;
            }
        }

        private void btnApplicationForms_Click(object sender, RoutedEventArgs e)
        {
            if (txbSurname.Text == "" ||
                txbFirstName.Text == "" ||
                txbPatronymic.Text == "" ||
                txbPhone.Text == "" ||
                txbHouse.Text == "" ||
                txbFlat.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    Users AddUsers = new Users();
                    {
                        AddUsers.Surname = txbSurname.Text;
                        AddUsers.Firstname = txbFirstName.Text;
                        AddUsers.Patronymic = txbPatronymic.Text;
                        AddUsers.Phone = txbPhone.Text;
                        AddUsers.ID_StatusConfirmation = 2;
                        AddUsers.ID_Roles = 2;
                    }

                    int houseNumber = int.Parse(txbHouse.Text);
                    var idhouse = AppData.db.House.FirstOrDefault(h => h.NumberHouse == houseNumber);
                    
                    int nf = int.Parse(txbHouse.Text);
                    var numberflat = AppData.db.Flat.FirstOrDefault(g => g.NumberFlat == nf);

                    House AddHouse = new House();
                    {
                        AddHouse.NumberHouse = Convert.ToInt32(txbHouse.Text);
                        AddHouse.ID_House = idhouse != null ? idhouse.ID_House : 0;
                    }
                    Random random = new Random();
                    int area = random.Next(50, 76);
                    int NumberOfResidents = random.Next(1, 4);

                    Flat AddFlat = new Flat();
                    {
                        AddFlat.NumberFlat = Convert.ToInt32(txbFlat.Text);
                        AddFlat.ID_Users = AddUsers.ID_Users;
                        AddFlat.ID_House = AddHouse.ID_House;
                        AddFlat.Area = area;
                    }
                    if (idhouse == null)
                    {
                        MessageBox.Show("Указанный дом - не числится в нашей управляющей компании, " +
                            "к нам относятся - №30,№32, №34, №36",
                            "Уведомление",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                    }
                    else if (numberflat != null)
                    {
                        MessageBox.Show("Данная квартира уже имеет владельца и лицевой счёт",
                            "Уведомление",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                    }
                    else
                    {
                        AppData.db.Users.Add(AddUsers);
                        AppData.db.Flat.Add(AddFlat);
                        AppData.db.SaveChanges();

                        int addedFlatId = AddFlat.ID_Flat;
                        PersonalАccount per = new PersonalАccount
                        {
                            ID_Flat = addedFlatId,
                            NumberOfResidents = random.Next(1, 6),
                            Number_PersonalAccount = GenerateRandomNumber()
                        };
                        AppData.db.PersonalАccount.Add(per);
                        AppData.db.SaveChanges();
                        MessageBox.Show("Вы успешно оставили заявку, ожидайте рассмотрения - Администратором! После одобрения заявки, Администратор выдаст, Вам, логин и пароль от учётной записи!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.GoBack();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public static string GenerateRandomNumber()
        {
            Random random = new Random();
            string result = "7";
            for (int i = 0; i < 9; i++)
            {
                result += random.Next(0, 10).ToString();
            }

            return result;
        }

        private bool IsTextAllowed(string text, string regexPattern)
        {
            return Regex.IsMatch(text, regexPattern);
        }

        private void ValidateRussianTextInput(TextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                if (!Regex.IsMatch(textBox.Text, @"^[А-Яа-я]+$"))
                {
                    MessageBox.Show("Введите только русские буквы, первая буква должна быть заглавной.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    textBox.Text = string.Empty;
                }
                else
                {
                    if (textBox.Text.Length > 1)
                    {
                        textBox.Text = char.ToUpper(textBox.Text[0]) + textBox.Text.Substring(1).ToLower();
                    }
                    else
                    {
                        textBox.Text = textBox.Text.ToUpper();
                    }
                }
            }
        }

        private void txbSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text, @"^[А-Яа-я]+$");
        }

        private void txbSurname_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateRussianTextInput((TextBox)sender);
        }

        private void txbFirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text, @"^[А-Яа-я]+$");
        }

        private void txbFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateRussianTextInput((TextBox)sender);
        }

        private void txbPatronymic_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text, @"^[А-Яа-я]+$");
        }

        private void txbPatronymic_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateRussianTextInput((TextBox)sender);
        }

        private void txbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text, @"^[0-9]+$");
        }

        private void txbPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                string phone = textBox.Text;
                if (phone.Length == 10)
                {
                    textBox.Text = "+7(" + phone.Substring(0, 3) + ")" + phone.Substring(3, 3) + "-" + phone.Substring(6, 2) + "-" + phone.Substring(8, 2);
                }
                else
                {
                    MessageBox.Show("Введите корректный номер телефона (10 цифр без кода страны).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    textBox.Text = string.Empty;
                }
            }
        }

        private void txbHouse_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text, @"^[0-9]+$");
        }

        private void txbFlat_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text, @"^[0-9]+$");
        }
    }
} 
        
