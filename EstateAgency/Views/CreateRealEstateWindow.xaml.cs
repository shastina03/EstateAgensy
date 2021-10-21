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
    /// Логика взаимодействия для CreateRealEstate.xaml
    /// </summary>
    public partial class CreateRealEstateWindow : Window
    {
        EstateType newEstateType;
        public CreateRealEstateWindow(EstateType type)
        {
            InitializeComponent();
            newEstateType = type;
            if(newEstateType == EstateType.Apartment)
            {
                SpHouse.Visibility = Visibility.Collapsed;
            }
            else if (newEstateType == EstateType.House)
            {
                SpApartment.Visibility = Visibility.Collapsed;
            }
            else
            {
                SpApartment.Visibility = Visibility.Collapsed; 
                SpHouse.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var newEstate = new RealEstateSet();
            if (newEstateType == EstateType.Apartment)
            {
                newEstate.RealEstateSet_Apartment = new RealEstateSet_Apartment
                {
                    Floor = int.Parse(TbFloor.Text),
                    Rooms = int.Parse(TbRooms.Text),
                    TotalArea = double.Parse(TbTotalArea.Text)
                };
                newEstate.Address_City = TbCity.Text;
                newEstate.Address_Street = TbStreet.Text;
                newEstate.Address_Number = TbNumber.Text;
                newEstate.Address_House = TbHouse.Text;
                newEstate.Coordinate_latitude = double.Parse(TbLatitude.Text);
                newEstate.Coordinate_longitude = double.Parse(TbLongTitude.Text);
            }
            else if (newEstateType == EstateType.House)
            {
                newEstate.RealEstateSet_House = new RealEstateSet_House
                {
                    TotalFloors = int.Parse(TbFloors.Text),
                    TotalArea = double.Parse(TbTotalArea.Text)
                };
                newEstate.Address_City = TbCity.Text;
                newEstate.Address_Street = TbStreet.Text;
                newEstate.Address_Number = TbNumber.Text;
                newEstate.Address_House = TbHouse.Text;
                newEstate.Coordinate_latitude = double.Parse(TbLatitude.Text);
                newEstate.Coordinate_longitude = double.Parse(TbLongTitude.Text);
            }
            else
            {
                newEstate.RealEstateSet_Land = new RealEstateSet_Land
                {
                    TotalArea = double.Parse(TbTotalArea.Text)
                };
                newEstate.Address_City = TbCity.Text;
                newEstate.Address_Street = TbStreet.Text;
                newEstate.Address_Number = TbNumber.Text;
                newEstate.Address_House = TbHouse.Text;
                newEstate.Coordinate_latitude = double.Parse(TbLatitude.Text);
                newEstate.Coordinate_longitude = double.Parse(TbLongTitude.Text);
            }
            App.Context.RealEstateSets.Add(newEstate);
            App.Context.SaveChanges();
            Close();
        }
    }
}
