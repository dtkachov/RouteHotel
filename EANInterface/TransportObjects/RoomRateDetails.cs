using System;
using System.Collections.Generic;
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
        private dynamic ResponseData;

        public RoomRateDetails(dynamic responseData)
        {
            this.ResponseData = responseData;
            ParseData();
        }

        /// <summary>
        /// Parses Response data
        /// </summary>
        public void ParseData()
        {
            this.MaxRoomOccupancy = (int)ResponseData.maxRoomOccupancy;
            this.QuotedRoomOccupancy = (int)ResponseData.quotedRoomOccupancy;
            this.MinGuestAge = (int)ResponseData.minGuestAge;
            this.RoomDescription = ResponseData.roomDescription;
            this.PropertyAvailable = ResponseData.propertyAvailable;
            this.PropertyRestricted = ResponseData.propertyRestricted;

            ParseRate();
        }

        private void ParseRate()
        {
            // parse rate 
            var rateInfos = ResponseData.RateInfos;
            var rateInfo = (rateInfos.IsArray) ? rateInfos[0] : rateInfos.RateInfo;

            var chargeableRateInfo = rateInfo.ChargeableRateInfo;

            string itemName = string.Empty;
            foreach (var item in chargeableRateInfo)
            {
                    itemName = item.Key.ToString() + " - " + item.Value.ToString();  
            }

            this.Price = chargeableRateInfo.total;
            this.Currency = chargeableRateInfo.currencyCode;

        }
    }
}
