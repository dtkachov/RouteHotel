
using System;
using System.Collections.Generic;
using System.Web;

namespace RouteHotel.TransportObjects
{
    public class RouteLeg : RouteTransportObjects.RouteLeg
    {
        internal RouteLeg(GoogleDirections.RouteLeg leg)
        {
            StartAddress = leg.StartAddress;
            EndAddress = leg.EndAddress;
            Distance = leg.Distance;
            Duration = leg.Duration;
            StartLocation = new LatLng(leg.StartLocation);
            EndLocation = new LatLng(leg.EndLocation);

            if (null != leg.Steps)
            {
                Steps = new RouteStep[leg.Steps.Length];
                for (int i = 0; i < leg.Steps.Length; i++ )
                {
                    Steps[i] = new RouteStep(leg.Steps[i]);
                }
            }            
        }

        public RouteLeg() { }

    }
}