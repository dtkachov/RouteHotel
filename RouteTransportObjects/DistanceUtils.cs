using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RouteTransportObjects
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
            return Distance(from, to, CalculationUtils.CONSTS.DEFAULT_DISTANCE_UNIT);
        }

        /// <summary>
        /// This routine calculates the distance between two points 
        /// (given the latitude/longitude of those points). 
        /// It is being used to calculate  the distance between two locations using 
        /// GeoDataSource(TM) products 
        /// South latitudes are negative, east longitudes are positive  
        /// </summary>
        /// <param name="from">From point</param>
        /// <param name="to">To point</param>
        /// <param name="unit">The unit you desire for results </param>
        /// <returns>Distamce in unit selected</returns>
        private static double Distance(LatLng from, LatLng to, CalculationUtils.DistanceUnit unit)
        {
            // call correct method from here
            //DistanceSinCos(from, to, unit);
            //return Distance_geodatasource(from, to, unit);
            return CalculationUtils.DistanceUtils.Distance(from.Latitude, from.Longitude, to.Latitude, to.Longitude, unit);
        }


    }
}
