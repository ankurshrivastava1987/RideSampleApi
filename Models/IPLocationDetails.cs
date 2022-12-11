using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideSampleAPI.Models
{
    public class IPLocationDetails
    {
        public string city { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string geoNameId { get; set; }
        public string capital { get; set; }
        public string callingCode { get; set; }
    }
}
