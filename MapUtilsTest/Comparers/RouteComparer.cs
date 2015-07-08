using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MapUtilsTest.HotelRouteAPI;

namespace MapUtilsTest.Comparers
{
    /// <summary>
    /// Route classes comparison utils
    /// </summary>
    class RouteComparer
    {
        public static bool EqualPoints(LatLng point1, GoogleDirections.LatLng point2)
        {
            return point1.Latitude == point2.Latitude && point1.Longitude == point2.Longitude;
        }

        public static bool EqualSteps(RouteStep step1, GoogleDirections.RouteStep step2)
        {
            if ((null != step1.Points ) != step2.HasPoints) return false;
            if (step2.HasPoints)
                if (step1.Points.Length != step2.Points.Length) return false;

            if (step1.Duration != step2.Duration) return false;
            if (step1.Distance != step2.Distance) return false;
            if (!step1.StartLocation.Equals(step2.StartLocation)) return false;
            if (!step1.EndLocation.Equals(step2.EndLocation)) return false;

            if (step2.HasPoints)
            {
                for (int i = 0; i < step1.Points.Length; ++i)
                {
                    if (!EqualPoints(step1.Points[i], step2.Points[i])) return false;
                }
            }

            return true;
        }

        public static bool EqualLegs(RouteLeg leg1, GoogleDirections.RouteLeg leg2)
        {
            if ((null != leg1.Steps) != leg2.HasSteps) return false;
            if (leg2.HasSteps)
                if (leg1.Steps.Length != leg2.Steps.Length) return false;

            if (leg1.StartAddress != leg2.StartAddress) return false;
            if (leg1.EndAddress != leg2.EndAddress) return false;
            if (leg1.Duration != leg2.Duration) return false;
            if (leg1.Distance != leg2.Distance) return false;
            if (!leg1.StartLocation.Equals(leg2.StartLocation)) return false;
            if (!leg1.EndLocation.Equals(leg2.EndLocation)) return false;

            if (leg2.HasSteps)
            {
                for (int i = 0; i < leg1.Steps.Length; ++i)
                {
                    if (!EqualSteps(leg1.Steps[i], leg2.Steps[i])) return false;
                }
            }

            return true;
        }

        public static bool EqualRoute(Route route1, GoogleDirections.Route route2)
        {
            if (route1.Summary != route2.Summary) return false;
            if (route1.Legs.Length != route2.Legs.Length) return false;

            // block below is optimization - if calculated params differs- this is not same route
            if (route1.Distance != route2.Distance) return false;
            if (route1.Duration != route2.Duration) return false;

            if (null != route2.Legs)
            {
                for (int i = 0; i < route1.Legs.Length; ++i)
                {
                    if (!EqualLegs(route1.Legs[i], route2.Legs[i])) return false;
                }
            }

            return true;
        }
    }
}
