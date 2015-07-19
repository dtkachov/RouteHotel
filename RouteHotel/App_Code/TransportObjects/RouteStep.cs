using System;
using System.Collections.Generic;
using System.Web;

namespace RouteHotel.TransportObjects
{
    public class RouteStep : RouteTransportObjects.RouteStep
    {
        internal RouteStep(GoogleDirections.RouteStep step)
        {
            Distance = step.Distance;
            Duration = step.Duration;
            StartLocation = new LatLng(step.StartLocation);
            EndLocation = new LatLng(step.EndLocation);
            
            if (step.HasPoints)
            {
                Points = new LatLng[step.Points.Length];
                for (int i = 0; i < step.Points.Length; i++)
                {
                    Points[i] = new LatLng(step.Points[i]);
                }
            }
        }

        public RouteStep() { }

    }
}