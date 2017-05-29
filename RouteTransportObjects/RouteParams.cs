using System;
using System.Collections.Generic;
using System.Web;

namespace RouteTransportObjects
{
    /// <summary>
    /// Class representaitn transport object for oute parameters
    /// </summary>
    public class RouteParams
    {
        /// <summary>
        /// Whether system should optimize route
        /// if set to <c>true</c> optimize the route by re-ordering the locations to minimise the
        /// time to complete the route.
        /// </summary>
        public bool OptimizeRoute { get; set; }

        /// <summary>
        /// Location list
        /// </summary>
        public Location[] Locations { get; set;}

        /// <summary>
        /// Proximity radius for hotel search in meters
        /// </summary>
        public int ProximityRadius { get; set; }

        /// <summary>
        /// Default c.tor
        /// </summary>
        public RouteParams()
        {
        }

        public override string ToString()
        {
            string locationsStr = string.Empty;
            if (null != Locations)
            {
                for (int i = 0; i < Locations.Length; ++i)
                {
                    if (i > 0) locationsStr += ", ";
                    locationsStr += Locations[i].ToString();
                }
            }

            string result = string.Format("OptimizeRoute: {0}, Locations: <{1}>, ProximityRadius: '{2}'", OptimizeRoute, locationsStr, ProximityRadius);
            return result;
        }

    }
}