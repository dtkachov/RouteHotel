using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MapUtilsTest.HotelRouteAPI;
using System.ServiceModel;

namespace MapUtilsTest
{
    [TestClass]
    public class TestWebAPI
    {
        [TestMethod]
        public void SimpleTest()
        {
            RouteAPISoapClient routeWebAPI = new RouteAPISoapClient();
            EndpointAddress address = new EndpointAddress("http://localhost:56766/routeapi.asmx");
            routeWebAPI.Endpoint.Address = address;
            // Note: do not delete! "The maximum message size quota for incoming messages" issues  - fixed in app.config - for service reference - bindings  section
            
            string res = routeWebAPI.GetAnything();

            Route route = routeWebAPI.GetRoute(); //MapUtilsTest.HoteRouteAPI.Route

            DumpRote(route);
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
