using System;
using System.Collections.Generic;
using System.Web;

namespace RouteTransportObjects
{
    /// <summary>
    /// Class representing a location, defined by name and/or by latitude/longitude
    /// </summary>
    public class Location
    {
        private LatLng latLng;
        /// <summary>
        /// Gets/sets the latitude/longitude of the location.
        /// </summary>
        public LatLng LatLng
        {
            get { return latLng; }
            set { latLng = value; }
        }

        private string locationName;
        /// <summary>
        /// Gets/sets the name/address of the location.
        /// </summary>
        /// <value>
        /// The name/address of the location.
        /// </value>
        public string LocationName
        {
            get { return locationName; }
            set { locationName = value; }
        }

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
            this.locationName = locationName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="latLng">The latitude/longitude of the location.</param>
        public Location(LatLng latLng)
        {
            this.latLng = latLng;
        }

        internal Location(LatLng latLng, string locationName)
        {
            this.latLng = latLng;
            this.locationName = locationName;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (locationName != null)
                return locationName;

            return latLng.ToString();
        }

    }
}