using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RouteHotel.TransportObjects
{
    /// <summary>
    /// Latitude/Longitude object
    /// </summary>
    public class LatLng : RouteTransportObjects.LatLng
    {       


        internal LatLng(MapTypes.LatLng location)
        {
            this.Latitude = location.Latitude;
            this.Longitude = location.Longitude;
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
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// Converts this transport object into Google direction LanLng object
        /// </summary>
        /// <returns>Google direction LanLng object</returns>
        public MapTypes.LatLng ConvertToLatLng()
        {
            return ConvertToLatLng(this);
        }

        /// <summary>
        /// Converts this transport object into Google direction LanLng object
        /// </summary>
        /// <param name="latLng">Object to convert</param>
        /// <returns>Google direction LanLng object</returns>
        internal static MapTypes.LatLng ConvertToLatLng(RouteTransportObjects.LatLng latLng)
        {
            return new MapTypes.LatLng(latLng.Latitude, latLng.Longitude);
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
        /// Returns true if two objects are equal
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns>Wether two objects are equal</returns>
        public bool Equals(GoogleDirections.LatLng obj)
        {
            return Equals(new LatLng(obj));
        }



        public static bool operator ==(LatLng a, GoogleDirections.LatLng b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(LatLng a, GoogleDirections.LatLng b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Returns hashcode
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}