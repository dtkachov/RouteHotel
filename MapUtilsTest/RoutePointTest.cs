using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HotelRouteCalculation;
using GoogleDirections;
using MapUtils;
using System.IO;
using HotelInterface.TransportObjects;

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

        /// <summary>
        /// Small distance, few hotels - to be used in test in general
        /// other cities to be used occasionally
        /// </summary>
        [TestMethod]
        public void TestWyzneLutoryz()
        {
            InitRoute(
                new Location("Wyzne"),
                new Location("Lutoryz")
                );
            DoTest();
        }

        [TestMethod, TestCategory("Route")]
        public void TestWashingtonSanDiego()
        {
            InitRoute(
                new Location("Washington"),
                new Location("San Diego")
                );
            DoTest();
        }

        [TestMethod, TestCategory("Route")]
        public void TestWarsawLissbon()
        {
            InitRoute(
                new Location("Warsaw"),
                new Location("Lissbon")
                );
            DoTest();
        }

        [TestMethod, TestCategory("Route")]
        public void TestDubrovnikParis()
        {
            InitRoute(
                new Location("Dubrovnik"),
                new Location("Paris")
                );
            DoTest();
        }

        /// <summary>
        /// Ignore as it looks like google cannot identify the points
        /// </summary>
        [TestMethod, TestCategory("Route")]
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
            const int RADIUS = 1000; // meters
            Proximity proximity = new Proximity(RADIUS);

            const bool OPTIMIZE = true;
            Route route = GoogleDirections.RouteDirections.GetCachedRoute(OPTIMIZE, locations);
            HotelPreference hotelParameters = BuildHotelParameters();

            Search = new RouteHotelSearch(route, proximity, hotelParameters);
        }

        private HotelPreference BuildHotelParameters()
        {
            HotelPreference result = new HotelPreference();
            result.ArrivalDate = DateTime.Now.AddDays(2);
            result.DepartureDate = result.ArrivalDate.AddDays(2);
            result.CurrencyCode = "UAH";
            result.Locale = "ua_UK";

            {
                List<RoomParameter> rooms = new List<RoomParameter>();

                RoomParameter room1 = new RoomParameter();
                room1.AdultsCount = 2;
                rooms.Add(room1);

                result.Rooms = rooms.ToArray();
            }

            return result;
        }

        private void DoTest()
        {
            Search.Progress += Search_Progress;
            Search.HotelSearchError += Search_HotelSearchError;

            SearchInProgress = true;
            Search.Search();

            // wait until seacch is done
            Search.WaitUntilFinished();
        }

        private void Search_HotelSearchError(object sender, HotelSearchErrorEventArgs e)
        {
            string errStr = e.ErrorSource.ToString() + e.Point.ToString();
            Console.WriteLine(errStr);
            throw e.ErrorSource;
        }

        private void Search_Progress(object sender, CalculationStatusEventArgs e)
        {
            if (e.Finished)
            {
                CheckResults();
                SearchInProgress = false;
            }
            else
            {
                int hotelCount = Search.Hotels.Count;
                string progressStr = string.Format(
                    "Processed {0} from {1} points. {2}  hotels found", 
                    e.Progress, 
                    e.Count,
                    hotelCount
                    );
                System.Diagnostics.Trace.WriteLine(progressStr);
            }
        }

        private void CheckResults()
        {
            SearchInProgress = false;

            DumpPoints();
            DumpHotels();
        }

        private void DumpHotels()
        {
            string fileName = string.Format(@"d:\temp\HotelData\RoutePointTest.Search hotels at {0}.txt", DateTime.Now.ToFileTime());

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (HotelSummary hotel in Search.Hotels)
                {
                    DumpHotel(hotel, writer);
                }
            }
        }

        private void DumpHotel(HotelSummary hotel, StreamWriter writer)
        {
            string hotelData = string.Format("Hotel '{0}' located in city {1} more info: {2}, link: {3}", hotel.Name, hotel.City, hotel.ShortDescription, hotel.DeepLink);
            writer.WriteLine(hotelData);

            int index = 0;
            foreach (HotelInterface.TransportObjects.RoomRateDetails roomRate in hotel.RoomRates)
            {
                string roomRateStr = string.Format("room rate {0}: {1} {2} for max guests: {3}", ++index, roomRate.Price, roomRate.Currency, roomRate.MaxRoomOccupancy, roomRate.RoomDescription);
                writer.WriteLine(roomRateStr);
            }
            writer.WriteLine(string.Empty);
        }

        private void DumpPoints()
        {
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
                            DistanceUtils.Distance(currentPoint.Point, currentPoint.OriginalNext.Point)
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
