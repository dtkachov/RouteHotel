using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelInterface.TransportObjects
{
    /// <summary>
    /// User [preference for hotel (price, stars, review, etc)
    /// </summary>
    public class HotelPreference
    {
        /// <summary>
        /// Locale for Response.
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Date of arrival for hotel search
        /// </summary>
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Date of departure for hotel search
        /// </summary>
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Code of currency to present data in
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Represents rooms array
        /// </summary>
        public RoomParameter[] Rooms { get; set; }
    }
}
