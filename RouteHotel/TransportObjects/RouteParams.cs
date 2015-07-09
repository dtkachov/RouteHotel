using System;
using System.Collections.Generic;
using System.Web;

namespace RouteHotel.TransportObjects
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
        /// Default c.tor
        /// </summary>
        public RouteParams()
        {
        }

    }
}