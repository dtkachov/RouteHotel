using HotelInterface.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteHotel.TransportObjects
{
    /// <summary>
    /// Class representaitn transport object for oute parameters
    /// </summary>
    public class RouteParams : RouteTransportObjects.RouteParams
    {
        /// <summary>
        /// Hotel filter criterias
        /// </summary>
        public HotelPreference HotelParameters { get; set; }

        /// <summary>
        /// Default c.tor
        /// </summary>
        public RouteParams()
        {
        }

        public override string ToString()
        {
            string baseInfo = base.ToString();
            string hotelParametersStr = HotelParameters.ToString();
            return baseInfo + '\t' + hotelParametersStr;
        }
    }
}