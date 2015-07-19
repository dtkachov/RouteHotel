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
        private dynamic ResponseData;

        public string CustomerSessionId { get; set; }
        public int NumberOfRoomsRequested { get; set; }
        public bool MoreResultsAvailable { get; set; }
        public string CacheKey { get; set; }
        public string CacheLocation { get; set; }

        public HotelList Hotels { get; set; }

        public HotelListResponse(dynamic data)
        {
            ResponseData = data.HotelListResponse;
        }

        /// <summary>
        /// Parses Response data
        /// </summary>
        public void ParseData()
        {
            CustomerSessionId = ResponseData.@customerSessionId;
            NumberOfRoomsRequested = (int) (ResponseData.@numberOfRoomsRequested);
            MoreResultsAvailable = ResponseData.@moreResultsAvailable;
            CacheKey = ResponseData.@cacheKey;
            CacheLocation = ResponseData.@cacheLocation;

            Hotels = new HotelList(ResponseData);
            Hotels.Parce();
        }

    }
}
