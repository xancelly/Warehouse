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
using Warehouse.Entities;

namespace Warehouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditGoodsPage.xaml
    /// </summary>
    public partial class EditGoodsPage : Page
    {
        Good CurrentGood = null;
        public EditGoodsPage(Good good)
        {
            InitializeComponent();
            ManufacturerComboBox.ItemsSource = AppData.Context.Manufacturer.ToList();
            GroupGoodComboBox.ItemsSource = AppData.Context.GroupGood.ToList();
            UnitComboBox.ItemsSource = AppData.Context.Unit.ToList();
            TypeOrgComboBox.ItemsSource = AppData.Context.TypeOrganization.ToList();
            CountryComboBox.ItemsSource = AppData.Context.Country.ToList();

            CurrentGood = good;
            if (CurrentGood != null)
            {
                SaveButton.Content = "Изменить";
                ManufacturerComboBox.SelectedItem = CurrentGood.Manufacturer;
                GroupGoodComboBox.SelectedItem = CurrentGood.GroupGood;
                NameTextBox.Text = CurrentGood.Name;
                UnitComboBox.SelectedItem = CurrentGood.Unit;
                ShelfLifeDatePicker.SelectedDate = CurrentGood.ShelfLife;
            }

            }

        private void AddManButton_Click(object sender, RoutedEventArgs e)
        {
            if (TypeOrgComboBox.SelectedItem != null && !String.IsNullOrWhiteSpace(NameManTextBox.Text) && CountryComboBox.SelectedItem != null)
            {
                if (AppData.Context.Manufacturer.Where(c => c.Name == NameManTextBox.Text).FirstOrDefault() == null)
                {
                    Manufacturer CurrentManufacturer = new Manufacturer()
                    {
                        TypeOrganization = TypeOrgComboBox.SelectedItem as TypeOrganization,
                        Name = NameManTextBox.Text,
                        Country = CountryComboBox.SelectedItem as Country,
                    };
                    AppData.Context.Manufacturer.Add(CurrentManufacturer);
                    MessageBox.Show("Производитель успешно добавлен!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppData.Context.SaveChanges();
                    TypeOrgComboBox.SelectedItem = null;
                    NameManTextBox.Text = "";
                    CountryComboBox.SelectedItem = null;
                    ManufacturerComboBox.ItemsSource = AppData.Context.Manufacturer.ToList();
                }
                else
                {
                    MessageBox.Show("Производитель с таким названием уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    NameTextBox.Focus();
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ManufacturerComboBox.SelectedItem != null && !String.IsNullOrWhiteSpace(NameTextBox.Text) && GroupGoodComboBox.SelectedItem != null && ShelfLifeDatePicker.SelectedDate != null)
            {
                if (ShelfLifeDatePicker.SelectedDate < DateTime.Today)
                {
                    if (CurrentGood == null)
                    {
                        if (AppData.Context.Good.Where(c => c.Name == NameTextBox.Text).FirstOrDefault() == null)
                        {
                            CurrentGood = new Good()
                            {
                                Manufacturer = ManufacturerComboBox.SelectedItem as Manufacturer,
                                GroupGood = GroupGoodComboBox.SelectedItem as GroupGood,
                                Name = NameTextBox.Text,
                                Unit = UnitComboBox.SelectedItem as Unit,
                                ShelfLife = ShelfLifeDatePicker.SelectedDate
                            };
                            AppData.Context.Good.Add(CurrentGood);
                            MessageBox.Show("Товар успешно добавлен!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppData.Context.SaveChanges();
                            NavigationService.GoBack();
                        }
                        else
                        {
                            MessageBox.Show("Товар с таким наименованием уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        CurrentGood.Manufacturer = ManufacturerComboBox.SelectedItem as Manufacturer;
                        CurrentGood.GroupGood = GroupGoodComboBox.SelectedItem as GroupGood;
                        CurrentGood.Name = NameTextBox.Text;
                        CurrentGood.Unit = UnitComboBox.SelectedItem as Unit;
                        CurrentGood.ShelfLife = ShelfLifeDatePicker.SelectedDate;
                        MessageBox.Show("Информация обновлена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppData.Context.SaveChanges();
                        NavigationService.GoBack();
                    }
                }
                else
                {
                    MessageBox.Show("Дата введена некорректно или срок годности подошёл к концу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
