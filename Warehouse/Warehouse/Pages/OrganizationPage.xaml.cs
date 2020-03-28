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
    /// Логика взаимодействия для OrganizationPage.xaml
    /// </summary>
    public partial class OrganizationPage : Page
    {
        public OrganizationPage()
        {
            InitializeComponent();
            OrganizationDataGrid.ItemsSource = AppData.Context.Organization.ToList();
        }

        public void UpdateOrganization()
        {
            var CurrentOrganization = AppData.Context.Organization.ToList();
            CurrentOrganization = CurrentOrganization.Where(c => c.LastName.ToLower().Contains(SearchTextBox.Text.ToLower()) || c.FirstName.ToLower().Contains(SearchTextBox.Text.ToLower()) || c.NameOrg.ToLower().Contains(SearchTextBox.Text.ToLower()) || c.MiddleName.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            OrganizationDataGrid.ItemsSource = CurrentOrganization;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateOrganization();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOrganization();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditOrganization(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Organization CurrentOrganization = OrganizationDataGrid.SelectedItem as Organization;
            if (CurrentOrganization != null)
            {
                NavigationService.Navigate(new EditOrganization(CurrentOrganization));
            }
            else
            {
                MessageBox.Show("Выберите сотрудника!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Organization CurrentOrganization = OrganizationDataGrid.SelectedItem as Organization;
            if (CurrentOrganization != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить организацию?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AppData.Context.Organization.Remove(CurrentOrganization);
                    AppData.Context.SaveChanges();
                    Page_Loaded(null, null);
                    MessageBox.Show("Организация была удалена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
