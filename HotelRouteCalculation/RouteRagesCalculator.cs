using MapUtils;
using CalculationUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRouteCalculation
{
    /// <summary>
    /// The class is used to calculate ranges for hotel selection
    /// Calculate range occupied by provided maximum distance betyween points 
    /// </summary>
    class RouteRangesCalculator
    {
        /// <summary>
        /// Route leg start point
        /// </summary>
        private LinkedPoint LegStart;

        /// <summary>
        /// Maximum distance between points (meters).
        /// </summary>
        private int MaxDistance;

        /// <summary>
        /// Search radius to seek (meters).
        /// </summary>
        private List<RouteRange> Ranges;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="legStart">Route leg start point</param>
        /// <param name="maxDistance">Maximum distance between points (meters) .</param>
        public RouteRangesCalculator(LinkedPoint legStart, int maxDistance)
        {
            if (null == legStart) throw new ArgumentNullException("Argument legStart cannot be null.");
            if (maxDistance <= 0) throw new ArgumentNullException("Argument maxDistance should be bigger than zero.");

            this.LegStart = legStart;
            this.MaxDistance = maxDistance;
        }

        /// <summary>
        /// Calculates ranges
        /// </summary>
        /// <returns>List of ranges calculated.</returns>
        public RouteRange[] Calculate()
        {
            Ranges = new List<RouteRange>();

            CalculateInt();

            return Ranges.ToArray();
        }

        /// <summary>
        /// Calculates ranges 
        /// </summary>
        private void CalculateInt()
        {
            LinkedPoint start = LegStart;

            RouteRange range = null;
            do
            {
                range = CalculateNextRange(start);
                if (null != range)
                {
                    Ranges.Add(range); // out of range exception!
                    start = range.Finish;
                }
            }
            while (null != range);
        }

        /// <summary>
        /// Calculates next range from Start point specified.
        /// Ensures that start and finish points can reach each other.
        /// </summary>
        /// <param name="start">Range start point</param>
        /// <returns>Route range or null if no more ranges</returns>
        private RouteRange CalculateNextRange(LinkedPoint start)
        {
            if (null == start) return null;
            if (start.IsLast) return null;

            LinkedPoint current = start;
            LinkedPoint next = current.Next;

            while (null != next) // IsLast property is not good work in this case as we still need to check the last one as well
            {
                double distance = Distance(start, next); // distance from start
                if (distance > MaxDistance)
                {
                    /* we have indentified that current point is last reachalbed from start
                     * so start point can reacv all till current
                     * now we need to find backward reachability
                     * so what last point can stil reach all the points from star to that point
                     */
                    current = FindBackwardCompatibilityPoint(start, current);
                    break;
                }

                current = next;
                next = current.Next;
            }

            if (current == start) return null;

            return new RouteRange(start, current);
        }

        /// <summary>
        /// Seeks and return point that can be compatible from back.
        /// This method ensure that forward compatibility is verified in CalculateNextRange method
        /// </summary>
        /// <param name="start">Start point in range</param>
        /// <param name="last">Last point in range</param>
        /// <returns>Last point that still can reach all points from the begining</returns>
        private LinkedPoint FindBackwardCompatibilityPoint(LinkedPoint start, LinkedPoint last)
        {
            LinkedPoint current = start;
            LinkedPoint next = current.Next;
            
            while (next != last) // IsLast property is not good work in this case as we still need to check the last one as well
            {
                double distance = Distance(last, current); // distance from last
                if (distance > MaxDistance)
                {
                    /* Identified point that is not reachable from last one
                     * so now we need to find the one preeceeding that can be reached all points
                     * from the begining
                     * 
                     * TBD - cover this with unit test as there would be hardly to find case that would match this peice
                     * however this is left just in case, but ieven in that case it must be tested.
                     */
                    LinkedPoint prior = next; // seek point prior to last. 
                    while (prior.Next != last)
                    {
                        prior = prior.Next;
                    }

                    return FindBackwardCompatibilityPoint(start, prior);
                }

                current = next;
                next = current.Next;
            }

            return last; // since we reached here all the point are eigher backward and forward reachable

        }

        /// <summary>
        /// Calculates distance between points
        /// </summary>
        /// <param name="start">Start point</param>
        /// <param name="finish">finish point</param>
        /// <returns>Distance</returns>
        private double Distance(LinkedPoint start, LinkedPoint finish)
        {
            return DistanceUtils.Distance(start.Point.Latitude, start.Point.Longitude, finish.Point.Latitude, finish.Point.Longitude);
        }
    }
}
