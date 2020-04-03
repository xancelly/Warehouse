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
    /// Логика взаимодействия для ExpenceInvoicePage.xaml
    /// </summary>
    public partial class ExpenceInvoicePage : Page
    {
        public ExpenceInvoicePage()
        {
            InitializeComponent();
            ExpenceInvoiceDataGrid.ItemsSource = AppData.Context.Document.Where(c => c.IdTypeDocument == 2).ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditExpenceInvoicePage());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
