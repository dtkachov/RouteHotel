using HotelRouteCalculation;
using MapUtils;
using RouteHotel.TransportObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteHotel
{
    /// <summary>
    /// Route calculator - takes care of hotels calculation
    /// </summary>
    public class RouteCalculator
    {
        /// <summary>
        /// ID of calculatory
        /// </summary>
        public Guid ID { get { return _ID; } }
        private Guid _ID = GenerateID();

        /// <summary>
        /// Route search parameters
        /// </summary>
        private RouteParams Params;

        /// <summary>
        /// Search object
        /// </summary>
        private RouteHotelSearch HotelSearchObject;

        /// <summary>
        /// Route object
        /// </summary>
        public GoogleDirections.Route Route
        {
            get { return HotelSearchObject.Route; }
        }

        /// <summary>
        /// Whether search are in progress
        /// </summary>
        public bool SearchInProgress
        {
            get { return searchInProgress; }
        }
        public bool searchInProgress;

        /// <summary>
        /// Generates new ID for Route calculator
        /// </summary>
        /// <returns>ID</returns>
        private static Guid GenerateID()
        {
            return new Guid();
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="routeParams">Route search parameters</param>
        public RouteCalculator(RouteParams routeParams)
        {
            if (null == routeParams) throw new ArgumentNullException("Argument routeParams cannot be null");

            this.Params = routeParams;
        }

        /// <summary>
        /// Starts route search
        /// When returns route is found, but hotel search execution is carried in background until search is marked as finished
        /// </summary>
        public void Search()
        {
            searchInProgress = true;

            GoogleDirections.Location[] locations = Location.ConvertLocations(Params.Locations);
            GoogleDirections.Route route = GoogleDirections.RouteDirections.GetRoute(Params.OptimizeRoute, locations);

            Proximity proximity = new Proximity(Params.ProximityRadius);
            HotelSearchObject = new RouteHotelSearch(route, proximity);

            SearchHotelsAsync();
        }

        /// <summary>
        /// Starts async route search
        /// </summary>
        private void SearchHotelsAsync()
        {
            // TODO
        }
    }
}