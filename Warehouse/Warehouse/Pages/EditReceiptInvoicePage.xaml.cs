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
    /// Логика взаимодействия для EditReceiptInvoicePage.xaml
    /// </summary>
    public partial class EditReceiptInvoicePage : Page
    {
        public EditReceiptInvoicePage()
        {
            InitializeComponent();
            SenTypeOrgComboBox.ItemsSource = AppData.Context.TypeOrganization.ToList();
            RecTypeOrgComboBox.ItemsSource = AppData.Context.TypeOrganization.ToList();
        }

        private void SenNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SenNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var CurrentSenOrganisation = AppData.Context.Organization.Where(c => c.Name.ToLower().Contains(SenNameTextBox.Text.ToLower())).FirstOrDefault();
            if (CurrentSenOrganisation != null)
            {
                if (MessageBox.Show("Организация с таким названием уже есть в базе.\nЖелаете воспользоваться автозаполнением?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    SenTypeOrgComboBox.SelectedItem = CurrentSenOrganisation.TypeOrganization;
                    SenNameTextBox.Text = CurrentSenOrganisation.Name;
                    SenDateRegDatePicker.SelectedDate = CurrentSenOrganisation.DateRegistration;
                    SenPhoneNumberTextBox.Text = CurrentSenOrganisation.PhoneNumber;
                    SenEmailTextBox.Text = CurrentSenOrganisation.Email;
                    SenInnTextBox.Text = CurrentSenOrganisation.INN;
                    SenOgrnTextBox.Text = CurrentSenOrganisation.OGRN;
                    SenFIOTextBox.Text = CurrentSenOrganisation.FullnameDirector;

                    SenAreaTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentSenOrganisation.IdAddress).Select(c => c.Region).FirstOrDefault();
                    SenCityTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentSenOrganisation.IdAddress).Select(c => c.City).FirstOrDefault();
                    SenStreetTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentSenOrganisation.IdAddress).Select(c => c.Street).FirstOrDefault();
                    SenHouseTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentSenOrganisation.IdAddress).Select(c => c.House).FirstOrDefault();
                }
            }
        }

        private void RecNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var CurrentRecOrganisation = AppData.Context.Organization.Where(c => c.Name.ToLower().Contains(RecNameTextBox.Text.ToLower())).FirstOrDefault();
            if (CurrentRecOrganisation != null)
            {
                if (MessageBox.Show("Организация с таким названием уже есть в базе.\nЖелаете воспользоваться автозаполнением?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    RecTypeOrgComboBox.SelectedItem = CurrentRecOrganisation.TypeOrganization;
                    RecNameTextBox.Text = CurrentRecOrganisation.Name;
                    RecDateRegDatePicker.SelectedDate = CurrentRecOrganisation.DateRegistration;
                    RecPhoneNumberTextBox.Text = CurrentRecOrganisation.PhoneNumber;
                    RecEmailTextBox.Text = CurrentRecOrganisation.Email;
                    RecInnTextBox.Text = CurrentRecOrganisation.INN;
                    RecOgrnTextBox.Text = CurrentRecOrganisation.OGRN;
                    RecFIOTextBox.Text = CurrentRecOrganisation.FullnameDirector;

                    RecAreaTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentRecOrganisation.IdAddress).Select(c => c.Region).FirstOrDefault();
                    RecCityTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentRecOrganisation.IdAddress).Select(c => c.City).FirstOrDefault();
                    RecStreetTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentRecOrganisation.IdAddress).Select(c => c.Street).FirstOrDefault();
                    RecHouseTextBox.Text = AppData.Context.Address.Where(c => c.Id == CurrentRecOrganisation.IdAddress).Select(c => c.House).FirstOrDefault();
                }
            }
        }
    }
}
