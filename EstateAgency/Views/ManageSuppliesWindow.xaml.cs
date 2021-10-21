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
    /// Логика взаимодействия для ManageSuppliesWindow.xaml
    /// </summary>
    public partial class ManageSuppliesWindow : Window
    {
        public ManageSuppliesWindow()
        {
            InitializeComponent();
            
            CbAgent.ItemsSource = App.Context.PersonSet_Agent.ToList(); 
            CbClient.ItemsSource = App.Context.PersonSet_Client.ToList();
            Update();
            DgSupplies.SelectedIndex = 0;
        }
        private void Update()
        {
            var supplies = App.Context.SupplySets.ToList();
            if (CbClient.SelectedItem != null)
            {
                supplies = supplies.Where(p => p.ClientId == (CbClient.SelectedItem as PersonSet_Client).Id).ToList();
            }
            if (CbAgent.SelectedItem != null)
            {
                supplies = supplies.Where(p => p.AgentId == (CbAgent.SelectedItem as PersonSet_Agent).Id).ToList();
            }
            if(CbRealState.SelectedItem != null)
            {
                supplies = supplies.Where(p => p.RealEstateSet.Type == (CbRealState.SelectedItem as ComboBoxItem).Content.ToString() || (CbRealState.SelectedItem as ComboBoxItem).Content.ToString() == "Not select").ToList();
            }
            if (TbMinPrice.Text != "")
            {
                supplies = supplies.Where(p => p.Price >= long.Parse(TbMinPrice.Text)).ToList();
            }
            if (TbMaxPrice.Text != "")
            {
                supplies = supplies.Where(p => p.Price <= long.Parse(TbMaxPrice.Text)).ToList();
            }
            if (TbCityFilt.Text != "")
            {
                supplies = supplies.Where(p => 4 > SearchEngine.EditDistance(p.RealEstateSet.Address_City, TbCityFilt.Text)).ToList();
            }
            if (TbStreetFilt.Text != "")
            {
                supplies = supplies.Where(p => 4 > SearchEngine.EditDistance(p.RealEstateSet.Address_Street, TbStreetFilt.Text)).ToList();
            }
            if (TbNumberFilt.Text != "")
            {
                supplies = supplies.Where(p => 3 > SearchEngine.EditDistance(p.RealEstateSet.Address_Number, TbNumberFilt.Text)).ToList();
            }
            if (TbHouseFilt.Text != "")
            {
                supplies = supplies.Where(p => 3 > SearchEngine.EditDistance(p.RealEstateSet.Address_House, TbHouseFilt.Text)).ToList();
            }

            DgSupplies.ItemsSource = supplies;
        }
        private void DgSupplies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curSupp = (sender as DataGrid).SelectedItem as SupplySet;
            if (curSupp != null)
            {
                if (curSupp.RealEstateSet.RealEstateSet_Apartment != null)
                {
                    SpHouse.Visibility = Visibility.Collapsed;
                    SpAppart.Visibility = Visibility.Visible;
                    TbTotalArea.Text = curSupp.RealEstateSet.RealEstateSet_Apartment.TotalArea.ToString();
                    TbRooms.Text = curSupp.RealEstateSet.RealEstateSet_Apartment.Rooms.ToString();
                    TbFloor.Text = curSupp.RealEstateSet.RealEstateSet_Apartment.Floor.ToString();
                }
                else if (curSupp.RealEstateSet.RealEstateSet_House != null)
                {
                    SpHouse.Visibility = Visibility.Visible;
                    SpAppart.Visibility = Visibility.Collapsed;
                    TbTotalArea.Text = curSupp.RealEstateSet.RealEstateSet_House.TotalArea.ToString();
                    TbFloors.Text = curSupp.RealEstateSet.RealEstateSet_House.TotalFloors.ToString();
                }
                else
                {
                    TbTotalArea.Text = curSupp.RealEstateSet.RealEstateSet_Land.TotalArea.ToString();
                }
                TbAgent.Text = curSupp.PersonSet_Agent != null ? curSupp.PersonSet_Agent.PersonSet.FullName : string.Empty;
                TbClient.Text = curSupp.PersonSet_Client.PersonSet.FullName;
                TbPrice.Text = curSupp.Price.ToString();
                TbRealEstate.Text = curSupp.RealEstateSet.Info;
                TbCity.Text = curSupp.RealEstateSet.Address_City;
                TbHouse.Text = curSupp.RealEstateSet.Address_House;
                TbStreet.Text = curSupp.RealEstateSet.Address_Street;
                TbNumber.Text = curSupp.RealEstateSet.Address_Number;
                TbLongtitude.Text = curSupp.RealEstateSet.Coordinate_longitude.ToString();
                TbLatitude.Text = curSupp.RealEstateSet.Coordinate_latitude.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = new CreateSupplieWindow();
            window.Closed += (s, ev) => Update();
            window.Show();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.Context.SupplySets.Remove(DgSupplies.SelectedItem as SupplySet);
                App.Context.SaveChanges();
                Update();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось удалить предложение так как присутствую связанные с ним записи в других таблицах");
            }
        }

        private void CbClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void CbAgent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void CbRealState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void BtnFilt_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
