using System;
using System.Collections.Generic;
using System.Web;

namespace RouteHotel.TransportObjects
{
    /// <summary>
    /// Class representing a location, defined by name and/or by latitude/longitude
    /// </summary>
    public class Location : RouteTransportObjects.Location
    {

        /// <summary>
        /// Default c.tor
        /// </summary>
        public Location()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="locationName">Name of the location.</param>
        public Location(string locationName)
        {
            this.LocationName = locationName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="latLng">The latitude/longitude of the location.</param>
        public Location(LatLng latLng)
        {
            this.LatLng = latLng;
        }

        internal Location(LatLng latLng, string locationName)
        {
            this.LatLng = latLng;
            this.LocationName = locationName;
        }


        /// <summary>
        /// Converts into GoogleDirections.Location
        /// </summary>
        /// <returns>GoogleDirections.Location object</returns>
        public static GoogleDirections.Location ConvertToLocation(RouteTransportObjects.Location location)
        {
            GoogleDirections.LatLng googleLatLng = null == location.LatLng
                ? GoogleDirections.LatLng.EMPTY
                : RouteHotel.TransportObjects.LatLng.ConvertToLatLng(location.LatLng);
            return new GoogleDirections.Location(googleLatLng, location.LocationName);
        }

        /// <summary>
        /// Converts array of transport objects into location list
        /// </summary>
        /// <param name="locations">List of locations</param>
        /// <returns>Google direction list of locations</returns>
        public static GoogleDirections.Location[] ConvertLocations(RouteTransportObjects.Location[] locations)
        {
            if (null == locations) return null;

            GoogleDirections.Location[] result = new GoogleDirections.Location[locations.Length];

            for (int i = 0; i < locations.Length; ++i)
            {
                result[i] = ConvertToLocation(locations[i]);
            }

            return result;
        }
    }
}