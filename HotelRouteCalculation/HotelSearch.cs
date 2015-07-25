using HotelInterface;
using HotelInterface.TransportObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Utilizes hotel search routines
    /// </summary>
    public class HotelSearch
    {
        /// <summary>
        /// Point to search hotels for
        /// </summary>
        public LinkedPoint Point
        {
            get { return point; }
        }
        private LinkedPoint point;

        /// <summary>
        /// Represents hotel search criterias
        /// </summary>
        public HotelListParameters HotelParameters
        {
            get
            {
                return _hotelParameters;
            }
        }
        private HotelListParameters _hotelParameters;

        /// <summary>
        /// Hotels in current location
        /// </summary>
        private List<HotelSummary> Hotels;

        /// <summary>
        /// .ctoe
        /// </summary>
        /// <param name="point">Point to search hotels for</param>
        /// <param name="hotelParameters">Represents hotel search criterias</param>
        public HotelSearch(LinkedPoint point, HotelListParameters hotelParameters)
        {
            if (null == point) throw new ArgumentNullException("Argument point cannot be null");
            if (null == hotelParameters) throw new ArgumentNullException("Argument hotelParameters cannot be null");

            _hotelParameters = hotelParameters;
            this.point = point;
        }

        /// <summary>
        /// Returns hotels in current location
        /// </summary>
        /// <returns>Hotels in current location</returns>
        public HotelSummary[] GetHotels()
        {
            return Hotels.ToArray();
        }

        /// <summary>
        /// Perform hotels search
        /// </summary>
        /// <returns>Count of hotels found</returns>
        public int Search()
        {
            Hotels = new List<HotelSummary>();

            IHotelListRequest[] requests = HotelSearchFactory.Instance.CreateSearchRequestObjects(HotelParameters);

            foreach (IHotelListRequest request in requests)
            {
                HotelList result = request.Request();
                Hotels.AddRange(result.Hotels);
            }

            return Hotels.Count;
        }


    }
}
