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
    /// Логика взаимодействия для ManageRealEstateWindow.xaml
    /// </summary>
    public partial class ManageRealEstateWindow : Window
    {
        
        public ManageRealEstateWindow()
        {
            InitializeComponent();
            Update();
            LbRealEstate.SelectedIndex = 0;
        }
        private void Update()
        {
            LbRealEstate.ItemsSource = App.Context.RealEstateSets.ToList();
        }
        private void BtnNewApartment_Click(object sender, RoutedEventArgs e)
        {
            Window window = new CreateRealEstateWindow(EstateType.Apartment);
            window.Closed += (s, ev) => Update();
            window.Show();
        }

        private void BtnNewHouse_Click(object sender, RoutedEventArgs e)
        {
            Window window = new CreateRealEstateWindow(EstateType.House);
            window.Closed += (s, ev) => Update();
            window.Show();
        }

        private void BtnNewLand_Click(object sender, RoutedEventArgs e)
        {
            Window window = new CreateRealEstateWindow(EstateType.Land);
            window.Closed += (s, ev) => Update();
            window.Show();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            App.Context.RealEstateSets.Remove(LbRealEstate.SelectedItem as RealEstateSet);
            App.Context.SaveChanges();
            Update();
        }

        private void LbRealEstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curRealEstate = LbRealEstate.SelectedItem as RealEstateSet;
            if (curRealEstate != null)
            {
                if (curRealEstate.RealEstateSet_Apartment != null)
                {
                    SpRoomInfo.Visibility = Visibility.Visible;
                    SpFloorInfo.Visibility = Visibility.Visible;
                    SpFloorsInfo.Visibility = Visibility.Collapsed;
                    TbTotalArea.Text = curRealEstate.RealEstateSet_Apartment.TotalArea.ToString();
                    TbRooms.Text = curRealEstate.RealEstateSet_Apartment.Rooms.ToString();
                    TbFloor.Text = curRealEstate.RealEstateSet_Apartment.Floor.ToString();
                }
                else if ((LbRealEstate.SelectedItem as RealEstateSet).RealEstateSet_House != null)
                {
                    SpRoomInfo.Visibility = Visibility.Collapsed;
                    SpFloorInfo.Visibility = Visibility.Collapsed;
                    SpFloorsInfo.Visibility = Visibility.Visible; 
                    TbTotalArea.Text = curRealEstate.RealEstateSet_House.TotalArea.ToString();
                    TbFloors.Text = curRealEstate.RealEstateSet_House.TotalFloors.ToString();
                }
                else
                {
                    SpRoomInfo.Visibility = Visibility.Collapsed;
                    SpFloorInfo.Visibility = Visibility.Collapsed;
                    SpFloorsInfo.Visibility = Visibility.Collapsed; 
                    TbTotalArea.Text = curRealEstate.RealEstateSet_Land.TotalArea.ToString();
                }
                TbCity.Text = curRealEstate.Address_City;
                TbStreet.Text = curRealEstate.Address_Street;
                TbHouse.Text = curRealEstate.Address_House;
                TbNumber.Text = curRealEstate.Address_Number;
                TbLongTitude.Text = curRealEstate.Coordinate_longitude.ToString();
                TbLatitude.Text = curRealEstate.Coordinate_latitude.ToString();
            }
        }
    }
}
