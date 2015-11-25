using System.Globalization;
using System.Xml;


namespace GoogleDirections
{
    /// <summary>
    /// Class representing a latitude/longitude pair
    /// </summary>
    public class LatLng : MapTypes.LatLng
    {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LatLng"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public LatLng(double latitude, double longitude) : base (latitude, longitude)
        {
        }

        internal LatLng(XmlElement locationElement) :
            base(
            double.Parse(locationElement.SelectSingleNode("lat").InnerText, CultureInfo.InvariantCulture),
            double.Parse(locationElement.SelectSingleNode("lng").InnerText, CultureInfo.InvariantCulture)
            )
        {
            
        }

   
    }
}
