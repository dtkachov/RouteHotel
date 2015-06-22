using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleDirections;
using System.Diagnostics;
using MapUtils;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Encapsulate points definition
    /// </summary>
    public class RoutePoints
    {
        /// <summary>
        /// List of locations to route fetch
        /// </summary>
        public Location[] Locations { get { return locations; } }
        private Location[] locations;

        /// <summary>
        /// Rote object for give locations list
        /// </summary>
        public Route Route
        {
            get
            {
                if (null == _route)
                {
                    _route = RouteDirections.GetRoute(true, locations.ToArray());
                }
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
        /// Links to first point of each leg in a route
        /// </summary>
        public List<LinkedPoint> LegsStart
        {
            get { return legsStart; }
        }
        private List<LinkedPoint> legsStart;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="locations">List of locations to route fetch</param>
        /// <param name="proximity">Required accuracy values object</param>
        public RoutePoints(Location[] locations, Proximity proximity)
        {
            if (null == locations) throw new ArgumentNullException("Argument 'locations' cannot be null");
            this.locations = locations;
            this.proximity = proximity;

            BuildInitialList();
            OptimizePoints();
        }

        /// <summary>
        /// Builds initial list of linked points.
        /// Simply parse route and add points to the list
        /// </summary>
        private void BuildInitialList()        
        {
            legsStart = new List<LinkedPoint>();

            for (int l = 0; l < Route.Legs.Length; ++l)
            {
                LinkedPoint startPoint = null;
                LinkedPoint prevPoint = null;

                RouteLeg leg = Route.Legs[l];
                for (int s = 0; s < leg.Steps.Length; ++s)
                {
                    RouteStep step = leg.Steps[s];
                    foreach (LatLng point in step.Points)
                    {
                        LinkedPoint currentPoint = new LinkedPoint(point);
                        if (null == startPoint)
                        {
                            // this is first point in Leg
                            startPoint = currentPoint;
                        }
                        else
                        {
                            // this is not the first point in list
                            prevPoint.Next = currentPoint;
                        }
                        prevPoint = currentPoint;
                    }
                }

                LegsStart.Add(startPoint);
            }
        }

        /// <summary>
        /// Iterates through points and
        ///  - add new points to segments with big distance between them (right linear segments)
        ///  - removes point for very intensive segments
        /// </summary>
        private void OptimizePoints()
        {
            foreach (LinkedPoint startPoint in LegsStart)
            {
                LinkedPoint currentPoint = startPoint;

                while (!currentPoint.IsLast)
                {
                    if (currentPoint.Distance > Proximity.Step)
                    {
                        // distance between points is higher requred step to satisfy accuracy
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
                    if (IsClose(currentPoint))
                    {
                        OptimizeClosePoints(currentPoint);
                        if (currentPoint.Distance > Proximity.Step)
                        {
                            string errorMsg = string.Format(
                                "Error in logic: new point distance is bigger than step defined. Distance: {0}, step: {1}",
                                currentPoint.Distance, Proximity.Step
                                );
                            throw new InvalidOperationException(errorMsg);
                        }
                    }
                    currentPoint = currentPoint.Next;
                }
            }
        }

        /// <summary>
        /// If the point and following point are close to each other
        /// </summary>
        /// <param name="point">Point to check if this and following point are to close.</param>
        /// <returns>if point are very close to each other</returns>
        private bool IsClose(LinkedPoint point)
        {
            return point.Distance < Proximity.Step / 2.5;
        }

        /// <summary>
        /// Optimizes close points by removing some of them 
        /// </summary>
        /// <param name="start">Starting point</param>
        private void OptimizeClosePoints(LinkedPoint start)
        {
            List<LinkedPoint> closePoints = new List<LinkedPoint>();
            LinkedPoint currentPoint = start;
            double distance = start.Distance;

            while (!currentPoint.IsLast && (distance + currentPoint.Distance) < Proximity.Step)
            {
                currentPoint = currentPoint.Next;
                distance = Utils.Distance(start.Point, currentPoint.Point);
                closePoints.Add(currentPoint);
            }

            LinkedPoint finishPoint = currentPoint;
            if (start.Next == finishPoint) return;

            /* 
             * TBD: identify any optimization mechanism
             * right now two poins simply connected and all the via points are ignored
             * but it might happed that via points might lay away of line that connects
             * start and finish points
             */

            start.Next = finishPoint;
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
                LinkedPoint point = new LinkedPoint(mapPoint, finish);
                currentPoint.Next = point;
                if (distanceBeforeInsert <= currentPoint.Distance) throw new InvalidOperationException("Logic in points optimization After making attempt to insert points in the middle distance between points grown or stay unchanged");

                currentPoint = currentPoint.Next;
            }
        }
 

    }
}
