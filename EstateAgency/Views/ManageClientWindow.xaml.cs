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
using System.Windows.Shapes;

namespace EstateAgency.Views
{
    /// <summary>
    /// Логика взаимодействия для ManageClientWindow.xaml
    /// </summary>
    public partial class ManageClientWindow : Window
    {
        public ManageClientWindow()
        {
            InitializeComponent();
            LbClients.ItemsSource = App.Context.PersonSet_Client.ToList();
            LbClients.SelectedIndex = 0;
        }

        private void LbClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currClient = LbClients.SelectedItem as PersonSet_Client;
            if (currClient != null)
            {
                TbFirstName.Text = currClient.PersonSet.FirstName;
                TbMiddleName.Text = currClient.PersonSet.MiddleName;
                TbLastName.Text = currClient.PersonSet.LastName;
                TbEmail.Text = currClient.Email;
                TbPhone.Text = currClient.Phone;
                DgDemands.ItemsSource = currClient.DemandSets.ToList();
                DgSupplies.ItemsSource = currClient.SupplySets.ToList();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.Context.PersonSet_Client.Remove(LbClients.SelectedItem as PersonSet_Client);
                App.Context.SaveChanges();
                Update();
            }
            catch (Exception)
            {

                MessageBox.Show("Не удалось удалить клиента так как присутствую связанные с ним записи в других таблицах");
            }
        }
        private void Update()
        {
            LbClients.ItemsSource = App.Context.PersonSet_Client.ToList();
        }
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            Window window = new CreateAgentOrClientWindow(PersonType.Client);
            window.Closed += (s, ev) => Update();
            window.Show();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            
            if (BtnEdit.Content as string == "Edit")
            {
                BtnEdit.Content = "Save";
                foreach (object controll in SpEdit.Children)
                {
                    if (controll is TextBox)
                        (controll as TextBox).IsReadOnly = false;
                }
            }
            else
            {
                BtnEdit.Content = "Edit";
                foreach (var controll in SpEdit.Children)
                {
                    if (controll is TextBox)
                       (controll as TextBox).IsReadOnly = true;
                }

                PersonSet_Client curClient = App.Context.PersonSet_Client.Find((LbClients.SelectedItem as PersonSet_Client).Id);
                curClient.Email = TbEmail.Text;
                curClient.Phone = TbPhone.Text;
                curClient.PersonSet.LastName = TbLastName.Text;
                curClient.PersonSet.MiddleName = TbMiddleName.Text;
                curClient.PersonSet.FirstName = TbFirstName.Text;
                App.Context.SaveChanges();
                Update();
            }
        }
    }
}
