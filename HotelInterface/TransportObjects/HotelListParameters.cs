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
        /// Location object to search hotels for
        /// </summary>
        public RouteTransportObjects.LatLng Location { get; set; }

        /// <summary>
        /// Hotel search radius - minnimum 2km
        /// </summary>
        public int SearchRadius { get; set; }

        /// <summary>
        /// Hotel search radius unit - minimum 2km
        /// </summary>
        public DistanceUnit SearchRadiusUnit { get; set; }

        /// <summary>
        /// User's preference for the hotel
        /// </summary>
        public HotelPreference HotelPreferences
        {
            get { return _hotelPreferences; }
            set { _hotelPreferences = value; } 
        }
        private HotelPreference _hotelPreferences;
    }
}
