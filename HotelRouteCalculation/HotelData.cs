using GoogleDirections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Represents data for single hotel
    /// </summary>
    public class HotelData
    {
        /// <summary>
        /// Hotel's location
        /// </summary>
        public LatLng Location
        {
            get { return location; }
        }
        private LatLng location;

        /// <summary>
        /// Hotel's name
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        private string name;
    }
}
