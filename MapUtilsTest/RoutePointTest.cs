using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HotelRouteCalculation;
using MapTypes;
using MapUtils;
using System.IO;
using HotelInterface.TO;

namespace MapUtilsTest
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    [TestClass]
    public class RoutePointTest
    {

        [AssemblyInitialize]
        public static void Configure(TestContext tc)
        {
            //Diag output will go to the "output" logs if you add tehse two lines
            //TextWriterTraceListener writer = new TextWriterTraceListener(System.Console.Out);
            //Debug.Listeners.Add(writer);

            FileInfo file = new FileInfo("log4net.config.xml");
            log4net.Config.XmlConfigurator.Configure(file);
            // create the first logger AFTER we run the configuration
            log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            Log.Debug("log4net initialized for tests");
        }

        const int RADIUS = 2000; // meters

        private IRouteHotelSearch SignlePointSearch;
        private IRouteHotelSearch LoadBalancingSearch;

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

        /// <summary>
        /// Google directions route object
        /// </summary>
        private GoogleDirections.Route GoogleDirectionsRoute;


        private int CurrentLegIndex = 0;

        private static int TestIndex = 0;

        /// <summary>
        /// Small distance, few hotels - to be used in test in general
        /// other cities to be used occasionally
        /// </summary>
        [TestMethod]
        public void TestWieliczkaKatowice()
        {
            InitRoute(
                new GoogleDirections.Location("Wieliczka"),
                new GoogleDirections.Location("Katowice")
                );
            DoTest();
        }

        [TestMethod, TestCategory("Route")]
        public void TestLancutDebica()
        {
            InitRoute(
                new GoogleDirections.Location("Lancut"),
                new GoogleDirections.Location("Debica")
                );
            DoTest();
        }

        [TestMethod, TestCategory("Route")]
        public void TestWashingtonSanDiego()
        {
            InitRoute(
                new GoogleDirections.Location("Washington"),
                new GoogleDirections.Location("San Diego")
                );
            DoTest();
        }

        [TestMethod, TestCategory("Route")]
        public void TestWarsawLissbon()
        {
            InitRoute(
                new GoogleDirections.Location("Warsaw"),
                new GoogleDirections.Location("Lissbon")
                );
            DoTest();
        }

        [TestMethod, TestCategory("Route")]
        public void TestDubrovnikParis()
        {
            InitRoute(
                new GoogleDirections.Location("Dubrovnik"),
                new GoogleDirections.Location("Paris")
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
                new GoogleDirections.Location("Lviv"),
                new GoogleDirections.Location("Kyiv")
                );
            DoTest();
        }

        /// <summary>
        /// Ignore as it looks like google cannot identify the points
        /// </summary>
        [TestMethod, TestCategory("Route")]
        public void TestHotelSearchLvivKrakow()
        {
            InitRoute(
                new GoogleDirections.Location("Lviv"),
                new GoogleDirections.Location("Krakow")
                );
            DoTest();
        }

        private void InitRoute(params GoogleDirections.Location[] locations)
        {
            TestIndex++;
            Proximity proximity = new Proximity(RADIUS);

            const bool OPTIMIZE = true;
            GoogleDirectionsRoute = GoogleDirections.RouteDirections.GetCachedRoute(OPTIMIZE, locations);
            HotelRouteCalculation.Route route = BuildRoutePointsObject(GoogleDirectionsRoute);

            HotelPreference hotelParameters = BuildHotelParameters();

            SignlePointSearch = HoteSearchFactory.CreateSearch(SearchType.SinglePoint, route, proximity, hotelParameters);
            LoadBalancingSearch = HoteSearchFactory.CreateSearch(SearchType.LoadBalancing, route, proximity, hotelParameters);
        }

        /// <summary>
        /// Builds initial list of linked points.
        /// Simply parse route and add points to the list
        /// </summary>
        private HotelRouteCalculation.Route BuildRoutePointsObject(GoogleDirections.Route googleDirectionsRoute)
        {
            List<LinkedPoint> legsStart = new List<LinkedPoint>();

            for (int l = 0; l < googleDirectionsRoute.Legs.Length; ++l)
            {
                LinkedPoint startPoint = null;
                LinkedPoint prevPoint = null;

                GoogleDirections.RouteLeg leg = googleDirectionsRoute.Legs[l];
                for (int s = 0; s < leg.Steps.Length; ++s)
                {
                    GoogleDirections.RouteStep step = leg.Steps[s];
                    foreach (LatLng point in step.Points)
                    {
                        LinkedPoint currentPoint = new LinkedPoint(point);
                        if (null == startPoint)
                        {
                            // this is first point in Leg
                            startPoint = currentPoint;
                        }
                        else
                        {
                            // this is not the first point in list
                            prevPoint.Next = currentPoint;
                        }
                        prevPoint = currentPoint;
                    }
                }

                legsStart.Add(startPoint);
            }

            HotelRouteCalculation.Route result = new HotelRouteCalculation.Route(legsStart.ToArray());
            return result;
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
            DoTest(LoadBalancingSearch);
            DoTest(SignlePointSearch);

            //CompareSearchResults();
        }

        private void DoTest(IRouteHotelSearch search)
        {
            search.Progress += Search_Progress;
            search.HotelSearchError += Search_HotelSearchError;

            SearchInProgress = true;
            search.Search();

            // wait until seacch is done
            search.WaitUntilFinished();
        }

        private void Search_HotelSearchError(object sender, HotelSearchErrorEventArgs e)
        {
            string errStr = e.ErrorSource.ToString() + e.Point.ToString();
            Console.WriteLine(errStr);
            throw e.ErrorSource;
        }

        private void Search_Progress(object sender, CalculationStatusEventArgs e)
        {
            IRouteHotelSearch search = sender as IRouteHotelSearch;
            if (e.Finished)
            {
                CheckResults(search);
                SearchInProgress = false;
            }
            else
            {
                int hotelCount = search.Hotels.Count;
                string progressStr = string.Format(
                    "Processed {0} from {1} points. {2}  hotels found",
                    e.Progress,
                    e.Count,
                    hotelCount
                    );
                System.Diagnostics.Trace.WriteLine(progressStr);
            }
        }

        private void CheckResults(IRouteHotelSearch search)
        {
            SearchInProgress = false;

            DumpPoints(search);
            DumpHotels(search);
        }

        private void DumpHotels(IRouteHotelSearch search)
        {
            string fileName = string.Format(@"d:\temp\HotelData\RoutePointTest.Search hotels at {0}.txt", DateTime.Now.ToFileTime());

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (HotelSummary hotel in search.Hotels)
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
            foreach (HotelInterface.TO.RoomRateDetails roomRate in hotel.RoomRates)
            {
                string roomRateStr = string.Format("room rate {0}: {1} {2} for max guests: {3}", ++index, roomRate.Price, roomRate.Currency, roomRate.MaxRoomOccupancy, roomRate.RoomDescription);
                writer.WriteLine(roomRateStr);
            }
            writer.WriteLine(string.Empty);
        }

        private void DumpPoints(IRouteHotelSearch search)
        {
            string routeCalculationStatistic = string.Format("Points originally: '{0}', after optimization: '{1}'", CalculatePointsCountINoriginalRoute(search), search.RoutePoints.PointCount);
            Console.WriteLine(routeCalculationStatistic);

            CurrentLegIndex = 0;
            foreach (LinkedPoint legStart in search.RoutePoints.Route.RouteLegsStart)
            {
                LinkedPoint currentPoint = legStart;
                string fileName = string.Format(@"d:\temp\HotelRoute.Test#{1}.Leg#{0}.txt", CurrentLegIndex++, TestIndex);

                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    DumpRote(writer);
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

        private int CalculatePointsCountINoriginalRoute(IRouteHotelSearch search)
        {
            int result = 0;
            foreach (GoogleDirections.RouteLeg leg in GoogleDirectionsRoute.Legs)
            {
                foreach (GoogleDirections.RouteStep step in leg.Steps)
                {
                    if (step.HasPoints)
                        result += step.Points.Length;
                }
            }
            return result;
        }

        private void DumpRote(StreamWriter writer)
        {
            writer.WriteLine("starting dump of route with {0} legs", GoogleDirectionsRoute.Legs.Length);
            for (int l = 0; l < GoogleDirectionsRoute.Legs.Length; ++l)
            {
                GoogleDirections.RouteLeg leg = GoogleDirectionsRoute.Legs[l];
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
                        if (null == p) 
                            Assert.IsNotNull(p, "Parameter p is not expected to be null");
                        Assert.IsNotNull(p.Point);
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
                        Assert.IsNotNull(p);
                        Assert.IsNotNull(p.Point);
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

        /// <summary>
        /// Comparer if results obtaioned by different testmethods are the same 
        /// 
        /// Ignot this method for EAN since EAN at least in test mode each time returns different results.
        /// </summary>
        private void CompareSearchResults()
        {
            if (SignlePointSearch.Hotels.Count != LoadBalancingSearch.Hotels.Count)
            {
                CheckHotelsDifference();
            }

            Assert.AreEqual(
                SignlePointSearch.Hotels.Count, LoadBalancingSearch.Hotels.Count,
                string.Format("Count of hotels found by simple search ({0}) and load balancing search ({1}) is different", SignlePointSearch.Hotels.Count, LoadBalancingSearch.Hotels.Count)
                );

            foreach (HotelSummary hotel in SignlePointSearch.Hotels)
            {
                bool hasSameHotel = LoadBalancingSearch.Hotels.Contains(hotel);
                Assert.IsTrue(hasSameHotel);
            }

        }

        private void CheckHotelsDifference()
        {
            CheckAllHotelsWithinProximity(SignlePointSearch);
            CheckAllHotelsWithinProximity(LoadBalancingSearch);

            foreach (HotelSummary hotel in SignlePointSearch.Hotels)
            {
                bool hasSameHotel = ContainsHotel(hotel, LoadBalancingSearch.Hotels);
                if (!hasSameHotel)
                {
                    string missedHotelDescription = string.Format(
                        "Hotel {0} ID {1} located {2} {3} is present in simple search and missed in load balancing search",
                        hotel.Name, hotel.HotelId, hotel.Latitude, hotel.Longitude
                        );
                    System.Diagnostics.Trace.WriteLine(missedHotelDescription);
                }
            }

            foreach (HotelSummary hotel in LoadBalancingSearch.Hotels)
            {
                bool hasSameHotel = ContainsHotel(hotel, SignlePointSearch.Hotels);
                if (!hasSameHotel)
                {
                    string missedHotelDescription = string.Format(
                        "Hotel {0} ID {1} located {2} {3} is present in load balancing search and missed in simple search",
                        hotel.Name, hotel.HotelId, hotel.Latitude, hotel.Longitude
                        );
                    System.Diagnostics.Trace.WriteLine(missedHotelDescription);
                }
            }
        }

        private bool ContainsHotel(HotelSummary hotel, List<HotelSummary> hotelList)
        {
            foreach(HotelSummary h in hotelList)
            {
                if (h.HotelId == hotel.HotelId) return true;
            }
            return false;
        }

        private void CheckAllHotelsWithinProximity(IRouteHotelSearch search)
        {
            foreach (HotelSummary hotel in search.Hotels)
            {
                bool hotelInproximityToRoute = HotelInProximityToRoute(search.RoutePoints, hotel);
                Assert.IsTrue(hotelInproximityToRoute, 
                    string.Format("Hotel {0}  id: {1} located {2} {3} is not in proximity to route", hotel.Name, hotel.HotelId, hotel.Latitude, hotel.Longitude));
            }
        }

        /// <summary>
        /// Checks whether hotel is whithin proximity distance to the reoute represented by route points
        /// </summary>
        /// <param name="route">Route points representing the route</param>
        /// <param name="hotel">Hotel to be validated on matching procimity distance</param>
        /// <returns>Whether hotel is whithin proximity distance to the route</returns>
        private bool HotelInProximityToRoute(RoutePoints route, HotelSummary hotel)
        {
            foreach (LinkedPoint legStart in route.Route.RouteLegsStart)
            {
                bool belongToLeg = HotelInProximityToLeg(legStart, hotel);
                if (belongToLeg) return true;
            }
            return false;
        }

        /// <summary>
        /// Checks whether hotel is whithin proximity distance to the leg represented by leg start
        /// </summary>
        /// <param name="legStart">Starting point of the leg</param>
        /// <param name="hotel">Hotel to be validated on matching procimity distance</param>
        /// <returns>Whether hotel is whithin proximity distance to the leg represented by leg start</returns>
        private bool HotelInProximityToLeg(LinkedPoint legStart, HotelSummary hotel)
        {
            LinkedPoint p = legStart;
            do
            {
                double distance = CalculationUtils.DistanceUtils.Distance(p.Point.Latitude, p.Point.Longitude, hotel.Latitude, hotel.Longitude);
                if (distance < RADIUS) return true;
                p = p.Next;
            }
            while (null != p);

            return false;
        }

    }
}
