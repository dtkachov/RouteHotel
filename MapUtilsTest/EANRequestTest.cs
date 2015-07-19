using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EANInterface;
using HotelInterface.TransportObjects;
using EANInterface.TransportObjects;
using CalculationUtils;

namespace MapUtilsTest
{
    [TestClass]
    public class EANRequestTest
    {
        static EANRequestTest()
        {
            const string API_KEY = "cbrzfta369qwyrm9t5b8y8kf";
            const string CID = "55505";
            const string MINOR_REV = "99";
            EANCommon.Initialize(API_KEY, CID, MINOR_REV);
        }

        [TestMethod]
        public void TestHotelListRequest()
        {
            HotelListParameters parameters1 = Buildparameters1();
            TestInternal(parameters1);

            HotelListParameters parameters2 = Buildparameters2();
            TestInternal(parameters2);

            // TODO - add more test in other location (US, Europe, UK, Latam, etc) to verify parser works with different regions
        }

        private void TestInternal(HotelListParameters parameters)
        {
            HotelListRequest request = new HotelListRequest(parameters);
            request.Request();
            DumpData(request.Response);
        }

        private void DumpData(HotelListResponse Response)
        {
            string baseData = string.Format("Basic data: CacheKey: '{0}' CacheLocation: '{1}' CustomerSessionId: '{2}' MoreResultsAvailable: '{3}' NumberOfRoomsRequested: '{4}'", 
                Response.CacheKey, Response.CacheLocation, Response.CustomerSessionId, Response.MoreResultsAvailable, Response.NumberOfRoomsRequested);
            Console.WriteLine(baseData);
        }

        /// <summary>
        /// Returns multiple results
        /// </summary>
        /// <returns></returns>
        private HotelListParameters Buildparameters1()
        {
            HotelListParameters p = new HotelListParameters();
            p.Locale = "uk_UA";
            p.SearchRadius = 2000; // Meters
            p.SearchRadiusUnit = DistanceUnit.Meters;
            p.Location = new RouteTransportObjects.LatLng(49.8350004, 24.027728); // Lviv
            p.CurrencyCode = "USD";
            p.ArrivalDate = DateTime.Now.AddDays(2);
            p.DepartureDate = p.ArrivalDate.AddDays(1);
            {
                RoomParameter room = new RoomParameter();
                room.AdultsCount = 2;

                List<RoomParameter> roomList = new List<RoomParameter>();
                roomList.Add(room);
                p.Rooms = roomList.ToArray();
            }
            

            return p;
        }

        /// <summary>
        /// Returns single result
        /// </summary>
        /// <returns></returns>
        private HotelListParameters Buildparameters2()
        {
            HotelListParameters p = new HotelListParameters();
            p.Locale = "uk_UA";
            p.Location = new RouteTransportObjects.LatLng(50.5, 20.5);
            p.CurrencyCode = "USD";
            p.ArrivalDate = DateTime.Now.AddDays(2);
            p.DepartureDate = p.ArrivalDate.AddDays(1);
            {
                RoomParameter room = new RoomParameter();
                room.AdultsCount = 2;

                List<RoomParameter> roomList = new List<RoomParameter>();
                roomList.Add(room);
                p.Rooms = roomList.ToArray();
            }
            

            return p;
        }
    }
}
