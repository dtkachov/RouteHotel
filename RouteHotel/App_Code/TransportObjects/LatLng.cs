using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteHotel.TransportObjects
{
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

        internal LatLng(GoogleDirections.LatLng location)
        {
            this.latitude = location.Latitude;
            this.longitude = location.Longitude;
        }

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
        /// Converts this transport object into Google direction LanLng object
        /// </summary>
        /// <returns>Google direction LanLng object</returns>
        public GoogleDirections.LatLng ConvertToLatLng()
        {
            return new GoogleDirections.LatLng(Latitude, Longitude);
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
            double distance = GoogleDirections.Utils.Distance(Latitude, Longitude, obj.Latitude, obj.Longitude);

            double distanceMeters = distance;

            const double ACCURACY = 1; // meters
            return ACCURACY > distanceMeters;
        }

        /// <summary>
        /// Returns true if two objects are equal
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns>Wether two objects are equal</returns>
        public bool Equals(GoogleDirections.LatLng obj)
        {
            return Equals(new LatLng(obj));
        }

        /// <summary>
        /// Returns hashcode
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode()
        {
            return (Latitude + Longitude).GetHashCode();
        }

        public static bool operator ==(LatLng a, GoogleDirections.LatLng b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(LatLng a, GoogleDirections.LatLng b)
        {
            return !a.Equals(b);
        }
    }
}