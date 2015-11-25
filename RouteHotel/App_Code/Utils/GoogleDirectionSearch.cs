using System;
using System.Collections.Generic;
using System.Web;

namespace RouteHotel.App_Code.Utils
{
    /// <summary>
    /// Helper class to work with google direction search
    /// </summary>
    public class GoogleDirectionSearch
    {
        /// <summary>
        /// Route search parameters
        /// </summary>
        private RouteHotel.TransportObjects.RouteParams Params;

        /// <summary>
        /// Route representation used for calculation
        /// </summary>
        public HotelRouteCalculation.Route CalculationRoute
        { 
            get
            {
                if (null == _calculationRoute)
                {
                    _calculationRoute = BuildRoutePointsObject(this.GoogleDirectionRoute);
                }
                return _calculationRoute;
            }
        }
        private HotelRouteCalculation.Route _calculationRoute;

        /// <summary>
        /// Google direction route representaiton.
        /// </summary>
        public GoogleDirections.Route GoogleDirectionRoute
        {
            get
            {
                if (null == _googleDirectionRoute)
                {
                    Search();
                }
                return _googleDirectionRoute;
            }
        }
        private GoogleDirections.Route _googleDirectionRoute;
     

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="routeParams">Route search parameters</param>
        public GoogleDirectionSearch(RouteHotel.TransportObjects.RouteParams routeParams)
        {
            if (null == routeParams) throw new ArgumentNullException("Argument routeParams cannot be null");
            if (null == routeParams.HotelParameters) throw new ArgumentNullException("Field HotelParameters cannot be null");

            this.Params = routeParams;
        }

        /// <summary>
        /// Performs route search by parameters provided
        /// </summary>
        public void Search()
        {
            GoogleDirections.Location[] locations = RouteHotel.TransportObjects.Location.ConvertLocations(Params.Locations);
            _googleDirectionRoute = GoogleDirections.RouteDirections.GetRoute(Params.OptimizeRoute, locations);
        }


        /// <summary>
        /// Builds initial list of linked points.
        /// Simply parse route and add points to the list
        /// </summary>
        private static HotelRouteCalculation.Route BuildRoutePointsObject(GoogleDirections.Route googleDirectionsRoute)
        {
            List<HotelRouteCalculation.LinkedPoint> legsStart = new List<HotelRouteCalculation.LinkedPoint>();

            for (int l = 0; l < googleDirectionsRoute.Legs.Length; ++l)
            {
                HotelRouteCalculation.LinkedPoint startPoint = null;
                HotelRouteCalculation.LinkedPoint prevPoint = null;

                GoogleDirections.RouteLeg leg = googleDirectionsRoute.Legs[l];
                for (int s = 0; s < leg.Steps.Length; ++s)
                {
                    GoogleDirections.RouteStep step = leg.Steps[s];
                    foreach (GoogleDirections.LatLng point in step.Points)
                    {
                        HotelRouteCalculation.LinkedPoint currentPoint = new HotelRouteCalculation.LinkedPoint(point);
                        if (null == startPoint)
                        {
                            // this is first point in Leg
                            startPoint = currentPoint;
                        }
                        else
                        {
                            // this is not the first point in list
                            prevPoint.Next = currentPoint;
                        }
                        prevPoint = currentPoint;
                    }
                }

                legsStart.Add(startPoint);
            }

            HotelRouteCalculation.Route result = new HotelRouteCalculation.Route(legsStart.ToArray());
            return result;
        }
    }
}