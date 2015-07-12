using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteHotel.Utils
{
    /// <summary>
    /// Represents responce from http://freegeoip.net/xml/ web service
    /// </summary>
    public class IPRequestResponce
    {
        public string Ip { get; set; }
        public string Countrycode { get; set; }
        public string CountryName { get; set; }
        public string RegionCode { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MetroCode { get; set; }
    }
}