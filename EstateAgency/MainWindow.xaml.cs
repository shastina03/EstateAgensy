using EstateAgency.Views;
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

namespace EstateAgency
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAgent_Click(object sender, RoutedEventArgs e)
        {
            var agentWindow = new ManageAgentWindow();
            agentWindow.Show();
        }

        private void BtnClient_Click(object sender, RoutedEventArgs e)
        {
            var clientWindow = new ManageClientWindow();
            clientWindow.Show();
        }

        private void BtnRealEstate_Click(object sender, RoutedEventArgs e)
        {
            var realEstateWindow = new ManageRealEstateWindow();
            realEstateWindow.Show();
        }

        private void BtnSupplies_Click(object sender, RoutedEventArgs e)
        {
            var supliesWindow = new ManageSuppliesWindow();
            supliesWindow.Show();
        }

        private void BtnDemands_Click(object sender, RoutedEventArgs e)
        {
            var demandWindow = new ManageDemandWindow();
            demandWindow.Show();
        }
    }
}
