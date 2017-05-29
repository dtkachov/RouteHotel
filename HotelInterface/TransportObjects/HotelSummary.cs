using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelInterface.TO
{
    /// <summary>
    /// Transport object for hotel summary
    /// 
    /// TODO - after adding another provider try to make fields more generic and morve specific fields to rach provider
    /// </summary>
    public class HotelSummary
    {
        #region data sample
        /*
                            "hotelId": 115072,
                    "name": "Fairmont Olympic Hotel",
                    "address1": "411 University St",
                    "city": "Seattle",
                    "stateProvinceCode": "WA",
                    "postalCode": 98101,
                    "countryCode": "US",
                    "airportCode": "SEA",
                    "supplierType": "E",
                    "propertyCategory": 1,
                    "hotelRating": 5,
                    "confidenceRating": 52,
                    "amenityMask": 7828291,
                    "tripAdvisorRating": 4.5,
                    "tripAdvisorReviewCount": 1892,
                    "tripAdvisorRatingUrl": "http://www.tripadvisor.com/img/cdsi/img2/ratings/traveler/4.5-12345-4.gif",
                    "locationDescription": "Поруч: Pike Place Market",
                    "shortDescription": "&lt;p&gt;&lt;b&gt;Розташування готелю&lt;/b&gt; &lt;br /&gt;Якщо ви зупинитесь у Fairmont Olympic Hotel, то опинитесь в центрі міста Сієттл, Театр 5th Avenue буде за кілька кроків від вас, а Pike Place Market - за кілька",
                    "highRate": 369,
                    "lowRate": 313.65,
                    "rateCurrencyCode": "USD",
                    "latitude": 47.60831,
                    "longitude": -122.33481,
                    "proximityDistance": 11.162396,
                    "proximityUnit": "MI",
                    "hotelInDestination": true,
                    "thumbNailUrl": "/hotels/1000000/30000/20300/20230/20230_115_t.jpg",
                    "deepLink": "http://www.travelnow.com/templates/55505/hotels/115072/overview?lang=uk&amp;currency=USD&amp;standardCheckin=8/17/2015&amp;standardCheckout=8/19/2015&amp;roomsCount=1&amp;rooms[0].adultsCount=2",
         */
        #endregion

        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string StateProvinceCode { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string AirportCode { get; set; }
        public string SupplierType { get; set; }
        public double PropertyCategory { get; set; }
        public double HotelRating { get; set; }
        public double AgencyRating { get; set; }

        public double ConfidenceRating { get; set; }
        public double AmenityMask { get; set; }
        public int AgencyReviewCount { get; set; }
        public string AgencyRatingUrl { get; set; }

        public string LocationDescription { get; set; }
        public string ShortDescription { get; set; }
        public double HighRate { get; set; }
        public double LowRate { get; set; }
        public string RateCurrencyCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double ProximityDistance { get; set; }
        public string ProximityUnit { get; set; }

        public string ThumbNailUrl { get; set; }
        public string DeepLink { get; set; }

        public RoomRateDetails[] RoomRates;

        public HotelSummary() { }
    }
}
