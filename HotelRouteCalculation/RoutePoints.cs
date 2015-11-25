using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using MapUtils;
using MapTypes;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Encapsulate points definition
    /// </summary>
    public class RoutePoints
    {

        protected static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Rote object for given locations list
        /// </summary>
        public Route Route
        {
            get
            {
                return _route;
            }
        }
        private Route _route = null;

        /// <summary>
        /// Required accuracy values object
        /// </summary>
        public Proximity Proximity
        {
            get { return proximity; }
        }
        private Proximity proximity;

        /// <summary>
        /// Count of points in this route for all legs
        /// </summary>
        public int PointCount
        {
            get { return pointCount; }
        }
        private int pointCount = 0;

        /// <summary>
        /// Whether close point should be optimized
        /// Can help with opeformance while seeking the hotels, however might incerase inacuracy.
        /// </summary>
        public bool OptimizeClosePoints
        {
            get { return _optimizeClosePoints; }
            set { _optimizeClosePoints = value; }
        }
        private bool _optimizeClosePoints = false;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="route">Route to optimize points for</param>
        /// <param name="proximity">Required accuracy values object</param>
        public RoutePoints(Route route, Proximity proximity)
        {
            if (null == route) throw new ArgumentNullException("Argument 'route' cannot be null");
            this._route = route;
            this.proximity = proximity;
        }

        /// <summary>
        /// Build route point list
        /// </summary>
        public void BuildRoutePoints()
        {
            OptimizePoints();
            CalculateCount();
        }

        /// <summary>
        /// Iterates through points and
        ///  - add new points to segments with big distance between them (right linear segments)
        ///  - removes point for very intensive segments
        /// </summary>
        private void OptimizePoints()
        {
            foreach (LinkedPoint startPoint in Route.RouteLegsStart)
            {
                LinkedPoint currentPoint = startPoint;

                while (!currentPoint.IsLast)
                {
                    if (currentPoint.Distance > Proximity.Step)
                    {
                        // distance between points is higher requred step to satisfy accuracy
                        Log.DebugFormat("Start introducing via points for point {0}", currentPoint);
                        IntroduceViaPoints(currentPoint);
                        if (currentPoint.Distance > Proximity.Step)
                        {
                            string errorMsg = string.Format(
                                "Logical error: after making attempt to insert points distance still remain bigger than required step. Distance: {0}, step: {1}",
                                currentPoint.Distance, Proximity.Step
                                );
                            throw new InvalidOperationException(errorMsg);
                        }
                    }
                    /*
                     * Since load balancing method does not search hotels for each single point but do verify if hotel is in proximity
                     * there is no need to optimize calculation of close points. Also removing close points might lead to 
                     * accuracy mistakes. 
                     */
                    if (OptimizeClosePoints && IsClose(currentPoint))
                    {
                        Log.DebugFormat("Start optimizing close points for point {0}", currentPoint);
                        DoOptimizeClosePoints(currentPoint);
                        if (currentPoint.Distance > Proximity.Step)
                        {
                            string errorMsg = string.Format(
                                "Error in logic: new point distance is bigger than step defined. Distance: {0}, step: {1}",
                                currentPoint.Distance, Proximity.Step
                                );
                            throw new InvalidOperationException(errorMsg);
                        }

                        Debug.Assert(CheckSequencePreservedForDeletedPoints(currentPoint), "Close point optimization failed. We cannot reach looping by OriginalNext to Next");
                    }
                     
                    currentPoint = currentPoint.Next;
                }
            }
        }

        /// <summary>
        /// Calculates count of points
        /// </summary>
        private void CalculateCount()
        { 
            foreach(LinkedPoint start in this.Route.RouteLegsStart)
            {
                int legPointsCount = 1;
                LinkedPoint p = start;
                while (!p.IsLast)
                {
                    legPointsCount += 1;
                    p = p.Next;
                }
                pointCount += legPointsCount;
            }
        }

        /// <summary>
        /// If the point and following point are close to each other
        /// </summary>
        /// <param name="point">Point to check if this and following point are to close.</param>
        /// <returns>if point are very close to each other</returns>
        private bool IsClose(LinkedPoint point)
        {
            const double ACCURACY_DIVIDER = 2.5;
            return point.Distance < Proximity.Step / ACCURACY_DIVIDER;
        }

#if DEBUG
        
        /// <summary>
        /// This method is for debug purpose. Used for cases where close point optimization happened.
        /// It checks whether OrininalNext sequence still lead to next point
        /// </summary>
        /// <param name="point">Point optimized</param>
        /// <returns>Whether data is preserved fine and we would still reach by OriginalNext to Next point</returns>
        private bool CheckSequencePreservedForDeletedPoints(LinkedPoint point)
        {
            LinkedPoint p = point.OriginalNext;
            while (p != point.Next)
            {
                if (null == p)
                {
                    LinkedPoint temp = point.OriginalNext;
                    int counter = 0;
                    while (temp != p)
                    {
                        Log.DebugFormat("point #{0} {1}", counter++, temp);
                        temp = temp.OriginalNext;
                    }
                    Debug.Assert(null != p, "Parameter p is not expected to be null");
                    return false;
                }

                Debug.Assert(null != p.Point, "Point field cannot be null");
                p = p.OriginalNext;
            }

            return true; // since we reach here at some point OriginalNext led to Next field of point specified.
        }
#endif

        /// <summary>
        /// Optimizes close points by removing some of them 
        /// </summary>
        /// <param name="start">Starting point</param>
        private void DoOptimizeClosePoints(LinkedPoint start)
        {
            List<LinkedPoint> closePoints = new List<LinkedPoint>();
            LinkedPoint finishPoint = start.Next, nextPointToCheck = start.Next;

            int counter = 0;
            /*
             * warning - check if nextPointToCheck.IsIntroduced is very improtant since in case if short cut from original point to introduce original point loop might be broken.
             */
            while (!nextPointToCheck.IsLast && MapTypes.DistanceUtils.Distance(start.Point, nextPointToCheck.Point) < Proximity.Step && !nextPointToCheck.IsIntroduced)
            {
                Log.DebugFormat(
                    "\t\tclose point #{0} <{1}>, distance from start point to nextPointToCheck (<{3}>) {2}",
                    counter++, finishPoint, MapTypes.DistanceUtils.Distance(start.Point, nextPointToCheck.Point), nextPointToCheck
                    );

                finishPoint = nextPointToCheck;
                closePoints.Add(finishPoint);
                nextPointToCheck = finishPoint.Next;
            }
            
            if (finishPoint == start.Next) return;

            /* 
             * TBD: identify any optimization mechanism
             * right now two poins simply connected and all the via points are ignored
             * but it might happed that via points might lay away of line that connects
             * start and finish points
             */

            start.Next = finishPoint;
            Log.DebugFormat("\tafter optimization, start point {0}, finish {1}", start, finishPoint);
        }

        /// <summary>
        /// Introduces Via points starting from give point in order 
        /// to satisfy accuracy during calculation
        /// </summary>
        /// <param name="start">Starting point</param>
        private void IntroduceViaPoints(LinkedPoint start)
        {
            int requiredSegmentsCount = (int)Math.Ceiling(start.Distance / Proximity.Step);

            /* for simplicity sake Lat/Long considered as X,Y in Decartes coordinates
             * There is a general solution:
             *      x = p1.x + blend * (p2.x - p1.x);
             *      y = p1.y + blend * (p2.y - p1.y);
             * where blend is the percentage between the two points you want. 
             * So if you want halfway blend = 0.5
             * */

            double blend = 1 / (double)requiredSegmentsCount;
            if (0 == blend) throw new InvalidOperationException("Blend cannot be null. Error in applocation logic.");
 
            double x1 = start.Point.Latitude;
            double y1 = start.Point.Longitude;
            LinkedPoint finish = start.Next;
            double x2 = finish.Point.Latitude;
            double y2 = finish.Point.Longitude;

            LinkedPoint currentPoint = start;
            for (int i = 0; i < requiredSegmentsCount - 1; ++i)
            {
                double x = x1 + blend * (i + 1) * (x2 - x1);
                double y = y1 + blend * (i + 1) * (y2 - y1);
                LatLng mapPoint = new LatLng(x, y);

                double distanceBeforeInsert = currentPoint.Distance;
                // insert point in the middle and update the links
                LinkedPoint point = LinkedPoint.CreateIntroducedPoint(mapPoint, finish);
                currentPoint.Next = point;
                if (distanceBeforeInsert <= currentPoint.Distance) throw new InvalidOperationException("Logic in points optimization After making attempt to insert points in the middle distance between points grown or stay unchanged");

                Log.DebugFormat("\tintroduced point #{0} {1}", i, point);

                currentPoint = currentPoint.Next;
            }
        }
 

    }
}
