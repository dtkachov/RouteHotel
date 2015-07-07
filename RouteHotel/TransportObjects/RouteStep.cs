using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteHotel.TransportObjects
{
    public class RouteStep
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