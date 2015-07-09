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

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public RouteHotel.TransportObjects.Route GetRoute(RouteParams routeParams)
        {
            if (null == routeParams) return null; // nothing to search

            GoogleDirections.Location[] locations = Location.ConvertLocations(routeParams.Locations);
            GoogleDirections.Route webRequestedRoute = GoogleDirections.RouteDirections.GetRoute(routeParams.OptimizeRoute, locations);

            RouteHotel.TransportObjects.Route result = new RouteHotel.TransportObjects.Route(webRequestedRoute);
            return result;
        }
    }
}
