using System;
using System.Collections.Generic;
using System.Text;

namespace CalculationUtils
{
    /// <summary>
    /// Utils for distnce calculations
    /// </summary>
    public class DistanceUtils
    {
        /// <summary>
        /// Converts meters to kilimoters
        /// </summary>
        /// <param name="meters">Meters value</param>
        /// <returns>Kilometers value</returns>
        public static double MetersToKm(double meters)
        {
            return meters / CONSTS.METERS_IN_KM;
        }

        /// <summary>
        /// This routine calculates the distance between two points
        /// </summary>
        /// <param name="latitude1">From point latitude</param>
        /// <param name="longitude1">To point longitude</param>
        /// <param name="latitude2">From point latitude</param>
        /// <param name="longitude2">To point longitude</param>
        /// <returns>Distamce in kilometers</returns>
        public static double Distance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            // just redirect to well known method
            return Distance1(latitude1, longitude1, latitude2, longitude2, CONSTS.DEFAULT_DISTANCE_UNIT);
        }

        /// <summary>
        /// This routine calculates the distance between two points
        /// </summary>
        /// <param name="latitude1">From point latitude</param>
        /// <param name="longitude1">To point longitude</param>
        /// <param name="latitude2">From point latitude</param>
        /// <param name="longitude2">To point longitude</param>
        /// <param name="unit">The unit you desire for results </param>
        /// <returns>Distamce in kilometers</returns>
        public static double Distance(double latitude1, double longitude1, double latitude2, double longitude2, DistanceUnit unit)
        {
            // just redirect to well known method
            return Distance1(latitude1, longitude1, latitude2, longitude2, unit);
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
        /// <param name="latitude1">From point latitude</param>
        /// <param name="longitude1">To point longitude</param>
        /// <param name="latitude2">From point latitude</param>
        /// <param name="longitude2">To point longitude</param>
        /// <param name="unit">The unit you desire for results </param>
        /// <returns>Distamce in kilometers</returns>
        private static double Distance1(double latitude1, double longitude1, double latitude2, double longitude2, DistanceUnit unit)
        {
            const double CIRCUMFERENCE = CONSTS.CIRCUMFERENCE_KM * CONSTS.METERS_IN_KM; // Earth's circumference at the equator in meters

            double distance = 0.0;

            //Calculate radians
            double latitude1Rad = Deg2Rad(latitude1);
            double longitude1Rad = Deg2Rad(longitude1);
            double latititude2Rad = Deg2Rad(latitude2);
            double longitude2Rad = Deg2Rad(longitude2);

            double logitudeDiff = Math.Abs(longitude1Rad - longitude2Rad);

            if (logitudeDiff > Math.PI)
            {
                logitudeDiff = 2.0 * Math.PI - logitudeDiff;
            }

            double angleCalculation =
                Math.Acos(
                  Math.Sin(latititude2Rad) * Math.Sin(latitude1Rad) +
                  Math.Cos(latititude2Rad) * Math.Cos(latitude1Rad) * Math.Cos(logitudeDiff));
            if (double.IsNaN(angleCalculation))
            {
                const double LAT_LON_ACCURACY = 0.0000001; // accuracy to consider numbers equal
                bool latSame = LAT_LON_ACCURACY > Math.Abs(latitude1 - latitude2);
                bool lonSame = LAT_LON_ACCURACY > Math.Abs(longitude1 - longitude2);

                if (latSame && lonSame) angleCalculation = 0;
            }

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
