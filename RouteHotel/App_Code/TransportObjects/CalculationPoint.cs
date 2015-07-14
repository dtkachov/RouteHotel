using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteHotel.TransportObjects
{
    /// <summary>
    /// Represents transport object ued to tarnsmit data about calculation points
    /// It is not linked list as objects used internally in libraries as long list 
    /// cannot be passed over WS due to stack overflow cause by recursion creating objects
    /// </summary>
    public class CalculationPoint
    {
        /// <summary>
        /// Point on a map
        /// </summary>
        public LatLng Point
        {
            get;
            set;
        }

        /// <summary>
        /// Returns true if the poin were not originally in list and were introduced during route optimization
        /// </summary>
        public bool IsIntroduced
        {
            get;
            set;
        }

        /// <summary>
        /// Returns distance to the next point
        /// </summary>
        public double Distance
        {
            get;
            set;
        }

        /// <summary>
        /// Returns distance to the original point
        /// If original point is the same as next point the value equal to "Distance"
        /// </summary>
        public double OriginalDistance
        {
            get;
            set;
        }

        /// <summary>
        /// default .ctor for object transmition
        /// </summary>
        public CalculationPoint()
        { 
        }

        /// <summary>
        /// Copy .ctor
        /// </summary>
        /// <param name="point">Point to copy data from to this transport object</param>
        public CalculationPoint(HotelRouteCalculation.LinkedPoint point)
        {
            if (null == point) throw new ArgumentNullException("Argument point is not expected to be null");
            CopyData(point);
        }


        /// <summary>
        /// Copies data from point object provided into current one
        /// </summary>
        /// <param name="point">Point to copy data from to this transport object</param>
        private void CopyData(HotelRouteCalculation.LinkedPoint point)
        {
            this.Point = new LatLng(point.Point);
            this.IsIntroduced = point.IsIntroduced;
            this.Distance = point.Distance;
            this.OriginalDistance = point.OriginalDistance;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string result = string.Format(
                "point: {0}, is introduced: {1}, Distance: {2}, OriginalDistance: {3}, is last: {4}", 
                Point.ToString(), 
                IsIntroduced, 
                Distance, 
                OriginalDistance
                );

            return result;
        }
        
    }
}