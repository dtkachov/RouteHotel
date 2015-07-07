using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteHotel.TransportObjects
{
    public class Route
    {
        internal Route (GoogleDirections.Route route)
        {
            this.Summary = route.Summary;
            this.Duration = route.Duration;
            this.Distance = route.Distance;

            if (null != route.Legs)
            {
                this.Legs = new RouteLeg[route.Legs.Length];
                for (int i = 0; i < route.Legs.Length; i++)
                {
                    Legs[i] = new RouteLeg(route.Legs[i]);
                }
            }
        }

        public Route() { }
        
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
    }
}