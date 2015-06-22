using System.Globalization;
using System.Xml;

namespace GoogleDirections
{
    /// <summary>
    /// Class representing a latitude/longitude pair
    /// </summary>
    public struct LatLng
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LatLng"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public LatLng(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        internal LatLng(XmlElement locationElement)
        {
            latitude = double.Parse(locationElement.SelectSingleNode("lat").InnerText, CultureInfo.InvariantCulture);
            longitude = double.Parse(locationElement.SelectSingleNode("lng").InnerText, CultureInfo.InvariantCulture);
        }

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
        private static LatLng EMPTY = new LatLng(double.MaxValue, double.MaxValue);

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
            return latitude.ToString(CultureInfo.InvariantCulture)
                + ", "
                + longitude.ToString(CultureInfo.InvariantCulture);
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
            double distance = Utils.Distance(this, obj);

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
            return (latitude + longitude).GetHashCode();
        }
    }
}
