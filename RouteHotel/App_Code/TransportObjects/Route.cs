using System;
using System.Collections.Generic;
using System.Web;

namespace RouteHotel.TransportObjects
{
    public class Route : RouteTransportObjects.Route
    {
 
        internal Route(GoogleDirections.Route route)
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
    }
}