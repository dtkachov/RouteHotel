
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
        public HotelSummary[] GetHotels(string routeID, int alreadyFetchedHotelsCount)
        {
            if (null == routeID) return null;
            Guid routeIDobj = new Guid(routeID);

            RouteCalculator calculator = SessionObjects.Current.GetCalculator(routeIDobj);
            if (null == calculator) return null;

            List<HotelSummary> allHotels = calculator.HotelSearch.Hotels;

            if (!calculator.SearchInProgress)
            {
                //remove calculator once web grabbeb all the data
                SessionObjects.Current.RemoveCalculator(routeIDobj);
            }

            HotelSummary[] result = FilterHotels(allHotels.ToArray(), alreadyFetchedHotelsCount);

            return result;
        }

        /// <summary>
        /// Filter hotel. Return hotels since position after index specified.
        /// This is to help selection of hotels by bunches
        /// </summary>
        /// <param name="hotels">List of hotels</param>
        /// <param name="alreadyFetchedHotelsCount">Count of already fetched hotels</param>
        /// <returns>Hotel list</returns>
        private HotelSummary[] FilterHotels(HotelSummary[] hotels, int alreadyFetchedHotelsCount)
        {
            if (alreadyFetchedHotelsCount > hotels.Length)
            {
                string err = string.Format(
                    "Hotels count {0}, but requested to fetch hotels since index {1}. Logic error", 
                    hotels.Length,
                    alreadyFetchedHotelsCount
                    );
                throw new ApplicationException(err);
            }

            int resultCount = hotels.Length - alreadyFetchedHotelsCount;
            HotelSummary[] result = new HotelSummary[resultCount];

            int startPosition = alreadyFetchedHotelsCount;
            for (int i = startPosition; i < hotels.Length; ++i)
            { 
                int index = i - startPosition;
                result[index] = hotels[i];
            }

            return result;
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
