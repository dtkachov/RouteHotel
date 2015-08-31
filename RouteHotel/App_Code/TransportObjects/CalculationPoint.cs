using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteHotel.TransportObjects
{
    /// <summary>
    /// Represents transport object used to transmit data about calculation points
    /// It is not linked list as objects used internally in libraries as long list 
    /// cannot be passed over WS due to stack overflow cause by recursion creating objects
    /// </summary>
    public class CalculationPoint : RouteTransportObjects.CalculationPoint
    {


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


        
    }
}