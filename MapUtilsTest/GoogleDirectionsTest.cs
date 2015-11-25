using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MapTypes;

namespace MapUtilsTest
{
    /// <summary>
    /// Summary description for GoogleDirectionsTest
    /// </summary>
    [TestClass]
    public class GoogleDirectionsTest
    {
        private class PointsDumper : GoogleDirections.IPointVisitor
        {
            private StreamWriter writer;

            public List<double> Distances = new List<double>();

            public PointsDumper(StreamWriter writer)
            {
                this.writer = writer;
            }

            /// <summary>
            /// Visitor method, will be invoked by class accepting the visitor 
            /// and two nearest points parameters will be returned 
            /// </summary>
            /// <param name="from">Starting point</param>
            /// <param name="to">Finish point</param>
            public void Visit(GoogleDirections.LatLng from, GoogleDirections.LatLng to)
            {
                double km = DistanceUtils.Distance(from, to);
                const int KM_TO_METERS = 1000;
                double meters = km * KM_TO_METERS;
                writer.WriteLine("Segment with {2:0.00} meters started at <{0}>, finished at <{1}> ",
                    from.ToString(), to.ToString(), meters);

                Distances.Add(meters);
            }

            public void DumpSortedDistances()
            {
                List<double> sortedList = new List<double>(Distances);
                sortedList.Sort();
                int index = 0;
                foreach (double distance in sortedList)
                {
                    writer.WriteLine("{0} {1:0.00}", index++, distance);
                }
            }
        }

        public GoogleDirectionsTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        // commented not to go out of limit
        [TestMethod]
        public void TestRoute()
        {
            {
                /*
                Location start = new Location("Lviv");
                Location finish = new Location("Kyiv");
                Route route = GetRoute(start, finish);
                DumpRote(route, "L-K");
                DumpSegments(route, "L-K");
                 */
            }
            
            {
                GoogleDirections.Location start = new GoogleDirections.Location("Vladivostok");
                GoogleDirections.Location finish = new GoogleDirections.Location("Lissabon");
                GoogleDirections.Route route = GetRoute(start, finish);
                DumpRote(route, "Vlad-Liss");
                DumpSegments(route, "Vlad-Liss");
            }

            {
                /*
                Location startLL = new Location(route.Legs[0].StartLocation);
                Location endLL = new Location(route.Legs[0].EndLocation);
                Route routeLL = GetRoute(startLL, endLL);
                DumpRote(routeLL, "L-K-LL");
                 */
            }
            /*
            for (int l = 0; l < route.Legs.Length; ++l)
            {
                RouteLeg leg = route.Legs[l];
                for (int s = 0; s < leg.Steps.Length; ++s)
                {
                    RouteStep step = leg.Steps[s];
                    Route routesRoute = GetRoute(new Location(step.StartLocation), new Location(step.EndLocation));
                    DumpRote(routesRoute, string.Format("{0}-{1}", step.StartLocation, step.EndLocation));
                }
            }
             */
        }

        private GoogleDirections.Route GetRoute(GoogleDirections.Location start, GoogleDirections.Location finish)
        {
            List<GoogleDirections.Location> locations = new List<GoogleDirections.Location>();
            locations.Add(start);
            locations.Add(finish);

            GoogleDirections.Route route = GoogleDirections.RouteDirections.GetCachedRoute(true, locations.ToArray());
            
            return route;
        }

        private void DumpSegments(GoogleDirections.Route route, string routeID)
        {
            int legsCount = 0;
            foreach (GoogleDirections.RouteLeg leg in route.Legs)
            {
                string fileName = @"d:\temp\Route_" + routeID + "_leg#" + (legsCount++).ToString() + "_distances.txt";
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine("starting dump of route with {0} legs", route.Legs.Length);
            
                    PointsDumper dumper = new PointsDumper(writer);
                    leg.AcceptPointVisitor(dumper);

                    dumper.DumpSortedDistances();
                }
            }

        }

        private void DumpRote(GoogleDirections.Route route, string routeID)
        {
            string fileName = @"d:\temp\Route_" + routeID + ".txt";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("starting dump of route with {0} legs", route.Legs.Length);
                for (int l = 0; l < route.Legs.Length; ++l)
                {
                    GoogleDirections.RouteLeg leg = route.Legs[l];
                    writer.WriteLine("Leg {0} has {1} steps", l, leg.Steps.Length);
                    for (int s = 0; s < leg.Steps.Length; ++s)
                    {
                        GoogleDirections.RouteStep step = leg.Steps[s];
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
