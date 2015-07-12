using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

using RouteHotel.TransportObjects;

namespace RouteHotel
{
    /// <summary>
    /// Summary description for RouteAPI
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    public class RouteAPI : System.Web.Services.WebService
    {

        /// <summary>
        /// Purely search the route
        /// Used for tests
        /// </summary>
        /// <param name="routeParams"></param>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public RouteHotel.TransportObjects.Route GetRoute(RouteParams routeParams)
        {
            if (null == routeParams) return null; // nothing to search

            GoogleDirections.Location[] locations = Location.ConvertLocations(routeParams.Locations);
            GoogleDirections.Route webRequestedRoute = GoogleDirections.RouteDirections.GetRoute(routeParams.OptimizeRoute, locations);

            RouteHotel.TransportObjects.Route result = new RouteHotel.TransportObjects.Route(webRequestedRoute);

            //result.RouteID = RouteCalculator.GenerateID().ToString();s
            // TO DO - start hotels processing here

            return result;
        }

        /// <summary>
        /// Search route and hotels on it
        /// </summary>
        /// <param name="routeParams"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = false)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public RouteHotel.TransportObjects.Route GetRouteHotels(RouteParams routeParams)
        {
            if (null == routeParams) return null; // nothing to search

            RouteCalculator calculator = new RouteCalculator(routeParams);

            SessionObjects.Current.AddCalculator(calculator); // add calculator to session object to enable later search it from otehr web requests in this session
            // TODO - remove calculator once web request see that calculation finished and grab all the data

            calculator.Search();

            RouteHotel.TransportObjects.Route route = new RouteHotel.TransportObjects.Route(calculator.Route);
            route.RouteID = calculator.ID.ToString(); // to identify hotels requests

            return route;
        }

        /// <summary>
        /// !!! Temporary method - to examine API - to be removed
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public RouteParams GetTO()
        {
            RouteParams result = new RouteParams();
            result.OptimizeRoute = true;

            Location[] locations = null;
            {
                Location location1 = new Location();
                location1.LocationName = "Lviv";
                location1.LatLng = new LatLng(new GoogleDirections.LatLng(49.83549134162667, 24.024996757507324));
                Location location2 = new Location();
                location2.LocationName = "Kyiv";

                List<Location> locationsList = new List<Location>();
                locationsList.Add(location1);
                locationsList.Add(location2);

                locations = locationsList.ToArray();
            }
            result.Locations = locations;

            return result;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public LatLng GetUserLocationByIP()
        {
            return RouteHotel.Utils.IPUtils.GetUserLocationByIP();
        }
    }
}
