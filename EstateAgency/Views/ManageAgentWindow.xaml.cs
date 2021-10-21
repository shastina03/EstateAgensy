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
    /// Логика взаимодействия для ManageAgentWindow.xaml
    /// </summary>
    public partial class ManageAgentWindow : Window
    {
        public ManageAgentWindow()
        {
            InitializeComponent();
            LbAgents.ItemsSource = App.Context.PersonSet_Agent.ToList();
            LbAgents.SelectedIndex = 0;
        }

        private void LbAgents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curAgent = LbAgents.SelectedItem as PersonSet_Agent;
            if(curAgent != null)
            {
                TbFirstName.Text = curAgent.PersonSet.FirstName;
                TbMiddleName.Text = curAgent.PersonSet.MiddleName;
                TbLastName.Text = curAgent.PersonSet.LastName;
                TbDealShare.Text = curAgent.DealShare.ToString();
                DgDemands.ItemsSource = curAgent.DemandSets.ToList();
                DgSupplies.ItemsSource = curAgent.SupplySets.ToList();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            App.Context.PersonSet_Agent.Remove(LbAgents.SelectedItem as PersonSet_Agent);
            App.Context.SaveChanges();
            Update();
        }
        private void Update()
        {
            LbAgents.ItemsSource = App.Context.PersonSet_Agent.ToList();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            Window window = new CreateAgentOrClientWindow(PersonType.Agent);
            window.Closed += (s, ev) => Update();
            window.Show();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(BtnEdit.Content as string == "Edit")
            {
                BtnEdit.Content = "Save";
                foreach (var controll in SpEdit.Children)
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

                var curAgent = App.Context.PersonSet_Agent.Find((LbAgents.SelectedItem as PersonSet_Agent).Id);
                curAgent.DealShare = int.Parse(TbDealShare.Text);
                curAgent.PersonSet.LastName = TbLastName.Text;
                curAgent.PersonSet.MiddleName = TbMiddleName.Text; 
                curAgent.PersonSet.FirstName = TbFirstName.Text;
                App.Context.SaveChanges();
                Update();
            }
        }
    }
}
