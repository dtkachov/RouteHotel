using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EANInterface.TransportObjects
{
    /// <summary>
    /// Represents EAN hotel specific hotel summary
    /// </summary>
    public class HotelSummary : HotelInterface.TO.HotelSummary
    {
        /// <summary>
        /// Response dynamic data
        /// </summary>
        private EANInterface.JsonNET.EANHotelListJsonTypes.HotelSummary RawResponseData;

        internal HotelSummary(EANInterface.JsonNET.EANHotelListJsonTypes.HotelSummary responseData)
        {
            this.RawResponseData = responseData;
            ParseData();
        }

        /// <summary>
        /// Parses Response data
        /// </summary>
        private void ParseData()
        {
            this.HotelId = RawResponseData.HotelId;
            this.Name = RawResponseData.Name;
            this.HotelRating = RawResponseData.HotelRating;
            this.Address1 = RawResponseData.Address1;
            this.AgencyRating = RawResponseData.TripAdvisorRating;
            this.AirportCode = RawResponseData.AirportCode;
            this.City = RawResponseData.City;
            this.CountryCode = RawResponseData.CountryCode;
            this.PostalCode = RawResponseData.PostalCode;
            this.PropertyCategory = RawResponseData.PropertyCategory;
            this.StateProvinceCode = RawResponseData.StateProvinceCode;
            this.SupplierType = RawResponseData.SupplierType;

            this.ConfidenceRating = RawResponseData.ConfidenceRating;
            this.AmenityMask = RawResponseData.AmenityMask;

            this.AgencyReviewCount = RawResponseData.TripAdvisorReviewCount;
            this.AgencyRatingUrl = RawResponseData.TripAdvisorRatingUrl;

            this.LocationDescription = RawResponseData.LocationDescription;
            this.ShortDescription = RawResponseData.ShortDescription;
            this.HighRate = RawResponseData.HighRate;
            this.LowRate = RawResponseData.LowRate;
            this.RateCurrencyCode = RawResponseData.RateCurrencyCode;
            this.Latitude = RawResponseData.Latitude;
            this.Longitude = RawResponseData.Longitude;
            this.ProximityDistance = RawResponseData.ProximityDistance;
            this.ProximityUnit = RawResponseData.ProximityUnit;

            this.ThumbNailUrl = RawResponseData.ThumbNailUrl;
            this.DeepLink = RawResponseData.DeepLink;

            ParseRoomRates();
        }

        private void ParseRoomRates()
        {
            List<RoomRateDetails> roomRateDetails = new List<RoomRateDetails>();

            {
                // TODO It might be a list - check it 
                RoomRateDetails rateDetails = new RoomRateDetails(RawResponseData.RoomRateDetailsList.RoomRateDetails);
                roomRateDetails.Add(rateDetails);
            }

            this.RoomRates = roomRateDetails.ToArray();
        }
    }
}
