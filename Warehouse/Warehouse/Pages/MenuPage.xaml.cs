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

namespace Warehouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void StaffButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaffPage());
        }

        private void OrganizationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrganizationPage());
        }

        private void GoodsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GoodsPage());
        }

        private void WarehouseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReceiptInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditReceiptInvoicePage());
        }
    }
}
