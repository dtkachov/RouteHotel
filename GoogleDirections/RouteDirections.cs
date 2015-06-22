using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace GoogleDirections
{
    /// <summary>
    /// Static class providing methods to retrieve directions between locations
    /// </summary>
    public static class RouteDirections
    {
        /// <summary>
        /// Builds main part of request string
        /// </summary>
        /// <param name="optimize">if set to <c>true</c> optimize the route by re-ordering the locations to minimise the
        /// time to complete the route.</param>
        /// <param name="locations">The locations.</param>
        /// <returns>Main part of request string</returns>
        public static string BuildRouteMainRequestString(bool optimize, params Location[] locations)
        {
            string result = "origin=" + locations[0].ToString() + "&destination=" + locations[locations.Length - 1].ToString();
            return result;
        }

        /// <summary>
        /// Gets a route from the Google Maps Directions web service.
        /// </summary>
        /// <param name="optimize">if set to <c>true</c> optimize the route by re-ordering the locations to minimise the
        /// time to complete the route.</param>
        /// <param name="locations">The locations.</param>
        /// <returns>The route xml document</returns>
        public static XmlDocument GetRouteXML(bool optimize, params Location[] locations)
        {
            if (locations.Length < 2)
                throw new ArgumentException("locations parameter must contains 2 or more locations", "locations");

            string reqStr = BuildRouteMainRequestString(optimize, locations);

            if (locations.Length > 2)
            {
                reqStr += "&waypoints=optimize:" + optimize.ToString().ToLower();
                for (int i = 1; i < locations.Length - 1; i++)
                {
                    reqStr += "|";
                    reqStr += locations[i].ToString();
                }

            }

            string fullRequestStr = "http://maps.googleapis.com/maps/api/directions/xml?sensor=false&" + reqStr;
            string request = HttpWebService.MakeRequest(fullRequestStr);

            return ParseResponse(request);
        }

        /// <summary>
        /// Gets a route from the Google Maps Directions web service.
        /// </summary>
        /// <param name="optimize">if set to <c>true</c> optimize the route by re-ordering the locations to minimise the
        /// time to complete the route.</param>
        /// <param name="locations">The locations.</param>
        /// <returns>The route </returns>
        public static Route GetRoute(bool optimize, params Location[] locations)
        {
            XmlDocument routeXmlDocument = GetRouteXML(true, locations);
            Route result = new Route(routeXmlDocument);
            return result;
        }

        /// <summary>
        /// Gets a route from the Google Maps Directions web service.
        /// If local cached data exists - loads from cache.
        /// If not - loads the Route, save it to cache (optimization) and return the data
        /// </summary>
        /// <param name="optimize">if set to <c>true</c> optimize the route by re-ordering the locations to minimise the
        /// time to complete the route.</param>
        /// <param name="locations">The locations.</param>
        /// <returns>The route </returns>
        public static CachedRoute GetCachedRoute(bool optimize, params Location[] locations)
        {
            CachedRoute result = new CachedRoute(optimize, locations);
            return result;
        }

        private static XmlDocument ParseResponse(string response)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);
            string status = xmlDoc.SelectSingleNode("DirectionsResponse/status").InnerText;
            if (status != "OK")
                throw new RoutingException(GetStatusMessage(status));

            return xmlDoc;
        }

        private static string GetStatusMessage(string status)
        {
            switch (status)
            {
                case "ZERO_RESULTS": return "No route found";
                case "NOT_FOUND": return "Not found";
                // TODO - other status messages
            }
            return status;
        }
    }
}
