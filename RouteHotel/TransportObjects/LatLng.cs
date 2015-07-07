using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteHotel.TransportObjects
{
    public class LatLng
    {
        internal LatLng(GoogleDirections.LatLng location)
        {
            Latitude = location.Latitude;
            Longitude = location.Longitude;
        }

        public LatLng() { }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        public double Latitude
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        public double Longitude
        {
            get;
            set;
        }
    }
}