using EANInterface.JsonNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EANInterface.TransportObjects
{
    /// <summary>
    /// Represents hotel list Response
    /// </summary>
    public class HotelListResponse
    {
        /// <summary>
        /// Response dynamic data
        /// </summary>
        private EANInterface.JsonNET.EANHotelListJsonTypes.HotelListResponse RawResponseData;

        public string CustomerSessionId { get; set; }
        public int NumberOfRoomsRequested { get; set; }
        public bool MoreResultsAvailable { get; set; }
        public string CacheKey { get; set; }
        public string CacheLocation { get; set; }

        public HotelList Hotels { get; set; }

        /// <summary>
        /// Whetehr any hotel has beedn found
        /// </summary>
        public bool AnyHotelFound 
        { 
            get 
            { 
                return (null != Hotels) && (Hotels.Hotels.Length > 0); 
            } 
        }

        internal HotelListResponse(EANHotelList data)
        {
            RawResponseData = data.HotelListResponse;
            ParseData();
        }

        /// <summary>
        /// Parses Response data
        /// </summary>
        private void ParseData()
        {
            CustomerSessionId = RawResponseData.CustomerSessionId;
            NumberOfRoomsRequested = RawResponseData.NumberOfRoomsRequested;
            MoreResultsAvailable = RawResponseData.MoreResultsAvailable;
            CacheKey = RawResponseData.CacheKey;
            CacheLocation = RawResponseData.CacheLocation;

            if (null != RawResponseData.HotelList)
            {
                Hotels = new HotelList(RawResponseData.HotelList);
            }
        }

    }
}
