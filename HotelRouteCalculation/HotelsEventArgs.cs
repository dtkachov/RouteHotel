using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Event that fires and notifies about new hotels found
    /// </summary>
    public class HotelsEventArgs : EventArgs
    {
        /// <summary>
        /// List of hotels fount
        /// </summary>
        public HotelInterface.TO.HotelSummary[] Hotels 
        {
            get { return _hotels; }
        }
        private HotelInterface.TO.HotelSummary[] _hotels;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="hotels">List of hotels</param>
        public HotelsEventArgs(HotelInterface.TO.HotelSummary[] hotels)
        {
            this._hotels = hotels;
        }
    }
}
