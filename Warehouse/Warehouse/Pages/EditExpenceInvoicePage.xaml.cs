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
using WpfMessageBoxLibrary;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Warehouse.Entities;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditExpenceInvoicePage.xaml
    /// </summary>
    public partial class EditExpenceInvoicePage : Page
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
        public EditExpenceInvoicePage()
        {
            InitializeComponent();
            SenTypeOrgComboBox.ItemsSource = AppData.Context.TypeOrganization.ToList();
            RecTypeOrgComboBox.ItemsSource = AppData.Context.TypeOrganization.ToList();

            CurrentDocument = new Document()
            {
                IdRecipient = null,
                IdSener = null,
                IdTypeDocument = 2,
                Date = null,
                IdWarRecipient = null,
                IdWarSender = null,
            };
            AppData.Context.Document.Add(CurrentDocument);
            AppData.Context.SaveChanges();
            Properties.Settings.Default.IdDocument = CurrentDocument.Id;

            SenCurrentOrganization = AppData.Context.Organization.Where(c => c.Name.ToLower().Contains("ТТЛ".ToLower())).FirstOrDefault();

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

            SenCurrentWarehouse = AppData.Context.Warehouse.Where(c => c.IdOrganization == SenCurrentOrganization.Id).FirstOrDefault();

            SenWarAreaTextBox.Text = SenCurrentWarehouse.Address.Region;
            SenWarCityTextBox.Text = SenCurrentWarehouse.Address.City;
            SenWarStreetTextBox.Text = SenCurrentWarehouse.Address.Street;
            SenWarHouseTextBox.Text = SenCurrentWarehouse.Address.House;

            WarehouseDataGrid.ItemsSource = AppData.Context.WarehouseGood.Where(c => c.Warehouse.Organization.Name.ToLower().Contains("ТТЛ".ToLower())).ToList();

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

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (CurrentDocument.IdRecipient == null && CurrentDocument.IdSener == null && CurrentDocument.IdWarRecipient == null && CurrentDocument.IdWarSender == null && CurrentDocument.Date == null)
            {
                AppData.Context.Document.Remove(CurrentDocument);
                AppData.Context.SaveChanges();
            }
        }

        private void WarehouseDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string PriceFromMsg = "", CountFromMsg = "";
            CurrentWarehouseGood = WarehouseDataGrid.SelectedItem as WarehouseGood;
            if (CurrentWarehouseGood != null)
            {
                string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
                var CountWarehouse = CurrentWarehouseGood.Party.Count;
                var CountMsg = new WpfMessageBoxProperties()
                {
                    Button = MessageBoxButton.OKCancel,
                    ButtonOkText = "Добавить",
                    ButtonCancelText = "Отмена",
                    Image = MessageBoxImage.Information,
                    Header = "Введите количество",
                    IsTextBoxVisible = true,
                };
                MessageBoxResult resultcount = WpfMessageBox.Show(ref CountMsg);
                if (resultcount == MessageBoxResult.OK && (CountMsg.TextBoxText.IndexOfAny(letterList.ToCharArray()) <= -1) && Convert.ToInt32(CountMsg.TextBoxText) <= CountWarehouse)
                {
                    CountFromMsg = CountMsg.TextBoxText;
                    var PriceMsg = new WpfMessageBoxProperties()
                    {
                        Button = MessageBoxButton.OKCancel,
                        ButtonOkText = "Добавить",
                        ButtonCancelText = "Отмена",
                        Image = MessageBoxImage.Information,
                        Header = "Введите стоимость 1 шт.",
                        IsTextBoxVisible = true,
                    };
                    MessageBoxResult resultprice = WpfMessageBox.Show(ref PriceMsg);
                    if (resultprice == MessageBoxResult.OK && (PriceMsg.TextBoxText.IndexOfAny(letterList.ToCharArray()) <= -1))
                    {
                        PriceFromMsg = PriceMsg.TextBoxText;

                        CurrentDocumentGood = new DocumentGood()
                        {
                            Count = Convert.ToInt32(CountFromMsg),
                            Price = Convert.ToDecimal(PriceFromMsg),
                            IdGood = CurrentWarehouseGood.Party.Good.Id,
                            IdDocument = CurrentDocument.Id,
                        };
                        AppData.Context.DocumentGood.Add(CurrentDocumentGood);

                        if (Convert.ToInt32(CountFromMsg) < CountWarehouse)
                        {
                            CurrentWarehouseGood.Party.Count -= Convert.ToInt32(CountFromMsg);
                        }
                        else if (Convert.ToInt32(CountFromMsg) == CountWarehouse)
                        {
                            AppData.Context.WarehouseGood.Remove(CurrentWarehouseGood);
                        }

                        AppData.Context.SaveChanges();
                        GoodsDataGrid.ItemsSource = AppData.Context.DocumentGood.Where(c => c.IdDocument == CurrentDocument.Id).ToList();
                        WarehouseDataGrid.ItemsSource = AppData.Context.WarehouseGood.Where(c => c.Warehouse.Organization.Name.ToLower().Contains("ТТЛ".ToLower())).ToList();

                    }
                    else
                    {
                        MessageBox.Show("Введено некорректное значение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Введено некорректное значение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void GoodsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CurrentDocumentGood = GoodsDataGrid.SelectedItem as DocumentGood;
            if (CurrentDocumentGood != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить товар из накладной?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var GoodCheck = AppData.Context.WarehouseGood.Where(c => c.Party.Good.Id == CurrentDocumentGood.Good.Id).FirstOrDefault();
                    if (GoodCheck == null)
                    {
                        CurrentParty = new Party()
                        {
                            Name = "Партия '" + CurrentDocumentGood.Good.Name + "'",
                            IdGood = CurrentDocumentGood.IdGood,
                            Count = CurrentDocumentGood.Count,
                            DateProduction = DateTime.Today,
                        };
                        AppData.Context.Party.Add(CurrentParty);

                        CurrentWarehouseGood = new WarehouseGood()
                        {
                            IdWarehouse = SenCurrentWarehouse.Id,
                            IdParty = CurrentParty.Id,
                        };
                        AppData.Context.WarehouseGood.Add(CurrentWarehouseGood);
                    } else if (GoodCheck != null)
                    {
                        GoodCheck.Party.Count += CurrentDocumentGood.Count;
                    }
                    AppData.Context.DocumentGood.Remove(CurrentDocumentGood);
                    AppData.Context.SaveChanges();
                    GoodsDataGrid.ItemsSource = AppData.Context.DocumentGood.Where(c => c.IdDocument == CurrentDocument.Id).ToList();
                    WarehouseDataGrid.ItemsSource = AppData.Context.WarehouseGood.Where(c => c.Warehouse.Organization.Name.ToLower().Contains("ТТЛ".ToLower())).ToList();
                    MessageBox.Show("Товар был удалён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
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

                                                    String[] FullNameSen = RecFIOTextBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                                    RecCurrentOrganization = new Organization()
                                                    {
                                                        TypeOrganization = RecTypeOrgComboBox.SelectedItem as TypeOrganization,
                                                        Name = RecNameTextBox.Text,
                                                        DateRegistration = SenDateRegDatePicker.SelectedDate,
                                                        PhoneNumber = RecPhoneNumberTextBox.Text,
                                                        Email = RecEmailTextBox.Text,
                                                        INN = RecInnTextBox.Text,
                                                        OGRN = RecOgrnTextBox.Text,
                                                        LastName = FullNameSen[0],
                                                        FirstName = FullNameSen[1],
                                                        MiddleName = FullNameSen[2],
                                                        IdAddress = RecCurrentAddress.Id,
                                                    };
                                                    AppData.Context.Organization.Add(RecCurrentOrganization);
                                                    Properties.Settings.Default.IdSenOrg = RecCurrentOrganization.Id;
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
                    MessageBox.Show("Не все поля отправителя заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

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
                        Properties.Settings.Default.IdSenWar = RecCurrentWarehouse.Id;
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

            if (RecTypeOrgComboBox.SelectedItem != null && !String.IsNullOrWhiteSpace(RecNameTextBox.Text) && RecDateRegDatePicker.SelectedDate != null && !String.IsNullOrWhiteSpace(RecPhoneNumberTextBox.Text) && !String.IsNullOrWhiteSpace(RecEmailTextBox.Text) && !String.IsNullOrWhiteSpace(RecInnTextBox.Text) && !String.IsNullOrWhiteSpace(RecOgrnTextBox.Text) && !String.IsNullOrWhiteSpace(RecFIOTextBox.Text) && !String.IsNullOrWhiteSpace(RecAreaTextBox.Text) && !String.IsNullOrWhiteSpace(RecCityTextBox.Text) && !String.IsNullOrWhiteSpace(RecStreetTextBox.Text) && !String.IsNullOrWhiteSpace(RecHouseTextBox.Text) && SenTypeOrgComboBox.SelectedItem != null && !String.IsNullOrWhiteSpace(SenNameTextBox.Text) && SenDateRegDatePicker.SelectedDate != null && !String.IsNullOrWhiteSpace(SenPhoneNumberTextBox.Text) && !String.IsNullOrWhiteSpace(SenEmailTextBox.Text) && !String.IsNullOrWhiteSpace(SenInnTextBox.Text) && !String.IsNullOrWhiteSpace(SenOgrnTextBox.Text) && !String.IsNullOrWhiteSpace(SenFIOTextBox.Text) && !String.IsNullOrWhiteSpace(SenAreaTextBox.Text) && !String.IsNullOrWhiteSpace(SenCityTextBox.Text) && !String.IsNullOrWhiteSpace(SenStreetTextBox.Text) && !String.IsNullOrWhiteSpace(SenHouseTextBox.Text) && !String.IsNullOrWhiteSpace(SenWarAreaTextBox.Text) && !String.IsNullOrWhiteSpace(SenWarCityTextBox.Text) && !String.IsNullOrWhiteSpace(SenWarStreetTextBox.Text) && !String.IsNullOrWhiteSpace(SenWarHouseTextBox.Text) && !String.IsNullOrWhiteSpace(RecWarAreaTextBox.Text) && !String.IsNullOrWhiteSpace(RecWarCityTextBox.Text) && !String.IsNullOrWhiteSpace(RecWarStreetTextBox.Text) && !String.IsNullOrWhiteSpace(RecWarHouseTextBox.Text))
            {
                CurrentDocument.IdRecipient = RecCurrentOrganization.Id;
                CurrentDocument.IdSener = SenCurrentOrganization.Id;
                CurrentDocument.IdWarRecipient = RecCurrentWarehouse.Id;
                CurrentDocument.IdWarSender = SenCurrentWarehouse.Id;
                CurrentDocument.Date = DateTime.Today;
                AppData.Context.SaveChanges();
                NavigationService.GoBack();
                MessageBox.Show("Накладная успешно зарегистрирована!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
