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
    /// Логика взаимодействия для GoodsPage.xaml
    /// </summary>
    public partial class GoodsPage : Page
    {
        public GoodsPage()
        {
            InitializeComponent();
            GoodsDataGrid.ItemsSource = AppData.Context.Good.ToList();
        }

        public void UpdateGoods()
        {
            var CurrentGood = AppData.Context.Good.ToList();
            CurrentGood = CurrentGood.Where(c => c.Manufacturer.NameMan.ToLower().Contains(SearchTextBox.Text.ToLower()) || c.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            GoodsDataGrid.ItemsSource = CurrentGood;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGoods();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGoods();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditGoodsPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Good CurrentGood = GoodsDataGrid.SelectedItem as Good;
            if (CurrentGood != null)
            {
                NavigationService.Navigate(new EditGoodsPage(CurrentGood));
            }
            else
            {
                MessageBox.Show("Выберите товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Good CurrentGood = GoodsDataGrid.SelectedItem as Good;
            if (CurrentGood != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить товар?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AppData.Context.Good.Remove(CurrentGood);
                    AppData.Context.SaveChanges();
                    Page_Loaded(null, null);
                    MessageBox.Show("Товар был удалён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
