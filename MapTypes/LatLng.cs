using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapTypes
{
    /// <summary>
    /// Class representing a latitude/longitude pair
    /// </summary>
    public class LatLng
    {

        private double latitude;
        /// <summary>
        /// Gets the latitude.
        /// </summary>
        public double Latitude
        {
            get
            {
                return latitude;
            }
        }

        private double longitude;
        /// <summary>
        /// Gets the longitude.
        /// </summary>
        public double Longitude
        {
            get
            {
                return longitude;
            }
        }

        /// <summary>
        /// Empty point
        /// </summary>
        public static LatLng EMPTY = new LatLng(double.MaxValue, double.MaxValue);

        /// <summary>
        /// Returns true is point is empty
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this.Equals(EMPTY);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LatLng"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public LatLng(double latitude, double longitude)
        {
            Init(latitude, longitude);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LatLng"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        protected void Init(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        /// <summary>
        /// Creates new Empty point
        /// </summary>
        /// <returns>New empty point</returns>
        public static LatLng CreateEmpty()
        {
            return EMPTY;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)
                + ", "
                + longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
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
            double distance = CalculationUtils.DistanceUtils.Distance(this.Latitude, this.Longitude, obj.Latitude, obj.Longitude);

            double distanceMeters = distance;

            return CalculationUtils.CONSTS.ACCURACY > distanceMeters;
        }

        /// <summary>
        /// Returns hashcode
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode()
        {
            return (latitude + longitude).GetHashCode();
        }
    }
}
