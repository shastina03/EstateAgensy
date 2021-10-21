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
    /// Логика взаимодействия для ManageDemandWindow.xaml
    /// </summary>
    public partial class ManageDemandWindow : Window
    {
        public ManageDemandWindow()
        {
            InitializeComponent();
            CbAgent.ItemsSource = App.Context.PersonSet_Agent.ToList();
            CbClient.ItemsSource = App.Context.PersonSet_Client.ToList();
            CbRealState.SelectedIndex = 0;
            Update();
        }

        private void Update()
        {
            var demands = App.Context.DemandSets.ToList();
            if (CbClient.SelectedItem != null)
            {
                demands = demands.Where(p => p.ClientId == (CbClient.SelectedItem as PersonSet_Client).Id).ToList();
            }
            if (CbAgent.SelectedItem != null)
            {
                demands = demands.Where(p => p.AgentId == (CbAgent.SelectedItem as PersonSet_Agent).Id).ToList();
            }
            if (CbRealState.SelectedItem != null)
            {
                demands = demands.Where(p => p.RealEstateFilterSet.Type == (CbRealState.SelectedItem as ComboBoxItem).Content.ToString() || (CbRealState.SelectedItem as ComboBoxItem).Content.ToString() == "Not select").ToList();
            }
            if (TbMinPrice.Text != "")
            {
                demands = demands.Where(p => p.MinPrice >= long.Parse(TbMinPrice.Text)).ToList();
            }
            if (TbMaxPrice.Text != "")
            {
                demands = demands.Where(p => p.MaxPrice <= long.Parse(TbMaxPrice.Text)).ToList();
            }
            if (TbCityFilt.Text != "")
            {
                demands = demands.Where(p => 4 > SearchEngine.EditDistance(p.Address_City, TbCityFilt.Text)).ToList();
            }
            if (TbStreetFilt.Text != "")
            {
                demands = demands.Where(p => 4 > SearchEngine.EditDistance(p.Address_Street, TbStreetFilt.Text)).ToList();
            }
            if (TbNumberFilt.Text != "")
            {
                demands = demands.Where(p => 3 > SearchEngine.EditDistance(p.Address_Number, TbNumberFilt.Text)).ToList();
            }
            if (TbHouseFilt.Text != "")
            {
                demands = demands.Where(p => 3 > SearchEngine.EditDistance(p.Address_House, TbHouseFilt.Text)).ToList();
            }

            DgDemands.ItemsSource = demands;
        }

        private void CbAgent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void CbClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void DgSupplies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgDemands.SelectedItem is DemandSet curDemand)
            {
                TbAgent.Text = curDemand.PersonSet_Agent?.PersonSet.FullName ?? "";
                TbClient.Text = curDemand.PersonSet_Client?.PersonSet.FullName ?? "";
                TbMaxPrice.Text = curDemand.MaxPrice.ToString();
                TbMinPrice.Text = curDemand.MinPrice.ToString();
                TbRealEstate.Text = curDemand.RealEstateFilterSet.Type;
                TbCity.Text = curDemand.Address_City;
                TbHouse.Text = curDemand.Address_House;
                TbNumber.Text = curDemand.Address_Number;
                TbStreet.Text = curDemand.Address_Street;
                if (curDemand.RealEstateFilterSet.TypeEstate == EstateType.Apartment)
                {
                    SpApartment.Visibility = Visibility.Visible;
                    SpHouse.Visibility = Visibility.Collapsed;
                    TbMinFloor.Text = curDemand.RealEstateFilterSet.RealEstateFilterSet_ApartmentFilter.MinFloor.ToString();
                    TbMaxFloor.Text = curDemand.RealEstateFilterSet.RealEstateFilterSet_ApartmentFilter.MaxFloor.ToString();
                    TbMinRooms.Text = curDemand.RealEstateFilterSet.RealEstateFilterSet_ApartmentFilter.MinRooms.ToString();
                    TbMaxRooms.Text = curDemand.RealEstateFilterSet.RealEstateFilterSet_ApartmentFilter.MaxRooms.ToString();
                    TbMinTotalArea.Text = curDemand.RealEstateFilterSet.RealEstateFilterSet_ApartmentFilter.MinArea.ToString();
                    TbMaxTotalArea.Text = curDemand.RealEstateFilterSet.RealEstateFilterSet_ApartmentFilter.MaxArea.ToString();

                }
                else if (curDemand.RealEstateFilterSet.TypeEstate == EstateType.House)
                {
                    SpApartment.Visibility = Visibility.Collapsed;
                    SpHouse.Visibility = Visibility.Visible;
                    TbMinFloors.Text = curDemand.RealEstateFilterSet.RealEstateFilterSet_HouseFilter.MinFloors.ToString();
                    TbMaxFloors.Text = curDemand.RealEstateFilterSet.RealEstateFilterSet_HouseFilter.MaxFloors.ToString();
                    TbMinTotalArea.Text = curDemand.RealEstateFilterSet.RealEstateFilterSet_HouseFilter.MinArea.ToString();
                    TbMaxTotalArea.Text = curDemand.RealEstateFilterSet.RealEstateFilterSet_HouseFilter.MaxArea.ToString();
                }
                else
                {
                    SpApartment.Visibility = Visibility.Visible;
                    SpApartment.Visibility = Visibility.Collapsed;
                    TbMinTotalArea.Text = curDemand.RealEstateFilterSet.RealEstateFilterSet_LandFilter.MinArea.ToString();
                    TbMaxTotalArea.Text = curDemand.RealEstateFilterSet.RealEstateFilterSet_LandFilter.MaxArea.ToString();
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.Context.DemandSets.Remove(DgDemands.SelectedItem as DemandSet);
                App.Context.SaveChanges();
                Update();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось удалить запись так как присутствуют связанные с ней записи в других таблицах");
            }

        }
    }
}
