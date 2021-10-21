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
    /// Логика взаимодействия для CreateAgentOrClient.xaml
    /// </summary>
    public partial class CreateAgentOrClientWindow : Window
    {
        PersonType winType;
        public CreateAgentOrClientWindow(PersonType type)
        {
            InitializeComponent();
            winType = type;
            if (type == PersonType.Agent)
                SpClient.Visibility = Visibility.Collapsed;
            else
                SpAgent.Visibility = Visibility.Collapsed;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (winType == PersonType.Agent)
            {
                var agent = new PersonSet_Agent
                {
                    DealShare = int.Parse(TbDealShare.Text),
                    PersonSet = new PersonSet()
                };
                agent.PersonSet.FirstName = TbFirstName.Text; 
                agent.PersonSet.MiddleName = TbMiddleName.Text;
                agent.PersonSet.LastName = TbLastName.Text;
                App.Context.PersonSet_Agent.Add(agent);
                App.Context.SaveChanges();
            }
            else
            {
                var client = new PersonSet_Client
                {
                    Email = TbEmail.Text,
                    Phone = TbEmail.Text,
                    PersonSet = new PersonSet()
                };
                client.PersonSet.FirstName = TbFirstName.Text;
                client.PersonSet.MiddleName = TbMiddleName.Text;
                client.PersonSet.LastName = TbLastName.Text;
                App.Context.PersonSet_Client.Add(client);
                App.Context.SaveChanges();
            }
            Close();
        }
    }
}
