﻿using System;
using System.Collections.Generic;
using System.Web;

namespace RouteTransportObjects
{
    public class Route
    {
        /// <summary>
        /// Unique ID of route for user.
        /// It is used to identify route in calculation and requests frm client to recieve data
        /// </summary>
        public string RouteID { get; set; }
        
        /// <summary>
        /// Gets a summary of the roads used in the calculated route.
        /// </summary>
        public string Summary
        {
            get; set;
        }

        /// <summary>
        /// Gets the legs of this route.
        /// </summary>
        public RouteLeg[] Legs
        {
            get; set;
        }

        /// <summary>
        /// Gets the duration of the route in seconds.
        /// </summary>
        public int Duration
        {
            get; set;
        }

        /// <summary>
        /// Gets the distance of the route in metres.
        /// </summary>
        public int Distance
        {
            get;
            set;
        }


        public Route() { }
    }
}