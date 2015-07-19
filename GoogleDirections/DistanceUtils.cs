using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDirections
{
    /// <summary>
    /// Utils for transport objects
    /// </summary>
    public class DistanceUtils
    {
        /// <summary>
        /// This routine calculates the distance between two points 
        /// (given the latitude/longitude of those points). 
        /// It is being used to calculate  the distance between two locations using 
        /// GeoDataSource(TM) products 
        /// South latitudes are negative, east longitudes are positive  
        /// </summary>
        /// <param name="from">From point</param>
        /// <param name="to">To point</param>
        /// <returns>Distamce in meters</returns>
        public static double Distance(LatLng from, LatLng to)
        {
            return CalculationUtils.DistanceUtils.Distance(from.Latitude, from.Longitude, to.Latitude, to.Longitude, CalculationUtils.CONSTS.DEFAULT_DISTANCE_UNIT);
        }
    }
}
