using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideSampleAPI.Models
{
    public class Quotes
    {
        public string from { get; set; }
        public string to { get; set; }
        public List<ListingsItem> listings { get; set; }
    }

    public class VehicleType
    {
        public string name { get; set; }
        public int maxPassengers { get; set; }
    }

    public class ListingsItem
    {
        public string name { get; set; }
        public double pricePerPassenger { get; set; }
        public double totalPrice { get; set; }
        public VehicleType vehicleType { get; set; }
    }

}
