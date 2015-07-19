
using System;
using System.Collections.Generic;
using System.Web;

namespace RouteTransportObjects
{
    public class RouteLeg
    {

        public RouteLeg() { }

        /// <summary>
        /// Gets the start address for this leg.
        /// </summary>
        public string StartAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the end address for this leg.
        /// </summary>
        public string EndAddress
        {
            get; set;
        }

        /// <summary>
        /// Gets the duration of this leg in seconds.
        /// </summary>
        public int Duration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the distance of this leg in metres.
        /// </summary>
        public int Distance
        {
            get;
            set; 
        }

        /// <summary>
        /// Gets the steps for this leg of the route.
        /// </summary>
        public RouteStep[] Steps
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the start location of this leg of the route.
        /// </summary>
        public LatLng StartLocation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the end location of this leg of the route.
        /// </summary>
        public LatLng EndLocation
        {
            get;
            set;
        }
    }
}