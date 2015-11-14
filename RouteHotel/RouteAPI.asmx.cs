
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

using RouteHotel.TransportObjects;
using HotelInterface.TO;

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
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public RouteHotel.TransportObjects.Route GetRouteHotels(RouteParams routeParams)
        {
            if (null == routeParams) return null; // nothing to search

            Log.Debug(routeParams.ToString());

            RouteCalculator calculator = new RouteCalculator(routeParams);

            SessionObjects.Current.AddCalculator(calculator); // add calculator to session object to enable later search it from otehr web requests in this session

            calculator.Search();

            RouteHotel.TransportObjects.Route route = new RouteHotel.TransportObjects.Route(calculator.Route);
            route.RouteID = calculator.ID.ToString(); // to identify hotels requests

            return route;
        }

        /// <summary>
        /// Returns calculation posints for route with Id provided
        /// If routeID is wrong or no data exists for any reason - null returned
        /// </summary>
        /// <param name="routeID">ID of route been calculated</param>
        /// <returns>Array of calculation points. </returns>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public RouteHotel.TransportObjects.CalculationRouteLeg[] GetCalculationPoints(string routeID)
        {
            if (null == routeID) return null;
            Guid routeIDobj = new Guid(routeID);

            RouteHotel.TransportObjects.CalculationRouteLeg[] result = FetchPoints(routeIDobj);
            
            return result;
        }

        /// <summary>
        /// Gets route object and fetch points for it
        /// </summary>
        /// <param name="routeIDobj">ID of route object</param>
        /// <returns>Array of calculation points. </returns>
        private RouteHotel.TransportObjects.CalculationRouteLeg[] FetchPoints(Guid routeIDobj)
        {
            RouteCalculator calculator = SessionObjects.Current.GetCalculator(routeIDobj);
            if (null == calculator) return null;

            List<CalculationRouteLeg> result = new List<CalculationRouteLeg>();
            foreach (HotelRouteCalculation.LinkedPoint firstLegPoint in calculator.HotelSearch.RoutePoints.LegsStart)
            {
                CalculationRouteLeg leg = new CalculationRouteLeg(firstLegPoint);
                result.Add(leg);
            }

            return result.ToArray();
        }

        /// <summary>
        /// Returns hotels found for specific route from index specied
        /// </summary>
        /// <param name="routeID">ID of route been calculated</param>
        /// <param name="alreadyFetchedHotelsCount">Count of already fetched hotels</param>
        /// <returns>Array of hotels. </returns>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public HotelResponse GetHotels(string routeID, int alreadyFetchedHotelsCount)
        {
            if (null == routeID) return null;
            Guid routeIDobj = new Guid(routeID);

            // TBD - add unit test for calculator! - at least to make sure we reach "finished" state
            RouteCalculator calculator = SessionObjects.Current.GetCalculator(routeIDobj);
            if (null == calculator) return null;

            HotelResponse response = new HotelResponse(calculator, alreadyFetchedHotelsCount);

            if (!calculator.SearchInProgress)
            {
                //remove calculator once web grabbeb all the data
                SessionObjects.Current.RemoveCalculator(routeIDobj);
            }

            return response;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public LatLng GetUserLocationByIP()
        {
            return RouteHotel.Utils.IPUtils.GetUserLocationByIP();
        }

#if DEBUG
        /// <summary>
        /// Debug purpose method to fetch points that will be used in hotel search
        /// Might be used to visualize covered areas on map
        /// </summary>
        /// <param name="routeID">ID of route selected</param>
        /// <returns>List of points wioth other params</returns>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public RouteHotel.TransportObjects.HotelCalculationPoints GetHotelSearchPoints(string routeID)
        {
            if (null == routeID) return null;
            Guid routeIDobj = new Guid(routeID);

            // TBD - add unit test for calculator! - at least to make sure we reach "finished" state
            RouteCalculator calculator = SessionObjects.Current.GetCalculator(routeIDobj);
            if (null == calculator) return null;

            GoogleDirections.LatLng[] calculationPoints = calculator.HotelSearch.GetCalculationPoints();
            int calculationRadius = calculator.HotelSearch.GetCalculationRaduis();

            RouteHotel.TransportObjects.HotelCalculationPoints result = new RouteHotel.TransportObjects.HotelCalculationPoints(calculationRadius, calculationPoints);

            return result;
        }

#endif

        /// <summary>
        /// Search route and hotels on it
        /// </summary>
        /// <param name="routeParams"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public RouteHotel.TransportObjects.Route StartHotelSearch(gmap.RouteResult routeResult) 
        {
            Log.Debug(routeResult.ToString());
            return null;
        }

    }
}
