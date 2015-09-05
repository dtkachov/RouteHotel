using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelInterface.TO
{
    /// <summary>
    /// User preference for hotel (price, stars, review, etc)
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

        /// <summary>
        /// .ctor 
        /// </summary>
        public HotelPreference() { }

        public override string ToString()
        {
            string roomsStr = string.Empty;
            for(int i = 0; i < Rooms.Length; ++i)
            {
                RoomParameter room = Rooms[i];
                if (i > 0) roomsStr += ", ";
                roomsStr += string.Format("<{0}>'{1}'", i, room );
            }

            string result = string.Format("Locale: '{0}' ArrivalDate: '{1}' DepartureDate: {2}, CurrencyCode: {3}, Rooms: '{4}'", Locale, ArrivalDate, DepartureDate, CurrencyCode, roomsStr);

            return result;
        }
    }
}
