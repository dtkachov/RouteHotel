using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EANInterface.TransportObjects
{
    /// <summary>
    /// Details for the room rate
    /// </summary>
    public class RoomRateDetails : HotelInterface.TransportObjects.RoomRateDetails
    {
        /// <summary>
        /// Response dynamic data
        /// </summary>
        private EANInterface.JsonNET.EANHotelListJsonTypes.RoomRateDetails RawResponseData;

        internal RoomRateDetails(EANInterface.JsonNET.EANHotelListJsonTypes.RoomRateDetails responseData)
        {
            this.RawResponseData = responseData;
            ParseData();
        }

        /// <summary>
        /// Parses Response data
        /// </summary>
        private void ParseData()
        {
            this.MaxRoomOccupancy = RawResponseData.MaxRoomOccupancy;
            this.QuotedRoomOccupancy = RawResponseData.QuotedRoomOccupancy;
            this.MinGuestAge = RawResponseData.MinGuestAge;
            this.RoomDescription = RawResponseData.RoomDescription;
            this.PropertyAvailable = RawResponseData.PropertyAvailable;
            this.PropertyRestricted = RawResponseData.PropertyRestricted;

            ParseRate();
        }

        private void ParseRate()
        {
            EANInterface.JsonNET.EANHotelListJsonTypes.ChargeableRateInfo chargeableRateInfo = RawResponseData.RateInfos.RateInfo.ChargeableRateInfo;

            this.Price = Double.Parse(chargeableRateInfo.Total, CultureInfo.InvariantCulture);
            this.Currency = chargeableRateInfo.CurrencyCode;

        }
    }
}
