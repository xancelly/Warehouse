using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Логика взаимодействия для EditOrganization.xaml
    /// </summary>
    public partial class EditOrganization : Page
    {
        Organization CurrentOrganization = null;
        public EditOrganization(Organization organization)
        {
            InitializeComponent();
            TypeOrgComboBox.ItemsSource = AppData.Context.TypeOrganization.ToList();

            CurrentOrganization = organization;
            if (CurrentOrganization != null)
            {
                SaveButton.Content = "Изменить";
                TypeOrgComboBox.SelectedItem = CurrentOrganization.TypeOrganization;
                NameTextBox.Text = CurrentOrganization.Name;
                DateRegDatePicker.SelectedDate = CurrentOrganization.DateRegistration;
                EmailTextBox.Text = CurrentOrganization.Email;
                PhoneNumberTextBox.Text = CurrentOrganization.PhoneNumber;
                InnTextBox.Text = CurrentOrganization.INN;
                OgrnTextBox.Text = CurrentOrganization.OGRN;
                LastNameTextBox.Text = CurrentOrganization.LastName;
                FirstNameTextBox.Text = CurrentOrganization.FirstName;
                MiddleNameTextBox.Text = CurrentOrganization.MiddleName;

                AreaTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentOrganization.IdAddress).Select(c => c.Region).FirstOrDefault();
                CityTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentOrganization.IdAddress).Select(c => c.City).FirstOrDefault();
                StreetTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentOrganization.IdAddress).Select(c => c.Street).FirstOrDefault();
                HouseTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentOrganization.IdAddress).Select(c => c.House).FirstOrDefault();
                ApartmentTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentOrganization.IdAddress).Select(c => c.Apartment).FirstOrDefault();
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
            if (TypeOrgComboBox.SelectedItem != null && !String.IsNullOrWhiteSpace(NameTextBox.Text) && DateRegDatePicker.SelectedDate != null && !String.IsNullOrWhiteSpace(PhoneNumberTextBox.Text) && !String.IsNullOrWhiteSpace(EmailTextBox.Text) && !String.IsNullOrWhiteSpace(InnTextBox.Text) && !String.IsNullOrWhiteSpace(OgrnTextBox.Text) && !String.IsNullOrWhiteSpace(LastNameTextBox.Text) && !String.IsNullOrWhiteSpace(FirstNameTextBox.Text) && !String.IsNullOrWhiteSpace(AreaTextBox.Text) && !String.IsNullOrWhiteSpace(CityTextBox.Text) && !String.IsNullOrWhiteSpace(StreetTextBox.Text) && !String.IsNullOrWhiteSpace(HouseTextBox.Text))
            {
                if (LastNameTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                {
                    if (FirstNameTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                    {
                        if (MiddleNameTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                        {
                            if (PhoneNumberTextBox.Text.Length == 18 && (PhoneNumberTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1))
                            {
                                if (new EmailAddressAttribute().IsValid(EmailTextBox.Text))
                                {
                                    if (InnTextBox.Text.Length == 10 && !InnTextBox.Text.Contains('_'))
                                    {
                                        if (OgrnTextBox.Text.Length == 13 && !OgrnTextBox.Text.Contains('_'))
                                        {
                                            if (DateRegDatePicker.SelectedDate < DateTime.Today)
                                            {
                                                if (AreaTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                                                {
                                                    if (CityTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                                                    {
                                                        if (CurrentOrganization == null)
                                                        {
                                                            if (AppData.Context.Organization.Where(c => c.Name == NameTextBox.Text).FirstOrDefault() == null)
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
                                                                CurrentOrganization = new Organization()
                                                                {
                                                                    TypeOrganization = TypeOrgComboBox.SelectedItem as TypeOrganization,
                                                                    Name = NameTextBox.Text,
                                                                    DateRegistration = DateRegDatePicker.SelectedDate,
                                                                    PhoneNumber = PhoneNumberTextBox.Text,
                                                                    Email = EmailTextBox.Text,
                                                                    INN = InnTextBox.Text,
                                                                    OGRN = OgrnTextBox.Text,
                                                                    LastName = LastNameTextBox.Text,
                                                                    FirstName = FirstNameTextBox.Text,
                                                                    MiddleName = MiddleNameTextBox.Text,
                                                                    IdAddress = CurrentAddress.Id,
                                                                };
                                                                AppData.Context.Organization.Add(CurrentOrganization);
                                                                MessageBox.Show("Организация успешно добавлена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                                                                AppData.Context.SaveChanges();
                                                                NavigationService.GoBack();
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Организация с таким название уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                                NameTextBox.Focus();
                                                            }

                                                        }
                                                        else
                                                        {
                                                            CurrentOrganization.TypeOrganization = TypeOrgComboBox.SelectedItem as TypeOrganization;
                                                            CurrentOrganization.Name = NameTextBox.Text;
                                                            CurrentOrganization.DateRegistration = DateRegDatePicker.SelectedDate;
                                                            CurrentOrganization.PhoneNumber = PhoneNumberTextBox.Text;
                                                            CurrentOrganization.Email = EmailTextBox.Text;
                                                            CurrentOrganization.INN = InnTextBox.Text;
                                                            CurrentOrganization.OGRN = OgrnTextBox.Text;
                                                            CurrentOrganization.LastName = LastNameTextBox.Text;
                                                            CurrentOrganization.FirstName = FirstNameTextBox.Text;
                                                            CurrentOrganization.MiddleName = MiddleNameTextBox.Text;

                                                            Address CurrentAddress = AppData.Context.Address.Where(c => c.Id == CurrentOrganization.IdAddress).FirstOrDefault();
                                                            CurrentAddress.Region = AreaTextBox.Text;
                                                            CurrentAddress.City = CityTextBox.Text;
                                                            CurrentAddress.Street = StreetTextBox.Text;
                                                            CurrentAddress.House = HouseTextBox.Text;
                                                            CurrentAddress.Apartment = ApartmentTextBox.Text;

                                                            MessageBox.Show("Информация обновлена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                                                            AppData.Context.SaveChanges();
                                                            NavigationService.GoBack();
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
                                                MessageBox.Show("Дата регистрации указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                DateRegDatePicker.Focus();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("ОГРН указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                            OgrnTextBox.Focus();
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
                                PhoneNumberTextBox.Focus();
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
                        MiddleNameTextBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Фамилия указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    MiddleNameTextBox.Focus();
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }




        }
    }
}
