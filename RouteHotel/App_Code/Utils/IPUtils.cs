using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace RouteHotel.Utils
{
    /// <summary>
    /// Utils to work with IP
    /// </summary>
    public class IPUtils
    {
        /// <summary>
        /// Checks user request IP address, and returns location for it
        /// </summary>
        /// <returns></returns>
        public static RouteHotel.TransportObjects.LatLng GetUserLocationByIP()
        {
            string URL = string.Format("http://freegeoip.net/xml/{0}", HttpContext.Current.Request.UserHostAddress);
            
            XmlDocument locrequest = new XmlDocument(); 
            locrequest.Load(URL); 
            XmlNode root = locrequest.DocumentElement;
            string latitudeStr = root.SelectSingleNode("Latitude").InnerText;
            string lobgitudeStr = root.SelectSingleNode("Latitude").InnerText;

            double lat = Double.Parse(latitudeStr, CultureInfo.InvariantCulture);
            double lon = Double.Parse(lobgitudeStr, CultureInfo.InvariantCulture);

            return new RouteHotel.TransportObjects.LatLng(lat, lon);
        }

    }
}