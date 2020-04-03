using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
using Warehouse.Entities;

namespace Warehouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        BitmapImage image;
        Staff CurrentStaff = null;
        public RegistrationPage(Staff staff)
        {
            InitializeComponent();
            CurrentStaff = staff;
            if (CurrentStaff != null)
            {
                SaveButton.Content = "Изменить";
                LoginTextBox.Text = CurrentStaff.Login;
                PasswordTextBox.Password = CurrentStaff.Password;
                RePasswordTextBox.Password = CurrentStaff.Password;
                LastNameTextBox.Text = CurrentStaff.LastName;
                FirstNameTextBox.Text = CurrentStaff.FirstName;
                MiddleNameTextBox.Text = CurrentStaff.MiddleName;
                GenderComboBox.Text = CurrentStaff.Gender;
                PhoneTextBox.Text = CurrentStaff.PhoneNumber;
                EmailTextBox.Text = CurrentStaff.Email;
                InnTextBox.Text = CurrentStaff.INN;
                SnilsTextBox.Text = CurrentStaff.Snils;
                DateOfBirthDatePicker.SelectedDate = CurrentStaff.DateOfBirth;
                GenderComboBox.SelectedItem = CurrentStaff.Gender;

                AreaTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentStaff.IdAddress).Select(c => c.Region).FirstOrDefault();
                CityTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentStaff.IdAddress).Select(c => c.City).FirstOrDefault();
                StreetTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentStaff.IdAddress).Select(c => c.Street).FirstOrDefault();
                HouseTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentStaff.IdAddress).Select(c => c.House).FirstOrDefault();
                ApartmentTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentStaff.IdAddress).Select(c => c.Apartment).FirstOrDefault();

                PassportTextBox.Text = AppData.Context.Passport.Where(c => c.Id == CurrentStaff.IdPassport).Select(c => c.Serial + c.Number).FirstOrDefault();
                DateOfIssueDatePicker.SelectedDate = AppData.Context.Passport.Where(c => c.Id == CurrentStaff.IdPassport).Select(c => c.DateOfIssue).FirstOrDefault();
                IssuedByWhomTextBox.Text = AppData.Context.Passport.Where(c => c.Id == CurrentStaff.IdPassport).Select(c => c.IssuedByWhom).FirstOrDefault();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string numList = "1234567890";
            if (!String.IsNullOrWhiteSpace(LoginTextBox.Text) && !String.IsNullOrWhiteSpace(PasswordTextBox.Password) && !String.IsNullOrWhiteSpace(RePasswordTextBox.Password) && !String.IsNullOrWhiteSpace(LastNameTextBox.Text) && !String.IsNullOrWhiteSpace(FirstNameTextBox.Text) && !String.IsNullOrWhiteSpace(GenderComboBox.Text) && !String.IsNullOrWhiteSpace(PhoneTextBox.Text) && !String.IsNullOrWhiteSpace(EmailTextBox.Text) && DateOfBirthDatePicker.SelectedDate != null && !String.IsNullOrWhiteSpace(InnTextBox.Text) && !String.IsNullOrWhiteSpace(SnilsTextBox.Text) && !String.IsNullOrWhiteSpace(AreaTextBox.Text) && !String.IsNullOrWhiteSpace(CityTextBox.Text) && !String.IsNullOrWhiteSpace(StreetTextBox.Text) && !String.IsNullOrWhiteSpace(HouseTextBox.Text) && !String.IsNullOrWhiteSpace(PassportTextBox.Text) && !String.IsNullOrWhiteSpace(IssuedByWhomTextBox.Text) && DateOfIssueDatePicker != null)
            {
                if (PasswordTextBox.Password == RePasswordTextBox.Password)
                {
                    if (LastNameTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                    {
                        if (FirstNameTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                        {
                            if (MiddleNameTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                            {
                                if (GenderComboBox.SelectedItem != null)
                                {
                                    if (PhoneTextBox.Text.Length == 18 && (PhoneTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1))
                                    {
                                        if (new EmailAddressAttribute().IsValid(EmailTextBox.Text))
                                        {
                                            if (InnTextBox.Text.Length == 12 && !InnTextBox.Text.Contains('_'))
                                            {
                                                if (SnilsTextBox.Text.Length == 14 && !SnilsTextBox.Text.Contains('_'))
                                                {
                                                    if (AreaTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                                                    {
                                                        if (CityTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                                                        {
                                                            if (PassportTextBox.Text.Length == 12)
                                                            {
                                                                if (DateOfIssueDatePicker.SelectedDate < DateTime.Today)
                                                                {
                                                                    if (DateOfBirthDatePicker.SelectedDate < DateTime.Today)
                                                                    {
                                                                        if (CurrentStaff == null)
                                                                        {
                                                                            if (AppData.Context.Staff.Where(c => c.Login == LoginTextBox.Text).FirstOrDefault() == null)
                                                                            {
                                                                                Address CurrentAddress = new Address()
                                                                                {
                                                                                    CodeCountry = "RU",
                                                                                    Region = AreaTextBox.Text,
                                                                                    City = CityTextBox.Text,
                                                                                    Street = StreetTextBox.Text,
                                                                                    House = HouseTextBox.Text,
                                                                                    Apartment = ApartmentTextBox.Text,
                                                                                };
                                                                                AppData.Context.Address.Add(CurrentAddress);
                                                                                Passport CurrentPassport = new Passport()
                                                                                {
                                                                                    Serial = PassportTextBox.Text.Remove(5, 7),
                                                                                    Number = PassportTextBox.Text.Remove(0, 6),
                                                                                    DateOfIssue = DateOfIssueDatePicker.SelectedDate,
                                                                                    IssuedByWhom = IssuedByWhomTextBox.Text,
                                                                                };
                                                                                AppData.Context.Passport.Add(CurrentPassport);
                                                                                CurrentStaff = new Staff()
                                                                                {
                                                                                    Login = LoginTextBox.Text,
                                                                                    Password = PasswordTextBox.Password,
                                                                                    LastName = LastNameTextBox.Text,
                                                                                    FirstName = FirstNameTextBox.Text,
                                                                                    MiddleName = MiddleNameTextBox.Text,
                                                                                    Gender = GenderComboBox.Text,
                                                                                    PhoneNumber = PhoneTextBox.Text,
                                                                                    Email = EmailTextBox.Text,
                                                                                    INN = InnTextBox.Text,
                                                                                    Snils = SnilsTextBox.Text,
                                                                                    DateOfBirth = DateOfBirthDatePicker.SelectedDate,
                                                                                    IdAddress = CurrentAddress.Id,
                                                                                    IdPassport = CurrentPassport.Id,
                                                                                    IdRole = 1,
                                                                                    ImagePreview = StaffImage.DataContext as byte[],
                                                                                };
                                                                                AppData.Context.Staff.Add(CurrentStaff);
                                                                                NavigationService.Navigate(new Captcha());
                                                                            }
                                                                            else
                                                                            {
                                                                                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                                                LoginTextBox.Focus();
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            CurrentStaff.Login = LoginTextBox.Text;
                                                                            CurrentStaff.Password = PasswordTextBox.Password;
                                                                            CurrentStaff.LastName = LastNameTextBox.Text;
                                                                            CurrentStaff.FirstName = FirstNameTextBox.Text;
                                                                            CurrentStaff.MiddleName = MiddleNameTextBox.Text;
                                                                            CurrentStaff.Gender = GenderComboBox.Text;
                                                                            CurrentStaff.PhoneNumber = PhoneTextBox.Text;
                                                                            CurrentStaff.Email = EmailTextBox.Text;
                                                                            CurrentStaff.DateOfBirth = DateOfBirthDatePicker.SelectedDate;
                                                                            CurrentStaff.INN = InnTextBox.Text;
                                                                            CurrentStaff.Snils = SnilsTextBox.Text;

                                                                            Address CurrentAddress = AppData.Context.Address.Where(c => c.Id == CurrentStaff.IdAddress).FirstOrDefault();
                                                                            CurrentAddress.Region = AreaTextBox.Text;
                                                                            CurrentAddress.City = CityTextBox.Text;
                                                                            CurrentAddress.Street = StreetTextBox.Text;
                                                                            CurrentAddress.House = HouseTextBox.Text;
                                                                            CurrentAddress.Apartment = ApartmentTextBox.Text;

                                                                            Passport CurrentPassport = AppData.Context.Passport.Where(c => c.Id == CurrentStaff.IdPassport).FirstOrDefault();
                                                                            CurrentPassport.Serial = PassportTextBox.Text.Remove(5, 7);
                                                                            CurrentPassport.Number = PassportTextBox.Text.Remove(0, 6);
                                                                            CurrentPassport.DateOfIssue = DateOfIssueDatePicker.SelectedDate;
                                                                            CurrentPassport.IssuedByWhom = IssuedByWhomTextBox.Text;
                                                                            MessageBox.Show("Информация обновлена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                                                                            NavigationService.Navigate(new Captcha());
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("Дата рождения указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                                        DateOfBirthDatePicker.Focus();
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Дата выдачи паспорта указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                                    DateOfIssueDatePicker.Focus();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Серия и номер паспорта указаны некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                                PassportTextBox.Focus();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Город указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                            CityTextBox.Focus();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Область указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                        AreaTextBox.Focus();
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("СНИЛС указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                    SnilsTextBox.Focus();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("ИНН указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                InnTextBox.Focus();
                                            }

                                        }
                                        else
                                        {
                                            MessageBox.Show("E-Mail адрес указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                            EmailTextBox.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Номер телефона указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                        PhoneTextBox.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Пол не выбран!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    GenderComboBox.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Отчество указано некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                MiddleNameTextBox.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Имя указано некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            FirstNameTextBox.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Фамилия указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        LastNameTextBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image | *.jpg;*.png;";
            if (ofd.ShowDialog() == true)
            {
                StaffImage.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }
    }
}
