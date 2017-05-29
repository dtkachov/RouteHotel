using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EANInterface
{
    /// <summary>
    /// Keys for request parameters
    /// </summary>
    public class RequestParametersKeys
    {
        public const string API_KEY = "apiKey";
        public const string CID = "cid";
        public const string MINOR_REV = "minorRev";
        public const string LOCALE = "locale";
        public const string CURRENCY_CODE = "currencyCode";
        public const string LATITUDE = "latitude";
        public const string LONGITUDE = "longitude";
        public const string ARRIVAL_DATE = "arrivalDate";
        public const string DEPARTURE_DATE = "departureDate";
        public const string ROOM = "room";
        public const string SEARCH_RADIUS = "searchRadius";
        public const string SEARCH_RADIUS_UNIT = "searchRadiusUnit";
        
        public const string DATE_TIME_FORMAT = "MM/dd/yyyy";

        public static CultureInfo Culture { get { return CultureInfo.InvariantCulture; } }
    }
}
