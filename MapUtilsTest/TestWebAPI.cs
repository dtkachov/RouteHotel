using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MapUtilsTest.HotelRouteAPI;
using System.ServiceModel;
using System.Collections.Generic;

namespace MapUtilsTest
{
    [TestClass]
    public class TestWebAPI
    {
        const string ROUTE_URL = "http://localhost:56766/routeapi.asmx";

        [TestMethod]
        public void TestRouteSearchWebAPI()
        {
            TestBetyweenCities();
            // TODO - add test netween three or more locaitons
        }

        private void TestBetyweenCities()
        {
            TestBetweenTwoCities("Lviv", "Kyiv");
            TestBetweenTwoCities("Kyiv", "Lviv");
            TestBetweenTwoCities("vladivostok", "lissabon");
        }

        private void TestBetweenTwoCities(string fromCity, string toCity)
        {
            MapUtilsTest.HotelRouteAPI.RouteParams routeParams = new MapUtilsTest.HotelRouteAPI.RouteParams();
            
            // fill the parameters object
            {
                routeParams.OptimizeRoute = true;

                MapUtilsTest.HotelRouteAPI.Location location1 = new MapUtilsTest.HotelRouteAPI.Location();
                location1.LocationName = fromCity;
                MapUtilsTest.HotelRouteAPI.Location location2 = new MapUtilsTest.HotelRouteAPI.Location();
                location2.LocationName = toCity;

                List<MapUtilsTest.HotelRouteAPI.Location> locations = new List<MapUtilsTest.HotelRouteAPI.Location>();
                locations.Add(location1);
                locations.Add(location2);

                routeParams.Locations = locations.ToArray();
            }

            TestRoute(routeParams);
        }


        /// <summary>
        /// The test gets route by parameters provided via web service and locally
        /// Then it compares results. 
        /// This is to make sure information is passed identicall as it was sent
        /// </summary>
        /// <param name="routeParams">Route search parameters</param>
        private void TestRoute(MapUtilsTest.HotelRouteAPI.RouteParams routeParams)
        {
            RouteAPISoapClient routeWebAPI = new RouteAPISoapClient();
            EndpointAddress address = new EndpointAddress(ROUTE_URL);
            routeWebAPI.Endpoint.Address = address;

            /* In case of error in line below - "cannot find end point" the issues might be that 
             * Unit test project is not set as startup project and no web app listining to requests
             * Just set current unit test project as start up one for solution and try re-run the test
             */
            Route route = routeWebAPI.GetRoute(routeParams); //MapUtilsTest.HoteRouteAPI.Route

            DumpRote(route);

            // the following block gets route localy (not via WS) and check if results are the same
            {
                GoogleDirections.Location[] locations = ConvertLocations(routeParams);
                GoogleDirections.Route locallyObtainedRoute = GoogleDirections.RouteDirections.GetRoute(routeParams.OptimizeRoute, locations);

                Assert.IsTrue(Comparers.RouteComparer.EqualRoute(route, locallyObtainedRoute), "There is a difference in information apssed via werb servioce and obtained locally - please check the transport.");
            }
        }

        private static GoogleDirections.Location[] ConvertLocations(MapUtilsTest.HotelRouteAPI.RouteParams routeParams)
        {
            if (null == routeParams.Locations) return null;

            GoogleDirections.Location[] result = new GoogleDirections.Location[routeParams.Locations.Length];
            for (int i = 0; i < routeParams.Locations.Length; ++i)
            {
                MapUtilsTest.HotelRouteAPI.Location location = routeParams.Locations[i];

                GoogleDirections.LatLng googleLatLng = (null == location.LatLng)
                    ? GoogleDirections.LatLng.EMPTY 
                    : new GoogleDirections.LatLng(location.LatLng.Latitude, location.LatLng.Longitude);

                GoogleDirections.Location googleLocation = new GoogleDirections.Location(googleLatLng, location.LocationName);

                result[i] = googleLocation;
            }
            return result;
        }


        private void DumpRote(MapUtilsTest.HotelRouteAPI.Route route)
        {
            string fileName = @"d:\temp\Route_WebAPI.txt";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("starting dump of route with {0} legs", route.Legs.Length);
                for (int l = 0; l < route.Legs.Length; ++l)
                {
                    MapUtilsTest.HotelRouteAPI.RouteLeg leg = route.Legs[l];
                    writer.WriteLine("Leg {0} has {1} steps", l, leg.Steps.Length);
                    for (int s = 0; s < leg.Steps.Length; ++s)
                    {
                        MapUtilsTest.HotelRouteAPI.RouteStep step = leg.Steps[s];
                        writer.WriteLine("Step {0} start: {1}\t finish: {2}, distance {3}, pointsCount: {4}",
                            s, step.StartLocation, step.EndLocation, step.Distance, step.Points.Length);

                        writer.WriteLine("\t");
                        foreach (LatLng point in step.Points)
                        {
                            writer.Write("{" + string.Format("{0}", point.ToString() + "}"));
                        }
                        writer.WriteLine("\r\n");
                    }
                }

                writer.WriteLine("Route dump finished");
            }

        }
         
    }
}
