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
    /// Логика взаимодействия для DocumentPage.xaml
    /// </summary>
    public partial class ReceiptInvoicePage : Page
    {
        public ReceiptInvoicePage()
        {
            InitializeComponent();
            ReceiptInvoiceDataGrid.ItemsSource = AppData.Context.Document.Where(c => c.IdTypeDocument == 1).ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditReceiptInvoicePage());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Document CurrentDocument = ReceiptInvoiceDataGrid.SelectedItem as Document;
            if (CurrentDocument != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить накладную?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var CountGood = AppData.Context.DocumentGood.Where(c => c.IdDocument == CurrentDocument.Id).Count();
                    for (int i = 0; i < CountGood; i++)
                    {
                        DocumentGood CurrentDocumentGood = AppData.Context.DocumentGood.Where(c => c.IdDocument == CurrentDocument.Id).FirstOrDefault();
                        AppData.Context.DocumentGood.Remove(CurrentDocumentGood);
                        AppData.Context.SaveChanges();
                    }
                    AppData.Context.Document.Remove(CurrentDocument);
                    AppData.Context.SaveChanges();
                    ReceiptInvoiceDataGrid.ItemsSource = AppData.Context.Document.Where(c => c.IdTypeDocument == 1).ToList();
                    MessageBox.Show("Накладная была удалена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите накладную!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
