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
    /// Логика взаимодействия для CreateSupplieWindow.xaml
    /// </summary>
    public partial class CreateSupplieWindow : Window
    {
        public CreateSupplieWindow()
        {
            InitializeComponent();
            CbClient.ItemsSource = App.Context.PersonSet_Client.ToList();
            CbAgent.ItemsSource = App.Context.PersonSet_Agent.ToList();
            CbRealState.ItemsSource = App.Context.RealEstateSets.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var newSup = new SupplySet()
            {
                AgentId = (CbAgent.SelectedItem as PersonSet_Agent).Id,
                ClientId = (CbClient.SelectedItem as PersonSet_Client).Id,
                RealEstateId = (CbRealState.SelectedItem as RealEstateSet).Id,
                Price = long.Parse(TbPrice.Text)
            };
            App.Context.SupplySets.Add(newSup);
            App.Context.SaveChanges();
            Close();
        }
    }
}
