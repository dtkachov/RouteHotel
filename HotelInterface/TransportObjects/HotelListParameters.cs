using CalculationUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelInterface.TransportObjects
{
    /// <summary>
    /// Represents transport object fopr hotel list parameters
    /// </summary>
    public class HotelListParameters
    {
        /// <summary>
        /// Locale for Response.
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Hotel search radius - minnimum 2km
        /// </summary>
        public int SearchRadius { get; set; }

        /// <summary>
        /// Hotel search radius - minnimum 2km
        /// </summary>
        public DistanceUnit SearchRadiusUnit { get; set; }

        /// <summary>
        /// Code of currency to present data in
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Location object to search hotels for
        /// </summary>
        public RouteTransportObjects.LatLng Location { get; set; }

        /// <summary>
        /// Date of arrival for hotel search
        /// </summary>
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Date of departure for hotel search
        /// </summary>
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Represents rooms array
        /// </summary>
        public RoomParameter[] Rooms { get; set; }
    }
}
