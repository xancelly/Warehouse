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
    /// Логика взаимодействия для WarehousePage.xaml
    /// </summary>
    public partial class WarehousePage : Page
    {
        public WarehousePage()
        {
            InitializeComponent();
            WarehouseDataGrid.ItemsSource = AppData.Context.WarehouseGood.Where(c => c.Warehouse.Organization.Name.ToLower().Contains("ТТЛ".ToLower())).ToList();
        }

        public void UpdateWarehouse()
        {
            var CurrentWarehouse = AppData.Context.WarehouseGood.ToList();
            CurrentWarehouse = CurrentWarehouse.Where(c => c.Warehouse.Organization.Name.ToLower().Contains("ТТЛ".ToLower()) && c.Party.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            WarehouseDataGrid.ItemsSource = CurrentWarehouse;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateWarehouse();
        }
    }
}
