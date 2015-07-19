
using CalculationUtils;
using HotelInterface.TransportObjects;
using RouteTransportObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace EANInterface
{
    /// <summary>
    /// Represents a builder for Hotel list request interface
    /// </summary>
    public class HotelListRequestParametersBuilder
    {
        #region request XML
        /*
<HotelListRequest>
        <latitude>50.43444</latitude>
        <longitude>20.34555</longitude>

    <arrivalDate>8/17/2015</arrivalDate>
    <departureDate>8/19/2015</departureDate>
    <RoomGroup>
        <Room>
            <numberOfAdults>2</numberOfAdults>
            <numberOfChildren>0</numberOfChildren>
            <childAges>2,5</childAges>
        </Room>
    </RoomGroup>
    <numberOfResults>25</numberOfResults>
    <includeDetails>no</includeDetails>
    <includeHotelFeeBreakdown>no</includeHotelFeeBreakdown>
</HotelListRequest>
         * 
 Example REST Availability Request for 2 Rooms (2 Adults, 2 Children Ages 3 and 5) {#2}
         http://api.ean.com/ean-services/rs/hotel/v3/list?
apiKey=#####
&cid=#####
&sig=####################
&customerIpAddress=#####
&customerUserAgent=######
&customerSessionId=#####
&minorRev=##
&locale=en_US
&currencyCode=USD
&city=Seattle
&stateProvinceCode=WA
&countryCode=US
&arrivalDate=09/04/2015
&departureDate=09/05/2015
&room1=1,3
&room2=1,5
         *
sample: http://api.ean.com/ean-services/rs/hotel/v3/list?apiKey=cbrzfta369qwyrm9t5b8y8kf&cid=55505&minorRev=99&locale=uk_UA&currencyCode=USD&latitude=50.43444&longitude=20.34555&arrivalDate=08/03/2015&departureDate=08/04/2015&room1=2
         * */


        #endregion

        /// <summary>
        /// List of hotel request parameters
        /// </summary>
        public HotelListParameters Params
        {
            get { return _params; } 
        }
        private HotelListParameters _params;

        /// <summary>
        /// Basic request data
        /// </summary>
        private const string REQUEST = "http://api.ean.com/ean-services/rs/hotel/v3/list?apiKey={0}&cid={1}&minorRev={2}";

        /// <summary>
        /// Temporary object for bulding the request
        /// </summary>
        private string ResultURL = string.Format(REQUEST, EANCommon.Instance.APIKey, EANCommon.Instance.CID, EANCommon.Instance.MinorRev);

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="parameters">Parameters</param>
        public HotelListRequestParametersBuilder(HotelListParameters parameters)
        {
            this._params = parameters;
        }

        /// <summary>
        /// Builds URL based on parameters supplied
        /// </summary>
        /// <returns>URL</returns>
        public Uri Build()
        {
            CheckParametersValidyty();
            BuildInt();
            return new Uri(ResultURL);
        }

        /// <summary>
        /// Checks whether paramers are valid (required parameters in place)
        /// Throws exceptions if parameters are invalid
        /// </summary>
        private void CheckParametersValidyty()
        {
            if (null == Params.Locale || 0 == Params.Locale.Trim().Length) throw new InvalidOperationException("Parameter locale is not specified");
            if (null == Params.Location) throw new InvalidOperationException("Parameter Location is not specified");
            if (null == Params.Rooms || 0 == Params.Rooms.Length) throw new InvalidOperationException("Parameter rooms  is not specified");
            
        }

        /// <summary>
        /// Builds URL based on parameters supplied
        /// </summary>
        private void BuildInt()
        {
            AddParameterToUrl(RequestParametersKeys.LOCALE, Params.Locale);
            AddParameterToUrl(RequestParametersKeys.CURRENCY_CODE, Params.CurrencyCode);
            
            AddParameterToUrl(RequestParametersKeys.LATITUDE, Params.Location.Latitude.ToString(RequestParametersKeys.Culture));
            AddParameterToUrl(RequestParametersKeys.LONGITUDE, Params.Location.Longitude.ToString(RequestParametersKeys.Culture));

            if (Params.SearchRadius > 0)
            {
                int val = Params.SearchRadius;
                if (DistanceUnit.Meters == Params.SearchRadiusUnit)
                {
                    val = MetersToKm(val);
                }
                AddParameterToUrl(RequestParametersKeys.SEARCH_RADIUS, val.ToString());
                AddParameterToUrl(RequestParametersKeys.SEARCH_RADIUS_UNIT, GetUnitsStr(Params.SearchRadiusUnit));
            }
            
            AddParameterToUrl(RequestParametersKeys.ARRIVAL_DATE, Params.ArrivalDate.ToString(RequestParametersKeys.DATE_TIME_FORMAT, RequestParametersKeys.Culture));
            AddParameterToUrl(RequestParametersKeys.DEPARTURE_DATE, Params.DepartureDate.ToString(RequestParametersKeys.DATE_TIME_FORMAT, RequestParametersKeys.Culture));
            AddRoomParameters();
        }

        private static string GetUnitsStr(DistanceUnit unit)
        {
            switch (unit)
            {
                case DistanceUnit.Meters:
                    return "KM";
                case DistanceUnit.Miles:
                    return "MI";
                default:
                case DistanceUnit.NauticalMiles:
                    throw new ApplicationException("Not supported value: NauticalMiles");
            }
        }

        private static int MetersToKm(int meters)
        {
            double km = CalculationUtils.DistanceUtils.MetersToKm(meters);
            int result = (int)Math.Ceiling(km);
            return result;
        }

        /// <summary>
        /// Adds new parameter to URL
        /// </summary>
        /// <param name="key">Parameter Key</param>
        /// <param name="value">Parameters value</param>
        private void AddParameterToUrl(string key, string value)
        {
            if (null == key || 0 == key.Trim().Length) throw new ArgumentNullException("Argument key cannot be null");
            if (null == value || 0 == value.Trim().Length) throw new ArgumentNullException("Argument value cannot be null");

            string urlPart = string.Format("&{0}={1}", key, value);
            ResultURL += urlPart;
        }

        /// <summary>
        /// Adds room parameters
        /// </summary>
        private void AddRoomParameters()
        {
            for (int i = 0; i < Params.Rooms.Length; ++i)
            {
                RoomParameter room = Params.Rooms[i];
                if (null == room) throw new InvalidOperationException(string.Format("Room {0} is null. Room cannot be null", i));

                string adultsKey = RequestParametersKeys.ROOM + (i + 1).ToString();
                string adultsVal = room.AdultsCount.ToString();

                if (null != room.Childrens && room.Childrens.Length > 0)
                {
                    string childAges = string.Empty;
                    for (int j = 0; i < room.Childrens.Length; ++i)
                    {
                        childAges += "," + room.Childrens[j].ToString();
                    }
                    adultsVal += childAges;
                }

                AddParameterToUrl(adultsKey, adultsVal);
            }
        }
    }
}
