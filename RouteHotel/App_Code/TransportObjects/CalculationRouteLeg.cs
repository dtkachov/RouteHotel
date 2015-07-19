using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteHotel.TransportObjects
{
    /// <summary>
    /// Class representing ropute leg consisting of calculation points
    /// Point order does matter. Each next poitn is next in linked point collection.
    /// This call ise used to replace initial idea of linked list since in case if 
    /// list of linked points is long it cannot be transfered through web service to client
    /// </summary>
    public class CalculationRouteLeg : RouteTransportObjects.CalculationRouteLeg
    {

        /// <summary>
        /// Defauls .ctor for WS
        /// </summary>
        public CalculationRouteLeg()
        { 
        }

        /// <summary>
        /// Copy .ctor
        /// Copies data from route leg
        /// </summary>
        /// <param name="firstLegPoint">Linked point represents first point of leg to copy data from to this transport object</param>
        public CalculationRouteLeg(HotelRouteCalculation.LinkedPoint firstLegPoint)
        { 
            if (null == firstLegPoint) throw new ArgumentNullException("Argument leg is not expected to be null");
            CopyData(firstLegPoint);
        }

        /// <summary>
        /// Copies data from leg represented by first point object provided into current one
        /// </summary>
        /// <param name="firstLegPoint">Linked point represents first point of leg to copy data from to this transport object</param>
        private void CopyData(HotelRouteCalculation.LinkedPoint firstLegPoint)
        {           
            List<CalculationPoint> pointList = new List<CalculationPoint>();
            pointList.Add(new CalculationPoint(firstLegPoint));

            HotelRouteCalculation.LinkedPoint currentPoint = firstLegPoint;
            while (!currentPoint.IsLast) 
            {
                currentPoint = currentPoint.Next;
                CalculationPoint point = new CalculationPoint(currentPoint);
                pointList.Add(point);
            }

            Points = pointList.ToArray();
        }
    }
}