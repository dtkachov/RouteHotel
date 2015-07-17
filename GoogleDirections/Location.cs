using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleDirections
{
    /// <summary>
    /// Class representing a location, defined by name and/or by latitude/longitude
    /// </summary>
    public class Location
    {
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="latLng">The latitude/longitude of the location.</param>
        /// <param name="locationName">Name of the location.</param>
        public Location(LatLng latLng, string locationName)
        {
            this.latLng = latLng;
            this.locationName = locationName;
        }

        private LatLng latLng;
        /// <summary>
        /// Gets the latitude/longitude of the location.
        /// </summary>
        public LatLng LatLng
        {
            get { return latLng; }
        }

        private string locationName;
        /// <summary>
        /// Gets the name/address of the location.
        /// </summary>
        /// <value>
        /// The name/address of the location.
        /// </value>
        public string LocationName
        {
            get { return locationName; }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (null != locationName && locationName.Trim().Length > 0)
                return locationName;

            return latLng.ToString();
        }
    }
}
