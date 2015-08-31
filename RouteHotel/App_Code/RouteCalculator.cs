using HotelInterface.TO;
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
        /// Type of route hotel search
        /// </summary>
        const SearchType SEARCH_TYPE = SearchType.LoadBalancing;

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
        /// Hotels search object
        /// </summary>
        public IRouteHotelSearch HotelSearch
        {
            get { return hotelSearch; }
        }
        private IRouteHotelSearch hotelSearch;

        /// <summary>
        /// Route object
        /// </summary>
        public GoogleDirections.Route Route
        {
            get { return HotelSearch.Route; }
        }

        /// <summary>
        /// Whether search are in progress
        /// </summary>
        public bool SearchInProgress
        {
            get { return searchInProgress; }
        }
        private bool searchInProgress;

        /// <summary>
        /// Current search progress
        /// </summary>
        public int CurrentProgress
        {
            get { return hotelSearch.CurrentProgress; }
        }

        /// <summary>
        /// Count of calculatin points in current search
        /// </summary>
        public int PointCount
        {
            get { return hotelSearch.RoutePoints.PointCount; }
        }

        /// <summary>
        /// Generates new ID for Route calculator
        /// </summary>
        /// <returns>ID</returns>
        private static Guid GenerateID()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="routeParams">Route search parameters</param>
        /// <param name="hotelParameters">Represents hotel search criterias</param>
        public RouteCalculator(RouteParams routeParams)
        {
            if (null == routeParams) throw new ArgumentNullException("Argument routeParams cannot be null");
            if (null == routeParams.HotelParameters) throw new ArgumentNullException("Field HotelParameters cannot be null");

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
            hotelSearch = HoteSearchFactory.CreateSearch(SEARCH_TYPE, route, proximity, Params.HotelParameters);
            hotelSearch.Search();
            hotelSearch.Progress += hotelSearch_Progress;
        }

        void hotelSearch_Progress(object sender, CalculationStatusEventArgs e)
        {
            searchInProgress = !e.Finished;
        }

    }
}