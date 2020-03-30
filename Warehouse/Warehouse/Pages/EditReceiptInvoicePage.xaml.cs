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
    /// Логика взаимодействия для EditReceiptInvoicePage.xaml
    /// </summary>
    public partial class EditReceiptInvoicePage : Page
    {
        Organization SenCurrentOrganization = null;
        Organization RecCurrentOrganization = null;
        DocumentGood CurrentDocumentGood = null;
        Document CurrentDocument = null;
        Entities.Warehouse SenCurrentWarehouse = null;
        Entities.Warehouse RecCurrentWarehouse = null;
        Manufacturer CurrentManufacturer = null;
        Good CurrentGood = null;
        public EditReceiptInvoicePage()
        {
            InitializeComponent();
            SenTypeOrgComboBox.ItemsSource = AppData.Context.TypeOrganization.ToList();
            RecTypeOrgComboBox.ItemsSource = AppData.Context.TypeOrganization.ToList();
            TypeOrgComboBox.ItemsSource = AppData.Context.TypeOrganization.ToList();
            CountryComboBox.ItemsSource = AppData.Context.Country.ToList();
            GroupGoodComboBox.ItemsSource = AppData.Context.GroupGood.ToList();
            UnitComboBox.ItemsSource = AppData.Context.Unit.ToList();
            CurrentDocument = new Document()
            {
                IdRecipient = null,
                IdSener = null,
                IdTypeDocument = 1,
                Date = null,
                IdWarRecipient = null,
                IdWarSender = null,
            };
            AppData.Context.Document.Add(CurrentDocument);
            AppData.Context.SaveChanges();
            Properties.Settings.Default.IdDocument = CurrentDocument.Id;
        }

        private void SenNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SenCurrentOrganization = AppData.Context.Organization.Where(c => c.Name.ToLower().Contains(SenNameTextBox.Text.ToLower())).FirstOrDefault();
            if (SenCurrentOrganization != null)
            {
                if (MessageBox.Show("Организация с таким названием уже есть в базе.\nЖелаете воспользоваться автозаполнением?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    SenTypeOrgComboBox.SelectedItem = SenCurrentOrganization.TypeOrganization;
                    SenNameTextBox.Text = SenCurrentOrganization.Name;
                    SenDateRegDatePicker.SelectedDate = SenCurrentOrganization.DateRegistration;
                    SenPhoneNumberTextBox.Text = SenCurrentOrganization.PhoneNumber;
                    SenEmailTextBox.Text = SenCurrentOrganization.Email;
                    SenInnTextBox.Text = SenCurrentOrganization.INN;
                    SenOgrnTextBox.Text = SenCurrentOrganization.OGRN;
                    SenFIOTextBox.Text = SenCurrentOrganization.FullnameDirector;

                    SenAreaTextBox.Text = AppData.Context.Address.Where(c => c.Id == SenCurrentOrganization.IdAddress).Select(c => c.Region).FirstOrDefault();
                    SenCityTextBox.Text = AppData.Context.Address.Where(c => c.Id == SenCurrentOrganization.IdAddress).Select(c => c.City).FirstOrDefault();
                    SenStreetTextBox.Text = AppData.Context.Address.Where(c => c.Id == SenCurrentOrganization.IdAddress).Select(c => c.Street).FirstOrDefault();
                    SenHouseTextBox.Text = AppData.Context.Address.Where(c => c.Id == SenCurrentOrganization.IdAddress).Select(c => c.House).FirstOrDefault();
                }
            }
        }

        private void RecNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            RecCurrentOrganization = AppData.Context.Organization.Where(c => c.Name.ToLower().Contains(RecNameTextBox.Text.ToLower())).FirstOrDefault();
            if (RecCurrentOrganization != null)
            {
                if (MessageBox.Show("Организация с таким названием уже есть в базе.\nЖелаете воспользоваться автозаполнением?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    RecTypeOrgComboBox.SelectedItem = RecCurrentOrganization.TypeOrganization;
                    RecNameTextBox.Text = RecCurrentOrganization.Name;
                    RecDateRegDatePicker.SelectedDate = RecCurrentOrganization.DateRegistration;
                    RecPhoneNumberTextBox.Text = RecCurrentOrganization.PhoneNumber;
                    RecEmailTextBox.Text = RecCurrentOrganization.Email;
                    RecInnTextBox.Text = RecCurrentOrganization.INN;
                    RecOgrnTextBox.Text = RecCurrentOrganization.OGRN;
                    RecFIOTextBox.Text = RecCurrentOrganization.FullnameDirector;

                    RecAreaTextBox.Text = AppData.Context.Address.Where(c => c.Id == RecCurrentOrganization.IdAddress).Select(c => c.Region).FirstOrDefault();
                    RecCityTextBox.Text = AppData.Context.Address.Where(c => c.Id == RecCurrentOrganization.IdAddress).Select(c => c.City).FirstOrDefault();
                    RecStreetTextBox.Text = AppData.Context.Address.Where(c => c.Id == RecCurrentOrganization.IdAddress).Select(c => c.Street).FirstOrDefault();
                    RecHouseTextBox.Text = AppData.Context.Address.Where(c => c.Id == RecCurrentOrganization.IdAddress).Select(c => c.House).FirstOrDefault();
                }
            }
        }

        private void AddGoodButton_Click(object sender, RoutedEventArgs e)
        {
            string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string numList = "1234567890";
            if (TypeOrgComboBox.SelectedItem != null && !String.IsNullOrWhiteSpace(NameManTextBox.Text) && CountryComboBox.SelectedItem != null && !String.IsNullOrWhiteSpace(NameTextBox.Text) && GroupGoodComboBox.SelectedItem != null && UnitComboBox.SelectedItem != null && ShelfLifeDatePicker.SelectedDate != null && !String.IsNullOrWhiteSpace(CountTextBox.Text) && !String.IsNullOrWhiteSpace(PriceTextBox.Text))
            {
                CurrentManufacturer = AppData.Context.Manufacturer.Where(c => c.Name.ToLower().Contains(NameManTextBox.Text.ToLower())).FirstOrDefault();
                if (CurrentManufacturer == null)
                {
                    CurrentManufacturer = new Manufacturer()
                    {
                        TypeOrganization = TypeOrgComboBox.SelectedItem as TypeOrganization,
                        Name = NameManTextBox.Text,
                        Country = CountryComboBox.SelectedItem as Country,
                    };
                    AppData.Context.Manufacturer.Add(CurrentManufacturer);
                    AppData.Context.SaveChanges();
                    Properties.Settings.Default.IdManufacturer = CurrentManufacturer.Id;
                }
                else
                {
                    Properties.Settings.Default.IdManufacturer = CurrentManufacturer.Id;
                }
                if (CountTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1)
                {
                    if (PriceTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1)
                    {
                        CurrentGood = new Good()
                        {
                            Name = NameTextBox.Text,
                            IdManufacturer = Properties.Settings.Default.IdManufacturer,
                            GroupGood = GroupGoodComboBox.SelectedItem as GroupGood,
                            Unit = UnitComboBox.SelectedItem as Unit,
                            ShelfLife = ShelfLifeDatePicker.SelectedDate,
                        };
                        AppData.Context.Good.Add(CurrentGood);
                        AppData.Context.SaveChanges();

                        CurrentDocumentGood = new DocumentGood()
                        {
                            IdDocument = Properties.Settings.Default.IdDocument,
                            IdGood = CurrentGood.Id,
                            Count = Convert.ToInt32(CountTextBox.Text),
                            Price = Convert.ToDecimal(PriceTextBox.Text),
                        };
                        AppData.Context.DocumentGood.Add(CurrentDocumentGood);
                        AppData.Context.SaveChanges();

                        GoodsDataGrid.ItemsSource = AppData.Context.DocumentGood.Where(c => c.IdDocument == CurrentDocument.Id).ToList();
                    } else
                    {
                        MessageBox.Show("Стоимость товара введена некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                } else
                {
                    MessageBox.Show("Количество товара введено некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } else
            {
                MessageBox.Show("Не все поля товара заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Если огранизация отправителя отсутствует, то сохранение в базу
            SenCurrentOrganization = AppData.Context.Organization.Where(c => c.Name.ToLower().Contains(SenNameTextBox.Text.ToLower())).FirstOrDefault();
            if (SenCurrentOrganization == null)
            {
                string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
                string numList = "1234567890";
                if (SenTypeOrgComboBox.SelectedItem != null && !String.IsNullOrWhiteSpace(SenNameTextBox.Text) && SenDateRegDatePicker.SelectedDate != null && !String.IsNullOrWhiteSpace(SenPhoneNumberTextBox.Text) && !String.IsNullOrWhiteSpace(SenEmailTextBox.Text) && !String.IsNullOrWhiteSpace(SenInnTextBox.Text) && !String.IsNullOrWhiteSpace(SenOgrnTextBox.Text) && !String.IsNullOrWhiteSpace(SenFIOTextBox.Text) && !String.IsNullOrWhiteSpace(SenAreaTextBox.Text) && !String.IsNullOrWhiteSpace(SenCityTextBox.Text) && !String.IsNullOrWhiteSpace(SenStreetTextBox.Text) && !String.IsNullOrWhiteSpace(SenHouseTextBox.Text))
                {
                    if (SenFIOTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                    {
                        if (SenPhoneNumberTextBox.Text.Length == 18 && (SenPhoneNumberTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1))
                        {
                            if (new EmailAddressAttribute().IsValid(SenEmailTextBox.Text))
                            {
                                if (SenInnTextBox.Text.Length == 10 && !SenInnTextBox.Text.Contains('_'))
                                {
                                    if (SenOgrnTextBox.Text.Length == 13 && !SenOgrnTextBox.Text.Contains('_'))
                                    {
                                        if (SenDateRegDatePicker.SelectedDate < DateTime.Today)
                                        {
                                            if (SenAreaTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                                            {
                                                if (SenCityTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                                                {
                                                    Address SenCurrentAddress = new Address()
                                                    {
                                                        CodeCountry = "RU",
                                                        Region = SenAreaTextBox.Text,
                                                        City = SenCityTextBox.Text,
                                                        Street = SenStreetTextBox.Text,
                                                        House = SenHouseTextBox.Text,
                                                    };
                                                    AppData.Context.Address.Add(SenCurrentAddress);

                                                    String[] FullNameSen = SenFIOTextBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                                    SenCurrentOrganization = new Organization()
                                                    {
                                                        TypeOrganization = SenTypeOrgComboBox.SelectedItem as TypeOrganization,
                                                        Name = SenNameTextBox.Text,
                                                        DateRegistration = SenDateRegDatePicker.SelectedDate,
                                                        PhoneNumber = SenPhoneNumberTextBox.Text,
                                                        Email = SenEmailTextBox.Text,
                                                        INN = SenInnTextBox.Text,
                                                        OGRN = SenOgrnTextBox.Text,
                                                        LastName = FullNameSen[0],
                                                        FirstName = FullNameSen[1],
                                                        MiddleName = FullNameSen[2],
                                                        IdAddress = SenCurrentAddress.Id,
                                                    };
                                                    AppData.Context.Organization.Add(SenCurrentOrganization);
                                                    Properties.Settings.Default.IdSenOrg = SenCurrentOrganization.Id;
                                                    AppData.Context.SaveChanges();
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Город указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                    SenCityTextBox.Focus();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Область указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                SenAreaTextBox.Focus();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Дата регистрации указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                            SenDateRegDatePicker.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("ОГРН указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                        SenOgrnTextBox.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("ИНН указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    SenInnTextBox.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("E-Mail адрес указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                SenEmailTextBox.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Номер телефона указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            SenPhoneNumberTextBox.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Фамилия указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        SenFIOTextBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Не все поля отправителя заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            //Если огранизация получателя отсутствует, то сохранение в базу
            RecCurrentOrganization = AppData.Context.Organization.Where(c => c.Name.ToLower().Contains(RecNameTextBox.Text.ToLower())).FirstOrDefault();
            if (RecCurrentOrganization == null)
            {
                string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
                string numList = "1234567890";
                if (RecTypeOrgComboBox.SelectedItem != null && !String.IsNullOrWhiteSpace(RecNameTextBox.Text) && RecDateRegDatePicker.SelectedDate != null && !String.IsNullOrWhiteSpace(RecPhoneNumberTextBox.Text) && !String.IsNullOrWhiteSpace(RecEmailTextBox.Text) && !String.IsNullOrWhiteSpace(RecInnTextBox.Text) && !String.IsNullOrWhiteSpace(RecOgrnTextBox.Text) && !String.IsNullOrWhiteSpace(RecFIOTextBox.Text) && !String.IsNullOrWhiteSpace(RecAreaTextBox.Text) && !String.IsNullOrWhiteSpace(RecCityTextBox.Text) && !String.IsNullOrWhiteSpace(RecStreetTextBox.Text) && !String.IsNullOrWhiteSpace(RecHouseTextBox.Text))
                {
                    if (RecFIOTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                    {
                        if (RecPhoneNumberTextBox.Text.Length == 18 && (RecPhoneNumberTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1))
                        {
                            if (new EmailAddressAttribute().IsValid(RecEmailTextBox.Text))
                            {
                                if (RecInnTextBox.Text.Length == 10 && !RecInnTextBox.Text.Contains('_'))
                                {
                                    if (RecOgrnTextBox.Text.Length == 13 && !RecOgrnTextBox.Text.Contains('_'))
                                    {
                                        if (RecDateRegDatePicker.SelectedDate < DateTime.Today)
                                        {
                                            if (RecAreaTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                                            {
                                                if (RecCityTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                                                {
                                                    Address RecCurrentAddress = new Address()
                                                    {
                                                        CodeCountry = "RU",
                                                        Region = RecAreaTextBox.Text,
                                                        City = RecCityTextBox.Text,
                                                        Street = RecStreetTextBox.Text,
                                                        House = RecHouseTextBox.Text,
                                                    };
                                                    AppData.Context.Address.Add(RecCurrentAddress);

                                                    String[] FullNameRec = RecFIOTextBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                                    RecCurrentOrganization = new Organization()
                                                    {
                                                        TypeOrganization = RecTypeOrgComboBox.SelectedItem as TypeOrganization,
                                                        Name = RecNameTextBox.Text,
                                                        DateRegistration = RecDateRegDatePicker.SelectedDate,
                                                        PhoneNumber = RecPhoneNumberTextBox.Text,
                                                        Email = RecEmailTextBox.Text,
                                                        INN = RecInnTextBox.Text,
                                                        OGRN = RecOgrnTextBox.Text,
                                                        LastName = FullNameRec[0],
                                                        FirstName = FullNameRec[1],
                                                        MiddleName = FullNameRec[2],
                                                        IdAddress = RecCurrentAddress.Id,
                                                    };
                                                    AppData.Context.Organization.Add(RecCurrentOrganization);
                                                    Properties.Settings.Default.IdRecOrg = RecCurrentOrganization.Id;
                                                    AppData.Context.SaveChanges();
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Город указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                    RecCityTextBox.Focus();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Область указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                RecAreaTextBox.Focus();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Дата регистрации указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                            RecDateRegDatePicker.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("ОГРН указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                        RecOgrnTextBox.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("ИНН указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    RecInnTextBox.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("E-Mail адрес указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                RecEmailTextBox.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Номер телефона указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            RecPhoneNumberTextBox.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Фамилия указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        RecFIOTextBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Не все поля получателя заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            //Добавление склада отправителя, если он отсутствует
            SenCurrentWarehouse = AppData.Context.Warehouse.Where(c => c.Address.Region.ToLower().Contains(SenWarAreaTextBox.Text.ToLower()) && c.Address.City.ToLower().Contains(SenWarCityTextBox.Text.ToLower()) && c.Address.Street.ToLower().Contains(SenWarStreetTextBox.Text.ToLower()) && c.Address.House.ToLower().Contains(SenWarHouseTextBox.Text.ToLower())).FirstOrDefault();
            if (SenCurrentWarehouse == null)
            {
                string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
                string numList = "1234567890";
                if (SenWarAreaTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                {
                    if (SenWarCityTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                    {
                        Address SenWarCurrentAddress = new Address()
                        {
                            CodeCountry = "RU",
                            Region = SenWarAreaTextBox.Text,
                            City = SenWarCityTextBox.Text,
                            Street = SenWarStreetTextBox.Text,
                            House = SenWarHouseTextBox.Text,
                        };
                        AppData.Context.Address.Add(SenWarCurrentAddress);

                        SenCurrentWarehouse = new Entities.Warehouse()
                        {
                            IdAddress = SenWarCurrentAddress.Id,
                            IdOrganization = SenCurrentOrganization.Id,
                        };
                        AppData.Context.Warehouse.Add(SenCurrentWarehouse);
                        Properties.Settings.Default.IdSenWar = SenCurrentWarehouse.Id;
                        AppData.Context.SaveChanges();
                    } else
                    {
                        MessageBox.Show("Город указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        RecWarCityTextBox.Focus();
                    }
                } else
                {
                    MessageBox.Show("Область указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    RecWarAreaTextBox.Focus();
                }
            } else
            {
                //SenCurrentWarehouse.Organization = SenCurrentOrganization;
                //Address SenWarCurrentAddress = AppData.Context.Address.Where(c => c.Region == SenWarAreaTextBox.Text && c.City == SenWarCityTextBox.Text && c.Street == SenWarStreetTextBox.Text && c.House == SenWarHouseTextBox.Text).FirstOrDefault();
                //SenCurrentWarehouse.Address = SenWarCurrentAddress;
            }

            //Добавление склада отправителя, если он отсутствует
            RecCurrentWarehouse = AppData.Context.Warehouse.Where(c => c.Address.Region.ToLower().Contains(RecWarAreaTextBox.Text.ToLower()) && c.Address.City.ToLower().Contains(RecWarCityTextBox.Text.ToLower()) && c.Address.Street.ToLower().Contains(RecWarStreetTextBox.Text.ToLower()) && c.Address.House.ToLower().Contains(RecWarHouseTextBox.Text.ToLower())).FirstOrDefault();
            if (RecCurrentWarehouse == null)
            {
                string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
                string numList = "1234567890";
                if (RecWarAreaTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                {
                    if (RecWarCityTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                    {
                        Address RecWarCurrentAddress = new Address()
                        {
                            CodeCountry = "RU",
                            Region = RecWarAreaTextBox.Text,
                            City = RecWarCityTextBox.Text,
                            Street = RecWarStreetTextBox.Text,
                            House = RecWarHouseTextBox.Text,
                        };
                        AppData.Context.Address.Add(RecWarCurrentAddress);

                        RecCurrentWarehouse = new Entities.Warehouse()
                        {
                            IdAddress = RecWarCurrentAddress.Id,
                            IdOrganization = RecCurrentOrganization.Id,
                        };
                        AppData.Context.Warehouse.Add(RecCurrentWarehouse);
                        Properties.Settings.Default.IdRecWar = RecCurrentWarehouse.Id;
                        AppData.Context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Город указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        RecWarCityTextBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Область указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    RecWarAreaTextBox.Focus();
                }
            }
            else
            {
                //RecCurrentWarehouse.Organization = RecCurrentOrganization;
                //Address RecWarCurrentAddress = AppData.Context.Address.Where(c => c.Region == RecWarAreaTextBox.Text && c.City == RecWarCityTextBox.Text && c.Street == RecWarStreetTextBox.Text && c.House == RecWarHouseTextBox.Text).FirstOrDefault();
                //RecCurrentWarehouse.Address = RecWarCurrentAddress;
            }

            //Создание докумкнта
            CurrentDocument.IdRecipient = RecCurrentOrganization.Id;
            CurrentDocument.IdSener = SenCurrentOrganization.Id;
            CurrentDocument.IdWarRecipient = RecCurrentWarehouse.Id;
            CurrentDocument.IdWarSender = SenCurrentWarehouse.Id;
            CurrentDocument.Date = DateTime.Today;
            AppData.Context.SaveChanges();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (CurrentDocument.IdRecipient == null && CurrentDocument.IdSener == null && CurrentDocument.IdWarRecipient == null && CurrentDocument.IdWarSender == null && CurrentDocument.Date == null)
            {
                AppData.Context.Document.Remove(CurrentDocument);
                AppData.Context.SaveChanges();
            }
        }
    }
}
