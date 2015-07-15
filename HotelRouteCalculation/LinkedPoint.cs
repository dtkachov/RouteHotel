using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleDirections;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Represents linked point that later might be used for calculation
    /// of hotels arround
    /// </summary>
    public class LinkedPoint
    {
        /// <summary>
        /// Point on a map
        /// </summary>
        public LatLng Point
        {
            get { return point;  }
        }
        private LatLng point;

        /// <summary>
        /// Next point in list
        /// </summary>
        public LinkedPoint Next
        {
            get { return next; }
            set { SetNextPoint(value); }
        }
        private LinkedPoint next;

        /// <summary>
        /// Originally next point in list
        /// Might be used to understand weather some points were ommited 
        /// or added in between the original two returned by map aggregator
        /// </summary>
        public LinkedPoint OriginalNext
        {
            get { return originalNext; }
        }
        private LinkedPoint originalNext;

        /// <summary>
        /// Returns true if the poin were not originally in list and were introduced during route optimization
        /// </summary>
        public bool IsIntroduced
        {
            get
            {
               return _isIntroduced;
            }
        }
        private bool _isIntroduced = false;

        /// <summary>
        /// Indicates whether point is the last in list
        /// </summary>
        public bool IsLast
        {
            get { return null == next; }
        }

        /// <summary>
        /// Returns distance to the next point
        /// </summary>
        public double Distance
        {
            get { return distance; }
        }
        private double distance;

        /// <summary>
        /// Returns distance to the original point
        /// If original point is the same as next point the value equal to "Distance"
        /// </summary>
        public double OriginalDistance
        {
            get { return originalDistance; }
        }
        private double originalDistance;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="point">Point on a map</param>
        public LinkedPoint(LatLng point)
        {
            this.point = point;
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="point">Point on a map</param>
        /// <param name="nextPoint">Sets next point</param>
        public LinkedPoint(LatLng point, LinkedPoint nextPoint)
        {
            this.point = point;
            SetNextPoint(nextPoint);
        }

        /// <summary>
        /// Constructs introduced point
        /// </summary>
        /// <param name="point">Point on a map</param>
        /// <param name="nextPoint">Sets next point</param>
        /// <returns>Linked point</returns>
        internal static LinkedPoint CreateIntroducedPoint(LatLng point, LinkedPoint nextPoint)
        {
            LinkedPoint result = new LinkedPoint(point, nextPoint);
            result._isIntroduced = true;
            return result;
        }

        /// <summary>
        /// Sets next point this in list
        /// </summary>
        /// <param name="nextPoint">Next point is list</param>
        private void SetNextPoint(LinkedPoint nextPoint)
        {
            next = nextPoint;
            distance = Utils.Distance(point, next.Point);

            if (null == originalNext)
            {
                // original point should be set just once - first time the mthod is invoked
                originalNext = next;
                originalDistance = distance;
            }
        }

        #region Equals

        /// <summary>
        /// Returns true if two linked points are equal
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if two linked points are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is LinkedPoint) return Equals(obj as LinkedPoint);

            return base.Equals(obj);
        }

        /// <summary>
        /// Returns true if two linked points are equal
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if two linked points are equal</returns>
        public bool Equals(LinkedPoint obj)
        {
            if (!Point.Equals(obj.Point)) return false;
            if (Next != obj.Next) return false;
            if (OriginalNext != obj.OriginalNext) return false;

            return true;
        }

        /// <summary>
        /// Generates Hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return point.GetHashCode();
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                "point: {0}, is introduced: {1}, Distance: {2}, OriginalDistance: {3}, is last: {4}",
                Point.ToString(),
                IsIntroduced,
                Distance,
                OriginalDistance,
                IsLast
                );
        }

    }
}
