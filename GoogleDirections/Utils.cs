using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleDirections
{
    /// <summary>
    /// Utils for Google directions
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Distance value enum
        /// </summary>
        public enum DistanceUnit
        {
            /// <summary>
            /// Meters
            /// </summary>
            Meters,
            /// <summary>
            /// Miles
            /// </summary>
            Miles,
            /// <summary>
            /// Nautical miles
            /// </summary>
            NauticalMiles
        }

        /// <summary>
        /// Count of meters in kilometer
        /// </summary>
        public const short METERS_IN_KM = 1000;

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
            return Distance(from, to, DistanceUnit.Meters);
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
        private static double Distance(LatLng from, LatLng to, DistanceUnit unit)
        {
            // call correct method from here
            //DistanceSinCos(from, to, unit);
            //return Distance_geodatasource(from, to, unit);
            return Distance1(from, to, unit);
        }
         
        /// <summary>
        /// Сonverts decimal degrees to radians
        /// </summary>
        private static double Deg2Rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        /// <summary>
        /// converts radians to decimal degrees
        /// </summary>
        private static double Rad2Deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        /// <summary>
        /// This routine calculates the distance between two points
        /// 
        /// code taken from  http://dotnet-snippets.com/snippet/calculate-distance-between-gps-coordinates/677
        /// </summary>
        /// <param name="from">From point</param>
        /// <param name="to">To point</param>
        /// <param name="unit">The unit you desire for results </param>
        /// <returns>Distamce in kilometers</returns>
        private static double Distance1(LatLng from, LatLng to, DistanceUnit unit)
        {
            const double CIRCUMFERENCE_KM = 40000.0; // Earth's circumference at the equator in km
            const double CIRCUMFERENCE = CIRCUMFERENCE_KM * METERS_IN_KM; // Earth's circumference at the equator in meters

            double distance = 0.0;

            //Calculate radians
            double latitude1Rad = Deg2Rad(from.Latitude);
            double longitude1Rad = Deg2Rad(from.Longitude);
            double latititude2Rad = Deg2Rad(to.Latitude);
            double longitude2Rad = Deg2Rad(to.Longitude);

            double logitudeDiff = Math.Abs(longitude1Rad - longitude2Rad);

            if (logitudeDiff > Math.PI)
            {
                logitudeDiff = 2.0 * Math.PI - logitudeDiff;
            }

            double angleCalculation =
                Math.Acos(
                  Math.Sin(latititude2Rad) * Math.Sin(latitude1Rad) +
                  Math.Cos(latititude2Rad) * Math.Cos(latitude1Rad) * Math.Cos(logitudeDiff));

            distance = CIRCUMFERENCE * angleCalculation / (2.0 * Math.PI);

            if (DistanceUnit.Miles == unit)
            {
                const double METERS_TO_MILES = 0.000621371192;
                distance *= METERS_TO_MILES;
            }
            else if (DistanceUnit.NauticalMiles == unit)
            {
                const double METERS_TO_NAUTICAL_MILES_TO_KM = 0.000539956803;
                distance *= METERS_TO_NAUTICAL_MILES_TO_KM;
            }

            return distance;
        }

    }
}
