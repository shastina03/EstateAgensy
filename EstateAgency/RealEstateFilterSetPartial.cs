using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateAgency
{
    public partial class RealEstateFilterSet
    {
        public string Type
        {
            get
            {
                if (this.RealEstateFilterSet_ApartmentFilter != null)
                {
                    return "Apartment";
                }
                if (this.RealEstateFilterSet_HouseFilter != null)
                {
                    return "House";
                }
                if (this.RealEstateFilterSet_LandFilter != null)
                {
                    return "Land";
                }
                return "Unknown";
            }
        }
        public EstateType TypeEstate
        {
            get
            {
                if (this.RealEstateFilterSet_ApartmentFilter != null)
                    return EstateType.Apartment;
                if (this.RealEstateFilterSet_HouseFilter != null)
                    return EstateType.House;
                return EstateType.Land;
            }
        }
    }
}
