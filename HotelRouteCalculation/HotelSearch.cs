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
        /// Hotels in current location
        /// </summary>
        private List<HotelData> Hotels;

        /// <summary>
        /// .ctoe
        /// </summary>
        /// <param name="point">Point to search hotels for</param>
        public HotelSearch(LinkedPoint point)
        {
            if (null == point) throw new ArgumentNullException("Argument point cannot be null");

            this.point = point;
        }

        /// <summary>
        /// Returns hotels in current location
        /// </summary>
        /// <returns>Hotels in current location</returns>
        public HotelData[] GetHotels()
        {
            return Hotels.ToArray();
        }

        /// <summary>
        /// Perform hotels search
        /// </summary>
        /// <returns>Count of hotels found</returns>
        public int Search()
        {
            Hotels = new List<HotelData>(); 
            // TODO - do hotel search here
            return Hotels.Count;
        }
    }
}
