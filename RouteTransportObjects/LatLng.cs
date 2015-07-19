using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RouteTransportObjects
{
    /// <summary>
    /// Latitude/Longitude object
    /// </summary>
    public class LatLng
    {       
        /// <summary>
        /// Gets the latitude.
        /// </summary>
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        private double latitude;

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        public double Longitude
        {
            get { return longitude;  }
            set { longitude = value; }
        }
        private double longitude;

        public LatLng()
        {
        }

        /// <summary>
        /// Constructs the object based on latitude, longitude params provided.
        /// </summary>
        /// <param name="latitude">Latitude value</param>
        /// <param name="longitude">Longitude value</param>
        public LatLng(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        /// <summary>
        /// Returns true if two objects are equal
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns>Wether two objects are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is LatLng) return Equals((LatLng)obj);

            return base.Equals(obj);
        }

        /// <summary>
        /// Returns true if two objects are equal
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns>Wether two objects are equal</returns>
        public bool Equals(LatLng obj)
        {
            double distance = CalculationUtils.DistanceUtils.Distance(Latitude, Longitude, obj.Latitude, obj.Longitude);

            double distanceMeters = distance;

            const double ACCURACY = 1; // meters
            return ACCURACY > distanceMeters;
        }


        /// <summary>
        /// Returns hashcode
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode()
        {
            return (Latitude + Longitude).GetHashCode();
        }


        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return latitude.ToString(CultureInfo.InvariantCulture)
                + ", "
                + longitude.ToString(CultureInfo.InvariantCulture);
        }
    }
}