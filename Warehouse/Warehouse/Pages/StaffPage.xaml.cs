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
    /// Логика взаимодействия для StaffPage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        public StaffPage()
        {
            InitializeComponent();
            StaffDataGrid.ItemsSource = AppData.Context.Staff.ToList();
        }

        public void UpdateStaff()
        {
            var CurrentStaff = AppData.Context.Staff.ToList();
            CurrentStaff = CurrentStaff.Where(c => c.LastName.ToLower().Contains(SearchTextBox.Text.ToLower()) || c.FirstName.ToLower().Contains(SearchTextBox.Text.ToLower()) || c.MiddleName.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            StaffDataGrid.ItemsSource = CurrentStaff;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateStaff();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Staff CurrentStaff = StaffDataGrid.SelectedItem as Staff;
            if (CurrentStaff != null)
            {
                NavigationService.Navigate(new RegistrationPage(CurrentStaff));
            }
            else
            {
                MessageBox.Show("Выберите сотрудника!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Staff CurrentStaff = StaffDataGrid.SelectedItem as Staff;
            if (CurrentStaff != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить агента?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AppData.Context.Staff.Remove(CurrentStaff);
                    AppData.Context.SaveChanges();
                    Page_Loaded(null, null);
                    MessageBox.Show("Сотрудник был удалён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStaff();
        }
    }
}
