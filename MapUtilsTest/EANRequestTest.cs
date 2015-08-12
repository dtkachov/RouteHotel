using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EANInterface;
using HotelInterface.TO;
using EANInterface.TransportObjects;
using CalculationUtils;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Collections.Specialized;


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
        public void TestEncodingWhileGettingDataFromEAN()
        {
            const string URL = "https://book.api.ean.com/ean-services/rs/hotel/v3/list?cid=55505&minorRev=99&apiKey=cbrzfta369qwyrm9t5b8y8kf&locale=uk_UA&currencyCode=USD&xml=%3CHotelListRequest%3E%0A%20%20%20%20%3Ccity%3ESeattle%3C%2Fcity%3E%0A%20%20%20%20%3CstateProvinceCode%3EWA%3C%2FstateProvinceCode%3E%0A%20%20%20%20%3CcountryCode%3EUS%3C%2FcountryCode%3E%0A%20%20%20%20%3CarrivalDate%3E8%2F24%2F2015%3C%2FarrivalDate%3E%0A%20%20%20%20%3CdepartureDate%3E8%2F26%2F2015%3C%2FdepartureDate%3E%0A%20%20%20%20%3CRoomGroup%3E%0A%20%20%20%20%20%20%20%20%3CRoom%3E%0A%20%20%20%20%20%20%20%20%20%20%20%20%3CnumberOfAdults%3E2%3C%2FnumberOfAdults%3E%0A%20%20%20%20%20%20%20%20%3C%2FRoom%3E%0A%20%20%20%20%3C%2FRoomGroup%3E%0A%20%20%20%20%3CnumberOfResults%3E25%3C%2FnumberOfResults%3E%0A%3C%2FHotelListRequest%3E";
            string responseStr = WebUtils.Utils.DownloadStringFromWeb(new Uri(URL));

            string fileName = @"d:\temp\HotelData\Encoding test.txt";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("тест кирилиці");
                writer.WriteLine(responseStr);
            }

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

        private void DumpData(HotelListResponse response)
        {
            string baseData = string.Format("Basic data: CacheKey: '{0}' CacheLocation: '{1}' CustomerSessionId: '{2}' MoreResultsAvailable: '{3}' NumberOfRoomsRequested: '{4}'", 
                response.CacheKey, response.CacheLocation, response.CustomerSessionId, response.MoreResultsAvailable, response.NumberOfRoomsRequested);

            string fileName = string.Format(@"d:\temp\HotelData\hotel reponse dump at {0}.txt", DateTime.Now.ToFileTime());

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(baseData);

                foreach (EANInterface.TransportObjects.HotelSummary hotel in response.Hotels.Hotels)
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

                writer.WriteLine("кінець");
            }

        }

        /// <summary>
        /// Returns multiple results
        /// </summary>
        /// <returns></returns>
        private HotelListParameters Buildparameters1()
        {
            HotelListParameters p = new HotelListParameters();
            p.HotelPreferences = new HotelPreference();

            p.HotelPreferences.Locale = "uk_UA";
            p.SearchRadius = 2000; // Meters
            p.SearchRadiusUnit = DistanceUnit.Meters;
            p.Location = new RouteTransportObjects.LatLng(49.8350004, 24.027728); // Lviv
            p.HotelPreferences.CurrencyCode = "USD";
            p.HotelPreferences.ArrivalDate = DateTime.Now.AddDays(2);
            p.HotelPreferences.DepartureDate = p.HotelPreferences.ArrivalDate.AddDays(1);
            {
                RoomParameter room = new RoomParameter();
                room.AdultsCount = 2;

                List<RoomParameter> roomList = new List<RoomParameter>();
                roomList.Add(room);
                p.HotelPreferences.Rooms = roomList.ToArray();
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
            p.HotelPreferences = new HotelPreference();

            p.HotelPreferences.Locale = "uk_UA";
            p.Location = new RouteTransportObjects.LatLng(50.5, 20.5);
            p.HotelPreferences.CurrencyCode = "USD";
            p.HotelPreferences.ArrivalDate = DateTime.Now.AddDays(2);
            p.HotelPreferences.DepartureDate = p.HotelPreferences.ArrivalDate.AddDays(1);
            {
                RoomParameter room = new RoomParameter();
                room.AdultsCount = 2;

                List<RoomParameter> roomList = new List<RoomParameter>();
                roomList.Add(room);
                p.HotelPreferences.Rooms = roomList.ToArray();
            }
            

            return p;
        }
    }
}
