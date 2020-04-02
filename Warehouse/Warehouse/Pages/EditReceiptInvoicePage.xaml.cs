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
        Party CurrentParty = null;
        WarehouseGood CurrentWarehouseGood = null;
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

            RecCurrentOrganization = AppData.Context.Organization.Where(c => c.Name.ToLower().Contains("ТТЛ".ToLower())).FirstOrDefault();

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

            RecCurrentWarehouse = AppData.Context.Warehouse.Where(c => c.IdOrganization == RecCurrentOrganization.Id).FirstOrDefault();

            RecWarAreaTextBox.Text = RecCurrentWarehouse.Address.Region;
            RecWarCityTextBox.Text = RecCurrentWarehouse.Address.City;
            RecWarStreetTextBox.Text = RecCurrentWarehouse.Address.Street;
            RecWarHouseTextBox.Text = RecCurrentWarehouse.Address.House;


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
                }
                if (CountTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1)
                {
                    if (PriceTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1)
                    {
                        CurrentGood = new Good()
                        {
                            Name = NameTextBox.Text,
                            IdManufacturer = CurrentManufacturer.Id,
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

                        CurrentParty = new Party()
                        {
                            Name = "Партия '" + CurrentGood.Name + "'",
                            IdGood = CurrentGood.Id,
                            Count = CurrentDocumentGood.Count,
                            DateProduction = DateTime.Today,
                        };
                        AppData.Context.Party.Add(CurrentParty);
                        AppData.Context.SaveChanges();

                        CurrentWarehouseGood = new WarehouseGood()
                        {
                            IdWarehouse = RecCurrentWarehouse.Id,
                            IdParty = CurrentParty.Id,
                        };
                        AppData.Context.WarehouseGood.Add(CurrentWarehouseGood);
                        AppData.Context.SaveChanges();
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
            }

            //Создание докумкнта
            if (RecTypeOrgComboBox.SelectedItem != null && !String.IsNullOrWhiteSpace(RecNameTextBox.Text) && RecDateRegDatePicker.SelectedDate != null && !String.IsNullOrWhiteSpace(RecPhoneNumberTextBox.Text) && !String.IsNullOrWhiteSpace(RecEmailTextBox.Text) && !String.IsNullOrWhiteSpace(RecInnTextBox.Text) && !String.IsNullOrWhiteSpace(RecOgrnTextBox.Text) && !String.IsNullOrWhiteSpace(RecFIOTextBox.Text) && !String.IsNullOrWhiteSpace(RecAreaTextBox.Text) && !String.IsNullOrWhiteSpace(RecCityTextBox.Text) && !String.IsNullOrWhiteSpace(RecStreetTextBox.Text) && !String.IsNullOrWhiteSpace(RecHouseTextBox.Text) && SenTypeOrgComboBox.SelectedItem != null && !String.IsNullOrWhiteSpace(SenNameTextBox.Text) && SenDateRegDatePicker.SelectedDate != null && !String.IsNullOrWhiteSpace(SenPhoneNumberTextBox.Text) && !String.IsNullOrWhiteSpace(SenEmailTextBox.Text) && !String.IsNullOrWhiteSpace(SenInnTextBox.Text) && !String.IsNullOrWhiteSpace(SenOgrnTextBox.Text) && !String.IsNullOrWhiteSpace(SenFIOTextBox.Text) && !String.IsNullOrWhiteSpace(SenAreaTextBox.Text) && !String.IsNullOrWhiteSpace(SenCityTextBox.Text) && !String.IsNullOrWhiteSpace(SenStreetTextBox.Text) && !String.IsNullOrWhiteSpace(SenHouseTextBox.Text) && !String.IsNullOrWhiteSpace(SenWarAreaTextBox.Text) && !String.IsNullOrWhiteSpace(SenWarCityTextBox.Text) && !String.IsNullOrWhiteSpace(SenWarStreetTextBox.Text) && !String.IsNullOrWhiteSpace(SenWarHouseTextBox.Text) && !String.IsNullOrWhiteSpace(RecWarAreaTextBox.Text) && !String.IsNullOrWhiteSpace(RecWarCityTextBox.Text) && !String.IsNullOrWhiteSpace(RecWarStreetTextBox.Text) && !String.IsNullOrWhiteSpace(RecWarHouseTextBox.Text))
            {
                CurrentDocument.IdRecipient = RecCurrentOrganization.Id;
                CurrentDocument.IdSener = SenCurrentOrganization.Id;
                CurrentDocument.IdWarRecipient = RecCurrentWarehouse.Id;
                CurrentDocument.IdWarSender = SenCurrentWarehouse.Id;
                CurrentDocument.Date = DateTime.Today;
                AppData.Context.SaveChanges();


            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (CurrentDocument.IdRecipient == null && CurrentDocument.IdSener == null && CurrentDocument.IdWarRecipient == null && CurrentDocument.IdWarSender == null && CurrentDocument.Date == null)
            {
                AppData.Context.DocumentGood.Remove(CurrentDocumentGood);
                AppData.Context.Document.Remove(CurrentDocument);
                AppData.Context.SaveChanges();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DocumentGood CurrentGood = GoodsDataGrid.SelectedItem as DocumentGood;
            if (CurrentGood != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить товар?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AppData.Context.DocumentGood.Remove(CurrentGood);
                    AppData.Context.SaveChanges();
                    MessageBox.Show("Товар был удалён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    GoodsDataGrid.ItemsSource = AppData.Context.DocumentGood.Where(c => c.IdDocument == CurrentDocument.Id).ToList();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
