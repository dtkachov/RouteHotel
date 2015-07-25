using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EANInterface.TransportObjects
{
    /// <summary>
    /// Represents EAN specific hotel list Response
    /// </summary>
    public class HotelList : HotelInterface.TransportObjects.HotelList
    {
        /// <summary>
        /// Response dynamic data
        /// </summary>
        private EANInterface.JsonNET.EANHotelListJsonTypes.HotelList RawResponseData;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="ResponseData">Response data to be parced</param>
        internal HotelList(EANInterface.JsonNET.EANHotelListJsonTypes.HotelList responseData)
        {
            RawResponseData = responseData;
            ParseData();
        }

        /// <summary>
        /// Parces responce
        /// </summary>
        private void ParseData()
        {
            List<HotelSummary> hotels = new List<HotelSummary>();
            foreach (EANInterface.JsonNET.EANHotelListJsonTypes.HotelSummary rawHotelSummary in RawResponseData.HotelSummary)
            {
                HotelSummary hotel = new HotelSummary(rawHotelSummary);
                hotels.Add(hotel);
            }

            this.Hotels = hotels.ToArray();
        }


    }
}
