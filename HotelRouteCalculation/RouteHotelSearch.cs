using GoogleDirections;
using MapUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HotelRouteCalculation
{
    /// <summary>
    /// Seeks for hotel on a route
    /// </summary>
    public class RouteHotelSearch
    {
        /// <summary>
        /// Rote points object 
        /// </summary>
        public RoutePoints RoutePoints
        {
            get { return routePoints; }
        }
        private RoutePoints routePoints;

        /// <summary>
        /// Event notifying about search progress
        /// </summary>
        public event EventHandler<CalculationStatusEventArgs> Progress
        {
            add { _progress += new EventHandler<CalculationStatusEventArgs>(value); }
            remove { _progress -= value; }
        }
        private event EventHandler<CalculationStatusEventArgs> _progress;

        /// <summary>
        /// Hotels in current search
        /// </summary>
        private List<HotelData> Hotels = new List<HotelData>();

        /// <summary>
        /// Current progress
        /// </summary>
        private int currentProgress = 0;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="route">Route to optimize points for</param>
        /// <param name="proximity">Required accuracy values object</param>
        public RouteHotelSearch(Route route, Proximity proximity)
        {
            this.routePoints = new RoutePoints(route, proximity);
        }

        /// <summary>
        /// Search hotels for each point
        /// </summary>
        public void SearchHotels()
        {
            routePoints.BuildRoutePoints();

            foreach (LinkedPoint start in routePoints.LegsStart)
            {
                LinkedPoint p = start;
                while (null != p) // IsLast property is not good work in this case as we still need to check the last one as well
                {
                    SeachHotelsForPoint(p);
                    p = p.Next;
                }
                
            }
        }

        /// <summary>
        /// Search hotels for specific point
        /// </summary>
        /// <param name="point">Point to search hotels for</param>
        private void SeachHotelsForPoint(LinkedPoint point)
        {
            HotelSearch search = new HotelSearch(point);
            int newHotelsFound = search.Search();

            if (newHotelsFound > 0)
            {
                HotelData[] hotels = search.GetHotels();
                Debug.Assert(null != hotels);

                AddNewHotels(hotels);
            }

            IncreaseProgress();
        }

        /// <summary>
        /// Checks current hotel list and add only new ones
        /// </summary>
        /// <param name="hotels">Hotels found for some of locations</param>
        private void AddNewHotels(HotelData[] hotels)
        {
            // TODO - do comparision and add only new hotels
        }

        /// <summary>
        /// Notifies subsctribers about current calculation progress
        /// </summary>
        private void IncreaseProgress()
        {
            currentProgress += 1;

            if (null != _progress)
            {
                CalculationStatusEventArgs args = new CalculationStatusEventArgs(RoutePoints.PointCount, currentProgress);
                _progress(this, args);
            }
        }
        
    }
}
