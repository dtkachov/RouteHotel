using System;
using System.Collections.Generic;
using System.Web;

namespace RouteTransportObjects
{
    public class RouteStep
    {

        public RouteStep() { }

        /// <summary>
        /// Gets the duration of this step in seconds.
        /// </summary>
        public int Duration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the distance in metres for this step.
        /// </summary>
        public int Distance
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the start location for this step.
        /// </summary>
        public LatLng StartLocation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the end location of this step.
        /// </summary>
        public LatLng EndLocation
        {
            get;
            set;
        }


        /// <summary>
        /// List of points
        /// </summary>
        public LatLng[] Points
        {
            get;
            set;
        }
    }
}