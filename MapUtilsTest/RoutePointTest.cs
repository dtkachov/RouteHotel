using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HotelRouteCalculation;
using GoogleDirections;
using MapUtils;
using System.IO;

namespace MapUtilsTest
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    [TestClass]
    public class RoutePointTest
    {
        private RouteHotelSearch Search;

        /// <summary>
        /// Indicate that search is in progress
        /// </summary>
        private bool SearchInProgress = false;

        public RoutePointTest()
        {
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

        private int CurrentLegIndex = 0;

        private static int TestIndex = 0;

        [TestMethod]
        public void TestVladivostokLissabon()
        {
            //InitRoute(
            //    new Location("Vladivostok"),
            //    new Location("Lissabon")
            //    );
            //DoTest();
        }

        [TestMethod]
        public void TestLissabonVladivostok()
        {
            //InitRoute(
            //    new Location("Lissabon"),
            //    new Location("Vladivostok")
            //    );
            //DoTest();
        }

        /// <summary>
        /// Ignore as it looks like google cannot identify the points
        /// </summary>
        [TestMethod, Ignore]
        public void TestSalvadorValparaiso()
        {
            InitRoute(
                new Location("Salvador"),
                new Location("Valparaiso")
                );
            DoTest();
        }

        /// <summary>
        /// Ignore as it looks like google cannot identify the points
        /// </summary>
        [TestMethod, Ignore]
        public void TestValparaisoSalvador()
        {
            InitRoute(
                new Location("Valparaiso"),
                new Location("Salvador")
                );
            DoTest();
        }

        /// <summary>
        /// Ignore as it looks like google cannot identify the points
        /// </summary>
        [TestMethod]
        public void TestHotelSearchLvivKyiv()
        {
            InitRoute(
                new Location("Lviv"),
                new Location("Kyiv")
                );
            DoTest();
        }

        private void InitRoute(params Location[] locations)
        {
            TestIndex++;
            const double RADIUS = 1000; // meters
            Proximity proximity = new Proximity(RADIUS);

            const bool OPTIMIZE = true;
            Route route = GoogleDirections.RouteDirections.GetCachedRoute(OPTIMIZE, locations);

            Search = new RouteHotelSearch(route, proximity);
        }

        private void DoTest()
        {
            Search.Progress += Search_Progress;

            SearchInProgress = true;
            Search.Search();

            Assert.IsFalse(SearchInProgress); // verify that test is finished and data priocessed
        }

        private void Search_Progress(object sender, CalculationStatusEventArgs e)
        {
            if (e.Finished)
            {
                CheckResults();
            }
        }

        private void CheckResults()
        {
            SearchInProgress = false;

            string routeCalculationStatistic = string.Format("Points originally: '{0}', after optimization: '{1}'", CalculatePointsCountINoriginalRoute(), Search.RoutePoints.PointCount);
            Console.WriteLine(routeCalculationStatistic);

            CurrentLegIndex = 0;
            foreach (LinkedPoint legStart in Search.RoutePoints.LegsStart)
            {
                LinkedPoint currentPoint = legStart;
                string fileName = string.Format(@"d:\temp\HotelRoute.Test#{1}.Leg#{0}.txt", CurrentLegIndex++, TestIndex);
                
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    DumpRote(Search.RoutePoints.Route, writer);
                    writer.WriteLine("-----------------");
                    writer.WriteLine("Started optimized points");
                    while (!currentPoint.IsLast)
                    {
                        int insertedPointsCount = DumpPoint(currentPoint, writer);
                        for (int i = 0; i < insertedPointsCount; ++i)
                        {
                            //advance point to pass inserted items
                            currentPoint = currentPoint.Next;
                        }

                        currentPoint = currentPoint.Next;
                    }
                    DumpPoint(currentPoint, writer); // dumpt last point
                }
            }
        }

        private int CalculatePointsCountINoriginalRoute()
        {
            int result = 0;
            foreach (RouteLeg leg in Search.RoutePoints.Route.Legs)
            {
                foreach(RouteStep step in leg.Steps)
                {
                    if (step.HasPoints)
                        result += step.Points.Length;
                }
            }
            return result;
        }

        private void DumpRote(Route route, StreamWriter writer)
        {
            writer.WriteLine("starting dump of route with {0} legs", route.Legs.Length);
            for (int l = 0; l < route.Legs.Length; ++l)
            {
                RouteLeg leg = route.Legs[l];
                writer.WriteLine("Leg {0} has {1} steps", l, leg.Steps.Length);
                for (int s = 0; s < leg.Steps.Length; ++s)
                {
                    RouteStep step = leg.Steps[s];
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
        
        /// <summary>
        /// Dumps the point
        /// In case if some pounts were inserted after this method 
        /// number of such items would be returned
        /// Inserted items would be dumped
        /// </summary>
        /// <param name="currentPoint">Current point</param>
        /// <param name="writer">Writer instance</param>
        /// <returns>Number of inserted points (if any)</returns>
        private int DumpPoint(LinkedPoint currentPoint, StreamWriter writer)
        {
            int result = 0;
            writer.WriteLine("{0}-{1} distance: {2}",
                currentPoint.Point.Latitude,
                currentPoint.Point.Longitude,
                currentPoint.Distance
                );
            if (currentPoint.Next != currentPoint.OriginalNext)
            {
                // some points were deleted or added, dump them
                if (currentPoint.Distance >= currentPoint.OriginalDistance)
                {
                    /* new distance is bigger, so points were deleted
                     * so iterate through original items unless
                     * we do not meet original point
                     * */
                    LinkedPoint p = currentPoint.OriginalNext;
                    while (p != currentPoint.Next)
                    {
                        writer.WriteLine("\t\tDeleted:{0}-{1} distance: {2}",
                            p.Point.Latitude,
                            p.Point.Longitude,
                            p.Distance
                        );
                        p = p.OriginalNext;
                    }
                }
                else
                {
                    // points were inserted after the current one
                    writer.WriteLine("\t\tOriginal:{0}-{1} distance: {2}",
                            currentPoint.Point.Latitude,
                            currentPoint.Point.Longitude,
                            Utils.Distance(currentPoint.Point, currentPoint.OriginalNext.Point)
                        );
                    LinkedPoint p = currentPoint.Next;
                    while (p != currentPoint.OriginalNext)
                    {
                        writer.WriteLine("\t\tInserted:{0}-{1} distance: {2}",
                            p.Point.Latitude,
                            p.Point.Longitude,
                            p.Distance
                        );
                        p = p.Next;
                        Assert.IsNotNull(p);
                        if (null == p)
                        {
                            writer.WriteLine("\t\t!!!!!Next point in inserted loop is NULL");
                            break;
                        }
                        result++;
                    }
                }
            }
            return result;
        }


    }
}
